using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Info;
using CNY_Buyer.UControl;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_Buyer.WForm
{
    public partial class Frm006MaterialReceipt : FrmBase
    {
        #region "Property"

        private XtraUC006MaterialReceipt xtraUCMain;
        private XtraUC006MaterialReceiptAE xtraUCMainAE;
        private GridView gvMain;
        public static bool ClearError;
        private bool allowRefreshMethold;
        public static bool RoleInsert;
        public static bool RoleUpdate;
        public static bool RoleDelete;
        public static bool RoleView;
        public static DataTable DtAFunction;
        public static DataTable DtSFunction;
        private WaitDialogForm dlg;

        private string option = "";


        #endregion "Property"

        #region "Contructor"

        public Frm006MaterialReceipt()
        {
            InitializeComponent();

            allowRefreshMethold = true;
            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            RoleInsert = PerIns;
            RoleUpdate = PerUpd;
            RoleDelete = PerDel;
            RoleView = PerViw;
            DtAFunction = DtPerFunction;
            DtSFunction = DtSpecialFunction;
            xtraUCMain = new XtraUC006MaterialReceipt()
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCM",
            };

            panelControlAdd.Controls.Add(xtraUCMain);
            ChangeMenuBarButtonCloseAE();
            gvMain = xtraUCMain.gvMainC;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvMain.KeyDown += gvMain_KeyDown;
        }

        public void ChangeMenuBarButtonCloseAE()
        {
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCancel = false;
            AllowGenerate = false;
            AllowCopyObject = RoleInsert;
            AllowCombine = false;
            AllowCheck = false;
            AllowPrint = false;
            AllowAdd = true;
            AllowEdit = true;
            AllowSave = false;
            AllowDelete = true;
            AllowFind = false;
            AllowRefresh = true;
            EnableAdd = RoleInsert;
            EnableEdit = true;
            EnableDelete = RoleDelete;
            AllowGenerate = false;

        }

        #endregion "Contructor"

        #region "Gridview"
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            PerformEdit();
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
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

        #endregion "Gridview"



        #region "Override button menubar click"

        protected override void PerformAdd()
        {
            if (!RoleInsert)
            {
                XtraMessageBox.Show("You are not authorized to perform the function add new data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            xtraUCMain.Visible = false;
            AddUserControlMainAE(-1, "Add");
            ChangeMenuBarButtonOpenAE();
        }

        protected override void PerformEdit()
        {
            if (!gvMain.IsDataRow(gvMain.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
            AddUserControlMainAE(pk, "Edit");
            ChangeMenuBarButtonOpenAE();
        }

        protected override void PerformSave()
        {
            xtraUCMainAE.Save();
            PerformRefresh();
        }

        protected override void PerformCancel()
        {
            option = "";
            ClearError = true;
            ChangeMenuBarButtonCloseAE();
            RemoveUserControlMainAE(panelControlAdd, "xtraUCMainAE");
            xtraUCMain.Visible = true;
        }

        protected override void PerformPrint()
        {
            if (xtraUCMainAE.Visible)
            {
                xtraUCMainAE.Print();
            }
        }

        protected override void PerformRefresh()
        {
            if (!allowRefreshMethold)
            {
                dlg = new WaitDialogForm("");
                if (!xtraUCMain.Visible)
                {
                    if (option.ToUpper() == "ADD")
                    {
                        xtraUCMainAE.ClearForm();
                        xtraUCMainAE.SetDefaultInfo();
                    }
                    else
                    {
                        xtraUCMainAE.DisplayDataForEditing();
                    }
                }
                else
                {
                    xtraUCMain.LoadData();
                }
                dlg.Close();
            }
            allowRefreshMethold = false;
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

        protected override void PerformCopy()
        {
            if (!gvMain.IsDataRow(gvMain.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform copying", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
            AddUserControlMainAE(pk, "Copy");
            ChangeMenuBarButtonOpenAE();
        }
        protected override void PerformGenerate()
        {
            if (xtraUCMainAE.Visible)
            {
                xtraUCMainAE.Generate();
            }
        }
        #endregion "Override button menubar click"

        #region "ChangeMenuBarButtonOpenAE

        public void ChangeMenuBarButtonOpenAE()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowFind = false;
            AllowSave = RoleUpdate;
            EnableSave = true;
            AllowPrint = true;
            AllowCopyObject = false;
            AllowGenerate = true;
        }

        #endregion "ChangeMenuBarButtonOpenAE

        #region Add/Remove User Control

        private void AddUserControlMainAE(int ID, string Otp)
        {
            dlg = new WaitDialogForm("");
            xtraUCMain.Visible = false;
            xtraUCMainAE = new XtraUC006MaterialReceiptAE(ID, Otp)
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCMainAE",
            };
            panelControlAdd.Controls.Add(xtraUCMainAE);
            dlg.Close();
        }

        private void RemoveUserControlMainAE(Control root, string target)
        {
            xtraUCMainAE = ProcessGeneral.FindControl(root, target) as XtraUC006MaterialReceiptAE;
            if (xtraUCMainAE != null)
            {
                root.Controls.Remove(xtraUCMainAE);
            }
        }

        #endregion Add/Remove User Control
    }
}