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
using NetSqlAzMan.Tester.NetSqlAzMan.Helper;
using Gizmox.WebGUI.Forms.Dialogs;
using NetSqlAzMan.Interfaces;

#endregion

namespace NetSqlAzMan.Tester.NetSqlAzMan.ListViews
{
    public partial class ItemDefinitions : UserControl
    {
        public ItemDefinitions()
        {
            InitializeComponent();
        }

        private void Storage_Load(object sender, EventArgs e)
        {
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
            toolBar.Buttons[0].Image = new IconResourceHandle("16.listview_columns.gif");
            toolBar.Buttons[0].ToolTipText = (@"Hide/Unhide Columns");
            toolBar.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            toolBar.Buttons[1].Image = new IconResourceHandle("16.listview_sorting.gif");
            toolBar.Buttons[1].ToolTipText = (@"Sorting");
            toolBar.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            toolBar.Buttons[2].Image = new IconResourceHandle("16.listview_checkbox.gif");
            toolBar.Buttons[2].ToolTipText = (@"Toggle Checkbox");
            toolBar.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            toolBar.Buttons[3].Image = new IconResourceHandle("16.listview_multiselect.gif");
            toolBar.Buttons[3].ToolTipText = (@"Toggle Multi-Select");
            toolBar.Buttons[3].Visible = false;
            #endregion

            toolBar.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            ToolbarHelper.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", ("View"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            toolBar.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(toolBar_Views_MenuClick);
            #endregion

            #region cmdPreference    - Buttons[6]
            ContextMenu ddlPreference = new ContextMenu();
            ToolbarHelper.AppendMenuItem_AppPref(ref ddlPreference);

            ToolBarButton cmdPreference = new ToolBarButton("Preference", ("Preference"));
            cmdPreference.Style = ToolBarButtonStyle.DropDownButton;
            cmdPreference.Image = new IconResourceHandle("24.mdi-table-heart.png");
            cmdPreference.DropDownMenu = ddlPreference;
            cmdPreference.MenuClick += new MenuEventHandler(toolBar_Preference_MenuClick);

            toolBar.Buttons.Add(cmdPreference);
            #endregion

            toolBar.Buttons.Add(sep);

            #region cmdRefresh cmdEditStoreGroups cmdNewStoreGroup cmdEditStoreGroup
            var cmdRefresh = new ToolBarButton("Refresh", ("Refresh"));
            cmdRefresh.Image = new IconResourceHandle("16.fa-sync-alt.png");

            toolBar.Buttons.Add(cmdRefresh);

            toolBar.Buttons.Add(sep);

            var cmdEditStoreGroups = new ToolBarButton("EditStoreGroups", ("Edit Store Groups"));
            cmdEditStoreGroups.Image = new IconResourceHandle("24.mdi-domain.png");

            toolBar.Buttons.Add(cmdEditStoreGroups);

            toolBar.Buttons.Add(sep);

            var cmdNewStoreGroup = new ToolBarButton("NewApplication", ("New Store Group"));
            cmdNewStoreGroup.Image = new IconResourceHandle("24.mdi-account-multiple-plus.png");

            toolBar.Buttons.Add(cmdNewStoreGroup);

            ToolBarButton cmdEdit = new ToolBarButton("Edit", ("Edit Store Group"));
            cmdEdit.Image = new IconResourceHandle("24.mdi-file-document-edit-outline.png");
            cmdEdit.Enabled = false;

            toolBar.Buttons.Add(cmdEdit);
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
                SortOrder = SortOrder.Ascending,
                SortPosition = 1,
                Visible = false,
                Width =30
            };
            var colItemName = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Name",
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
                colItemName, colItemDescription, colLN
            });
            #endregion

            listView.BackColor = Color.Transparent;
            //listView.BorderStyle = BorderStyle.None;
            listView.ListViewItemSorter = new ListViewItemSorter(listView);
            listView.Dock = DockStyle.Fill;
            listView.GridLines = true;
            listView.MultiSelect = false;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            listView.Tag = new Guid("7A7D79E9-5AD5-4C4A-9CD7-4253A030B74C");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

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
                        //RefreshForm();
                        //BindList();
                        this.Update();
                        break;
                    case "print":
                        //PrintManager print = new PrintManager();
                        //print.ShowDialog();
                        break;
                    case "Edit":
                        //this.ShowEditManager();
                        break;
                    case "popup":
                        //ShowRecord();
                        break;
                }
            }
        }
        #endregion

        #region ListView Events
        private void listView_Load()
        {
            var application = (IAzManApplication)Session["selectedObject"];

            listView.Items.Clear();

            ListViewItem oItem1 = listView.Items.Add("Role Definitions");
            oItem1.SmallImage = new IconResourceHandle("24.mdi-folder.png");
            oItem1.LargeImage = new IconResourceHandle("32.mdi-folder.png");
            oItem1.SubItems.Add("Role Definitions container");
            oItem1.SubItems.Add("1");
            ListViewItem oItem2 = listView.Items.Add("Task Definitions");
            oItem2.SmallImage = new IconResourceHandle("24.mdi-folder.png");
            oItem2.LargeImage = new IconResourceHandle("32.mdi-folder.png");
            oItem2.SubItems.Add("Task Definitions container");
            oItem2.SubItems.Add("2");
            ListViewItem oItem3 = listView.Items.Add("Operation Definitions");
            oItem3.SmallImage = new IconResourceHandle("24.mdi-folder.png");
            oItem3.LargeImage = new IconResourceHandle("32.mdi-folder.png");
            oItem3.SubItems.Add("Operation Definitions container");
            oItem3.SubItems.Add("3");

            listView.Sort();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmdEdit = toolBar.Buttons[toolBar.Buttons.Count - 1];
            if (this.listView.SelectedItem != null)
            {
                cmdEdit.Enabled = true;
            }

            else
            {
                cmdEdit.Enabled = false;
            }
        }
        #endregion
    }
}