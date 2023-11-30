using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Paint;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Card.Drawing;
using DevExpress.XtraGrid.Views.Card.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using CNY_BaseSys.Properties;
using DragObjectStartEventArgs = DevExpress.XtraGrid.Views.Base.DragObjectStartEventArgs;
using PopupMenuShowingEventArgs = DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs;

namespace CNY_BaseSys.Common
{
    
    public class MyFindPanelFilterHelperNew
    {
        #region "Property"
        private string _lastCriteria;
        private ExpressionEvaluator _lastEvaluator;
        private readonly FindVnGridView _view;
        private readonly bool _isAndSearchNew;
        private bool _isPerFormEvent = false;
        public bool IsPerFormEvent
        {
            set
            {
                if (value && !this._isPerFormEvent)
                {
                    GridControl gc = _view.GridControl;
                    Control parentControl = gc.GetParentContrl();

                    var barManagerMain = new BarManager
                    {
                        Form = parentControl,
                    };

                    gc.MenuManager = barManagerMain;


                    _view.CustomRowFilter += view_CustomRowFilter;
                    _view.PopupMenuShowing += view_PopupMenuShowing;
                    _view.ColumnWidthChanged += view_ColumnWidthChanged;
                    _view.CustomDrawColumnHeader += view_CustomDrawColumnHeader;
                    _view.MouseDown += view_MouseDown;

                    this._isPerFormEvent = true;
                }
            }
        }




        private bool _isDrawFilter = false;
        public bool IsDrawFilter
        {
            set
            {
                if (value && !this._isDrawFilter)
                {

                    _view.CustomDrawCell += view_CustomDrawCell;
                    this._isDrawFilter = true;
                }
            }
        }


        private bool _allowGroupBy = false;
        public bool AllowGroupBy
        {
            get { return this._allowGroupBy; }
            set
            {
                this._allowGroupBy = value;
            }
        }


        private bool _isBestFitDoubleClick = false;
        public bool IsBestFitDoubleClick
        {
            set
            {
                if (value && !this._isBestFitDoubleClick)
                {

                    _view.DoubleClick += view_DoubleClick;
                    _view.Click += view_Click;
                    _view.DragObjectStart += view_DragObjectStart;
                    this._isBestFitDoubleClick = true;
                }
            }
        }



        private bool _allowSort = false;
        public bool AllowSort
        {
            get { return this._allowSort; }
            set
            {
                this._allowSort = value;
            }
        }









        #endregion


        #region "Contructor"


        public MyFindPanelFilterHelperNew(FindVnGridView view, bool isAndSearchNew = false)
        {
            _view = view;
            _isAndSearchNew = isAndSearchNew;
            _view.FindPanelCreated += _view_FindPanelCreated;


        }

        









        #endregion


        #region "GridView Event"



        private void view_Click(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (gv.RowCount <= 0) return;
            GridControl gc = gv.GridControl;
            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
            if (!hi.InColumn || hi.InRow) return;
            GridColumn col = hi.Column;
            if (col == null) return;

            var q1 = gv.GetSelectedCells().Select(p => p.Column).Distinct().ToList();
            var q2 = q1.Any(p => p.FieldName == col.FieldName);
            gv.ClearSelection();

            int lastRow = gv.RowCount - 1;
            gv.FocusedRowHandle = 0;
            gv.FocusedColumn = col;
            if (q2)
            {
                if (q1.Any())
                {
                    gv.SelectCells(0, q1.First(), lastRow, q1.Last());
                }
                else
                {
                    gv.SelectCells(0, col, lastRow, col);
                }
            }
            else
            {
                gv.SelectCells(0, col, lastRow, col);
            }
        }


        private void view_DragObjectStart(object sender, DragObjectStartEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (_allowSort)
            {
                gv.OptionsCustomization.AllowSort = true;
            }
        }

        private void view_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (gv.RowCount <= 1) return;
            GridControl gc = gv.GridControl;
            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
            if (!hi.InColumn || hi.InRow) return;
            GridColumn col = hi.Column;
            if (col == null) return;
            if (!_allowSort) return;

            if (!gv.OptionsCustomization.AllowSort)
            {
                gv.OptionsCustomization.AllowSort = true;
            }
            if (col.OptionsColumn.AllowSort == DefaultBoolean.False) return;
            gv.BeginSort();
            ColumnSortOrder sortOrder;
            if (col.SortOrder == ColumnSortOrder.None)
            {
                sortOrder = ColumnSortOrder.Ascending;
            }
            else
            {
                sortOrder = col.SortOrder == ColumnSortOrder.Ascending
                    ? ColumnSortOrder.Descending
                    : ColumnSortOrder.Ascending;
            }
            gv.ClearSorting();
            col.SortOrder = sortOrder;
            gv.EndSort();
        }

