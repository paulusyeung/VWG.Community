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

#endregion

namespace VWG.Community.NetSqlAzMan.Forms.Store
{
    public partial class Store : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage;
        private IAzManStore _Store;
        private IAzManStoreGroup _StoreGroup = null;

        public Store()
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
            //_Store = (IAzManStore)Session["selectedObject"];

            _Mode = (_Store == null) ? Mode.Create : Mode.Update;

            SetAttributes();
            SetToolBar();

            if (_Mode == Mode.Update) LoadRecord();
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            switch (_Mode)
            {
                case Mode.Create:
                    Text = "Create a New Store";
                    break;
                case Mode.Update:
                    Text = "Edit Store - " + _Store.Name;
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

            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");

            var cmdMember = new ToolBarButton("Attributes", ("Attributes"));
            cmdMember.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdNonMember = new ToolBarButton("Permissions", ("Permissions"));
            cmdNonMember.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            toolbar.Buttons.Add(cmdSave);
            toolbar.Buttons.Add(sep);
            if (_Mode == Mode.Update)
            {
                toolbar.Buttons.Add(cmdDelete);
                toolbar.Buttons.Add(sep);
                toolbar.Buttons.Add(cmdMember);
                toolbar.Buttons.Add(sep);
                toolbar.Buttons.Add(cmdNonMember);
            }
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void LoadRecord()
        {
            txtName.Text = _Store.Name;
            txtDescription.Text = _Store.Description;
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
                    case "attributes":
                        var attr = new Attributes();
                        attr.ShowDialog();
                        break;
                    case "permissions":
                        var permission = new Permissions();
                        permission.ShowDialog();
                        break;
                }
            }
        }

        private void SaveRecord()
        {
            switch (_Mode)
            {
                case Mode.Create:
                    _Store = _Storage.CreateStore(txtName.Text.Trim(), txtDescription.Text);
                    break;
                case Mode.Update:
                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                    _Store.Rename(this.txtName.Text.Trim());
                    _Store.Update(this.txtDescription.Text.Trim());
                    _Storage.CommitTransaction();
                    break;
            }
        }
    }
}