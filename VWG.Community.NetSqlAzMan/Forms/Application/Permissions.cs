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
    public partial class Permissions : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;
        //private IAzManApplicationGroup _ApplicationGroup = null;
        //private IAzManItem _Role = null;

        #region public properties
        private bool _Dirty = false;
        public bool Dirty
        {
            get { return _Dirty; }
            set { _Dirty = value; }
        }
        #endregion

        public Permissions()
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
            /**
            if (Session["selectedObject"] as IAzManItem != null)
            {
                _Role = Session["selectedObject"] as IAzManItem;
                _Application = _Role.Application;
                _Store = _Application.Store;
            }
            if (Session["selectedObject"] as IAzManApplicationGroup != null)
            {
                _ApplicationGroup = Session["selectedObject"] as IAzManApplicationGroup;
                _Application = _ApplicationGroup.Application;
                _Store = _Application.Store;
            }

            _Mode = (_Role == null) ? Mode.Create : Mode.Update;
            */

            SetAttributes();
            SetToolBar();

            LoadRecord();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Application;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            Text = "Edit Permission - " + _Application.Name;

            lblNotes.BackColor = Color.WhiteSmoke;
            lblNotes.Text = "Sql Logins with special permissions." + Environment.NewLine +
                "(Logins must be added before to Sql Roles:" + Environment.NewLine +
                "\"NetSqlAzMan_Managers\", \"NetSqlAzMan_Users\", \"NetSqlAzMan_Readers)" + Environment.NewLine +
                "Only checked Logins will be granted for this Application.";

            picManagers.Image = new IconResourceHandle(GlobalVars.Theme + ".48.mdi-account-tie.png");
            picUsers.Image = new IconResourceHandle(GlobalVars.Theme + ".48.mdi-account.png");
            picReaders.Image = new IconResourceHandle(GlobalVars.Theme + ".48.mdi-account-outline.png");

            flpManagers.BackColor = flpUsers.BackColor = flpReaders.BackColor = Color.WhiteSmoke;

            flpManagers.Focus();
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
                toolbar.Buttons.Add(cmdPermissions);
            }
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void LoadRecord()
        {
            #region Manager List
            flpManagers.Controls.Clear();
            foreach (KeyValuePair<string, bool> kvp in _Application.GetManagers())
            {
                CheckBox chk = new CheckBox()
                {
                    AutoEllipsis = true,
                    AutoSize = false,
                    Checked = kvp.Value,
                    Margin = new Padding(4, 4, 4, 4),
                    Text = kvp.Key,
                    Width = 180
                };
                flpManagers.Controls.Add(chk);
            }
            #endregion

            #region User List
            flpUsers.Controls.Clear();
            foreach (KeyValuePair<string, bool> kvp in _Application.GetUsers())
            {
                CheckBox chk = new CheckBox()
                {
                    AutoEllipsis = true,
                    AutoSize = false,
                    Checked = kvp.Value,
                    Margin = new Padding(4, 4, 4, 4),
                    Text = kvp.Key,
                    Width = 180
                };
                flpUsers.Controls.Add(chk);
            }
            #endregion

            #region Reader List
            flpReaders.Controls.Clear();
            foreach (KeyValuePair<string, bool> kvp in _Application.GetReaders())
            {
                CheckBox chk = new CheckBox()
                {
                    AutoEllipsis = true,
                    AutoSize = false,
                    Checked = kvp.Value,
                    Margin = new Padding(4, 4, 4, 4),
                    Text = kvp.Key,
                    Width = 180
                };
                flpReaders.Controls.Add(chk);
            }
            #endregion

            if (!_Application.IAmManager)
                flpManagers.Enabled = flpUsers.Enabled = flpReaders.Enabled = toolbar.Enabled = false;
            flpManagers.Enabled = _Application.IAmAdmin;
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
                _Storage.OpenConnection();
                _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);

                #region  Managers
                KeyValuePair<string, bool>[] managers = _Application.GetManagers();
                foreach (CheckBox sqlLogin in flpManagers.Controls)
                {
                    if (sqlLogin.Checked)
                    {
                        if (!FindLogin(managers, sqlLogin.Text))
                            _Application.GrantAccessAsManager(sqlLogin.Text);
                    }
                    else
                    {
                        if (FindLogin(managers, sqlLogin.Text))
                            _Application.RevokeAccessAsManager(sqlLogin.Text);
                    }
                }
                #endregion

                #region Users
                KeyValuePair<string, bool>[] users = _Application.GetUsers();
                foreach (CheckBox sqlLogin in flpUsers.Controls)
                {
                    if (sqlLogin.Checked)
                    {
                        if (!FindLogin(users, sqlLogin.Text))
                            _Application.GrantAccessAsUser(sqlLogin.Text);
                    }
                    else
                    {
                        if (FindLogin(users, sqlLogin.Text))
                            _Application.RevokeAccessAsUser(sqlLogin.Text);
                    }
                }
                #endregion

                #region Readers
                KeyValuePair<string, bool>[] readers = _Application.GetReaders();
                foreach (CheckBox sqlLogin in flpReaders.Controls)
                {
                    if (sqlLogin.Checked)
                    {
                        if (!FindLogin(readers, sqlLogin.Text))
                            _Application.GrantAccessAsReader(sqlLogin.Text);
                    }
                    else
                    {
                        if (FindLogin(readers, sqlLogin.Text))
                            _Application.RevokeAccessAsReader(sqlLogin.Text);
                    }
                }
                #endregion

                _Storage.CommitTransaction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _Storage.CloseConnection();
            }
            _Dirty = true;
        }

        private bool FindLogin(KeyValuePair<string, bool>[] logins, string login)
        {
            foreach (KeyValuePair<string, bool> l in logins)
            {
                if (l.Value && String.Compare(l.Key, login, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}