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
using CNY_BaseSys.WForm;
using CNY_WH.UControl;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CNY_WH.WForm
{
    public partial class FrmWHStockItems : FrmBase
    {
        #region Declaration

        private UctrlWHStockItems _uctrlStockItems;
        private GridView _gvStkItems;
        private GridControl _gcStkItems;

        private const string CsStockCode = "Stock_Code";

        #endregion

        #region Init Form

        public FrmWHStockItems()
        {
            InitializeComponent();
            this.Load += FrmWHStockItems_Load;
        }

        private void FrmWHStockItems_Load(object sender, EventArgs e)
        {
            HideMenuBonForm();
            SetCaptionGenerate = "Stock Card";

            string sfrName = Cls001MasterFiles.GetFormNameFromMenuCode(this.MenuCode);
            this.Text = sfrName;

            _uctrlStockItems = new UctrlWHStockItems()
            {
                Dock = DockStyle.Fill,
                Name = "UctrlStockItems"
            };

            panStockItems.Controls.Add(_uctrlStockItems);
            _gvStkItems = _uctrlStockItems.GvStkItem;
            _gcStkItems = _uctrlStockItems.GcStkItem;
            _gcStkItems.ProcessGridKey += _gcStkItems_ProcessGridKey;
            _gvStkItems.DoubleClick += _gvStkItems_DoubleClick;
        }
        
        private void HideMenuBonForm()
        {
            AllowSave = false;
            AllowCancel = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowPrint = false;
            AllowRevision = false;
        }

        #endregion

        #region Grid_Click

        private void _gcStkItems_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.PerformGenerate();
            }
        }
        
        private void _gvStkItems_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            if (gv.FocusedColumn == null) return;
            if (gv.RowCount <= 0) return;
            GridControl gc = gv.GridControl;


            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(MousePosition));
            if (!hi.InRowCell) return;


            this.PerformGenerate();
        }

        #endregion

        #region Fn_Refresh & Find

        protected override void PerformRefresh()
        {
            Cursor.Current = Cursors.WaitCursor;
            _uctrlStockItems.StrFilter = "";
            _uctrlStockItems.UpdateDataForGridView();
            Cursor.Current = Cursors.Default;
        }

        private DataTable GetTableFieldSearch()
        {
            DataTable dtFiled = new DataTable();
            dtFiled.Columns.Add("FieldValue", typeof(string));
            dtFiled.Columns.Add("FieldDisplay", typeof(string));
            dtFiled.Columns.Add("FieldType", typeof(string));

            dtFiled.Rows.Add("T01.RMCode_001", "Stock_Code", "string");
            dtFiled.Rows.Add("T01.RMDescription_002", "Description_1", "string");
            dtFiled.Rows.Add("T01.RMLongDescription_031", "Description_2", "string");
            dtFiled.Rows.Add("C100.CNY001", "Stock_Balance", "number");
            dtFiled.Rows.Add("C05.CNY002", "Stock_Unit", "string");
            dtFiled.Rows.Add("T01.PurchaseFactor_019", "Factor", "number");
            dtFiled.Rows.Add("C01.CNY002", "Catagory", "string");
          
            return dtFiled;
        }

        private void frm_searchEvent(object sender, SearchEventArgs e)
        {
            _uctrlStockItems.StrFilter = e.filterexpression.Replace(" Where ", " ");
            _uctrlStockItems.UpdateDataForGridView();
        }

        protected override void PerformFind()
        {
            DataTable dtFiled = GetTableFieldSearch();

            FrmSearch frm = new FrmSearch(dtFiled);
            frm.TitileForm = "Search Form";
            frm.searchEvent += frm_searchEvent;
            frm.ShowDialog();
        }

        #endregion

        #region Fn_Stock Card

        protected override void PerformGenerate()
        {
            string sCode = ProcessGeneral.GetSafeString(_gvStkItems.GetRowCellValue(_gvStkItems.FocusedRowHandle, CsStockCode));
            var frmStkCard = new FrmWHStockCard(sCode);
            try
            {
                if (!ProcessGeneral.CheckReportFormOpened(frmStkCard))
                {
                    frmStkCard.MdiParent = this.MdiParent;
                    frmStkCard.WindowState = FormWindowState.Normal;
                    frmStkCard.StartPosition = FormStartPosition.CenterScreen;
                    frmStkCard.SetDefaultCommandAndPermission(this);
                    frmStkCard.CsFormActiveName = this.Name;
                    frmStkCard.Show();
                    this.Enabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Error on FStockCard", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
