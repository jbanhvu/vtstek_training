using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using System.Text.RegularExpressions;
using CNY_BaseSys.Common;
using DevExpress.CodeParser;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList.Nodes;
using FocusedColumnChangedEventArgs = DevExpress.XtraTreeList.FocusedColumnChangedEventArgs;
using SummaryItemType = DevExpress.Data.SummaryItemType;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
namespace CNY_BaseSys.WForm
{
    /// <summary>
    /// 11
    /// </summary>
    public partial class Frm002BoMReportFi : Form
    {
        // private Inf_002BoMReport _inf = new Inf_002BoMReport();
        // private Int64 _pk;
        private readonly DataSet _dsData;
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        private readonly RepositoryItemMemoEdit _repositoryTextNormal;
     //   private readonly RepositoryItemMemoEdit _repositoryTextGrid;
     
        public const int FormatFinishingValueDecimal = 10;
        
       
     
        public Frm002BoMReportFi(DataSet dsDataPara)
        {
            InitializeComponent();
            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            //_repositoryTextNormal.Appearance.Options.UseTextOptions = true;
            //_repositoryTextNormal.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            _dsData = dsDataPara;
         

            // _pk = pk;
            //LoadData();
        
            InitTreeList(tlMain);
           
            LoadDataTreeList(tlMain, _dsData.Tables[1]);
            SetUpMainGrid();
            LoadDataPaintGridViewFrist(_dsData.Tables[2]);

        }









        #region "Process Paint Grid Final"


        private void CreateBandedGridHeader(DataTable dtS)
        {


            GridBand[] arrBand = new GridBand[dtS.Columns.Count];


            for (int i = 0; i < dtS.Columns.Count; i++)
            {
                DataColumn col = dtS.Columns[i];
                BandedGridColumn gCol = new BandedGridColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new GridBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;

                string colName = col.ColumnName;
                string displayText = "";
                string displayNumber = "";

                HorzAlignment hoz;
                HorzAlignment hozH;
                SummaryItemType sumCol = SummaryItemType.None;
                if (colName == "FnishingColorCode")
                {
                    displayText = @"Mã màu";
                    hoz = HorzAlignment.Near;
                    hozH = HorzAlignment.Near;
                    //    sumCol = SummaryItemType.Count;
                }
                else if (colName.StartsWith("FnishingColorDesc"))
                {
                    displayText = @"Mô tả";
                    hoz = HorzAlignment.Near;
                    hozH = HorzAlignment.Near;
                    gCol.ColumnEdit = _repositoryTextGrid;
                }
                else if (colName.StartsWith("RowID"))
                {
                    displayText = @"STT";
                    hoz = HorzAlignment.Center;
                    hozH = HorzAlignment.Center;
                }
                else if (colName.StartsWith("IsFinishing"))
                {
                    displayText = @"X";
                    hoz = HorzAlignment.Center;
                    hozH = HorzAlignment.Center;
                }
                else if (colName.StartsWith("FnishingColorPK"))
                {
                    displayText = @"PK";
                    hoz = HorzAlignment.Center;
                    hozH = HorzAlignment.Center;
                }
                else if (colName.StartsWith("TotalFinishing"))
                {
                    displayText = @"Tổng (m2)";
                    displayNumber = StrFormatFinishingValueDecimal(false, false);
                    sumCol = SummaryItemType.Sum;
                    hozH = HorzAlignment.Center;
                    hoz = HorzAlignment.Far;
                }
                else
                {
                    displayText = colName;
                    displayNumber = StrFormatFinishingValueDecimal(false, false);
                    sumCol = SummaryItemType.Sum;
                    hozH = HorzAlignment.Center;
                    hoz = HorzAlignment.Far;
                }



                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozH;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozH;





                gCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;


                gCol.Caption = displayText;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;

                if (displayNumber != "")
                {
                    gCol.DisplayFormat.FormatType = FormatType.Numeric;
                    gCol.DisplayFormat.FormatString = displayNumber;
                }

                //if (sumCol == SummaryItemType.Count)
                //{
                //    gCol.SummaryItem.SummaryType = sumCol;
                //    gCol.SummaryItem.DisplayFormat = @"Total     .";
                //}
                //else 


                if (sumCol == SummaryItemType.Sum)
                {
                    gCol.SummaryItem.SummaryType = SummaryItemType.Sum;
                    gCol.SummaryItem.DisplayFormat = StrFormatFinishingValueDecimal(false, true);

                }

                gvPaint.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].VisibleIndex = 0;
                arrBand[i].Width = 100;

            }
            gvPaint.Bands.AddRange(new[] { arrBand[0], arrBand[1], arrBand[2], arrBand[3], arrBand[4] });



