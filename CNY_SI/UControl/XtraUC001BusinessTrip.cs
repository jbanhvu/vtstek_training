using CNY_BaseSys.Common;
using CNY_SI.Info;
using DevExpress.Utils;
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

namespace CNY_SI.UControl
{
    public partial class XtraUC001BusinessTrip : UserControl
    {
        public GridView gvBusinessTripC
        {
            get
            {
                return this.gvBusinessTrip;
            }
        }
        private Inf_001BusinessTrip inf = new Inf_001BusinessTrip();
        public XtraUC001BusinessTrip()
        {
            InitializeComponent();
            Declare_GridView();
            InitColumnGridview();
            gvBusinessTrip.RowStyle += gvBusinessTrip_RowStyle;
            gvBusinessTrip.CustomDrawCell += gvBusinessTrip_CustomDrawCell;
            this.Load += XtraUC001BusinessTrip_Load;
        }

        private void XtraUC001BusinessTrip_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void gvBusinessTrip_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "CompanyCode")
            {
                Image icon = Properties.Resources.folder_documents_icon;
                e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }
        }
        private void gvBusinessTrip_RowStyle(object sender, RowStyleEventArgs e)
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
            gcBusinessTrip.DataSource = inf.sp_BusinessTrip_Select(-1);
            gvBusinessTrip.BestFitColumns();
        }

        #region Declare Gridview 
        private void Declare_GridView()
        {
            // gcMain.ToolTipController = toolTipController1  ;

            gvBusinessTrip.OptionsCustomization.AllowColumnResizing = true;
            gvBusinessTrip.OptionsCustomization.AllowGroup = true;
            gvBusinessTrip.OptionsCustomization.AllowColumnMoving = true;
            gvBusinessTrip.OptionsCustomization.AllowQuickHideColumns = true;
            gvBusinessTrip.OptionsCustomization.AllowSort = true;
            gvBusinessTrip.OptionsCustomization.AllowFilter = true;

            gcBusinessTrip.UseEmbeddedNavigator = true;

            gcBusinessTrip.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcBusinessTrip.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcBusinessTrip.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcBusinessTrip.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcBusinessTrip.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvBusinessTrip.OptionsBehavior.Editable = false;
            gvBusinessTrip.OptionsBehavior.AllowAddRows = DefaultBoolean.False;

            //     gvMain.OptionsHint.ShowCellHints = true;
            gvBusinessTrip.OptionsView.BestFitMaxRowCount = 1000;
            gvBusinessTrip.OptionsView.ShowGroupPanel = true;
            gvBusinessTrip.OptionsView.ShowIndicator = true;
            gvBusinessTrip.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvBusinessTrip.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvBusinessTrip.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvBusinessTrip.OptionsView.ShowAutoFilterRow = true;
            gvBusinessTrip.OptionsView.AllowCellMerge = false;
            gvBusinessTrip.HorzScrollVisibility = ScrollVisibility.Auto;
            gvBusinessTrip.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvBusinessTrip.OptionsNavigation.AutoFocusNewRow = true;
            gvBusinessTrip.OptionsNavigation.UseTabKey = true;

            gvBusinessTrip.OptionsSelection.MultiSelect = false;
            gvBusinessTrip.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvBusinessTrip.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvBusinessTrip.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvBusinessTrip.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvBusinessTrip.OptionsView.EnableAppearanceEvenRow = false;
            gvBusinessTrip.OptionsView.EnableAppearanceOddRow = false;

            gvBusinessTrip.OptionsView.ShowFooter = false;

            gvBusinessTrip.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvBusinessTrip.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvBusinessTrip.OptionsFind.AlwaysVisible = false;
            gvBusinessTrip.OptionsFind.ShowCloseButton = true;
            gvBusinessTrip.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvBusinessTrip)
        {
            //AllowGroupBy = true,
            IsPerFormEvent = true,
            };
            gvBusinessTrip.OptionsView.ShowGroupedColumns = true;
            gvBusinessTrip.OptionsPrint.AutoWidth = true;
            gvBusinessTrip.OptionsPrint.ShowPrintExportProgress = true;
            gvBusinessTrip.OptionsPrint.AllowMultilineHeaders = true;
            gvBusinessTrip.OptionsPrint.ExpandAllDetails = true;
            gvBusinessTrip.OptionsPrint.ExpandAllGroups = true;
            gvBusinessTrip.OptionsPrint.PrintDetails = true;
            gvBusinessTrip.OptionsPrint.PrintFooter = true;
            gvBusinessTrip.OptionsPrint.PrintGroupFooter = true;
            gvBusinessTrip.OptionsPrint.PrintHeader = true;
            gvBusinessTrip.OptionsPrint.PrintHorzLines = true;
            gvBusinessTrip.OptionsPrint.PrintVertLines = true;
            gvBusinessTrip.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvBusinessTrip.OptionsPrint.SplitDataCellAcrossPages = true;
            gvBusinessTrip.OptionsPrint.UsePrintStyles = false;
            gvBusinessTrip.OptionsPrint.AllowCancelPrintExport = true;
            gvBusinessTrip.OptionsPrint.AutoResetPrintDocument = true;

            gcBusinessTrip.ForceInitialize();
        }
        #region Initilize Column GridView 
        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Người yêu cầu", "RequestUser", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Nội dung", "Content", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Ngày đi", "StartAt", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Ngày về", "EndAt", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Chi phí", "Cost", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Tình trạng", "Status", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Kết luận", "Conclusion", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Người tạo", "CreatedBy", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Thời gian tạo", "CreatedDate", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Người cập nhật", "UpdatedBy", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Thời gian cập nhật", "UpdatedDate", 1);
            FormatGridView.CreateColumnOnGridview(gvBusinessTrip, HorzAlignment.Default, "Ghi chú", "Note", 1);

            gvBusinessTrip.Columns["Cost"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvBusinessTrip.Columns["Cost"].DisplayFormat.FormatString = "{0:#,#}";

        }
        #endregion

        #endregion

        private void gcBusinessTrip_Click(object sender, EventArgs e)
        {

        }
    }
}
