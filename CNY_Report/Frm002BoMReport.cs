using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_Report.Info;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using System.Text.RegularExpressions;
using DevExpress.CodeParser;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList.Nodes;
using FocusedColumnChangedEventArgs = DevExpress.XtraTreeList.FocusedColumnChangedEventArgs;
using SummaryItemType = DevExpress.Data.SummaryItemType;

namespace CNY_Report
{
    /// <summary>
    /// 11
    /// </summary>
    public partial class Frm002BoMReport : Form
    {
        // private Inf_002BoMReport _inf = new Inf_002BoMReport();
        // private Int64 _pk;
        private readonly DataSet _dsData;
        private readonly int _layout;
        private readonly bool _isNormal;
        private readonly RepositoryItemMemoEdit _repositoryTextNormal;
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        private readonly bool _isMultiItemType;
        private readonly string _projectNo;
        private readonly string _projectName;
        private readonly string _productionOrder;
        public const int FormatFinishingValueDecimal = 10;
        private readonly bool _isShowCode;
        private readonly bool _showHis;
        private readonly Dictionary<string, string> _dicUnit;
        /*
        public Frm002BoMReport()
        {
            InitializeComponent();
            _pk = 34;
            LoadData();
            InitTreeList();
            InitGridView();
        }*/
        public Frm002BoMReport(DataSet dsDataPara, int layout, bool isNormal, bool isMultiItemType, string projectNo, string projectName, string productionOrder, bool isShowCode, Dictionary<string,string> dicUnit,bool showHis)
        {
            InitializeComponent();
            _dicUnit = dicUnit;
            this._showHis = showHis;
            this._isShowCode = isShowCode;
            this._projectNo = projectNo;
            this._projectName = projectName;
            this._productionOrder = productionOrder;
            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            //_repositoryTextNormal.Appearance.Options.UseTextOptions = true;
            //_repositoryTextNormal.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            _dsData = dsDataPara;
            _layout = layout;
            _isNormal = isNormal;
            this._isMultiItemType = isMultiItemType;

            // _pk = pk;
            //LoadData();
            SetUpAmountGrid();
            SetUpMainGrid();
            InitTreeList(tlMain);
            SetUpAttributeGrid();
            SetUpUpdatetGrid();
            SetUpInsertGrid();
            SetUpDeleteGrid();
            LoadDataTreeList(tlMain, _dsData.Tables[3]);
            LoadDataPaintGridViewFrist(_dsData.Tables[4]);


            LoadDataAmountGridViewFrist(_dsData.Tables[6]);

            LoadDataAttributeGridViewFrist(_dsData.Tables[2]);
            LoadDataUpdateGridViewFrist(_dsData.Tables[8]);
            LoadDataInsertGridViewFrist(_dsData.Tables[7]);
            LoadDataDeleteGridViewFrist(_dsData.Tables[9]);
        }



        #region "Init TreeList"




        /*
        private void TreeList_CalcNodeHeight(object sender, CalcNodeHeightEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;

            

            using (RepositoryItemMemoEdit edit = new RepositoryItemMemoEdit())
            {
                edit.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                MemoEditViewInfo viewInfo = edit.CreateViewInfo() as MemoEditViewInfo;
                if (viewInfo == null) return;
                //  viewInfo.PaintAppearance.FontSizeDelta = 0;
                int height = e.NodeHeight;//18
                using (Graphics graphics = tl.CreateGraphics())
                {
                    using (GraphicsCache cache = new GraphicsCache(graphics))
                    {
                        bool visibleGroup = tl.VisibleColumns.Contains(tl.Columns["MainMaterialGroup"]);
                        if (visibleGroup &&
                            ProcessGeneral.GetSafeString(node.GetValue("MainMaterialGroup")) != "")
                        {
                            viewInfo.EditValue = ProcessGeneral.GetSafeString(node.GetValue("MainMaterialGroup"));
                            e.NodeHeight = Math.Max(((IHeightAdaptable)viewInfo).CalcHeight(cache, tl.Columns["MainMaterialGroup"].VisibleWidth- height), height);
                        }
                        else
                        {
                            int realHeight1 = height;
                            int addWidth = 10;
                            int subStractWidth = 0;
                            if (!visibleGroup)
                            {
                                subStractWidth = 10 * node.Level;
                                addWidth = 0;
                            }


                            string fieldRmName = _isShowCode ? "RMName" : "RMNameT";
                      
                            viewInfo.EditValue = ProcessGeneral.GetSafeString(node.GetValue(fieldRmName));
                            int realHeight = Math.Max(((IHeightAdaptable)viewInfo).CalcHeight(cache, tl.Columns[fieldRmName].VisibleWidth + addWidth - subStractWidth), height);

                            if (tl.VisibleColumns.Contains(tl.Columns["Note"]))
                            {
                                viewInfo.EditValue = ProcessGeneral.GetSafeString(node.GetValue("Note"));
                                realHeight1 = Math.Max(((IHeightAdaptable)viewInfo).CalcHeight(cache, tl.Columns["Note"].VisibleWidth + 10), height);
                            }

                            int heightFinal = realHeight >= realHeight1 ? realHeight : realHeight1;

                            double value = (double)heightFinal / (double)height;
                            int v1 = (int)value;
                            if (value - v1 != 0)
                            {
                                e.NodeHeight = heightFinal - (v1 + 1) * 3;
                            }
                            else
                            {
                                e.NodeHeight = heightFinal - v1 * 3;
                            }
                        }

                       
                    }
                }
                viewInfo.Dispose();
            }




        }

    */

