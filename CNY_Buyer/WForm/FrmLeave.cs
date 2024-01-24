using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Info;
using CNY_SI.Report;
using CNY_WH.Report;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
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
    public partial class FrmLeave : FrmBase
    {
        #region Properties
        private Inf_Leave _inf;
        private bool IsInsert;
        #endregion Properties

        #region Constructor
        public FrmLeave()
        {
            InitializeComponent();
            this.Load += FrmLeave_Load; ;
            _inf = new Inf_Leave();
        }

        #endregion Constructor

        #region Load Data
        private void FrmLeave_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            DeclareGridview();
            InitColumnGridview();
            ChangeMenuBar_Close();
            gcLeave.DataSource = _inf.sp_Leave_Select(-1);
            LoadDateTime();
            LoadDataToSlue();
            LoadText();
            gvLeave.BestFitColumns();
        }

        private void LoadDateTime()
        {
            DateTime fromDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "FromDate")));
            DateTime toDate = Convert.ToDateTime(ProcessGeneral.GetSafeString(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "ToDate")));

            deFromDate.EditValue = fromDate.Date;
            teFromDate.EditValue = fromDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            deToDate.EditValue = toDate.Date;
            teToDate.EditValue = toDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            deFromDate.Enabled = false;
            teFromDate.Enabled = false;
            teToDate.Enabled = false;
            deToDate.Enabled = false;
        }

        private void LoadDataToSlue()
        {
            slueStaff.Properties.DataSource = _inf.Excute("SELECT USERID [StaffCode],FullName [Name], DepartmentName [Department] FROM dbo.ListUser INNER JOIN ListDepartment ON ListUser.DepartmentCode = ListDepartment.DepartmentCode");
            slueStaff.Properties.DisplayMember = "StaffCode";
            slueStaff.Properties.ValueMember = "StaffCode";
            slueStaff.Properties.NullText = Convert.ToString(ProcessGeneral.GetSafeInt64(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "StaffPK")));
        }

        private void LoadText()
        {
            Int64 staffPK = ProcessGeneral.GetSafeInt64(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "StaffPK"));

            DataTable dt = _inf.Excute($"SELECT USERID [StaffCode],FullName [Name],DepartmentName [Department] FROM dbo.ListUser INNER JOIN ListDepartment ON ListUser.DepartmentCode = ListDepartment.DepartmentCode WHERE USERID = {staffPK}");
            txtStaffNameDesc.EditValue = dt.Rows[0]["Name"];
            txtDepartment.EditValue = dt.Rows[0]["Department"];

            slueStaff.EditValue = staffPK;
            txtNote.EditValue = ProcessGeneral.GetSafeString(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "Note"));

            txtDepartment.Enabled = false;
            txtStaffNameDesc.Enabled = false;
            txtNote.Enabled = false;
        }
        private void ReloadData()
        {
            ChangeMenuBar_Close();
            LoadText();
            LoadDateTime();
            gcLeave.DataSource = _inf.sp_Leave_Select(-1);
            gvLeave.BestFitColumns();
            gcLeave.Enabled = true;
            IsInsert = false;
        }
        #endregion Load Data

        #region Format

        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvLeave, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvLeave, HorzAlignment.Default, "Mã nhân viên", "StaffPK", 2);
            FormatGridView.CreateColumnOnGridview(gvLeave, HorzAlignment.Default, "Ngày nghỉ", "FromDate", 3);
            FormatGridView.CreateColumnOnGridview(gvLeave, HorzAlignment.Default, "Ngày đi làm lại", "ToDate", 4);
            FormatGridView.CreateColumnOnGridview(gvLeave, HorzAlignment.Default, "Ghi chú", "Note", 5);
        }

        private void DeclareGridview()
        {
            // gcLeave.ToolTipController = toolTipController1  ;

            gvLeave.OptionsCustomization.AllowColumnResizing = true;
            gvLeave.OptionsCustomization.AllowGroup = true;
            gvLeave.OptionsCustomization.AllowColumnMoving = true;
            gvLeave.OptionsCustomization.AllowQuickHideColumns = true;
            gvLeave.OptionsCustomization.AllowSort = true;
            gvLeave.OptionsCustomization.AllowFilter = true;

            gcLeave.UseEmbeddedNavigator = true;

            gcLeave.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcLeave.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcLeave.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcLeave.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcLeave.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvLeave.OptionsBehavior.Editable = false;
            gvLeave.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvLeave.OptionsHint.ShowCellHints = true;
            gvLeave.OptionsView.BestFitMaxRowCount = 1000;
            gvLeave.OptionsView.ShowGroupPanel = true;
            gvLeave.OptionsView.ShowIndicator = true;
            gvLeave.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvLeave.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvLeave.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvLeave.OptionsView.ShowAutoFilterRow = true;
            gvLeave.OptionsView.AllowCellMerge = false;
            gvLeave.HorzScrollVisibility = ScrollVisibility.Auto;
            gvLeave.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvLeave.OptionsNavigation.AutoFocusNewRow = true;
            gvLeave.OptionsNavigation.UseTabKey = true;

            gvLeave.OptionsSelection.MultiSelect = false;
            gvLeave.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvLeave.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvLeave.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvLeave.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvLeave.OptionsView.EnableAppearanceEvenRow = false;
            gvLeave.OptionsView.EnableAppearanceOddRow = false;

            gvLeave.OptionsView.ShowFooter = false;

            gvLeave.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvLeave.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvLeave.OptionsFind.AlwaysVisible = false;
            gvLeave.OptionsFind.ShowCloseButton = true;
            gvLeave.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvLeave)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvLeave.OptionsView.ShowGroupedColumns = true;
            gvLeave.OptionsPrint.AutoWidth = true;
            gvLeave.OptionsPrint.ShowPrintExportProgress = true;
            gvLeave.OptionsPrint.AllowMultilineHeaders = true;
            gvLeave.OptionsPrint.ExpandAllDetails = true;
            gvLeave.OptionsPrint.ExpandAllGroups = true;
            gvLeave.OptionsPrint.PrintDetails = true;
            gvLeave.OptionsPrint.PrintFooter = true;
            gvLeave.OptionsPrint.PrintGroupFooter = true;
            gvLeave.OptionsPrint.PrintHeader = true;
            gvLeave.OptionsPrint.PrintHorzLines = true;
            gvLeave.OptionsPrint.PrintVertLines = true;
            gvLeave.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvLeave.OptionsPrint.SplitDataCellAcrossPages = true;
            gvLeave.OptionsPrint.UsePrintStyles = false;
            gvLeave.OptionsPrint.AllowCancelPrintExport = true;
            gvLeave.OptionsPrint.AutoResetPrintDocument = true;

            gcLeave.ForceInitialize();
        }
        #endregion Format

        #region ChangeMenuBar
        private void ChangeMenuBar_Close()
        {
            AllowAdd = true;
            AllowEdit = true;
            AllowClose = true;
            AllowDelete = true;
            AllowCancel = false;
            AllowExpand = false;
            AllowGenerate = false;
            AllowRevision = false;
            AllowSave = false;
            AllowFind = false;
            AllowPrint = true;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            slueStaff.Enabled = false;
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
            slueStaff.Enabled = true;
            GenerateEvent();
        }
        #endregion ChangeMenuBar

        #region PerformButton
        protected override void PerformAdd()
        {
            ChangeMenuBar_Open();

            IsInsert = true;

            txtNote.EditValue = string.Empty;

            txtNote.Enabled = true;
            deToDate.Enabled = true;
            deFromDate.Enabled = true;
            teFromDate.Enabled = true;
            teToDate.Enabled = true;

            gcLeave.Enabled = false;

        }
        protected override void PerformEdit()
        {
            ChangeMenuBar_Open();
            slueStaff.Enabled= false;

            if (!gvLeave.IsDataRow(gvLeave.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //txtStaffPK.Enabled = true;
            txtNote.Enabled = true;
            deToDate.Enabled = true;
            deFromDate.Enabled = true;
            teFromDate.Enabled = true;
            teToDate.Enabled = true;

            gcLeave.Enabled = false;
        }
        protected override void PerformCancel()
        {
            ReloadData();
        }
        protected override void PerformDelete()
        {
            Int64 PK = ProcessGeneral.GetSafeInt(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "PK"));

            if (!gvLeave.IsDataRow(gvLeave.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform deleting", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _inf.Excute($"Delete from dbo.Leave where PK = {PK}");

            ReloadData();

        }
        protected override void PerformSave()
        {
            Int64 PK;

            if (!IsInsert)
            {
                PK = ProcessGeneral.GetSafeInt(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "PK"));
            }
            else
            {
                PK = -1;
            }
            Int64 staffPK = ProcessGeneral.GetSafeInt64(slueStaff.EditValue);
            string note = ProcessGeneral.GetSafeString(txtNote.EditValue);

            DateTime FromDate_Date = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(deFromDate.EditValue));
            DateTime FromDate_Time = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(teFromDate.EditValue));
            DateTime fromDate = new DateTime(FromDate_Date.Year, FromDate_Date.Month, FromDate_Date.Day, FromDate_Time.Hour, FromDate_Time.Minute, FromDate_Time.Second);

            DateTime ToDate_Date = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(deToDate.EditValue));
            DateTime ToTime_Time = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(teToDate.EditValue));
            DateTime toDate = new DateTime(ToDate_Date.Year, ToDate_Date.Month, ToDate_Date.Day, ToTime_Time.Hour, ToTime_Time.Minute, ToTime_Time.Second);

            DataTable dtResult = _inf.sp_Leave_Update(PK, staffPK, fromDate, toDate, note );

            string msg = ProcessGeneral.GetSafeString(dtResult.Rows[0]["ErrMsg"]);
            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReloadData();
        }
        protected override void PerformPrint()
        {
            Int64 PK = ProcessGeneral.GetSafeInt64(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "PK"));
            Int64 staffPK = ProcessGeneral.GetSafeInt64(gvLeave.GetRowCellValue(gvLeave.FocusedRowHandle, "StaffPK"));

            DataTable dt = _inf.sp_Leave_Select(PK);
            DataTable dtStaff = _inf.Excute($"SELECT USERID [StaffCode],FullName [Name],DepartmentName [Department] FROM dbo.ListUser INNER JOIN ListDepartment ON ListUser.DepartmentCode = ListDepartment.DepartmentCode WHERE USERID = {staffPK}");
            ReportPrintTool printTool = new ReportPrintTool(new RptGatePassPaper(dt, dtStaff));

            // Xuất báo cáo
            printTool.ShowPreview();
        }

        #endregion PerformButton

        #region Events
        public void GenerateEvent()
        {
            GenerateEventSearchlookupEdit();
        }

        private void GenerateEventSearchlookupEdit()
        {
            slueStaff.EditValueChanged += SlueStaff_EditValueChanged;
            slueStaff.Popup += SlueStaff_Popup;
        }

        private void SlueStaff_EditValueChanged(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            SetDescriptionText(slue);
        }

        private void SetDescriptionText(SearchLookUpEdit slue)
        {
            DataTable dtsource = slue.Properties.DataSource as DataTable;
            if (dtsource == null)
            {
                txtStaffNameDesc.EditValue = string.Empty;
                txtDepartment.EditValue = string.Empty;
                return;
            }
            var drv = slue.Properties.GetRowByKeyValue(slue.EditValue) as DataRowView;

            if (drv != null)
            {
                txtStaffNameDesc.EditValue = ProcessGeneral.GetSafeString(drv.Row["Name"]);
                txtDepartment.EditValue = ProcessGeneral.GetSafeString(drv.Row["Department"]);
            }
            else
            {
                txtStaffNameDesc.EditValue = string.Empty;
                txtDepartment.EditValue = string.Empty;
            }
        }

        private void SlueStaff_Popup(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            GridView a = slue.Properties.View;
            a.BestFitColumns();
        }

        private void gcLeave_Click_1(object sender, EventArgs e)
        {
            LoadText();
            LoadDateTime();
        }
        #endregion
    }
}
