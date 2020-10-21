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
    public partial class AttributeItem : Form
    {
        private IAzManStorage _Storage;
        private IAzManStore _Store;
        private IAzManStoreGroup _StoreGroup = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _AuthItem = null;
        private IAzManAuthorization _Authorization = null;
        private String _DisplayName = "";

        #region public properties: AuthorizationID
        private Mode _Mode = Mode.Read;
        public Mode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        private int _AttributeID;
        public int AttributeID
        {
            get { return _AttributeID; }
            set { _AttributeID = value; }
        }

        private String _AttributeKey;
        public String AttributeKey
        {
            get { return _AttributeKey; }
            set { _AttributeKey = value; }
        }

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

        public AttributeItem()
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

                _Authorization = _AuthItem.GetAuthorization(_AuthorizationID);
                MemberType memberType = _Authorization.GetMemberInfo(out _DisplayName);
            }
            if (Session["selectedObject"] as IAzManApplicationGroup != null)
            {
                _ApplicationGroup = Session["selectedObject"] as IAzManApplicationGroup;
                _Application = _ApplicationGroup.Application;
                _Store = _Application.Store;
            }

            SetAttributes();
            SetToolBar();

            if (_Mode == Mode.Update) LoadRecord();

            txtKey.Focus();
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            switch (_Mode)
            {
                case Mode.Create:
                    Text = "Add Attribute";
                    break;
                default:
                    Text = String.Format("Attribute - {0}", _DisplayName);
                    txtKey.Enabled = false; // 唔可以改 Key
                    break;
            }

        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdSave cmdDelete
            var cmdSave = new ToolBarButton("Save", ("Save"));
            cmdSave.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-check-circle-outline.png");

            toolbar.Buttons.Add(cmdSave);
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void LoadRecord()
        {
            var item = _Authorization.GetAttribute(_AttributeKey);
            txtKey.Text = item.Key;
            txtValue.Text = item.Value;
        }

        private void SaveRecord()
        {
            var key = txtKey.Text.Trim();
            var value = txtValue.Text.Trim();

            if (key != String.Empty && value != String.Empty)
            {
                _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);

                switch (_Mode)
                {
                    case Mode.Create:
                        _Authorization.CreateAttribute(key, value);
                        break;
                    case Mode.Delete:
                        break;
                    case Mode.Update:
                        _Authorization.GetAttribute(key).Update(key, value);
                        break;
                }

                _Storage.CommitTransaction();

                _Dirty = true;
            }
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
                        Close();
                        break;
                }
            }
        }
    }
}