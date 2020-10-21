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

namespace VWG.Community.NetSqlAzMan.Forms.Definition
{
    public partial class OperationEdit : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _Operation = null;

        #region public properties
        private bool _Dirty = false;
        public bool Dirty
        {
            get { return _Dirty; }
            set { _Dirty = value; }
        }
        #endregion

        public OperationEdit()
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
                _Operation = Session["selectedObject"] as IAzManItem;
                _Application = _Operation.Application;
                _Store = _Application.Store;
            }
            if (Session["selectedObject"] as IAzManApplicationGroup != null)
            {
                _ApplicationGroup = Session["selectedObject"] as IAzManApplicationGroup;
                _Application = _ApplicationGroup.Application;
                _Store = _Application.Store;
            }

            _Mode = (_Operation == null) ? Mode.Create : Mode.Update;

            SetAttributes();
            LoadTab();
        }

        private void SetAttributes()
        {
            Text = "Edit Operation - " + _Operation.Name;

            this.FormClosed += Form_Close;
            this.tabControl1.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Application;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = (TabControl)sender;
            LoadTab(tab.SelectedIndex);
        }

        private void LoadTab(int index = 0)
        {
            switch (index)
            {
                case 1:     // Operation
                    #region Operation
                    if (tbrOperation.Buttons.Count == 0)
                    {
                        LoadOperation_Toolbar();
                        LoadOperation_ListViewHeader();
                        LoadOperation_Data();
                    }
                    break;
                    #endregion
                    break;
                case 0:
                default:    // Definition
                    #region definition
                    if (tbrDefinition.Buttons.Count == 0)
                    {
                        LoadDefinition_Toolbar();
                        LoadDefinition_Data();
                    }
                    break;
                    #endregion
            }
        }

        #region 共用 functions
        private void Common_ListView_LoadHeader(ref ListView listview, ItemType type)
        {
            IAzManItem[] members = _Operation.GetMembers();

            listview.Items.Clear();
            var ln = 1;
            foreach (IAzManItem member in members)
            {
                if (member.ItemType == type)
                {
                    ListViewItem oItem = listview.Items.Add(member.Name);
                    oItem.SubItems.Add(member.Description);
                    oItem.SubItems.Add(member.ItemId.ToString());
                }
            }
            listview.Sort();
        }

        private void Common_ListView_LoadItems(ref ListView listview)
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
            var colItemType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Type",
                Width = 100
            };
            var colItemDescription = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Description",
                Width = 250
            };
            var colItemId = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "ID",
                TextAlign = HorizontalAlignment.Center,
                Width = 50
            };

            listview.Columns.AddRange(new ColumnHeader[] {
                colItemName, colItemDescription, colItemId
            });
            #endregion

            listview.BackColor = Color.Transparent;
            //lvwRole.BorderStyle = BorderStyle.None;
            listview.ListViewItemSorter = new ListViewItemSorter(listview);
            listview.Dock = DockStyle.Fill;
            listview.GridLines = true;
            listview.MultiSelect = false;
            listview.SelectedIndexChanged += Common_ListView_SelectedIndexChanged;
            //lvwRole.DoubleClick += lvwRole_DoubleClick;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            listview.Tag = new Guid("358F24BC-10BA-48BA-8668-2762F192FFBD");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        private void Common_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            var cmdDelete = tbrOperation.Buttons[tbrOperation.Buttons.Count - 1];

            if (list.SelectedItem != null)
            {
                cmdDelete.Enabled = true;
            }

            else
            {
                cmdDelete.Enabled = false;
            }
        }

        private void Common_DeleteRecord(ListView listview)
        {
            if (listview.SelectedItem != null)
            {
                var item = listview.SelectedItem;

                try
                {
                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);

                    IAzManItem member = _Application.GetItem(item.SubItems[0].Text);
                    _Operation.RemoveMember(member);

                    _Storage.CommitTransaction();
                }
                catch { }
            }
        }
        #endregion

        #region tabDefinition functions
        private void LoadDefinition_Toolbar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdSave cmdDelete
            var cmdSave = new ToolBarButton("Save", ("Save"));
            cmdSave.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-check-circle-outline.png");

            var cmdAttributes = new ToolBarButton("Attributes", ("Attributes"));
            cmdAttributes.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdBizRule = new ToolBarButton("BizRule", ("Biz Rule"));
            cmdBizRule.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            tbrDefinition.Buttons.Add(cmdSave);
            tbrDefinition.Buttons.Add(sep);
            tbrDefinition.Buttons.Add(cmdAttributes);
            tbrDefinition.Buttons.Add(sep);
            tbrDefinition.Buttons.Add(cmdBizRule);
            #endregion

            tbrDefinition.ButtonClick += tbrDefinition_ButtonClick;
            tbrDefinition.Height = 24;
            tbrDefinition.TextAlign = ToolBarTextAlign.Right;
        }

        private void LoadDefinition_Data()
        {
            txtName.Text = _Operation.Name;
            txtDescription.Text = _Operation.Description;
        }

        private void tbrDefinition_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "save":
                        SaveDefinition();
                        Close();
                        break;
                    case "attributes":
                        var attr = new Definition.Attributes();
                        attr.ShowDialog();
                        break;
                    case "bizrule":
                        var biz = new Definition.BizRule();
                        biz.ShowDialog();
                        break;
                }
            }
        }

        private void SaveDefinition()
        {
            switch (_Mode)
            {
                case Mode.Create:
                    _Application = _Store.CreateApplication(txtName.Text.Trim(), txtDescription.Text);
                    break;
                case Mode.Update:
                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                    _Operation.Rename(this.txtName.Text.Trim());
                    _Operation.Update(this.txtDescription.Text.Trim());
                    _Storage.CommitTransaction();
                    break;
            }
            _Dirty = true;
        }
        #endregion

        #region tabOperation functions
        private void DeleteOperation()
        {
            Common_DeleteRecord(lvwOperation);
        }

        private void LoadOperation_Toolbar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdDelete
            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");
            cmdDelete.Enabled = false;

            tbrOperation.Buttons.Add(cmdDelete);
            #endregion

            tbrOperation.ButtonClick += tbrOperation_ButtonClick;
            tbrOperation.Height = 24;
            tbrOperation.TextAlign = ToolBarTextAlign.Right;
        }

        private void LoadOperation_ListViewHeader()
        {
            Common_ListView_LoadItems(ref lvwOperation);

            #region Floating Action Button
            cmdAddOperation.Image = new IconResourceHandle(GlobalVars.Theme + ".48.mdi-plus.circle.png");
            cmdAddOperation.Cursor = Cursors.Hand;
            cmdAddOperation.Click += cmdAddOperation_Click;
            toolTip1.SetToolTip(cmdAddOperation, "Add Operation");
            #endregion
        }

        private void LoadOperation_Data()
        {
            Common_ListView_LoadHeader(ref lvwOperation, ItemType.Operation);
        }

        private void tbrOperation_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "delete":
                        DeleteOperation();
                        LoadOperation_Data();
                        break;
                }
            }
        }

        private void cmdAddOperation_Click(object sender, EventArgs e)
        {
            var addOperation = new ItemList();
            addOperation.ItemType = ItemType.Operation;
            addOperation.FormClosed += AddOperation_FormClosed;
            addOperation.ShowDialog();
        }

        private void AddOperation_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (ItemList)sender;
            if (form.SelectedItems.Count > 0)
            {
                LoadOperation_Data();
            }
        }

        private void lvwOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmdDelete = tbrOperation.Buttons[tbrOperation.Buttons.Count - 1];
            if (this.lvwOperation.SelectedItem != null)
            {
                cmdDelete.Enabled = true;
            }

            else
            {
                cmdDelete.Enabled = false;
            }
        }
        #endregion
    }
}