using CNY_AdminSys.UControl;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.XtraEditors;
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

namespace CNY_AdminSys.WForm
{
    public partial class FrmUser : FrmBase
    {
        #region "Property"

        private XtraUserControl _ucMain = null;
        private PermissionFormInfo _perInfo;
        private string _option = "";
        GridView gvMain;
        private WaitDialogForm _dlg;

        #endregion

        public FrmUser()
        {
            InitializeComponent();
            LoadUC();
        }
        public void LoadUC()
        {
            _ucMain = new UCMain_User
            {
                Dock = DockStyle.Fill,
                Name = "UCMain_User"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("FrmUser");
            SetTextForm("User Listing");
            var ucMain1 = (UCMain_User)_ucMain;
            if (ucMain1 == null) return;
            gvMain = ucMain1.gvMainP;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvMain.KeyDown += gvMain_KeyDown;
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            PerformEdit();

        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformEdit();
            }
            //if (e.KeyCode == Keys.Delete)
            //{
            //    PerformDelete();
            //}

        }
        #region "override button menubar click"
        protected override void PerformGenerate()
        {
            if (_ucMain == null) return;
            string controlName = _ucMain.Name;

            if (controlName == "UCACF_User")
            {

                var ucMain = (UCACF_User)_ucMain;
                if (ucMain == null) return;
                //ucMain.GenerateData();
                return;
            }
        }
        /// <summary>
        ///    Perform when click Add button 
        /// </summary>
        protected override void PerformAdd()
        {

            if (_ucMain == null) return;
            string controlName = _ucMain.Name;
            if (controlName == "UCACF_Company")
            {

                var ucMain = (UCACF_Company)_ucMain;
                if (ucMain == null) return;
                _option = "ADD";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformAdd();
                return;
            }
            if (controlName == "UCACF_Department")
            {

                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                _option = "ADD";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformAdd();
                return;
            }
            if (controlName == "UCACF_Position")
            {

                var ucMain = (UCACF_Position)_ucMain;
                if (ucMain == null) return;
                _option = "ADD";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformAdd();
                return;
            }
            //if (controlName == "UCACF_User")
            //{

            //    var ucMain = (UCACF_User)_ucMain;
            //    if (ucMain == null) return;
            //    _option = "ADD";
            //    LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
            //    ucMain.PerformAdd();
            //    return;
            //}
            if (controlName == "UCMain_User")
            {
                if (_ucMain == null) return;
                pcAdd.Controls.Remove(_ucMain);
                _ucMain = new UCACF_User_New
                {
                    Dock = DockStyle.Fill,
                    Name = "UCACF_User_New"
                };
                pcAdd.Controls.Add(_ucMain);
                var ucMain = (UCACF_User_New)_ucMain;
                if (ucMain == null) return;

                _option = "ADD";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformAdd(_option); //Adjust 12/03/2020: thêm _option
                return;
            }
            if (controlName == "UCACF_Menu")
            {

                var ucMain = (UCACF_Menu)_ucMain;
                if (ucMain == null) return;
                _option = "ADD";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformAdd();
                return;
            }

            if (controlName == "UCACF_Rule")
            {

                var ucMain = (UCACF_Rule)_ucMain;
                if (ucMain == null) return;
                _option = "ADD";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformAdd();
                return;
            }


            if (controlName == "UCACF_Role")
            {

                var ucMain = (UCACF_Role)_ucMain;
                if (ucMain == null) return;
                _option = "ADD";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformAdd();
                return;
            }
        }


        /// <summary>
        /// Perform when click edit button
        /// </summary>
        protected override void PerformEdit()
        {
            if (_ucMain == null) return;
            string controlName = _ucMain.Name;
            if (controlName == "UCACF_Company")
            {
                var ucMain = (UCACF_Company)_ucMain;
                if (ucMain == null) return;
                bool isEdit = ucMain.PerformEdit();
                if (!isEdit) return;
                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                return;
            }
            if (controlName == "UCACF_Department")
            {

                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                bool isEdit = ucMain.PerformEdit();
                if (!isEdit) return;
                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                return;
            }
            if (controlName == "UCACF_Position")
            {

                var ucMain = (UCACF_Position)_ucMain;
                if (ucMain == null) return;
                bool isEdit = ucMain.PerformEdit();
                if (!isEdit) return;
                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                return;
            }
            if (controlName == "UCACF_User")
            {

                var ucMain = (UCACF_User)_ucMain;
                if (ucMain == null) return;
                //bool isEdit = ucMain.PerformEdit();
                //if (!isEdit) return;
                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                return;
            }

            if (controlName == "UCMain_User")
            {
                var ucMain1 = (UCMain_User)_ucMain;
                if (ucMain1 == null) return;
                bool isEdit = ucMain1.PerformEdit();
                if (!isEdit) return;
                long _userID = ucMain1._UserID;
                string _userName = ucMain1._UserName;
                pcAdd.Controls.Remove(_ucMain);
                _ucMain = new UCACF_User_New
                {
                    Dock = DockStyle.Fill,
                    Name = "UCACF_User_New"
                };
                pcAdd.Controls.Add(_ucMain);
                var ucMain = (UCACF_User_New)_ucMain;
                if (ucMain == null) return;

                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                ucMain.PerformEdit(_userID, _userName, _option); //Adjust 12/03/2020: thêm _option
                return;
            }

            if (controlName == "UCACF_Menu")
            {

                var ucMain = (UCACF_Menu)_ucMain;
                if (ucMain == null) return;
                bool isEdit = ucMain.PerformEdit();
                if (!isEdit) return;
                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                return;
            }

            if (controlName == "UCACF_Rule")
            {

                var ucMain = (UCACF_Rule)_ucMain;
                if (ucMain == null) return;
                bool isEdit = ucMain.PerformEdit();
                if (!isEdit) return;
                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                return;
            }
            if (controlName == "UCACF_Role")
            {

                var ucMain = (UCACF_Role)_ucMain;
                if (ucMain == null) return;
                bool isEdit = ucMain.PerformEdit();
                if (!isEdit) return;
                _option = "EDIT";
                LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole();
                return;
            }
        }

