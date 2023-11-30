using CNY_BaseSys.Common;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace CNY_SI.Report
{
    public partial class Rpt001BusinessTrip : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt001BusinessTrip(DataTable dt,DataTable dtDetail)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds.Tables.Add(dtDetail); 
            this.DataSource = ds;

            //GroupHeader2
            StartAt_Date.Text = "Ngày " + ProcessGeneral.FormatDate(ProcessGeneral.GetSafeDatetimeOjectNull(dt.Rows[0]["StartAt"]), "dd") +
                                " tháng " + ProcessGeneral.FormatDate(ProcessGeneral.GetSafeDatetimeOjectNull(dt.Rows[0]["StartAt"]), "MM") +
                                " năm " + ProcessGeneral.FormatDate(ProcessGeneral.GetSafeDatetimeOjectNull(dt.Rows[0]["StartAt"]), "yyyy");
            EndAt_Date.Text = "Ngày " + ProcessGeneral.FormatDate(ProcessGeneral.GetSafeDatetimeOjectNull(dt.Rows[0]["EndAt"]), "dd") +
                                " tháng " + ProcessGeneral.FormatDate(ProcessGeneral.GetSafeDatetimeOjectNull(dt.Rows[0]["EndAt"]), "MM") +
                                " năm " + ProcessGeneral.FormatDate(ProcessGeneral.GetSafeDatetimeOjectNull(dt.Rows[0]["EndAt"]), "yyyy");
            Content.Text = ProcessGeneral.GetSafeString(dt.Rows[0]["Content"]);
            BTLocaiton.Text = ProcessGeneral.GetSafeString(dt.Rows[0]["Conclusion"]);

        }

    }
}
