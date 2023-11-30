using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Paint;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Menu;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using CNY_BaseSys.Properties;
using System.Data;
using CNY_BaseSys.Class;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using FocusedColumnChangedEventArgs = DevExpress.XtraTreeList.FocusedColumnChangedEventArgs;

namespace CNY_BaseSys.Common
{
    public class TreeListMultiCellSelector
    {
        //------

        #region "Property"
        public event GetFixedColumnFieldNameHandler OnGetFixedColumn = null;
        private readonly TreeList _treeList;
     
   
        public TreeList TreeListMain
        {
            get { return _treeList; }
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

        private Int32 _increaseIdicatorWidth = 10;
        public Int32 IncreaseIdicatorWidth
        {
            get { return this._increaseIdicatorWidth; }
            set
            {
                this._increaseIdicatorWidth = value;
            }
        }


        private bool _isDoubleStringIdicator = false;
        public bool IsDoubleStringIdicator
        {
            get { return this._isDoubleStringIdicator; }
            set
            {
                this._isDoubleStringIdicator = value;
            }
        }
        private bool _filterShowChild = false;
        public bool FilterShowChild
        {
            get { return this._filterShowChild; }
            set
            {
                this._filterShowChild = value;
            }
        }
        private string _lastCriteria;
        private ExpressionEvaluator _lastEvaluator;
        private readonly bool _isAndSearchNew;




        private bool _showAutoFilerMenu = false;
        public bool ShowAutoFilerMenu
        {
            get { return this._showAutoFilerMenu; }
            set
            {
                this._showAutoFilerMenu = value;
            }
        }
        private bool _allowColumnsChooser = false;
        public bool AllowColumnsChooser
        {
            get { return this._allowColumnsChooser; }
            set
            {
                this._allowColumnsChooser = value;
            }
        }

      




        #endregion


        #region "Contructor"
        public TreeListMultiCellSelector(TreeList treeList, bool isAndSearchNew = false)
        {
            
            _isAndSearchNew = isAndSearchNew;
            _treeList = treeList;


            _treeList.OptionsMenu.EnableNodeMenu = false;

            //   _treeList.AllowDrop = false;
            //  _treeList.OptionsBehavior.Editable = true;

            //   _treeList.OptionsPrint.PrintFilledTreeIndent = true;
            //    _treeList.OptionsPrint.PrintTree = true;

            //_treeList.OptionsPrint.AllowCancelPrintExport = true;
            //_treeList.OptionsPrint.AutoRowHeight = true;
            //_treeList.OptionsPrint.AutoWidth = false;
            //_treeList.OptionsPrint.PrintAllNodes = true;
            //_treeList.OptionsPrint.PrintBandHeader = true;
            //_treeList.OptionsPrint.PrintHorzLines = true;
            //_treeList.OptionsPrint.PrintImages = true;
            //_treeList.OptionsPrint.PrintVertLines = true;
            //_treeList.OptionsPrint.UsePrintStyles = true;


            //_treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            //_treeList.OptionsBehavior.AutoNodeHeight = true;
            //_treeList.OptionsDragAndDrop.CanCloneNodesOnDrop = true;
            //_treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            //_treeList.OptionsBehavior.KeepSelectedOnClick = true;
            //_treeList.OptionsBehavior.ShowEditorOnMouseUp = false;
            //_treeList.OptionsNavigation.MoveOnEdit = false;
            //_treeList.OptionsBehavior.ImmediateEditor = false;
            //_treeList.OptionsBehavior.AllowExpandOnDblClick = false;

            //_treeList.OptionsFilter.AllowFilterEditor = true;
            //_treeList.OptionsBehavior.EnableFiltering = true;
            //_treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            //_treeList.OptionsFind.AllowFindPanel = true;
            //_treeList.OptionsFind.HighlightFindResults = true;

            _treeList.OptionsClipboard.AllowCopy = DefaultBoolean.True;
            _treeList.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            _treeList.OptionsClipboard.CopyCollapsedData = DefaultBoolean.False;

            _treeList.OptionsSelection.MultiSelect = true;
            _treeList.OptionsSelection.MultiSelectMode = TreeListMultiSelectMode.CellSelect;
            _treeList.OptionsSelection.SelectNodesOnRightClick = false;
            _treeList.OptionsSelection.EnableAppearanceFocusedCell = true;
            _treeList.OptionsSelection.EnableAppearanceFocusedRow = true;

            //_treeList.OptionsView.AllowBandColumnsMultiRow = true;
            //_treeList.OptionsView.AutoWidth = false;
            //_treeList.OptionsView.EnableAppearanceEvenRow = false;
            //_treeList.OptionsView.EnableAppearanceOddRow = false;
            //_treeList.OptionsView.AllowHtmlDrawHeaders = true;
            //_treeList.OptionsCustomization.AllowBandMoving = false;
            //_treeList.OptionsCustomization.AllowColumnMoving = false;







            Control parentControl = _treeList.GetParentContrl();

            var barManagerMain = new BarManager
            {
                Form = parentControl,
            };

            _treeList.MenuManager = barManagerMain;

       
         

         


            _treeList.FocusedColumnChanged += treeList_FocusedColumnChanged;
            _treeList.FocusedNodeChanged += treeList_FocusedNodeChanged;
            _treeList.MouseMove += treeList_MouseMove;
            _treeList.BeforeExpand += treeList_BeforeExpand;
            _treeList.BeforeCollapse += treeList_BeforeCollapse;
            _treeList.CustomDrawNodeCell += treeList_CustomDrawNodeCell;

            _treeList.PopupMenuShowing += treeList_PopupMenuShowing;
            _treeList.ColumnWidthChanged += treeList_ColumnWidthChanged;
            _treeList.CustomDrawColumnHeader += treeList_CustomDrawColumnHeader;
            _treeList.DoubleClick += treeList_DoubleClick;

            _treeList.LeftCoordChanged += treeList_LeftCoordChanged;
            _treeList.TopVisibleNodeIndexChanged += treeList_TopVisibleNodeIndexChanged;


            _treeList.Paint += treeList_Paint;
            _treeList.NodesReloaded += treeList_NodesReloaded;
            _treeList.Click += treeList_Click;
            _treeList.MouseDown += treeList_MouseDown;
            _treeList.FilterNode += treeList_FilterNode;

     


        }




     
















        #endregion




        #region "Filter Node"
      

    
        private void treeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
      
            //  TreeListCell

            if (tl.OptionsFind.HighlightFindResults && !string.IsNullOrEmpty(tl.FindFilterText) && _isAndSearchNew)
            {




                string searchText = tl.FindFilterText.Trim().Replace("\"", "");

                string[] arrSearchText = tl.FindFilterText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Replace("\"", "").Trim()).ToArray();
                int searchCount = arrSearchText.Length;


                string cellText = e.CellText;
                int lengthDisp = cellText.Length;

                if (searchCount == 1)
                {


                    int res = cellText.AccentInsensitiveIndexOf(searchText);
                    if (res >= 0)
                    {

                        e.EditViewInfo.MatchedString = cellText.Substring(res, searchText.Length);
                    }
                }
                else

                {
                    if (lengthDisp >= searchText.Length)
                    {
                        string tmpStr = cellText;
                        int cutOutSymbolsCount = 0;
                        List<Indexes> searchIndexesList = new List<Indexes>();
                        foreach (string sSearchTemp in arrSearchText)
                        {
                            int resIndex = tmpStr.AccentInsensitiveIndexOf(sSearchTemp);

                            if (resIndex < 0) continue;
                            int sSearchTempLength = sSearchTemp.Length;

                            searchIndexesList.Add(new Indexes
                            {
                                Start = resIndex + cutOutSymbolsCount,
                                End = resIndex + cutOutSymbolsCount + sSearchTempLength
                            });
                            //  tmpStr = tmpStr.Remove(resIndex, sSearchTempLength);
                            tmpStr = tmpStr.Substring(resIndex + sSearchTempLength,
                                tmpStr.Length - resIndex - sSearchTempLength);
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
                                drawParams.Ranges[i * 2] = new CharacterRangeWithFormat(currentStrIndex,
                                    start - currentStrIndex, e.Appearance.ForeColor, e.Appearance.BackColor);
                                drawParams.Ranges[i * 2 + 1] = new CharacterRangeWithFormat(start, end - start,
                                    e.Appearance.ForeColor, System.Drawing.Color.FromArgb(255, 210, 0));
                                currentStrIndex = end;
                                i++;
                            }
                            drawParams.Ranges[i * 2] = new CharacterRangeWithFormat(currentStrIndex,
                                lengthDisp - currentStrIndex, e.Appearance.ForeColor, e.Appearance.BackColor);
                            drawParams.Ranges = drawParams.Ranges.Where(val => val.Length != 0).ToArray();


                            paint.MultiColorDrawString(new DevExpress.Utils.Drawing.GraphicsCache(e.Graphics),
                                drawParams);
                        }
                    }
                }



            }
            else if (tl.OptionsView.ShowAutoFilterRow)
            {
                object valueSearch = e.Column.FilterInfo.AutoFilterRowValue;
                if (valueSearch != null)
                {

                    string filterCellText = valueSearch.ToString();
                    if (!String.IsNullOrEmpty(filterCellText))
                    {
                        string textCon = e.Node.GetDisplayText(e.Column);
                        int filterTextIndex = textCon.IndexOf(filterCellText, StringComparison.CurrentCultureIgnoreCase);
                        if (filterTextIndex != -1)
                        {
                            XPaint.Graphics.DrawMultiColorString(e.Cache, e.Bounds, textCon, filterCellText,
                                e.Appearance, Color.Black, Color.Gold, false,
                                filterTextIndex);
                            e.Handled = true;
                            return;
                        }
                    }

                }
            }









