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
using DevExpress.XtraSpreadsheet;

namespace CNY_BaseSys.WForm
{
    //2
    public partial class FrmFiAmountProductionOrder : FrmBase
    {
        #region "Property"

        
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

        private const string DefaultUcUnit = "M3";


        #endregion


        #region "Contructor"


        public FrmFiAmountProductionOrder(DataSet dsProduction, Int64 cny00019Pk)
        {

            InitializeComponent();
          
            _repositorySpinN0 = new RepositoryItemSpinEdit
            {AutoHeight = false,
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
            this.Load += FrmFiAmountProductionOrder_Load;






            txtCustCode.Properties.ReadOnly = true;
            txtCustName.Properties.ReadOnly = true;
            txtCustOrderNo.Properties.ReadOnly = true;
            txtOrderNo.Properties.ReadOnly = true;
            txtProductionOrderNo.Properties.ReadOnly = true;
            txtProjectNo.Properties.ReadOnly = true;
            txtProjectName.Properties.ReadOnly = true;
          
          
            _bomSplitPos = splitCCB.SplitterPosition;
            tgHideSplit.Toggled += TgHideSplit_Toggled;


            spMain.Options.Behavior.Cut = DocumentCapability.Disabled;
            spMain.Options.Behavior.Paste = DocumentCapability.Disabled;
            spMain.CellBeginEdit += spMain_CellBeginEdit;
            new SpreadSheetFreezeColumn(spMain);

            tlTemp.Visible = false;


        }

        private void spMain_CellBeginEdit(object sender, SpreadsheetCellCancelEventArgs e)
        {
            e.Cancel = true;

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
        private void FrmFiAmountProductionOrder_Load(object sender, EventArgs e)
        {
  
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
            this.Text = string.Format("Report Amount BOM - (Production Order No. : {0})", productionOrderNo);

            txtCustCode.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["Customer"]);
            txtCustName.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["CustomerName"]);
            txtCustOrderNo.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["CustomerOrderNo"]);
            txtOrderNo.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["OrderNo"]);
            txtProductionOrderNo.EditValue = productionOrderNo;
            txtProjectName.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["ProjectName"]);
            txtProjectNo.EditValue = ProcessGeneral.GetSafeString(_dsProduction.Tables[0].Rows[0]["ProjectNo"]);

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

            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "TDG00001PK", "BOMID", "SortOrderNode", "PlanQuantity");












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
        private Dictionary<Int64, int> GetTableSoLinePk()
        {

            var q1 = tlMain.GetAllCheckedNodes().Select(p => new
            {
                BOMID = ProcessGeneral.GetSafeString(p.GetValue("BOMID")),
                SortOrderNode = ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode")),

            }).Where(p => p.BOMID != "").Distinct().ToList();
            Dictionary<Int64, int> q2 = q1.SelectMany(t => t.BOMID.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => ProcessGeneral.GetSafeInt64(p.Trim())).Where(p => p > 0), (m, n) => new
            {
                m.SortOrderNode,
                BOMID = n
            }).GroupBy(p => p.BOMID).Select(t => new
            {
                BOMID = t.Key,
                SortOrderNode = t.Max(s => s.SortOrderNode)
            }).OrderBy(p => p.SortOrderNode).ThenBy(p => p.BOMID).ToDictionary(p => p.BOMID, p => p.SortOrderNode);
            return q2;
        }

        #endregion








        #region "Override Button Click Event"





        protected override void PerformPrint()
        {

            ProcessGeneral.PrintSpreadSheet(spMain.Document, PageOrientation.Landscape, true);

        }

        protected override void PerformFind()
        {
            var f = new SaveFileDialog()
            {
                Title = @"Export Data",
                Filter = @"Excel 2007,2010,2013 Files (*.xlsx)|*.xlsx|Excel 2003 files (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv|Text (Tab delimited) (*.txt)|*.txt|Pdf files (*.pdf)|*.pdf",
                RestoreDirectory = true
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm dlg = new WaitDialogForm("");
                string pathExport = f.FileName;


                switch (Path.GetExtension(pathExport).ToLower().Trim())
                {
                    case ".pdf":
                        spMain.ExportToPdf(pathExport);
                        break;
                    case ".xlsx":
                        spMain.SaveDocument(pathExport, DocumentFormat.Xlsx);
                        break;
                    case ".xls":
                        spMain.SaveDocument(pathExport, DocumentFormat.Xls);
                        break;
                    case ".csv":
                        spMain.SaveDocument(pathExport, DocumentFormat.Csv);
                        break;
                    case ".txt":
                        spMain.SaveDocument(pathExport, DocumentFormat.Text);
                        break;
                }
                dlg.Close();
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
        protected override void PerformRefresh()
        {


            tlTemp.Visible = false;
            Dictionary<Int64, int> dicSel = GetTableSoLinePk();
            if (dicSel.Count <= 0)
            {
                XtraMessageBox.Show("No Item Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<DataSet> lDs = new List<DataSet>();


            WaitDialogForm dlg = new WaitDialogForm();


            foreach (var itemSel in dicSel)
            {
                DataSet dsData = _inf.PrintBoMAmount(itemSel.Key);
                if (dsData.Tables[1].Rows.Count <= 0) continue;
                lDs.Add(dsData);
            }


            if (lDs.Count <= 0)
            {
                dlg.Close();
                XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadDataIntoGrid(lDs);
            dlg.Close();
          //  tlTemp.Visible = true;

        }

        #endregion




        #region "Property"

        private const int BeginDataRow = 3;
        private const int BeginHeaderRow = 0;
        private const int EndHeaderRow = 2;
        private const int LeftCol = 2;
        private const int SpaceCount = 3;
 
    
      

        #endregion





        #region "Form Event"


        private DataTable StandardDataTreeList(List<TreeListNode> lNodeMain, DataTable dtTemp, DataTable dtAtemp)
        {

           
            DataTable dt = dtTemp.Clone();
            dt.Columns.Add("IsHasChild", typeof(bool));
            dt.Columns.Add("RowIndex", typeof(int));
            dt.Columns.Remove("SortOrderNode");
            dt.Columns.Remove("ChildPK");
            dt.Columns.Remove("ParentPK");
            dt.Columns.Remove("ItemType");



            var qAttCol = dtAtemp.AsEnumerable().Select(p => new
            {
                AttibutePK = p.Field<Int64>("AttibutePK"),
                AttibuteName = p.Field<String>("AttibuteName"),
                SortIndex = p.Field<Int32>("SortIndex"),
            }).Distinct().OrderBy(p => p.SortIndex).Select((p, idx) => new
            {
                p.AttibutePK,
                p.AttibuteName,
                SortIndex = idx + 2
            }).ToList();

            foreach (var itemCol in qAttCol)
            {
                dt.Columns.Add(string.Format("{0}-{1}", itemCol.AttibutePK, itemCol.AttibuteName), typeof(string)).SetOrdinal(itemCol.SortIndex);
            }









            Dictionary<Int64, List<BoMInputAttInfoCompact>> queryPivot1 = dtAtemp.AsEnumerable().GroupBy(f => new
            {
                ChildPK = f.Field<Int64>("RowIndex"),
            }).Where(myGroup => myGroup.Any())
                .Select(myGroup => new
                {
                    myGroup.Key.ChildPK,
                    GroupIndexAggreate = myGroup.GroupBy(f => new
                    {
                        AttibutePK = f.Field<Int64>("AttibutePK"),
                        AttibuteName = f.Field<String>("AttibuteName"),
                        AttibuteValue = f.Field<String>("AttibuteValue"),
                    }).Select(m => new BoMInputAttInfoCompact
                    {
                        AttibuteName = string.Format("{0}-{1}", m.Key.AttibutePK, m.Key.AttibuteName),
                        AttibuteValue = m.Key.AttibuteValue,
                    }).Where(n => !string.IsNullOrEmpty(n.AttibuteValue)).ToList()
                }).ToDictionary(p => p.ChildPK, p => p.GroupIndexAggreate);








            int rowIndex = BeginDataRow;
            foreach (TreeListNode node in lNodeMain)
            {
                DataRow dr = dt.NewRow();
               // dr["ItemType"] = node.GetValue("ItemType");
                dr["RMCode_001"] = node.GetValue("RMCode_001");
                dr["RMDescription_002"] = node.GetValue("RMDescription_002");
                dr["Factor"] = node.GetValue("Factor");
                dr["QuantityUnit"] = node.GetValue("QuantityUnit");
                dr["UC"] = node.GetValue("UC");
                dr["UCUnit"] = node.GetValue("UCUnit");
                dr["IsHasChild"] = node.HasChildren;
                dr["RowIndex"] = rowIndex;
                Int64 childPk = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
                List<BoMInputAttInfoCompact> lBom;
                if (queryPivot1.TryGetValue(childPk, out lBom) && lBom.Count > 0)
                {
                    foreach (BoMInputAttInfoCompact itemB in lBom)
                    {
                        dr[itemB.AttibuteName] = itemB.AttibuteValue;
                    }
                }

                
                dt.Rows.Add(dr);
                rowIndex++;

            }
            return dt;
        }


        private void LoadDataIntoGrid(List<DataSet> lDs)
        {
            //  workbook.Worksheets.RemoveAt(0);

            spMain.Options.Cells.AutoFitMergedCellRowHeight = true;
            spMain.Document.History.IsEnabled = false;
            spMain.BeginUpdate();


            IWorkbook workbook = spMain.Document;
            WorksheetCollection arrSheet = workbook.Worksheets;
            int countTemp = arrSheet.Count;

            string sheetNameTest = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now);
            workbook.Worksheets.Insert(countTemp, sheetNameTest);


            for (int i = countTemp - 1; i >= 0; i--)
            {
                arrSheet.RemoveAt(i);
            }
            countTemp = arrSheet.Count;

            for (int i = 0; i < lDs.Count; i++)
            {
                DataRow dr0 = lDs[i].Tables[0].Rows[0];
                workbook.Worksheets.Insert(countTemp + i, string.Format("BOM No. {0} Ver. {1}",  dr0["CNY015_BOMNo"], dr0["CNY004_Version"]));
            }
            for (int i = countTemp - 1; i >= 0; i--)
            {
                arrSheet.RemoveAt(i);
            }





            int j = 0;
            foreach (DataSet ds in lDs)
            {
                tlTemp.Visible = true;
                DataTable dtTree= ds.Tables[1];
                DataTable dtHeader = ds.Tables[0];
                DataRow dr0 = dtHeader.Rows[0];
                DataTable dtAtt = ds.Tables[2];
                ds.RemoveAllDataTableOnDataSet();
                tlTemp.BeginUpdate();
                tlTemp.Columns.Clear();
                tlTemp.DataSource = null;
                tlTemp.DataSource = dtTree;
                tlTemp.ParentFieldName = "ParentPK";
                tlTemp.KeyFieldName = "ChildPK";
                ProcessGeneral.HideVisibleColumnsTreeList(tlTemp, false, "SortOrderNode");
                tlTemp.ExpandAll();
                tlTemp.ForceInitialize();
                tlTemp.BeginSort();
                tlTemp.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
                tlTemp.EndSort();
                tlTemp.EndUpdate();


                List<TreeListNode> lNodeMain = tlTemp.GetAllNodeTreeList();

                Dictionary<int,List<TreeListNode>> dicNode = lNodeMain.Select(p=>new
                {
                    p.Level,
                    Node = p
                }).GroupBy(p => p.Level).Select(s => new
                {
                    Level = s.Key,
                    Node = s.Select(t=>t.Node).ToList()
                }).OrderByDescending(p=>p.Level).ToDictionary(p=>p.Level,p=>p.Node);


                var firstItem = dicNode.First();
                int maxLevel = firstItem.Key;
                List<TreeListNode> qNodeFrist = firstItem.Value;


                List<Int64> lNodeDel = new List<Int64>();

                foreach (var nodeFrist in qNodeFrist)
                {
                    string itemTypeFrist = ProcessGeneral.GetSafeString(nodeFrist.GetValue("ItemType"));
                    if (itemTypeFrist != "R")
                    {
                        nodeFrist.SetValue("UC", 0);
                        nodeFrist.SetValue("UCUnit", DefaultUcUnit);
                    }
                    else
                    {
                        if (nodeFrist.ParentNode != null)
                        {
                            lNodeDel.Add(ProcessGeneral.GetSafeInt64(nodeFrist.GetValue("ChildPK")));
                        }
                    }
                    string rmCodeFrist = ProcessGeneral.GetSafeString(nodeFrist.GetValue("RMCode_001"));
                    rmCodeFrist = rmCodeFrist.SetStringLeftSpace(maxLevel * SpaceCount);
                    nodeFrist.SetValue("RMCode_001", rmCodeFrist);
                 
                }

                maxLevel--;

                while (true)
                {
                    List<TreeListNode> lParent;
                    if (!dicNode.TryGetValue(maxLevel, out lParent))
                        break;
                    foreach (TreeListNode parentNode in lParent)
                    {
                        string itemType = ProcessGeneral.GetSafeString(parentNode.GetValue("ItemType"));
                        var lChild = parentNode.Nodes.Select(p=>new
                        {
                            GroupColumn = 1,
                            UC = ProcessGeneral.GetSafeDouble(p.GetValue("UC")),
                            UCUnit = ProcessGeneral.GetSafeString(p.GetValue("UCUnit")),

                        }).GroupBy(p=>p.GroupColumn).Select(s=>new
                        {
                            UC =   s.Sum(t=>t.UC),
                            UCUnit = s.Select(t =>  string.IsNullOrEmpty(t.UCUnit) ? DefaultUcUnit : t.UCUnit).Distinct().OrderBy(m=>m).First(),
                        }).FirstOrDefault();
                        if (lChild != null)
                        {
                            parentNode.SetValue("UC", lChild.UC);
                            parentNode.SetValue("UCUnit", lChild.UCUnit);
                        }
                        else 
                        {
                            if (itemType != "R")
                            {
                                parentNode.SetValue("UC", 0);
                                parentNode.SetValue("UCUnit", DefaultUcUnit);

                            }
                            else
                            {
                                if (parentNode.ParentNode != null && !parentNode.HasChildren)
                                {
                                    lNodeDel.Add(ProcessGeneral.GetSafeInt64(parentNode.GetValue("ChildPK")));
                             
                                }
                            }
                              

                        }

                        string rmCode = ProcessGeneral.GetSafeString(parentNode.GetValue("RMCode_001"));
                        rmCode = rmCode.SetStringLeftSpace(maxLevel * SpaceCount);
                        parentNode.SetValue("RMCode_001", rmCode);

                    }

                    maxLevel--;
                }
                dtTree.AcceptChanges();

                lNodeMain = tlTemp.GetAllNodeTreeList();

                List<TreeListNode> lDel = lNodeMain.Where(p => lNodeDel.Any(t => t == ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK")))).ToList();



                foreach (TreeListNode nodeDel in lDel)
                {
                    tlTemp.DeleteNode(nodeDel);
                    lNodeMain.Remove(nodeDel);
                }
                dtTree.AcceptChanges();

                DataTable dtFinal = StandardDataTreeList( lNodeMain, dtTree, dtAtt);
                tlTemp.Visible = false;
                string header = string.Format("{0} (BOM No.: {1} Ver. {2} - Status : {3})", dr0["Reference"], dr0["CNY015_BOMNo"], dr0["CNY004_Version"], dr0["CNY014_Status"]);
                LoadDataLayout1(dtFinal, arrSheet[j], header);
                j++;
            }

            arrSheet.ActiveWorksheet = arrSheet[0];



            spMain.EndUpdate();
            spMain.Document.History.IsEnabled = true;







            //_colSource = dt.Columns.Count;
            //_endColFix = GetEndColFix();


        }
        private void LoadDataLayout1(DataTable dt, Worksheet ws, string header)
        {


            int rowSource = dt.Rows.Count;
            int endRow = BeginDataRow + rowSource - 1;
            int colSource = dt.Columns.Count;
            Dictionary<string, int> dicCol = dt.Columns.Cast<DataColumn>().Select(p => p.ColumnName).Select((p, idx) => new
            {
                FieldName = p,
                Index = idx
            }).ToDictionary(p => p.FieldName, p => p.Index);
            int endColFix = GetEndColFix(dicCol);


            //  Worksheet ws = spMain.ActiveWorksheet;
            ws.ActiveView.ShowZeroValues = false;

            ws.Import(dt, false, BeginDataRow, 0);

            int colSNoFormat = colSource - LeftCol;

            string[] arrColBefor = { "Mã", "Tên" };
            for (int i = 0; i < arrColBefor.Length; i++)
            {
                FormatHeaderRange(ws, ws.Range.FromLTRB(i, BeginHeaderRow+1, i, EndHeaderRow),
                    SpreadsheetHorizontalAlignment.Left, arrColBefor[i], true);
            }



            string[] arrColUnit = {  "Số Lượng", "Đơn Vị (SL)", "Khối Lượng", "Đơn Vị (KL)" };
            int endColFixTemp = endColFix;
            for (int j = 0; j < arrColUnit.Length; j++)
            {



                FormatHeaderRange(ws, ws.Range.FromLTRB(endColFixTemp, BeginHeaderRow+1, endColFixTemp, EndHeaderRow),
                    SpreadsheetHorizontalAlignment.Center, arrColUnit[j], true);

                if (j == 1 || j == 3)
                {
                    FormatRangeNumber(ws.Range.FromLTRB(endColFixTemp, BeginDataRow, endColFixTemp, endRow), SpreadsheetHorizontalAlignment.Center, "");
                }

                endColFixTemp++;
            }


            var qAtt = dicCol.Where(p => p.Key.Contains("-")).ToList();
            if (qAtt.Any())
            {

                int minColAtt = qAtt.Min(p => p.Value);
                int maxColAtt = qAtt.Max(p => p.Value);


                FormatHeaderRange(ws, ws.Range.FromLTRB(minColAtt, EndHeaderRow - 1, maxColAtt, EndHeaderRow - 1),
                    SpreadsheetHorizontalAlignment.Center, "Quy Cách", true);



                foreach (var itemAtt in qAtt)
                {

                    FormatHeaderRange(ws, ws.Range.FromLTRB(itemAtt.Value, EndHeaderRow, itemAtt.Value, EndHeaderRow),
                        SpreadsheetHorizontalAlignment.Center, GetDescriptionInString(itemAtt.Key, "-"), false);
                }


                FormatRangeNumber(ws.Range.FromLTRB(minColAtt, BeginDataRow, maxColAtt, endRow), SpreadsheetHorizontalAlignment.Center, "");
            }


            FormatHeaderRange(ws, ws.Range.FromLTRB(0, BeginHeaderRow, colSNoFormat-1, BeginHeaderRow),
                SpreadsheetHorizontalAlignment.Center, header, true,true);

            var qParent = dt.AsEnumerable().Where(p => p.Field<bool>("IsHasChild")).Select(p => new
            {
                RowIndex = p.Field<Int32>("RowIndex")
            }).ToList();

            foreach (var itemP in qParent)
            {
                FormatParentRowRange(ws, ws.Range.FromLTRB(0, itemP.RowIndex, colSNoFormat - 1, itemP.RowIndex));
            }

            FormatHeaderTableColor(ws, BeginHeaderRow+1, EndHeaderRow, colSNoFormat);
            FormatBorderTable(ws, colSNoFormat, BeginHeaderRow, endRow);

            for (int delCol = 1; delCol <= LeftCol; delCol++)
            {
                ws.Columns.Remove(colSource - delCol);
            }

            BestFitColumns(ws, colSNoFormat);


          




        }

        private int GetEndColFix(Dictionary<string, int> dicCol)
        {
            var q1 = dicCol.Where(p => p.Key.Contains("-")).Select(p => p.Value).ToList();
            if (q1.Any()) return q1.Max() + 1;
            return 2;
        }


        
     

        #endregion


        #region "SpreadSheet"
     
        private void BestFitColumns(Worksheet ws, int columnNo)
        {

            ws.Columns.AutoFit(0, columnNo - 1);
           
        }

        private void FormatParentRowRange(Worksheet ws, CellRange range)
        {

            Formatting rangeFormatting = range.BeginUpdateFormatting();
            rangeFormatting.Font.FontStyle = SpreadsheetFontStyle.Bold;
            range.EndUpdateFormatting(rangeFormatting);



        }
        private void FormatHeaderRange(Worksheet ws, CellRange range, SpreadsheetHorizontalAlignment align, string strValue, bool isMerge,bool isHeader = false)
        {//11

            if (isMerge)
            {
                ws.MergeCells(range);
            }
            range.Value = strValue;
            Formatting rangeFormatting = range.BeginUpdateFormatting();

            //rangeFormatting.Font.Color = Color.DarkRed;
            rangeFormatting.Font.FontStyle = SpreadsheetFontStyle.Bold;
            rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting.Alignment.Horizontal = align;

        
            rangeFormatting.Alignment.WrapText = true;
            if (isHeader)
            {
                rangeFormatting.Font.Size = 20;
                rangeFormatting.Font.Color = Color.Black;
                rangeFormatting.Fill.BackgroundColor = Color.Azure;
            }
            else
            {
                rangeFormatting.Font.Size = 13;
            }



         









            //Fill fillA1 = rangeFormatting.Fill;
            //fillA1.FillType = FillType.Gradient;
            //fillA1.Gradient.Type = GradientFillType.Linear;
            //fillA1.Gradient.Degree = 0;
            //fillA1.Gradient.Stops.Add(0, Color.FromArgb(0, 255, 128, 0));
            //fillA1.Gradient.Stops.Add(1, Color.FromArgb(100, Color.Blue));



            range.EndUpdateFormatting(rangeFormatting);



        }

        private string GetDescriptionInString(string value, string dot)
        {
            string[] arrValue = value.Split(new string[] { dot }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (arrValue.Length == 2)
                return arrValue[1];
            return "";
        }


     
        private void FormatRangeNumber(CellRange range, SpreadsheetHorizontalAlignment align, string numberFormat)
        {

            Formatting rangeFormatting = range.BeginUpdateFormatting();
            if (!string.IsNullOrEmpty(numberFormat))
            {
                rangeFormatting.NumberFormat = numberFormat;
            }
            rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting.Alignment.Horizontal = align;
            range.EndUpdateFormatting(rangeFormatting);

        }
        private void FormatHeaderTableColor(Worksheet ws, int startHeader, int endHeader, int colNo)
        {
            CellRange rangeHeader = ws.Range.FromLTRB(0, startHeader, colNo - 1, endHeader);
            Formatting rangeFormatting = rangeHeader.BeginUpdateFormatting();
            rangeFormatting.Font.Color = Color.Chocolate;
            Fill fillA1 = rangeFormatting.Fill;
            fillA1.FillType = FillType.Gradient;
            fillA1.Gradient.Type = GradientFillType.Linear;
            fillA1.Gradient.Degree = 0;
            fillA1.Gradient.Stops.Add(0, Color.FromArgb(220, 221, 226));
            fillA1.Gradient.Stops.Add(1, Color.WhiteSmoke);
            rangeHeader.EndUpdateFormatting(rangeFormatting);
        }
        private void FormatBorderTable(Worksheet ws, int colNo, int beginRow, int endRow)
        {

            CellRange range = ws.Range.FromLTRB(0, beginRow, colNo - 1, endRow);
            Formatting range4Formatting = range.BeginUpdateFormatting();
            Borders range4Borders = range4Formatting.Borders;
            range4Borders.SetAllBorders(Color.DeepSkyBlue, BorderLineStyle.Thin);
            range.EndUpdateFormatting(range4Formatting);

        }

        #endregion

      

      
    }

}