        private void view_MouseDown(object sender, MouseEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (_allowSort)
            {
                BandedGridView bgv = gv as BandedGridView;
                if (bgv == null)
                {
                    GridHitInfo hi = gv.CalcHitInfo(e.Location);
                    if (hi.InColumn && e.Clicks == 1 && e.Button == MouseButtons.Left)
                    {
                        gv.OptionsCustomization.AllowSort = false;
                    }
                    if (hi.InColumn && e.Clicks == 2 && e.Button == MouseButtons.Left)
                    {
                        gv.OptionsCustomization.AllowSort = true;
                    }
                }
                else
                {
                    BandedGridHitInfo hiB = bgv.CalcHitInfo(e.Location);
                    if (hiB.InBandPanel)
                    {
                        if (e.Clicks == 1 && e.Button == MouseButtons.Left)
                        {
                            bgv.OptionsCustomization.AllowSort = false;
                        }
                        if (e.Clicks == 2 && e.Button == MouseButtons.Left)
                        {
                            bgv.OptionsCustomization.AllowSort = true;
                        }
                    }

                }

            }
            ProcessGeneral.CheckGridViewClick(e, gv);
        }
        /*
		public static bool CaseInsensitiveContainsAllSubStrings(string source, string subStr)
		{
			if (!string.IsNullOrWhiteSpace(subStr))
			{
				foreach (string sub in subStr.Split('+'))
				{
					if (AccentInsensitiveIndexOf(source, sub) < 0)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}
       
             
             */



