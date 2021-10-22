#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI;
using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Forms;

#endregion

namespace VWG.Community.Forms
{
    public partial class MessageBox2Form : Form
    {
        [ToolboxItem(false), Serializable()]
        internal class MessageBoxButton : Button
        {
            private bool mblnUserClick = false;

            protected override void FireEvent(IEvent objEvent)
            {
                if (objEvent.Type == "Click")
                {
                    mblnUserClick = true;
                }

                base.FireEvent(objEvent);

                if (objEvent.Type == "Click")
                {
                    mblnUserClick = false;
                }
            }

            // Used to determine if an even originated from a button click or from a key press (Enter / Esc).
            public bool UserClick
            {
                get { return mblnUserClick; }
                set { mblnUserClick = value; }
            }
        }

        #region Class Members

        [NonSerialized]
        private MessageBoxDefaultButton menmDefaultButton;
        private MessageBoxButtons menmButtons;
        //private double mintXFactor = 5.7;
        //private double mintYFactor = 15;

        #endregion Class Members

        #region C'Tor/D'Tor
        internal MessageBox2Form(string strText, string strCaption, MessageBoxButtons enmButtons, MessageBoxIcon enmIcon, MessageBoxDefaultButton enmDefaultButton, MessageBoxOptions enmOptions, Font font = null)
			: this((Form)Global.Context.ActiveForm, strText, strCaption, enmButtons, enmIcon, enmDefaultButton, enmOptions, font)
		{
        }

        internal MessageBox2Form(Form objOwner, string strText, string strCaption, MessageBoxButtons enmButtons, MessageBoxIcon enmIcon, MessageBoxDefaultButton enmDefaultButton, MessageBoxOptions enmOptions, Font font = null)
			: base(objOwner == null ? ((Form)Global.Context.ActiveForm).Context : objOwner.Context)
		{
            InitializeComponent();

            Font defaultFont = objOwner == null ? null : objOwner.Font;
            if (font != null)
            {
                defaultFont = font;
            }

            menmButtons = enmButtons;

            menmDefaultButton = enmDefaultButton;

            int intButtonCount = 0;

            #region Buttons

            this.AddTableLayoutRowStyle(this.mobjButtonsLayout, new RowStyle(SizeType.Absolute, 26F));
            this.AddTableLayoutColumnStyle(this.mobjButtonsLayout, new ColumnStyle(SizeType.Percent, 50F));

            #region Button1

            intButtonCount++;

            // Resetting the AcceptButton and CancelButton properties of the Form.
            this.AcceptButton = this.CancelButton = null;

            // Set the first button.
            this.mobjButton1 = new MessageBoxButton();

            switch (menmButtons)
            {
                case MessageBoxButtons.OK:
                    this.mobjButton1.Text = WGLabels.Ok;
                    this.mobjButton1.DialogResult = DialogResult.OK;

                    // Setting the AcceptButton and CancelButton to their relevant buttons.
                    this.AcceptButton = mobjButton1;
                    this.CancelButton = mobjButton1; // Using the newly created separate 
                    break;
                case MessageBoxButtons.OKCancel:
                    this.mobjButton1.Text = WGLabels.Ok;
                    this.mobjButton1.DialogResult = DialogResult.OK;

                    // Setting the AcceptButton to it's relevant button.
                    this.AcceptButton = mobjButton1;
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    this.mobjButton1.Text = WGLabels.Abort;
                    this.mobjButton1.DialogResult = DialogResult.Abort;
                    break;

                case MessageBoxButtons.RetryCancel:
                    this.mobjButton1.Text = WGLabels.Retry;
                    this.mobjButton1.DialogResult = DialogResult.Retry;
                    break;

                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.YesNoCancel:
                    this.mobjButton1.Text = WGLabels.Yes;
                    this.mobjButton1.DialogResult = DialogResult.Yes;
                    //this.mobjButton3 = null;
                    break;
            }
            this.AddTableLayoutColumnStyle(this.mobjButtonsLayout, new ColumnStyle(SizeType.Absolute, 76F));
            this.mobjButtonsLayout.Controls.Add(this.mobjButton1, 1, 0);

            #endregion

            #region Button2

            // Add the second button only if not OK.
            if (menmButtons != MessageBoxButtons.OK)
            {
                intButtonCount++;

                this.mobjButton2 = new MessageBoxButton();
                switch (menmButtons)
                {
                    case MessageBoxButtons.OKCancel:
                    case MessageBoxButtons.RetryCancel:
                        this.mobjButton2.Text = WGLabels.Cancel;
                        this.mobjButton2.DialogResult = DialogResult.Cancel;

                        // Setting the CancelButton to it's relevant button.
                        this.CancelButton = mobjButton2;
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                        this.mobjButton2.Text = WGLabels.Retry;
                        this.mobjButton2.DialogResult = DialogResult.Retry;
                        break;
                    case MessageBoxButtons.YesNo:
                    case MessageBoxButtons.YesNoCancel:
                        this.mobjButton2.Text = WGLabels.No;
                        this.mobjButton2.DialogResult = DialogResult.No;
                        break;
                }
                this.AddTableLayoutColumnStyle(this.mobjButtonsLayout, new ColumnStyle(SizeType.Absolute, 6F));
                this.AddTableLayoutColumnStyle(this.mobjButtonsLayout, new ColumnStyle(SizeType.Absolute, 76F));
                this.mobjButtonsLayout.Controls.Add(this.mobjButton2, 3, 0);
            }

            #endregion

            #region Button3

            // Add the third button if is needed.
            if (menmButtons == MessageBoxButtons.AbortRetryIgnore || menmButtons == MessageBoxButtons.YesNoCancel)
            {
                intButtonCount++;

                this.mobjButton3 = new MessageBoxButton();
                switch (menmButtons)
                {
                    case MessageBoxButtons.AbortRetryIgnore:
                        this.mobjButton3.Text = WGLabels.Ignore;
                        this.mobjButton3.DialogResult = DialogResult.Ignore;
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        this.mobjButton3.Text = WGLabels.Cancel;
                        this.mobjButton3.DialogResult = DialogResult.Cancel;

                        // Setting the CancelButton to it's relevant button.
                        this.CancelButton = mobjButton3;
                        break;
                }
                this.AddTableLayoutColumnStyle(this.mobjButtonsLayout, new ColumnStyle(SizeType.Absolute, 6F));
                this.AddTableLayoutColumnStyle(this.mobjButtonsLayout, new ColumnStyle(SizeType.Absolute, 76F));
                this.mobjButtonsLayout.Controls.Add(this.mobjButton3, 5, 0);
            }

            #endregion

            this.AddTableLayoutColumnStyle(this.mobjButtonsLayout, new ColumnStyle(SizeType.Percent, 50F));

            #endregion

            #region Icon

            if (enmIcon != MessageBoxIcon.None)
            {
                this.mobjLabelText.Text = enmIcon.ToString();
                mobjIcon.Image = new SkinResourceHandle(typeof(MessageBox), enmIcon.ToString() + ".gif");
            }
            else
            {
                // Remove icon layout
                this.Controls.Remove(this.mobjIconLayout);
            }

            #endregion

            #region Texts

            // Set description and caption - text values.
            this.mobjLabelText.Text = strText;
            this.Text = strCaption;

            if (this.Context != null && this.Context.MainForm != null)
            {
                objOwner = this.Context.MainForm as Form;
            }

            // Measure the description's text size.
            Size objTextsize = CommonUtils.GetStringMeasurements(strText, this.mobjLabelText.Font, objOwner.Width - (enmIcon == MessageBoxIcon.None ? 0 : mobjIcon.Width));

            // Calculate the messagebox sizes.
            int intWidth = Math.Max(GetMinimalWidthForButtonsLayout(), objTextsize.Width) + (enmIcon == MessageBoxIcon.None ? 0 : mobjIconLayout.Width) + (this.Padding.All * 2);
            int intHeight = mobjButtonsLayout.Height + Math.Max((enmIcon == MessageBoxIcon.None ? 0 : mobjIcon.Height), objTextsize.Height) + 50;

            // Set the messagebox's calculated size.
            this.SuspendLayout();
            this.Size = new Size(intWidth, intHeight);
            this.ClientSize = new Size(intWidth, intHeight);
            this.ResumeLayout(false);

            #endregion

            if (defaultFont != null)
            {
                this.Font = defaultFont;

                if (this.mobjButton1 != null) this.mobjButton1.Font = defaultFont;
                if (this.mobjButton2 != null) this.mobjButton2.Font = defaultFont;
                if (this.mobjButton3 != null) this.mobjButton3.Font = defaultFont;

                if (this.mobjLabelText != null) this.mobjLabelText.Font = defaultFont;
            }

            if (enmButtons == MessageBoxButtons.YesNo || enmButtons == MessageBoxButtons.AbortRetryIgnore)
            {
                this.CloseBox = false;
            }
        }
        #endregion C'Tor/D'Tor

