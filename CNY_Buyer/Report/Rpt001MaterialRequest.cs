using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using CNY_BaseSys.Common;

namespace CNY_Buyer
{
    public partial class Rpt001PurchaseRequisition : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt001PurchaseRequisition(DataTable dt, DataTable dtHeader, DataTable dtSignature)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            this.DataSource = ds;

            Decimal total = ProcessGeneral.GetSafeDecimal(CalculateColumnSum(dt, "Amount"));
            cellTotal.Text = total.ToString("N0");
            cellTotalInText.Text = ProcessGeneral.NumberToText(total);

            lblPaymentmethod.Text += ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PaymentMethodName"]);
            cellNCC.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["SupplierName"]);
            cellAddress.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Address"]);
            cellPhone.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Phone"]);
            this.lblRightCorner.Text = @"Ngày Ban Hành: " +ProcessGeneral.FormatDate( ProcessGeneral.GetSafeDatetimeOjectNull(dtHeader.Rows[0]["Created_Date"]),"dd/MM/yyyy")+
            "\r\nLần Ban Hành: " +
                "\r\nLần cập nhật: "+
                "\r\nSố: " + ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PK"]);
            foreach(DataRow dr in dtSignature.Rows)
            {
                int Level = ProcessGeneral.GetSafeInt(dr["level"]);
                string FullName = ProcessGeneral.GetSafeString(dr["FullName"]);
                object Signature = dr["Signature"];
                LoadSignature(Level, FullName, Signature);
            }
        }
        public void LoadSignature(int Level, string fullname, object img)
        {
            XRLabel lbl =GroupFooter1.FindControl("lblLevel"+Level.ToString(),true) as XRLabel;
            if(lbl!=null)
            {
                lbl.Text = fullname;
            }
            XRPictureBox pic = GroupFooter1.FindControl("picLevel" + Level.ToString(), true) as XRPictureBox;
            if (pic != null&& img!=null)
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

        // Tính tổng của một cột trong DataTable
        public Decimal CalculateColumnSum(DataTable dataTable, string columnName)
        {
            Decimal sum = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                // Kiểm tra xem cột có tồn tại trong DataTable hay không
                if (dataTable.Columns.Contains(columnName))
                {
                    Decimal value = ProcessGeneral.GetSafeDecimal(row[columnName]);
                    sum += value;
                }
                else
                {
                    throw new ArgumentException("Column not found in DataTable.");
                }
            }
            return sum;
        }

    }
}
