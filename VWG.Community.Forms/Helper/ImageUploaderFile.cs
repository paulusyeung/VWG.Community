using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Hosting;

namespace VWG.Community.Forms.Helper
{
    public class ImageUploaderFile
    {
        private HttpPostedFileHandle[] mobjThumbs;
        private HttpPostedFileHandle mobjFile;
        private string mstrDescription = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploaderFile"/> class.
        /// </summary>
        /// <param name="objHttpContext">The obj HTTP context.</param>
        /// <param name="intFileIndex">Index of the int file.</param>
        internal ImageUploaderFile(HostContext objHttpContext, int intFileIndex)
        {
            // Load from request
            mobjThumbs = LoadThumbs(objHttpContext, intFileIndex);
            mobjFile = LoadFile(objHttpContext, intFileIndex);
            mstrDescription = LoadDescription(objHttpContext, intFileIndex);
        }

        /// <summary>
        /// Loads the description.
        /// </summary>
        /// <param name="objHttpContext">The HTTP context.</param>
        /// <param name="intFileIndex">The current file index.</param>
        /// <returns></returns>
        private string LoadDescription(HostContext objHttpContext, int intFileIndex)
        {
            return objHttpContext.Request.Form[string.Format("Description_{0}", intFileIndex)];
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <param name="objHttpContext">The HTTP context.</param>
        /// <param name="intFileIndex">The current file index.</param>
        /// <returns></returns>
        private HttpPostedFileHandle LoadFile(HostContext objHttpContext, int intFileIndex)
        {
            HttpPostedFileHandle objFile = null;

            HostPostedFile objPostedFile = objHttpContext.Request.Files[string.Format("SourceFile_{0}", intFileIndex)];
            if (objPostedFile != null)
            {
                objFile = HttpPostedFileHandle.Create(objPostedFile);
            }

            return objFile;
        }

        /// <summary>
        /// Loads the thumbs.
        /// </summary>
        /// <param name="objHttpContext">The HTTP context.</param>
        /// <param name="intFileIndex">The current file index.</param>
        /// <returns></returns>
        private HttpPostedFileHandle[] LoadThumbs(HostContext objHttpContext, int intFileIndex)
        {
            // List of posted tumbnails
            List<HttpPostedFileHandle> objThumbnails = new List<HttpPostedFileHandle>();

            int intThumbIndex = 1;

            // Reference to the posted tumbnail
            HostPostedFile objPostedThumb = null;

            do
            {
                // Get the posted thumbnail
                objPostedThumb = objHttpContext.Request.Files[string.Format("Thumbnail{0}_{1}", intThumbIndex, intFileIndex)];
                if (objPostedThumb != null)
                {
                    // Get the visual webgui upload file
                    objThumbnails.Add(HttpPostedFileHandle.Create(objPostedThumb));
                }

                intThumbIndex++;
            }
            while (objPostedThumb != null);

            return objThumbnails.ToArray();
        }

        /// <summary>
        /// Gets the posted thumbnails.
        /// </summary>
        /// <value>The posted thumbnails.</value>
        public HttpPostedFileHandle[] PostedThumbnails
        {
            get
            {
                return mobjThumbs;
            }
        }

        /// <summary>
        /// Gets the posted file.
        /// </summary>
        /// <value>The posted file.</value>
        public HttpPostedFileHandle PostedFile
        {
            get
            {
                return mobjFile;
            }
        }
    }
}
