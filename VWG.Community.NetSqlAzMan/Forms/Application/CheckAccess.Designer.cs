using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    partial class CheckAccess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Visual WebGui Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.panel = new Gizmox.WebGUI.Forms.Panel();
            this.cmdCheckAccess = new Gizmox.WebGUI.Forms.Button();
            this.chkStorageCache = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkUserPermisisonCache = new Gizmox.WebGUI.Forms.CheckBox();
            this.cmdLookup = new Gizmox.WebGUI.Forms.Button();
            this.gbxResults = new Gizmox.WebGUI.Forms.GroupBox();
            this.tvwResults = new Gizmox.WebGUI.Forms.TreeView();
            this.gbxDetails = new Gizmox.WebGUI.Forms.GroupBox();
            this.txtDetails = new Gizmox.WebGUI.Forms.TextBox();
            this.txtUser = new Gizmox.WebGUI.Forms.TextBox();
            this.lblUser = new Gizmox.WebGUI.Forms.Label();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.lblValidFor = new Gizmox.WebGUI.Forms.Label();
            this.txtValidFor = new Gizmox.WebGUI.Forms.TextBox();
            this.panel.SuspendLayout();
            this.gbxResults.SuspendLayout();
            this.gbxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.ButtonSize = new System.Drawing.Size(24, 24);
            this.toolbar.DragHandle = true;
            this.toolbar.DropDownArrows = true;
            this.toolbar.ImageSize = new System.Drawing.Size(16, 16);
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.MenuHandle = true;
            this.toolbar.Name = "toolbar";
            this.toolbar.ShowToolTips = true;
            this.toolbar.Size = new System.Drawing.Size(640, 30);
            this.toolbar.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.txtValidFor);
            this.panel.Controls.Add(this.lblValidFor);
            this.panel.Controls.Add(this.cmdCheckAccess);
            this.panel.Controls.Add(this.chkStorageCache);
            this.panel.Controls.Add(this.chkUserPermisisonCache);
            this.panel.Controls.Add(this.cmdLookup);
            this.panel.Controls.Add(this.gbxResults);
            this.panel.Controls.Add(this.gbxDetails);
            this.panel.Controls.Add(this.txtUser);
            this.panel.Controls.Add(this.lblUser);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(640, 450);
            this.panel.TabIndex = 1;
            // 
            // cmdCheckAccess
            // 
            this.cmdCheckAccess.Location = new System.Drawing.Point(390, 12);
            this.cmdCheckAccess.Name = "cmdCheckAccess";
            this.cmdCheckAccess.Size = new System.Drawing.Size(238, 23);
            this.cmdCheckAccess.TabIndex = 5;
            this.cmdCheckAccess.Text = "Start Check Access";
            // 
            // chkStorageCache
            // 
            this.chkStorageCache.AutoSize = true;
            this.chkStorageCache.Location = new System.Drawing.Point(459, 67);
            this.chkStorageCache.Name = "chkStorageCache";
            this.chkStorageCache.Size = new System.Drawing.Size(97, 17);
            this.chkStorageCache.TabIndex = 7;
            this.chkStorageCache.Text = "Storage Cache";
            // 
            // chkUserPermisisonCache
            // 
            this.chkUserPermisisonCache.AutoSize = true;
            this.chkUserPermisisonCache.Location = new System.Drawing.Point(432, 44);
            this.chkUserPermisisonCache.Name = "chkUserPermisisonCache";
            this.chkUserPermisisonCache.Size = new System.Drawing.Size(134, 17);
            this.chkUserPermisisonCache.TabIndex = 6;
            this.chkUserPermisisonCache.Text = "User Permission Cache";
            // 
            // cmdLookup
            // 
            this.cmdLookup.Location = new System.Drawing.Point(246, 34);
            this.cmdLookup.Name = "cmdLookup";
            this.cmdLookup.Size = new System.Drawing.Size(28, 26);
            this.cmdLookup.TabIndex = 4;
            // 
            // gbxResults
            // 
            this.gbxResults.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Bottom) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.gbxResults.Controls.Add(this.tvwResults);
            this.gbxResults.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxResults.Location = new System.Drawing.Point(12, 256);
            this.gbxResults.Name = "gbxResults";
            this.gbxResults.Size = new System.Drawing.Size(616, 219);
            this.gbxResults.TabIndex = 9;
            this.gbxResults.TabStop = false;
            this.gbxResults.Text = "Check Access Results";
            // 
            // tvwResults
            // 
            this.tvwResults.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tvwResults.Location = new System.Drawing.Point(3, 17);
            this.tvwResults.Name = "tvwResults";
            this.tvwResults.Size = new System.Drawing.Size(610, 199);
            this.tvwResults.TabIndex = 0;
            // 
            // gbxDetails
            // 
            this.gbxDetails.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.gbxDetails.Controls.Add(this.txtDetails);
            this.gbxDetails.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxDetails.Location = new System.Drawing.Point(12, 87);
            this.gbxDetails.Name = "gbxDetails";
            this.gbxDetails.Size = new System.Drawing.Size(616, 156);
            this.gbxDetails.TabIndex = 8;
            this.gbxDetails.TabStop = false;
            this.gbxDetails.Text = "Details";
            // 
            // txtDetails
            // 
            this.txtDetails.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.txtDetails.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtDetails.Location = new System.Drawing.Point(3, 17);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtDetails.Size = new System.Drawing.Size(610, 136);
            this.txtDetails.TabIndex = 0;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(103, 37);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(143, 20);
            this.txtUser.TabIndex = 3;
            // 
            // lblUser
            // 
            this.lblUser.Location = new System.Drawing.Point(18, 41);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(82, 13);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Database User:";
            // 
            // lblValidFor
            // 
            this.lblValidFor.Location = new System.Drawing.Point(18, 13);
            this.lblValidFor.Name = "lblValidFor";
            this.lblValidFor.Size = new System.Drawing.Size(82, 13);
            this.lblValidFor.TabIndex = 0;
            this.lblValidFor.Text = "Valid For:";
            // 
            // txtValidFor
            // 
            this.txtValidFor.Location = new System.Drawing.Point(103, 9);
            this.txtValidFor.Name = "txtValidFor";
            this.txtValidFor.Size = new System.Drawing.Size(143, 20);
            this.txtValidFor.TabIndex = 1;
            // 
            // CheckAccess
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            this.gbxResults.ResumeLayout(false);
            this.gbxDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private TextBox txtDetails;
        private TextBox txtUser;
        private Label lblUser;
        private GroupBox gbxResults;
        private TreeView tvwResults;
        private GroupBox gbxDetails;
        private Button cmdCheckAccess;
        private CheckBox chkStorageCache;
        private CheckBox chkUserPermisisonCache;
        private Button cmdLookup;
        private ToolTip toolTip1;
        private TextBox txtValidFor;
        private Label lblValidFor;
    }
}