using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using CNY_BaseSys;
using DevExpress.XtraGrid.Filtering.Provider;
using CNY_BaseSys.Common;
using CNY_Report.Info;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using CNY_Report.Common;
using DevExpress.XtraRichEdit;

namespace CNY_Report
{
    public partial class rpt001SOReport : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly Int64 _primaryKey;
        private readonly Inf_001SOReport _inf = new Inf_001SOReport();
     
        public rpt001SOReport(Int64 primaryKey, TreeList tl ,DataTable dtHeader, TreeList tlServices)
        {
            InitializeComponent();
            this.ExportOptions.PrintPreview.DefaultFileName = string.Format("PI_{0}_{1}_V{2}_{3}", ProcessGeneral.GetSafeString(dtHeader.Rows[0]["OrderNOT"]),
                ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ProjectNameEP"]),
                ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Version"]),
                ProcessGeneral.GetSafeString(dtHeader.Rows[0]["OrderStatusDes"])) ;
            string sCurrency = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Currency_Des"]);
            string sCurrencyName = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Currency_Des2"]);
            double totalUnits = ProcessGeneral.GetSafeDouble(tl.GetSummaryValue(tl.Columns["Amount"]));
            double totalUnitsServices = ProcessGeneral.GetSafeDouble(tlServices.GetSummaryValue(tlServices.Columns["Amount"]));
            double totalUnitsFinal = totalUnits + totalUnitsServices;
            xrRichText1.Font = new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            xrRichText2.Font= new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            switch (sCurrency)
            {
                case "USD":
                    if(totalUnitsServices <= 0 && totalUnits>0)
                    {
                        xrRichText1.Text = string.Format("SAY (1): {0} {1}", sCurrencyName, NumberInWord.changeToWords(ProcessGeneral.GetSafeString(totalUnitsFinal)).ToUpper());
                        
                        xrRichText2.Text = string.Format("Total (1): {0:#,#.00} ({1})", totalUnitsFinal, sCurrency.ToUpper());
                    }
                    else if(totalUnitsServices > 0 && totalUnits <= 0)
                    {
                        xrRichText1.Text = string.Format("SAY (2): {0} {1}", sCurrencyName, NumberInWord.changeToWords(ProcessGeneral.GetSafeString(totalUnitsFinal)).ToUpper());
                        xrRichText2.Text = string.Format("Total (2): {0:#,#.00} ({1})", totalUnitsFinal, sCurrency.ToUpper());
                    }
                    else
                    {
                        xrRichText1.Text = string.Format("SAY (1) + (2): {0} {1}", sCurrencyName, NumberInWord.changeToWords(ProcessGeneral.GetSafeString(totalUnitsFinal)).ToUpper());
                        xrRichText2.Text = string.Format("Total (1) + (2): {0:#,#.00} ({1})", totalUnitsFinal, sCurrency.ToUpper());
                    }
               
                    break;
                case "VND":
                    if (totalUnitsServices <= 0 && totalUnits > 0)
                    {
                        xrRichText1.Text = string.Format("Total (1): {0:#,#.00} ({1})", totalUnitsFinal, sCurrency.ToUpper());
                        xrRichText2.Text = string.Format("SAY (1): {0} {1}", sCurrencyName, NumberInWord.So_chu(totalUnitsFinal).ToUpper());
                    }
                    else if (totalUnitsServices > 0 && totalUnits <= 0)
                    {
                        xrRichText1.Text = string.Format("Total (2): {0:#,#.00} ({1})", totalUnitsFinal, sCurrency.ToUpper());
                        xrRichText2.Text = string.Format("SAY (2): {0} {1}", sCurrencyName, NumberInWord.So_chu(totalUnitsFinal).ToUpper());
                    }
                    else
                    {
                        xrRichText1.Text = string.Format("Total (1) + (2): {0:#,#.00} ({1})", totalUnitsFinal, sCurrency.ToUpper());
                        xrRichText2.Text = string.Format("SAY 1) + (2): {0} {1}", sCurrencyName, NumberInWord.So_chu(totalUnitsFinal).ToUpper());
                    }


                        break;
            }
     
           

            _primaryKey = primaryKey;
            LoadHeader(dtHeader);
            //LoadAgreement();
            rpt001SOReportSub1 sub1 = new rpt001SOReportSub1(_primaryKey);
            xrSubreport1.ReportSource = sub1;
            //xrPanel1.Controls.Add(CreateXRTableAgreement());
            printableComponentContainer1.PrintableComponent = tl;
            printableComponentContainer2.PrintableComponent = tlServices;
            rtRemark.BeforePrint += RtRemark_BeforePrint;
        }

        private void RtRemark_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRRichText richText = (XRRichText)sender;

