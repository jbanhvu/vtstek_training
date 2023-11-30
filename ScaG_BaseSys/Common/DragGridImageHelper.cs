using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.Utils.Drawing;


namespace CNY_BaseSys.Common
{
    public class DragGridImageHelper : GridPainter
    {
        private readonly DevExpress.XtraGrid.Views.Grid.GridView _view;

        public DragGridImageHelper(DevExpress.XtraGrid.Views.Grid.GridView view)
            : base(view)
        {
            _view = view;
        }

        public Cursor GetDragCursor(int rowHandle, Point e)
        {
            GridViewInfo info = (GridViewInfo) _view.GetViewInfo();
            if (info == null) return null;
            GridRowInfo rowInfo = info.GetGridRowInfo(rowHandle);
           
            Bitmap result = GetRowDragBitmap(rowHandle);
            Point offset = new Point(rowInfo.Bounds.X, e.Y - rowInfo.Bounds.Y);
            return CursorCreator.CreateCursor(result, offset);
        }

        public Bitmap GetRowDragBitmap(int rowHandle)
        {
            Bitmap bmpRow = null;
            GridViewInfo info = (GridViewInfo) _view.GetViewInfo();
            if (info == null) return null;
            Rectangle totalBounds = info.Bounds;
            GridRowInfo ri = info.GetGridRowInfo(rowHandle);
            Rectangle imageBounds = new Rectangle(new Point(0, 0), ri.Bounds.Size);
            try
            {
                var bmpView = new Bitmap(totalBounds.Width, totalBounds.Height);
                using (Graphics gView = Graphics.FromImage(bmpView))
                {
                    using (XtraBufferedGraphics grView = XtraBufferedGraphicsManager.Current.Allocate(gView, new Rectangle(Point.Empty, bmpView.Size)))
                    {
                        Color color = ri.Appearance.BackColor == Color.Transparent ? Color.White : ri.Appearance.BackColor;
                        grView.Graphics.Clear(color);
                        IntPtr handle = View.GridControl.Handle;
                        DXPaintEventArgs paintArgs = new DXPaintEventArgs(new PaintEventArgs(grView.Graphics, totalBounds), handle);
                        DevExpress.Utils.Drawing.GraphicsCache cache = new DevExpress.Utils.Drawing.GraphicsCache(paintArgs);
                        GridViewDrawArgs args = new GridViewDrawArgs(cache, info, totalBounds);
                        DrawRow(args, ri);
                        grView.Graphics.FillRectangle(args.Cache.GetSolidBrush(Color.Transparent), ri.Bounds);
                        grView.Render();
                        bmpRow = new Bitmap(ri.Bounds.Width, ri.Bounds.Height);
                        using (Graphics gRow = Graphics.FromImage(bmpRow))
                        {
                            using (XtraBufferedGraphics grRow = XtraBufferedGraphicsManager.Current.Allocate(gRow, new Rectangle(Point.Empty, bmpRow.Size)))
                            {
                                grRow.Graphics.Clear(color);
                                grRow.Graphics.DrawImage(bmpView, imageBounds, ri.Bounds, GraphicsUnit.Pixel);
                                grRow.Render();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return bmpRow;
        }



        public Cursor GetDragCursor(int[] arrRow, Point e)
        {
            if (arrRow.Length <= 0) return null;
            GridViewInfo info = (GridViewInfo)_view.GetViewInfo();
            if (info == null) return null;
            GridRowInfo rowInfo = info.GetGridRowInfo(arrRow[0]);

            Bitmap result = GetRowDragBitmap(arrRow);
            Point offset = new Point(rowInfo.Bounds.X, e.Y - rowInfo.Bounds.Y);
            return CursorCreator.CreateCursor(result, offset);
        }

        public Bitmap GetRowDragBitmap(int[] arrRow)
        {
            if (arrRow.Length <= 0) return null;
            Bitmap bmpRow = null;
            GridViewInfo info = (GridViewInfo) _view.GetViewInfo();
            if (info == null) return null;
            Rectangle totalBounds = info.Bounds;
            GridRowInfo ri = info.GetGridRowInfo(arrRow[0]);
            Rectangle imageBounds = new Rectangle(new Point(0, 0), ri.Bounds.Size);
            int imgHeight = imageBounds.Height;
            for (int i = 1; i < arrRow.Length; i++)
            {
                int height = info.GetGridRowInfo(arrRow[0]).Bounds.Height;
                imgHeight += height;
            }

            imageBounds.Height = imgHeight;
            try
            {
                var bmpView = new Bitmap(totalBounds.Width, totalBounds.Height);
                using (Graphics gView = Graphics.FromImage(bmpView))
                {
                    using (XtraBufferedGraphics grView = XtraBufferedGraphicsManager.Current.Allocate(gView, new Rectangle(Point.Empty, bmpView.Size)))
                    {
                        Color color = ri.Appearance.BackColor == Color.Transparent ? Color.White : ri.Appearance.BackColor;
                        grView.Graphics.Clear(color);
                        IntPtr handle = View.GridControl.Handle;
                        DXPaintEventArgs paintArgs = new DXPaintEventArgs(new PaintEventArgs(grView.Graphics, totalBounds), handle);
                        DevExpress.Utils.Drawing.GraphicsCache cache = new DevExpress.Utils.Drawing.GraphicsCache(paintArgs);
                        GridViewDrawArgs args = new GridViewDrawArgs(cache, info, totalBounds);
                        DrawRow(args, ri);
                        grView.Graphics.FillRectangle(args.Cache.GetSolidBrush(Color.Transparent), ri.Bounds);
                        grView.Render();
                        bmpRow = new Bitmap(ri.Bounds.Width, ri.Bounds.Height);
                        using (Graphics gRow = Graphics.FromImage(bmpRow))
                        {
                            using (XtraBufferedGraphics grRow = XtraBufferedGraphicsManager.Current.Allocate(gRow, new Rectangle(Point.Empty, bmpRow.Size)))
                            {
                                grRow.Graphics.Clear(color);
                                grRow.Graphics.DrawImage(bmpView, imageBounds, ri.Bounds, GraphicsUnit.Pixel);
                                grRow.Render();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return bmpRow;
        }
    }
}
