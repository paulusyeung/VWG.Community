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
    public partial class TaskAuthorization : UserControl
    {
        public TaskAuthorization()
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

            #region cmdRefresh cmdEditRole
            var cmdRefresh = new ToolBarButton("Refresh", ("Refresh"));
            cmdRefresh.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-reload.png");

            toolBar.Buttons.Add(cmdRefresh);

            toolBar.Buttons.Add(sep);

            ToolBarButton cmdManageAuth = new ToolBarButton("ManageAuthorization", ("Manage Authorization"));
            cmdManageAuth.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-shield-account.png");

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
                Width = 30
            };
            var colItemType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Member Type",
                SortOrder = SortOrder.Ascending,
                SortPosition = 1,
                Width = 100
            };
            var colItemName = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                SortOrder = SortOrder.Ascending,
                SortPosition = 2,
                Text = "Name",
                Width = 100
            };
            var colItemAuthType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Authorization Type",
                TextAlign = HorizontalAlignment.Center,
                Width = 100
            };
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
            var colAuthId = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Authorization ID",
                Visible = false,
                Width = 100
            };

            listView.Columns.AddRange(new ColumnHeader[] {
                colItemType, colItemName, colItemAuthType, colWhereDefined, colOwnerd, colValidOn, colValidTo, colAuthId
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
            listView.Tag = new Guid("4A47132C-8C19-4F40-B66D-2BEC79CE4F16");

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
                        listView_Load();
                        break;
                    case "manageauthorization":
                        var manageAuth = new Forms.Authorization.ManageAuthorization();
                        manageAuth.ShowDialog();
                        break;
                }
            }
        }
        #endregion

        #region ListView Events
        private void listView_Load()
        {
            var storage = (IAzManStorage)Session["storage"];
            var app = (IAzManItem)Session["selectedObject"];
            var items = app.GetAuthorizations();

            listView.Items.Clear();
            var ln = 1;
            foreach (var authorization in app.GetAuthorizations())
            {
                #region extract row data
                string sAuthType;
                switch (authorization.AuthorizationType)
                {
                    default:
                    case AuthorizationType.Neutral: sAuthType = "Neutral"; break;
                    case AuthorizationType.Allow: sAuthType = "Allow"; break;
                    case AuthorizationType.AllowWithDelegation: sAuthType = "Allow With Delegation"; break;
                    case AuthorizationType.Deny: sAuthType = "Deny"; break;
                }
                string displayName;
                MemberType memberType = authorization.GetMemberInfo(out displayName);
                string ownerName;
                MemberType ownerType = authorization.GetOwnerInfo(out ownerName);
                string memberTypeText = String.Empty;
                switch (memberType)
                {
                    case MemberType.AnonymousSID: memberTypeText = "SID Not Found"; break;
                    case MemberType.ApplicationGroup:
                        if (app.Application.GetApplicationGroup(authorization.SID).GroupType == GroupType.Basic)
                            memberTypeText = "Application Group";
                        else
                            memberTypeText = "LDAP Group";
                        break;
                    case MemberType.StoreGroup:
                        if (app.Application.Store.GetStoreGroup(authorization.SID).GroupType == GroupType.Basic)
                            memberTypeText = "Store Group";
                        else
                            memberTypeText = "LDAP Group";
                        break;
                    case MemberType.WindowsNTGroup: memberTypeText = "Windows Basic Group"; break;
                    case MemberType.WindowsNTUser: memberTypeText = "Windows User"; break;
                    case MemberType.DatabaseUser: memberTypeText = "DB User"; break;
                }
                string sidWDS = String.Empty;
                switch (authorization.SidWhereDefined.ToString())
                {
                    case "LDAP": sidWDS = "Active Directory"; break;
                    case "Local": sidWDS = "Local Computer"; break;
                    case "Database": sidWDS = "Database"; break;
                    case "Store": sidWDS = "Store"; break;
                    case "Application": sidWDS = "Application"; break;
                }
                #endregion

                if (!(authorization.SidWhereDefined == WhereDefined.Local && storage.Mode == NetSqlAzManMode.Administrator))
                {
                    ListViewItem oItem = listView.Items.Add(memberTypeText);
                    oItem.SubItems.Add(displayName);
                    oItem.SubItems.Add(sAuthType);
                    oItem.SubItems.Add(sidWDS);
                    oItem.SubItems.Add(ownerName);
                    oItem.SubItems.Add(authorization.ValidFrom.HasValue ? authorization.ValidFrom.Value.ToString("yyyy-MM-dd") : String.Empty);
                    oItem.SubItems.Add(authorization.ValidTo.HasValue ? authorization.ValidTo.Value.ToString("yyyy-MM-dd") : String.Empty);
                    oItem.SubItems.Add(authorization.AuthorizationId.ToString());
                    //oItem.SubItems.Add(authorization.SID.StringValue);
                }

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
        #endregion
    }
}