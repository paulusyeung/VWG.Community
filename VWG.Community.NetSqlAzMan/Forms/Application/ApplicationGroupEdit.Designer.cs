using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Application
{
    partial class ApplicationGroupEdit
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
            this.tabControl1 = new Gizmox.WebGUI.Forms.TabControl();
            this.tabDefinition = new Gizmox.WebGUI.Forms.TabPage();
            this.txtGroupType = new Gizmox.WebGUI.Forms.TextBox();
            this.lblGroupType = new Gizmox.WebGUI.Forms.Label();
            this.lblDescription = new Gizmox.WebGUI.Forms.Label();
            this.tbrDefinition = new Gizmox.WebGUI.Forms.ToolBar();
            this.txtDescription = new Gizmox.WebGUI.Forms.TextBox();
            this.txtName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblName = new Gizmox.WebGUI.Forms.Label();
            this.tabMember = new Gizmox.WebGUI.Forms.TabPage();
            this.pnlMember = new Gizmox.WebGUI.Forms.Panel();
            this.lvwMember = new Gizmox.WebGUI.Forms.ListView();
            this.tbrMember = new Gizmox.WebGUI.Forms.ToolBar();
            this.tabNonMember = new Gizmox.WebGUI.Forms.TabPage();
            this.pnlNonMember = new Gizmox.WebGUI.Forms.Panel();
            this.lvwNonMember = new Gizmox.WebGUI.Forms.ListView();
            this.tbrNonMember = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.clientStorage1 = new Gizmox.WebGUI.Forms.Client.ClientStorage();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabDefinition.SuspendLayout();
            this.tabMember.SuspendLayout();
            this.pnlMember.SuspendLayout();
            this.tabNonMember.SuspendLayout();
            this.pnlNonMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDefinition);
            this.tabControl1.Controls.Add(this.tabMember);
            this.tabControl1.Controls.Add(this.tabNonMember);
            this.tabControl1.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 480);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDefinition
            // 
            this.tabDefinition.Controls.Add(this.txtGroupType);
            this.tabDefinition.Controls.Add(this.lblGroupType);
            this.tabDefinition.Controls.Add(this.lblDescription);
            this.tabDefinition.Controls.Add(this.tbrDefinition);
            this.tabDefinition.Controls.Add(this.txtDescription);
            this.tabDefinition.Controls.Add(this.txtName);
            this.tabDefinition.Controls.Add(this.lblName);
            this.tabDefinition.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabDefinition.Location = new System.Drawing.Point(4, 22);
            this.tabDefinition.Name = "tabDefinition";
            this.tabDefinition.Size = new System.Drawing.Size(632, 454);
            this.tabDefinition.TabIndex = 0;
            this.tabDefinition.Text = "General";
            // 
            // txtGroupType
            // 
            this.txtGroupType.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtGroupType.Location = new System.Drawing.Point(8, 246);
            this.txtGroupType.Name = "txtGroupType";
            this.txtGroupType.Size = new System.Drawing.Size(616, 20);
            this.txtGroupType.TabIndex = 1;
            // 
            // lblGroupType
            // 
            this.lblGroupType.AutoSize = true;
            this.lblGroupType.Location = new System.Drawing.Point(8, 226);
            this.lblGroupType.Name = "lblGroupType";
            this.lblGroupType.Size = new System.Drawing.Size(35, 13);
            this.lblGroupType.TabIndex = 0;
            this.lblGroupType.Text = "Group Type:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(11, 86);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(64, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description:";
            // 
            // tbrDefinition
            // 
            this.tbrDefinition.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrDefinition.DragHandle = true;
            this.tbrDefinition.DropDownArrows = true;
            this.tbrDefinition.ImageSize = new System.Drawing.Size(16, 16);
            this.tbrDefinition.Location = new System.Drawing.Point(3, 3);
            this.tbrDefinition.MenuHandle = true;
            this.tbrDefinition.Name = "tbrDefinition";
            this.tbrDefinition.ShowToolTips = true;
            this.tbrDefinition.Size = new System.Drawing.Size(626, 24);
            this.tbrDefinition.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(11, 106);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(618, 114);
            this.txtDescription.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(11, 57);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(618, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(11, 37);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // tabMember
            // 
            this.tabMember.Controls.Add(this.pnlMember);
            this.tabMember.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabMember.Location = new System.Drawing.Point(4, 22);
            this.tabMember.Name = "tabMember";
            this.tabMember.Size = new System.Drawing.Size(497, 311);
            this.tabMember.TabIndex = 1;
            this.tabMember.Text = "Members";
            // 
            // pnlMember
            // 
            this.pnlMember.Controls.Add(this.lvwMember);
            this.pnlMember.Controls.Add(this.tbrMember);
            this.pnlMember.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlMember.Location = new System.Drawing.Point(0, 0);
            this.pnlMember.Name = "pnlMember";
            this.pnlMember.Size = new System.Drawing.Size(632, 454);
            this.pnlMember.TabIndex = 0;
            // 
            // lvwMember
            // 
            this.lvwMember.DataMember = null;
            this.lvwMember.Location = new System.Drawing.Point(201, 203);
            this.lvwMember.Name = "lvwMember";
            this.lvwMember.Size = new System.Drawing.Size(100, 100);
            this.lvwMember.TabIndex = 1;
            // 
            // tbrMember
            // 
            this.tbrMember.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrMember.DragHandle = true;
            this.tbrMember.DropDownArrows = true;
            this.tbrMember.ImageSize = new System.Drawing.Size(16, 16);
            this.tbrMember.Location = new System.Drawing.Point(0, 0);
            this.tbrMember.MenuHandle = true;
            this.tbrMember.Name = "tbrMember";
            this.tbrMember.ShowToolTips = true;
            this.tbrMember.Size = new System.Drawing.Size(632, 24);
            this.tbrMember.TabIndex = 0;
            // 
            // tabNonMember
            // 
            this.tabNonMember.Controls.Add(this.pnlNonMember);
            this.tabNonMember.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabNonMember.Location = new System.Drawing.Point(0, 0);
            this.tabNonMember.Name = "tabNonMember";
            this.tabNonMember.Size = new System.Drawing.Size(632, 454);
            this.tabNonMember.TabIndex = 2;
            this.tabNonMember.Text = "Non-Members";
            // 
            // pnlNonMember
            // 
            this.pnlNonMember.Controls.Add(this.lvwNonMember);
            this.pnlNonMember.Controls.Add(this.tbrNonMember);
            this.pnlNonMember.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlNonMember.Location = new System.Drawing.Point(0, 0);
            this.pnlNonMember.Name = "pnlNonMember";
            this.pnlNonMember.Size = new System.Drawing.Size(632, 454);
            this.pnlNonMember.TabIndex = 0;
            // 
            // lvwNonMember
            // 
            this.lvwNonMember.DataMember = null;
            this.lvwNonMember.Location = new System.Drawing.Point(201, 203);
            this.lvwNonMember.Name = "lvwNonMember";
            this.lvwNonMember.Size = new System.Drawing.Size(100, 100);
            this.lvwNonMember.TabIndex = 1;
            // 
            // tbrNonMember
            // 
            this.tbrNonMember.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrNonMember.DragHandle = true;
            this.tbrNonMember.DropDownArrows = true;
            this.tbrNonMember.ImageSize = new System.Drawing.Size(16, 16);
            this.tbrNonMember.Location = new System.Drawing.Point(0, 0);
            this.tbrNonMember.MenuHandle = true;
            this.tbrNonMember.Name = "tbrNonMember";
            this.tbrNonMember.ShowToolTips = true;
            this.tbrNonMember.Size = new System.Drawing.Size(632, 30);
            this.tbrNonMember.TabIndex = 0;
            // 
            // clientStorage1
            // 
            this.clientStorage1.Description = "";
            this.clientStorage1.MajorVersion = ((ushort)(1));
            this.clientStorage1.MinorVersion = ((ushort)(0));
            // 
            // StoreGroupEdit
            // 
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabDefinition.ResumeLayout(false);
            this.tabMember.ResumeLayout(false);
            this.pnlMember.ResumeLayout(false);
            this.tabNonMember.ResumeLayout(false);
            this.pnlNonMember.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private TabControl tabControl1;
        private TabPage tabDefinition;
        private Label lblDescription;
        private ToolBar tbrDefinition;
        private TextBox txtDescription;
        private TextBox txtName;
        private Label lblName;
        private TabPage tabMember;
        private TabPage tabNonMember;
        private Panel pnlMember;
        private ListView lvwMember;
        private ToolBar tbrMember;
        private ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.Client.ClientStorage clientStorage1;
        private Panel pnlNonMember;
        private ListView lvwNonMember;
        private ToolBar tbrNonMember;
        private TextBox txtGroupType;
        private Label lblGroupType;
    }
}