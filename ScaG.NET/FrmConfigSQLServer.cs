using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using CNY_BaseSys.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;
using CNY_BaseSys;
using System.IO;
using CNY_Main.Class;

namespace CNY_Main
{
    public partial class FrmConfigSQLServer : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
        Thread tExcute;
        #endregion

        #region "Contructor"
        public FrmConfigSQLServer()
        {
            InitializeComponent();
            mqProcess.Visible = false;
            int left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Top = top - 20;
            this.Left = left;
            txtDataSource.Properties.ReadOnly = true;
            rgChooseServer.SelectedIndex = 0;
            UseTextControlInputUserPass();
            LoadComboBoxEdit(lookupServerName, GetlocalSqlserverinstance(), "Name");
            LoadComboBoxEdit(cbIniKey, TableIniKeyInSectionSystem(), "Name");

            
        }
        #endregion

        #region "Load Data "
        /// <summary>
        ///     Load From CNY.ini 
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable TableIniKeyInSectionSystem()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            if (File.Exists(Application.StartupPath + @"\Extension\CNY.ini"))
            {
                var iniS = new IniFile();
                iniS.Load(Application.StartupPath + @"\Extension\CNY.ini");
                foreach (IniFile.IniSection.IniKey k in iniS.GetSection("system").Keys)
                {
                    if (!k.Name.ToLower().Trim().Contains("connectionstring")) continue;
                    dt.Rows.Add(k.Name.Trim());
                }
                if (dt.Rows.Count > 0)
                    return dt.AsEnumerable().OrderBy(p => p.Field<String>("Name")).CopyToDataTable();
                return dt;
        
            }
            return dt;
        }
        /// <summary>
        ///     Load Data Into Combobox Edit
        /// </summary>
        /// <param name="cbEdit" type="DevExpress.XtraEditors.ComboBoxEdit">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="dt" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="valueMember" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void LoadComboBoxEdit(ComboBoxEdit cbEdit, DataTable dt, string valueMember)
        {
            if (cbEdit.InvokeRequired)
            {
                cbEdit.Invoke(new MethodInvoker(() =>
                {
                    cbEdit.Properties.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        cbEdit.Properties.Items.Add(row[valueMember.Trim()]);
                    }
                }));

            }
            else
            {
                cbEdit.Properties.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    cbEdit.Properties.Items.Add(row[valueMember.Trim()]);
                }
            }
        }

       

        /// <summary>
        ///     template datasource for combobox edit,lookup edit control
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable TableTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            return dt;
        }

        /// <summary>
        ///     Get All SQl server intance name Network
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable Getnetworkallsqlinstance()
        {

                DataTable kq = TableTemplate();
                try
                {
                    var query = from p in SmoApplication.EnumAvailableSqlServers().AsEnumerable()
                                orderby p.Field<bool>("IsLocal") descending, p.Field<string>("Name") ascending                                 
                                select new { Name = p.Field<string>("Name"), };
                    foreach (var item in query)
                    {
                        kq.Rows.Add(item.Name);
                    }
                    if (kq.Rows.Count == 0)
                    {
                        kq=GetlocalSqlserverinstance();
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
          
            return kq;

        }

        /// <summary>
        ///     Get Local Server Name
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable GetlocalSqlserverinstance()
        {
            DataTable dt = TableTemplate();
            
            try
            {

                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                var instances = (String[])rk.GetValue("InstalledInstances");
                if (instances.Length > 0)
                {
                    dt.Rows.Add(System.Environment.MachineName);
                    foreach (String element in instances)
                    {
                        dt.Rows.Add(string.Format(@"{0}\{1}", System.Environment.MachineName, element));
                    }
                }
                rk.Close();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            return dt;
            
        }

        /// <summary>
        ///    Init Server SQL Database
        /// </summary>
        /// <param name="server" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="login" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="password" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="chonserver" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A Microsoft.SqlServer.Management.Smo.Server value...
        /// </returns>
        private Server InitializeServer(string server, string login, string password, bool chonserver)
        {
            var conn = new ServerConnection {ServerInstance = server};
            if (chonserver)
            {
                conn.LoginSecure = false;
                conn.Login = login;
                conn.Password = password;
            }
            return new Server(conn);
        }

        /// <summary>
        ///     Get All Database in Server
        /// </summary>
        /// <param name="server" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="login" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="password" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="chonserver" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable TableDatabase(string server, string login, string password, bool chonserver)
        {
            
            DataTable kq = TableTemplate();
            try
            {
                Server srv = InitializeServer(server, login, password, chonserver);                
                foreach (Database db in srv.Databases)
                {
                    kq.Rows.Add(db.Name.Trim());
                }

          //      kq = kq.DefaultView.ToTable(true,"Name");//distinct value
                IEnumerable<DataRow> query = from rc in kq.AsEnumerable()
                                             orderby rc.Field<string>("Name")
                                             select rc;
                kq = query.CopyToDataTable();                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            return kq;
        }

        #endregion

        #region "use control, get check server"

        private bool LoginAuthenticationSQL()
        {
            if (rgChooseServer.SelectedIndex == 0)
                return false;
            return true;
        }
        private void UseTextControlInputUserPass()
        {
            if (rgChooseServer.SelectedIndex == 0)
            {
                txtUser.EditValue = "";
                txtPass.EditValue = "";
                txtUser.Enabled = false;
                txtPass.Enabled = false;

            }
            else
            {
                txtUser.Enabled = true;
                txtPass.Enabled = true;
            }
        }
        private void rgChooseServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseTextControlInputUserPass();
           
        }

        private void EnableControlWhenFindServer(bool status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => {
                    mqProcess.Visible = !status;
                    txtDataSource.Enabled = status;
                    lookupServerName.Enabled = status;
                    btnRefresh.Enabled = status;
                    rgChooseServer.Enabled = status;
                    if (rgChooseServer.SelectedIndex == 1)
                    {
                        txtUser.Enabled = status;
                        txtPass.Enabled = status;
                    }
                    lookupDatabase.Enabled = status;
                    btnTestConnect.Enabled = status;
                    btnSave.Enabled = status;
                    btnClose.Enabled = status;
                    pClose.Enabled = status;
                    tExcute.Abort();
                }));
              
            }
            else
            {
                mqProcess.Visible = !status;
                txtDataSource.Enabled = status;
                lookupServerName.Enabled = status;
                btnRefresh.Enabled = status;
                rgChooseServer.Enabled = status;
                if (rgChooseServer.SelectedIndex == 1)
                {
                    txtUser.Enabled = status;
                    txtPass.Enabled = status;
                }
                lookupDatabase.Enabled = status;
                btnTestConnect.Enabled = status;
                btnSave.Enabled = status;
                btnClose.Enabled = status;
                pClose.Enabled = status;
            }
        }
        #endregion

        #region "button event click"
        private void pClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            Program.GetInfoApplication();
            if (TempParameter.IsCheckConnectionOpenForm && !Program.CheckOpenConnectionSQL()) return;

            this.Close();
            var fShow = new FrmLogin();
            Program.OpenFormByThread(fShow, false);
          
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strError = GetErrorControlInputSave();
            if (strError.Trim().Length != 0)
            {
                XtraMessageBox.Show(string.Format("{0} is not blank.", strError), "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            string strConnectTest = BuildConnectString(ProcessGeneral.GetSafeString(lookupServerName.EditValue),
                txtUser.Text.Trim(),
                txtPass.Text.Trim(), LoginAuthenticationSQL(),
                ProcessGeneral.GetSafeString(lookupDatabase.Text.Trim()), 15);
            bool staConnect = CheckConnection(strConnectTest.Trim());
            if (!staConnect) return;
            var ini = new IniFile();
            ini.Load(Application.StartupPath + @"\Extension\CNY.ini");
            if (!File.Exists(Application.StartupPath + @"\Extension\CNY.ini"))
            {
                XtraMessageBox.Show("Config File not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ProcessGeneral.GetSafeString(cbIniKey.EditValue) == "")
            {
                XtraMessageBox.Show("You do not select connect string key...!!!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                cbIniKey.Select();
                return;
            }
            string strConnect = BuildConnectString(
                ProcessGeneral.GetSafeString(lookupServerName.EditValue), txtUser.Text.Trim(),
                txtPass.Text.Trim(), LoginAuthenticationSQL(),
                ProcessGeneral.GetSafeString(lookupDatabase.Text.Trim()), 0);
            ini.SetKeyValue("system", ProcessGeneral.GetSafeString(cbIniKey.EditValue),
                EnDeCrypt.Encrypt(strConnect.Trim(), true));
            ini.Save(Application.StartupPath + @"\Extension\CNY.ini");
            XtraMessageBox.Show("Config connection succeed", "Information", MessageBoxButtons.OK,
                MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

            DialogResult dlRs =
                XtraMessageBox.Show("Do you want to continue config connection string system ???",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlRs == DialogResult.Yes) return;
            btnClose_Click(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadComboBoxEdit(lookupDatabase, TableTemplate(), "Name");
            EnableControlWhenFindServer(false);

             tExcute = new Thread(() =>
                                      {
                                          LoadComboBoxEdit(lookupServerName, Getnetworkallsqlinstance(), "Name");
                                          EnableControlWhenFindServer(true);
                                      });
            tExcute.Start();
        }
       
        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            string strError = GetErrorControlInputTest();
            if (strError.Trim().Length == 0)
            {
                string strConnect = BuildConnectString(ProcessGeneral.GetSafeString(lookupServerName.EditValue), txtUser.Text.Trim(), txtPass.Text.Trim(), LoginAuthenticationSQL(), ProcessGeneral.GetSafeString(lookupDatabase.Text.Trim()),15);
                bool staConnect = CheckConnection(strConnect.Trim());
                if (staConnect)
                {
                    XtraMessageBox.Show("Test connect succeeded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                XtraMessageBox.Show(string.Format("{0} is not blank.",strError), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
          
        }

        #endregion

        #region "get Error Input"
        /// <summary>
        ///     Get Controls Do not Input When Save Button Clicked
        /// </summary>
        /// <returns>
        ///     A string value...
        /// </returns>
        private string GetErrorControlInputSave()
        {
            string strError = "";
            if (ProcessGeneral.GetSafeString(lookupServerName.EditValue) == "")
            {
                strError += "Server Name, ";
            }            
            if (rgChooseServer.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(txtUser.Text.Trim()))
                {
                    strError += "Username, ";
                }
                if (string.IsNullOrEmpty(txtPass.Text.Trim()))
                {
                    strError += "Password, ";
                }
           
            }
            if (string.IsNullOrEmpty(lookupDatabase.Text.Trim()))
            {
                strError += "Database Name, ";
            }
            if (strError.Trim().Length > 0)
            {
                strError = strError.Trim().Substring(0, strError.Trim().Length-1);
            }
            return strError;
        }

        /// <summary>
        ///     Get Controls Do not Input When Test Connection Button Clicked
        /// </summary>
        /// <returns>
        ///     A string value...
        /// </returns>
        private string GetErrorControlInputTest()
        {
            string strError = "";
            if (ProcessGeneral.GetSafeString(lookupServerName.EditValue) == "")
            {
                strError += "Server Name, ";
            }
            if (rgChooseServer.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(txtUser.Text.Trim()))
                {
                    strError += "Username, ";
                }
                if (string.IsNullOrEmpty(txtPass.Text.Trim()))
                {
                    strError += "Password, ";
                }

            }
            if (strError.Trim().Length > 0)
            {
                strError = strError.Trim().Substring(0, strError.Trim().Length - 1);
            }
            return strError;
        }

        /// <summary>
        ///     Get Controls Do not Input When Load Information Database keydown event
        /// </summary>
        /// <returns>
        ///     A string value...
        /// </returns>
        private string GetErrorControlInputLoadDatabase()
        {
            string strError = "";          
            if (rgChooseServer.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(txtUser.Text.Trim()))
                {
                    strError += "Username, ";
                }
                if (string.IsNullOrEmpty(txtPass.Text.Trim()))
                {
                    strError += "Password, ";
                }

            }
            if (strError.Trim().Length > 0)
            {
                strError = strError.Trim().Substring(0, strError.Trim().Length - 1);
            }
            return strError;
        }
        #endregion
        
        #region "Process Connection"
        /// <summary>
        ///     Check For Opening SQL connection
        /// </summary>
        /// <param name="strConnect" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        private bool CheckConnection(string strConnect)
        {
            bool result = false;
            var conn = new SqlConnection(strConnect);
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                result = true;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                result = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }
        
      
        private string BuildConnectString(string server, string login, string password, bool chonserver, string dbnew,int timeOut)
        {
            return !chonserver ? string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connection Timeout={2}", server, dbnew, timeOut) :
string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connection Timeout={4}", server, dbnew, login, password, timeOut); 
        }
        #endregion

        #region "combobox event"
       
        private void lookupServerName_EditValueChanged(object sender, EventArgs e)
        {
            LoadComboBoxEdit(lookupDatabase, TableTemplate(), "Name");
        }

        private void lookupServerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadComboBoxEdit(lookupDatabase, TableTemplate(), "Name");
            }
        }

        private void lookupDatabase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ProcessGeneral.GetSafeString(lookupServerName.EditValue) != "")
                {
                    string strError = GetErrorControlInputLoadDatabase();
                    if (strError.Trim().Length == 0)
                    {
                        LoadComboBoxEdit(lookupDatabase, TableDatabase(ProcessGeneral.GetSafeString(lookupServerName.EditValue), txtUser.Text.Trim(), txtPass.Text.Trim(), LoginAuthenticationSQL()), "Name");
                    }
                    else
                    {
                        XtraMessageBox.Show(string.Format("{0} is not blank.", strError), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    }
                }
                else
                {
                    LoadComboBoxEdit(lookupDatabase, TableTemplate(), "Name");
                }
            }
        }
      

        #endregion

        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {

                case Keys.Control | Keys.S:
                    {

                        btnSave_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.T:
                    {

                        btnTestConnect_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.F5:
                    {
                        btnRefresh_Click(null, null);
                        return true;
                    }

            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion

    }
}