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
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace VWG.Community.FormsTest
{
    public partial class PagerBoxTest : Form
    {
        private PagerBox pagerbox = null;

        public PagerBoxTest()
        {
            InitializeComponent();
        }

        private void PagerBoxTest_Load(object sender, EventArgs e)
        {
            LoadPagerBox();
        }

        private void LoadPagerBox()
        {
            pagerbox = new PagerBox()
            {
                BackColor = Color.LightGray,
                Location = new Point(0, 0),
                Size = new Size(280, 32),       // 英文字，最細嘅 size
                RowsPerPage = 100,
                TotalRows = 1500,
                CurrentPage = 1,
                RowsPerPageLabel = "行/頁：",
                FirstPage = "Top",
                LastPage = "Bottom",
                PreviousPage = "Up",
                NextPage = "Down",
                IconFirstPage = "dark.mdi-page-first.20.circle.png",
                IconLastPage = "dark.mdi-page-last.20.circle.png",
                IconPreviousPage = "dark.mdi-chevron-left.20.circle.png",
                IconNextPage = "dark.mdi-chevron-right.20.circle.png"
            };
            pagerbox.FirstPageOnClick += Pagerbox_FirstPageOnClick;
            pagerbox.LastPageOnClick += Pagerbox_LastPageOnClick;
            pagerbox.PreviousPageOnClick += Pagerbox_PreviousPageOnClick;
            pagerbox.NextPageOnClick += Pagerbox_NextPageOnClick;

            pagerbox.CurrentPageOnTextChanged += Pagerbox_CurrentPageTextChanged;
            pagerbox.RowsPerPageOnSelectedIndexChanged += Pagerbox_RowsPerPageOnSelectedIndexChanged;
            this.Controls.Add(pagerbox);
        }

        private void Pagerbox_RowsPerPageOnSelectedIndexChanged(object sender, EventArgs e)
        {
            var cboRowsPerPage = sender as ComboBox;
            if (cboRowsPerPage != null)
            {
                var index = cboRowsPerPage.SelectedIndex;
                var text = cboRowsPerPage.SelectedItem.ToString();
                var rowsPerPage = 0;

                int.TryParse(text, out rowsPerPage);

                //MessageBox.Show(rowsPerPage.ToString());
                pagerbox.RowsPerPage = rowsPerPage;
                pagerbox.TotalRows = 2500;
                pagerbox.Update();
            }
        }

        private void Pagerbox_CurrentPageTextChanged(object sender, EventArgs e)
        {
            var txtCurrentPage = sender as TextBox;
            if (txtCurrentPage != null)
            {
                int currentPage = 0;
                if (int.TryParse(txtCurrentPage.Text, out currentPage))
                {
                    MessageBox.Show(currentPage.ToString());
                }
            }
        }

        private void Pagerbox_LastPageOnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Last Page");
        }

        private void Pagerbox_FirstPageOnClick(object sender, EventArgs e)
        {
            MessageBox.Show("First Page");
        }

        private void Pagerbox_PreviousPageOnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Previous Page");
        }

        private void Pagerbox_NextPageOnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Next Page");
        }
    }
}