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
using NetSqlAzMan.Cache;

#endregion

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    public partial class CheckAccess : Form
    {
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;

        private IAzManDBUser _DbUser = null;
        private UserPermissionCache _UserPermissionCache = null;
        private StorageCache _StorageCache = null;


        #region public properties
        private bool _IsDirty = false;
        public bool IsDirty
        {
            get { return _IsDirty; }
            set { _IsDirty = value; }
        }
        #endregion

        public CheckAccess()
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

            SetAttributes();
            SetToolBar();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Application;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            Text = "Check Access - " + _Application.Name;
            txtValidFor.Text = DateTime.Now.ToString();

            cmdLookup.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-account-search.png");
            cmdLookup.Click += cmdLookup_Click;
            toolTip1.SetToolTip(cmdLookup, "Lookup DB User");

            cmdCheckAccess.Click += cmdCheckAccess_Click;

            this.txtDetails.Font = new System.Drawing.Font("Consolas", 8.25F);

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

            var cmdAttributes = new ToolBarButton("Attributes", ("Attributes"));
            cmdAttributes.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdPermissions = new ToolBarButton("Permissions", ("Permissions"));
            cmdPermissions.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            toolbar.Buttons.Add(cmdSave);
            //toolbar.Buttons.Add(sep);
            //if (_Mode == Mode.Update)
            //{
            //    toolbar.Buttons.Add(cmdDelete);
            //    toolbar.Buttons.Add(sep);
            //    toolbar.Buttons.Add(cmdAttributes);
            //    toolbar.Buttons.Add(cmdPermissions);
            //}
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "save":
                        //SaveRecord();
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

        private void cmdCheckAccess_Click(object sender, EventArgs e)
        {
            if (txtUser.Tag != null)
            {
                _DbUser = (IAzManDBUser)txtUser.Tag;
                GetDetails(_DbUser);
            }
        }

        #region Lookup DBUser
        private void cmdLookup_Click(object sender, EventArgs e)
        {
            var form = new DBUsersList();
            form.FormClosed += DBUserList_FormClosed;
            form.ShowDialog();
        }

        private void DBUserList_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (DBUsersList)sender;
            if (form.SelectedItems != null)
            {
                if (form.SelectedItems.Count > 0)
                {
                    var item = form.SelectedItems[0];
                    if (item != null)
                    {
                        IAzManDBUser dbUser = _Application.GetDBUser(item.SubItems[0].Text);
                        txtUser.Tag = dbUser;
                        txtUser.Text = dbUser.UserName;

                        txtDetails.Text = String.Empty;
                        tvwResults.Nodes.Clear();
                    }
                }
            }
        }

        private void GetDetails(IAzManDBUser dbUser)
        {
            txtDetails.Text = String.Format(@"
Check Access Test started at {0}

Identity Details:
Name: {1}Custom Sid: {2}
", DateTime.Now.ToString(), dbUser.UserName + "\t", dbUser.CustomSid);

            txtDetails.Text += "Member of these Store Groups:" + Environment.NewLine;

            foreach (IAzManStoreGroup storeGroup in _Application.Store.GetStoreGroups())
            {
                if (storeGroup.IsInGroup(dbUser))
                {
                    txtDetails.Text += storeGroup.Name + Environment.NewLine;
                }
            }

            txtDetails.Text += "Member of these Application Groups:" + Environment.NewLine;
            foreach (IAzManApplicationGroup applicationGroup in _Application.GetApplicationGroups())
            {
                if (applicationGroup.IsInGroup(dbUser))
                {
                    txtDetails.Text += applicationGroup.Name + Environment.NewLine;
                }
            }

            #region Load Hierarchy();
            txtDetails.Text += Environment.NewLine;
            txtDetails.Text += "Building Items Hierarchy ...";

            tvwResults.Nodes.Clear();
            BuildApplicationsTreeView();
            tvwResults.ExpandAll();

            txtDetails.Text += "Done." + Environment.NewLine + Environment.NewLine;
            #endregion

            #region Check Access
            TreeNode applicationTreeNode = tvwResults.Nodes[0].Nodes[0];
            foreach (TreeNode itemTreeNode in applicationTreeNode.Nodes)
            {
                CheckNodeAccess(itemTreeNode);
            }

            txtDetails.Text += Environment.NewLine;
            txtDetails.Text += "Check Access Test finished at " + DateTime.Now.ToString();
            #endregion
        }
        #endregion

        #region Build Items Hierarchy

        internal bool findNode(TreeNode startingNode, string text)
        {
            foreach (TreeNode childNode in startingNode.Nodes)
            {
                if (childNode.Text.Equals(text))
                    return true;
                if (this.findNode(childNode, text))
                    return true;
            }
            return false;
        }

        internal TreeNode findTreeNode(TreeNode startingNode, string text)
        {
            if (String.Compare(startingNode.Text, text, true) == 0)
                return startingNode;
            foreach (TreeNode childNode in startingNode.Nodes)
            {
                TreeNode result = this.findTreeNode(childNode, text);
                if (result != null)
                    return result;
            }
            return null;
        }

        internal protected void BuildApplicationsTreeView()
        {
            Font font = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);
            TreeNode root = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-domain.png"),
                Label = _Store.Name,
                NodeFont = font,
            };

            this.add(root, _Application);
            root.Expand();

            tvwResults.Nodes.Add(root);
        }

        private void add(TreeNode parent, IAzManApplication app)
        {
            Font font = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            TreeNode node = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder-cog-outline.png"),
                Label = app.Name,
                NodeFont = font,
            };
            //TreeNode node = new TreeNode(app.Name, app.Name, this.getImageUrl("Application_16x16.gif"));
            parent.Nodes.Add(node);
            node.Expand();
            foreach (IAzManItem item in app.Items.Values)
            {
                if (item.ItemType == ItemType.Role)
                {
                    if (item.ItemsWhereIAmAMember.Count == 0) this.AddRole(node, item, node);
                }
            }
            foreach (IAzManItem item in app.Items.Values)
            {
                if (item.ItemType == ItemType.Task)
                {
                    if (item.ItemsWhereIAmAMember.Count == 0) this.AddTask(node, item, node);
                }
            }
            foreach (IAzManItem item in app.Items.Values)
            {
                if (item.ItemType == ItemType.Operation)
                {
                    if (item.ItemsWhereIAmAMember.Count == 0) this.AddOperation(node, item, node);
                }
            }
            node.Collapse();
        }

        private void AddRole(TreeNode parent, IAzManItem item, TreeNode applicationNode)
        {
            Font font = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            TreeNode node = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-briefcase-account.png"),
                Label = item.Name,
                NodeFont = font,
                Tag = ItemType.Role
            };
            //TreeNode node = new TreeNode(item.Name, item.Name, this.getImageUrl("Role_16x16.gif"));
            parent.Nodes.Add(node);
            foreach (IAzManItem subItem in item.Members.Values)
            {
                if (subItem.ItemType == ItemType.Role)
                {
                    this.AddRole(node, subItem, applicationNode);
                }
            }
            foreach (IAzManItem subItem in item.Members.Values)
            {
                if (subItem.ItemType == ItemType.Task)
                {
                    this.AddTask(node, subItem, applicationNode);
                }
            }
            foreach (IAzManItem subItem in item.Members.Values)
            {
                if (subItem.ItemType == ItemType.Operation)
                {
                    this.AddOperation(node, subItem, applicationNode);
                }
            }
            node.Collapse();
        }

        private void AddTask(TreeNode parent, IAzManItem item, TreeNode applicationNode)
        {
            Font font = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            TreeNode node = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-clipboard-check-outline.png"),
                Label = item.Name,
                NodeFont = font,
                Tag = ItemType.Task
            };
            //TreeNode node = new TreeNode(item.Name, item.Name, this.getImageUrl("Task_16x16.gif"));
            parent.Nodes.Add(node);
            foreach (IAzManItem subItem in item.Members.Values)
            {
                if (subItem.ItemType == ItemType.Task)
                {
                    this.AddTask(node, subItem, applicationNode);
                }
            }
            foreach (IAzManItem subItem in item.Members.Values)
            {
                if (subItem.ItemType == ItemType.Operation)
                {
                    this.AddOperation(node, subItem, applicationNode);
                }
            }
            node.Collapse();
        }
        private void AddOperation(TreeNode parent, IAzManItem item, TreeNode applicationNode)
        {
            Font font = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            TreeNode node = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-cog-outline.png"),
                Label = item.Name,
                NodeFont = font,
                Tag = ItemType.Operation
            };
            //TreeNode node = new TreeNode(item.Name, item.Name, this.getImageUrl("Operation_16x16.gif"));
            parent.Nodes.Add(node);
            foreach (IAzManItem subItem in item.Members.Values)
            {
                this.AddOperation(node, subItem, applicationNode);
            }
            node.Collapse();
        }
        #endregion

        #region Check Access
        private void CheckNodeAccess(TreeNode tn)
        {
            var tag = (ItemType)tn.Tag;
            string sItemType = String.Empty;
            switch (tag)
            {
                case ItemType.Role:
                    sItemType = "Role";
                    break;
                case ItemType.Task:
                    sItemType = "Task";
                    break;
                case ItemType.Operation:
                    sItemType = "Operation";
                    break;
            }

            AuthorizationType auth = AuthorizationType.Neutral;
            string sAuth = String.Empty;
            DateTime chkStart = DateTime.Now;
            TimeSpan elapsedTime = TimeSpan.Zero;
            DateTime chkEnd = DateTime.Now;
            List<KeyValuePair<string, string>> attributes = null;
            //Cache Build
            if (chkUserPermisisonCache.Checked && _UserPermissionCache == null)
            {
                txtDetails.Text += "Building UserPermissionCache ... " + Environment.NewLine;
                _UserPermissionCache = new UserPermissionCache(_Storage, _Store.Name, _Application.Name, _DbUser, true, false);
                chkEnd = DateTime.Now;
                elapsedTime = (TimeSpan)chkEnd.Subtract(chkStart);
                txtDetails.Text += String.Format("[{0} mls.]\r\n", elapsedTime.TotalMilliseconds) + Environment.NewLine;
            }
            else if (chkStorageCache.Checked && _StorageCache == null)
            {
                txtDetails.Text += "Building StorageCache ... " + Environment.NewLine;
                _StorageCache = new StorageCache(_Storage.ConnectionString);
                _StorageCache.BuildStorageCache(_Store.Name, _Application.Name);
                chkEnd = DateTime.Now;
                elapsedTime = (TimeSpan)chkEnd.Subtract(chkStart);
                txtDetails.Text += String.Format("[{0} mls.]\r\n", elapsedTime.TotalMilliseconds) + Environment.NewLine;
            }
            chkStart = DateTime.Now;
            elapsedTime = TimeSpan.Zero;
            txtDetails.Text += String.Format("{0} {1} '{2}' ... ", "Check Access Test on", sItemType, tn.Text);

            try
            {
                if (chkUserPermisisonCache.Checked)
                {
                    auth = _UserPermissionCache.CheckAccess(
                        tn.Text,
                        !String.IsNullOrEmpty(txtValidFor.Text) ? Convert.ToDateTime(txtValidFor.Text) : DateTime.Now,
                        out attributes);
                }
                else if (this.chkStorageCache.Checked)
                {
                    auth = _StorageCache.CheckAccess(
                        _Store.Name, 
                        _Application.Name, 
                        tn.Text, _DbUser.CustomSid.StringValue, 
                        !String.IsNullOrEmpty(txtValidFor.Text) ? Convert.ToDateTime(txtValidFor.Text) : DateTime.Now,
                        false,
                        out attributes);
                }
                else
                {
                    auth = _Storage.CheckAccess(
                        _Store.Name, 
                        _Application.Name, 
                        tn.Text, _DbUser, 
                        !String.IsNullOrEmpty(txtValidFor.Text) ? Convert.ToDateTime(txtValidFor.Text) : DateTime.Now,
                        false,
                        out attributes);
                }

                chkEnd = DateTime.Now;
                elapsedTime = (TimeSpan)chkEnd.Subtract(chkStart);
                sAuth = "Neutral";
                switch (auth)
                {
                    case AuthorizationType.AllowWithDelegation:
                        sAuth = "Allow with Delegation";
                        break;
                    case AuthorizationType.Allow:
                        sAuth = "Allow";
                        break;
                    case AuthorizationType.Deny:
                        sAuth = "Deny";
                        break;
                    case AuthorizationType.Neutral:
                        sAuth = "Neutral";
                        break;
                }
                //tn.ToolTip = sAuth;
                txtDetails.Text += String.Format("{0} [{1} mls.]", sAuth, elapsedTime.TotalMilliseconds) + Environment.NewLine;
                if (attributes != null && attributes.Count > 0)
                {
                    txtDetails.Text += String.Format(" {0} attribute(s) found:", attributes.Count) + Environment.NewLine;
                    int attributeIndex = 0;
                    foreach (KeyValuePair<string, string> attr in attributes)
                    {
                        txtDetails.Text += String.Format("  {0}) Key: {1} Value: {2}", ++attributeIndex, attr.Key, attr.Value) + Environment.NewLine;
                    }
                }
            }
            catch (Exception ex)
            {
                sAuth = "Check Access Test Error";
                txtDetails.Text += String.Format("{0} [{1} mls.]", ex.Message, elapsedTime.TotalMilliseconds) + Environment.NewLine;
            }
            tn.Text = String.Format("{0} - ({1})", tn.Text, sAuth.ToUpper());
            foreach (TreeNode tnChild in tn.Nodes)
            {
                CheckNodeAccess(tnChild);
            }
        }
        #endregion
    }
}