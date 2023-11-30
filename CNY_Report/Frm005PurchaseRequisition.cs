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
    public partial class Frm005PurchaseRequisition : Form
    {
        // private Inf_002BoMReport _inf = new Inf_002BoMReport();
        // private Int64 _pk;
        private readonly DataSet _dsData;

        private readonly RepositoryItemMemoEdit _repositoryTextNormal;
    
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
        public Frm005PurchaseRequisition(DataSet dsDataPara,   Dictionary<string, string> dicUnit)
        {
            InitializeComponent();
            _dicUnit = dicUnit;
         

            _repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            //_repositoryTextNormal.Appearance.Options.UseTextOptions = true;
            //_repositoryTextNormal.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            _dsData = dsDataPara;
          
            InitTreeList(tlMain);
           
            LoadDataTreeList(tlMain, _dsData.Tables[2]);
           
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
            tl.Columns["STT"].SortOrder = SortOrder.Ascending;
            tl.EndSort();

            tl.EndUpdate();

            tl.BeginUpdate();
            tl.OptionsView.ShowColumns = true;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.False;
            tl.BestFitColumns();
        
            if (tl.Columns["RMDescription_002"].Visible && tl.Columns["RMDescription_002"].Width > 190)
                tl.Columns["RMDescription_002"].Width = 190;
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
                    if (colName == "RMDescription_002")
                    {
                        displayText = "Tên Vật Tư";
                        colCaption = "Tên Vật Tư";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("RMCode_001"))
                    {
                        displayText = "Mã Vật Tư";
                        colCaption = "Mã Vật Tư";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("Note"))
                    {
                        displayText = "Ghi Chú";
                        colCaption = "Ghi Chú";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("Unit"))
                    {
                        displayText = "Đơn Vị";
                        colCaption = "Đơn Vị";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("POQty_CNY003"))
                    {
                        displayText = "Khối Lượng";
                        colCaption = "Khối Lượng";
                        hoz = HorzAlignment.Center;
                        formatType = FormatType.Numeric;
                        displayFormat = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
                    }
                    else if (colName.Equals("STT"))
                    {
                        displayText = "STT";
                        colCaption = "STT";
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


            const int fristBandNo = 3;


            for (int e = 0; e < fristBandNo; e++)
            {
                tl.Bands.Add(arrBand[e]);
            }







            string tagLoop = "%%%%%";
            int sumPaint = 0;


            Dictionary<string, BoMPrintTextInfo> dicColFinal = dicCol.Where(p => p.Value.BandCount > 1).ToDictionary(s => s.Key, s => s.Value);

            string sName = "Quy Cách";
           
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

            int beginPaint = sumPaint + fristBandNo;


            for (int m = beginPaint; m < arrBand.Length; m++)
            {
                tl.Bands.Add(arrBand[m]);
            }
           
        

          

        }





        private TreeListBand AddChildBand(TreeListBand rootBand, string sItem)
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
            ////  treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;









        }

        private void TreeList_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {
            if (e.Column.FieldName == "STT")
            {
                if (ProcessGeneral.GetSafeInt(e.Value) <= 0)
                    e.Value = "";
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


            if (node.ParentNode == null)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkRed;
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
            Print1();
            // this.Show();

        }
        private void Print1()
        {


            rpt005PurchaseRequisition rpt = new rpt005PurchaseRequisition(tlMain, _dsData.Tables[0], _dsData.Tables[1], _dsData.Tables[3]);
            ReportPrintTool pt = new ReportPrintTool(rpt);
            Form form = pt.PreviewForm;
            form.MdiParent = this.ParentForm;
            pt.ShowPreview();
            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = string.Format("Print PR No.: {0}", ProcessGeneral.GetSafeString(_dsData.Tables[0].Rows[0]["PRNo"]));
            form.Show();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print1();
        }




     



    }



}
