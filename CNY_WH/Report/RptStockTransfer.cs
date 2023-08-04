using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using CNY_BaseSys.Common;
using CNY_Report.Common;
using CNY_WH.Info;
using DevExpress.XtraReports.UI;

namespace CNY_WH.Report
{
    public partial class RptStockTransfer : DevExpress.XtraReports.UI.XtraReport
    {
        //readonly InfoWhReports _infRep = new InfoWhReports();
        public RptStockTransfer(long lPk, int iFg)
        {
            InitializeComponent();
            //DataTable dtStkTransf =_infRep.LoadRepStockReceive128R(lPk, iFg);
            DataTable dtStkTransf = null;
            this.DataSource = dtStkTransf;
            
            double dTamt = dtStkTransf.Rows.Cast<DataRow>().Sum(drRow => ProcessGeneral.GetSafeDouble(drRow["Amt"]));
            dTamt = dTamt < 0 ? -dTamt : dTamt;
            xrcSay.Text = NumberInWord.So_chu(dTamt);
        }

    }
}