        private void LoadDataTreeList(TreeList tl, DataTable dtS)
        {
            // Int64 pkColor key, Int64 cny00050Pk value




            tl.BeginUpdate();


            tl.Bands.Clear();
            tl.Columns.Clear();
            tl.DataSource = null;
            CreateBandedTreeHeader(tl, dtS);
            tl.DataSource = dtS;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";





            tl.ExpandAll();

            tl.ForceInitialize();


            tl.BeginSort();
            tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();

            tl.BeginUpdate();
            tl.OptionsView.ShowColumns = true;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.False;
            tl.BestFitColumns();
            if (tl.Columns["MainMaterialGroup"].Visible && tl.Columns["MainMaterialGroup"].Width > 90)
                tl.Columns["MainMaterialGroup"].Width = 90;
            if (tl.Columns["RMName"].Visible && tl.Columns["RMName"].Width > 190)
                tl.Columns["RMName"].Width = 190;
            if (tl.Columns["RMNameT"].Visible && tl.Columns["RMNameT"].Width > 190)
                tl.Columns["RMNameT"].Width = 190;
            if (tl.Columns["Note"].Visible && tl.Columns["Note"].Width > 190)
                tl.Columns["Note"].Width = 190;
       
            tl.OptionsView.ShowBandsMode = DefaultBoolean.True;
            tl.OptionsView.ShowColumns = false;
            tl.EndUpdate();



            //RepositoryItemMemoEdit repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true };
            //tl.Columns["RMName"].ColumnEdit = repositoryTextNormal;
            //tl.Columns["Note"].ColumnEdit = repositoryTextNormal;






        }


