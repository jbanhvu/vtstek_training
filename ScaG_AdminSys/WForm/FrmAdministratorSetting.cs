using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CNY_AdminSys.UControl;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_AdminSys.WForm
{
    public partial class FrmAdministratorSetting : FrmBase
    {
        #region "Property"

        private XtraUserControl _ucMain = null;
        private PermissionFormInfo _perInfo;
        private string _option = "";
        GridView gvMain;
        private WaitDialogForm _dlg;

        #endregion

        #region "Contructor"


        public FrmAdministratorSetting()
        {
            InitializeComponent();
            _perInfo = new PermissionFormInfo();
            tileMain.ScrollMode = TileControlScrollMode.TouchScrollBar;
            InitListingsTile(ucUserManagement, new List<TileImageItemInfo>
            {
                new TileImageItemInfo
                {BackImage = null,
                    Text3 = "Add, Edit, Delete User",
                    IncreasedFontCaption = 4,
                    CaptionText = "User Management",
                    IncreasedFontDescription = 0,
                    HeaderImage = Properties.Resources.publicfix_32x321,
                    ColorDesc = Color.Empty,

                },
                new TileImageItemInfo
                {BackImage = null,
                    Text3 = "Update Role For User",
                    IncreasedFontCaption = 4,
                    CaptionText = "User Management",
                    IncreasedFontDescription = 0,
                    HeaderImage = Properties.Resources.usergroup_32x32,
                    ColorDesc = Color.Empty,
                },
            });


        


       
            //txtSearch.EditValueChanged += TxtSearch_EditValueChanged;

            this.Load += FrmAdministratorSetting_Load;
            ucCompany.ItemDoubleClick += UcCompany_ItemDoubleClick;
            ucDepartment.ItemDoubleClick += UcDepartment_ItemDoubleClick;
            ucPosition.ItemDoubleClick += UcPosition_ItemDoubleClick;
            ucUserManagement.ItemDoubleClick += UcUserManagement_ItemDoubleClick;
            ucMenu.ItemDoubleClick += UcMenu_ItemDoubleClick;
            ucRule.ItemDoubleClick += UcRule_ItemDoubleClick;
            ucRole.ItemDoubleClick += UcRole_ItemDoubleClick;
            ucMFList.ItemDoubleClick += UcMFList_ItemDoubleClick;



        }

        //TRUNGND bổ sung ngày 30/12/2019
        //BEGIN
        private void UcMFList_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            _perInfo = ProcessGeneral.GetPermissionByFormCode("FrmMasterFileListGridView");
            //_perInfo.PerIns = PerIns;
            //_perInfo.PerDel = PerDel;
            //_perInfo.PerUpd = PerUpd;
            //_perInfo.PerViw = PerViw;
            //_perInfo.DtAdvanceFunc = DtPerFunction;
            //_perInfo.StrAdvanceFunction = StrAdvanceFunction;
            //_perInfo.PerCheckAdvanceFunction = PerCheckAdvanceFunction;
            //_perInfo.StrSpecialFunction = StrSpecialFunction;
            //_perInfo.DtSpecialFunction = DtSpecialFunction;

            TileItem tileItem = sender as TileItem;
            if (tileItem == null) return;

            Form parentForm = this.MdiParent;
            string tileFrom = ucMFList.Name;
            string tileText = ucMFList.Text;
            bool activeForm = false;
            int mdiFormIndex = -1;
            foreach (Form frm in parentForm.MdiChildren)
            {
                mdiFormIndex++;
                if (frm.Text.Trim().ToUpper() != tileText.ToUpper())
                    continue;
                activeForm = true;
                break;
            }

            if (activeForm)
            {
                parentForm.MdiChildren[mdiFormIndex].Activate();
            }
            else
            {
                try
                {
                    var frmAE = new FrmBase();
                    frmAE = new FrmMasterFileListGridView(tileFrom, tileText, _perInfo);
                    _dlg = new WaitDialogForm();
                    frmAE.MdiParent = parentForm;
                    frmAE.WindowState = FormWindowState.Normal;
                    frmAE.StartPosition = FormStartPosition.CenterScreen;
                    frmAE.SetDefaultCommandAndPermission(this);
                    frmAE.Show();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _dlg.Close();
                }
            }
        }
        //END

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

        private void FrmAdministratorSetting_Load(object sender, EventArgs e)
        {
            EnableAllButtonMenu();
            LoadButtonWhenLoad();
          
        }

        private void SetTextForm(string moduleText)
        {
            string text = "System Setting";
            if (!string.IsNullOrEmpty(moduleText))
            {
                text = string.Format("{0} - ({1})", text, moduleText);
            }

            this.Text = text;
        }

        #endregion



        #region "Filter Control"
        /*
        private void TxtSearch_EditValueChanged(object sender, EventArgs e)
        {
            FilterTileControl(txtSearch.Text.Trim().ToLower());
        }
        private void FilterTileControl(string filterText)
        {
            foreach (TileGroup group in tileMain.Groups)
            {
                foreach (var tileItem2 in group.Items)
                {
                    var tileItem = (TileItem) tileItem2;
                    tileItem.Visible = CheckFilterItem(tileItem, filterText);
                }
            }
        }
        private bool CheckFilterItem(TileItem tileItem, string filterText)
        {
            if (string.IsNullOrEmpty(filterText))
                return true;
            foreach (TileItemElement element in tileItem.Elements)
            {
                string text = ProcessGeneral.GetSafeString(element.Text).ToLower();
                if (text.Contains(filterText))
                    return true;
            }
            return false;
        }
        */
        #endregion


        #region "Tile Event Click"
        private void UcDepartment_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            VisibleMainControl(false);
            _ucMain = new UCACF_Department
            {
                Dock = DockStyle.Fill,
                Name = "UCACF_Department"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCACF_Department");
            SetTextForm("Department Listing");
        }
        private void UcCompany_ItemDoubleClick(object sender, TileItemEventArgs e)
        {

            VisibleMainControl(false);
            _ucMain = new UCACF_Company
            {
                Dock = DockStyle.Fill,
                Name = "UCACF_Company"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCACF_Company");
            SetTextForm("Company Listing");
        }


        private void UcPosition_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            VisibleMainControl(false);
            _ucMain = new UCACF_Position
            {
                Dock = DockStyle.Fill,
                Name = "UCACF_Position"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCACF_Position");
            SetTextForm("Position Listing");
        }


        private void UcUserManagement_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            VisibleMainControl(false);
            //_ucMain = new UCACF_User
            //{
            //    Dock = DockStyle.Fill,
            //    Name = "UCACF_User"
            //};
            //pcAdd.Controls.Add(_ucMain);
            //LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCACF_User");
            //SetTextForm("User Listing");

            _ucMain = new UCMain_User
            {
                Dock = DockStyle.Fill,
                Name = "UCMain_User"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCMain_User");
            SetTextForm("User Listing");
            var ucMain1 = (UCMain_User)_ucMain;
            if (ucMain1 == null) return;
            gvMain = ucMain1.gvMainP;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvMain.KeyDown += gvMain_KeyDown;
        }

        private void UcMenu_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            VisibleMainControl(false);
            _ucMain = new UCACF_Menu
            {
                Dock = DockStyle.Fill,
                Name = "UCACF_Menu"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCACF_Menu");
            SetTextForm("Menu Listing");
        }

        private void UcRule_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            VisibleMainControl(false);
            _ucMain = new UCACF_Rule
            {
                Dock = DockStyle.Fill,
                Name = "UCACF_Rule"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCACF_Rule");
            SetTextForm("Rule Listing");
        }
        private void UcRole_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            VisibleMainControl(false);
            _ucMain = new UCACF_Role
            {
                Dock = DockStyle.Fill,
                Name = "UCACF_Role"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCACF_Role");
            SetTextForm("Role Listing");
        }

        #endregion

        #region "Process Tile"



        private void InitListingsTile(TileItem tile, List<TileImageItemInfo> lInfo)
        {
          
            foreach (var info in lInfo)
            {
                Image imgBack = info.BackImage;
                
                TileItemFrame item = new TileItemFrame
                {
                    AnimateBackgroundImage = true,
                  
                    AnimateText = true,
                    UseText = true,
                    Tag = tile.Name
                };
                if (imgBack != null)
                {
                    item.UseBackgroundImage = true;
                    item.BackgroundImage = imgBack;
                }

                string text3 = info.Text3;
                item.Text3 = string.Format("{0}", text3);
                if (info.ColorDesc != Color.Empty)
                {
                    item.Appearance.ForeColor = info.ColorDesc;
                }
               
                int increasdDesc = info.IncreasedFontDescription;
                if (increasdDesc > 0)
                {
                    item.Appearance.FontSizeDelta = item.Appearance.FontSizeDelta + increasdDesc;
                }
                //item.Text2 = string.Format("<backcolor=108,189,69> {0} Beds   <br> {1} Baths  ", home.BedsString, home.BathsString);


                //item.AnimateImage = true;
                //item.Text = "<backcolor=108,189,69><size=+3>User Management";
                //item.ImageAlignment = TileItemContentAlignment.MiddleCenter;
                //item.ImageToTextAlignment = TileControlImageToTextAlignment.Top;

                TileItemElement childItem = new TileItemElement();
                string captionText = info.CaptionText;
                if (!string.IsNullOrEmpty(captionText))
                {
                    childItem.Text = info.CaptionText;
                    childItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    childItem.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                }
                Image headerImage = info.HeaderImage;
                if (headerImage != null)
                {
                    childItem.Image = headerImage;
                    childItem.ImageAlignment = TileItemContentAlignment.MiddleCenter;
                    childItem.ImageToTextAlignment = TileControlImageToTextAlignment.Top;
                }
                childItem.AnimateTransition = DefaultBoolean.True;

            //    childItem.Appearance.Normal.ForeColor = Color.FromArgb(61, 3, 15);
                int increasdCaption = info.IncreasedFontCaption;
                if (increasdCaption > 0)
                {
                    childItem.Appearance.Normal.FontSizeDelta = childItem.Appearance.Normal.FontSizeDelta + increasdCaption;
                }
        
                item.Elements.Add(childItem);
                tile.Frames.Add(item);
            }

        
            //this.ucUserManagement.Elements.Add(tileItemElement3);
            //this.ucUserManagement.Id = 0;
            //this.ucUserManagement.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            //this.ucUserManagement.Name = "ucUserManagement";
        }


        #endregion


        #region "Process Button"

        private void VisibleMainControl(bool status)
        {
            //txtSearch.Visible = status;
            tileMain.Visible = status;
        }
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
                if(!isEdit) return;
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
            if (controlName == "UCACF_User")
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
        protected override void PerformClose()
        {
            if (tileMain.Visible)
            {
                this.Close();
                return;
            }

            if (_ucMain == null) return;
            string controlName = _ucMain.Name;
            if (controlName == "UCACF_User_New")
            {
                if (_ucMain == null) return;
                pcAdd.Controls.Remove(_ucMain);
                _ucMain = new UCMain_User
                {
                    Dock = DockStyle.Fill,
                    Name = "UCMain_User"
                };
                pcAdd.Controls.Add(_ucMain);
                LoadButtonWhenLoadCompanyDepartmentPosUserMenuRuleRole("UCMain_User");
                SetTextForm("User Listing");
                var ucMain1 = (UCMain_User)_ucMain;
                if (ucMain1 == null) return;
                gvMain = ucMain1.gvMainP;
                gvMain.DoubleClick += gvMain_DoubleClick;
                gvMain.KeyDown += gvMain_KeyDown;
                return;
            }
            RemoveMainAeControl();
            VisibleMainControl(true);
            SetTextForm("");
            LoadButtonWhenLoad();

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
    }
}
