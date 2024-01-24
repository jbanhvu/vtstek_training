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
        public RptGatePassPaper(DataTable dt, DataTable dtStaff)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            this.DataSource = ds;

            txtName.Text = ProcessGeneral.GetSafeString(dtStaff.Rows[0]["Name"]);
            txtDepartment.Text = ProcessGeneral.GetSafeString(dtStaff.Rows[0]["Department"]);
            txtReason.Text = ProcessGeneral.GetSafeString(dt.Rows[0]["Note"]);
            txtGoOutTime.Text = Convert.ToString(ProcessGeneral.GetSafeDatetime(dt.Rows[0]["FromDate"]));
            txtGoInTime.Text = Convert.ToString(ProcessGeneral.GetSafeDatetime(dt.Rows[0]["ToDate"]));
        }
    }
}
