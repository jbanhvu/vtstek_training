using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CNY_BaseSys.Common
{
    public static class CheckboxHeaderCell
    {
   //     public RepositoryItemCheckEdit chkedit = new RepositoryItemCheckEdit();
        /// <summary>
        ///     Draw CheckBox Header Cell Column["Selected"]
        /// </summary>
        /// <param name="g" type="System.Drawing.Graphics">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="r" type="System.Drawing.Rectangle">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="Checked" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void DrawCheckBox(RepositoryItemCheckEdit chkedit, Graphics g, Rectangle r, bool Checked)
        {
            CheckEditViewInfo info;
            CheckEditPainter painter;
            ControlGraphicsInfoArgs args;
            info = chkedit.CreateViewInfo() as CheckEditViewInfo;
            painter = chkedit.CreatePainter() as CheckEditPainter;
            info.EditValue = Checked;

            info.Bounds = r;
            info.PaintAppearance.ForeColor = Color.Black;
            info.CalcViewInfo(g);
            args = new ControlGraphicsInfoArgs(info, new GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }

        /// <summary>
        ///     Get Count CheckBox Is Check 
        /// </summary>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetCheckedCount(GridView gv)
        {
            int count = 0;
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                if ((bool)gv.GetRowCellValue(i, gv.Columns["Selected"]))
                    count++;
            }
            return count;
        }
        /// <summary>
        ///      Set State Check In CheckBox For earch rows GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void CheckAll(GridView gv)
        {
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                gv.SetRowCellValue(i, gv.Columns["Selected"], true);
            }
        }

        /// <summary>
        ///      Set State Check In CheckBox For earch rows GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="backColor" type="System.Drawing.Color[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void CheckAll(GridView gv, Color[] backColor)
        {
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                GridCellInfo gColInfo = ProcessGeneral.GetGridCellInfo(gv, i, "Selected");
                Color cellBackColor = gColInfo.Appearance.BackColor;
                if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                {
                    gv.SetRowCellValue(i, gv.Columns["Selected"], true);
                }
              
            }
        }

        public static void CheckCell(GridView gv, Color[] backColor)
        {
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                GridCellInfo gColInfo = ProcessGeneral.GetGridCellInfo(gv, i, "Selected");
                Color cellBackColor = gColInfo.Appearance.BackColor;
                if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                {
                    bool value = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(i, "Selected"));
                    gv.SetRowCellValue(i, "Selected", !value);
                }
           
            }
        }

        /// <summary>
        ///      Set State UnCheck In CheckBox For earch rows GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void UnChekAll(GridView gv)
        {
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                gv.SetRowCellValue(i, gv.Columns["Selected"], false);
            }
        }

        /// <summary>
        ///      Set State UnCheck In CheckBox For earch rows GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="backColor" type="System.Drawing.Color[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void UnChekAll(GridView gv, Color[] backColor)
        {
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                GridCellInfo gColInfo = ProcessGeneral.GetGridCellInfo(gv, i, "Selected");
                Color cellBackColor = gColInfo.Appearance.BackColor;
                if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                {
                    gv.SetRowCellValue(i, "Selected", false);
                }

            }
        }
    }
}
