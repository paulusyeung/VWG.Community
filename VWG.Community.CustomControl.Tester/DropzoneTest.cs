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

#endregion

namespace VWG.Community.CustomControl.Tester
{
    public partial class DropzoneTest : Form
    {
        public DropzoneTest()
        {
            InitializeComponent();
        }

        private void DropzoneTest_Load(object sender, EventArgs e)
        {
            //Controls.Clear();
            var dz = new DropzoneBox()
            {
                //BackColor = Color.DarkGray,
                Dock = DockStyle.Fill,
                Name = "dropZone"
            };
            groupBox1.Height = 480;
            groupBox1.Controls.Add(dz);
        }
    }
}