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
using System.Text.RegularExpressions;

#endregion Using

namespace VWG.Community.Forms
{
    /// <summary>
    /// A ECharts control
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmapAttribute(typeof(EChartsGMapBox), "EChartsGMapBox.bmp")]
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
    [Serializable()]
    //[MetadataTag("EChartsGMapBox")]
    [Skin(typeof(EChartsGMapBoxSkin))]
    public partial class EChartsGMapBox : HtmlBox
    {
        private static String _FormId = String.Empty;
        private static String _BoxId = String.Empty;

        #region "C'tor"
        public EChartsGMapBox()
        {
            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }
        #endregion

        #region "Properties"

        #region "Serializable property definitions"
        private static readonly SerializableProperty EChartsGoogleApiKeyProperty = SerializableProperty.Register("EChartsGoogleApiKey", typeof(string), typeof(EChartsGMapBox));
        private static readonly SerializableProperty EChartsGoogleMapLocaleProperty = SerializableProperty.Register("EChartsGoogleMapLocale", typeof(string), typeof(EChartsGMapBox));
        private static readonly SerializableProperty EChartsOptionProperty = SerializableProperty.Register("EChartsOption", typeof(string), typeof(EChartsGMapBox));
        private static readonly SerializableProperty EChartsExtraScriptProperty = SerializableProperty.Register("EChartsExtraScript", typeof(string), typeof(EChartsGMapBox));

        private static readonly SerializableProperty BackColorProperty = SerializableProperty.Register("BackColor", typeof(String), typeof(EChartsGMapBox));
        private static readonly SerializableProperty ForeColorProperty = SerializableProperty.Register("ForeColor", typeof(String), typeof(EChartsGMapBox));
        #endregion

        /// <summary>
        /// Google Api Key
        /// </summary>
        [Category("EChartsGMapBox")]
        [Description("Private Google Api Key")]
        [DefaultValue("")]
        public string EChartsGoogleApiKey
        {
            get
            {
                return this.GetValue<string>(EChartsGMapBox.EChartsGoogleApiKeyProperty, String.Empty);
            }
            set
            {
                if (this.EChartsGoogleApiKey != value)
                {
                    this.SetValue<string>(EChartsGMapBox.EChartsGoogleApiKeyProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Google Map Locale: en, zh-CN, zh-HK
        /// </summary>
        [Category("EChartsGMapBox")]
        [Description("Google Map Locale")]
        [DefaultValue("")]
        public string EChartsGoogleMapLocale
        {
            get
            {
                return this.GetValue<string>(EChartsGMapBox.EChartsGoogleMapLocaleProperty, "en");
            }
            set
            {
                if (this.EChartsGoogleMapLocale != value)
                {
                    this.SetValue<string>(EChartsGMapBox.EChartsGoogleMapLocaleProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Text used to prompt for files on upload control
        /// </summary>
        [Category("EChartsGMapBox")]
        [Description("Data Json file to be shown")]
        [DefaultValue("")]
        public string EChartsOption
        {
            get
            {
                return this.GetValue<string>(EChartsGMapBox.EChartsOptionProperty, String.Empty);
            }
            set
            {
                if (this.EChartsOption != value)
                {
                    this.SetValue<string>(EChartsGMapBox.EChartsOptionProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Text used to prompt for files on upload control
        /// </summary>
        [Category("EChartsGMapBox")]
        [Description("Extra Javascript to be added to page")]
        [DefaultValue("")]
        public string EChartsExtraScript
        {
            get
            {
                return this.GetValue<string>(EChartsGMapBox.EChartsExtraScriptProperty, String.Empty);
            }
            set
            {
                if (this.EChartsExtraScript != value)
                {
                    this.SetValue<string>(EChartsGMapBox.EChartsExtraScriptProperty, value);
                    this.Update();
                }
            }
        }

        [Category("EChartsGMapBox")]
        [Description("Background Color of EChartsGMapBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color BackColor
        {
            get
            {
                var color = this.GetValue<String>(EChartsGMapBox.BackColorProperty, "transparent");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(EChartsGMapBox.BackColorProperty, ColorHelper.SafeColorName(color), "transparent");
                    this.Update();
                }
            }
        }

        [Category("EChartsGMapBox")]
        [Description("Text Color of EChartsGMapBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color ForeColor
        {
            get
            {
                var color = this.GetValue<String>(EChartsGMapBox.ForeColorProperty, "black");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(EChartsGMapBox.ForeColorProperty, ColorHelper.SafeColorName(color), "black");
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

            #region Custom Attributes
            //writer.WriteAttributeString("SEL", "SELECTABLE");
            //writer.WriteAttributeString("ANYTHING", "ANYTHING WILL DO");

            #region ECharts with simple option
            var pattern = @"(""(?:[^""\\]|\\.)*"")|\s+";
            var echartsOption = Regex.Replace(@"{
    ""title"": {
        ""text"": ""第一个 ECharts 实例""
    },
    ""tooltip"": {},
    ""legend"": {
        ""data"":[""销量""]
    },
    ""xAxis"": {
        ""data"": [""衬衫"",""羊毛衫"",""雪纺衫"",""裤子"",""高跟鞋"",""袜子""]
    },
    ""yAxis"": {},
    ""series"": [{
        ""name"": ""销量"",
        ""type"": ""bar"",
        ""data"": [5, 20, 36, 10, 10, 20]
    }]
}", pattern, "$1");   // Use RegEx to remove spaces CRLF
            #endregion

            writer.WriteAttributeString("EChartsOption", this.EChartsOption == string.Empty ? echartsOption : this.EChartsOption);

            //writer.WriteAttributeString(WGAttributes.Text, Text);
            String url = (new SkinResourceHandle(typeof(EChartsGMapBoxSkin), "EChartsGMapBox.html")).ToString();
            writer.WriteAttributeString("sUrl", url);   // 冇用嘅，攞嚟試下 Data_GetAttribute work 唔 work

            writer.WriteAttributeString(WGAttributes.HTML5, "EChartsGMapBoxOption");

            #endregion
        }
        #endregion

        #region Hiding HtmlBox properties
        // Prevent design time serialization and setting of certain HtmlBox properties that could interfere 
        // with EChartsGMapBox's rendering.
        // 照抄 CKEditor

        #region Not in use
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Html
        {
            get
            {
                // 原本用 Source 搞嘅，不過出唔倒 Formatter 之內嘅 callback function，改為直接 replace 啲 HTML code
                #region reading embedded EChartsGMapBox.direct.html file
                var hostName = string.Format("{0}://{1}:{2}",
                    System.Web.HttpContext.Current.Request.Url.Scheme,
                    System.Web.HttpContext.Current.Request.Url.Host,
                    System.Web.HttpContext.Current.Request.Url.Port);
                var file = (new SkinResourceHandle(typeof(EChartsGMapBoxSkin), "EChartsGMapBox.html")).ToString();

                using (var wc = new System.Net.WebClient())
                {
                    var html = wc.DownloadString(hostName + "/" + file);
                    return html
                        .Replace("#EChartsGMapTitle#", this.Text)
                        .Replace("#GoogleApiKey#", this.EChartsGoogleApiKey)
                        .Replace("#GoogleMapLocale#", this.EChartsGoogleMapLocale)
                        .Replace("#VWGControlId#", _FormId.ToString())
                        .Replace("#EChartsExtraScript#", this.EChartsExtraScript)
                        .Replace("#EChartsOption#", this.EChartsOption);
                }
                #endregion
            }
        }
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
        /// 直接 inject 啲 html page + Query Strings，原本唔想用 Query Strings，不過點搞都用唔倒 client side javascript Data_GetAttribute
        /// </summary>
        protected override string Source
        {
            get
            {
                /** deprecated
                EChartsGMapBoxSkin skin = this.Skin as EChartsGMapBoxSkin;
                if (skin != null)
                {
                    String src = (new SkinResourceHandle(typeof(EChartsGMapBoxSkin), "EChartsGMapBox.html")).ToString();  // 將 EChartsGMapBoxSkin 中的 EChartsGMapBox.html 轉換為 VWG 式 http url link

                    String optionUrl = "";   // (new GeneralResourceHandle(this.EChartsOption)).ToString();

                    return String.Format("{0}?option={1}&id={2}", src, System.Web.HttpUtility.UrlEncode(optionUrl), _FormId);
                }
                */
                return base.Source;
            }
        }
        #endregion 
    }

}
