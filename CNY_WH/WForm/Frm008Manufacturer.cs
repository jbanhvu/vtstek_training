using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Info;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CNY_WH.WForm
{
    public partial class Frm008Manufacturer : FrmBase
    {
        Inf_008Manufacturer inf;
        bool Isupdate;
        public Frm008Manufacturer()
        {
            InitializeComponent();
            inf = new Inf_008Manufacturer();
            this.Load += Frm008Manufacturer_Load;
            txt_manufacuturer_code.Enabled = false;
            Txt_manufacturer_name.Enabled = false;
        }

        private void Frm008Manufacturer_Load(object sender, EventArgs e)
        {
            HidenButton();
            LoadData();
            gvManufacturer.FocusedRowChanged += gvManufacturer_FocusedRowChanged;
        }

        private void gvManufacturer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Txt_manufacturer_name.Text = ProcessGeneral.GetSafeString(gvManufacturer.GetRowCellValue(gvManufacturer.FocusedRowHandle, "Name"));
            txt_manufacuturer_code.Text = ProcessGeneral.GetSafeString(gvManufacturer.GetRowCellValue(gvManufacturer.FocusedRowHandle, "Code"));
        }
        private void txtTypeDesc_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            gcManufacturer.DataSource = inf.sp_Manufacturer_select();
        }

        public void HidenButton()
        {
            AllowAdd = true;
            AllowEdit = true;
            AllowDelete = true;
            AllowExpand = false;
            AllowGenerate = false;
            AllowCancel = true;
            AllowRevision = false;
            AllowSave = true;
            AllowFind = false;
            AllowPrint = false;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            EnableCancel = false;
        }
        protected override void PerformAdd()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowExpand = false;
            AllowGenerate = false;
            EnableCancel = true;
            AllowRevision = false;
            EnableSave = true;
            AllowFind = false;
            AllowPrint = false;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            AllowClose = false;
            Isupdate = false;
            txt_manufacuturer_code.Enabled = true;
            Txt_manufacturer_name.Enabled = true;
        }
        protected override void PerformCancel()
        {
            AllowAdd = true;
            AllowEdit = true;
            AllowDelete = true;
            AllowExpand = false;
            AllowGenerate = false;
            AllowCancel = true;
            AllowRevision = false;
            AllowSave = true;
            AllowFind = false;
            AllowPrint = false;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            EnableSave = false;
            AllowClose = true;
            EnableCancel = false;
            txt_manufacuturer_code.Enabled = false;
            Txt_manufacturer_name.Enabled = false;
        }
        protected override void PerformEdit()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowCancel = true;
            AllowDelete = false;
            AllowExpand = false;
            AllowGenerate = false;
            EnableCancel = true;
            AllowRevision = false;
            EnableSave = true;
            AllowFind = false;
            AllowPrint = false;
            AllowRefresh = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            AllowClose = false;
            Isupdate = true;
            txt_manufacuturer_code.Enabled = true;
            Txt_manufacturer_name.Enabled = true;
        }
        protected override void PerformSave()
        {
            int PK = ProcessGeneral.GetSafeInt(gvManufacturer.GetListSourceRowCellValue(gvManufacturer.FocusedRowHandle, "PK"));
            string Name = Txt_manufacturer_name.Text;
            string Code = txt_manufacuturer_code.Text;
            DataTable dtresult = new DataTable();
            if (Isupdate)
            {
                dtresult = inf.sp_Manufacturer_Update(PK, Name, Code);
            }
            else
            {
                dtresult = inf.sp_Manufacturer_Insert(Name, Code);
            }
            string msg = ProcessGeneral.GetSafeString(dtresult.Rows[0]["ErrMsg"]);

            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            PerformCancel();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
