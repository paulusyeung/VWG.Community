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
            this.cmdShowAttribute = new Gizmox.WebGUI.Forms.Button();
            this.cmdHarvest = new Gizmox.WebGUI.Forms.Button();
            this.rightPanel = new Gizmox.WebGUI.Forms.Panel();
            this.leftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.cmdShowAttribute);
            this.leftPanel.Controls.Add(this.cmdHarvest);
            this.leftPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(180, 600);
            this.leftPanel.TabIndex = 1;
            // 
            // cmdShowAttribute
            // 
            this.cmdShowAttribute.Location = new System.Drawing.Point(27, 71);
            this.cmdShowAttribute.Name = "cmdShowAttribute";
            this.cmdShowAttribute.Size = new System.Drawing.Size(133, 34);
            this.cmdShowAttribute.TabIndex = 0;
            this.cmdShowAttribute.Text = "Show Attribute";
            this.cmdShowAttribute.Click += new System.EventHandler(this.cmdShowAttribute_Click);
            // 
            // cmdHarvest
            // 
            this.cmdHarvest.Location = new System.Drawing.Point(27, 24);
            this.cmdHarvest.Name = "cmdHarvest";
            this.cmdHarvest.Size = new System.Drawing.Size(133, 34);
            this.cmdHarvest.TabIndex = 0;
            this.cmdHarvest.Text = "Harvest";
            this.cmdHarvest.Click += new System.EventHandler(this.cmdHarvest_Click);
            // 
            // rightPanel
            // 
            this.rightPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(180, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(620, 600);
            this.rightPanel.TabIndex = 2;
            // 
            // Form1
            // 
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Size = new System.Drawing.Size(800, 600);
            this.Text = "XonomyBoxTest";
            this.leftPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel leftPanel;
        private Gizmox.WebGUI.Forms.Panel rightPanel;
        private Gizmox.WebGUI.Forms.Button cmdHarvest;
        private Gizmox.WebGUI.Forms.Button cmdShowAttribute;
    }
}