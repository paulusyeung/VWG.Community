using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace VWG.Community.UtilTest
{
    partial class OpenCCTest
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
            this.cboConfiguration = new Gizmox.WebGUI.Forms.ComboBox();
            this.txtSource = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdConvert = new Gizmox.WebGUI.Forms.Button();
            this.txtResult = new Gizmox.WebGUI.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboConfiguration
            // 
            this.cboConfiguration.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.cboConfiguration.FormattingEnabled = true;
            this.cboConfiguration.Location = new System.Drawing.Point(20, 21);
            this.cboConfiguration.Name = "cboConfiguration";
            this.cboConfiguration.Size = new System.Drawing.Size(596, 21);
            this.cboConfiguration.TabIndex = 0;
            this.cboConfiguration.SelectedIndexChanged += new System.EventHandler(this.cboConfiguration_SelectedIndexChanged);
            // 
            // txtSource
            // 
            this.txtSource.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.txtSource.Location = new System.Drawing.Point(20, 57);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtSource.Size = new System.Drawing.Size(596, 104);
            this.txtSource.TabIndex = 1;
            // 
            // cmdConvert
            // 
            this.cmdConvert.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.cmdConvert.Location = new System.Drawing.Point(426, 173);
            this.cmdConvert.Name = "cmdConvert";
            this.cmdConvert.Size = new System.Drawing.Size(190, 23);
            this.cmdConvert.TabIndex = 2;
            this.cmdConvert.Text = "Convert";
            this.cmdConvert.Click += new System.EventHandler(this.cmdConvert_Click);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.txtResult.Location = new System.Drawing.Point(20, 209);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(596, 104);
            this.txtResult.TabIndex = 1;
            // 
            // OpenCCTest
            // 
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.cmdConvert);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.cboConfiguration);
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "OpenCCTest";
            this.Load += new System.EventHandler(this.OpenCCTest_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private ComboBox cboConfiguration;
        private TextBox txtSource;
        private Button cmdConvert;
        private TextBox txtResult;
    }
}