using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Authorization
{
    partial class DBUsersList
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
            this.lvwStoreGroups = new Gizmox.WebGUI.Forms.ListView();
            this.panel.SuspendLayout();
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
            this.panel.Controls.Add(this.lvwStoreGroups);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(640, 450);
            this.panel.TabIndex = 1;
            // 
            // lvwAuth
            // 
            this.lvwStoreGroups.DataMember = null;
            this.lvwStoreGroups.Location = new System.Drawing.Point(213, 136);
            this.lvwStoreGroups.Name = "lvwAuth";
            this.lvwStoreGroups.Size = new System.Drawing.Size(194, 100);
            this.lvwStoreGroups.TabIndex = 0;
            // 
            // Authorization
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private ListView lvwStoreGroups;
    }
}