using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Authorization
{
    partial class AuthorizationItem
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
            this.cboAuthType = new Gizmox.WebGUI.Forms.ComboBox();
            this.datValidTo = new Gizmox.WebGUI.Forms.DateTimePicker();
            this.datValidFrom = new Gizmox.WebGUI.Forms.DateTimePicker();
            this.lblValidTo = new Gizmox.WebGUI.Forms.Label();
            this.lblValidFrom = new Gizmox.WebGUI.Forms.Label();
            this.lblOwner = new Gizmox.WebGUI.Forms.Label();
            this.txtOwner = new Gizmox.WebGUI.Forms.TextBox();
            this.txtWhereDefined = new Gizmox.WebGUI.Forms.TextBox();
            this.lblWhereDefined = new Gizmox.WebGUI.Forms.Label();
            this.lblAuthType = new Gizmox.WebGUI.Forms.Label();
            this.lblMemberType = new Gizmox.WebGUI.Forms.Label();
            this.txtMemberType = new Gizmox.WebGUI.Forms.TextBox();
            this.txtName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblName = new Gizmox.WebGUI.Forms.Label();
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
            this.toolbar.Size = new System.Drawing.Size(480, 30);
            this.toolbar.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.cboAuthType);
            this.panel.Controls.Add(this.datValidTo);
            this.panel.Controls.Add(this.datValidFrom);
            this.panel.Controls.Add(this.lblValidTo);
            this.panel.Controls.Add(this.lblValidFrom);
            this.panel.Controls.Add(this.lblOwner);
            this.panel.Controls.Add(this.txtOwner);
            this.panel.Controls.Add(this.txtWhereDefined);
            this.panel.Controls.Add(this.lblWhereDefined);
            this.panel.Controls.Add(this.lblAuthType);
            this.panel.Controls.Add(this.lblMemberType);
            this.panel.Controls.Add(this.txtMemberType);
            this.panel.Controls.Add(this.txtName);
            this.panel.Controls.Add(this.lblName);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(480, 290);
            this.panel.TabIndex = 1;
            // 
            // cboAuthType
            // 
            this.cboAuthType.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cboAuthType.FormattingEnabled = true;
            this.cboAuthType.Location = new System.Drawing.Point(12, 118);
            this.cboAuthType.Name = "cboAuthType";
            this.cboAuthType.Size = new System.Drawing.Size(456, 21);
            this.cboAuthType.TabIndex = 3;
            // 
            // datValidTo
            // 
            this.datValidTo.CustomFormat = "";
            this.datValidTo.Location = new System.Drawing.Point(252, 255);
            this.datValidTo.Name = "datValidTo";
            this.datValidTo.Size = new System.Drawing.Size(216, 21);
            this.datValidTo.TabIndex = 2;
            // 
            // datValidFrom
            // 
            this.datValidFrom.CustomFormat = "";
            this.datValidFrom.Location = new System.Drawing.Point(12, 255);
            this.datValidFrom.Name = "datValidFrom";
            this.datValidFrom.Size = new System.Drawing.Size(216, 21);
            this.datValidFrom.TabIndex = 2;
            // 
            // lblValidTo
            // 
            this.lblValidTo.AutoSize = true;
            this.lblValidTo.Location = new System.Drawing.Point(253, 237);
            this.lblValidTo.Name = "lblValidTo";
            this.lblValidTo.Size = new System.Drawing.Size(35, 13);
            this.lblValidTo.TabIndex = 0;
            this.lblValidTo.Text = "Valid To:";
            // 
            // lblValidFrom
            // 
            this.lblValidFrom.AutoSize = true;
            this.lblValidFrom.Location = new System.Drawing.Point(12, 237);
            this.lblValidFrom.Name = "lblValidFrom";
            this.lblValidFrom.Size = new System.Drawing.Size(35, 13);
            this.lblValidFrom.TabIndex = 0;
            this.lblValidFrom.Text = "Valid From:";
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.Location = new System.Drawing.Point(12, 190);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(35, 13);
            this.lblOwner.TabIndex = 0;
            this.lblOwner.Text = "Owner:";
            // 
            // txtOwner
            // 
            this.txtOwner.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtOwner.Location = new System.Drawing.Point(12, 210);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(456, 20);
            this.txtOwner.TabIndex = 1;
            // 
            // txtWhereDefined
            // 
            this.txtWhereDefined.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtWhereDefined.Location = new System.Drawing.Point(12, 164);
            this.txtWhereDefined.Name = "txtWhereDefined";
            this.txtWhereDefined.Size = new System.Drawing.Size(456, 20);
            this.txtWhereDefined.TabIndex = 1;
            // 
            // lblWhereDefined
            // 
            this.lblWhereDefined.AutoSize = true;
            this.lblWhereDefined.Location = new System.Drawing.Point(12, 144);
            this.lblWhereDefined.Name = "lblWhereDefined";
            this.lblWhereDefined.Size = new System.Drawing.Size(35, 13);
            this.lblWhereDefined.TabIndex = 0;
            this.lblWhereDefined.Text = "Where Defined:";
            // 
            // lblAuthType
            // 
            this.lblAuthType.AutoSize = true;
            this.lblAuthType.Location = new System.Drawing.Point(12, 98);
            this.lblAuthType.Name = "lblAuthType";
            this.lblAuthType.Size = new System.Drawing.Size(35, 13);
            this.lblAuthType.TabIndex = 0;
            this.lblAuthType.Text = "Authorization Type:";
            // 
            // lblMemberType
            // 
            this.lblMemberType.AutoSize = true;
            this.lblMemberType.Location = new System.Drawing.Point(12, 7);
            this.lblMemberType.Name = "lblMemberType";
            this.lblMemberType.Size = new System.Drawing.Size(35, 13);
            this.lblMemberType.TabIndex = 0;
            this.lblMemberType.Text = "Member Type:";
            // 
            // txtMemberType
            // 
            this.txtMemberType.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtMemberType.Location = new System.Drawing.Point(12, 27);
            this.txtMemberType.Name = "txtMemberType";
            this.txtMemberType.Size = new System.Drawing.Size(456, 20);
            this.txtMemberType.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(12, 72);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(456, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 52);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // AuthItemRecord
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(480, 320);
            this.Text = "Store";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private TextBox txtName;
        private Label lblName;
        private DateTimePicker datValidTo;
        private DateTimePicker datValidFrom;
        private Label lblValidTo;
        private Label lblValidFrom;
        private Label lblOwner;
        private TextBox txtOwner;
        private TextBox txtWhereDefined;
        private Label lblWhereDefined;
        private Label lblAuthType;
        private Label lblMemberType;
        private TextBox txtMemberType;
        private ComboBox cboAuthType;
    }
}