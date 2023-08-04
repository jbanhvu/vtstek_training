using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Info;
using CNY_WH.UControl;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
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

namespace CNY_WH.WForm
{
    public partial class Frm002ReceiptNote : FrmBase
    {
        #region "Property"

        private XtraUC002ReceiptNote xtraUCMain;
        private XtraUC002ReceiptNoteAE xtraUCMainAE;
        private GridView gvMain;
        public static bool ClearError;
        private readonly Inf_002ReceiptNote inf = new Inf_002ReceiptNote();
        private bool allowRefreshMethold;
        public static bool RoleInsert;
        public static bool RoleUpdate;
        public static bool RoleDelete;
        public static bool RoleView;
        public static DataTable DtAFunction;
        public static DataTable DtSFunction;
        private WaitDialogForm dlg;
        Int32 _pk;

        private string option = "";


        #endregion "Property"

        #region "Contructor"

        public Frm002ReceiptNote()
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
            xtraUCMain = new XtraUC002ReceiptNote()
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCM",
            };

            panelControlAdd.Controls.Add(xtraUCMain);
            ChangeMenuBarButtonCloseAE();
            gvMain = xtraUCMain.gvMainC;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvMain.FocusedRowChanged += GvMain_FocusedRowChanged;
            gvMain.KeyDown += gvMain_KeyDown;
        }



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

        #endregion "Contructor"

        #region "Use Button"

        private void txtStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.F4) return;

            var lG = new List<GridViewTransferDataColumnInit>
            {
            };

            var f = new FrmTransferData
            {
                DtSource = DtAFunction,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(350, 400),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Status",
                StrFilter = "",
                IsShowAutoFilterRow = true,
                IsShowFindPanel = false,
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                //txtStatus.Text = ProcessGeneral.GetStringPkDataTransferForm(lDr, "CodePer", ",", true);
            };
            f.ShowDialog();
        }

        public DataTable GetTblStatus()
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add(
            return dt;
        }

        #endregion "Use Button"

        #region "Gridview"

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            PerformEdit();
        }
        private void GvMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
        }

        /// <summary>
        ///    perform Edit Button Click If Key Code = EnterKey, Perform Delete Button Click If Key Code= DeleteKey
        /// </summary>
        /// <param name="sender" type="object">
        ///     <para>
        ///
        ///     </para>
        /// </param>
        /// <param name="e" type="System.Windows.Forms.KeyEventArgs">
        ///     <para>
        ///
        ///     </para>
        /// </param>
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
        #region MenuItemClick

        private void itemOpen_Click(object sender, EventArgs e)
        {
            PerformEdit();
        }
        private void itemNewProject_Click(object sender, EventArgs e)
        {
            PerformAdd();
        }
        private void itemNewSurvey_Click(object sender, EventArgs e)
        {

        }
        private void itemApprove_Click(object sender, EventArgs e)
        {

        }
        private void itemReject_Click(object sender, EventArgs e)
        {

        }

        #endregion
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
            AddUserControlMainAE(-1);
            ChangeMenuBarButtonOpenAE();
        }

        protected override void PerformEdit()
        {
            _pk = ProcessGeneral.GetSafeInt(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "PK"));
            if (!gvMain.IsDataRow(gvMain.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AddUserControlMainAE(_pk);
            ChangeMenuBarButtonOpenAE();
        }

        protected override void PerformSave()
        {
            //xtraUCMainAE.Save();
            PerformRefresh();
            xtraUCMain.LoadData();
        }

        protected override void PerformCancel()
        {
            option = "";
            ClearError = true;
            ChangeMenuBarButtonCloseAE();
            RemoveUserControlMainAE(panelControlAdd, "xtraUCMainAE");
            xtraUCMain.Visible = true;
        }

        protected override void PerformDelete()
        {

        }

        protected override void PerformPrint()
        {

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
                        //xtraUCMainAE.ClearForm();
                        //xtraUCMainAE.SetDefaultInfo();
                    }
                    else
                    {
                        //xtraUCMainAE.DisplayDataForEditing();
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

        #endregion "Override button menubar click"

        #region "Methold search"

        /// <summary>
        ///     Create Template Table Search Into Search Form
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable GetTableFieldSearch()
        {
            var dtFiled = new DataTable();//number string datetime bool
            dtFiled.Columns.Add("FieldValue", typeof(string));
            dtFiled.Columns.Add("FieldDisplay", typeof(string));
            dtFiled.Columns.Add("FieldType", typeof(string));
            //  dtFiled.Rows.Add("","","");
            dtFiled.Rows.Add("AA94039", "Upload", "string");
            dtFiled.Rows.Add("AA94013", "Global ID", "string");
            dtFiled.Rows.Add("AA94001", "Customer ID", "string");
            dtFiled.Rows.Add("AA94002", "Customer's Name", "string");
            dtFiled.Rows.Add("AA94005", "Address", "string");
            dtFiled.Rows.Add("SY24003", "Country", "string");
            dtFiled.Rows.Add("AA94014", "Phone Number", "string");
            dtFiled.Rows.Add("AA94016", "Fax", "string");
            dtFiled.Rows.Add("AA94010", "Reference", "string");//3 4 5
            dtFiled.Rows.Add("AA94060", "Status", "number");
            dtFiled.Rows.Add("AA94PK", "PK", "number");
            return dtFiled;
        }

        #endregion "Methold search"

        #region "ChangeMenuBarButtonOpenAE

        public void ChangeMenuBarButtonOpenAE()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowFind = false;
            AllowSave = true;
            EnableSave = true;
            AllowPrint = false;
        }

        #endregion "ChangeMenuBarButtonOpenAE

        #region Add/Remove User Control

        private void AddUserControlMainAE(int ID)
        {
            dlg = new WaitDialogForm("");
            xtraUCMain.Visible = false;
            xtraUCMainAE = new XtraUC002ReceiptNoteAE(ID)
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCMainAE",
            };
            panelControlAdd.Controls.Add(xtraUCMainAE);

            dlg.Close();
        }

        private void RemoveUserControlMainAE(Control root, string target)
        {
            xtraUCMainAE = ProcessGeneral.FindControl(root, target) as XtraUC002ReceiptNoteAE;
            if (xtraUCMainAE != null)
            {
                root.Controls.Remove(xtraUCMainAE);
            }
        }

        #endregion Add/Remove User Control
    }
}