        private Dictionary<string, BoMPrintTextInfo> GetColCaptionCrossTab(DataTable dtS)
        {
            Dictionary<string, BoMPrintTextInfo> dicCol = new Dictionary<string, BoMPrintTextInfo>();
     
            var qCol = dtS.Columns.Cast<DataColumn>().Select(p => new
            {
                p.ColumnName,
                TypeCol = p.ColumnName.Contains("^^^^^") ? "^^^^^" : (p.ColumnName.Contains("%%%%%") ? "%%%%%" : ""),
            }).Where(p => p.TypeCol != "").ToList();



         
            foreach (var item in qCol)
            {
                string colName = item.ColumnName;
                string typeCol = item.TypeCol;
                string displayText = "";


                string[] arrTag = colName.Split(new[] { typeCol }, StringSplitOptions.RemoveEmptyEntries);
                if (typeCol == "^^^^^")
                {
                 
          
                    displayText = _layout == 0 ? arrTag[1] : arrTag[0];
                }
                else 
                {
                    

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
                }


                List<String> qSplit = displayText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Where(p=>!string.IsNullOrEmpty(p.Trim())).Select(p=>p.Trim()).ToList();
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


        private void CreateBandedTreeHeader(TreeList tl, DataTable dtS)
        {
           
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
                arrBand[i] = new TreeListBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;

                HorzAlignment hoz = HorzAlignment.Near;
                string displayText = "";
                string displayFormat = "";
                FormatType formatType = FormatType.None;
                string tag = "";
                string colCaption = "";


                BoMPrintTextInfo infoItem;
                if (dicCol.TryGetValue(colName, out infoItem))
                {
                    tag = infoItem.Tag;
                    displayText = infoItem.DisplayText;
                    colCaption = infoItem.Caption;
                    hoz = HorzAlignment.Center;
                    /*
                    else if (colName.Contains("^^^^^"))
                    {
                        tag = "^^^^^";
                        string[] arrTagO = colName.Split(new String[] { "^^^^^" }, StringSplitOptions.RemoveEmptyEntries);
                        displayText = _layout == 0 ? arrTagO[1] : arrTagO[0];
                        colCaption = _layout == 0 ? arrTagO[1] : arrTagO[0];
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Contains("%%%%%"))
                    {
                        tag = "%%%%%";
                        string[] arrTagA = colName.Split(new String[] { "%%%%%" }, StringSplitOptions.RemoveEmptyEntries);


                        string unit = "";
                        _dicUnit.TryGetValue(colName, out unit);
                        if (string.IsNullOrEmpty(unit))
                        {
                            displayText = arrTagA[1];
                            colCaption = arrTagA[1];
                        }
                        else
                        {

                            unit = string.Format("({0})", unit);
                            displayText = string.Format("{0}<br>{1}", arrTagA[1], unit);
                            colCaption = arrTagA[1].Length < unit.Length - 1 ? unit.Substring(1) : arrTagA[1];

                        }
                        hoz = HorzAlignment.Center;
                    }
                    */
                }
                else
                {
                    if (colName == "MainMaterialGroup")
                    {
                        displayText = "Nhóm Vật Tư";
                        colCaption = "Nhóm Vật Tư";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("ItemType"))
                    {
                        displayText = "Item Type";
                        colCaption = "Item Type";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("RMName"))
                    {
                        displayText = "Danh mục";
                        colCaption = "Name";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("RMNameT"))
                    {
                        displayText = "Danh mục";
                        colCaption = "Name";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("FinishingColor"))
                    {
                        displayText = "Color";
                        colCaption = "Color";
                        hoz = HorzAlignment.Near;
                    }
                    else if (colName.Equals("Factor"))
                    {
                        displayText = "SL";
                        colCaption = "SL";
                        hoz = HorzAlignment.Center;
                        formatType = FormatType.Numeric;
                        displayFormat = "#,0.##########";
                    }
                    else if (colName.Equals("UC"))
                    {
                        displayText = "Amount";
                        colCaption = "Amount";
                        hoz = HorzAlignment.Center;
                        formatType = FormatType.Numeric;
                        displayFormat = "#,0.##########";
                    }
                    else if (colName.Equals("UCUnit"))
                    {
                        displayText = "Unit";
                        colCaption = "Unit";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("Waste"))
                    {
                        displayText = "(%) Waste";
                        colCaption = "(%) Waste";
                        hoz = HorzAlignment.Center;
                        formatType = FormatType.Numeric;
                        displayFormat = "#,0.#####";
                    }
                    else if (colName.Equals("PercentUsing"))
                    {
                        displayText = "(%) Using";
                        colCaption = "(%) Using";
                        hoz = HorzAlignment.Center;
                        formatType = FormatType.Numeric;
                        displayFormat = "#,0.#####";
                    }
                    else if (colName.Equals("Note"))
                    {
                        displayText = "Ghi chú";
                        colCaption = "Note";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("SortOrderNode"))
                    {
                        displayText = "SortOrderNode";
                        colCaption = "SortOrderNode";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("ChildPKNew"))
                    {
                        displayText = "ChildPKNew";
                        colCaption = "ChildPKNew";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("ParentPKNew"))
                    {
                        displayText = "ParentPKNew";
                        colCaption = "ParentPKNew";
                        hoz = HorzAlignment.Center;
                    }
                  
                }

               
                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hoz;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hoz;
                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                gCol.AppearanceHeader.TextOptions.HAlignment = hoz;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hoz;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].Name = tag;
               // arrBand[i].RowCount = 2;
                gCol.OptionsColumn.ReadOnly = false;
                gCol.Tag = tag;
                gCol.Caption = string.Format("<b>{0}</b>", colCaption);
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
                arrBand[i].Caption = string.Format("<b>{0}</b>", displayText);
                arrBand[i].Columns.Add(gCol);
                arrBand[i].Visible = true;

                // arrBand[i].Width = 100;

                i++;
            }


            const int fristBandNo = 5;


            for (int e = 0; e < fristBandNo; e++)
            {
                tl.Bands.Add(arrBand[e]);
            }

    


            List<string> l = new List<string>();
            if (_layout == 0)
            {
                l.Add("^^^^^");
                l.Add("%%%%%");

            }
            else
            {
                l.Add("%%%%%");
                l.Add("^^^^^");
            }


            int sumPaint = 0;


            Dictionary<string, BoMPrintTextInfo> dicColFinal = dicCol.Where(p => p.Value.BandCount > 1).ToDictionary(s => s.Key, s => s.Value);

            foreach (string tagLoop in l)
            {
                string sName = "";
                if (tagLoop == "%%%%%")
                {
                    sName = "Quy Cách";
                }
                else
                {
                    if (_layout == 0)
                    {
                        sName = "Vị Trí";
                    }
                    else
                    {
                        sName = "Số Lượng";
                    }
                }
                TreeListBand gParent = new TreeListBand();
                gParent.AppearanceHeader.Options.UseTextOptions = true;
                gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gParent.Caption = string.Format("<b>{0}</b>", sName);
                //  gParent.Width = 100;
                var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Name) == tagLoop).ToArray();
                int len = q2.Length;
                sumPaint += len;
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
                    tl.Bands.Add(gParent);
                }
              

            }

            int beginPaint = sumPaint + fristBandNo;


            for (int m = beginPaint; m < arrBand.Length; m++)
            {
                tl.Bands.Add(arrBand[m]);
            }
            List<string> lHide = new List<string>();
            if (_layout == 0)
            {
                if (_isNormal)
                {
                    lHide.Add("MainMaterialGroup");
                    lHide.Add("ItemType");
                    lHide.Add("FinishingColor");
                    lHide.Add("UC");
                    lHide.Add("UCUnit");
                    lHide.Add("Waste");
                    lHide.Add("PercentUsing");
                    lHide.Add("SortOrderNode");
                    lHide.Add("ChildPKNew");
                    lHide.Add("ParentPKNew");

                }
                else
                {
                    lHide.Add("ItemType");
                    lHide.Add("FinishingColor");
                    lHide.Add("UC");
                    lHide.Add("UCUnit");
                    lHide.Add("Waste");
                    lHide.Add("PercentUsing");
                    lHide.Add("SortOrderNode");
                    lHide.Add("ChildPKNew");
                    lHide.Add("ParentPKNew");
                }
            }
            else
            {
                lHide.Add("ItemType");
                lHide.Add("FinishingColor");
                lHide.Add("Factor");
                lHide.Add("UC");
                lHide.Add("Waste");
                lHide.Add("PercentUsing");
                lHide.Add("Note");
                lHide.Add("SortOrderNode");
                lHide.Add("ChildPKNew");
                lHide.Add("ParentPKNew");
            }

            if (_isShowCode)
            {
                lHide.Add("RMNameT");
            }
            else
            {
                lHide.Add("RMName");
            }
            var qHideBand = tl.Bands.Where(p => !p.HasChildren).Where(p => lHide.Any(s => s == p.Columns[0].FieldName)).OrderByDescending(p => p.Index).ToList();
            foreach (TreeListBand bandHide in qHideBand)
            {
                bandHide.Visible = false;
            }


        }





        private TreeListBand AddChildBand(TreeListBand rootBand,string sItem)
        {
            TreeListBand gChild = new TreeListBand();
            gChild.AppearanceHeader.Options.UseTextOptions = true;
            gChild.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gChild.Caption = string.Format("<b>{0}</b>", sItem);
            return rootBand.Bands.Add(gChild);
        }


        private void InitTreeList(TreeList treeList)
        {


            treeList.OptionsView.ShowBandsMode = DefaultBoolean.True;

            treeList.OptionsView.AllowHtmlDrawHeaders = true;

            treeList.OptionsView.AllowBandColumnsMultiRow = false;

            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.AutoWidth = false;
           // treeList.AppearancePrint.Lines.BackColor = Color.Blue;
            treeList.OptionsPrint.UsePrintStyles = false;


            treeList.OptionsPrint.PrintAllNodes = true;
            treeList.OptionsPrint.PrintBandHeader = true;
            treeList.OptionsPrint.PrintCheckBoxes = false;
            treeList.OptionsPrint.PrintFilledTreeIndent = false;
            treeList.OptionsPrint.PrintHorzLines = true;
            treeList.OptionsPrint.PrintImages = true;
            treeList.OptionsPrint.PrintPageHeader = false;
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


            if (fieldName == "RMName" || fieldName == "RMNameT")
            {
                if (_isMultiItemType)
                {
                    int level = node.Level;
                    if (!_isNormal && level >= 2)
                    {
                        string oldV1 = ProcessGeneral.GetSafeString(node.GetValue(fieldName));
                        e.Value = oldV1.SetStringLeftSpace(level - 1);
                    }
                }

            }
            else if (fieldName.Contains("^^^^^"))
            {
                if (_layout == 1)
                {

                    if (ProcessGeneral.GetSafeString(node.GetValue(fieldName)) == "0")
                    {
                        e.Value = "";
                    }
                }
            }


        }





        private void tlMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;

          
            
            
          
            string type = ProcessGeneral.GetSafeString(node.GetValue("ItemType")).ToUpper();
            switch (type)
            {
                case "P":
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                }
                    break;
                case "A":
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkRed;
                }
                    break;
                case "C":
                {
                    e.Appearance.Font = new Font("Tahoma", 7F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkGreen;
                }
                    break;
                case "R":
                {
                    e.Appearance.Font = new Font("Tahoma", 7F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.Black;
                }
                    break;
                default:
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
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


            rpt002BoMReport rpt = new rpt002BoMReport(_dsData.Tables[0], _dsData.Tables[1], _dsData.Tables[5], gcAttribute, tlMain, gcPaint, _projectNo,
                _projectName, _productionOrder, gcAmount, _layout, gcUpdate, gcInsert, gvInsert.RowCount > 0, gvUpdate.RowCount > 0, _showHis, gcDelete, gvDelete.RowCount > 0);
            ReportPrintTool pt = new ReportPrintTool(rpt);
            Form form = pt.PreviewForm;
            form.MdiParent = this.ParentForm;
            pt.ShowPreview();
            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = string.Format("Print BoM No.: {0}", ProcessGeneral.GetSafeString(_dsData.Tables[0].Rows[0]["CNY015_BOMNo"]));
            form.Show();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print1();
        }




        #region "Process update Grid Final"


        private void CreateInsertGridHeader(GridView gv, DataTable dtS)
        {





            for (int i = 0; i < dtS.Columns.Count; i++)
            {
                DataColumn col = dtS.Columns[i];
                string colName = col.ColumnName;
                GridColumn gCol = gv.Columns[colName];
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;



                string displayText = "";
                string displayNumber = "";
                HorzAlignment hoz;
                HorzAlignment hozH;
              //  DefaultBoolean allowMerge = DefaultBoolean.False;

                switch (colName)
                {
                    case "PK":
                        {
                            displayText = "STT";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                         //   allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "RMCode_001":
                        {
                            displayText = "Mã";
                            hoz = HorzAlignment.Near;
                            hozH = HorzAlignment.Near;
                         //   allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "RMDescription_Fi":
                        {
                            displayText = "Danh mục";
                            hoz = HorzAlignment.Near;
                            hozH = HorzAlignment.Near;
                            //   gCol.ColumnEdit = _repositoryTextGrid;
                        //    allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "PosDesc":
                        {
                            displayText = "Vị Trí";
                            hoz = HorzAlignment.Near;
                            hozH = HorzAlignment.Near;
                         //   allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "Factor":
                        {
                            displayText = "SL";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                        //    allowMerge = DefaultBoolean.True;
                            displayNumber = StrFormatFinishingValueDecimal(false, false);
                        }
                        break;
                    case "UC":
                        {
                            displayText = "KL";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                     //       allowMerge = DefaultBoolean.True;
                            displayNumber = StrFormatFinishingValueDecimal(false, false);
                        }
                        break;
                    case "UCUnit":
                        {
                            displayText = "ĐVT";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                        //    allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    default:
                        {
                            displayText = colName;
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                          //  allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                }






                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozH;
                //    gCol.OptionsColumn.AllowMerge = allowMerge;





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





            }


        }














        private void LoadDataInsertGridViewFrist(DataTable dtS)
        {


            if (dtS == null || dtS.Rows.Count <= 0)
            {
                gvInsert.Columns.Clear();
                gcInsert.DataSource = null;
                return;
            }









            //   finish:


            gvInsert.BeginUpdate();
            gvInsert.Columns.Clear();
            gcInsert.DataSource = null;
            gcInsert.DataSource = dtS;
            CreateInsertGridHeader(gvInsert, dtS);
            gvInsert.Columns["RMCode_001"].Visible = false;

            gvInsert.BestFitColumns();


            gvInsert.EndUpdate();




        }






        #region "GridView Event && Init Grid"



        private void SetUpInsertGrid()
        {
            
            gcInsert.UseEmbeddedNavigator = true;

            gcInsert.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcInsert.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcInsert.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcInsert.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcInsert.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvInsert.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvInsert.OptionsBehavior.Editable = true;
            gvInsert.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvInsert.OptionsCustomization.AllowColumnMoving = false;
            gvInsert.OptionsCustomization.AllowQuickHideColumns = true;
            gvInsert.OptionsCustomization.AllowSort = false;
            gvInsert.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvInsert.OptionsView.ColumnAutoWidth = false;
            gvInsert.OptionsView.ShowGroupPanel = false;
            gvInsert.OptionsView.ShowIndicator = true;
            gvInsert.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvInsert.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvInsert.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvInsert.OptionsView.ShowAutoFilterRow = false;
            gvInsert.OptionsView.AllowCellMerge = false;
            gvInsert.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvInsert.OptionsNavigation.AutoFocusNewRow = true;
            gvInsert.OptionsNavigation.UseTabKey = true;

            gvInsert.OptionsSelection.MultiSelect = true;
            gvInsert.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvInsert.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvInsert.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvInsert.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvInsert.OptionsView.EnableAppearanceEvenRow = false;
            gvInsert.OptionsView.EnableAppearanceOddRow = false;

            gvInsert.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gvInsert.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvInsert.OptionsFind.AlwaysVisible = false;
            gvInsert.OptionsFind.ShowCloseButton = true;
            gvInsert.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvInsert)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvInsert.OptionsMenu.EnableFooterMenu = false;
            gvInsert.OptionsMenu.EnableColumnMenu = false;

            gvInsert.OptionsView.RowAutoHeight = true;

            gvInsert.OptionsPrint.UsePrintStyles = false;

            gvInsert.OptionsPrint.AllowMultilineHeaders = true;
            gvInsert.OptionsPrint.AutoWidth = true;
            gvInsert.OptionsPrint.EnableAppearanceEvenRow = false;
            gvInsert.OptionsPrint.EnableAppearanceOddRow = false;
            gvInsert.OptionsPrint.ExpandAllDetails = true;
            gvInsert.OptionsPrint.ExpandAllGroups = true;
            gvInsert.OptionsPrint.PrintDetails = true;
            gvInsert.OptionsPrint.PrintFooter = false;
            gvInsert.OptionsPrint.PrintHeader = true;
            gvInsert.OptionsPrint.PrintHorzLines = true;
            gvInsert.OptionsPrint.PrintVertLines = true;


          


            gcInsert.ForceInitialize();


        }









        #endregion

        #endregion



        #region "Process update Grid Final"


        private void CreateUpdateGridHeader(GridView gv, DataTable dtS)
        {





            for (int i = 0; i < dtS.Columns.Count; i++)
            {
                DataColumn col = dtS.Columns[i];
                string colName = col.ColumnName;
                GridColumn gCol = gv.Columns[colName];
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;



                string displayText = "";
                string displayNumber = "";
                HorzAlignment hoz;
                HorzAlignment hozH;

                switch (colName)
                {
                    case "PK":
                    {
                        displayText = "STT";
                        hoz = HorzAlignment.Center;
                        hozH = HorzAlignment.Center;
                        displayNumber = "";
                        }
                        break;
                    case "RMCode_001":
                    {
                        displayText = "Mã";
                        hoz = HorzAlignment.Near;
                        hozH = HorzAlignment.Near;
                        displayNumber = "";
                        }
                        break;
                    case "RMDescription_Fi":
                    {
                        displayText = "Danh mục";
                        hoz = HorzAlignment.Near;
                        hozH = HorzAlignment.Near;
                     //   gCol.ColumnEdit = _repositoryTextGrid;
                        displayNumber = "";
                        }
                        break;
                    case "PosDesc":
                    {
                        displayText = "Vị Trí";
                        hoz = HorzAlignment.Near;
                        hozH = HorzAlignment.Near;
                        displayNumber = "";
                        }
                        break;
                    case "Factor":
                    {
                        displayText = "SL";
                        hoz = HorzAlignment.Center;
                        hozH = HorzAlignment.Center;
                        displayNumber = StrFormatFinishingValueDecimal(false, false);
                        }
                        break;
                    case "UC":
                    {
                        displayText = "KL";
                        hoz = HorzAlignment.Center;
                        hozH = HorzAlignment.Center;
                        displayNumber = StrFormatFinishingValueDecimal(false, false);
                        }
                        break;
                    case "UCUnit":
                    {
                        displayText = "ĐVT";
                        hoz = HorzAlignment.Center;
                        hozH = HorzAlignment.Center;
                        displayNumber = "";
                        }
                        break;
                    default:
                    {
                        displayText = colName;
                        hoz = HorzAlignment.Center;
                        hozH = HorzAlignment.Center;
                        displayNumber = "";
                        }
                        break;
                }


             



                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozH;
            //    gCol.OptionsColumn.AllowMerge = allowMerge;





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


               


            }


        }














        private void LoadDataUpdateGridViewFrist(DataTable dtS)
        {


            if (dtS == null || dtS.Rows.Count <= 0)
            {
                gvUpdate.Columns.Clear();
                gcUpdate.DataSource = null;
                return;
            }









            //   finish:


            gvUpdate.BeginUpdate();
            gvUpdate.Columns.Clear();
            gcUpdate.DataSource = null;
            gcUpdate.DataSource = dtS;
            CreateUpdateGridHeader(gvUpdate, dtS);
            gvUpdate.Columns["RMCode_001"].Visible = false;

            gvUpdate.BestFitColumns();

  
            gvUpdate.EndUpdate();




        }






        #region "GridView Event && Init Grid"



        private void SetUpUpdatetGrid()
        {

            gcUpdate.UseEmbeddedNavigator = true;

            gcUpdate.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcUpdate.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcUpdate.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcUpdate.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcUpdate.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvUpdate.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvUpdate.OptionsBehavior.Editable = true;
            gvUpdate.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvUpdate.OptionsCustomization.AllowColumnMoving = false;
            gvUpdate.OptionsCustomization.AllowQuickHideColumns = true;
            gvUpdate.OptionsCustomization.AllowSort = false;
            gvUpdate.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvUpdate.OptionsView.ColumnAutoWidth = false;
            gvUpdate.OptionsView.ShowGroupPanel = false;
            gvUpdate.OptionsView.ShowIndicator = true;
            gvUpdate.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvUpdate.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvUpdate.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvUpdate.OptionsView.ShowAutoFilterRow = false;
            gvUpdate.OptionsView.AllowCellMerge = true;
            gvUpdate.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvUpdate.OptionsNavigation.AutoFocusNewRow = true;
            gvUpdate.OptionsNavigation.UseTabKey = true;

            gvUpdate.OptionsSelection.MultiSelect = true;
            gvUpdate.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvUpdate.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvUpdate.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvUpdate.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvUpdate.OptionsView.EnableAppearanceEvenRow = false;
            gvUpdate.OptionsView.EnableAppearanceOddRow = false;

            gvUpdate.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gvUpdate.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvUpdate.OptionsFind.AlwaysVisible = false;
            gvUpdate.OptionsFind.ShowCloseButton = true;
            gvUpdate.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvUpdate)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvUpdate.OptionsMenu.EnableFooterMenu = false;
            gvUpdate.OptionsMenu.EnableColumnMenu = false;

            gvUpdate.OptionsView.RowAutoHeight = true;

            gvUpdate.OptionsPrint.UsePrintStyles = false;

            gvUpdate.OptionsPrint.AllowMultilineHeaders = true;
            gvUpdate.OptionsPrint.AutoWidth = true;
            gvUpdate.OptionsPrint.EnableAppearanceEvenRow = false;
            gvUpdate.OptionsPrint.EnableAppearanceOddRow = false;
            gvUpdate.OptionsPrint.ExpandAllDetails = true;
            gvUpdate.OptionsPrint.ExpandAllGroups = true;
            gvUpdate.OptionsPrint.PrintDetails = true;
            gvUpdate.OptionsPrint.PrintFooter = false;
            gvUpdate.OptionsPrint.PrintHeader = true;
            gvUpdate.OptionsPrint.PrintHorzLines = true;
            gvUpdate.OptionsPrint.PrintVertLines = true;


            gvUpdate.CellMerge += GvUpdate_CellMerge;


            gcUpdate.ForceInitialize();


        }

        private void GvUpdate_CellMerge(object sender, CellMergeEventArgs e)
        {
           GridView gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            if (fieldName == "PK")
            {
              
                e.Merge = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(e.RowHandle1, fieldName)) == ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(e.RowHandle2, fieldName));
                e.Handled = true;
            }
            else
            {
                if (ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(e.RowHandle1, "PK")) != ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(e.RowHandle2, "PK")))
                {
                    e.Merge = false;
                    e.Handled = true;
                }
                else
                {//[g].[Factor],[g].[UC]
                    switch (fieldName)
                    {
                        case "Factor":
                        case "UC":
                        {
                            e.Merge = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(e.RowHandle1, fieldName)) == ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(e.RowHandle2, fieldName));
                            e.Handled = true;
                            }
                            break;
                        default:
                        {
                            e.Merge = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle1, fieldName)) == ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle2, fieldName));
                            e.Handled = true;
                            }
                            break;  
                    }
              
                }
            }
        }










        #endregion

        #endregion


        #region "Process Amount Grid Final"


        private void CreateAmountGridHeader(GridView gv, DataTable dtS)
        {


           


            for (int i = 0; i < dtS.Columns.Count; i++)
            {
                DataColumn col = dtS.Columns[i];
                string colName = col.ColumnName;
                GridColumn gCol = gv.Columns[colName];
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
             

          
                string displayText = "";
                string displayNumber = "";

                HorzAlignment hoz;
                HorzAlignment hozH;
                SummaryItemType sumCol = SummaryItemType.None;
                if (colName == "MainMaterialGroup")
                {
                    displayText = @"Nhóm vật tư";
                    hoz = HorzAlignment.Near;
                    hozH = HorzAlignment.Near;
                    gCol.ColumnEdit = _repositoryTextGrid;
                    gCol.OptionsColumn.AllowMerge = DefaultBoolean.True;
                    //    sumCol = SummaryItemType.Count;
                }
                else if (colName.Equals("RMGroup"))
                {
                    displayText = @"Loại vật tư";
                    hoz = HorzAlignment.Near;
                    hozH = HorzAlignment.Near;
                    gCol.ColumnEdit = _repositoryTextGrid;
                    gCol.OptionsColumn.AllowMerge = DefaultBoolean.False;
                }
                else if (colName.StartsWith("Amount"))
                {
                    displayText = @"Khối lượng";
                    displayNumber = StrFormatFinishingValueDecimal(false, false);
                    sumCol = SummaryItemType.Sum;
                    hozH = HorzAlignment.Center;
                    hoz = HorzAlignment.Far;
                    gCol.OptionsColumn.AllowMerge = DefaultBoolean.False;
                }
                else
                {
                    displayText = @"ĐVT";
                    hoz = HorzAlignment.Center;
                    hozH = HorzAlignment.Center;
                    gCol.ColumnEdit = _repositoryTextGrid;
                    gCol.OptionsColumn.AllowMerge = DefaultBoolean.False;
                }



                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozH;
               





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

             
            }
          

        }












       

        private void LoadDataAmountGridViewFrist(DataTable dtS)
        {


            if (dtS == null || dtS.Rows.Count <= 0)
            {
                gvAmount.Columns.Clear();
                gcAmount.DataSource = null;
                return;
            }









            //   finish:


            gvAmount.BeginUpdate();
            gvAmount.Columns.Clear();
            gcAmount.DataSource = null;
            gcAmount.DataSource = dtS;
            CreateAmountGridHeader(gvAmount, dtS);
      

            gvAmount.BestFitColumns();
            gvAmount.Columns["Amount"].Width += 10;
            gvAmount.EndUpdate();




        }






        #region "GridView Event && Init Grid"



        private void SetUpAmountGrid()
        {


            gcAmount.UseEmbeddedNavigator = true;

            gcAmount.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcAmount.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcAmount.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcAmount.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcAmount.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvAmount.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvAmount.OptionsBehavior.Editable = true;
            gvAmount.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvAmount.OptionsCustomization.AllowColumnMoving = false;
            gvAmount.OptionsCustomization.AllowQuickHideColumns = true;
            gvAmount.OptionsCustomization.AllowSort = false;
            gvAmount.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvAmount.OptionsView.ColumnAutoWidth = false;
            gvAmount.OptionsView.ShowGroupPanel = false;
            gvAmount.OptionsView.ShowIndicator = true;
            gvAmount.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvAmount.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvAmount.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvAmount.OptionsView.ShowAutoFilterRow = false;
            gvAmount.OptionsView.AllowCellMerge = true;
            gvAmount.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvAmount.OptionsNavigation.AutoFocusNewRow = true;
            gvAmount.OptionsNavigation.UseTabKey = true;

            gvAmount.OptionsSelection.MultiSelect = true;
            gvAmount.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvAmount.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvAmount.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvAmount.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvAmount.OptionsView.EnableAppearanceEvenRow = false;
            gvAmount.OptionsView.EnableAppearanceOddRow = false;

            gvAmount.OptionsView.ShowFooter = false;
            gvAmount.OptionsFind.AllowFindPanel = false;
            gvAmount.OptionsFind.AlwaysVisible = false;
            gvAmount.OptionsFind.ShowCloseButton = true;
            gvAmount.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvAmount)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvAmount.OptionsMenu.EnableFooterMenu = false;
            gvAmount.OptionsMenu.EnableColumnMenu = false;

            gvAmount.OptionsView.RowAutoHeight = true;

            gvAmount.OptionsPrint.UsePrintStyles = false;

            gvAmount.OptionsPrint.AllowMultilineHeaders = true;
            gvAmount.OptionsPrint.AutoWidth = true;
            gvAmount.OptionsPrint.EnableAppearanceEvenRow = false;
            gvAmount.OptionsPrint.EnableAppearanceOddRow = false;
            gvAmount.OptionsPrint.ExpandAllDetails = true;
            gvAmount.OptionsPrint.ExpandAllGroups = true;
            gvAmount.OptionsPrint.PrintDetails = true;
            gvAmount.OptionsPrint.PrintFooter = false;
            gvAmount.OptionsPrint.PrintHeader = true;
            gvAmount.OptionsPrint.PrintHorzLines = true;
            gvAmount.OptionsPrint.PrintVertLines = true;
            gcAmount.ForceInitialize();
        }










        #endregion

        #endregion





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
                    displayText = @"Màu sơn";
                    hoz = HorzAlignment.Near;
                    hozH = HorzAlignment.Near;
                    gCol.ColumnEdit = _repositoryTextGrid;
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
            gvPaint.Bands.AddRange(new[] { arrBand[0], arrBand[1], arrBand[2] });



            GridBand gParent = new GridBand();
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gParent.Caption = @"Diện tích sơn";
            gParent.VisibleIndex = 3;
            gParent.Width = 100;

            for (int h = 3; h < dtS.Columns.Count - 1; h++)
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
            foreach (BandedGridColumn col in view.Columns)
            {
                GridBand gBand = col.OwnerBand;
                string fieldName = col.FieldName;
                if (fieldName.StartsWith("FnishingColorPK"))
                {
                    gBand.Visible = false;
                }
                else
                {
                    col.Caption = gBand.Caption;
                    col.Width = col.GetBestWidth() + 10;

                }


            }

            // view.BestFitColumns();
            //   view.Columns["MenuCode"].Width += 20;
            view.OptionsView.ShowColumnHeaders = false;
            view.EndUpdate();



            gvPaint.Bands[0].Visible = false;


        }


        private void LoadDataPaintGridViewFrist(DataTable dtS)
        {
           

            if (dtS == null ||  dtS.Rows.Count <= 0)
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



        #region "Process Paint Grid Final"


        private void CreateBandedGridHeaderAttribute(GridView gv)
        {


        

            for (int i = 0; i < gv.Columns.Count; i++)
            {
           
                GridColumn gCol = gv.Columns[i];
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
              

           

                HorzAlignment hoz = HorzAlignment.Center;
                HorzAlignment hozH = HorzAlignment.Center;
               


                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozH;
                gCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;


            

            }
        

        }













        private void LoadDataAttributeGridViewFrist(DataTable dtS)
        {


          







            //   finish:


            gvAttribute.BeginUpdate();
            gvAttribute.Columns.Clear();
            gcAttribute.DataSource = null;


            gcAttribute.DataSource = dtS;
            CreateBandedGridHeaderAttribute(gvAttribute);
            gvAttribute.BestFitColumns();

            gvAttribute.EndUpdate();




        }






        #region "GridView Event && Init Grid"



        private void SetUpAttributeGrid()
        {


            gcAttribute.UseEmbeddedNavigator = true;

            gcAttribute.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvAttribute.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvAttribute.OptionsBehavior.Editable = true;
            gvAttribute.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvAttribute.OptionsCustomization.AllowColumnMoving = false;
            gvAttribute.OptionsCustomization.AllowQuickHideColumns = true;
            gvAttribute.OptionsCustomization.AllowSort = false;
            gvAttribute.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvAttribute.OptionsView.ColumnAutoWidth = false;
            gvAttribute.OptionsView.ShowGroupPanel = false;
            gvAttribute.OptionsView.ShowIndicator = true;
            gvAttribute.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvAttribute.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvAttribute.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvAttribute.OptionsView.ShowAutoFilterRow = false;
            gvAttribute.OptionsView.AllowCellMerge = false;
            gvAttribute.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvAttribute.OptionsNavigation.AutoFocusNewRow = true;
            gvAttribute.OptionsNavigation.UseTabKey = true;

            gvAttribute.OptionsSelection.MultiSelect = true;
            gvAttribute.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvAttribute.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvAttribute.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvAttribute.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvAttribute.OptionsView.EnableAppearanceEvenRow = false;
            gvAttribute.OptionsView.EnableAppearanceOddRow = false;

            gvAttribute.OptionsView.ShowFooter = true;


            //   gridView1.RowHeight = 25;

            gvAttribute.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvAttribute.OptionsFind.AlwaysVisible = false;
            gvAttribute.OptionsFind.ShowCloseButton = true;
            gvAttribute.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvAttribute)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvAttribute.OptionsMenu.EnableFooterMenu = false;
            gvAttribute.OptionsMenu.EnableColumnMenu = false;

            gvAttribute.OptionsView.RowAutoHeight = true;

            gvAttribute.OptionsPrint.UsePrintStyles = false;

            gvAttribute.OptionsPrint.AllowMultilineHeaders = true;
            gvAttribute.OptionsPrint.AutoWidth = false;
            gvAttribute.OptionsPrint.EnableAppearanceEvenRow = false;
            gvAttribute.OptionsPrint.EnableAppearanceOddRow = false;
            gvAttribute.OptionsPrint.ExpandAllDetails = true;
            gvAttribute.OptionsPrint.ExpandAllGroups = true;
            gvAttribute.OptionsPrint.PrintDetails = true;
            gvAttribute.OptionsPrint.PrintFooter = true;
            gvAttribute.OptionsPrint.PrintHeader = true;
            gvAttribute.OptionsPrint.PrintHorzLines = true;
            gvAttribute.OptionsPrint.PrintVertLines = true;





            gcAttribute.ForceInitialize();


        }










        #endregion

        #endregion





        #region "Process Delete Grid Final"


        private void CreateDeleteGridHeader(GridView gv, DataTable dtS)
        {





            for (int i = 0; i < dtS.Columns.Count; i++)
            {
                DataColumn col = dtS.Columns[i];
                string colName = col.ColumnName;
                GridColumn gCol = gv.Columns[colName];
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;



                string displayText = "";
                string displayNumber = "";
                HorzAlignment hoz;
                HorzAlignment hozH;
                //  DefaultBoolean allowMerge = DefaultBoolean.False;

                switch (colName)
                {
                    case "PK":
                        {
                            displayText = "STT";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                            //   allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "RMCode_001":
                        {
                            displayText = "Mã";
                            hoz = HorzAlignment.Near;
                            hozH = HorzAlignment.Near;
                            //   allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "RMDescription_Fi":
                        {
                            displayText = "Danh mục";
                            hoz = HorzAlignment.Near;
                            hozH = HorzAlignment.Near;
                            //   gCol.ColumnEdit = _repositoryTextGrid;
                            //    allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "PosDesc":
                        {
                            displayText = "Vị Trí";
                            hoz = HorzAlignment.Near;
                            hozH = HorzAlignment.Near;
                            //   allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    case "Factor":
                        {
                            displayText = "SL";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                            //    allowMerge = DefaultBoolean.True;
                            displayNumber = StrFormatFinishingValueDecimal(false, false);
                        }
                        break;
                    case "UC":
                        {
                            displayText = "KL";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                            //       allowMerge = DefaultBoolean.True;
                            displayNumber = StrFormatFinishingValueDecimal(false, false);
                        }
                        break;
                    case "UCUnit":
                        {
                            displayText = "ĐVT";
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                            //    allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                    default:
                        {
                            displayText = colName;
                            hoz = HorzAlignment.Center;
                            hozH = HorzAlignment.Center;
                            //  allowMerge = DefaultBoolean.True;
                            displayNumber = "";
                        }
                        break;
                }






                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozH;
                //    gCol.OptionsColumn.AllowMerge = allowMerge;





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





            }


        }














        private void LoadDataDeleteGridViewFrist(DataTable dtS)
        {
            

            if (dtS == null || dtS.Rows.Count <= 0)
            {
                gvDelete.Columns.Clear();
                gcDelete.DataSource = null;
                return;
            }









            //   finish:


            gvDelete.BeginUpdate();
            gvDelete.Columns.Clear();
            gcDelete.DataSource = null;
            gcDelete.DataSource = dtS;
            CreateDeleteGridHeader(gvDelete, dtS);
            gvDelete.Columns["RMCode_001"].Visible = false;

            gvDelete.BestFitColumns();


            gvDelete.EndUpdate();




        }






        #region "GridView Event && Init Grid"



        private void SetUpDeleteGrid()
        {

            gcDelete.UseEmbeddedNavigator = true;

            gcDelete.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcDelete.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcDelete.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcDelete.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcDelete.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvDelete.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvDelete.OptionsBehavior.Editable = true;
            gvDelete.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvDelete.OptionsCustomization.AllowColumnMoving = false;
            gvDelete.OptionsCustomization.AllowQuickHideColumns = true;
            gvDelete.OptionsCustomization.AllowSort = false;
            gvDelete.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvDelete.OptionsView.ColumnAutoWidth = false;
            gvDelete.OptionsView.ShowGroupPanel = false;
            gvDelete.OptionsView.ShowIndicator = true;
            gvDelete.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvDelete.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvDelete.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvDelete.OptionsView.ShowAutoFilterRow = false;
            gvDelete.OptionsView.AllowCellMerge = false;
            gvDelete.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvDelete.OptionsNavigation.AutoFocusNewRow = true;
            gvDelete.OptionsNavigation.UseTabKey = true;

            gvDelete.OptionsSelection.MultiSelect = true;
            gvDelete.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvDelete.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvDelete.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvDelete.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvDelete.OptionsView.EnableAppearanceEvenRow = false;
            gvDelete.OptionsView.EnableAppearanceOddRow = false;

            gvDelete.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gvDelete.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvDelete.OptionsFind.AlwaysVisible = false;
            gvDelete.OptionsFind.ShowCloseButton = true;
            gvDelete.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvDelete)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvDelete.OptionsMenu.EnableFooterMenu = false;
            gvDelete.OptionsMenu.EnableColumnMenu = false;

            gvDelete.OptionsView.RowAutoHeight = true;

            gvDelete.OptionsPrint.UsePrintStyles = false;

            gvDelete.OptionsPrint.AllowMultilineHeaders = true;
            gvDelete.OptionsPrint.AutoWidth = true;
            gvDelete.OptionsPrint.EnableAppearanceEvenRow = false;
            gvDelete.OptionsPrint.EnableAppearanceOddRow = false;
            gvDelete.OptionsPrint.ExpandAllDetails = true;
            gvDelete.OptionsPrint.ExpandAllGroups = true;
            gvDelete.OptionsPrint.PrintDetails = true;
            gvDelete.OptionsPrint.PrintFooter = false;
            gvDelete.OptionsPrint.PrintHeader = true;
            gvDelete.OptionsPrint.PrintHorzLines = true;
            gvDelete.OptionsPrint.PrintVertLines = true;





            gcDelete.ForceInitialize();


        }









        #endregion

        #endregion




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
