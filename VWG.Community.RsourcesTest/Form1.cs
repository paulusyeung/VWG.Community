#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using VWG.Community.Resources;
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace VWG.Community.ResourcesTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.htmlCustom.Url         = (new StaticCustomResourcesFileResourceHandle("George.png")).ToString();
            this.htmlFile.Url           = (new StaticFileResourceHandle("Icons/George.png")).ToString();
            this.htmlUnrestricted.Url   = (new StaticUnrestrictedFileResourceHandle(Context.Server.MapPath("~/Web.config"))).ToString();
            this.htmlUserData.Url       = (new StaticUserDataResourceHandle("George.png")).ToString();

            this.htmlImage.Url          = (new ImageResourceHandle("George.png")).ToString();
        }

        private void btnResizeForm_Click(object sender, EventArgs e)
        {
            Context.Transfer(new ResizeForm());
        }
    }
}