using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security;
using CNY_BaseSys.Info;
using CNY_BaseSys.WForm;
using CNY_WH.Class;
using CNY_WH.WForm;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
namespace CNY_WH.UControl
{
    public partial class XtraUC002ReceiptNote : UserControl
    {
        #region Property
        readonly Inf_002ReceiptNote inf = new Inf_002ReceiptNote();
        List<DataFieldControl> List_ItemField = new List<DataFieldControl>();
        private readonly GridViewControlPagingFinal _tp;
        Int32 _pk;
        public GridView gvMainC
        {
            get
            {
                return this.gvMain;
            }
        }
        DataTable dtTemplate;
        #endregion

        #region Contructor
        public XtraUC002ReceiptNote()
        {
            InitializeComponent();
            this.Load += XtraUC002ReceipNote_Load;
            GridViewCustomInit();

            var lbFit = new List<BestFitColumnPaging>();
            _tp = new GridViewControlPagingFinal(gcMain, gvMain, lblDisplayInfo, lblCurrentRecord, txtPageSize, btnFirstPage, btnPreviousPage, btnNextPage, btnLastPage, lbFit);
        }

        private void XtraUC002ReceipNote_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            DataTable dt = inf.sp_ReceiptNote_Select(-1);
            PermissionFormInfo qPer = ProcessGeneral.GetPermissionByFormCode("Frm002ReceipNote");
            if (!qPer.StrSpecialFunction.Contains("CEO") && !qPer.StrSpecialFunction.Contains("SM"))
            {
                var q1 = dt.AsEnumerable().Where(p => p.Field<String>("SalemanID") == DeclareSystem.SysUserName).ToList();
                dt = q1.Any() ? q1.CopyToDataTable() : dt.Clone();
            }
            _tp.Innitial(dt);
            gvMain.Columns[0].Width += 20;
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

        private void Declare_GridView()
        {
            // gcMain.ToolTipController = toolTipController1  ;

            gvMain.OptionsCustomization.AllowColumnResizing = true;
            gvMain.OptionsCustomization.AllowGroup = false;
            gvMain.OptionsCustomization.AllowColumnMoving = true;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = true;
            gvMain.OptionsCustomization.AllowFilter = false;

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
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            //new MyFindPanelFilterHelper(gvMain)
            //{
            //    //AllowGroupBy = true,
            //    IsPerFormEvent = true,
            //};
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

    }
}
