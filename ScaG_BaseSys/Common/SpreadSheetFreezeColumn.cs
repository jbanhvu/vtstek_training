using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Commands;
using DevExpress.XtraSpreadsheet.Menu;
using DevExpress.XtraSpreadsheet.Services;
using CNY_BaseSys.Properties;

namespace CNY_BaseSys.Common
{
    public class SpreadSheetFreezeColumn
    {
        #region "Property"

        private readonly SpreadsheetControl _spreadSheet;
        private readonly IWorkbook _workbook;
        #endregion


        #region "Contructor"

        public SpreadSheetFreezeColumn(SpreadsheetControl spreadSheet)
        {
            _spreadSheet = spreadSheet;
            _workbook = _spreadSheet.Document;
            _spreadSheet.PopupMenuShowing += spreadSheet_PopupMenuShowing;
        }

        #endregion

        #region "SpreadSheett"
        private void spreadSheet_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == SpreadsheetMenuType.Cell)
            {
                // Remove the "Clear Contents" menu item.
                e.Menu.RemoveMenuItem(SpreadsheetCommandId.FormatClearContentsContextMenuItem);

                // Disable the "Hyperlink" menu item.
                e.Menu.DisableMenuItem(SpreadsheetCommandId.InsertHyperlinkContextMenuItem);

                // Create a menu item for the Spreadsheet command, which inserts a picture into a worksheet.
                //ISpreadsheetCommandFactoryService service = (ISpreadsheetCommandFactoryService)_spreadSheet.GetService(typeof(ISpreadsheetCommandFactoryService));
                //SpreadsheetCommand cmd = service.CreateCommand(SpreadsheetCommandId.InsertPicture);
                //SpreadsheetMenuItemCommandWinAdapter menuItemCommandAdapter = new SpreadsheetMenuItemCommandWinAdapter(cmd);
                //SpreadsheetMenuItem menuItem = (SpreadsheetMenuItem)menuItemCommandAdapter.CreateMenuItem(DevExpress.Utils.Menu.DXMenuItemPriority.Normal);
                //menuItem.Image = Resources.freeze_column_16x16;
                //menuItem.BeginGroup = true;
                //e.Menu.Items.Add(menuItem);

                // Insert a new item into the Spreadsheet popup menu and handle its click event.


                SpreadsheetMenuItem freezeMenu = new SpreadsheetMenuItem("Freeze Panes", FreezeMenu_Click)
                {
                    Image = Resources.papersize_16x16,
                    BeginGroup = true
                };
                e.Menu.Items.Add(freezeMenu);

                SpreadsheetMenuItem freezeRow = new SpreadsheetMenuItem("Freeze Top Row", FreezeRow_Click)
                {
                    Image = Resources.multiplepages_16x16,
                    BeginGroup = false
                };
                e.Menu.Items.Add(freezeRow);

                SpreadsheetMenuItem freezeCol = new SpreadsheetMenuItem("Freeze First Column", FreezeColumn_Click)
                {
                    Image = Resources.insertpagebreak_16x16,
                    BeginGroup = false
                };
                e.Menu.Items.Add(freezeCol);






                SpreadsheetMenuItem unfreezeMenu = new SpreadsheetMenuItem("Unfreeze Panes", UnFreezeMenu_Click)
                {
                    Image = Resources.pagemargins_16x16,
                    BeginGroup = false
                };
                e.Menu.Items.Add(unfreezeMenu);
            }
        }
        #endregion
 

        #region "Freeze Column"

        private void FreezeRow_Click(object sender, EventArgs e)
        {
            Worksheet worksheet = _workbook.Worksheets.ActiveWorksheet;

            // Access the cell range that is currently visible.
            CellRange visibleRange = _spreadSheet.VisibleRange;

            // Freeze the top visible row.
            worksheet.FreezeRows(0, visibleRange);
        }

        private void FreezeColumn_Click(object sender, EventArgs e)
        {
            Worksheet worksheet = _workbook.Worksheets.ActiveWorksheet;

            // Access the cell range that is currently visible.
            CellRange visibleRange = _spreadSheet.VisibleRange;

            // Freeze the first visible column.


            worksheet.FreezeColumns(0, visibleRange);
        }

        private void FreezeMenu_Click(object sender, EventArgs e)
        {
            //Access the active worksheet.
            Worksheet worksheet = _workbook.Worksheets.ActiveWorksheet;

            // Access the cell range that is currently visible.
            CellRange visibleRange = _spreadSheet.VisibleRange;

            // Access the active cell. 
            Cell activeCell = _spreadSheet.ActiveCell;

            int rowOffset = activeCell.RowIndex - visibleRange.TopRowIndex - 1;
            int columnOffset = activeCell.ColumnIndex - visibleRange.LeftColumnIndex - 1;

            // If the active cell is outside the visible range of cells, no rows and columns are frozen.
            if (!visibleRange.IsIntersecting(activeCell))
            {
                return;
            }

            if (activeCell.ColumnIndex == visibleRange.LeftColumnIndex)
            {
                // If the active cell matches the top left visible cell, no rows and columns are frozen.
                if (activeCell.RowIndex == visibleRange.TopRowIndex) { return; }
                else
                    // Freeze visible rows above the active cell if it is located in the leftmost visible column.
                    worksheet.FreezeRows(rowOffset, visibleRange);
            }

            else if (activeCell.RowIndex == visibleRange.TopRowIndex)
            {
                // Freeze visible columns to the left of the active cell if it is located in the topmost visible row.
                worksheet.FreezeColumns(columnOffset, visibleRange);
            }

            else
            {
                // Freeze both rows and columns above and to the left of the active cell.
                worksheet.FreezePanes(rowOffset, columnOffset, visibleRange);
            } 
        }


        private void UnFreezeMenu_Click(object sender, EventArgs e)
        {
            Worksheet worksheet = _workbook.Worksheets.ActiveWorksheet;
            // Unfreeze rows and columns.
            worksheet.UnfreezePanes();
        }
        #endregion
    }
}
