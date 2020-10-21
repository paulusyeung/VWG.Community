using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms.Definition
{
    partial class TaskEdit
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
            this.lblDescription = new Gizmox.WebGUI.Forms.Label();
            this.tbrDefinition = new Gizmox.WebGUI.Forms.ToolBar();
            this.txtDescription = new Gizmox.WebGUI.Forms.TextBox();
            this.txtName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblName = new Gizmox.WebGUI.Forms.Label();
            this.tabTask = new Gizmox.WebGUI.Forms.TabPage();
            this.pnlTask = new Gizmox.WebGUI.Forms.Panel();
            this.cmdAddTask = new Gizmox.WebGUI.Forms.PictureBox();
            this.lvwTask = new Gizmox.WebGUI.Forms.ListView();
            this.tbrTask = new Gizmox.WebGUI.Forms.ToolBar();
            this.tabOperation = new Gizmox.WebGUI.Forms.TabPage();
            this.pnlOperation = new Gizmox.WebGUI.Forms.Panel();
            this.cmdAddOperation = new Gizmox.WebGUI.Forms.PictureBox();
            this.lvwOperation = new Gizmox.WebGUI.Forms.ListView();
            this.tbrOperation = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.clientStorage1 = new Gizmox.WebGUI.Forms.Client.ClientStorage();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabDefinition.SuspendLayout();
            this.tabTask.SuspendLayout();
            this.pnlTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddTask)).BeginInit();
            this.tabOperation.SuspendLayout();
            this.pnlOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddOperation)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDefinition);
            this.tabControl1.Controls.Add(this.tabTask);
            this.tabControl1.Controls.Add(this.tabOperation);
            this.tabControl1.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 480);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDefinition
            // 
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
            this.tabDefinition.Text = "Role Definition";
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
            // tabTask
            // 
            this.tabTask.Controls.Add(this.pnlTask);
            this.tabTask.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabTask.Location = new System.Drawing.Point(0, 0);
            this.tabTask.Name = "tabTask";
            this.tabTask.Size = new System.Drawing.Size(497, 311);
            this.tabTask.TabIndex = 1;
            this.tabTask.Text = "Task";
            // 
            // pnlTask
            // 
            this.pnlTask.Controls.Add(this.cmdAddTask);
            this.pnlTask.Controls.Add(this.lvwTask);
            this.pnlTask.Controls.Add(this.tbrTask);
            this.pnlTask.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlTask.Location = new System.Drawing.Point(0, 0);
            this.pnlTask.Name = "pnlTask";
            this.pnlTask.Size = new System.Drawing.Size(632, 454);
            this.pnlTask.TabIndex = 0;
            // 
            // cmdAddTask
            // 
            this.cmdAddTask.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Bottom | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdAddTask.Location = new System.Drawing.Point(584, 420);
            this.cmdAddTask.Name = "cmdAddTask";
            this.cmdAddTask.Size = new System.Drawing.Size(48, 48);
            this.cmdAddTask.TabIndex = 2;
            this.cmdAddTask.TabStop = false;
            // 
            // lvwTask
            // 
            this.lvwTask.DataMember = null;
            this.lvwTask.Location = new System.Drawing.Point(201, 203);
            this.lvwTask.Name = "lvwTask";
            this.lvwTask.Size = new System.Drawing.Size(100, 100);
            this.lvwTask.TabIndex = 1;
            // 
            // tbrTask
            // 
            this.tbrTask.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrTask.DragHandle = true;
            this.tbrTask.DropDownArrows = true;
            this.tbrTask.ImageSize = new System.Drawing.Size(16, 16);
            this.tbrTask.Location = new System.Drawing.Point(0, 0);
            this.tbrTask.MenuHandle = true;
            this.tbrTask.Name = "tbrTask";
            this.tbrTask.ShowToolTips = true;
            this.tbrTask.Size = new System.Drawing.Size(632, 30);
            this.tbrTask.TabIndex = 0;
            // 
            // tabOperation
            // 
            this.tabOperation.Controls.Add(this.pnlOperation);
            this.tabOperation.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabOperation.Location = new System.Drawing.Point(0, 0);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Size = new System.Drawing.Size(632, 454);
            this.tabOperation.TabIndex = 2;
            this.tabOperation.Text = "Operation";
            // 
            // pnlOperation
            // 
            this.pnlOperation.Controls.Add(this.cmdAddOperation);
            this.pnlOperation.Controls.Add(this.lvwOperation);
            this.pnlOperation.Controls.Add(this.tbrOperation);
            this.pnlOperation.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlOperation.Location = new System.Drawing.Point(0, 0);
            this.pnlOperation.Name = "pnlOperation";
            this.pnlOperation.Size = new System.Drawing.Size(632, 454);
            this.pnlOperation.TabIndex = 0;
            // 
            // cmdAddOperation
            // 
            this.cmdAddOperation.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Bottom | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdAddOperation.Location = new System.Drawing.Point(584, 420);
            this.cmdAddOperation.Name = "cmdAddOperation";
            this.cmdAddOperation.Size = new System.Drawing.Size(48, 48);
            this.cmdAddOperation.TabIndex = 2;
            this.cmdAddOperation.TabStop = false;
            // 
            // lvwOperation
            // 
            this.lvwOperation.DataMember = null;
            this.lvwOperation.Location = new System.Drawing.Point(201, 203);
            this.lvwOperation.Name = "lvwOperation";
            this.lvwOperation.Size = new System.Drawing.Size(100, 100);
            this.lvwOperation.TabIndex = 1;
            // 
            // tbrOperation
            // 
            this.tbrOperation.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrOperation.DragHandle = true;
            this.tbrOperation.DropDownArrows = true;
            this.tbrOperation.ImageSize = new System.Drawing.Size(16, 16);
            this.tbrOperation.Location = new System.Drawing.Point(0, 0);
            this.tbrOperation.MenuHandle = true;
            this.tbrOperation.Name = "tbrOperation";
            this.tbrOperation.ShowToolTips = true;
            this.tbrOperation.Size = new System.Drawing.Size(632, 30);
            this.tbrOperation.TabIndex = 0;
            // 
            // clientStorage1
            // 
            this.clientStorage1.Description = "";
            this.clientStorage1.MajorVersion = ((ushort)(1));
            this.clientStorage1.MinorVersion = ((ushort)(0));
            // 
            // TaskEdit
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
            this.tabTask.ResumeLayout(false);
            this.pnlTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddTask)).EndInit();
            this.tabOperation.ResumeLayout(false);
            this.pnlOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddOperation)).EndInit();
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
        private TabPage tabTask;
        private TabPage tabOperation;
        private ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.Client.ClientStorage clientStorage1;
        private Panel pnlTask;
        private PictureBox cmdAddTask;
        private ListView lvwTask;
        private ToolBar tbrTask;
        private Panel pnlOperation;
        private PictureBox cmdAddOperation;
        private ListView lvwOperation;
        private ToolBar tbrOperation;
    }
}