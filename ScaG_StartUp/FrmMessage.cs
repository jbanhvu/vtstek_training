using System;

namespace CNY_StartUp
{
    public partial class FrmMessage : DevExpress.XtraEditors.XtraForm
    {
        #region "Contructor"
        public FrmMessage()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            lblVersion.Text = string.Format("CNY lastet version: {0}", DeclareSystem.SysVersionServer);
            lblVersionServer.Text = string.Format("CNY installed version: {0}", DeclareSystem.SysVersion);
            lblVersionReport.Text = string.Format("CNY report lastet version: {0}", DeclareSystem.SysVersionReportServer);
            lblVersionReportServer.Text = string.Format("CNY report installed version: {0}", DeclareSystem.SysVersionReport);
            this.Load += FrmMessage_Load;

        }

        private void FrmMessage_Load(object sender, EventArgs e)
        {
            if (!btnYes.Focused)
            {
                btnYes.SelectNextControl(ActiveControl, true, true, true, true);
                btnYes.Focus();
            }
        }
        #endregion

        #region "Button Click Event"
        private void btnNo_Click(object sender, EventArgs e)
        {
            DeclareSystem.IsCancel = false;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DeclareSystem.IsCancel = true;
            this.Close();
        }
        #endregion
    }
}