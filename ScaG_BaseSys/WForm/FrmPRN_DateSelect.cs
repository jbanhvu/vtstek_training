using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;

namespace CNY_BaseSys.WForm
{
    
    public partial class FrmPRN_DateSelect : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"



      
        public event OnGetValueHandler OnGetValue = null;
        private readonly List<DateTime> _lDate;
        #endregion

        #region "Contructor"

        public FrmPRN_DateSelect(List<DateTime> lDate)
        {
            InitializeComponent();
            _lDate = lDate;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += Frm_Load;
            btnClose.Click += btnClose_Click;
            btnOK.Click += btnOK_Click;
            dnMain.DisableCalendarDate += DnMain_DisableCalendarDate;
        }

        private void DnMain_DisableCalendarDate(object sender, DevExpress.XtraEditors.Calendar.DisableCalendarDateEventArgs e)
        {
            if (_lDate.Count <= 0)
            {

                e.IsDisabled = false;
                return;
            }

            e.IsDisabled = _lDate.Any(p => EqualsDate(p, e.Date));
        }

        private bool EqualsDate(DateTime startDate, DateTime endDate)
        {

            TimeSpan ts = endDate.Date - startDate.Date;
            if (ts.Days == 0)
                return true;
            return false;
        }



        private void Frm_Load(object sender, EventArgs e)
        {
            dnMain.Focus();
        }
        #endregion

        #region "Button Click Event"

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<DateTime> l = new List<DateTime>();
            SchedulerDateRangeCollection dateRange = dnMain.SelectedRanges;
            foreach (DateRange item in dateRange)
            {
                DateTime startDate = item.StartDate;
                DateTime endDate = item.EndDate;
                int loop = (endDate - startDate).Days;
                for (int i = 0; i <= loop; i++)
                {
                   DateTime date = startDate.AddDays(i);
                   if (dateRange.IsDateSelected(date))
                   {
                       l.Add(date);
                   }
                }
            }

            OnGetValue?.Invoke(this, new OnGetValueEventArgs
            {
                LDate = l.Distinct().ToList()
            });
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
