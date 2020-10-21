using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Helper
{
    public class UploadBoxHelper
    {
        //
        // Summary:
        //     UploadControl size of each chunk when uploading big files
        public const string UploadBoxClientChunkSize = "UBC";
        //
        // Summary:
        //     UploadControl enabled or not
        public const string UploadBoxEnabled = "UBEN";
        //
        // Summary:
        //     UploadControl regular expression for files allowed for upload
        public const string UploadBoxFileTypes = "UBTypes";
        //
        // Summary:
        //     UploadControl maximum size of uploaded file
        public const string UploadBoxMaxFileSize = "UBMax";
        //
        // Summary:
        //     UploadControl maximum number of files allowed to upload in one group
        public const string UploadBoxMaxNumberOfFiles = "UBFiles";
        //
        // Summary:
        //     UploadControl minimum size of uploaded file
        public const string UploadBoxMinFileSize = "UBMin";
        //
        // Summary:
        //     UploadControl postback Url
        public const string UploadBoxPost = "UBP";
        //
        // Summary:
        //     UploadControl show name of currently uploading file on progressbar
        public const string UploadBoxShowFilenameOnBar = "UBBF";
        //
        // Summary:
        //     UploadControl show current speed of upload on progress bar
        public const string UploadBoxShowSpeedOnBar = "UBBS";
        //
        // Summary:
        //     UploadControl text used to prompt for files
        public const string UploadBoxText = "UBT";
        //
        //
        public const string UploadBoxBatchCompletedHandler = "UBBCH";
        public const string UploadBoxBatchStartingHandler = "UBBSH";
        public const string UploadBoxErrorHandler = "UBEH";
        public const string UploadBoxFileCompletedHandler = "UBFCH";
    }
}
