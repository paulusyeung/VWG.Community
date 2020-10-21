using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace NetSqlAzMan.Tester.NetSqlAzMan
{
    partial class WebConsole
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
            this.pnlLeft = new Gizmox.WebGUI.Forms.Panel();
            this.tvwNavTree = new Gizmox.WebGUI.Forms.TreeView();
            this.splitter1 = new Gizmox.WebGUI.Forms.Splitter();
            this.pnlRight = new Gizmox.WebGUI.Forms.Panel();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.tvwNavTree);
            this.pnlLeft.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(250, 320);
            this.pnlLeft.TabIndex = 0;
            // 
            // tvwNavTree
            // 
            this.tvwNavTree.Location = new System.Drawing.Point(25, 64);
            this.tvwNavTree.Name = "tvwNavTree";
            this.tvwNavTree.Size = new System.Drawing.Size(100, 100);
            this.tvwNavTree.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(150, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 320);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // pnlRight
            // 
            this.pnlRight.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(153, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(277, 320);
            this.pnlRight.TabIndex = 4;
            // 
            // WebConsole
            // 
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Size = new System.Drawing.Size(480, 320);
            this.Text = "WebConsole";
            this.Load += new System.EventHandler(this.WebConsole_Load);
            this.pnlLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private Panel pnlLeft;
        private Splitter splitter1;
        private Panel pnlRight;
        private TreeView tvwNavTree;
    }
}