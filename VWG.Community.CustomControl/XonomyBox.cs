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


#endregion

namespace VWG.Community.CustomControl
{
    /// <summary>
    /// Summary description for XonomyBox
    /// </summary>
    [ToolboxItem(true)]
    //[ToolboxBitmapAttribute(typeof(XonomyBox), "VWG.Community.CustomControl.XonomyBox.bmp")]
    //[DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.0.5701.0 , Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    //[ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.0.5701.0 , Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
    [Serializable()]
    //HACK: 唔用得 MetadataTag，一用就唔識 load
    //[MetadataTag("VWG.Community.CustomControl.XonomyBox")]
    [Skin(typeof(XonomyBoxSkin))]
    public partial class XonomyBox : HtmlBox
    {
        private String _XmlData = null;
        private String _DocSpec = null;

        private static String _FormId = String.Empty;
        private static String _BoxId = String.Empty;

        public XonomyBox()
        {
            InitializeComponent();

            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }

        public XonomyBox(String xmlData, String docSpec)
        {
            InitializeComponent();

            this.XmlData = xmlData;
            this.DocSpec = docSpec;

            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }

        protected override void RenderAttributes(IContext context, IAttributeWriter writer)
        {
            base.RenderAttributes(context, writer);

            //writer.WriteAttributeString(WGAttributes.Text, Text);
            String url = (new SkinResourceHandle(typeof(CustomControl.XonomyBoxSkin), "Xonomy.html")).ToString();
            writer.WriteAttributeString("sUrl", url);   // 冇用嘅，攞嚟試下 Data_GetAttribute work 唔 work
        }

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

            String filepath = this.XmlData.StartsWith("~/") ? Context.Server.MapPath(this.XmlData) : Context.Server.MapPath("~/" + this.XmlData);
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

        public String XmlData
        {
            get { return _XmlData; }
            set { _XmlData = value; }
        }

        public String DocSpec
        {
            get { return _DocSpec; }
            set { _DocSpec = value; }
        }

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
                    String src = (new SkinResourceHandle(typeof(CustomControl.XonomyBoxSkin), "XonomyBox.html")).ToString();    // 將 XonomyBoxSkin 中的 XonomyBox.html 轉換為 VWG 式 http url link

                    String xmlFile = (new GeneralResourceHandle(this.XmlData)).ToString();                                      // 將 XonomyBox.XmlData 轉換為 VWG 式 http url link
                    String docSpec = (new GeneralResourceHandle(this.DocSpec)).ToString();                                      // 將 XonomyBox.DocSpec 轉換為 VWG 式 http url link

                    return String.Format("{0}?xml={1}&spec={2}", src, xmlFile, docSpec);
                }

                return base.Source;
            }
        }
        #endregion 

    }
}