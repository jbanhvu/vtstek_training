using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using CNY_BaseSys.Common;
using System.Drawing.Drawing2D;
using CNY_BaseSys.Class;
using CNY_BaseSys.WForm;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CNY_BaseSys.UControl
{
    public partial class XtraUCGridViewPopup : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property"
        public event TransferDataOnGridViewHandler OnTransferData = null;
        private string _findPanelText = "";
        private bool _isMultiSelected = true;
        public bool IsMultiSelected { get { return this._isMultiSelected; } set { this._isMultiSelected = value; } }
        private bool _isShowFindPanel = true;
        public bool IsShowFindPanel { get { return this._isShowFindPanel; } set { this._isShowFindPanel = value; } }
        private bool _isShowFooter = true;
        public bool IsShowFooter { get { return this._isShowFooter; } set { this._isShowFooter = value; } }
        private bool _isShowAutoFilterRow = false;
        public bool IsShowAutoFilterRow { get { return this._isShowAutoFilterRow; } set { this._isShowAutoFilterRow = value; } }
        private string _strFilter = "";
        public string StrFilter
        {
            get { return this._strFilter; }
            set { this._strFilter = value; }
        }

        public DataTable DtSource
        {
            get { return (DataTable)gcMain.DataSource; }
        }

        private List<GridViewTransferFormatFooter> lFooterFormat = new List<GridViewTransferFormatFooter>();

        private GridMultiSelectMode _multiSelectMode = GridMultiSelectMode.RowSelect;

        public GridMultiSelectMode MultiSelectMode
        {
            get { return this._multiSelectMode; }
            set { this._multiSelectMode = value; }
        }

        private bool isCheckBoxSelected = false;

        private List<String> _listColorCol = new List<string>();

        public List<String> ListColorCol
        {
            get { return this._listColorCol; }
            set { this._listColorCol = value; }
        }

        private List<Int32> qSelectedRow;

        private Int32 _bestFitRowCount = 0;
        public Int32 BestFitRowCount { get { return this._bestFitRowCount; } set { this._bestFitRowCount = value; } }

        private int _focusedColAutoRow = 0;
        public Int32 FocusedColAutoRow { get { return this._focusedColAutoRow; } set { this._focusedColAutoRow = value; } }

        private bool _isAddColumnSelected = false;
        public bool IsAddColumnSelected
        {
            get { return this._isAddColumnSelected; }
            set { this._isAddColumnSelected = value; }
        }

        private const string CheckBoxSelectedField = "IsSelected";
        private bool _isPerformSelect = true;

        private string _keyField = "PK";
        public string KeyField
        {
            get
            { return this._keyField; }
            set
            {
                this._keyField = value;
            }
        }


        private string _displayField = "";
        public string DisplayField
        {
            get
            { return this._displayField; }
            set
            {
                this._displayField = value;
            }
        }

        private string _separator = ", ";
        public string Separator
        {
            get { return this._separator; }
            set { this._separator = value; }
        }
        public PopupContainerEdit PpEdit
        {
            get { return ppEdit; }
        }

        public string EditText
        {
            get { return ProcessGeneral.GetSafeString(ppEdit.EditValue); }
            set { ppEdit.EditValue = value; }
        }

        private List<object> _lValue = new List<object>();
        public List<object> LValue
        {
            get
            { return this._lValue; }
            set
            {
                this._lValue = value;
            }
        }

        public Int32 PopupWidth
        {
            get
            { return this.popupContainerGv.Width; }
            set
            {
                this.popupContainerGv.Width = value;
            }
        }
        public Int32 PopupHeight
        {
            get
            { return this.popupContainerGv.Height; }
            set
            {
                this.popupContainerGv.Height = value;
            }
        }

        public bool ShowButton
        {
            get { return splitContainerMain.PanelVisibility == SplitPanelVisibility.Both; }
            set
            {
                if (!value)
                {
                    splitContainerMain.PanelVisibility = SplitPanelVisibility.Panel2;
                }
            }

        }

        public bool ShowFinishButton
        {
            get { return btnFinish.Visible; }
            set
            {
                btnFinish.Visible = value;

            }
        }

        #endregion

        #region "Contructor"
        public XtraUCGridViewPopup()
        {
            InitializeComponent();

            ProcessGeneral.SetTooltipControl(btnFinish, "Enter Key", "Info", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);

            qSelectedRow = new List<int>();

            ppEdit.KeyPress += PpEdit_KeyPress;
            ppEdit.QueryPopUp += PpEdit_QueryPopUp;
            ppEdit.CloseUp += PpEdit_CloseUp;
            ppEdit.BeforePopup += PpEdit_BeforePopup;
            ppEdit.Popup += PpEdit_Popup;
            ppEdit.KeyDown += PpEdit_KeyDown;
            ppEdit.EditValueChanged += PpEdit_EditValueChanged;
            btnFinish.Click += BtnFinish_Click;
        }

        #endregion

        #region "Process Popup Control"
        private bool CompareDataKey(object value1, object value2)
        {
            bool rs = Object.Equals(value1, value2);
            return rs;
        }

        public void SetEditValue(List<object> lValue)
        {
            this._lValue.Clear();
            DataTable dtS = (DataTable)gcMain.DataSource;
            if (dtS == null)
            {
                ppEdit.EditValue = "";
                return;
            }

            string displayField = GetDisplayField(dtS);
            var q1 = dtS.AsEnumerable().Where(p => lValue.Any(t => CompareDataKey(t, p[_keyField]))).Select(p => new
            {
                Value = p[_keyField],
                Text = ProcessGeneral.GetSafeString(p[displayField])
            }).ToList();

            foreach (var item in q1)
            {
                this._lValue.Add(item.Value);
            }


            string text = string.Join(_separator, q1.Select(p => p.Text).Distinct().ToArray()).Trim();
            ppEdit.EditValue = text;
        }

        private string GetDisplayField(DataTable dtS)
        {
            string displayField = _displayField;
            if (string.IsNullOrEmpty(displayField))
            {
                if (gvMain.VisibleColumns.Count > 0)
                {
                    displayField = gvMain.VisibleColumns[0].FieldName;
                }
                else
                {
                    displayField = dtS.Columns[0].ColumnName;
                }
            }

            return displayField;
        }

        private void PpEdit_EditValueChanged(object sender, EventArgs e)
        {
            ppEdit.ToolTip = ProcessGeneral.GetSafeString(ppEdit.EditValue);
        }

        private void PpEdit_BeforePopup(object sender, EventArgs e)
        {
            if (_lValue != null)
            {
                gvMain.ClearSelection();
                foreach (var key in _lValue)
                {
                    int rowHandle = gvMain.LocateByValue(_keyField, key);
                    gvMain.SelectRow(rowHandle);
                }
            }
            if (_isShowFindPanel)
            {
                gvMain.ShowFindPanel();

                if (!string.IsNullOrEmpty(this._strFilter))
                {
                    gvMain.ApplyFindFilter(this._strFilter);
                }
                BeginInvoke(new MethodInvoker(gvMain.FocusFindEdit));
            }
        }

        private void PpEdit_Popup(object sender, EventArgs e)
        {
            if (gvMain.RowCount > 0)
            {
                gcMain.Focus();
                gvMain.Focus();
            }

        }


        private void PpEdit_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            _findPanelText = "";
            if (!string.IsNullOrEmpty(gvMain.FindFilterText.Trim()))
            {
                gvMain.ApplyFindFilter("");
            }
        }

        private void PpEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isInputString;
            string letter = e.KeyChar.CheckIsKeyInputText(out isInputString);
            if (!isInputString)
            {
                _findPanelText = "";
                return;
            }
            _findPanelText = letter;
            ppEdit.ShowPopup();
        }


        private void PpEdit_KeyDown(object sender, KeyEventArgs e)
        {
            #region "Delete Key"
            if (e.KeyCode == Keys.Delete)
            {
                _lValue.Clear();
                gvMain.ClearSelection();
                ppEdit.EditValue = "";
                OnTransferData?.Invoke(this, new TransferDataOnGridViewEventArgs
                {
                    ReturnRowsSelected = new List<DataRow>()
                });
                return;

            }
            #endregion
            #region "Process Ctrl+C key"

            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control && !string.IsNullOrEmpty(ppEdit.Text))
            {
                Clipboard.SetText(ppEdit.Text);
                return;
            }

            #endregion
        }

        private void PpEdit_QueryPopUp(object sender, CancelEventArgs e)
        {
            e.Cancel = DtSource == null || DtSource.Rows.Count <= 0;
        }
        #endregion

        #region "Button Event"

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            this._lValue.Clear();
            DataTable dtS = (DataTable)gcMain.DataSource;

            List<DataRow> returnRowsSelected;
            if (gvMain.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect && _isAddColumnSelected)
            {
                returnRowsSelected = dtS.AsEnumerable().Where(p => ProcessGeneral.GetSafeBool(p[CheckBoxSelectedField])).Select(p => p).ToList();
            }
            else
            {
                returnRowsSelected = gvMain.GetSelectedRows().Where(r => r != GridControl.AutoFilterRowHandle).Select(p => gvMain.GetDataRow(p)).ToList();

            }

            if (returnRowsSelected.Count <= 0)
            {
                ppEdit.EditValue = "";
                ppEdit.ClosePopup();
                return;
            }

            string displayField = GetDisplayField(dtS);

            var q1 = returnRowsSelected.Select(p => new
            {
                Value = p[_keyField],
                Text = ProcessGeneral.GetSafeString(p[displayField])
            }).ToList();

            foreach (var item in q1)
            {
                this._lValue.Add(item.Value);
            }

            string text = string.Join(_separator, q1.Select(p => p.Text).Distinct().ToArray()).Trim();
            ppEdit.EditValue = text;

            OnTransferData?.Invoke(this, new TransferDataOnGridViewEventArgs
            {
                ReturnRowsSelected = returnRowsSelected,
            });

            ppEdit.ClosePopup();
        }


        #endregion


        #region "GridView"

        public void LoadDataGridView(DataTable dtSource, List<GridViewTransferDataColumnInit> ListGvColFormat)
        {

            lFooterFormat = (ListGvColFormat.AsEnumerable().Where(p => p.SummayType != SummaryItemType.None)
                            .Select(p => new GridViewTransferFormatFooter
                            {
                                FieldName = p.FieldName,
                                SummaryHorzAlign = p.SummaryHorzAlign,
                            })).ToList();
            GridViewCustomInit();

            CreateEventOnGridView();

            gvMain.Columns.Clear();
            gcMain.DataSource = null;
            if (dtSource == null || dtSource.Rows.Count <= 0) return;

            if (isCheckBoxSelected && _isAddColumnSelected)
            {
                if (dtSource.Columns.Cast<DataColumn>().All(p => p.ColumnName != CheckBoxSelectedField))
                {
                    dtSource.Columns.Add(CheckBoxSelectedField, typeof(bool));
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row[CheckBoxSelectedField] = false;
                    }
                    dtSource.AcceptChanges();
                }

                GridViewTransferDataColumnInit checkColumnUpd = ListGvColFormat.FirstOrDefault(p => p.FieldName == CheckBoxSelectedField);
                if (checkColumnUpd == null)
                {
                    GridViewTransferDataColumnInit checkColumn = new GridViewTransferDataColumnInit
                    {
                        Caption = CheckBoxSelectedField,
                        FieldName = CheckBoxSelectedField,
                        HorzAlign = HorzAlignment.Center,
                        FixStyle = FixedStyle.None,
                        VisibleIndex = -1,
                        FormatField = FormatType.None,
                        FormatString = "",
                        IncreaseWdith = 0,
                        SummayType = SummaryItemType.None,
                        SummaryFormatString = "",
                        SummaryHorzAlign = HorzAlignment.Center,
                    };
                    ListGvColFormat.Add(checkColumn);
                }
                else if (checkColumnUpd.VisibleIndex >= 0)
                {
                    checkColumnUpd.VisibleIndex = -1;
                }
                //gvMain.OptionsSelection.CheckBoxSelectorField = CheckBoxSelectedField;
            }


            gcMain.DataSource = dtSource;

            gvMain.BeginUpdate();

            Dictionary<string, int> dicBesfit = new Dictionary<string, int>();

            if (ListGvColFormat != null)
            {
                foreach (var item in ListGvColFormat)
                {

                    GridColumn gCol = gvMain.Columns[item.FieldName.Trim()];
                    gCol.AppearanceCell.Options.UseTextOptions = true;
                    gCol.AppearanceCell.TextOptions.HAlignment = item.HorzAlign;
                    gCol.AppearanceHeader.Options.UseTextOptions = true;
                    gCol.AppearanceHeader.TextOptions.HAlignment = item.HorzAlign;
                    gCol.Caption = item.Caption.Trim();
                    int visibleIndex = item.VisibleIndex;
                    if (visibleIndex >= 0)
                    {
                        if (isCheckBoxSelected)
                        {
                            gCol.VisibleIndex = visibleIndex + 1;
                        }
                        else
                        {
                            gCol.VisibleIndex = visibleIndex;
                        }

                    }
                    else
                    {
                        gCol.VisibleIndex = visibleIndex;
                    }

                    gCol.Fixed = item.FixStyle;
                    gCol.DisplayFormat.FormatType = item.FormatField;
                    gCol.OptionsFilter.AutoFilterCondition = item.FilterCondition;

                    // gCol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
                    if (item.SummayType != SummaryItemType.None)
                    {
                        gCol.SummaryItem.SummaryType = item.SummayType;
                        if (!string.IsNullOrEmpty(item.SummaryFormatString))
                        {
                            gCol.SummaryItem.DisplayFormat = item.SummaryFormatString;
                        }

                    }
                    if (!string.IsNullOrEmpty(item.FormatString))
                    {
                        gCol.DisplayFormat.FormatString = item.FormatString;
                    }
                    if (item.IncreaseWdith > 0)
                    {
                        dicBesfit.Add(gCol.FieldName, item.IncreaseWdith);
                        // gCol.Width += item.IncreaseWdith;
                    }

                    // gvMain.Columns.Add(gCol);

                }
            }

            gvMain.BestFitColumns();

            foreach (var itemB in dicBesfit)
            {
                gvMain.Columns[itemB.Key].Width += itemB.Value;
            }

            gvMain.EndUpdate();

            if (_isShowAutoFilterRow)
            {

                int focusedColAutoRow = 0;
                if (isCheckBoxSelected)
                {
                    focusedColAutoRow = 1;
                }
                else if (gvMain.VisibleColumns[_focusedColAutoRow] != null)
                {
                    focusedColAutoRow = _focusedColAutoRow;
                }


                gvMain.FocusedColumn = gvMain.VisibleColumns[focusedColAutoRow];

                gvMain.FocusedRowHandle = GridControl.AutoFilterRowHandle;

                if (!string.IsNullOrEmpty(this._strFilter))
                {
                    //_focusedColAutoRow

                    gvMain.SetRowCellValue(GridControl.AutoFilterRowHandle, gvMain.VisibleColumns[focusedColAutoRow], this._strFilter);


                }
                gvMain.ShowEditor();
            }

            _isPerformSelect = true;
        }

        private void GridViewCustomInit()
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
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsCustomization.AllowColumnResizing = true;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = _isShowAutoFilterRow;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.OptionsView.EnableAppearanceEvenRow = true;
            gvMain.OptionsView.EnableAppearanceOddRow = true;
            // gvMain.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;
            if (_bestFitRowCount > 0)
            {
                gvMain.OptionsView.BestFitMaxRowCount = _bestFitRowCount;
            }

            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.MultiSelect = _isMultiSelected;
            gvMain.OptionsSelection.MultiSelectMode = _multiSelectMode;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            if (_multiSelectMode == GridMultiSelectMode.CheckBoxRowSelect)
            {
                gvMain.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
                gvMain.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;

                SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gvMain, false);
            }

            gvMain.OptionsView.ShowFooter = _isShowFooter;

            //   gvMain.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = true;

            gvMain.OptionsFind.AlwaysVisible = false;

            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain, true)
            {
                IsPerFormEvent = true,
                IsDrawFilter = true,
            };

            isCheckBoxSelected = gvMain.OptionsSelection.MultiSelect && gvMain.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(GridView gv, bool value)
        {
            if (IsMultiSelected && gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect)
            {
                gv.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = value;
                //  gv.VisibleColumns[0].Width = 30;
            }

        }
        private void SelectionRows(GridView gv, MouseEventArgs e)
        {
            GridView view = gv;
            Point pt = e.Location;
            //int focusedRowHandle = view.FocusedRowHandle;
            GridHitInfo hi = view.CalcHitInfo(pt);

            int clickedRowHandle = hi.RowHandle;
            _isPerformSelect = false;

            var lSelected = _isAddColumnSelected ? Enumerable.Range(0, gv.RowCount).Where(p => p != clickedRowHandle && ProcessGeneral.GetSafeBool(gv.GetRowCellValue(p, CheckBoxSelectedField))).ToList() : qSelectedRow.Where(p => p != clickedRowHandle).ToList();

            if (ShiftKeyIsPressed())
            {
                int rowSelectMax = lSelected.Count > 0 ? lSelected.Max() : clickedRowHandle;
                int rowSelectMin = lSelected.Count > 0 ? lSelected.Min() : clickedRowHandle;
                if (clickedRowHandle == rowSelectMax)
                {
                    view.SelectRow(clickedRowHandle);
                    if (_isAddColumnSelected)
                    {
                        view.SetRowCellValue(clickedRowHandle, CheckBoxSelectedField, true);
                    }
                }
                if (clickedRowHandle > rowSelectMax)
                {
                    for (int i = rowSelectMax + 1; i <= clickedRowHandle; i++)
                    {
                        view.SelectRow(i);
                        if (_isAddColumnSelected)
                        {
                            view.SetRowCellValue(i, CheckBoxSelectedField, true);
                        }
                    }
                }
                if (clickedRowHandle < rowSelectMin)
                {
                    for (int i = rowSelectMin - 1; i >= clickedRowHandle; i--)
                    {
                        view.SelectRow(i);
                        if (_isAddColumnSelected)
                        {
                            view.SetRowCellValue(i, CheckBoxSelectedField, true);
                        }
                    }
                }

                if (clickedRowHandle > rowSelectMin && clickedRowHandle < rowSelectMax)
                {
                    for (int i = rowSelectMin; i <= clickedRowHandle; i++)
                    {
                        if (view.IsRowSelected(i)) continue;
                        view.SelectRow(i);
                        if (_isAddColumnSelected)
                        {
                            view.SetRowCellValue(i, CheckBoxSelectedField, true);
                        }
                    }
                }
            }
            if (CtrlKeyIsPressed())
            {
                if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(clickedRowHandle, CheckBoxSelectedField)))
                {
                    view.UnselectRow(clickedRowHandle);
                    if (_isAddColumnSelected)
                    {
                        view.SetRowCellValue(clickedRowHandle, CheckBoxSelectedField, false);
                    }
                }
                else
                {
                    view.SelectRow(clickedRowHandle);
                    if (_isAddColumnSelected)
                    {
                        view.SetRowCellValue(clickedRowHandle, CheckBoxSelectedField, true);
                    }
                }
            }
            foreach (var i in lSelected)
            {
                view.SelectRow(i);
                view.SetRowCellValue(i, CheckBoxSelectedField, true);
            }
            _isPerformSelect = true;
        }
        private bool IsCheckBoxColumnClicked(GridView gv, MouseEventArgs e)
        {
            GridView view = gv;
            Point pt = e.Location;
            GridHitInfo hi = view.CalcHitInfo(pt);
            if (hi.InRowCell && hi.Column.Name == GridView.CheckBoxSelectorColumnName && (ShiftKeyIsPressed() || CtrlKeyIsPressed()))
            {
                return true;
            }
            return false;
        }
        private bool HandleColumnHeaderClick(GridView gv, GridHitInfo hi)
        {
            if (gv == null)
                return false;
            //     GridHitInfo hi = gv.CalcHitInfo(pt);
            if (hi.InColumnPanel && hi.Column.Name == GridView.CheckBoxSelectorColumnName && (ShiftKeyIsPressed() || CtrlKeyIsPressed()))
                return true;
            return false;
        }
        private bool CtrlKeyIsPressed()
        {
            return Control.ModifierKeys == Keys.Control;
        }
        private bool ShiftKeyIsPressed()
        {
            return Control.ModifierKeys == Keys.Shift;
        }
        private void CreateEventOnGridView()
        {
            gvMain.KeyDown += gvMain_KeyDown;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.CustomDrawFooter += gvMain_CustomDrawFooter;
            gvMain.CustomDrawCell += gvMain_CustomDrawCell;
            gvMain.CustomDrawFooterCell += gvMain_CustomDrawFooterCell;
            gvMain.KeyUp += gvMain_KeyUp;
            gvMain.MouseDown += gvMain_MouseDown;
            gvMain.LeftCoordChanged += gvMain_LeftCoordChanged;
            gvMain.MouseMove += gvMain_MouseMove;
            gvMain.TopRowChanged += gvMain_TopRowChanged;
            gvMain.FocusedColumnChanged += gvMain_FocusedColumnChanged;
            gvMain.FocusedRowChanged += gvMain_FocusedRowChanged;
            gcMain.Paint += gcMain_Paint;
            gvMain.RowCellStyle += gvMain_RowCellStyle;
            gvMain.CustomColumnDisplayText += gvMain_CustomColumnDisplayText;

            gvMain.SelectionChanged += GvMain_SelectionChanged;
            gvMain.ColumnFilterChanged += GvMain_ColumnFilterChanged;

            gcMain.ForceInitialize();
        }



        #endregion


        #region "GridView Event"

        private void GvMain_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (!isCheckBoxSelected || !_isAddColumnSelected || gvMain.RowCount <= 0) return;
            _isPerformSelect = false;
            var q1 = Enumerable.Range(0, gvMain.RowCount).Where(p =>
                ProcessGeneral.GetSafeBool(gvMain.GetRowCellValue(p, CheckBoxSelectedField))).ToArray();
            foreach (int j in q1)
            {
                gvMain.SelectRow(j);
            }
            _isPerformSelect = true;
        }

        private void GvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isPerformSelect || !isCheckBoxSelected || !_isAddColumnSelected || gvMain.RowCount <= 0) return;

            GridHitInfo hi = gvMain.CalcHitInfo(gcMain.PointToClient(Control.MousePosition));

            if (hi.Column != null && hi.Column.Name == GridView.CheckBoxSelectorColumnName && hi.InColumn)
            {
                for (int i = 0; i < gvMain.RowCount; i++)
                {
                    if (gvMain.IsRowSelected(i))
                    {
                        gvMain.SetRowCellValue(i, CheckBoxSelectedField, true);
                    }
                    else
                    {
                        gvMain.SetRowCellValue(i, CheckBoxSelectedField, false);
                    }
                }
            }

            if (gvMain.FocusedRowHandle == GridControl.AutoFilterRowHandle)
            {
                _isPerformSelect = false;
                var q1 = Enumerable.Range(0, gvMain.RowCount).Where(p =>
                    ProcessGeneral.GetSafeBool(gvMain.GetRowCellValue(p, CheckBoxSelectedField))).ToArray();
                foreach (int j in q1)
                {
                    gvMain.SelectRow(j);
                }
                _isPerformSelect = true;
                return;
            }


            if (hi.Column != null && hi.Column.Name == GridView.CheckBoxSelectorColumnName && hi.InRowCell) return;

            int[] arrSel = gvMain.GetSelectedRows();

            for (int i = 0; i < gvMain.RowCount; i++)
            {
                if (arrSel.Contains(i))
                {
                    if (!ProcessGeneral.GetSafeBool(gvMain.GetRowCellValue(i, CheckBoxSelectedField)))
                    {
                        gvMain.SetRowCellValue(i, CheckBoxSelectedField, true);
                    }
                }
                else
                {
                    if (ProcessGeneral.GetSafeBool(gvMain.GetRowCellValue(i, CheckBoxSelectedField)))
                    {
                        gvMain.SetRowCellValue(i, CheckBoxSelectedField, false);
                    }
                }

            }
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridControl gc = gv.GridControl;
            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(MousePosition));
            if (!hi.InRowCell) return;
            if (!IsMultiSelected)
            {
                goto selectRow;

            }
            if (gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect && hi.Column.Name != GridView.CheckBoxSelectorColumnName)
            {
                goto selectRow;
            }
            if (gv.SelectedRowsCount == 1)
            {
                goto selectRow;
            }

            return;
            selectRow:
            BtnFinish_Click(null, null);
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                BtnFinish_Click(null, null);
                return;
            }

            if (gv.OptionsSelection.MultiSelect &&
                gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect &&
                (e.KeyData == (Keys.ShiftKey | Keys.Shift) || e.KeyData == (Keys.Control | Keys.ControlKey)))
            {
                SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, true);
            }
        }

        private void gvMain_KeyUp(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            if (gv.OptionsSelection.MultiSelect &&
                gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect &&
                (e.KeyData == Keys.ShiftKey || e.KeyData == Keys.ControlKey))
            {
                SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, false);
            }
        }

        private void gvMain_MouseDown(object sender, MouseEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) goto finished1;
            if (!gv.OptionsSelection.MultiSelect) goto finished1;
            GridHitInfo hi = gv.CalcHitInfo(e.Location);
            if (hi.Column == null) goto finished1;
            if (gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CheckBoxRowSelect) goto finished1;
            if (hi.Column.Name != GridView.CheckBoxSelectorColumnName) goto finished1;
            if (hi.InColumn) goto finished1;

            if (IsCheckBoxColumnClicked(gv, e))
            {
                SelectionRows(gv, e);
                ((DXMouseEventArgs)e).Handled = HandleColumnHeaderClick(gv, hi);

                goto finished1;
            }

            bool handled = HandleColumnHeaderClick(gv, hi);

            if (!handled)
            {
                int rH = hi.RowHandle;
                if (gv.IsDataRow(rH))
                {
                    _isPerformSelect = false;
                    var q2 = _isAddColumnSelected ? Enumerable.Range(0, gv.RowCount).Where(p => p != rH && ProcessGeneral.GetSafeBool(gv.GetRowCellValue(p, CheckBoxSelectedField))).ToList() : qSelectedRow.Where(p => p != rH).ToList();

                    if (_isAddColumnSelected)
                    {
                        if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rH, CheckBoxSelectedField)))
                        {
                            gv.UnselectRow(rH);
                            gv.SetRowCellValue(rH, CheckBoxSelectedField, false);

                        }
                        else
                        {
                            gv.SelectRow(rH);
                            gv.SetRowCellValue(rH, CheckBoxSelectedField, true);

                        }
                    }
                    else
                    {
                        if (qSelectedRow.Any(p => p == rH))
                        {
                            gv.UnselectRow(rH);
                        }
                        else
                        {
                            gv.SelectRow(rH);

                        }
                    }

                    foreach (var i in q2)
                    {
                        gv.SelectRow(i);
                    }
                    _isPerformSelect = true;

                }

            }
            ((DXMouseEventArgs)e).Handled = true;

            finished1:
            _isPerformSelect = true;
        }


        private void gvMain_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.Column == null) return;
            string fieldName = e.Column.FieldName;
            if (_listColorCol.Count <= 0) return;
            if (_listColorCol.Any(p => p.ToUpper().Trim() == fieldName.ToUpper().Trim()))
            {
                e.DisplayText = "";
            }
        }

        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.Column == null) return;
            string fieldName = e.Column.FieldName;
            if (_listColorCol.Count <= 0) return;
            if (_listColorCol.Any(p => p.ToUpper().Trim() == fieldName.ToUpper().Trim()))
            {
                string hexCode = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, e.Column));
                if (ProcessGeneral.CheckHexColorRegex(hexCode, false))
                {
                    Color color = hexCode.ConvertHexCodeToColor();
                    e.Appearance.BackColor = color;
                }
            }

        }

        private void gvMain_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!IsMultiSelected || gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect) return;
            DrawRectangleSelection.RePaintGridView(gv);
        }

        private void gvMain_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            var gv = sender as GridView;
            if (gv == null) return;
            qSelectedRow = gv.GetSelectedRows().ToList();
            if (!IsMultiSelected || gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect) return;
            DrawRectangleSelection.RePaintGridView(gv);
        }

        private void gvMain_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {

            var gv = sender as GridView;
            if (gv == null) return;
            if (!IsMultiSelected || gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect) return;
            DrawRectangleSelection.RePaintGridView(gv);
        }

        private void gvMain_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!IsMultiSelected || gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect) return;
            DrawRectangleSelection.RePaintGridView(gv);

        }
        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!IsMultiSelected || gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect) return;
            DrawRectangleSelection.RePaintGridView(gv);

        }

        private void gcMain_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            var gc = (GridControl)sender;
            if (gc == null) return;
            var gv = gc.FocusedView as GridView;
            if (gv == null) return;
            if (!IsMultiSelected || gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }

        private void gvMain_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.OptionsView.ShowAutoFilterRow || !gv.IsDataRow(e.RowHandle)) return;
            string filterCellText = gv.GetRowCellDisplayText(GridControl.AutoFilterRowHandle, e.Column);
            if (String.IsNullOrEmpty(filterCellText)) return;
            int filterTextIndex = e.DisplayText.IndexOf(filterCellText, StringComparison.CurrentCultureIgnoreCase);
            if (filterTextIndex == -1) return;
            XPaint.Graphics.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, filterCellText, e.Appearance, Color.Black, Color.Gold, false,
                filterTextIndex);
            e.Handled = true;
        }


        private void gvMain_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            if (!_isShowFooter) return;
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }

        private void gvMain_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (!IsShowFooter) return;
            if (lFooterFormat.Count <= 0) return;
            string fieldName = e.Column.FieldName.Trim();
            var q1 = lFooterFormat.AsEnumerable().Where(p => p.FieldName == fieldName).Select(p => new
            {
                p.SummaryHorzAlign
            }).ToList();
            if (!q1.Any()) return;
            Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            e.Appearance.ForeColor = Color.Chocolate;
            HorzAlignment horzAlignment = q1.Select(p => p.SummaryHorzAlign).FirstOrDefault();
            e.Appearance.TextOptions.HAlignment = horzAlignment;
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            e.Handled = true;
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            if (!IsMultiSelected)
            {
                goto formatRow;
            }
            if (gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect)
            {
                goto formatRow;
            }
            return;
            formatRow:

            if (gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect && _isAddColumnSelected)
            {
                if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, CheckBoxSelectedField)))
                {
                    e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                    e.HighPriority = true;
                    e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                    e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                    e.Appearance.GradientMode = LinearGradientMode.Horizontal;
                }

            }

            else if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }
        }


        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;


        }

        #endregion
    }
}