        /// <summary>
        /// Perform when click delete button
        /// </summary>
        protected override void PerformDelete()
        {
            if (_ucMain == null) return;
            string controlName = _ucMain.Name;
            if (controlName == "UCACF_Company")
            {
                var ucMain = (UCACF_Company)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }
            if (controlName == "UCACF_Department")
            {

                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }
            if (controlName == "UCACF_Position")
            {

                var ucMain = (UCACF_Position)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }
            if (controlName == "UCACF_User")
            {

                var ucMain = (UCACF_User)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }
            if (controlName == "UCMain_User")
            {
                var ucMain = (UCMain_User)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }
            if (controlName == "UCACF_Menu")
            {

                var ucMain = (UCACF_Menu)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }

            if (controlName == "UCACF_Rule")
            {

                var ucMain = (UCACF_Rule)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }

            if (controlName == "UCACF_Role")
            {

                var ucMain = (UCACF_Role)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
                return;
            }
        }

        /// <summary>
        /// Perform when click save button
        /// </summary>
        protected override void PerformSave()
        {
            if (_ucMain == null) return;
            string controlName = _ucMain.Name;
            if (controlName == "UCACF_Company")
            {

                var ucMain = (UCACF_Company)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }
            if (controlName == "UCACF_Department")
            {

                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }

            if (controlName == "UCACF_Position")
            {

                var ucMain = (UCACF_Position)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }
            if (controlName == "UCACF_User")
            {

                var ucMain = (UCACF_User)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }

            if (controlName == "UCACF_User_New")
            {

                var ucMain = (UCACF_User_New)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                //LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }

            if (controlName == "UCACF_Menu")
            {

                var ucMain = (UCACF_Menu)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }
            if (controlName == "UCACF_Rule")
            {

                var ucMain = (UCACF_Rule)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }

            if (controlName == "UCACF_Role")
            {

                var ucMain = (UCACF_Role)_ucMain;
                if (ucMain == null) return;
                bool isSave = ucMain.PerformSave(_option);
                if (!isSave) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                return;
            }
        }

        /// <summary>
        /// Perform when click cancel button
        /// </summary>     
        protected override void PerformCancel()
        {

            if (_ucMain == null) return;
            string controlName = _ucMain.Name;
            if (controlName == "UCACF_Company")
            {

                var ucMain = (UCACF_Company)_ucMain;
                if (ucMain == null) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                ucMain.PerformCancel();
                return;
            }
            if (controlName == "UCACF_Department")
            {

                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                ucMain.PerformCancel();
                return;
            }

            if (controlName == "UCACF_Position")
            {

                var ucMain = (UCACF_Position)_ucMain;
                if (ucMain == null) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                ucMain.PerformCancel();
                return;
            }
            //if (controlName == "UCACF_User")
            if (controlName == "UCACF_User_New")
            {

                var ucMain = (UCACF_User)_ucMain;
                if (ucMain == null) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                ucMain.PerformCancel();
                return;
            }
            if (controlName == "UCACF_Menu")
            {

                var ucMain = (UCACF_Menu)_ucMain;
                if (ucMain == null) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                ucMain.PerformCancel();
                return;
            }
            if (controlName == "UCACF_Rule")
            {

                var ucMain = (UCACF_Rule)_ucMain;
                if (ucMain == null) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                ucMain.PerformCancel();
                return;
            }

            if (controlName == "UCACF_Role")
            {

                var ucMain = (UCACF_Role)_ucMain;
                if (ucMain == null) return;
                _option = "";
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(controlName);
                ucMain.PerformCancel();
                return;
            }
        }
        /// <summary>
        /// Perform when click Refresh button
        /// </summary>
        protected override void PerformRefresh()
        {
            if (_ucMain == null) return;
            string controlName = _ucMain.Name;
            if (controlName == "UCACF_Company")
            {

                var ucMain = (UCACF_Company)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }
            if (controlName == "UCACF_Department")
            {

                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }

            if (controlName == "UCACF_Position")
            {

                var ucMain = (UCACF_Position)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }
            if (controlName == "UCACF_User")
            {

                var ucMain = (UCACF_User)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }
            if (controlName == "UCACF_User_New")
            {

                var ucMain = (UCACF_User_New)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }
            if (controlName == "UCMain_User")
            {

                var ucMain = (UCMain_User)_ucMain;
                if (ucMain == null) return;
                ucMain.UpdateDataForGridView(true);
                return;
            }
            if (controlName == "UCACF_Menu")
            {

                var ucMain = (UCACF_Menu)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }
            if (controlName == "UCACF_Rule")
            {

                var ucMain = (UCACF_Rule)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }

            if (controlName == "UCACF_Role")
            {

                var ucMain = (UCACF_Role)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformRefresh(_option);
                return;
            }
        }

        /// <summary>
        /// Perform when click Close button
        /// </summary>
        //protected override void PerformClose()
        //{
        //    if (_ucMain == null) return;
        //    string controlName = _ucMain.Name;
        //    if (controlName == "UCACF_User_New")
        //    {
        //        if (_ucMain == null) return;
        //        pcAdd.Controls.Remove(_ucMain);
        //        _ucMain = new UCMain_User
        //        {
        //            Dock = DockStyle.Fill,
        //            Name = "UCMain_User"
        //        };
        //        pcAdd.Controls.Add(_ucMain);
        //        LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCMain_User");
        //        SetTextForm("User Listing");
        //        var ucMain1 = (UCMain_User)_ucMain;
        //        if (ucMain1 == null) return;
        //        gvMain = ucMain1.gvMainP;
        //        gvMain.DoubleClick += gvMain_DoubleClick;
        //        gvMain.KeyDown += gvMain_KeyDown;
        //        return;
        //    }
        //    RemoveMainAeControl();
        //    SetTextForm("");
        //    LoadButtonWhenLoad();
        //}
        protected override void PerformClose()
        {
            //if (_ucMain.Visible)
            if (_option == "")
            {
                this.Close();
            }
            else
            {
                string controlName = _ucMain.Name;
                if (_ucMain == null) return;
                _option = "";
                pcAdd.Controls.Remove(_ucMain);

                LoadUC();
            }
        }
        #endregion

        #region "Process Button"

        private void LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole(string formCode)
        {
            _perInfo = ProcessGeneral.GetPermissionByFormCode(formCode);
            AllowAdd = _perInfo.PerIns;
            AllowEdit = _perInfo.PerUpd;
            AllowDelete = _perInfo.PerDel;
            AllowSave = false;
            AllowCancel = false;
            AllowRefresh = true;



            AllowFind = false;
            AllowPrint = false;
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCopyObject = false;
            AllowCombine = false;
            AllowCheck = false;
            if (formCode.ToUpper() == "UCACF_USER")
            {
                AllowGenerate = false;
                //AllowGenerate = _perInfo.PerIns || _perInfo.PerUpd || _perInfo.PerDel;
                //SetCaptionGenerate = @"Check Send Mail";
                //SetImageGenerate = Resources.email_send_icon;
            }
            else
            {
                AllowGenerate = false;
            }

        }


        private void LoadButtonWhenAddEditCompanyDepartmentPosUserMenuRuleRole()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowSave = true;
            if (_ucMain.Name.ToUpper() == "UCACF_USER_NEW")
            {
                AllowCancel = false;
            }
            else
            {
                AllowCancel = true;
            }
            //AllowCancel = true;
            AllowRefresh = true;



            AllowFind = false;
            AllowPrint = false;
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCopyObject = false;
            AllowGenerate = false;
            AllowCombine = false;
            AllowCheck = false;
            EnableSave = true;
        }

        private void LoadButtonWhenLoad()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowSave = false;
            AllowCancel = false;
            AllowRefresh = false;
            AllowFind = false;
            AllowPrint = false;
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCopyObject = false;
            AllowGenerate = false;
            AllowCombine = false;
            AllowCheck = false;
        }



        private void EnableAllButtonMenu()
        {
            EnableAdd = true;
            EnableEdit = true;
            EnableDelete = true;
            EnableSave = true;
            EnableCancel = true;
            EnableRefresh = true;
            EnableFind = true;
            EnablePrint = true;
            EnableRevision = true;
            EnableBreakDown = true;
            EnableRangSize = true;
            EnableCopyObject = true;
            EnableGenerate = true;
            EnableCombine = true;
            EnableCheck = true;
            EnableClose = true;
        }






        #endregion

        #region "Remove Control"

        private void RemoveMainAeControl()
        {
            if (_ucMain == null) return;
            pcAdd.Controls.Remove(_ucMain);
            _ucMain = null;
        }


        #endregion
        private void SetTextForm(string moduleText)
        {
            string text = "System Setting";
            if (!string.IsNullOrEmpty(moduleText))
            {
                text = string.Format("{0} - ({1})", text, moduleText);
            }

            this.Text = text;
        }
    }
}
