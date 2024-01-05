using CNY_BaseSys.Common;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CNY_SI.Report
{
    public partial class RptGatePass : DevExpress.XtraReports.UI.XtraReport
    {
        public RptGatePass(DataTable dtHeader, DataTable dtDetail, DataTable dtSignature)
        {
            InitializeComponent();
            //Header
            RequestUser.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["RequestUser"]);
            CreatedDate.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreatedDate"]);
            PK.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PK"]);

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
