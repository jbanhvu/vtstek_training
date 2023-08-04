using CNY_BaseSys.Common;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_WH.UControl
{
    public partial class XtraUC001Stock : UserControl
    {
        public GridView gvMainC
        {
            get
            {
                return this.gvMain;
            }
        }
        private Inf_001Stock inf = new Inf_001Stock();
        public XtraUC001Stock()
        {
            InitializeComponent();
            Declare_GridView();
            InitColumGridview();
            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.CustomDrawCell += gvMain_CustomDrawCell;
            this.Load += XtraUCCompany_Load;
        }

        private void XtraUCCompany_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void gvMain_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "CompanyCode")
            {
                Image icon = Properties.Resources.folder_documents_icon;
                e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }
        }
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.HighPriority = true;
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }
        }
        public void LoadData()
        {
            gcMain.DataSource = inf.sp_Stock_Select(-1);
            gvMain.BestFitColumns();
        }

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
            gvMain.OptionsView.ShowGroupPanel = true;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = true;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

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

            gvMain.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
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

        #region InitColumGridview
        private void InitColumGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Mã vật tư", "Code", 1);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Tên vật tư", "Name", 2);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Center, "Nhóm vật tư", "StockType", 3);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Center, "Loại vật tư", "StockGroup", 4);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Center, "Phạm vi", "StockScope", 5);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Center, "Đơn vị tính", "Unit", 6);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Center, "Hãng sản xuát", "Manufacturer", 6);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Center, "Xuất xứ", "Origin", 6);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Center, "Chứng chỉ", "Certificate", 6);
        }
        #endregion
        #endregion
    }
}
