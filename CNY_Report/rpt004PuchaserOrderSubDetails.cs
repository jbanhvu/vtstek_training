using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CNY_Report.Info;
using CNY_Report.Common;
using CNY_BaseSys.Common;
using System.Data;
using DevExpress.XtraPrinting;

namespace CNY_Report
{
    public partial class rpt004PuchaserOrderSubDetails : DevExpress.XtraReports.UI.XtraReport
    {
        Inf_001POReport _inf = new Inf_001POReport();
        double totalUnits = 0;
        public rpt004PuchaserOrderSubDetails(Int64 pk)
        {
            InitializeComponent();
           DataTable dt = _inf.LoadPODeatis(pk);
            //CreateXRTableAgreement(dt);
            //this.Detail.Controls.Add(CreateXRTableAgreement(dt));
            this.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                totalUnits += ProcessGeneral.GetSafeDouble(dr["Amount"]);
            }
            //xrTableCell10.Text = string.Format("{0:#.###}", totalUnits);
            xrLabel2.Text = NumberInWord.So_chu(totalUnits);
            xrLabel4.Text = NumberInWord.NumberToWordsEnglish(ProcessGeneral.GetSafeInt(totalUnits));
        }
        public XRTable CreateXRTableAgreement(DataTable dt)
        {
            XRTable table = new XRTable();
            
            table.Width = 1102;

            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth = 1;
            table.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular);
            int columnCount = dt.Columns.Count;

            foreach (DataRow dr in dt.Rows)
            {
                XRTableRow row = new XRTableRow();
                XRTableCell cellNameSTT = new XRTableCell();
                XRTableCell cellNameRMDescription_002 = new XRTableCell();
                XRTableCell cellRMCode_001 = new XRTableCell();
                //XRPictureBox PictureBoxHinhSP = new XRPictureBox();
                XRTableCell cellNameSupplierRef = new XRTableCell();
                XRTableCell cellNamePurchaseUnitDesc = new XRTableCell();
                XRTableCell cellNamePOQty = new XRTableCell();
                XRTableCell cellNamePrice = new XRTableCell();
                XRTableCell cellNameAmount = new XRTableCell();
                XRTableCell cellNameETD = new XRTableCell();
                XRTableCell cellNameNote = new XRTableCell();
                FormatCell(cellNameSTT, dr["STT"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(40.28), TextAlignment.MiddleCenter);
                FormatCell(cellNameRMDescription_002, dr["RMDescription_002"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(159.14), TextAlignment.MiddleLeft);
                FormatCell(cellRMCode_001, dr["RMCode_001"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(105.49), TextAlignment.MiddleCenter);
                FormatCell(cellNameSupplierRef, dr["SupplierRef"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(105.49), TextAlignment.MiddleCenter);
                FormatCell(cellNamePurchaseUnitDesc, dr["PurchaseUnitDesc"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                  ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                  | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(54.79), TextAlignment.MiddleCenter);
                FormatCell(cellNamePOQty, dr["POQty"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                 ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                 | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(63.43), TextAlignment.MiddleRight);
                FormatCell(cellNamePrice, dr["Price"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                 ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(71.92), TextAlignment.MiddleRight);
                FormatCell(cellNameAmount, dr["Amount"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                 ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                 | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(132.99), TextAlignment.MiddleRight);
                FormatCell(cellNameETD, ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(dr["ETD"]).ToString(ConstSystem.SysDateFormat),
                    Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                 ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                 | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(113.89), TextAlignment.MiddleCenter);
                FormatCell(cellNameNote, dr["Note"].ToString(), Color.White, new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular),
                 ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom
                | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top))), ProcessGeneral.GetSafeFloat(221.57), TextAlignment.MiddleLeft);

                row.Cells.Add(cellNameSTT);
                row.Cells.Add(cellNameRMDescription_002);
                row.Cells.Add(cellRMCode_001);
                row.Cells.Add(cellNameSupplierRef);
                row.Cells.Add(cellNamePurchaseUnitDesc);
                row.Cells.Add(cellNamePOQty);
                row.Cells.Add(cellNamePrice);
                row.Cells.Add(cellNameAmount);
                row.Cells.Add(cellNameETD);
                row.Cells.Add(cellNameNote);
                table.Rows.Add(row);

            }
            table.AdjustSize();
            table.EndInit();

            return table;
        }
        private void FormatCell(XRTableCell Cell, string Text, Color Backcolor, Font Font, DevExpress.XtraPrinting.BorderSide Borderside, float WidthF, TextAlignment TextAlignment)
        {
            Cell.Text = Text;
            Cell.BackColor = Backcolor;
            Cell.Font = Font;
            Cell.Borders = Borderside;
            Cell.WidthF = WidthF;
            Cell.TextAlignment = TextAlignment;
        }
    }
}
