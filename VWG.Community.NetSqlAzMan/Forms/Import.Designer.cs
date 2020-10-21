using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.NetSqlAzMan.Forms
{
    partial class Import
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
            this.gbxUpload = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdSelectFile = new Gizmox.WebGUI.Forms.Button();
            this.txtFileName = new Gizmox.WebGUI.Forms.TextBox();
            this.gbxMergeOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkCreatesNewItemAuthorizations = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkOverwritesItemAuthorizations = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDeleteMissingItemAuthorizations = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDeleteMissingItems = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkOverwritesExistingItems = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkCreatesNewItems = new Gizmox.WebGUI.Forms.CheckBox();
            this.gbxOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkDBUsers = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkWindowsUsers = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkAuthorizations = new Gizmox.WebGUI.Forms.CheckBox();
            this.fileUpload = new Gizmox.WebGUI.Forms.OpenFileDialog();
            this.uploadBox = new Gizmox.WebGUI.Forms.UploadControl();
            this.panel.SuspendLayout();
            this.gbxUpload.SuspendLayout();
            this.gbxMergeOptions.SuspendLayout();
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
            this.panel.Controls.Add(this.uploadBox);
            this.panel.Controls.Add(this.gbxUpload);
            this.panel.Controls.Add(this.gbxMergeOptions);
            this.panel.Controls.Add(this.gbxOptions);
            this.panel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(640, 450);
            this.panel.TabIndex = 1;
            // 
            // gbxUpload
            // 
            this.gbxUpload.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.gbxUpload.Controls.Add(this.cmdSelectFile);
            this.gbxUpload.Controls.Add(this.txtFileName);
            this.gbxUpload.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxUpload.Location = new System.Drawing.Point(9, 5);
            this.gbxUpload.Name = "gbxUpload";
            this.gbxUpload.Size = new System.Drawing.Size(622, 56);
            this.gbxUpload.TabIndex = 0;
            this.gbxUpload.TabStop = false;
            this.gbxUpload.Text = "Upload XML File to Import";
            // 
            // cmdSelectFile
            // 
            this.cmdSelectFile.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdSelectFile.Location = new System.Drawing.Point(534, 19);
            this.cmdSelectFile.Name = "cmdSelectFile";
            this.cmdSelectFile.Size = new System.Drawing.Size(75, 23);
            this.cmdSelectFile.TabIndex = 1;
            this.cmdSelectFile.Text = "Select File";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(15, 20);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(516, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // gbxMergeOptions
            // 
            this.gbxMergeOptions.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.gbxMergeOptions.Controls.Add(this.chkCreatesNewItemAuthorizations);
            this.gbxMergeOptions.Controls.Add(this.chkOverwritesItemAuthorizations);
            this.gbxMergeOptions.Controls.Add(this.chkDeleteMissingItemAuthorizations);
            this.gbxMergeOptions.Controls.Add(this.chkDeleteMissingItems);
            this.gbxMergeOptions.Controls.Add(this.chkOverwritesExistingItems);
            this.gbxMergeOptions.Controls.Add(this.chkCreatesNewItems);
            this.gbxMergeOptions.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxMergeOptions.Location = new System.Drawing.Point(9, 183);
            this.gbxMergeOptions.Name = "gbxMergeOptions";
            this.gbxMergeOptions.Size = new System.Drawing.Size(622, 169);
            this.gbxMergeOptions.TabIndex = 2;
            this.gbxMergeOptions.TabStop = false;
            this.gbxMergeOptions.Text = "Merge Options";
            // 
            // chkCreatesNewItemAuthorizations
            // 
            this.chkCreatesNewItemAuthorizations.AutoSize = true;
            this.chkCreatesNewItemAuthorizations.Checked = true;
            this.chkCreatesNewItemAuthorizations.CheckState = Gizmox.WebGUI.Forms.CheckState.Checked;
            this.chkCreatesNewItemAuthorizations.Location = new System.Drawing.Point(15, 94);
            this.chkCreatesNewItemAuthorizations.Name = "chkCreatesNewItemAuthorizations";
            this.chkCreatesNewItemAuthorizations.Size = new System.Drawing.Size(185, 17);
            this.chkCreatesNewItemAuthorizations.TabIndex = 3;
            this.chkCreatesNewItemAuthorizations.Text = "Creates New Item Authorizations";
            // 
            // chkOverwritesItemAuthorizations
            // 
            this.chkOverwritesItemAuthorizations.AutoSize = true;
            this.chkOverwritesItemAuthorizations.Location = new System.Drawing.Point(15, 118);
            this.chkOverwritesItemAuthorizations.Name = "chkOverwritesItemAuthorizations";
            this.chkOverwritesItemAuthorizations.Size = new System.Drawing.Size(166, 17);
            this.chkOverwritesItemAuthorizations.TabIndex = 4;
            this.chkOverwritesItemAuthorizations.Text = "Overwrite Item Authorization";
            // 
            // chkDeleteMissingItemAuthorizations
            // 
            this.chkDeleteMissingItemAuthorizations.AutoSize = true;
            this.chkDeleteMissingItemAuthorizations.Location = new System.Drawing.Point(15, 140);
            this.chkDeleteMissingItemAuthorizations.Name = "chkDeleteMissingItemAuthorizations";
            this.chkDeleteMissingItemAuthorizations.Size = new System.Drawing.Size(191, 17);
            this.chkDeleteMissingItemAuthorizations.TabIndex = 5;
            this.chkDeleteMissingItemAuthorizations.Text = "Delete Missing Item Authorizations";
            // 
            // chkDeleteMissingItems
            // 
            this.chkDeleteMissingItems.AutoSize = true;
            this.chkDeleteMissingItems.Location = new System.Drawing.Point(15, 72);
            this.chkDeleteMissingItems.Name = "chkDeleteMissingItems";
            this.chkDeleteMissingItems.Size = new System.Drawing.Size(124, 17);
            this.chkDeleteMissingItems.TabIndex = 2;
            this.chkDeleteMissingItems.Text = "Delete Missing Items";
            // 
            // chkOverwritesExistingItems
            // 
            this.chkOverwritesExistingItems.AutoSize = true;
            this.chkOverwritesExistingItems.Location = new System.Drawing.Point(15, 50);
            this.chkOverwritesExistingItems.Name = "chkOverwritesExistingItems";
            this.chkOverwritesExistingItems.Size = new System.Drawing.Size(144, 17);
            this.chkOverwritesExistingItems.TabIndex = 1;
            this.chkOverwritesExistingItems.Text = "Overwrite Existing Items";
            // 
            // chkCreatesNewItems
            // 
            this.chkCreatesNewItems.AutoSize = true;
            this.chkCreatesNewItems.Checked = true;
            this.chkCreatesNewItems.CheckState = Gizmox.WebGUI.Forms.CheckState.Checked;
            this.chkCreatesNewItems.Location = new System.Drawing.Point(15, 26);
            this.chkCreatesNewItems.Name = "chkCreatesNewItems";
            this.chkCreatesNewItems.Size = new System.Drawing.Size(118, 17);
            this.chkCreatesNewItems.TabIndex = 0;
            this.chkCreatesNewItems.Text = "Creates New Items";
            // 
            // gbxOptions
            // 
            this.gbxOptions.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left) 
            | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.gbxOptions.Controls.Add(this.chkDBUsers);
            this.gbxOptions.Controls.Add(this.chkWindowsUsers);
            this.gbxOptions.Controls.Add(this.chkAuthorizations);
            this.gbxOptions.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxOptions.Location = new System.Drawing.Point(9, 72);
            this.gbxOptions.Name = "gbxOptions";
            this.gbxOptions.Size = new System.Drawing.Size(622, 101);
            this.gbxOptions.TabIndex = 1;
            this.gbxOptions.TabStop = false;
            this.gbxOptions.Text = "Export Options";
            // 
            // chkDBUsers
            // 
            this.chkDBUsers.AutoSize = true;
            this.chkDBUsers.Location = new System.Drawing.Point(15, 72);
            this.chkDBUsers.Name = "chkDBUsers";
            this.chkDBUsers.Size = new System.Drawing.Size(140, 17);
            this.chkDBUsers.TabIndex = 2;
            this.chkDBUsers.Text = "Include Database Users";
            // 
            // chkWindowsUsers
            // 
            this.chkWindowsUsers.AutoSize = true;
            this.chkWindowsUsers.Enabled = false;
            this.chkWindowsUsers.Location = new System.Drawing.Point(15, 50);
            this.chkWindowsUsers.Name = "chkWindowsUsers";
            this.chkWindowsUsers.Size = new System.Drawing.Size(181, 17);
            this.chkWindowsUsers.TabIndex = 1;
            this.chkWindowsUsers.Text = "Include Windows Users / Groups";
            // 
            // chkAuthorizations
            // 
            this.chkAuthorizations.AutoSize = true;
            this.chkAuthorizations.Checked = true;
            this.chkAuthorizations.CheckState = Gizmox.WebGUI.Forms.CheckState.Checked;
            this.chkAuthorizations.Location = new System.Drawing.Point(15, 26);
            this.chkAuthorizations.Name = "chkAuthorizations";
            this.chkAuthorizations.Size = new System.Drawing.Size(158, 17);
            this.chkAuthorizations.TabIndex = 0;
            this.chkAuthorizations.Text = "Include Item Authorizations";
            // 
            // fileUpload
            // 
            this.fileUpload.Theme = "";
            // 
            // uploadBox
            // 
            this.uploadBox.Location = new System.Drawing.Point(9, 371);
            this.uploadBox.Name = "uploadBox";
            this.uploadBox.Size = new System.Drawing.Size(622, 58);
            this.uploadBox.TabIndex = 3;
            this.uploadBox.UploadMaxFileSize = ((long)(0));
            this.uploadBox.UploadMinFileSize = ((long)(0));
            this.uploadBox.UploadTempFilePath = "C:\\Users\\paulus\\AppData\\Local\\Temp\\";
            // 
            // Import
            // 
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel.ResumeLayout(false);
            this.gbxUpload.ResumeLayout(false);
            this.gbxMergeOptions.ResumeLayout(false);
            this.gbxOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar toolbar;
        private Panel panel;
        private GroupBox gbxOptions;
        private CheckBox chkDBUsers;
        private CheckBox chkWindowsUsers;
        private CheckBox chkAuthorizations;
        private GroupBox gbxMergeOptions;
        private CheckBox chkCreatesNewItemAuthorizations;
        private CheckBox chkOverwritesItemAuthorizations;
        private CheckBox chkDeleteMissingItemAuthorizations;
        private CheckBox chkDeleteMissingItems;
        private CheckBox chkOverwritesExistingItems;
        private CheckBox chkCreatesNewItems;
        private GroupBox gbxUpload;
        private Button cmdSelectFile;
        private TextBox txtFileName;
        private OpenFileDialog fileUpload;
        private UploadControl uploadBox;
    }
}