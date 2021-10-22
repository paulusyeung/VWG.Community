using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.Forms
{
    partial class PagerBox
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
            this.cmdFirstPage = new Gizmox.WebGUI.Forms.Button();
            this.cmdPreviousPage = new Gizmox.WebGUI.Forms.Button();
            this.cmdNextPage = new Gizmox.WebGUI.Forms.Button();
            this.cmdLastPage = new Gizmox.WebGUI.Forms.Button();
            this.tooltip = new Gizmox.WebGUI.Forms.ToolTip();
            this.lblRowsPerPage = new Gizmox.WebGUI.Forms.Label();
            this.cboRowsPerPage = new Gizmox.WebGUI.Forms.ComboBox();
            this.lblTotalPages = new Gizmox.WebGUI.Forms.Label();
            this.txtCurrentPage = new Gizmox.WebGUI.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdFirstPage
            // 
            this.cmdFirstPage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdFirstPage.Location = new System.Drawing.Point(203, 6);
            this.cmdFirstPage.Name = "cmdFirstPage";
            this.cmdFirstPage.Size = new System.Drawing.Size(20, 20);
            this.cmdFirstPage.TabIndex = 0;
            this.cmdFirstPage.Text = "|<";
            // 
            // cmdPreviousPage
            // 
            this.cmdPreviousPage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdPreviousPage.Location = new System.Drawing.Point(228, 6);
            this.cmdPreviousPage.Name = "cmdPreviousPage";
            this.cmdPreviousPage.Size = new System.Drawing.Size(20, 20);
            this.cmdPreviousPage.TabIndex = 0;
            this.cmdPreviousPage.Text = "<";
            // 
            // cmdNextPage
            // 
            this.cmdNextPage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdNextPage.Location = new System.Drawing.Point(312, 6);
            this.cmdNextPage.Name = "cmdNextPage";
            this.cmdNextPage.Size = new System.Drawing.Size(20, 20);
            this.cmdNextPage.TabIndex = 0;
            this.cmdNextPage.Text = ">";
            // 
            // cmdLastPage
            // 
            this.cmdLastPage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmdLastPage.Location = new System.Drawing.Point(337, 6);
            this.cmdLastPage.Name = "cmdLastPage";
            this.cmdLastPage.Size = new System.Drawing.Size(20, 20);
            this.cmdLastPage.TabIndex = 0;
            this.cmdLastPage.Text = ">|";
            // 
            // lblRowsPerPage
            // 
            this.lblRowsPerPage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.lblRowsPerPage.Location = new System.Drawing.Point(18, 2);
            this.lblRowsPerPage.Name = "lblRowsPerPage";
            this.lblRowsPerPage.Size = new System.Drawing.Size(128, 28);
            this.lblRowsPerPage.TabIndex = 1;
            this.lblRowsPerPage.Text = "Rows/Page:";
            this.lblRowsPerPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboRowsPerPage
            // 
            this.cboRowsPerPage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cboRowsPerPage.FormattingEnabled = true;
            this.cboRowsPerPage.Items.AddRange(new object[] {
            "",
            "10",
            "25",
            "50",
            "100"});
            this.cboRowsPerPage.Location = new System.Drawing.Point(149, 6);
            this.cboRowsPerPage.Name = "cboRowsPerPage";
            this.cboRowsPerPage.Size = new System.Drawing.Size(44, 35);
            this.cboRowsPerPage.TabIndex = 2;
            // 
            // lblTotalPages
            // 
            this.lblTotalPages.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.lblTotalPages.Location = new System.Drawing.Point(285, 4);
            this.lblTotalPages.Name = "lblTotalPages";
            this.lblTotalPages.Size = new System.Drawing.Size(36, 24);
            this.lblTotalPages.TabIndex = 1;
            this.lblTotalPages.Text = "/999";
            this.lblTotalPages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.txtCurrentPage.Location = new System.Drawing.Point(256, 8);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(28, 20);
            this.txtCurrentPage.TabIndex = 3;
            this.txtCurrentPage.Text = "1";
            this.txtCurrentPage.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // PagerBox
            // 
            this.Controls.Add(this.txtCurrentPage);
            this.Controls.Add(this.cboRowsPerPage);
            this.Controls.Add(this.lblRowsPerPage);
            this.Controls.Add(this.cmdLastPage);
            this.Controls.Add(this.cmdNextPage);
            this.Controls.Add(this.cmdPreviousPage);
            this.Controls.Add(this.cmdFirstPage);
            this.Controls.Add(this.lblTotalPages);
            this.Size = new System.Drawing.Size(364, 42);
            this.Text = "PagerBox";
            this.Load += new System.EventHandler(this.PagerBox_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private Button cmdFirstPage;
        private Button cmdPreviousPage;
        private Button cmdNextPage;
        private Button cmdLastPage;
        private ToolTip tooltip;
        private Label lblRowsPerPage;
        private ComboBox cboRowsPerPage;
        private Label lblTotalPages;
        private TextBox txtCurrentPage;
    }
}