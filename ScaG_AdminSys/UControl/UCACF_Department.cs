﻿using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CNY_AdminSys.Info;
using CNY_BaseSys.Common;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
namespace CNY_AdminSys.UControl
{


    public partial class UCACF_Department : XtraUserControl
    {
        #region "Property"

        readonly Inf_Department _inf = new Inf_Department();
        private bool _disableEventWhenLoad = false;
        WaitDialogForm _dlg;


        #endregion

        #region "Contructor"
        public UCACF_Department()
        {
            InitializeComponent();

            GridViewCustomInit(gcMainAE, gvMainAE);

            this.Load += Control_Load;
        
            ProcessGeneral.LoadSearchLookup(slueComp, _inf.ListComboCompany_Load(), "Code", "Code");

            slueComp.EditValueChanged += SlueBaseCurrency_EditValueChanged;
            txtCode.Properties.MaxLength = 10;
            txtCode.Properties.CharacterCasing = CharacterCasing.Upper;
        }


        #endregion



        #region "Use Button Control Input Empty Control Input"
        private void ClearTextControl()
        {
            txtCode.EditValue = "";
            txtName.EditValue = "";
            slueComp.EditValue = "";
        }


        private void UseControlInput(string option)
        {
            if (option == "Load")
            {
                txtCode.Properties.ReadOnly = true;
                txtName.Properties.ReadOnly = true;
                slueComp.Properties.ReadOnly = true;
            }
            else
            {
                txtCode.Properties.ReadOnly = option == "Edit";

                txtName.Properties.ReadOnly = false;
                slueComp.Properties.ReadOnly = false;
            }


            txtCompDesc.Enabled = false;
        }

        #endregion

        #region "Form Event"
        private void Control_Load(object sender, EventArgs e)
        {


            UseControlInput("Load");
            _disableEventWhenLoad = true;
            LoadDataInGridView(gcMainAE, gvMainAE);

            DisplatDetailFocusedRowChanged(gvMainAE);
            _disableEventWhenLoad = false;
        }
        #endregion







        #region "methold"

        /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
        private void GridViewCustomInit(GridControl gc, GridView gv)
        {







            gc.UseEmbeddedNavigator = true;

            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Append.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gv.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gv.OptionsCustomization.AllowColumnMoving = false;
            gv.OptionsCustomization.AllowQuickHideColumns = true;

            gv.OptionsCustomization.AllowSort = true;

            gv.OptionsCustomization.AllowFilter = true;

            gv.OptionsView.AllowCellMerge = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowIndicator = true;
            gv.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gv.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gv.OptionsView.ShowAutoFilterRow = false;
            gv.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gv.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gv.OptionsNavigation.AutoFocusNewRow = true;
            gv.OptionsNavigation.UseTabKey = true;

            gv.OptionsSelection.MultiSelect = false;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gv.OptionsSelection.EnableAppearanceFocusedRow = false;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;


            //gv.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            //gv.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;
            //SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, false);





            gv.OptionsView.EnableAppearanceEvenRow = false;
            gv.OptionsView.EnableAppearanceOddRow = false;
            gv.OptionsView.ShowFooter = false;

            gv.OptionsHint.ShowFooterHints = false;
            gv.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gv.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gv.OptionsFind.AllowFindPanel = true;

            gv.OptionsFind.AlwaysVisible = false;





            gv.OptionsFind.ShowCloseButton = true;
            gv.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gv)
            {
                IsPerFormEvent = true,
                IsDrawFilter = true,
            };




            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;











            //     gvMainAE.OptionsHint.ShowCellHints = true;



