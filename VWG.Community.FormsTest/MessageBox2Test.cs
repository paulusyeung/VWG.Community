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
using VWG.Community.Forms;

#endregion

namespace VWG.Community.FormsTest
{
    public partial class MessageBox2Test : Form
    {
        public MessageBox2Test()
        {
            InitializeComponent();
        }

        private void MessageBox2Test_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox2.Show("This is MessageBox2", true, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox2.Show("This is MessageBox2", "Caption Text", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, true, null);
        }
    }
}