using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.Forms
{
    partial class MessageBox2Form
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
            this.mobjButtonsLayout = new Gizmox.WebGUI.Forms.TableLayoutPanel();
            this.mobjIconLayout = new Gizmox.WebGUI.Forms.Panel();
            this.mobjIcon = new Gizmox.WebGUI.Forms.PictureBox();
            this.mobjLabelText = new Gizmox.WebGUI.Forms.Label();
            this.mobjIconLayout.SuspendLayout();
            this.SuspendLayout();
            //
            // mobjButtonsLayout
            //
            this.mobjButtonsLayout.Dock = Gizmox.WebGUI.Forms.DockStyle.Bottom;
            this.mobjButtonsLayout.Location = new System.Drawing.Point(10, 85);
            this.mobjButtonsLayout.Name = "mobjButtonsLayout";
            this.mobjButtonsLayout.Size = new System.Drawing.Size(460, 26);
            this.mobjButtonsLayout.TabIndex = 0;
            //
            // mobjIconLayout
            //
            this.mobjIconLayout.Controls.Add(this.mobjIcon);
            this.mobjIconLayout.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.mobjIconLayout.Location = new System.Drawing.Point(10, 10);
            this.mobjIconLayout.Name = "mobjIconLayout";
            this.mobjIconLayout.Size = new System.Drawing.Size(50, 75);
            this.mobjIconLayout.TabIndex = 1;
            //
            // mobjIcon
            //
            this.mobjIcon.Location = new System.Drawing.Point(9, 15);
            this.mobjIcon.Name = "mobjIcon";
            this.mobjIcon.Size = new System.Drawing.Size(32, 32);
            this.mobjIcon.TabIndex = 0;
            this.mobjIcon.TabStop = false;
            //
            // mobjLabelText
            //
            this.mobjLabelText.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.mobjLabelText.Location = new System.Drawing.Point(60, 10);
            this.mobjLabelText.Name = "mobjLabelText";
            this.mobjLabelText.Size = new System.Drawing.Size(410, 75);
            this.mobjLabelText.TabIndex = 2;
            this.mobjLabelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mobjLabelText.UseMnemonic = false;
            //
            // MessageBoxWindow2
            //            
            this.Load += new System.EventHandler(Form_Load);
            this.ClientSize = new System.Drawing.Size(480, 125);
            this.Controls.Add(this.mobjLabelText);
            this.Controls.Add(this.mobjIconLayout);
            this.Controls.Add(this.mobjButtonsLayout);
            this.DockPadding.All = 10;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxWindow2";
            this.mobjIconLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Gizmox.WebGUI.Forms.TableLayoutPanel mobjButtonsLayout;
        private Gizmox.WebGUI.Forms.Panel mobjIconLayout;
        private Gizmox.WebGUI.Forms.PictureBox mobjIcon;
        private Gizmox.WebGUI.Forms.Label mobjLabelText;
        private Gizmox.WebGUI.Forms.Button mobjButton1;
        private Gizmox.WebGUI.Forms.Button mobjButton2;
        private Gizmox.WebGUI.Forms.Button mobjButton3;

        #endregion


    }
}