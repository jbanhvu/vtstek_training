using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CNY_Report.Info;
using DevExpress.XtraTreeList;
using System.Data;
using CNY_BaseSys.Common;
using CNY_Report.Common;
using System.Linq;
namespace CNY_Report
{
    public partial class rpt004PuchaserOrder_VN : DevExpress.XtraReports.UI.XtraReport
    {
        #region "Property"
        private readonly Int64 _primaryKey;
        Inf_001POReport _inf = new Inf_001POReport();

        public TreeList _tl;
        public TreeList _tlServices;
        #endregion

        public rpt004PuchaserOrder_VN(Int64 Pk, TreeList tl, DataTable dtHeader, TreeList tlServices, bool isCOdeEn)
        {
            InitializeComponent();
            DataTable dtLSX = _inf.LoadLSXNumber(Pk);
            var q1 = dtLSX.AsEnumerable().Select(p => p.Field<string>("CNY032")).Distinct().ToList();
            string ProjecNo = string.Join(",", q1);
            xrTableCellLSX.Text = ProjecNo;

            _tl = tl;
            _tlServices = tlServices;
            string sCurrency = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Currency_Des"]);
            string sCurrencyName = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Currency_Des2"]);
            double totalUnits = ProcessGeneral.GetSafeDouble(tl.GetSummaryValue(tl.Columns["Amount"]));
            double totalUnitsServices = ProcessGeneral.GetSafeDouble(tlServices.GetSummaryValue(tlServices.Columns["Amount"]));
            double totalUnitsFinal = totalUnits + totalUnitsServices;
            xrLabel4.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PurchaserDes"]);
            xrLabel5.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["FullNameConfirm"]);
            xrLabelNote.Text = "Đơn giá trên chưa bao gồm VAT";



            xrTableCellPaymentTermDes.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PaymentTermDes"]);
            xrTableCellDeliveryTermDes.Text= ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryTermDes"]);
            xrTableCellDeliveryMethodName.Text= ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryMethodName"]);

            string sDelivery = string.Format("{0}\n{1}", ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryName"]), ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryAdd"]));
            xrTableCellDeliveryName.Text = sDelivery;
            //xrTableCellDeliveryName.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryName"]);
            //xrTableCelldeliveryAdd.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DeliveryAdd"]);

            string sInvoice = string.Format("{0}\n{1}\n{2}", ProcessGeneral.GetSafeString(dtHeader.Rows[0]["InvoicerName"]), ProcessGeneral.GetSafeString(dtHeader.Rows[0]["InvoicerAdd"]),
         ProcessGeneral.GetSafeString(dtHeader.Rows[0]["InvoiceTaxCode"]));
            xrTableCellInvoiceName.Text = sInvoice;

            //xrTableCellInvoiceName.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["InvoicerName"]);
            //xrTableCellInvoiceAdd.Text = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["InvoicerAdd"]);
            //xrTableCellInvoiceTaxCode.Text=ProcessGeneral.GetSafeString(dtHeader.Rows[0]["InvoiceTaxCode"]);

            string sPRNO = string.Format("PYCMH: {0}", ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PRNo"]));
            string sPONo = string.Format("ĐĐH: {0}", ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PONo"]));
            xrLabelPRNo.Text = sPRNO;
            xrLabelPONo.Text = sPONo;
            switch (sCurrency)
            {
                case "USD":
                    {
                        string TotalUSD = string.Format("{0:#.00}", totalUnitsFinal);
                        if (isCOdeEn)
                        {
                            xrRichText2.Text = string.Format(@"Say: {0}",  NumberInWord.changeToWords(ProcessGeneral.GetSafeString(TotalUSD)));
                            xrRichText2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                            if (totalUnits > 0 && totalUnitsServices <= 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1): {0} ({1})", TotalUSD, sCurrency);
                            }
                            else if (totalUnits <= 0 && totalUnitsServices > 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (2): {0} ({1})", TotalUSD, sCurrency);
                            }
                            else
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1 + 2): {0} ({1})", TotalUSD, sCurrency);
                            }

                            xrRichText3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);

                        }
                        else
                        {
                            if (totalUnits > 0 && totalUnitsServices <= 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1): {0:#,#.00} ({1})", ProcessGeneral.GetSafeDouble(TotalUSD), sCurrency);
                            }
                            else if (totalUnits <= 0 && totalUnitsServices > 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (2): {0:#,#.00} ({1})", ProcessGeneral.GetSafeDouble(TotalUSD), sCurrency);
                            }
                            else
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1 + 2): {0:#,#.00} ({1})", ProcessGeneral.GetSafeDouble(TotalUSD), sCurrency);
                            }
                            //xrRichText3.Text = string.Format(@"Thành Tiền (1 + 2): {0:#,#.000} ({1})", totalUnitsFinal, sCurrency);
                            xrRichText2.Text = string.Format(@"Thành Tiền(bằng chữ): {0}", NumberInWord.PhanCach(ProcessGeneral.GetSafeString(TotalUSD), sCurrency));
                            xrRichText2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                            xrRichText3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                        }
                    }
                    break;
               
                case "VND":
                    {
                        string TotalVN = string.Format("{0:#}", totalUnitsFinal);
                        if (isCOdeEn)
                        {
                            xrRichText2.Text = string.Format(@"Thành Tiền (Bằng chữ): {0} {1} ", sCurrency, NumberInWord.changeToWords(ProcessGeneral.GetSafeString(TotalVN)));
                            xrRichText2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                            if (totalUnits > 0 && totalUnitsServices <= 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1): {0:#,#} ({1})", TotalVN, sCurrency);
                            }
                            else if (totalUnits <= 0 && totalUnitsServices > 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (2): {0:#,#} ({1})", TotalVN, sCurrency);
                            }
                            else
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1 + 2): {0:#,#} ({1})", TotalVN, sCurrency);
                            }

                            xrRichText3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);

                        }
                        else
                        {
                            if (totalUnits > 0 && totalUnitsServices <= 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1): {0:#,##0} ({1})", ProcessGeneral.GetSafeDouble(TotalVN), sCurrency);
                            }
                            else if (totalUnits <= 0 && totalUnitsServices > 0)
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (2): {0:#,##0} ({1})", ProcessGeneral.GetSafeDouble(TotalVN), sCurrency);
                            }
                            else
                            {
                                xrRichText3.Text = string.Format(@"Thành Tiền (1 + 2): {0:#,##0} ({1})", ProcessGeneral.GetSafeDouble(TotalVN), sCurrency);
                            }
                            //xrRichText3.Text = string.Format(@"Thành Tiền (1 + 2): {0:#,#.000} ({1})", totalUnitsFinal, sCurrency);
                            xrRichText2.Text = string.Format(@"Thành Tiền (Bằng chữ): {0}", NumberInWord.PhanCach(ProcessGeneral.GetSafeString(TotalVN), sCurrency));
                            xrRichText2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                            xrRichText3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                        }
                    }
            break;
        }
        _primaryKey = Pk;
            this.DataSource = dtHeader;
            PrintableComponentContainer container = new PrintableComponentContainer();
            container.PrintableComponent = tl;
            this.Detail.Controls.Add(container);
            PrintableComponentContainer container1 = new PrintableComponentContainer();
            container1.PrintableComponent = tlServices;
            this.SubBand1.Controls.Add(container1);



            //this.SubBand1.Controls.Add(container1);
        }


    }
}
