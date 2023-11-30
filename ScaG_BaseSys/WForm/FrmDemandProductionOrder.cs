using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.Info;
using CNY_BaseSys.Properties;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using CNY_BaseSys.Class;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;

namespace CNY_BaseSys.WForm
{
    //2
    public partial class FrmDemandProductionOrder : FrmBase
    {
        #region "Property"

        private Dictionary<string, ColorDemandPrInfoRpt> _dicColor;
        private readonly Inf_General _inf = new Inf_General();

        private Int64 _cny00019Pk = 0;

        public Int64 Cny00019Pk
        {
            get { return this._cny00019Pk; }
            set { this._cny00019Pk = value; }
        }
        private DataSet _dsProduction;

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private readonly RepositoryItemSpinEdit _repositorySpinN0;
        private Int32 _bomSplitPos = 0;

        private Int64 _cny00012Pk = 0;

        private bool _isSummaryClick = false;
        #endregion


        #region "Contructor"


        public FrmDemandProductionOrder(DataSet dsProduction, Int64 cny00019Pk)
        {

            InitializeComponent();
            xtraTabCenter.ShowTabHeader = DefaultBoolean.False;
            toolTipControllerMain.GetActiveObjectInfo += toolTipControllerMain_GetActiveObjectInfo;
            _dicColor = new Dictionary<string, ColorDemandPrInfoRpt>
            {
                {"RED", new ColorDemandPrInfoRpt
                {
                    ColorSet = Color.Red,
                    DescriptionSet = "<size=10> <b><color=red>*</color></b><backcolor=red>     </backcolor> Missing PR</size>"
                }},
                {"GREEN", new ColorDemandPrInfoRpt
                {
                    ColorSet = Color.Green,
                    DescriptionSet = "<size=10> <b><color=green>*</color></b><backcolor=green>     </backcolor> Planned Qty = Demand</size>"
                }},
                {"YELLOW", new ColorDemandPrInfoRpt
                {
                    ColorSet = Color.Yellow,
                    DescriptionSet = "<size=10> <b><color=yellow>*</color></b><backcolor=yellow>     </backcolor> Planned Qty <> Demand</size>"
                }},
                {"", new ColorDemandPrInfoRpt
                {
                    ColorSet = Color.White,
                    DescriptionSet = "<size=10> <b><color=white>*</color></b><backcolor=white>     </backcolor> No Material Required</size>"
                }},
            };
            _repositorySpinN0 = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = 0,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
                EditMask = "N0"
            };

            _repositorySpinN0.Buttons.Clear();
            _cny00019Pk = cny00019Pk;
            _dsProduction = dsProduction;
            //    this.Text = string.Format("{0} - (Order No. : {1})", this.Text, _soInfo.OrderNo);

            InitTreeList(tlMain);
            btnCheckAll.Click += BtnCheckAll_Click;
            this.Load += FrmDemandProductionOrder_Load;


           
      


            txtCustCode.Properties.ReadOnly = true;
            txtCustName.Properties.ReadOnly = true;
            txtCustOrderNo.Properties.ReadOnly = true;
            txtOrderNo.Properties.ReadOnly = true;
            txtProductionOrderNo.Properties.ReadOnly = true;
            txtProjectNo.Properties.ReadOnly = true;
            txtProjectName.Properties.ReadOnly = true;
            txtCode.Properties.ReadOnly = true;
            txtLine.Properties.ReadOnly = true;
            txtRef.Properties.ReadOnly = true;
            spinOrder.Properties.ReadOnly = true;
            ProcessGeneral.SpinEditSetMinMaxValue(spinOrder, 0, Decimal.MaxValue, 0, false, true, "N0");
            ProcessGeneral.SpinEditSetMinMaxValue(spinPlan, 0, Decimal.MaxValue, 0, false, true, "N0");
            _bomSplitPos = splitCCB.SplitterPosition;
            tgHideSplit.Toggled += TgHideSplit_Toggled;


            SetUpMainGrid();
            SetUpMainSummaryGrid();
            SetUpMainGridDetail();
            InitTreeListDetail(tlDetail);
            btnCal.Click += BtnCal_Click;
            

        }

        
        private void TgHideSplit_Toggled(object sender, EventArgs e)
        {
            if (tgHideSplit.IsOn)
            {
                splitCCB.SplitterPosition = _bomSplitPos;
            }
            else
            {
                _bomSplitPos = splitCCB.SplitterPosition;
                splitCCB.SplitterPosition = 0;
            }
        }
        private void FrmDemandProductionOrder_Load(object sender, EventArgs e)
        {
            ShowGridMain(true);
            LoadDataSo();





            AllowAdd = true;
            AllowEdit = false;
            AllowDelete = false;

            AllowCancel = false;
            AllowRefresh = true;

            AllowPrint = true;
            AllowBreakDown = false;
            AllowRevision = false;
            AllowRangeSize = false;
            AllowCopyObject = false;
            AllowGenerate = false;
            AllowCombine = false;
            AllowCheck = false;
            EnableFind = true;
            EnableRefresh = true;
            EnablePrint = true;
            EnableAdd = true;

            SetCaptionAdd = "Refresh";
            SetImageAdd = SetImageRefresh;

            SetCaptionRefresh = @"View";
            SetImageRefresh = Resources.viewonweb_32x32;
            AllowSave = false;

            AllowFind = true;
            SetCaptionFind = @"Export";
            SetImageFind = Resources.export_32x32;





        }


        private void LoadDataSo()
        {
            LoadDataTreeView(tlMain);
            string productionOrderNo = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["ProductionOrder"]);

