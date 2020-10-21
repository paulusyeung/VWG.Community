#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using VWG.Community.NetSqlAzMan.Helper;
using static VWG.Community.NetSqlAzMan.Helper.Enums;
#endregion

namespace VWG.Community.NetSqlAzMan
{
    public partial class WebConsole : UserControl
    {
        private String _SqlConnString = String.Empty;
        private String _StoreName = "";     // 2020.09.05 paulus: 假設每個 database 得一個 Store
        private String _ServerRole = "";

        #region Properties
        internal IAzManStorage Storage
        {
            get
            {
                if (this.Session["storage"] == null)
                {
                    //Response.Redirect("StorageConnection.aspx");
                }
                return (IAzManStorage)this.Session["storage"];
            }
        }

        internal object SelectedObject
        {
            get
            {
                return this.Session["selectedObject"];
            }
            set
            {
                this.Session["selectedObject"] = value;
            }
        }

        public String SqlConnectionString
        {
            get { return GlobalVars.SqlConnectionString; }
            set { GlobalVars.SqlConnectionString = value; }
        }

        public String Theme
        {
            get { return GlobalVars.Theme; }
            set { GlobalVars.Theme = value; }
        }
        #endregion Properties

        public WebConsole()
        {
            InitializeComponent();
        }

        private void WebConsole_Load(object sender, EventArgs e)
        {
            _SqlConnString = GlobalVars.SqlConnectionString;

            Session["storage"] = new SqlAzManStorage(_SqlConnString);

            SetAttributes();
            LoadNavTree();
        }

        private void SetAttributes()
        {
            tvwNavTree.BackColor = Color.Transparent;
            //tvwNavTree.BorderStyle = BorderStyle.None;
            tvwNavTree.Dock = DockStyle.Fill;
            tvwNavTree.Margin = new Padding(0, 24, 0, 0);
            tvwNavTree.ShowLines = true;
            tvwNavTree.ShowPlusMinus = true;
            tvwNavTree.AfterSelect += tvwNavTree_AfterSelect;
        }

        private void tvwNavTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = tvwNavTree.SelectedNode;

            LoadListView(ref node);
        }

