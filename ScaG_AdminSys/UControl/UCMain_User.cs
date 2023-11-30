using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CNY_AdminSys.Info;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;

namespace CNY_AdminSys.UControl
{
    public partial class UCMain_User : XtraUserControl
    {


        #region "Property"
        private string _strFilter = "";
        public string StrFilter
        {
            get
            {
                return _strFilter;
            }
            set
            {
                _strFilter = value;
            }
        }
        private WaitDialogForm _dlg;
        public TextEdit TxtSearchp
        {
            get
            {
                return this.txtSearch;
            }

        }
        private DataTable _dt;
        //private readonly GridViewControlPagingFinal _tp;
        readonly Inf_User _inf = new Inf_User();

        public GridView gvMainP
        {
            get
            {
                return this.gvMain;
            }
        }

        public CheckEdit chkAllP
        {
            get
            {
                return this.chkAll;
            }
        }

        public CheckEdit chkWorkingP
        {
            get
            {
                return this.chkWorking;
            }
        }
        public CheckEdit chkWorkOffP
        {
            get
            {
                return this.chkWorkOff;
            }
        }
        #endregion
        public UCMain_User()
        {
            InitializeComponent();
            GridViewCustomInit();
            this._strFilter = "";

            this.Load += new EventHandler(UCMain_User_Load);


            //var lbFit = new List<BestFitColumnPaging>();
            ////var lbFitItem1 = new BestFitColumnPaging { FiledName = "Customer", IncreaseWidth = 64 };
            ////lbFit.Add(lbFitItem1);
            //_tp = new GridViewControlPagingFinal(gcMain, gvMain, lblDisplayInfo, lblCurrentRecord, txtPageSize, btnFirstPage, btnPreviousPage, btnNextPage, btnLastPage, lbFit);

            btnSearch.Click += new EventHandler(btnSearch_Click);
            txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);
            chkWorking.Click += chkWorking_Click;
            chkWorkOff.Click += chkWorkOff_Click;
            chkAll.Click += chkAll_Click;
        }

        private void chkWorking_Click(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            chkWorkOff.Checked = false;
            //if (!chkWorking.Checked)
            //{
            chkWorking.Checked = true;
            //}
            this._strFilter = " where emp.CNY027 =0 ";
            UpdateDataForGridView();
        }

        private void chkAll_Click(object sender, EventArgs e)
        {
            chkWorking.Checked = false;
            chkWorkOff.Checked = false;
            //if (!chkAll.Checked)
            //{
            chkAll.Checked = true;
            //}
            this._strFilter = string.Empty;
            UpdateDataForGridView();
        }

        private void chkWorkOff_Click(object sender, EventArgs e)
        {

            chkAll.Checked = false;
            chkWorking.Checked = false;
            //if (!chkWorkOff.Checked)
            //{
            chkWorkOff.Checked = true;
            //}
            this._strFilter = " where emp.CNY027 =1 ";
            UpdateDataForGridView();
        }

