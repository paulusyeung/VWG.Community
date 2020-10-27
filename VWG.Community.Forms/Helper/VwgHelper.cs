using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Helper
{
    public class VWGHelper
    {
        /// <summary>
        /// Returns ASP.NET Application Base URL. User-Friendly URL.
        /// </summary>
        /// <returns></returns>
        public static String GetAspNetBaseUrl()
        {
            var request = VWGContext.Current.HttpContext.Request;
            var url = request.Url;
            var appPath = request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/";
            var baseUrl = String.Format("{0}://{1}:{2}{3}/",
                url.Scheme,
                url.Host,
                url.Port,
                appPath);


            return baseUrl;
        }

        /// <summary>
        /// Returns Physical Path of File and/or Folder on Server (Relative to Web Application's Virtual Directory)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fullTrust"></param>
        /// <returns></returns>
        public static String GetPhysicalPath(String filePath, bool fullTrust = false)
        {
            return (GetPhysicalPathOfWebVirtualDirectory(fullTrust) + filePath);
        }

        /// <summary>
        /// Returns Physical Path to the Virtual Directory of this Web Application
        /// </summary>
        /// <param name="fullTrust"></param>
        /// <returns></returns>
        public static String GetPhysicalPathOfWebVirtualDirectory(bool fullTrust = false)
        {
            var result = "";
            if (fullTrust)
            {
                result = VWGContext.Current.Server.MapPath(VWGContext.Current.HttpContext.Request.ApplicationPath);
            }
            else
            {
                result = VWGContext.Current.Server.MapPath("~/");
            }

            return result;
        }

        /// <summary>
        /// Returns VWG Base URL (Route/...../Default/).  This is the Base URL that is used by all VWG Controls.
        /// </summary>
        /// <returns></returns>
        public static String GetVWGBaseUrl()
        {
            var request = VWGContext.Current.HttpContext.Request;
            var url = request.Url;
            var baseUrl = String.Format("{0}://{1}:{2}{3}/",
                url.Scheme,
                url.Host,
                url.Port,
                Path.GetDirectoryName(url.AbsolutePath));

            return baseUrl;
        }
    }
}