            GridBand gParent = new GridBand();
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gParent.Caption = @"Diện tích (m2)";
            gParent.VisibleIndex = 5;
            gParent.Width = 100;

            for (int h = 5; h < dtS.Columns.Count - 1; h++)
            {
                arrBand[h].VisibleIndex = h;
                gParent.Children.Add(arrBand[h]);
            }
            gvPaint.Bands.Add(gParent);

            gvPaint.Bands.Add(arrBand[dtS.Columns.Count - 1]);

        }






        private string StrFormatFinishingValueDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(FormatFinishingValueDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(FormatFinishingValueDecimal, isN);
        }






        private void BestFitBandsGrid(BandedGridView view)
        {

            view.BeginUpdate();
            view.OptionsView.ShowColumnHeaders = true;

            

            Dictionary<string, Int32> dicBefor = new Dictionary<string, Int32>();
            gvPaint.OptionsView.ShowColumnHeaders = true;
            int sumWidth = 0;
            foreach (BandedGridColumn col in gvPaint.Columns)
            {
                GridBand gBand = col.OwnerBand;
                string fieldName = col.FieldName;
                if (fieldName == "FnishingColorPK")
                {
                    gBand.Visible = false;
                }
                else
                {
                    col.Caption = gBand.Caption;
                    int width = col.GetBestWidth();
                    if (fieldName != "FnishingColorDesc")
                    {
                        // col.Width = width;
                        dicBefor.Add(fieldName, width);
                        sumWidth += width;
                    }
                }



            }

            // gvPaint.OptionsView.ColumnAutoWidth = false;
            int leftWidth = gcPaint.Width - sumWidth - 2;
            gvPaint.Columns["FnishingColorDesc"].Width = leftWidth;
            foreach (var itemB1 in dicBefor)
            {
                gvPaint.Columns[itemB1.Key].Width = itemB1.Value;
            }









            // view.BestFitColumns();
            //   view.Columns["MenuCode"].Width += 20;
            view.OptionsView.ShowColumnHeaders = false;
            view.EndUpdate();



         //   gvPaint.Bands[0].Visible = false;


        }


        private void LoadDataPaintGridViewFrist(DataTable dtS)
        {


            if (dtS == null || dtS.Rows.Count <= 0)
            {
                gvPaint.Bands.Clear();
                gvPaint.Columns.Clear();
                gcPaint.DataSource = null;
                return;
            }









            //   finish:


            gvPaint.BeginUpdate();
            gvPaint.Bands.Clear();
            gvPaint.Columns.Clear();
            gcPaint.DataSource = null;

            CreateBandedGridHeader(dtS);
            gcPaint.DataSource = dtS;
            BestFitBandsGrid(gvPaint);

            gvPaint.EndUpdate();




        }






        #region "GridView Event && Init Grid"



