using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.ResourcesTest
{
    partial class ResizeForm
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
            this.btnGeneralForm = new Gizmox.WebGUI.Forms.Button();
            this.label4 = new Gizmox.WebGUI.Forms.Label();
            this.html150Percent = new Gizmox.WebGUI.Forms.HtmlBox();
            this.label3 = new Gizmox.WebGUI.Forms.Label();
            this.lblUnrestricted = new Gizmox.WebGUI.Forms.Label();
            this.lblFile = new Gizmox.WebGUI.Forms.Label();
            this.label2 = new Gizmox.WebGUI.Forms.Label();
            this.label1 = new Gizmox.WebGUI.Forms.Label();
            this.htmlResizeTo40x40 = new Gizmox.WebGUI.Forms.HtmlBox();
            this.html50Percent = new Gizmox.WebGUI.Forms.HtmlBox();
            this.htmlResizeTo20x20 = new Gizmox.WebGUI.Forms.HtmlBox();
            this.htmNoResize = new Gizmox.WebGUI.Forms.HtmlBox();
            this.SuspendLayout();
            // 
            // btnGeneralForm
            // 
            this.btnGeneralForm.Location = new System.Drawing.Point(198, 295);
            this.btnGeneralForm.Name = "btnGeneralForm";
            this.btnGeneralForm.Size = new System.Drawing.Size(133, 64);
            this.btnGeneralForm.TabIndex = 6;
            this.btnGeneralForm.Text = "Demo for General ResourceHandle";
            this.btnGeneralForm.Click += new System.EventHandler(this.btnGeneralForm_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Resize to 150%";
            // 
            // html150Percent
            // 
            this.html150Percent.ContentType = "text/html";
            this.html150Percent.Html = "<HTML>No content.</HTML>";
            this.html150Percent.Location = new System.Drawing.Point(409, 295);
            this.html150Percent.Name = "html150Percent";
            this.html150Percent.Size = new System.Drawing.Size(182, 171);
            this.html150Percent.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(557, 62);
            this.label3.TabIndex = 5;
            this.label3.Text = "Demonstrate use of StaticImageResizeHandle\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnrestricted
            // 
            this.lblUnrestricted.AutoSize = true;
            this.lblUnrestricted.Location = new System.Drawing.Point(0, 272);
            this.lblUnrestricted.Name = "lblUnrestricted";
            this.lblUnrestricted.Size = new System.Drawing.Size(35, 13);
            this.lblUnrestricted.TabIndex = 4;
            this.lblUnrestricted.Text = "Resize to 40x40";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(406, 84);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(35, 13);
            this.lblFile.TabIndex = 4;
            this.lblFile.Text = "Resize to 50%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Resize to 20x20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "No Resize";
            // 
            // htmlResizeTo40x40
            // 
            this.htmlResizeTo40x40.ContentType = "text/html";
            this.htmlResizeTo40x40.Html = "<HTML>No content.</HTML>";
            this.htmlResizeTo40x40.Location = new System.Drawing.Point(3, 295);
            this.htmlResizeTo40x40.Name = "htmlResizeTo40x40";
            this.htmlResizeTo40x40.Size = new System.Drawing.Size(148, 138);
            this.htmlResizeTo40x40.TabIndex = 2;
            // 
            // html50Percent
            // 
            this.html50Percent.ContentType = "text/html";
            this.html50Percent.Html = "<HTML>No content.</HTML>";
            this.html50Percent.Location = new System.Drawing.Point(409, 108);
            this.html50Percent.Name = "html50Percent";
            this.html50Percent.Size = new System.Drawing.Size(148, 138);
            this.html50Percent.TabIndex = 2;
            // 
            // htmlResizeTo20x20
            // 
            this.htmlResizeTo20x20.ContentType = "text/html";
            this.htmlResizeTo20x20.Html = "<HTML>No content.</HTML>";
            this.htmlResizeTo20x20.Location = new System.Drawing.Point(198, 108);
            this.htmlResizeTo20x20.Name = "htmlResizeTo20x20";
            this.htmlResizeTo20x20.Size = new System.Drawing.Size(148, 138);
            this.htmlResizeTo20x20.TabIndex = 2;
            // 
            // htmNoResize
            // 
            this.htmNoResize.ContentType = "text/html";
            this.htmNoResize.Html = "<HTML>No content.</HTML>";
            this.htmNoResize.Location = new System.Drawing.Point(0, 108);
            this.htmNoResize.Name = "htmNoResize";
            this.htmNoResize.Size = new System.Drawing.Size(148, 138);
            this.htmNoResize.TabIndex = 2;
            // 
            // ResizeForm
            // 
            this.Controls.Add(this.htmNoResize);
            this.Controls.Add(this.htmlResizeTo20x20);
            this.Controls.Add(this.html50Percent);
            this.Controls.Add(this.htmlResizeTo40x40);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblUnrestricted);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.html150Percent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGeneralForm);
            this.Size = new System.Drawing.Size(619, 492);
            this.Text = "ResizeForm";
            this.Load += new System.EventHandler(this.ResizeForm_Load);
            this.ResumeLayout(false);

        }


        #endregion

        internal Button btnGeneralForm;
        private Label label4;
        private HtmlBox html150Percent;
        private Label label3;
        private Label lblUnrestricted;
        private Label lblFile;
        private Label label2;
        private Label label1;
        private HtmlBox htmlResizeTo40x40;
        private HtmlBox html50Percent;
        private HtmlBox htmlResizeTo20x20;
        private HtmlBox htmNoResize;
    }
}