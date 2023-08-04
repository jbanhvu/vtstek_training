using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraReports.UI;
using CNY_Report.Info;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using System.IO;
using DevExpress.XtraPrinting;

namespace CNY_Report
{
    public partial class rpt003ProductionOrderSub1 : DevExpress.XtraReports.UI.XtraReport
    {
        Inf_003ProductionOrder _inf=new Inf_003ProductionOrder();
        Int64 _PK;
        public rpt003ProductionOrderSub1()
        {
            InitializeComponent();
        }
        public rpt003ProductionOrderSub1(Int64 PK)
        {
            _PK = PK;
            InitializeComponent();
            DataTable dtS = _inf.LoadSub1(PK);
            this.Detail.Controls.Add(CreateXRTableAgreement(dtS));
        }


        public XRTable CreateXRTableAgreement( DataTable dt)
        {
            XRTable table = new XRTable();
            table.Width = 1102;

            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth = 1;
            table.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)));
            table.Font = new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular);
            int columnCount = dt.Columns.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XRTableRow row = new XRTableRow();
                XRTableCell cellName = new XRTableCell();
                XRTableCell cellNameMaHang = new XRTableCell();
                XRTableCell cellReference = new XRTableCell();
                XRTableCell cellNameTenSP = new XRTableCell();
                XRPictureBox PictureBoxHinhSP = new XRPictureBox();
                XRTableCell cellNameHinhSP = new XRTableCell();
                XRTableCell cellNameDVT = new XRTableCell();
                XRTableCell cellNameSoLuong = new XRTableCell();
                XRTableCell cellNameSoLuongMauSale = new XRTableCell();
                XRTableCell cellNameNguyenLieu = new XRTableCell();
                XRTableCell cellNameMauHoanThien = new XRTableCell();
                XRTableCell cellNameGhiChu = new XRTableCell();
                XRRichText RNguyenLieu = new XRRichText();
                XRRichText RFinishing = new XRRichText();
                XRPictureBox RPictureBox = new XRPictureBox();

                FormatCell(cellName, dt.Rows[i]["STT"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Bold),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(33.27),TextAlignment.MiddleCenter);

                FormatCell(cellNameMaHang, dt.Rows[i]["MaHang"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Bold),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(110.01), TextAlignment.MiddleCenter);

                FormatCell(cellReference, dt.Rows[i]["Reference"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Bold),
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(89.18), TextAlignment.MiddleCenter);


                FormatCell(cellNameTenSP, dt.Rows[i]["TenSP"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Bold),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))),ProcessGeneral.GetSafeFloat(109.62), TextAlignment.MiddleCenter);

                Image img = null;
                if(!string.IsNullOrEmpty(ProcessGeneral.GetSafeString(dt.Rows[i]["HinhSP"])))
                {
                    img = ProcessGeneral.ConvertByteArrayToImage((byte[])dt.Rows[i]["HinhSP"]);
                }
                RPictureBox.Image = img;
                RPictureBox.Size = new System.Drawing.Size(106, 106);
                RPictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                RPictureBox.WidthF = ProcessGeneral.GetSafeFloat(111.9);
                RPictureBox.ImageAlignment = ImageAlignment.MiddleCenter;
                RPictureBox.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.None)));
                cellNameHinhSP.Controls.Add(RPictureBox);
                cellNameHinhSP.WidthF = ProcessGeneral.GetSafeFloat(111.9);
                cellNameHinhSP.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)));
                cellNameHinhSP.TextAlignment = TextAlignment.MiddleCenter;

                FormatCell(cellNameDVT, dt.Rows[i]["DVT"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(34.83), TextAlignment.MiddleCenter);

                FormatCell(cellNameSoLuong, dt.Rows[i]["SoLuong"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(56.9), TextAlignment.MiddleRight);

                if (ProcessGeneral.GetSafeInt(dt.Rows[i]["SoLuongMauSale"]) != ProcessGeneral.GetSafeInt(dt.Rows[i]["SoLuongMauSaleBK"]))
                {
                    FormatCell(cellNameSoLuongMauSale, dt.Rows[i]["SoLuongMauSale"].ToString(), Color.Gray, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(49.46), TextAlignment.MiddleRight);

                }
                else
                {
                    FormatCell(cellNameSoLuongMauSale, dt.Rows[i]["SoLuongMauSale"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(49.46), TextAlignment.MiddleRight);
                }

                RNguyenLieu.Text = ProcessGeneral.GetSafeString(dt.Rows[i]["NguyenLieu"]);
                RNguyenLieu.WidthF = ProcessGeneral.GetSafeFloat(238);
                RNguyenLieu.Borders = DevExpress.XtraPrinting.BorderSide.None;
                RNguyenLieu.Font = new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular);
                cellNameNguyenLieu.Controls.Add(RNguyenLieu);
            
                if (ProcessGeneral.GetSafeString(dt.Rows[i]["NguyenLieu"]) != ProcessGeneral.GetSafeString(dt.Rows[i]["NguyenLieuBK"]))
                {
                
                    FormatCell(cellNameNguyenLieu, dt.Rows[i]["NguyenLieu"].ToString(), Color.Gray, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(238), TextAlignment.MiddleLeft);

                }
                else
                {
                    cellNameNguyenLieu.Controls.Add(RNguyenLieu);
                    FormatCell(cellNameNguyenLieu, dt.Rows[i]["NguyenLieu"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                   ((DevExpress.XtraPrinting.BorderSide)( DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)), ProcessGeneral.GetSafeFloat(238), TextAlignment.MiddleLeft);
                }
                RFinishing.Rtf = ProcessGeneral.GetSafeString(dt.Rows[i]["MauHoanThien"]);
                RFinishing.WidthF = ProcessGeneral.GetSafeFloat(176.62);
                RFinishing.Borders = DevExpress.XtraPrinting.BorderSide.None;
                RFinishing.Font = new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular);
                cellNameMauHoanThien.Controls.Add(RFinishing);
                if (ProcessGeneral.GetSafeString(dt.Rows[i]["MauHoanThien"]) != ProcessGeneral.GetSafeString(dt.Rows[i]["MauHoanThienBK"]))
                {
                 
                    FormatCell(cellNameMauHoanThien, dt.Rows[i]["MauHoanThien"].ToString(), Color.Gray, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(176.62), TextAlignment.TopLeft);

                }
                else
                {
               
                    FormatCell(cellNameMauHoanThien, dt.Rows[i]["MauHoanThien"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(176.62), TextAlignment.TopLeft);
                }

                XRRichText RGhiChu = new XRRichText();

                RGhiChu.Text = ProcessGeneral.GetSafeString(dt.Rows[i]["GhiChu"]);
                RGhiChu.WidthF = ProcessGeneral.GetSafeFloat(98.38);
                RGhiChu.Borders = DevExpress.XtraPrinting.BorderSide.None;
                RGhiChu.Font = new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular);
                cellNameGhiChu.Controls.Add(RGhiChu);

                if (ProcessGeneral.GetSafeString(dt.Rows[i]["GhiChu"]) != ProcessGeneral.GetSafeString(dt.Rows[i]["GhiChuBK"]))
                {
                    FormatCell(cellNameGhiChu, dt.Rows[i]["GhiChu"].ToString(), Color.Gray, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(98.38), TextAlignment.TopLeft);

                }
                else
                {
                    FormatCell(cellNameGhiChu, dt.Rows[i]["GhiChu"].ToString(), Color.White, new Font(new FontFamily("Times New Roman"), 8, FontStyle.Regular),
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(98.38), TextAlignment.TopLeft);
                }
           

                row.Cells.Add(cellName);
                row.Cells.Add(cellNameMaHang);
                row.Cells.Add(cellReference);
                row.Cells.Add(cellNameTenSP);
                row.Cells.Add(cellNameHinhSP);
                row.Cells.Add(cellNameDVT);
                row.Cells.Add(cellNameSoLuong);
                row.Cells.Add(cellNameSoLuongMauSale);
                row.Cells.Add(cellNameNguyenLieu);
                row.Cells.Add(cellNameMauHoanThien);
                row.Cells.Add(cellNameGhiChu);
                table.Rows.Add(row);

            }
            //foreach (XRTableRow row in table)
            //{
            //    row.Cells[0].Width = maxLengh * 3 + 20;
            //    row.Cells[1].Width = table.Width - row.Cells[0].Width;
            //}
            //Finish table initialization.
           table.AdjustSize();
            table.EndInit();

            return table;
        }
        private void FormatCell(XRTableCell Cell,string Text,Color Backcolor, Font Font, DevExpress.XtraPrinting.BorderSide Borderside, float WidthF, TextAlignment TextAlignment)
        {
            Cell.Text = Text;
            Cell.BackColor = Backcolor;
            Cell.Font = Font;
            Cell.Borders = Borderside;
            Cell.WidthF = WidthF;
            Cell.TextAlignment = TextAlignment;

            //Maxlengt = Maxlengt > Cell.Text.Length ? Maxlengt : Cell.Text.Length;

            //((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)))
            //new Font(new FontFamily("Times New Roman"), 6, FontStyle.Bold)
        }


    }
}
