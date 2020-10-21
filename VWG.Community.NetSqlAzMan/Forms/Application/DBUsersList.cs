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

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    public partial class DBUsersList : Form
    {
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManStoreGroup _StoreGroup = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;

        #region public properties
        private bool _IsMember = true;
        public bool IsMember
        {
            get { return _IsMember; }
            set { _IsMember = value; }
        }

        private List<ListViewItem> _SelectedItems = new List<ListViewItem>();
        public List<ListViewItem> SelectedItems
        {
            get { return _SelectedItems; }
            set { _SelectedItems = value; }
        }
        #endregion

        public DBUsersList()
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
            if (Session["selectedObject"] as IAzManStoreGroup != null)
            {
                _StoreGroup = this.Session["selectedObject"] as IAzManStoreGroup;
                _Store = _StoreGroup.Store;
            }
            if (Session["selectedObject"] as IAzManApplication != null)
            {
                _Application = Session["selectedObject"] as IAzManApplication;
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

            LoadList();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _AuthItem;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;
            lvwStoreGroups.Dock = DockStyle.Fill;
            lvwStoreGroups.GridLines = true;

            Text = (_ApplicationGroup != null) ?
                 "Add DB Users - " + _ApplicationGroup.Name :
                 "Select DB Users - " + _Application.Name;

            this.FormClosed += Form_Close;
        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdSave cmdDelete
            var cmdSave = new ToolBarButton("Save", ((_ApplicationGroup != null) ? "Save" : "Select"));
            cmdSave.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-check-circle-outline.png");

            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");

            var cmdAddStoreGroups = new ToolBarButton("AddStoreGroups", ("Add Store Groups"));
            cmdAddStoreGroups.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdAddApplicationGroups = new ToolBarButton("AddApplicationGroups", ("Add Application Groups"));
            cmdAddApplicationGroups.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdAddDBUsers = new ToolBarButton("AddDBUsers", ("Add DB Users"));
            cmdAddDBUsers.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            toolbar.Buttons.Add(cmdSave);
            toolbar.Buttons.Add(sep);

            //toolbar.Buttons.Add(cmdDelete);
            //toolbar.Buttons.Add(sep);
            //toolbar.Buttons.Add(cmdAddStoreGroups);
            //toolbar.Buttons.Add(cmdAddApplicationGroups);
            //toolbar.Buttons.Add(cmdAddDBUsers);
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void SetListView()
        {
            #region define header
            var colLN = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "#",
                Width = 30
            };
            var colItemName = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                SortOrder = SortOrder.Ascending,
                SortPosition = 1,
                Text = "Name",
                Width = 80
            };
            var colItemAuthType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Custom SID",
                Width = 220
            };
            var colItemType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Type",
                //TextAlign = HorizontalAlignment.Center,
                Width = 100
            };
            var colLoginName = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Login Name",
                Width = 100
            };
            var colAlias = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Alias",
                Width = 80
            };

            lvwStoreGroups.Columns.AddRange(new ColumnHeader[] {
                colItemName, colItemAuthType, colItemType, colLoginName, colAlias
            });
            #endregion

            lvwStoreGroups.BackColor = Color.Transparent;
            //listView.BorderStyle = BorderStyle.None;
            lvwStoreGroups.CheckBoxes = true;
            lvwStoreGroups.ListViewItemSorter = new ListViewItemSorter(lvwStoreGroups);
            lvwStoreGroups.Dock = DockStyle.Fill;
            lvwStoreGroups.GridLines = true;
            lvwStoreGroups.MultiSelect = false;
//            lvwAuth.SelectedIndexChanged += lvwAuth_SelectedIndexChanged;
//            lvwAuth.DoubleClick += lvwAuth_DoubleClick;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvwStoreGroups.Tag = new Guid("A7F5BBDC-C625-414D-8A55-CF24DA09696A");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        private void SaveRecord()
        {
            try
            {
                _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                foreach (ListViewItem item in lvwStoreGroups.Items)
                {
                    #region update checked items only, not selected
                    if (item.Checked)
                    {
                        _SelectedItems.Add(item);

                        if (_ApplicationGroup != null)
                        {
                            #region 立即 save
                            IAzManDBUser dbUser = _Storage.GetDBUser(item.SubItems[0].Text);
                            _ApplicationGroup.CreateApplicationGroupMember(dbUser.CustomSid, WhereDefined.Database, _IsMember);
                            #endregion
                        }
                    }
                    #endregion
                }
                _Storage.CommitTransaction();
            }
            catch (Exception ex)
            {
                _Storage.RollBackTransaction();
                throw (ex);
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
                        //MessageBox.Show("Done!", onResponse);
                        Close();
                        break;
                }
            }
        }

        private void onResponse(object sender, EventArgs e)
        {
            Close();
        }

        #region lvwAuth
        private void LoadList()
        {
            SetListView();
            lvwStoreGroups.Items.Clear();

            IAzManDBUser[] dbUsers = _Store.GetDBUsers();
            foreach (IAzManDBUser dbUser in dbUsers)
            {
                ListViewItem oItem = lvwStoreGroups.Items.Add(dbUser.UserName);
                oItem.SubItems.Add(dbUser.CustomSid.ToString());
                oItem.SubItems.Add("Database User");
                foreach (var customColumn in dbUser.CustomColumns)
                {
                    if (customColumn.Value != null && customColumn.Value != DBNull.Value)
                    {
                        oItem.SubItems.Add(customColumn.Value.ToString());
                    }
                    else
                    {
                        oItem.SubItems.Add(String.Empty);
                    }
                }
            }

            lvwStoreGroups.Sort();
        }
        #endregion
    }
}