        private void LoadNavTree()
        {
            string displayName = "Authorization Manager";
            string connectedUserName = "?";
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(this.Storage.ConnectionString);
            if (csb.IntegratedSecurity)
                connectedUserName = ""; // this.Request.LogonUserIdentity.Name;
            else
                connectedUserName = csb.UserID.Trim();
            //if (!String.IsNullOrEmpty(csb.DataSource))
            //    displayName += String.Format(" ({0}\\{1} - {2})", csb.DataSource.Trim().ToUpper(), csb.InitialCatalog.Trim(), connectedUserName);
            tvwNavTree.Nodes.Clear();

            Font font = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);
            TreeNode root = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-database.png"),
                Label = displayName,
                NodeFont = font,
                Tag = Enums.AzManItemType.Storage
            };
            root.Expand();
            SelectedObject = this.Storage;

            LoadNavTree4Store(ref root);
            tvwNavTree.Nodes.Add(root);
        }

        private void LoadNavTree4Store(ref TreeNode parent)
        {
            var tag = (AzManItemType)parent.Tag;
            TreeNode gParent = parent.Parent;
            TreeNode ggParent = gParent == null ? null : gParent.Parent;
            TreeNode tn = null;

            switch (tag)
            {
                case AzManItemType.Storage:
                    #region Storage: => Stores
                    var stores = Storage.GetStores();
                    foreach (var store in stores)
                    {
                        var fixedserverrole = "";

                        if (store.IAmAdmin) fixedserverrole = "Admin";
                        else if (store.IAmManager) fixedserverrole = "Manager";
                        else if (store.IAmUser) fixedserverrole = "User";
                        else fixedserverrole = "Reader";
                        string displayName = String.Format(" {0} ({1})", store.Name, fixedserverrole);

                        Font font4Storage = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                        TreeNode node4Storage = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-domain.png"),
                            Label = displayName,
                            NodeFont = font4Storage,
                            Tag = AzManItemType.Store
                        };

                        _StoreName = store.Name;
                        _ServerRole = fixedserverrole;
                        LoadNavTree4Store(ref node4Storage);
                        parent.Nodes.Add(node4Storage);
                    }
                    break;
                #endregion
                case AzManItemType.Store:
                    #region Store
                    Font font4Store = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                    TreeNode node4Store = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Store Groups",
                        NodeFont = font4Store,
                        Tag = AzManItemType.StoreGroups
                    };

                    LoadNavTree4Store(ref node4Store);
                    parent.Nodes.Add(node4Store);
                    #endregion
                    #region Applications
                    IAzManApplication[] apps = Storage[StripItemName(parent.Label)].GetApplications();

                    foreach (IAzManApplication app in apps)
                    {
                        string fixedserverrole;
                        if (app.IAmAdmin) fixedserverrole = "Admin";
                        else if (app.IAmManager) fixedserverrole = "Manager";
                        else if (app.IAmUser) fixedserverrole = "User";
                        else fixedserverrole = "Reader";
                        string displayName = String.Format(" {0} ({1})", app.Name, fixedserverrole);

                        Font font4App = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                        TreeNode node4App = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder-cog-outline.png"),
                            Label = displayName,
                            NodeFont = font4Store,
                            Tag = AzManItemType.Application
                        };

                        LoadNavTree4Store(ref node4App);
                        parent.Nodes.Add(node4App);
                    }
                    break;
                #endregion
                case AzManItemType.StoreGroups:
                    #region Store Groups
                    var storeGroups = this.Storage[_StoreName].GetStoreGroups();

                    foreach (IAzManStoreGroup sg in storeGroups)
                    {
                        //this.newTreeNode(sg.Name, "Store Group|" + Utility.PipeEncode(sg.Name) + "|" + nodeValue, e.Node, sg.GroupType == GroupType.Basic ? "StoreApplicationGroup_16x16.gif" : "WindowsQueryLDAPGroup_16x16.gif");
                        var iconName = sg.GroupType == GroupType.Basic ? GlobalVars.Theme + ".16.mdi-account-multiple.png" : "WindowsQueryLDAPGroup_16x16.gif";
                        Font font4StoreGroup = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                        TreeNode node4App = new TreeNode()
                        {
                            Image = new IconResourceHandle(iconName),
                            Label = " " + sg.Name,
                            NodeFont = font4StoreGroup,
                            Tag = AzManItemType.StoreGroup
                        };
                        parent.Nodes.Add(node4App);
                    }
                    break;
                #endregion
                case AzManItemType.Application:
                    #region Application => Application Groups, Item Definitions, & Item Authorizations
                    TreeNode node4AppGroup = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Application Groups",
                        Tag = AzManItemType.ApplicationGroups
                    };

                    TreeNode node4ItemDef = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Item Definitions",
                        Tag = AzManItemType.ItemDefinitions
                    };
                    TreeNode node4Itemauth = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Item Authorizations",
                        Tag = AzManItemType.ItemAuthorizations
                    };
                    parent.Nodes.AddRange(new[] { node4AppGroup, node4ItemDef, node4Itemauth });
                    LoadNavTree4Store(ref node4AppGroup);
                    LoadNavTree4Store(ref node4ItemDef);
                    LoadNavTree4Store(ref node4Itemauth);
                    break;
                    #endregion
                case AzManItemType.ApplicationGroups:
                    #region Application Groups
                    var gp = parent.Parent;
                    var appGroups = Storage[_StoreName][StripItemName(gp.Label)].GetApplicationGroups();

                    foreach (var ag in appGroups)
                    {
                        var icon = ag.GroupType == GroupType.Basic ? GlobalVars.Theme + ".16.mdi-account-tie.png" : "WindowsQueryLDAPGroup_16x16.gif";
                        tn = new TreeNode()
                        {
                            Image = new IconResourceHandle(ag.GroupType == GroupType.Basic ? GlobalVars.Theme + ".16.mdi-account-tie.png" : "WindowsQueryLDAPGroup_16x16.gif"),
                            Label = ag.Name,
                            Tag = AzManItemType.ApplicationGroup
                        };
                        parent.Nodes.Add(tn);
                    }
                    break;
                    #endregion
                case AzManItemType.ItemDefinitions:
                    #region Item Definitions
                    TreeNode tnRoleDef = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Role Definitions",
                        Tag = AzManItemType.RoleDefinitions
                    };
                    TreeNode tnTaskDef = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Task Definitions",
                        Tag = AzManItemType.TaskDefinitions
                    };
                    TreeNode tnOpDef = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Operation Definitions",
                        Tag = AzManItemType.OperationDefinitions
                    };
                    parent.Nodes.AddRange(new[] { tnRoleDef, tnTaskDef, tnOpDef });
                    LoadNavTree4Store(ref tnRoleDef);
                    LoadNavTree4Store(ref tnTaskDef);
                    LoadNavTree4Store(ref tnOpDef);
                    break;
                    #endregion
                case AzManItemType.ItemAuthorizations:
                    #region Item Authorization
                    TreeNode tnRoleAuth = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Role Authorizations",
                        Tag = AzManItemType.RoleAuthorizations
                    };
                    TreeNode tnTaskAuth = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Task Authorizations",
                        Tag = AzManItemType.TaskAuthorizations
                    };
                    TreeNode tnOpAuth = new TreeNode()
                    {
                        Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder.png"),
                        Label = " Operation Authorizations",
                        Tag = AzManItemType.OperationAuthorizations
                    };
                    parent.Nodes.AddRange(new[] { tnRoleAuth, tnTaskAuth, tnOpAuth });
                    LoadNavTree4Store(ref tnRoleAuth);
                    LoadNavTree4Store(ref tnTaskAuth);
                    LoadNavTree4Store(ref tnOpAuth);
                    break;
                #endregion
                case AzManItemType.RoleDefinitions:
                    #region Role Definitions
                    var roleDefinitions = Storage[_StoreName][StripItemName(ggParent.Label)].GetItems(ItemType.Role);

                    foreach (IAzManItem item in roleDefinitions)
                    {
                        tn = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-briefcase-account.png"),
                            Label = " " + item.Name,
                            Tag = AzManItemType.RoleDefinition  //.RoleAuthorization
                        };
                        parent.Nodes.Add(tn);
                        LoadNavTree4Store(ref tn);
                    }
                    break;
                #endregion
                case AzManItemType.TaskDefinitions:
                    #region Task Definitions
                    var taskDefinitions = Storage[_StoreName][StripItemName(ggParent.Label)].GetItems(ItemType.Task);

                    foreach (IAzManItem item in taskDefinitions)
                    {
                        tn = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-clipboard-check-outline.png"),
                            Label = item.Name,
                            Tag = AzManItemType.TaskDefinition
                        };
                        parent.Nodes.Add(tn);
                        LoadNavTree4Store(ref tn);
                    }
                    break;
                #endregion
                case AzManItemType.OperationDefinitions:
                    #region Operation Definitions
                    var opDefinitions = Storage[_StoreName][StripItemName(ggParent.Label)].GetItems(ItemType.Operation);

                    foreach (IAzManItem item in opDefinitions)
                    {
                        tn = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-cog-outline.png"),
                            Label = " " + item.Name,
                            Tag = AzManItemType.OperationDefinition
                        };
                        parent.Nodes.Add(tn);
                        LoadNavTree4Store(ref tn);
                    }
                    break;
                #endregion
                case AzManItemType.RoleAuthorizations:
                    #region Role Authorization
                    var roleAuth = Storage[_StoreName][StripItemName(ggParent.Label)].GetItems(ItemType.Role);

                    foreach (IAzManItem item in roleAuth)
                    {
                        tn = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-briefcase-account.png"),
                            Label = " " + item.Name,
                            Tag = AzManItemType.RoleAuthorization
                        };
                        parent.Nodes.Add(tn);
                        LoadNavTree4Store(ref tn);
                    }
                    break;
                #endregion
                case AzManItemType.TaskAuthorizations:
                    #region Task Authorization
                    var taskAuth = Storage[_StoreName][StripItemName(ggParent.Label)].GetItems(ItemType.Task);

                    foreach (IAzManItem item in taskAuth)
                    {
                        tn = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-clipboard-check-outline.png"),
                            Label = item.Name,
                            Tag = AzManItemType.TaskAuthorization
                        };
                        parent.Nodes.Add(tn);
                        LoadNavTree4Store(ref tn);
                    }
                    break;
                #endregion
                case AzManItemType.OperationAuthorizations:
                    #region Operation Authorization
                    var opAuth = Storage[_StoreName][StripItemName(ggParent.Label)].GetItems(ItemType.Operation);

                    foreach (IAzManItem item in opAuth)
                    {
                        tn = new TreeNode()
                        {
                            Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-cog-outline.png"),
                            Label = " " + item.Name,
                            Tag = AzManItemType.OperationAuthorization
                        };
                        parent.Nodes.Add(tn);
                        LoadNavTree4Store(ref tn);
                    }
                    break;
                    #endregion
            }
        }

        private void LoadListView(ref TreeNode node)
        {
            TreeNode p = node.Parent;
            TreeNode pp = p == null ? null : p.Parent;
            TreeNode ppp = pp == null ? null : pp.Parent;
            TreeNode pppp = ppp == null ? null : ppp.Parent;

            var tag = (AzManItemType)node.Tag;

            switch (tag)
            {
                case AzManItemType.Storage:
                    #region Storage
                    SelectedObject = Storage;

                    var storageList = new ListViews.Storage();
                    storageList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(storageList);

                    break;
                #endregion
                case AzManItemType.Store:
                    #region Store
                    SelectedObject = Storage[NetSqlAzManHelper.getName(node.Label.Trim(), 0)];

                    var storeList = new ListViews.Store();
                    storeList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(storeList);

                    break;
                #endregion
                case AzManItemType.StoreGroups:
                    #region Store Groups
                    SelectedObject = Storage[NetSqlAzManHelper.getName(p.Label.Trim(), 0)];

                    var storeGroupsList = new ListViews.StoreGroups();
                    storeGroupsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(storeGroupsList);

                    break;
                #endregion
                case AzManItemType.StoreGroup:
                    #region Store Group, ignor LDAP
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pp.Label, 0)].GetStoreGroup(node.Label.Trim());

                    var storeGroupList = new ListViews.StoreGroup();
                    storeGroupList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(storeGroupList);

                    break;
                #endregion
                case AzManItemType.Application:
                    #region Application
                    SelectedObject = Storage[NetSqlAzManHelper.getName(p.Label, 0)][NetSqlAzManHelper.getName(node.Label.Trim(), 0)];

                    var applicationist = new ListViews.Application();
                    applicationist.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(applicationist);

                    break;
                #endregion
                case AzManItemType.ApplicationGroup:
                    #region Application Group
                    SelectedObject = Storage[NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(pp.Label.Trim(), 0)].GetApplicationGroup(node.Label.Trim());

                    var appGroupList = new ListViews.ApplicationGroup();
                    appGroupList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(appGroupList);

                    break;
                #endregion
                case AzManItemType.ApplicationGroups:
                    #region Application Groups
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pp.Label.Trim(), 0)][NetSqlAzManHelper.getName(p.Label.Trim(), 0)];

                    var appGroupsList = new ListViews.ApplicationGroups();
                    appGroupsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(appGroupsList);

                    break;
                #endregion
                case AzManItemType.ItemDefinitions:
                    #region Item Definitions
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pp.Label.Trim(), 0)][NetSqlAzManHelper.getName(p.Label.Trim(), 0)];

                    var ItemDefinitionsList = new ListViews.ItemDefinitions();
                    ItemDefinitionsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(ItemDefinitionsList);

                    break;
                #endregion
                case AzManItemType.ItemAuthorizations:
                    #region Item Authorizations
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pp.Label.Trim(), 0)][NetSqlAzManHelper.getName(p.Label.Trim(), 0)];

                    var ItemAuthorizationsList = new ListViews.ItemAuthorizations();
                    ItemAuthorizationsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(ItemAuthorizationsList);

                    break;
                #endregion
                case AzManItemType.RoleAuthorization:
                    #region Role Definition
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][node.Label.Trim()];

                    var roleAuthorizationList = new ListViews.RoleAuthorization();
                    roleAuthorizationList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(roleAuthorizationList);

                    break;
                #endregion
                case AzManItemType.RoleAuthorizations:
                    #region Role Authorizations
                    SelectedObject = Storage[NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(pp.Label.Trim(), 0)];

                    var roleAuthorizationsList = new ListViews.RoleAuthorizations();
                    roleAuthorizationsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(roleAuthorizationsList);

                    break;
                #endregion
                case AzManItemType.RoleDefinition:
                    #region Role Definition
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][node.Label.Trim()];

                    var roleDefinitionList = new ListViews.RoleDefinition();
                    roleDefinitionList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(roleDefinitionList);

                    break;
                #endregion
                case AzManItemType.RoleDefinitions:
                    #region Role Definitions
                    SelectedObject = Storage[NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(pp.Label.Trim(), 0)];

                    var roleDefinitionsList = new ListViews.RoleDefinitions();
                    roleDefinitionsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(roleDefinitionsList);

                    break;
                #endregion
                case AzManItemType.TaskAuthorization:
                    #region Task Authorization
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][node.Label.Trim()];

                    var taskAuthorizationList = new ListViews.TaskAuthorization();
                    taskAuthorizationList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(taskAuthorizationList);

                    break;
                #endregion
                case AzManItemType.TaskAuthorizations:
                    #region Task Authorizations
                    SelectedObject = Storage[NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(pp.Label.Trim(), 0)];

                    var taskAuthorizationsList = new ListViews.TaskAuthorizations();
                    taskAuthorizationsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(taskAuthorizationsList);

                    break;
                #endregion
                case AzManItemType.TaskDefinition:
                    #region Task Definition
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][node.Label.Trim()];

                    var taskDefinitionList = new ListViews.TaskDefinition();
                    taskDefinitionList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(taskDefinitionList);

                    break;
                #endregion
                case AzManItemType.TaskDefinitions:
                    #region Task Definitions
                    SelectedObject = Storage[NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(pp.Label.Trim(), 0)];

                    var taskDefinitionsList = new ListViews.TaskDefinitions();
                    taskDefinitionsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(taskDefinitionsList);

                    break;
                #endregion
                case AzManItemType.OperationAuthorization:
                    #region Operation Authorization
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][node.Label.Trim()];

                    var operationAuthorizationList = new ListViews.OperationAuthorization();
                    operationAuthorizationList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(operationAuthorizationList);

                    break;
                #endregion
                case AzManItemType.OperationAuthorizations:
                    #region Operation Authorizations
                    SelectedObject = Storage[NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(pp.Label.Trim(), 0)];

                    var operationAuthorizationsList = new ListViews.OperationAuthorizations();
                    operationAuthorizationsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(operationAuthorizationsList);

                    break;
                #endregion
                case AzManItemType.OperationDefinition:
                    #region Operation Definition
                    SelectedObject = Storage[NetSqlAzManHelper.getName(pppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][node.Label.Trim()];

                    var operationDefinitionList = new ListViews.OperationDefinition();
                    operationDefinitionList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(operationDefinitionList);

                    break;
                #endregion
                case AzManItemType.OperationDefinitions:
                    #region Operation Definitions
                    SelectedObject = Storage[NetSqlAzManHelper.getName(ppp.Label.Trim(), 0)][NetSqlAzManHelper.getName(pp.Label.Trim(), 0)];

                    var operationDefinitionsList = new ListViews.OperationDefinitions();
                    operationDefinitionsList.Dock = DockStyle.Fill;

                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(operationDefinitionsList);

                    break;
                    #endregion
            }
        }

        /// <summary>
        /// split it with '(', return the first word, trimmed
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private String StripItemName(String source)
        {
            var result = "";

            result = source.Split('(')[0].Trim();

            return result;
        }
    }
}