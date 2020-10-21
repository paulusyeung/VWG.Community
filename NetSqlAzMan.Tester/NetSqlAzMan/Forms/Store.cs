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
using static NetSqlAzMan.Tester.NetSqlAzMan.Helper.Enums;
using NetSqlAzMan.Interfaces;

#endregion

namespace NetSqlAzMan.Tester.NetSqlAzMan.Forms
{
    public partial class Store : Form
    {
        private Mode _Mode = Mode.Read;

        public Store()
        {
            InitializeComponent();
        }

        private void Store_Load(object sender, EventArgs e)
        {
            var storage = (IAzManStorage)Session["storage"];
            var store = (IAzManStore)Session["selectedObject"];

            if (store != null)
            {
                _Mode = Mode.Update;

                Text = "Store Properties - " + store.Name;
            }
            else
            {
                _Mode = Mode.Create;

                Text = "Create a New Store";
            }
        }
    }
}