using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_AdminSys.Info;
using CNY_AdminSys.WForm;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FocusedColumnChangedEventArgs = DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs;

namespace CNY_AdminSys.UControl
{
    public partial class UCACF_Role : XtraUserControl
    {
        #region "Property"
        readonly Inf_Role _inf = new Inf_Role();
        private bool _disableEventWhenLoad = false;
        WaitDialogForm _dlg;
        PermissionFormInfo _perInfo;
       
        private DataTable _dtUser;
        private DataTable _dtRule;
        Dictionary<string,DataTable> _dicRule = new Dictionary<string, DataTable>();
        Dictionary<string, DataTable> _dicUser = new Dictionary<string, DataTable>();
        #endregion

        #region "Contructor"
        public UCACF_Role()
        {
            InitializeComponent();
            _dtUser = new DataTable();
            _dtRule = new DataTable();
            _perInfo = ProcessGeneral.GetPermissionByFormCode("UCACF_Role");
            GridViewCustomInit(gcMainAE, gvMainAE);
            GridViewTestCustomInit(gcRuleInRole, gvRuleInRole);
            GridViewTestCustomInit(gcUserInRole, gvUserInRole);
            GridViewTestCustomInit(gcRule, gvRule);
            GridViewTestCustomInit(gcUser, gvUser);
            this.Load += Control_Load;
            ProcessGeneral.SpinEditSetMinMaxValue(spinPriority, 0, Decimal.MaxValue, 0, false, false, "N0");
            txtCode.Properties.MaxLength = 10;


            bool isDrop = DeclareSystem.SysUserName.ToUpper() == "ADMIN" || _perInfo.PerIns || _perInfo.PerUpd || _perInfo.PerDel || DeclareSystem.SysUserName.ToUpper() == "DENNTX" ;

            gcRule.Enabled = isDrop;
            gcRuleInRole.Enabled = isDrop;
            gcUser.Enabled = isDrop;
            gcUserInRole.Enabled = isDrop;

         
            DragDropEvents[] arrDragDropEvents = { dragDropEvents1, dragDropEvents2, dragDropEvents3, dragDropEvents4 };

          


         
            foreach (DragDropEvents dragDropEvents in arrDragDropEvents)
            {
            
                dragDropEvents.DragDrop += Behavior_DragDrop;
                dragDropEvents.DragOver += Behavior_DragOver;
              

            }
            btnRole.Click += BtnRole_Click;


        }

       


        #endregion
        #region "Form Event"



        private void Control_Load(object sender, EventArgs e)
        {

            if (!_perInfo.PerIns && !_perInfo.PerUpd && !_perInfo.PerDel)
            {
                btnRole.Enabled = false;
            }
            else
            {
                btnRole.Enabled = true;
            }

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
        
            gc.ForceInitialize();
        }

