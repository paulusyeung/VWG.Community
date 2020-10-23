using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.ComponentModel;
using Gizmox.WebGUI.Forms.Skins;

namespace VWG.Community.Forms.Skins
{
    /// <summary>
    /// Summary description for XonomyBoxSkin
    /// </summary>   
    public class XonomyBoxSkin : HtmlBoxSkin
    {
        private void InitializeComponent()
        {

        }


        /// <summary>
        /// Gets the HTML resource.
        /// </summary>
        /// <value>The HTML resource.</value>
        public TextResourceReference HtmlResource
        {
            get
            {
                return new TextResourceReference(typeof(XonomyBoxSkin), "Xonomy.html");
            }
        }

    }
}
