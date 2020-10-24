//
// start() 喺 body onload 嘅時候用，initialize Xonomy
//
function start(dataFile, styleFile) {
    if (!dataFile) dataFile = getParameterByName("datafile");                      // 如果 caller 冇提供 sUrlXmls 同 UrlSpec，就去 QueryString 搵
    if (!styleFile) styleFile = getParameterByName("stylefile");

    //var editor = document.getElementById("editor");
    //
    //$.ajax({ url: sUrlXml, dataType: 'xml', type: 'GET',                    // sUrlXml 係 XMLDocument
    //    error: function () {
    //        alert("failed to load xml file");
    //    },
    //    success: function (xmlFile) {
    //        $.ajax({ url: sUrlSpec, dataType: 'script', type: 'GET',        // sUrlSpec 係 javascript
    //            error: function () {
    //                //no doc spec, read only
    //                Xonomy.render(xmlFile, editor, "null");
    //            },
    //            success: function (docFile) {
    //                Xonomy.render(xmlFile, editor, docSpec);                // 這裡的 docSpec 係隱藏在 docFile 中
    //            }
    //        });
    //    }
    //});
}

function harvest(formId) {
    if (typeof (VWG) == 'undefined' || VWG == null || VWG.Events == null) return;
    var xml = Xonomy.harvest();
    var eventType = "XonomyHarvest";
    var objEvent = VWG.Events.CreateEvent(formId, eventType);
    VWG.Events.SetEventAttribute(objEvent, "Value", xml);
    VWG.Events.RaiseEvents();
}

function showAttribute(attrId) {
    debugger;
    var xml = getParameterByName("xml");
    var spec = getParameterByName("spec");
    var attrValue = Data_GetAttribute(attrId, "sUrl", "None");
    alert(attrValue);
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
