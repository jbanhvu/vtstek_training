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
    public partial class FrmUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
        private bool _updateDll = false;
        private bool _updateReport = false;
        private bool _isCancel = false;
        private bool _connect=false;
        #endregion

        #region "Contructor"
        public FrmUpdate(bool updateDll,bool updateReport,bool connect)
        {
            InitializeComponent();
            mqProcess.Visible = false;
            this.Load += FrmUpdate_Load;
            this.FormClosing += FrmUpdate_FormClosing;
            btnCancel.Click += btnCancel_Click;
            this._updateDll = updateDll;
            this._updateReport = updateReport;
            this._connect = connect;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            backgroundWorker1 = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

     

   
        #endregion

        #region "Form Event"

        private void FrmUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy && !_isCancel)
            {
                DialogResult kq = XtraMessageBox.Show("Are you sure to cancel the update process??", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    backgroundWorker1.CancelAsync();
                    backgroundWorker1.Dispose();
                    DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
                }
            }
           
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            mqProcess.Visible = true;
        }

        #endregion


        #region "button click event"

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult kq = XtraMessageBox.Show("Are you sure to cancel the update process??", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                _isCancel = true;
                backgroundWorker1.CancelAsync();
                backgroundWorker1.Dispose();
                this.Close();
                DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));

            }
            else
            {
                _isCancel = false;
            }
        }
        #endregion

        #region "Process Backgrouound worker"
      

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            DeclareSystem.LaunchExeWithOutThread(string.Format("{0}\\{1}", Application.StartupPath, DeclareSystem.RunExeMainName));
        }
       
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DoSomething();
        }
        private void DoSomething()
        {
            
            if (!Directory.Exists(Application.StartupPath + @"\Extension"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Extension"); //Tao folder moi
            }
            string[] filesSystem = Directory.GetFiles(DeclareSystem.SysPathUpdate);
            foreach (string fS in filesSystem)
            {
                string fsName = Path.GetFileName(fS);
                if (fsName.ToUpper().Trim() == "CNY.INI" || fsName.ToUpper().Trim() == "VERSION.INI")
                {
                   Copy(fS, Path.Combine(Application.StartupPath + @"\Extension", fsName), true);
                }
            }
            //------------------------
            bool updateAllDll = false;
            bool updateAllReport = false;
            var dtCom = new DataTable();
            var dtRep = new DataTable();
            if (!_connect)
            {
                updateAllDll = true;
                updateAllReport = true;
            }
            else
            {
                dtCom = DeclareSystem.TblReadDataSQL(DeclareSystem.SysConnectionString, "select ComponentName,[FileName] from UpdateComponent Where isnull(IsUpdate,0)=1", null);
                updateAllDll = dtCom.Rows.Count <= 0;
                dtRep = DeclareSystem.TblReadDataSQL(DeclareSystem.SysConnectionString, "select ReportName,[FileName] from UpdateReport Where isnull(IsUpdate,0)=1", null);
                updateAllReport = dtRep.Rows.Count <= 0;
              
            }
            if (_updateDll)
            {
                string[] filesDll = Directory.GetFiles(DeclareSystem.SysPathDllServer);
                if (updateAllDll)
                {
                    foreach (string fDa in filesDll)
                    {
                       Copy(fDa, Path.Combine(Application.StartupPath, Path.GetFileName(fDa)), true);
                    }
                }
                else
                {
                    var queryDll = from p in filesDll
                                   join t in dtCom.AsEnumerable() on Path.GetFileName(p).ToUpper().Trim() equals t.Field<string>("FileName").ToUpper().Trim()
                                   select new
                                              {
                                                  PathDLL = p,
                                                  FileNameDLL = Path.GetFileName(p)
                                              };
                    foreach (var itemDll in queryDll)
                    {
                       Copy(itemDll.PathDLL, Path.Combine(Application.StartupPath, itemDll.FileNameDLL), true);
                    }
                }
              
            }
            if (_updateReport)
            {
                if (!Directory.Exists(Application.StartupPath + @"\Report"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\Report"); //Tao folder moi
                }
                string[] filesReport = Directory.GetFiles(DeclareSystem.SysPathReportServer);
                if (updateAllReport)
                {
                    foreach (string fRa in filesReport)
                    {
                       Copy(fRa, Path.Combine(Application.StartupPath + @"\Report", Path.GetFileName(fRa)), true);
                    }
                }
                else
                {
                    var queryReport = from p in filesReport
                                      join t in dtRep.AsEnumerable() on Path.GetFileName(p).ToUpper().Trim() equals t.Field<string>("FileName").ToUpper().Trim()
                                      select new
                                                 {
                                                     PathReport = p,
                                                     FileNameReport = Path.GetFileName(p)
                                                 };
                    foreach (var itemReport in queryReport)
                    {
                        
                        Copy(itemReport.PathReport, Path.Combine(Application.StartupPath + @"\Report", itemReport.FileNameReport), true);
                    }
                }
            }
            Thread.Sleep(5000);
        }


        private void Copy(string sourceFile,string destFile,bool overideFile)
        {
            if (File.Exists(destFile))
            {
                File.SetAttributes(destFile, FileAttributes.Normal);
            }
            File.Copy(sourceFile, destFile, overideFile);
        }

        #endregion
    }
}