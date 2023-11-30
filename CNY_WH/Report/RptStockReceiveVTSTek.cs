using CNY_BaseSys.Common;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace CNY_WH.Report
{
    public partial class RptStockReceiveVTSTek : DevExpress.XtraReports.UI.XtraReport
    {
        public RptStockReceiveVTSTek(DataTable dtHeader, DataTable dtDetail, DataTable dtSignature)
        {
            InitializeComponent();
            //Header
            Supplier.Text =ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Supplier"]);
            Address.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Address"]);
            DeliveryName.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryName"]);
            DeliverPhone.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliverPhone"]);
            PurchaseRequistionPK.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PurchaseRequistionPK"]);
            CreatedDate.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreatedDate"]);
            UpdatedDate.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["UpdatedDate"]);
            Status.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Status"]);
            ProjectCode.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ProjectCode"]);

            //Detail
            DataSet ds = new DataSet();
            ds.Tables.Add(dtDetail);
            this.DataSource = ds;

            //Signature
            foreach (DataRow dr in dtSignature.Rows)
            {
                int Level = ProcessGeneral.GetSafeInt(dr["level"]);
                string FullName = ProcessGeneral.GetSafeString(dr["FullName"]);
                object Signature = dr["Signature"];
                int Status = ProcessGeneral.GetSafeInt(dr["Status"]);
                if (Status == 1)
                {
                    LoadSignature(Level, FullName, Signature);
                }
            }
        }
        public void LoadSignature(int Level, string fullname, object img)
        {
            XRLabel lbl = ReportFooter.FindControl("lblLevel" + Level.ToString(), true) as XRLabel;
            if (lbl != null)
            {
                lbl.Text = fullname;
            }
            XRPictureBox pic = ReportFooter.FindControl("picLevel" + Level.ToString(), true) as XRPictureBox;
            if (pic != null && img != null)
            {
                try
                {
                    pic.Image = ProcessGeneral.ConvertByteArrayToImage((byte[])img);
                }
                catch
                {

                }

            }

        }
    }
}
