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
using System.Text.RegularExpressions;
using VWG.Community.FormsTest.Helper;

#endregion

namespace VWG.Community.FormsTest
{
    public partial class EChartsGMapBoxTest : Form
    {
        public EChartsGMapBoxTest()
        {
            InitializeComponent();
        }

        private void EChartsGMapBoxTest_Load(object sender, EventArgs e)
        {
            #region temperature bar
            var pattern = @"(""(?:[^""\\]|\\.)*"")|\s+";
            var extraScript = EChartsGMapDataHelper.GetExtraScript_HK();
            var option = EChartsGMapDataHelper.GetOption_HK();
            var gmapBox = new EChartsGMapBox()
            {
                Dock = DockStyle.Fill,
                EChartsExtraScript = extraScript,
                EChartsOption = option,
                EChartsGoogleApiKey = "",   //! string.empty = WaterMark "For development purposes only"
                EChartsGoogleMapLocale = "zh-HK"
            };
            #endregion

            this.Controls.Add(gmapBox);
        }
    }
}