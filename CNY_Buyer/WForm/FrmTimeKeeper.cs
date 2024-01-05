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
    public partial class FrmTimeKeeper : FrmBase
    {
        Inf_TimeKeeper _inf;
        public FrmTimeKeeper()
        {
            InitializeComponent();
            this.Load += FrmTimeKeeper_Load;
            _inf = new Inf_TimeKeeper();
        }

        private void FrmTimeKeeper_Load(object sender, EventArgs e)
        {
            HidenButton();
            LoadData();
        }

        private void LoadData()
        {
            DeclareGridview();
            InitColumnGridview();
            gcTimeKeeper.DataSource = _inf.sp_TimeKeeper_Select_1(-1);
            gvTimeKeeper.BestFitColumns();
            TextLoad();
        }

        private void TextLoad()
        {
            txtEncrollNumber.EditValue = ProcessGeneral.GetSafeInt64(gvTimeKeeper.GetRowCellValue(gvTimeKeeper.FocusedRowHandle, "EncrollNumber"));
            txtRecord.EditValue = ProcessGeneral.GetSafeDatetime(gvTimeKeeper.GetRowCellValue(gvTimeKeeper.FocusedRowHandle, "Record"));
            txtDateTime.EditValue = ProcessGeneral.GetSafeDatetime(gvTimeKeeper.GetRowCellValue(gvTimeKeeper.FocusedRowHandle, "InsertDateTime"));

            txtEncrollNumber.Enabled = false;
            txtRecord.Enabled = false;
            txtDateTime.Enabled = false;
        }

        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvTimeKeeper, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvTimeKeeper, HorzAlignment.Default, "EncrollNumber", "EncrollNumber", 2);
            FormatGridView.CreateColumnOnGridview(gvTimeKeeper, HorzAlignment.Default, "Record", "Record", 3);
            FormatGridView.CreateColumnOnGridview(gvTimeKeeper, HorzAlignment.Default, "InsertDateTime", "InsertDateTime", 4);
        }

        private void DeclareGridview()
        {
            // gcTimeKeeper.ToolTipController = toolTipController1  ;

            gvTimeKeeper.OptionsCustomization.AllowColumnResizing = true;
            gvTimeKeeper.OptionsCustomization.AllowGroup = true;
            gvTimeKeeper.OptionsCustomization.AllowColumnMoving = true;
            gvTimeKeeper.OptionsCustomization.AllowQuickHideColumns = true;
            gvTimeKeeper.OptionsCustomization.AllowSort = true;
            gvTimeKeeper.OptionsCustomization.AllowFilter = true;

            gcTimeKeeper.UseEmbeddedNavigator = true;

            gcTimeKeeper.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcTimeKeeper.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcTimeKeeper.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcTimeKeeper.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcTimeKeeper.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvTimeKeeper.OptionsBehavior.Editable = false;
            gvTimeKeeper.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvTimeKeeper.OptionsHint.ShowCellHints = true;
            gvTimeKeeper.OptionsView.BestFitMaxRowCount = 1000;
            gvTimeKeeper.OptionsView.ShowGroupPanel = true;
            gvTimeKeeper.OptionsView.ShowIndicator = true;
            gvTimeKeeper.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvTimeKeeper.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvTimeKeeper.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvTimeKeeper.OptionsView.ShowAutoFilterRow = true;
            gvTimeKeeper.OptionsView.AllowCellMerge = false;
            gvTimeKeeper.HorzScrollVisibility = ScrollVisibility.Auto;
            gvTimeKeeper.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvTimeKeeper.OptionsNavigation.AutoFocusNewRow = true;
            gvTimeKeeper.OptionsNavigation.UseTabKey = true;

            gvTimeKeeper.OptionsSelection.MultiSelect = false;
            gvTimeKeeper.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvTimeKeeper.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvTimeKeeper.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvTimeKeeper.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvTimeKeeper.OptionsView.EnableAppearanceEvenRow = false;
            gvTimeKeeper.OptionsView.EnableAppearanceOddRow = false;

            gvTimeKeeper.OptionsView.ShowFooter = false;

            gvTimeKeeper.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvTimeKeeper.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvTimeKeeper.OptionsFind.AlwaysVisible = false;
            gvTimeKeeper.OptionsFind.ShowCloseButton = true;
            gvTimeKeeper.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvTimeKeeper)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvTimeKeeper.OptionsView.ShowGroupedColumns = true;
            gvTimeKeeper.OptionsPrint.AutoWidth = true;
            gvTimeKeeper.OptionsPrint.ShowPrintExportProgress = true;
            gvTimeKeeper.OptionsPrint.AllowMultilineHeaders = true;
            gvTimeKeeper.OptionsPrint.ExpandAllDetails = true;
            gvTimeKeeper.OptionsPrint.ExpandAllGroups = true;
            gvTimeKeeper.OptionsPrint.PrintDetails = true;
            gvTimeKeeper.OptionsPrint.PrintFooter = true;
            gvTimeKeeper.OptionsPrint.PrintGroupFooter = true;
            gvTimeKeeper.OptionsPrint.PrintHeader = true;
            gvTimeKeeper.OptionsPrint.PrintHorzLines = true;
            gvTimeKeeper.OptionsPrint.PrintVertLines = true;
            gvTimeKeeper.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvTimeKeeper.OptionsPrint.SplitDataCellAcrossPages = true;
            gvTimeKeeper.OptionsPrint.UsePrintStyles = false;
            gvTimeKeeper.OptionsPrint.AllowCancelPrintExport = true;
            gvTimeKeeper.OptionsPrint.AutoResetPrintDocument = true;

            gcTimeKeeper.ForceInitialize();
        }
        private void HidenButton()
        {
            AllowAdd = false;
            AllowEdit = true;
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
        private void gcTimeKeeper_Click_1(object sender, EventArgs e)
        {
            TextLoad();
        }
        protected override void PerformEdit()
        {

            if (!gvTimeKeeper.IsDataRow(gvTimeKeeper.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtEncrollNumber.Enabled = true;
            txtRecord.Enabled = true;
            txtDateTime.Enabled = true;

            gcTimeKeeper.Enabled = false;
        }
        protected override void PerformSave()
        {
            Int64 PK = ProcessGeneral.GetSafeInt(gvTimeKeeper.GetRowCellValue(gvTimeKeeper.FocusedRowHandle, "PK"));
            Int64 encrollNumber = ProcessGeneral.GetSafeInt64(txtEncrollNumber.EditValue);
            DateTime record = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtRecord.EditValue));
            DateTime insertDateTime = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtDateTime.EditValue));

            DataTable dtResult = _inf.sp_TimeKeeper_Update(PK, encrollNumber, record, insertDateTime);

            string msg = ProcessGeneral.GetSafeString(dtResult.Rows[0]["ErrMsg"]);
            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReloadData();
        }

        private void ReloadData()
        {
            TextLoad();
            gcTimeKeeper.DataSource = _inf.sp_TimeKeeper_Select_1(-1);
            gvTimeKeeper.BestFitColumns();
            gcTimeKeeper.Enabled = true;
        }
    }
}
