using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Info;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
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
using System.Xml.Linq;

namespace CNY_Buyer.WForm
{
    public partial class Frm001UpdateRequestType : FrmBase
    {
        Inf_001PurchaseRequisition _inf;
        Int64 requestTypePK;
        Int64 requestPK;
        public Frm001UpdateRequestType()
        {
            InitializeComponent();
            this.Load += Frm001UpdateRequestType_Load;
            _inf = new Inf_001PurchaseRequisition();
        }

        private void Frm001UpdateRequestType_Load(object sender, EventArgs e)
        {
            HidenButton();
            Request_LoadData();
            RequestType_LoadData();
        }

        #region Load Data GridView
        private void Request_LoadData()
        {
            Request_DeclareGridview();
            Request_InitColumnGridview();
            gcRequest.DataSource = _inf.sp_PurchaseRequisition_Select(-1);
            gvRequest.BestFitColumns();
        }

        private void RequestType_LoadData()
        {
            RequestType_DeclareGridview();
            RequestType_InitColumnGridview();
            gcRequestType.DataSource = _inf.sp_FunctionInApproval_Select("Add","PurchaseRequisition");
            gvRequestType.BestFitColumns();

        }
        #endregion Load Data GridView

        #region Initilize Column GridView 
        private void RequestType_InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvRequestType, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvRequestType, HorzAlignment.Default, "Loại yêu cầu", "Name", 2);
        }

        private void Request_InitColumnGridview()
        {
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "STT", "PK", -1);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Mã dự án", "ProjectCode", -1);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Người yêu cầu", "Requester", 1);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Người mua", "Buyer", 2);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Số yêu cầu", "RequestNumber", 3);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Số hóa đơn", "ReceiptNumber", 4);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Số PO", "PONumber", 5);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Nhà cung cấp", "Supplier", 6);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Phương thức thanh toán", "PaymentMethod", 7);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Ngày nợ", "DateOfDebt", 7);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Tạo bởi", "Created_By", 7);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Ngày tạo", "Created_Date", 7);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Cập nhật bởi", "Updated_By", 7);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Ngày cập nhật", "Updated_Date", 7);
                FormatGridView.CreateColumnOnGridview(gvRequest, HorzAlignment.Default, "Ghi chú", "Note", 7);

        }
        #endregion Initilize Column GridView 

        #region Declare GridView
        private void RequestType_DeclareGridview()
        {
            // gcMain.ToolTipController = toolTipController1  ;

            gvRequestType.OptionsCustomization.AllowColumnResizing = true;
            gvRequestType.OptionsCustomization.AllowGroup = true;
            gvRequestType.OptionsCustomization.AllowColumnMoving = true;
            gvRequestType.OptionsCustomization.AllowQuickHideColumns = true;
            gvRequestType.OptionsCustomization.AllowSort = true;
            gvRequestType.OptionsCustomization.AllowFilter = true;

            gcRequestType.UseEmbeddedNavigator = true;

            gcRequestType.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcRequestType.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcRequestType.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcRequestType.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcRequestType.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvRequestType.OptionsBehavior.Editable = false;
            gvRequestType.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvMain.OptionsHint.ShowCellHints = true;
            gvRequestType.OptionsView.BestFitMaxRowCount = 1000;
            gvRequestType.OptionsView.ShowGroupPanel = true;
            gvRequestType.OptionsView.ShowIndicator = true;
            gvRequestType.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvRequestType.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvRequestType.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvRequestType.OptionsView.ShowAutoFilterRow = true;
            gvRequestType.OptionsView.AllowCellMerge = false;
            gvRequestType.HorzScrollVisibility = ScrollVisibility.Auto;
            gvRequestType.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvRequestType.OptionsNavigation.AutoFocusNewRow = true;
            gvRequestType.OptionsNavigation.UseTabKey = true;

            gvRequestType.OptionsSelection.MultiSelect = false;
            gvRequestType.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvRequestType.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvRequestType.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvRequestType.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvRequestType.OptionsView.EnableAppearanceEvenRow = false;
            gvRequestType.OptionsView.EnableAppearanceOddRow = false;

            gvRequestType.OptionsView.ShowFooter = false;

            gvRequestType.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvRequestType.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvRequestType.OptionsFind.AlwaysVisible = false;
            gvRequestType.OptionsFind.ShowCloseButton = true;
            gvRequestType.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvRequestType)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvRequestType.OptionsView.ShowGroupedColumns = true;
            gvRequestType.OptionsPrint.AutoWidth = true;
            gvRequestType.OptionsPrint.ShowPrintExportProgress = true;
            gvRequestType.OptionsPrint.AllowMultilineHeaders = true;
            gvRequestType.OptionsPrint.ExpandAllDetails = true;
            gvRequestType.OptionsPrint.ExpandAllGroups = true;
            gvRequestType.OptionsPrint.PrintDetails = true;
            gvRequestType.OptionsPrint.PrintFooter = true;
            gvRequestType.OptionsPrint.PrintGroupFooter = true;
            gvRequestType.OptionsPrint.PrintHeader = true;
            gvRequestType.OptionsPrint.PrintHorzLines = true;
            gvRequestType.OptionsPrint.PrintVertLines = true;
            gvRequestType.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvRequestType.OptionsPrint.SplitDataCellAcrossPages = true;
            gvRequestType.OptionsPrint.UsePrintStyles = false;
            gvRequestType.OptionsPrint.AllowCancelPrintExport = true;
            gvRequestType.OptionsPrint.AutoResetPrintDocument = true;

            gcRequestType.ForceInitialize();
        }


        private void Request_DeclareGridview()
        {
            // gcMain.ToolTipController = toolTipController1  ;

            gvRequest.OptionsCustomization.AllowColumnResizing = true;
            gvRequest.OptionsCustomization.AllowGroup = true;
            gvRequest.OptionsCustomization.AllowColumnMoving = true;
            gvRequest.OptionsCustomization.AllowQuickHideColumns = true;
            gvRequest.OptionsCustomization.AllowSort = true;
            gvRequest.OptionsCustomization.AllowFilter = true;

            gcRequest.UseEmbeddedNavigator = true;

            gcRequest.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcRequest.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcRequest.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcRequest.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcRequest.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvRequest.OptionsBehavior.Editable = false;
            gvRequest.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvMain.OptionsHint.ShowCellHints = true;
            gvRequest.OptionsView.BestFitMaxRowCount = 1000;
            gvRequest.OptionsView.ShowGroupPanel = true;
            gvRequest.OptionsView.ShowIndicator = true;
            gvRequest.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvRequest.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvRequest.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvRequest.OptionsView.ShowAutoFilterRow = true;
            gvRequest.OptionsView.AllowCellMerge = false;
            gvRequest.HorzScrollVisibility = ScrollVisibility.Auto;
            gvRequest.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvRequest.OptionsNavigation.AutoFocusNewRow = true;
            gvRequest.OptionsNavigation.UseTabKey = true;

            gvRequest.OptionsSelection.MultiSelect = false;
            gvRequest.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvRequest.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvRequest.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvRequest.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvRequest.OptionsView.EnableAppearanceEvenRow = false;
            gvRequest.OptionsView.EnableAppearanceOddRow = false;

            gvRequest.OptionsView.ShowFooter = false;

            gvRequest.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvRequest.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvRequest.OptionsFind.AlwaysVisible = false;
            gvRequest.OptionsFind.ShowCloseButton = true;
            gvRequest.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvRequest)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvRequest.OptionsView.ShowGroupedColumns = true;
            gvRequest.OptionsPrint.AutoWidth = true;
            gvRequest.OptionsPrint.ShowPrintExportProgress = true;
            gvRequest.OptionsPrint.AllowMultilineHeaders = true;
            gvRequest.OptionsPrint.ExpandAllDetails = true;
            gvRequest.OptionsPrint.ExpandAllGroups = true;
            gvRequest.OptionsPrint.PrintDetails = true;
            gvRequest.OptionsPrint.PrintFooter = true;
            gvRequest.OptionsPrint.PrintGroupFooter = true;
            gvRequest.OptionsPrint.PrintHeader = true;
            gvRequest.OptionsPrint.PrintHorzLines = true;
            gvRequest.OptionsPrint.PrintVertLines = true;
            gvRequest.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvRequest.OptionsPrint.SplitDataCellAcrossPages = true;
            gvRequest.OptionsPrint.UsePrintStyles = false;
            gvRequest.OptionsPrint.AllowCancelPrintExport = true;
            gvRequest.OptionsPrint.AutoResetPrintDocument = true;

            gcRequest.ForceInitialize();
        }


        #endregion Declare GridView

        private void HidenButton()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowExpand = false;
            AllowGenerate = false;
            AllowCancel = true;
            AllowRevision = false;
            AllowSave = true;
            AllowFind = false;
            AllowPrint = false;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            EnableCancel = false;
            EnableSave = true;
        }

        protected override void PerformSave()
        {
            requestTypePK = ProcessGeneral.GetSafeInt(gvRequestType.GetRowCellValue(gvRequestType.FocusedRowHandle, "PK"));
            requestPK = ProcessGeneral.GetSafeInt(gvRequest.GetRowCellValue(gvRequest.FocusedRowHandle, "PK"));

            DataTable dtResult = _inf.sp_PurchaseRequisition_UpdateRequestType(requestPK, requestTypePK);

            if(dtResult != null)
            {
                string msg = "Cập nhật thành công!";
                XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Request_LoadData();
            RequestType_LoadData();
        }
    }
}
