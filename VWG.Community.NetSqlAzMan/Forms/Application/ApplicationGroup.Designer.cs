using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    partial class ApplicationGroup
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
            this.lblDescription = new Gizmox.WebGUI.Forms.Label();
            this.txtDescription = new Gizmox.WebGUI.Forms.TextBox();
            this.txtName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblName = new Gizmox.WebGUI.Forms.Label();
            this.radLDAP = new Gizmox.WebGUI.Forms.RadioButton();
            this.radBasic = new Gizmox.WebGUI.Forms.RadioButton();
            this.gbxGroupType = new Gizmox.WebGUI.Forms.GroupBox();
            this.panel.SuspendLayout();
            this.gbxGroupType.SuspendLayout();
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
            this.panel.Controls.Add(this.gbxGroupType);
            this.panel.Controls.Add(this.lblDescription);
            this.panel.Controls.Add(this.txtDescription);
            this.panel.Controls.Add(this.txtName);
            this.panel.Controls.Add(this.lblName);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(640, 450);
            this.panel.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 61);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(35, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(12, 81);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(616, 114);
            this.txtDescription.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(12, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(616, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // radLDAP
            // 
            this.radLDAP.AutoSize = true;
            this.radLDAP.Enabled = false;
            this.radLDAP.Location = new System.Drawing.Point(98, 26);
            this.radLDAP.Name = "radLDAP";
            this.radLDAP.Size = new System.Drawing.Size(50, 17);
            this.radLDAP.TabIndex = 1;
            this.radLDAP.Text = "LDAP";
            // 
            // radBasic
            // 
            this.radBasic.AutoSize = true;
            this.radBasic.Checked = true;
            this.radBasic.Location = new System.Drawing.Point(18, 26);
            this.radBasic.Name = "radBasic";
            this.radBasic.Size = new System.Drawing.Size(49, 17);
            this.radBasic.TabIndex = 0;
            this.radBasic.Text = "Basic";
            // 
            // gbxGroupType
            // 
            this.gbxGroupType.Controls.Add(this.radLDAP);
            this.gbxGroupType.Controls.Add(this.radBasic);
            this.gbxGroupType.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxGroupType.Location = new System.Drawing.Point(12, 207);
            this.gbxGroupType.Name = "gbxGroupType";
            this.gbxGroupType.Size = new System.Drawing.Size(616, 58);
            this.gbxGroupType.TabIndex = 2;
            this.gbxGroupType.TabStop = false;
            this.gbxGroupType.Text = "Group Type:";
            // 
            // ApplicationGroup
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            this.gbxGroupType.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private Label lblDescription;
        private TextBox txtDescription;
        private TextBox txtName;
        private Label lblName;
        private GroupBox gbxGroupType;
        private RadioButton radLDAP;
        private RadioButton radBasic;
    }
}