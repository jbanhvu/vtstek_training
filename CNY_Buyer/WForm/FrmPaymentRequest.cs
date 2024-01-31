using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Info;
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
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

namespace CNY_Buyer.WForm
{
    public partial class FrmPaymentRequest : FrmBase
    {
        #region Properties
        private Inf_PaymentRequest _inf;
        #endregion Properties

        public FrmPaymentRequest()
        {
            InitializeComponent();
            this.Load += FrmPaymentRequest_Load;
            _inf = new Inf_PaymentRequest();
        }

        #region Load Data
        private void FrmPaymentRequest_Load(object sender, EventArgs e)
        {
            //GridView
            DeclareGridview();
            InitColumnGridview();
            gcPaymentRequest.DataSource = _inf.sp_PaymentRequest_Select(-1);
            gvPaymentRequest.BestFitColumns();

            //SearchLookUpEdit
            SearchLookUpEdit_Load();

            //TextEdit
            TextEdit_Load();
        }

        private void TextEdit_Load()
        {
            txtReason.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "Reason"));
            txtNote.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "Note"));
            txtCreatedBy.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "CreatedBy"));
            txtUpdatedBy.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "UpdatedBy"));
            dePaymentDate.EditValue = ProcessGeneral.GetSafeDatetime(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "PaymentDate"));
            deCreatedDate.EditValue = ProcessGeneral.GetSafeDatetime(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "CreatedDate"));
            deUpdatedDate.EditValue = ProcessGeneral.GetSafeDatetime(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "UpdatedDate"));
            txtProjectCode.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "ProjectCode"));
            txtRequestType.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "RequestType"));
            txtStatus.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "Status"));
            txtPaymentStatus.EditValue = ProcessGeneral.GetSafeString(gvPaymentRequest.GetRowCellValue(gvPaymentRequest.FocusedRowHandle, "PaymentStatus"));

            //UnEnable TextEdit
            txtReason.Enabled = false;
            txtNote.Enabled = false;
            txtCreatedBy.Enabled = false;
            txtUpdatedBy.Enabled = false;
            dePaymentDate.Enabled = false;
            deCreatedDate.Enabled = false;
            deUpdatedDate.Enabled = false;
            txtProjectCode.Enabled  = false;
            txtRequestType.Enabled = false;
            txtStatus.Enabled = false;
            txtPaymentStatus.Enabled = false;

        }

        private void SearchLookUpEdit_Load()
        {
            //Purchase Requisition
            sluePurchaseRequisitionPK.Properties.DataSource = _inf.Excute("Select PK [Code],ProjectCode [Mã dự án], Buyer [Người mua], Requester [Người yêu cầu] from dbo.PurchaseRequisition");
            sluePurchaseRequisitionPK.Properties.DisplayMember = "Code";
            sluePurchaseRequisitionPK.Properties.ValueMember = "Code";

            //RequestType
            slueRequestType.Properties.DataSource = _inf.Excute("SELECT Code [Code], Name [Loại yêu cầu] FROM dbo.Common_Masterdata WHERE Module = 'RequestType'");
            slueRequestType.Properties.DisplayMember = "Code";
            slueRequestType.Properties.ValueMember = "Code";

            //Status
            slueStatus.Properties.DataSource = _inf.Excute("SELECT Code [Code], Name [Tình trạng] FROM dbo.Common_Masterdata WHERE Module = 'PaymentRequest_Status'");
            slueStatus.Properties.DisplayMember = "Code";
            slueStatus.Properties.ValueMember = "Code";

            //Payment Status
            sluePaymentStatus.Properties.DataSource = _inf.Excute("SELECT Code [Code], Name [Tình trạng] FROM dbo.Common_Masterdata WHERE Module = 'PaymentRequest_PaymentStatus'");
            sluePaymentStatus.Properties.DisplayMember = "Code";
            sluePaymentStatus.Properties.ValueMember = "Code";

            //Init value
            sluePurchaseRequisitionPK.Properties.NullText = string.Empty;
            slueRequestType.Properties.NullText = string.Empty;
            slueStatus.Properties.NullText = string.Empty;
            sluePaymentStatus.Properties.NullText = string.Empty;

            //UnEnable Search Look up Edit 
            //sluePurchaseRequisitionPK.Enabled = false;
            //slueRequestType.Enabled = false;
            //slueStatus.Enabled = false;
            //sluePaymentStatus.Enabled = false;
        }
        #endregion Load Data

        #region Format GridView
        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Loại đề nghị", "RequestType", 2);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Mã dự án", "ProjectCode", 3);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Lý do", "Reason", 4);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Tình trạng yêu cầu", "Status", 5);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Tình trạng thanh toán", "PaymentStatus", 6);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Ngày thanh toán", "PaymentDate", 7);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Ghi chú", "Note", 8);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Người tạo", "CreatedBy", 9);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Ngày tạo", "CreatedDate", 10);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Người cập nhật", "UpdatedBy", 11);
            FormatGridView.CreateColumnOnGridview(gvPaymentRequest, HorzAlignment.Default, "Ngày cập nhật", "UpdatedDate", 12);
        }

        private void DeclareGridview()
        {
            // gcPaymentRequest.ToolTipController = toolTipController1  ;

            gvPaymentRequest.OptionsCustomization.AllowColumnResizing = true;
            gvPaymentRequest.OptionsCustomization.AllowGroup = true;
            gvPaymentRequest.OptionsCustomization.AllowColumnMoving = true;
            gvPaymentRequest.OptionsCustomization.AllowQuickHideColumns = true;
            gvPaymentRequest.OptionsCustomization.AllowSort = true;
            gvPaymentRequest.OptionsCustomization.AllowFilter = true;

            gcPaymentRequest.UseEmbeddedNavigator = true;

            gcPaymentRequest.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcPaymentRequest.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcPaymentRequest.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcPaymentRequest.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcPaymentRequest.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvPaymentRequest.OptionsBehavior.Editable = false;
            gvPaymentRequest.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvPaymentRequest.OptionsHint.ShowCellHints = true;
            gvPaymentRequest.OptionsView.BestFitMaxRowCount = 1000;
            gvPaymentRequest.OptionsView.ShowGroupPanel = true;
            gvPaymentRequest.OptionsView.ShowIndicator = true;
            gvPaymentRequest.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvPaymentRequest.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvPaymentRequest.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvPaymentRequest.OptionsView.ShowAutoFilterRow = true;
            gvPaymentRequest.OptionsView.AllowCellMerge = false;
            gvPaymentRequest.HorzScrollVisibility = ScrollVisibility.Auto;
            gvPaymentRequest.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvPaymentRequest.OptionsNavigation.AutoFocusNewRow = true;
            gvPaymentRequest.OptionsNavigation.UseTabKey = true;

            gvPaymentRequest.OptionsSelection.MultiSelect = false;
            gvPaymentRequest.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvPaymentRequest.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvPaymentRequest.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvPaymentRequest.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvPaymentRequest.OptionsView.EnableAppearanceEvenRow = false;
            gvPaymentRequest.OptionsView.EnableAppearanceOddRow = false;

            gvPaymentRequest.OptionsView.ShowFooter = false;

            gvPaymentRequest.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvPaymentRequest.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvPaymentRequest.OptionsFind.AlwaysVisible = false;
            gvPaymentRequest.OptionsFind.ShowCloseButton = true;
            gvPaymentRequest.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvPaymentRequest)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvPaymentRequest.OptionsView.ShowGroupedColumns = true;
            gvPaymentRequest.OptionsPrint.AutoWidth = true;
            gvPaymentRequest.OptionsPrint.ShowPrintExportProgress = true;
            gvPaymentRequest.OptionsPrint.AllowMultilineHeaders = true;
            gvPaymentRequest.OptionsPrint.ExpandAllDetails = true;
            gvPaymentRequest.OptionsPrint.ExpandAllGroups = true;
            gvPaymentRequest.OptionsPrint.PrintDetails = true;
            gvPaymentRequest.OptionsPrint.PrintFooter = true;
            gvPaymentRequest.OptionsPrint.PrintGroupFooter = true;
            gvPaymentRequest.OptionsPrint.PrintHeader = true;
            gvPaymentRequest.OptionsPrint.PrintHorzLines = true;
            gvPaymentRequest.OptionsPrint.PrintVertLines = true;
            gvPaymentRequest.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvPaymentRequest.OptionsPrint.SplitDataCellAcrossPages = true;
            gvPaymentRequest.OptionsPrint.UsePrintStyles = false;
            gvPaymentRequest.OptionsPrint.AllowCancelPrintExport = true;
            gvPaymentRequest.OptionsPrint.AutoResetPrintDocument = true;

            gcPaymentRequest.ForceInitialize();
        }
        #endregion Format

        #region Events

        #endregion Events

        private void gcPaymentRequest_Click(object sender, EventArgs e)
        {
            DataTable dt = _inf.sp_PaymentRequest_Select(-1);
            gcPaymentRequest.DataSource = dt;
            gvPaymentRequest.BestFitColumns();
            SearchLookUpEdit_Load();
            TextEdit_Load();
        }
    }
}