        private DataTable TableRuleTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PermisionGroupCode", typeof(string));
            dt.Columns.Add("PermisionGroupName", typeof(string));
            dt.Columns.Add("PermisionGroupDescription", typeof(string));
            return dt;
        }
        private DataTable TableUserTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserID", typeof(Int64));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            return dt;
        }
        private void LoadDataInGridView(GridControl gc, GridView gv)
        {



            _dlg = new WaitDialogForm();

            _dtUser = _inf.GetListUser();
            _dtRule = _inf.GetListRule();


            DataTable dtS = _inf.GridViewData_Load();


            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Code", typeof(string));

            foreach (DataRow drTemp in dtS.Rows)
            {
                dtTemp.Rows.Add(ProcessGeneral.GetSafeString(drTemp["GroupUserCode"]));
            }

            _dicRule.Clear();
            _dicUser.Clear();

            DataTable dtUiR = _inf.LoadUserByRole(dtTemp);
            DataTable dtRiR = _inf.LoadRuleByRole(dtTemp);
            _dicUser = dtUiR.AsEnumerable().GroupBy(p => p.Field<string>("GroupUserCode")).Select(s => new
            {
                GroupUserCode = s.Key,
                ListField = s.Select(t => new 
                {
                    UserID = t.Field<Int64>("UserID"),
                    UserName = t.Field<String>("UserName"),
                    FullName = t.Field<String>("FullName"),
                    Email = t.Field<String>("Email"),
                }).OrderBy(t=>t.UserName).ToList().CopyToDataTableNew(),
            }).ToDictionary(s => s.GroupUserCode, s => s.ListField);

            _dicRule = dtRiR.AsEnumerable().GroupBy(p => p.Field<string>("GroupUserCode")).Select(s => new
            {
                GroupUserCode = s.Key,
                ListField = s.Select(t => new
                {
                    PermisionGroupCode = t.Field<String>("PermisionGroupCode"),
                    PermisionGroupName = t.Field<String>("PermisionGroupName"),
                    PermisionGroupDescription = t.Field<String>("PermisionGroupDescription"),
                }).OrderBy(t=>t.PermisionGroupCode).ToList().CopyToDataTableNew(),
            }).ToDictionary(s => s.GroupUserCode, s => s.ListField);


            if (gv.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in dtS.Columns)
                {
                    gv.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }


                ProcessGeneral.SetGridColumnHeader(gv.Columns["GroupUserCode"], "Code", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["GroupUserName"], "Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["GroupUserDescription"], "Description", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);

                ProcessGeneral.SetGridColumnHeader(gv.Columns["Priority"], "Priority", DefaultBoolean.True, HorzAlignment.Center, FixedStyle.None);
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
        
        #endregion

        #region "gridview event"

     
  

        private void BtnRole_Click(object sender, EventArgs e)
        {
            if (!_perInfo.PerIns && !_perInfo.PerUpd && !_perInfo.PerDel) return;
            FrmBase parentForm = (FrmBase)this.FindForm();
            if (parentForm == null) return;
            if (gvMainAE.RowCount == 0) return;
            int rHRole = gvMainAE.FocusedRowHandle;
            if (!gvMainAE.IsDataRow(rHRole)) return;
            string roleCode = ProcessGeneral.GetSafeString(gvMainAE.GetRowCellValue(rHRole, "GroupUserCode"));
            string roleName = ProcessGeneral.GetSafeString(gvMainAE.GetRowCellValue(rHRole, "GroupUserName"));

            if (gvRuleInRole.RowCount == 0) return;
            int rHRule = gvRuleInRole.FocusedRowHandle;
            if (!gvRuleInRole.IsDataRow(rHRule)) return;
            string ruleCode = ProcessGeneral.GetSafeString(gvRuleInRole.GetRowCellValue(rHRule, "PermisionGroupCode"));
            string ruleName = ProcessGeneral.GetSafeString(gvRuleInRole.GetRowCellValue(rHRule, "PermisionGroupName"));
            Int64 authorizationOnUserGroupId = _inf.Role_GetAuthorizationOnUserGroupID(roleCode, ruleCode);
            if (authorizationOnUserGroupId <= 0) return;

            DataTable dtUser = _inf.AuthorizationOnUser_LoadGridUser(roleCode);
            if (dtUser.Rows.Count <= 0) return;
            var f = new FrmRole_Edit(roleCode,
                roleName, ruleCode, ruleName, authorizationOnUserGroupId, dtUser, parentForm)
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

            XtraMessageBox.Show(string.Format("Could not delete Rule {0} \n because of an error in the process of deleting data", txtCode.Text.Trim()),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            return false;


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
                    XtraMessageBox.Show(string.Format("Insert Successful Role : {0}", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Role {0} have been already exists in system", txtCode.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                }
            }
            else
            {
                if (_inf.Update(ProcessGeneral.GetSafeString(txtCode.EditValue), ProcessGeneral.GetSafeString(txtName.EditValue)
                        , ProcessGeneral.GetSafeString(txtDesc.EditValue), ProcessGeneral.GetSafeInt(spinPriority.EditValue)) == 1)
                {
                    XtraMessageBox.Show(string.Format("Update Successful Role : {0}", txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Role {0} not exists in system", txtCode.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
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
                XtraMessageBox.Show("Role Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtCode.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(txtName.EditValue) == "")
            {
                XtraMessageBox.Show("Role Name could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtName.Focus();
                return false;
            }
            return true;
        }






        #endregion





        #region "Process Grid Rule In Role"
        #region "methold"

        private void GridViewTestCustomInit(GridControl gc, GridView gv)
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

            gv.OptionsSelection.MultiSelect = true;
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
  
            gv.RowStyle += gvTest_RowStyle;
            gv.RowCountChanged += gvTest_RowCountChanged;
            gv.CustomDrawRowIndicator += gvTest_CustomDrawRowIndicator;
           
           
            gc.ForceInitialize();
        }

     
        
      
        #endregion

        #region "gridview event"

      


       

        private void gvTest_RowCountChanged(object sender, EventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvTest_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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



        private void gvTest_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;


            }


        }



        #endregion

        #endregion







        #region
        private void DisplatDetailFocusedRowChanged(GridView gv)
        {
            int rH = gv.FocusedRowHandle;
            if (gv.RowCount == 0 || !gv.IsDataRow(rH))
            {
                txtCode.EditValue = string.Empty;
                txtName.EditValue = string.Empty;
                txtDesc.EditValue = string.Empty;
                spinPriority.EditValue = 0;
                LoadDataInGridViewUiR("");
            }
            else
            {
                string ruleCode = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "GroupUserCode"));
                txtCode.EditValue = ruleCode;
                txtName.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "GroupUserName"));
                txtDesc.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "GroupUserDescription"));
                spinPriority.EditValue = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(rH, "Priority"));
                LoadDataInGridViewRiR(ruleCode);
                LoadDataInGridViewUiR(ruleCode);
            }

        }
        private void LoadDataInGridViewRiR(string ruleCode)
        {






            DataTable dtS;
            if (!_dicRule.TryGetValue(ruleCode, out dtS))
            {
                dtS = TableRuleTemp();
            }

            var qS1 = _dtRule.AsEnumerable().Where(p => dtS.AsEnumerable().All(t => t.Field<string>("PermisionGroupCode") != p.Field<string>("PermisionGroupCode"))).OrderBy(p=> p.Field<string>("PermisionGroupCode")).ToList();

            DataTable dtS1 = qS1.Any() ? qS1.CopyToDataTable() : TableRuleTemp();

            if (gvRuleInRole.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in dtS.Columns)
                {
                    gvRuleInRole.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }


                ProcessGeneral.SetGridColumnHeader(gvRuleInRole.Columns["PermisionGroupCode"], "Code", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvRuleInRole.Columns["PermisionGroupName"], "Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvRuleInRole.Columns["PermisionGroupDescription"], "Description", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);

               

            }
            gvRuleInRole.BeginUpdate();




            //gc.DataSource = null;
            //gv.Columns.Clear();

            gcRuleInRole.DataSource = dtS;


            gvRuleInRole.BestFitColumns();
            gvRuleInRole.EndUpdate();









            if (gvRule.VisibleColumns.Count <= 0)
            {
                int ind1 = 0;
                foreach (DataColumn col1 in dtS.Columns)
                {
                    gvRule.Columns.Add(new GridColumn { FieldName = col1.ColumnName, VisibleIndex = ind1 });
                    ind1++;
                }


                ProcessGeneral.SetGridColumnHeader(gvRule.Columns["PermisionGroupCode"], "Code", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvRule.Columns["PermisionGroupName"], "Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvRule.Columns["PermisionGroupDescription"], "Description", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);



            }
            gvRule.BeginUpdate();




            //gc.DataSource = null;
            //gv.Columns.Clear();

            gcRule.DataSource = dtS1;


            gvRule.BestFitColumns();
            gvRule.EndUpdate();


        }



        private void LoadDataInGridViewUiR(string ruleCode)
        {



         


            DataTable dtS;
            if (!_dicUser.TryGetValue(ruleCode, out dtS))
            {
                dtS = TableUserTemp();
            }

            var qS1 = _dtUser.AsEnumerable().Where(p => dtS.AsEnumerable().All(t => t.Field<Int64>("UserID") != p.Field<Int64>("UserID"))).OrderBy(p=>p.Field<string>("UserName")).ToList();

            DataTable dtS1 = qS1.Any() ? qS1.CopyToDataTable() : TableRuleTemp();

            if (gvUserInRole.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in dtS.Columns)
                {
                    gvUserInRole.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }


                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["UserID"], "UserID", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["UserName"], "User Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["FullName"], "Full Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["Email"], "Email", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.HideVisibleColumnsGridView(gvUserInRole, false, "UserID");



            }
            gvUserInRole.BeginUpdate();




            //gc.DataSource = null;
            //gv.Columns.Clear();

            gcUserInRole.DataSource = dtS;


            gvUserInRole.BestFitColumns();
            gvUserInRole.EndUpdate();









            if (gvUser.VisibleColumns.Count <= 0)
            {
                int ind1 = 0;
                foreach (DataColumn col1 in dtS.Columns)
                {
                    gvUser.Columns.Add(new GridColumn { FieldName = col1.ColumnName, VisibleIndex = ind1 });
                    ind1++;
                }


                ProcessGeneral.SetGridColumnHeader(gvUser.Columns["UserID"], "UserID", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvUser.Columns["UserName"], "User Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvUser.Columns["FullName"], "Full Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.SetGridColumnHeader(gvUser.Columns["Email"], "Email", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
                ProcessGeneral.HideVisibleColumnsGridView(gvUser, false, "UserID");



            }
            gvUser.BeginUpdate();




            //gc.DataSource = null;
            //gv.Columns.Clear();

            gcUser.DataSource = dtS1;


            gvUser.BestFitColumns();
            gvUser.EndUpdate();


        }
        #endregion












        // Assigning a required content for each auto generated Document


        #region "Drag Drop Final"
       
        private Stream ReadImageFile(string path)
        {
            return File.OpenRead(path);
        }
        private Cursor SetDragCursor(DragDropActions e)
        {
            if (e == DragDropActions.Move)
                return new Cursor(ReadImageFile(Application.StartupPath + @"\move.ico"));
            if (e == DragDropActions.Copy)
                return new Cursor(ReadImageFile(Application.StartupPath + @"\copy.ico"));
            if (e == DragDropActions.None)
                return Cursors.No;
            return Cursors.Default;
        }


    
        Int32 GetDestRowHandle(GridView gv, Point hitPoint)
        {
            
               GridControl gc = gv.GridControl;
            Point pt = gc.PointToClient(hitPoint);
            GridHitInfo ht = gv.CalcHitInfo(pt);
            int rH = ht.RowHandle;
            if (ht.InRowCell && gv.IsDataRow(rH))
            {
                
                return rH;
            }
            return -1;

        }



  






        private void Behavior_DragOver(object sender, DragOverEventArgs e)
        {
            
        
            e.Default();
            Cursor current = Cursors.No;
            DragDropActions action = DragDropActions.None;
            if (e.InsertType == InsertType.None) goto finshed;
            if (e.Target == e.Source) goto finshed;


            GridView gvTarget = (GridView)e.Target;
            GridView gvSoucre = (GridView)e.Source;
            string tagTarget = ProcessGeneral.GetSafeString(gvTarget.Tag);
            string tagSoucre = ProcessGeneral.GetSafeString(gvSoucre.Tag);
            if (tagTarget != tagSoucre) goto finshed;
            if (gvTarget.RowCount <= 0) goto finshed1;


            int rH = GetDestRowHandle(gvTarget, e.Location);
            if (rH < 0) goto finshed;
            finshed1:
            GridControl gcTarget = gvTarget.GridControl;
            if (ProcessGeneral.GetSafeString(gcTarget.Tag) == "MOVE")
            {
                action = DragDropActions.Move;
                current = SetDragCursor(DragDropActions.Move);
            }

            else
            {
                action = DragDropActions.Move;
                current = new Cursor(ReadImageFile(Application.StartupPath + @"\delete.ico"));
            }
            finshed:

            e.Action = action;
            Cursor.Current = current;


        }

        private void Behavior_DragDrop(object sender, DragDropEventArgs e)
        {
            e.Handled = true;
            if (e.Action == DragDropActions.None || e.InsertType == InsertType.None)
            {
                goto finish;
            }
            int rhDest = -1;
            GridView gvTarget = (GridView)e.Target;
            GridView gvSource = (GridView)e.Source;

            if (gvTarget.RowCount > 0)
            {
                rhDest = GetDestRowHandle(gvTarget, e.Location);
            }

            int rcTarget = gvTarget.RowCount;
            if (rcTarget > 0 && rhDest < 0) goto finish;

            Int32[] arrRowSelected = (Int32[])e.Data;
            DataRow[] arrRowSelectedData = arrRowSelected.Select(p => gvSource.GetDataRow(p)).ToArray();
            if (e.Source == e.Target) goto finish;

            if (gvTarget.Name == "gvRuleInRole" || gvTarget.Name == "gvRule")
            {
                UpdateDataRuleInRole(gvSource, gvTarget, rhDest, arrRowSelectedData);
            }
            else
            {
                UpdateDataUserInRole(gvSource, gvTarget, rhDest, arrRowSelectedData);
            }




            finish:
            Cursor.Current = Cursors.Default;
        }

        private void UpdateDataRuleInRole(GridView gvSource, GridView gvTarget, int rH, DataRow[] arrRowSelected)
        {
            if (arrRowSelected.Length <= 0) return;
            GridControl gcSource = gvSource.GridControl;
            GridControl gcTarget = gvTarget.GridControl;
            DataTable dtS = (DataTable)gcSource.DataSource;
            DataTable dtT = (DataTable)gcTarget.DataSource;
            bool isAddRole = gvTarget.Name == "gvRuleInRole";
            int tarGetIndex = 0;
            if (gvTarget.RowCount > 0)
            {
                tarGetIndex = gvTarget.GetDataSourceRowIndex(rH) + 1;
            }

            List<string> lRule = new List<string>();
            foreach (DataRow drS in arrRowSelected)
            {
                lRule.Add(ProcessGeneral.GetSafeString(drS["PermisionGroupCode"]));
                DataRow drT = dtT.NewRow();
                drT["PermisionGroupCode"] = drS["PermisionGroupCode"];
                drT["PermisionGroupName"] = drS["PermisionGroupName"];
                drT["PermisionGroupDescription"] = drS["PermisionGroupDescription"];
                dtT.Rows.InsertAt(drT, tarGetIndex);
                dtS.Rows.Remove(drS);
                tarGetIndex++;
            }
            dtT.AcceptChanges();
            dtS.AcceptChanges();
            string userGroupCode = txtCode.Text.Trim();
            if (isAddRole)
            {
                foreach (string s in lRule)
                {
                    _inf.Role_InsertRule(userGroupCode, s);
                }


                /*
                string strGroupSuccess = "";
                string strGroupError = "";
                
                foreach (string s in lRule)
                {
                    if (_inf.Role_InsertRule(userGroupCode, s) == 1)
                    {
                        strGroupSuccess = string.Format("{0}{1},", strGroupSuccess, s);
                    }
                    else
                    {
                        strGroupError = string.Format("{0}{1},", strGroupError, s);
                    }
                }
                if (!string.IsNullOrEmpty(strGroupSuccess))
                {
                    strGroupSuccess = strGroupSuccess.Substring(0, strGroupSuccess.Length - 1);
                    XtraMessageBox.Show(string.Format("Insert successful rule : {0} into role {1} ...! ", strGroupSuccess, userGroupCode),
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
                if (!string.IsNullOrEmpty(strGroupError))
                {
                    strGroupError = strGroupError.Substring(0, strGroupError.Length - 1);
                    XtraMessageBox.Show(string.Format("Insert unsuccessful rule : {0} into role {1} ...! ", strGroupError, userGroupCode),
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                */

            }
            else
            {
                foreach (string s in lRule)
                {
                    _inf.Role_DeleteRule(userGroupCode, s);
                }
               // XtraMessageBox.Show(string.Format("Delete Successful {0} rule leave role {1} .!", lRule.Count, txtCode.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }

        }

        private void UpdateDataUserInRole(GridView gvSource, GridView gvTarget , int rH,  DataRow [] arrRowSelected)
        {
            if (arrRowSelected.Length <= 0) return;
            GridControl gcSource = gvSource.GridControl;
            GridControl gcTarget = gvTarget.GridControl;
            DataTable dtS = (DataTable)gcSource.DataSource;
            DataTable dtT = (DataTable)gcTarget.DataSource;
            bool isAddUser = gvTarget.Name == "gvUserInRole";
            int tarGetIndex = 0;
            if (gvTarget.RowCount > 0)
            {
                tarGetIndex = gvTarget.GetDataSourceRowIndex(rH) + 1;
            }

            List<Int64> lUser = new List<Int64>();
            foreach (DataRow drS in arrRowSelected)
            {
                lUser.Add(ProcessGeneral.GetSafeInt64(drS["UserID"]));
                DataRow drT = dtT.NewRow();
                drT["UserID"] = drS["UserID"];
                drT["UserName"] = drS["UserName"];
                drT["FullName"] = drS["FullName"];
                drT["Email"] = drS["Email"];
                dtT.Rows.InsertAt(drT, tarGetIndex);
                dtS.Rows.Remove(drS);
                tarGetIndex++;
            }
            dtT.AcceptChanges();
            dtS.AcceptChanges();
            string userGroupCode = txtCode.Text.Trim();
            if (isAddUser)
            {

                foreach (Int64 userId in lUser)
                {
                    _inf.Role_InsertUser(userGroupCode, userId);
                }
             //   XtraMessageBox.Show(string.Format("Insert Successful {0} users into group {1} .!", lUser.Count, userGroupCode), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            else
            {
                foreach (Int64 userId in lUser)
                {
                    _inf.Role_DeleteUser(userGroupCode, userId);
                }
              //  XtraMessageBox.Show(string.Format("Delete Successful {0} users leave group {1} .!", lUser.Count, userGroupCode), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }

        }




        #endregion
    }
}
