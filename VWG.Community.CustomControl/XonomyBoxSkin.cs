#region Using

using System;
using System.Collections.Generic;
using System.Text;
using Gizmox.WebGUI.Forms.Skins;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;


#endregion

namespace VWG.Community.CustomControl
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

