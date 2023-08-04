using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using CNY_Main.Class;
using CNY_Main.Properties;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Property;
using DevExpress.Utils;
using System.Threading;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;

namespace CNY_Main
{
    public partial class FrmLogin : XtraForm
    {
        #region "Property"
        Ctr_Login _ctrl;
        private const Int32 LookUpEditCompWidth = 170;
        #endregion

        #region "contructor"
     
        public FrmLogin()
        {
            InitializeComponent();
          
            //lblCompanyCode.Visible = false;
            //lblLocation.Visible = false;

            //lookupServer.Visible = false;
            //lueCC.Visible = false;
            //txtComp.Visible = false;

            _ctrl = new Ctr_Login();
            txtComp.Enabled = false;



            lueCC.Properties.AcceptEditorTextAsNewValue = DefaultBoolean.False;



            lueCC.EditValueChanged += lueCC_EditValueChanged;

   //         lueCC.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;


            Program.GetConnectStringServerMain();


            bool rsInfo =  GetSystemInfo();

            if (rsInfo)
            {
                SystemProperty.DtPrDepartmentEmail = ProcessGeneral.GetListEmailUserByDepartmentPR(SystemProperty.SysConnectionString, out SystemProperty.DicPrUserEmail);
                SystemProperty.DtUserListWhenLogin = _ctrl.LoadListUserWhenLogin();
            }


            lookupServer.EditValueChanged += lookupServer_EditValueChanged;
            lookupServer.Popup += lookupServer_Popup;
            Load_Factory();
            //   this.Text = string.Format("Log on ( Domain: {0}; Server SQL: {1}; User SQL: {2}; Computer Name: {3} )",sl.DomainName,sl.ServerSQL,sl.LoggedInUser,sl.HostName);

        }

      
        
        #endregion





        #region"Get Application Info"





        private bool GetSystemInfo()
        {
            bool isConnect = Program.CheckOpenConnectionSQL();
            if (!isConnect) goto loadComptemp;

            SystemLoginInfo sl = _ctrl.GetSystemLoginInfo();

            SystemProperty.SysHostName = sl.HostName;
            SystemProperty.SysLoggedInUser = sl.LoggedInUser;
            SystemProperty.SysServerSql = sl.ServerSQL;

            SystemProperty.SysDomainName = sl.DomainName;
            SystemProperty.SysFullDomainName = sl.FullDomainName;
            SystemProperty.SysIpAddress = sl.IpAddress;

            SystemProperty.SysMachineName = sl.MachineName;
            SystemProperty.SysDefaultGateway = sl.DefaultGateway;
            SystemProperty.SysUserLoginWindows = sl.UserLoginWindows;
            SystemProperty.SysIsDomainUser = sl.IsDomainUser;
            SystemProperty.SysProductName = Application.ProductName;
            lblServer.Text = string.Format("Server Database : {0}", sl.ServerSQL);

            DataTable dtComp = _ctrl.LoadTableCompanyCode();
            //IDCCode
            Load_CompCode(dtComp);

            string compSet = Settings.Default.CompanyCodeSetting;

            bool checkComp = dtComp.AsEnumerable().Any(p => p.Field<String>("IDCCode") == compSet);
            if (checkComp)
            {
                lueCC.EditValue = compSet;
            }
            else if (dtComp.Rows.Count > 0)
            {
                lueCC.EditValue = ProcessGeneral.GetSafeString(dtComp.Rows[0]["IDCCode"]);
            }

            return true;
        loadComptemp:
            Load_CompCode(TableCompanyCodeTemp());
            return false;

        }
    
        #endregion

        #region "Form Event

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            WinApi.ForceWindowToForeground(this.Handle);
            txeFinYear.EditValue = DateTime.Now.Year;
            txtUser.Text = Settings.Default.UserNameSetting;
            chkRemember.Checked = Settings.Default.RememberPassSetting;
            txtPass.Text = Settings.Default.RememberPassSetting ? EnDeCrypt.Decrypt(Settings.Default.PasswordSetting.Trim(), true) : string.Empty;
           



