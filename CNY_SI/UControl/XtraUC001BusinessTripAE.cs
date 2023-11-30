using CNY_BaseSys.Common;
using CNY_SI.Class;
using CNY_SI.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Frames;
using DevExpress.Utils.About;
using CNY_BaseSys.WForm;
using DevExpress.XtraGrid.Views.Base;
using CNY_BaseSys;
using DevExpress.XtraDashboardLayout;
using DevExpress.XtraEditors.Repository;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraExport.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CNY_Report;
using DevExpress.XtraReports.UI;
using CNY_SI.Report;

namespace CNY_SI.UControl
{
    public partial class XtraUC001BusinessTripAE : UserControl
    {
        #region Properties
        private readonly Inf_001BusinessTrip _inf = new Inf_001BusinessTrip();
        private readonly Cls_001BusinessTrip _cls = new Cls_001BusinessTrip();

        private readonly Inf_001BusinessTripDetail _infDetail = new Inf_001BusinessTripDetail();
        Int64 _pk;
        string _otp;

        #endregion Properties

        #region Constructor
        public XtraUC001BusinessTripAE(Int64 pk, string otp)
        {
            InitializeComponent();
            _pk = pk;
            _otp = otp;
            LoadDataGridView(pk);
            LoadDataTextBox();
        }



        public void gcMain_Click(object sender, EventArgs e)
        {

        }
        #endregion Constructor

        #region LoadDataTextEdit
        public void LoadDataTextBox()
        {
            if (_otp == "Add")
            {
                DisplayDataForAdding();
            }
            if (_otp == "Edit")
            {
                DisplayDataForEdditing();
            }  
        }