        void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this._strFilter = string.Format(" where emp.PK like '%{0}%' ", txtSearch.Text.Trim());
                UpdateDataForGridView();
            }
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            this._strFilter = string.Format(" where emp.PK like '%{0}%' ", txtSearch.Text.Trim());

            UpdateDataForGridView();
        }

        #region "GridViewCustomInit"
        private void GridViewCustomInit()
        {


            gcMain.UseEmbeddedNavigator = true;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;

            gvMain.OptionsCustomization.AllowSort = true;

            gvMain.OptionsCustomization.AllowFilter = true;

            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = true;
            gvMain.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.OptionsSelection.MultiSelect = false;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMain.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = false;


            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsView.ShowFooter = false;

            gvMain.OptionsHint.ShowFooterHints = false;
            gvMain.OptionsHint.ShowCellHints = false;

            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvMain.OptionsFind.AllowFindPanel = true;

            gvMain.OptionsFind.AlwaysVisible = false;





            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
                IsDrawFilter = true,
            };

            gvMain.FocusedRowChanged += gvMain_FocusedRowChanged;
            //gvMain.CustomDrawCell += gvMain_CustomDrawCell;
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
            gvMain.FocusedColumnChanged += gvMain_FocusedColumnChanged;
            //gvMain.CustomDrawFooter += gvMain_CustomDrawFooter;
            //gvMain.CustomDrawFooterCell += gvMain_CustomDrawFooterCell;
            gvMain.RowStyle += gvMain_RowStyle;
            //gvMain.CustomColumnDisplayText += gvMain_CustomColumnDisplayText; //hien thi text theo dieu khien


            gcMain.ForceInitialize();



        }
        #endregion
        #region "gridview event"
        private void gvMain_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "DateIn":
                case "DateBirth":
                case "DateIssue":
                case "DateWorkOff":
                    if (e.DisplayText.EndsWith("1900"))
                    {
                        e.DisplayText = "";
                    }
                    break;
            }
        }
        private void gvMain_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gvMain_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "UserName" && e.Column.FieldName != "FullName") return;
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                using (brush)
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "UserName")
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                e.Graphics.DrawString(@"   " + e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            }
            else
            {
                e.Appearance.ForeColor = Color.Chocolate;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            }
            e.Handled = true;
        }


        private void gvMain_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = sender as GridView;
            //if (!disableEventWhenLoad)
            //{
            //DisplatDetailFocusedRowChanged(gv);
            //}
        }

        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = sender as GridView;
            //if (!disableEventWhenLoad)
            //{
            //DisplatDetailFocusedRowChanged(gv);
            //}            

        }

        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
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

        private void gvMain_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.VisibleIndex == 0)
            //{
            //    //Image icon = CNY_AdminSys.Properties.Resources.Increase_icon;
            //    //e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            //    e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            //    e.Handled = true;
            //}
            var gv = sender as GridView;
            if (e.Column.VisibleIndex == 0)
            {
                //Image icon = CNY_AdminSys.Properties.Resources.Increase_icon;
                //e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText,
                    new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }
            //tô màu ô những người nghỉ việc
            //if (e.Column.FieldName == "WorkOff")
            //{
            //    if (gv.GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString() == "") return;
            //    if (gv.GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString() == "True")
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //        e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //    }
            //}
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.FocusedRowHandle == e.RowHandle)
            {
                if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["WorkOff"])) == "True")
                {
                    e.HighPriority = true;
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.BackColor = Color.FromArgb(129, 232, 250);
                    e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                    e.Appearance.GradientMode = LinearGradientMode.Horizontal;
                }
                else
                {
                    e.HighPriority = true;
                    e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                    e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                    e.Appearance.GradientMode = LinearGradientMode.Horizontal;
                }

            }
            //tô màu dòng những người nghỉ việc
            //if (e.RowHandle >= 0)
            //{
            //    string WorkOff = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, gv.Columns["WorkOff"]));
            //    if (WorkOff == "True")
            //    {
            //        e.HighPriority = true;
            //        e.Appearance.ForeColor = Color.Red;
            //        //e.Appearance.BackColor = Color.FromArgb(129, 232, 250);
            //        //e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
            //        e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            //    }
            //}


        }
        //private void gvMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        //{
        //    var gv = sender as GridView;
        //    if (e.Column.FieldName == "WorkOff" &&
        //        string.IsNullOrEmpty(gv.GetRowCellDisplayText(e.RowHandle, e.Column)))
        //    {
        //        e.Appearance.BackColor = Color.DeepSkyBlue;
        //    }
        //}
        #endregion
        void UCMain_User_Load(object sender, EventArgs e)
        {
            _dt = GetData();
            LoadDataForGridView();
            //_tp.Innitial(_dt);

            //ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "UserID", "Password", "Signature", "PositionsCode", "DepartmentCode", "Sex");

            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["RoleGroup"], "Role", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["FullName"], "Full Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["UserName"], "User Name", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Email"], "Email", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["DateOfBirth"], "Date Of Birth", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["PositionsName"], "Position", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["DepartmentName"], "Department", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["IsActive"], "Active", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["ChangePassDate"], "Change Pass Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);

            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["CreateDate"], "Created Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["CreateBy"], "Created By", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["UpdateDate"], "Updated Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["UpdateBy"], "Updated By", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
        }
        public void LoadDataForGridView()
        {
            gvMain.BeginUpdate();
            gcMain.DataSource = _dt;
            ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "UserID", "Password", "Signature", "PositionsCode", "DepartmentCode", "Sex");

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["RoleGroup"], "Role", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["FullName"], "Full Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["UserName"], "User Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Email"], "Email", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["DateOfBirth"], "Date Of Birth", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["PositionsName"], "Position", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["DepartmentName"], "Department", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["IsActive"], "Active", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["ChangePassDate"], "Change Pass Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["CreateDate"], "Created Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["CreateBy"], "Created By", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["UpdateDate"], "Updated Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["UpdateBy"], "Updated By", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            gvMain.BestFitColumns();
            gvMain.EndUpdate();
        }
        public void UpdateDataForGridView(bool isShowDialog = true)
        {
            if (isShowDialog)
            {
                _dlg = new WaitDialogForm("");
            }
            _dt = GetData();
            //_tp.Innitial(_dt);
            LoadDataForGridView();

            if (isShowDialog)
            {
                _dlg.Close();
            }


        }

        private DataTable GetData()
        {
            DataTable dtS = _inf.GridViewData_Load();

            if (DeclareSystem.SysUserName.ToUpper() != "ADMIN")
            {
                var q1 = dtS.AsEnumerable().Where(p => p.Field<string>("UserName").ToUpper().Trim() == "ADMIN").ToList();
                if (q1.Any())
                {
                    foreach (DataRow drQ1 in q1)
                    {
                        dtS.Rows.Remove(drQ1);
                    }
                    dtS.AcceptChanges();
                }

            }
            _dt = dtS;
            //_dt = _inf.GridViewData_Load_New(_strFilter);
            return _dt;
        }

        public long _UserID;
        public string _UserName;
        public bool PerformEdit()
        {
            if (gvMain.RowCount == 0 || !gvMain.IsDataRow(gvMain.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return false;
            }
            _UserID = ProcessGeneral.GetSafeInt64(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, gvMain.Columns["UserID"]));
            _UserName = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, gvMain.Columns["UserName"]));
            return true;
        }

        public bool PerformDelete()
        {
            if (gvMain.RowCount == 0 || !gvMain.IsDataRow(gvMain.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform deleting", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return false;
            }
            _UserID = ProcessGeneral.GetSafeInt64(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, gvMain.Columns["UserID"]));
            _UserName = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, gvMain.Columns["UserName"]));
            if (_UserName.Trim().ToUpper() == "ADMIN")
            {
                XtraMessageBox.Show("This User don't allow to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to delete user {0} ? (yes/No) \n Note:You could not restore this record!", _UserName.Trim()), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return false;



            if (_inf.Delete(_UserID) == 1 && _inf.DeleteCNY00004(_UserID) == 1) //Adjust 12/03/2020
            {
                XtraMessageBox.Show(string.Format("User {0} has been deleted from the database", _UserName.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                UpdateDataForGridView(true);
                return true;
            }
            else
            {
                XtraMessageBox.Show(string.Format("Could not delete User {0} \n because of an error in the process of deleting data", _UserName.Trim()),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }



        }
    }
}