            this.Text = string.Format("Production Info - (Production Order No. : {0})", productionOrderNo);
            txtCustCode.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["Customer"]);
            txtCustName.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["CustomerName"]);
            txtCustOrderNo.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["CustomerOrderNo"]);
            txtOrderNo.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["OrderNo"]);
            txtProductionOrderNo.EditValue = productionOrderNo;
            txtProjectName.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["ProjectName"]);
            txtProjectNo.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["ProjectNo"]);

            _cny00012Pk = ProcessGeneral.GetSafeInt64(_dsProduction.Tables[0].Rows[0]["CNY00012PK"]);
        }

    

        #endregion



        #region "Button Click Event"
        private void SetInfoCheckButton(bool enable, Image image, string text)
        {
            btnCheckAll.Image = image;
            btnCheckAll.ToolTip = text;
            btnCheckAll.Enabled = enable;
        }
        private void BtnCheckAll_Click(object sender, EventArgs e)
        {
            if (tlMain.AllNodesCount <= 0) return;
            if (btnCheckAll.ToolTip == @"Check All")
            {
                foreach (TreeListNode node in tlMain.Nodes)
                {
                    node.Checked = true;
                }

                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
            }
            else
            {
                foreach (TreeListNode node in tlMain.Nodes)
                {
                    node.Checked = false;
                }

                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }


        }
        private void BtnCal_Click(object sender, EventArgs e)
        {


            if (_isSummaryClick)
            {
                ShowDetalGroup(gvSumary.FocusedRowHandle, gvSumary.FocusedColumn.FieldName, true);
            }
            else
            {
                ShowDetalGroup(gvPaint.FocusedRowHandle, gvPaint.FocusedColumn.FieldName, true);
            }
            

        }

        #endregion



        #region "Init TreeView"

        private void LoadDataTreeView(TreeList tl)
        {


     






            DataTable dt = StandardTreeTableSo();




            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";


            tl.BeginUpdate();

            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "TDG00001PK", "BOMID", "SortOrderNode", "PlanQuantity", "OrderLine");












            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["OrderLine"], "Order Line", true, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProDimension"], "Dimension", true, HorzAlignment.Near, TreeFixedStyle.None, "");



            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductCode"], "Product Code", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Reference"], "Reference", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductName"], "Product Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["OrderQuantity"], "Order Quantity", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            tl.Columns["OrderQuantity"].Format.FormatType = FormatType.Numeric;
            tl.Columns["OrderQuantity"].Format.FormatString = "N0";

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["PlanQuantity"], "Plan Quantity", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["PlanQuantity"].Format.FormatType = FormatType.Numeric;
            tl.Columns["PlanQuantity"].Format.FormatString = "N0";
            tl.Columns["PlanQuantity"].ImageIndex = 0;
            tl.Columns["PlanQuantity"].ImageAlignment = StringAlignment.Far;
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FinishingColor"], "Finishing Color", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["BoMNo"], "BOM No.", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.ExpandAll();
            tl.BestFitColumns();
            if (tl.Columns["FinishingColor"].Width > 200)
            {
                tl.Columns["FinishingColor"].Width = 200;
            }
            tl.ForceInitialize();
            tl.BeginSort();
            tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();

            tl.EndUpdate();


            foreach (TreeListNode node in tl.Nodes)
            {
                node.Checked = true;
            }
           

            SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All"); 


           


        }

        private DataTable StandardTreeTableSo()
        {



            DataTable dtTreeTemp = _dsProduction.Tables[2];

            DataTable dtBi = _dsProduction.Tables[1];


            Dictionary<Int64, SOBOMIDNOINFO> qF2 = dtBi.AsEnumerable().GroupBy(p => p.Field<Int64>("CNY00020PK")).Select(t => new
            {
                KeyDic = t.Key,
                TempData = new SOBOMIDNOINFO
                {
                    StrBOMID = string.Join(",", t.Select(s => ProcessGeneral.GetSafeString(s["BOMID"])).Where(m => !string.IsNullOrEmpty(m)).Distinct().ToArray()),
                    StrBoMNo = string.Join(",", t.Select(s => ProcessGeneral.GetSafeString(s["BoMNo"])).Where(m => !string.IsNullOrEmpty(m)).Distinct().ToArray())
                },

            }).ToDictionary(item => item.KeyDic, item => item.TempData);
            foreach (DataRow drSource in dtTreeTemp.Rows)
            {
                Int64 cny00020Pk = ProcessGeneral.GetSafeInt64(drSource["ChildPK"]);
                SOBOMIDNOINFO itemBoM;
                if (qF2.TryGetValue(cny00020Pk, out itemBoM))
                {
                    drSource["BoMNo"] = itemBoM.StrBoMNo;
                    drSource["BOMID"] = itemBoM.StrBOMID;

                }
            }
            dtTreeTemp.AcceptChanges();
            return dtTreeTemp;




        }


        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.Product_BoM_16x16);
            imgLt.Images.Add(Resources.RawMaterial_BoM_16x16);
            return imgLt;
        }


        private void InitTreeList(TreeList treeList)
        {

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
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

            treeList.OptionsBehavior.Editable = true; treeList.OptionsView.ShowColumns = true;
            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;
            treeList.OptionsView.AutoWidth = false;
            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;
            treeList.StateImageList = GetImageListDisplayTreeView();
            treeList.OptionsBehavior.AutoChangeParent = false;
            treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            treeList.OptionsBehavior.AutoNodeHeight = true;

            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = true;




            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };


            treeList.GetStateImage += TreeList_GetStateImage;
            treeList.ShowingEditor += TreeList_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;





            treeList.NodeCellStyle += TreeList_NodeCellStyle;






            treeList.AfterCheckNode += TreeList_AfterCheckNode;

            treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;

            treeList.ShownEditor += TreeList_ShownEditor;
        }



        private void TreeList_ShownEditor(object sender, EventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "PlanQuantity":
                    if (!node.HasChildren)
                    {
                        var editor = tl.ActiveEditor as SpinEdit;
                        if (editor != null)
                        {
                            editor.Properties.MinValue = 0;
                            editor.Properties.MaxValue = ProcessGeneral.GetSafeInt(node.GetValue("OrderQuantity"));
                        }
                    }

                    break;
            }

        }

        private void TreeList_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            string fieldName = e.Column.FieldName;

            switch (fieldName)
            {
                case "PlanQuantity":
                    e.RepositoryItem = _repositorySpinN0;
                    break;


            }

        }

        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = e.Node.HasChildren ? 0 : 1;
        }


        private void TreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            List<TreeListNode> lCheckNode = tl.GetAllCheckedNodes().ToList();
            if (lCheckNode.Count == tl.AllNodesCount)
            {
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");

            }
            else
            {
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }
 
        }
       
      
        private void TreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
       
            bool isCheck = e.Node.CheckState == CheckState.Checked;

            switch (fieldName)
            {
                case "OrderLine":
                {
                    if (node.GetDisplayText("BoMNo").Trim() == "")
                    {
                        e.Appearance.ForeColor = Color.Red;
                        }
                }
                    break;
                case "OrderQuantity":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "PlanQuantity":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;

            }

           if (fieldName == "Reference" || fieldName == "BoMNo" || fieldName == "OrderLine" || fieldName == "ProductCode" || fieldName == "Reference" || fieldName == "ProductName")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.LavenderBlush;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "OrderQuantity")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.GhostWhite;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "PlanQuantity")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Lavender;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else
            {
                if (isCheck)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
            }



        }

        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "PlanQuantity":
                    e.Cancel = false;

                    break;
                default:
                    e.Cancel = true;
                    break;
            }


        }

        



        private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;


            bool isCheck = e.Node.CheckState == CheckState.Checked;

            LinearGradientBrush backBrush;

            if (isCheck)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }


            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            if (isCheck)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = e.Node.HasChildren ? Color.DarkMagenta : Color.DarkOrchid;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }


            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            string value = (tl.GetVisibleIndexByNode(e.Node) + 1).ToString().Trim();
            e.Graphics.DrawString(value, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache),
                e.Bounds, e.Appearance.GetStringFormat());
            e.ImageIndex = -1;
            e.Handled = true;

        }

        //  private int updateRmSourceNo = 1;
        #endregion



        #region
        private DataTable GetTableSoLinePk(bool isCheckPlan)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00012PK", typeof(Int64));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("PlanQty", typeof(Int32));
            dt.Columns.Add("SOQty", typeof(Int32));
            var q1 = tlMain.GetAllCheckedNodes().Select(p => new
            {
                StrCNY00012PK = ProcessGeneral.GetSafeString(p.GetValue("BOMID")),
                CNY00020PK = ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK")),
                PlanQty = ProcessGeneral.GetSafeInt(p.GetValue("PlanQuantity")),
                SOQty = ProcessGeneral.GetSafeInt(p.GetValue("OrderQuantity")),
            }).Where(p =>  p.SOQty > 0 && p.StrCNY00012PK != "" && p.CNY00020PK > 0 && (!isCheckPlan || p.PlanQty > 0)).Distinct().ToList();


            foreach (var item in q1)
            {
                string strCny00012Pk = item.StrCNY00012PK;
                Int64[] arrBomId = strCny00012Pk.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => ProcessGeneral.GetSafeInt64(p.Trim())).Where(p => p > 0).Distinct().ToArray();
                foreach (Int64 cny00012Pk in arrBomId)
                {
                    dt.Rows.Add(cny00012Pk, item.CNY00020PK, item.PlanQty, item.SOQty);
                }
            }
            return dt;
        }

        #endregion


       



    

        #region "Override Button Click Event"

        protected override void PerformRefresh()
        {



            DataTable dtLine = GetTableSoLinePk(false);
            LoadDataCheckMain(dtLine);

        }

        protected override void PerformClose()
        {



            if (xtraTabCenter.SelectedTabPage == xtraTabDetail)
            {
                ShowGridMain(true);
                return;
            }

            base.PerformClose();
        }

        protected override void PerformPrint()
        {


            GridControl gcMain = xtraTabCenter.SelectedTabPage == xtraTabDetail ? gcDetail : gcPaint;
            GridView gvMain = (GridView)gcMain.FocusedView;
      
            if (gvMain == null || gvMain.RowCount <= 0)
            {
                XtraMessageBox.Show(@"No data display for perform printing...!!!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!gcMain.IsPrintingAvailable)
            {
                XtraMessageBox.Show(@"The 'DevExpress.XtraPrinting' library is not found", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
         
            gcMain.ShowPrintPreview();
         
        }

        protected override void PerformFind()
        {
            GridControl gcMain = xtraTabCenter.SelectedTabPage == xtraTabDetail ? gcDetail : gcPaint;
            GridView gvMain = (GridView)gcMain.FocusedView;

            if (gvMain == null || gvMain.RowCount <= 0)
            {
                XtraMessageBox.Show(@"No data display for perform exporting...!!!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var f = new SaveFileDialog()
            {
                Title = @"Export Data",
                Filter = @"Excel 2007,2010,2013 Files (*.xlsx)|*.xlsx|Excel 2003 files (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv|Text (Tab delimited) (*.txt)|*.txt|Pdf files (*.pdf)|*.pdf",
                RestoreDirectory = true
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                
                string pathExport = f.FileName;
                switch (Path.GetExtension(pathExport).ToLower().Trim())
                {
                    case ".pdf":
                        gcMain.ExportToPdf(pathExport);
                        break;
                    case ".xlsx":
                        gcMain.ExportToXlsx(pathExport, new XlsxExportOptions { TextExportMode = TextExportMode.Text,});
                        break;
                    case ".xls":
                        gcMain.ExportToXls(pathExport, new XlsExportOptions { TextExportMode = TextExportMode.Text });
                        break;
                    case ".csv"://CsvExportOptions
                        gcMain.ExportToCsv(pathExport, new CsvExportOptions { TextExportMode = TextExportMode.Text, });
                        break;
                    case ".txt":
                        gcMain.ExportToText(pathExport, new TextExportOptions{TextExportMode = TextExportMode.Text});
                        break;
                }
              
                XtraMessageBox.Show("Export data complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                DialogResult dlResult = XtraMessageBox.Show("Do you want to open file after export complete ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlResult == DialogResult.Yes)
                {

                    Process.Start(pathExport);
                }
            }
        }



        protected override void PerformAdd()
        {
            WaitDialogForm dlg = new WaitDialogForm("");
            LoadDataSo();
            dlg.Close();
        }


        #endregion


        #region "GridView Event && Init Grid"

        private void ShowGridMain(bool value)
        {
            //gcPaint.Visible = value;
            //pcInfo.Visible = !value;

            xtraTabGeneral.PageVisible = value;
            xtraTabDetail.PageVisible = !value;

          


            EnableAdd = value;
            EnableRefresh = value;
        }
        private void ResetDataPaintGrid()
        {
            gvPaint.BeginUpdate();
            gvPaint.Bands.Clear();
            gvPaint.Columns.Clear();
            gcPaint.DataSource = null;
            gvPaint.EndUpdate();
        }

        private bool _isCust = false;
        private void LoadDataCheckMain(DataTable dt04)
        {
            if (dt04.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No Item Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WaitDialogForm dlg = new WaitDialogForm();
            _isCust = chKHCC.Checked;
            DataTable dt = _inf.Report_LoadDataCheckMain_F1(dt04, _cny00012Pk, _isCust);


            int rC = dt.Rows.Count;
            if (rC <= 0)
            {
                ResetDataPaintGrid();
                ResetDataSummaryGrid();
                dlg.Close();
                XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }








            var lTree = tlMain.GetAllNodeTreeList().Select(p => new
            {
                CNY00020PK = ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK")),
                OrderLine = ProcessGeneral.GetSafeString(p.GetValue("OrderLine")),
                ProductCode = ProcessGeneral.GetSafeString(p.GetValue("ProductCode")),
                Reference = ProcessGeneral.GetSafeString(p.GetValue("Reference")),
                SortOrderNode = ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode")),
                SOQty = ProcessGeneral.GetSafeInt(p.GetValue("OrderQuantity")),
            }).ToList();

            var qPivot = dt.AsEnumerable().GroupBy(p => new
            {
                CNY00020PK = p.Field<Int64>("CNY00020PK"),
                //  p.Index,
                //     p.TDG00001PK,


            }).Where(myGroup => myGroup.Any())
                .Select(myGroup => new
                {
                    //  myGroup.Key.TDG00001PK,
                    myGroup.Key.CNY00020PK,
                    GroupIndexAggreate = myGroup.GroupBy(f => new
                    {
                        MainMaterialGroupCode = f.Field<string>("MainMaterialGroupCode"),
                        //   MainMaterialGroupDesc = f.Field<string>("MainMaterialGroupDesc"),

                    }).Select(m => new BOMCHECKPIVOTINFO
                    {
                        MainMaterialGroupCode = m.Key.MainMaterialGroupCode,
                        FinishPR = m.Select(s => s.Field<string>("FinishPR")).FirstOrDefault(),
                        FinishPO = m.Select(s => s.Field<string>("FinishPO")).FirstOrDefault(),
                    }).ToList()
                }).Join(lTree, p => new
                {
                    p.CNY00020PK
                }, t => new
                {
                    t.CNY00020PK
                }, (p, t) => new
                {
                    t.CNY00020PK,
                    t.OrderLine,
                    t.ProductCode,
                    t.Reference,
                    t.SOQty,
                    p.GroupIndexAggreate,
                    t.SortOrderNode
                }).OrderBy(p => p.SortOrderNode).Select((p, idx) => new
                {
                    p.CNY00020PK,
                    p.OrderLine,
                    p.ProductCode,
                    p.Reference,
                    p.SOQty,
                    p.GroupIndexAggreate
                }).ToList();



            Dictionary<string, string> qLoop = dt.AsEnumerable().Select(p => new
            {
                MainMaterialGroupCode = p.Field<string>("MainMaterialGroupCode"),
                MainMaterialGroupDesc = p.Field<string>("MainMaterialGroupDesc")
            }).Distinct().OrderBy(p => p.MainMaterialGroupCode).ToDictionary(t => t.MainMaterialGroupCode, t => t.MainMaterialGroupDesc);


            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("CNY00020PK", typeof(Int64));
            dtFinal.Columns.Add("OrderLine", typeof(string));
            dtFinal.Columns.Add("ProductCode", typeof(string));
            dtFinal.Columns.Add("Reference", typeof(string));
            dtFinal.Columns.Add("SOQty", typeof(int));

            DataTable dtSumary = new DataTable();
            dtSumary.Columns.Add("ColorType", typeof(string));
            dtSumary.Columns.Add("ColorDesc", typeof(string));
            dtSumary.Rows.Add("GREEN", "Planned Qty = Demand");
            dtSumary.Rows.Add("YELLOW", "Planned Qty<> Demand(Full Item)");
            dtSumary.Rows.Add("RED", "Missing PR / PO");




            foreach (var itemMain in qLoop)
            {

                dtFinal.Columns.Add(string.Format("FinishPR_{0}", itemMain.Key), typeof(string));
                dtFinal.Columns.Add(string.Format("FinishPO_{0}", itemMain.Key), typeof(string));
                dtSumary.Columns.Add(string.Format("FinishPR_{0}", itemMain.Key), typeof(string));
                dtSumary.Columns.Add(string.Format("FinishPO_{0}", itemMain.Key), typeof(string));
            }



            Dictionary<string, int> dicRowFooter = new Dictionary<string, int> {{"GREEN", 0}, { "YELLOW", 1 } , { "RED", 2 } };


            foreach (var item in qPivot)
            {
                DataRow drFinal = dtFinal.NewRow();
                drFinal["CNY00020PK"] = item.CNY00020PK;
                drFinal["OrderLine"] = item.OrderLine;
                drFinal["ProductCode"] = item.ProductCode;
                drFinal["Reference"] = item.Reference;
                drFinal["SOQty"] = item.SOQty;
                List<BOMCHECKPIVOTINFO> lInfo = item.GroupIndexAggreate;

                foreach (BOMCHECKPIVOTINFO info in lInfo)
                {
                    string prField = string.Format("FinishPR_{0}", info.MainMaterialGroupCode);
                    string poField = string.Format("FinishPO_{0}", info.MainMaterialGroupCode);
                    drFinal[prField] = info.FinishPR;
                    drFinal[poField] = info.FinishPO;

                    if (!string.IsNullOrEmpty(info.FinishPR))
                    {
                        DataRow rowPrIndPr = dtSumary.Rows[dicRowFooter[info.FinishPR]];
                        string valueRowPr = ProcessGeneral.GetSafeString(rowPrIndPr[prField]);
                        if (string.IsNullOrEmpty(valueRowPr))
                        {
                            rowPrIndPr[prField] = info.FinishPR;
                        }
                    }



                    if (!string.IsNullOrEmpty(info.FinishPO))
                    {
                        DataRow rowPrIndPo = dtSumary.Rows[dicRowFooter[info.FinishPO]];
                        string valueRowPo = ProcessGeneral.GetSafeString(rowPrIndPo[poField]);
                        if (string.IsNullOrEmpty(valueRowPo))
                        {
                            rowPrIndPo[poField] = info.FinishPO;
                        }
                    }

                }





                dtFinal.Rows.Add(drFinal);
            };
            dtSumary.AcceptChanges();
            // dt.AsEnumerable().AsParallel().AsOrdered().WithExecutionMode(ParallelExecutionMode.ForceParallelism).Where(e => e.StartsWith("J"));

            int rcSumary = dtSumary.Rows.Count;
            for (int j = rcSumary - 1; j >= 0; j--)
            {
                DataRow drSu = dtSumary.Rows[j];
                bool isDelSu = true;
                foreach (var itemMain in qLoop)
                {
                    if (ProcessGeneral.GetSafeString(drSu[string.Format("FinishPR_{0}", itemMain.Key)]) != "" || ProcessGeneral.GetSafeString(drSu[string.Format("FinishPO_{0}", itemMain.Key)]) != "")
                    {
                        isDelSu = false;
                        break;
                    }
                }

                if (isDelSu)
                {
                    dtSumary.Rows.RemoveAt(j);
                }
            }
            dtSumary.AcceptChanges();

            gvPaint.BeginUpdate();
            gvPaint.Bands.Clear();
            gvPaint.Columns.Clear();
            gcPaint.DataSource = null;
    
            CreateBandedGridHeader(dtFinal, qLoop);
            gcPaint.DataSource = dtFinal;
            BestFitBandsGrid(gvPaint);
           
            gvPaint.EndUpdate();


            LoadDataGridSummary(dtSumary, qLoop);
            dlg.Close();
            //  LoadDataFinishing(spFinishing, dtLine);
        }

        private void CreateBandedGridHeader(DataTable dtS, Dictionary<string, string> dicCg)
        {

            GridBand[] arrBand = new GridBand[dtS.Columns.Count];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {
                BandedGridColumn gCol = new BandedGridColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new GridBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                HorzAlignment hozAlign = HorzAlignment.Center;
                string colName = col.ColumnName;
                string displayText = "";
                if (colName == "CNY00020PK")
                {
                    displayText = "CNY00020PK";
                    arrBand[i].RowCount = 3;
                }
                else if (colName == "OrderLine")
                {
                    displayText = "Line";
                    arrBand[i].RowCount = 3;
                }
                else if (colName == "ProductCode")
                {
                    displayText = "Prod. Code";
                    hozAlign = HorzAlignment.Near;
                  //  arrBand[i].RowCount = 3;
                }
                else if (colName == "Reference")
                {
                    displayText = "Reference";
                    hozAlign = HorzAlignment.Near;
                    arrBand[i].RowCount = 3;
                }
                else if (colName == "SOQty")
                {
                    displayText = "Order Qty";
                    gCol.DisplayFormat.FormatType = FormatType.Numeric;
                    gCol.DisplayFormat.FormatString = FunctionFormatModule.StrFormatOrderQtyDecimal(false, false);
                    arrBand[i].RowCount = 3;
                }
                else if (colName.StartsWith("FinishPR_"))
                {
                    displayText = "PR";
                    arrBand[i].RowCount = 1;
                }
                else if (colName.StartsWith("FinishPO_"))
                {
                    displayText = "PO";
                    arrBand[i].RowCount = 1;
                }
                else
                {
                    displayText = colName;
                }



                //gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                //gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
                //arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;

                string[] arrTag = colName.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                string tag = "";
                if (arrTag.Length == 2)
                {
                    
                    tag = arrTag[1].Trim();


                }



                gCol.AppearanceCell.TextOptions.HAlignment = hozAlign;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].Tag = tag;

                gCol.Tag = tag;
                gCol.Caption = displayText;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;

          


                gvPaint.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].VisibleIndex = 0;
                arrBand[i].Width = 100;
                i++;
            }
            gvPaint.Bands.AddRange(new[] { arrBand[0], arrBand[1], arrBand[2], arrBand[3], arrBand[4] });

            int t = 5;
            foreach (var itemTemp in dicCg)
            {
              
                string sName = itemTemp.Value;
                string sKey = itemTemp.Key;
                GridBand gParent = new GridBand();
                gParent.AppearanceHeader.Options.UseTextOptions = true;
                gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                gParent.AppearanceHeader.Options.UseFont = true;
                gParent.RowCount = 2;
                gParent.Caption = string.Format("{0}-{1}", sKey, sName);
                gParent.VisibleIndex = t;
                gParent.Width = 100;
                // gParent.RowCount = 2;
                var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Tag) == sKey).ToArray();
                for (int h = 0; h < q2.Length; h++)
                {
                    q2[h].VisibleIndex = h;
                    gParent.Children.Add(q2[h]);
                }
                gvPaint.Bands.Add(gParent);
                t++;
            }


        }
        private void BestFitBandsGrid(BandedGridView view)
        {

            view.BeginUpdate();
            view.OptionsView.ShowColumnHeaders = true;


            foreach (BandedGridColumn col in view.Columns)
            {
                GridBand gBand = col.OwnerBand;string fieldName = col.FieldName;
                if (fieldName == "CNY00020PK" || fieldName == "OrderLine" || fieldName == "ProductCode")
                {
                    gBand.Visible = false;
                }
                else
                {
                    col.Caption = gBand.Caption;
                    if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
                    {
                        col.Width = 50;
                    }
                    else 
                    {
                        col.Width = col.GetBestWidth() + 10;
                    }

                }


            }

            // view.BestFitColumns();
            //   view.Columns["MenuCode"].Width += 20;
            view.OptionsView.ShowColumnHeaders = false;


           
         


                view.EndUpdate();



          //  view.Bands[1].Visible = false;


        }




        private void SetUpMainGrid()
        {


            gcPaint.UseEmbeddedNavigator = true;

            gcPaint.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcPaint.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvPaint.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

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

            

            gvPaint.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Wrap;
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

            gvPaint.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gvPaint.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvPaint.OptionsFind.AlwaysVisible = false;
            gvPaint.OptionsFind.ShowCloseButton = true;
            gvPaint.OptionsFind.HighlightFindResults = true;



            gvPaint.OptionsPrint.PrintBandHeader = true;
            gvPaint.OptionsPrint.AllowCancelPrintExport = true;
            gvPaint.OptionsPrint.AllowMultilineHeaders = true;
            gvPaint.OptionsPrint.AutoResetPrintDocument = true;
            gvPaint.OptionsPrint.AutoWidth = false;
            gvPaint.OptionsPrint.PrintHeader = false;
            gvPaint.OptionsPrint.PrintPreview = true;
            gvPaint.OptionsPrint.PrintSelectedRowsOnly = false;
            gvPaint.OptionsPrint.UsePrintStyles = false;

            new MyFindPanelFilterHelper(gvPaint)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvPaint.OptionsMenu.EnableFooterMenu = false;
            gvPaint.OptionsMenu.EnableColumnMenu = false;

            


            gvPaint.RowCellStyle += gvPaint_RowCellStyle;

            gvPaint.LeftCoordChanged += gvPaint_LeftCoordChanged;
            gvPaint.MouseMove += gvPaint_MouseMove;
            gvPaint.TopRowChanged += gvPaint_TopRowChanged;
            gvPaint.FocusedColumnChanged += gvPaint_FocusedColumnChanged;
            gvPaint.FocusedRowChanged += gvPaint_FocusedRowChanged;
            gvPaint.RowCountChanged += gvPaint_RowCountChanged;
            gvPaint.CustomDrawRowIndicator += gvPaint_CustomDrawRowIndicator;

            gcPaint.Paint += gcPaint_Paint;
            gcPaint.KeyDown += gcPaint_KeyDown;
            gcPaint.EditorKeyDown += gcPaint_EditorKeyDown;
            gvPaint.ShowingEditor += gvPaint_ShowingEditor;
            gvPaint.CustomColumnDisplayText += GvPaint_CustomColumnDisplayText;
            gvPaint.DoubleClick += gvPaint_DoubleClick;




            gcPaint.ForceInitialize();



        }


        private void gvPaint_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as BandedGridView;
            if (gv == null) return;
            GridControl gc = gv.GridControl;
            BandedGridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
            if (!hi.InRowCell) return;
            BandedGridColumn gCol = hi.Column;
            if (gCol == null) return;
            _isSummaryClick = false;
            ShowDetalGroup(hi.RowHandle, gCol.FieldName,false);
        }

        private bool _checkKeyDown;
        private void gcPaint_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDown)
            {
                gcPaint_KeyDown(sender, e);
            }
            _checkKeyDown = false;
        }


        private void gcPaint_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = (GridControl) sender;
            if (gc == null) return;
            var gv = (GridView) gc.FocusedView;
            if (gv == null) return;
            GridColumn gColF = gv.FocusedColumn;
            string fieldName = gColF.FieldName;

            int rH = gv.FocusedRowHandle;
            _checkKeyDown = true;


            #region "Process Enter key"

            if (e.KeyData == Keys.Enter)
            {
                _isSummaryClick = false;
                ShowDetalGroup(rH, fieldName, false);
                return;
            }

            #endregion

            





        }



        #region "Key Down"


     
        #endregion

     
        private void GvPaint_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;

            if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
            {
                e.DisplayText = "";
            }
        }
        private void gvPaint_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void gvPaint_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = (GridView) sender;
            if (gv == null) return;
            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            int visibleIndex = gCol.VisibleIndex;
            if (visibleIndex < 0) return;
            string fieldName = gCol.FieldName;

            switch (fieldName)
            {
                case "OrderLine":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkGreen;
                    break;
                case "ProductCode":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkRed;
                    break;
                case "Reference":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkRed;
                    break;
                case "SOQty":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                    break;
            }

            if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(rH, gCol))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }


           if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
            {
                string displayText = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fieldName));
                ColorDemandPrInfoRpt infoColor;
                if (displayText != "" && _dicColor.TryGetValue(displayText, out infoColor))
                {
                //    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = infoColor.ColorSet;
                  //  e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                }

            }
            else if (fieldName == "OrderLine" || fieldName == "SOQty")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.WhiteSmoke;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
           else if (fieldName == "ProductCode" || fieldName == "Reference")
           {
               e.Appearance.GradientMode = LinearGradientMode.Vertical;
               e.Appearance.BackColor = Color.Cornsilk;
               e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
           }

        }

        private void gvPaint_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvPaint_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvPaint_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvPaint_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvPaint_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcPaint_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvPaint_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvPaint_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //GetStatusPriorityPaintOnRow(GridView gv, int rH)
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
            bool isSelected = gv.IsRowSelected(e.RowHandle);
            LinearGradientBrush backBrush;
            if (isSelected)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

            if (isSelected)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());



        }


        #endregion






        #region "GridView Event && Init Grid Summary"

   
        private void ResetDataSummaryGrid()
        {
            gvSumary.BeginUpdate();
            gvSumary.Bands.Clear();
            gvSumary.Columns.Clear();
            gcSumary.DataSource = null;
            gvSumary.EndUpdate();
        }

  
        private void LoadDataGridSummary(DataTable dtSumary, Dictionary<string, string> qLoop)
        {
          






        

            //DataTable dtSumary = new DataTable();
            //dtSumary.Columns.Add("ColorType", typeof(string));
            //dtSumary.Columns.Add("ColorDesc", typeof(string));
            //dtSumary.Rows.Add("GREEN", "Planned Qty = Demand");
            //dtSumary.Rows.Add("YELLOW", "Planned Qty<> Demand(Full Item)");
            //dtSumary.Rows.Add("RED", "Missing PR / PO");






            gvSumary.BeginUpdate();
            gvSumary.Bands.Clear();
            gvSumary.Columns.Clear();
            gcSumary.DataSource = null;

            CreateBandedGridSummaryHeader(dtSumary, qLoop);
            gcSumary.DataSource = dtSumary;
            BestFitBandsSummaryGrid(gvSumary);

            gvSumary.IndicatorWidth = gvPaint.IndicatorWidth;

            gvSumary.EndUpdate();



            //  LoadDataFinishing(spFinishing, dtLine);
        }

        private void CreateBandedGridSummaryHeader(DataTable dtS, Dictionary<string, string> dicCg)
        {

            GridBand[] arrBand = new GridBand[dtS.Columns.Count];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {
                BandedGridColumn gCol = new BandedGridColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new GridBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                HorzAlignment hozAlign = HorzAlignment.Center;
                string colName = col.ColumnName;
                string displayText = "";
                if (colName == "ColorType")
                {
                    displayText = "Color";
                    arrBand[i].RowCount = 3;
                }
                else if (colName == "ColorDesc")
                {
                    displayText = "Description";
                    hozAlign = HorzAlignment.Near;
                    //  arrBand[i].RowCount = 3;
                }
                else if (colName.StartsWith("FinishPR_"))
                {
                    displayText = "PR";
                    arrBand[i].RowCount = 1;
                }
                else if (colName.StartsWith("FinishPO_"))
                {
                    displayText = "PO";
                    arrBand[i].RowCount = 1;
                }
                else
                {
                    displayText = colName;
                }



                //gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                //gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
                //arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;

                string[] arrTag = colName.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                string tag = "";
                if (arrTag.Length == 2)
                {

                    tag = arrTag[1].Trim();


                }



                gCol.AppearanceCell.TextOptions.HAlignment = hozAlign;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].Tag = tag;

                gCol.Tag = tag;
                gCol.Caption = displayText;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;




                gvSumary.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].VisibleIndex = 0;
                arrBand[i].Width = 100;
                i++;
            }
            gvSumary.Bands.AddRange(new[] { arrBand[0], arrBand[1] });

            int t = 2;
            foreach (var itemTemp in dicCg)
            {

                string sName = itemTemp.Value;
                string sKey = itemTemp.Key;
                GridBand gParent = new GridBand();
                gParent.AppearanceHeader.Options.UseTextOptions = true;
                gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                gParent.AppearanceHeader.Options.UseFont = true;
                gParent.RowCount = 2;
                gParent.Caption = string.Format("{0}-{1}", sKey, sName);
                gParent.VisibleIndex = t;
                gParent.Width = 100;
                // gParent.RowCount = 2;
                var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Tag) == sKey).ToArray();
                for (int h = 0; h < q2.Length; h++)
                {
                    q2[h].VisibleIndex = h;
                    gParent.Children.Add(q2[h]);
                }
                gvSumary.Bands.Add(gParent);
                t++;
            }


        }
        private void BestFitBandsSummaryGrid(BandedGridView view)
        {

            view.BeginUpdate();
            view.OptionsView.ShowColumnHeaders = true;

            int widthCross = 0;
            int totalWidth = 0;
            foreach (BandedGridColumn col in gvPaint.VisibleColumns)
            {
                string fieldName = col.FieldName;
                if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
                {
                    view.Columns[fieldName].Width = col.Width;
                    widthCross+= col.Width;
                }

                totalWidth+= col.Width;



               


            }
            int w1= view.Columns["ColorType"].GetBestWidth() + 10;
            widthCross += w1;
            view.Columns["ColorType"].Width = w1;


            int leftWidth = totalWidth - widthCross;
            view.Columns["ColorDesc"].Width = leftWidth;
            view.OptionsView.ShowColumnHeaders = false;
            view.EndUpdate();


        }




        private void SetUpMainSummaryGrid()
        {


            gcSumary.UseEmbeddedNavigator = false;

            gcSumary.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcSumary.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcSumary.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcSumary.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcSumary.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvSumary.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

            gvSumary.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvSumary.OptionsBehavior.Editable = true;
            gvSumary.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvSumary.OptionsCustomization.AllowColumnMoving = false;
            gvSumary.OptionsCustomization.AllowQuickHideColumns = true;
            gvSumary.OptionsCustomization.AllowSort = false;
            gvSumary.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvSumary.OptionsView.ColumnAutoWidth = false;
            gvSumary.OptionsView.ShowGroupPanel = false;
            gvSumary.OptionsView.ShowIndicator = true;
            gvSumary.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvSumary.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvSumary.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvSumary.OptionsView.ShowAutoFilterRow = false;
            gvSumary.OptionsView.AllowCellMerge = false;
            gvSumary.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;



            gvSumary.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Wrap;
            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvSumary.OptionsNavigation.AutoFocusNewRow = true;
            gvSumary.OptionsNavigation.UseTabKey = true;

            gvSumary.OptionsSelection.MultiSelect = true;
            gvSumary.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvSumary.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvSumary.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvSumary.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvSumary.OptionsView.EnableAppearanceEvenRow = false;
            gvSumary.OptionsView.EnableAppearanceOddRow = false;

            gvSumary.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gvSumary.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvSumary.OptionsFind.AlwaysVisible = false;
            gvSumary.OptionsFind.ShowCloseButton = true;
            gvSumary.OptionsFind.HighlightFindResults = true;



            gvSumary.OptionsPrint.PrintBandHeader = true;
            gvSumary.OptionsPrint.AllowCancelPrintExport = true;
            gvSumary.OptionsPrint.AllowMultilineHeaders = true;
            gvSumary.OptionsPrint.AutoResetPrintDocument = true;
            gvSumary.OptionsPrint.AutoWidth = false;
            gvSumary.OptionsPrint.PrintHeader = false;
            gvSumary.OptionsPrint.PrintPreview = true;
            gvSumary.OptionsPrint.PrintSelectedRowsOnly = false;
            gvSumary.OptionsPrint.UsePrintStyles = false;

            new MyFindPanelFilterHelper(gvSumary)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvSumary.OptionsMenu.EnableFooterMenu = false;
            gvSumary.OptionsMenu.EnableColumnMenu = false;




            gvSumary.RowCellStyle += gvSumary_RowCellStyle;

            gvSumary.LeftCoordChanged += gvSumary_LeftCoordChanged;
            gvSumary.MouseMove += gvSumary_MouseMove;
            gvSumary.TopRowChanged += gvSumary_TopRowChanged;
            gvSumary.FocusedColumnChanged += gvSumary_FocusedColumnChanged;
            gvSumary.FocusedRowChanged += gvSumary_FocusedRowChanged;
            gvSumary.RowCountChanged += gvSumary_RowCountChanged;
            gvSumary.CustomDrawRowIndicator += gvSumary_CustomDrawRowIndicator;

            gcSumary.Paint += gcSumary_Paint;
            gcSumary.KeyDown += gcSumary_KeyDown;
            gcSumary.EditorKeyDown += gcSumary_EditorKeyDown;
            gvSumary.ShowingEditor += gvSumary_ShowingEditor;
            gvSumary.CustomColumnDisplayText += gvSumary_CustomColumnDisplayText;
            gvSumary.DoubleClick += gvSumary_DoubleClick;


          

            gcSumary.ForceInitialize();



        }


        private void gvSumary_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as BandedGridView;
            if (gv == null) return;
            GridControl gc = gv.GridControl;
            BandedGridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
            if (!hi.InRowCell) return;
            BandedGridColumn gCol = hi.Column;
            if (gCol == null) return;
            _isSummaryClick = true;
            ShowDetalGroup(hi.RowHandle, gCol.FieldName, false);
        }

     
        private void gcSumary_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDown)
            {
                gcSumary_KeyDown(sender, e);
            }
            _checkKeyDown = false;
        }


        private void gcSumary_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = (GridControl)sender;
            if (gc == null) return;
            var gv = (GridView)gc.FocusedView;
            if (gv == null) return;
            GridColumn gColF = gv.FocusedColumn;
            string fieldName = gColF.FieldName;

            int rH = gv.FocusedRowHandle;
            _checkKeyDown = true;


            #region "Process Enter key"

            if (e.KeyData == Keys.Enter)
            {
                _isSummaryClick = true;
                ShowDetalGroup(rH, fieldName, false);
                return;
            }

            #endregion







        }



        #region "Key Down"



        #endregion


        private void gvSumary_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;

            if (fieldName == "ColorType" || fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_") )
            {
                e.DisplayText = "";
            }
        }
        private void gvSumary_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void gvSumary_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            int visibleIndex = gCol.VisibleIndex;
            if (visibleIndex < 0) return;
            string fieldName = gCol.FieldName;

            switch (fieldName)
            {
                case "ColorDesc":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                    break;

            }

            if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(rH, gCol))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }


            if (fieldName == "ColorType" || fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
            {
                string displayText = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fieldName));
                ColorDemandPrInfoRpt infoColor;
                if (displayText != "" && _dicColor.TryGetValue(displayText, out infoColor))
                {
                    //    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = infoColor.ColorSet;
                    //  e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                }

            }
          
            else 
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Cornsilk;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }

        }

        private void gvSumary_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvSumary_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvSumary_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvSumary_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvSumary_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcSumary_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvSumary_RowCountChanged(object sender, EventArgs e)
        {
            //var gvP = sender as GridView;
            //if (gvP == null) return;
            ////  if (!gv.GridControl.IsHandleCreated) return;
            //Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            //SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            //gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvSumary_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //GetStatusPriorityPaintOnRow(GridView gv, int rH)
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
            bool isSelected = gv.IsRowSelected(e.RowHandle);
            LinearGradientBrush backBrush;
            if (isSelected)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

            if (isSelected)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());



        }


        #endregion





























        #region "Show Tooltip"



        private void toolTipControllerMain_GetActiveObjectInfo(object sender,
            ToolTipControllerGetActiveObjectInfoEventArgs e)
        {


            GridControl gc = null;

            if (e.SelectedControl == gcPaint)
            {
                gc = gcPaint;
            }
            else if (e.SelectedControl == gcSumary)
            {
                gc = gcSumary;
            }

            if (gc != null)
            {
                GridView gv = (GridView) gc.FocusedView;
                if (gv == null) return;
                GridHitInfo hi = gv.CalcHitInfo(e.ControlMousePosition);
                if (!hi.InRowCell) return;
                GridColumn gCol = hi.Column;
                if (gCol == null) return;
                string fieldName = gCol.FieldName;
                if (!fieldName.StartsWith("FinishPR_") && !fieldName.StartsWith("FinishPO_")) return;
                int rH = hi.RowHandle;
                if (!gv.IsDataRow(rH)) return;
                string displayText = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fieldName));
                ColorDemandPrInfoRpt infoColor;
                if (!_dicColor.TryGetValue(displayText, out infoColor)) return;
                string text = infoColor.DescriptionSet;
                if (string.IsNullOrEmpty(text)) return;





            

                SuperToolTip sTooltip = new SuperToolTip();
                // Create an object to initialize the SuperToolTip.
                SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();
                args.Title.Text = @"<size=12> <color=blue><b>Info</b></color></size>";
                if (text.Contains("Missing PR") && fieldName.StartsWith("FinishPO_"))
                {
                    text = text.Replace("Missing PR", "Missing PO");
                }
                args.Contents.Text = text;
                sTooltip.Setup(args);
                // Enable HTML formatting.
                sTooltip.AllowHtmlText = DefaultBoolean.True;
              
                e.Info = new ToolTipControlInfo();
                //<bold>Updated by John (DevExpress Support):</bold>
                //e.Info.Object = hitInfo.Column;
                e.Info.Object = hi.HitTest.ToString() + hi.RowHandle.ToString(); //NewLine
                //<bold>End Update</bold>
                e.Info.ToolTipType = ToolTipType.SuperTip;
                e.Info.SuperTip = sTooltip;
               // e.Info.SuperTip.Setup(toolTipArgs);

            }/*
            if (e.SelectedControl == gcPaint)
            {
              
                var gv = gcPaint.FocusedView as GridView;
                if (gv == null) return;
                if (gv.RowCount <= 0) return;


                GridHitInfo hiG = gv.CalcHitInfo(gcPaint.PointToClient(MousePosition));
                if (!hiG.InRowCell) return;
                GridColumn colG = hiG.Column;
                if (colG == null) return;
                if (!colG.FieldName.StartsWith("AreaPaint")) return;

                int rH = hiG.RowHandle;

                string sFormular = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "FormulaStringPaint"));
                if (string.IsNullOrEmpty(sFormular)) return;

                object oG = hiG.HitTest.ToString() + rH.ToString(); //NewLine
                ToolTipControlInfo infoG = new ToolTipControlInfo(oG, sFormular, ToolTipIconType.Information) { AllowHtmlText = DefaultBoolean.True };
                e.Info = infoG;


          

            }


            */










        }

        #endregion


        #region "Show Detail Group"

        private void ShowDetalGroup(int rH, string fielName, bool buttonClick )
        {

            GridView gv = _isSummaryClick ? gvSumary : gvPaint;

            if (!gv.IsDataRow(rH)) return;
            if (!fielName.StartsWith("FinishPR_") && !fielName.StartsWith("FinishPO_")) return;
            string text = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fielName));
            if (string.IsNullOrEmpty(text)) return;


            List<Int64> lCny00020Pk = new List<Int64>();
            if (!_isSummaryClick)
            {
                Int64 cny00020Pk = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(rH, "CNY00020PK"));
                if (cny00020Pk > 0)
                {
                    lCny00020Pk.Add(cny00020Pk);
                }

                txtCode.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "ProductCode"));
                txtRef.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "Reference"));
                txtLine.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "OrderLine"));
                spinOrder.EditValue = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(rH, "SOQty"));
            }
            else
            {
                txtCode.EditValue = "";
                txtRef.EditValue = "";
                txtLine.EditValue = "";
                spinOrder.EditValue = null;


                for (int i = 0; i < gvPaint.RowCount; i++)
                {
                    Int64 cny00020Pk = ProcessGeneral.GetSafeInt64(gvPaint.GetRowCellValue(i, "CNY00020PK"));
                    if (cny00020Pk > 0)
                    {
                        lCny00020Pk.Add(cny00020Pk);
                    }
                }
            }

           
            
           
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00012PK", typeof(Int64));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("PlanQty", typeof(Int32));
            dt.Columns.Add("SOQty", typeof(Int32));
            var q1 = tlMain.GetAllCheckedNodes().Select(p => new
            {
                StrCNY00012PK = ProcessGeneral.GetSafeString(p.GetValue("BOMID")),
                CNY00020PK = ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK")),
                PlanQty = ProcessGeneral.GetSafeInt(p.GetValue("PlanQuantity")),
                SOQty = ProcessGeneral.GetSafeInt(p.GetValue("OrderQuantity")),
            }).Where(p => p.SOQty > 0 && p.StrCNY00012PK != "" && lCny00020Pk.Contains(p.CNY00020PK)).Distinct().ToList();
            if (!q1.Any()) return;


            int minPlanQtyFinal = 0;
            foreach (var item in q1)
            {
                string strCny00012Pk = item.StrCNY00012PK;
                Int64[] arrBomId = strCny00012Pk.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => ProcessGeneral.GetSafeInt64(p.Trim())).Where(p => p > 0).Distinct().ToArray();
                Int32 planQtyFinal = buttonClick  ? Convert.ToInt32(spinPlan.Value) : item.SOQty;


                if (minPlanQtyFinal > planQtyFinal)
                {
                    minPlanQtyFinal = planQtyFinal;
                }
                else if (minPlanQtyFinal == 0)
                {
                    minPlanQtyFinal = planQtyFinal;
                }

                foreach (Int64 cny00012Pk in arrBomId)
                {
                    dt.Rows.Add(cny00012Pk, item.CNY00020PK, planQtyFinal, item.SOQty);
                }
              
              
           
            }


            if (!buttonClick)
            {
                spinPlan.EditValue = minPlanQtyFinal;
            }

            string mainGroup = ProcessGeneral.GetDescriptionInString(fielName, "_");
            WaitDialogForm dlg = new WaitDialogForm();
            DataTable dtFinal = _inf.Report_LoadBoMPrPO_F1(dt, mainGroup, _cny00012Pk, _isCust);

            dlg.Close();
            if (dtFinal.Rows.Count <= 0) return;


            var qParent = dtFinal.AsEnumerable().GroupBy(p => new
            {
                RMCode_001 = p.Field<string>("RMCode_001"),
                RMDescription_002 = p.Field<string>("RMDescription_002")
            }).Select(t => new
            {


                ChildPK = t.Key.RMCode_001,
              //  t.Key.RMDescription_002,
                Dimenson = t.Key.RMCode_001,
                CalDemand = t.Sum(s => s.Field<decimal>("CalDemand")),
                CalUnit = t.Select(s => s.Field<string>("CalUnit")).First(),
                BoMDemand = t.Sum(s => s.Field<decimal>("BoMDemand")),
                BOMUnit = t.Select(s => s.Field<string>("BOMUnit")).First(),
                PRQty = t.Sum(s => s.Field<decimal>("PRQty")),
                PRUnit = t.Select(s => s.Field<string>("PRUnit")).First(),
                POQty = t.Sum(s => s.Field<decimal>("POQty")),
                POUnit = t.Select(s => s.Field<string>("POUnit")).First(),
                CalShow = t.Select(s => s.Field<string>("CalShow")).OrderByDescending(s => s).First(),
                BOMShow = t.Select(s => s.Field<string>("BOMShow")).OrderByDescending(s => s).First(),
                PRShow = t.Select(s => s.Field<string>("PRShow")).OrderByDescending(s => s).First(),
                POShow = t.Select(s => s.Field<string>("POShow")).OrderByDescending(s => s).First(),
                ParentPK = ""



            }).OrderBy(p => p.Dimenson).ToList();



            var qChild = dtFinal.AsEnumerable().Select((p, idx) => new
            {

                ChildPK = idx.ToString(),
                //t.Key.RMCode_001,
                //t.Key.RMDescription_002,
                Dimenson = p.Field<string>("Dimenson"),
                CalDemand = p.Field<decimal>("CalDemand"),
                CalUnit = p.Field<string>("CalUnit"),
                BoMDemand = p.Field<decimal>("BoMDemand"),
                BOMUnit = p.Field<string>("BOMUnit"),
                PRQty = p.Field<decimal>("PRQty"),
                PRUnit = p.Field<string>("PRUnit"),
                POQty = p.Field<decimal>("POQty"),
                POUnit = p.Field<string>("POUnit"),
                CalShow = p.Field<string>("CalShow"),
                BOMShow = p.Field<string>("BOMShow"),
                PRShow = p.Field<string>("PRShow"),
                POShow = p.Field<string>("POShow"),
                ParentPK = p.Field<string>("RMCode_001"),

            }).OrderBy(p => p.Dimenson).ToList();



            DataTable dtFi = new DataTable();
           
            dtFi.Columns.Add("RMDescription_002", typeof(string));
            dtFi.Columns.Add("Dimenson", typeof(string));
            dtFi.Columns.Add("CalDemand", typeof(decimal));
            dtFi.Columns.Add("CalUnit", typeof(string));
            dtFi.Columns.Add("BoMDemand", typeof(decimal));
            dtFi.Columns.Add("BOMUnit", typeof(string));
            dtFi.Columns.Add("PRQty", typeof(decimal));
            dtFi.Columns.Add("PRUnit", typeof(string));
            dtFi.Columns.Add("POQty", typeof(decimal));
            dtFi.Columns.Add("POUnit", typeof(string));
            dtFi.Columns.Add("CalShow", typeof(string));
            dtFi.Columns.Add("BOMShow", typeof(string));
            dtFi.Columns.Add("PRShow", typeof(string));
            dtFi.Columns.Add("POShow", typeof(string));
            dtFi.Columns.Add("ChildPK", typeof(string));
            dtFi.Columns.Add("ParentPK", typeof(string));

            foreach (var itemP in qParent)
            {
                dtFi.Rows.Add( "", itemP.Dimenson, itemP.CalDemand, itemP.CalUnit, itemP.BoMDemand, itemP.BOMUnit, itemP.PRQty, itemP.PRUnit, itemP.POQty, itemP.POUnit, itemP.CalShow, itemP.BOMShow,
                    itemP.PRShow, itemP.POShow, itemP.ChildPK, itemP.ParentPK);
            }

            foreach (var itemC in qChild)
            {
                dtFi.Rows.Add( "", itemC.Dimenson, itemC.CalDemand, itemC.CalUnit, itemC.BoMDemand, itemC.BOMUnit, itemC.PRQty, itemC.PRUnit, itemC.POQty, itemC.POUnit, itemC.CalShow, itemC.BOMShow,
                    itemC.PRShow, itemC.POShow, itemC.ChildPK, itemC.ParentPK);
            }
            dtFi.AcceptChanges();







            // var q5 = dtFinal.AsEnumerable()
            ShowGridMain(false);
            gvDetail.BeginUpdate();
            gvDetail.ClearGrouping();
            gvDetail.Bands.Clear();
            gvDetail.Columns.Clear();
            gcDetail.DataSource = null;

            CreateBandedGridHeader(dtFinal);
            gcDetail.DataSource = dtFinal;

    



            gvDetail.BeginSort();
            gvDetail.ClearSorting(); // <<< NEW LINE
            gvDetail.Columns["RMCode_001"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            gvDetail.Columns["RMCode_001"].GroupIndex = 0;
            gvDetail.EndSort();

            BestFitBandsGridDetail(gvDetail);

            gvDetail.EndUpdate();

            LoadDataTreeViewDetail(tlDetail, dtFi);

        }



        #region "Proccess Treeview"


    

        private void LoadDataTreeViewDetail(TreeList tl, DataTable dt)
        {

       
            bool isShowFind = false;
            string findPanelText = "";
            if (tl.FindPanelVisible)
            {
                isShowFind = true;
                findPanelText = tl.FindFilterText;
                tl.HideFindPanel();
            }






            tl.BeginUpdate();

            tl.Bands.Clear();
            tl.Columns.Clear();
            tl.DataSource = null;
            CreateBandedTreeHeader(tl, dt);
            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";
            tl.ExpandAll();
            tl.ForceInitialize();
            tl.BeginSort();
            tl.Columns["Dimenson"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();






            tl.BeginUpdate();
            tl.OptionsView.ShowColumns = true;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.False;
            tl.BestFitColumns();
            tl.OptionsView.ShowBandsMode = DefaultBoolean.True;
            tl.OptionsView.ShowColumns = false;
            tl.CollapseAll();
            tl.EndUpdate();











            if (isShowFind)
            {
                tl.ShowFindPanelTreeList(findPanelText);
            }


        }


        private void CreateBandedTreeHeader(TreeList tl, DataTable dtS)
        {






       
            
          




            string[] arrColVisible =
            {
               "RMDescription_002", "Dimenson", "CalDemand", "CalUnit", "BoMDemand", "BOMUnit", "PRQty", "PRUnit", "POQty", "POUnit","CalShow","BOMShow","PRShow","POShow"
            };

       

          
            TreeListBand[] arrBand = new TreeListBand[arrColVisible.Length];


            //     "F1^^^^^F1", "F2^^^^^F2","E1^^^^^E1", "E2^^^^^E2", "H1^^^^^H1",
            //   "H2^^^^^H2", "17%%%%%Dày", "19%%%%%Rộng", "18%%%%%Dài", "26%%%%%Cao", "22%%%%%ĐK",


            //   List<string> lFieldName 


            int i = 0;
            foreach (string colName in arrColVisible)
            {

                TreeListColumn gCol = new TreeListColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new TreeListBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                HorzAlignment hozAlign = HorzAlignment.Center;
            
                string displayText = "";
                if (colName == "RMDescription_002")
                {
                    displayText = "STT";
                    hozAlign = HorzAlignment.Center;
                }
                else if (colName == "Dimenson")
                {
                    displayText = "Description";
                    hozAlign = HorzAlignment.Near;
                    //  arrBand[i].RowCount = 3;
                }
                else if (colName == "CalUnit" || colName == "BOMUnit" || colName == "PRUnit" || colName == "POUnit")
                {
                    displayText = "Unit";
                }
                else if (colName == "CalShow" || colName == "BOMShow" || colName == "PRShow" || colName == "POShow")
                {
                    displayText = colName;
                }
                else
                {
                    displayText = "Quantity";
                    gCol.Format.FormatType = FormatType.Numeric;
                    switch (colName)
                    {
                        case "CalDemand":
                        case "BoMDemand":
                        case "PRQty":
                            gCol.Format.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
                            break;
                        default:
                            gCol.Format.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
                            break;
                    }


                }



                //gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                //gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
                //arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;




                gCol.AppearanceCell.TextOptions.HAlignment = hozAlign;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;

                gCol.Caption = displayText;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;




                tl.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].Width = 100;
                i++;



         
             
            }


            tl.Bands.AddRange(new[] { arrBand[0], arrBand[1] });

            Dictionary<string, int[]> dicCg = new Dictionary<string, int[]>
            {
                {"CALCULATE", new[] {2, 3}},
                {"DEMAND", new[] {4, 5}},
                {"PR", new[] {6, 7}},
                {"PO", new[] {8, 9}}
            };
            int t = 2;
            foreach (var itemTemp in dicCg)
            {

                int[] q2 = itemTemp.Value;
                string sKey = itemTemp.Key;
                TreeListBand gParent = new TreeListBand();
                gParent.AppearanceHeader.Options.UseTextOptions = true;
                gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                gParent.AppearanceHeader.Options.UseFont = true;
                //     gParent.RowCount = 2;
                gParent.Caption = sKey;
                gParent.Width = 100;
                // gParent.RowCount = 2;
                // var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Tag) == sKey).ToArray();
                for (int h = 0; h < q2.Length; h++)
                {
                    TreeListBand childBand = arrBand[q2[h]];
                    gParent.Bands.Add(childBand);
                }
                tl.Bands.Add(gParent);
                t++;
            }
            tl.Bands.AddRange(new[] { arrBand[10], arrBand[11], arrBand[12], arrBand[13] });




            arrBand[10].Visible = false;

            arrBand[11].Visible = false;
            arrBand[12].Visible = false;
            arrBand[13].Visible = false;


        }


        private ImageList GetImageListDisplayTreeDetail()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.Product_BoM_16x16);
            imgLt.Images.Add(Resources.Assembly_BoM_16x16);
            return imgLt;
        }

    

      

        private void InitTreeListDetail(TreeList treeList)
        {


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

            treeList.OptionsBehavior.Editable = true;
           // treeList.OptionsView.ShowColumns = true;
            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;
            treeList.OptionsView.AutoWidth = false;
            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;
            treeList.StateImageList = GetImageListDisplayTreeDetail();
            treeList.OptionsBehavior.AutoChangeParent = false;
            treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            treeList.OptionsBehavior.AutoNodeHeight = true;

            treeList.OptionsView.ShowSummaryFooter = false;



            treeList.OptionsView.AllowHtmlDrawHeaders = true;


            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;



            treeList.OptionsCustomization.AllowColumnResizing = true;
            treeList.OptionsCustomization.AllowColumnMoving = false;
            treeList.OptionsCustomization.AllowQuickHideColumns = true;
            treeList.OptionsCustomization.AllowSort = true;
            treeList.OptionsCustomization.AllowFilter = true;


            treeList.OptionsView.ShowBandsMode = DefaultBoolean.True;

            treeList.OptionsView.AllowHtmlDrawHeaders = true;

            treeList.OptionsView.AllowBandColumnsMultiRow = false;

            treeList.OptionsView.ShowColumns = false;


            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,
                AllowColumnsChooser = true,

            };
         





            treeList.GetStateImage += tlDetail_GetStateImage;
            treeList.ShowingEditor += tlDetail_ShowingEditor;



            treeList.CustomDrawNodeIndicator += tlDetail_CustomDrawNodeIndicator;





            treeList.NodeCellStyle += tlDetail_NodeCellStyle;
        

            treeList.KeyDown += tlDetail_KeyDown;
            treeList.EditorKeyDown += tlDetail_EditorKeyDown;


            treeList.GetNodeDisplayValue += tlDetail_GetNodeDisplayValue;











        }



        private void tlDetail_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            if (!col.Visible) return;

            
            switch (fieldName)
            {
                case "RMDescription_002":
                {
                    string value = "";
                    TreeListNode parentNode = node.ParentNode;
                    if (node.ParentNode != null)
                    {
                        string sttParent = parentNode.GetDisplayText(fieldName);
                        value = string.Format("{0}.{1}", sttParent, parentNode.Nodes.IndexOf(node) + 1);
                    }
                    else
                    {
                        value = (tl.Nodes.IndexOf(node) + 1).ToString().Trim();
                    }
                    e.Value = value;

                }
                    break;
                case "CalDemand":
                case "CalUnit":
                    if (ProcessGeneral.GetSafeString(node.GetValue("CalShow")) == "0")
                    {
                        e.Value = "";
                    }
                    break;
                case "BoMDemand":
                case "BOMUnit":
                    if (ProcessGeneral.GetSafeString(node.GetValue("BOMShow")) == "0")
                    {
                        e.Value = "";
                    }
                    break;
                case "PRQty":
                case "PRUnit":
                    if (ProcessGeneral.GetSafeString(node.GetValue("PRShow")) == "0")
                    {
                        e.Value = "";
                    }
                    break;
                case "POQty":
                case "POUnit":
                    if (ProcessGeneral.GetSafeString(node.GetValue("POShow")) == "0")
                    {
                        e.Value = "";
                    }
                    break;
            }


        }






    
      


        #region "Process Key Down"

        private void tlDetail_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDown)
            {
                tlDetail_KeyDown(sender, e);
            }
            _checkKeyDown = false;
        }

        private void tlDetail_KeyDown(object sender, KeyEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;
            _checkKeyDown = true;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            int visibleIndex = col.VisibleIndex;

            #region "Process F9 Key"

            if (e.KeyCode == Keys.F9)
            {
                tl.BeginUpdate();
                tl.ExpandCollapseNodeSelected(true);
                tl.EndUpdate();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            #endregion

            #region "Process F10 Key"

            if (e.KeyCode == Keys.F10)
            {
                tl.BeginUpdate();
                tl.ExpandCollapseNodeSelected(false);
                tl.EndUpdate();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            #endregion


            #region "Process F11 Key"

            if (e.KeyCode == Keys.F11)
            {
                tl.ExpandAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            #endregion


            #region "Process F12 Key"

            if (e.KeyCode == Keys.F12)
            {
                tl.CollapseAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            #endregion







         


       


        


            #region "Key Enter"

            if (e.KeyCode == Keys.Enter)
            {
                tl.SelectedNextCell(node, visibleIndex);
                return;
            }
            #endregion

        }



     







     
     

   



   
        #endregion







        private void tlDetail_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;



            bool selected = tl.Selection.Contains(e.Node);
            LinearGradientBrush backBrush;
            if (selected)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }



            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            if (selected)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = e.Node.HasChildren ? Color.DarkMagenta : Color.DarkOrchid;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }


            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            string value = (tl.GetVisibleIndexByNode(e.Node) + 1).ToString().Trim();
            e.Graphics.DrawString(value, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache),
                e.Bounds, e.Appearance.GetStringFormat());
            e.ImageIndex = -1;
            e.Handled = true;

        }

        //  private int updateRmSourceNo = 1;
        private void tlDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;




        }



   


        private void tlDetail_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
          
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;

         
       
      


            switch (fieldName)
            {
                case "CalDemand":
                case "CalUnit":
                    if (ProcessGeneral.GetSafeDecimal(node.GetValue("CalDemand")) !=
                        ProcessGeneral.GetSafeDecimal(node.GetValue("BoMDemand")))
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, ((node.HasChildren?FontStyle.Bold: FontStyle.Regular) | FontStyle.Underline), GraphicsUnit.Point, 0);
                    }
                    else if (node.HasChildren)
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    e.Appearance.ForeColor = Color.DarkOrange;
                    break;
                case "BoMDemand":
                case "BOMUnit":
                    if (node.HasChildren)
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    e.Appearance.ForeColor = Color.DarkRed;
                    break;
                case "PRQty":
                case "PRUnit":
                    if (ProcessGeneral.GetSafeDecimal(node.GetValue("PRQty")) !=
                        ProcessGeneral.GetSafeDecimal(node.GetValue("BoMDemand")))
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, ((node.HasChildren ? FontStyle.Bold : FontStyle.Regular) | FontStyle.Underline), GraphicsUnit.Point, 0);
                    }
                    else if (node.HasChildren)
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    e.Appearance.ForeColor = Color.DarkGreen;
                    break;
                case "POQty":
                case "POUnit":
                    if (ProcessGeneral.GetSafeDecimal(node.GetValue("PRQty")) !=
                        ProcessGeneral.GetSafeDecimal(node.GetValue("POQty")))
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, ((node.HasChildren ? FontStyle.Bold : FontStyle.Regular) | FontStyle.Underline), GraphicsUnit.Point, 0);
                    }
                    else if (node.HasChildren)
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    e.Appearance.ForeColor = Color.DarkBlue;
                    break;
                default:
                    if (node.HasChildren)
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    break;
            }




            

            if (fieldName == "RMDescription_002")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Cornsilk;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "Dimenson")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.WhiteSmoke; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "CalDemand" || fieldName == "CalUnit")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.LightYellow; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "BoMDemand" || fieldName == "BOMUnit")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.LavenderBlush; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "PRQty" || fieldName == "PRUnit")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Honeydew; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Beige; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }


        }
      


        private void tlDetail_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;

            e.NodeImageIndex = node.HasChildren ? 0 : 1;



          
        }
     




        #endregion





        private void CreateBandedGridHeader(DataTable dtS)
        {

            GridBand[] arrBand = new GridBand[dtS.Columns.Count];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {
                BandedGridColumn gCol = new BandedGridColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new GridBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                HorzAlignment hozAlign = HorzAlignment.Center;
                string colName = col.ColumnName;
                string displayText = "";
                if (colName == "RMCode_001")
                {
                    displayText = "Code";
                    hozAlign = HorzAlignment.Near;
                }
                else if (colName == "RMDescription_002")
                {
                    displayText = "Name";
                    hozAlign = HorzAlignment.Near;
                }
                else if (colName == "Dimenson")
                {
                    displayText = "Description";
                    hozAlign = HorzAlignment.Near;
                    //  arrBand[i].RowCount = 3;
                }
                else if (colName == "CalUnit" || colName == "BOMUnit" || colName == "PRUnit" || colName == "POUnit")
                {
                    displayText = "Unit";
                }
                else if (colName == "CalShow" || colName == "BOMShow" || colName == "PRShow" || colName == "POShow")
                {
                    displayText = colName;
                }
                else 
                {
                    displayText = "Quantity";
                    gCol.DisplayFormat.FormatType = FormatType.Numeric;
                    switch (colName)
                    {
                        case "CalDemand":
                        case "BoMDemand":
                        case "PRQty":
                            gCol.DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
                            break;
                        default:
                            gCol.DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
                            break;
                    }
             
                 
                }



                //gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                //gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
                //arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;




                gCol.AppearanceCell.TextOptions.HAlignment = hozAlign;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlign;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
              
                gCol.Caption = displayText;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;




                gvDetail.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].VisibleIndex = 0;
                arrBand[i].Width = 100;
                i++;
            }
            gvDetail.Bands.AddRange(new[] { arrBand[0], arrBand[1], arrBand[2] });

            Dictionary<string, int[]> dicCg = new Dictionary<string, int[]>
            {
                {"CALCULATE", new[] {3, 4}},
                {"DEMAND", new[] {5, 6}},
                {"PR", new[] {7, 8}},
                {"PO", new[] {9, 10}}
            };
            int t = 3;
            foreach (var itemTemp in dicCg)
            {

                int[] q2 = itemTemp.Value;
                string sKey = itemTemp.Key;
                GridBand gParent = new GridBand();
                gParent.AppearanceHeader.Options.UseTextOptions = true;
                gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                gParent.AppearanceHeader.Options.UseFont = true;
           //     gParent.RowCount = 2;
                gParent.Caption = sKey;
                gParent.VisibleIndex = t;
                gParent.Width = 100;
                // gParent.RowCount = 2;
               // var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Tag) == sKey).ToArray();
                for (int h = 0; h < q2.Length; h++)
                {
                    GridBand childBand = arrBand[q2[h]];
                    childBand.VisibleIndex = h;
                    gParent.Children.Add(childBand);
                }
                gvDetail.Bands.Add(gParent);
                t++;
            }
            gvDetail.Bands.AddRange(new[] { arrBand[11], arrBand[12], arrBand[13], arrBand[14] });

        }
        private void BestFitBandsGridDetail(BandedGridView view)
        {

            view.BeginUpdate();
            view.OptionsView.ShowColumnHeaders = true;


            foreach (BandedGridColumn col in view.Columns)
            {
                GridBand gBand = col.OwnerBand;
                string fieldName = col.FieldName;
                col.Caption = gBand.Caption;
                if (fieldName == "CalShow" || fieldName == "BOMShow" || fieldName == "PRShow" || fieldName == "POShow" || fieldName == "RMCode_001" || fieldName == "RMDescription_002")
                {
                    gBand.Visible = false;
                }

                //if (fieldName== "RMCode_001" || fieldName == "RMDescription_002" ||fieldName == "Dimenson")
                //{
                //    col.Width = col.GetBestWidth() + 10;
                //}
                //else
                //{
                //    col.Width = col.GetBestWidth();
                //}
                col.Width = col.GetBestWidth() + 10;

            }

            // view.BestFitColumns();
            //   view.Columns["MenuCode"].Width += 20;
            view.OptionsView.ShowColumnHeaders = false;







            view.EndUpdate();



            //  view.Bands[1].Visible = false;


        }




        private void SetUpMainGridDetail()
        {


            gcDetail.UseEmbeddedNavigator = true;

            gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvDetail.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

            gvDetail.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvDetail.OptionsBehavior.Editable = true;
            gvDetail.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvDetail.OptionsCustomization.AllowColumnMoving = false;
            gvDetail.OptionsCustomization.AllowQuickHideColumns = true;
            gvDetail.OptionsCustomization.AllowSort = false;
            gvDetail.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvDetail.OptionsView.ColumnAutoWidth = false;
            gvDetail.OptionsView.ShowGroupPanel = false;
            gvDetail.OptionsView.ShowIndicator = true;
            gvDetail.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvDetail.OptionsView.ShowAutoFilterRow = false;
            gvDetail.OptionsView.AllowCellMerge = false;
            gvDetail.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;



            gvDetail.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Wrap;
            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvDetail.OptionsNavigation.AutoFocusNewRow = true;
            gvDetail.OptionsNavigation.UseTabKey = true;

            gvDetail.OptionsSelection.MultiSelect = true;
            gvDetail.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvDetail.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvDetail.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvDetail.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvDetail.OptionsView.EnableAppearanceEvenRow = false;
            gvDetail.OptionsView.EnableAppearanceOddRow = false;

            gvDetail.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gvDetail.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvDetail.OptionsFind.AlwaysVisible = false;
            gvDetail.OptionsFind.ShowCloseButton = true;
            gvDetail.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvDetail)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvDetail.OptionsMenu.EnableFooterMenu = false;
            gvDetail.OptionsMenu.EnableColumnMenu = false;




            gvDetail.RowCellStyle += gvDetail_RowCellStyle;

            gvDetail.LeftCoordChanged += gvDetail_LeftCoordChanged;
            gvDetail.MouseMove += gvDetail_MouseMove;
            gvDetail.TopRowChanged += gvDetail_TopRowChanged;
            gvDetail.FocusedColumnChanged += gvDetail_FocusedColumnChanged;
            gvDetail.FocusedRowChanged += gvDetail_FocusedRowChanged;
            gvDetail.RowCountChanged += gvDetail_RowCountChanged;
            gvDetail.CustomDrawRowIndicator += gvDetail_CustomDrawRowIndicator;

            gcDetail.Paint += gcDetail_Paint;

            gvDetail.ShowingEditor += gvDetail_ShowingEditor;

            gvDetail.CustomColumnDisplayText += GvDetail_CustomColumnDisplayText;

            gvDetail.CustomDrawGroupRow += gvDetail_CustomDrawGroupRow;
            gvDetail.GroupLevelStyle += gvDetail_GroupLevelStyle;
            gcDetail.ForceInitialize();



        }
        private void gvDetail_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {

            GridView gv = sender as GridView;
            if (gv == null) return;
            GridGroupRowInfo groupRowInfo = e.Info as GridGroupRowInfo;
            if (groupRowInfo != null)
            {
                Rectangle groupRowBounds = groupRowInfo.DataBounds;
                Rectangle expandButtonBounds = groupRowInfo.ButtonBounds;
                Rectangle textBounds = e.Bounds;
                textBounds.X = expandButtonBounds.Right + 4;


                Brush brush = e.Cache.GetGradientBrush(groupRowBounds, Color.LemonChiffon, Color.Tan, LinearGradientMode.Horizontal);

                Brush brushText = Brushes.Black, brushTextShadow = Brushes.White;
                if (e.RowHandle == gv.FocusedRowHandle)
                {
                    brush = brushTextShadow = Brushes.DarkBlue;
                    brushText = Brushes.White;
                }


                e.Graphics.FillRectangle(brush, groupRowBounds);

                Image img = gv.GetRowExpanded(e.RowHandle)
                    ? Properties.Resources._1450289438_bullet_toggle_minus
                    : Properties.Resources._1450289456_bullet_toggle_plus;
                e.Graphics.DrawImageUnscaled(img,
                    expandButtonBounds);

                string s = gv.GetGroupRowDisplayText(e.RowHandle);
                e.Appearance.DrawString(e.Cache, s, new Rectangle(textBounds.X + 1, textBounds.Y + 1,
                    textBounds.Width, textBounds.Height), brushTextShadow);
                e.Appearance.DrawString(e.Cache, s, textBounds, brushText);
            }

            e.Handled = true;
        }

        private void gvDetail_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            e.LevelAppearance.BackColor = Color.LemonChiffon;
        }
        private void GvDetail_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            int rH = e.ListSourceRowIndex;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;

            switch (fieldName)
            {
                //case "RMCode_001":
                //case "RMDescription_002":
                //case "Dimenson":
                //    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                //    break;
                case "CalDemand":
                case "CalUnit":
                    if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "CalShow")) == "0")
                    {
                        e.DisplayText = "";
                    }
                    break;
                case "BoMDemand":
                case "BOMUnit":
                    if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "BOMShow")) == "0")
                    {
                        e.DisplayText = "";
                    }
                    break;
                case "PRQty":
                case "PRUnit":
                    if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "PRShow")) == "0")
                    {
                        e.DisplayText = "";
                    }
                    break;
                case "POQty":
                case "POUnit":
                    if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "POShow")) == "0")
                    {
                        e.DisplayText = "";
                    }
                    break;
            }
        }
        private void gvDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            int visibleIndex = gCol.VisibleIndex;
            if (visibleIndex < 0) return;
            string fieldName = gCol.FieldName;

            switch (fieldName)
            {
                //case "RMCode_001":
                //case "RMDescription_002":
                //case "Dimenson":
                //    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                //    break;
                case "CalDemand":
                case "CalUnit":
                    if (ProcessGeneral.GetSafeDecimal(gv.GetRowCellValue(rH, "CalDemand")) !=
                        ProcessGeneral.GetSafeDecimal(gv.GetRowCellValue(rH, "BoMDemand")))
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point, 0);
                    }
                    else
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    e.Appearance.ForeColor = Color.DarkOrange;
                    break;
                case "BoMDemand":
                case "BOMUnit":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkRed;
                    break;
                case "PRQty":
                case "PRUnit":
                    if (ProcessGeneral.GetSafeDecimal(gv.GetRowCellValue(rH, "PRQty")) !=
                        ProcessGeneral.GetSafeDecimal(gv.GetRowCellValue(rH, "BoMDemand")))
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point, 0);
                    }
                    else
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    e.Appearance.ForeColor = Color.DarkGreen;
                    break;
                case "POQty":
                case "POUnit":
                    if (ProcessGeneral.GetSafeDecimal(gv.GetRowCellValue(rH, "PRQty")) !=
                        ProcessGeneral.GetSafeDecimal(gv.GetRowCellValue(rH, "POQty")))
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point, 0);
                    }
                    else
                    {
                        e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    }
                    e.Appearance.ForeColor = Color.DarkBlue;
                    break;
            }

            if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(rH, gCol))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }


            if (fieldName == "RMCode_001" || fieldName == "RMDescription_002")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Cornsilk;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "Dimenson")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.WhiteSmoke; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "CalDemand" || fieldName == "CalUnit")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.LightYellow; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "BoMDemand" || fieldName == "BOMUnit")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.LavenderBlush; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "PRQty" || fieldName == "PRUnit")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Honeydew; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Beige; ;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
        }
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                #region "System"
                case Keys.Escape:
                {
                    if (xtraTabCenter.SelectedTabPage == xtraTabDetail)
                    {
                        ShowGridMain(true);
                        }
                    return true;
                }
               
                #endregion



            }
            return base.ProcessCmdKey(ref message, keys);



        }

        #region "Key Down"



        #endregion


      
        private void gvDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
       

        private void gvDetail_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvDetail_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcDetail_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvDetail_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //GetStatusPriorityPaintOnRow(GridView gv, int rH)
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
            bool isSelected = gv.IsRowSelected(e.RowHandle);
            LinearGradientBrush backBrush;
            if (isSelected)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

            if (isSelected)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());



        }

        #endregion
    }

}