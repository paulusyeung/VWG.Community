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

#endregion

namespace VWG.Community.Forms
{
    public partial class PagerBox : UserControl
    {
        #region public properties
        public int CurrentPage { get; set; }
        public int RowsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public string RowsPerPageLabel { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public string PreviousPage { get; set; }
        public string NextPage { get; set; }
        public string IconFirstPage { get; set; }
        public string IconLastPage { get; set; }
        public string IconPreviousPage { get; set; }
        public string IconNextPage { get; set; }
        #endregion

        public PagerBox()
        {
            InitializeComponent();
        }

        private void PagerBox_Load(object sender, EventArgs e)
        {
            SetCaptions();
            SetAttributes();
        }

        #region SetCaptions & SetAttribtues
        private void SetCaptions()
        {
            tooltip.SetToolTip(cmdFirstPage, FirstPage);
            tooltip.SetToolTip(cmdLastPage, LastPage);
            tooltip.SetToolTip(cmdPreviousPage, PreviousPage);
            tooltip.SetToolTip(cmdNextPage, NextPage);

            lblRowsPerPage.Text = RowsPerPageLabel;
        }

        private void SetAttributes()
        {
            cmdFirstPage.Text = cmdLastPage.Text = cmdPreviousPage.Text = cmdNextPage.Text = "";

            cboRowsPerPage.Size = new Size(44, 20);
            txtCurrentPage.Size = new Size(28, 16);

            cmdFirstPage.Image = new IconResourceHandle(IconFirstPage);
            cmdLastPage.Image = new IconResourceHandle(IconLastPage);
            cmdPreviousPage.Image = new IconResourceHandle(IconPreviousPage);
            cmdNextPage.Image = new IconResourceHandle(IconNextPage);

            cmdFirstPage.Click += cmdFirstPage_OnClick;
            cmdLastPage.Click += cmdFLastPage_OnClick;
            cmdPreviousPage.Click += cmdPreviousPage_OnClick;
            cmdNextPage.Click += cmdNextPage_OnClick;

            txtCurrentPage.TextChanged += txtCurrentPage_OnTextChanged;
            txtCurrentPage.Enter += (sender, args) =>
            {   // 進入，全選
                txtCurrentPage.SelectAll();
            };

            cboRowsPerPage.SelectedIndexChanged += cboRowsPerPage_OnSelectedIndexChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 1. 根據最新嘅 RowsPerPage 同埋 TotalRows，重新整理 CurrentPage, txtCurrentPage, lblTotalPages
        /// </summary>
        public override void Update()
        {
            #region 根據依家嘅 RowsPerPage 同埋 TotalRows，重新整理 CurrentPage, txtCurrentPage, lblTotalPages
            CurrentPage = 1;
            TotalPages = CalcTotalPages();

            txtCurrentPage.Text = CurrentPage.ToString();
            lblTotalPages.Text = string.Format("/{0}", TotalPages.ToString());
            #endregion
        }

        private int CalcTotalPages()
        {
            if (RowsPerPage != 0)
            {
                int quotient = TotalRows / RowsPerPage;
                int remainder = TotalRows % RowsPerPage;

                return remainder == 0 ? quotient : quotient + 1;
            }
            else
            {
                return 1;
            }
        }
        #endregion

        #region Events
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks First Page button")]
        public event EventHandler FirstPageOnClick;

        protected void cmdFirstPage_OnClick(object sender, EventArgs e)
        {
            FirstPageOnClick?.Invoke(sender, e);      //bubble the event up to the parent
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Last Page button")]
        public event EventHandler LastPageOnClick;

        protected void cmdFLastPage_OnClick(object sender, EventArgs e)
        {
            LastPageOnClick?.Invoke(sender, e);      //bubble the event up to the parent
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Previous Page button")]
        public event EventHandler PreviousPageOnClick;

        protected void cmdPreviousPage_OnClick(object sender, EventArgs e)
        {
            PreviousPageOnClick?.Invoke(sender, e);      //bubble the event up to the parent
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Next Page button")]
        public event EventHandler NextPageOnClick;

        protected void cmdNextPage_OnClick(object sender, EventArgs e)
        {
            NextPageOnClick?.Invoke(sender, e);      //bubble the event up to the parent
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user changes Current Page")]
        public event EventHandler CurrentPageOnTextChanged;

        protected void txtCurrentPage_OnTextChanged(object sender, EventArgs e)
        {
            CurrentPageOnTextChanged?.Invoke(sender, e);      //bubble the event up to the parent
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user changes Rows Per Page")]
        public event EventHandler RowsPerPageOnSelectedIndexChanged;

        protected void cboRowsPerPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RowsPerPageOnSelectedIndexChanged?.Invoke(sender, e);      //bubble the event up to the parent
        }
        #endregion
    }
}