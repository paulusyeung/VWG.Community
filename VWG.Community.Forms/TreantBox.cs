#region Using

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

using Gizmox.WebGUI;
using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Extensibility;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Design;
using Gizmox.WebGUI.Forms.Skins;

using VWG.Community.Forms.Helper;
using VWG.Community.Forms.Skins;

#endregion Using

namespace VWG.Community.Forms
{
    /// <summary>
    /// Summary description for TreantBox
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmapAttribute(typeof(TreantBox), "TreantBox.bmp")]
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
    [Serializable()]
    /*! 加 MetadataTag XmlDoc 會變咗係 TreantBox.html，奇怪？
     *  明明哋：加咗，VWG 會用 TreantBox.xslt
     *  唔加 MetadataTag 就會 call this.Source，可以正常顯示 */
    //[MetadataTag("TreantBox")]
    [Skin(typeof(TreantBoxSkin))]
    public partial class TreantBox : HtmlBox
    {
        private static String _FormId = String.Empty;
        private static String _BoxId = String.Empty;

        #region "C'tor"
        public TreantBox()
        {
            //InitializeComponent();

            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }

        public TreantBox(String xmlData)
        {
            //InitializeComponent();

            this.TreantBoxDataFile = xmlData;

            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }
        #endregion

        #region "Properties"

        #region "Serializable property definitions"
        private static readonly SerializableProperty TreantBoxDataFileProperty = SerializableProperty.Register("TreantBoxDataFile", typeof(string), typeof(TreantBox));

        private static readonly SerializableProperty BackColorProperty = SerializableProperty.Register("BackColor", typeof(String), typeof(TreantBox));
        private static readonly SerializableProperty ForeColorProperty = SerializableProperty.Register("ForeColor", typeof(String), typeof(TreantBox));
        #endregion

        /// <summary>
        /// Text used to prompt for files on upload control
        /// </summary>
        [Category("TreantBox")]
        [Description("Data Json file to be shown")]
        [DefaultValue("")]
        public string TreantBoxDataFile
        {
            get
            {
                return this.GetValue<string>(TreantBox.TreantBoxDataFileProperty, String.Empty);
            }
            set
            {
                if (this.TreantBoxDataFile != value)
                {
                    this.SetValue<string>(TreantBox.TreantBoxDataFileProperty, value);
                    this.Update();
                }
            }
        }

        [Category("TreantBox")]
        [Description("Background Color of TreantBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color BackColor
        {
            get
            {
                var color = this.GetValue<String>(TreantBox.BackColorProperty, "transparent");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(TreantBox.BackColorProperty, ColorHelper.SafeColorName(color), "transparent");
                    this.Update();
                }
            }
        }

        [Category("TreantBox")]
        [Description("Text Color of TreantBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color ForeColor
        {
            get
            {
                var color = this.GetValue<String>(TreantBox.ForeColorProperty, "black");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(TreantBox.ForeColorProperty, ColorHelper.SafeColorName(color), "black");
                    this.Update();
                }
            }
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                if (base.Enabled != value)
                {
                    base.Enabled = value;
                    this.Update();
                }
            }
        }
        #endregion

        #region "Rendering"
        protected override void RenderAttributes(IContext context, IAttributeWriter writer)
        {
            base.RenderAttributes(context, writer);

            writer.WriteAttributeString("SEL", "SELECTABLE");
            writer.WriteAttributeString("ANYTHING", "ANYTHING");

            //writer.WriteAttributeString(WGAttributes.Text, Text);
            String url = (new SkinResourceHandle(typeof(TreantBoxSkin), "TreantBox.html")).ToString();
            writer.WriteAttributeString("sUrl", url);   // 冇用嘅，攞嚟試下 Data_GetAttribute work 唔 work
        }
        #endregion

        #region "Event handling/firing"
        protected override void FireEvent(IEvent objEvent)
        {
            switch (objEvent.Type)
            {
                case "XonomyHarvest":
                    SaveHarvest(objEvent["Value"]);
                    break;
                default:
                    base.FireEvent(objEvent);
                    break;
            }
        }

        private bool SaveHarvest(String editedXml)
        {
            Boolean result = false;

            String filepath = this.TreantBoxDataFile.StartsWith("~/") ? Context.Server.MapPath(this.TreantBoxDataFile) : Context.Server.MapPath("~/" + this.TreantBoxDataFile);
            if (File.Exists(filepath))
            {
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    sw.Write(editedXml);
                    sw.Close();
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Invoke client side TreantBox.js 的 function harvest(formId)
        /// 然後，client 會 raise event，我哋（server) 去 TreantBox.FireEvent 收貨
        /// HACK: async，掂樣可以等埋 FireEvent 呢？
        /// </summary>
        public void Harvest()
        {
            String script = String.Format("document.getElementById(\"TRG_{0}\").contentWindow.harvest(\"VWG_{1}\");", _BoxId, _FormId);
            VWGClientContext.Current.Invoke(this, "eval", script);
        }

        /// <summary>
        /// 原本打算用 Custom Attributes pass 啲 XmlData & DocSpec 去 client，
        /// 不過搞唔掂 Data_GetAttribute，改用 QueryString
        /// </summary>
        public void ShowAttribute()
        {
            String script = String.Format("document.getElementById(\"TRG_{0}\").contentWindow.showAttribute(\"{1}\");", _BoxId, _FormId);
            VWGClientContext.Current.Invoke(this, "eval", script);
        }
        #endregion

        #region Hiding HtmlBox properties
        // Prevent design time serialization and setting of certain HtmlBox properties that could interfere 
        // with TreantBox's rendering.
        // 照抄 CKEditor

        #region Not in use
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Html { get { return ""; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsWindowless { get { return false; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Url { get { return ""; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Path { get { return ""; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Gizmox.WebGUI.Common.Resources.ResourceHandle Resource { get { return null; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HtmlBoxType Type { get { return HtmlBoxType.HTML; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ContentType { get { return "text/html"; } }
        #endregion

        /// <summary>
        /// 直接 inject 啲 html page + Query Strings，原本唔想用 Query Strings，不過掂搞都用唔倒 client side javascript Data_GetAttribute
        /// query string 1: xml = xml data file
        /// query string 2: spec = docSpec file
        /// </summary>
        protected override string Source
        {
            get
            {
                TreantBoxSkin skin = this.Skin as TreantBoxSkin;
                if (skin != null)
                {
                    String src = (new SkinResourceHandle(typeof(TreantBoxSkin), "TreantBox.html")).ToString();  // 將 TreantBoxSkin 中的 TreantBox.html 轉換為 VWG 式 http url link

                    String dataFile = (new GeneralResourceHandle(this.TreantBoxDataFile)).ToString();

                    return String.Format("{0}?datafile={1}", src, dataFile);
                }

                return base.Source;
            }
        }
        #endregion 
    }
}