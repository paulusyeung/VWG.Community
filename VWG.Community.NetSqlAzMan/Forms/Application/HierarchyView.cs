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

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    public partial class HierarchyView : Form
    {
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;

        public HierarchyView()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _Storage = (IAzManStorage)Session["storage"];

            if (Session["selectedObject"] as IAzManStore != null)
            {
                _Store = this.Session["selectedObject"] as IAzManStore;
            }
            if (Session["selectedObject"] as IAzManApplication != null)
            {
                _Application = this.Session["selectedObject"] as IAzManApplication;
                _Store = _Application.Store;
            }

            SetAttributes();
            SetToolBar();
            LoadHierarchyTree();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Store;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            tvwHierarchyView.BackColor = Color.Transparent;
            //tvwHierarchyView.BorderStyle = BorderStyle.None;
            tvwHierarchyView.Dock = DockStyle.Fill;
            tvwHierarchyView.Margin = new Padding(8, 8, 8, 8);
            tvwHierarchyView.ShowLines = true;
            tvwHierarchyView.ShowPlusMinus = true;

            Text = "Hierarchy View - " + _Application.Name;
            FormClosed += Form_Close;
        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdSave cmdDelete
            var cmdSave = new ToolBarButton("ExpandAll", ("Expand All"));
            cmdSave.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-arrow-expand.png");

            var cmdDelete = new ToolBarButton("CollapseAll", ("Collapse All"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-arrow-collapse.png");

            var cmdAttributes = new ToolBarButton("Attributes", ("Attributes"));
            cmdAttributes.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdPermissions = new ToolBarButton("Permissions", ("Permissions"));
            cmdPermissions.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            toolbar.Buttons.Add(cmdSave);
            toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdDelete);
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        #region Load Record
        private void LoadHierarchyTree()
        {
            tvwHierarchyView.Nodes.Clear();

            Font font = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);
            TreeNode root = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-domain.png"),
                Label = _Store.Name,
                NodeFont = font,
                Tag = Enums.AzManItemType.Store
            };
            root.Expand();

            //TreeNode app = new TreeNode()
            //{
            //    Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder-cog-outline.png"),
            //    Label = _Application.Name,
            //    //NodeFont = font,
            //    Tag = Enums.AzManItemType.Application
            //};
            //app.Expand();
            //root.Nodes.Add(app);

            LoadChildNodes(ref root, _Application);
            tvwHierarchyView.Nodes.Add(root);
        }

        private void LoadChildNodes(ref TreeNode parent, IAzManApplication app)
        {
            Font font = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            TreeNode node = new TreeNode()
            {
                Image = new IconResourceHandle(GlobalVars.Theme + ".16.mdi-folder-cog-outline.png"),
                Label = _Application.Name,
                NodeFont = font,
                Tag = Enums.AzManItemType.Application
            };
            //TreeNode node = new TreeNode(app.Name, app.Name, this.getImageUrl("Application_16x16.gif"));
            //node.ToolTip = app.Description;
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
            if (app.Store.Storage.Mode == NetSqlAzManMode.Developer)
            {
                foreach (IAzManItem item in app.Items.Values)
                {
                    if (item.ItemType == ItemType.Operation)
                    {
                        if (item.ItemsWhereIAmAMember.Count == 0) this.AddOperation(node, item, node);
                    }
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
                //Tag = Enums.AzManItemType.Application
            };
            //TreeNode node = new TreeNode(item.Name, item.Name, this.getImageUrl("Role_16x16.gif"));
            //node.ToolTip = item.Description;
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
            if (item.Application.Store.Storage.Mode == NetSqlAzManMode.Developer)
            {
                foreach (IAzManItem subItem in item.Members.Values)
                {
                    if (subItem.ItemType == ItemType.Operation)
                    {
                        this.AddOperation(node, subItem, applicationNode);
                    }
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
                //Tag = Enums.AzManItemType.Application
            };
            //TreeNode node = new TreeNode(item.Name, item.Name, this.getImageUrl("Task_16x16.gif"));
            //node.ToolTip = item.Description;
            parent.Nodes.Add(node);
            foreach (IAzManItem subItem in item.Members.Values)
            {
                if (subItem.ItemType == ItemType.Task)
                {
                    this.AddTask(node, subItem, applicationNode);
                }
            }
            if (item.Application.Store.Storage.Mode == NetSqlAzManMode.Developer)
            {
                foreach (IAzManItem subItem in item.Members.Values)
                {
                    if (subItem.ItemType == ItemType.Operation)
                    {
                        this.AddOperation(node, subItem, applicationNode);
                    }
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
                //Tag = Enums.AzManItemType.Application
            };
            //TreeNode node = new TreeNode(item.Name, item.Name, this.getImageUrl("Operation_16x16.gif"));
            //node.ToolTip = item.Description;
            parent.Nodes.Add(node);
            foreach (IAzManItem subItem in item.Members.Values)
            {
                this.AddOperation(node, subItem, applicationNode);
            }
            node.Collapse();
        }

        #endregion

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "expandall":
                        tvwHierarchyView.ExpandAll();
                        break;
                    case "collapseall":
                        tvwHierarchyView.CollapseAll();
                        break;
                }
            }
        }
    }
}