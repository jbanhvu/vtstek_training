using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_WH.Info;
using CNY_WH.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_WH.UControl
{
    public partial class UctrlWHStockItems : UserControl
    {
        #region Declaration

        private readonly InfoWhReports _infStkItem = new InfoWhReports();

        private DataTable _dtSrcItems;
        private bool _bload = false;
        private string _strFilter = "";
        public string StrFilter
        {
            get
            {
                return _strFilter;
            }
            set
            {
                _strFilter = value;
            }
        }
        public TextEdit TxtSearchp => this.txtSearch;
        
        public GridView GvStkItem => this.gvStkItems;

        public GridControl GcStkItem => this.gcStkItems;

        private const string Spk = "PK";
        private const string SearchField01 = "T01.RMCode_001";
        private const string SearchField02 = "T01.RMDescription_002";

        private const string CsStockCode = "Stock_Code";
        private const string CsStockBalance = "Stock_Balance";
        private const string CsFactor = "Factor";
        private const string CsQtyPurchase = "Qty_Purchase";

        #endregion

        #region Init Form Stock Card

        public UctrlWHStockItems()
        {
            InitializeComponent();
            ProcessGeneral.SetTooltipControl(txtSearch, "Search Description.", "Quick Search",
                ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);

            _dtSrcItems = GetStockItemList();
            GridViewCustomInit();

            txtSearch.KeyDown += TxtSearch_KeyDown;
            txtSearch.MouseMove += TxtSearch_MouseMove;
            btnSearch.Click += BtnSearch_Click;
            btnExportExcel.Click += BtnExportExcel_Click;
        }
        
        private void GridViewCustomInit()
        {
            Cls001GenFunctions.InitGridSelectCell(gcStkItems, gvStkItems, false);
            Cls001GenFunctions.GridViewCustomCols(gvStkItems, _dtSrcItems);
            Cls001GenFunctions.ShowGridViewIndicator(gvStkItems);

            gvStkItems.RowStyle += GvStkItems_RowStyle;
            gvStkItems.CustomDrawCell += GvStkItems_CustomDrawCell;

            GcStkItem.DataSource = _dtSrcItems;
            GvStkItem.BestFitColumns();
            FormatGridColumns();
        }

        private DataTable GetStockItemList()
        {
            return _infStkItem.LoadStockItemsBalance_App_00001(this._strFilter);
        }

        #endregion

        #region Format Grid

        private void FormatGridColumns()
        {
            gvStkItems.Columns[CsStockBalance].DisplayFormat.FormatType = FormatType.Numeric;
            gvStkItems.Columns[CsStockBalance].DisplayFormat.FormatString = "N3";
            gvStkItems.Columns[CsFactor].DisplayFormat.FormatType = FormatType.Numeric;
            gvStkItems.Columns[CsFactor].DisplayFormat.FormatString = "N3";
            gvStkItems.Columns[CsQtyPurchase].DisplayFormat.FormatType = FormatType.Numeric;
            gvStkItems.Columns[CsQtyPurchase].DisplayFormat.FormatString = "N3";

            gvStkItems.Columns[CsStockCode].Width = 120;
            ProcessGeneral.HideVisibleColumnsGridView(gvStkItems, false, Spk);
        }

        private void GvStkItems_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = Resources.chartsshowlegend_24x24;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void GvStkItems_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsRowSelected(e.RowHandle)) return;

            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
            e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }

        #endregion

        #region Fn_Search & Export Excel/Files

        public void UpdateDataForGridView()
        {
            Cursor.Current = Cursors.WaitCursor;

            _dtSrcItems = GetStockItemList();
            gcStkItems.DataSource = _dtSrcItems;
            gvStkItems.BestFitColumns();

            FormatGridColumns();
            Cursor.Current = Cursors.Default;
        }

        private void SearchResult()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                this._strFilter = string.Format(@" ((UPPER({0}) LIKE '%{2}%') OR (UPPER({1}) LIKE '%{2}%')) ",
                    SearchField01, SearchField02, txtSearch.Text.Trim().ToUpper());
            }
            else
            {
                this._strFilter = string.Empty;
                XtraMessageBox.Show("Invalid Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
                txtSearch.Select();
            }

            UpdateDataForGridView();
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Cls001GenFunctions.ExportGridViewtoExcel(gvStkItems);
            Cursor.Current = Cursors.Default;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        private void TxtSearch_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(txtSearch),
                ToolTipController.DefaultController.GetTitle(txtSearch), txtSearch.PointToScreen(e.Location));
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchResult();
            }
        }

        #endregion
    }
}
