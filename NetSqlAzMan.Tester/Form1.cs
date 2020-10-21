#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

#endregion

namespace NetSqlAzMan.Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VWGContext.Current.CurrentTheme = "Vista";
            //VWGContext.Current.CurrentTheme = "Graphite";

            var webConsole = new VWG.Community.NetSqlAzMan.WebConsole()
            {
                Dock =  DockStyle.Fill
            };
            webConsole.SqlConnectionString = @"Data Source=192.168.12.141;Initial Catalog=NetSqlAzmanStorage_Newish;Integrated Security=False;User ID=sa;Password=sa-9602;";
            webConsole.Theme = "light";
            this.Form.Controls.Add(webConsole);
        }
    }
}