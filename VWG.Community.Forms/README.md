###         VWG Client side functions:
         * Data_GetNode
         * Web_GetEventSource
         * Web_IsAttribute
         * Web_GetQueryStringParam
         * Web_GetVwgElement

#### Javacript Notes：
```javascript
        // Get applicaion and client API reference
        var anything = "";
        window.mobjApp = parent.mobjApp;
        window.vwgContext = parent.vwgContext;

        // Get WebGUI control 嘅 id
        var strControlId = mobjApp.Web_GetQueryStringParam(document.location.href, "id");
        var objNode = mobjApp.Data_GetNode(strControlId);

        // 顯示 attributes 數量：default + TreantBox.RenderAttributes()
        console.log(strControlId + ": " + JSON.stringify(objNode.attributes));

        // 顯示 attributes key value pairs
        var strBackColor = "transparent";
        var strForeColor = "transparent";
        //
        for (i = 0; i < objNode.attributes.length; i++) {
            var attr = objNode.attributes[i];
            console.log(attr.nodeName + " = " + attr.nodeValue);

            switch (attr.nodeName) {
                case "Attr.UploadControlMaxNumberOfFiles":
                    break;
                case "Attr.Background":
                    strBackColor = attr.nodeValue;
                    break;
                case "Attr.Fore":
                    strForeColor = attr.nodeValue;
                    break;
                case "ANYTHING":    // custom attribute (no leading Attr.)
                    anything = attr.nodeValue;
                    break;
            };
        };
```
### [UploadBox](https://github.com/blueimp/jQuery-File-Upload)

![Screen Cap](https://github.com/paulusyeung/VWG.Community/blob/master/assets/images/UploadBox.png)

左邊係 WebGUI 嘅，右邊係我改嘅

改造 WebGUI 嘅 UploadControl，改動：
1. 依然用 jQuery-File-Upload，不過唔用 embedded，改用 cdnjs 最新嘅 source code
2. 加咗 filter 喺 click to browse file
3. 加咗 BackColor 可以自選 background color
4. 加咗 ForeColor 可以自選 text color
5. 個 drop zone 同個 click to browse button 變為同一個 box
6. 個 drop zone size 可控，可以用 DockStyle.Fill

用嘅時候一定要取消 Disable Obscuring

### [DropzoneJs](https://github.com/enyo/dropzone)

新款 file uploader，不過未完成，因為我臨時決定改造 UploadBox，已經好靚仔，啲 code 應該可靠，可以減少 debugging。

參考：

1. [Asp.net C# + Dropzone js : Easy Way to Upload Images (Drag & Drop Feature)](https://codepedia.info/using-dropzone-js-file-image-upload-in-asp-net-webform-c/)

### [MessageBox2](#)

跟 MessageBox 大致相同，唯一分別係：MessageBox 係 async，即係當你 call 完佢，佢有佢做，你會接住做下一句。而 MessageBox2 係 sync，你要等佢做完先可以 execute 下一句。

### [Xonomy](https://github.com/michmech/xonomy)

XML Editor

![Screen Cap](https://github.com/paulusyeung/VWG.Community/blob/master/assets/images/XonomyBox.png)

### [Treant.js](https://fperucic.github.io/treant-js/)

Organization Tree

![Screen Cap](https://github.com/paulusyeung/VWG.Community/blob/master/assets/images/TreantBox.png)

