using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;
using System.Windows.Forms;

namespace CNY_BaseSys.Common
{
    public static class TreeListDrawRectangleSelection
    {
        public static void TreeListPaintSelectionRect(TreeList tl, PaintEventArgs e)
        {
          
            TreeListViewInfo info = tl.ViewInfo;
            if (info == null) return;
            var treeCells = tl.GetSelectedCells();
            if (treeCells.Count == 0) return;
            var visibleColl = info.RowsInfo.Rows.SelectMany(row => row.Cells).ToList();
            var collectionTemp = treeCells.SelectMany(cell => visibleColl, (cell, cellInfo) => new { cell, cellInfo })
              .Where(t => t.cellInfo.RowInfo != null && t.cellInfo.ColumnInfo != null && t.cellInfo.Column != null && t.cell.Node == t.cellInfo.RowInfo.Node && t.cell.Column.VisibleIndex == t.cellInfo.Column.VisibleIndex)
              .Select(t => new
              {
                  CellInfoFinal = t.cellInfo,
                  ColIndex = t.cellInfo.Column.VisibleIndex,
                  RowIndex = t.cellInfo.RowInfo.VisibleIndex,
              }).OrderBy(m=>m.RowIndex).ThenBy(m=>m.ColIndex).ToList();
            if (collectionTemp.Count == 0) return;
            var collection = collectionTemp.Select(p => p.CellInfoFinal).Distinct().ToList();

            var q1 = collectionTemp.Select(p => p.RowIndex).Distinct().ToArray();

            int count = q1.Length;
            if (count <= 1) goto finish;
            var q2 = q1.FindNumberLostInArray(1).ToList();
            if (q2.Count > 0) goto finish;

            CellInfo topCellInfo = collection.First();
            CellInfo bottomCellInfo = collection.Last();
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), TreeListGetSelectionRect(topCellInfo, bottomCellInfo));
            return;

        //t.cellInfo.Column

        finish:


            for (int i = 0; i <= collection.Count - 1; i++)
            {
                CellInfo cell = collection[i];
                CellInfo bottomCell = TreeListGetBottomCell(i, collection);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), TreeListGetSelectionRect(cell, bottomCell));
                i = collection.IndexOf(bottomCell);
            }
        }

        public static void TreeListRePaintRect(TreeList tl)
        {
            tl.Invalidate();
        }


        public static Rectangle TreeListGetSelectionRect(CellInfo topCell, CellInfo bottomCell)
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

        public static CellInfo TreeListGetRightCell(int cellIndex, List<CellInfo> collection)
        {
            CellInfo cell = collection[cellIndex];
            for (int i = cellIndex; i <= collection.Count - 1; i++)
            {
                CellInfo nextCell = collection[i];
                if (cell.RowInfo.VisibleIndex == nextCell.RowInfo.VisibleIndex)
                {
                    if (cell.Column.VisibleIndex < nextCell.Column.VisibleIndex)
                    {
                        cell = nextCell;
                    }
                }
            }
            return cell;
        }


        public static CellInfo TreeListGetBottomCell(int cellIndex, List<CellInfo> collection)
        {

            CellInfo cell = collection[cellIndex];
            for (int i = cellIndex; i <= collection.Count - 1; i++)
            {
                if (i < collection.Count - 1)
                {
                    CellInfo nextCell = collection[i + 1];
                    if (Math.Abs(cell.RowInfo.VisibleIndex - nextCell.RowInfo.VisibleIndex) > 1)
                    {
                        return TreeListGetRightCell(i, collection);
                    }
                    cell = nextCell;
                }
            }
            return TreeListGetRightCell(collection.IndexOf(cell), collection);

        }
    }
}
