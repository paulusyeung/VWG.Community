#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using VWG.Community.NetSqlAzMan.Helper;
using Gizmox.WebGUI.Forms.Dialogs;
using NetSqlAzMan.Interfaces;

#endregion

namespace VWG.Community.NetSqlAzMan.ListViews
{
    public partial class StoreGroups : UserControl
    {
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        //private IAzManStoreGroup _StoreGroup = null;

        public StoreGroups()
        {
            InitializeComponent();
        }

        private void Storage_Load(object sender, EventArgs e)
        {
            _Storage = (IAzManStorage)Session["storage"];

            if (Session["selectedObject"] as IAzManStore != null)
            {
                _Store = this.Session["selectedObject"] as IAzManStore;
            }
            //if (Session["selectedObject"] as IAzManStoreGroup != null)
            //{
            //    _StoreGroup = this.Session["selectedObject"] as IAzManStoreGroup;
            //    _Store = _StoreGroup.Store;
            //}

            SetAttributes();

            SetToolbar();
            SetListView();

            listView_Load();
        }

        private void SetAttributes()
        {
            listView.Dock = DockStyle.Fill;
            toolBar.Height = 24;
        }

        private void SetToolbar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]
            toolBar.Buttons.Add(new ToolBarButton("Columns", string.Empty));
            toolBar.Buttons[0].Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-view-column-outline.png");
            toolBar.Buttons[0].ToolTipText = (@"Hide/Unhide Columns");
            toolBar.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            toolBar.Buttons[1].Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-sort-ascending.png");
            toolBar.Buttons[1].ToolTipText = (@"Sorting");
            toolBar.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            toolBar.Buttons[2].Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-check-box-outline.png");
            toolBar.Buttons[2].ToolTipText = (@"Toggle Checkbox");
            toolBar.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            toolBar.Buttons[3].Image = new IconResourceHandle(GlobalVars.Theme + ".16.listview_multiselect.gif");
            toolBar.Buttons[3].ToolTipText = (@"Toggle Multi-Select");
            toolBar.Buttons[3].Visible = false;
            #endregion

