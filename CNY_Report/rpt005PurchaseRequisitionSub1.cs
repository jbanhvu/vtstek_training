using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraReports.UI;

namespace CNY_Report
{
    public partial class rpt005PurchaseRequisitionSub1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt005PurchaseRequisitionSub1(DataTable dtSource)
        {
            InitializeComponent();
            this.DataSource = dtSource;
            xrCellProductionOrder1.DataBindings.AddRange(new[] {new XRBinding("Text", null, "dtSo.ProductionOrder") });
            xrCellCustomerSearch1.DataBindings.AddRange(new[] { new XRBinding("Text", null, "dtSo.SearchNameCust") });
           
            xrCellProjectNo1.DataBindings.AddRange(new[] { new XRBinding("Text", null, "dtSo.ProjectNo") });
            xrCellProjectName1.DataBindings.AddRange(new[] { new XRBinding("Text", null, "dtSo.ProjectName") });



            xrCellRef1.DataBindings.AddRange(new[] { new XRBinding("Text", null, "dtSo.Reference") });

            xrCellOrderQty1.DataBindings.AddRange(new[] { new XRBinding("Text", null, "dtSo.OrderQty") });
            xrCellPurchaseQty1.DataBindings.AddRange(new[] { new XRBinding("Text", null, "dtSo.PlanQty") });
        }

    }
}
