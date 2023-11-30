using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraTab;
using Control = System.Windows.Forms.Control;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace CNY_BaseSys.Common
{
    public class FocusNextControl
    {
        private Dictionary<int, List<Control>> _dicListControl;
        private XtraTabControl _xTabMain;
        private bool _checkKeyDown = false;
        private bool _nextControl = true;

        /// <summary>
        /// No TabMain
        /// </summary>
        /// <param name="arrListCtrl"></param>
        public FocusNextControl(params List<Control>[] arrListCtrl)
        {
            AddListControlDic(arrListCtrl);
        }

        /// <summary>
        /// Has TabMain
        /// </summary>
        /// <param name="xTabMain"></param>
        /// <param name="arrListCtrl"></param>
        public FocusNextControl(XtraTabControl xTabMain, params List<Control>[] arrListCtrl)
        {
            _xTabMain = xTabMain;
            AddListControlDic(arrListCtrl);
        }

        private void AddListControlDic(params List<Control>[] arrListCtrl)
        {
            _dicListControl = new Dictionary<int, List<Control>>();
            for (int tabPageIndex = 0; tabPageIndex < arrListCtrl.Length; tabPageIndex++)
            {
                _dicListControl.Add(tabPageIndex, arrListCtrl[tabPageIndex]);
            }
        }

        public void AddTabIndexControl()
        {
            List<Control> lControl = new List<Control>();
            int index = -1;
            for (int tabPageIndex = 0; tabPageIndex < _dicListControl.Count; tabPageIndex++)
            {
                lControl = GetControlOfCurrentTab(tabPageIndex);
                foreach (Control control in lControl)
                {
                    if (control == null) continue;
                    control.TabIndex = ++index;
                    //control.Tag = index;
                    if (control is PopupContainerEdit)
                    {
                        //set tabindex parent (usercontrol)
                        control.Parent.TabIndex = index;
                    }
                    if (control is GridControl)
                    {
                        var gc = control as GridControl;
                        gc.EditorKeyDown += Gc_EditorKeyDown;
                        gc.KeyDown += Gc_KeyDown;
                        continue;
                    }
                    control.PreviewKeyDown += Control_PreviewKeyDown;
                }
            }
        }
        private List<Control> GetControlOfCurrentTab(int tabIndex)
        {
            if (_dicListControl.TryGetValue(tabIndex, out List<Control> lControl))
            {
                return lControl;
            }
            return new List<Control>();
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var control = sender as Control;

            if (control == null) return;

            int index = ProcessGeneral.GetSafeInt(control.TabIndex);

            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    e.IsInputKey = true;
                    SelectNextControlKeyDown(control, true, index);
                    break;

                case Keys.Escape:
                    e.IsInputKey = true;
                    SelectNextControlKeyDown(control, false, index);
                    break;
            }
        }

        private void SelectNextControlKeyDown(Control control, bool next, int index)
        {
            //LookUpEdit lE = control as LookUpEdit;
            //if (lE != null && lE.IsPopupOpen) return;
            if (_xTabMain != null)
            {
                SetSelectedNextControl(GetControlOfCurrentTab(_xTabMain.SelectedTabPageIndex), next, index, _xTabMain.SelectedTabPageIndex, _xTabMain);
            }
            else
            {
                SetSelectedNextControl(GetControlOfCurrentTab(0), next, index, 0, null);
            }

        }

        private void Gc_EditorKeyDown(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            if (gc == null) return;

            bool isPerform = false;
            if (!_checkKeyDown)
            {
                Gc_KeyDown(sender, e);
                isPerform = true;
            }

            if (!isPerform)
            {
                var gv = gc.FocusedView as GridView;
                if (gv != null)
                {
                    ProcessKeyNextFocus(gv, e);
                }
                else
                {
                    ProcessKeyNextFocus((LayoutView)gc.FocusedView, e);
                }
            }
            _checkKeyDown = false;
        }

        private void Gc_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            if (gc == null) return;
            _checkKeyDown = true;
            var gv = gc.FocusedView as GridView;
            if (gv != null)
            {
                ProcessKeyNextFocus(gv, e);
            }
            else
            {
                ProcessKeyNextFocus((LayoutView)gc.FocusedView, e);
            }
        }

        private void ProcessKeyNextFocus(GridView gv, KeyEventArgs e)

        {
            int rH = gv.FocusedRowHandle;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                GridColumn col = gv.FocusedColumn;

                if (col != null)
                {
                    int visibleIndex = col.VisibleIndex;
                    SelectedNextCell(gv, rH, visibleIndex);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                GridColumn col = gv.FocusedColumn;

                if (col != null)
                {
                    int visibleIndex = col.VisibleIndex;
                    SelectedPreviousCell(gv, rH, visibleIndex);
                }
            }
        }

        private void ProcessKeyNextFocus(LayoutView lv, KeyEventArgs e)
        {
            int rH = lv.FocusedRowHandle;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                GridColumn col = lv.FocusedColumn;

                if (col != null)
                {
                    int visibleIndex = col.AbsoluteIndex;
                    SelectedNextCell(lv, rH, visibleIndex);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                GridColumn col = lv.FocusedColumn;

                if (col != null)
                {
                    int visibleIndex = col.AbsoluteIndex;
                    SelectedPreviousCell(lv, rH, visibleIndex);
                }
            }
        }


        private void SelectedNextCell(GridView gv, int rH, int currentVisibleIndex)
        {
            if (gv == null) return;

            if (gv.RowCount == 0)
            {
                SelectNextControlKeyDown(gv.GridControl, true, ProcessGeneral.GetSafeInt(gv.GridControl.TabIndex));
                return;
            }

            GridColumn col = gv.VisibleColumns[currentVisibleIndex + 1];

            if (col != null)
            {
                ProcessGeneral.SetFocusedCellOnGrid(gv, rH, col.FieldName);
            }
            else
            {
                int nextRh = rH + 1;
                if (nextRh >= gv.RowCount)
                {
                    //Next control if cell last visible, enter second next
                    if (!_nextControl)
                    {
                        SelectNextControlKeyDown(gv.GridControl, true, ProcessGeneral.GetSafeInt(gv.GridControl.TabIndex));
                        _nextControl = true;
                        return;
                    }
                    if (gv.ActiveEditor != null)
                    {
                        gv.CloseEditor();
                    }
                    _nextControl = false;
                    return;
                }
                GridColumn nextColumn = gv.VisibleColumns[0];
                ProcessGeneral.SetFocusedCellOnGrid(gv, nextRh, nextColumn.FieldName);
            }
        }

        private void SelectedNextCell(LayoutView lv, int rH, int currentVisibleIndex)
        {
            if (lv == null) return;

            if (lv.RowCount == 0)
            {
                SelectNextControlKeyDown(lv.GridControl, true, ProcessGeneral.GetSafeInt(lv.GridControl.TabIndex));
                return;
            }

            GridColumn col = lv.VisibleColumns[currentVisibleIndex + 1];

            if (col != null)
            {
                lv.SetFocusedRowOnLayoutView(rH, col);
            }
            else
            {
                int nextRh = rH + 1;
                if (nextRh >= lv.RecordCount)
                {
                    //Next control if cell last visible, enter second next
                    if (!_nextControl)
                    {
                        SelectNextControlKeyDown(lv.GridControl, true, ProcessGeneral.GetSafeInt(lv.GridControl.TabIndex));
                        _nextControl = true;
                        return;
                    }
                    if (lv.ActiveEditor != null)
                    {
                        lv.CloseEditor();
                    }
                    _nextControl = false;
                    return;
                }
                GridColumn nextColumn = lv.VisibleColumns[0];
                lv.SetFocusedRowOnLayoutView(nextRh, nextColumn);
            }
        }

        private void SelectedPreviousCell(GridView gv, int rH, int currentVisibleIndex)
        {
            if (gv == null) return;

            if (gv.RowCount == 0)
            {
                SelectNextControlKeyDown(gv.GridControl, false, ProcessGeneral.GetSafeInt(gv.GridControl.TabIndex));
                return;
            }

            GridColumn col = gv.VisibleColumns[currentVisibleIndex - 1];

            if (col != null)
            {
                ProcessGeneral.SetFocusedCellOnGrid(gv, rH, col.FieldName);
            }
            else
            {
                int previousRh = rH - 1;
                if (previousRh < 0)
                {
                    //Previous control if first cell visible
                    SelectNextControlKeyDown(gv.GridControl, false, ProcessGeneral.GetSafeInt(gv.GridControl.TabIndex));
                    return;
                }
                GridColumn previousColumn = gv.VisibleColumns.Last();
                ProcessGeneral.SetFocusedCellOnGrid(gv, previousRh, previousColumn.FieldName);

            }
        }

        private void SelectedPreviousCell(LayoutView lv, int rH, int currentVisibleIndex)
        {
            if (lv == null) return;

            if (lv.RowCount == 0)
            {
                SelectNextControlKeyDown(lv.GridControl, false, ProcessGeneral.GetSafeInt(lv.GridControl.TabIndex));
                return;
            }

            GridColumn col = lv.VisibleColumns[currentVisibleIndex - 1];

            if (col != null)
            {
                lv.SetFocusedRowOnLayoutView(rH, col);
            }
            else
            {
                int previousRh = rH - 1;
                if (previousRh < 0)
                {
                    //Previous control if first cell visible
                    SelectNextControlKeyDown(lv.GridControl, false, ProcessGeneral.GetSafeInt(lv.GridControl.TabIndex));
                    return;
                }
                GridColumn previousColumn = lv.VisibleColumns.Last();
                lv.SetFocusedRowOnLayoutView(previousRh, previousColumn);
            }
        }

        private void SetSelectedNextControl(List<Control> lControl, bool next, int index, int currentTab, DevExpress.XtraTab.XtraTabControl xTabMain)
        {
            int tabCount = xTabMain == null ? 1 : xTabMain.TabPages.Count;
            var q1 = lControl.Where(p => (p.Visible && p.Enabled));
            if (next)
            {
                var q2 = q1.Where(p => ProcessGeneral.GetSafeInt(p.TabIndex) > index).Select(p => new
                {
                    ChildControl = p,
                    Index = ProcessGeneral.GetSafeInt(p.TabIndex)
                }).OrderBy(p => p.Index).ToList();
                if (q2.Any())
                {
                    Control nextControl = q2.Select(p => p.ChildControl).FirstOrDefault();
                    if (nextControl == null) return;
                    nextControl.Focus();
                }
                else
                {
                    if (currentTab == tabCount - 1) return;
                    xTabMain.SelectedTabPageIndex = currentTab + 1;
                    int activeNewTab = xTabMain.SelectedTabPageIndex;

                    if (activeNewTab == currentTab) return;

                    var q3 = GetControlOfCurrentTab(activeNewTab).Where(p => (p.Visible && p.Enabled))
                          .Select(p => new
                          {
                              ChildControl = p,
                              Index = ProcessGeneral.GetSafeInt(p.TabIndex)
                          }).OrderBy(p => p.Index).ToList();
                    if (!q3.Any()) return;
                    Control nextTabControl = q3.Select(p => p.ChildControl).FirstOrDefault();
                    if (nextTabControl == null) return;
                    nextTabControl.Focus();
                }


            }
            else
            {
                var q4 = q1.Where(p => ProcessGeneral.GetSafeInt(p.TabIndex) < index).Select(p => new
                {
                    ChildControl = p,
                    Index = ProcessGeneral.GetSafeInt(p.TabIndex)
                }).OrderByDescending(p => p.Index).ToList();
                if (q4.Any())
                {
                    Control nextControl = q4.Select(p => p.ChildControl).FirstOrDefault();
                    if (nextControl == null) return;
                    nextControl.Focus();
                }
                else
                {
                    if (currentTab == 0) return;
                    xTabMain.SelectedTabPageIndex = currentTab - 1;
                    int activePrivousTab = xTabMain.SelectedTabPageIndex;
                    if (activePrivousTab == currentTab) return;

                    var q5 = GetControlOfCurrentTab(activePrivousTab).Where(p => (p.Visible && p.Enabled))
                             .Select(p => new
                             {
                                 ChildControl = p,
                                 Index = ProcessGeneral.GetSafeInt(p.TabIndex)
                             }).OrderByDescending(p => p.Index).ToList();
                    if (!q5.Any()) return;
                    Control privousTabControl = q5.Select(p => p.ChildControl).FirstOrDefault();
                    if (privousTabControl == null) return;
                    privousTabControl.Focus();
                }
            }
        }

    }
}