        private void Form_Load(object sender, EventArgs e)
        {
            // Creating a temporary button object reference.
            Button objButton = null;

            // The temporary button gets the default button object.
            switch (menmDefaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    objButton = mobjButton1;
                    break;
                case MessageBoxDefaultButton.Button2:
                    objButton = mobjButton2;
                    break;
                case MessageBoxDefaultButton.Button3:
                    objButton = mobjButton3;
                    break;
            }

            // If it is a valid button.
            if (objButton != null)
            {
                // Set focus to this (default) button.
                objButton.Focus();
            }
        }

        #region Methods
        /// <summary>
        /// Adds the table layout column style.
        /// </summary>
        /// <param name="objTableLayoutPanel">The obj table layout panel.</param>
        /// <param name="objColumnStyle">The obj column style.</param>
        private void AddTableLayoutColumnStyle(TableLayoutPanel objTableLayoutPanel, ColumnStyle objColumnStyle)
        {
            if (objTableLayoutPanel != null && objColumnStyle != null)
            {
                objTableLayoutPanel.ColumnStyles.Add(objColumnStyle);
                objTableLayoutPanel.ColumnCount += 1;
            }
        }

        /// <summary>
        /// Adds the table layout row style.
        /// </summary>
        /// <param name="objTableLayoutPanel">The obj table layout panel.</param>
        /// <param name="objRowStyle">The obj row style.</param>
        private void AddTableLayoutRowStyle(TableLayoutPanel objTableLayoutPanel, RowStyle objRowStyle)
        {
            if (objTableLayoutPanel != null && objRowStyle != null)
            {
                objTableLayoutPanel.RowStyles.Add(objRowStyle);
                objTableLayoutPanel.RowCount += 1;
            }
        }

        /// <summary>
        /// Gets the minimal width for the buttons layout.
        /// </summary>
        /// <returns></returns>
        private int GetMinimalWidthForButtonsLayout()
        {
            int intMinimalWidthForButtonsLayout = 0;

            if (mobjButtonsLayout != null)
            {
                foreach (ColumnStyle objColumnStyle in mobjButtonsLayout.ColumnStyles)
                {
                    intMinimalWidthForButtonsLayout += Convert.ToInt32(objColumnStyle.Width);
                }
            }

            return intMinimalWidthForButtonsLayout;
        }

        #endregion Methods
    }
}