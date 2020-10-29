using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Resources
{
    /// <summary>
    /// Static gateway that will serve any resource from any path (UNC or traditional), as long
    /// as the server can access it.
    /// </summary>
    [Serializable()]
    public class StaticUnrestrictedFileResourceHandle : StaticFileResourceHandle
    {
        #region Class Members

        #endregion

        #region C'Tors

        /// <summary>
        /// Static constructor
        /// </summary>
        static StaticUnrestrictedFileResourceHandle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUnrestrictedFileResourceHandle"/> class. 
        /// Automatically detects content type based on strFilename's extension
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        public StaticUnrestrictedFileResourceHandle(string strFileName)
            : this(strFileName, StaticUnrestrictedFileResourceHandle.detectContentType(strFileName))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUnrestrictedFileResourceHandle"/> class.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request</param>
        public StaticUnrestrictedFileResourceHandle(string strFileName, string strContentType)
            : this(strFileName, strContentType, typeof(StaticUnrestrictedFileResourceHandle).FullName, typeof(StaticUnrestrictedFileResourceHandle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUnrestrictedFileResourceHandle"/> class.
        /// </summary>
        /// <param name="strFileName">The filename to reference</param>
        /// <param name="strContentType">The content type to use when serving the gateway request.</param>
        /// <param name="strTypeName">The full name of the static gateway class type</param>
        /// <param name="objType">The static gateway class type</param>
        internal protected StaticUnrestrictedFileResourceHandle(string strFileName, string strContentType, string strTypeName, Type objType)
            : base(strFileName, strContentType, strTypeName, objType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticUnrestrictedFileResourceHandle"/> class.
        /// 
        /// Required by StaticGatewayResourceHandle derived classes - Best practice: No code should be in here.
        /// 
        /// This constructor is required by the router when serving requests for resources served by this static
        /// gateway and should not contain any code.
        /// </summary>
        public StaticUnrestrictedFileResourceHandle()
        {
        }

        #endregion

        /// <summary>
        /// Return empty path, as there are no restrictions
        /// </summary>
        /// <returns></returns>
        public override string BasePath()
        {
            return string.Empty;
        }


        /// <summary>
        /// Gets the unique name of the file resource handle type - required to distinguish gateway types
        /// </summary>
        /// <returns></returns>
        protected override string GetUniqueName()
        {
            return this.GetType().FullName;
        }

        /// <summary>
        /// Return empty list as there are no restrictions.
        /// </summary>
        /// <returns></returns>
        public override string[] GrantedPaths()
        {
            return new string[] { };
        }
    }
}
