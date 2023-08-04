using System.Windows.Forms;
using DevExpress.XtraEditors;
using CNY_BaseSys.Interfaces;

namespace CNY_BaseSys.WForm
{
    public partial class FrmModalCompositeProgressForm : XtraForm
    {
        public FrmModalCompositeProgressForm(
          ICompositeProgressiveOperation operation)
        {
            InitializeComponent();

            // Subscribe to operation events
            operation.NewOperation += (sender, e) =>
                {
                    lblCurrentComponent.Text =
                        operation.CurrentOperation.MainTitle;

                    Refresh();
                };

            operation.OperationStart += (sender, e) =>
                {
                    lblTitle.Text = operation.MainTitle;
                    lblSubtitle.Text = operation.SubTitle;

                    Refresh();
                };

            operation.OperationProgress += (sender, e) =>
                {
                    pgbOverallProgress.EditValue = operation.CurrentProgress;
                    pgbCurrentProgress.EditValue = operation.CurrentOperation.CurrentProgress;

                    Application.DoEvents();
                };

            operation.OperationEnd += (sender, e) => Close();

            // Subscribe to Shown event of the form
            Shown += (sender, e) => operation.Start();
        }
    }
}