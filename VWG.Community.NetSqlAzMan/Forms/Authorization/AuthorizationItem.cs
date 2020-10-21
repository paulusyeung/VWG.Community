#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using static VWG.Community.NetSqlAzMan.Helper.Enums;
using NetSqlAzMan.Interfaces;
using Gizmox.WebGUI.Common.Resources;
using VWG.Community.NetSqlAzMan.Helper;
using NetSqlAzMan;

#endregion

namespace VWG.Community.NetSqlAzMan.Forms.Authorization
{
    public partial class AuthorizationItem : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage;
        private IAzManStore _Store;
        private IAzManStoreGroup _StoreGroup = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _AuthItem = null;

        #region public properties: AuthorizationID
        private int _AuthorizationID;
        public int AuthorizationID
        {
            get { return _AuthorizationID; }
            set { _AuthorizationID = value; }
        }

        private bool _Dirty = false;
        public bool Dirty
        {
            get { return _Dirty; }
            set { _Dirty = value; }
        }
        #endregion

        public AuthorizationItem()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _Storage = (IAzManStorage)Session["storage"];

            if (Session["selectedObject"] as IAzManStore != null)
            {
                _Store = Session["selectedObject"] as IAzManStore;
            }
            if (Session["selectedObject"] as IAzManApplication != null)
            {
                _Application = Session["selectedObject"] as IAzManApplication;
                _Store = _Application.Store;
            }
            if (Session["selectedObject"] as IAzManItem != null)
            {
                _AuthItem = Session["selectedObject"] as IAzManItem;
                _Application = _AuthItem.Application;
                _Store = _Application.Store;
            }
            if (Session["selectedObject"] as IAzManApplicationGroup != null)
            {
                _ApplicationGroup = Session["selectedObject"] as IAzManApplicationGroup;
                _Application = _ApplicationGroup.Application;
                _Store = _Application.Store;
            }

            SetAttributes();
            SetToolBar();

            LoadRecord();
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            Text = "Item Authorization - " + _AuthItem.Name;

            txtMemberType.Enabled = false;
            txtName.Enabled = false;
            txtWhereDefined.Enabled = false;
            txtOwner.Enabled = false;

            #region 用 Key Pairs 砌 Authorization Type combo box
            Dictionary<int, String> ddlist = Enum.GetValues(typeof(AuthorizationType))
                .Cast<AuthorizationType>()
                .ToDictionary(k => (int)k, v => v.ToString());
            var ddl = ddlist.OrderBy(x => x.Value).ToList();
            
            cboAuthType.DataSource = ddl;
            cboAuthType.DisplayMember = "value";
            cboAuthType.ValueMember = "key";
            #endregion

            #region datetime picker 效果：未選 => 吉嘅，選咗 => yyyy-MM-dd
            datValidFrom.Checked = false;
            datValidFrom.CustomFormat = @" ";
            datValidFrom.Format = DateTimePickerFormat.Custom;
            datValidFrom.ShowCheckBox = true;

            datValidTo.Checked = false;
            datValidTo.CustomFormat = @" ";
            datValidTo.Format = DateTimePickerFormat.Custom;
            datValidTo.ShowCheckBox = true;
            #endregion

            datValidFrom.CheckedChanged += DateTiemPicker_CheckedChanged;
            datValidTo.CheckedChanged += DateTiemPicker_CheckedChanged;

        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdSave cmdDelete
            var cmdSave = new ToolBarButton("Save", ("Save"));
            cmdSave.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-check-circle-outline.png");

            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");

            var cmdAttributes = new ToolBarButton("Attributes", ("Attributes"));
            cmdAttributes.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdPermissions = new ToolBarButton("Permissions", ("Permissions"));
            cmdPermissions.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            toolbar.Buttons.Add(cmdSave);
            toolbar.Buttons.Add(sep);
            if (_Mode == Mode.Update)
            {
                toolbar.Buttons.Add(cmdDelete);
                toolbar.Buttons.Add(sep);
                toolbar.Buttons.Add(cmdAttributes);
                toolbar.Buttons.Add(cmdPermissions);
            }
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void LoadRecord()
        {
            string displayName;
            var item = _AuthItem.GetAuthorization(_AuthorizationID);
            MemberType memberType = item.GetMemberInfo(out displayName);

            var authType = NetSqlAzManHelper.GetAuthTypeName(item.AuthorizationType);

            txtMemberType.Text = NetSqlAzManHelper.GetMemberTypeName(memberType, item.SID, _AuthItem);
            txtName.Text = displayName;
            txtWhereDefined.Text = item.SidWhereDefined.ToString();
            txtOwner.Text = item.Owner.ToString();

            for (int i = 0; i < cboAuthType.Items.Count; i++)
            {
                var keypair = ((KeyValuePair<int, String>)cboAuthType.Items[i]);
               
                if (keypair.Value == authType.ToString())
                {
                    cboAuthType.SelectedItem = cboAuthType.Items[i];
                }
            }

            if (item.ValidFrom != null)
            {
                datValidFrom.Checked = true;
                datValidFrom.Value = (DateTime)item.ValidFrom;
            }
            if (item.ValidTo != null)
            {
                datValidTo.Checked = true;
                datValidTo.Value = (DateTime)item.ValidTo;
            }
        }

        private void SaveRecord()
        {
            var keypair = (KeyValuePair<int, String>)cboAuthType.SelectedItem;
            var authType = (AuthorizationType)Enum.Parse(typeof(AuthorizationType), keypair.Value);

            DateTime? validFrom = null;
            if (datValidFrom.Checked) validFrom = datValidFrom.Value;
            DateTime? validTo = null;
            if (datValidFrom.Checked) validTo = datValidTo.Value;

            _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);

            var item = _AuthItem.GetAuthorization(_AuthorizationID);
            item.Update(item.Owner, item.SID, item.SidWhereDefined,
                authType,
                validFrom,
                validTo
                );

            _Storage.CommitTransaction();

            _Dirty = true;
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "save":
                        SaveRecord();
                        Close();
                        break;
                    case "delete":
                        //PrintManager print = new PrintManager();
                        //print.ShowDialog();
                        Close();
                        break;
                }
            }
        }

        private void DateTiemPicker_CheckedChanged(object sender, EventArgs e)
        {
            var thisDateTimePicker = (DateTimePicker)sender;
            if (thisDateTimePicker.Checked == false)
            {
                thisDateTimePicker.CustomFormat = @" ";
                thisDateTimePicker.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                thisDateTimePicker.CustomFormat = @"yyyy-MM-dd";
                thisDateTimePicker.Format = DateTimePickerFormat.Custom;
            }
        }
    }
}