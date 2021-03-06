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
    public partial class RoleDefinitions : UserControl
    {
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;

        public RoleDefinitions()
        {
            InitializeComponent();
        }

        private void Storage_Load(object sender, EventArgs e)
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

            #region cmdRefresh cmdNewRole cmdEditRole
            var cmdRefresh = new ToolBarButton("Refresh", ("Refresh"));
            cmdRefresh.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-reload.png");

            toolBar.Buttons.Add(cmdRefresh);

            toolBar.Buttons.Add(sep);

            var cmdNewRole = new ToolBarButton("NewRole", ("New Role"));
            cmdNewRole.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-folder-plus-outline.png");

            toolBar.Buttons.Add(cmdNewRole);

            toolBar.Buttons.Add(sep);

            ToolBarButton cmdEditRole = new ToolBarButton("EditRole", ("Edit Role"));
            cmdEditRole.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-file-document-edit-outline.png");
            cmdEditRole.Enabled = false;

            ToolBarButton cmdDeleteRole = new ToolBarButton("DeleteRole", ("Delete Role"));
            cmdDeleteRole.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");
            cmdDeleteRole.Enabled = false;

            toolBar.Buttons.Add(cmdEditRole);
            toolBar.Buttons.Add(sep);
            toolBar.Buttons.Add(cmdDeleteRole);
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
                Text = "Role Name",
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
                Text = "Role ID",
                TextAlign = HorizontalAlignment.Center,
                Width = 50
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
            listView.Tag = new Guid("9433FC6D-77F6-4292-8F64-AC2BDCEDB2E2");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        #region Edit Delete Record methods
        private void EditRecord()
        {
            if (listView.SelectedItem != null)
            {
                var item = listView.SelectedItem;

                // 由 IAzManApplication 改為 IAzManItem
                Session["selectedObject"] = _Application.GetItem(item.Text.Trim());

                var editRole = new Forms.Definition.RoleEdit();
                editRole.FormClosed += Role_FormClosed;
                editRole.ShowDialog();
            }
        }

        private void DeleteRecord()
        {
            if (listView.SelectedItem != null)
            {
                var item = listView.SelectedItem;
                try
                {
                    var storage = (IAzManStorage)Session["storage"];
                    var app = (IAzManApplication)Session["selectedObject"];
                    var oAzManItem = app.GetItem(item.Text.Trim());
                    oAzManItem.Delete();
                    listView_Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error found..." + Environment.NewLine + ex.Message);
                }
            }
        }
        #endregion

        private void Role_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 改回 IAzManApplication
            Session["selectedObject"] = _Application;

            var form = (Forms.Definition.RoleEdit)sender;
            if (form.Dirty) listView_Load();
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
                        listView_Load();
                        break;
                    case "newrole":
                        var newRole = new Forms.Definition.Role();
                        newRole.ShowDialog();
                        break;
                    case "editrole":
                        EditRecord();
                        break;
                    case "deleterole":
                        DeleteRecord();
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
            foreach (var item in app.GetItems(ItemType.Role))
            {
                ListViewItem oItem = listView.Items.Add(item.Name);
                #region Icon
                oItem.SmallImage = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-briefcase-account.png");
                oItem.LargeImage = new IconResourceHandle(GlobalVars.Theme + ".32.mdi-briefcase-account.png");
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