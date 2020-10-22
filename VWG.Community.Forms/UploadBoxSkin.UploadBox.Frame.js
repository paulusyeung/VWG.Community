

function UploadBox_Initialize(strControlId) {
    // Get reference to the XML data node 
    var objNode = mobjApp.Data_GetNode(strControlId);

    /* 2020.10.19 paulus: un-comment for debugging */
    //console.log(strControlId + ": " + JSON.stringify(objNode.attributes));

    // Prepare for settings 
    var objSettings = {};

    // Default prompt text
    var strText = "select or drop files here";

    // Default showing name of uploaded file on bar.
    var blnShowFilenameOnBar = false;

    // Default showing current upload speed on bar.
    var blnShowSpeedOnBar = false;

    // Default setting is enabled control
    var blnEnabled = true;

    // Default regular expression for accepted filenames
    var strAcceptFiles = null;

    // Name of file being uploaded
    var strActiveFile = null;

    // 2020.10.21 paulus: Name of color
    var strBackColor = "transparent";
    var strForeColor = "transparent";

    // Establish default settings
    objSettings["minFileSize"] = 0;
    objSettings["maxFileSize"] = 0;
    objSettings["multipart"] = true;
    objSettings["maxChunkSize"] = 3000 * 1024;  
    objSettings["autoUpload"] = true;          
    objSettings["replaceFileInput"] = true;
    objSettings["singleFileUploads"] = true;        // Required as true for current VWG implementation
    objSettings["sequentialUploads"] = true;        // Required as true for current VWG implementation
    objSettings["dataType"] = 'json';

    objSettings["acceptFileTypes"] = '^.*$';        /* 2020.10.19 paulus: 漏咗？ */
    objSettings["backColor"] = 'transparent';       /* 2020.10.19 paulus: 我加嘅 */
    objSettings["foreColor"] = 'black';             /* 2020.10.19 paulus: 我加嘅 */

    // Event that fires once at the start of every group of files added to the control in the same operation
    objSettings["add"] = function (e, data) {
        var objEvent = vwgContext.events.createEvent(mstrControlId, "Start");
        if (mobjApp.Data_IsCriticalEvent(mstrControlId, "Event.UploadControlBatchStartingHandler")) {
            vwgContext.events.raiseEvents();
        }
        mobjApp.Events_ProcessClientEvent(objEvent);
        data.submit();
    };

    // Event firing on regular intervals during active upload
    objSettings["progressall"] = function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('#progress').css('width', progress + '%');
        UploadBox_process(blnShowFilenameOnBar, blnShowSpeedOnBar, strActiveFile, data.bitrate);
    };

    // Event firing when file has been fully uploaded. Fired once per file.
    objSettings["done"] = function (e, data) {
        var objEvent = vwgContext.events.createEvent(mstrControlId, "Done");
        vwgContext.events.setEventAttribute(objEvent, "Name", data.result[0].Name);
        vwgContext.events.setEventAttribute(objEvent, "TempName", data.result[0].TempFileFullName);
        vwgContext.events.setEventAttribute(objEvent, "Type", data.result[0].Type);
        vwgContext.events.setEventAttribute(objEvent, "Size", data.files[0].size);
        if (mobjApp.Data_IsCriticalEvent(mstrControlId, "Event.UploadControlFileCompletedHandler")) {
            vwgContext.events.raiseEvents();
        }
        mobjApp.Events_ProcessClientEvent(objEvent);

        if (blnShowFilenameOnBar)
            UploadBox_process(blnShowFilenameOnBar, blnShowSpeedOnBar, "", -1);
    };

    // Event firing at the start of every file to be uploaded. Fired once per file.
    objSettings["send"] = function (e, data) {
        // If there is a regular expression for filename matching, evaluate it, else all files are allowed
        if (strAcceptFiles) {
            try 
            {
                // Evaluate regular expression
                var objRegEx = new RegExp(strAcceptFiles, "i");
                // If filename failes match, fire Fail event and return false and prevent download
                if (objRegEx && !objRegEx.test(data.files[0].name)) {
                    var objEvent = vwgContext.events.createEvent(mstrControlId, "Fail");
                    vwgContext.events.setEventAttribute(objEvent, "error", "RMFile");
                    vwgContext.events.setEventAttribute(objEvent, "result", data.files[0].name);
                    if (mobjApp.Data_IsCriticalEvent(mstrControlId, "Event.UploadControlErrorHandler")) {
                        vwgContext.events.raiseEvents();
                    }
                    mobjApp.Events_ProcessClientEvent(objEvent);
                    strActiveFile = null;

                    return false;
                }
            }
            catch (ex) {
                // In case the regular expression is invalid, fire Fail and prevent download
                var objEvent = vwgContext.events.createEvent(mstrControlId, "Fail");
                vwgContext.events.setEventAttribute(objEvent, "error", "RMFail");
                vwgContext.events.setEventAttribute(objEvent, "result", "");
                if (mobjApp.Data_IsCriticalEvent(mstrControlId, "Event.UploadControlErrorHandler")) {
                    vwgContext.events.raiseEvents();
                }
                mobjApp.Events_ProcessClientEvent(objEvent);
                strActiveFile = null;

                return false;
            }
        }

        // Validate size of file
        var intFileSize = data.files[0].size;
        if (objSettings["maxFileSize"] > 0 && intFileSize > objSettings["maxFileSize"]) {
            var objEvent = vwgContext.events.createEvent(mstrControlId, "Fail");
            vwgContext.events.setEventAttribute(objEvent, "error", "MaxSize");
            vwgContext.events.setEventAttribute(objEvent, "result", data.files[0].name);
            if (mobjApp.Data_IsCriticalEvent(mstrControlId, "Event.UploadControlErrorHandler")) {
                vwgContext.events.raiseEvents();
            }
            mobjApp.Events_ProcessClientEvent(objEvent);
            strActiveFile = null;

            return false;
        }
        if (objSettings["minFileSize"] > 0 && intFileSize < objSettings["minFileSize"]) {
            var objEvent = vwgContext.events.createEvent(mstrControlId, "Fail");
            vwgContext.events.setEventAttribute(objEvent, "error", "MinSize");
            vwgContext.events.setEventAttribute(objEvent, "result", data.files[0].name);
            if (mobjApp.Data_IsCriticalEvent(mstrControlId, "Event.UploadControlErrorHandler")) {
                vwgContext.events.raiseEvents();
            }
            mobjApp.Events_ProcessClientEvent(objEvent);
            strActiveFile = null;

            return false;
        }

        // Show filename on progressbar, if requested.
        strActiveFile = data.files[0].name;
        UploadBox_process(blnShowFilenameOnBar, blnShowSpeedOnBar, strActiveFile, -1);
    };

    // Event that fires once at the end of every group of files added to the control in the same operation
    objSettings["stop"] = function (e) {
        var objEvent = vwgContext.events.createEvent(mstrControlId, "Stop");
        if (mobjApp.Data_IsCriticalEvent(mstrControlId, "Event.UploadControlBatchCompletedHandler")) {
            vwgContext.events.raiseEvents();
        }
        mobjApp.Events_ProcessClientEvent(objEvent);
        UploadBox_process(blnShowFilenameOnBar, blnShowSpeedOnBar, "", -1);
    };

    // Set dynamic attributes rendered with the control
    for (i = 0; i < objNode.attributes.length; i++) {
        var attr = objNode.attributes[i];
        var intValue;
        var strValue;
        console.log(attr.nodeName + " = " + attr.nodeValue)
        switch (attr.nodeName) {
            case "Attr.UploadControlMaxNumberOfFiles":
                intValue = parseInt(attr.nodeValue);
                if (intValue > 0)
                    objSettings["maxNumberOfFiles"] = intValue;
                break;
            case "Attr.UploadControlMaxFileSize":
                intValue = parseInt(attr.nodeValue);
                if (intValue > 0)
                    objSettings["maxFileSize"] = intValue;
                break;
            case "Attr.UploadControlMinFileSize":
                intValue = parseInt(attr.nodeValue);
                if (intValue > 0)
                    objSettings["minFileSize"] = intValue;
                break;
            case "Attr.UploadControlFileTypes":
                strAcceptFiles = attr.nodeValue;
                /* 2020.10.19 paulus: 漏咗？ */
                if (strAcceptFiles != '')
                    objSettings["acceptFileTypes"] = strAcceptFiles;
                break;
            case "Attr.UploadControlText":
                strText = attr.nodeValue;
                break;
            case "Attr.UploadControlClientChunkSize":
                strValue = attr.nodeValue;
                if (strValue)
                    objSettings["maxChunkSize"] = parseInt(strValue);
                break;
            case "Attr.UploadControlPost":
                strValue = attr.nodeValue;
                objSettings["url"] = strValue;
                break;
            case "Attr.UploadControlShowFilenameOnBar":
                strValue = attr.nodeValue;
                blnShowFilenameOnBar = (strValue == "1");
                break;
            case "Attr.UploadControlShowSpeedOnBar":
                strValue = attr.nodeValue;
                blnShowSpeedOnBar = (strValue == "1");
                break;
            case "Attr.UploadControlEnabled":
                strValue = attr.nodeValue;
                blnEnabled = (strValue == "1");
                break;
            case "Attr.Background":
                strBackColor = attr.nodeValue;
                break;
            case "Attr.Fore":
                strForeColor = attr.nodeValue;
                break;
        }
    }

    // Activate upload control features
    $(function () {
        // Prevent default browser events
        $(document).bind('drop dragover', function (e) {
            e.preventDefault();
        });

        // Set FileUpload attributes
        $("#drop_text").text(strText);
        $('#fileupload').fileupload(objSettings);

        // 2020.10.19 paulus: 改 inline style
        $('#progress').css({ 'background-color': strBackColor });
        $('#uploadbox-button').css({ 'background-color': strBackColor });
        $('#uploadbox-container').css({ 'color': strForeColor });
        // 加 file type filter
        $('#fileupload').attr( 'accept', strAcceptFiles );
        
        // Disable according to VWG controls setting
        if (!blnEnabled) {
            $('#fileupload').fileupload('disable');
            $('#fileupload').prop("disabled", ! blnEnabled);
        }

        // Safari for Windows doesn't correctly handle selecting of multiple files for upload, so disable it.
        // OS file drag and drop of multiple files works fine though
        var is_winsafari = ((navigator.userAgent.indexOf("Safari") != -1)
            && (navigator.userAgent.indexOf("Chrome") == -1)
            && (navigator.userAgent.indexOf("Windows") != -1));
        if (is_winsafari && (navigator.appVersion.slice(0, 3) < "5.2")) {
            $('#fileupload').removeAttr("multiple");
        }
    });

}

function UploadBox_process(blnShowFilename, blnShowSpeed, strFile, fltBitrate) {
    var strInfo = "";
    if (blnShowFilename)
        strInfo = strFile;
    if (blnShowSpeed && fltBitrate >= 0) {
        if (strInfo)
            strInfo = strInfo + " ";
        strInfo = strInfo + String(Math.round(fltBitrate / 1024)) + " Kbps";
    }
    if (blnShowFilename || blnShowSpeed)
        $('#progress-bar-file').text(strInfo);

    console.log(DateTimeNow() + " Progress (out)...");
}

function DateTimeNow() {
    var m = new Date();
    var dateString =
        m.getUTCFullYear() + "/" +
        ("0" + (m.getUTCMonth() + 1)).slice(-2) + "/" +
        ("0" + m.getUTCDate()).slice(-2) + " " +
        ("0" + m.getUTCHours()).slice(-2) + ":" +
        ("0" + m.getUTCMinutes()).slice(-2) + ":" +
        ("0" + m.getUTCSeconds()).slice(-2);
    return dateString;
}
