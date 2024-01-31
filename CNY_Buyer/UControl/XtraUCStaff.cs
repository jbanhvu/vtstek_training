using CNY_BaseSys.Common;
using CNY_Buyer.Info;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_Buyer.UControl
{
    public partial class XtraUCStaff : UserControl
    {
        #region Properties
        public GridView gvMainC
        {
            get
            {
                return this.gvMain;
            }
        }
        private Inf_Staff _inf;
        #endregion

        #region Constructor
        public XtraUCStaff()
        {
            InitializeComponent();
            _inf = new Inf_Staff();
            InitColumnGridview();
            DeclareGridview();
            this.Load += XtraUCStaff_Load;
        }
        #endregion

        #region Load Data
        private void XtraUCStaff_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        internal void LoadData()
        {
            gcMain.DataSource = _inf.sp_Staff_Select(-1);
            gvMain.BestFitColumns();

            //Load Data for Search Look Up Edit 

        }
        #endregion

        #region Format GridView
        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "DBO", "DOB", 2);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Họ và tên", "FullName", 3);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Mã NV", "Code", 4);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Mã chấm công", "EncrolNumber", 5);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Ngày vào làm", "HireDate", 6);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Giới tính", "Sex", 7);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Số CMND", "IdentityCard_Number", 8);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Ngày cấp", "IdentityCard_CreatedDate", 9);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Nơi cấp", "IdentityCard_CreatedWhere", 10);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Phòng Ban", "DepartmentCode", 11);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Vị trí", "PositionCode", 12);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Học vấn", "EducationLevel", 13);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Số điện thoại", "PhoneNumber", 14);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Email", "Email", 15);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Thâm niên", "Seniority", 16);
            FormatGridView.CreateColumnOnGridview(gvMain, HorzAlignment.Default, "Địa chỉ", "Address", 17);

        }
        private void DeclareGridview()
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
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

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
        #endregion
    }
}
