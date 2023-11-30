using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_Report.Properties;
using DevExpress.XtraEditors.Repository;

namespace CNY_Report.Common
{
    public static class FormatGridView
    {
        public static void Init4(GridControl gcMain, GridView gvMain)
        {
            gcMain.UseEmbeddedNavigator = true;
            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = false;
            gvMain.OptionsCustomization.AllowFilter = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;
            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsView.ShowFooter = false;
            gvMain.OptionsHint.ShowFooterHints = false;
            gvMain.OptionsHint.ShowCellHints = false;
            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            gvMain.OptionsFind.AllowFindPanel = false;
            gvMain.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvMain)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
        }
        public static void Init3(GridControl gcMain, GridView gvMain)
        {
            gcMain.UseEmbeddedNavigator = true;
            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = false;
            gvMain.OptionsCustomization.AllowFilter = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;
            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsView.ShowFooter = false;
            gvMain.OptionsHint.ShowFooterHints = false;
            gvMain.OptionsHint.ShowCellHints = false;
            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            gvMain.OptionsFind.AllowFindPanel = false;
            gvMain.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvMain)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
        }
        public static void Init2(GridControl gcMain, GridView gvMain)
        {
            gcMain.UseEmbeddedNavigator = true;
            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = true;
            gvMain.OptionsCustomization.AllowFilter = true;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false; gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.MultiSelect = false;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = true;
            gvMain.OptionsView.EnableAppearanceOddRow = true;
            gvMain.OptionsView.ShowFooter = false;
            gvMain.OptionsHint.ShowCellHints = false;
            gvMain.OptionsFind.AllowFindPanel = false;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = false;
            gvMain.OptionsFind.HighlightFindResults = false;
            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
            };
            gcMain.ForceInitialize();
        }
        public static void InitAE(GridControl gcMain, GridView gvMain)
        {
            gcMain.UseEmbeddedNavigator = true;
            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = false;
            gvMain.OptionsCustomization.AllowFilter = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;
            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsView.ShowFooter = false;
            gvMain.OptionsHint.ShowFooterHints = false;
            gvMain.OptionsHint.ShowCellHints = false;
            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            gvMain.OptionsFind.AllowFindPanel = false;
        }
        static void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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


            if (gv.IsRowSelected(e.RowHandle))
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
 
        public static void CreateColumnOnGridview(GridView gvMain, HorzAlignment HAlignment, string Caption, string FieldName, string Name, string Tooltip, Boolean Editable, int VisibleIndex)
        {
            var gridColumn10 = new GridColumn();
            gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn10.AppearanceHeader.TextOptions.HAlignment = HAlignment;
            gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            gridColumn10.AppearanceCell.TextOptions.HAlignment = HAlignment;
            gridColumn10.Caption = Caption;
            gridColumn10.FieldName = FieldName;
            gridColumn10.Name = Name;
            gridColumn10.ToolTip = Tooltip;
            gridColumn10.OptionsColumn.AllowEdit = Editable;
            gridColumn10.VisibleIndex = VisibleIndex;
            gvMain.Columns.Add(gridColumn10);
        }
        public static void CreateColumnOnGridview(GridView gvMain, HorzAlignment HAlignment, string Caption, string FieldName, string Name, string Tooltip, Boolean Editable, int VisibleIndex, Image image)
        {
            var gridColumn10 = new GridColumn();
            gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn10.AppearanceHeader.TextOptions.HAlignment = HAlignment;
            gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            gridColumn10.AppearanceCell.TextOptions.HAlignment = HAlignment;
            gridColumn10.Caption = Caption;
            gridColumn10.FieldName = FieldName;
            gridColumn10.Name = Name;
            gridColumn10.ToolTip = Tooltip;
            gridColumn10.OptionsColumn.AllowEdit = Editable;
            gridColumn10.VisibleIndex = VisibleIndex;
            gridColumn10.Image = image;
            gvMain.Columns.Add(gridColumn10);

        }
        public static void CreateColumnOnGridview(GridView gvMain, HorzAlignment HAlignment, string Caption, string FieldName, string Name, string Tooltip, Boolean Editable, int VisibleIndex, FormatType FormatType, string FormatString)
        {
            var gridColumn10 = new GridColumn();
            gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn10.AppearanceHeader.TextOptions.HAlignment = HAlignment;
            gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            gridColumn10.AppearanceCell.TextOptions.HAlignment = HAlignment;
            gridColumn10.Caption = Caption;
            gridColumn10.FieldName = FieldName;
            gridColumn10.Name = Name;
            gridColumn10.ToolTip = Tooltip;
            gridColumn10.OptionsColumn.AllowEdit = Editable;
            gridColumn10.VisibleIndex = VisibleIndex;
            gridColumn10.DisplayFormat.FormatType = FormatType;
            gridColumn10.DisplayFormat.FormatString = FormatString;
            gvMain.Columns.Add(gridColumn10);

        }
        public static void AddMaxLengthOnGridColumn(GridColumn gc, int maxLength)
        {
            RepositoryItemTextEdit rTE = new RepositoryItemTextEdit();
            rTE.MaxLength = maxLength;
            gc.ColumnEdit = rTE;
        }
        public static void DeleteAllColumn(GridView gvMain)
        {
            int ColumnCount = gvMain.Columns.Count;
            for (int i = ColumnCount - 1; i >= 0; i--)
            {
                gvMain.Columns.RemoveAt(i);
            }
        }
        public static void Init(GridControl gcMain, GridView gvMain)
        {
            gcMain.UseEmbeddedNavigator = true;
            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = true;
            gvMain.OptionsCustomization.AllowFilter = true;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = true;
            gvMain.OptionsView.EnableAppearanceOddRow = true;
            gvMain.OptionsView.ShowFooter = false;
            gvMain.OptionsHint.ShowCellHints = false;
            gvMain.OptionsFind.AllowFindPanel = true;
            gvMain.OptionsFind.AlwaysVisible = true;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            gvMain.OptionsFind.ShowFindButton = true;
            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
            };
            gcMain.ForceInitialize();

        }
    }
}
