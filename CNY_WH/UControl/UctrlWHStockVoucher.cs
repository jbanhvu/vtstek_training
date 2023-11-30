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
using CNY_WH.Common;
using CNY_WH.Info;
using CNY_WH.Properties;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_WH.UControl
{
    public partial class UctrlWHStockVoucher : UserControl
    {
        #region Decl.Const

        private const string SearchField = "Voucher No";
        private const string Spath = "Path";
        private const string Spk = "PK";
        
        #endregion

        #region Declaration

        private string _sType;
        readonly InfoWh00001 _inf01 = new InfoWh00001();
        WaitDialogForm _dlg;

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
        DataTable _dt;
        public TextEdit TxtSearchp => this.txtSearch;
        public GridView GvMainP => this.gvMain;

        public DropDownButton DdbImportiScala => this.ddbImportiScala;

        #endregion

        #region Ini_Form

        public UctrlWHStockVoucher(string sType)
        {
            _sType = sType;
            InitializeComponent();
            this.Load += UctrlWh001Load;
            
            txtSearch.KeyDown += TxtSearch_KeyDown;
            txtSearch.MouseMove += TxtSearch_MouseMove;
            btnExportExcel.Click += BtnExportExcel_Click;
            btnSearch.Click += BtnSearch_Click;

            ProcessGeneral.SetTooltipControl(txtSearch, "Search By Stock Document No.", "Quick Search",
                ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);
            _dt = GetData(DeclareSystem.SysUserName);
            GridViewCustomInit();
        }

        private void UctrlWh001Load(object sender, EventArgs e)
        {
            //
        }
        private void GridViewCustomInit()
        {
            Cls001GenFunctions.InitGridSelectCell(gcMain, gvMain, false);
            Cls001GenFunctions.GridViewCustomCols(gvMain, _dt);
            Cls001GenFunctions.ShowGridViewIndicator(gvMain);
            gvMain.OptionsView.ShowGroupPanel = true;
            gvMain.OptionsView.ShowAutoFilterRow = true;
           
            gvMain.RowStyle += GvMain_RowStyle;
            gvMain.CustomDrawCell += GvMain_CustomDrawCell;

            gcMain.DataSource = _dt;
            gvMain.BestFitColumns();
            gcMain.ForceInitialize();
            ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, Spath, Spk);
        }
        private DataTable GetData(string sUser)
        {
            return _inf01.LoadGvStockDocumentList00101(sUser, _sType, this._strFilter);
        }

        #endregion

        #region Fn_Search & Export Excel/Files

        public void UpdateDataForGridView(string sUser, bool isShowDialog = true)
        {
            if (isShowDialog)
            {
                _dlg = new WaitDialogForm("");
            }
            _dt = GetData(sUser);
            gcMain.DataSource = _dt;
            gvMain.BestFitColumns();

            if (isShowDialog)
            {
                _dlg.Close();
            }
        }

        private void SearchResult()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                this._strFilter = string.Format(@" UPPER({0}) LIKE '%{1}%' ",
                    SearchField, txtSearch.Text.Trim().ToUpper());
            }
            else
            {
                this._strFilter = string.Empty;
                XtraMessageBox.Show("Invalid Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
                txtSearch.Select();
            }
            UpdateDataForGridView("");
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
        
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ClsWhExportFiles.ExportGridViewtoExcel(gvMain);
            Cursor.Current = Cursors.Default;
        }
        
        #endregion

        #region Format Grid

        private void GvMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = Resources.chartsshowlegend_24x24;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }
        
        private void GvMain_RowStyle(object sender, RowStyleEventArgs e)
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
    }
}