        private void SetUpMainGrid()
        {


            gcPaint.UseEmbeddedNavigator = true;

            gcPaint.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvPaint.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvPaint.OptionsBehavior.Editable = true;
            gvPaint.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvPaint.OptionsCustomization.AllowColumnMoving = false;
            gvPaint.OptionsCustomization.AllowQuickHideColumns = true;
            gvPaint.OptionsCustomization.AllowSort = false;
            gvPaint.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvPaint.OptionsView.ColumnAutoWidth = false;
            gvPaint.OptionsView.ShowGroupPanel = false;
            gvPaint.OptionsView.ShowIndicator = true;
            gvPaint.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvPaint.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvPaint.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvPaint.OptionsView.ShowAutoFilterRow = false;
            gvPaint.OptionsView.AllowCellMerge = false;
            gvPaint.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvPaint.OptionsNavigation.AutoFocusNewRow = true;
            gvPaint.OptionsNavigation.UseTabKey = true;

            gvPaint.OptionsSelection.MultiSelect = true;
            gvPaint.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvPaint.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvPaint.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvPaint.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvPaint.OptionsView.EnableAppearanceEvenRow = false;
            gvPaint.OptionsView.EnableAppearanceOddRow = false;

            gvPaint.OptionsView.ShowFooter = true;


            //   gridView1.RowHeight = 25;

            gvPaint.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvPaint.OptionsFind.AlwaysVisible = false;
            gvPaint.OptionsFind.ShowCloseButton = true;
            gvPaint.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvPaint)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvPaint.OptionsMenu.EnableFooterMenu = false;
            gvPaint.OptionsMenu.EnableColumnMenu = false;

            gvPaint.OptionsView.RowAutoHeight = true;

            gvPaint.OptionsPrint.UsePrintStyles = false;
            gvPaint.OptionsPrint.PrintBandHeader = true;

            gvPaint.OptionsPrint.AllowMultilineHeaders = true;
            gvPaint.OptionsPrint.AutoWidth = true;
            gvPaint.OptionsPrint.EnableAppearanceEvenRow = false;
            gvPaint.OptionsPrint.EnableAppearanceOddRow = false;
            gvPaint.OptionsPrint.ExpandAllDetails = true;
            gvPaint.OptionsPrint.ExpandAllGroups = true;
            gvPaint.OptionsPrint.PrintDetails = true;
            gvPaint.OptionsPrint.PrintFooter = true;
            gvPaint.OptionsPrint.PrintHeader = false;
            gvPaint.OptionsPrint.PrintHorzLines = true;
            gvPaint.OptionsPrint.PrintVertLines = true;





