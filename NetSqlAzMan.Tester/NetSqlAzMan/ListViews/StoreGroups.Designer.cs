using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace NetSqlAzMan.Tester.NetSqlAzMan.ListViews
{
    partial class StoreGroups
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

        #region Visual WebGui UserControl Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolBar = new Gizmox.WebGUI.Forms.ToolBar();
            this.listView = new Gizmox.WebGUI.Forms.ListView();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.ButtonSize = new System.Drawing.Size(24, 24);
            this.toolBar.DragHandle = true;
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageSize = new System.Drawing.Size(16, 16);
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.MenuHandle = true;
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(391, 30);
            this.toolBar.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.DataMember = null;
            this.listView.Location = new System.Drawing.Point(123, 113);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(100, 100);
            this.listView.TabIndex = 1;
            // 
            // Storage
            // 
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolBar);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "Storage";
            this.Load += new System.EventHandler(this.Storage_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolBar;
        private ListView listView;
    }
}