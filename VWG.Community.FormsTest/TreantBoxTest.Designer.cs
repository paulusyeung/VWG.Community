namespace VWG.Community.FormsTest
{
    partial class TreantBoxTest
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
            this.leftPanel = new Gizmox.WebGUI.Forms.Panel();
            this.rightPanel = new Gizmox.WebGUI.Forms.Panel();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(180, 600);
            this.leftPanel.TabIndex = 1;
            // 
            // rightPanel
            // 
            this.rightPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(180, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(620, 600);
            this.rightPanel.TabIndex = 2;
            // 
            // TreantBoxTest
            // 
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Size = new System.Drawing.Size(800, 600);
            this.Text = "XonomyBoxTest";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel leftPanel;
        private Gizmox.WebGUI.Forms.Panel rightPanel;
    }
}