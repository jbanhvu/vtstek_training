using System.Windows.Forms;
using DevExpress.XtraEditors;
using CNY_BaseSys.Interfaces;

namespace CNY_BaseSys.WForm
{
    public partial class FrmModalProgressForm : XtraForm
    {
        public FrmModalProgressForm(IProgressiveOperation operation)
        {
            InitializeComponent();

            // Subscribe to the operation events
            operation.OperationStart += (sender, e) =>
                {
                    lblTitle.Text = operation.MainTitle;
                    lblSubtitle.Text = operation.SubTitle;
                    pgb.EditValue = operation.CurrentProgress;

                    Refresh();
                };

            operation.OperationProgress += (sender, e) =>
                {
                    pgb.EditValue = operation.CurrentProgress;

                    Application.DoEvents();
                };

            operation.OperationEnd += (sender, e) => Close();

            // Subscribe to Shown event of the Form
            Shown += (sender, e) => operation.Start();
        }
    }
}