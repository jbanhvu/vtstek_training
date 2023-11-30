using System;
using System.Collections.Generic;
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
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CNY_BaseSys.WForm
{
    public partial class FrmTransferDataNew : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
        public event TransferDataOnGridViewHandler OnTransferData = null;
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

        public List<GridViewTransferDataColumnInit> ListGvColFormat { get; set; }
        public DataTable DtSource { get; set; }

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


        private bool _autoResizeForm = false;
        public bool AutoResizeForm { get { return this._autoResizeForm; } set { this._autoResizeForm = value; } }


        private Int32 _maxWidthForm = 0;
        public Int32 MaxWidthForm { get { return this._maxWidthForm; } set { this._maxWidthForm = value; } }


        private Int32 _bestFitRowCount = 0;
        public Int32 BestFitRowCount { get { return this._bestFitRowCount; } set { this._bestFitRowCount = value; } }


        private int _focusedColAutoRow = 0;
        public Int32 FocusedColAutoRow { get { return this._focusedColAutoRow; } set { this._focusedColAutoRow = value; } }


        #endregion


        #region "Contructor"
        public FrmTransferDataNew()
        {
            InitializeComponent();
            qSelectedRow = new List<int>();
            this.Load += Frm_Load;
        }


        #endregion



        #region "Form Event"


        private void Frm_Load(object sender, EventArgs e)
        {

            lFooterFormat = (ListGvColFormat.AsEnumerable().Where(p => p.SummayType != SummaryItemType.None)
                            .Select(p => new GridViewTransferFormatFooter
                            {
                                FieldName = p.FieldName,
                                SummaryHorzAlign = p.SummaryHorzAlign,
                            })).ToList();
            GridViewCustomInit();

            CreateEventOnGridView();

            gvMain.BeginUpdate();

            gcMain.DataSource = DtSource;




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
            if (_isShowFindPanel)
            {
                gvMain.ShowFindPanel();

                if (!string.IsNullOrEmpty(this._strFilter))
                {
                    gvMain.ApplyFindFilter(this._strFilter);
                }
                BeginInvoke(new MethodInvoker(gvMain.FocusFindEdit));
            }


            if (_autoResizeForm)
            {
                int width = gvMain.GetTotalWidth() + 10;
                if (_maxWidthForm == 0)
                {
                    this.Size = new Size(width, this.Size.Height);
                }
                else if (width >= _maxWidthForm)
                {
                    this.Size = new Size(width, this.Size.Height);
                }
                else
                {
                    this.Size = new Size(_maxWidthForm, this.Size.Height);
                }
            }


        }
        #endregion


        #region "Methold"



        /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
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
            //gvMain.OptionsFind.FindDelay = 0;
            //gvMain.OptionsFind.FindMode = FindMode.Always;
            new MyFindPanelFilterHelperNew(gvMain, true)
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
            int focusedRowHandle = view.FocusedRowHandle;
            GridHitInfo hi = view.CalcHitInfo(pt);

            int clickedRowHandle = hi.RowHandle;

            if (ShiftKeyIsPressed())
            {
                for (int i = focusedRowHandle + 1; i < clickedRowHandle + 1; i++)
                {
                    if (view.IsRowSelected(i))
                    {
                        view.UnselectRow(i);
                    }
                    else
                    {
                        view.SelectRow(i);
                    }
                }
            }
            if (CtrlKeyIsPressed())
            {
                if (view.IsRowSelected(clickedRowHandle))
                {
                    view.UnselectRow(clickedRowHandle);
                }
                else
                {
                    view.SelectRow(clickedRowHandle);
                }
            }
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
            gcMain.ForceInitialize();
            gvMain.RowCellStyle += gvMain_RowCellStyle;
            gvMain.CustomColumnDisplayText += gvMain_CustomColumnDisplayText;
        }








        private void SelectedData()
        {
            List<Int32> lR = gvMain.GetSelectedRows().Where(r => r != GridControl.AutoFilterRowHandle).ToList();


            if (lR.Count <= 0)
            {
                this.Close();
                return;
            }
            CreateEventGetSelected(lR);
        }

        #endregion


        #region "GridView Event"
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




        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.KeyCode == Keys.Enter)
            {
                SelectedData();
                return;
            }

            if (gv.OptionsSelection.MultiSelect &&
                gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect &&
                (e.KeyData == (Keys.ShiftKey | Keys.Shift) || e.KeyData == (Keys.Control | Keys.LButton | Keys.ShiftKey)))
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
                (e.KeyData == Keys.ShiftKey || e.KeyData == (Keys.LButton | Keys.ShiftKey)))
            {
                SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, false);
            }
        }

        private void gvMain_MouseDown(object sender, MouseEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.OptionsSelection.MultiSelect) return;
            GridHitInfo hi = gv.CalcHitInfo(e.Location);
            if (hi.Column == null) return;
            if (gv.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CheckBoxRowSelect) return;
            if (hi.Column.Name != GridView.CheckBoxSelectorColumnName) return;
            if (hi.InColumn) return;

            if (IsCheckBoxColumnClicked(gv, e))
            {
                SelectionRows(gv, e);
                ((DXMouseEventArgs)e).Handled = HandleColumnHeaderClick(gv, hi);

                return;
            }



            bool handled = HandleColumnHeaderClick(gv, hi);

            if (!handled)
            {
                int rH = hi.RowHandle;
                if (gv.IsDataRow(rH))
                {

                    var q2 = qSelectedRow.Where(p => p != rH).ToList();
                    if (qSelectedRow.Any(p => p == rH))
                    {
                        gv.UnselectRow(rH);

                    }
                    else
                    {
                        gv.SelectRow(rH);

                    }
                    foreach (var i in q2)
                    {
                        gv.SelectRow(i);
                    }
                }



            }
            ((DXMouseEventArgs)e).Handled = true;




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
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
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
            SelectedData();


        }



        public void CreateEventGetSelected(List<Int32> lR)
        {
            if (OnTransferData != null)
            {
                List<DataRow> returnRowsSelected = lR.Select(i => gvMain.GetDataRow(i)).ToList();
                OnTransferData(this, new TransferDataOnGridViewEventArgs
                {
                    ReturnRowsSelected = returnRowsSelected,
                });
                gvMain.ClearSelection();
            }
            this.Close();
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


        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Escape:
                    {
                        this.Close();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion

    }




}