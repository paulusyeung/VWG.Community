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
#endregion

namespace VWG.Community.NetSqlAzMan.Forms
{
    public partial class Export : Form, IGatewayComponent
    {
        private IAzManStorage _Storage = null;
        private IAzManStore _Store = null;
        private IAzManApplication _Application = null;
        private IAzManApplicationGroup _ApplicationGroup = null;
        private IAzManItem _Role = null;

        String _FileName = "RT2020AzMan_{0}.xml";   // 用嘅時候再加 DateTime.Now.ToString("yyyyMMddhhmmss")

        #region public properties
        private AzManItemType _Type = AzManItemType.Storage;
        public AzManItemType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        #endregion

        public Export()
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

            Text = String.Format("Export - {0}", _Type.ToString());

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

            var cmdExport = new ToolBarButton("Export", ("Export"));
            cmdExport.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-export.png");

            var cmdReset = new ToolBarButton("Reset", ("Reset Options"));
            cmdReset.Image = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-reload.png");

            //toolbar.Buttons.Add(cmdSpace);
            //toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdReset);
            toolbar.Buttons.Add(sep);
            toolbar.Buttons.Add(cmdExport);
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
                    case "export":
                        ExportRecord();     // HACK: 好奇怪？做完之後個 toolbar 第一隻 button 唔正常，加多個 reset 頂當
                        break;
                    case "reset":
                        chkAuthorization.Checked = true;
                        chkWindowsUser.Checked = chkDbUser.Checked = false;
                        break;
                }
            }
        }

        private void ExportRecord()
        {
            try
            {
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

                byte[] result = this.doExport(objectsToExport.ToArray(), chkWindowsUser.Checked, chkDbUser.Checked, chkAuthorization.Checked);
                DownloadFile(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] doExport(IAzManExport[] objectsToExport, bool includeItemAuthorizations, bool includeDBUsers, bool includeAuthorizations)
        {
            MemoryStream ms = new MemoryStream();
            XmlWriter xw = null;
            xw = XmlWriter.Create(ms);
            this.BeginExport(xw);
            foreach (IAzManExport objectToExport in objectsToExport)
            {
                objectToExport.Export(xw, includeItemAuthorizations, includeDBUsers, includeAuthorizations, objectToExport);
            }
            this.EndExport(xw);
            xw.Flush();
            xw.Close();
            return ms.ToArray();
        }

        protected void BeginExport(XmlWriter xmlWriter)
        {
            xmlWriter.WriteComment("*************************************");
            xmlWriter.WriteComment(".NET SQL Authorization Manager (Ms-PL)");
            xmlWriter.WriteComment("*************************************");
            xmlWriter.WriteComment("http://netsqlazman.codeplex.com");
            xmlWriter.WriteComment("Andrea Ferendeles");
            xmlWriter.WriteComment("*************************************");
            xmlWriter.WriteComment(String.Format("Creation Date: {0}", DateTime.Now.ToString()));
            xmlWriter.WriteComment(String.Format("NetSqlAzMan Run-Time version: {0}", _Storage.GetType().Assembly.GetName().Version.ToString()));
            xmlWriter.WriteComment(String.Format("NetSqlAzMan Database version: {0}", _Storage.DatabaseVesion));
            xmlWriter.WriteComment("*************************************");
            xmlWriter.WriteStartElement("NetSqlAzMan");
        }

        protected void EndExport(XmlWriter xmlWriter)
        {
            xmlWriter.WriteEndElement();
            xmlWriter.WriteComment("*************************************");
            xmlWriter.WriteComment(".NET SQL Authorization Manager (Ms-PL)");
            xmlWriter.WriteComment("*************************************");
        }

        private void DownloadFile(byte[] source)
        {
            //String filepath = Path.Combine(VWGContext.Current.Config.GetDirectory("UserData"), filename);
            //String filepath = Path.Combine(Path.GetTempPath(), filename);

            //File.WriteAllBytes(filepath, source);
            /**
            if (File.Exists(filepath))
            {
                //Link.Open(new GatewayReference(this, filename));

                var dl = new Helper.FileDownloadGateway();
                dl.Filename = filename;
                dl.SetContentType(Helper.DownloadContentType.OctetStream);
                dl.StartFileDownload(this, filepath);
            }
            */
            /**
            using (var stream = new MemoryStream(source))
            {
                stream.Position = 0;
                var dl = new Helper.FileDownloadGateway();
                dl.Filename = filename;
                dl.SetContentType(Helper.DownloadContentType.OctetStream);
                dl.StartStreamDownload(this, stream);
            }
            */
            
            var dl = new Helper.FileDownloadGateway();
            dl.Filename = String.Format(_FileName, DateTime.Now.ToString("yyyyMMddhhmmss"));
            dl.SetContentType(Helper.DownloadContentType.OctetStream);
            dl.StartBytesDownload(this, source);

            /**
            using (var stream = new MemoryStream(source))
            {
                var viewer = new Viewer();
                viewer.ReportName = filename;
                viewer.BinarySource = stream;
                viewer.Show();
            }
            */
            //Link.Open(new GatewayReference(this, filename));
        }
    }
}