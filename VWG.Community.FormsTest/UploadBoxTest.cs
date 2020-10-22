#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using VWG.Community.Forms;

#endregion

namespace VWG.Community.FormsTest
{
    public partial class UploadBoxTest : Form
    {
        public UploadBoxTest()
        {
            InitializeComponent();
        }

        private void UploadBoxTest_Load(object sender, EventArgs e)
        {
            //VWGContext.Current.CurrentTheme = "Vista";
            VWGContext.Current.CurrentTheme = "Graphite";
            Const_Uploaders();
        }

        private void Const_Uploaders()
        {
            Controls.Clear();

            var filter = @".xml";   //"/^image\/(gif|jpe?g|png)$/i"
            var tempFolder = @"C:\Temp";
            var prompt = "上傳檔案（單擊＋號或拖拉檔案至此）";

            #region 左邊，用 UploadControl 倣做
            var gbx1 = new GroupBox()
            {
                Location = new Point(10, 10),
                Size = new Size(480, 360)
            };
            var dl = new UploadControl()
            {
                Cursor = Cursors.WaitCursor,
                BackColor = Color.DarkSlateGray,
                Dock = DockStyle.Fill,
                UploadFileTypes = filter,
                UploadTempFilePath = tempFolder,
                UploadText = prompt
            };
            dl.UploadBatchCompleted += uploadControl_UploadBatchCompleted;
            dl.UploadError += uploadControl_Error;
            dl.UploadFileCompleted += uploadControl_FileCompleted;
            gbx1.Controls.Add(dl);
            Controls.Add(gbx1);
            #endregion

            #region 右邊，用 UploadBox 倣做
            var gbx2 = new GroupBox()
            {
                Location = new Point(500, 10),
                Size = new Size(480, 120)
            };
            var dark = ColorTranslator.FromHtml("#414141");
            var light = ColorTranslator.FromHtml("#639CD9");    // Color.Green
            var dlb = new UploadBox()
            {
                BackColor = VWGContext.Current.CurrentTheme == "Vista" ? light : dark,
                Dock = DockStyle.Fill,
                ForeColor = Color.WhiteSmoke,
                UploadFileTypes = filter,
                UploadTempFilePath = tempFolder,
                UploadText = prompt
            };
            dlb.UploadBatchCompleted += uploadBox_UploadBatchCompleted;
            dlb.UploadError += uploadBox_Error;
            dlb.UploadFileCompleted += uploadBox_FileCompleted;
            gbx2.Controls.Add(dlb);
            Controls.Add(gbx2);
            #endregion
        }

        #region Events for UploadControl & UploadBox
        private void uploadControl_UploadBatchCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("Batch Completed");
        }

        private void uploadControl_Error(object sender, Gizmox.WebGUI.Forms.UploadErrorEventArgs e)
        {
            MessageBox.Show("Error:" + Environment.NewLine + e.Error);
        }

        private void uploadControl_FileCompleted(object sender, Gizmox.WebGUI.Forms.UploadCompletedEventArgs e)
        {
            MessageBox.Show("File Completed");
        }

        private void uploadBox_UploadBatchCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("Batch Completed");
        }

        private void uploadBox_Error(object sender, Forms.UploadErrorEventArgs e)
        {
            MessageBox.Show("Error:" + Environment.NewLine + e.Error);
        }

        private void uploadBox_FileCompleted(object sender, Forms.UploadCompletedEventArgs e)
        {
            MessageBox.Show("File Completed");
        }
        #endregion
    }
}