            toolBar.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            ToolbarHelper.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", ("View"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-eye.png");
            cmdViews.DropDownMenu = ddlViews;
            toolBar.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(toolBar_Views_MenuClick);
            #endregion

            #region cmdPreference    - Buttons[6]
            ContextMenu ddlPreference = new ContextMenu();
            ToolbarHelper.AppendMenuItem_AppPref(ref ddlPreference);

            ToolBarButton cmdPreference = new ToolBarButton("Preference", ("Preference"));
            cmdPreference.Style = ToolBarButtonStyle.DropDownButton;
            cmdPreference.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-table-heart.png");
            cmdPreference.DropDownMenu = ddlPreference;
            cmdPreference.MenuClick += new MenuEventHandler(toolBar_Preference_MenuClick);

            toolBar.Buttons.Add(cmdPreference);
            #endregion

            toolBar.Buttons.Add(sep);

            #region cmdRefresh cmdEditStoreGroups cmdNewStoreGroup cmdEditStoreGroup
            var cmdRefresh = new ToolBarButton("Refresh", ("Refresh"));
            cmdRefresh.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-reload.png");

            toolBar.Buttons.Add(cmdRefresh);
            toolBar.Buttons.Add(sep);

            var cmdImport = new ToolBarButton("Import", ("Import Store Groups"));
            cmdImport.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-import.png");

            var cmdNewStoreGroup = new ToolBarButton("NewStoreGroup", ("New Store Group"));
            cmdNewStoreGroup.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-account-multiple-plus.png");

            ToolBarButton cmdEdit = new ToolBarButton("EditStoreGroup", ("Edit Store Group"));
            cmdEdit.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-file-document-edit-outline.png");
            cmdEdit.Enabled = false;

            ToolBarButton cmdDelete = new ToolBarButton("DeleteStoreGroup", ("Delete Store Group"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");
            cmdDelete.Enabled = false;

            toolBar.Buttons.Add(cmdImport);
            toolBar.Buttons.Add(sep);
            toolBar.Buttons.Add(cmdNewStoreGroup);
            toolBar.Buttons.Add(cmdEdit);
            toolBar.Buttons.Add(sep);
            toolBar.Buttons.Add(cmdDelete);
            #endregion

            toolBar.TextAlign = ToolBarTextAlign.Right;
            toolBar.ButtonClick += toolBar_ButtonClick;
        }

        private void SetListView()
        {
            #region define header
            var colLN = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "#",
                Width =30
            };
            var colItemName = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                SortOrder = SortOrder.Ascending,
                SortPosition = 1,
                Text = "Store Group Name",
                Width = 150
            };
            var colItemType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Group Type",
                TextAlign = HorizontalAlignment.Center,
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
                Text = "SID",
                TextAlign = HorizontalAlignment.Center,
                Width = 250
            };

            listView.Columns.AddRange(new ColumnHeader[] {
                colItemName, colItemDescription, colItemType, colItemId
            });
            #endregion

            listView.BackColor = Color.Transparent;
            //listView.BorderStyle = BorderStyle.None;
            listView.ListViewItemSorter = new ListViewItemSorter(listView);
            listView.Dock = DockStyle.Fill;
            listView.GridLines = true;
            listView.MultiSelect = false;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            listView.DoubleClick += listView_DoubleClick;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            listView.Tag = new Guid("BE04A845-1F53-4A2E-ACC8-953C47CBADD5");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        #region Edit Delete Record
        private void EditRecord()
        {
            if (listView.SelectedItem != null)
            {
                var item = listView.SelectedItem;

                Session["selectedObject"] = _Storage[_Store.Name].GetStoreGroup(item.Text.Trim());
                var editStoreGroup = new Forms.Store.StoreGroupEdit();
                editStoreGroup.FormClosed += StoreGroupEdit_FormClosed;
                editStoreGroup.ShowDialog();
            }
        }

        private void DeleteRecord()
        {
            if (listView.SelectedItem != null)
            {
                try
                {
                    var item = listView.SelectedItem;

                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);

                    var sg = _Storage[_Store.Name].GetStoreGroup(item.Text.Trim());
                    sg.Delete();

                    _Storage.CommitTransaction();

                    listView_Load();
                }
                catch { }
            }
        }

        private void StoreGroupEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Session["selectedObject"] = _Store;

            var form = (Forms.Store.StoreGroupEdit)sender;
            if (form.IsDirty) listView_Load();
        }
        #endregion

        #region ToolBar Events
        private void toolBar_Views_MenuClick(object sender, MenuItemEventArgs e)
        {
            switch (e.MenuItem.Tag.ToString())
            {
                case "Icon":
                    listView.View = View.SmallIcon;
                    break;
                case "Tile":
                    listView.View = View.LargeIcon;
                    break;
                case "List":
                    listView.View = View.List;
                    break;
                case "Details":
                    listView.View = View.Details;
                    break;
            }
        }

        private void toolBar_Preference_MenuClick(object sender, MenuItemEventArgs e)
        {
            //throw new NotImplementedException();
            switch (e.MenuItem.Tag.ToString())
            {
                case "Save":
                    //xPort5.Controls.Utility.DisplayPreference.Save(listView);
                    break;
                case "Reset":
                    //xPort5.Controls.Utility.DisplayPreference.Delete(listView);
                    break;
            }
            MessageBox.Show(("finish"));
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "columns":
                        ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(listView);
                        objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        ListViewSortingOptions objListViewSortingOption = new ListViewSortingOptions(listView);
                        objListViewSortingOption.ShowDialog();
                        break;
                    case "checkbox":
                        this.listView.CheckBoxes = !this.listView.CheckBoxes;

                        if (this.listView.CheckBoxes)
                        {
                            //this.ansList.Buttons[12].Visible = false;
                        }
                        break;
                    case "multiselect":
                        this.listView.MultiSelect = !this.listView.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                    case "refresh":
                        listView_Load();
                        break;
                    case "import":
                        var oImport = new Forms.Import();
                        oImport.Type = Enums.AzManItemType.StoreGroups;
                        oImport.ShowDialog();
                        break;
                    case "editstoregroups":
                        break;
                    case "newstoregroup":
                        #region New Store Group
                        var newStoreGroup = new Forms.Store.StoreGroup();
                        newStoreGroup.FormClosed += NewStoreGroup_FormClosed;
                        newStoreGroup.ShowDialog();
                        break;
                    #endregion
                    case "editstoregroup":
                        #region Edit Store Group
                        EditRecord();
                        break;
                    #endregion
                    case "deletestoregroup":
                        #region Delete Store Group
                        DeleteRecord();
                        break;
                        #endregion
                }
            }
        }

        private void NewStoreGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (Forms.Store.StoreGroup)sender;
            if (form.IsDirty) listView_Load();
        }
        #endregion

        #region ListView Events
        private void listView_Load()
        {
            listView.Items.Clear();
            var ln = 1;
            foreach (var sg in _Store.GetStoreGroups())
            {
                ListViewItem oItem = listView.Items.Add(sg.Name);
                #region Icon
                oItem.SmallImage = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-account-multiple.png");
                oItem.LargeImage = new IconResourceHandle(GlobalVars.Theme + ".32.mdi-account-multiple.png");
                #endregion
                oItem.SubItems.Add(sg.Description);
                oItem.SubItems.Add(sg.GroupType.ToString());
                oItem.SubItems.Add(sg.SID.ToString());

                ln++;
            }
            listView.Sort();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmdEdit = toolBar.Buttons[toolBar.Buttons.Count - 3];
            var cmdDelete = toolBar.Buttons[toolBar.Buttons.Count - 1];
            if (this.listView.SelectedItem != null)
            {
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
            }

            else
            {
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
            }
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            EditRecord();
        }
        #endregion
    }
}