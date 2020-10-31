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

namespace VWG.Community.UtilTest
{
    public partial class CountryCodes : Form
    {
        public CountryCodes()
        {
            InitializeComponent();
        }

        private void CountryCodes_Load(object sender, EventArgs e)
        {
            var codes = new VWG.Community.Util.CountryCodes();
            var list = codes.GetList();

            var lvw = new ListView()
            {
                Dock = DockStyle.Fill,
                DataSource = list
            };
            Controls.Add(lvw);
        }
    }
}