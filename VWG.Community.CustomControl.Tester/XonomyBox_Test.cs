#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace VWG.Community.CustomControl.Tester
{
    public partial class XonomyBox_Test : Form
    {
        public XonomyBox_Test()
        {
            InitializeComponent();
        }

        private void XonomyBox_Test_Load(object sender, EventArgs e)
        {
            CustomControl.XonomyBox box = new CustomControl.XonomyBox();
            box.XmlData = "~/Resources/UserData/SampleXmlData.xml";
            box.DocSpec = "~/Resources/UserData/SampleDocSpec.js";
            box.Dock = DockStyle.Fill;
            this.Controls.Add(box);
            this.Size = new Size(640, 480);
            this.Update();
        }
    }
}