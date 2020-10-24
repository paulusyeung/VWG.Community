#region Using

using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;


using Gizmox.WebGUI;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Skins;
using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms.Design;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Extensibility;
using Gizmox.WebGUI.Common.Resources;

using System.IO;
using System.Threading;
using VWG.Community.Forms.Skins;
using VWG.Community.Forms.Helper;
using System.Globalization;

#endregion Using

namespace VWG.Community.Forms
{
    /// <summary>
    /// Summary description for XonomyBox
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmapAttribute(typeof(XonomyBox), "XonomyBox.bmp")]
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
    [Serializable()]
    /*! 加 MetadataTag XmlData 會變咗係 XonomyBox.html，奇怪？
        唔加 MetadataTag 就會 call this.Source */
    //[MetadataTag("XonomyBox")]
    [Skin(typeof(XonomyBoxSkin))]
    public partial class XonomyBox : HtmlBox
    {
        private static String _FormId = String.Empty;
        private static String _BoxId = String.Empty;

        #region "C'tor"
        public XonomyBox()
        {
            //InitializeComponent();

            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }

        public XonomyBox(String xmlData, String docSpec)
        {
            //InitializeComponent();

            this.XonomyBoxXmlDoc = xmlData;
            this.XonomyBoxSpecDoc = docSpec;

            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }
        #endregion

        #region "Properties"

        #region "Serializable property definitions"
        private static readonly SerializableProperty XonomyBoxXmlDocProperty = SerializableProperty.Register("XonomyBoxXmlDoc", typeof(string), typeof(XonomyBox));
        private static readonly SerializableProperty XonomyBoxSpecDocProperty = SerializableProperty.Register("XonomyBoxSpecDoc", typeof(string), typeof(XonomyBox));

        private static readonly SerializableProperty BackColorProperty = SerializableProperty.Register("BackColor", typeof(String), typeof(XonomyBox));
        private static readonly SerializableProperty ForeColorProperty = SerializableProperty.Register("ForeColor", typeof(String), typeof(XonomyBox));
        #endregion

        /// <summary>
        /// Text used to prompt for files on upload control
        /// </summary>
        [Category("XonomyBox")]
        [Description("XML Document to be edited")]
        [DefaultValue("")]
        public string XonomyBoxXmlDoc
        {
            get
            {
                return this.GetValue<string>(XonomyBox.XonomyBoxXmlDocProperty, String.Empty);
            }
            set
            {
                if (this.XonomyBoxXmlDoc != value)
                {
                    this.SetValue<string>(XonomyBox.XonomyBoxXmlDocProperty, value);
                    this.Update();
                }
            }
        }

        [Category("XonomyBox")]
        [Description("Specification used to read the XML Document")]
        [DefaultValue("")]
        public string XonomyBoxSpecDoc
        {
            get
            {
                return this.GetValue<string>(XonomyBox.XonomyBoxSpecDocProperty, String.Empty);
            }
            set
            {
                if (this.XonomyBoxSpecDoc != value)
                {
                    this.SetValue<string>(XonomyBox.XonomyBoxSpecDocProperty, value);
                    this.Update();
                }
            }
        }

        [Category("XonomyBox")]
        [Description("Background Color of XonomyBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color BackColor
        {
            get
            {
                var color = this.GetValue<String>(XonomyBox.BackColorProperty, "transparent");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(XonomyBox.BackColorProperty, ColorHelper.SafeColorName(color), "transparent");
                    this.Update();
                }
            }
        }

        [Category("XonomyBox")]
        [Description("Text Color of XonomyBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color ForeColor
        {
            get
            {
                var color = this.GetValue<String>(XonomyBox.ForeColorProperty, "black");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(XonomyBox.ForeColorProperty, ColorHelper.SafeColorName(color), "black");
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

            //writer.WriteAttributeString(WGAttributes.Text, Text);
            String url = (new SkinResourceHandle(typeof(Skins.XonomyBoxSkin), "Xonomy.html")).ToString();
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

            String filepath = this.XonomyBoxXmlDoc.StartsWith("~/") ? Context.Server.MapPath(this.XonomyBoxXmlDoc) : Context.Server.MapPath("~/" + this.XonomyBoxXmlDoc);
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
        /// Invoke client side XonomyBox.js 的 function harvest(formId)
        /// 然後，client 會 raise event，我哋（server) 去 XonomyBox.FireEvent 收貨
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
        // with XonomyBox's rendering.
        // 照抄 CKEditor

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

        /// <summary>
        /// 直接 inject 啲 html page + Query Strings，原本唔想用 Query Strings，不過掂搞都用唔倒 client side javascript Data_GetAttribute
        /// query string 1: xml = xml data file
        /// query string 2: spec = docSpec file
        /// </summary>
        protected override string Source
        {
            get
            {
                XonomyBoxSkin skin = this.Skin as XonomyBoxSkin;
                if (skin != null)
                {
                    String src = (new SkinResourceHandle(typeof(XonomyBoxSkin), "XonomyBox.html")).ToString();  // 將 XonomyBoxSkin 中的 XonomyBox.html 轉換為 VWG 式 http url link

                    String xmlFile = (new GeneralResourceHandle(this.XonomyBoxXmlDoc)).ToString();              // 將 XonomyBox.XmlData 轉換為 VWG 式 http url link
                    String docSpec = (new GeneralResourceHandle(this.XonomyBoxSpecDoc)).ToString();             // 將 XonomyBox.DocSpec 轉換為 VWG 式 http url link

                    return String.Format("{0}?xml={1}&spec={2}", src, xmlFile, docSpec);
                }

                return base.Source;
            }
        }
        #endregion 
    }
}