            lueCC.Select();
        }

        #endregion

        #region "Load Data Combobox"

        private DataTable TableCompanyCodeTemp()
        {
            DataTable dtCompTemp = new DataTable();
            dtCompTemp.Columns.Add("IDCCode", typeof(String));
            dtCompTemp.Columns.Add("NameCCode", typeof(String));
            return dtCompTemp;
        }


        /// <summary>
        /// Tao du lieu cho combo company code
        /// </summary>
        private void Load_CompCode(DataTable dtS)
        {
            lueCC.Properties.Columns.Clear();
            lueCC.Properties.DataSource = dtS;
            lueCC.Properties.DisplayMember = "IDCCode";
            lueCC.Properties.ValueMember = "IDCCode";
            lueCC.Properties.Columns.Add(new LookUpColumnInfo("IDCCode", "IDCCode", 22));
            lueCC.Properties.Columns.Add(new LookUpColumnInfo("NameCCode", "NameCCode", 143));      
            lueCC.Properties.ForceInitialize();
          //  lueCC.ItemIndex = 0;
        }


        private void Load_Factory()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Server", typeof(string));
            dt.Columns.Add("ServerName", typeof(string));
            dt.Rows.Add("PRODUCTION", "PRODUCTION");
            //dt.Rows.Add("TRIAL", "EOL");
            dt.Rows.Add("TEST", "TRAINING");

            lookupServer.Properties.Columns.Clear();
            lookupServer.Properties.DataSource = dt;
            lookupServer.Properties.DisplayMember = "ServerName";
            lookupServer.Properties.ValueMember = "Server";
            LookUpColumnInfo colCode = new LookUpColumnInfo("Server", "Server", 0);
            colCode.Visible = false;
            lookupServer.Properties.Columns.Add(colCode);
            lookupServer.Properties.Columns.Add(new LookUpColumnInfo("ServerName", "Server", 0));


            lookupServer.Properties.ForceInitialize();


            string serverEdit = SystemProperty.SysWorkingServer.ToUpper().Trim();

