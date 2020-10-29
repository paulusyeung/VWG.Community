using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gizmox.WebGUI.Forms;

namespace VWG.Community.Resources
{
    /// <summary>
    /// Static gateway that will serve any resource from the folder specified by the UserData directory
    /// specified in web.config. The gateway will serve any file resource from this folder and subfolders.
    /// Any (custom) directory can be used by overriding the BasePath() method in inherited classes.
    /// </summary>
    [Serializable()]
    public class StaticUserDataResourceHandle : StaticFileResourceHandle
    {
        #region Class Members

        private static string mstrCustomFolderName = "UserData";
        private static string mstrCustomFolderPath = string.Empty;

        #endregion

        #region C'Tors

        /// <summary>
        /// Static constructor to get a full path to the folder to serve resources from
        /// Requests arriving as Gateway requests don't have context, so path must be saved as static variable.
        /// 
        /// This is a static constructor which means it is invoked once per application, before the first
        /// instance if the class is instanciated.
        /// </summary>
        static StaticUserDataResourceHandle()
        {
            // Get configuration value for full path to UserData directory's folder
            mstrCustomFolderPath = VWGContext.Current.Config.GetDirectory(mstrCustomFolderName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUserDataResourceHandle"/> class. 
        /// Automatically detects content type based on strFilename's extension
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        public StaticUserDataResourceHandle(string strFileName)
            : this(strFileName, StaticUserDataResourceHandle.detectContentType(strFileName))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUserDataResourceHandle"/> class.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request</param>
        public StaticUserDataResourceHandle(string strFileName, string strContentType)
            : this(strFileName, strContentType, typeof(StaticUserDataResourceHandle).FullName, typeof(StaticUserDataResourceHandle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUserDataResourceHandle"/> class.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request.</param>
        /// <param name="strTypeName">The full name of the static gateway class type</param>
        /// <param name="objType">The static gateway class type</param>
        internal protected StaticUserDataResourceHandle(string strFileName, string strContentType, string strTypeName, Type objType)
            : base(strFileName, strContentType, strTypeName, objType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUserDataResourceHandle"/> class.
        /// 
        /// Required by StaticGatewayResourceHandle derived classes - Best practice: No code should be in here.
        /// 
        /// This constructor is required by the router when serving requests for resources served by this static
        /// gateway and should not contain any code.
        /// </summary>
        public StaticUserDataResourceHandle()
        {
        }

        #endregion

        /// <summary>
        /// Files are restricted to the custom directory folder, so return the full path of it.
        /// </summary>
        /// <returns></returns>
        public override string BasePath()
        {
            return mstrCustomFolderPath;
        }


        /// <summary>
        /// Gets the unique name of the file resource handle type - required to distinguish gateway types
        /// </summary>
        /// <returns></returns>
        protected override string GetUniqueName()
        {
            return this.GetType().FullName;
        }


    }
}
