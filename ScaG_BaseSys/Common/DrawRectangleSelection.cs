using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Windows.Forms;

namespace CNY_BaseSys.Common
{
    public static class DrawRectangleSelection
    {


        public static void PaintGridViewRowSelectionRect(GridControl control, PaintEventArgs e)
        {
            GridView view = control.FocusedView as GridView;
            if (view == null) return;
            GridViewInfo info = view.GetViewInfo() as GridViewInfo;
            if (info == null) return;

            if (view.SelectedRowsCount <= 0) return;

            List<GridCellInfo> collection = new List<GridCellInfo>();

            var qRow = view.GetSelectedRows();
            foreach (int  row in qRow)
            {
                foreach (GridColumn col in view.VisibleColumns)
                {
                    GridCellInfo gCellInfo = info.GetGridCellInfo(row, col);
                    collection.Add(gCellInfo);
                }

               
            }




          
            for (int i = 0; i <= collection.Count - 1; i++)
            {
                GridCellInfo cell = collection[i];
                GridCellInfo bottomCell = GetBottomCell(i, collection);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), GetSelectionRect(cell, bottomCell));
                i = collection.IndexOf(bottomCell);
            }

            //ProcessGeneral.GetGridCellInfo()

            // if (gv.IsRowVisible(rowHandle) == RowVisibleState.Hidden)
            // {
            //     gv.MakeRowVisible(rowHandle, false);
            // }
            // var vi = gv.GetViewInfo() as GridViewInfo;
            // if (vi == null) return null;
            // GridCellInfo gCellInfo = vi.GetGridCellInfo(rowHandle, gv.Columns[columnName.Trim()]);
            // vi.UpdateCellAppearance(gCellInfo);
            // return gCellInfo;
        }

        public static void PaintGridViewSelectionRect(GridControl control, PaintEventArgs e)
        {
            GridView view = control.FocusedView as GridView;
            if (view == null) return;
            GridViewInfo info = view.GetViewInfo() as GridViewInfo;
            if (info == null) return;
            GridCell[] gridCells = view.GetSelectedCells();
            if (gridCells.Length == 0) return;

            var visibleColl = info.RowsInfo.OfType<GridDataRowInfo>().SelectMany(row => row.Cells).ToList();

            var collection = (gridCells.SelectMany(cell => visibleColl, (cell, cellInfo) => new {cell, cellInfo})
                .Where(@t => @t.cellInfo.RowInfo != null)
                .Where(@t => @t.cellInfo.ColumnInfo != null)
                .Where(@t => @t.cell.RowHandle == @t.cellInfo.RowHandle)
                .Where(@t => @t.cell.Column.VisibleIndex == @t.cellInfo.Column.VisibleIndex)
                .Select(@t => @t.cellInfo)).ToList();


           

            if (collection.Count == 0) return;
            for (int i = 0; i <= collection.Count - 1; i++)
            {
                GridCellInfo cell = collection[i];
                GridCellInfo bottomCell = GetBottomCell(i, collection);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), GetSelectionRect(cell, bottomCell));
                i = collection.IndexOf(bottomCell);
            }
        }

        public static void RePaintGridView(GridView view)
        {
            view.Invalidate();
        }

        public static Rectangle GetSelectionRect(GridCellInfo topCell, GridCellInfo bottomCell)
        {
            int height = 0;
            int width = 0;
            Rectangle rTop = topCell.Bounds;
            Rectangle rBottom = bottomCell.Bounds;
            if (rTop.Y > rBottom.Y)
            {
                height = rTop.Y - rBottom.Bottom;
            }
            else
            {
                height = rBottom.Bottom - rTop.Y;
            }

            if (rTop.X <= rBottom.X)
            {
                width = rBottom.Right - rTop.X;
            }
            else
            {
                width = rTop.X - rBottom.Right;
            }
            return new Rectangle(rTop.X, rTop.Y, width, height);
        }

        public static GridCellInfo GetRightCell(int cellIndex, List<GridCellInfo> collection)
        {
            GridCellInfo cell = collection[cellIndex];
            for (int i = cellIndex; i <= collection.Count - 1; i++)
            {
                GridCellInfo nextCell = collection[i];
                if (cell.RowHandle == nextCell.RowHandle)
                {
                    if (cell.Column.VisibleIndex < nextCell.Column.VisibleIndex)
                    {
                        cell = nextCell;
                    }
                }
            }
            return cell;
        }

     
        public static GridCellInfo GetBottomCell(int cellIndex, List<GridCellInfo> collection)
        {

            GridCellInfo cell = collection[cellIndex];
            for (int i = cellIndex; i <= collection.Count - 1; i++)
            {
                if (i < collection.Count - 1)
                {
                    GridCellInfo nextCell = collection[i + 1];
                    if (Math.Abs(cell.RowHandle - nextCell.RowHandle) > 1)
                    {
                        return GetRightCell(i, collection);
                    }
                    cell = nextCell;
                }
            }
            return GetRightCell(collection.IndexOf(cell), collection);

        }
     

      
    }
}
