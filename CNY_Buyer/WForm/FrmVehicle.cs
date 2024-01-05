using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Info;
using DevExpress.Utils;
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

namespace CNY_Buyer.WForm
{
    public partial class FrmVehicle : FrmBase
    {
        Inf_Vehicle _inf;
        private bool IsInsert;
        public FrmVehicle()
        {
            InitializeComponent();
            this.Load += FrmVehicle_Load;
            _inf = new Inf_Vehicle();
        }

        private void FrmVehicle_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DeclareGridview();
            InitColumnGridview();
            ChangeMenuBar_Close();
            gcVehicle.DataSource = _inf.sp_Vehicle_Select(-1);
            gvVehicle.BestFitColumns();
            LoadText();
        }

        private void LoadText()
        {
            txtName.EditValue = ProcessGeneral.GetSafeString(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "Name"));
            txtRegistration.EditValue = ProcessGeneral.GetSafeString(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "Registration"));
            txtNote.EditValue = ProcessGeneral.GetSafeString(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "Note"));
            txtCreatedBy.EditValue = ProcessGeneral.GetSafeString(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "CreatedBy"));
            txtCreatedDate.EditValue = ProcessGeneral.GetSafeDatetime(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "CreatedDate"));
            txtUpdatedBy.EditValue = ProcessGeneral.GetSafeString(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "UpdatedBy"));
            txtUpdatedDate.EditValue = ProcessGeneral.GetSafeString(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "UpdatedDate"));

            txtName.Enabled = false;
            txtRegistration.Enabled = false;
            txtNote.Enabled = false;
            txtCreatedBy.Enabled = false;
            txtCreatedDate.Enabled = false;
            txtUpdatedBy.Enabled = false;
            txtUpdatedDate.Enabled = false;
        }

        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "Tên phương tiện", "Name", 2);
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "Biển số", "Registration", 3);
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "Ghi chú", "Note", 4);
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "Người tạo", "CreatedBy", 5);
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "Ngày tạo", "CreatedDate", 6);
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "Người cập nhật", "UpdatedBy", 7);
            FormatGridView.CreateColumnOnGridview(gvVehicle, HorzAlignment.Default, "Ngày cập nhật", "UpdatedDate", 8);
        }

        private void DeclareGridview()
        {
            // gcVehicle.ToolTipController = toolTipController1  ;

            gvVehicle.OptionsCustomization.AllowColumnResizing = true;
            gvVehicle.OptionsCustomization.AllowGroup = true;
            gvVehicle.OptionsCustomization.AllowColumnMoving = true;
            gvVehicle.OptionsCustomization.AllowQuickHideColumns = true;
            gvVehicle.OptionsCustomization.AllowSort = true;
            gvVehicle.OptionsCustomization.AllowFilter = true;

            gcVehicle.UseEmbeddedNavigator = true;

            gcVehicle.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcVehicle.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcVehicle.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcVehicle.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcVehicle.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvVehicle.OptionsBehavior.Editable = false;
            gvVehicle.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvVehicle.OptionsHint.ShowCellHints = true;
            gvVehicle.OptionsView.BestFitMaxRowCount = 1000;
            gvVehicle.OptionsView.ShowGroupPanel = true;
            gvVehicle.OptionsView.ShowIndicator = true;
            gvVehicle.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvVehicle.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvVehicle.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvVehicle.OptionsView.ShowAutoFilterRow = true;
            gvVehicle.OptionsView.AllowCellMerge = false;
            gvVehicle.HorzScrollVisibility = ScrollVisibility.Auto;
            gvVehicle.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvVehicle.OptionsNavigation.AutoFocusNewRow = true;
            gvVehicle.OptionsNavigation.UseTabKey = true;

            gvVehicle.OptionsSelection.MultiSelect = false;
            gvVehicle.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvVehicle.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvVehicle.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvVehicle.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvVehicle.OptionsView.EnableAppearanceEvenRow = false;
            gvVehicle.OptionsView.EnableAppearanceOddRow = false;

            gvVehicle.OptionsView.ShowFooter = false;

            gvVehicle.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvVehicle.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvVehicle.OptionsFind.AlwaysVisible = false;
            gvVehicle.OptionsFind.ShowCloseButton = true;
            gvVehicle.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvVehicle)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvVehicle.OptionsView.ShowGroupedColumns = true;
            gvVehicle.OptionsPrint.AutoWidth = true;
            gvVehicle.OptionsPrint.ShowPrintExportProgress = true;
            gvVehicle.OptionsPrint.AllowMultilineHeaders = true;
            gvVehicle.OptionsPrint.ExpandAllDetails = true;
            gvVehicle.OptionsPrint.ExpandAllGroups = true;
            gvVehicle.OptionsPrint.PrintDetails = true;
            gvVehicle.OptionsPrint.PrintFooter = true;
            gvVehicle.OptionsPrint.PrintGroupFooter = true;
            gvVehicle.OptionsPrint.PrintHeader = true;
            gvVehicle.OptionsPrint.PrintHorzLines = true;
            gvVehicle.OptionsPrint.PrintVertLines = true;
            gvVehicle.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvVehicle.OptionsPrint.SplitDataCellAcrossPages = true;
            gvVehicle.OptionsPrint.UsePrintStyles = false;
            gvVehicle.OptionsPrint.AllowCancelPrintExport = true;
            gvVehicle.OptionsPrint.AutoResetPrintDocument = true;

            gcVehicle.ForceInitialize();
        }
        private void ChangeMenuBar_Close()
        {
            AllowAdd = true;
            AllowEdit = true;
            AllowClose = true;
            AllowDelete = false;
            AllowCancel = false;
            AllowExpand = false;
            AllowGenerate = false;
            AllowRevision = false;
            AllowSave = false;
            AllowFind = false;
            AllowPrint = false;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
        }
        private void ChangeMenuBar_Open()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowExpand = false;
            AllowSave = true;
            AllowGenerate = false;
            AllowCancel = true;
            EnableCancel = true;
            AllowRevision = false;
            EnableSave = true;
            AllowFind = false;
            AllowPrint = false;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            AllowClose = false;
        }
        protected override void PerformAdd()
        {
            ChangeMenuBar_Open();

            IsInsert = true;

            txtName.EditValue = string.Empty;
            txtRegistration.EditValue = string.Empty;
            txtNote.EditValue = string.Empty;
            txtCreatedBy.EditValue = string.Empty;
            txtCreatedDate.EditValue = string.Empty;
            txtUpdatedBy.EditValue = string.Empty;
            txtUpdatedDate.EditValue = string.Empty;

            txtName.Enabled = true;
            txtRegistration.Enabled = true;
            txtNote.Enabled = true;
            txtCreatedBy.Enabled = true;
            txtCreatedDate.Enabled = true;
            txtUpdatedBy.Enabled = true;
            txtUpdatedDate.Enabled = true;

            gcVehicle.Enabled = false;

        }
        protected override void PerformEdit()
        {
            ChangeMenuBar_Open();

            if (!gvVehicle.IsDataRow(gvVehicle.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtName.Enabled = true;
            txtRegistration.Enabled = true;
            txtNote.Enabled = true;
            txtCreatedBy.Enabled = true;
            txtCreatedDate.Enabled = true;
            txtUpdatedBy.Enabled = true;
            txtUpdatedDate.Enabled = true;

            gcVehicle.Enabled = false;
        }
        protected override void PerformCancel()
        {
            ReloadData();
        }
        protected override void PerformSave()
        {
            Int64 PK;

            if (!IsInsert)
            {
                PK = ProcessGeneral.GetSafeInt(gvVehicle.GetRowCellValue(gvVehicle.FocusedRowHandle, "PK"));
            }
            else
            {
                PK = -1;
            }
            string name = ProcessGeneral.GetSafeString(txtName.EditValue);
            string registration = ProcessGeneral.GetSafeString(txtRegistration.EditValue);
            string note = ProcessGeneral.GetSafeString(txtNote.EditValue);
            string createdBy = ProcessGeneral.GetSafeString(txtCreatedBy.EditValue);
            string updatedBy = ProcessGeneral.GetSafeString(txtUpdatedBy.EditValue);
            DateTime createdDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtCreatedDate.EditValue));
            DateTime updatedDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtUpdatedDate.EditValue));

            DataTable dtResult = _inf.sp_Vehicle_Update(PK, name, registration, note, createdBy, createdDate, updatedBy, updatedDate);

            string msg = ProcessGeneral.GetSafeString(dtResult.Rows[0]["ErrMsg"]);
            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReloadData();
        }

        private void ReloadData()
        {
            ChangeMenuBar_Close();
            LoadText();
            gcVehicle.DataSource = _inf.sp_Vehicle_Select(-1);
            gvVehicle.BestFitColumns();
            gcVehicle.Enabled = true;
            IsInsert = false;
        }

        private void gcVehicle_Click(object sender, EventArgs e)
        {
            LoadText();
        }
    }
}
