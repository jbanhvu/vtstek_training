using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraReports.UI;

namespace CNY_Report
{
    public partial class rpt002BoMReportSub1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt002BoMReportSub1(DataTable dtSource)
        {
            this.DataSource = dtSource;
            InitializeComponent();
        }

    }
}
