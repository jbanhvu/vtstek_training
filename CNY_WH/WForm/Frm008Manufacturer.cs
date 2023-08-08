using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
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
using DevExpress.XtraGrid.Views.Base;

namespace CNY_WH.WForm
{
    public partial class Frm008Manufacturer : FrmBase
    {
        Inf_008Manufacturer inf;
        bool IsUpdate;
        public Frm008Manufacturer()
        {
            InitializeComponent();
            inf = new Inf_008Manufacturer();
            this.Load += Frm008Manufacturer_Load;
        }

        private void Frm008Manufacturer_Load(object sender, EventArgs e)
        {
            HiddenButton();
            LoadData();
            UnableText();
            gvManufacturer.FocusedRowChanged += GvManufacturer_FocusedRowChanged;
            GridViewCustomInit(gcManufacturer, gvManufacturer);
        }

        private void GvManufacturer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtName.Text = ProcessGeneral.GetSafeString(gvManufacturer.GetRowCellValue(gvManufacturer.FocusedRowHandle, "Name"));
            txtCode.Text = ProcessGeneral.GetSafeString(gvManufacturer.GetRowCellValue(gvManufacturer.FocusedRowHandle, "Code"));
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void EnableText()
        {
            txtName.Enabled = true;
            txtCode.Enabled = true;
        }
        public void UnableText()
        {
            txtName.Enabled = false;
            txtCode.Enabled = false;
        }
        public void HiddenText()
        {
            txtName.Text = "";
            txtCode.Text = "";
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
            IsUpdate = false;
            EnableText();
            HiddenText();
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
            UnableText();
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
            IsUpdate = true;
            EnableText();
        }
        protected override void PerformSave()
        {
            int PK = ProcessGeneral.GetSafeInt(gvManufacturer.GetRowCellValue(gvManufacturer.FocusedRowHandle, "PK"));
            string Name = txtName.Text;
            string Code = txtCode.Text;
            DataTable dt = new DataTable();
            if (IsUpdate)
            {
                dt = inf.sp_Manufacturer_update(PK, Name, Code);
            }
            else
            {
                dt = inf.sp_Manufacturer_insert(Name, Code);
            }
            string msg = ProcessGeneral.GetSafeString(dt.Rows[0]["ErrMsg"]);
            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            PerformCancel();
        }
        public void LoadData()
        {
            DataTable dt = inf.sp_Manufacturer_select();
            gcManufacturer.DataSource = dt;
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
