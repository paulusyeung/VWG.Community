#region Using

using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

using Gizmox.WebGUI;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Client;
using Gizmox.WebGUI.Forms.Skins;
using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Forms.Design;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Extensibility;

using VWG.Community.Forms.Skins;
using VWG.Community.Forms.Helper;

#endregion

namespace VWG.Community.Forms
{
    /// <summary>
    /// Summary description for UploadBox
    /// 
    /// jQuery File Upload Plugin 5.40.1
    /// https://github.com/blueimp/jQuery-File-Upload
    /// http://blueimp.github.io/jQuery-File-Upload/
    /// 
    /// Copyright 2010, Sebastian Tschan
    /// https://blueimp.net
    /// 
    /// Licensed under the MIT license:
    /// http://www.opensource.org/licenses/MIT
    /// </summary>
    
#if WG_NET45 || WG_NET451 || WG_NET452 || WG_NET46
    [ToolboxBitmapAttribute(typeof(UploadBox), "Upload.UploadBox.png")]
#else
    [ToolboxBitmapAttribute(typeof(UploadBox), "Upload.UploadBox.bmp")]
#endif
#if WG_NET46
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.6.5701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.6.5701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
#elif WG_NET452
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.25701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.25701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
#elif WG_NET451
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.15701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.15701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
#elif WG_NET45
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.5.5701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.5.5701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
#elif WG_NET40
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=4.0.5701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=4.0.5701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
#elif WG_NET35
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=3.0.5701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=3.0.5701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
#else
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=2.0.5701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=2.0.5701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
#endif
    
    [ToolboxItem(true)]
    [Serializable()]
    [MetadataTag("UploadBox")]
    [Skin(typeof(UploadBoxSkin))]
    public partial class UploadBox : Control, IRequiresRegistration
    {
        #region "C'tor"
        /// <summary>
        /// Constructor for UploadBox
        /// </summary>
        public UploadBox()
        {
            
        }
        #endregion

        #region "Properties"
        #region "Serializable property definitions"
        private static readonly SerializableProperty UploadMaxNumberOfFilesProperty = SerializableProperty.Register("UploadMaxNumberOfFiles", typeof(int), typeof(UploadBox));
        private static readonly SerializableProperty UploadMaxFileSizeProperty = SerializableProperty.Register("UploadMaxFileSize", typeof(long), typeof(UploadBox));
        private static readonly SerializableProperty UploadMinFileSizeProperty = SerializableProperty.Register("UploadMinFileSize", typeof(long), typeof(UploadBox));
        private static readonly SerializableProperty UploadFileTypesProperty = SerializableProperty.Register("UploadFileTypes", typeof(string), typeof(UploadBox));
        private static readonly SerializableProperty UploadTempFilePathProperty = SerializableProperty.Register("UploadTempFilePath", typeof(string), typeof(UploadBox));
        private static readonly SerializableProperty UploadBufferSizeProperty = SerializableProperty.Register("UploadBufferSize", typeof(int), typeof(UploadBox));
        private static readonly SerializableProperty UploadClientChunkSizeProperty = SerializableProperty.Register("UploadClientChunkSize", typeof(int), typeof(UploadBox));
        private static readonly SerializableProperty UploadTextProperty = SerializableProperty.Register("UploadText", typeof(string), typeof(UploadBox));
        private static readonly SerializableProperty UploadShowFilenameOnBarProperty = SerializableProperty.Register("UploadShowFilenameOnBar", typeof(bool), typeof(UploadBox));
        private static readonly SerializableProperty UploadShowSpeedOnBarProperty = SerializableProperty.Register("UploadShowSpeedOnBar", typeof(bool), typeof(UploadBox));
        
        //! 2020.10.22 paulus: serialize 增加嘅 properties
        private static readonly SerializableProperty BackColorProperty = SerializableProperty.Register("BackColor", typeof(String), typeof(UploadBox));
        private static readonly SerializableProperty ForeColorProperty = SerializableProperty.Register("ForeColor", typeof(String), typeof(UploadBox));
        #endregion

