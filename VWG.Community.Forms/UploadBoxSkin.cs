#region Using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Gizmox.WebGUI.Forms.Skins;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;
using System.ComponentModel;
using Gizmox.WebGUI.Common.Interfaces;

#endregion

namespace VWG.Community.Forms.Skins
{
    /// <summary>
    /// Summary description for UploadBoxSkin
    /// </summary>   
    [Serializable]
    public class UploadBoxSkin : ControlSkin
    {
        private void InitializeComponent()
        {
            
        }

        /// <summary>
        /// Gets or sets Cursor for the upload control
        /// </summary>
        /// <value></value>
        [Category("UploadBox")]
        [Description("Cursor for the Upload Control")]
        public virtual Cursor Cursor
        {
            get
            {
                var cursor = this.GetValue<Cursor>("Cursor", Cursors.No); ;
                return this.GetValue<Cursor>("Cursor", Cursors.No);
            }
            set
            {
                this.SetValue("Cursor", value);
            }
        }

        /// <summary>
        /// Gets or sets Background Color for the upload control
        /// </summary>
        /// <value></value>
        [Category("UploadBox")]
        [Description("Background Color for the Upload Control")]
        public virtual String BackgroundColor
        {
            get
            {
                // 2020.10.20 paulus: hard-code 唔理想，改為 UploadBox.cs properties
                var color = "";
                switch (VWGContext.Current.CurrentTheme)
                {
                    case "dark":
                    case "Graphite":
                        color = "#212121";
                        break;
                    case "light":
                    case "Vista":
                    default:
                        color = "#5CB85C";
                        break;
                }
                return color;   // this.GetValue<Color>("BackgroundColor", Color.Transparent);
            }
            set
            {
                this.SetValue("BackgroundColor", value);
            }
        }

        /// <summary>
        /// Gets or sets Cursor for the upload control
        /// </summary>
        /// <value></value>
        [Category("UploadButton")]
        [Description("Cursor for the Upload Button")]
        public virtual Cursor UploadButtonCursor
        {
            get
            {
                return this.GetValue<Cursor>("UploadButtonCursor", Cursors.No);
            }
            set
            {
                this.SetValue("UploadButtonCursor", value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the upload button in pixels
        /// </summary>
        /// <value></value>
        [Category("UploadButton")]
        [Description("Width of upload button in pixels.")]
        public virtual int UploadButtonWidth
        {
            /**
            UploadBox.Frame.css 中 [Skin.UploadButtonWidth] 會自動 call 呢度，
            不過我唔知點樣可以 dynamic 改佢，照計應該同 GetValue 個 Key 有關？
            我試過喺 UploadBox.cs 用：
                var skin = (UploadBoxSkin)this.Skin;
                skin.UploadButtonWidth = value;
            Compile 冇事，run 嘅時候會出：
                "Cannot set a skin value in runtime."
            */
            get
            {
                var val = this.GetValue<int>("UploadButtonWidth", 40);

                return val == 0 ? 40 : val;
            }
            set
            {
                this.SetValue("UploadButtonWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets Style of the upload button
        /// </summary>
        /// <value></value>
        [Category("UploadButton")]
        [Description("Style of the Upload button")]
        public StyleValue UploadButtonStyle
        {
            get
            {
                StyleValue objStyle = new StyleValue(this, "UploadButtonStyle");
                return objStyle;
            }
        }

        internal void ResetUploadButtonStyle()
        {
            this.Reset("UploadButtonStyle");
        }

        /// <summary>
        /// Gets or sets the height of progress bar in pixels
        /// </summary>
        /// <value></value>
        [Category("UploadBar")]
        [Description("Height of progress bar in pixels")]
        public virtual int UploadBarHeight
        {
            get
            {
                return this.GetValue<int>("UploadBarHeight", 0);
            }
            set
            {
                this.SetValue("UploadBarHeight", value);
            }
        }

        /// <summary>
        /// Gets or sets Style of the not completed area area of the progress bar bar
        /// </summary>
        /// <value></value>
        [Category("UploadBar")]
        [Description("Style of the not completed area area of the progress bar")]
        public StyleValue UploadBarStyle
        {
            get
            {
                StyleValue objStyle = new StyleValue(this, "UploadBarStyle");
                return objStyle;
            }
        }

        internal void ResetUploadBarStyle()
        {
            this.Reset("UploadBarStyle");
        }

        /// <summary>
        /// Gets or sets Style of completed area of the progress bar
        /// </summary>
        /// <value></value>
        [Category("UploadBar")]
        [Description("Style of completed area of the progress bar")]
        public StyleValue UploadBarCompletedStyle
        {
            get
            {
                StyleValue objStyle = new StyleValue(this, "UploadBarCompletedStyle");
                return objStyle;
            }
        }

        internal void ResetUploadBarCompletedStyle()
        {
            this.Reset("UploadBarCompletedStyle");
        }
    }
}

