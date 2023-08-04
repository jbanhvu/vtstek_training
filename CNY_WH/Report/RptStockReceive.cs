using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_WH.Info;
using CNY_WH.Common;
using CNY_Report.Common;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace CNY_WH.Report
{
    public partial class RptStockReceive : DevExpress.XtraReports.UI.XtraReport
    {
        readonly InfoWhReports _infRep= new InfoWhReports();
        private readonly InfoWhMf _infMf = new InfoWhMf();

        public RptStockReceive(long lPk, int iFg)
        {
            InitializeComponent();
            #region Add Logo

            DataTable dtImageLogo = _infMf.LoadImageLogo();
            if (dtImageLogo.Rows.Count > 0)
            {
                Image imglogo = null;
                if (!string.IsNullOrEmpty(ProcessGeneral.GetSafeString(dtImageLogo.Rows[0]["Logo"])))
                {
                    imglogo = ProcessGeneral.ConvertByteArrayToImage((byte[])dtImageLogo.Rows[0]["Logo"]);

                }
                xrPicLogo.Image = imglogo;
                xrPicLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                xrPicLogo.ImageAlignment = ImageAlignment.MiddleCenter;
                xrPicLogo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.All)));
                xrPicLogo.Size = new Size(150, 60);
            }

            #endregion

            DataTable dtStkRec =_infRep.LoadRepStockReceive164(lPk); 
            this.DataSource = dtStkRec;
            //xrcPrintedDate.Text =string.Format("{0: dd-MMM-yyyy hh:ss}", Cls001MasterFiles.GetServerDate());

            double dTamt = dtStkRec.Rows.Cast<DataRow>().Sum(drRow => ProcessGeneral.GetSafeDouble(drRow["Amt"]));
            dTamt = dTamt < 0 ? -dTamt : dTamt;
            xrcSay.Text = NumberInWord.So_chu(dTamt);
        }
    }
}
