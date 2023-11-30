using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Info;
using CNY_Buyer.UControl;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing.Drawing2D;

namespace CNY_Buyer.WForm
{
    public partial class Frm001PurchaseRequisitionReport : FrmBase
    {
        #region "Property"

        private XtraUC001PurchaseRequisition xtraUCMain;
        private XtraUC001PurchaseRequisitionAE xtraUCMainAE;
        Inf_002PurchaseRequisitionDetail _inf002 = new Inf_002PurchaseRequisitionDetail();
        private GridView gvMain;
        public static bool ClearError;
        private readonly Inf_001PurchaseRequisition inf = new Inf_001PurchaseRequisition();
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

        public Frm001PurchaseRequisitionReport()
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
            ChangeMenuBarButton();
            FormatGridView.Init2(gcReport, gvReport);
            InitColumGridview();
            LoadData();
            
        }

        public void ChangeMenuBarButton()
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
            AllowEdit = false;
            AllowSave = false;
            AllowDelete = false;
            AllowFind = false;
            AllowRefresh = false;
            SetCaptionAdd = "Export";
            SetImageAdd = Properties.Resources.export_32x32;

        }

        #endregion "Contructor"

        #region Load Data
        public void LoadData()
        {
            DataTable dt = _inf002.sp_PurchaseRequisitionDetail_SelectReport();
            gcReport.DataSource = dt;
            gvReport.BestFitColumns();
        }
        #endregion

        #region "Override button menubar click"

        protected override void PerformAdd()
        {
            ExportToExcel(gcReport);
        }
        #endregion "Override button menubar click"

        #region Excel
        public static void ExportToExcel(GridControl gridControl)
        {
            GridView gridView = gridControl.DefaultView as GridView;

            if (gridView != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    Title = "Export to Excel",
                    FileName = "exported_data.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx
                        {
                            ExportType = DevExpress.Export.ExportType.WYSIWYG
                        };

                        gridView.ExportToXlsx(fs, options);
                    }
                }
            }
        }
        #endregion

        #region InitColumGridview
        private void InitColumGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvReport, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvReport, "PurchaseRequisitionPK", "PurchaseRequisitionPK", -1);
            FormatGridView.CreateColumnOnGridview(gvReport, "StockPK", "StockPK", -1);
            FormatGridView.CreateColumnOnGridview(gvReport, "Mã dự án", "ProjectCode", 1);
            FormatGridView.CreateColumnOnGridview(gvReport, "Người ĐNLVT", "Requester", 2);
            FormatGridView.CreateColumnOnGridview(gvReport, "Người MH", "Supplier", 2);
            FormatGridView.CreateColumnOnGridview(gvReport, "Số phiếu ĐNMH", "RequestNumber", 2);
            FormatGridView.CreateColumnOnGridview(gvReport, "Số phiếu nhập kho", "ReceiptNumber", 3);
            FormatGridView.CreateColumnOnGridview(gvReport, "Số hóa đơn", "PONumber", 4);
            FormatGridView.CreateColumnOnGridview(gvReport, "STT", "OrdinalNumber", 4);
            FormatGridView.CreateColumnOnGridview(gvReport, "Mã vật tư (F4)", "StockCode", 4);
            FormatGridView.CreateColumnOnGridview(gvReport, "Tên vật tư (F4)", "StockName", 4);
            FormatGridView.CreateColumnOnGridview(gvReport, "Số lượng", "Quantity", 4);
            FormatGridView.CreateColumnOnGridview(gvReport, "Giá", "Price",4);
            FormatGridView.CreateColumnOnGridview(gvReport, "Thuế", "Tax", 4);
            FormatGridView.CreateColumnOnGridview(gvReport, "Thành tiền", "Amount", 5);
            FormatGridView.CreateColumnOnGridview(gvReport, "Ngày yêu cầu", "RequestedDate", 5);
            FormatGridView.CreateColumnOnGridview(gvReport, "Ngày nhận đề nghị", "ReceiveRequestDate", 7);
            FormatGridView.CreateColumnOnGridview(gvReport, "Ngày yêu cầu gia hàng", "RequestDeliveryDate", 8);
            FormatGridView.CreateColumnOnGridview(gvReport, "Ngày giao hàng thực tế", "DeliveryDate", 9);
            FormatGridView.CreateColumnOnGridview(gvReport, "DeliveryStatusPK", "DeliveryStatus", -1);
            FormatGridView.CreateColumnOnGridview(gvReport, "Tình trạng giao hàng (F4)", "DeliveryStatusName", 10);
            FormatGridView.CreateColumnOnGridview(gvReport, "Ghi chú", "Note", 11);


            gvReport.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvReport.Columns["Price"].DisplayFormat.FormatString = "N0";
            gvReport.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvReport.Columns["Quantity"].DisplayFormat.FormatString = "N0";
            gvReport.Columns["Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvReport.Columns["Amount"].DisplayFormat.FormatString = "N0";
            gvReport.Columns["Tax"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvReport.Columns["Tax"].DisplayFormat.FormatString = "N0";
            gvReport.Columns["RequestedDate"].DisplayFormat.FormatType = FormatType.DateTime;
            gvReport.Columns["RequestedDate"].DisplayFormat.FormatString = "dd-MM-yyyy";


            InitFooterCell(gvReport, "Quantity");
            InitFooterCell(gvReport, "Amount");
        }

        void InitFooterCell(GridView gvMain, string ColumnName)
        {
            GridColumn column = gvMain.Columns[ColumnName];
            column.SummaryItem.SummaryType = SummaryItemType.Sum;
            column.SummaryItem.DisplayFormat = "{0:0,0}";
        }
        #endregion

        #region GridView Custom Init
        private void GridViewCustomInit()
        {
            FormatGridView.Init2(gcReport, gvReport);
            gvMain.CustomDrawCell += gvMain_CustomDrawCell;
            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
            gcReport.ForceInitialize();

        }
        #region "gridview event"

        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
        }
        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }
        private void gvMain_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            var gv = sender as GridView;

            if (e.Column.VisibleIndex == 0)
            {
                Image icon = Properties.Resources.folder_documents_icon;
                e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }
        }
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.FocusedRowHandle == e.RowHandle)
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }
        }
        #endregion
        #endregion
    }
}
