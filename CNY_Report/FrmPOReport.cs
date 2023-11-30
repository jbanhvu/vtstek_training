using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.Utils;
using DevExpress.XtraTreeList.Columns;
using CNY_BaseSys.Common;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using CNY_Report.Common;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_Report
{

    public partial class FrmPOReport : DevExpress.XtraEditors.XtraForm
    {

        private readonly RepositoryItemMemoEdit _repositoryTextNormal;
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        private readonly RepositoryItemRichTextEdit _repositoryRichTextEdit;
        private Int64 _soHeaderPk;
        private string _codeReport;
        bool isCOdeEn;
        //private Inf_001SOReport _inf = new Inf_001SOReport();
        //DataTable tb_Att = new DataTable();
        private readonly DataSet _dsData;
        private readonly Dictionary<string, string> _dicUnit;
        public FrmPOReport(Int64 _PK, DataSet dsDataPara, Dictionary<string, string> dicUnit, string codeReport)
        {

       
            InitializeComponent();
            _dsData = dsDataPara;
            _soHeaderPk = _PK;
            _dicUnit = dicUnit;
            _codeReport = codeReport;
            isCOdeEn = _codeReport == "02";
            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _repositoryRichTextEdit = new RepositoryItemRichTextEdit { AutoHeight = false  };
            InitTreeList(tlMain);
            InitTreeListServices(tlServices);
            if (_dsData.Tables[0].Rows.Count > 0)
            {
                LoadDataTreeList(tlMain, _dsData.Tables[0], dsDataPara.Tables[1]);
            }
            if(_dsData.Tables[2].Rows.Count>0)
            {
                LoadDataTreeListServices(tlServices, _dsData.Tables[2], dsDataPara.Tables[1]);
            }

           
        }

        #region "Init TreeList"






        private void LoadDataTreeList(TreeList tl, DataTable dtS, DataTable dtSHeader)
        {
            // Int64 pkColor key, Int64 cny00050Pk value




            tl.BeginUpdate();


            tl.Bands.Clear();
            tl.Columns.Clear();
            tl.DataSource = null;
            CreateBandedTreeHeader(tl, dtS, dtSHeader);
            tl.DataSource = dtS;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            tl.ExpandAll();

            tl.ForceInitialize();


            tl.BeginSort();
            //tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();

            tl.BeginUpdate();
            tl.OptionsView.ShowColumns = true;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.False;
            tl.BestFitColumns();
            if (tl.Columns["STT"].Visible && tl.Columns["STT"].Width > 40)
                tl.Columns["STT"].Width = 40;
            if (tl.Columns["RMCode_001"].Visible && tl.Columns["RMCode_001"].Width > 105)
                tl.Columns["RMCode_001"].Width = 105;
            if (tl.Columns["RMDescription_002"].Visible && tl.Columns["RMDescription_002"].Width > 130)
                tl.Columns["RMDescription_002"].Width = 130;

            if (tl.Columns["SupplierRef"].Visible && tl.Columns["SupplierRef"].Width > 105)
                tl.Columns["SupplierRef"].Width = 105;
            if (tl.Columns["PurchaseUnitDesc"].Visible && tl.Columns["PurchaseUnitDesc"].Width > 55)
                tl.Columns["PurchaseUnitDesc"].Width = 55;

            //if (tl.Columns["Price"].Visible && tl.Columns["Price"].Width > 70)
            //    tl.Columns["Price"].Width = 70;
            tl.Columns["Price"].Format.FormatType = FormatType.Numeric;
            tl.Columns["Price"].Format.FormatString = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, false);

            if (tl.Columns["ETD"].Visible && tl.Columns["ETD"].Width > 115)
                tl.Columns["ETD"].Width = 115;
            if (tl.Columns["Note"].Visible && tl.Columns["Note"].Width > 150)
                tl.Columns["Note"].Width = 150;
            //if (tl.Columns["Amount"].Visible && tl.Columns["Amount"].Width > 150)
            //    tl.Columns["Amount"].Width = 120;

            if (tl.Columns["Note"].Visible && tl.Columns["Note"].Width > 70)
                tl.Columns["Note"].Width = 70;
            tl.Columns["Amount"].Width = 110;
            tl.Columns["Amount"].SummaryFooter = SummaryItemType.Sum;
            tl.Columns["Amount"].SummaryFooterStrFormat = "{0:N2}";
                //Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, true);

            tl.Columns["Amount"].Format.FormatType = FormatType.Numeric;
            tl.Columns["Amount"].Format.FormatString = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, false);
            tl.Columns["Price"].Width = 80;
            //#,#.00}
            tl.Columns["Price"].SummaryFooter = SummaryItemType.Count;
            tl.Columns["Price"].SummaryFooterStrFormat = isCOdeEn?  @"Total :": @"Thành Tiền :";

            tl.OptionsView.ShowBandsMode = DefaultBoolean.True;
            tl.OptionsView.ShowColumns = false;
            tl.EndUpdate();



            //RepositoryItemMemoEdit repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true };

            //tl.Columns["Note"].ColumnEdit = repositoryTextNormal;






        }


        private Dictionary<string, BoMPrintTextInfo> GetColCaptionCrossTab(DataTable dtS)
        {
            Dictionary<string, BoMPrintTextInfo> dicCol = new Dictionary<string, BoMPrintTextInfo>();

            var qCol = dtS.Columns.Cast<DataColumn>().Select(p => new
            {
                p.ColumnName,
                TypeCol = p.ColumnName.Contains("%%%%%") ? "%%%%%" : "",
            }).Where(p => p.TypeCol != "").ToList();




            foreach (var item in qCol)
            {
                string colName = item.ColumnName;
                string typeCol = item.TypeCol;
                string displayText = "";


                string[] arrTag = colName.Split(new[] { typeCol }, StringSplitOptions.RemoveEmptyEntries);

                string unit = "";
                _dicUnit.TryGetValue(colName, out unit);
                if (string.IsNullOrEmpty(unit))
                {
                    displayText = arrTag[1];
                }
                else
                {

                    unit = string.Format("({0})", unit);
                    displayText = string.Format("{0} {1}", arrTag[1], unit);

                }



                List<String> qSplit = displayText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Where(p => !string.IsNullOrEmpty(p.Trim())).Select(p => p.Trim()).ToList();
                int count = qSplit.Count;
                List<String> lFinal = new List<string>();
                string text = "";
                string caption = "";
                for (int i = 0; i < count; i++)
                {
                    string sSplit = qSplit[i];
                    if (sSplit.Length > caption.Length)
                    {
                        caption = sSplit;
                    }
                    if (i == count - 1)
                    {
                        text = sSplit;
                    }
                    else
                    {
                        lFinal.Add(sSplit);
                    }
                }
                BoMPrintTextInfo info = new BoMPrintTextInfo
                {
                    Caption = caption,
                    DisplayText = text,
                    Tag = typeCol,
                    BandCount = count,
                    ListStrHeader = lFinal
                };
                dicCol.Add(colName, info);



            }

            return dicCol;




        }


        private void CreateBandedTreeHeader(TreeList tl, DataTable dtS, DataTable dtSHeader)
        {
           

            string sCurency = ProcessGeneral.GetSafeString(dtSHeader.Rows[0]["Currency_Des"]);

            Dictionary<string, BoMPrintTextInfo> dicCol = GetColCaptionCrossTab(dtS);

          


            TreeListBand[] arrBand = new TreeListBand[dtS.Columns.Count - 2];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {

                string colName = col.ColumnName;
                if (colName == "ChildPK" || colName == "ParentPK") continue;
                TreeListColumn gCol = new TreeListColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                gCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i] = new TreeListBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                HorzAlignment hoz = HorzAlignment.Near;
                string displayText = "";
                string displayFormat = "";
                FormatType formatType = FormatType.Custom;
                string tag = "";
                string colCaption = "";


                BoMPrintTextInfo infoItem;
                if (dicCol.TryGetValue(colName, out infoItem))
                {
                    tag = infoItem.Tag;
                    displayText = infoItem.DisplayText;
                    colCaption = infoItem.Caption;
                    hoz = HorzAlignment.Center;
                }
                else
                {
                    if (colName == "STT")
                    {
                        displayText = isCOdeEn? "No.": "STT";
                        colCaption = isCOdeEn ? "No." : "STT";
                        hoz = HorzAlignment.Center;
                        // gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("RMCode_001"))
                    {
                        displayText = isCOdeEn ? @"Material Code" : @"Mã VT";
                        colCaption = isCOdeEn ? @"Material Code" : @"Mã VT";
                        hoz = HorzAlignment.Near;
                    }
                    else if (colName.Equals("RMDescription_002"))
                    {
                        displayText = isCOdeEn ? @"Description": @"Tên hàng";
                        colCaption = isCOdeEn ? @"Description" : @"Tên hàng";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("SupplierRef"))
                    {
                        displayText = isCOdeEn ? @"Sup. Ref." : @"Mã NCC"; ;
                        colCaption = isCOdeEn ? @"Sup. Ref." : @"Mã NCC"; ;
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("POQty"))
                    {
                        displayText = isCOdeEn ? @"Quantity": @"Số lượng"; 
                        colCaption = isCOdeEn ? @"Quantity" : @"Số lượng";
                        hoz = HorzAlignment.Far;
                        //gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("PurchaseUnitDesc"))
                    {
                        displayText = isCOdeEn ? @"Unit" : @"ĐVT"; 
                        colCaption = isCOdeEn ? @"Unit" : @"ĐVT";
                        hoz = HorzAlignment.Near;
                        //gCol.ColumnEdit = _repositoryRichTextEdit;
                    }
                    else if (colName.Equals("Price"))
                    {
                        displayText = isCOdeEn ? string.Format(@"Unit price ({0})", sCurency) : string.Format(@"Đơn Giá ({0})", sCurency); 
                        colCaption = isCOdeEn ? string.Format(@"Unit Price") : string.Format(@"Thành Tiền");
                        hoz = HorzAlignment.Far;
                        //gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("Amount"))
                    {
                        displayText = isCOdeEn ? string.Format(@"Total Amount ({0})", sCurency): string.Format(@"Thành Tiền ({0})", sCurency);
                        colCaption = isCOdeEn ? string.Format(@"Total Amount") : string.Format(@"Thành Tiền");
                        hoz = HorzAlignment.Far;
                        //gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("ETD"))
                    {
                        displayText = isCOdeEn ? @"Delivery Date" : @"Ngày Giao Hàng"; 
                        colCaption = isCOdeEn ? @"Delivery" : @"Ngày Giao";
                        hoz = HorzAlignment.Far;
                        formatType = FormatType.DateTime;
                        displayFormat = "dd/MM/yyyy";
                    }
                    else if (colName.Equals("Note"))
                    {
                        displayText = isCOdeEn ? @"Remark": @"Ghi chú";
                        colCaption = isCOdeEn ? @"Remark" : @"Ghi chú";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;
                    }
                }


                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hoz;
                gCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.Font= new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].Name = tag;
                // arrBand[i].RowCount = 2;
                gCol.OptionsColumn.ReadOnly = false;
                gCol.Tag = tag;
                gCol.Caption = colCaption;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                if (displayFormat != "")
                {
                    gCol.Format.FormatType = formatType;
                    gCol.Format.FormatString = displayFormat;
                }
                //if (colName.Equals("Amount"))
                //{
                //    tl.Columns["Amount"].SummaryFooterStrFormat = Ctrl_SOGenneral.StrFormatOrderQtyDecimal(false, true);
                //}


                tl.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].Visible = true;

                // arrBand[i].Width = 100;

                i++;
            }


            string ServicesCaption = isCOdeEn ? "(1) Items": @"(1) Thông Tin Sản Phẩm";
            TreeListBand gParentTop = new TreeListBand();
           
            //gParentTop.AppearanceHeader.Font.Bold = Font.Bold;

            gParentTop.Caption = ServicesCaption;
            gParentTop.AppearanceHeader.Options.UseFont = true;
            gParentTop.AppearanceHeader.Options.UseTextOptions = true;
            gParentTop.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gParentTop.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gParentTop.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));




            //this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            const int fristBandNo = 3;

            //gParentTop.Bands.AddRange(arrBand);


            //string[] arrNameVnLeft = new string[] { "STT", "Mã hàng", "Tên hàng" };


            for (int e = 0; e < fristBandNo; e++)
            {
                //TreeListBand gParentLeft = new TreeListBand();
            //  //  gParentLeft.AppearanceHeader.Options.UseTextOptions = true;
              //  gParentLeft.AppearanceHeader.TextOptions.HAlignment = arrBand[e].AppearanceHeader.TextOptions.HAlignment;
             //   gParentLeft.Caption = arrBand[e].Caption;
           //     arrBand[e].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Top;
           //     gParentLeft.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
              //  gParentLeft.Bands.Add(arrBand[e]);
                gParentTop.Bands.Add(arrBand[e]);

            }


            //gParent.Bands.Add(gParent);



            //List<string> l = new List<string>();
            //l.Add("%%%%%");


            Dictionary<string, BoMPrintTextInfo> dicColFinal = dicCol.Where(p => p.Value.BandCount > 1).ToDictionary(s => s.Key, s => s.Value);

     

            string tagLoop = "%%%%%";
            string sName = isCOdeEn ? "Specifications" : @"Quy Cách";

            TreeListBand gParent = new TreeListBand();
            gParent.AppearanceHeader.Options.UseFont = true;
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gParent.Caption = string.Format("<b>{0}</b>", sName);
            //  gParent.Width = 100;
            var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Name) == tagLoop).ToArray();
            int len = q2.Length;
          
            if (len > 0)
            {
                foreach (TreeListBand t1 in q2)
                {
                    string fieldNamG = t1.Columns[0].FieldName;
                    BoMPrintTextInfo infoG;
                    if (dicColFinal.TryGetValue(fieldNamG, out infoG))
                    {
                        TreeListBand parentBand = gParent;
                        List<string> lStr = infoG.ListStrHeader;
                        foreach (string t in lStr)
                        {
                            parentBand = AddChildBand(parentBand, t);
                        }
                        parentBand.Bands.Add(t1);
                    }
                    else
                    {
                        gParent.Bands.Add(t1);
                    }

                    // t1.Columns.Add();

                    //  t1.RowCount = bandRowCount;

                }
                gParentTop.Bands.Add(gParent);
              
            }

            int beginPaint = len + fristBandNo;

            //int e1 = 0;
            //string[] arrNameVnRight = new string[] { "Mã KH", "Số lượng", "DVT", "Đơn Giá", "Thành tiền", "Ngày Giao Hàng", "Ghi chú" };
            for (int m = beginPaint; m < arrBand.Length; m++)
            {
                //TreeListBand gNameVnRight = new TreeListBand();
                //gNameVnRight.AppearanceHeader.Options.UseTextOptions = true;
                //gNameVnRight.AppearanceHeader.TextOptions.HAlignment = arrBand[m].AppearanceHeader.TextOptions.HAlignment;
                //gNameVnRight.Caption = arrBand[m].Caption;
                //gNameVnRight.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                //arrBand[m].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Top;

                //gNameVnRight.Bands.Add(arrBand[m]);
                gParentTop.Bands.Add(arrBand[m]);
                //arrNameVnRight
                //tl.Bands.Add(arrBand[e1]);
                //e1++;
            }

            tl.Bands.Add(gParentTop);


            List<string> lHide = new List<string>();
            //lHide.Add("ItemType");
            //lHide.Add("RMCode_001");
            //lHide.Add("SortOrderNode");
            var qHideBand = tl.Bands.Where(p => !p.HasChildren).Where(p => lHide.Any(s => s == p.Columns[0].FieldName)).OrderByDescending(p => p.Index).ToList();
            foreach (TreeListBand bandHide in qHideBand)
            {
                bandHide.Visible = false;
            }


        }





        private TreeListBand AddChildBand(TreeListBand rootBand, string sItem)
        {
            TreeListBand gChild = new TreeListBand();
            gChild.AppearanceHeader.Options.UseFont = true;
            gChild.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            gChild.AppearanceHeader.Options.UseTextOptions = true;
            gChild.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gChild.Caption = string.Format("<b>{0}</b>", sItem);
            return rootBand.Bands.Add(gChild);
        }


        private void InitTreeList(TreeList treeList)
        {


            treeList.OptionsView.ShowBandsMode = DefaultBoolean.True;

           
            treeList.OptionsView.AllowBandColumnsMultiRow = false;

            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.AutoWidth = false;

            treeList.OptionsPrint.UsePrintStyles = false;

            
            treeList.OptionsPrint.PrintPageHeader = false;
            treeList.OptionsPrint.PrintAllNodes = true;
            treeList.OptionsPrint.PrintBandHeader = true;
            treeList.OptionsPrint.PrintCheckBoxes = false;
            treeList.OptionsPrint.PrintFilledTreeIndent = false;
            treeList.OptionsPrint.PrintHorzLines = true;
            treeList.OptionsPrint.PrintImages = true;
           // treeList.OptionsPrint.PrintPageHeader = false;
            treeList.OptionsPrint.AutoRowHeight = true;
            treeList.OptionsPrint.AutoWidth = true;
            treeList.OptionsPrint.PrintTree = true;
            treeList.OptionsPrint.PrintTreeButtons = false;
            treeList.OptionsPrint.PrintVertLines = true;

            treeList.OptionsPrint.PrintReportFooter = true;
            treeList.OptionsPrint.PrintRowFooterSummary = true;
           



            //   treeList.AppearancePrint.Lines.BackColor = Color.Red;

            //tlMain.AppearancePrint.Row.Options.UseTextOptions = true;
            //tlMain.AppearancePrint.Row.Options.UseFont = true;
            //tlMain.AppearancePrint.Row.Options.UseBackColor = true;
            //tlMain.AppearancePrint.Row.TextOptions.WordWrap = WordWrap.Wrap;

            treeList.OptionsBehavior.AutoNodeHeight = true;
            treeList.OptionsBehavior.Editable = false;
            treeList.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            treeList.OptionsView.AllowHtmlDrawHeaders = true;



            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = false;



            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;

            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;

            treeList.OptionsBehavior.AutoChangeParent = false;



            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = false;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;







            //new TreeListMultiCellSelector(treeList, true)
            //{
            //    AllowSort = false,
            //    FilterShowChild = true,

            //};








            treeList.NodeCellStyle += tlMain_NodeCellStyle;
            //     treeList.CalcNodeHeight += TreeList_CalcNodeHeight;
            treeList.GetNodeDisplayValue += TreeList_GetNodeDisplayValue;
            //  treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;
     







        }

    

        private void TreeList_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            if (!col.Visible) return;
            //if (fieldName != "STT") return;
            //string value = (tl.GetVisibleIndexByNode(node) + 1).ToString().Trim();
            //e.Value = value;
        }




        private void tlMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;


            if (node.ParentNode == null)
            {
                if (node.HasChildren)
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                }
                else
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.Black;
                }

            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 7F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }




        }

        #endregion

        #region "Init TreeList Services"






        private void LoadDataTreeListServices(TreeList tl, DataTable dtS, DataTable dtSHeader)
        {
            // Int64 pkColor key, Int64 cny00050Pk value




            tl.BeginUpdate();


            tl.Bands.Clear();
            tl.Columns.Clear();
            tl.DataSource = null;
            CreateBandedTreeHeaderServices(tl, dtS, dtSHeader);
            tl.DataSource = dtS;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            tl.ExpandAll();

            tl.ForceInitialize();


            tl.BeginSort();
            //tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();

            tl.BeginUpdate();
            tl.OptionsView.ShowColumns = true;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.False;
            tl.BestFitColumns();

           
          
            if (tl.Columns["STT"].Visible && tl.Columns["STT"].Width > 40)
                tl.Columns["STT"].Width = 40;
            if (tl.Columns["RMCode_001"].Visible && tl.Columns["RMCode_001"].Width > 105)
                tl.Columns["RMCode_001"].Width = 105;
            if (tl.Columns["RMDescription_002"].Visible && tl.Columns["RMDescription_002"].Width > 150)
                tl.Columns["RMDescription_002"].Width = 150;

            if (tl.Columns["Price"].Visible && tl.Columns["Price"].Width > 70)
                tl.Columns["Price"].Width = 70;
            tl.Columns["Price"].Format.FormatType = FormatType.Numeric;
            tl.Columns["Price"].Format.FormatString = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, false);

            if (tl.Columns["Note"].Visible && tl.Columns["Note"].Width > 150)
                tl.Columns["Note"].Width = 150;
            if (tl.Columns["Amount"].Visible && tl.Columns["Amount"].Width > 100)
                tl.Columns["Amount"].Width = 100;

            tl.Columns["Amount"].SummaryFooter = SummaryItemType.Sum;
            tl.Columns["Amount"].SummaryFooterStrFormat = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, true);
            tl.Columns["Amount"].Format.FormatType = FormatType.Numeric;
            tl.Columns["Amount"].Format.FormatString = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, false);
            tl.Columns["Price"].SummaryFooter = SummaryItemType.Count;
            tl.Columns["Price"].SummaryFooterStrFormat = isCOdeEn? @"Total :": @"Thành Tiền :";

            tl.OptionsView.ShowBandsMode = DefaultBoolean.True;
            tl.OptionsView.ShowColumns = false;
            tl.EndUpdate();



            //RepositoryItemMemoEdit repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true };

            //tl.Columns["Note"].ColumnEdit = repositoryTextNormal;






        }


   


        private void CreateBandedTreeHeaderServices(TreeList tl, DataTable dtS, DataTable dtSHeader)
        {
           
            string sCurency = ProcessGeneral.GetSafeString(dtSHeader.Rows[0]["Currency_Des"]);

            string ServicesCaption = isCOdeEn ? "(2) Services": @"(2) Dịch Vụ";
            TreeListBand gParent = new TreeListBand();
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.Options.UseFont = true;
            gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gParent.Caption = ServicesCaption;
            gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            //gParent.Bands.Add(gParent);
          


            TreeListBand[] arrBand = new TreeListBand[dtS.Columns.Count - 2];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {

                string colName = col.ColumnName;
                if (colName == "ChildPK" || colName == "ParentPK") continue;
                TreeListColumn gCol = new TreeListColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                
                gCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i] = new TreeListBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                HorzAlignment hoz = HorzAlignment.Near;
                string displayText = "";
                string displayFormat = "";
                FormatType formatType = FormatType.Custom;
                string tag = "";
                string colCaption = "";
            
                    if (colName == "STT")
                    {
                        displayText = isCOdeEn ? "No.": "STT";
                        colCaption = isCOdeEn ? "No." : "STT";
                    hoz = HorzAlignment.Center;
                        // gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("RMCode_001"))
                    {
                        displayText = isCOdeEn ? @"Code": @"Mã hàng";
                        colCaption = isCOdeEn ? @"Code" : @"Mã hàng";
                    hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("RMDescription_002"))
                    {
                        displayText = isCOdeEn ? @"Description" : @"Tên hàng";
                        colCaption = isCOdeEn ? @"Description" : @"Tên hàng";
                    hoz = HorzAlignment.Near;
                        //  gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("Qty"))
                    {
                        displayText = isCOdeEn ? @"Quantity": @"Số lượng";
                        colCaption = isCOdeEn ? @"Quantity" : @"Số lượng";
                    hoz = HorzAlignment.Near;
                        //gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("Price"))
                    {
                        displayText = isCOdeEn ? @"Unit price": @"Đơn Giá";
                        colCaption = isCOdeEn ? @"Unit price" : @"Đơn Giá";
                    hoz = HorzAlignment.Near;
                        //gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("Amount"))
                    {
                        displayText = isCOdeEn ? string.Format(@"Amount ({0})", sCurency): string.Format(@"Thành Tiền ({0})", sCurency);
                        colCaption = isCOdeEn ? string.Format(@"Amount ({0})", sCurency) : string.Format(@"Thành Tiền ({0})", sCurency);
                    hoz = HorzAlignment.Center;
                        //gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("Note"))
                    {
                        displayText = isCOdeEn ? @"Remark" : @"Ghi chú";
                        colCaption = isCOdeEn ? @"Remark" : @"Ghi chú";
                    hoz = HorzAlignment.Near;
                    }

                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                gCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                //gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].Name = tag;
                // arrBand[i].RowCount = 2;
                gCol.OptionsColumn.ReadOnly = false;
                gCol.Tag = tag;
                gCol.Caption = colCaption;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                if (displayFormat != "")
                {
                    gCol.Format.FormatType = formatType;
                    gCol.Format.FormatString = displayFormat;
                }

                tl.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].Visible = true;
                i++;
            }
            gParent.Bands.AddRange(arrBand);
            tl.Bands.Add(gParent);


            //List<string> lHide = new List<string>();
            ////lHide.Add("ItemType");
            ////lHide.Add("RMCode_001");
            ////lHide.Add("SortOrderNode");
            //var qHideBand = tl.Bands.Where(p => !p.HasChildren).Where(p => lHide.Any(s => s == p.Columns[0].FieldName)).OrderByDescending(p => p.Index).ToList();
            //foreach (TreeListBand bandHide in qHideBand)
            //{
            //    bandHide.Visible = false;
            //}


        }

        private TreeListBand AddChildBandServices(TreeListBand rootBand, string sItem)
        {
            TreeListBand gChild = new TreeListBand();
            gChild.AppearanceHeader.Options.UseTextOptions = true;
            gChild.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gChild.Caption = string.Format("<b>{0}</b>", sItem);
            return rootBand.Bands.Add(gChild);
        }


        private void InitTreeListServices(TreeList treeList)
        {


            treeList.OptionsView.ShowBandsMode = DefaultBoolean.True;


            treeList.OptionsView.AllowBandColumnsMultiRow = false;

            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.AutoWidth = false;

            treeList.OptionsPrint.UsePrintStyles = false;


            treeList.OptionsPrint.PrintPageHeader = false;
            treeList.OptionsPrint.PrintAllNodes = true;
            treeList.OptionsPrint.PrintBandHeader = true;
            treeList.OptionsPrint.PrintCheckBoxes = false;
            treeList.OptionsPrint.PrintFilledTreeIndent = false;
            treeList.OptionsPrint.PrintHorzLines = true;
            treeList.OptionsPrint.PrintImages = true;
            // treeList.OptionsPrint.PrintPageHeader = false;
            treeList.OptionsPrint.AutoRowHeight = true;
            treeList.OptionsPrint.AutoWidth = true;
            treeList.OptionsPrint.PrintTree = true;
            treeList.OptionsPrint.PrintTreeButtons = false;
            treeList.OptionsPrint.PrintVertLines = true;

            treeList.OptionsPrint.PrintReportFooter = true;
            treeList.OptionsPrint.PrintRowFooterSummary = true;

            //   treeList.AppearancePrint.Lines.BackColor = Color.Red;

            //tlMain.AppearancePrint.Row.Options.UseTextOptions = true;
            //tlMain.AppearancePrint.Row.Options.UseFont = true;
            //tlMain.AppearancePrint.Row.Options.UseBackColor = true;
            //tlMain.AppearancePrint.Row.TextOptions.WordWrap = WordWrap.Wrap;

            treeList.OptionsBehavior.AutoNodeHeight = true;
            treeList.OptionsBehavior.Editable = false;
            treeList.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            treeList.OptionsView.AllowHtmlDrawHeaders = true;



            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = false;



            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;

            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;

            treeList.OptionsBehavior.AutoChangeParent = false;



            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = false;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;







            //new TreeListMultiCellSelector(treeList, true)
            //{
            //    AllowSort = false,
            //    FilterShowChild = true,

            //};








            treeList.NodeCellStyle += TreeListServices_NodeCellStyle;
            //     treeList.CalcNodeHeight += TreeList_CalcNodeHeight;
            treeList.GetNodeDisplayValue += TreeListServices_GetNodeDisplayValue;
            //  treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;








        }



        private void TreeListServices_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            if (!col.Visible) return;
            //if (fieldName != "STT") return;
            //string value = (tl.GetVisibleIndexByNode(node) + 1).ToString().Trim();
            //e.Value = value;
        }




        private void TreeListServices_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;


            if (node.ParentNode == null)
            {
                if (node.HasChildren)
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                }
                else
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.Black;
                }

            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 7F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }




        }

        #endregion

        public void Print()
        {
          
          
            if(isCOdeEn)
            {
                rpt004PuchaserOrder rpt = new rpt004PuchaserOrder(_soHeaderPk, tlMain, _dsData.Tables[1], tlServices, isCOdeEn);
                ReportPrintTool pt = new ReportPrintTool(rpt);
                Form form = pt.PreviewForm;
                form.MdiParent = this.ParentForm;
                pt.ShowPreview();
                form.WindowState = FormWindowState.Maximized;
                form.StartPosition = FormStartPosition.CenterScreen;
                //form.Text = string.Format("Print PO No.: {0}", ProcessGeneral.GetSafeString(_dsData.Tables[1].Rows[0]["OrderNOT"]));
                form.Show();
            }
            else
            {
                rpt004PuchaserOrder_VN rpt = new rpt004PuchaserOrder_VN(_soHeaderPk, tlMain, _dsData.Tables[1], tlServices, isCOdeEn);
                ReportPrintTool pt = new ReportPrintTool(rpt);
                Form form = pt.PreviewForm;
                form.MdiParent = this.ParentForm;
                pt.ShowPreview();
                form.WindowState = FormWindowState.Maximized;
                form.StartPosition = FormStartPosition.CenterScreen;
                //form.Text = string.Format("Print PO No.: {0}", ProcessGeneral.GetSafeString(_dsData.Tables[1].Rows[0]["OrderNOT"]));
                form.Show();
            }
  
           
        
        }
    }
}