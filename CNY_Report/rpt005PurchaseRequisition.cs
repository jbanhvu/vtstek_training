using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using CNY_BaseSys.Common;
using CNY_Report.Info;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;

namespace CNY_Report
{
    public partial class rpt005PurchaseRequisition : DevExpress.XtraReports.UI.XtraReport
    {

        public rpt005PurchaseRequisition(TreeList tl, DataTable dtHeader, DataTable dtSo, DataTable dtParent)
        {
            InitializeComponent();
          
            this.DataSource = dtHeader;

            rpt005PurchaseRequisitionSub1 sub1=new rpt005PurchaseRequisitionSub1(dtSo);
            xrSubreport1.ReportSource = sub1;

            printableComponentContainer1.PrintableComponent = tl;
            string strGroup = string.Join(" - ", dtParent.AsEnumerable()
                .Select(p => string.Format("{0}.{1}", p.Field<Int64>("ChildPK"), p.Field<string>("MainMaterialGroupD"))).ToArray()).Trim();


            if (!string.IsNullOrEmpty(strGroup))
            {
                strGroup = string.Format("({0})", strGroup);
            }
            lblMainGroup.Text = strGroup;
            lblPrNo.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PRNo"]);
            lblNgayBH.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreatedDate"]);
            lblVersion.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Version"]);
            lblNgayDuyet.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ApprovedDate"]);

            picNguoiTao.Image = GetImageData(dtHeader.Rows[0]["CreatedBySignature"]);
            picRelease.Image = GetImageData(dtHeader.Rows[0]["ReleasedBySignature"]);
            picApprove.Image = GetImageData(dtHeader.Rows[0]["ApprovedBySignature"]);

            lblNguoiTao.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreatedByFull"]);
            lblNgayTao.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreatedDate"]);

            lblReleaseBy.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ReleasedByFull"]);
            lblReleaseDate.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ReleasedDate"]);

            lblApproveBy.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ApprovedByFull"]);
            lblApproveDate.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ApprovedDate"]);
        }


        private Image GetImageData(object bytesArr)
        {
            if (bytesArr == DBNull.Value || bytesArr == null)
                return null;
            byte[] data = (byte[]) bytesArr;
            return ProcessGeneral.ConvertByteArrayToImage(data);


        }


    }
}
