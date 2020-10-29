namespace VWG.Community.RsourcesTest
{
    partial class Form1
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
            this.btnResizeForm = new Gizmox.WebGUI.Forms.Button();
            this.label3 = new Gizmox.WebGUI.Forms.Label();
            this.lblUnrestricted = new Gizmox.WebGUI.Forms.Label();
            this.lblUserData = new Gizmox.WebGUI.Forms.Label();
            this.lblFile = new Gizmox.WebGUI.Forms.Label();
            this.label2 = new Gizmox.WebGUI.Forms.Label();
            this.label1 = new Gizmox.WebGUI.Forms.Label();
            this.htmlUnrestricted = new Gizmox.WebGUI.Forms.HtmlBox();
            this.htmlUserData = new Gizmox.WebGUI.Forms.HtmlBox();
            this.htmlFile = new Gizmox.WebGUI.Forms.HtmlBox();
            this.htmlCustom = new Gizmox.WebGUI.Forms.HtmlBox();
            this.htmlImage = new Gizmox.WebGUI.Forms.HtmlBox();
            this.SuspendLayout();
            // 
            // btnResizeForm
            // 
            this.btnResizeForm.Location = new System.Drawing.Point(425, 309);
            this.btnResizeForm.Name = "btnResizeForm";
            this.btnResizeForm.Size = new System.Drawing.Size(133, 64);
            this.btnResizeForm.TabIndex = 6;
            this.btnResizeForm.Text = "Demo for Resizing ResourceHandle";
            this.btnResizeForm.Click += new System.EventHandler(this.btnResizeForm_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(16, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(557, 62);
            this.label3.TabIndex = 5;
            this.label3.Text = "Demonstrate use of StaticFileResourceHandle and its derived classes\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnrestricted
            // 
            this.lblUnrestricted.AutoSize = true;
            this.lblUnrestricted.Location = new System.Drawing.Point(16, 285);
            this.lblUnrestricted.Name = "lblUnrestricted";
            this.lblUnrestricted.Size = new System.Drawing.Size(35, 13);
            this.lblUnrestricted.TabIndex = 4;
            this.lblUnrestricted.Text = "Unrestricted - web.config";
            // 
            // lblUserData
            // 
            this.lblUserData.AutoSize = true;
            this.lblUserData.Location = new System.Drawing.Point(211, 285);
            this.lblUserData.Name = "lblUserData";
            this.lblUserData.Size = new System.Drawing.Size(35, 13);
            this.lblUserData.TabIndex = 4;
            this.lblUserData.Text = "UserData - Image";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(422, 97);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(35, 13);
            this.lblFile.TabIndex = 4;
            this.lblFile.Text = "Resources (File) - Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CustomResource - Image";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ImageResourceHandle - Image";
            // 
            // htmlUnrestricted
            // 
            this.htmlUnrestricted.ContentType = "text/html";
            this.htmlUnrestricted.Html = "<HTML>No content.</HTML>";
            this.htmlUnrestricted.Location = new System.Drawing.Point(16, 308);
            this.htmlUnrestricted.Name = "htmlUnrestricted";
            this.htmlUnrestricted.Size = new System.Drawing.Size(148, 138);
            this.htmlUnrestricted.TabIndex = 2;
            // 
            // htmlUserData
            // 
            this.htmlUserData.ContentType = "text/html";
            this.htmlUserData.Html = "<HTML>No content.</HTML>";
            this.htmlUserData.Location = new System.Drawing.Point(214, 309);
            this.htmlUserData.Name = "htmlUserData";
            this.htmlUserData.Size = new System.Drawing.Size(148, 138);
            this.htmlUserData.TabIndex = 2;
            // 
            // htmlFile
            // 
            this.htmlFile.ContentType = "text/html";
            this.htmlFile.Html = "<HTML>No content.</HTML>";
            this.htmlFile.Location = new System.Drawing.Point(425, 121);
            this.htmlFile.Name = "htmlFile";
            this.htmlFile.Size = new System.Drawing.Size(148, 138);
            this.htmlFile.TabIndex = 2;
            // 
            // htmlCustom
            // 
            this.htmlCustom.ContentType = "text/html";
            this.htmlCustom.Html = "<HTML>No content.</HTML>";
            this.htmlCustom.Location = new System.Drawing.Point(214, 121);
            this.htmlCustom.Name = "htmlCustom";
            this.htmlCustom.Size = new System.Drawing.Size(148, 138);
            this.htmlCustom.TabIndex = 2;
            // 
            // htmlImage
            // 
            this.htmlImage.ContentType = "text/html";
            this.htmlImage.Html = "<HTML>No content.</HTML>";
            this.htmlImage.Location = new System.Drawing.Point(16, 121);
            this.htmlImage.Name = "htmlImage";
            this.htmlImage.Size = new System.Drawing.Size(148, 138);
            this.htmlImage.TabIndex = 2;
            // 
            // Form1
            // 
            this.Controls.Add(this.htmlImage);
            this.Controls.Add(this.htmlCustom);
            this.Controls.Add(this.htmlFile);
            this.Controls.Add(this.htmlUserData);
            this.Controls.Add(this.htmlUnrestricted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblUserData);
            this.Controls.Add(this.lblUnrestricted);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnResizeForm);
            this.Size = new System.Drawing.Size(589, 461);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal Gizmox.WebGUI.Forms.Button btnResizeForm;
        private Gizmox.WebGUI.Forms.Label label3;
        private Gizmox.WebGUI.Forms.Label lblUnrestricted;
        private Gizmox.WebGUI.Forms.Label lblUserData;
        private Gizmox.WebGUI.Forms.Label lblFile;
        private Gizmox.WebGUI.Forms.Label label2;
        private Gizmox.WebGUI.Forms.Label label1;
        private Gizmox.WebGUI.Forms.HtmlBox htmlUnrestricted;
        private Gizmox.WebGUI.Forms.HtmlBox htmlUserData;
        private Gizmox.WebGUI.Forms.HtmlBox htmlFile;
        private Gizmox.WebGUI.Forms.HtmlBox htmlCustom;
        private Gizmox.WebGUI.Forms.HtmlBox htmlImage;
    }
}