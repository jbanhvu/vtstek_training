using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using CNY_BaseSys.Common;
using CNY_Report.Common;
using CNY_WH.Common;
using CNY_WH.Info;
using DevExpress.XtraReports.UI;

namespace CNY_WH.Report
{
    public partial class RptStockIssue : DevExpress.XtraReports.UI.XtraReport
    {
        readonly InfoWhReports _infRep = new InfoWhReports();
        public RptStockIssue(long lPk, int iFg)
        {
            InitializeComponent();
            DataTable dtStkIss = _infRep.LoadRepStockReceive164(lPk);
            this.DataSource = dtStkIss;
            //xrcPrintedDate.Text = string.Format("{0: dd-MMM-yyyy hh:ss}", Cls001MasterFiles.GetServerDate());

            double dTamt = dtStkIss.Rows.Cast<DataRow>().Sum(drRow => ProcessGeneral.GetSafeDouble(drRow["Amt"]));
            dTamt = dTamt < 0 ? -dTamt : dTamt;
            xrcSay.Text = NumberInWord.So_chu(dTamt);
        }
    }
}