            //   gv.MouseMove += gv_MouseMove;
            gv.FocusedRowChanged += gv_FocusedRowChanged;
            gv.RowStyle += gv_RowStyle;
            gv.RowCountChanged += gv_RowCountChanged;
            gv.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gv.FocusedColumnChanged += gv_FocusedColumnChanged;
            //gv.KeyUp += gv_KeyUp;
            //gv.MouseDown += gv_MouseDown;
            //  ProcessGeneral.HideVisibleColumnsGridView(gvMainAE, false, "CC46PK");
            gc.ForceInitialize();
        }
        /*
          private void SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(GridView gv, bool value)
        {
            gv.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = value;

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
        private bool CtrlKeyIsPressed()
        {
            return Control.ModifierKeys == Keys.Control;
        }
        private bool ShiftKeyIsPressed()
        {
            return Control.ModifierKeys == Keys.Shift;
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

                    private void gv_KeyUp(object sender, KeyEventArgs e)
        {
            var gv = (GridView) sender;
            if (gv == null) return;

            if (e.KeyData == Keys.ShiftKey || e.KeyData == (Keys.LButton | Keys.ShiftKey))
            {
                SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, false);
            }
        }

        private void gv_MouseDown(object sender, MouseEventArgs e)
        {

            var gv = (GridView)sender;
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

        private void gv_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            qSelectedRow = gv.GetSelectedRows().ToList();
        }
         */

