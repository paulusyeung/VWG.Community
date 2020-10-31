namespace VWG.Community.UtilTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Visual WebGui Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new Gizmox.WebGUI.Forms.Label();
            this.txtFirstText = new Gizmox.WebGUI.Forms.TextBox();
            this.groupBox1 = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdMetaphone = new Gizmox.WebGUI.Forms.Button();
            this.cmdLevenshteinDistance = new Gizmox.WebGUI.Forms.Button();
            this.cmdMatch = new Gizmox.WebGUI.Forms.Button();
            this.cmdSoundex2 = new Gizmox.WebGUI.Forms.Button();
            this.cmdSoundex1 = new Gizmox.WebGUI.Forms.Button();
            this.label2 = new Gizmox.WebGUI.Forms.Label();
            this.txtSecondText = new Gizmox.WebGUI.Forms.TextBox();
            this.groupBox2 = new Gizmox.WebGUI.Forms.GroupBox();
            this.label5 = new Gizmox.WebGUI.Forms.Label();
            this.txtTLD = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdTLD = new Gizmox.WebGUI.Forms.Button();
            this.cmdFindFIFACode = new Gizmox.WebGUI.Forms.Button();
            this.txtFIFACode = new Gizmox.WebGUI.Forms.TextBox();
            this.label4 = new Gizmox.WebGUI.Forms.Label();
            this.label3 = new Gizmox.WebGUI.Forms.Label();
            this.txtCountryName = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdFindCountryName = new Gizmox.WebGUI.Forms.Button();
            this.cmdLoadCsvForm = new Gizmox.WebGUI.Forms.Button();
            this.cmdLoadCsv = new Gizmox.WebGUI.Forms.Button();
            this.lvwCountryCodes = new Gizmox.WebGUI.Forms.ListView();
            this.label6 = new Gizmox.WebGUI.Forms.Label();
            this.txtPhone = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdPhone = new Gizmox.WebGUI.Forms.Button();
            this.cmdISO3166 = new Gizmox.WebGUI.Forms.Button();
            this.txtISO3166 = new Gizmox.WebGUI.Forms.TextBox();
            this.label7 = new Gizmox.WebGUI.Forms.Label();
            this.label8 = new Gizmox.WebGUI.Forms.Label();
            this.txtFIPS = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdFIPS = new Gizmox.WebGUI.Forms.Button();
            this.cmdAll = new Gizmox.WebGUI.Forms.Button();
            this.txtAll = new Gizmox.WebGUI.Forms.TextBox();
            this.label9 = new Gizmox.WebGUI.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Text:";
            // 
            // txtFirstText
            // 
            this.txtFirstText.Location = new System.Drawing.Point(121, 29);
            this.txtFirstText.Name = "txtFirstText";
            this.txtFirstText.Size = new System.Drawing.Size(260, 20);
            this.txtFirstText.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.cmdMetaphone);
            this.groupBox1.Controls.Add(this.cmdLevenshteinDistance);
            this.groupBox1.Controls.Add(this.txtFirstText);
            this.groupBox1.Controls.Add(this.cmdMatch);
            this.groupBox1.Controls.Add(this.cmdSoundex2);
            this.groupBox1.Controls.Add(this.cmdSoundex1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSecondText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(54, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 120);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Soundex";
            // 
            // cmdMetaphone
            // 
            this.cmdMetaphone.Location = new System.Drawing.Point(121, 80);
            this.cmdMetaphone.Name = "cmdMetaphone";
            this.cmdMetaphone.Size = new System.Drawing.Size(127, 23);
            this.cmdMetaphone.TabIndex = 2;
            this.cmdMetaphone.Text = "Metaphone ";
            this.cmdMetaphone.Click += new System.EventHandler(this.cmdMetaphone_Click);
            // 
            // cmdLevenshteinDistance
            // 
            this.cmdLevenshteinDistance.Location = new System.Drawing.Point(251, 80);
            this.cmdLevenshteinDistance.Name = "cmdLevenshteinDistance";
            this.cmdLevenshteinDistance.Size = new System.Drawing.Size(130, 23);
            this.cmdLevenshteinDistance.TabIndex = 2;
            this.cmdLevenshteinDistance.Text = "Levenshtein Distance";
            this.cmdLevenshteinDistance.Click += new System.EventHandler(this.cmdLevenshteinDistance_Click);
            // 
            // cmdMatch
            // 
            this.cmdMatch.Location = new System.Drawing.Point(384, 80);
            this.cmdMatch.Name = "cmdMatch";
            this.cmdMatch.Size = new System.Drawing.Size(75, 23);
            this.cmdMatch.TabIndex = 2;
            this.cmdMatch.Text = "Match";
            this.cmdMatch.Click += new System.EventHandler(this.cmdMatch_Click);
            // 
            // cmdSoundex2
            // 
            this.cmdSoundex2.Location = new System.Drawing.Point(384, 53);
            this.cmdSoundex2.Name = "cmdSoundex2";
            this.cmdSoundex2.Size = new System.Drawing.Size(75, 23);
            this.cmdSoundex2.TabIndex = 2;
            this.cmdSoundex2.Text = "Soundex";
            this.cmdSoundex2.Click += new System.EventHandler(this.cmdSoundex2_Click);
            // 
            // cmdSoundex1
            // 
            this.cmdSoundex1.Location = new System.Drawing.Point(384, 27);
            this.cmdSoundex1.Name = "cmdSoundex1";
            this.cmdSoundex1.Size = new System.Drawing.Size(75, 23);
            this.cmdSoundex1.TabIndex = 2;
            this.cmdSoundex1.Text = "Soundex";
            this.cmdSoundex1.Click += new System.EventHandler(this.cmdSoundex1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(18, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Second Text:";
            // 
            // txtSecondText
            // 
            this.txtSecondText.Location = new System.Drawing.Point(121, 55);
            this.txtSecondText.Name = "txtSecondText";
            this.txtSecondText.Size = new System.Drawing.Size(260, 20);
            this.txtSecondText.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtAll);
            this.groupBox2.Controls.Add(this.cmdAll);
            this.groupBox2.Controls.Add(this.cmdFIPS);
            this.groupBox2.Controls.Add(this.txtFIPS);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtISO3166);
            this.groupBox2.Controls.Add(this.cmdISO3166);
            this.groupBox2.Controls.Add(this.cmdPhone);
            this.groupBox2.Controls.Add(this.txtPhone);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtTLD);
            this.groupBox2.Controls.Add(this.cmdTLD);
            this.groupBox2.Controls.Add(this.cmdFindFIFACode);
            this.groupBox2.Controls.Add(this.txtFIFACode);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCountryName);
            this.groupBox2.Controls.Add(this.cmdFindCountryName);
            this.groupBox2.Controls.Add(this.cmdLoadCsvForm);
            this.groupBox2.Controls.Add(this.cmdLoadCsv);
            this.groupBox2.Controls.Add(this.lvwCountryCodes);
            this.groupBox2.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(54, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 463);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Country Codes";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Find TLD Code (.2):";
            // 
            // txtTLD
            // 
            this.txtTLD.Location = new System.Drawing.Point(163, 298);
            this.txtTLD.Name = "txtTLD";
            this.txtTLD.Size = new System.Drawing.Size(218, 20);
            this.txtTLD.TabIndex = 1;
            // 
            // cmdTLD
            // 
            this.cmdTLD.Location = new System.Drawing.Point(384, 296);
            this.cmdTLD.Name = "cmdTLD";
            this.cmdTLD.Size = new System.Drawing.Size(75, 23);
            this.cmdTLD.TabIndex = 2;
            this.cmdTLD.Text = "Find";
            this.cmdTLD.Click += new System.EventHandler(this.cmdTLD_Click);
            // 
            // cmdFindFIFACode
            // 
            this.cmdFindFIFACode.Location = new System.Drawing.Point(384, 265);
            this.cmdFindFIFACode.Name = "cmdFindFIFACode";
            this.cmdFindFIFACode.Size = new System.Drawing.Size(75, 23);
            this.cmdFindFIFACode.TabIndex = 2;
            this.cmdFindFIFACode.Text = "Find";
            this.cmdFindFIFACode.Click += new System.EventHandler(this.cmdFindFIFACode_Click);
            // 
            // txtFIFACode
            // 
            this.txtFIFACode.Location = new System.Drawing.Point(163, 267);
            this.txtFIFACode.Name = "txtFIFACode";
            this.txtFIFACode.Size = new System.Drawing.Size(218, 20);
            this.txtFIFACode.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Find FIFA Code (3):";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Find Country Name:";
            // 
            // txtCountryName
            // 
            this.txtCountryName.Location = new System.Drawing.Point(163, 236);
            this.txtCountryName.Name = "txtCountryName";
            this.txtCountryName.Size = new System.Drawing.Size(218, 20);
            this.txtCountryName.TabIndex = 1;
            // 
            // cmdFindCountryName
            // 
            this.cmdFindCountryName.Location = new System.Drawing.Point(384, 234);
            this.cmdFindCountryName.Name = "cmdFindCountryName";
            this.cmdFindCountryName.Size = new System.Drawing.Size(75, 23);
            this.cmdFindCountryName.TabIndex = 2;
            this.cmdFindCountryName.Text = "Find";
            this.cmdFindCountryName.Click += new System.EventHandler(this.cmdFindCountryName_Click);
            // 
            // cmdLoadCsvForm
            // 
            this.cmdLoadCsvForm.Location = new System.Drawing.Point(271, 201);
            this.cmdLoadCsvForm.Name = "cmdLoadCsvForm";
            this.cmdLoadCsvForm.Size = new System.Drawing.Size(110, 23);
            this.cmdLoadCsvForm.TabIndex = 1;
            this.cmdLoadCsvForm.Text = "Load CSV Form";
            this.cmdLoadCsvForm.Click += new System.EventHandler(this.cmdLoadCsvForm_Click);
            // 
            // cmdLoadCsv
            // 
            this.cmdLoadCsv.Location = new System.Drawing.Point(384, 201);
            this.cmdLoadCsv.Name = "cmdLoadCsv";
            this.cmdLoadCsv.Size = new System.Drawing.Size(75, 23);
            this.cmdLoadCsv.TabIndex = 1;
            this.cmdLoadCsv.Text = "Load CSV";
            this.cmdLoadCsv.Click += new System.EventHandler(this.cmdLoadCsv_Click);
            // 
            // lvwCountryCodes
            // 
            this.lvwCountryCodes.DataMember = null;
            this.lvwCountryCodes.Location = new System.Drawing.Point(18, 29);
            this.lvwCountryCodes.Name = "lvwCountryCodes";
            this.lvwCountryCodes.Size = new System.Drawing.Size(441, 160);
            this.lvwCountryCodes.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(18, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Find Phone Code:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(163, 324);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(218, 20);
            this.txtPhone.TabIndex = 1;
            // 
            // cmdPhone
            // 
            this.cmdPhone.Location = new System.Drawing.Point(384, 322);
            this.cmdPhone.Name = "cmdPhone";
            this.cmdPhone.Size = new System.Drawing.Size(75, 23);
            this.cmdPhone.TabIndex = 2;
            this.cmdPhone.Text = "Find";
            this.cmdPhone.Click += new System.EventHandler(this.cmdPhone_Click);
            // 
            // cmdISO3166
            // 
            this.cmdISO3166.Location = new System.Drawing.Point(384, 349);
            this.cmdISO3166.Name = "cmdISO3166";
            this.cmdISO3166.Size = new System.Drawing.Size(75, 23);
            this.cmdISO3166.TabIndex = 2;
            this.cmdISO3166.Text = "Find";
            this.cmdISO3166.Click += new System.EventHandler(this.cmdISO3166_Click);
            // 
            // txtISO3166
            // 
            this.txtISO3166.Location = new System.Drawing.Point(163, 351);
            this.txtISO3166.Name = "txtISO3166";
            this.txtISO3166.Size = new System.Drawing.Size(218, 20);
            this.txtISO3166.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(18, 351);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Find ISO3166-1 Alpha 3:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(18, 379);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 23);
            this.label8.TabIndex = 0;
            this.label8.Text = "Find FIPS (2):";
            // 
            // txtFIPS
            // 
            this.txtFIPS.Location = new System.Drawing.Point(163, 379);
            this.txtFIPS.Name = "txtFIPS";
            this.txtFIPS.Size = new System.Drawing.Size(218, 20);
            this.txtFIPS.TabIndex = 1;
            // 
            // cmdFIPS
            // 
            this.cmdFIPS.Location = new System.Drawing.Point(384, 377);
            this.cmdFIPS.Name = "cmdFIPS";
            this.cmdFIPS.Size = new System.Drawing.Size(75, 23);
            this.cmdFIPS.TabIndex = 2;
            this.cmdFIPS.Text = "Find";
            this.cmdFIPS.Click += new System.EventHandler(this.cmdFIPS_Click);
            // 
            // cmdAll
            // 
            this.cmdAll.Location = new System.Drawing.Point(384, 416);
            this.cmdAll.Name = "cmdAll";
            this.cmdAll.Size = new System.Drawing.Size(75, 23);
            this.cmdAll.TabIndex = 2;
            this.cmdAll.Text = "Find";
            this.cmdAll.Click += new System.EventHandler(this.cmdAll_Click);
            // 
            // txtAll
            // 
            this.txtAll.Location = new System.Drawing.Point(163, 418);
            this.txtAll.Name = "txtAll";
            this.txtAll.Size = new System.Drawing.Size(218, 20);
            this.txtAll.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(18, 418);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "Find All of the Above:";
            // 
            // Form1
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Size = new System.Drawing.Size(604, 643);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Label label1;
        private Gizmox.WebGUI.Forms.TextBox txtFirstText;
        private Gizmox.WebGUI.Forms.GroupBox groupBox1;
        private Gizmox.WebGUI.Forms.Button cmdMatch;
        private Gizmox.WebGUI.Forms.Button cmdSoundex2;
        private Gizmox.WebGUI.Forms.Button cmdSoundex1;
        private Gizmox.WebGUI.Forms.Label label2;
        private Gizmox.WebGUI.Forms.TextBox txtSecondText;
        private Gizmox.WebGUI.Forms.Button cmdLevenshteinDistance;
        private Gizmox.WebGUI.Forms.Button cmdMetaphone;
        private Gizmox.WebGUI.Forms.GroupBox groupBox2;
        private Gizmox.WebGUI.Forms.Button cmdLoadCsv;
        private Gizmox.WebGUI.Forms.ListView lvwCountryCodes;
        private Gizmox.WebGUI.Forms.Button cmdLoadCsvForm;
        private Gizmox.WebGUI.Forms.Label label3;
        private Gizmox.WebGUI.Forms.TextBox txtCountryName;
        private Gizmox.WebGUI.Forms.Button cmdFindCountryName;
        private Gizmox.WebGUI.Forms.Button cmdFindFIFACode;
        private Gizmox.WebGUI.Forms.TextBox txtFIFACode;
        private Gizmox.WebGUI.Forms.Label label4;
        private Gizmox.WebGUI.Forms.Label label5;
        private Gizmox.WebGUI.Forms.TextBox txtTLD;
        private Gizmox.WebGUI.Forms.Button cmdTLD;
        private Gizmox.WebGUI.Forms.Button cmdPhone;
        private Gizmox.WebGUI.Forms.TextBox txtPhone;
        private Gizmox.WebGUI.Forms.Label label6;
        private Gizmox.WebGUI.Forms.Label label7;
        private Gizmox.WebGUI.Forms.TextBox txtISO3166;
        private Gizmox.WebGUI.Forms.Button cmdISO3166;
        private Gizmox.WebGUI.Forms.Button cmdFIPS;
        private Gizmox.WebGUI.Forms.TextBox txtFIPS;
        private Gizmox.WebGUI.Forms.Label label8;
        private Gizmox.WebGUI.Forms.Label label9;
        private Gizmox.WebGUI.Forms.TextBox txtAll;
        private Gizmox.WebGUI.Forms.Button cmdAll;
    }
}