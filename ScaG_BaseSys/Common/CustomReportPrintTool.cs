using System;
using System.IO;
using System.Windows.Forms;
using CNY_BaseSys.Properties;
using DevExpress.XtraBars;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using Microsoft.Office.Interop.Outlook;

namespace CNY_BaseSys.Common
{
    public class CustomReportPrintTool
    {
        private string _defaultFileNameExport = "";
        public string DefaultFileNameExport
        {
            get { return this._defaultFileNameExport;}
            set { this._defaultFileNameExport = value; }
        }

        private bool _isExportSuccess = false;
        public bool IsExportSuccess
        {
            get { return this._isExportSuccess; }
            set { this._isExportSuccess = value; }
        }



        private PdfExportOptions _exportOptionsPdf = null;
        public PdfExportOptions ExportOptionsPdf
        {
            get { return this._exportOptionsPdf; }
            set { this._exportOptionsPdf = value; }
        }


        private string _emailSubject = "";
        public string EmailSubject
        {
            get { return this._emailSubject; }
            set { this._emailSubject = value; }
        }
        private string _emailBodyHtml = "";
        public string EmailBodyHtml
        {
            get { return this._emailBodyHtml; }
            set { this._emailBodyHtml = value; }
        }
        
        public event AfterExportCustomReportHandler OnAfterExport = null;
        private readonly ReportPrintTool _pt;
        public CustomReportPrintTool(ReportPrintTool pt)
        {
            this._pt = pt;
            PrintBarManager bManager = _pt.PreviewForm.PrintBarManager;

            BarButtonItem batExportPdfEmail = new BarButtonItem(bManager, "");
            batExportPdfEmail.Hint = @"Send Email (PDF File)";
            batExportPdfEmail.Id = bManager.GetNewItemId();
            batExportPdfEmail.ImageOptions.Image = Resources.sendpdf_16x16;
            batExportPdfEmail.ImageOptions.LargeImage = Resources.sendpdf_32x32;
            batExportPdfEmail.Name = "batExportPdfEmail";
            batExportPdfEmail.ItemClick += BatExportPdfEmail_ItemClick;
            Bar toolBar = bManager.Bars["Toolbar"];
            BarItemLink itemLink = toolBar.ItemLinks.Insert(toolBar.ItemLinks.Count - 1, batExportPdfEmail);
            itemLink.BeginGroup = true;
        }

        private void BatExportPdfEmail_ItemClick(object sender, ItemClickEventArgs e)
        {

            ExportPdfAndSendEmail();
        }



        public bool ExportPdfAndSendEmail()
        {
            var f = new SaveFileDialog
            {
                Title = @"Export Data",
                Filter = @"Pdf files (*.pdf)|*.pdf",
                RestoreDirectory = true
            };
            if (!string.IsNullOrEmpty(_defaultFileNameExport))
            {
                f.FileName = _defaultFileNameExport;
            }

            if (f.ShowDialog() != DialogResult.OK)
            {
                _isExportSuccess = false;
                return false;
            }


            string pathExport = f.FileName;
            try
            {
                if (_exportOptionsPdf != null)
                {
                    _pt.PrintingSystem.ExportToPdf(pathExport, _exportOptionsPdf);
                }
                else
                {
                    _pt.PrintingSystem.ExportToPdf(pathExport);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                _isExportSuccess = false;
                return false;
            }



            Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            oMailItem.BodyFormat = OlBodyFormat.olFormatHTML;
            oMailItem.Subject = _emailSubject;
            // oMailItem.Body = _emailBody;
            oMailItem.HTMLBody = _emailBodyHtml;
            oMailItem.Attachments.Add(pathExport, OlAttachmentType.olByValue, 1, Path.GetFileName(pathExport));
            oMailItem.Display(true);
            _isExportSuccess = true;

            OnAfterExport?.Invoke(this, new EventArgs());


            return true;
        }
    }
}
