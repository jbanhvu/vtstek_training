using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_WH.Report;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace CNY_WH.WForm
{
    public partial class FrmWHPrint : XtraForm
    {
        public FrmWHPrint()
        {
            InitializeComponent();
        }

        public void PrintStockReceive(long lPk, int iFg)
        {
            RptStockReceive repStk = new RptStockReceive(lPk, iFg);
            ReportPrintTool pt = new ReportPrintTool(repStk);
            Form form = pt.PreviewForm;

            form.MdiParent = this.ParentForm;
            pt.ShowPreview();

            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        public void PrintStockVoucherIss(long lPk, int iFg)
        {
            RptStockIssue repStk = new RptStockIssue(lPk, iFg);
            ReportPrintTool pt = new ReportPrintTool(repStk);
            Form form = pt.PreviewForm;

            form.MdiParent = this.ParentForm;
            pt.ShowPreview();

            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        public void PrintStockVoucherTrans(long lPk, int iFg)
        {
            //RptStockTransfer repStk = new RptStockTransfer(lPk, iFg);
            //ReportPrintTool pt = new ReportPrintTool(repStk);
            //Form form = pt.PreviewForm;

            //form.MdiParent = this.ParentForm;
            //pt.ShowPreview();
            //form.Show();
        }


    }
}
