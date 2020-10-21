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
    /// Summary description for DropzoneBoxSkin
    /// </summary>   
    public class DropzoneBoxSkin : HtmlBoxSkin
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
                return new TextResourceReference(typeof(DropzoneBoxSkin), "DropzoneBox.html");
            }
        }
    }
}

