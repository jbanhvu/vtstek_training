using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CNY_Report.Info;
using DevExpress.XtraPrinting.BarCode;
using System.Data;
using DevExpress.XtraRichEdit;
using CNY_BaseSys.Common;

namespace CNY_Report
{
    public partial class Rpt003ProductionOrder : DevExpress.XtraReports.UI.XtraReport
    {
        private Int64 _pk;
        //private DataTable _dtPrint;
        
            Inf_003ProductionOrder _inf=new Inf_003ProductionOrder();

        public Rpt003ProductionOrder(Int64 _PK)
        {
            InitializeComponent();
            _pk = _PK;
            //_dtPrint = dtPrint;
            LoadData();
        }
        private void LoadData()
        {
            //Load header
            DataTable dtHeader = _inf.LoadHeader(_pk);

            this.ExportOptions.PrintPreview.DefaultFileName = string.Format("LSX_{0}_{1}", ProcessGeneral.GetSafeString(dtHeader.Rows[0]["No"]), ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DuAn"]));
            RichEditControl rE = new RichEditControl();
            rE.DocumentLoaded += RE_DocumentLoaded;
             xrBarCode1 = CreateQRCodeBarCode(dtHeader.Rows[0]["No"].ToString());
            xrBarCode1.Text = dtHeader.Rows[0]["No"].ToString();
            xrLabel5.Text = dtHeader.Rows[0]["No"].ToString();
            xrTableCell33.Text = dtHeader.Rows[0]["No"].ToString();
            xrTableCell10.Text= dtHeader.Rows[0]["SaleMen"].ToString();
            xrTableCell12.Text= dtHeader.Rows[0]["SaleSupervior"].ToString();
            //xrRichText1.Text = dtHeader.Rows[0]["ThongTinKyThuat"].ToString();
            xrRichText1.Rtf= dtHeader.Rows[0]["ThongTinKyThuat"].ToString();
            this.DataSource = dtHeader;
            rpt003ProductionOrderSub1 sub1=new rpt003ProductionOrderSub1(_pk);
            xrSubreport1.ReportSource = sub1;
            rpt003ProductionOrderSub2 sub2 = new rpt003ProductionOrderSub2(_pk);
            subQuyDinhChung.ReportSource = sub2;
        }

        private void RE_DocumentLoaded(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public XRBarCode CreateQRCodeBarCode(string BarCodeText)
        {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to QRCode.
            barCode.Symbology = new QRCodeGenerator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;

            // If the AutoModule property is set to false, uncomment the next line.
            barCode.AutoModule = true;
            // barcode.Module = 3;

            // Adjust the properties specific to the bar code type.
            ((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.AlphaNumeric;
            ((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;

            return barCode;
        }
    }
}
