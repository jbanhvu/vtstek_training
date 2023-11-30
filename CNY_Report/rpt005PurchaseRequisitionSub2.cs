using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraReports.UI;

namespace CNY_Report
{
    public partial class rpt005PurchaseRequisitionSub2 : DevExpress.XtraReports.UI.XtraReport
    {
        private DataTable dt;
        public rpt005PurchaseRequisitionSub2(DataTable dtSource)
        {
            InitializeComponent();
            dt = dtSource;
            this.Detail.Controls.Add(CreateXRTableAgreement());
        }

        public XRTable CreateXRTableAgreement()
        {
            // Create an empty table and set its size.
            XRTable table = new XRTable();
            table.Width = 1102;

            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth = 1;
            table.Font = new Font(new FontFamily("Microsoft Sans Serif"), 5, FontStyle.Regular);
            int columnCount = dt.Columns.Count;
            // Create table row.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XRTableRow row = new XRTableRow();

            }
            return table;
        }
    }
}
