using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevSpSheet = DevExpress.XtraSpreadsheet;

namespace CNY_BaseSys.Common
{
    public class Cls001GenFunctions
    {
        public static void SplitLine(GridControl gc, bool bCopyDataLine, string[] sColField, string[] sColVal)
        {
            var gv = gc.FocusedView as GridView;
            var dtS = gc.DataSource as DataTable;
            DataRow dr = gv.GetDataRow(gv.FocusedRowHandle);
            DataRow dr1 = dtS.NewRow();

            if (bCopyDataLine)
            {
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    dr1[i] = dr[i];
                }
            }
            for (int i = 0; i < sColField.Length; i++)
            {
                dr1[sColField[i]] = sColVal[i];
            }

            dtS.Rows.InsertAt(dr1, gv.FocusedRowHandle + 1);
        }

        public static void SplitGridViewLine(GridView gv, DataTable dtS, bool bCopyDataLine, string[] sColField, string[] sColVal)
        {
            DataRow dr = gv.GetDataRow(gv.FocusedRowHandle);
            DataRow dr1 = dtS.NewRow();

            if (bCopyDataLine)
            {
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    dr1[i] = dr[i];
                }
            }
            for (int i = 0; i < sColField.Length; i++)
            {
                dr1[sColField[i]] = sColVal[i];
            }

