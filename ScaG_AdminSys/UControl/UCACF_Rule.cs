using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CNY_AdminSys.Info;
using CNY_AdminSys.WForm;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;

namespace CNY_AdminSys.UControl
{
    public partial class UCACF_Rule : XtraUserControl
    {
        #region "Property"
        readonly Inf_Rule _inf = new Inf_Rule();
        private bool _disableEventWhenLoad = false;
        WaitDialogForm _dlg;
        PermissionFormInfo _perInfo;
        #endregion

        #region "Contructor"
        public UCACF_Rule()
        {
            InitializeComponent();
            _perInfo = ProcessGeneral.GetPermissionByFormCode("UCACF_Rule");
            GridViewCustomInit(gcMainAE, gvMainAE);
            this.Load += Control_Load;
            ProcessGeneral.SpinEditSetMinMaxValue(spinPriority, 0, Decimal.MaxValue, 0, false, false, "N0");
            txtCode.Properties.MaxLength = 10;
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

        #region "Use Button Control Input Empty Control Input"
        private void ClearTextControl()
        {
            txtCode.EditValue = "";
            txtName.EditValue = "";
            txtDesc.EditValue = "";
            spinPriority.EditValue = 0;
        }


        private void UseControlInput(string option)
        {
            if (option == "Load")
            {
                txtCode.Enabled = false;
                txtName.Enabled = false;
                txtDesc.Enabled = false;
                spinPriority.Enabled = false;
            }
            else
            {

                txtCode.Enabled = option == "Add";
                txtName.Enabled = true;
                txtDesc.Enabled = true;
                spinPriority.Enabled = true;
            }

        }

        #endregion

        #region "methold"

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
            gv.KeyDown += Gv_KeyDown;
            gv.DoubleClick += Gv_DoubleClick;
            gc.ForceInitialize();
        }
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
                

                ProcessGeneral.SetGridColumnHeader(gv.Columns["PermisionGroupCode"], "Code", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["PermisionGroupName"], "Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["PermisionGroupDescription"], "Description", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);

                ProcessGeneral.SetGridColumnHeader(gv.Columns["Priority"], "Priority", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                gv.Columns["Priority"].DisplayFormat.FormatType = FormatType.Numeric;
                gv.Columns["Priority"].DisplayFormat.FormatString = "N0";
             
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
            int rH = gv.FocusedRowHandle;
            if (gv.RowCount == 0 || !gv.IsDataRow(rH))
            {
                txtCode.EditValue = string.Empty;
                txtName.EditValue = string.Empty;
                txtDesc.EditValue = string.Empty;
                spinPriority.EditValue = 0;
            }
            else
            {
                txtCode.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "PermisionGroupCode"));
                txtName.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "PermisionGroupName"));
                txtDesc.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "PermisionGroupDescription"));
                spinPriority.EditValue = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(rH, "Priority"));
            }

        }
        #endregion

        #region "gridview event"

        private void Gv_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (e.KeyCode == Keys.Enter)
            {
                UpdateMenuInRule(gv, gv.FocusedRowHandle);
            }

        }

        private void Gv_DoubleClick(object sender, EventArgs e)
        {


            var gv = (GridView)sender;
            if (gv == null) return;
            GridControl gc = gv.GridControl;
            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
            if (!hi.InRowCell) return;
            UpdateMenuInRule(gv, hi.RowHandle);

        }

        private void UpdateMenuInRule(GridView gv, int rH)
        {

            if (gv.RowCount == 0) return;
            if (!gv.IsDataRow(rH)) return;
            if (!_perInfo.PerIns && !_perInfo.PerUpd && !_perInfo.PerDel) return;

            FrmBase parentForm = (FrmBase)this.FindForm();
            if (parentForm == null) return;

            var f = new FrmRule_Edit(ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "PermisionGroupCode")),
                ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "PermisionGroupName")), parentForm)
            {
                MdiParent = parentForm.ParentForm,
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterScreen
            };
            f.SetDefaultCommandAndPermission(parentForm);
            f.Show();

        }

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

        public bool PerformAdd()
        {
            UseControlInput("Add");
            ClearTextControl();
            gcMainAE.Enabled = false;
            txtCode.Focus();
            return true;

        }


        /// <summary>
        /// Perform when click edit button
        /// </summary>
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

          
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to delete rule {0} !!!?? (yes/No) \n Note:You could not restore this record!", txtCode.Text.Trim()), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return false;
            if (_inf.Delete(txtCode.Text.Trim()) == 1)
            {
                XtraMessageBox.Show(string.Format("Rule {0} has been deleted from the database", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                _disableEventWhenLoad = true;
                LoadDataInGridView(gcMainAE, gvMainAE);
                DisplatDetailFocusedRowChanged(gvMainAE);
                _disableEventWhenLoad = false;
                return true;
            }
            else
            {
                XtraMessageBox.Show(string.Format("Could not delete Rule {0} \n because of an error in the process of deleting data", txtCode.Text.Trim()),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }



        }


        public bool PerformSave(string option)
        {

            if (!CheckInputControlBeforSave())
                return false;
            bool isSuccess = false;
            if (option.ToUpper() == "ADD")
            {
                if (_inf.Insert(ProcessGeneral.GetSafeString(txtCode.EditValue), ProcessGeneral.GetSafeString(txtName.EditValue)
                        , ProcessGeneral.GetSafeString(txtDesc.EditValue), ProcessGeneral.GetSafeInt(spinPriority.EditValue)) == 1)
                {
                    XtraMessageBox.Show(string.Format("Insert Successful Rule : {0}", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Rule {0} have been already exists in system", txtCode.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
               
                }
            }
            else
            {
                if (_inf.Update(ProcessGeneral.GetSafeString(txtCode.EditValue), ProcessGeneral.GetSafeString(txtName.EditValue)
                        , ProcessGeneral.GetSafeString(txtDesc.EditValue), ProcessGeneral.GetSafeInt(spinPriority.EditValue)) == 1)
                {
                    XtraMessageBox.Show(string.Format("Update Successful Rule : {0}", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Rule {0} not exists in system", txtCode.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
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
        public bool PerformCancel()
        {


            UseControlInput("Load");
            gcMainAE.Enabled = true;

            _disableEventWhenLoad = true;
            DisplatDetailFocusedRowChanged(gvMainAE);
            _disableEventWhenLoad = false;
            return true;


        }
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
                XtraMessageBox.Show("Permission Group Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtCode.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(txtName.EditValue) == "")
            {
                XtraMessageBox.Show("Permission Group Name could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtName.Focus();
                return false;
            }
            return true;
        }
      
        #endregion

      
    }
}
