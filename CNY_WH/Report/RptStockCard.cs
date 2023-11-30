using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_WH.Common;
using CNY_WH.Info;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace CNY_WH.Report
{
    public partial class RptStockCard : DevExpress.XtraReports.UI.XtraReport
    {
        readonly InfoWhReports _infRep = new InfoWhReports();
        private InfoWhMf _infMf = new InfoWhMf();

        public RptStockCard(string sCode, string sWh, DateTime dFr, DateTime dTo)
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

            DataTable dtStkCard = _infRep.LoadRepStockCard160(sCode, sWh, dFr, dTo);
            this.DataSource = dtStkCard;
        }

    }
}
