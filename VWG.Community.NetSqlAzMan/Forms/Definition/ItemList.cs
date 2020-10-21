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

namespace VWG.Community.NetSqlAzMan.Forms.Definition
{
    public partial class ItemList : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManStoreGroup _StoreGroup = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _AuthItem = null;
        private DataTable _AuthData;
        private WhereDefined _CurrentOwnerSidWhereDefined = WhereDefined.Local;

        #region public properties
        private ItemType _ItemType = ItemType.Role;
        public ItemType ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }

        private List<ListViewItem> _SelectedItems = new List<ListViewItem>();
        public List<ListViewItem> SelectedItems
        {
            get { return _SelectedItems; }
            set { _SelectedItems = value; }
        }
        #endregion

        public ItemList()
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
            SetListView();

            LoadList();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _AuthItem;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;
            lvwItemList.Dock = DockStyle.Fill;
            lvwItemList.GridLines = true;

            switch (_ItemType)
            {
                case ItemType.Role:
                    Text = "Add Role - " + _AuthItem.Name; break;
                case ItemType.Task:
                    Text = "Add Task - " + _AuthItem.Name; break;
                case ItemType.Operation:
                    Text = "Add Operation - " + _AuthItem.Name; break;
            }

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
                Width = 200
            };
            var colItemAuthType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Description",
                Width = 200
            };
            var colItemType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "ID",
                TextAlign = HorizontalAlignment.Center,
                Width = 100
            };
            /**
            var colWhereDefined = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Where Defined",
                TextAlign = HorizontalAlignment.Center,
                Width = 100
            };
            var colOwnerd = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Owner",
                //TextAlign = HorizontalAlignment.Center,
                Width = 280
            };
            var colValidOn = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Valid From",
                TextAlign = HorizontalAlignment.Center,
                Width = 80
            };
            var colValidTo = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Valid To",
                TextAlign = HorizontalAlignment.Center,
                Width = 80
            };
            */

            lvwItemList.Columns.AddRange(new ColumnHeader[] {
                colItemName, colItemAuthType, colItemType
            });
            #endregion

            lvwItemList.BackColor = Color.Transparent;
            //listView.BorderStyle = BorderStyle.None;
            lvwItemList.CheckBoxes = true;
            lvwItemList.ListViewItemSorter = new ListViewItemSorter(lvwItemList);
            lvwItemList.Dock = DockStyle.Fill;
            lvwItemList.GridLines = true;
            lvwItemList.MultiSelect = true;
            lvwItemList.SelectedIndexChanged += lvwItemList_SelectedIndexChanged;
            //            lvwAuth.DoubleClick += lvwAuth_DoubleClick;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvwItemList.Tag = new Guid("E1B1778B-4DD6-454D-98F4-FF3E44DE3F68");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        private void lvwItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = item.Selected ? true : false;
            }
        }

        private void SaveRecord()
        {
            _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
            foreach (ListViewItem item in lvwItemList.Items)
            {
                #region update checked items only, not selected
                if (item.Checked)
                {
                    _SelectedItems.Add(item);

                    #region 立即 save
                    try
                    {
                        IAzManItem member = _Application.GetItem(item.SubItems[0].Text);

                        _AuthItem.AddMember(member);
                    }
                    catch (Exception ex)
                    {
                        //throw (ex);
                        MessageBox.Show(ex.Message, "Error Found", MessageBoxButtons.OK, MessageBoxIcon.Error, new EventHandler(ErrorPrompt));
                        //MessageBox.Show(ex.Message, "Error Found");
                    }
                    #endregion
                }
                #endregion
            }
            _Storage.CommitTransaction();
            MessageBox.Show("Error Found");
        }

        private void ErrorPrompt(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.OK)
            {

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
                    case "delete":
                        //PrintManager print = new PrintManager();
                        //print.ShowDialog();
                        Close();
                        break;
                }
            }
        }

        #region lvwAuth
        private void LoadList()
        {
            lvwItemList.Items.Clear();

            IAzManItem[] members = _Application.GetItems(_ItemType);
            foreach (IAzManItem member in members)
            {
                if ((_AuthItem == null) || (_AuthItem != null && member.Name != _AuthItem.Name))
                {
                    ListViewItem oItem = lvwItemList.Items.Add(member.Name);
                    oItem.SubItems.Add(member.Description);
                    oItem.SubItems.Add(member.ItemId.ToString());
                }
            }

            lvwItemList.Sort();
        }
        #endregion
    }
}