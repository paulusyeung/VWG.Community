#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace VWG.Community.CustomControl.Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            CustomControl.XonomyBox box2 = new CustomControl.XonomyBox("Resources/UserData/SampleXmlData.xml", "Resources/UserData/SampleDocSpec.js");
            box2.Dock = DockStyle.Fill;
            this.rightPanel.Controls.Add(box2);
        }

        private void cmdLoadXonomyBox_Click(object sender, EventArgs e)
        {
            CustomControl.XonomyBox box = (CustomControl.XonomyBox)this.rightPanel.Controls[0];
            box.XmlData = "Resources/UserData/SampleXmlData.xml";
            box.DocSpec = "Resources/UserData/SampleDocSpec.js";

            this.rightPanel.Controls.Add(box);
            this.Update();
        }

        private void cmdHarvest_Click(object sender, EventArgs e)
        {
            CustomControl.XonomyBox box = (CustomControl.XonomyBox)this.rightPanel.Controls[0];
            box.Harvest();
        }

        private void cmdShowAttribute_Click(object sender, EventArgs e)
        {
            CustomControl.XonomyBox box2 = (CustomControl.XonomyBox)this.rightPanel.Controls[0];
            if (box2 != null) box2.ShowAttribute();
        }
    }
}