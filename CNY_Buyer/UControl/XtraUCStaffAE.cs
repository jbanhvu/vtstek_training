using CNY_BaseSys.Common;
using CNY_Buyer.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_Buyer.UControl
{
    public partial class XtraUCStaffAE : UserControl
    {
        #region Properties
        private readonly int _pk;
        private Inf_Staff _inf;
        private Inf_LaborContract _infDetail;
        
        private string _opt;
        #endregion

        #region Constructor
        public XtraUCStaffAE(int pk, string opt)
        {
            InitializeComponent();
            _inf = new Inf_Staff();
            _infDetail = new Inf_LaborContract();
            _pk = pk;
            _opt = opt;
            LoadData();
        }
        #endregion

        #region Load Data
        private void LoadData()
        {
            LoadSlue();
            if (_opt == "Add")
            {
                DisplayDataForAdding();
            }
            else
            {
                DisplayDataForEditing();
            }
            InitColumnGridview();
            DeclareGridview();
            LoadDataGridView();
        }

        #endregion

        #region Display data based on option
        public void DisplayDataForEditing()
        {

            DataTable dt = _inf.sp_Staff_Select(_pk);
            txtFullName.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["FullName"]);
            txtStaffCode.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Code"]);
            txtEncrolNumber.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["EncrolNumber"]);
            txtSex.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["Sex"]);
            txtIdentityCard_CreatedWhere.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["IdentityCard_CreatedWhere"]);
            txtIdentityCard_Number.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["IdentityCard_Number"]);
            txtAddress.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Address"]);
            txtEducationLevel.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["EducationLevel"]);
            txtEmail.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Email"]);
            txtSeniority.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Seniority"]);
            txtPhoneNumber.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["PhoneNumber"]);

            deHireDate.EditValue = ProcessGeneral.GetSafeDatetime(dt.Rows[0]["HireDate"]);
            deDOB.EditValue = ProcessGeneral.GetSafeDatetime(dt.Rows[0]["DOB"]);
            deIdentityCard_CreatedDate.EditValue = ProcessGeneral.GetSafeDatetime(dt.Rows[0]["IdentityCard_CreatedDate"]);


            
            sluePosition.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["PositionCode"]); 

            var departmentCode = ProcessGeneral.GetSafeString(dt.Rows[0]["DepartmentCode"]);
            slueDepartment.EditValue = departmentCode;
        }
        public void LoadSlue()
        {
            //Load Slue 
            slueDepartment.Properties.DataSource = _inf.Excute("SELECT DepartmentCode[Code], DepartmentName  FROM dbo.ListDepartment");
            slueDepartment.Properties.DisplayMember = "DepartmentName";
            slueDepartment.Properties.ValueMember = "Code";

            sluePosition.Properties.DataSource = _inf.Excute("SELECT PositionsCode [Code],PositionsName FROM dbo.ListPositions");
            sluePosition.Properties.DisplayMember = "PositionsName";
            sluePosition.Properties.ValueMember = "Code";
        }
        private void DisplayDataForAdding()
        {
            txtFullName.EditValue = string.Empty;
            txtEncrolNumber.EditValue= string.Empty;
            txtSex.EditValue= string.Empty;
            txtStaffCode.EditValue= string.Empty;
            txtAddress.EditValue= string.Empty;
            txtEmail.EditValue= string.Empty;
            txtEducationLevel.EditValue= string.Empty;
            txtIdentityCard_CreatedWhere.EditValue= string.Empty;
            txtIdentityCard_Number.EditValue= string.Empty;
            txtSeniority.EditValue= string.Empty;

            deDOB.EditValue= string.Empty;
            deHireDate.EditValue= string.Empty;
            deIdentityCard_CreatedDate.EditValue = string.Empty;

            //Initial Loading - Slue
            slueDepartment.Text = string.Empty;
            sluePosition.Text = string.Empty;

            slueDepartment.Properties.DataSource = _inf.Excute("SELECT DepartmentCode [Code], DepartmentName [Phòng Ban] FROM dbo.ListDepartment");
            slueDepartment.Properties.DisplayMember = "Phòng Ban";
            slueDepartment.Properties.ValueMember = "Code";

            sluePosition.Properties.DataSource = _inf.Excute("SELECT PositionsCode [Code],PositionsName [Vị trí] FROM dbo.ListPositions");
            sluePosition.Properties.DisplayMember = "Vị trí";
            sluePosition.Properties.ValueMember = "Code";

        }
        #endregion

        #region GridView
        private void LoadDataGridView()
        {
            if (_opt=="Edit")
            {
                gcDetail.DataSource = _infDetail.Excute($"SELECT * FROM LaborContract WHERE StaffPK = {_pk}");
                gvDetail.BestFitColumns();
            }
            else
            {
                return;
            }
        }
        #region Format GridView
        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "STT", "PK", 1);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Mã nhân viên", "StaffPK", 2);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Ngày ký", "SignDate", 3);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Loại lao động", "LaborType", 4);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Ngày ký", "SignDate", 5);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Ngày hết hạn", "ExpiredDate", 6);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Ngày tạo", "CreatedDate", 7);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Người tạo", "CreatedUser", 8);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Ngày cập nhật", "UpdatedDate", 9);
            FormatGridView.CreateColumnOnGridview(gvDetail, HorzAlignment.Default, "Người cập nhật", "UpdatedUser", 10);

        }
        private void DeclareGridview()
        {
            // gcDetail.ToolTipController = toolTipController1  ;
            gvDetail.OptionsCustomization.AllowColumnResizing = true;
            gvDetail.OptionsCustomization.AllowGroup = true;
            gvDetail.OptionsCustomization.AllowColumnMoving = true;
            gvDetail.OptionsCustomization.AllowQuickHideColumns = true;
            gvDetail.OptionsCustomization.AllowSort = true;
            gvDetail.OptionsCustomization.AllowFilter = true;

            gcDetail.UseEmbeddedNavigator = true;

            gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvDetail.OptionsBehavior.Editable = true;
            gvDetail.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvDetail.OptionsHint.ShowCellHints = true;
            gvDetail.OptionsView.BestFitMaxRowCount = 1000;
            gvDetail.OptionsView.ShowGroupPanel = true;
            gvDetail.OptionsView.ShowIndicator = true;
            gvDetail.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvDetail.OptionsView.ShowAutoFilterRow = true;
            gvDetail.OptionsView.AllowCellMerge = false;
            gvDetail.HorzScrollVisibility = ScrollVisibility.Auto;
            gvDetail.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvDetail.OptionsNavigation.AutoFocusNewRow = true;
            gvDetail.OptionsNavigation.UseTabKey = true;

            gvDetail.OptionsSelection.MultiSelect = false;
            gvDetail.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvDetail.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvDetail.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvDetail.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDetail.OptionsView.EnableAppearanceEvenRow = false;
            gvDetail.OptionsView.EnableAppearanceOddRow = false;

            gvDetail.OptionsView.ShowFooter = false;
            gvDetail.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;
            gvDetail.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvDetail.OptionsFind.AlwaysVisible = false;
            gvDetail.OptionsFind.ShowCloseButton = true;
            gvDetail.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvDetail)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvDetail.OptionsView.ShowGroupedColumns = true;
            gvDetail.OptionsPrint.AutoWidth = true;
            gvDetail.OptionsPrint.ShowPrintExportProgress = true;
            gvDetail.OptionsPrint.AllowMultilineHeaders = true;
            gvDetail.OptionsPrint.ExpandAllDetails = true;
            gvDetail.OptionsPrint.ExpandAllGroups = true;
            gvDetail.OptionsPrint.PrintDetails = true;
            gvDetail.OptionsPrint.PrintFooter = true;
            gvDetail.OptionsPrint.PrintGroupFooter = true;
            gvDetail.OptionsPrint.PrintHeader = true;
            gvDetail.OptionsPrint.PrintHorzLines = true;
            gvDetail.OptionsPrint.PrintVertLines = true;
            gvDetail.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvDetail.OptionsPrint.SplitDataCellAcrossPages = true;
            gvDetail.OptionsPrint.UsePrintStyles = false;
            gvDetail.OptionsPrint.AllowCancelPrintExport = true;
            gvDetail.OptionsPrint.AutoResetPrintDocument = true;

            gcDetail.ForceInitialize();
        }
        #endregion
        #endregion

        #region Save
        public void Save()
        {
            var fullName = ProcessGeneral.GetSafeString(txtFullName.EditValue);
            var staffCode  = ProcessGeneral.GetSafeString(txtStaffCode.EditValue);
            var encrolNumber = ProcessGeneral.GetSafeInt(txtEncrolNumber.EditValue);
            var sex = ProcessGeneral.GetSafeInt(txtSex.EditValue);
            var IdCard_CreatedWhere = ProcessGeneral.GetSafeString(txtIdentityCard_CreatedWhere.EditValue);
            var IdCard_Number = ProcessGeneral.GetSafeString(txtIdentityCard_Number.EditValue);
            var address = ProcessGeneral.GetSafeString(txtAddress.EditValue);
            var educationLevel = ProcessGeneral.GetSafeString(txtEducationLevel.EditValue);
            var email = ProcessGeneral.GetSafeString(txtEmail.EditValue);
            var seniority = ProcessGeneral.GetSafeString(txtSeniority.EditValue);
            var departmentCode = ProcessGeneral.GetSafeString(slueDepartment.EditValue);
            var positionCode = ProcessGeneral.GetSafeString(sluePosition.EditValue);
            var phoneNumber = ProcessGeneral.GetSafeString(txtPhoneNumber.EditValue);

            DateTime IdCard_CreatedDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(deIdentityCard_CreatedDate.EditValue));
            DateTime hireDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(deHireDate.EditValue));
            DateTime DOB = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(deDOB.EditValue));

            DataTable dtResult = _inf.sp_Staff_Update(_pk, DOB, fullName, staffCode, encrolNumber, hireDate, sex, IdCard_CreatedDate,
                IdCard_CreatedWhere, IdCard_Number, departmentCode, positionCode, educationLevel, phoneNumber, email, seniority, address);

            //string msg = ProcessGeneral.GetSafeString(dtResult.Rows[0]["ErrMsg"]);
            //XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SaveDetail();

        }

        private void SaveDetail()
        {
            DataTable dt = gcDetail.DataSource as DataTable;
            if (dt == null) return;

            var pk = -1;
            var staffPK = _pk;
            DateTime signDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(dt.Rows[0]["SignDate"]));
            var duration = ProcessGeneral.GetSafeInt(dt.Rows[0]["Duration"]);
            var laborType = ProcessGeneral.GetSafeInt(dt.Rows[0]["LaborType"]);
            DateTime expiredDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(dt.Rows[0]["ExpiredDate"]));
            DateTime createdDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(dt.Rows[0]["CreatedDate"]));
            DateTime updatedDate = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(dt.Rows[0]["UpdatedDate"]));
            var createdUser = ProcessGeneral.GetSafeString(dt.Rows[0]["CreatedUser"]);
            var updatedUser = ProcessGeneral.GetSafeString(dt.Rows[0]["UpdatedUser"]);

            DataTable dtResult = _infDetail.sp_LaborContract_Update(pk,staffPK, signDate, duration, laborType, expiredDate, createdDate, createdUser, updatedDate, updatedUser);

            string msg = ProcessGeneral.GetSafeString(dtResult.Rows[0]["ErrMsg"]);
            XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region InitTableDetail

        #endregion

        #region Handle Add/Delete Rows Event
        private void btnInsert_Click(object sender, EventArgs e)
        {
            gvDetail.AddNewRow();
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            dr["PK"] = -1;
            dr["StaffPK"] = _pk;
            gvDetail.UpdateCurrentRow();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            gvDetail.DeleteRow(gvDetail.FocusedRowHandle);
        }
        #endregion


    }
}