            if (dt.AsEnumerable().All(p => p.Field<string>("Server") != serverEdit))
            {
                serverEdit = ProcessGeneral.GetSafeString(dt.Rows[0]["Server"]);
            }
            lookupServer.EditValue = serverEdit;


        }
        #endregion

        #region "check input when login"
        private string CheckInputFieldOnControl()
        {
            string strError = "";
            //if (ProcessGeneral.GetSafeString(lueCC.EditValue) == string.Empty)
            //{
            //    strError += "Company Code, ";
            //}
            if (ProcessGeneral.GetSafeString(txeFinYear.EditValue) == string.Empty)
            {
                strError += "Finance Year, ";
            }
            if (ProcessGeneral.GetSafeString(txtUser.EditValue) == string.Empty)
            {
                strError += "Username, ";
            }
            if (ProcessGeneral.GetSafeString(txtPass.EditValue) == string.Empty)
            {
                strError += "Password, ";
            }
            if (strError.Trim().Length > 0)
            {
                strError = strError.Trim().Substring(0, strError.Trim().Length-1);
            }
            return strError;
        }
        private void SetFocusWhenErrorControl()
        {
            if (ProcessGeneral.GetSafeString(lueCC.EditValue) == string.Empty)
            {
                lueCC.Select();
                return;
            }
            if (ProcessGeneral.GetSafeString(txeFinYear.EditValue) == string.Empty)
            {
                txeFinYear.Select();
                return;
            }
            if (ProcessGeneral.GetSafeString(txtUser.EditValue) == string.Empty)
            {
                txtUser.Select();
                return;
            }
            if (ProcessGeneral.GetSafeString(txtPass.EditValue) == string.Empty)
            {
                txtPass.Select();
                return;
            }
          
            
        }
        #endregion

        #region "button click event"
        private void pictureBoxExit_Click(object sender, EventArgs e){
            Application.ExitThread();Application.Exit();
        }
        private void btnLogon_Click(object sender, EventArgs e)
        {

            bool isConnect = Program.CheckOpenConnectionSQL();
            if (!isConnect)
            {
                
                return;
            }

            string strError=CheckInputFieldOnControl();
            if (!string.IsNullOrEmpty(strError.Trim()))
            {
                XtraMessageBox.Show(string.Format("{0} could not empty !.",strError), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
                SetFocusWhenErrorControl();
                return;
            }

            DataTable dtLoginSuccess = _ctrl.CheckUserLogin(txtUser.Text.Trim(), EnDeCrypt.Encrypt(txtPass.Text.Trim(),true));


           
            if (dtLoginSuccess.Rows.Count > 0)
            {
                // gan gia tri sau dang nhap
                SystemProperty.SysUserId = ProcessGeneral.GetSafeInt64(dtLoginSuccess.Rows[0]["UserID"]);
                SystemProperty.SysUserName = ProcessGeneral.GetSafeString(txtUser.EditValue);
                SystemProperty.SysFullName = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["FullName"]);
                SystemProperty.SysEmail = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["Email"]);
                SystemProperty.SysPassword = ProcessGeneral.GetSafeString(txtPass.EditValue);


                string emailPass = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["EmailPassword"]);
                if (!string.IsNullOrEmpty(emailPass))
                {
                    SystemProperty.SysDefaultSendMailPass = EnDeCrypt.Decrypt(emailPass, true);
                }



                SystemProperty.SysFinanceYear = ProcessGeneral.GetSafeInt(txeFinYear.EditValue);
                SystemProperty.SysPositionsCode = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["PositionsCode"]);
                SystemProperty.SysPositionsName = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["PositionsName"]);
                SystemProperty.SysDepartmentCode = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["DepartmentCode"]);
                SystemProperty.SysDepartmentName = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["DepartmentName"]);
                SystemProperty.SysCompanyCode = ProcessGeneral.GetSafeString(lueCC.EditValue);
                SystemProperty.SysChangePassDate = ProcessGeneral.GetSafeString(dtLoginSuccess.Rows[0]["ChangePassDate"]);
                SystemProperty.SysAccount = txtUser.Text.Trim();
                Settings.Default.UserNameSetting = txtUser.Text.Trim();
                Settings.Default.PasswordSetting = chkRemember.Checked ? EnDeCrypt.Encrypt(txtPass.Text.Trim(),true) : string.Empty;
                Settings.Default.RememberPassSetting = chkRemember.Checked;
                Settings.Default.CompanyCodeSetting = ProcessGeneral.GetSafeString(lueCC.EditValue);
                Settings.Default.Save();
                //
                DataTable dtGroup = _ctrl.GetGroupWhenLogin(SystemProperty.SysUserId);
                if (dtGroup.Rows.Count > 0)
                {
                    SystemProperty.SysIdAuthorization = ProcessGeneral.GetSafeInt64(dtGroup.Rows[0]["IDAuthorization"]);
                    SystemProperty.SysUserInGroupId = ProcessGeneral.GetSafeInt64(dtGroup.Rows[0]["UserInGroupID"]);
                    SystemProperty.SysUserGroupCode = ProcessGeneral.GetSafeString(dtGroup.Rows[0]["GroupUserCode"]); ;
                    SystemProperty.SysPermissionGroupCode = ProcessGeneral.GetSafeString(dtGroup.Rows[0]["PermisionGroupCode"]); ;
                }
                else
                {
                    SystemProperty.SysIdAuthorization = 0;
                    SystemProperty.SysUserInGroupId = 0;
                    SystemProperty.SysUserGroupCode = "";
                    SystemProperty.SysPermissionGroupCode = "";
                }
                SystemProperty.SysDtCbUserGroup = _ctrl.LoadCbGroupCodeWhenLogin(SystemProperty.SysUserId);

                if (!File.Exists(Application.StartupPath + @"\Extension\CNY.ini"))
                {
                    XtraMessageBox.Show("Config File not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                }


                SystemProperty.SysWorkingServer = ProcessGeneral.GetSafeString(lookupServer.EditValue);
                var ini = new IniFile();
                ini.Load(Application.StartupPath + @"\Extension\CNY.ini");
                ini.SetKeyValue("system", "WorkingServer", EnDeCrypt.Encrypt(SystemProperty.SysWorkingServer, true));
                ini.Save(Application.StartupPath + @"\Extension\CNY.ini");



                this.Close();
                Program.OpenMainFormByThread();
            }
            else
            {
                XtraMessageBox.Show("Username or Password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
        }

       
        


       


        #endregion

        #region "combobox textbox event"

        private void lookupServer_EditValueChanged(object sender, EventArgs e)
        {

            SystemProperty.SysWorkingServer = ProcessGeneral.GetSafeString(lookupServer.EditValue);
            Program.GetConnectStringServerMain();
            _ctrl = new Ctr_Login();
            GetSystemInfo();



        }

        private void lookupServer_Popup(object sender, EventArgs e)
        {
            var edit = sender as UserLookUpEdit;
            var popupControl = (IPopupControl)edit;
            if (popupControl == null) return;
            var f = popupControl.PopupWindow as UserPopupLookUpEditForm;
            var dtHeight = (DataTable)edit.Properties.DataSource;
            int dropDownRow = edit.Properties.DropDownRows;
            if (f == null) return;
            f.Width = LookUpEditCompWidth;
            if (dtHeight.Rows.Count < dropDownRow)
            {
                f.Height = (dtHeight.Rows.Count + 1) * edit.Properties.DropDownItemHeight;
            }
        }


        private void lueCC_EditValueChanged(object sender, EventArgs e)
        {
            var lE = sender as LookUpEdit;
            if (lE == null) return;
            if (lE.Properties.DataSource == null)
            {
                txtComp.EditValue = "";
                return;
            }
            var drv = lE.Properties.GetDataSourceRowByKeyValue(lE.EditValue) as DataRowView;
            txtComp.EditValue = drv != null ? ProcessGeneral.GetSafeString(drv.Row["NameCCode"]) : "";
        }

        private void lueCC_Popup(object sender, EventArgs e)
        {
            var edit = sender as UserLookUpEdit;
            var popupControl = (IPopupControl) edit;
            if (popupControl == null) return;
            var f = popupControl.PopupWindow as UserPopupLookUpEditForm;
            var dtHeight = (DataTable)edit.Properties.DataSource;
            int dropDownRow = edit.Properties.DropDownRows;
            if (f == null) return;
            f.Width = LookUpEditCompWidth;
            if (dtHeight.Rows.Count < dropDownRow)
            {
                f.Height = (dtHeight.Rows.Count + 1) * edit.Properties.DropDownItemHeight;
            }
        }


        private void txeFinYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (!Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                    e.Handled = true;
            }
        }
        #endregion

        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Escape:
                    {
                        pictureBoxExit_Click(null, null);
                        return true;
                    }
               
                case Keys.Control | Keys.Alt | Keys.S:
                    {
                        this.Close();
                        var fShow = new FrmConfigSQLServer();
                        Program.OpenFormByThread(fShow, false);
                        return true;
                    }
                case Keys.Control | Keys.Alt | Keys.C:
                    {
                        try
                        {
                            const string command = @"lodctr /R";
                            var procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                                                    {
                                                        RedirectStandardOutput = true, 
                                                        UseShellExecute = false,
                                                        CreateNoWindow = true
                                                    };

                            var proc = new Process {StartInfo = procStartInfo};
                            proc.Start();
                            string result = proc.StandardOutput.ReadToEnd();
                            XtraMessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return true;
                    }

            }
            return base.ProcessCmdKey(ref message, keys);
        }






        #endregion

        
    }
}