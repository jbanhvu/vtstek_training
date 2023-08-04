using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CNY_StartUp
{
    public partial class FormVitural : DevExpress.XtraEditors.XtraForm
    {

        #region "Property"
        private static Mutex _mutex;
        const string MutexName = "Local\\{4db335a5-e1e9-4453-b000-66713ae619f2_19901111_19901006_CNY_Main}";
        readonly BackgroundWorker bwMain;
        #endregion

        #region "Contructor"

        public FormVitural()
        {
            InitializeComponent();
            this.Load += FormVitural_Load;
            bwMain = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bwMain.DoWork += bwMain_DoWork;
            bwMain.RunWorkerCompleted += bwMain_RunWorkerCompleted;
       
        }
        #endregion

        #region "Process Background worker"


        private void bwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckForUpdate();
        }

  

        private void bwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
         
            this.Close();
           
        }
        #endregion


        #region "Form Event"
        private void FormVitural_Load(object sender, EventArgs e)
        {
            bwMain.RunWorkerAsync();
            
         

        }
        #endregion

        #region "Check Update Process"
        public void CheckForUpdate()
        {
            if (!File.Exists(Application.StartupPath + @"\Extension\CNY.ini") ||
                !File.Exists(Application.StartupPath + @"\Extension\Version.ini"))
            {
                //this.Close();
                DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
                return;
            }

            using ( var iniS = new IniFile())
            {
                iniS.Load(Application.StartupPath + @"\Extension\CNY.ini");
                DeclareSystem.SysConnectionString = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "ConnectionString"), true);
            }


            using (var iniV = new IniFile())
            {
                iniV.Load(Application.StartupPath + @"\Extension\Version.ini");
                DeclareSystem.SysVersion = iniV.GetKeyValue("version", "VersionCNY");
                DeclareSystem.SysVersionReport = iniV.GetKeyValue("version", "VersionReport");
                DeclareSystem.SysPathUpdate = iniV.GetKeyValue("version", "UpdatePath");
               
             
                DeclareSystem.SysPathFileVersionServer = DeclareSystem.SysPathUpdate.Trim() + @"\Version.ini";


            }
            if (!File.Exists(DeclareSystem.SysPathFileVersionServer))
            {
              //  this.Close();
                DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
                return;
            }
            using (var iniF = new IniFile())
            {
                iniF.Load(DeclareSystem.SysPathFileVersionServer);
                DeclareSystem.SysVersionServer = iniF.GetKeyValue("version", "VersionCNY");
                DeclareSystem.SysVersionReportServer = iniF.GetKeyValue("version", "VersionReport");
                DeclareSystem.SysPathDllServer = iniF.GetKeyValue("version", "PathDLL");
                DeclareSystem.SysPathReportServer = iniF.GetKeyValue("version", "PathReport");
              

            }
            bool isUpdateDll = DeclareSystem.SysVersionServer.Trim() != DeclareSystem.SysVersion.Trim();
            bool isUpdateReport = DeclareSystem.SysVersionReportServer.Trim() != DeclareSystem.SysVersionReport.Trim();
            if (!isUpdateDll && !isUpdateReport)
            {
               // this.Close();
                DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
                return;
            }
            bool connect = DeclareSystem.GetConnectionSQL(DeclareSystem.SysConnectionString);
            if (connect)
            {
                DataTable dtMac = DeclareSystem.TblReadDataSQL(DeclareSystem.SysConnectionString, "select MachineName  from UpdateMachine Where isnull(IsUpdate,0)=1", null);
                if (dtMac.Rows.Count > 0)
                {
                    var query = dtMac.AsEnumerable()
                        .Where(p => p.Field<string>("MachineName").ToUpper().Trim() ==
                                    Environment.MachineName.ToUpper().Trim()).ToList();
                    if (query.Count <= 0) return;
                }
            }
          



            var f =new  FrmMessage();
            f.ShowDialog();
            if (!DeclareSystem.IsCancel)
            {
              //  this.Close();
                DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
                return;
            }
            bool onlyInstance = false;
       
            _mutex = new Mutex(false, MutexName, out onlyInstance);
            if (!onlyInstance)
            {
                DeclareSystem.IsCancel = false;
                XtraMessageBox.Show(
                    "Update Failed.\nYou have to close CNY Application in running.\nThen re-open CNY to complete the upgrade process.", "Exclamation", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
                return;
            }


         
            try
            {
                var fUpd = new FrmUpdate(isUpdateDll, isUpdateReport, connect);
                fUpd.ShowDialog();
              
            }
            catch 
            {
              //  this.Close();
                DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
                return;
            }

        }
        #endregion
    }
}