        private void LoadDataInGridView(GridControl gc, GridView gv)
        {
            _dlg = new WaitDialogForm();
            DataTable dtS = _inf.GridViewData_Load();

            if (gv.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in dtS.Columns)
                {
                    gv.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }
                ProcessGeneral.SetGridColumnHeader(gv.Columns["CompanyCode"], "Comp Code", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["CompanyName"], "Comp Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["DepartmentCode"], "Code", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["DepartmentName"], "Name.", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            }


            gv.BeginUpdate();



            //gc.DataSource = null;
            //gv.Columns.Clear();

            gc.DataSource = dtS;



         
            gv.BestFitColumns();
            gv.EndUpdate();

            _dlg.Close();
        }
        private void DisplatDetailFocusedRowChanged(GridView gv)
        {




            if (gv.RowCount == 0 || !gv.IsDataRow(gv.FocusedRowHandle))
            {
                txtCode.EditValue = "";
                txtName.EditValue = "";
                slueComp.EditValue = "";
            }
            else
            {
                DataRow dr = gv.GetDataRow(gv.FocusedRowHandle);

                txtCode.EditValue = ProcessGeneral.GetSafeString(dr["DepartmentCode"]);
                txtName.EditValue = ProcessGeneral.GetSafeString(dr["DepartmentName"]);
                slueComp.EditValue = ProcessGeneral.GetSafeString(dr["CompanyCode"]);


            }

        }
        #endregion

        #region "gridview event"




        private void gv_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged(gv);
            }
        }
        private void gv_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged(gv);
            }

        }

        private void gv_RowCountChanged(object sender, EventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
            bool selected = gv.IsRowSelected(e.RowHandle);
            if (selected)
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }

            if (selected)
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



        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (gv.FocusedRowHandle == e.RowHandle)
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;


            }


        }
        #endregion











        #region "override button menubar click"

        /// <summary>
        ///    Perform when click Add button 
        /// </summary>
        public bool PerformAdd()
        {
            UseControlInput("Add");
            ClearTextControl();
            gcMainAE.Enabled = false;
            txtCode.Focus();
            return true;

        }



        public bool PerformEdit()
        {
            if (gvMainAE.RowCount == 0 || !gvMainAE.IsDataRow(gvMainAE.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return false;
            }


            UseControlInput("Edit");
            gcMainAE.Enabled = false;
            txtName.Focus();
            return true;
        }


        public bool PerformDelete()
        {
            if (ProcessGeneral.GetSafeString(txtCode.EditValue) == "")
            {
                XtraMessageBox.Show("No row is selected to perform deleting", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return false;
            }
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to delete department code {0} !!!?? (yes/No) \n Note:You could not restore this record!", txtCode.Text.Trim()), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return false;
            if (_inf.Delete(ProcessGeneral.GetSafeString(txtCode.EditValue)) == 1)
            {
                XtraMessageBox.Show(string.Format("Department Code {0} has been deleted from the database", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                _disableEventWhenLoad = true;
                LoadDataInGridView(gcMainAE, gvMainAE);
                DisplatDetailFocusedRowChanged(gvMainAE);
                _disableEventWhenLoad = false;
                return true;
            }
            else
            {
                XtraMessageBox.Show(string.Format("Could not delete Department code {0} \n because of an error in the process of deleting data", txtCode.Text.Trim()),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }



        }

        /// <summary>
        /// Perform when click save button
        /// </summary>
        public bool PerformSave(string option)
        {

            if (!CheckInputControlBeforSave())
                return false;
            bool isSuccess = false;
            if (option.ToUpper() == "ADD")
            {
                if (_inf.Insert(txtCode.Text.Trim(), txtName.Text.Trim(),ProcessGeneral.GetSafeString(slueComp.EditValue)) == 1)
                {
                    XtraMessageBox.Show(string.Format("Insert Successful Company Code : {0}", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Company Code {0} have been already exists in system", txtCode.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                if (_inf.Update(txtCode.Text.Trim(), txtName.Text.Trim(), ProcessGeneral.GetSafeString(slueComp.EditValue)) == 1)
                {
                    XtraMessageBox.Show(string.Format("Update Successful Company Code : {0}", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Company Code {0} not exists in system", txtCode.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
            }
            if (!isSuccess) return false;


            UseControlInput("Load");
            gcMainAE.Enabled = true;

            _disableEventWhenLoad = true;
            LoadDataInGridView(gcMainAE, gvMainAE);
            DisplatDetailFocusedRowChanged(gvMainAE);
            _disableEventWhenLoad = false;
            return true;
        }

        /// <summary>
        /// Perform when click cancel button
        /// </summary>     
        public bool PerformCancel()
        {


            UseControlInput("Load");
            gcMainAE.Enabled = true;

            _disableEventWhenLoad = true;
            DisplatDetailFocusedRowChanged(gvMainAE);
            _disableEventWhenLoad = false;
            return true;


        }
        /// <summary>
        /// Perform when click Refresh button
        /// </summary>
        public bool PerformRefresh(string option)
        {
            switch (option.ToUpper())
            {
                case "":
                    {

                        _disableEventWhenLoad = true;
                        LoadDataInGridView(gcMainAE, gvMainAE);
                        DisplatDetailFocusedRowChanged(gvMainAE);
                        _disableEventWhenLoad = false;

                    }
                    break;
                case "ADD":
                    {
                        ClearTextControl();
                    }
                    break;
                case "EDIT":
                    {
                        _disableEventWhenLoad = true;
                        DisplatDetailFocusedRowChanged(gvMainAE);
                        _disableEventWhenLoad = false;
                    }
                    break;
                default: break;
            }

            return true;
        }



        #endregion

        #region "Update Data"
        private bool CheckInputControlBeforSave()
        {
            if (ProcessGeneral.GetSafeString(txtCode.EditValue) == "")
            {
                XtraMessageBox.Show("Department Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtCode.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(txtName.EditValue) == "")
            {
                XtraMessageBox.Show("Department Name could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtName.Focus();
                return false;
            }

            if (ProcessGeneral.GetSafeString(slueComp.EditValue) == "")
            {
                XtraMessageBox.Show("Company Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                slueComp.Focus();
                return false;
            }
            return true;
        }

        #endregion






        #region "Process Combobox"

        private void SlueBaseCurrency_EditValueChanged(object sender, EventArgs e)
        {
            var sLe = (SearchLookUpEdit)sender;
            if (sLe == null) return;
            txtCompDesc.EditValue = ProcessGeneral.GetSafeString(sLe.EditValue) != "" ? ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(sLe)["Description"]) : string.Empty;
        }

      

        #endregion
    }
}
