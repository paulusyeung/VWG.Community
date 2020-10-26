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
using VWG.Community.Forms;
using System.IO;

#endregion

namespace VWG.Community.FormsTest
{
    public partial class TreantBoxTest : Form
    {
        public TreantBoxTest()
        {
            InitializeComponent();

            VWGContext.Current.CurrentTheme = "Vista";
            //VWGContext.Current.CurrentTheme = "Graphite";

            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            string filename = "json-data-sample.json", json = "";
            var file = File.OpenRead(Path.Combine(VWGContext.Current.Config.GetDirectory("UserData"), filename));
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }

            //var box2 = new TreantBox("Resources/UserData/json-data-sample.json");
            // or
            var box2 = new TreantBox();
            box2.TreantBoxDataJson = json;

            box2.Dock = DockStyle.Fill;
            this.rightPanel.Controls.Add(box2);
        }
    }
}