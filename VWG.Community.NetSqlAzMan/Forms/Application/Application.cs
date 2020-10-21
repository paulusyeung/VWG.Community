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

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    public partial class Application : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;

        #region public properties
        private bool _Dirty = false;
        public bool Dirty
        {
            get { return _Dirty; }
            set { _Dirty = value; }
        }
        #endregion

        public Application()
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
            if (Session["selectedObject"] as IAzManApplication != null)
            {
                _Application = this.Session["selectedObject"] as IAzManApplication;
                _Store = _Application.Store;
            }

            _Mode = (_Application == null) ? Mode.Create : Mode.Update;

            SetAttributes();
            SetToolBar();

            if (_Mode == Mode.Update) LoadRecord();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Store;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            switch (_Mode)
            {
                case Mode.Create:
                    Text = "Create a New Application";
                    break;
                case Mode.Update:
                    Text = "Edit Application - " + _Application.Name;
                    break;
            }
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
                toolbar.Buttons.Add(sep);
                toolbar.Buttons.Add(cmdPermissions);
            }
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        #region Load Save Delete Record
        private void LoadRecord()
        {
            txtName.Text = _Application.Name;
            txtDescription.Text = _Application.Description;
        }

        private void SaveRecord()
        {
            try
            {
                switch (_Mode)
                {
                    case Mode.Create:
                        _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                        _Application = _Store.CreateApplication(txtName.Text.Trim(), txtDescription.Text);
                        _Storage.CommitTransaction();
                        break;
                    case Mode.Update:
                        _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                        _Application.Rename(this.txtName.Text.Trim());
                        _Application.Update(this.txtDescription.Text.Trim());
                        _Storage.CommitTransaction();
                        break;
                }
                _Dirty = true;
            }
            catch (Exception ex)
            { }
        }

        private void DeleteRecord()
        {
            try
            {
                _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                _Application.Delete();
                _Storage.CommitTransaction();
                _Dirty = true;
            }
            catch { }
        }
        #endregion

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
                        DeleteRecord();
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
    }
}