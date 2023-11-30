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
    public partial class Frm004MaterialRequirement : FrmBase
    {
        #region "Property"

        private XtraUC004MaterialRequirement xtraUCMain;
        private XtraUC004MaterialRequirementAE xtraUCMainAE;
        private GridView gvMain;
        public static bool ClearError;
        private readonly Inf_004MaterialRequirement inf = new Inf_004MaterialRequirement();
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

        public Frm004MaterialRequirement()
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
            xtraUCMain = new XtraUC004MaterialRequirement()
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

        /// <summary>
        ///     perform Edit Button Click
        /// </summary>
        /// <param name="sender" type="object">
        ///     <para>
        ///
        ///     </para>
        /// </param>
        /// <param name="e" type="System.EventArgs">
        ///     <para>
        ///
        ///     </para>
        /// </param>
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            PerformEdit();
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
            AllowSave = RoleUpdate;
            EnableSave = true;
            AllowPrint = true;
            AllowCopyObject = false;
        }

        #endregion "ChangeMenuBarButtonOpenAE

        #region Add/Remove User Control

        private void AddUserControlMainAE(int ID, string Otp)
        {
            dlg = new WaitDialogForm("");
            xtraUCMain.Visible = false;
            xtraUCMainAE = new XtraUC004MaterialRequirementAE(ID, Otp)
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCMainAE",
            };
            panelControlAdd.Controls.Add(xtraUCMainAE);
            dlg.Close();
        }

        private void RemoveUserControlMainAE(Control root, string target)
        {
            xtraUCMainAE = ProcessGeneral.FindControl(root, target) as XtraUC004MaterialRequirementAE;
            if (xtraUCMainAE != null)
            {
                root.Controls.Remove(xtraUCMainAE);
            }
        }

        #endregion Add/Remove User Control
    }
}