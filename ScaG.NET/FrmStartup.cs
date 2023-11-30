using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CNY_Main.Class;
using CNY_Property;

namespace CNY_Main
{
    public partial class FrmStartup : XtraForm
    {
        #region "Property"
        readonly BackgroundWorker bwMain;
        #endregion

        #region "Contructor"
        public FrmStartup()
        {
            InitializeComponent();
            progressBarControl1.Visible = false;
            Program.GetInfoApplication();
            pictureLogo.Visible = true;
            lblSofName.ForeColor = Color.FromArgb(239, 172, 3);
            lblPlatform.ForeColor = Color.FromArgb(140, 140, 140);
            string version = string.IsNullOrEmpty(SystemProperty.SysVersion) ? @"1.0.0.0" : SystemProperty.SysVersion;
            string finalRelease = SystemProperty.SysVersion.IndexOf(".", StringComparison.Ordinal) > 0 ? string.Format("{0}.x", SystemProperty.SysVersion.Substring(0, SystemProperty.SysVersion.IndexOf(".", StringComparison.Ordinal))) : @"1.x";
            lblVersion.Text = string.Format("{0} Final Release | Version {1}", finalRelease, version);
            lblCopyRight.Text = string.Format("Copyright \u00a9  CNY 2015-{0}", DateTime.Now.Year);
            bwMain = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bwMain.DoWork += bwMain_DoWork;
            bwMain.ProgressChanged += bwMain_ProgressChanged;
            bwMain.RunWorkerCompleted += bwMain_RunWorkerCompleted;

        }
        #endregion

        #region "Process Background worker"

        private void bwMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!bwMain.CancellationPending)
            {

                if (e.ProgressPercentage >= progressBarControl1.Properties.Minimum && e.ProgressPercentage <= progressBarControl1.Properties.Maximum)
                {
                    this.Opacity = e.ProgressPercentage;
                    progressBarControl1.EditValue = e.ProgressPercentage;
                }

            }

        }


        private void bwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            DoSomething();

        }

        private void DoSomething()
        {

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(20);
                bwMain.ReportProgress(i, String.Format("Step {0} / 100...", i));

            }
            if (progressBarControl1.InvokeRequired)
            {
                progressBarControl1.Invoke(new MethodInvoker(() => { progressBarControl1.Visible = false; }));
            }
            else
            {
                progressBarControl1.Visible = false;
            }
            Program.GetRAMCounter(Program.GetProgramProcess());


            if (TempParameter.IsCheckConnectionOpenForm)
            {
                connectResult = Program.CheckOpenConnectionSQL();
            }
            else
            {
                connectResult = true;
            }
    
        }
       
        private bool connectResult=false;
        private void bwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mqSplash.Visible = false;
            this.Close();
            Form fShow;
            if (TempParameter.IsCheckConnectionOpenForm)
            {
                if (connectResult)
                {

                    fShow = new FrmLogin();
                }
                else
                {
                    fShow = new FrmConfigSQLServer();
                }
            }
            else
            {
                fShow = new FrmLogin();
            }
         
            // fShow.TopMost = true;
            Program.OpenFormByThread(fShow, false);
        }
        #endregion

        #region "Form Event"
        private void FrmStartup_Load(object sender, EventArgs e)
        {
            bwMain.RunWorkerAsync();
        }
        #endregion




    }
}