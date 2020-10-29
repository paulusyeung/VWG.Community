using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Hosting;

namespace VWG.Community.Resources
{
    /// <summary>
    /// Static gateway that will serve any file on Application virtual folder and subfolders.
    /// </summary>
    [Serializable()]
    public class StaticFileResourceHandle : StaticGatewayResourceHandle, IStaticGateway
    {
        #region Class Members

        /// <summary>
        /// Full name of file
        /// </summary>
        protected string mstrFileName = string.Empty;

        /// <summary>
        /// ContentType
        /// </summary>
        protected string mstrContentType = string.Empty;

        /// <summary>
        /// Full path to application's virtual folder. 
        /// Set before first instance of StaticFileResourceHandle is instanciated.
        /// </summary>
        protected static string mstrApplicationPath = string.Empty;


        #endregion

        #region C'Tors

        /// <summary>
        /// Static constructor to get a full path to the Application folder
        /// Requests arriving as Gateway requests don't have context, so path must be saved as static variable.
        /// 
        /// This is a static constructor which means it is invoked once per application, before the first
        /// instance if the class is instanciated.
        /// </summary>
        static StaticFileResourceHandle()
        {
            mstrApplicationPath = StaticFileResourceHandle.removeBackslash(VWGContext.Current.Server.MapPath("~"));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFileResourceHandle"/> class. 
        /// Automatically detects content type based on strFilename's extension
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        public StaticFileResourceHandle(string strFileName)
            : this(strFileName, StaticFileResourceHandle.detectContentType(strFileName), typeof(StaticFileResourceHandle).FullName, typeof(StaticFileResourceHandle))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFileResourceHandle"/> class.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request</param>
        public StaticFileResourceHandle(string strFileName, string strContentType)
            : this(strFileName, strContentType, typeof(StaticFileResourceHandle).FullName, typeof(StaticFileResourceHandle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFileResourceHandle"/> class.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request.</param>
        /// <param name="strTypeName">The full name of the static gateway class type</param>
        /// <param name="objType">The static gateway class type</param>
        internal protected StaticFileResourceHandle(string strFileName, string strContentType, string strTypeName, Type objType)
            : base(strTypeName, objType)
        {
            string strFullname = this.getFullName(strFileName);
            mstrFileName = strFileName;
            mstrContentType = strContentType;

            if (!this.GrantPathAccess(strFullname))
                throw new AccessViolationException("Gateway is not allowed to access path");
            if (!this.GrantFileAccess(strFullname))
                throw new AccessViolationException("Gateway is not allowed to access file");
            if (!System.IO.File.Exists(strFullname))
                throw new FileNotFoundException("Gateway can not find file");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFileResourceHandle"/> class.
        /// 
        /// Required by StaticGatewayResourceHandle derived classes - Best practice: No code should be in here.
        /// 
        /// This constructor is required by the router when serving requests for resources served by this static
        /// gateway and should not contain any code.
        /// </summary>
        public StaticFileResourceHandle()
        {
        }

        #endregion

        #region Path/File related

        /// <summary>
        /// List of paths from which gateway can serve files. Defaults to BasePath().
        /// 
        /// Empty list means unrestricted access to any path
        /// 
        /// To have unrestricted access to any path, do ALL of the following:
        ///     Override GrantedPaths to return empty list
        ///     Override BasePath to return empty string
        ///     Override GrantFileAccess to return true for all files (which is the default)
        ///     
        /// Important:
        /// If overridden, care must be taken not to base list of paths on values obtained dynamically from
        /// the Visual WebGui context (VWGContext). This method is called within the context of
        /// a gateway request, which has no VWGContext.
        /// </summary>
        /// <returns></returns>
        public virtual string[] GrantedPaths()
        {
            return new string[] { this.BasePath() };
        }

        /// <summary>
        /// Should gateway allow serving of a particular file.
        /// </summary>
        /// <param name="strFullname"></param>
        /// <returns>True if access is granted, false if not granted. Returns true for all files by default.</returns>
        public virtual bool GrantFileAccess(string strFullname)
        {
            return true;
        }

        /// <summary>
        /// The base path from which all files of this gateway are served.
        /// All files served by the gateway will be served from this folder and it's subfolders.
        /// 
        /// To have unrestricted access to any path, do ALL of the following:
        ///     Override GrantedPaths to return empty list
        ///     Override BasePath to return empty string
        ///     Override GrantFileAccess to return true for all files (which is the default)
        ///     
        /// Important:
        /// BasePath() is called only once per application instance, when the application instanciates the
        /// first instance of this class.
        /// </summary>
        /// <returns></returns>
        public virtual string BasePath()
        {
            return Path.Combine(mstrApplicationPath, "Resources");
        }


        #endregion 
        #region IStaticGateway Members

        /// <summary>
        /// Gets the gateway handler.
        /// </summary>
        /// <param name="objContext">Request context.</param>
        /// <returns></returns>
        IStaticGatewayHandler IStaticGateway.GetGatewayHandler(IContext objContext)
        {
            // Get request and response
            HostRequest objRequest = objContext.HostContext.Request;
            HostResponse objResponse = objContext.HostContext.Response;

            // Get filename and content type from decoded query parameters
            string strFilename = HttpUtility.UrlDecode(objRequest.QueryString["Qualifier"]);
            string strContentType = HttpUtility.UrlDecode(objRequest.QueryString["Token"]);

            // Write it
            this.Write(objContext, this.getFullName(strFilename), strContentType);

            return null;
        }

        #endregion

        #region Repsponse writing virtuals

        /// <summary>
        /// Writes the resource and it's headers to the response.
        /// </summary>
        /// <param name="objContext">The response.</param>
        /// <param name="strFullName">The data reader.</param>
        protected virtual void Write(IContext objContext, string strFullName, string strContentType)
        {
            WriteCacheHeaders(objContext);
            WriteContentType(objContext, strContentType);
            WriteContent(objContext, strFullName);
        }

        /// <summary>
        /// Writes the caching headers.
        /// </summary>
        /// <param name="objContext">The response.</param>
        protected virtual void WriteCacheHeaders(IContext objContext)
        {
            objContext.HostContext.Response.Expires = this.Expires;
            objContext.HostContext.Response.Cache.SetCacheability(this.Cacheability);
        }

        /// <summary>
        /// Writes the content type header.
        /// </summary>
        /// <param name="objContext">The response.</param>
        /// <param name="strContentType">The data reader.</param>
        protected virtual void WriteContentType(IContext objContext, string strContentType)
        {
            objContext.HostContext.Response.ContentType = strContentType;
        }


        /// <summary>
        /// Writes the binary content of the file/resource.
        /// </summary>
        /// <param name="objContext">The response.</param>
        /// <param name="strFullName">The data reader.</param>
        protected virtual void WriteContent(IContext objContext, string strFullName)
        {
            objContext.HostContext.Response.WriteFile(strFullName);
        }

        /// <summary>
        /// Gets the cacheability.
        /// </summary>
        /// <value>The cacheability.</value>
        protected virtual HttpCacheability Cacheability
        {
            get
            {
                return HttpCacheability.Public;
            }
        }

        /// <summary>
        /// Gets the expires value.
        /// </summary>
        /// <value>The expires value.</value>
        protected virtual int Expires
        {
            get
            {
                return -1;
            }
        }
        #endregion


        #region StaticGatewayResourceHandle Members
        /// <summary>
        /// Gets a value indicating whether this resource is a local server resource.
        /// This gateway returns true, assuming that all resources it will serve are accessible by the server.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a local server resource; otherwise, <c>false</c>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool IsServerResource
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Return a Stream object for the resource being served.
        /// 
        /// By providing a Stream object for the resource, you can use ToImage() and ToIcon() for resource types
        /// that support them. For instance, it doesn't make sense to to a ToImage() on a text file resource, but
        /// it does make sense for PNG file resource.
        /// </summary>
        /// <returns></returns>
        public override Stream ToStream()
        {
            if (this.IsServerResource)
                return new FileStream(this.getFullName(mstrFileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            else
                return null;
        }
        /// <summary>
        /// Gets the specific resource handle  - used for ToString().
        /// 
        /// The returned value will be the Url used for this particular resource/file.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override string GetSpecificResourceHandle()
        {
            return string.Format("{0}?Qualifier={1}&Token={2}", base.GetSpecificResourceHandle(), HttpUtility.UrlEncode(this.mstrFileName), HttpUtility.UrlEncode(this.mstrContentType));
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override string GetUniqueIdentifier()
        {
            return base.GetSpecificResourceHandle();
        }

        /// <summary>
        /// Gets the unique name of the file resource handle type.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual string GetUniqueName()
        {
            return this.GetType().FullName;
        }
        #endregion

        #region Private helpers
        /// <summary>
        /// Remove potential single backslash from the end of a path
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns>Original path with potential trailing backslash removed</returns>
        private static string removeBackslash(string strPath)
        {
            if (string.IsNullOrEmpty(strPath) || strPath.Length == 0)
                return strPath;
            return strPath.TrimEnd('\\');
        }

        /// <summary>
        /// Contatenates BasePath() and the filename to return as a full name of file resource
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        protected string getFullName(string strFileName)
        {
            string strFullName = strFileName;
            if (!string.IsNullOrEmpty(this.BasePath()))
                strFullName = Path.Combine(this.BasePath(), strFileName);
            return Path.GetFullPath(strFullName);
        }

        /// <summary>
        /// Check if access to path should be granted. 
        /// </summary>
        /// <param name="strFullName"></param>
        /// <returns>True if access is granted, false if not</returns>
        private bool GrantPathAccess(string strFullName)
        {
            if (this.GrantedPaths() != null && this.GrantedPaths().Length > 0)
            {
                string strFilePath = Path.GetDirectoryName(Path.GetFullPath(strFullName)) + '\\';
                foreach (string strPath in this.GrantedPaths())
                {
                    string mstrPathToCompare = (StaticFileResourceHandle.removeBackslash(strPath) + '\\');
                    if (mstrPathToCompare.Length > strFilePath.Length)
                        return false;
                    else if (!strFilePath.StartsWith(mstrPathToCompare, StringComparison.InvariantCulture))
                        return false;
                }
                return true;
            }
            else
                return true;
        }

        /// <summary>
        /// Dynamically detect content type from filename extension, if not specifically stated.
        /// </summary>
        /// <param name="strFilename"></param>
        /// <returns></returns>
        protected static string detectContentType(string strFilename)
        {
            string strContentType = "application/octetstream";
            string strExtension = System.IO.Path.GetExtension(strFilename);
            Microsoft.Win32.RegistryKey objKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(strExtension);
            if (objKey != null)
            {
                object objValue = objKey.GetValue("Content Type");
                if (objValue != null)
                    strContentType = objValue.ToString();
            }
            return strContentType;
        }


        #endregion 

    }
}
