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
using Gizmox.WebGUI.Common.Gateways;
using System.Web;
using System.Xml;
using Gizmox.WebGUI.Hosting;
using VWG.Community.Forms.Helper;
using System.IO;
using Gizmox.WebGUI.Common.Resources;


#endregion

namespace VWG.Community.Forms
{
    /// <summary>
    /// Summary description for Dropzone
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmapAttribute(typeof(Dropzone), "VWG.Community.Forms.Dropzone.bmp")]
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.25701.0 , Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
    [Serializable()]
    //[MetadataTag("VWG.Community.Forms.Dropzone")]
    [MetadataTag("FC")] //FrameControl
    [Skin(typeof(DropzoneSkin))]
    public partial class Dropzone : FrameControl, IRequiresRegistration
    {
        #region Class Members 

        /// <summary>
        /// The LicenseKey property registration.
        /// </summary>
        private static readonly SerializableProperty LicenseKeyProperty = SerializableProperty.Register("LicenseKey", typeof(string), typeof(Dropzone));

        /// <summary>
        /// The PaneLayout property registration.
        /// </summary>
        private static readonly SerializableProperty PaneLayoutProperty = SerializableProperty.Register("PaneLayout", typeof(string), typeof(Dropzone));

        /// <summary>
        /// The Files property registration.
        /// </summary>
        private static readonly SerializableProperty FilesProperty = SerializableProperty.Register("Files", typeof(ImageUploaderFile[]), typeof(Dropzone));

        /// <summary>
        /// The FileUploaded event registration.
        /// </summary>
        private static readonly SerializableEvent FileUploadedEvent = SerializableEvent.Register("FileUploaded", typeof(EventHandler), typeof(Dropzone));

        /// <summary>
        /// Occurs when files where uploaded.
        /// </summary>
        public event EventHandler FileUploaded
        {
            add
            {
                this.AddHandler(Dropzone.FileUploadedEvent, value);
            }
            remove
            {
                this.RemoveHandler(Dropzone.FileUploadedEvent, value);
            }
        }

        /// <summary>
        /// Gets the hanlder for the FileUploaded event.
        /// </summary>
        private EventHandler FileUploadedHandler
        {
            get
            {
                return (EventHandler)this.GetHandler(Dropzone.FileUploadedEvent);
            }
        }

        #endregion

        #region C'Tor

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropzone"/> class.
        /// </summary>
        public Dropzone()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles raised event.
        /// </summary>
        /// <param name="objEvent">The VWG event.</param>
        protected override void FireEvent(IEvent objEvent)
        {
            // If is upload complete event
            if (objEvent.Type == "Upload")
            {
                // Get the event handler if possible
                EventHandler objFileUploadedHandler = this.FileUploadedHandler;
                if (objFileUploadedHandler != null)
                {
                    // Raise the uploaded event
                    objFileUploadedHandler(this, EventArgs.Empty);
                }
            }
            else
            {
                base.FireEvent(objEvent);
            }
        }

        /// <summary>
        /// Provides a way to handle gateway requests.
        /// </summary>
        /// <param name="objHttpContext">The gateway request HTTP context.</param>
        /// <param name="strAction">The gateway request action.</param>
        /// <returns>
        /// By default this method returns a instance of a class which implements the IGatewayHandler and
        /// throws a non implemented HttpException.
        /// </returns>
        /// <remarks>
        /// This method is called from the implementation of IGatewayComponent which replaces the
        /// IGatewayControl interface. The IGatewayCompoenent is implemented by default in the
        /// RegisteredComponent class which is the base class of most of the Visual WebGui
        /// components.
        /// Referencing a RegisterComponent that overrides this method is done the same way that
        /// a control implementing IGatewayControl, which is by using the GatewayReference class.
        /// </remarks>
        protected override IGatewayHandler ProcessGatewayRequest(HostContext objHttpContext, string strAction)
        {
            if (strAction == "Content")
            {
                ProcessContentRequest(objHttpContext);
            }
            else if (strAction == "Upload")
            {
                ProcessUploadRequest(objHttpContext);
            }
            return null;
        }

        /// <summary>
        /// Processes the upload request.
        /// </summary>
        /// <param name="objHttpContext">The HTTP context.</param>
        private void ProcessUploadRequest(HostContext objHttpContext)
        {
            var guid = objHttpContext.Request.Form["PackageGuid"];
            var recd = IsRequestCompletelyReceived(objHttpContext);

            // Check upload is valid
            if ((!string.IsNullOrEmpty(objHttpContext.Request.Form["PackageGuid"]) && this.IsRequestCompletelyReceived(objHttpContext)))
            {
                // Get the file count
                int intFileCount = int.Parse(objHttpContext.Request.Form["FileCount"]);

                // Create a files list for collecting posted files
                List<ImageUploaderFile> objFiles = new List<ImageUploaderFile>();

                // Clear the previous files value
                this.RemoveValue<ImageUploaderFile[]>(Dropzone.FilesProperty);

                // Loop all files
                for (int intFileIndex = 1; intFileIndex <= intFileCount; intFileIndex++)
                {
                    objFiles.Add(new ImageUploaderFile(objHttpContext, intFileIndex));
                }

                // Set the files collection
                this.SetValue<ImageUploaderFile[]>(Dropzone.FilesProperty, objFiles.ToArray());
            }
        }

        /// <summary>
        /// Processes the content request.
        /// </summary>
        /// <param name="objHttpContext">The HTTP context.</param>
        private void ProcessContentRequest(HostContext objHttpContext)
        {
            objHttpContext.Response.ContentType = "text/html";
            XmlTextWriter objWriter = new XmlTextWriter(objHttpContext.Response.OutputStream, Encoding.UTF8);

            objWriter.Formatting = Formatting.Indented;
            objWriter.WriteStartElement("html");
            objWriter.WriteStartElement("head");
            objWriter.WriteElementString("title", "Dropzone");
            objWriter.WriteStartElement("script");
            objWriter.WriteAttributeString("language", "javascript");
            objWriter.WriteAttributeString("src", GetSkinResource("iuembed.js"));
            objWriter.WriteValue(" ");
            objWriter.WriteEndElement();
            objWriter.WriteStartElement("script");
            objWriter.WriteAttributeString("language", "javascript");
            objWriter.WriteAttributeString("src", GetSkinResource("iuintegration.js"));
            objWriter.WriteValue(" ");
            objWriter.WriteEndElement();
            objWriter.WriteStartElement("script");
            objWriter.WriteAttributeString("language", "javascript");
            objWriter.WriteValue(String.Format("var mstrControlId={0}; var mstrSessionID='{1}';", this.ID, this.Session.SessionID));
            objWriter.WriteEndElement();
            objWriter.WriteEndElement();
            objWriter.WriteStartElement("body");
            objWriter.WriteAttributeString("style", String.Format("margin:2px;background-color:#{0};", CommonUtils.GetWebColor(this.BackColor)));
            objWriter.WriteStartElement("script");
            objWriter.WriteAttributeString("language", "javascript");
            objWriter.WriteString(GetScript());
            objWriter.WriteEndElement();
            objWriter.WriteEndElement();
            objWriter.WriteEndElement();
            objWriter.Flush();
            objWriter.Close();
        }

        /// <summary>
        /// Gets the script.
        /// </summary>
        /// <returns></returns>
        private string GetScript()
        {
            StringBuilder objScript = new StringBuilder();
            objScript.AppendLine("var iu = new DropzoneWriter('Dropzone', '100%', '100%');");
            objScript.AppendLine("iu.activeXControlEnabled = true;");
            objScript.AppendLine("iu.javaAppletEnabled = true;");
            objScript.AppendLine(String.Format("iu.activeXControlCodeBase = '{0}';", this.GetSkinResource("Dropzone6.cab")));
            objScript.AppendLine(String.Format("iu.javaAppletJarFileName = '{0}';", this.GetSkinResource("Dropzone6.jar")));
            objScript.AppendLine(String.Format("iu.addParam('LicenseKey', '{0}');", this.GetSafeScriptString(this.LicenseKey)));
            objScript.AppendLine(String.Format("iu.addParam('PaneLayout', '{0}');", this.GetSafeScriptString(this.PaneLayout)));
            objScript.AppendLine(String.Format("iu.addParam('Action','{0}');", this.Action));
            objScript.AppendLine("iu.addEventListener('AfterUpload', 'OnUploadComplete');");
            // to solve the problem of losing the session of Java applet - for on IE browsers
            // see http://www.aurigma.com/docs/iu/PreservingSessionsandAuthenticationTicketsPassedinCookies.htm
            objScript.AppendLine("iu.addEventListener('BeforeUpload', 'OnBeforeUpload');");
            objScript.AppendLine(String.Format("iu.addParam('BackgroundColor', '#{0}');", CommonUtils.GetWebColor(this.BackColor)));
            objScript.AppendLine("iu.writeHtml();");
            return objScript.ToString();
        }

        /// <summary>
        /// Gets the safe script string.
        /// </summary>
        /// <param name="strString">The string.</param>
        /// <returns></returns>
        private string GetSafeScriptString(string strString)
        {
            if (!string.IsNullOrEmpty(strString))
            {
                return strString.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r");
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the skin resource.
        /// </summary>
        /// <param name="strResource">The resource.</param>
        /// <returns></returns>
        private string GetSkinResource(string strResource)
        {
            return this.Skin.GetResourcePath(strResource);
        }

        /// <summary>
        /// Determines whether is request completely received the specified HTTP context.
        /// </summary>
        /// <param name="objHttpContext">The HTTP context.</param>
        /// <returns>
        /// 	<c>true</c> if [is request completely received the specified HTTP context; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRequestCompletelyReceived(HostContext objHttpContext)
        {
            return objHttpContext.Request.Form["FileCount"] != null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        protected override string Source
        {
            get
            {
                // if skin defined, intercept and make use of html tamplate
                var skin = this.Skin as DropzoneSkin;
                if (skin != null)
                {
                    String src = (new SkinResourceHandle(typeof(DropzoneSkin), "dropzone.html")).ToString();    // 將 XonomyBoxSkin 中的 XonomyBox.html 轉換為 VWG 式 http url link

                    //String xmlFile = (new GeneralResourceHandle(this.XmlData)).ToString();                                      // 將 XonomyBox.XmlData 轉換為 VWG 式 http url link
                    //String docSpec = (new GeneralResourceHandle(this.DocSpec)).ToString();                                      // 將 XonomyBox.DocSpec 轉換為 VWG 式 http url link

                    //return String.Format("{0}?xml={1}&spec={2}", src, xmlFile, docSpec);
                    return src;
                }

                // call ProcessGatewayRequest
                return (new GatewayReference(this, "Content")).ToString();
            }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        private string Action
        {
            get
            {
                return new GatewayReference(this, "Upload").ToString();
            }
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <value>The files.</value>
        public ImageUploaderFile[] Files
        {
            get
            {
                return this.GetValue<ImageUploaderFile[]>(Dropzone.FilesProperty, new ImageUploaderFile[] { });
            }
        }

        /// <summary>
        /// Gets or sets the license key.
        /// </summary>
        /// <value>The license key.</value>
        [DefaultValue("")]
        public string LicenseKey
        {
            get
            {
                return this.GetValue<string>(Dropzone.LicenseKeyProperty, string.Empty);
            }
            set
            {
                if (this.LicenseKey != value)
                {
                    this.SetValue<string>(Dropzone.LicenseKeyProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Gets or sets the pane layout.
        /// </summary>
        /// <value>The pane layout.</value>
        [DefaultValue("ThreePanes")]
        public string PaneLayout
        {
            get
            {
                return this.GetValue<string>(Dropzone.PaneLayoutProperty, "ThreePanes");
            }
            set
            {
                if (this.PaneLayout != value)
                {
                    this.SetValue<string>(Dropzone.PaneLayoutProperty, value);
                    this.Update();
                }
            }
        }

        #endregion
    }
}