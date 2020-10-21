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

namespace VWG.Community.NetSqlAzMan.Forms.Authorization
{
    public partial class ManageAuthorization : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _AuthItem = null;
        private DataTable _AuthData;

        public ManageAuthorization()
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

        #region before form load
        private void SetAttributes()
        {
            toolbar.Height = 24;
            //lvwAuth.CheckBoxes = true;
            lvwAuth.Dock = DockStyle.Fill;
            lvwAuth.GridLines = true;
            lvwAuth.MultiSelect = false;

            Text = "Item Authorization - " + _AuthItem.Name;

            this.Width = 880;
        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdEdit cmdDelete cmdAttributes
            var cmdEdit = new ToolBarButton("Edit", ("Edit"));
            cmdEdit.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-file-document-edit-outline.png");
            cmdEdit.Enabled = false;

            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");
            cmdDelete.Enabled = false;

            var cmdAttributes = new ToolBarButton("Attributes", ("Attributes"));
            cmdAttributes.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");
            cmdAttributes.Enabled = false;

            var cmdAddStoreGroups = new ToolBarButton("AddStoreGroups", ("Add Store Groups"));
            cmdAddStoreGroups.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdAddApplicationGroups = new ToolBarButton("AddApplicationGroups", ("Add Application Groups"));
            cmdAddApplicationGroups.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdAddDBUsers = new ToolBarButton("AddDBUsers", ("Add DB Users"));
            cmdAddDBUsers.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            toolbar.Buttons.Add(cmdAddStoreGroups);
            toolbar.Buttons.Add(cmdAddApplicationGroups);
            toolbar.Buttons.Add(cmdAddDBUsers);

            toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdEdit);
            toolbar.Buttons.Add(cmdDelete);
            toolbar.Buttons.Add(cmdAttributes);
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

            lvwAuth.Columns.AddRange(new ColumnHeader[] {
                colItemType, colItemName, colItemAuthType, colWhereDefined, colOwnerd, colValidOn, colValidTo, colAuthId
            });
            #endregion

            lvwAuth.BackColor = Color.Transparent;
            //listView.BorderStyle = BorderStyle.None;
            lvwAuth.ListViewItemSorter = new ListViewItemSorter(lvwAuth);
            lvwAuth.Dock = DockStyle.Fill;
            lvwAuth.GridLines = true;
            lvwAuth.MultiSelect = false;
            lvwAuth.SelectedIndexChanged += lvwAuth_SelectedIndexChanged;
//            lvwAuth.DoubleClick += lvwAuth_DoubleClick;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvwAuth.Tag = new Guid("4BC532E5-7660-4537-BE1F-50B5899349B0");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }
        #endregion

        #region Edit Save Delete Record methods
        private void EditRecord()
        {
            if (lvwAuth.SelectedItem != null)
            {
                var edit = new Forms.Authorization.AuthorizationItem();
                edit.AuthorizationID = GetAuthorizationID();
                edit.FormClosed += AuthFormRecord_FormClosed;
                edit.ShowDialog();
            }
        }

        private void SaveRecord()
        {
            switch (_Mode)
            {
                case Mode.Create:
                    //_Application = _Store.CreateApplication(txtName.Text.Trim(), txtDescription.Text);
                    break;
                case Mode.Update:
                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                    //_Application.Rename(this.txtName.Text.Trim());
                    //_Application.Update(this.txtDescription.Text.Trim());
                    _Storage.CommitTransaction();
                    break;
            }
        }

