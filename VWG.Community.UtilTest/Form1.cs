#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using VWG.Community.Util;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace VWG.Community.UtilTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFirstText.Focus();
        }

        private void cmdSoundex1_Click(object sender, EventArgs e)
        {
            var msg = Soundex.Encode(txtFirstText.Text.Trim());
            MessageBox.Show(msg);
        }

        private void cmdSoundex2_Click(object sender, EventArgs e)
        {
            var msg = Soundex.Encode(txtSecondText.Text.Trim());
            MessageBox.Show(msg);
            var phonetic = new PhoneticMatch();
            var pMsg = phonetic.CreateToken(txtSecondText.Text.Trim());
            MessageBox.Show(pMsg);
        }

        private void cmdMatch_Click(object sender, EventArgs e)
        {
            var match = Soundex.Diff(txtFirstText.Text.Trim(), txtSecondText.Text.Trim());
            MessageBox.Show(match.ToString());
        }

        private void cmdLevenshteinDistance_Click(object sender, EventArgs e)
        {
            var distance = LevenshteinDistance.CalculateSimilarity(txtFirstText.Text.Trim(), txtSecondText.Text.Trim());
            MessageBox.Show(distance.ToString("0.###"));
        }

        private void cmdMetaphone_Click(object sender, EventArgs e)
        {
            var metaphone = new Metaphone();
            var msg1 = metaphone.Encode(txtFirstText.Text.Trim());
            MessageBox.Show(msg1);

            var msg2 = metaphone.Encode(txtSecondText.Text.Trim());
            MessageBox.Show(msg2);
        }

        private void cmdLoadCsv_Click(object sender, EventArgs e)
        {
            var codes = new VWG.Community.Util.CountryCodes();
            var list = codes.GetList();

            lvwCountryCodes.DataSource = list;
        }

        private void cmdLoadCsvForm_Click(object sender, EventArgs e)
        {
            Context.Transfer(new VWG.Community.UtilTest.CountryCodes());
        }

        private void ShowResult(CountryCode item)
        {
            if (item != null)
            {
                MessageBox.Show(JsonConvert.SerializeObject(item));
            }
            else
            {
                MessageBox.Show("Not found");
            }
        }

        private void cmdFindCountryName_Click(object sender, EventArgs e)
        {
            var target = txtCountryName.Text.Trim();
            var codes = new VWG.Community.Util.CountryCodes();
            var item = codes.FindByDisplayName(target);

            ShowResult(item);
        }

        private void cmdFindFIFACode_Click(object sender, EventArgs e)
        {
            var target = txtFIFACode.Text.Trim();
            var codes = new VWG.Community.Util.CountryCodes();
            var item = codes.FindByFIFA(target);

            ShowResult(item);
        }

        private void cmdTLD_Click(object sender, EventArgs e)
        {
            var target = txtTLD.Text.Trim();
            var codes = new VWG.Community.Util.CountryCodes();
            var item = codes.FindByTLD(target);

            ShowResult(item);
        }

        private void cmdPhone_Click(object sender, EventArgs e)
        {
            var target = txtPhone.Text.Trim();
            var codes = new VWG.Community.Util.CountryCodes();
            var item = codes.FindByDial(target);

            ShowResult(item);
        }

        private void cmdISO3166_Click(object sender, EventArgs e)
        {
            var target = txtISO3166.Text.Trim();
            var codes = new VWG.Community.Util.CountryCodes();
            var item = codes.FindByISO3166_Alpha3(target);

            ShowResult(item);
        }

        private void cmdFIPS_Click(object sender, EventArgs e)
        {
            var target = txtFIPS.Text.Trim();
            var codes = new VWG.Community.Util.CountryCodes();
            var item = codes.FindByFIPS(target);

            ShowResult(item);
        }

        private void cmdAll_Click(object sender, EventArgs e)
        {
            var target = txtAll.Text.Trim();
            var codes = new VWG.Community.Util.CountryCodes();
            var item = codes.FindByAll(target);

            ShowResult(item);
        }
    }
}