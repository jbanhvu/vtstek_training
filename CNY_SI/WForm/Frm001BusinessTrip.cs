using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_SI.UControl;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
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
using CNY_SI.Info;
using DevExpress.Data.Async;

namespace CNY_SI.WForm
{
    public partial class Frm001BusinessTrip : FrmBase
    {
        #region Properties
        private readonly Inf_001BusinessTrip inf = new Inf_001BusinessTrip();
        private XtraUC001BusinessTripAE xtraUCMainAE;
        private XtraUC001BusinessTrip xtraUCMain;
        private bool AllowRefreshMethod;
        public static bool RoleInsert;
        public static bool RoleUpdate;
        public static bool RoleDelete;
        public static bool RoleView;
        public static DataTable DtAFunction;
        public static DataTable DtSFunction;
        private GridView gvMain;
        private WaitDialogForm dlg;
        public static bool ClearError;
        #endregion Properties

        #region Constructor
        public Frm001BusinessTrip()
        {
            InitializeComponent();
            AllowRefreshMethod = true;
            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            RoleInsert = PerIns;
            RoleUpdate = PerUpd;
            RoleDelete = PerDel;
            DtAFunction = DtPerFunction;
            DtSFunction = DtSpecialFunction;
            xtraUCMain = new XtraUC001BusinessTrip()
            {
                Dock = DockStyle.Fill,
                Name = "xtrraUCM",
            };
            panelControl.Controls.Add(xtraUCMain);
            ChangeMenuBarButtonCloseAE(); // Defualt menubar set up
            gvMain = xtraUCMain.gvBusinessTripC;
            gvMain.DoubleClick += gvMain_DoubleClick;

        }

        #endregion Constructor

        #region "Override button menubar click"
        protected override void PerformPrint()
        {
            if (xtraUCMainAE.Visible)
            {
                xtraUCMainAE.Print();
            }
        }
        protected override void PerformDelete()
        {
            Int64 pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
            gvMain.DeleteRow((int)pk);
            inf.Excute($"delete from BusinessTrip where PK ={pk}");
            PerformRefresh();
        }
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
                Int64 pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
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
            ClearError = true;
            ChangeMenuBarButtonCloseAE();
            RemoveUserControlMainAE(panelControl, "xtraUCMainAE");
            xtraUCMain.Visible = true;
        }
        protected override void PerformRefresh()
        {
            if (!AllowRefreshMethod)
            {
                dlg = new WaitDialogForm("");
                if (!xtraUCMain.Visible)
                {
                    xtraUCMainAE.Refresh();
                }
                else
                {
                    xtraUCMain.LoadData();
                }
                dlg.Close();
            }
            AllowRefreshMethod = false;
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

        #endregion "Override button menubar click"

        #region GridView
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            xtraUCMain.Visible = false;
            Int64 pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
            AddUserControlMainAE(pk,"Edit");
            ChangeMenuBarButtonOpenAE();
        }

        #endregion GridView

        #region Set up menubar
        private void ChangeMenuBarButtonCloseAE()
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
            AllowAdd = true;
            AllowEdit = true;
            AllowSave = false;
            AllowDelete = true;
            AllowFind = false;
            AllowRefresh = true;
            EnableAdd = RoleInsert;
            EnableEdit = true;
            EnableDelete = RoleDelete;
        }
        public void ChangeMenuBarButtonOpenAE()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowFind = false;
            AllowSave = true;
            EnableSave = true;
            AllowPrint = true;
            AllowCopyObject = false;
            AllowRefresh = false;
        }
        #endregion Set up menubar

        #region Add/Remove User Control
        private void AddUserControlMainAE(Int64 ID, string otp)
        {
            dlg = new WaitDialogForm("");
            xtraUCMain.Visible = false;
            xtraUCMainAE = new XtraUC001BusinessTripAE(ID, otp)
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCMainAE",
            };
            panelControl.Controls.Add(xtraUCMainAE);
            dlg.Close();
        }
        private void RemoveUserControlMainAE(Control root, string target)
        {
            xtraUCMainAE = ProcessGeneral.FindControl(root, target) as XtraUC001BusinessTripAE;
            if (xtraUCMainAE != null)
            {
                root.Controls.Remove(xtraUCMainAE);
            }
        }
        #endregion

    }
}