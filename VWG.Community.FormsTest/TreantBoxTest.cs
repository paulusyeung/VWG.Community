#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using VWG.Community.Forms;
using System.IO;
using VWG.Community.Forms.Helper;
using Newtonsoft.Json;
using System.Web;

#endregion

namespace VWG.Community.FormsTest
{
    public partial class TreantBoxTest : Form
    {
        public TreantBoxTest()
        {
            InitializeComponent();

            VWGContext.Current.CurrentTheme = "Vista";
            //VWGContext.Current.CurrentTheme = "Graphite";

            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            #region 讀入隻 Json data，由 file 變為 string
            string filename = "json-data-sample.json", json = "";
            var file = File.OpenRead(Path.Combine(VWGContext.Current.Config.GetDirectory("UserData"), filename));
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }
            #endregion

            #region 要加工隻 image url，將 VWG 認可嘅 url 加入隻 image value (file url location) 度
            // 將 json string 轉為 object list
            List<TreantBoxRecord> items = JsonConvert.DeserializeObject<List<TreantBoxRecord>>(json);
            foreach (var item in items)
            {
                // 假設啲 images 放喺 Resources/Images 之下，加個 VWG 認可嘅 Url，保持原 size
                var vwgImageUrl = (new GeneralSizeableHandle("Resources/Images" + item.image, 1)).ToString();
                // 加好之後放返落去 image value
                item.image = vwgImageUrl;
            }
            // 將 object list 轉回 json string
            var jsonItems = JsonConvert.SerializeObject(items);
            #endregion

            //var box2 = new TreantBox("Resources/UserData/json-data-sample.json");
            // or
            var box2 = new TreantBox();
            box2.TreantBoxDataJson = jsonItems;

            box2.Dock = DockStyle.Fill;
            this.rightPanel.Controls.Add(box2);
        }
    }
}