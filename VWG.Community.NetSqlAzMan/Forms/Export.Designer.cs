using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms
{
    partial class Export
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
            this.gbxOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkAuthorization = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkWindowsUser = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDbUser = new Gizmox.WebGUI.Forms.CheckBox();
            this.panel.SuspendLayout();
            this.gbxOptions.SuspendLayout();
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
            this.panel.Controls.Add(this.gbxOptions);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(640, 450);
            this.panel.TabIndex = 1;
            // 
            // gbxOptions
            // 
            this.gbxOptions.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.gbxOptions.Controls.Add(this.chkDbUser);
            this.gbxOptions.Controls.Add(this.chkWindowsUser);
            this.gbxOptions.Controls.Add(this.chkAuthorization);
            this.gbxOptions.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxOptions.Location = new System.Drawing.Point(9, 9);
            this.gbxOptions.Name = "gbxOptions";
            this.gbxOptions.Size = new System.Drawing.Size(622, 101);
            this.gbxOptions.TabIndex = 0;
            this.gbxOptions.TabStop = false;
            this.gbxOptions.Text = "Export Options";
            // 
            // chkAuthorization
            // 
            this.chkAuthorization.AutoSize = true;
            this.chkAuthorization.Checked = true;
            this.chkAuthorization.CheckState = Gizmox.WebGUI.Forms.CheckState.Checked;
            this.chkAuthorization.Location = new System.Drawing.Point(15, 26);
            this.chkAuthorization.Name = "chkAuthorization";
            this.chkAuthorization.Size = new System.Drawing.Size(158, 17);
            this.chkAuthorization.TabIndex = 0;
            this.chkAuthorization.Text = "Include Item Authorizations";
            // 
            // chkWindowsUser
            // 
            this.chkWindowsUser.AutoSize = true;
            this.chkWindowsUser.Enabled = false;
            this.chkWindowsUser.Location = new System.Drawing.Point(15, 50);
            this.chkWindowsUser.Name = "chkWindowsUser";
            this.chkWindowsUser.Size = new System.Drawing.Size(181, 17);
            this.chkWindowsUser.TabIndex = 0;
            this.chkWindowsUser.Text = "Include Windows Users / Groups";
            // 
            // chkDbUser
            // 
            this.chkDbUser.AutoSize = true;
            this.chkDbUser.Location = new System.Drawing.Point(15, 72);
            this.chkDbUser.Name = "chkDbUser";
            this.chkDbUser.Size = new System.Drawing.Size(140, 17);
            this.chkDbUser.TabIndex = 0;
            this.chkDbUser.Text = "Include Database Users";
            // 
            // Export
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            this.gbxOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private GroupBox gbxOptions;
        private CheckBox chkDbUser;
        private CheckBox chkWindowsUser;
        private CheckBox chkAuthorization;
    }
}