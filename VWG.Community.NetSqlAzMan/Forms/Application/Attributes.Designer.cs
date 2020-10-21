using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    partial class Attributes
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
            this.lvwAttributes = new Gizmox.WebGUI.Forms.ListView();
            this.cmdAddNew = new Gizmox.WebGUI.Forms.PictureBox();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddNew)).BeginInit();
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
            this.toolbar.Size = new System.Drawing.Size(480, 30);
            this.toolbar.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.cmdAddNew);
            this.panel.Controls.Add(this.lvwAttributes);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(480, 290);
            this.panel.TabIndex = 1;
            // 
            // lvwAttributes
            // 
            this.lvwAttributes.DataMember = null;
            this.lvwAttributes.Location = new System.Drawing.Point(89, 17);
            this.lvwAttributes.Name = "lvwAuth";
            this.lvwAttributes.Size = new System.Drawing.Size(194, 100);
            this.lvwAttributes.TabIndex = 0;
            // 
            // cmdAddNew
            // 
            this.cmdAddNew.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Bottom | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdAddNew.Location = new System.Drawing.Point(411, 252);
            this.cmdAddNew.Name = "cmdAddNew";
            this.cmdAddNew.Size = new System.Drawing.Size(48, 48);
            this.cmdAddNew.TabIndex = 2;
            this.cmdAddNew.TabStop = false;
            this.cmdAddNew.Click += new System.EventHandler(this.cmdAddNew_Click);
            // 
            // Attributes
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(480, 320);
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddNew)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private ListView lvwAttributes;
        private PictureBox cmdAddNew;
        private ToolTip toolTip1;
    }
}