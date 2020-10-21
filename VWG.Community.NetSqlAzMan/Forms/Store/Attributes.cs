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
using static VWG.Community.NetSqlAzMan.Helper.Enums;
using NetSqlAzMan.Interfaces;
using Gizmox.WebGUI.Common.Resources;
using VWG.Community.NetSqlAzMan.Helper;

#endregion

namespace VWG.Community.NetSqlAzMan.Forms.Store
{
    public partial class Attributes : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        //private IAzManApplication _Application = null;
        //private IAzManApplicationGroup _ApplicationGroup = null;
        //private IAzManItem _AzManItem = null;
        //private IAzManAuthorization _Authorization = null;
        //private String _DisplayName = "";

        #region public properties: AuthorizationID
        private int _AuthorizationID;
        public int AuthorizationID
        {
            get { return _AuthorizationID; }
            set { _AuthorizationID = value; }
        }

        private bool _Dirty = false;
        public bool Dirty
        {
            get { return _Dirty; }
            set { _Dirty = value; }
        }
        #endregion

        public Attributes()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _Storage = (IAzManStorage)Session["storage"];

            if (Session["selectedObject"] as IAzManStore != null)
            {
                _Store = Session["selectedObject"] as IAzManStore;
            }
            /**
            if (Session["selectedObject"] as IAzManApplication != null)
            {
                _Application = Session["selectedObject"] as IAzManApplication;
                _Store = _Application.Store;
            }
            if (Session["selectedObject"] as IAzManItem != null)
            {
                _AzManItem = Session["selectedObject"] as IAzManItem;
                _Application = _AzManItem.Application;
                _Store = _Application.Store;

                //_Authorization = _AuthItem.GetAuthorization(_AuthorizationID);
                //MemberType memberType = _Authorization.GetMemberInfo(out _DisplayName);
            }
            if (Session["selectedObject"] as IAzManApplicationGroup != null)
            {
                _ApplicationGroup = Session["selectedObject"] as IAzManApplicationGroup;
                _Application = _ApplicationGroup.Application;
                _Store = _Application.Store;
            }
            */

            SetAttributes();
            SetToolBar();
            SetListView();

            LoadList();
        }

        #region before form load
        private void SetAttributes()
        {
            toolbar.Height = 24;
            //lvwAuth.CheckBoxes = true;
            lvwAttributes.Dock = DockStyle.Fill;
            lvwAttributes.GridLines = true;
            lvwAttributes.MultiSelect = false;

            cmdAddNew.Image = new IconResourceHandle(GlobalVars.Theme + ".48.mdi-plus.circle.png");
            cmdAddNew.Cursor = Cursors.Hand;
            toolTip1.SetToolTip(cmdAddNew, "Add New");

            Text = "Attributes - " + _Store.Name;
        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdEdit cmdDelete
            var cmdEdit = new ToolBarButton("Edit", ("Edit"));
            cmdEdit.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-file-document-edit-outline.png");
            cmdEdit.Enabled = false;

            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");
            cmdDelete.Enabled = false;

            toolbar.Buttons.Add(cmdEdit);
            toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdDelete);
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void SetListView()
        {
            #region define header
            var colLN = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "#",
                Width = 30
            };
            var colItemType = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Key",
                SortOrder = SortOrder.Ascending,
                SortPosition = 1,
                Width = 200
            };
            var colItemName = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Value",
                Width = 200
            };
            var colAttributeId = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "ID",
                Visible = false,
                Width = 30
            };

            lvwAttributes.Columns.AddRange(new ColumnHeader[] {
                colItemType, colItemName, colAttributeId
            });
            #endregion

            lvwAttributes.BackColor = Color.Transparent;
            //listView.BorderStyle = BorderStyle.None;
            lvwAttributes.ListViewItemSorter = new ListViewItemSorter(lvwAttributes);
            lvwAttributes.Dock = DockStyle.Fill;
            lvwAttributes.GridLines = true;
            lvwAttributes.MultiSelect = false;
            lvwAttributes.SelectedIndexChanged += lvwAttributes_SelectedIndexChanged;
//            lvwAuth.DoubleClick += lvwAuth_DoubleClick;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvwAttributes.Tag = new Guid("D22BAA3D-7312-495F-811B-B46546A45B36");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }
        #endregion

        #region EditRecord DeleteRecord AddNew
        private void EditRecord()
        {
            if (lvwAttributes.SelectedItem != null)
            {
                var key = lvwAttributes.SelectedItem.SubItems[0].Text;
                var value = lvwAttributes.SelectedItem.SubItems[1].Text;
                var index = int.Parse(lvwAttributes.SelectedItem.SubItems[2].Text);
                var item = _Store.GetAttribute(key);

                var edit = new AttributeItem();
                edit.AttributeID = item.AttributeId;    // _AuthorizationID;
                //edit.AttributeID = item.AttributeId;
                edit.AttributeKey = item.Key;
                edit.Mode = Mode.Update;
                edit.FormClosed += AttributeRecord_FormClosed;
                edit.ShowDialog();
            }
        }

        private void DeleteRecord()
        {
            if (lvwAttributes.SelectedItem != null)
            {
                try
                {
                    var key = lvwAttributes.SelectedItem.SubItems[0].Text;
                    var value = lvwAttributes.SelectedItem.SubItems[1].Text;
                    var index = int.Parse(lvwAttributes.SelectedItem.SubItems[2].Text);

                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                    _Store.GetAttribute(key).Delete();
                    _Store.Storage.CommitTransaction();

                    LoadList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error found...Job aborted");
                }
            }
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "edit":
                        EditRecord();
                        break;
                    case "delete":
                        DeleteRecord();
                        break;
                }
            }
        }

        private void cmdAddNew_Click(object sender, EventArgs e)
        {
            var form = new AttributeItem();
            form.AttributeID = _AuthorizationID;
            form.Mode = Mode.Create;
            form.FormClosed += AttributeRecord_FormClosed;
            form.ShowDialog();
        }

        private void AttributeRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (AttributeItem)sender;
            if (form.Dirty) LoadList();
        }
        #endregion

        #region lvwAuth
        private void LoadList()
        {
            lvwAttributes.Items.Clear();

            IAzManAttribute<IAzManStore>[] attributes = _Store.GetAttributes();
            foreach (var attr in attributes)
            {
                ListViewItem oItem = lvwAttributes.Items.Add(attr.Key);
                oItem.SubItems.Add(attr.Value);
                oItem.SubItems.Add(attr.AttributeId.ToString());
            }

            lvwAttributes.Sort();
        }

        private void lvwAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = toolbar.Buttons.Count;

            var cmdEdit = toolbar.Buttons[index - 1];
            var cmdDelete = toolbar.Buttons[index - 3];

            if (lvwAttributes.SelectedItem != null)
            {
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
            }
            else
            {
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
            }
        }
        #endregion
    }
}