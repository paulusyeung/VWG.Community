using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.FormsTest
{
    partial class MessageBox2Test
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
            this.button1 = new Gizmox.WebGUI.Forms.Button();
            this.button2 = new Gizmox.WebGUI.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(171, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(171, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MessageBox2Test
            // 
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Size = new System.Drawing.Size(419, 466);
            this.Text = "MessageBox2Test";
            this.Load += new System.EventHandler(this.MessageBox2Test_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private Button button1;
        private Button button2;
    }
}