﻿#region Using

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
    public partial class OperationAuthorizations : UserControl
    {
        public OperationAuthorizations()
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

            #region cmdRefresh 
            var cmdRefresh = new ToolBarButton("Refresh", ("Refresh"));
            cmdRefresh.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-reload.png");

            toolBar.Buttons.Add(cmdRefresh);

            //toolBar.Buttons.Add(sep);

            //var cmdStoreProperties = new ToolBarButton("StoreProperties", ("Store Properties"));
            //cmdStoreProperties.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-domain.png");

            //toolBar.Buttons.Add(cmdStoreProperties);

            //var cmdNewOperation = new ToolBarButton("NewOperation", ("New Operation"));
            //cmdNewOperation.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-folder-plus-outline.png");

            //toolBar.Buttons.Add(cmdNewOperation);
            #endregion

            toolBar.Buttons.Add(sep);

            #region Edit
            ToolBarButton cmdManageAuth = new ToolBarButton("ManageAuthorization", ("Manage Authorization"));
            cmdManageAuth.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-shield-account.png");
            cmdManageAuth.Enabled = false;

            toolBar.Buttons.Add(cmdManageAuth);
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
                Text = "Operation Name",
                Width = 150
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
                Text = "Operation ID",
                TextAlign = HorizontalAlignment.Center,
                Width = 80
            };

            listView.Columns.AddRange(new ColumnHeader[] {
                colItemName, colItemDescription, colItemId
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
            listView.Tag = new Guid("D32B14C0-51EB-44CA-A33D-41F8618D4A27");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        private void EditRecord()
        {
            if (listView.SelectedItem != null)
            {
                var item = listView.SelectedItem;
                var storage = (IAzManStorage)Session["storage"];
                var app = (IAzManApplication)Session["selectedObject"];

                Session["selectedObject"] = app.GetItem(item.Text.Trim());
                var auth = new Forms.Authorization.ManageAuthorization();
                auth.ShowDialog();
                Session["selectedObject"] = app;
            }
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
                    case "manageauthorization":
                        EditRecord();
                        break;
                }
            }
        }
        #endregion

        #region ListView Events
        private void listView_Load()
        {
            var app = (IAzManApplication)Session["selectedObject"];

            listView.Items.Clear();
            var ln = 1;
            foreach (var item in app.GetItems(ItemType.Operation))
            {
                ListViewItem oItem = listView.Items.Add(item.Name);
                #region Icon
                oItem.SmallImage = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-cog-outline.png");
                oItem.LargeImage = new IconResourceHandle(GlobalVars.Theme + ".32.mdi-cog-outline.png");
                #endregion
                //oItem.SubItems.Add(ln.ToString());
                oItem.SubItems.Add(item.Description);
                oItem.SubItems.Add(item.ItemId.ToString());

                ln++;
            }
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

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            EditRecord();
        }
        #endregion
    }
}