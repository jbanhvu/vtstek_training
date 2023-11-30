using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using CNY_BaseSys.Common;
using CNY_WH.Common;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing.Drawing2D;
using DevExpress.Utils.Extensions;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;

namespace CNY_WH.WForm
{
    public partial class Frm_SDR_AddNewItem : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
        public event TransferDataOnGridViewItemHandler OnWizardFinish = null;
        RepositoryItemCheckEdit chkedit = new RepositoryItemCheckEdit();
        private List<Int32> qSelectedRow;
        #endregion
        #region "COnStructor"
        public Frm_SDR_AddNewItem(DataTable dtM)
        {

            InitializeComponent();
            qSelectedRow = new List<int>();
            GridViewPackingCustomInit();
            LoadDataGridViewItem(gcItemCode, gvItemCode, dtM);
            btnDownUp.Click += BtnDownUp_Click;
            btnUpDown.Click += BtnUpDown_Click;
            this.KeyDown += Frm_SDR_AddNewItem_KeyDown;
        }

        private void Frm_SDR_AddNewItem_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }

        private void BtnUpDown_Click(object sender, EventArgs e)
        {
            //SelectedData(true);
            SelectedData(false);
        }

        private void BtnDownUp_Click(object sender, EventArgs e)
        {
            SelectedData(false);
        }
        #endregion


        #region "Process Grid Packing"


        #region "GridView Methold"
        private void LoadDataGridViewItem(GridControl gc, GridView gv, DataTable dt)
        {

            gv.BeginUpdate();
            gc.DataSource = null;
            gv.Columns.Clear();
            gc.DataSource = dt;

            ProcessGeneral.HideVisibleColumnsGridView(gv, false, "PK", "PKCode", "UnitPK", "CustomerPK");
            // check box
            ProcessGeneral.SetGridColumnHeader(gv.Columns["Selected"], ".", DefaultBoolean.True, HorzAlignment.Center, FixedStyle.None);
            gv.Columns["Selected"].ColumnEdit = chkedit;
            gv.Columns["Selected"].OptionsColumn.AllowSort = DefaultBoolean.False;
            gv.Columns["Selected"].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;

            ProcessGeneral.SetGridColumnHeader(gv.Columns["CustomerName"], "Customer", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);

            //ProcessGeneral.SetGridColumnHeader(gv.Columns["ItemType"], "ItemType", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["ItemTypeName"], "ItemType Name", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["ProductSpec"], "ProductSpec", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            ////gv.Columns["Qty"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatCodePackingFactorDecimal(false, false);
            ////gv.Columns["Qty"].DisplayFormat.FormatType = FormatType.Numeric;
            ////gv.Columns["Qty"].DisplayFormat.FormatString = "#,0.#####";
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["Reference"], "Reference", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            ////gv.Columns["Factor_CNY001"].DisplayFormat.FormatType = FormatType.Numeric;
            ////gv.Columns["Factor_CNY001"].DisplayFormat.FormatString = "#,0.#####";

            //ProcessGeneral.SetGridColumnHeader(gv.Columns["ItemCode"], "Item Code", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["ItemName"], "Item Name", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["CreatedDate"], "Created Date", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["CreatedBy"], "Created By", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["CategoryGroup"], "Category Group", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["Group"], "Group", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["Dimension"], "Dimension", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            //ProcessGeneral.SetGridColumnHeader(gv.Columns["Unit"], "Unit", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            ////ProcessGeneral.SetGridColumnHeader(gv.Columns["CodeLevel"], "Code Level", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);
            ////ProcessGeneral.SetGridColumnHeader(gv.Columns["AllowChildSameLevel"], "AllowChildSameLevel", DefaultBoolean.False, HorzAlignment.Near, FixedStyle.None);



            gv.BestFitColumns();
            gv.Columns["Selected"].Width = 30;

            //if (gv.Columns["Qty"].Width < 100) gv.Columns["Qty"].Width = 50;

            gv.EndUpdate();
            //gv.CustomSummaryCalculate += Gv_CustomSummaryCalculate;
     

        }



        private void SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(GridView gv, bool value)
        {
            gv.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = value;

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
            if (hi.InRowCell && hi.Column.Name == GridView.CheckBoxSelectorColumnName &&
                (ShiftKeyIsPressed() || CtrlKeyIsPressed()))
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
            if (hi.InColumnPanel && hi.Column.Name == GridView.CheckBoxSelectorColumnName &&
                (ShiftKeyIsPressed() || CtrlKeyIsPressed()))
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

        private void SelectedData()
        {
            List<Int32> lR = gvItemCode.GetSelectedRows().Where(r => r != GridControl.AutoFilterRowHandle).ToList();


            if (lR.Count <= 0)
            {
                this.Close();
                return;
            }
            // CreateEventGetSelected(lR);
        }
        private void GridViewPackingCustomInit()
        {



            gcItemCode.UseEmbeddedNavigator = true;

            gcItemCode.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcItemCode.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcItemCode.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcItemCode.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcItemCode.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvItemCode.OptionsBehavior.Editable = true;
            gvItemCode.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvItemCode.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvItemCode.OptionsCustomization.AllowColumnMoving = false;
            gvItemCode.OptionsCustomization.AllowQuickHideColumns = true;

            gvItemCode.OptionsCustomization.AllowSort = false;

            gvItemCode.OptionsCustomization.AllowFilter = true;


            gvItemCode.OptionsView.ShowGroupPanel = false;
            gvItemCode.OptionsView.ShowIndicator = true;
            gvItemCode.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvItemCode.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvItemCode.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            gvItemCode.OptionsView.ShowAutoFilterRow = true;
            gvItemCode.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvItemCode.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvItemCode.OptionsNavigation.AutoFocusNewRow = true;
            gvItemCode.OptionsNavigation.UseTabKey = true;

            gvItemCode.OptionsSelection.MultiSelect = true;
            gvItemCode.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvItemCode.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DefaultBoolean.True;
            gvItemCode.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;
            gvItemCode.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            gvItemCode.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;
            gvItemCode.OptionsSelection.UseIndicatorForSelection = true;
            SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gvItemCode, false);

            gvItemCode.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvItemCode.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvItemCode.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvItemCode.OptionsView.EnableAppearanceEvenRow = false;
            gvItemCode.OptionsView.EnableAppearanceOddRow = false;
            gvItemCode.OptionsView.ShowFooter = false;
            gvItemCode.OptionsView.RowAutoHeight = true;
            gvItemCode.OptionsHint.ShowFooterHints = false;
            gvItemCode.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gvItemCode.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvItemCode.OptionsFind.AllowFindPanel = false;


            //gvItemCode.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvItemCode)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
             
            };

            gvItemCode.ShowingEditor += gvItemCode_ShowingEditor;
            gvItemCode.RowCountChanged += gvItemCode_RowCountChanged;
            gvItemCode.CustomDrawRowIndicator += gvItemCode_CustomDrawRowIndicator;

            //gvItemCode.RowCellStyle += gvItemCode_RowCellStyle;

            gvItemCode.RowStyle += gvItemCode_RowStyle;
            gvItemCode.LeftCoordChanged += gvItemCode_LeftCoordChanged;
            gvItemCode.MouseMove += gvItemCode_MouseMove;
            gvItemCode.TopRowChanged += gvItemCode_TopRowChanged;
            gvItemCode.FocusedColumnChanged += gvItemCode_FocusedColumnChanged;
            gvItemCode.FocusedRowChanged += gvItemCode_FocusedRowChanged;
            gcItemCode.Paint += gcItemCode_Paint;
            gvItemCode.KeyDown += gvItemCode_KeyDown;
            gvItemCode.KeyUp += gvItemCode_KeyUp;
            gvItemCode.MouseDown += gvItemCode_MouseDown;
            gvItemCode.GroupLevelStyle += gvItemCode_GroupLevelStyle;

            gcItemCode.ProcessGridKey += gcItemCode_ProcessGridKey; // check chọn
            gvItemCode.Click += gvItemCode_Click; // check chọn all
            gvItemCode.CustomDrawColumnHeader += gvItemCode_CustomDrawColumnHeader; // check chọn all

            //gcItemCode.EditorKeyDown += gcItemCode_EditorKeyDown;
            //gcItemCode.KeyDown += gcItemCode_KeyDown;

            //gvItemCode.CellValueChanged += gvItemCode_CellValueChanged;
            //gvItemCode.ShownEditor += gvItemCode_ShownEditor;
            //gvItemCode.HiddenEditor += gvItemCode_HiddenEditor;
            gcItemCode.ForceInitialize();



        }

        private void gcItemCode_ProcessGridKey(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Space)
            {
                switch (gvItemCode.FocusedColumn.FieldName)
                {
                    case "Selected":
                        bool selected = ProcessGeneral.GetSafeBool(gvItemCode.GetRowCellValue(gvItemCode.FocusedRowHandle, "Selected"));
                        gvItemCode.SetRowCellValue(gvItemCode.FocusedRowHandle, "Selected", !selected);
                        break;

                }
            }
        }

        private void gvItemCode_Click(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            Point clickPoint = gcItemCode.PointToClient(Control.MousePosition);
            GridHitInfo hitInfo = gv.CalcHitInfo(clickPoint);
            if (hitInfo.InColumn && hitInfo.Column.FieldName == "Selected")
            {
                if (CheckboxHeaderCell.GetCheckedCount(gv) == gv.DataRowCount)
                    CheckboxHeaderCell.UnChekAll(gv);
                else
                    CheckboxHeaderCell.CheckAll(gv);
            }
            if (hitInfo.InRowCell)
            {
                if (hitInfo.RowHandle >= 0 || hitInfo.Column != null)
                {
                    if (hitInfo.Column.FieldName == "Selected")
                    {
                        if (Convert.ToBoolean(gv.GetRowCellValue(hitInfo.RowHandle, hitInfo.Column)))
                        {
                            gv.SetRowCellValue(hitInfo.RowHandle, hitInfo.Column, false);
                        }
                        else
                        {
                            gv.SetRowCellValue(hitInfo.RowHandle, hitInfo.Column, true);
                        }
                        gv.RefreshData();
                    }
                }

            }
        }
        private void gvItemCode_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.Column == gv.Columns["Selected"])
            {
                e.Info.InnerElements.Clear();
                e.Info.Appearance.ForeColor = Color.Blue;
                e.Painter.DrawObject(e.Info);
                CheckboxHeaderCell.DrawCheckBox(chkedit, e.Graphics, e.Bounds, CheckboxHeaderCell.GetCheckedCount(gv) == gv.DataRowCount);
                e.Handled = true;
            }
        }
        //=== End Check Box


        private void gvItemCode_MouseMove(object sender, MouseEventArgs e) //draw rectangle cell secltion
        {
            var gv = sender as GridView;
            if (gv == null) return;
            qSelectedRow = gv.GetSelectedRows().ToList();
        }
        private void gvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.KeyCode == Keys.Enter)
            {
                //SelectedData();
                return;
            }

            if ((e.KeyData == (Keys.ShiftKey | Keys.Shift) || e.KeyData == (Keys.Control | Keys.LButton | Keys.ShiftKey)))
            {
                SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, true);
            }
        }

        private void gvItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            if ((e.KeyData == Keys.ShiftKey || e.KeyData == (Keys.LButton | Keys.ShiftKey)))
            {
                SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, false);
            }
        }
        private void gvItemCode_MouseDown(object sender, MouseEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;

            GridHitInfo hi = gv.CalcHitInfo(e.Location);
            if (hi.Column == null) return;
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


        private void gvItemCode_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            e.LevelAppearance.BackColor = Color.LemonChiffon;
        }
        private void gvItemCode_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
            e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }

        private void gvItemCode_HiddenEditor(object sender, EventArgs e)
        {

        }

        private void gvItemCode_ShownEditor(object sender, EventArgs e)
        {
            GridView Gv = sender as GridView;
            if (Gv == null) return;

            //if (Gv.ActiveEditor != null)
            //{
            //    object value = Gv.ActiveEditor.EditValue;
            //    a = value;
            //}
        }

        private Int32 GetSortOrderFieldControlD(bool isAttribute)
        {
            if (isAttribute) return 3;
            return 2;
        }

        // private string[] _arrFieldEdit = new[] { "RMCode_001", "Position", "UC", "Factor", "UCUnit", "Tolerance", "Note", "PercentUsing", "BOMLineType" }; 






    



        private string GetAttName(string fieldName)
        {
            string[] arr = fieldName.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 2)
                return arr[1].Trim();
            return fieldName;
        }

    


    


        private void ChangeStatusRow(GridView gv, string fieldRowSate, int rH)
        {

            if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fieldRowSate)) == DataStatus.Unchange.ToString())
            {
                gv.SetRowCellValue(rH, fieldRowSate, DataStatus.Update.ToString());
            }
        }








        #endregion

        #region "GridView Event"

 

        private void gvItemCode_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
            e.Cancel = true;

        }


        private void gvItemCode_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.Column == null) return;
            string fieldName = e.Column.FieldName;
            //if (_listColorCol.Count <= 0) return;
            //if (_listColorCol.Any(p => p.ToUpper().Trim() == fieldName.ToUpper().Trim()))
            //{
                string hexCode = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, e.Column));
                if (ProcessGeneral.CheckHexColorRegex(hexCode, false))
                {
                    Color color = hexCode.ConvertHexCodeToColor();
                    e.Appearance.BackColor = color;
                }
            //}

        }

        private void gvItemCode_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        //private void gvItemCode_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        //{
        //    GridView gv = (GridView)sender;
        //    if (gv == null) return;
        //    DrawRectangleSelection.RePaintGridView(((GridView)sender));
        //}

        private void gvItemCode_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvItemCode_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvItemCode_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcItemCode_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvItemCode_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvItemCode_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;

            LinearGradientBrush backBrush;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "RowStateColor"));
            if (rowState == DataStatus.Insert.ToString())
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                if (rowState == DataStatus.Update.ToString())
                {
                    backBrush = new LinearGradientBrush(e.Bounds, Color.Aquamarine, Color.Azure, 90);
                }
                else
                {
                    backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
                }
            }
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());

        }
  


        private void gcItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            if (gc == null) return;
            var gv = gc.FocusedView as GridView;
            if (gv == null) return;
            GridColumn gColF = gv.FocusedColumn;
            string fieldName = gColF.FieldName;
            int visibleIndex = gColF.VisibleIndex;
            int rH = gv.FocusedRowHandle;
            //_checkKeyDown = true;




        }








        #endregion




        #endregion

        private void SelectedData(bool isUpdown)
        {
            //List<Int32> lR = gvItemCode.GetSelectedRows().Where(r => r != GridControl.AutoFilterRowHandle).ToList();

            
            List<DataRow> q1 = Enumerable.Range(0, gvItemCode.RowCount)
                     .Where(p => !gvItemCode.IsGroupRow(p) && p != GridControl.AutoFilterRowHandle && ProcessGeneral.GetSafeBool(gvItemCode.GetRowCellValue(p,"Selected")))
                     .Select(p => gvItemCode.GetDataRow(p))
                     .ToList();
            if (!q1.Any())
            {
                this.Close();
                return;
            }
            //if (lR.Count <= 0)
            //{
            //    this.Close();
            //    return;
            //}
            CreateEventGetSelected(q1, isUpdown);
        }
        public void CreateEventGetSelected(List<DataRow> lR, bool isUpdown)
        {
            if (OnWizardFinish != null)
            {

                List<DataRow> returnRowsSelected = lR;
                List<DataRow> returnRowsSelectedNew;
                if (isUpdown)
                {
                    //returnRowsSelected.OrderByDescending(p => p.Field<Int64>("PKCode"));
                    returnRowsSelectedNew = returnRowsSelected.OrderByDescending(p => p.Field<Int64>("PKCode")).ToList();
                }
                else
                {
                    returnRowsSelectedNew = returnRowsSelected.OrderBy(p => p.Field<Int64>("PKCode")).ToList();

                }

                OnWizardFinish(this, new Common.TransferDataOnGridViewEventArgs
                {
                    ReturnRowsSelected = returnRowsSelectedNew,
                });
            }
            this.Close();
        }
        public void CreateEventGetSelected(List<Int32> lR, bool isUpdown)
        {
            if (OnWizardFinish != null)
            {
               
                List<DataRow> returnRowsSelected = lR.Select(i => gvItemCode.GetDataRow(i)).ToList();
                List<DataRow> returnRowsSelectedNew;
                if (isUpdown)
                {
                    //returnRowsSelected.OrderByDescending(p => p.Field<Int64>("PKCode"));
                     returnRowsSelectedNew = returnRowsSelected.OrderByDescending(p => p.Field<Int64>("PKCode")).ToList();
                }
                else
                {
                    returnRowsSelectedNew =  returnRowsSelected.OrderBy(p => p.Field<Int64>("PKCode")).ToList();
   
                }

                OnWizardFinish(this, new Common.TransferDataOnGridViewEventArgs
                {
                    ReturnRowsSelected = returnRowsSelectedNew,
                });
            }
            this.Close();
        }
    }
}