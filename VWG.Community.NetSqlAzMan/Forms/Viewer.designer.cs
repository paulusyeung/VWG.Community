namespace VWG.Community.NetSqlAzMan.Forms
{
    partial class Viewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.htmlBox1 = new Gizmox.WebGUI.Forms.HtmlBox();
            this.SuspendLayout();
            // 
            // htmlBox1
            // 
            this.htmlBox1.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.htmlBox1.ContentType = "text/html";
            this.htmlBox1.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.htmlBox1.Expires = -1;
            this.htmlBox1.Html = "<HTML>No content.</HTML>";
            this.htmlBox1.Location = new System.Drawing.Point(0, 0);
            this.htmlBox1.Name = "htmlBox1";
            this.htmlBox1.Size = new System.Drawing.Size(419, 466);
            this.htmlBox1.TabIndex = 0;
            // 
            // Viewer
            // 
            this.Controls.Add(this.htmlBox1);
            this.Size = new System.Drawing.Size(419, 466);
            this.Text = "Report Viewer";
            this.Load += new System.EventHandler(this.Viewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.HtmlBox htmlBox1;


    }
}