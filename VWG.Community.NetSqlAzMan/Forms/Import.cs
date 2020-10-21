#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using NetSqlAzMan.Interfaces;

using VWG.Community.NetSqlAzMan.Helper;
using static VWG.Community.NetSqlAzMan.Helper.Enums;
using NetSqlAzMan;
#endregion

namespace VWG.Community.NetSqlAzMan.Forms
{
    public partial class Import : Form
    {
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _Role = null;

        private List<String> _UploadedFiles = new List<String>();

        #region public properties
        private AzManItemType _Type = AzManItemType.Storage;
        public AzManItemType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        #endregion

        public Import()
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
                _Role = Session["selectedObject"] as IAzManItem;
                _Application = _Role.Application;
                _Store = _Application.Store;
            }
            if (Session["selectedObject"] as IAzManApplicationGroup != null)
            {
                _ApplicationGroup = Session["selectedObject"] as IAzManApplicationGroup;
                _Application = _ApplicationGroup.Application;
                _Store = _Application.Store;
            }

            SetAttributes();
            SetToolBar();
        }

        private void Form_Close(object sender, FormClosedEventArgs e)
        {
            //Session["selectedObject"] = _Application;
        }

        private void SetAttributes()
        {
            toolbar.Height = 24;

            Text = String.Format("Import - {0}", _Type.ToString());

            #region Open File Dialog
            fileUpload.Filter = "XML Files (*.xml)|*.xml;*.XML";     // 無效？
            fileUpload.Multiselect = false;
            fileUpload.MaxFileSize = 1024 * 10;             // in kb
            fileUpload.Title = String.Format("Upload {0} XML File", _Type.ToString());

            fileUpload.FileOk += new CancelEventHandler(this.fileUpload_FileOk);
            #endregion

            cmdSelectFile.Click += cmdSelectFile_Click;

            #region UploadControl
            uploadBox.AllowDrop = true;
            uploadBox.BackColor = Color.DarkSlateGray;
            //uploadBox.Dock = DockStyle.Fill;
            uploadBox.ForeColor = Color.WhiteSmoke;  //.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            uploadBox.UploadTempFilePath = Path.GetTempPath();
            uploadBox.UploadMaxFileSize = 1024 * 1000 * 10;
            uploadBox.UploadFileTypes = @"^.*\.(xml)$";                                 // default = @"^.*$"
            uploadBox.UploadMaxNumberOfFiles = 1;
            uploadBox.UploadTempFilePath = Path.GetTempPath();
            uploadBox.UploadText = "Select or drop files into the \"green zone\"";      // @"選出檔案或是拖拉檔案至此";

            uploadBox.UploadBatchCompleted += new UploadEventHandler(uploadBox_UploadBatchCompleted);
            uploadBox.UploadError += new UploadErrorHandler(uploadBox_UploadError);
            uploadBox.UploadFileCompleted += new UploadFileCompletedHandler(uploadBox_UploadFileCompleted);
            #endregion

            this.FormClosed += Form_Close;
        }

        private void SetToolBar()
        {
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdExport
            var cmdSpace = new ToolBarButton("Space", (" "));
            //cmdSpace.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-export.png");
            //cmdSpace.Enabled = false;

            var cmdImport = new ToolBarButton("Import", ("Import"));
            cmdImport.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-import.png");

            var cmdReset = new ToolBarButton("Reset", ("Reset Options"));
            cmdReset.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-reload.png");

            //toolbar.Buttons.Add(cmdSpace);
            //toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdReset);
            toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdImport);
            #endregion

            toolbar.ImageSize = new System.Drawing.Size(20, 20);
            toolbar.TextAlign = ToolBarTextAlign.Right;
            toolbar.ButtonClick += toolBar_ButtonClick;
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            var tb = (ToolBar)sender;
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "import":
                        #region Import XML
                        if (_UploadedFiles.Count > 0)
                        {
                            var result = ImportRecord();
                            if (result)
                            {
                                MessageBox.Show("Done!");
                            }
                            else
                            {
                                MessageBox.Show("Error found...Job aborted!");
                            }
                        }
                        break;
                        #endregion
                    case "reset":
                        ResetScreen();
                        break;
                }
            }
        }

        private void ResetScreen()
        {
            txtFileName.Text = String.Empty;
            chkAuthorizations.Checked = chkCreatesNewItems.Checked = chkCreatesNewItemAuthorizations.Checked = true;
            chkWindowsUsers.Checked = chkDBUsers.Checked = chkOverwritesExistingItems.Checked = chkDeleteMissingItems.Checked = chkOverwritesItemAuthorizations.Checked = chkDeleteMissingItemAuthorizations.Checked = false;
            _UploadedFiles.Clear();
        }

        private void cmdSelectFile_Click(object sender, EventArgs e)
        {
            fileUpload.ShowDialog();
        }

        #region Uploader Events
        private void fileUpload_FileOk(object sender, CancelEventArgs e)
        {
            string FileName = string.Empty;
            string FullName = string.Empty;
            string dropbox = Path.GetTempPath();

            OpenFileDialog oFileDialog = sender as OpenFileDialog;

            switch (oFileDialog.DialogResult)
            {
                case DialogResult.OK:
                    for (int i = 0; i < oFileDialog.Files.Count; i++)
                    {
                        HttpPostedFileHandle file = oFileDialog.Files[i] as HttpPostedFileHandle;
                        if (file.ContentLength > 0)
                        {
                            FileName = Path.GetFileName(file.PostedFileName);
                            FullName = Path.Combine(dropbox, FileName);
                            file.SaveAs(FullName);

                            txtFileName.Text = FileName;
                            _UploadedFiles.Add(FullName);
                        }
                    }
                    //BindFileExplorer();
                    this.Update();
                    break;
            }
        }

        private void uploadBox_UploadFileCompleted(object sender, UploadCompletedEventArgs e)
        {
            UploadFileResult result = e.Result;
            _UploadedFiles.Add(result.TempFileFullName);
        }

        private void uploadBox_UploadError(object sender, UploadErrorEventArgs e)
        {
            MessageBox.Show(String.Format("{0}?", e.Error), "Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                new EventHandler(uploadBox_ErrorClick));
        }

        private void uploadBox_ErrorClick(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {

            }
        }

        private void uploadBox_UploadBatchCompleted(object sender, EventArgs e)
        {
            //this.Close();
        }
        #endregion

        #region DoImport
        private bool ImportRecord()
        {
            bool result = false;
            try
            {
                /**
                List<IAzManExport> objectsToExport = new List<IAzManExport>();
                object selectedObject = Session["selectedObject"];

                switch (_Type)
                {
                    case AzManItemType.Storage:
                        objectsToExport.AddRange(_Storage.GetStores());
                        break;
                    case AzManItemType.ItemDefinitions:
                        objectsToExport.AddRange(((IAzManApplication)selectedObject).GetItems());
                        break;
                    default:
                        objectsToExport.Add((IAzManExport)selectedObject);
                        break;
                }
                */
                SqlAzManMergeOptions mergeOptions = SqlAzManMergeOptions.NoMerge;
                if (this.chkCreatesNewItems.Checked) mergeOptions |= SqlAzManMergeOptions.CreatesNewItems;
                if (this.chkOverwritesExistingItems.Checked) mergeOptions |= SqlAzManMergeOptions.OverwritesExistingItems;
                if (this.chkDeleteMissingItems.Checked) mergeOptions |= SqlAzManMergeOptions.DeleteMissingItems;
                if (this.chkCreatesNewItemAuthorizations.Checked) mergeOptions |= SqlAzManMergeOptions.CreatesNewItemAuthorizations;
                if (this.chkOverwritesItemAuthorizations.Checked) mergeOptions |= SqlAzManMergeOptions.OverwritesExistingItemAuthorization;
                if (this.chkDeleteMissingItemAuthorizations.Checked) mergeOptions |= SqlAzManMergeOptions.DeleteMissingItemAuthorizations;
                this.doImport(this.Session["selectedObject"], chkWindowsUsers.Checked, this.chkDBUsers.Checked, this.chkAuthorizations.Checked, mergeOptions);

                result = true;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return result;
        }

        public void doImport(object importIntoObject, bool chkUsersAndGroups, bool chkDBUsers, bool chkAuthorizations, SqlAzManMergeOptions mergeOptions)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_UploadedFiles[0]);
            XmlNode xmlStartNode;
            if (this.checkScopeNodePosition(doc, ref importIntoObject, out xmlStartNode))
            {
                try
                {
                    _Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
                    ((IAzManImport)importIntoObject).ImportChildren(
                        xmlStartNode, chkUsersAndGroups, chkDBUsers, chkAuthorizations, mergeOptions);
                    _Storage.CommitTransaction();
                }
                catch
                {
                    _Storage.RollBackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// Checks the scope node position.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="objectForImport">The object for import.</param>
        /// <param name="xmlStartNode">The XML start node.</param>
        /// <returns></returns>
        private bool checkScopeNodePosition(XmlDocument doc, ref object objectForImport, out XmlNode xmlStartNode)
        {
            string firstChildNodeNameMustBe = null;
            string firstChildNodeNameMustBeOr = String.Empty;
            if (objectForImport as IAzManStorage != null)
                firstChildNodeNameMustBe = "Store";
            if (objectForImport as IAzManStore != null)
            {
                firstChildNodeNameMustBe = "Application";
                firstChildNodeNameMustBeOr = "StoreGroups";
            }
            if (objectForImport as IAzManStore != null && _Type == AzManItemType.StoreGroups)   // this.menuItem == "Import Store Groups")
            {
                firstChildNodeNameMustBe = "StoreGroup";
                firstChildNodeNameMustBeOr = "StoreGroups";
            }
            if (objectForImport as IAzManApplication != null)
            {
                firstChildNodeNameMustBe = "Item";
                firstChildNodeNameMustBeOr = "ApplicationGroups";
            }
            if (objectForImport as IAzManApplication != null && _Type == AzManItemType.ApplicationGroups)   // this.menuItem == "Import Application Groups")
            {
                firstChildNodeNameMustBe = "ApplicationGroup";
                firstChildNodeNameMustBeOr = "ApplicationGroups";
            }
            if (objectForImport as IAzManApplication != null && _Type == AzManItemType.ItemDefinitions)     // this.menuItem == "Import Items")
            {
                firstChildNodeNameMustBe = "Item";
                firstChildNodeNameMustBeOr = "Items";
            }
            else if (String.IsNullOrEmpty(firstChildNodeNameMustBe)) throw new ArgumentException("objectForImport type not supported.");

            if (doc["NetSqlAzMan"] == null ||
                (doc["NetSqlAzMan"][firstChildNodeNameMustBe] == null && doc["NetSqlAzMan"][firstChildNodeNameMustBeOr] == null))
            {
                throw new System.Xml.Schema.XmlSchemaValidationException("The Xml file is not valid or wrong import position.");
            }
            else
            {
                xmlStartNode = doc["NetSqlAzMan"];
                return true;
            }
        }
        #endregion
    }
}