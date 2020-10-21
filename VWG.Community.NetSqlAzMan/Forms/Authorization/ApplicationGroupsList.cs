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

namespace VWG.Community.NetSqlAzMan.Forms.Authorization
{
    public partial class ApplicationGroupsList : Form
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

        private List<ListViewItem> _SelectedItems = new List<ListViewItem>();
        public List<ListViewItem> SelectedItems
        {
            get { return _SelectedItems; }
            set { _SelectedItems = value; }
        }

        public ApplicationGroupsList()
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

            Text = "Add Application Group - " + _AuthItem.Name;

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
                Width = 100
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
                Text = "Type",
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

            lvwStoreGroups.Columns.AddRange(new ColumnHeader[] {
                colItemName, colItemAuthType, colItemType
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
            lvwStoreGroups.Tag = new Guid("3A956361-CBE6-4B70-83BD-2011AC4AF88D");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        private void SaveRecord()
        {
            _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
            foreach (ListViewItem item in lvwStoreGroups.Items)
            {
                #region update checked items only, not selected
                if (item.Checked)
                {
                    _SelectedItems.Add(item);

                    #region 立即 save
                    IAzManApplicationGroup appGroup = _Application.GetApplicationGroup(item.SubItems[0].Text);
                    IAzManAuthorization auth = _AuthItem.CreateAuthorization(
                        new SqlAzManSID(appGroup.SID.ToString(), this._CurrentOwnerSidWhereDefined == WhereDefined.Application),
                        this._CurrentOwnerSidWhereDefined,
                        new SqlAzManSID(appGroup.SID.ToString(), true),
                        WhereDefined.Application,
                        AuthorizationType.Neutral,
                        null,
                        null);
                    #endregion
                }
                #endregion
            }
            _Storage.CommitTransaction();
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

        private void onResponse(object sender, EventArgs e)
        {
            Close();
        }

        #region lvwAuth
        private void LoadList()
        {
            SetListView();
            lvwStoreGroups.Items.Clear();

            IAzManApplicationGroup[] appGroups = _Application.GetApplicationGroups();
            foreach (IAzManApplicationGroup appGroup in appGroups)
            {
                if ((_StoreGroup == null) || (_StoreGroup != null && appGroup.SID.StringValue != _StoreGroup.SID.StringValue))
                {
                    ListViewItem oItem = lvwStoreGroups.Items.Add(appGroup.Name);
                    oItem.SubItems.Add(appGroup.Description);
                    oItem.SubItems.Add(appGroup.GroupType == GroupType.Basic ? "Basic Group" : "LDAP Group");
                }
            }

            lvwStoreGroups.Sort();
        }

        private void ConstDataColumn()
        {
            _AuthData = new DataTable();
            DataColumn dcAuthorizationId = new DataColumn("AuthorizationID", typeof(int));
            dcAuthorizationId.AutoIncrement = true;
            dcAuthorizationId.AutoIncrementSeed = -1;
            dcAuthorizationId.AutoIncrementStep = -1;
            dcAuthorizationId.AllowDBNull = false;
            dcAuthorizationId.Unique = true;
            DataColumn dcAttributesLink = new DataColumn("AttributesLink", typeof(string));
            DataColumn dcMemberTypeEnum = new DataColumn("MemberTypeEnum", typeof(MemberType));
            DataColumn dcMemberType = new DataColumn("MemberType", typeof(string));
            DataColumn dcOwner = new DataColumn("Owner", typeof(string));
            DataColumn dcOwnerSid = new DataColumn("OwnerSID", typeof(string));
            DataColumn dcName = new DataColumn("Name", typeof(string));
            DataColumn dcObjectSid = new DataColumn("ObjectSID", typeof(string));
            DataColumn dcWhereDefined = new DataColumn("WhereDefined", typeof(string));
            DataColumn dcWhereDefinedEnum = new DataColumn("WhereDefinedEnum", typeof(WhereDefined));
            DataColumn dcAuthorizationType = new DataColumn("AuthorizationType", typeof(string));
            DataColumn dcAuthorizationTypeEnum = new DataColumn("AuthorizationTypeEnum", typeof(AuthorizationType));
            DataColumn dcValidFrom = new DataColumn("ValidFrom", typeof(DateTime));
            dcValidFrom.AllowDBNull = true;
            DataColumn dcValidTo = new DataColumn("ValidTo", typeof(DateTime));
            dcValidTo.AllowDBNull = true;

            dcMemberType.Caption = "Member Type";
            dcOwner.Caption = "Owner";
            dcOwnerSid.Caption = "Owner SID";
            dcName.Caption = "Name";
            dcObjectSid.Caption = "Object SID";
            dcWhereDefined.Caption = "Where Defined";
            dcAuthorizationType.Caption = "Authorization Type";
            dcValidFrom.Caption = "Valid From";
            dcValidTo.Caption = "Valid To";

            _AuthData.Columns.AddRange(
                new DataColumn[]
            {
                    dcAuthorizationId,
                    dcMemberType,
                    dcName,
                    dcAuthorizationType,
                    dcWhereDefined,
                    dcOwner,
                    dcOwnerSid,
                    dcValidFrom,
                    dcValidTo,
                    dcObjectSid,
                    dcAuthorizationTypeEnum,
                    dcWhereDefinedEnum,
                    dcMemberTypeEnum,
                    dcAttributesLink
            });
            foreach (DataColumn dc in _AuthData.Columns)
            {
                dc.AllowDBNull = true;
                dc.ColumnMapping = MappingType.Hidden;
            }
            dcMemberType.AllowDBNull = false;
            dcAuthorizationType.AllowDBNull = false;
        }

        private void LoadData()
        {
            _AuthData.Rows.Clear();
            IAzManAuthorization[] authorizations = _AuthItem.GetAuthorizations();
            foreach (IAzManAuthorization authorization in authorizations)
            {
                AddAuthDataRow(authorization);
            }
            _AuthData.AcceptChanges();
            
            if (!_AuthItem.Application.IAmManager)
            {
                //this.btnAddStoreGroups.Enabled = this.btnAddApplicationGroups.Enabled = this.btnAddWindowsUsersAndGroups.Enabled = this.btnAddDBUsers.Enabled =
                //this.btnOk.Enabled = this.btnApply.Enabled = this.btnRemove.Enabled = false;
            }
        }

        private void AddAuthDataRow(IAzManAuthorization authorization)
        {
            DataRow dr = _AuthData.NewRow();
            dr["AuthorizationID"] = authorization.AuthorizationId;
            dr["AttributesLink"] = "";  // this.getAttributesLink((int)dr["AuthorizationID"]);
            string displayName;
            MemberType memberType = authorization.GetMemberInfo(out displayName);
            string ownerName;
            MemberType ownerType = authorization.GetOwnerInfo(out ownerName);
            dr["MemberType"] = NetSqlAzManHelper.GetMemberTypeName(memberType, authorization.SID, _AuthItem);
            dr["MemberTypeEnum"] = memberType;
            dr["Owner"] = ownerName;
            dr["Name"] = displayName;
            dr["OwnerSID"] = authorization.Owner;
            if (authorization.SidWhereDefined == WhereDefined.Database)
            {
                dr["ObjectSID"] = authorization.SID.StringValue;
            }
            else
            {
                dr["ObjectSID"] = authorization.SID.StringValue;
            }

            switch (authorization.SidWhereDefined.ToString())
            {
                case "LDAP": dr["WhereDefined"] = "Active Directory"; break;
                case "Local": dr["WhereDefined"] = "Local"; break;
                case "Database": dr["WhereDefined"] = "DB User"; break;
                case "Store": dr["WhereDefined"] = "Store"; break;
                case "Application": dr["WhereDefined"] = "Application"; break;
            }

            dr["WhereDefinedEnum"] = authorization.SidWhereDefined;
            dr["AuthorizationType"] = NetSqlAzManHelper.GetAuthTypeName(authorization.AuthorizationType);
            dr["AuthorizationTypeEnum"] = authorization.AuthorizationType;
            dr["ValidFrom"] = authorization.ValidFrom.HasValue ? (object)authorization.ValidFrom.Value : DBNull.Value;
            dr["ValidTo"] = authorization.ValidTo.HasValue ? (object)authorization.ValidTo.Value : DBNull.Value;
            _AuthData.Rows.Add(dr);
        }
        #endregion
    }
}