using CNY_BaseSys.Common;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace CNY_WH.Report
{
    public partial class RptGatePassPaper : DevExpress.XtraReports.UI.XtraReport
    {
        public RptGatePassPaper(DataTable dt)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            this.DataSource = ds;
            txtNote.Text = ProcessGeneral.GetSafeString(dt.Rows[0]["Note"]);
        }
    }
}