        private void DisplayDataForEdditing()
        {
            DataTable dt = _inf.sp_BusinessTrip_Select(_pk);
            txtBusinessTripPK.EditValue = ProcessGeneral.GetSafeInt64(dt.Rows[0]["PK"]);
            txtRequestUser.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["RequestUser"]);
            txtContent.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Content"]);
            txtConclusion.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Conclusion"]);
            txtCost.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["Cost"]);
            txtNote.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Note"]);
            txtStartAt.EditValue = ProcessGeneral.GetSafeDatetime(dt.Rows[0]["StartAt"]);
            txtEndAt.EditValue = ProcessGeneral.GetSafeDatetime(dt.Rows[0]["EndAt"]);
            txtStatus.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Status"]);
            txtUpdatedBy.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["UpdatedBy"]);
            txtUpdatedDate.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["UpdatedDate"]);

            txtCreatedBy.Enabled = false;
            txtUpdatedBy.Enabled = false;
            txtCreatedDate.Enabled = false;
            txtUpdatedDate.Enabled = false;
            txtBusinessTripPK.Enabled = false;

        }

        public void DisplayDataForAdding()
        {
            txtBusinessTripPK.EditValue = string.Empty;
            txtBusinessTripPK.Enabled = false;
            txtCreatedBy.Enabled = false;
            txtUpdatedBy.Enabled = false;
            txtCreatedDate.Enabled = false;
            txtUpdatedDate.Enabled = false;
            txtBusinessTripPK.Enabled = false;
        }

        #endregion LoadDataTextEdit

        #region LoadDataGridView
        public void LoadDataGridView(Int64 pk)
        {
            Declare_GridView();
            InitColumnGridview();
            gcChiPhi.DataSource = _infDetail.sp_BusinessTripDetail_Select(pk);
            gvChiPhi.BestFitColumns();

        }

        #region Declare Gridview 
        private void Declare_GridView()
        {
            // gcMain.ToolTipController = toolTipController1  ;

            gvChiPhi.OptionsCustomization.AllowColumnResizing = true;
            gvChiPhi.OptionsCustomization.AllowGroup = true;
            gvChiPhi.OptionsCustomization.AllowColumnMoving = true;
            gvChiPhi.OptionsCustomization.AllowQuickHideColumns = true;
            gvChiPhi .OptionsCustomization.AllowSort = true;
            gvChiPhi.OptionsCustomization.AllowFilter = true;

            gcChiPhi.UseEmbeddedNavigator = true;

            gcChiPhi.EmbeddedNavigator.Buttons.CancelEdit.Visible =false;
            gcChiPhi.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcChiPhi.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcChiPhi.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcChiPhi.EmbeddedNavigator.Buttons.Remove.Visible = false;



            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvChiPhi.OptionsBehavior.Editable = true;
            gvChiPhi.OptionsBehavior.AllowAddRows = DefaultBoolean.True;

            //     gvMain.OptionsHint.ShowCellHints = true;
            gvChiPhi.OptionsView.BestFitMaxRowCount = 1000;
            gvChiPhi.OptionsView.ShowGroupPanel = true;
            gvChiPhi.OptionsView.ShowIndicator = true;
            gvChiPhi.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvChiPhi.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvChiPhi.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvChiPhi.OptionsView.ShowAutoFilterRow = true;
            gvChiPhi.OptionsView.AllowCellMerge = false;
            gvChiPhi.HorzScrollVisibility = ScrollVisibility.Auto;
            gvChiPhi.OptionsView.ColumnAutoWidth = false;//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvChiPhi.OptionsNavigation.AutoFocusNewRow = true;
            gvChiPhi.OptionsNavigation.UseTabKey = true;

            gvChiPhi.OptionsSelection.MultiSelect = false;
            gvChiPhi.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvChiPhi.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvChiPhi.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvChiPhi.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvChiPhi.OptionsView.EnableAppearanceEvenRow = false;
            gvChiPhi.OptionsView.EnableAppearanceOddRow = false;

            gvChiPhi.OptionsView.ShowFooter = false;

            gvChiPhi.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvChiPhi.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvChiPhi.OptionsFind.AlwaysVisible = false;
            gvChiPhi.OptionsFind.ShowCloseButton = true;
            gvChiPhi.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvChiPhi)
            {
                //AllowGroupBy = true,
                IsPerFormEvent = true,
            };
            gvChiPhi.OptionsView.ShowGroupedColumns = true;
            gvChiPhi.OptionsPrint.AutoWidth = true;
            gvChiPhi.OptionsPrint.ShowPrintExportProgress = true;
            gvChiPhi.OptionsPrint.AllowMultilineHeaders = true;
            gvChiPhi.OptionsPrint.ExpandAllDetails = true;
            gvChiPhi.OptionsPrint.ExpandAllGroups = true;
            gvChiPhi.OptionsPrint.PrintDetails = true;
            gvChiPhi.OptionsPrint.PrintFooter = true;
            gvChiPhi.OptionsPrint.PrintGroupFooter = true;
            gvChiPhi.OptionsPrint.PrintHeader = true;
            gvChiPhi.OptionsPrint.PrintHorzLines = true;
            gvChiPhi.OptionsPrint.PrintVertLines = true;
            gvChiPhi.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvChiPhi.OptionsPrint.SplitDataCellAcrossPages = true;
            gvChiPhi.OptionsPrint.UsePrintStyles = false;
            gvChiPhi.OptionsPrint.AllowCancelPrintExport = true;
            gvChiPhi.OptionsPrint.AutoResetPrintDocument = true;
            gvChiPhi.CellValueChanged += GvMain_CellValueChanged;

            gcChiPhi.ForceInitialize();
        }

        private void GvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv=sender as GridView;
            int row = gv.FocusedRowHandle;

            if (e.Column.Name== "Price"|| e.Column.Name == "Quantity" || e.Column.Name == "Tax")
            {
                Decimal price =ProcessGeneral.GetSafeDecimal( gv.GetRowCellValue(row, "Price"));
                int quantity= ProcessGeneral.GetSafeInt(gv.GetRowCellValue(row, "Quantity"));
                int tax = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(row, "Tax"));
                Decimal total= price*quantity*(1+((decimal)tax/100));
                gv.SetRowCellValue(row, "Total", total);
            }
        }

        public void AddSearchLookUpToGridView()
        {
            // Tạo RepositoryItemSearchLookUpEdit
            RepositoryItemSearchLookUpEdit Item = new RepositoryItemSearchLookUpEdit();
            RepositoryItemSearchLookUpEdit Unit = new RepositoryItemSearchLookUpEdit();
            Item.DataSource = _inf.Excute("select PK, Name from BusinessTripDetailItem");
            Item.ValueMember = "PK"; // Trường chứa giá trị
            Item.DisplayMember = "Name"; // Trường hiển thị

            Unit.DataSource = _inf.Excute("select PK, Name from BusinessTripDetailItemUnit");
            Unit.ValueMember = "PK";
            Unit.DisplayMember = "Name";


            // Thiết lập SearchLookUpEdit cho cột ProductName
            gvChiPhi.Columns["BusinessTripItemPK"].ColumnEdit = Item;
            gvChiPhi.Columns["Unit"].ColumnEdit = Unit;
        }

        #region Initilize Column GridView 
        private void InitColumnGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "BusinessTripPK", "BusinessTripPK", -1);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "Loại công tác", "BusinessTripItemPK", 1);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "Đơn giá", "Price", 2);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "Đơn vị", "Unit", 3);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "Số Lượng", "Quantity", 4);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "Thuế", "Tax", 5);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "Thành tiền", "Total", 6);
            FormatGridView.CreateColumnOnGridview(gvChiPhi, HorzAlignment.Default, "Ghi chú", "Note", 7);

            gvChiPhi.Columns["Total"].OptionsColumn.AllowEdit = false;
            gvChiPhi.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvChiPhi.Columns["Price"].DisplayFormat.FormatString = "{0:#,#}";
            gvChiPhi.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvChiPhi.Columns["Quantity"].DisplayFormat.FormatString = "{0:#,#}";
            gvChiPhi.Columns["Total"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvChiPhi.Columns["Total"].DisplayFormat.FormatString = "{0:#,#}";
            gvChiPhi.Columns["Tax"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvChiPhi.Columns["Tax"].DisplayFormat.FormatString = "{#:%}";
            AddSearchLookUpToGridView();
        }
        #endregion
        #endregion
        #endregion LoadDataGridView

        #region Save
        public void Save()
        {
            #region Check input

            #endregion

            #region Get Info
            _cls.PK = _pk;
            _cls.RequestUser = ProcessGeneral.GetSafeString(txtRequestUser.EditValue);
            _cls.Content = ProcessGeneral.GetSafeString(txtContent.EditValue);
            _cls.StartAt = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtStartAt.EditValue));
            _cls.EndAt = Convert.ToDateTime(ProcessGeneral.GetSafeDatetime(txtEndAt.EditValue));
            _cls.Cost = ProcessGeneral.GetSafeInt(txtCost.EditValue);
            _cls.Status = ProcessGeneral.GetSafeString(txtStatus.EditValue);
            _cls.Conclusion = ProcessGeneral.GetSafeString(txtConclusion.EditValue);
            _cls.CreatedBy = DeclareSystem.SysUserName;
            _cls.CreatedDate = DateTime.Now;
            _cls.UpdatedBy = DeclareSystem.SysUserName;
            _cls.UpdatedDate = DateTime.Now;
            _cls.Note = ProcessGeneral.GetSafeString(txtNote.EditValue);
            #endregion

            #region Save and show message
            //Update data
            DataTable dtSaveResult = _inf.sp_BusinessTrip_Update(_cls);

            //Get result info
            string msg = ProcessGeneral.GetSafeString(dtSaveResult.Rows[0]["ErrMsg"]);

            ////Set new ID
            //if (_cls.PK == -1)
            //{
            //    //Update new ID
            //    _cls.PK = ProcessGeneral.GetSafeInt(dtSaveResult.Rows[0]["IDENTITY"]);
            //    _pk = _cls.PK;
            //}

            //Get result info

            //XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SaveItem();
            #endregion
        }

        public void SaveItem()
        {
            DataTable dtDetail = new DataTable();
            dtDetail.Columns.Add("PK", typeof(Int64));
            dtDetail.Columns.Add("BusinessTripPK", typeof(Int64));
            dtDetail.Columns.Add("BusinessTripItemPK", typeof(Int64));
            dtDetail.Columns.Add("Price", typeof(Decimal));
            dtDetail.Columns.Add("Unit", typeof(int));
            dtDetail.Columns.Add("Quantity", typeof(int));
            dtDetail.Columns.Add("Tax", typeof(int));
            dtDetail.Columns.Add("Note", typeof(String));


            DataTable dtSource = gcChiPhi.DataSource as DataTable;

            for (int i = 0; i < gvChiPhi.RowCount; i++)
            {
                dtDetail.Rows.Add();
                DataRow dr = dtDetail.Rows[i];

                dr["PK"] = ProcessGeneral.GetSafeInt64(dtSource.Rows[i]["PK"]);
                dr["BusinessTripPK"] = _pk;
                dr["BusinessTripItemPK"] = ProcessGeneral.GetSafeInt64(dtSource.Rows[i]["BusinessTripItemPK"]);
                dr["Price"] = ProcessGeneral.GetSafeDecimal(dtSource.Rows[i]["Price"]);
                dr["Unit"] = ProcessGeneral.GetSafeInt(dtSource.Rows[i]["Unit"]);
                dr["Quantity"] = ProcessGeneral.GetSafeInt(dtSource.Rows[i]["Quantity"]);
                dr["Tax"] = ProcessGeneral.GetSafeInt(dtSource.Rows[i]["Tax"]);
                dr["Note"] = ProcessGeneral.GetSafeString(dtSource.Rows[i]["Note"]);

                //dtDetail.Rows.Add();
                //dtDetail.Rows.Add(dr);
            }

            //Update data for details

            DataTable dtSaveResult = _infDetail.sp_BusinessTripDetail_Update(_pk, dtDetail);
            //Get result info
            string msg = ProcessGeneral.GetSafeString(dtSaveResult.Rows[0]["ErrMsg"]);

            //Save item

            XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Save

        #region events
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gvChiPhi.AddNewRow();
            DataRow dr = gvChiPhi.GetDataRow(gvChiPhi.FocusedRowHandle);
            dr["PK"] = -1;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        #endregion events
        #region Print
        public void Print()
        {
            DataTable dt = _inf.sp_BusinessTrip_Select(_pk);
            DataTable dtDetail = _infDetail.sp_BusinessTripDetail_SelectReport(_pk);
            ReportPrintTool printTool = new ReportPrintTool(new Rpt001BusinessTrip(dt,dtDetail));

            // Xuất báo cáo
            printTool.ShowPreview();
        }
        #endregion Print
    }
}