            //if (e.Node.Focused && tl.FocusedColumn.FieldName == e.Column.FieldName)
            //{
            //    Brush backBrush = new LinearGradientBrush(e.Bounds, SystemCellColor.BackColorCellFocused, SystemCellColor.BackColor2ShowEditor,
            //        LinearGradientMode.ForwardDiagonal);
            //    e.Graphics.FillRectangle(backBrush, e.Bounds);
            //    ControlGraphicsInfoArgs c = new ControlGraphicsInfoArgs(e.EditViewInfo, e.Cache, e.EditViewInfo.Bounds);
            //    e.EditPainter.Draw(c);
            //    e.Handled = true;
            //} 
            //if (tl.IsCellSelected(node, col))
            //{
            //    e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
            //    e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
            //    e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            //    return;

            //}


     


            TreeListColumn focusCol = tl.FocusedColumn;
           
            //ControlGraphicsInfoArgs c1 = new ControlGraphicsInfoArgs(e.EditViewInfo, e.Cache, e.EditViewInfo.Bounds);
            //e.EditPainter.Draw(c1);
            //e.Handled = true;
            if (focusCol != null)
            {
                if (e.Node.Focused && focusCol.FieldName == e.Column.FieldName)
                {
                    Brush backBrush = new LinearGradientBrush(e.Bounds, SystemCellColor.BackColorCellFocused, SystemCellColor.BackColor2ShowEditor, LinearGradientMode.ForwardDiagonal);
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);

                    e.Graphics.FillRectangle(backBrush, e.Bounds);
                   
                    ControlGraphicsInfoArgs c = new ControlGraphicsInfoArgs(e.EditViewInfo, e.Cache, e.EditViewInfo.Bounds);
                    e.EditPainter.Draw(c);
                    e.Handled = true;
                    return;
                }
            }
      
            if (tl.IsCellSelected(e.Node, e.Column))
            {
                Brush backBrush = new LinearGradientBrush(e.Bounds, SystemCellColor.BackColorCellSelected, SystemCellColor.BackColor2ShowEditor, LinearGradientMode.ForwardDiagonal);
                e.Graphics.FillRectangle(backBrush, e.Bounds);
           
                ControlGraphicsInfoArgs c = new ControlGraphicsInfoArgs(e.EditViewInfo, e.Cache, e.EditViewInfo.Bounds);
                e.EditPainter.Draw(c);
                e.Handled = true;
            }





        }




        private ExpressionEvaluator GetExpressionEvaluator(CriteriaOperator criteria, bool isReload , out DataView dv)
        {
            //= false
            DataTable dtFt = _treeList.DataSource as DataTable;
            dv = null;
            if (dtFt == null) return null;
            dv = dtFt.DefaultView;

          


            if (!isReload)
            {
                if (criteria.ToString() == _lastCriteria)
                    return _lastEvaluator;
            }
          
            _lastCriteria = criteria.ToString();


            PropertyDescriptorCollection pdc = ((ITypedList)dv).GetItemProperties(null);
            _lastEvaluator = new ExpressionEvaluator(pdc, criteria, false);
            return _lastEvaluator;
        }

        private CriteriaOperator GetFindPanelCriteria(bool isAndSearchNew)
        {
            return FilterCriteriaHelperTreeList.MyConvertFindPanelTextToCriteriaOperator(_treeList, isAndSearchNew);
        }


      

        private DataRowView GetDataRowSearch(DataView dv, TreeListNode node)
        {
          
            return dv[node.Id];
        }


        private void treeList_FilterNode(object sender, FilterNodeEventArgs e)
        {
            
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            string findText = tl.FindFilterText;
            if (string.IsNullOrEmpty(findText)) goto finished;
            e.Handled = true;

           
            DataView dv;

            CriteriaOperator criteria = FilterCriteriaHelperTreeList.ReplaceFindPanelCriteria(_treeList.ActiveFilterCriteria, tl, GetFindPanelCriteria(_isAndSearchNew), _isAndSearchNew);
            ExpressionEvaluator evaluator = GetExpressionEvaluator(criteria, false, out dv);
            if(dv == null) goto finished;
            
           


            //int id = e.Node.Id;
            //  object data;
            DataRowView data = GetDataRowSearch(dv, e.Node);
          




            bool visible = false;
            try
            {
                visible = evaluator.Fit(data);
            }
            catch (Exception ex)
            {
                evaluator = GetExpressionEvaluator(criteria, true, out dv);
                data = GetDataRowSearch(dv, e.Node);
                visible = evaluator.Fit(data);


                Console.WriteLine(ex.ToString());
            }
            finally
            {
                e.Node.Visible = visible;
            }

            finished:






            if (!_filterShowChild) return;

            if (e.IsFitDefaultFilter) return;
          

            TreeListNode nodeTarget = e.Node;
            if (nodeTarget == null) return;
            bool visibleTarget = nodeTarget.Visible;
            if (visibleTarget) return;
            TreeListNode parentNode = nodeTarget.ParentNode;
            if (parentNode == null) return;
            bool visibleParent = parentNode.Visible;
            if (visibleParent)
            {
                nodeTarget.Visible = true;
            }



         





        }

        #endregion
        #region "Menu Item Click"



        private void OnFixedClick(object sender, EventArgs e)
        {
            
            DXMenuItem item = sender as DXMenuItem;
            if (item == null) return;

            _treeList.Update();
            TreeListMenuInfo info = item.Tag as TreeListMenuInfo;
            if (info == null) return;

            string fieldNameCheck = info.Column.FieldName;
            string fieldName = "";
            if (!info.Checked)
            {
                for (int i = 0; i < _treeList.VisibleColumns.Count; i++)
                {
                    if (_treeList.VisibleColumns[i].FieldName == fieldNameCheck)
                    {
                        fieldName = fieldNameCheck;
                        _treeList.VisibleColumns[i].Fixed = FixedStyle.Left;
                        break;
                    }
                    _treeList.VisibleColumns[i].Fixed = FixedStyle.Left;
                }
            }
            else if (info.Checked)
            {
                for (int i = _treeList.VisibleColumns.Count - 1; i >= 0; i--)
                    _treeList.VisibleColumns[i].Fixed = FixedStyle.None;
            }

            OnGetFixedColumn?.Invoke(this, new GetFixedColumnFieldNameEventArgs
            {
                FieldName = fieldName,
            });
        }





        private DXMenuCheckItem CreateCheckItem(string caption, TreeListColumn column, FixedStyle style, Image image)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, column.Fixed == style, image, OnFixedClick)
            {
                BeginGroup = true
            };
            item.Tag = new TreeListMenuInfo(column, style, item.Checked);
            item.Caption = item.Checked ? "Unfreeze Columns" : "Freeze Columns";

            return item;
        }

        #endregion


        #region "Treelist event"
        private void treeList_MouseDown(object sender, MouseEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (!_allowSort) return;
            TreeListHitInfo hi = tl.CalcHitInfo(e.Location);
            if (hi.HitInfoType != HitInfoType.Column) return;
            TreeListColumn tlCol = hi.Column;
            if (tlCol == null) return;
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                tlCol.OptionsColumn.AllowSort = false;

                //if (tlCol.SortOrder == SortOrder.Ascending)
                //{
                //    tlCol.SortOrder = SortOrder.Descending;
                //}
                //else if (tlCol.SortOrder == SortOrder.Descending)
                //{
                //    tlCol.SortOrder = SortOrder.Ascending;
                //}
            }
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                tlCol.OptionsColumn.AllowSort = true;
            }

        }

        private void treeList_Click(object sender, EventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            int count = tl.AllNodesCount;
            TreeListHitInfo hi = tl.CalcHitInfo(tl.PointToClient(Control.MousePosition));
            TreeListColumn tlCol = hi.Column;
            if (hi.HitInfoType == HitInfoType.ColumnButton)
            {
                if (count > 0)
                {
                    tl.BeginUpdate();
                    tl.Selection.Clear();
                    tl.FocusedNode = tl.Nodes[0];
                    tl.FocusedColumn = tlCol ?? tl.VisibleColumns[0];
                    tl.BeginSelection();
                    tl.SelectAll();
                    tl.EndSelection();
                    tl.EndUpdate();

                }
                return;
            }
            if (hi.HitInfoType == HitInfoType.RowIndicator)
            {
                var qT1 = tl.GetSelectedCells().Select(p => p.Node).Distinct().ToList();
                if (qT1.Count > 1) return;

                TreeListNode nodeTemp = hi.Node;
                bool isS = false;
                foreach (TreeListColumn colTemp in tl.VisibleColumns)
                {
                    if (!tl.IsCellSelected(nodeTemp, colTemp))
                    {
                        isS = true;
                        break;
                    }
                }

                if (isS)
                {
                    tl.BeginUpdate();

                    tl.BeginSelection();





                    tl.Selection.Clear();
                    tl.SelectNode(nodeTemp);

                    tl.EndSelection();
                    tl.EndUpdate();
                }

                return;
            }
            if (hi.HitInfoType != HitInfoType.Column) return;
            if (count <= 1) return;

            if (tlCol == null) return;

            var q1 = tl.GetSelectedCells().Select(p => p.Column).Distinct().ToList();
            if (q1.Count <= 0) return;
            var q2 = q1.Any(p => p.FieldName == tlCol.FieldName);
            TreeListNode lastNode = tl.GetLastNodeVisible();
            tl.BeginUpdate();

            tl.BeginSelection();



            tl.FocusedNode = tl.Nodes[0];
            tl.FocusedColumn = tlCol;

            tl.Selection.Clear();
            if (q2)
            {
                if (q1.Any())
                {
                    tl.SelectCells(tl.Nodes[0], q1.First(), lastNode, q1.Last());

                }
                else
                {
                    tl.SelectCells(tl.Nodes[0], tlCol, lastNode, tlCol);
                }
            }
            else
            {
                tl.SelectCells(tl.Nodes[0], tlCol, lastNode, tlCol);
            }

            tl.EndSelection();
            tl.EndUpdate();
        }
        private void treeList_DoubleClick(object sender, EventArgs e)
        {
            var tl = (TreeList)sender;
            if (tl == null) return;
            TreeListHitInfo hi = tl.CalcHitInfo(tl.PointToClient(Control.MousePosition));
            if (hi.HitInfoType != HitInfoType.Column) return;
            if (tl.AllNodesCount <= 1) return;
            TreeListColumn tlCol = hi.Column;
            if (tlCol == null) return;
            if (!_allowSort) return;

            if (!tlCol.OptionsColumn.AllowSort) return;


            SortOrder sortOrder;
            if (tlCol.SortOrder == SortOrder.None)
            {
                sortOrder = SortOrder.Ascending;
            }
            else
            {
                if (tlCol.SortOrder == SortOrder.Ascending)
                {
                    sortOrder = SortOrder.Descending;
                }
                else
                {
                    sortOrder = SortOrder.Ascending;
                }

            }

            //tlCol.SortOrder = SortOrder.None;
            tl.ClearSorting();
            //   tlCol.OptionsColumn.AllowSort = true;
            tl.BeginSort();
            tlCol.SortOrder = sortOrder;
            tl.EndSort();
            tlCol.OptionsColumn.AllowSort = false;



        }
      
     

        private void treeList_NodesReloaded(object sender, EventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            Graphics g = tl.CreateGraphics();
            string value = (tl.AllNodesCount + 1).ToString();
            if (_isDoubleStringIdicator)
            {
                value = string.Format("{0}{0}", value);
            }
          //  _isDoubleStringIdicator
            Int32 width = (Int32)g.MeasureString(value, tl.Appearance.Row.Font).Width;
            tl.IndicatorWidth = width + _increaseIdicatorWidth;
        }
        private void treeList_Paint(object sender, PaintEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListPaintSelectionRect(tl, e);
        }

        private void treeList_MouseMove(object sender, MouseEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListRePaintRect(tl);


        }
        
        private void treeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListRePaintRect(tl);
        }

        private void treeList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListRePaintRect(tl);
        }

        private void treeList_BeforeCollapse(object sender, BeforeCollapseEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListRePaintRect(tl);
        }

        private void treeList_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListRePaintRect(tl);
        }

        private void treeList_TopVisibleNodeIndexChanged(object sender, EventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListRePaintRect(tl);
        }

        private void treeList_LeftCoordChanged(object sender, EventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListDrawRectangleSelection.TreeListRePaintRect(tl);
        }




        
        private void treeList_CustomDrawColumnHeader(object sender, CustomDrawColumnHeaderEventArgs e)
        {

            if (e.Column == null) return;
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            if (tl.AllNodesCount <= 0) return;

            List<TreeListColumn> lCol = tl.GetSelectedColumnsTreeList();



            if (lCol.Any(p => p == e.Column))
            {
                Rectangle rect = e.Bounds;
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
                Brush brush = e.Cache.GetGradientBrush(e.Bounds, Color.DarkTurquoise, Color.Azure, LinearGradientMode.Vertical);
                rect.Inflate(-2, -2);
                e.Graphics.FillRectangle(brush, rect);

                string caption = e.Caption.Replace("<br>", "\n").Trim();
                if (caption.IndexOf("<b>", StringComparison.Ordinal) >= 0)
                {
                    e.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    e.Appearance.DrawString(e.Cache, caption.Replace("<b>", "").Replace("</b>", "").Trim(), e.CaptionRect, e.Appearance.Font, e.Appearance.GetStringFormat());
                }
                else
                {
                    e.Appearance.DrawString(e.Cache, caption.Replace("<b>", "").Replace("</b>", "").Trim(), e.CaptionRect);
                }
                var colInfo = (ColumnInfo)e.ObjectArgs;

                foreach (DevExpress.Utils.Drawing.DrawElementInfo info in colInfo.InnerElements)
                {
                    if (!info.Visible) continue;
                    DevExpress.Utils.Drawing.ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
                }

                /*
                 DevExpress.XtraTreeList.ViewInfo.ColumnInfo info = (DevExpress.XtraTreeList.ViewInfo.ColumnInfo)e.ObjectArgs;
    foreach(DevExpress.Utils.Drawing.DrawElementInfo item in info.InnerElements)
        if (item.ElementPainter is DevExpress.Utils.Drawing.SortedShapeObjectPainter) {
            info.InnerElements.Remove(item);
            break;
        }

                 */

                e.Handled = true;
            }

        }


        private void treeList_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (e.Menu.MenuType != TreeListMenuType.Column) return;
   
          
            
            var menu = e.Menu as TreeListColumnMenu;
            if (menu == null) return;
            TreeListColumn tlCol = menu.Column;
            if (tlCol == null) return;

            bool showSort = false;
            if (_allowSort)
            {
                if (!tlCol.OptionsColumn.AllowSort)
                {
                    tlCol.OptionsColumn.AllowSort = true;
                    showSort = true;
                }
            }

            foreach (DXMenuItem item in e.Menu.Items)
            {
                //|| !tl.OptionsView.ShowAutoFilterRow
                string caption = item.Caption.Trim();

                switch (caption)
                {
                    case "Column Chooser":
                        item.Enabled = _allowColumnsChooser;
                        break;
                    case "Show Auto Filter Row":
                        if (tl.OptionsBehavior.EnableFiltering && _showAutoFilerMenu)
                        {
                            item.Enabled = true;
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                        break;
                    case "Sort Ascending":
                        if (showSort)
                            item.Enabled = true;
                        break;
                    case "Sort Descending":
                        if (showSort)
                            item.Enabled = true;
                        break;
                    case "Clear Sorting":
                        if (showSort && tl.SortedColumnCount > 0)
                        {
                            item.Enabled = true;
                        }
                        break;

                }
            
                
             
           
            }


         

            menu.Items.Add(CreateCheckItem(null, tlCol, FixedStyle.Left, Resources.freeze_column_16x16));
            if (tlCol.OptionsColumn.ShowInCustomizationForm && _allowColumnsChooser)
            {
                menu.Items.Add(CreateHideItem(tlCol, Resources.hidedetail_16x16));
            }
          




        }


        private DXMenuCheckItem CreateHideItem(TreeListColumn column,  Image image)
        {
            DXMenuCheckItem item = new DXMenuCheckItem("Hide This Column", false, image, OnHideClick)
            {
                BeginGroup = true
            };
            item.Tag = new TreeListMenuInfo(column, FixedStyle.None, item.Checked);
            return item;
        }

        private void OnHideClick(object sender, EventArgs e)
        {


            DXMenuItem item = (DXMenuItem)sender;
            if (item == null) return;
            TreeListMenuInfo info = (TreeListMenuInfo)item.Tag;
            if (info == null) return;
            TreeListColumn col = info.Column;
            if (col == null) return;

            TreeList tl = col.TreeList;
            if (tl == null) return;

            tl.BeginUpdate();
            col.VisibleIndex = -1;
            col.Visible = false;
            tl.EndUpdate();



            //_treeList.Update();


            //string fieldNameCheck = info.Column.FieldName;
            //string fieldName = "";
            //if (!info.Checked)
            //{
            //    for (int i = 0; i < _treeList.VisibleColumns.Count; i++)
            //    {
            //        if (_treeList.VisibleColumns[i].FieldName == fieldNameCheck)
            //        {
            //            fieldName = fieldNameCheck;
            //            _treeList.VisibleColumns[i].Fixed = FixedStyle.Left;
            //            break;
            //        }
            //        _treeList.VisibleColumns[i].Fixed = FixedStyle.Left;
            //    }
            //}
            //else if (info.Checked)
            //{
            //    for (int i = _treeList.VisibleColumns.Count - 1; i >= 0; i--)
            //        _treeList.VisibleColumns[i].Fixed = FixedStyle.None;
            //}

            //OnGetFixedColumn?.Invoke(this, new GetFixedColumnFieldNameEventArgs
            //{
            //    FieldName = fieldName,
            //});
        }

        private void treeList_ColumnWidthChanged(object sender, ColumnChangedEventArgs e)
        {
        
            if (e.Column == null) return;
            TreeList tl = (TreeList) sender;
            if (tl == null) return;
            if (tl.AllNodesCount <= 0) return;

            //  List<TreeListColumn> lCol = tl.GetSelectedColumnsTreeList();

            ChangedWithFinal(tl, e.Column);


           


        }

       
        private void ChangedWithFinal(TreeList tl, TreeListColumn colTarget)
        {
            List<string> lCol = tl.GetSelectedCells().Select(p => p.Column.FieldName).Distinct().ToList();
            string fieldName = colTarget.FieldName;
            var q2 = lCol.Where(p => p != fieldName).ToList();
            if (q2.Count <= 0) return;
            int width = colTarget.Width;

            tl.BeginUpdate();
            tl.ColumnWidthChanged -= treeList_ColumnWidthChanged;
            tl.ForceInitialize();
            foreach (string col in q2)
            {
                tl.Columns[col].Width = width;
            }
            tl.ColumnWidthChanged += treeList_ColumnWidthChanged;
            tl.EndUpdate();
        }
    




     
 
    
   
        #endregion

     

        #region "Methold"

     


     

     

  
       



        #endregion
  
    }

 


   



}