        private void view_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {


            GridView gv = sender as GridView;
            if (gv == null) return;
            if (e.Column == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            GridCellInfo ci = e.Cell as GridCellInfo;
            if (ci == null) return;
            if (gv.OptionsFind.HighlightFindResults && !string.IsNullOrEmpty(gv.FindFilterText) && _isAndSearchNew)
            {




                string searchText = gv.FindFilterText.Trim().Replace("\"", "");

                string[] arrSearchText = gv.FindFilterText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Replace("\"", "").Trim()).ToArray();
                int searchCount = arrSearchText.Length;


                string cellText = e.DisplayText;
                int lengthDisp = cellText.Length;

                if (searchCount == 1)
                {


                    int res = cellText.AccentInsensitiveIndexOf(searchText);
                    if (res >= 0)
                    {
                        ci.ViewInfo.MatchedString = cellText.Substring(res, searchText.Length);
                    }
                }
                else

                {
                    if (lengthDisp < searchText.Length)
                        return;


                    string tmpStr = cellText;
                    int cutOutSymbolsCount = 0;
                    List<Indexes> searchIndexesList = new List<Indexes>();
                    foreach (string sSearchTemp in arrSearchText)
                    {

                        int resIndex = tmpStr.AccentInsensitiveIndexOf(sSearchTemp);

                        if (resIndex < 0) continue;
                        int sSearchTempLength = sSearchTemp.Length;

                        searchIndexesList.Add(new Indexes { Start = resIndex + cutOutSymbolsCount, End = resIndex + cutOutSymbolsCount + sSearchTempLength });
                        //  tmpStr = tmpStr.Remove(resIndex, sSearchTempLength);
                        tmpStr = tmpStr.Substring(resIndex + sSearchTempLength, tmpStr.Length - resIndex - sSearchTempLength);
                        cutOutSymbolsCount = cutOutSymbolsCount + sSearchTempLength + resIndex;
                    }
                    int countListIndex = searchIndexesList.Count;

                    if (countListIndex > 0)
                    {
                        e.Handled = true;
                        XPaint paint = new XPaint();
                        MultiColorDrawStringParams drawParams = new MultiColorDrawStringParams(e.Appearance);
                        drawParams.Bounds = e.Bounds;
                        drawParams.Text = cellText;

                        drawParams.Ranges = new CharacterRangeWithFormat[countListIndex * 2 + 1];
                        int currentStrIndex = 0;
                        int i = 0;
                        while (i < countListIndex)
                        {
                            Indexes ind = searchIndexesList[i];
                            int start = ind.Start;
                            int end = ind.End;
                            drawParams.Ranges[i * 2] = new CharacterRangeWithFormat(currentStrIndex, start - currentStrIndex, e.Appearance.ForeColor, e.Appearance.BackColor);
                            drawParams.Ranges[i * 2 + 1] = new CharacterRangeWithFormat(start, end - start, e.Appearance.ForeColor, System.Drawing.Color.FromArgb(255, 210, 0));
                            currentStrIndex = end;
                            i++;
                        }
                        drawParams.Ranges[i * 2] = new CharacterRangeWithFormat(currentStrIndex, lengthDisp - currentStrIndex, e.Appearance.ForeColor, e.Appearance.BackColor);
                        drawParams.Ranges = drawParams.Ranges.Where(val => val.Length != 0).ToArray();


                        paint.MultiColorDrawString(new DevExpress.Utils.Drawing.GraphicsCache(e.Graphics), drawParams);
                    }

                }



            }
            else
            {
                if (!gv.OptionsView.ShowAutoFilterRow) return;
                string filterCellText = gv.GetRowCellDisplayText(GridControl.AutoFilterRowHandle, e.Column);
                if (String.IsNullOrEmpty(filterCellText)) return;
                int filterTextIndex = e.DisplayText.IndexOf(filterCellText, StringComparison.CurrentCultureIgnoreCase);
                if (filterTextIndex == -1) return;
                XPaint.Graphics.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, filterCellText, e.Appearance, Color.Black, Color.Gold, false, filterTextIndex);
                e.Handled = true;
            }









        }

   

        private void view_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (!_isPerFormEvent) return;
            if (e.Column == null) return;
            var gv = sender as GridView;
            if (gv == null) return;

            GridColumn[] arrCol = gv.GetSelectedCells().Select(p => p.Column).ToArray();


            if (arrCol.Any(p => p == e.Column))
            {


                Rectangle rect = e.Bounds;
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
                Brush brush = e.Cache.GetGradientBrush(e.Bounds, Color.DarkTurquoise, Color.Azure, LinearGradientMode.Vertical);
                rect.Inflate(-2, -2);
                e.Graphics.FillRectangle(brush, rect);

                string caption = e.Info.Caption.Replace("<br>", "\n").Trim();
                if (caption.IndexOf("<b>", StringComparison.Ordinal) >= 0)
                {
                    e.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    e.Appearance.DrawString(e.Cache, caption.Replace("<b>", "").Replace("</b>", "").Trim(), e.Info.CaptionRect, e.Appearance.Font, e.Appearance.GetStringFormat());
                }
                else
                {
                    e.Appearance.DrawString(e.Cache, caption.Replace("<b>", "").Replace("</b>", "").Trim(), e.Info.CaptionRect);
                }
                foreach (DevExpress.Utils.Drawing.DrawElementInfo info in e.Info.InnerElements)
                {
                    if (!info.Visible) continue;
                    DevExpress.Utils.Drawing.ObjectPainter.DrawObject(e.Cache, info.ElementPainter,
                        info.ElementInfo);
                }
                e.Handled = true;
            }




        }

    


        private void view_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            if (!_isPerFormEvent) return;
            if (e.Column == null) return;
            var gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            int width = e.Column.Width;
            var q1 = gv.GetSelectedCells().Select(p => p.Column.FieldName).Distinct().ToList();
            if (q1.All(p => p != fieldName)) return;

            var q2 = q1.Where(p => p != fieldName).ToList();
            foreach (string s in q2)
            {
                gv.Columns[s].Width = width;
            }

        }

        private void view_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {

            if (!_isPerFormEvent) return;


            if (e.MenuType != GridMenuType.Column //&& e.MenuType != GridMenuType.User
                )
                return;
            //
            //&& e.MenuType != GridMenuType.User

            var menu = e.Menu as GridViewColumnMenu;
            if (menu == null) return;



            foreach (DXMenuItem item in e.Menu.Items)
            {

                if (item.Caption.Equals("Group By This Column", StringComparison.CurrentCultureIgnoreCase) ||
                    item.Caption.Equals("Show Group By Box", StringComparison.CurrentCultureIgnoreCase) ||
                    item.Caption.Equals("Hide This Column", StringComparison.CurrentCultureIgnoreCase))
                {
                    item.Enabled = _allowGroupBy;
                }
                else if (item.Caption.Equals("Clear All Sorting", StringComparison.CurrentCultureIgnoreCase) && !_allowSort)
                {
                    item.Enabled = false;
                }
            }




            var gv = sender as BandedGridView;



            if (gv == null)
            {
                if (menu.Column != null)
                {
                    menu.Items.Add(CreateCheckItem(null, menu.Column, null, FixedStyle.Left, Resources.freeze_column_16x16));
                }
            }
            else
            {

                BandedGridHitInfo hi = gv.CalcHitInfo(e.Point);
                if (hi != null && hi.InBandPanel)
                {
                    GridBand gBand = hi.Band;

                    if (gBand.Columns.Count > 0)
                    {
                        menu.Items.Add(CreateCheckItem(null, gBand.Columns[0], gBand, FixedStyle.Left, Resources.freeze_column_16x16));
                    }

                }

            }







        }

        private void view_CustomRowFilter(object sender, RowFilterEventArgs e)
        {
            if (!_isPerFormEvent) return;
            string findText = _view.FindFilterText.ToLower();
            if (string.IsNullOrEmpty(findText))
                return;

            bool findKhongDau = !findText.CheckVNStringInput();
            DataView dv = findKhongDau ? _dvVn : _dv;

            CriteriaOperator criteria = FilterCriteriaHelper.ReplaceFindPanelCriteria(_view.DataController.FilterCriteria, _view, GetFindPanelCriteria(_isAndSearchNew), _isAndSearchNew);
            ExpressionEvaluator evaluator = GetExpressionEvaluator(criteria,dv);
            e.Handled = true;
            e.Visible = evaluator.Fit(dv[e.ListSourceRow]);
        }


        private void _view_FindPanelCreated(object sender, FindPanelCreatedEventArgs e)
        {
            GetDataSearch();
        }
        private DataView _dv = null;
        private DataView _dvVn = null;
        private void GetDataSearch()
        {
          
            GridControl gc = _view.GridControl;
            DataTable dtFt = gc.DataSource as DataTable;


            if (dtFt == null || dtFt.Rows.Count <= 0)
            {
                _dv = null;
                _dvVn = null;
                return;
            }
            _dv = dtFt.DefaultView;
            var lColVn = dtFt.Columns.Cast<DataColumn>().Where(p => p.DataType == typeof(string)).Select(p => p.ColumnName).ToList();
            if (lColVn.Count > 0)
            {
                DataTable dtTest = dtFt.AsEnumerable().CopyToDataTable();
                foreach (DataRow drTest in dtTest.Rows)
                {
                    foreach (string col in lColVn)
                    {
                        drTest[col] = ProcessGeneral.GetSafeString(drTest[col]).ConvertVNToNormalStr();
                    }
                }
                dtTest.AcceptChanges();
                _dvVn = dtTest.DefaultView;
            }
            else
            {
                _dvVn = dtFt.DefaultView;
            }
        }
        #endregion



        #region "Menu Item Click"


        private void OnFixedClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            if (item == null) return;
            GridControl grid = _view.GridControl;
            if (grid == null) return;
            grid.Update();
            MenuInfo info = item.Tag as MenuInfo;
            if (info == null) return;


            if (!info.Checked)
            {
                for (int i = 0; i < _view.VisibleColumns.Count; i++)
                {
                    if (_view.VisibleColumns[i].FieldName == info.Column.FieldName)
                    {
                        _view.VisibleColumns[i].Fixed = FixedStyle.Left;
                        break;
                    }
                    _view.VisibleColumns[i].Fixed = FixedStyle.Left;
                }
            }
            else if (info.Checked)
            {
                for (int i = _view.VisibleColumns.Count - 1; i >= 0; i--)
                    _view.VisibleColumns[i].Fixed = FixedStyle.None;
            }
        }


     


        private DXMenuCheckItem CreateCheckItem(string caption, GridColumn column, GridBand band, FixedStyle style, Image image)
        {
            bool check;
            if (band == null)
            {
                check = column.Fixed == style;
            }
            else
            {
                check = band.Fixed == style;
            }





            DXMenuCheckItem item = new DXMenuCheckItem(caption, check, image, OnFixedClick);
            item.Tag = new MenuInfo(column, style, item.Checked);
            item.Caption = item.Checked ? "Unfreeze Columns" : "Freeze Columns";

            return item;
        }

        #endregion


        #region "Process Filter"

        private ExpressionEvaluator GetExpressionEvaluator(CriteriaOperator criteria, DataView dv)
        {
            if (criteria.ToString() == _lastCriteria)
                return _lastEvaluator;
            _lastCriteria = criteria.ToString();
            PropertyDescriptorCollection pdc = ((ITypedList)dv).GetItemProperties(null);

            _lastEvaluator = new ExpressionEvaluator(pdc, criteria, false);
            return _lastEvaluator;
        }

        private CriteriaOperator GetFindPanelCriteria(bool isAndSearchNew)
        {
            return FilterCriteriaHelper.MyConvertFindPanelTextToCriteriaOperator(_view, isAndSearchNew);
        }

        #endregion




    }
}
