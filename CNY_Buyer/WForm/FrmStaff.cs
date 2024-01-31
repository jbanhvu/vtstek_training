using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Info;
using CNY_Buyer.UControl;
using DevExpress.Diagram.Core.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;

namespace CNY_Buyer.WForm
{
    public partial class FrmStaff : FrmBase
    {
        #region Properties
        private Inf_Staff _inf;
        private XtraUCStaff xtraUCMain;
        private GridView gvMain;
        private XtraUCStaffAE xtraUCMainAE;
        private WaitDialogForm dlg;

        #endregion

        #region Constructor
        public FrmStaff()
        {
            InitializeComponent();
            this.Load += FrmStaff_Load;
            _inf = new Inf_Staff();
        }
        #endregion

        #region Load Data
        private void FrmStaff_Load(object sender, EventArgs e)
        {
            xtraUCMain = new XtraUCStaff()
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCMain",
            };
            panelControlAdd.Controls.Add(xtraUCMain);
            ChangeMenuBarButtonCloseAE();
            gvMain = xtraUCMain.gvMainC;
            gvMain.DoubleClick += GvMain_DoubleClick;
            gvMain.KeyDown += GvMain_KeyDown;
        }

        #endregion

        #region Handling Grid Click + Keydown
        private void GvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformEdit();
            }
            if (e.KeyCode == Keys.Delete)
            {
                PerformDelete();
            }
        }

        private void GvMain_DoubleClick(object sender, EventArgs e)
        {
            PerformEdit();
        }
        #endregion

        #region Change Menu Bar
        public void ChangeMenuBarButtonCloseAE()
        {
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCancel = false;
            AllowGenerate = false;
            AllowCopyObject = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowPrint = false;
            AllowClose = true;
            AllowAdd = true;
            AllowEdit = true;
            AllowSave = false;
            AllowDelete = true;
            AllowFind = false;
            AllowRefresh = true;
            EnableAdd = true;
            EnableEdit = true;
            EnableDelete = true;

        }
        public void ChangeMenuBarButtonOpenAE()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowFind = false;
            AllowSave = true;
            AllowClose = false;
            AllowCancel = true;
            EnableCancel = true;
            EnableSave = true;
            AllowPrint = false;
            AllowCopyObject = false;
        }
        #endregion

        #region Overide Button
        protected override void PerformAdd()
        {
            xtraUCMain.Visible = false;
            AddUserControlMainAE(-1, "Add");
            ChangeMenuBarButtonOpenAE();
        }
        protected override void PerformEdit()
        {
            if (!gvMain.IsDataRow(gvMain.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
            AddUserControlMainAE(pk, "Edit");
            ChangeMenuBarButtonOpenAE();
        }
        protected override void PerformDelete()
        {
            if (!gvMain.IsDataRow(gvMain.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
            _inf.Excute($"DELETE FROM DBO.STAFF WHERE PK = {pk}");

            PerformRefresh();
        }
        protected override void PerformRefresh()
        {
                dlg = new WaitDialogForm("");
                if (!xtraUCMain.Visible)
                {
                  xtraUCMainAE.DisplayDataForEditing();
                }
                else
                {
                    xtraUCMain.LoadData();
                }
                dlg.Close();
        }
        protected override void PerformCancel()
        {
            ChangeMenuBarButtonCloseAE();
            RemoveUserControlMainAE(panelControlAdd, "xtraUCMainAE");
            xtraUCMain.Visible = true;
        }
        protected override void PerformClose()
        {
            if (xtraUCMain.Visible)
            {
                this.Close();
            }
            else
            {
                PerformCancel();
            }
        }
        protected override void PerformSave()
        {
            xtraUCMainAE.Save();
            PerformRefresh();
        }
        #endregion

        #region Add/Remove User Control
        private void AddUserControlMainAE(int id, string otp)
        {
            dlg = new WaitDialogForm("");
            xtraUCMain.Visible = false;
            xtraUCMainAE = new XtraUCStaffAE(id, otp)
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCMainAE",
            };
            panelControlAdd.Controls.Add(xtraUCMainAE);
            dlg.Close();
        }

        private void RemoveUserControlMainAE(Control root, string target)
        {
            xtraUCMainAE = ProcessGeneral.FindControl(root, target) as XtraUCStaffAE;
            if (xtraUCMainAE != null)
            {
                root.Controls.Remove(xtraUCMainAE);
            }
        }

        #endregion Add/Remove User Control
    }
}
