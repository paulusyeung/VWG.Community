using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web;

using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Hosting;

namespace VWG.Community.Resources
{
    /// <summary>
    /// Static gateway that will serve any resource from any path (UNC or traditional), as long
    /// as the server can access it.
    /// </summary>
    [Serializable()]
    public class StaticImageResizeHandle : StaticFileResourceHandle
    {
        #region Private definitions
        public enum ResizeMethod
        {
            /// <summary>
            /// No Scaling
            /// </summary>
            None = 0,

            /// <summary>
            /// Scale and keep propotions. 1 = 100% size
            /// </summary>
            Propotional = 1,

            /// <summary>
            /// Scale to absolute width and height in pixels
            /// </summary>
            Size = 2
        }

        #endregion

        #region Class Members

        private ResizeMethod menmResizeMethod = ResizeMethod.None;
        private double mdblPercentage = 0;
        private int mintWidth = 0;
        private int mintHeight = 0;

        #endregion

        #region C'Tors

        /// <summary>
        /// Static constructor
        /// </summary>
        static StaticImageResizeHandle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class - No scaling. 
        /// Automatically detects content type based on strFilename's extension
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        public StaticImageResizeHandle(string strFileName)
            : this(strFileName, StaticImageResizeHandle.detectContentType(strFileName))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class - No scaling.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request</param>
        public StaticImageResizeHandle(string strFileName, string strContentType)
            : this(strFileName, strContentType, ResizeMethod.None, 0, 0, 0, typeof(StaticImageResizeHandle).FullName, typeof(StaticImageResizeHandle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class - Propotional scaling (1=100%) .
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="dblPercentage">Scale to percentage, if percentage scaling.</param>
        public StaticImageResizeHandle(string strFileName, double dblPercentage)
            : this(strFileName, StaticImageResizeHandle.detectContentType(strFileName), ResizeMethod.Propotional, dblPercentage, 0, 0, typeof(StaticImageResizeHandle).FullName, typeof(StaticImageResizeHandle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class - Absolute scaling to pixels .
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="intWidth">Scale to absolute width, if Size scaling</param>
        /// <param name="intHeight">Scale to absolute height, if Size scaling</param>
        public StaticImageResizeHandle(string strFileName, int intWidth, int intHeight)
            : this(strFileName, StaticImageResizeHandle.detectContentType(strFileName), ResizeMethod.Size, 0, intWidth, intHeight, typeof(StaticImageResizeHandle).FullName, typeof(StaticImageResizeHandle))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class - Propotional scaling (1=100%) .
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request</param>
        /// <param name="dblPercentage">Scale to percentage, if percentage scaling.</param>
        public StaticImageResizeHandle(string strFileName, string strContentType, double dblPercentage)
            : this(strFileName, strContentType, ResizeMethod.Propotional, dblPercentage, 0, 0, typeof(StaticImageResizeHandle).FullName, typeof(StaticImageResizeHandle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class - Absolute scaling to pixels .
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request</param>
        /// <param name="intWidth">Scale to absolute width, if Size scaling</param>
        /// <param name="intHeight">Scale to absolute height, if Size scaling</param>
        public StaticImageResizeHandle(string strFileName, string strContentType, int intWidth, int intHeight)
            : this(strFileName, strContentType, ResizeMethod.Size, 0, intWidth, intHeight, typeof(StaticImageResizeHandle).FullName, typeof(StaticImageResizeHandle))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request.</param>
        /// <param name="enmResizeMethod">Resize method</param>
        /// <param name="dblPercentage">Scale to percentage, if percentage scaling.</param>
        /// <param name="intWidth">Scale to absolute width, if Size scaling</param>
        /// <param name="intHeight">Scale to absolute height, if Size scaling</param>
        /// <param name="strTypeName">The full name of the static gateway class type</param>
        /// <param name="objType">The static gateway class type</param>
        internal protected StaticImageResizeHandle(string strFileName,
                                                        string strContentType,
                                                        ResizeMethod enmResizeMethod,
                                                        double dblPercentage, int intWidth, int intHeight,
                                                        string strTypeName,
                                                        Type objType)
            : base(strFileName, strContentType, strTypeName, objType)
        {
            this.menmResizeMethod = enmResizeMethod;
            this.mdblPercentage = dblPercentage;
            this.mintWidth = intWidth;
            this.mintHeight = intHeight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticImageResizeHandle"/> class.
        /// 
        /// Required by StaticGatewayResourceHandle derived classes - Best practice: No code should be in here.
        /// 
        /// This constructor is required by the router when serving requests for resources served by this static
        /// gateway and should not contain any code.
        /// </summary>
        public StaticImageResizeHandle()
        {
        }

        #endregion

        #region StaticGatewayResourceHandle Members

        /// <summary>
        /// Gets the unique name of the file resource handle type - required to distinguish gateway types
        /// </summary>
        /// <returns></returns>
        protected override string GetUniqueName()
        {
            return this.GetType().FullName;
        }

        public override Stream ToStream()
        {
            if (this.menmResizeMethod == ResizeMethod.None)
                return base.ToStream();
            else if (this.menmResizeMethod == ResizeMethod.Propotional)
            {
                string strFullName = this.getFullName(this.mstrFileName);
                Image objImage = this.ScaleToPercent(strFullName, this.mdblPercentage);
                MemoryStream ms = new MemoryStream();
                objImage.Save(ms, this.GetImageFormat(strFullName));
                objImage.Dispose();
                return ms;
            }
            else if (this.menmResizeMethod == ResizeMethod.Size)
            {
                string strFullName = this.getFullName(this.mstrFileName);
                Image objImage = this.ScaleToSize(strFullName, this.mintWidth, this.mintHeight);
                MemoryStream ms = new MemoryStream();
                objImage.Save(ms, this.GetImageFormat(strFullName));
                objImage.Dispose();
                return ms;
            }
            else
                throw new ArgumentOutOfRangeException("StaticImageResizeHandle missing resize method");
        }

        protected override void WriteContent(IContext objContext, string strFullName)
        {
            // Get request and response
            HostRequest objRequest = objContext.HostContext.Request;
            HostResponse objResponse = objContext.HostContext.Response;
            // Get ResizeMethod and then either Percentage or Width+Height
            ResizeMethod enmResizeMethod = (ResizeMethod)Enum.Parse(typeof(ResizeMethod), objRequest.QueryString["Method"]);
            if (enmResizeMethod == ResizeMethod.None)
                base.WriteContent(objContext, strFullName);
            else if (enmResizeMethod == ResizeMethod.Propotional)
            {
                double dblPercentage = double.Parse(objRequest.QueryString["Percent"], System.Globalization.CultureInfo.InvariantCulture);
                Image objImage = this.ScaleToPercent(strFullName, dblPercentage);
                this.SaveImageToOutputStream(objResponse.OutputStream, objImage, strFullName);
                objImage.Dispose();
            }
            else if (enmResizeMethod == ResizeMethod.Size)
            {
                int intWidth = int.Parse(objRequest.QueryString["Width"]);
                int intHeight = int.Parse(objRequest.QueryString["Height"]);
                Image objImage = this.ScaleToSize(strFullName, intWidth, intHeight);
                this.SaveImageToOutputStream(objResponse.OutputStream, objImage, strFullName);
                objImage.Dispose();
            }
            else
                throw new ArgumentOutOfRangeException("StaticImageResizeHandle missing resize method");
        }

        protected virtual ImageFormat GetImageFormat(string strFullName)
        {
            string strExtension = System.IO.Path.GetExtension(strFullName).ToLower();
            switch (strExtension)
            {
                case "bmp": return ImageFormat.Bmp;
                case "gif": return ImageFormat.Gif;
                case "ico":
                case "icon": return ImageFormat.Icon;
                case "jpg":
                case "jpeg": return ImageFormat.Jpeg;
                case "png": return ImageFormat.Png;
                case "tiff": return ImageFormat.Tiff;
                case "wmf": return ImageFormat.Wmf;
                default: return ImageFormat.Png;
            }
        }
        protected override string GetSpecificResourceHandle()
        {
            string strHandle = base.GetSpecificResourceHandle();
            strHandle += "&Method=" + menmResizeMethod.ToString();
            if (menmResizeMethod == ResizeMethod.Propotional)
                strHandle += "&Percent=" + mdblPercentage.ToString(System.Globalization.CultureInfo.InstalledUICulture);
            else if (menmResizeMethod == ResizeMethod.Size)
                strHandle += "&Width=" + mintWidth.ToString() + "&Height=" + mintHeight.ToString();
            return strHandle;
        }
        #endregion 

        #region Resize helpers
        // Reference: http://www.codeproject.com/Articles/2941/Resizing-a-Photographic-image-with-GDI-for-NET

        protected virtual Image ScaleToPercent(string strFullName, double Percent)
        {

            Image imgPhoto = Image.FromFile(strFullName);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * Percent);
            int destHeight = (int)(sourceHeight * Percent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                      System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                    imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();

            return bmPhoto;
        }


        protected virtual Image ScaleToSize(string strFullName, int Width, int Height)
        {
            Image imgPhoto = Image.FromFile(strFullName);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        /// <summary>
        /// On restricted hosts, an exception is thrown if you save an Image directly to the response stream.
        /// Converting the Image to byte array and saving that array does the trick however.
        /// </summary>
        /// <param name="objStream"></param>
        /// <param name="objImage"></param>
        /// <param name="strFullName"></param>
        private void SaveImageToOutputStream(Stream objStream, Image objImage, string strFullName)
        {
            byte[] buf = ImageToByteArray(objImage, this.GetImageFormat(strFullName));
            objStream.Write(buf, 0, buf.Length);
        }

        private byte[] ImageToByteArray(Image objImage, ImageFormat enmFormat)
        {
            MemoryStream MS = new MemoryStream();
            objImage.Save(MS, enmFormat);
            MS.Seek(0, SeekOrigin.Begin);

            byte[] buf = new byte[MS.Length + 1];
            MS.Read(buf, 0, (int)MS.Length);
            return buf;
        }
        #endregion
    }
}
