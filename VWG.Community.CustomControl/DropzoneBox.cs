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
using System.Web;
using System.IO;
using Gizmox.WebGUI.Common.Resources;


#endregion

namespace VWG.Community.CustomControl
{
    /// <summary>
    /// Summary description for DropzoneBox
    /// </summary>
    [ToolboxItem(true)]
    //[ToolboxBitmapAttribute(typeof(DropzoneBox), "VWG.Community.CustomControl.DropzoneBox.bmp")]
    //[DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    //[ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
    [Serializable()]
    //[MetadataTag("VWG.Community.CustomControl.DropzoneBox")]
    [Skin(typeof(DropzoneBoxSkin))]
    public partial class DropzoneBox : HtmlBox
    {
        private static String _FormId = String.Empty;
        private static String _BoxId = String.Empty;

        public DropzoneBox()
        {
            InitializeComponent();

            _FormId = this.ID.ToString();
            _BoxId = this.ID.ToString();
        }

        protected override void RenderAttributes(IContext objContext, IAttributeWriter objWriter)
        {
            base.RenderAttributes(objContext, objWriter);

            //VWGClientContext.Current.Invoke(string.Format("$('#VWG_{0}').attr('vwgvisible',0);$('#VWG_{0}').css('display','none')", this.ID));
        }

        protected override void FireEvent(IEvent objEvent)
        {
            //MessageBox.Show(objEvent.ToString());
            switch (objEvent.Type)
            {
                case "XonomyHarvest":
                    var value = objEvent["Value"];
                    ProcessRequest(VWGContext.Current.HttpContext);
                    break;
                default:
                    var val = objEvent["Value"];

                    String script = String.Format("document.getElementById(\"TRG_{0}\").contentWindow.harvest(\"VWG_{1}\");", _BoxId, _FormId);
                    VWGClientContext.Current.Invoke(this, "eval", script);

                    base.FireEvent(objEvent);
                    break;
            }
        }

        /// <summary>
        /// get the files, rename it and save it to our Media Uploader folder .i.e.  ( MediaUploader )
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            numFiles = files.Length;
            numFiles = numFiles + 1;

            string str_image = "";

            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                //  int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExtension = System.IO.Path.GetExtension(fileName);
                    str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                    string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                    file.SaveAs(pathToSave_100);
                }
            }
            context.Response.Write(str_image);
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
        public override ResourceHandle Resource { get { return null; } }
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
                DropzoneBoxSkin skin = this.Skin as DropzoneBoxSkin;
                if (skin != null)
                {
                    String src = (new SkinResourceHandle(typeof(DropzoneBoxSkin), "DropzoneBox.html")).ToString();    // 將 DropzoneBoxSkin 中的 dropzonebox.html 轉換為 VWG 式 http url link

                    return src;
                }

                return base.Source;
            }
        }
        #endregion 
    }
}