            gcPaint.ForceInitialize();


        }










        #endregion

        #endregion








        #region "Init TreeList"





        private void LoadDataTreeList(TreeList tl, DataTable dtS)
        {
            // Int64 pkColor key, Int64 cny00050Pk value




            tl.BeginUpdate();


       
            tl.Columns.Clear();
            tl.DataSource = null;
           // CreateBandedTreeHeader(tl, dtS);
            tl.DataSource = dtS;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";






            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["QualityCode"], string.Format("Quality{0}(Chất{0}Lượng)", Environment.NewLine), false, HorzAlignment.Center, TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["QualityCode"]);







            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RMDescription_002"], string.Format("Process/Item List{0}(Bước Thực Hiện/Tên Hóa Chất)", Environment.NewLine), false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            tl.Columns["RMDescription_002"].ColumnEdit = _repositoryTextNormal;
            SetWordWrapTree(tlMain.Columns["RMDescription_002"]);





            tl.Columns["Note"].ColumnEdit = _repositoryTextNormal;


            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["UCUnit"], string.Format("Unit{0}(ĐVT)", Environment.NewLine), false, HorzAlignment.Center, TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["UCUnit"]);

            //ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RMGroup_066"], "RM Group", false, HorzAlignment.Near,
            //    TreeFixedStyle.None, "");



            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["QtyGram"], string.Format("Rate{0}(Tỷ Lệ){0}(Gr)", Environment.NewLine), false, HorzAlignment.Center, TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["QtyGram"]);
            tlMain.Columns["QtyGram"].Format.FormatType = FormatType.Numeric;
            tlMain.Columns["QtyGram"].Format.FormatString = "#,0.##";

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RateGram"], string.Format("Rate{0}(Tỷ Lệ){0}(%)", Environment.NewLine), false, HorzAlignment.Center, TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["RateGram"]);
            tlMain.Columns["RateGram"].Format.FormatType = FormatType.Numeric;
            tlMain.Columns["RateGram"].Format.FormatString = "{0:#,0.##}%";

            //  tl.Columns["RatePercent"].Format.FormatString = "C2";

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["UC"], string.Format("UC{0}(Định Mức){0}(Kg/M2)", Environment.NewLine), false, HorzAlignment.Center, TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["UC"]);
            tlMain.Columns["UC"].Format.FormatType = FormatType.Numeric;
            tlMain.Columns["UC"].Format.FormatString = "#,0.######";

        //    UC(Định mức sử dụng Kg / M2).


            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["Note"], string.Format("Note{0}(Ghi Chú)", Environment.NewLine), false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["Note"]);
            tl.Columns["Note"].ColumnEdit = _repositoryTextNormal;

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["Supplier"], string.Format("Supplier{0}(Nhà Cung Cấp)", Environment.NewLine), false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["Supplier"]);
            tl.Columns["Supplier"].ColumnEdit = _repositoryTextNormal;


            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["SupplierRef"], string.Format("Supplier Ref{0}(Mã VT NCC)", Environment.NewLine), false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            SetWordWrapTree(tlMain.Columns["SupplierRef"]);
            tl.Columns["SupplierRef"].ColumnEdit = _repositoryTextNormal;


            tl.ExpandAll();

            tl.ForceInitialize();


            tl.BeginSort();
            tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
          
            tl.EndSort();
            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "SortOrderNode", "TableCode");


       //     tl.BestFitColumns();


            int widthTree = tl.Width + tl.Columns["QtyGram"].MinWidth;


            string[] arrColNum =  { "QtyGram", "RateGram", "UC", "UCUnit", "QualityCode" };
            foreach (string strColNum in arrColNum)
            {
                tl.Columns[strColNum].BestFit();
                int widthNum = tl.Columns[strColNum].Width;
                tl.Columns[strColNum].Width = widthNum - 7;
                widthTree = widthTree - widthNum - 7;

            }

            string[] arrColStr = { "RMDescription_002", "SupplierRef", "Supplier", "Note"};
            Dictionary<string, int> dicWidth = new Dictionary<string, int>();



            int widthTemp = 0;
            foreach (string strColNum in arrColStr)
            {
                tl.Columns[strColNum].BestFit();
                int widthNum = tl.Columns[strColNum].Width;
                dicWidth.Add(strColNum, widthNum - 7);
                widthTemp = widthTemp + (widthNum - 7);



            }
        
            if (widthTemp != widthTree)
            {
                if (widthTemp < widthTree)
                {
                    tl.Columns["RMDescription_002"].Width = widthTree - dicWidth.Where(p => p.Key != "RMDescription_002").Sum(p => p.Value);
                }
                else
                {
                    int minWidth = widthTree  / 4;

                    var q2 = dicWidth.Where(p => p.Value <= minWidth).Select(p => p.Value).ToList();
                    if (q2.Count > 0)
                    {
                        widthTree = widthTree - q2.Sum();
                    }


                    Dictionary<string, int> q1 = dicWidth.Where(p => p.Value > minWidth).OrderBy(p=>p.Value).ToDictionary(t => t.Key, t => t.Value);



                    int c1 = q1.Count;
                    if (c1 > 0)
                    {
                        if (c1 == 1)
                        {
                            tl.Columns[q1.First().Key].Width = widthTree;
                        }
                        else
                        {
                            minWidth = widthTree / c1;

                            string last = q1.Last().Key;
                            var q3 = q1.Where(p => p.Key != last).ToDictionary(p => p.Key, p => p.Value);
                            foreach (var itemQ3 in q3)
                            {
                                tl.Columns[itemQ3.Key].Width = minWidth;
                                widthTree = widthTree - minWidth;
                            }
                            tl.Columns[last].Width = widthTree;



                        }


                    }





                }
             

            }


            int qSum = tl.VisibleColumns.Select(t => t.Width).Sum();
            if (qSum > tl.Width)
            {
                tl.Columns["RMDescription_002"].Width = tl.Columns["RMDescription_002"].Width - (qSum - tl.Width);


            }
            else if(qSum < tl.Width)
            {
                int wInxrease = tl.Width - qSum;
                int value1 = wInxrease / 2;
                int value2 = wInxrease - value1;
                tl.Columns["RMDescription_002"].Width = tl.Columns["RMDescription_002"].Width + value1;
                tl.Columns["Supplier"].Width = tl.Columns["Supplier"].Width + value2;
            }
            //RMDescription_002





            //int height = tl.GetColumnPanelHeight();
            //tl.ColumnPanelRowHeight = height;


            tl.EndUpdate();


            //RepositoryItemMemoEdit repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true };





        }



   
        
        private void SetWordWrapTree(TreeListColumn tCol)
        {
            tCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            tCol.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
        }
    

  

        


        private void InitTreeList(TreeList treeList)
        {

        //    int a = treeList.ColumnPanelRowHeight;
         //   treeList.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
        
            treeList.OptionsView.ShowBandsMode = DefaultBoolean.False;

            treeList.OptionsView.AllowHtmlDrawHeaders = true;

            treeList.OptionsView.AllowBandColumnsMultiRow = false;

            treeList.OptionsView.ShowColumns = true;
            treeList.OptionsView.AutoWidth = false;
            // treeList.AppearancePrint.Lines.BackColor = Color.Blue;
            treeList.OptionsPrint.UsePrintStyles = false;

          

            treeList.OptionsPrint.PrintAllNodes = true;
            treeList.OptionsPrint.PrintBandHeader = false;
            treeList.OptionsPrint.PrintCheckBoxes = false;
            treeList.OptionsPrint.PrintFilledTreeIndent = false;
            treeList.OptionsPrint.PrintHorzLines = true;
            treeList.OptionsPrint.PrintImages = true;
            treeList.OptionsPrint.PrintPageHeader = true;
            treeList.OptionsPrint.AutoRowHeight = true;
            treeList.OptionsPrint.AutoWidth = true;
            treeList.OptionsPrint.PrintTree = true;
            treeList.OptionsPrint.PrintTreeButtons = false;
            treeList.OptionsPrint.PrintVertLines = true;

            treeList.OptionsPrint.PrintReportFooter = false;
            treeList.OptionsPrint.PrintRowFooterSummary = false;

            //   treeList.AppearancePrint.Lines.BackColor = Color.Red;

            //tlMain.AppearancePrint.Row.Options.UseTextOptions = true;
            //tlMain.AppearancePrint.Row.Options.UseFont = true;
            //tlMain.AppearancePrint.Row.Options.UseBackColor = true;
            //tlMain.AppearancePrint.Row.TextOptions.WordWrap = WordWrap.Wrap;

            treeList.OptionsBehavior.AutoNodeHeight = true;
            treeList.OptionsBehavior.Editable = false;
            treeList.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;





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
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
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
       //     treeList.GetNodeDisplayValue += TreeList_GetNodeDisplayValue;
            //  treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;







        }







        private void tlMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;





            string type = ProcessGeneral.GetSafeString(node.GetValue("TableCode")).ToUpper();
            switch (type)
            {
                case "CNY00050":
                    {
                        e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "CNYMF016":
                    {
                        e.Appearance.Font = new Font("Tahoma", 7F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;
                default:
                    {
                        e.Appearance.Font = new Font("Tahoma", 7F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                        e.Appearance.ForeColor = Color.Black;
                    }
                    break;

            }


            //if (e.Node.HasChildren && e.Node.HasAsParent())
            //{
            //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //}
        }

        #endregion


        public void Print()
        {
            Print1();
            // this.Show();

        }
        private void Print1()
        {
    

            rpt002BoMReportFi rpt = new rpt002BoMReportFi(_dsData.Tables[0], tlMain, gcPaint);
            ReportPrintTool pt = new ReportPrintTool(rpt);
            Form form = pt.PreviewForm;
            form.MdiParent = this.ParentForm;
            pt.ShowPreview();
            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = string.Format("Print Finishing Process No.: {0}", ProcessGeneral.GetSafeString(_dsData.Tables[0].Rows[0]["CNY015_BOMNo"]));
            form.Show();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print1();
        }









      






     


    }


    public class BoMPrintTextInfo
    {
        public string DisplayText { get; set; }
        public string Caption { get; set; }
        public string Tag { get; set; }
        public Int32 BandCount { get; set; }
        public List<String> ListStrHeader { get; set; }
    }
}