            using (RichEditDocumentServer docServer = new RichEditDocumentServer())
            {
                docServer.RtfText = richText.Rtf;
                docServer.Document.DefaultCharacterProperties.FontName = "Times New Roman";
                docServer.Document.DefaultCharacterProperties.FontSize = 8;
                richText.Rtf = docServer.RtfText;
            }
        }

        private void LoadHeader(DataTable dtHeader)
        {

            //            string buyerInformation = $"Company name:\t {dtHeader.Rows[0]["CustomerName"].ToString()} \n";
            //            buyerInformation += $"Address:\t {dtHeader.Rows[0]["InvoiceToAddress"].ToString()} \n";
            //            buyerInformation += $"Contact Person:\t {dtHeader.Rows[0]["ContactPerson"].ToString()} \n";
            //            buyerInformation += $"Email:\t {dtHeader.Rows[0]["Email"].ToString()} \n";
            //buyerInformation += $"Tel:\t {dtHeader.Rows[0]["Tel"].ToString()} \n";
            //            buyerInformation += $"Fax:\t {dtHeader.Rows[0]["Fax"].ToString()} \n";
            //            lblBuyerInformation.Text = buyerInformation;
            //rtRemark.RichEditControlCompatibility
            //rtRemark.Font= new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            rtRemark.Rtf = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["SpecialRemarks"]);
            //lblPaymentTern.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PayMentTern_Des"]);

            //string Delivery= $"Delivery Date:\t{ ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryDate"])} \n";
            //Delivery+= $"Delivery Method:\t{ ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Deliverymethod_Des"])} \n";
            //Delivery += $"Delivery Term:\t{ ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryTerm_Des"])} \n";
            //lblDeliveryTime.Text = Delivery;

            this.DataSource = dtHeader;

            //xrTableCellStaff.Text = @"-Sales Staff: "+DeclareSystem.SysFullName;
        }



        public XRTable CreateXRTableAgreement()
        {
            DataTable dt = LoadDataAgreement();

            // Create an empty table and set its size.
            XRTable table = new XRTable();
            table.Width = 1080;

            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth = 1;
            table.Font = new Font(new FontFamily("Microsoft Sans Serif"), 6, FontStyle.Regular);
            int columnCount = dt.Columns.Count;
            int maxLengh = 0;
            // Create table row.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XRTableRow row = new XRTableRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.ToString() == "Name")
                    {
                        XRTableCell cellName = new XRTableCell();
                        if (i == 0)
                        {

                            cellName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));
                        }
                        else
                        {

                            cellName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
                        }
                        cellName.Text = " " + (i + 1).ToString() + ". " + dt.Rows[i][j].ToString() + ":";
                        maxLengh = maxLengh > cellName.Text.Length ? maxLengh : cellName.Text.Length;
                        //cellName.Width = (maxLengh * 6) + 50;
                        cellName.Font = new Font(new FontFamily("Microsoft Sans Serif"), 6, FontStyle.Bold);
                        cellName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        row.Cells.Add(cellName);
                    }
                    else
                    {
                        XRTableCell cellValue = new XRTableCell();
                        if (i == 0)
                        {
                            cellValue.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)));
                        }
                        else
                        {

                            cellValue.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right)));
                        }

                        cellValue.Multiline = true;
                        cellValue.Text = GetInfoByDataRow(dt.Rows[i], columnCount);
                        row.Cells.Add(cellValue);
                        break;
                    }

                }
                table.Rows.Add(row);
            }
            foreach (XRTableRow row in table)
            {
                row.Cells[0].Width = maxLengh * 3 + 20;
                row.Cells[1].Width = table.Width - row.Cells[0].Width;
            }
            // Finish table initialization.
            //table.AdjustSize();
            table.EndInit();

            return table;
        }
        public string GetInfoByDataRow(DataRow dr, int columnCount)
        {
            bool isChecked = false;
            string s = "";
            int agreementCount = 0;
            for (int i = 1; i < columnCount; i++)
            {
                if (i % 2 == 1)
                {
                    if (ProcessGeneral.GetSafeBool(dr[i]))
                    {
                        isChecked = true;
                    }
                    else
                    {
                        isChecked = false;
                    }
                }
                else
                {
                    if (isChecked)
                    {
                        if (agreementCount > 0)
                        {
                            s = s + "\n";
                        }
                        s += "- " + dr[i].ToString();
                        agreementCount++;
                    }
                }
            }
            return s;
        }
        private DataTable LoadDataAgreement()
        {
            DataTable dt = new DataTable();
            if (_primaryKey > 0)
            {
                dt = _inf.LoadAgreeMent_Save(_primaryKey);
            }
            else
            {
                dt = _inf.LoadAgreeMent();
            }
            var q1 = dt.AsEnumerable().GroupBy(p => new
            {
                PKParent = Convert.ToInt32(p["PKParent"]),
                Name = p.Field<String>("Name"),
            }).Select(t => new
            {
                t.Key.PKParent,
                t.Key.Name,
                DataItem = t.Select(s => new ChildItemAgreement
                {
                    Col = s.Field<String>("Values"),
                    Pk = Convert.ToInt32(s["ChildPK"]),
                    Sav = Convert.ToInt32(s["CNY021PK"]),
                    Chk = s.Field<bool>("IsChecked"),
                }).OrderBy(a => a.Pk).ToList()
            }).ToList();

            Int32 maxRow = q1.Select(p => p.DataItem.Count).Max();

            DataTable dtT = new DataTable();
            dtT.Columns.Add("PKParent", typeof(Int32));
            dtT.Columns.Add("Name", typeof(String));
            for (int i = 0; i < maxRow; i++)
            {
                dtT.Columns.Add($"chk{i}", typeof(bool));
                dtT.Columns.Add($"Col{i}", typeof(String));
            }

            foreach (var item in q1)
            {
                DataRow dr = dtT.NewRow();
                dr["PKParent"] = item.PKParent;
                dr["Name"] = item.Name;

                List<ChildItemAgreement> l = item.DataItem;

                for (int i = 0; i < l.Count; i++)
                {
                    ChildItemAgreement c = l[i];
                    dr[$"chk{i}"] = c.Chk;
                    dr[$"Col{i}"] = c.Col;
                }
                dtT.Rows.Add(dr);
            }
            for (int i = 0; i < dtT.Columns.Count; i++)
            {
                if (dtT.Columns[i].ColumnName.Trim() == "PKParent")
                {
                    dtT.Columns.RemoveAt(i);
                }
            }
            return dtT;
        }

        

    }
    public class ChildItemAgreement
    {
        public string Col { get; set; }
        public Int32 Pk { get; set; }
        public Int32 Sav { get; set; }
        public bool Chk { get; set; }
        public bool ChkBK { get; set; }
    }



}