        private void DeleteRecord()
        {
            if (lvwAuth.SelectedItem != null)
            {
                try
                {
                    var index = int.Parse(lvwAuth.SelectedItem.SubItems[lvwAuth.SelectedItem.SubItems.Count - 1].Text);

                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                    _AuthItem.GetAuthorization(index).Delete();
                    _Store.Storage.CommitTransaction();

                    LoadList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error found...Job aborted");
                }
            }
        }
        #endregion

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "edit":
                        EditRecord();
                        break;
                    case "delete":
                        DeleteRecord();
                        break;
                    case "attributes":
                        var attr = new Forms.Authorization.Attributes();
                        attr.AuthorizationID = GetAuthorizationID();
                        attr.FormClosed += Attributes_FormClosed;
                        attr.ShowDialog();
                        break;
                    case "addstoregroups":
                        var sgList = new Forms.Authorization.StoreGroupsList();
                        sgList.FormClosed += StoreGroupsList_FormClosed;
                        sgList.ShowDialog();
                        break;
                    case "addapplicationgroups":
                        var appList = new Forms.Authorization.ApplicationGroupsList();
                        appList.FormClosed += ApplicationGroupsList_FormClosed;
                        appList.ShowDialog();
                        break;
                    case "adddbusers":
                        var dbList = new Forms.Authorization.DBUsersList();
                        dbList.FormClosed += DBUserList_FormClosed;
                        dbList.ShowDialog();
                        break;
                }
            }
        }

        private void AuthFormRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            var record = (AuthorizationItem)sender;
            if (record.Dirty) LoadList();
        }

        private void Attributes_FormClosed(object sender, FormClosedEventArgs e)
        {
            var record = (Attributes)sender;
        }

        #region On Forms Close Events
        private void StoreGroupsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            var sgList = (StoreGroupsList)sender;
            var selectedItems = sgList.SelectedItems;
            if (selectedItems.Count > 0) LoadList();
        }

        private void ApplicationGroupsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            var agList = (ApplicationGroupsList)sender;
            var selectedItems = agList.SelectedItems;
            if (selectedItems.Count > 0) LoadList();
        }

        private void DBUserList_FormClosed(object sender, FormClosedEventArgs e)
        {
            var dbUserList = (Forms.Authorization.DBUsersList)sender;
            var selectedItems = dbUserList.SelectedItems;
            if (selectedItems.Count > 0) LoadList();
        }
        #endregion

        #region lvwAuth
        private void LoadList()
        {
            lvwAuth.Items.Clear();

            IAzManAuthorization[] authorizations = _AuthItem.GetAuthorizations();
            foreach (IAzManAuthorization authorization in authorizations)
            {
                string displayName;
                MemberType memberType = authorization.GetMemberInfo(out displayName);
                string ownerName;
                MemberType ownerType = authorization.GetOwnerInfo(out ownerName);

                ListViewItem oItem = lvwAuth.Items.Add(NetSqlAzManHelper.GetMemberTypeName(memberType, authorization.SID, _AuthItem));
                oItem.SubItems.Add(displayName);
                oItem.SubItems.Add(NetSqlAzManHelper.GetAuthTypeName(authorization.AuthorizationType));
                oItem.SubItems.Add(authorization.SidWhereDefined.ToString());
                oItem.SubItems.Add(ownerName);
                oItem.SubItems.Add(authorization.ValidFrom.HasValue ? authorization.ValidFrom.Value.ToString("yyyy-MM-dd") : "");
                oItem.SubItems.Add(authorization.ValidTo.HasValue ? authorization.ValidTo.Value.ToString("yyyy-MM-dd") : "");
                oItem.SubItems.Add(authorization.AuthorizationId.ToString());
            }

            lvwAuth.Sort();
        }

        private void lvwAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = toolbar.Buttons.Count;

            var cmdAttributes = toolbar.Buttons[index - 1];
            var cmdDelete = toolbar.Buttons[index - 2];
            var cmdEdit = toolbar.Buttons[index - 3];

            if (lvwAuth.SelectedItem != null)
            {
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
                cmdAttributes.Enabled = true;
            }
            else
            {
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdAttributes.Enabled = false;
            }
        }

        private void listAuth_DoubleClick(object sender, EventArgs e)
        {
            EditRecord();
        }

        private int GetAuthorizationID(ListViewItem item = null)
        {
            var result = 0;

            if (lvwAuth.SelectedItem != null)
            {
                if (item == null) item = lvwAuth.SelectedItem;
                try
                {
                    result = int.Parse(item.SubItems[item.SubItems.Count - 1].Text);
                }
                catch { }
            }

            return result;
        }
        #endregion
    }
}