using System;
using System.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using System.Data;
using System.Linq;
using CNY_BaseSys.Common;
using DevExpress.XtraLayout.Helpers;
using DevExpress.XtraPrinting;


namespace CNY_BaseSys.WForm
{
    public partial class rpt002BoMReportFi : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt002BoMReportFi(DataTable dtHeader, TreeList tlRawMaterialDtail, GridControl gcPaint)
        {
            InitializeComponent();


            printableComponentContainer2.PrintableComponent = gcPaint;
            printableComponentContainer1.PrintableComponent = tlRawMaterialDtail;
          

       

            this.DataSource = dtHeader;
           
            xrBarCode2.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY015_BOMNo"]);
           
            lblRemark.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY005_Remark"]);

            lblVersion.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY004_Version"]);
            lblKhachHang.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CustomerSearchName"]);
            string projectName = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ProjectName"]).ToUpper();
            lblDuAn.Text = string.Format("DỰ ÁN : {0}", projectName);
            lblLSX.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ProductionOrderNo"]);
            lblQuyTrinh.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY015_BOMNo"]);
        
            //

            picNguoiTao.Image = GetImageData(dtHeader.Rows[0]["CNY006_CreatedBy_ST"]);
            picUpdate.Image = GetImageData(dtHeader.Rows[0]["CNY008_UpdatedBy_ST"]);


            lblNguoiTao.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY006_CreatedBy_FN"]);
            lblNgayTao.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY007_CreatedDate"]);

            lblUpdateBy.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY008_UpdatedBy_FN"]);
            lblUpdateDate.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY009_UpdatedDate"]);

            //SubBand1.KeepTogether = false;
            //SubBand1.CanGrow = false;
            //SubBand1.PageBreak = PageBreak.AfterBandExceptLastEntry;
            
      
        
        }

    

      

      

        private Image GetImageData(object bytesArr)
        {
            if (bytesArr == DBNull.Value || bytesArr == null)
                return null;
            byte[] data = (byte[])bytesArr;
            return ProcessGeneral.ConvertByteArrayToImage(data);


        }


    }

    public class VisibleSubBandHistoryInfo
    {
        public SubBand SubBandData { get; set; }
        public XRLabel LabelText { get; set; }
        public bool BoolValue { get; set; }
        public string StrText { get; set; }
    }
}
