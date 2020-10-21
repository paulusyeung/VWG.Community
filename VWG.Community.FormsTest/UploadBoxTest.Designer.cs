namespace VWG.Community.FormsTest
{
    partial class UploadBoxTest
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
            this.uploadBox1 = new VWG.Community.Forms.UploadBox();
            this.SuspendLayout();
            // 
            // uploadBox1
            // 
            this.uploadBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.uploadBox1.Location = new System.Drawing.Point(71, 76);
            this.uploadBox1.Name = "uploadBox1";
            this.uploadBox1.Size = new System.Drawing.Size(329, 100);
            this.uploadBox1.TabIndex = 0;
            this.uploadBox1.UploadMaxFileSize = ((long)(0));
            this.uploadBox1.UploadMinFileSize = ((long)(0));
            this.uploadBox1.UploadTempFilePath = "C:\\Users\\paulus\\AppData\\Local\\Temp\\";
            // 
            // Form1
            // 
            this.Controls.Add(this.uploadBox1);
            this.Size = new System.Drawing.Size(475, 363);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.UploadBox uploadBox1;
    }
}