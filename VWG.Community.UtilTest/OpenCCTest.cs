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
using VWG.Community.Util;

#endregion

namespace VWG.Community.UtilTest
{
    public partial class OpenCCTest : Form
    {
        private String _ConfigFileName;
        private CurrentMode _CurrentMode = CurrentMode.Text;

        public OpenCCTest()
        {
            InitializeComponent();
        }

        private void OpenCCTest_Load(object sender, EventArgs e)
        {
            SetDropdownList();
        }

        private enum CurrentMode
        {
            Text,
            FileList
        }

        private void SetDropdownList()
        {
            TextValuePair[] configs = new TextValuePair[]
            {
                new TextValuePair("簡體 => 繁體", "s2t.json"),
                new TextValuePair("繁體 => 簡體", "t2s.json"),
                new TextValuePair("簡體 => 香港繁體", "s2hk.json"),
                new TextValuePair("簡體 => 臺灣正體", "s2tw.json"),
                new TextValuePair("簡體 => 繁體（臺灣正體標準）並轉換爲臺灣常用詞彙", "s2twp.json"),
                new TextValuePair("香港繁體 => 簡體", "hk2s.json"),
                new TextValuePair("臺灣 => 體到簡體", "tw2s.json"),
                new TextValuePair("繁體（臺灣正體標準）=> 簡體 並轉換爲中國大陸常用詞彙", "tw2sp.json"),
                new TextValuePair("繁體（OpenCC 標準）=> 臺灣正體", "t2tw.json"),
                new TextValuePair("繁體（OpenCC 標準）=> 香港繁體", "t2hk.json"),
            };
            var origIndex = cboConfiguration.SelectedIndex;
            cboConfiguration.Items.Clear();
            cboConfiguration.Items.AddRange(configs);
            cboConfiguration.SelectedIndex = origIndex == -1 ? 0 : origIndex;
        }

        private class TextValuePair
        {
            public string Text;
            public string Value;

            public TextValuePair(string text, string value)
            {
                this.Text = text;
                this.Value = value;
            }

            public override string ToString()
            {
                return this.Text;
            }
        }

        private void cboConfiguration_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ConfigFileName = ((TextValuePair)cboConfiguration.SelectedItem).Value;
        }

        private void cmdConvert_Click(object sender, EventArgs e)
        {
            switch (_CurrentMode)
            {
                case CurrentMode.Text:
                    try
                    {
                        var opencc = new OpenCC();
                        opencc.Load();
                        txtResult.Text = opencc.Convert(txtSource.Text, _ConfigFileName);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }

                    break;
                case CurrentMode.FileList:
                    //FileListUtility.ConvertAndStoreFilesInList(fileListItems, _ConfigFileName);
                    break;
            }

        }

    }
}