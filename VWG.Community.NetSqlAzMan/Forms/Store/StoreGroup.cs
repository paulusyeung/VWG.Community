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

namespace VWG.Community.NetSqlAzMan.Forms.Store
{
    public partial class StoreGroup : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManStoreGroup _StoreGroup = null;

        #region public properties
        private bool _IsDirty = false;
        public bool IsDirty
        {
            get { return _IsDirty; }
            set { _IsDirty = value; }
        }
        #endregion

        public StoreGroup()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _Storage = (IAzManStorage)Session["storage"];

            if (Session["selectedObject"] as IAzManStore != null)
            {
                _Store = this.Session["selectedObject"] as IAzManStore;
            }
            if (Session["selectedObject"] as IAzManStoreGroup != null)
            {
                _StoreGroup = this.Session["selectedObject"] as IAzManStoreGroup;
                _Store = _StoreGroup.Store;
            }

            _Mode = (_StoreGroup == null) ? Mode.Create : Mode.Update;

            SetAttributes();
            SetToolBar();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Store;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            Text = "Create a New Store Group";
            txtName.Focus();

            this.FormClosed += Form_Close;
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
                }
            }
        }

        private void SaveRecord()
        {
            try
            {
                _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);

                IAzManStoreGroup storeGroup = _Store.CreateStoreGroup(
                    SqlAzManSID.NewSqlAzManSid(),
                    txtName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    String.Empty,
                    (radBasic.Checked ? GroupType.Basic : GroupType.LDapQuery));

                _Storage.CommitTransaction();
                _IsDirty = true;
            }
            catch (Exception ex)
            {
                _Storage.RollBackTransaction();
                throw ex;
            }
        }
    }
}