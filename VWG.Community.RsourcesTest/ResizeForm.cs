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
using VWG.Community.Resources;

#endregion

namespace VWG.Community.RsourcesTest
{
    public partial class ResizeForm : Form
    {
        public ResizeForm()
        {
            InitializeComponent();
        }

        private void ResizeForm_Load(object sender, EventArgs e)
        {
            // StaticImageResizeHandle 已經俾 WebGUI 嘅 GeneralSizeableHandle 取代
            this.htmNoResize.Url = (new StaticImageResizeHandle("Icons/George.png")).ToString();
            this.htmlResizeTo20x20.Url = (new StaticImageResizeHandle("Icons/George.png", 20, 20)).ToString();
            this.html50Percent.Url = (new StaticImageResizeHandle("Icons/George.png", 0.50)).ToString();
            this.htmlResizeTo40x40.Url = (new StaticImageResizeHandle("Icons/George.png", 40, 40)).ToString();
            this.html150Percent.Url = (new StaticImageResizeHandle("Icons/George.png", 1.50)).ToString();
        }

        private void btnGeneralForm_Click(object sender, EventArgs e)
        {
            Context.Transfer(new Form1());
        }
    }
}