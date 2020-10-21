using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Definition
{
    partial class BizRule
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
            this.panel = new Gizmox.WebGUI.Forms.Panel();
            this.txtSource = new Gizmox.WebGUI.Forms.TextBox();
            this.gbxLanguage = new Gizmox.WebGUI.Forms.GroupBox();
            this.radCSharp = new Gizmox.WebGUI.Forms.RadioButton();
            this.radVBNet = new Gizmox.WebGUI.Forms.RadioButton();
            this.toolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.gbxLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(480, 290);
            this.panel.TabIndex = 1;
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(12, 93);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Both;
            this.txtSource.Size = new System.Drawing.Size(616, 370);
            this.txtSource.TabIndex = 2;
            // 
            // gbxLanguage
            // 
            this.gbxLanguage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.gbxLanguage.Controls.Add(this.radVBNet);
            this.gbxLanguage.Controls.Add(this.radCSharp);
            this.gbxLanguage.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxLanguage.Location = new System.Drawing.Point(12, 28);
            this.gbxLanguage.Name = "gbxLanguage";
            this.gbxLanguage.Size = new System.Drawing.Size(616, 53);
            this.gbxLanguage.TabIndex = 1;
            this.gbxLanguage.TabStop = false;
            this.gbxLanguage.Text = "Language";
            // 
            // radCSharp
            // 
            this.radCSharp.AutoSize = true;
            this.radCSharp.Checked = true;
            this.radCSharp.Location = new System.Drawing.Point(13, 21);
            this.radCSharp.Name = "radCSharp";
            this.radCSharp.Size = new System.Drawing.Size(38, 17);
            this.radCSharp.TabIndex = 0;
            this.radCSharp.Text = "c#";
            // 
            // radVBNet
            // 
            this.radVBNet.AutoSize = true;
            this.radVBNet.Location = new System.Drawing.Point(132, 21);
            this.radVBNet.Name = "radVBNet";
            this.radVBNet.Size = new System.Drawing.Size(60, 17);
            this.radVBNet.TabIndex = 1;
            this.radVBNet.Text = "VB.NET";
            // 
            // toolbar
            // 
            this.toolbar.DragHandle = true;
            this.toolbar.DropDownArrows = true;
            this.toolbar.ImageSize = new System.Drawing.Size(16, 16);
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.MenuHandle = true;
            this.toolbar.Name = "toolbar";
            this.toolbar.ShowToolTips = true;
            this.toolbar.Size = new System.Drawing.Size(640, 42);
            this.toolbar.TabIndex = 0;
            // 
            // BizRule
            // 
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.gbxLanguage);
            this.Controls.Add(this.txtSource);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Store";
            this.Load += new System.EventHandler(this.Form_Load);
            this.gbxLanguage.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private Panel panel;
        private TextBox txtSource;
        private GroupBox gbxLanguage;
        private RadioButton radVBNet;
        private RadioButton radCSharp;
        private ToolBar toolbar;
    }
}