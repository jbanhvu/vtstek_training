using System;
using DevExpress.XtraEditors;
using CNY_Property;

namespace CNY_Main
{
    public partial class FrmAboutForm : XtraForm
    {
        public FrmAboutForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
         
        }

        private void FrmAboutForm_Load(object sender, EventArgs e)
        {
            string version = string.IsNullOrEmpty(SystemProperty.SysVersion) ? @"1.0.0.0" : SystemProperty.SysVersion;
            lblVersion.Text = string.Format("(version {0})", version);
            lblCopyRight.Text = string.Format("Copyright \u00a9 2015-{0} VTStek", DateTime.Now.Year);

            lblLink.AllowHtmlString = true;
            lblLink.Text = @"<b><color=5,150,123>Internet : </color></b><link=vtstek.com/>www.vtstek.com</link>";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}