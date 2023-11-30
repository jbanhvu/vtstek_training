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
using System.Text;
using System.Windows.Forms;

namespace CNY_WH.WForm
{
    public partial class Frm003StockType : FrmBase
    {
        Inf_003StockType inf;
        bool Isupdate;
        public Frm003StockType()
        {
            InitializeComponent();
            inf = new Inf_003StockType();
            this.Load += Frm003StockType_Load;
            txtName.Enabled = false;
        }

        private void Frm003StockType_Load(object sender, EventArgs e)
        {
            HidenButton();
            LoadData();
            gvStockType.FocusedRowChanged += GvStockType_FocusedRowChanged;
        }

        private void GvStockType_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtName.Text = ProcessGeneral.GetSafeString(gvStockType.GetRowCellValue(gvStockType.FocusedRowHandle, "Name"));
        }

        public void LoadData()
        {
            gcStockType.DataSource = inf.sp_StockType_Select();
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
            txtName.Enabled = true;
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
            txtName.Enabled = false;
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
            txtName.Enabled = true;
        }

        protected override void PerformSave()
        {
            int PK = ProcessGeneral.GetSafeInt(gvStockType.GetListSourceRowCellValue(gvStockType.FocusedRowHandle, "PK"));
            string Name = txtName.Text;
            DataTable dtresult = new DataTable();
            if (Isupdate)
            {
                dtresult = inf.sp_StockType_Update(PK, Name);
            }
            else
            {
                dtresult = inf.sp_StockType_Insert(Name);
            }
            string msg = ProcessGeneral.GetSafeString(dtresult.Rows[0]["ErrMsg"]);

            XtraMessageBox.Show(msg, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            PerformCancel();
        }
    }
}