            dtS.Rows.InsertAt(dr1, gv.FocusedRowHandle + 1);
        }

        public static void InitGridSelectCell(GridControl gc, GridView gv, bool fEditable = true,
            bool fReadonly = true, bool fMultiSel = true)
        {
            gc.UseEmbeddedNavigator = true;
            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Append.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gv.OptionsBehavior.Editable = fEditable;
            gv.OptionsBehavior.ReadOnly = fReadonly;
            gv.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gv.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            gv.OptionsCustomization.AllowColumnMoving = false;
            gv.OptionsCustomization.AllowQuickHideColumns = true;
            gv.OptionsCustomization.AllowSort = true;
            gv.OptionsCustomization.AllowFilter = true;

            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowIndicator = true;
            gv.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gv.OptionsView.ShowHorizontalLines = DefaultBoolean.True;

            gv.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gv.OptionsView.ShowAutoFilterRow = false;
            gv.OptionsView.AllowCellMerge = false;
            gv.HorzScrollVisibility = ScrollVisibility.Auto;
            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsNavigation.AutoFocusNewRow = true;
            gv.OptionsNavigation.UseTabKey = true;
            gv.FocusRectStyle = DrawFocusRectStyle.CellFocus;

            gv.OptionsSelection.MultiSelect = fMultiSel;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gv.OptionsView.EnableAppearanceEvenRow = true;
            gv.OptionsView.EnableAppearanceOddRow = true;
            gv.OptionsSelection.EnableAppearanceFocusedRow = true;
            gv.OptionsSelection.EnableAppearanceFocusedCell = true;

            gv.OptionsView.ShowFooter = false;
            gv.OptionsHint.ShowCellHints = false;

            gv.OptionsFind.AllowFindPanel = true;
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsFind.ShowCloseButton = true;
            gv.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gv)
            {
                AllowGroupBy = true,
                AllowSort = true,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
        }

        public static void GridViewCustomCols(GridView gv, DataTable dtGrid)
        {
            gv.Columns.Clear();
            for (int i = 0; i < dtGrid.Columns.Count; i++)
            {
                GridColumn gCol = new GridColumn();
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                gCol.AppearanceCell.Options.UseTextOptions = true;
                if (dtGrid.Columns[i].DataType == typeof(string))
                {
                    gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                }
                else if (dtGrid.Columns[i].DataType == typeof(DateTime))
                {
                    gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
                else
                {
                    gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                }

                string sCap = ProcessGeneral.GetSafeString(dtGrid.Columns[i].ColumnName);
                gv.OptionsView.AllowHtmlDrawHeaders = true;
                gCol.Caption = "<b>" + sCap + "</b>";

                gCol.FieldName = sCap;
                gCol.Name = sCap;
                gCol.Visible = true;
                gCol.VisibleIndex = i;
                gCol.Width = 100;
                gv.Columns.Add(gCol);
            }
            ShowGridViewIndicator(gv);
        }

        public static void ShowGridViewIndicator(GridView gridView)
        {
            gridView.RowCountChanged += gridView_RowCountChanged;
            gridView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
        }
        
        public static void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (e.Info.IsRowIndicator)
            {
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.ImageIndex = -1;
            }
        }

        public static void gridView_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
        }

        public static string ReverseString(string sString)
        {
            var reverseStr = new StringBuilder(sString.Length);
            string[] aStr = sString.Split(new char[] { '|' });
            int iCo = aStr.Count();

            for (int i = iCo - 1; i >= 0; i--)
            {
                reverseStr.Append(string.Format("{0},", aStr[i]));
            }

            string sResult = reverseStr.ToString();
            sResult = sResult.Length > 0 ? sResult.Substring(0, sResult.Length - 1) : sResult;
            return sResult;
        }

        public static void LoadGridDefault(GridControl gcDef, GridView gvDef, DataTable dtDef, string sCol)
        {
            if (dtDef == null || dtDef.Rows.Count == 0)
            {
                DataRow drStode = dtDef.NewRow();
                drStode[sCol] = "-1";
                dtDef.Rows.Add(drStode);
            }

            GridViewCustomCols(gvDef, dtDef);
            gcDef.DataSource = dtDef;
        }

        public static Color GetBackColorCellGridView(GridView gv, int rowHandle, string fieldName)
        {
            GridCellInfo cellInfo = ProcessGeneral.GetGridCellInfo(gv, rowHandle, fieldName);
            return cellInfo.Appearance.BackColor;
        }

        public static void DrawRectangleSelectiononGridView(GridControl gcDef, GridView gvDef)
        {
            gcDef.Paint += gcDef_Paint;
            gvDef.TopRowChanged += gvDef_TopRowChanged;
            gvDef.FocusedColumnChanged += gvDef_FocusedColumnChanged;
            gvDef.FocusedRowChanged += gvDef_FocusedRowChanged;
            gvDef.MouseMove += gvDef_MouseMove;
            gvDef.LeftCoordChanged += gvDef_LeftCoordChanged;
        }

        public static void gvDef_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView((GridView)sender);
        }

        public static void gvDef_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView((GridView)sender);
        }

        public static void gvDef_LeftCoordChanged(object sender, EventArgs e)
        {
            DrawRectangleSelection.RePaintGridView((GridView)sender);
        }

        public static void gvDef_MouseMove(object sender, MouseEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView((GridView)sender);
        }

        public static void gvDef_TopRowChanged(object sender, EventArgs e)
        {
            DrawRectangleSelection.RePaintGridView((GridView)sender);
        }

        public static void gcDef_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangleSelection.PaintGridViewSelectionRect((GridControl)sender, e);
        }

        #region Export SpreadSheet

        public static void SetSpreadSheetData(DevSpSheet.SpreadsheetControl spreadsheetControl1, DataTable dtWrep, string sTitle,
          string sConditionString, string sFromCol, string sToCol, bool fShowHeader = true, int iRow = 3, int iClearCol = 50)
        {
            spreadsheetControl1.Document.History.IsEnabled = false;
            spreadsheetControl1.BeginUpdate();
            Worksheet iWorkS = spreadsheetControl1.ActiveWorksheet;
            ClearSpreadSheet(iWorkS, iClearCol);
            iWorkS.Import(dtWrep, fShowHeader, iRow, 0);

            // Format Title
            Cell WCell = iWorkS.Cells["A1"];
            WCell.Value = sTitle;
            iWorkS.Range[string.Format("{0}1:{1}1", sFromCol, sToCol)].Merge(MergeCellsMode.ByRows);
            Style WStype = spreadsheetControl1.Document.Styles[BuiltInStyleId.Accent1_60percent];
            WCell.Style = WStype;
            WCell.Font.Name = "Tahoma";
            WCell.Font.Size = 22;
            WCell.Font.FontStyle = SpreadsheetFontStyle.Italic;
            WCell.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            WCell.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;

            // Criteria
            Cell WCell1 = iWorkS.Cells["A2"];
            WCell1.Value = sConditionString;
            iWorkS.Range[string.Format("{0}2:{1}2", sFromCol, sToCol)].Merge(MergeCellsMode.ByRows);
            WCell1.Font.Name = "Tahoma";
            WCell1.Font.Size = 16;
            WCell1.Font.FontStyle = SpreadsheetFontStyle.Italic;
            WCell1.Borders.BottomBorder.LineStyle = BorderLineStyle.Hair;
            WCell1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            WCell1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;

            if (fShowHeader)
            {
                CellRange WRange = iWorkS.Range[string.Format("{0}{2}:{1}{2}", sFromCol, sToCol, iRow + 1)];
                WRange.Style = spreadsheetControl1.Document.Styles[BuiltInStyleId.Accent1_20percent];
                WRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                WRange.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                WRange.Borders.TopBorder.LineStyle = BorderLineStyle.Double;
                WRange.Borders.BottomBorder.LineStyle = BorderLineStyle.Double;
            }

            spreadsheetControl1.EndUpdate();
            spreadsheetControl1.Document.History.IsEnabled = true;

            // Fit & Lock
            iWorkS.Columns.AutoFit(0, 400);
            iWorkS.FreezeRows(iRow);
        }

        public static void ClearSpreadSheet(Worksheet worksheet, int iToCol)
        {
            for (int i = iToCol; i >= 0; i--)
            {
                worksheet.Columns.Remove(i);

            }
        }

        public static void ExportSpSheettoExcel(DevSpSheet.SpreadsheetControl spSheet, DataTable dtHeader, DataTable dtDetails)
        {
            #region Load Data

            if (dtHeader != null)
            {
                WorksheetCollection iWCollection = spSheet.Document.Worksheets;
                DevExpress.Spreadsheet.Worksheet iWorkS = spSheet.Document.Worksheets[0];
                ClearSpreadSheet(iWorkS, 200);
                
                DevExpress.Spreadsheet.CellRange wRange = iWorkS.Range["A1:Z1"];
                wRange.Style = spSheet.Document.Styles[BuiltInStyleId.Accent1_20percent];
                wRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                wRange.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                wRange.Borders.TopBorder.LineStyle = BorderLineStyle.Double;
                wRange.Borders.BottomBorder.LineStyle = BorderLineStyle.Double;
                iWorkS.Import(dtHeader, true, 0, 0);

                if (dtDetails != null)
                {
                    DevExpress.Spreadsheet.CellRange wRangeD =
                        iWorkS.Range[string.Format("A{0}:Z{0}", dtHeader.Rows.Count + 3)];
                    wRangeD.Style = spSheet.Document.Styles[BuiltInStyleId.Accent1_20percent];
                    wRangeD.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                    wRangeD.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    wRangeD.Borders.TopBorder.LineStyle = BorderLineStyle.Double;
                    wRangeD.Borders.BottomBorder.LineStyle = BorderLineStyle.Double;
                    iWorkS.Import(dtDetails, true, dtHeader.Rows.Count + 2, 0);
                }
                iWorkS.Columns.AutoFit(0, 400);
            }

            #endregion

            #region Export Files

            var f = new SaveFileDialog()
            {
                Title = @"Export Data",
                Filter = @"Excel 2007,2010,2013 Files (*.xlsx)|*.xlsx|Excel 2003 files (*.xls)|*.xls|Pdf files (*.pdf)|*.pdf",
                RestoreDirectory = true
            };

            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.CheckFileExists)
                {
                    File.Delete(f.FileName);
                }

                string pathExport = f.FileName;
                switch (Path.GetExtension(pathExport).ToLower().Trim())
                {
                    case ".pdf":
                        spSheet.ExportToPdf(pathExport);
                        break;
                    case ".xlsx":
                        spSheet.SaveDocument(pathExport, DocumentFormat.Xlsx);
                        break;
                    case ".xls":
                        spSheet.SaveDocument(pathExport, DocumentFormat.Xls);
                        break;
                }

                XtraMessageBox.Show("Export data complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2);
            }

            #endregion
        }
        #endregion

        #region Export Grid

        public static void ExportGridViewtoExcel(GridView gView)
        {
            #region Export Files

            var f = new SaveFileDialog()
            {
                Title = @"Export Data",
                Filter = @"Excel 2007,2010,2013 Files (*.xlsx)|*.xlsx|Excel 2003 files (*.xls)|*.xls|Pdf files (*.pdf)|*.pdf",
                RestoreDirectory = true
            };

            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.CheckFileExists)
                {
                    File.Delete(f.FileName);
                }

                string pathExport = f.FileName;
                if (string.IsNullOrEmpty(pathExport))
                {
                    XtraMessageBox.Show("Invalid Path.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                switch (Path.GetExtension(pathExport).ToLower().Trim())
                {
                    case ".pdf":
                        gView.ExportToPdf(pathExport);
                        break;
                    case ".xlsx":
                        gView.ExportToXlsx(pathExport);
                        break;
                    case ".xls":
                        gView.ExportToXls(pathExport);
                        break;
                }

                XtraMessageBox.Show("Export data complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2);
            }

            #endregion
        }

        #endregion

        public static string GetColumnNameFrdt(DataTable dtSrc)
        {
            string sColName = "";
            foreach (DataColumn dcol in dtSrc.Columns)
            {
                sColName += dcol.ColumnName.Trim() + ',';
            }

            if (!string.IsNullOrEmpty(sColName))
                sColName = sColName.LeftString(sColName.Length - 1);
            return sColName;
        }

        public static string GetStrColumninDataTable(DataTable dtSrc, string sColumnName)
        {
            string strResult = string.Empty;
            if (dtSrc != null)
            {
                int iRows = dtSrc.Rows.Count;
                var ar = new string[iRows];
                for (int i = 0; i < iRows; i++)
                {
                    ar[i] = ProcessGeneral.GetSafeString(dtSrc.Rows[i][sColumnName]);
                }
                strResult = String.Join(",", ar.Distinct().Where(s => !String.IsNullOrWhiteSpace(s)));
            }
            return strResult;
        }
    }
}
