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
    public partial class StoreGroupEdit : Form
    {
        private Mode _Mode = Mode.Read;
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManStoreGroup _StoreGroup = null;

        private List<IAzManStoreGroupMember> _Members = new List<IAzManStoreGroupMember>();
        private List<IAzManStoreGroupMember> _NonMembers = new List<IAzManStoreGroupMember>();

        #region public properties
        private bool _IsDirty = false;
        public bool IsDirty
        {
            get { return _IsDirty; }
            set { _IsDirty = value; }
        }
        #endregion

        public StoreGroupEdit()
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
            if (Session["selectedObject"] as IAzManStoreGroup != null)
            {
                _StoreGroup = this.Session["selectedObject"] as IAzManStoreGroup;
                _Store = _StoreGroup.Store;
            }

            _Mode = Mode.Update;

            SetAttributes();
            LoadTab();
        }

        private void SetAttributes()
        {
            Text = "Edit Store Group - " + _StoreGroup.Name;

            txtGroupType.ReadOnly = true;
            txtName.Focus();

            FormClosed += Form_Close;
            tabControl1.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Application;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = (TabControl)sender;
            LoadTab(tab.SelectedIndex);
        }

        private void LoadTab(int index = 0)
        {
            switch (index)
            {
                case 1:     // Member
                    #region Role
                    if (tbrMember.Buttons.Count == 0)
                    {
                        LoadMember_Toolbar();
                        LoadMember_ListViewHeader();
                        LoadMember_Data();
                    }
                    break;
                #endregion
                case 2:     // Non-Member
                    #region Task
                    if (tbrNonMember.Buttons.Count == 0)
                    {
                        LoadNonMember_Toolbar();
                        LoadNonMember_ListViewHeader();
                        LoadNonMember_Data();
                    }
                    break;
                #endregion
                case 0:
                default:    // Definition
                    #region definition
                    if (tbrDefinition.Buttons.Count == 0)
                    {
                        LoadDefinition_Toolbar();
                        LoadDefinition_Data();
                    }
                    break;
                    #endregion
            }
        }

        #region 共用 functions
        private void Common_ListView_LoadHeader(ref ListView listview, bool isMember)
        {
            IAzManStoreGroupMember[] members = isMember ?
                _StoreGroup.GetStoreGroupMembers() :
                _StoreGroup.GetStoreGroupNonMembers();


            listview.Items.Clear();
            var ln = 1;
            foreach (IAzManStoreGroupMember member in members)
            {
                String displayName = String.Empty;
                MemberType memberType = MemberType.AnonymousSID;
                memberType = member.GetMemberInfo(out displayName);

                if (member.IsMember == isMember)
                {
                    ListViewItem oItem = listview.Items.Add(displayName);
                    oItem.SubItems.Add(NetSqlAzManHelper.GetWhereDefinedName(member.WhereDefined));
                    oItem.SubItems.Add(member.SID.ToString());
                }
                if (isMember)
                    _Members.Add(member);
                else
                    _NonMembers.Add(member);
            }
            listview.Sort();
        }

        private void Common_ListView_LoadItems(ref ListView listview)
        {
            #region define header
            var colLN = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "#",
                Width = 30
            };
            var colItemName = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                SortOrder = SortOrder.Ascending,
                SortPosition = 1,
                Text = "Name",
                Width = 150
            };
            var colItemWhereDefined = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "Where Defined",
                Width = 150
            };
            var colItemId = new ColumnHeader()
            {
                ContentAlign = ExtendedHorizontalAlignment.Center,
                Text = "SID",
                TextAlign = HorizontalAlignment.Center,
                Width = 250
            };

            listview.Columns.AddRange(new ColumnHeader[] {
                colItemName, colItemWhereDefined, colItemId
            });
            #endregion

            listview.BackColor = Color.Transparent;
            //lvwRole.BorderStyle = BorderStyle.None;
            //listview.CheckBoxes = true;
            listview.Dock = DockStyle.Fill;
            listview.GridLines = true;
            listview.MultiSelect = false;

            listview.ListViewItemSorter = new ListViewItemSorter(listview);
            listview.SelectedIndexChanged += Common_ListView_SelectedIndexChanged;
            //lvwRole.DoubleClick += lvwRole_DoubleClick;

            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            listview.Tag = new Guid("1EF5B324-F82E-4F81-840F-395144DAE096");

            //xPort5.Controls.Utility.DisplayPreference.Load(ref lvwList);
        }

        private void Common_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            var cmdDelete =
                tabControl1.SelectedIndex == 1 ? tbrMember.Buttons[tbrMember.Buttons.Count - 1] :
                tabControl1.SelectedIndex == 2 ? tbrNonMember.Buttons[tbrNonMember.Buttons.Count - 1] : null;
            if (list.SelectedItem != null)
            {
                cmdDelete.Enabled = true;
            }

            else
            {
                cmdDelete.Enabled = false;
            }
        }

        private void Common_DeleteRecord(ListView listview, bool isMember = true)
        {
            if (listview.SelectedItem != null)
            {
                var item = listview.SelectedItem;

                try
                {
                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);

                    var sid = item.SubItems[2].Text;
                    var mbr = isMember ?
                        _Members.Where(x => x.SID.ToString() == sid).FirstOrDefault() :
                        _NonMembers.Where(x => x.SID.ToString() == sid).FirstOrDefault();

                    if (mbr != null)
                    {
                        _StoreGroup.GetStoreGroupMember(mbr.SID).Delete();
                    }

                    _Storage.CommitTransaction();
                }
                catch (Exception ex)
                {
                    _Storage.RollBackTransaction();
                    throw (ex);
                }
            }
        }
        #endregion

        #region tabDefinition functions
        private void LoadDefinition_Toolbar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdSave cmdDelete
            var cmdSave = new ToolBarButton("Save", ("Save"));
            cmdSave.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-check-circle-outline.png");

            tbrDefinition.Buttons.Add(cmdSave);
            #endregion

            tbrDefinition.ButtonClick += tbrDefinition_ButtonClick;
            tbrDefinition.Height = 24;
            tbrDefinition.TextAlign = ToolBarTextAlign.Right;
        }

        private void LoadDefinition_Data()
        {
            txtName.Text = _StoreGroup.Name;
            txtDescription.Text = _StoreGroup.Description;
            txtGroupType.Text = _StoreGroup.GroupType == GroupType.Basic ? "Basic Group" : "LDAP Group";
        }

        private void tbrDefinition_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "save":
                        SaveDefinition();
                        Close();
                        break;
                }
            }
        }

        private void SaveDefinition()
        {
            try
            {
                _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                _StoreGroup.Rename(txtName.Text.Trim());
                _StoreGroup.Update(txtDescription.Text.Trim(), _StoreGroup.GroupType);
                _Storage.CommitTransaction();
                _IsDirty = true;
            }
            catch (Exception ex)
            {
                _Storage.RollBackTransaction();
                throw ex;
            }
        }
        #endregion

        #region tabMember functions
        private void DeleteMember()
        {
            Common_DeleteRecord(lvwMember, true);
        }

        private void LoadMember_Toolbar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdAddStoreGroups cmdAddDBUsers
            var cmdAddStoreGroups = new ToolBarButton("AddStoreGroups", ("Add Store Groups"));
            cmdAddStoreGroups.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdAddDBUsers = new ToolBarButton("AddDBUsers", ("Add DB Users"));
            cmdAddDBUsers.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");
            #endregion

            #region cmdDelete
            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");
            cmdDelete.Enabled = false;
            #endregion

            tbrMember.Buttons.Add(cmdAddStoreGroups);
            tbrMember.Buttons.Add(cmdAddDBUsers);
            tbrMember.Buttons.Add(sep);
            tbrMember.Buttons.Add(cmdDelete);

            tbrMember.ButtonClick += tbrMember_ButtonClick;
            tbrMember.Height = 24;
            tbrMember.TextAlign = ToolBarTextAlign.Right;
        }

        private void LoadMember_ListViewHeader()
        {
            Common_ListView_LoadItems(ref lvwMember);
        }

        private void LoadMember_Data()
        {
            Common_ListView_LoadHeader(ref lvwMember, true);
        }

        private void tbrMember_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "delete":
                        DeleteMember();
                        LoadMember_Data();
                        break;
                    case "addstoregroups":
                        var sg = new StoreGroupsList();
                        sg.IsMember = true;
                        sg.FormClosed += MemberStoreGroup_FormClosed;
                        sg.ShowDialog();
                        break;
                    case "adddbusers":
                        var dbUser = new DBUsersList();
                        dbUser.IsMember = true;
                        dbUser.FormClosed += MemberDbUser_FormClosed;
                        dbUser.ShowDialog();
                        break;
                }
            }
        }

        private void MemberStoreGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (StoreGroupsList)sender;
            if (form.SelectedItems.Count > 0) LoadMember_Data();
        }

        private void MemberDbUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (DBUsersList)sender;
            if (form.SelectedItems.Count > 0) LoadMember_Data();
        }

        private void lvwMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmdDelete = tbrMember.Buttons[tbrMember.Buttons.Count - 1];
            if (this.lvwMember.SelectedItem != null)
            {
                cmdDelete.Enabled = true;
            }

            else
            {
                cmdDelete.Enabled = false;
            }
        }
        #endregion

        #region tabNonMember functions
        private void DeleteNonMember()
        {
            Common_DeleteRecord(lvwNonMember, false);
        }

        private void LoadNonMember_Toolbar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdAddStoreGroups cmdAddDBUsers
            var cmdAddStoreGroups = new ToolBarButton("AddStoreGroups", ("Add Store Groups"));
            cmdAddStoreGroups.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");

            var cmdAddDBUsers = new ToolBarButton("AddDBUsers", ("Add DB Users"));
            cmdAddDBUsers.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-pencil-outline.png");
            #endregion

            #region cmdDelete
            var cmdDelete = new ToolBarButton("Delete", ("Delete"));
            cmdDelete.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-delete.png");
            cmdDelete.Enabled = false;
            #endregion

            tbrNonMember.Buttons.Add(cmdAddStoreGroups);
            tbrNonMember.Buttons.Add(cmdAddDBUsers);
            tbrNonMember.Buttons.Add(sep);
            tbrNonMember.Buttons.Add(cmdDelete);

            tbrNonMember.ButtonClick += tbrNonMember_ButtonClick;
            tbrNonMember.Height = 24;
            tbrNonMember.TextAlign = ToolBarTextAlign.Right;
        }

        private void LoadNonMember_ListViewHeader()
        {
            Common_ListView_LoadItems(ref lvwNonMember);
        }

        private void LoadNonMember_Data()
        {
            Common_ListView_LoadHeader(ref lvwNonMember, false);
        }

        private void tbrNonMember_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "delete":
                        DeleteNonMember();
                        LoadNonMember_Data();
                        break;
                    case "addstoregroups":
                        var sg = new StoreGroupsList();
                        sg.IsMember = false;
                        sg.FormClosed += NonMemberStoreGroup_FormClosed;
                        sg.ShowDialog();
                        break;
                    case "adddbusers":
                        var dbUser = new DBUsersList();
                        dbUser.IsMember = false;
                        dbUser.FormClosed += NonMemberDbUser_FormClosed;
                        dbUser.ShowDialog();
                        break;
                }
            }
        }

        private void NonMemberStoreGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (StoreGroupsList)sender;
            if (form.SelectedItems.Count > 0) LoadNonMember_Data();
        }

        private void NonMemberDbUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (DBUsersList)sender;
            if (form.SelectedItems.Count > 0) LoadNonMember_Data();
        }

        private void lvwNonMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmdDelete = tbrNonMember.Buttons[tbrNonMember.Buttons.Count - 1];
            if (this.lvwNonMember.SelectedItem != null)
            {
                cmdDelete.Enabled = true;
            }

            else
            {
                cmdDelete.Enabled = false;
            }
        }
        #endregion
    }
}