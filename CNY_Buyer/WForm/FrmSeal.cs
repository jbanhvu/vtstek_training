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
using System.Xml.Linq;

namespace CNY_Buyer.WForm
{
    public partial class FrmSeal : FrmBase
    {
        Inf_Seal _inf;
        private bool IsInsert;
        public FrmSeal()
        {
            InitializeComponent();
            this.Load += FrmSeal_Load;
            _inf = new Inf_Seal();
        }

        private void FrmSeal_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DeclareGridview();
            InitColumnGridview();
            ChangeMenuBar_Close();
            gcSeal.DataSource = _inf.sp_Seal_Select(-1);
            gvSeal.BestFitColumns();
            LoadText();
        }

        private void LoadText()
        {
            txtSealDate.EditValue = ProcessGeneral.GetSafeDatetime(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "SealDate"));
            txtStaffPK.EditValue = ProcessGeneral.GetSafeString(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "StaffPK"));
            txtContent.EditValue = ProcessGeneral.GetSafeString(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "Content"));
            txtCreatedBy.EditValue = ProcessGeneral.GetSafeString(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "CreatedBy"));
            txtCreatedDate.EditValue = ProcessGeneral.GetSafeDatetime(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "CreatedDate"));
            txtUpdatedBy.EditValue = ProcessGeneral.GetSafeString(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "UpdatedBy"));
            txtUpdatedDate.EditValue = ProcessGeneral.GetSafeString(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "UpdatedDate"));

            txtSealDate.Enabled = false;
            txtContent.Enabled = false;
            txtCreatedBy.Enabled = false;
            txtStaffPK.Enabled = false;
            txtCreatedDate.Enabled = false;
            txtUpdatedBy.Enabled = false;
            txtUpdatedDate.Enabled = false;
        }

        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "Nhân viên", "StaffPK", 1);
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "Ngày đóng gói", "SealDate", 2);
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "Nội dung", "Content", 3);
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "Người tạo", "CreatedBy", 5);
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "Ngày tạo", "CreatedDate", 6);
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "Người cập nhật", "UpdatedBy", 7);
            FormatGridView.CreateColumnOnGridview(gvSeal, HorzAlignment.Default, "Ngày cập nhật", "UpdatedDate", 8);
        }
        private void DeclareGridview()
        {
            // gcSeal.ToolTipController = toolTipController1  ;
            gvSeal.OptionsCustomization.AllowColumnResizing = true;
            gvSeal.OptionsCustomization.AllowGroup = true;
            gvSeal.OptionsCustomization.AllowColumnMoving = true;
            gvSeal.OptionsCustomization.AllowQuickHideColumns = true;
            gvSeal.OptionsCustomization.AllowSort = true;
            gvSeal.OptionsCustomization.AllowFilter = true;

            gcSeal.UseEmbeddedNavigator = true;

            gcSeal.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcSeal.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcSeal.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcSeal.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcSeal.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvSeal.OptionsBehavior.Editable = false;
            gvSeal.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvSeal.OptionsHint.ShowCellHints = true;
            gvSeal.OptionsView.BestFitMaxRowCount = 1000;
            gvSeal.OptionsView.ShowGroupPanel = true;
            gvSeal.OptionsView.ShowIndicator = true;
            gvSeal.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvSeal.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvSeal.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvSeal.OptionsView.ShowAutoFilterRow = true;
            gvSeal.OptionsView.AllowCellMerge = false;
            gvSeal.HorzScrollVisibility = ScrollVisibility.Auto;
            gvSeal.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvSeal.OptionsNavigation.AutoFocusNewRow = true;
            gvSeal.OptionsNavigation.UseTabKey = true;

            gvSeal.OptionsSelection.MultiSelect = false;
            gvSeal.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvSeal.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvSeal.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvSeal.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvSeal.OptionsView.EnableAppearanceEvenRow = false;
            gvSeal.OptionsView.EnableAppearanceOddRow = false;

            gvSeal.OptionsView.ShowFooter = false;
            gvSeal.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;
            gvSeal.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvSeal.OptionsFind.AlwaysVisible = false;
            gvSeal.OptionsFind.ShowCloseButton = true;
            gvSeal.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvSeal)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvSeal.OptionsView.ShowGroupedColumns = true;
            gvSeal.OptionsPrint.AutoWidth = true;
            gvSeal.OptionsPrint.ShowPrintExportProgress = true;
            gvSeal.OptionsPrint.AllowMultilineHeaders = true;
            gvSeal.OptionsPrint.ExpandAllDetails = true;
            gvSeal.OptionsPrint.ExpandAllGroups = true;
            gvSeal.OptionsPrint.PrintDetails = true;
            gvSeal.OptionsPrint.PrintFooter = true;
            gvSeal.OptionsPrint.PrintGroupFooter = true;
            gvSeal.OptionsPrint.PrintHeader = true;
            gvSeal.OptionsPrint.PrintHorzLines = true;
            gvSeal.OptionsPrint.PrintVertLines = true;
            gvSeal.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvSeal.OptionsPrint.SplitDataCellAcrossPages = true;
            gvSeal.OptionsPrint.UsePrintStyles = false;
            gvSeal.OptionsPrint.AllowCancelPrintExport = true;
            gvSeal.OptionsPrint.AutoResetPrintDocument = true;

            gcSeal.ForceInitialize();
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

            txtSealDate.EditValue = string.Empty;
            txtContent.EditValue = string.Empty;
            txtStaffPK.EditValue = string.Empty;
            txtCreatedBy.EditValue = string.Empty;
            txtCreatedDate.EditValue = string.Empty;
            txtUpdatedBy.EditValue = string.Empty;
            txtUpdatedDate.EditValue = string.Empty;

            txtSealDate.Enabled = true;
            txtContent.Enabled = true;
            txtStaffPK.Enabled = true;
            txtCreatedBy.Enabled = true;
            txtCreatedDate.Enabled = true;
            txtUpdatedBy.Enabled = true;
            txtUpdatedDate.Enabled = true;

            gcSeal.Enabled = false;

        }
        protected override void PerformEdit()
        {
            ChangeMenuBar_Open();

            if (!gvSeal.IsDataRow(gvSeal.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtSealDate.Enabled = true;
            txtContent.Enabled = true;
            txtStaffPK.Enabled = true;
            txtCreatedBy.Enabled = true;
            txtCreatedDate.Enabled = true;
            txtUpdatedBy.Enabled = true;
            txtUpdatedDate.Enabled = true; 

            gcSeal.Enabled = false;
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
                PK = ProcessGeneral.GetSafeInt(gvSeal.GetRowCellValue(gvSeal.FocusedRowHandle, "PK"));
            }
            else
            {
                PK = -1;
            }
            DateTime sealDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtSealDate.EditValue));
            string content = ProcessGeneral.GetSafeString(txtContent.EditValue);
            Int64 staffPK = ProcessGeneral.GetSafeInt(txtStaffPK.EditValue);
            string createdBy = ProcessGeneral.GetSafeString(txtCreatedBy.EditValue);
            string updatedBy = ProcessGeneral.GetSafeString(txtUpdatedBy.EditValue);
            DateTime createdDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtCreatedDate.EditValue));
            DateTime updatedDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtUpdatedDate.EditValue));

            DataTable dtResult = _inf.sp_Seal_Update(PK, sealDate, staffPK, content, createdBy, createdDate, updatedBy, updatedDate);

            string msg = ProcessGeneral.GetSafeString(dtResult.Rows[0]["ErrMsg"]);
            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReloadData();
        }

        private void ReloadData()
        {
            ChangeMenuBar_Close();
            LoadText();
            gcSeal.DataSource = _inf.sp_Seal_Select(-1);
            gvSeal.BestFitColumns();
            gcSeal.Enabled = true;
            IsInsert = false;
        }

        private void gcSeal_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
    }
}
