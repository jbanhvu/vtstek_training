using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;

namespace CNY_WH.WForm
{
    public partial class Frm009Survey : FrmBase
    {
        Inf_009Survey inf;
        bool Isupdate;
        public Frm009Survey()
        {
            InitializeComponent();
            inf = new Inf_009Survey();
            this.Load += Frm009Survey_Load;
        }

        private void Frm009Survey_Load(object sender, EventArgs e)
        {
            HiddenButton();
            LoadData();
            LoadDataSlueProject();
            LoadDataSlueCreated();
            LoadDataSlueUpdate();
            gvsurvey.FocusedRowChanged += Gvsurvey_FocusedRowChanged;
            EnableText();
            GridViewCustomInit(gcsurvey, gvsurvey);

        }

        private void Gvsurvey_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            searchProjectPK.EditValue = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Project_PK"));
            
            txtContent.Text = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Content"));
            deeStartAt.EditValue = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Start_At"));
            deeEndAt.EditValue = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "End_At"));
            txtCost.Text = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Cost"));
            txtStatus.Text = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Status"));
            searchCreated_By.EditValue = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Created_By"));
            deeCreated_Date.EditValue = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Created_Date"));
            searchUpdate_By.EditValue = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Updated_By"));
            deeUpdate_date.EditValue = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Updated_Date"));
            txtConclusion.Text = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Conclusion"));
            txtNote.Text = ProcessGeneral.GetSafeString(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "Note"));
        }

        public void LoadData()
        {
            DataTable dt = inf.sp_Survey_Select(0);
            gcsurvey.DataSource = dt;
        }
        public void LoadDataSlueProject()
        {
            DataTable dt = new DataTable();
            dt = inf.Excute("select PK, Project_Name from Project");
            searchProjectPK.Properties.DataSource = dt;
            searchProjectPK.Properties.ValueMember = "PK";
            searchProjectPK.Properties.DisplayMember = "Project_Name";
        }
        public void LoadDataSlueCreated()
        {
            DataTable dt = new DataTable();
            dt = inf.Excute("select UserName,FullName from ListUser");
            searchCreated_By.Properties.DataSource = dt;
            searchCreated_By.Properties.ValueMember = "UserName";
            searchCreated_By.Properties.DisplayMember = "FullName";

        }
        public void LoadDataSlueUpdate()
        {
            DataTable dt = new DataTable();
            dt = inf.Excute("select UserName,FullName from ListUser");
            searchUpdate_By.Properties.DataSource = dt;
            searchUpdate_By.Properties.ValueMember = "UserName";
            searchUpdate_By.Properties.DisplayMember = "FullName";

        }
        public void HiddenButton()
        {
            AllowAdd = true;
            AllowEdit = true;
            AllowDelete = true;
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
        }
        public void EnableText()
        {
            searchProjectPK.Enabled = false;
            txtContent.Enabled = false;
            txtConclusion.Enabled = false;
            deeStartAt.Enabled = false;
            deeEndAt.Enabled = false;
            txtCost.Enabled = false;
            txtStatus.Enabled = false;
            searchCreated_By.Enabled = false;
            searchUpdate_By.Enabled = false;
            deeCreated_Date.Enabled = false;
            deeUpdate_date.Enabled = false;
            txtNote.Enabled = false;
        }
        public void Unenabletext()
        {
            searchProjectPK.Enabled = true;
            txtContent.Enabled = true;
            txtConclusion.Enabled = true;
            deeStartAt.Enabled = true;
            deeEndAt.Enabled = true;
            txtCost.Enabled = true;
            txtStatus.Enabled = true;
            searchCreated_By.Enabled = true;
            searchUpdate_By.Enabled = true;
            deeCreated_Date.Enabled = true;
            deeUpdate_date.Enabled = true;
            txtNote.Enabled = true;
        }
        public void HiddenText()
        {
            searchProjectPK.EditValue = "";
            txtContent.Text = "";
            txtConclusion.Text = "";
            deeStartAt.EditValue = "";
            deeEndAt.EditValue = "";
            txtCost.Text = "";
            txtStatus.Text = "";
            searchCreated_By.EditValue = "";
            searchUpdate_By.EditValue = "";
            deeCreated_Date.EditValue = "";
            deeUpdate_date.EditValue = "";
        }
        protected override void PerformAdd()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowExpand = false;
            AllowGenerate = false;
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
            Isupdate = false;
            Unenabletext();
            HiddenText();
            searchProjectPK.Focus();
        }
        protected override void PerformCancel()
        {
            AllowAdd = true;
            AllowEdit = true;
            AllowDelete = true;
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
            EnableSave = false;
            AllowClose = true;
            EnableCancel = false;
            EnableText();
        }
        protected override void PerformEdit()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowCancel = true;
            AllowDelete = false;
            AllowExpand = false;
            AllowGenerate = false;
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
            Isupdate = true;
            Unenabletext();
            searchProjectPK.Focus();   
            
        }
        protected override void PerformSave()
        {
            int PK = ProcessGeneral.GetSafeInt(gvsurvey.GetRowCellValue(gvsurvey.FocusedRowHandle, "PK"));
            int ProjectPK = ProcessGeneral.GetSafeInt(searchProjectPK.EditValue);
            string content = txtContent.Text;
            DateTime Start_at = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(deeStartAt.EditValue);
            DateTime End_at = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(deeEndAt.EditValue);
            string conclusion = txtConclusion.Text;
            string cost = txtCost.Text;
            string status = txtStatus.Text;
            string Created_by = ProcessGeneral.GetSafeString(searchCreated_By.EditValue);
            string Update_by = ProcessGeneral.GetSafeString(searchUpdate_By.EditValue);
            DateTime Created_date = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(deeCreated_Date.EditValue);
            DateTime Updated_date = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(deeUpdate_date.EditValue);
            string Note = txtNote.Text;
            DataTable dt = new DataTable();
            if(Isupdate)
            {
                dt = inf.sp_Survey_Update(PK, ProjectPK, content, Start_at, End_at, conclusion, Note, Created_by, Created_date, Update_by, Updated_date);
            }
            else
            {
                dt = inf.sp_Survey_InSert(ProjectPK,content, Start_at, End_at, status, cost,conclusion, Created_by, Created_date,Update_by, Updated_date,Note);
            }
            string msg = ProcessGeneral.GetSafeString(dt.Rows[0]["ErrMsg"]);
            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            PerformCancel();

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void GridViewCustomInit(GridControl gcMain, GridView gvMain)
        {
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
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsView.BestFitMaxRowCount = 1000;
            gvMain.OptionsView.ShowGroupPanel = true;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = true;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;
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

            gvMain.OptionsFind.AllowFindPanel = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain)
            {
                AllowGroupBy = true,
                IsPerFormEvent = true,
            };

            gvMain.OptionsPrint.AutoWidth = false;
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
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;

            //gvMain.CustomColumnDisplayText += GvJobItem_CustomColumnDisplayText;
            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.EndGrouping += GvMain_EndGrouping;
            gcMain.ForceInitialize();
        }
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
            e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }
        private void GvMain_EndGrouping(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            gvP.BestFitColumns();
        }

        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;

            LinearGradientBrush backBrush;
            bool selected = gv.IsRowSelected(e.RowHandle);








            if (selected)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }

            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

            if (selected)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());






        }
}
}
