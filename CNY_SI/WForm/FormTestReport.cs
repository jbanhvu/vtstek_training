using CNY_SI.Info;
using CNY_SI.Report;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CNY_SI.WForm
{
    public partial class FormTestReport : Form
    {
        private readonly Inf_001BusinessTrip _inf;
        private readonly Inf_001BusinessTripDetail _infDetail;
        public FormTestReport()
        {
            InitializeComponent();
            _inf = new Inf_001BusinessTrip();
            _infDetail = new Inf_001BusinessTripDetail();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dtHeader = new DataTable();
            dtHeader = _inf.sp_BusinessTrip_Select(1);

            DataTable dtDetail = new DataTable();
            dtDetail = _infDetail.sp_BusinessTripDetail_Select( Convert.ToInt64(dtHeader.Rows[0]["PK"]));

            DataTable dtSignature = new DataTable();
            dtSignature = _inf.sp_ApprovalHistory_SelectUserSignature(1, 1);

            //Goi bao baosp_ApprovalHistory_SelectUserSignature(1,1);
            ReportPrintTool printTool = new ReportPrintTool(new RptGatePass(dtHeader, dtDetail, dtSignature));
            // Xuất báo cáo
            printTool.ShowPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataTable dtHeader = new DataTable();
            dtHeader = _inf.sp_BusinessTrip_Select(1);

            DataTable dtDetail = new DataTable();
            dtDetail = _infDetail.sp_BusinessTripDetail_Select(Convert.ToInt64(dtHeader.Rows[0]["PK"]));

            DataTable dtSignature = new DataTable();
            dtSignature = _inf.sp_ApprovalHistory_SelectUserSignature(1, 1);
            ReportPrintTool printTool = new ReportPrintTool(new RptGatePass(dtHeader, dtDetail, dtSignature));
            // Xuất báo cáo
            printTool.ShowPreview();
        }
    }
}
