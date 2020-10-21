using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Store
{
    partial class Permissions
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
            this.flpManagers = new Gizmox.WebGUI.Forms.FlowLayoutPanel();
            this.lblManagers = new Gizmox.WebGUI.Forms.Label();
            this.picManagers = new Gizmox.WebGUI.Forms.PictureBox();
            this.lblNotes = new Gizmox.WebGUI.Forms.Label();
            this.txtDescription = new Gizmox.WebGUI.Forms.TextBox();
            this.txtName = new Gizmox.WebGUI.Forms.TextBox();
            this.flpUsers = new Gizmox.WebGUI.Forms.FlowLayoutPanel();
            this.lblUsers = new Gizmox.WebGUI.Forms.Label();
            this.picUsers = new Gizmox.WebGUI.Forms.PictureBox();
            this.flpReaders = new Gizmox.WebGUI.Forms.FlowLayoutPanel();
            this.lblReaders = new Gizmox.WebGUI.Forms.Label();
            this.picReaders = new Gizmox.WebGUI.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picManagers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReaders)).BeginInit();
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
            this.panel.Controls.Add(this.picReaders);
            this.panel.Controls.Add(this.lblReaders);
            this.panel.Controls.Add(this.flpReaders);
            this.panel.Controls.Add(this.picUsers);
            this.panel.Controls.Add(this.lblUsers);
            this.panel.Controls.Add(this.flpUsers);
            this.panel.Controls.Add(this.flpManagers);
            this.panel.Controls.Add(this.lblManagers);
            this.panel.Controls.Add(this.picManagers);
            this.panel.Controls.Add(this.lblNotes);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(640, 450);
            this.panel.TabIndex = 1;
            // 
            // flpManagers
            // 
            this.flpManagers.FlowDirection = Gizmox.WebGUI.Forms.FlowDirection.TopDown;
            this.flpManagers.Location = new System.Drawing.Point(15, 168);
            this.flpManagers.Name = "flpManagers";
            this.flpManagers.Size = new System.Drawing.Size(200, 267);
            this.flpManagers.TabIndex = 3;
            // 
            // lblManagers
            // 
            this.lblManagers.Location = new System.Drawing.Point(15, 146);
            this.lblManagers.Name = "lblManagers";
            this.lblManagers.Size = new System.Drawing.Size(200, 20);
            this.lblManagers.TabIndex = 2;
            this.lblManagers.Text = "Store Managers";
            this.lblManagers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picManagers
            // 
            this.picManagers.Location = new System.Drawing.Point(91, 95);
            this.picManagers.Name = "picManagers";
            this.picManagers.Size = new System.Drawing.Size(48, 48);
            this.picManagers.TabIndex = 1;
            this.picManagers.TabStop = false;
            // 
            // lblNotes
            // 
            this.lblNotes.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNotes.Location = new System.Drawing.Point(12, 12);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(619, 81);
            this.lblNotes.TabIndex = 0;
            this.lblNotes.Text = "lblNotes";
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // flpUsers
            // 
            this.flpUsers.FlowDirection = Gizmox.WebGUI.Forms.FlowDirection.TopDown;
            this.flpUsers.Location = new System.Drawing.Point(220, 168);
            this.flpUsers.Name = "flpUsers";
            this.flpUsers.Size = new System.Drawing.Size(200, 267);
            this.flpUsers.TabIndex = 3;
            // 
            // lblUsers
            // 
            this.lblUsers.Location = new System.Drawing.Point(220, 146);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(200, 20);
            this.lblUsers.TabIndex = 2;
            this.lblUsers.Text = "Store Users";
            this.lblUsers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picUsers
            // 
            this.picUsers.Location = new System.Drawing.Point(296, 95);
            this.picUsers.Name = "picUsers";
            this.picUsers.Size = new System.Drawing.Size(48, 48);
            this.picUsers.TabIndex = 1;
            this.picUsers.TabStop = false;
            // 
            // flpReaders
            // 
            this.flpReaders.FlowDirection = Gizmox.WebGUI.Forms.FlowDirection.TopDown;
            this.flpReaders.Location = new System.Drawing.Point(425, 167);
            this.flpReaders.Name = "flpReaders";
            this.flpReaders.Size = new System.Drawing.Size(200, 268);
            this.flpReaders.TabIndex = 3;
            // 
            // lblReaders
            // 
            this.lblReaders.Location = new System.Drawing.Point(425, 145);
            this.lblReaders.Name = "lblReaders";
            this.lblReaders.Size = new System.Drawing.Size(200, 21);
            this.lblReaders.TabIndex = 2;
            this.lblReaders.Text = "Store Readers";
            this.lblReaders.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picReaders
            // 
            this.picReaders.Location = new System.Drawing.Point(501, 94);
            this.picReaders.Name = "picReaders";
            this.picReaders.Size = new System.Drawing.Size(48, 49);
            this.picReaders.TabIndex = 1;
            this.picReaders.TabStop = false;
            // 
            // Permissions
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Store";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picManagers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReaders)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private TextBox txtDescription;
        private TextBox txtName;
        private Label lblNotes;
        private FlowLayoutPanel flpManagers;
        private Label lblManagers;
        private PictureBox picManagers;
        private PictureBox picReaders;
        private Label lblReaders;
        private FlowLayoutPanel flpReaders;
        private PictureBox picUsers;
        private Label lblUsers;
        private FlowLayoutPanel flpUsers;
    }
}