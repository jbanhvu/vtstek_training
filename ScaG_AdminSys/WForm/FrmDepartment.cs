using CNY_AdminSys.UControl;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
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

namespace CNY_AdminSys.WForm
{
    public partial class FrmDepartment : FrmBase
    {
        #region "Property"

        private XtraUserControl _ucMain = null;
        private PermissionFormInfo _perInfo;
        private WaitDialogForm _dlg;
        private string _option = "";

        #endregion

        public FrmDepartment()
        {
            InitializeComponent();
                LoadUC();
        }
        public void LoadUC()
        {
            _ucMain = new UCACF_Department
            {
                Dock = DockStyle.Fill,
                Name = "UCACF_Department"
            };
            pcAdd.Controls.Add(_ucMain);
            LoadButtonWhenLoadForm();
        }
        private void LoadButtonWhenLoadForm()
        {
            _perInfo = ProcessGeneral.GetPermissionByFormCode("FrmDepartment");
            AllowAdd = _perInfo.PerIns;
            AllowEdit = _perInfo.PerUpd;
            AllowDelete = _perInfo.PerDel;
            AllowSave = false;
            AllowCancel = false;
            AllowRefresh = true;
            AllowFind = false;
            AllowPrint = false;
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCopyObject = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowGenerate = false;

        }
        protected override void PerformAdd()
        {
                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                _option = "ADD";
                LoadButtonWhenAddEdit();
                ucMain.PerformAdd();
        }
        protected override void PerformEdit()
        {
           
                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                bool isEdit = ucMain.PerformEdit();
                if (!isEdit) return;
                _option = "EDIT";
            LoadButtonWhenAddEdit();
        }
        protected override void PerformDelete()
        {
                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                ucMain.PerformDelete();
        }
        protected override void PerformCancel()
        {
                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
                _option = "";
            LoadButtonWhenLoadForm();
                ucMain.PerformCancel();
        }
        protected override void PerformSave()
        {
                var ucMain = (UCACF_Department)_ucMain;
                if (ucMain == null) return;
            _dlg = new WaitDialogForm();
            bool isSave = ucMain.PerformSave(_option);
            _dlg.Close();
            if (!isSave) return;
                _option = "";
            LoadButtonWhenLoadForm();
        }
        private void LoadButtonWhenAddEdit()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowRefresh = false;
            AllowClose = false;
            AllowSave = true; 
            EnableSave = true;
            AllowCancel = true;
            EnableCancel = true;
        }
    }
}
