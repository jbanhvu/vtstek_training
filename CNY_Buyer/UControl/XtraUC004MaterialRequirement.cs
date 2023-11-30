﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
//using CNY_Buyer.Common;
using CNY_Buyer.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_Buyer.UControl
{
    public partial class XtraUC004MaterialRequirement : UserControl
    {
        #region "Property"
        readonly Inf_004MaterialRequirement inf = new Inf_004MaterialRequirement();
        private readonly GridViewControlPagingFinal _tp;
        public GridView gvMainC
        {
            get
            {
                return this.gvMain;
            }
        }
        DataTable dtTemplate;
        #endregion

        #region "Contructor"
        public XtraUC004MaterialRequirement()
        {
            InitializeComponent();
            this.Load += XtraUCMain_Load;
            GridViewCustomInit();
            InitColumGridview();

            var lbFit = new List<BestFitColumnPaging>();
            _tp = new GridViewControlPagingFinal(gcMain, gvMain, lblDisplayInfo, lblCurrentRecord, txtPageSize, btnFirstPage, btnPreviousPage, btnNextPage, btnLastPage, lbFit);
        }

        private void XtraUCMain_Load(object sender, EventArgs e)
        {
            _tp.Innitial(inf.sp_MaterialRequirement_Select(-1));

        }
        #endregion

        public void Print()
        {
            try
            {
                if (gvMain.RowCount <= 0)
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
            catch (Exception e)
            {

                XtraMessageBox.Show(e.ToString());

            }
        }

        #region "Get Data"
        public void LoadData()
        {
            gcMain.DataSource = inf.sp_MaterialRequirement_Select(-1);
            gvMain.BestFitColumns();
        }

        #endregion

        #region GridView Custom Init
        private void GridViewCustomInit()
        {
            Declare_GridView();
            gvMain.CustomDrawCell += gvMain_CustomDrawCell;
            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
        }
        #endregion

        #region "khai bao Gridview"
        private void Declare_GridView()
        {
            // gcMain.ToolTipController = toolTipController1  ;

            gvMain.OptionsCustomization.AllowColumnResizing = true;
            gvMain.OptionsCustomization.AllowGroup = true;
            gvMain.OptionsCustomization.AllowColumnMoving = true;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = true;
            gvMain.OptionsCustomization.AllowFilter = true;

            gcMain.UseEmbeddedNavigator = true;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;

            //     gvMain.OptionsHint.ShowCellHints = true;
            gvMain.OptionsView.BestFitMaxRowCount = 1000;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.OptionsSelection.MultiSelect = false;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;

            gvMain.OptionsView.ShowFooter = false;

            gvMain.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = false;
            gvMain.OptionsFind.HighlightFindResults = false;
            gvMain.OptionsView.ShowGroupedColumns = true;
            gvMain.OptionsPrint.AutoWidth = true;
            gvMain.OptionsPrint.ShowPrintExportProgress = true;
            gvMain.OptionsPrint.AllowMultilineHeaders = true;
            gvMain.OptionsPrint.ExpandAllDetails = true;
            gvMain.OptionsPrint.ExpandAllGroups = true;
            gvMain.OptionsPrint.PrintDetails = true;
            gvMain.OptionsPrint.PrintFooter = true;
            gvMain.OptionsPrint.PrintGroupFooter = true;
            gvMain.OptionsPrint.PrintHeader = true;
            gvMain.OptionsPrint.PrintHorzLines = true;
            gvMain.OptionsPrint.PrintVertLines = true;
            gvMain.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvMain.OptionsPrint.SplitDataCellAcrossPages = true;
            gvMain.OptionsPrint.UsePrintStyles = false;
            gvMain.OptionsPrint.AllowCancelPrintExport = true;
            gvMain.OptionsPrint.AutoResetPrintDocument = true;

            gcMain.ForceInitialize();
        }
        #endregion

        #region Gridview event
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
            e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }
        private void gvMain_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = Properties.Resources.buy_24_24_2;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
        }
        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }
        #endregion

        #region InitColumGridview
        private void InitColumGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvMain, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvMain, "Số đề nghị", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvMain, "Mã dự án", "ProjectCode", 1);
            FormatGridView.CreateColumnOnGridview(gvMain, "Tên dự án", "ProjectName", 2);
            FormatGridView.CreateColumnOnGridview(gvMain, "Lần đề nghị", "RequestTimes", 2);
            FormatGridView.CreateColumnOnGridview(gvMain, "Người yêu cầu", "Requester", 2);
            FormatGridView.CreateColumnOnGridview(gvMain, "Chức vụ", "Position", 3);
            FormatGridView.CreateColumnOnGridview(gvMain, "Bộ phận", "Department", 4);
            FormatGridView.CreateColumnOnGridview(gvMain, "Mực độ yêu cầu", "CriticalLevel", 4);
            FormatGridView.CreateColumnOnGridview(gvMain, "Ngày đề nghị", "CreatedDate", 4);
            FormatGridView.CreateColumnOnGridview(gvMain, "Ghi chú", "Note", 8);
            FormatGridView.CreateColumnOnGridview(gvMain, "Created By", "CreatedBy", 8);
            FormatGridView.CreateColumnOnGridview(gvMain, "Created Date", "CreatedDate", 9);
            FormatGridView.CreateColumnOnGridview(gvMain, "Updated By", "UpdatedBy", 10);
            FormatGridView.CreateColumnOnGridview(gvMain, "Updated Date", "UpdatedDate", 10);
        }
        #endregion

    }
}