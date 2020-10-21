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
using NetSqlAzMan;

#endregion

namespace VWG.Community.NetSqlAzMan.Forms.Definition
{
    public partial class BizRule : Form
    {
        private IAzManStorage _Storage;
        private IAzManStore _Store;
        private IAzManStoreGroup _StoreGroup = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _AzManItem = null;
        //private IAzManAuthorization _Authorization = null;
        //private String _DisplayName = "";

        #region public properties: AttributeID
        private Mode _Mode = Mode.Read;
        public Mode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        private int _AttributeID;
        public int AttributeID
        {
            get { return _AttributeID; }
            set { _AttributeID = value; }
        }

        private String _AttributeKey;
        public String AttributeKey
        {
            get { return _AttributeKey; }
            set { _AttributeKey = value; }
        }

        //private int _AuthorizationID;
        //public int AuthorizationID
        //{
        //    get { return _AuthorizationID; }
        //    set { _AuthorizationID = value; }
        //}

        private bool _Dirty = false;
        public bool Dirty
        {
            get { return _Dirty; }
            set { _Dirty = value; }
        }
        #endregion

        public BizRule()
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
            }
            if (Session["selectedObject"] as IAzManApplicationGroup != null)
            {
                _ApplicationGroup = Session["selectedObject"] as IAzManApplicationGroup;
                _Application = _ApplicationGroup.Application;
                _Store = _Application.Store;
            }

            _Mode = String.IsNullOrEmpty(_AzManItem.BizRuleSource) ? Mode.Create : Mode.Update;

            SetAttributes();
            SetToolBar();

            LoadBizRule();
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            Text = String.Format("Biz Rule - {0}", _AzManItem.Name);

            #region pre-select language
            if (_AzManItem.BizRuleSourceLanguage.HasValue)
            {
                if (_AzManItem.BizRuleSourceLanguage.Value == BizRuleSourceLanguage.VBNet)
                {
                    radVBNet.Checked = true;
                }
                else
                {
                    radCSharp.Checked = true;
                }
            }
            else
            {
                radCSharp.Checked = true;
            }
            #endregion

            #region Allow Editing for Admin only
            if (!_Application.IAmManager)
            {
                toolbar.Enabled = gbxLanguage.Enabled = false;
            }
            #endregion
        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdReload cmdClear cmdNew
            var cmdReload = new ToolBarButton("Reload", ("Reload Rule into Store"));
            cmdReload.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-reload.png");

            var cmdClear = new ToolBarButton("Clear", ("Clear Rule from Store"));
            cmdClear.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-notification-clear-all.png");

            var cmdNew = new ToolBarButton("New", ("New Biz Rule"));
            cmdNew.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-bell-plus.png");

            toolbar.Buttons.Add(cmdReload);
            toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdClear);
            toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdNew);
            #endregion

            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void LoadBizRule()
        {
            txtSource.Text = _Mode == Mode.Create ?
                @"(Choose a .NET language and press 'New Biz Rule' button to compose a new Business Rule, then press 'Reload rule into Store' to persist changes and compile Biz Rule.)" :
                _AzManItem.BizRuleSource; ;
        }

        private void ReloadBizRule()
        {
            try
            {
                _AzManItem.ReloadBizRule(txtSource.Text, radCSharp.Checked ? BizRuleSourceLanguage.CSharp : BizRuleSourceLanguage.VBNet);
            }
            catch { }
        }

        private void ClearBizRule()
        {
            txtSource.Text = String.Empty;
            _AzManItem.ClearBizRule();
        }

        private void NewBizRule()
        {
            string source = String.Empty;
            if (this.radCSharp.Checked)
            {
                #region Get from BizRuleCSharpTemplate.cs
                source =
@"using System;
using System.Security.Principal;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;

namespace {0}.BizRules
{
    public sealed class BizRule : IAzManBizRule
    {
        public BizRule()
        { }

        public bool Execute(Hashtable contextParameters, IAzManSid identity, IAzManItem ownerItem, ref AuthorizationType ForcedCheckAccessResult)
        {
            //Insert your code here
            return true;
        }
    }
}
";
                #endregion Get from BizRuleCSharpTemplate.cs
            }
            else
            {
                #region Get from BizRuleVBNetTemplate.vb
                source =
@"Imports System
Imports System.Security.Principal
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Collections.Generic
Imports System.Text
Imports NetSqlAzMan
Imports NetSqlAzMan.Interfaces

Namespace {0}.BizRules
    Public NotInheritable Class BizRule : Implements IAzManBizRule
        Public Sub New()
        End Sub

        Public Overloads Function Execute(ByVal contextParameters As Hashtable, ByVal identity As IAzManSid, ByVal ownerItem As IAzManItem, ByRef ForcedCheckAccessResult As AuthorizationType) As Boolean _
            Implements IAzManBizRule.Execute
            'Insert your code here
            Return True
        End Function
    End Class
End Namespace
";
                #endregion Get from BizRuleVBNetTemplate.vb
            }
            this.txtSource.Text = source.Replace("{0}", this.TransformToVariable("", _Application.Name, false));
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "new":
                        NewBizRule();
                        break;
                    case "clear":
                        break;
                    case "reload":
                        break;
                }
            }
        }

        private string TransformToVariable(string prefix, string name, bool toupper)
        {
            string ris = "";
            if (String.IsNullOrEmpty(name)) return String.Empty;
            char[] nc = name.ToCharArray();
            for (int i = 0; i < nc.Length; i++)
            {
                if (!char.IsLetterOrDigit(nc[i]) && (nc[i] != '_'))
                {
                    if (nc[i] != '[')
                    {
                        ris += "_";
                    }
                }
                else
                {
                    ris += nc[i].ToString();
                }
            }
            if (toupper)
            {
                ris = ris.ToUpper();
            }
            if (!char.IsLetter(ris.ToCharArray()[0]))
            {
                ris = "_" + ris;

            }
            ris = ris.Trim('_');
            ris = prefix + ris;
            return ris;
        }
    }
}