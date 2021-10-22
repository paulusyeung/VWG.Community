using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.FormsTest
{
    partial class PagerBoxTest
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
            this.panel1 = new Gizmox.WebGUI.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Location = new System.Drawing.Point(0, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 100);
            this.panel1.TabIndex = 0;
            // 
            // PagerBoxTest
            // 
            this.Controls.Add(this.panel1);
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "PagerBoxTest";
            this.Load += new System.EventHandler(this.PagerBoxTest_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private Panel panel1;
    }
}