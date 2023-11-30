using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using CNY_BaseSys.Common;
using CNY_Report.Common;
using CNY_WH.Info;
using DevExpress.XtraReports.UI;

namespace CNY_WH.Report
{
    public partial class RptMaterialReleaseOrder : DevExpress.XtraReports.UI.XtraReport
    {
        //readonly InfoWhReports _infRep = new InfoWhReports();
        public RptMaterialReleaseOrder(DataSet ds)
        {
            InitializeComponent();

            this.DataSource = ds;
            DataTable dtHeader=ds.Tables[0];
            DataTable dtDetail = ds.Tables[1];
            dtHeader.TableName = "Header";
            dtDetail.TableName = "Detail";
            //ds.Tables.Add(dtHeader);
            //ds.Tables.Add(dtDetail);
            this.DataMember = dtDetail.TableName;


            //double dTamt = dtDetail.Rows.Cast<DataRow>().Sum(drRow => ProcessGeneral.GetSafeDouble(drRow["Amt"]));
            //dTamt = dTamt < 0 ? -dTamt : dTamt;
            ////xrcSay.Text = NumberInWord.So_chu(dTamt);


            //========== Header =======================
            //picConfirm.Image = GetImageData(dtHeader.Rows[0]["ConfirmedBySignature"]);

            txtMRONo.Text =string.Format("Số: {0}", ProcessGeneral.GetSafeString(dtHeader.Rows[0]["MRONo"]));
            txtNguoiDeNghi.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["SenderFull"]);
            txtBoPhan.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["SenderFull"]);
            txtKhachHang.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["SenderFull"]);
            txtBoPhanSuDung.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["SenderFull"]);

            //========== Detail =======================
            cellLSX.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.ProductionOrder") });
            cellMaHang.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.ProductCode") });
            cellTenVT.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.ItemName") });
            cellMaVT.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.ItemCode") });
            cellDVT.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.UnitName") });
            cellSLYeuCau.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.NeededQty") });
            cellSLThucCapMoi.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.ActualQty") });
            cellGhiChu.DataBindings.AddRange(new[] { new XRBinding("Text", ds, "Detail.Remark") });

        }
        private Image GetImageData(object bytesArr)
        {
            if (bytesArr == DBNull.Value || bytesArr == null)
                return null;
            byte[] data = (byte[])bytesArr;
            return ProcessGeneral.ConvertByteArrayToImage(data);


        }


    }
}