        /// <summary>
        /// Maximum number of files that can be uploaded in each multiselection. 0 = Unlimited.
        /// </summary>
        [Category("UploadBox")]
        [Description("Maximum number of files that can be uploaded in each multiselection. 0 = Unlimited")]
        [DefaultValue(0)]
        public int UploadMaxNumberOfFiles
        {
            get
            {
                return this.GetValue<int>(UploadBox.UploadMaxNumberOfFilesProperty, 0);
            }
            set
            {
                if (this.UploadMaxNumberOfFiles != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException("UploadMaxNumberOfFiles", string.Format(SR.GetString("UploadBoxErrorMinSize"), value));
                    }
                    this.SetValue<int>(UploadBox.UploadMaxNumberOfFilesProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Maximum size of upload file in bytes. 0 = Unlimited
        /// </summary>
        [Category("UploadBox")]
        [Description("Maximum size of upload file in bytes. 0 = Unlimited")]
        [DefaultValue(0)]
        public long UploadMaxFileSize
        {
            get
            {
                return this.GetValue<long>(UploadBox.UploadMaxFileSizeProperty, 0);
            }
            set
            {
                if (this.UploadMaxFileSize != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException("UploadMaxFileSize", string.Format(SR.GetString("UploadBoxErrorMinSize"), value));
                    }
                    this.SetValue<long>(UploadBox.UploadMaxFileSizeProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Minimum size of upload file in bytes. 0 = Unlimited
        /// </summary>
        [Category("UploadBox")]
        [Description("Minimum size of upload file in bytes. 0 = Unlimited")]
        [DefaultValue(0)]
        public long UploadMinFileSize
        {
            get
            {
                return this.GetValue<long>(UploadBox.UploadMinFileSizeProperty, 0);
            }
            set
            {
                if (this.UploadMinFileSize != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException("UploadMinFileSize", string.Format(SR.GetString("UploadBoxErrorMinSize"), value));
                    }
                    this.SetValue<long>(UploadBox.UploadMinFileSizeProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Regular expression match string for valid filename and extension
        /// Default: "^.*$", means all files. 
        /// Example: "^.*\.(gif|jpe?g|png)$", means *.gif, *.jpg, *.jpeg, *.png
        /// </summary>
        [Category("UploadBox")]
        [Description("Regular expression match string for valid filename and extension")]
        [DefaultValue("^.*$")]
        public string UploadFileTypes
        {
            get
            {
                return this.GetValue<string>(UploadBox.UploadFileTypesProperty, "^.*$");
            }
            set
            {
                if (this.UploadFileTypes != value)
                {
                    this.SetValue<string>(UploadBox.UploadFileTypesProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Path to store temporary files while being uploaded
        /// Files on this path are not deleted by this control and must be cleaned up by the user's code
        /// </summary>
        [Category("UploadBox")]
        [Description("Path to store temporary files while being uploaded")]
        [DefaultValue("")]
        public string UploadTempFilePath
        {
            get
            {
                string strPath = this.GetValue<string>(UploadBox.UploadTempFilePathProperty, "");
                if (string.IsNullOrEmpty(strPath))
                    strPath = System.IO.Path.GetTempPath();
                return strPath;
            }
            set
            {
                if (this.UploadTempFilePath != value)
                {
                    this.SetValue<string>(UploadBox.UploadTempFilePathProperty, value);
                }
            }
        }

        /// <summary>
        /// Buffer size in bytes used by server-side to process uploaded chunks of uploaded files 
        /// </summary>
        [Category("UploadBox")]
        [Description("Buffer size in bytes used by server-side to process uploaded chunks of uploaded files")]
        [DefaultValue(3072000)]
        public int UploadBufferSize
        {
            get
            {
                int intBuffer = this.GetValue<int>(UploadBox.UploadBufferSizeProperty, 3072000);
                if (intBuffer <= 0)
                    intBuffer = 3000 * 1024;
                return intBuffer;
            }
            set
            {
                int intBuffer = value;
                if (intBuffer <= 0)
                    intBuffer = 3000 * 1024;
                if (this.UploadBufferSize != intBuffer)
                {
                    this.SetValue<int>(UploadBox.UploadBufferSizeProperty, intBuffer);
                }
            }
        }

        /// <summary>
        /// Size of each chunk the jQuery client uses to upload
        /// When increasing client chunk size, make sure to check and/or adjust your web.config's values for:
        ///   - system.web/httpRuntime/maxRequestLength
        ///   - system.web/httpRuntime/executionTimeout
        ///   - system.webServer/security/requestFiltering/requestLimit/maxAllowedContentLength
        /// </summary>
        [Category("UploadBox")]
        [Description("Size of each chunk the jQuery client uses to upload")]
        [DefaultValue(3072000)]
        public int UploadClientChunkSize
        {
            get
            {
                int intBuffer = this.GetValue<int>(UploadBox.UploadClientChunkSizeProperty, 3072000);
                if (intBuffer <= 0)
                    intBuffer = 3000 * 1024;
                return intBuffer;
            }
            set
            {
                int intBuffer = value;
                if (intBuffer <= 0)
                    intBuffer = 3000 * 1024;
                if (this.UploadClientChunkSize != intBuffer)
                {
                    this.SetValue<int>(UploadBox.UploadClientChunkSizeProperty, intBuffer);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Text used to prompt for files on upload control
        /// </summary>
        [Category("UploadBox")]
        [Description("Text used to prompt for files on upload control")]
        [DefaultValue("select or drop files here")]
        public string UploadText
        {
            get
            {
                return this.GetValue<string>(UploadBox.UploadTextProperty, "select or drop files here");
            }
            set
            {
                if (this.UploadText != value)
                {
                    this.SetValue<string>(UploadBox.UploadTextProperty, value);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Show name of file currently being uploaded on progressbar in UploadBox
        /// </summary>
        [Category("UploadBox")]
        [Description("Show name of file currently being uploaded on progressbar in UploadBox")]
        [DefaultValue(true)]
        public bool UploadShowFilenameOnBar
        {
            get
            {
                return this.GetValue<bool>(UploadBox.UploadShowFilenameOnBarProperty, true);
            }
            set
            {
                if (this.UploadShowFilenameOnBar != value)
                {
                    this.SetValue<bool>(UploadBox.UploadShowFilenameOnBarProperty, value, true);
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Show current upload speed in Kilo bits per second on progressbar in UploadBox
        /// </summary>
        [Category("UploadBox")]
        [Description("Show current upload speed in Kilo bits per second on progressbar in UploadBox")]
        [DefaultValue(true)]
        public bool UploadShowSpeedOnBar
        {
            get
            {
                return this.GetValue<bool>(UploadBox.UploadShowSpeedOnBarProperty, true);
            }
            set
            {
                if (this.UploadShowSpeedOnBar != value)
                {
                    this.SetValue<bool>(UploadBox.UploadShowSpeedOnBarProperty, value, true);
                    this.Update();
                }
            }
        }

        #region 2020.10.20 paulus: 增加 public properties
        [Category("UploadBox")]
        [Description("Background Color of UploadBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color BackColor
        {
            get
            {
                var color = this.GetValue<String>(UploadBox.BackColorProperty, "transparent");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(UploadBox.BackColorProperty, ColorHelper.SafeColorName(color), "transparent");
                    this.Update();
                }
            }
        }

        [Category("UploadBox")]
        [Description("Text Color of UploadBox. Use Color Names only.")]
        [DefaultValue(0)]
        public override Color ForeColor
        {
            get
            {
                var color = this.GetValue<String>(UploadBox.ForeColorProperty, "black");
                return Color.FromName(color);
            }
            set
            {
                if (this.BackColor != value)
                {
                    var color = (Color)value;
                    this.SetValue<String>(UploadBox.ForeColorProperty, ColorHelper.SafeColorName(color), "black");
                    this.Update();
                }
            }
        }
        #endregion

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

        #region "Gateway upload handler"

        /// <summary>
        /// Internal temporary full name of file being uploaded in chunks.
        /// </summary>
        private string mstrChunkedFileName;

        /// <summary>
        /// Detecting if upload chunk gateway request is the first chunk of a file being uploaded
        /// </summary>
        /// <param name="strRange">Pattern used by jQuery upload control: "bytes 0-116649/116650"</param>
        /// <returns></returns>
        private bool isFirstRange(string strRange)
        {
            return strRange.StartsWith("bytes 0-");
        }

        /// <summary>
        /// Detecting if upload chunk gateway request is the last chunk of a file being uploaded
        /// </summary>
        /// <param name="strRange">Pattern range used by upload control: "bytes 100000-116649/116650"</param>
        /// <returns></returns>
        private bool isLastRange(string strRange)
        {
            long lngEnd = long.Parse(strRange.Substring(strRange.IndexOf("-") + 1, strRange.IndexOf("/") - strRange.IndexOf("-") - 1));
            long lngFull = long.Parse(strRange.Substring(strRange.IndexOf("/") + 1));
            return ((lngEnd + 1) >= lngFull);
        }

        /// <summary>
        /// Process Ajax gateway upload request posted by the jQuery upload client component
        /// </summary>
        /// <param name="objHostContext"></param>
        /// <param name="strAction">The only supported action is "ULPost", posted by the jQuery upload control as Ajax gateway request</param>
        /// <returns></returns>
        protected override IGatewayHandler ProcessGatewayRequest(Gizmox.WebGUI.Hosting.HostContext objHostContext, string strAction)
        {
            if (strAction == WGAttributes.UploadControlPost)
            {
                // The response will be plain text.
                objHostContext.Response.ContentType = "text/plain";

                try
                {
                    // The byte range of current chunk being posted/uploaded
                    string strContentRange = (string)objHostContext.Request.Headers["Content-Range"];

                    // Current implementation as chunked uploads only support one file
                    if ((strContentRange != null) && (objHostContext.Request.Files.Count > 1))
                        throw new NotSupportedException("Chunked file uploads only supported for one file at a time");

                    // Prepare for list of uploaded file results - since chunked uploads are being used, there will be only one file in list
                    List<UploadFileResult> objUploadResults = new List<UploadFileResult>();

                    // Prepare serializer for json serialization of response to Ajax request
                    JavaScriptSerializer objJS = new JavaScriptSerializer();

                    // Get the posted file handle. Only one file, since we are using chunked uploads
                    HttpPostedFile objPostedFile = objHostContext.HttpContext.Request.Files[0] as HttpPostedFile;
                    string strUploadedFileName = string.Empty;

                    // Get filename without path
                    string[] files = objPostedFile.FileName.Split(new char[] { '\\' });
                    strUploadedFileName = files[files.Length - 1];

                    #region On first chunk of a new file, build temporary filename
                    if (strContentRange != null)
                    {
                        if (this.isFirstRange(strContentRange))
                            mstrChunkedFileName = System.IO.Path.Combine(this.UploadTempFilePath, Guid.NewGuid().ToString() + "_" + strUploadedFileName);
                        if (mstrChunkedFileName == null)
                            throw new NotSupportedException("Chunked file upload missing initial chunk");

                    }
                    #endregion

                    string savedFileName;
                    if (strContentRange == null)
                    {
                        #region If no range, file uploaded in one chunk, so save it
                        savedFileName = System.IO.Path.Combine(this.UploadTempFilePath, Guid.NewGuid().ToString() + "_" + strUploadedFileName);
                        objPostedFile.SaveAs(savedFileName);
                        #endregion
                    }
                    else
                    {
                        #region If range, save chunk. Assume sequential chunks
                        savedFileName = mstrChunkedFileName;
                        using (System.IO.FileStream fs = new System.IO.FileStream(savedFileName, System.IO.FileMode.Append, System.IO.FileAccess.Write))
                        {
                            byte[] buffer = new byte[UploadBufferSize];

                            long l = objPostedFile.InputStream.Read(buffer, 0, UploadBufferSize);
                            while (l > 0)
                            {
                                fs.Write(buffer, 0, (int)l);
                                l = objPostedFile.InputStream.Read(buffer, 0, UploadBufferSize);
                            }
                            fs.Flush();
                            fs.Close();
                        }
                        #endregion
                    }

                    // Prepare results for uploaded file request
                    UploadFileResult objResult = new UploadFileResult(strUploadedFileName, objPostedFile.ContentLength, objPostedFile.ContentType, savedFileName);
                    objUploadResults.Add(objResult);

                    #region Fire chunk completed event
                    if (this.UploadChunkRequestCompletedHandler != null)
                    {
                        UploadChunkRequestResult objChunkResult = new UploadChunkRequestResult(
                            strUploadedFileName,
                            (long)objPostedFile.ContentLength,
                            objPostedFile.ContentType,
                            savedFileName,
                            strContentRange);
                        this.UploadChunkRequestCompletedHandler(this, new UploadChunkRequestEventArgs(objChunkResult));
                    }
                    #endregion

                    // Prepare response as json serialized results object
                    object jsonObj = objJS.Serialize(objUploadResults.ToArray());
                    objHostContext.Response.Write(jsonObj.ToString());
                    return null;
                }

                catch (Exception ex)
                {
                    #region Unhandled error detected. Throw exception, or call error handler
                    if (this.UploadErrorHandler != null)
                    {
                        this.UploadErrorHandler(this, new UploadErrorEventArgs(ex.Message, ex));
                    }
                    else
                        throw;
                    return null;
                    #endregion
                }
            }

            // Call base, if action doesn't belong here.
            return base.ProcessGatewayRequest(objHostContext, strAction);

        }

        /// <summary>
        /// Simle error checking 
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="objFile"></param>
        /// <returns></returns>
        private string UploadValidate(string strFileName, HttpPostedFile objFile)
        {
            if (objFile.ContentLength == 0)
                return Gizmox.WebGUI.Forms.SR.GetString("UploadBoxErrorContentMissing");
            if (this.UploadMinFileSize > 0 && (long)objFile.ContentLength < this.UploadMinFileSize)
                return string.Format(Gizmox.WebGUI.Forms.SR.GetString("UploadBoxErrorMinSize"), strFileName);
            if (this.UploadMaxFileSize > 0 && (long)objFile.ContentLength > this.UploadMaxFileSize)
                return string.Format(Gizmox.WebGUI.Forms.SR.GetString("UploadBoxErrorMaxSize"), strFileName);

            if (!string.IsNullOrEmpty(this.UploadFileTypes))
            {
                Regex objRegEx = new Regex(this.UploadFileTypes);
                if (!objRegEx.IsMatch(System.IO.Path.GetExtension(strFileName)))
                    return string.Format(Gizmox.WebGUI.Forms.SR.GetString("UploadBoxFileNotAllowed"), strFileName);
            }

            return string.Empty;
        }
        #endregion

        #region "Rendering
        /// <summary>
        /// Render attributes for UploadBox
        /// </summary>
        /// <param name="context"></param>
        /// <param name="writer"></param>
        protected override void RenderAttributes(IContext context, IAttributeWriter writer)
        {
            //! 2020.10.22 paulus: WGAttributes 屬於 WebGUI Core，唔識增加 WGAttributes.UploadBox???，借用現有嘅 WGAttributes.UploadControl???
            writer.WriteAttributeString(WGAttributes.UploadControlMaxNumberOfFiles, this.UploadMaxNumberOfFiles.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString(WGAttributes.UploadControlMaxFileSize, this.UploadMaxFileSize.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString(WGAttributes.UploadControlMinFileSize, this.UploadMinFileSize.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString(WGAttributes.UploadControlFileTypes, this.UploadFileTypes);
            writer.WriteAttributeString(WGAttributes.UploadControlPost, (new GatewayReference(this, WGAttributes.UploadControlPost)).ToString());
            writer.WriteAttributeString(WGAttributes.UploadControlText, this.UploadText);
            writer.WriteAttributeString(WGAttributes.UploadControlClientChunkSize, this.UploadClientChunkSize.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString(WGAttributes.UploadControlShowFilenameOnBar, (this.UploadShowFilenameOnBar ? "1" : "0"));
            writer.WriteAttributeString(WGAttributes.UploadControlShowSpeedOnBar, (this.UploadShowSpeedOnBar ? "1" : "0"));
            writer.WriteAttributeString(WGAttributes.UploadControlEnabled, (this.Enabled ? "1" : "0"));

            base.RenderAttributes(context, writer);
        }
        #endregion

        #region "Event handling/firing"
        /// <summary>
        /// Handle and process events raised by the jQuery upload control
        /// </summary>
        /// <param name="objEvent"></param>
        protected override void FireEvent(IEvent objEvent)
        {
            if (objEvent.Type == "Fail")
            {
                #region Failed upload request
                string error = objEvent["error"].ToString();
                string res = string.Format(SR.GetString("UploadControlError" + error), objEvent["result"].ToString());
                if (this.UploadErrorHandler != null)
                    this.UploadErrorHandler(this, new UploadErrorEventArgs(res, null));
                else
                    throw new HttpException(res);
                #endregion
            }
            else if (objEvent.Type == "Done")
            {
                #region Completed upload request
                long lngSize = 0;
                if (!long.TryParse(objEvent["Size"], out lngSize))
                    lngSize = 0;
                UploadFileResult objResult = new UploadFileResult(
                    objEvent["Name"].ToString(),
                    lngSize,
                    objEvent["Type"].ToString(),
                    objEvent["TempName"]);
                if (this.UploadFileCompletedHandler != null)
                    this.UploadFileCompletedHandler(this, new UploadCompletedEventArgs(objResult));
                #endregion
            }
            else if (objEvent.Type == "Start")
            {
                #region One or more files have been added to jQuery control, upload will now start
                if (this.UploadBatchStartingHandler != null)
                    this.UploadBatchStartingHandler(this, EventArgs.Empty);
                #endregion
            }
            else if (objEvent.Type == "Stop")
            {
                #region Finished uploading batch of files
                if (this.UploadBatchCompletedHandler != null)
                    this.UploadBatchCompletedHandler(this, EventArgs.Empty);
                #endregion
            }
            else
            {
                base.FireEvent(objEvent);
            }
        }

        /// <summary>
        /// Gets the critical events.
        /// </summary>
        /// <returns></returns>
        protected override CriticalEventsData GetCriticalEventsData()
        {
            CriticalEventsData objEvents = base.GetCriticalEventsData();

            //! 2020.10.22 paulus: WGEvents 屬於 WebGUI Core，唔識增加 WGEvents.UploadBox???，借用現有嘅 WGEvents.UploadControl???
            if (this.UploadErrorHandler != null) objEvents.Set(WGEvents.UploadControlErrorHandler);
            if (this.UploadFileCompletedHandler != null) objEvents.Set(WGEvents.UploadControlFileCompletedHandler);
            if (this.UploadBatchStartingHandler != null) objEvents.Set(WGEvents.UploadControlBatchStartingHandler);
            if (this.UploadBatchCompletedHandler != null) objEvents.Set(WGEvents.UploadControlBatchCompletedHandler);

            return objEvents;
        }

        /// <summary>
        /// Gets the critical client events.
        /// </summary>
        /// <returns></returns>
        protected override CriticalEventsData GetCriticalClientEventsData()
        {
            CriticalEventsData objEvents = base.GetCriticalClientEventsData();

            //! 2020.10.22 paulus: WGEvents 屬於 WebGUI Core，唔識增加 WGEvents.UploadBox???，借用現有嘅 WGEvents.UploadControl???
            if (this.HasClientHandler("UploadErrorHandler")) objEvents.Set(WGEvents.UploadControlErrorHandler);
            if (this.HasClientHandler("UploadFileCompletedHandler")) objEvents.Set(WGEvents.UploadControlFileCompletedHandler);
            if (this.HasClientHandler("UploadBatchStartingHandler")) objEvents.Set(WGEvents.UploadControlBatchStartingHandler);
            if (this.HasClientHandler("UploadBatchCompletedHandler")) objEvents.Set(WGEvents.UploadControlBatchCompletedHandler);

            return objEvents;
        }

        #endregion

        #region "Events"

        #region "Serializable definitions"
        /// <summary>
        /// File has been added to the upload control, uploads will now start
        /// </summary>
        private static readonly SerializableEvent UploadBatchStartingEvent = SerializableEvent.Register("UploadBatchStarting", typeof(UploadEventHandler), typeof(UploadBox));
        /// <summary>
        /// The gateway file complete event registration
        /// </summary>
        private static readonly SerializableEvent UploadChunkRequestCompletedEvent = SerializableEvent.Register("UploadChunkRequestCompletedEvent", typeof(UploadChunkRequestCompletedHandler), typeof(UploadBox));
        /// <summary>
        /// The upload file complete event registration
        /// </summary>
        private static readonly SerializableEvent UploadFileCompletedEvent = SerializableEvent.Register("UploadFileCompletedEvent", typeof(UploadFileCompletedHandler), typeof(UploadBox));
        /// <summary>
        /// The upload batch complete event registration
        /// </summary>
        private static readonly SerializableEvent UploadBatchCompletedEvent = SerializableEvent.Register("UploadBatchCompleted", typeof(UploadEventHandler), typeof(UploadBox));
        /// <summary>
        /// The unhandled error event registration
        /// </summary>
        private static readonly SerializableEvent UploadErrorEvent = SerializableEvent.Register("UploadError", typeof(UploadErrorHandler), typeof(UploadBox));
        #endregion

        /// <summary>
        /// Fires when an upload of a single file has completed. Fired once for every uploaded file.
        /// EventArgs have information on the file and the temprary storage of the file.
        /// The file is stored on the temporary folder, possibly using name different from the original name.
        /// </summary>
        [Category("UploadBox")]
        [Description("Fires when an upload of a single file has completed. Fired once for every uploaded file.")]
        public event UploadFileCompletedHandler UploadFileCompleted
        {
            add
            {
                this.AddCriticalHandler(UploadFileCompletedEvent, value);
            }
            remove
            {
                this.RemoveCriticalHandler(UploadFileCompletedEvent, value);
            }
        }

        /// <summary>
        /// Get the handler for UploadFileCompleted
        /// </summary>
        private UploadFileCompletedHandler UploadFileCompletedHandler
        {
            get { return (UploadFileCompletedHandler)this.GetHandler(UploadFileCompletedEvent); }
        }


        /// <summary>
        /// Event fired when a file has been added to the jQuery control on the client, and upload is about to start
        /// </summary>
        [Category("UploadBox")]
        [Description("Event fired when a file has been added to the jQuery control on the client, and upload is about to start")]
        public event UploadEventHandler UploadBatchStarting
        {
            add
            {
                this.AddCriticalHandler(UploadBatchStartingEvent, value);
            }
            remove
            {
                this.RemoveCriticalHandler(UploadBatchStartingEvent, value);
            }
        }

        /// <summary>
        /// Get the handler for UploadStart
        /// </summary>
        private UploadEventHandler UploadBatchStartingHandler
        {
            get { return (UploadEventHandler)this.GetHandler(UploadBatchStartingEvent); }
        }

        /// <summary>
        /// Event fired at the end of each batch of uploaded files, when the batch completes.
        /// When multiselecting files, UploadFileCompleted will fire for each individual file, followed by one UploadBatchCompleted.
        /// </summary>
        [Category("UploadBox")]
        [Description("Event fired at the end of each batch of uploaded files, when the batch completes.")]
        public event UploadEventHandler UploadBatchCompleted
        {
            add
            {
                this.AddCriticalHandler(UploadBatchCompletedEvent, value);
            }
            remove
            {
                this.RemoveCriticalHandler(UploadBatchCompletedEvent, value);
            }
        }

        /// <summary>
        /// Get the handler for UploadBatchCompleted
        /// </summary>
        private UploadEventHandler UploadBatchCompletedHandler
        {
            get { return (UploadEventHandler)this.GetHandler(UploadBatchCompletedEvent); }
        }

        /// <summary>
        /// Event fired upon detecting an unhandled Exception
        /// </summary>
        [Category("UploadBox")]
        [Description("Event fired upon detecting an unhandled Exception")]
        public event UploadErrorHandler UploadError
        {
            add
            {
                this.AddCriticalHandler(UploadErrorEvent, value);
            }
            remove
            {
                this.RemoveCriticalHandler(UploadErrorEvent, value);
            }
        }

        /// <summary>
        /// Get the handler for UploadError
        /// </summary>
        private UploadErrorHandler UploadErrorHandler
        {
            get { return (UploadErrorHandler)this.GetHandler(UploadErrorEvent); }
        }

        /// <summary>
        /// Fired when every chunk of an uploaded file has been received by the gateway. Fired once for each chunk of every uploaded file.
        /// </summary>
        [Category("UploadBox")]
        [Description("Fires when upload completes on a group of files added in same action")]
        public event UploadChunkRequestCompletedHandler UploadChunkRequestCompleted
        {
            add
            {
                this.AddCriticalHandler(UploadChunkRequestCompletedEvent, value);
            }
            remove
            {
                this.RemoveCriticalHandler(UploadChunkRequestCompletedEvent, value);
            }
        }

        /// <summary>
        /// Get the handler for UploadFileRequestCompleted
        /// </summary>
        private UploadChunkRequestCompletedHandler UploadChunkRequestCompletedHandler
        {
            get { return (UploadChunkRequestCompletedHandler)this.GetHandler(UploadChunkRequestCompletedEvent); }
        }

        #region "Client events"
        /// <summary>
        /// Fires when an upload of a single file has completed. Fired once for every uploaded file.
        /// EventArgs have a list type for uploaded files, which will contain only one uploaded file.
        /// The error property of the file's results indicates the error if not empty.
        /// </summary>
        [Category("UploadBox")]
        [Description("Fires when an upload of a single file has completed. Fired once for every uploaded file.")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event ClientEventHandler ClientUploadFileCompleted
        {
            add
            {
                this.AddClientHandler("UploadFileCompleted", value);
            }
            remove
            {
                this.RemoveClientHandler("UploadFileCompleted", value);
            }
        }

        /// <summary>
        /// Event fired when a file has been added to the jQuery control on the client, and upload is about to start
        /// </summary>
        [Category("UploadBox")]
        [Description("Event fired when a file has been added to the jQuery control on the client, and upload is about to start")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event ClientEventHandler ClientUploadBatchStarting
        {
            add
            {
                this.AddClientHandler("UploadBatchStarting", value);
            }
            remove
            {
                this.RemoveClientHandler("UploadBatchStarting", value);
            }
        }

        /// <summary>
        /// Event fired at the end of each batch of uploaded files, when the batch completes.
        /// When multiselecting files, UploadFileCompleted will fire for each individual file, followed by one UploadBatchCompleted.
        /// </summary>
        [Category("UploadBox")]
        [Description("Event fired at the end of each batch of uploaded files, when the batch completes.")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event ClientEventHandler ClientUploadBatchCompleted
        {
            add
            {
                this.AddClientHandler("UploadBatchCompleted", value);
            }
            remove
            {
                this.RemoveClientHandler("UploadBatchCompleted", value);
            }
        }

        /// <summary>
        /// Event fired upon detecting an unhandled Exception
        /// </summary>
        [Category("UploadBox")]
        [Description("Event fired upon detecting an unhandled Exception")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event ClientEventHandler ClientUploadError
        {
            add
            {
                this.AddClientHandler("UploadError", value);
            }
            remove
            {
                this.RemoveClientHandler("UploadError", value);
            }
        }

        /// <summary>
        /// Fired when every chunk of an uploaded file has been received by the gateway. Fired once for each chunk of every uploaded file.
        /// </summary>
        [Category("UploadBox")]
        [Description("Fires when upload completes on a group of files added in same action")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event ClientEventHandler ClientUploadChunkRequestCompleted
        {
            add
            {
                this.AddClientHandler("UploadChunkRequestCompleted", value);
            }
            remove
            {
                this.RemoveClientHandler("UploadChunkRequestCompleted", value);
            }
        }

        #endregion
    }
    #endregion

    #region "Delegates and EventArgs"

    /// <summary>
    /// Represents chunk uploaded event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void UploadChunkRequestCompletedHandler(object sender, UploadChunkRequestEventArgs e);

    /// <summary>
    /// Represents file uploaded event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void UploadFileCompletedHandler(object sender, UploadCompletedEventArgs e);

    /// <summary>
    /// Represents error event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void UploadErrorHandler(object sender, UploadErrorEventArgs e);

    /// <summary>
    /// Represents generic upload events with no arguments
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void UploadEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Base EventArg information type for UploadBox
    /// </summary>
    [Serializable()]
    public class UploadFileResult
    {
        #region "privates"
        private string mstrName;
        private long mlngSize;
        private string mstrType;
        private string mstrTempFileFullName;
        #endregion

        #region "C'tor"
        /// <summary>
        /// Parameterized constructor for UploadFileResult
        /// </summary>
        /// <param name="strName">The filename without path</param>
        /// <param name="lngSize">Size of file in bytes</param>
        /// <param name="strType">MIME type of file</param>
        /// <param name="strTempFileFullName">Full path name of temporary file (where file is uploaded by the control)</param>
        public UploadFileResult(string strName, long lngSize, string strType, string strTempFileFullName)
        {
            mstrName = strName;
            mlngSize = lngSize;
            mstrType = strType;
            mstrTempFileFullName = strTempFileFullName;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UploadFileResult()
        {
        }
        #endregion
        /// <summary>
        /// Name of uploaded file as reported by the jQuery upload control
        /// </summary>
        public string Name
        {
            get { return mstrName; }
            set { mstrName = value; }
        }

        /// <summary>
        /// The size of the file in bytes
        /// </summary>
        public long Size
        {
            get { return mlngSize; }
            set { mlngSize = value; }
        }

        /// <summary>
        /// The MIME content type for the uploaded file
        /// </summary>
        public string Type
        {
            get { return mstrType; }
            set { mstrType = value; }
        }

        /// <summary>
        /// The fully qualified name of the uploaded file on temporary storage, saved by the uploade control. 
        /// This name does not reflect the original filename, since GUID is used upon conflict.
        /// </summary>
        public string TempFileFullName
        {
            get { return mstrTempFileFullName; }
            set { mstrTempFileFullName = value; }
        }
    }

    /// <summary>
    /// EventArgs for end of gateway/Ajax request of a chunk or a one chunk file upload
    /// </summary>
    [Serializable()]
    public class UploadChunkRequestResult : UploadFileResult
    {
        #region "Privtes"
        private string mstrRange;
        #endregion

        #region "C'tor"
        /// <summary>
        /// Parameterized constructor for UploadFileResult
        /// </summary>
        /// <param name="strName">The filename without path</param>
        /// <param name="lngSize">Size of file in bytes</param>
        /// <param name="strType">MIME type of file</param>
        /// <param name="strTempFileFullName">Full path name of temporary file (where file is uploaded by the control)</param>
        public UploadChunkRequestResult(string strName, long lngSize, string strType, string strTempFileFullName, string strRange)
            : base(strName, lngSize, strType, strTempFileFullName)
        {
            mstrRange = strRange;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UploadChunkRequestResult()
        {
        }
        #endregion

        /// <summary>
        /// The contained range, for chunked upload. For files uploaded in one chunk, this will be null
        /// </summary>
        public string Range
        {
            get { return mstrRange; }
            set { mstrRange = value; }
        }
    }

    /// <summary>
    /// EventArgs for single gateway/Ajax request
    /// </summary>
    [Serializable()]
    public class UploadChunkRequestEventArgs : EventArgs
    {
        private UploadChunkRequestResult mobjResult;

        /// <summary>
        /// Initializes the arguments with UploadFileResults
        /// </summary>
        /// <param name="objResult"></param>
        public UploadChunkRequestEventArgs(UploadChunkRequestResult objResult)
        {
            mobjResult = objResult;
        }

        /// <summary>
        /// The results of a single uploaded file
        /// </summary>
        public UploadChunkRequestResult Result
        {
            get { return mobjResult; }
        }
    }

    /// <summary>
    /// EventArgs for completed upload of one uploaded files
    /// </summary>
    [Serializable()]
    public class UploadCompletedEventArgs : EventArgs
    {
        private UploadFileResult mobjResult;

        /// <summary>
        /// Initializes the arguments with the results for the uploaded file
        /// </summary>
        /// <param name="objList"></param>
        public UploadCompletedEventArgs(UploadFileResult objResult)
        {
            mobjResult = objResult;
        }

        /// <summary>
        /// The uploaded file's upload results
        /// </summary>
        public UploadFileResult Result
        {
            get { return mobjResult; }
        }
    }

    /// <summary>
    /// EventArgs for unhandled exceptions
    /// </summary>
    [Serializable()]
    public class UploadErrorEventArgs : EventArgs
    {
        private string mstrError;
        private Exception mobjException;

        /// <summary>
        /// Initializes the arguments with information on the error
        /// </summary>
        /// <param name="strError"></param>
        /// <param name="objException"></param>
        public UploadErrorEventArgs(string strError, Exception objException)
        {
            mstrError = strError;
            mobjException = objException;
        }

        /// <summary>
        /// The error message
        /// </summary>
        public string Error
        {
            get { return this.mstrError; }
        }

        /// <summary>
        /// The error exception
        /// </summary>
        public Exception ErrorException
        {
            get { return mobjException; }
        }
    }
    #endregion

}