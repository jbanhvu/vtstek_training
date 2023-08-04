using System;
using System.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using System.Data;
using System.Linq;
using CNY_BaseSys.Common;
using DevExpress.XtraPrinting;

namespace CNY_Report
{
    public partial class rpt002BoMReport : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt002BoMReport(DataTable dtHeader, DataTable dtThayDoiThongTin, DataTable dtPicture, GridControl gcAttribute, TreeList tlRawMaterialDtail, GridControl gcPaint, string projectNo, string projectName, 
            string productionOrder, GridControl gcAmount, Int32 layout, GridControl gcUpdate, GridControl gcInsert,bool dataInsert, bool dataUpdate, bool showHis, GridControl gcDelete, bool dataDelete)
        {
            InitializeComponent();
            printableComponentContainer1.PrintableComponent = tlRawMaterialDtail;
            printableComponentContainer2.PrintableComponent = gcPaint;

            printableComponentContainer3.PrintableComponent = gcAttribute;
            printableComponentContainer4.PrintableComponent = gcAmount;
            printableComponentContainer5.PrintableComponent = gcUpdate;
            printableComponentContainer6.PrintableComponent = gcInsert;
            printableComponentContainer7.PrintableComponent = gcDelete;

            this.DataSource = dtHeader;
            Int32 bomVersion = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["CNY004_Version"]);
            //xrBarCode1 = CreateQRCodeBarCode(dtHeader.Rows[0]["CNY015_BOMNo"].ToString());
            //xrBarCode1.Text = dtHeader.Rows[0]["CNY015_BOMNo"].ToString();
            //xrPicBarCode.Image = GetBarCodeImage1(dtHeader.Rows[0]["CNY015_BOMNo"].ToString());
          //  xrLabel5.Text = dtHeader.Rows[0]["CNY015_BOMNo"].ToString();
            xrBarCode2.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY015_BOMNo"]);
            if (dtPicture.Rows.Count > 0)
            {
                xrPicProduct.Image = GetImageData(dtPicture.Rows[0][0]);
            }
            lblRemark.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY005_Remark"]);

 

            //
            rpt002BoMReportSub1 sub1 = new rpt002BoMReportSub1(dtThayDoiThongTin);
            xrSubreport1.ReportSource = sub1;

            xrTableCell14.Text = projectNo + @"-" + projectName;
            //xrLabel6.Visible = false;
            //printableComponentContainer4.Visible = false;

            if (layout != 0)
            {
                SubBand1.Visible = false;
                xrLabel2.Text = @"II/ DIỆN TÍCH SƠN:";
                xrLabel7.Text = @"III/ LỊCH SỬ CẬP NHẬT:";
            }
            if (!showHis)
            {
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand5.Visible = false;
                SubBand6.Visible = false;
                return;
            }
            if (bomVersion == 0)
            {
          
                SubBand4.Visible = false;
                SubBand5.Visible = false;
                if (!dataDelete)
                {
                    SubBand3.Visible = false;
                    SubBand6.Visible = false;
                }
                return;
            }
            if ( !dataInsert && !dataUpdate && !dataDelete)
            {
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand5.Visible = false;
                SubBand6.Visible = false;
                return;
            }
            if(dataInsert && dataUpdate && dataDelete)
            {
                return;
            }
            VisibleSubBandHistoryInfo[] arrData = {new VisibleSubBandHistoryInfo
            {
                SubBandData = SubBand4,
                BoolValue = dataUpdate,
                LabelText = xrLabel9,
                StrText = "DỮ LIỆU CẬP NHẬT:"
            },new VisibleSubBandHistoryInfo
                {
                    SubBandData = SubBand5,
                    BoolValue = dataInsert,
                    LabelText = xrLabel10,
                    StrText = "DỮ LIỆU THÊM MỚI:"
                },new VisibleSubBandHistoryInfo
                {
                    SubBandData = SubBand6,
                    BoolValue = dataDelete,
                    LabelText = xrLabel11,
                    StrText = "DỮ LIỆU ĐÃ XÓA:"
                },  };


            var qVisible = arrData.Where(p => p.BoolValue).ToList();
            for (int i = 0; i < qVisible.Count; i++)
            {
                VisibleSubBandHistoryInfo item1 = qVisible[i];
                XRLabel lbl1 = item1.LabelText;
                lbl1.Text = string.Format("{0}. {1}", i + 1, item1.StrText);
            }

            var qUnVisible = arrData.Where(p => !p.BoolValue).ToList();
            foreach (VisibleSubBandHistoryInfo item2 in qUnVisible)
            {
                SubBand subBandT = item2.SubBandData;
                subBandT.Visible = false;
            }


           

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
