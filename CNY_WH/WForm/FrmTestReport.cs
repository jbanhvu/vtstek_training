using CNY_WH.Info;
using CNY_WH.Report;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_WH.WForm
{
    public partial class FrmTestReport : Form
    {
        Inf_002ReceiptNote inf;
        public FrmTestReport()
        {
            InitializeComponent();
            inf = new Inf_002ReceiptNote();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Lay du lieu tu database
            DataTable dtHeader = new DataTable();
            dtHeader = inf.sp_GoodsReceivedNote_Select(1);

            DataTable dtDetail = new DataTable();
            dtDetail = inf.sp_GoodsReceivedNoteDetail_Select(1);

            DataTable dtSignature=new DataTable();
            dtSignature = inf.sp_ApprovalHistory_SelectUserSignature(1,1);

            //Goi bao baosp_ApprovalHistory_SelectUserSignature(1,1);
            ReportPrintTool printTool = new ReportPrintTool(new RptStockReceiveVTSTek(dtHeader, dtDetail, dtSignature));
            // Xuất báo cáo
            printTool.ShowPreview();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            DataTable dtHeader = new DataTable();
            dtHeader = inf.sp_GoodsDeliveryNote_Select(1);

            DataTable dtDetail = new DataTable();
            dtDetail = inf.sp_GoodsDeliveryNoteDetail_Select(1);

            DataTable dtSignature = new DataTable();
            dtSignature = inf.sp_ApprovalHistory_SelectUserSignature(1, 1);


            ReportPrintTool printTool = new ReportPrintTool(new RptStockShipOut(dtHeader, dtDetail, dtSignature));

            printTool.ShowPreview();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataTable dtHeader = new DataTable();
            dtHeader = inf.sp_GoodsDeliveryNote_Select(1);

            DataTable dtDetail = new DataTable();
            dtDetail = inf.sp_GoodsDeliveryNoteDetail_Select(1);

            DataTable dtSignature = new DataTable();
            dtSignature = inf.sp_ApprovalHistory_SelectUserSignature(1, 1);


            ReportPrintTool printTool = new ReportPrintTool(new RptStockReceiveVT(dtHeader, dtDetail, dtSignature));

            printTool.ShowPreview();
        }
    }
}
