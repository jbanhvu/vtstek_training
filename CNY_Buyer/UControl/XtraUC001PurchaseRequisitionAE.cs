using CNY_BaseSys;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_BaseSys.Info;
using CNY_BaseSys.WForm;
using CNY_Buyer.Class;
using CNY_Buyer.Common;
using CNY_Buyer.Info;
using CNY_Buyer.WForm;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CNY_Buyer.UControl
{
    public partial class XtraUC001PurchaseRequisitionAE : UserControl
    {
        #region Properties

        private Inf_001PurchaseRequisition _inf001 = new Inf_001PurchaseRequisition();
        private Cls_001PurchaseRequisition _cls001 = new Cls_001PurchaseRequisition();
        private Inf_002PurchaseRequisitionDetail _inf002 = new Inf_002PurchaseRequisitionDetail();
        private Inf_003PurchaseRequisitionAttachment _inf003 = new Inf_003PurchaseRequisitionAttachment();
        private Inf_ApprovalHistory _inf_approval = new Inf_ApprovalHistory();
        private List<Tuple<Control, int>> _list = new List<Tuple<Control, int>>();
        private Int64 _pk;
        private Int32 _status = 0;
        private bool _isRejected;
        private string _otp;
        private readonly Int32 _functionInApprovalPK = 1;
        private readonly String _menuCode = "MN00379";
        private OpenFileDialog op = new OpenFileDialog();
        private bool _performEditValueChangeEvent = false;
        private List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControlApprove = new List<Tuple<LabelControl, SimpleButton, SimpleButton>>();
        private WaitDialogForm dlg;
        private DataTable dtAttachment;
        private string folderPath = "\\\\172.16.0.235\\cny\\Upload File\\PR\\";
        PermissionFormInfo qPer;
        bool _needReApprove;
        int _signPerRow = 6;
        Confirmation _conf;

        #endregion Properties

        #region Contractor

        public XtraUC001PurchaseRequisitionAE(Int64 pk, string otp)
        {
            InitializeComponent();
            _pk = pk;
            _otp = otp;
            _needReApprove = false;
            qPer = ProcessGeneral.GetPermissionByFormCode("Frm001PurchaseRequisition");
            Format();
            GenerateEvent();
            LoadData();
        }

        #endregion Contractor

        #region Format
        public void Format()
        {
            FormatHeader();
            FormatGridview(gcPurchaseRequisition, gvPurchaseRequisition);
            HideSignature();


        }

        #region HideSignature
        public void HideSignature()
        {
            if (_otp != "Edit")
            {
                groupControl3.Visible = false;
            }
            else
            {
                groupControl3.Visible = true;
            }
        }
        #endregion

        #region RemoveControlConfirm
        public void RemoveControlConfirm()
        {
            groupControl3.Controls.Clear();
            _listControlApprove.Clear();
        }
        #endregion

        #region FormatHeader
        public void FormatHeader()
        {
            FormatTextbox();
        }

        #endregion

        #region Format textbox

        public void FormatTextbox()
        {
            txtBuyerDesc.Properties.ReadOnly = true;
            txtRequesterDesc.Properties.ReadOnly = true;
            txtReceiptNumber.Properties.ReadOnly = true;
        }

        #endregion Format textbox

        #region Declare GridView

        private void FormatGridview(GridControl gcMain, GridView gvMain)
        {
            #region "a"

            gcMain.UseEmbeddedNavigator = true;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;

            //gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.Default;
            //gvMain.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
            gvMain.OptionsBehavior.AutoPopulateColumns = true;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = false;
            gvMain.OptionsCustomization.AllowFilter = true;
            //gvMain.OptionsBehavior.Reset();

            //gvMain.OptionsHint.ShowCellHints = true;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            //gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            //gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;

            // gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;
            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            //gvMain.FocusRectStyle = DrawFocusRectStyle.None;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            //gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            gvMain.OptionsView.ShowFooter = false;
            //gridView1.RowHeight = 25;
            gvMain.OptionsView.RowAutoHeight = true; gvMain.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain);
            gvMain.OptionsPrint.AutoWidth = false;

            gvMain.OptionsView.ShowFooter = true;
            gcMain.ForceInitialize();

            #endregion "a"


            InitColumGridviewAttachment();
            FormatGridView.Init3(gcAttachment, gvAttachment);
            LockColumnGridview(gvMain);
            InitColumGridview();

            GenerateEventGridview();
        }
        public void GenerateEventGridview()
        {
            gvPurchaseRequisition.RowCountChanged += gvDeliveryList_RowCountChanged;
            gvPurchaseRequisition.CustomDrawRowIndicator += gvDeliveryList_CustomDrawRowIndicator;
            gvPurchaseRequisition.LeftCoordChanged += gvDeliveryList_LeftCoordChanged;
            gvPurchaseRequisition.MouseMove += gvDeliveryList_MouseMove;
            gvPurchaseRequisition.TopRowChanged += gvDeliveryList_TopRowChanged;
            gvPurchaseRequisition.FocusedColumnChanged += gvDeliveryList_FocusedColumnChanged;
            gvPurchaseRequisition.FocusedRowChanged += gvDeliveryList_FocusedRowChanged;
            gcPurchaseRequisition.Paint += gcDeliveryList_Paint;
            gvPurchaseRequisition.KeyDown += gvDeliveryList_KeyDown;
            gvPurchaseRequisition.RowCellStyle += gvDeliveryList_RowCellStyle;
            gvPurchaseRequisition.ShowingEditor += GvMain_ShowingEditor;
            gvPurchaseRequisition.CellValueChanged += GvPurchaseRequisition_CellValueChanged;
            gvPurchaseRequisition.CustomDrawFooterCell += GvPurchaseRequisition_CustomDrawFooterCell;

            gvAttachment.DoubleClick += GvAttachment_DoubleClick;
            gvAttachment.ShowingEditor += GvAttachment_ShowingEditor;
        }
        void LockColumnGridview(GridView gvMain)
        {
            foreach (GridColumn column in gvMain.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
            }
        }
        void OpenColumnGridviewByPermission()
        {
            string _userName = ProcessGeneral.GetSafeString(txtCreatedBy.EditValue);
            if (qPer.StrSpecialFunction.Contains("BS") && _userName == DeclareSystem.SysUserName)
            {
                gvPurchaseRequisition.Columns["Price"].OptionsColumn.AllowEdit = true;
                gvPurchaseRequisition.Columns["Tax"].OptionsColumn.AllowEdit = true;
            }
            gvPurchaseRequisition.Columns["Note"].OptionsColumn.AllowEdit = true;
        }

        private void GvAttachment_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void GvAttachment_DoubleClick(object sender, EventArgs e)
        {
            GridView gv = sender as GridView;
            Int64 AttachmentPK = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(gv.FocusedRowHandle, "PK"));
            string fileName = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, "AttachName"));
            string extension = GetFileExtension(fileName);
            if (AttachmentPK == -1)//Nếu PK=-1, thì nghĩa là file chưa upload hiển thị file lưu trong datatable
            {
                byte[] filevalue = (byte[])gvAttachment.GetRowCellValue(gv.FocusedRowHandle, "BinaryValue");
                var fNew = new FrmHelpFinal { Text = fileName };
                try
                {
                    Stream stream = new MemoryStream(filevalue);
                    if (extension == "pdf")
                    {
                        fNew.LoadDocument(stream);
                        fNew.Show();
                    }
                    else
                    {
                        Image img = ProcessGeneral.ConvertByteArrayToImage(filevalue);
                        FrmViewImage frm = new FrmViewImage(img);
                        frm.Show();
                    }
                }
                catch
                {
                }
            }
            else//Ngược lại hiển thị file trong folder trên server
            {
                string Path = folderPath + "\\" + _pk + "\\" + ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, "AttachName"));
                var fNew = new FrmHelpFinal { Text = fileName };
                try
                {
                    if (extension == "pdf")
                    {
                        fNew.LoadDocument(Path);
                        fNew.Show();
                    }
                    else
                    {
                        FrmViewImage frm = new FrmViewImage(Path);
                        frm.Show();
                    }
                }
                catch
                {
                }
            }
        }

        public string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).TrimStart('.'); // Lấy phần mở rộng và loại bỏ ký tự '.'
        }

        private void GvPurchaseRequisition_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }

        #endregion Declare GridView

        #region Gridview event

        private void InsertGridDeliveryTo()
        {
            gvPurchaseRequisition.AddNewRow();
            DataRow dr = gvPurchaseRequisition.GetDataRow(gvPurchaseRequisition.FocusedRowHandle);
            dr["PK"] = -1;
            dr["PurchaseRequisitionPK"] = -1;
            dr["Tax"] = 10;
            dr["RequestedDate"] = DateTime.Today;
            dr["DeliveryStatus"] = 1;
            dr["Quantity"] = 1;

            gvPurchaseRequisition.UpdateCurrentRow();
            gvPurchaseRequisition.FocusedColumn = gvPurchaseRequisition.Columns["StockCode"];
            gvPurchaseRequisition.FocusedRowHandle = gvPurchaseRequisition.RowCount - 1;
            gvPurchaseRequisition.ClearSelection();
            gvPurchaseRequisition.SelectCell(gvPurchaseRequisition.RowCount - 1, gvPurchaseRequisition.Columns["StockCode"]);
            gvPurchaseRequisition.Focus();
        }

        private void gvDeliveryList_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    {
                        InsertGridDeliveryTo();
                        break;
                    }
                case Keys.F4:
                    {
                        switch (gv.FocusedColumn.FieldName)
                        {
                            case "StockCode":
                            case "StockName":
                                F4_Keydown(_inf001.Excute("SELECT a.PK,a.Name,a.Code,b.PK Unit, b.NAME UnitName FROM dbo.Stock a INNER JOIN dbo.StockUnit b ON a.Unit =b.PK"),
                                    "List of Stock", "GcDeliveryMethod_KeyDown", new[] { "PK" });
                                break;

                            case "DeliveryStatusName":
                                F4_Keydown(_inf001.Excute("SELECT PK,Name,Code FROM Common_Masterdata WHERE Module='PurchaseRequisitionDetailStatus'"),
                                    "List of Status", "GcDeliveryStatus_KeyDown", new[] { "PK" });
                                break;
                        }
                        break;
                    }
            }
        }

        private void gcDeliveryList_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangleSelection.PaintGridViewSelectionRect((GridControl)sender, e);
        }

        private void gvDeliveryList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            //DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_TopRowChanged(object sender, EventArgs e)
        {
            //DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_MouseMove(object sender, MouseEventArgs e)
        {
            //DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_LeftCoordChanged(object sender, EventArgs e)
        {
            //DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GvMain_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var gv = sender as GridView;
            switch (gv.FocusedColumn.FieldName)
            {
                case "StockCode":
                case "DeliveryStatusName":
                case "StockName":
                case "Amount":
                case "UnitName":
                    e.Cancel = true;
                    break;

                default:
                    e.Cancel = false;
                    break;
            }
        }

        private void gvDeliveryList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.OptionsColumn.AllowEdit == false)
            {
                // Đặt màu cho ô khi cột bị khóa
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else
            {
                // Đặt màu cho các ô khác (các cột không bị khóa)
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
            }
        }

        private void gvDeliveryList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
            bool selected = gv.IsRowSelected(e.RowHandle);
            if (selected)
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }

            if (selected)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
        }

        private void gvDeliveryList_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
        }

        private void GvPurchaseRequisition_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            Int32 rH = e.RowHandle;
            if (fieldName == "Price" || fieldName == "Tax" || fieldName == "Quantity")
            {
                Int64 Price = ProcessGeneral.GetSafeInt64(gvPurchaseRequisition.GetRowCellValue(rH, "Price"));
                Int64 Tax = ProcessGeneral.GetSafeInt64(gvPurchaseRequisition.GetRowCellValue(rH, "Tax"));
                Int64 Quantity = ProcessGeneral.GetSafeInt64(gvPurchaseRequisition.GetRowCellValue(rH, "Quantity"));
                Decimal Amount = (Price * Tax * Quantity / 100) + Price * Quantity;
                gvPurchaseRequisition.SetRowCellValue(rH, "Amount", Amount);
            }
        }

        public void ButtonClick()
        {
            btnInsertMatarial.Click += BtnInsertMainMaterial_Click;
            btnDeleteRow.Click += BtnDeleteMainMaterial_Click;
            btnInsertAttact.Click += BtnInsertAttact_Click;
            btnDeleteAttact.Click += BtnDeleteAttact_Click;
        }

        private void BtnDeleteAttact_Click(object sender, EventArgs e)
        {
            if (gvAttachment.FocusedRowHandle >= 0)
            {
                gvAttachment.DeleteRow(gvAttachment.FocusedRowHandle);
                gvAttachment.RefreshData();
            }
        }

        private void BtnInsertAttact_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataRow dr = dtAttachment.NewRow();
                dr["PK"] = -1;
                dr["PurchaseRequisitionPK"] = _pk;
                dr["AttachName"] = ofd.SafeFileName;
                dr["BinaryValue"] = GetFileData(ofd.FileName);
                dtAttachment.Rows.Add(dr);
                gcAttachment.DataSource = dtAttachment;
            }
        }

        public byte[] GetFileData(string strPath)
        {
            var fs = new FileStream(strPath, FileMode.Open, FileAccess.Read); //create a file stream object associate to user selected file
            byte[] fileData;
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                fileData = ms.ToArray();
            }
            return fileData;
        }

        private void BtnDeleteMainMaterial_Click(object sender, EventArgs e)
        {
            int rH = gvPurchaseRequisition.FocusedRowHandle;
            if (rH + 1 == gvPurchaseRequisition.RowCount)//Kiểm tra row hiện tại row cuối không
            {
                //Kiểm tra phía trên còn row nào không
                if (rH > 0)
                {
                    gvPurchaseRequisition.FocusedRowHandle = rH - 1;
                }
            }
            else //nếu ko thì thì foucs nhảy xuống dưới 1 row
            {
                gvPurchaseRequisition.FocusedRowHandle = rH;
            }
            gvPurchaseRequisition.DeleteRow(rH);
            gvPurchaseRequisition.UpdateCurrentRow();
            gvPurchaseRequisition.Focus();
        }

        private void BtnInsertMainMaterial_Click(object sender, EventArgs e)
        {
            Generate();
        }

        #endregion Gridview event

        #region InitColumGridview

        private void InitColumGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "PurchaseRequisitionPK", "PurchaseRequisitionPK", -1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "StockPK", "StockPK", -1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Unit", "Unit", -1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "MaterialRequirementPK", "MaterialRequirementPK", -1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Mã dự án", "ProjectCode", 1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "STT", "OrdinalNumber", 1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Mã vật tư (F4)", "StockCode", 1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Tên vật tư (F4)", "StockName", 2);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Đơn vị tính", "UnitName", 2);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Số lượng", "Quantity", 2);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Giá", "Price", 3);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Thuế", "Tax", 4);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Thành tiền", "Amount", 5);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Chứng chỉ", "Certificate", 5);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Xuất xứ", "Origin", 5);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Hãng sản xuất", "Manufacturer", 5);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Ngày yêu cầu", "RequestedDate", 5);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Ngày nhận đề nghị", "ReceiveRequestDate", -7);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Ngày yêu cầu gia hàng", "RequestDeliveryDate", -8);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Ngày giao hàng thực tế", "DeliveryDate", -9);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "DeliveryStatusPK", "DeliveryStatus", -1);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Tình trạng giao hàng (F4)", "DeliveryStatusName", 10);
            FormatGridView.CreateColumnOnGridview(gvPurchaseRequisition, "Ghi chú", "Note", 11);

            gvPurchaseRequisition.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvPurchaseRequisition.Columns["Price"].DisplayFormat.FormatString = "N0";
            gvPurchaseRequisition.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvPurchaseRequisition.Columns["Quantity"].DisplayFormat.FormatString = "N0";
            gvPurchaseRequisition.Columns["Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvPurchaseRequisition.Columns["Amount"].DisplayFormat.FormatString = "N0";
            gvPurchaseRequisition.Columns["Tax"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvPurchaseRequisition.Columns["Tax"].DisplayFormat.FormatString = "N0";
            gvPurchaseRequisition.Columns["RequestedDate"].DisplayFormat.FormatType = FormatType.DateTime;
            gvPurchaseRequisition.Columns["RequestedDate"].DisplayFormat.FormatString = "dd-MM-yyyy";

            InitFooterCell(gvPurchaseRequisition, "Quantity");
            InitFooterCell(gvPurchaseRequisition, "Amount");
        }

        private void InitFooterCell(GridView gvMain, string ColumnName)
        {
            GridColumn column = gvMain.Columns[ColumnName];
            column.SummaryItem.SummaryType = SummaryItemType.Sum;
            column.SummaryItem.DisplayFormat = "{0:0,0}";
        }

        private void InitColumGridviewAttachment()
        {
            FormatGridView.CreateColumnOnGridview(gvAttachment, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvAttachment, "PurchaseRequisitionPK", "PurchaseRequisitionPK", -1);
            FormatGridView.CreateColumnOnGridview(gvAttachment, "File đính kèm", "AttachName", 1);
            FormatGridView.CreateColumnOnGridview(gvAttachment, "CreatedBy", "CreatedBy", -1);
            FormatGridView.CreateColumnOnGridview(gvAttachment, "CreatedDate", "CreatedDate", -1);
        }

        #endregion InitColumGridview

        #endregion

        #region LoadData
        public void LoadData()
        {
            LoadDataToSlue();
            if (_pk == -1)
            {
                DisplayDataForAdding();
            }
            else
            {
                DisplayDataForEditing();
            }
            //Kiểm tra nếu đây là sao chép từ phiếu cũ thì PK=-1
            if (_otp == "Copy")
            {
                _pk = -1;
            }
            OpenColumnGridviewByPermission();
            InitTableAttachment();
        }
        #region Load Data To Search lookup Edit

        public void LoadDataToSlue()
        {
            //slueBuyer
            slueBuyer.Properties.DataSource = _inf001.Excute("SELECT UserName Code, FullName [Name] FROM dbo.ListUser WHERE DepartmentCode='PMH' AND IsActive =1");
            slueBuyer.Properties.DisplayMember = "Code";
            slueBuyer.Properties.ValueMember = "Code";
            slueBuyer.Properties.NullText = null;

            //PaymentMethod
            sluePaymentMethod.Properties.DataSource = _inf001.Excute("SELECT PK [Code],[Name] FROM dbo.PaymentMethod");
            sluePaymentMethod.Properties.DisplayMember = "Name";
            sluePaymentMethod.Properties.ValueMember = "Code";
            sluePaymentMethod.Properties.NullText = null;

            //Supplier
            slueSupplier.Properties.DataSource = _inf001.Excute("SELECT PK [Code],RTRIM(CNY002)Name,CNY001 [Tax Code] FROM dbo.CNY00002 where CNY022=1");
            slueSupplier.Properties.DisplayMember = "Name";
            slueSupplier.Properties.ValueMember = "Code";
            slueSupplier.Properties.NullText = null;

            //Requester
            slueRequester.Properties.DataSource = _inf001.Excute("SELECT UserName Code, FullName [Name],b.DepartmentName FROM dbo.ListUser a INNER JOIN dbo.ListDepartment b ON a.DepartmentCode=b.DepartmentCode WHERE a.UserName NOT IN ('admin') AND a.IsActive =1 ORDER BY a.UserName ");
            slueRequester.Properties.DisplayMember = "Code";
            slueRequester.Properties.ValueMember = "Code";
            slueRequester.Properties.NullText = null;
        }

        #endregion Load Data To Search lookup Edit

        #region Display Data For Editing

        public void DisplayDataForEditing()
        {
            DataTable dtHeader = _inf001.sp_PurchaseRequisition_Select(_pk);
            if (dtHeader.Rows.Count == 0) return;
            _pk = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["PK"]);
            txtReceiptNumber.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ReceiptNumber"]);
            txtRequestNumber.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["RequestNumber"]);
            txtPoNumber.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PoNumber"]);
            slueBuyer.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Buyer"]);
            slueRequester.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Requester"]);
            sluePaymentMethod.EditValue = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["PaymentMethod"]);
            slueSupplier.EditValue = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["Supplier"]);
            spinDayOfDebt.EditValue = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["DayOfDebt"]);
            txtNote.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Note"]);
            txtCreatedBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Created_By"]);
            deCreatedDate.EditValue = ProcessGeneral.GetSafeDatetimeOjectNull(dtHeader.Rows[0]["Created_Date"]);
            txtUpdatedBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Updated_By"]);
            deUpdatedDate.EditValue = ProcessGeneral.GetSafeDatetimeOjectNull(dtHeader.Rows[0]["Updated_Date"]);

            LoadDataGridviewPurchaseRequisition();
            LoadDataGridviewAttachment();
            if (_pk > 0)
            {
                _conf = new Confirmation(_functionInApprovalPK, _pk, _signPerRow, groupControl3, qPer, _menuCode);
                _conf.RemoveControlConfirm();
                _conf.GenerateControlConfirm();
                _conf.DisplayConfirm();
            }
        }

        #endregion Display Data For Editing

        #region Display Data For Adding

        public void DisplayDataForAdding()
        {
            txtCreatedBy.Text = DeclareSystem.SysUserName;
            deCreatedDate.EditValue = DateTime.Today;
            LoadDataGridviewPurchaseRequisition();
        }

        #endregion Display Data For Adding

        #region LoadListControApprove
        //private void LoadListControApprove()
        //{
        //    foreach (DataRow dr in dtLevelFunctionApprove.Rows)
        //    {
        //        Tuple<LabelControl, SimpleButton, SimpleButton> newItem =
        //         new Tuple<LabelControl, SimpleButton, SimpleButton>(GetLable("lblNotice" + dr["Level"].ToString()), GetSimpleButton("btnConfirm" + dr["Level"].ToString()), GetSimpleButton("btnReject" + dr["Level"].ToString()));

        //        _listControlApprove.Add(newItem);

        //    }
        //}
        #endregion

        #region InitTableAttachment

        public void InitTableAttachment()
        {
            dtAttachment = new DataTable();
            dtAttachment.Columns.Add("PK", typeof(Int64));
            dtAttachment.Columns.Add("PurchaseRequisitionPK", typeof(Int64));
            dtAttachment.Columns.Add("AttachName", typeof(String));
            dtAttachment.Columns.Add("BinaryValue", typeof(Byte[]));
            dtAttachment.Columns.Add("CreatedBy", typeof(String));
            dtAttachment.Columns.Add("CreatedDate", typeof(DateTime));
        }

        #endregion InitTableAttachment

        #region LoadDataGridview

        public void LoadDataGridviewPurchaseRequisition()
        {
            DataTable dtPurchaseRequisitionDetail = _inf002.sp_PurchaseRequisitionDetail_Select(_pk);
            if (_otp == "Copy")
            {
                dtPurchaseRequisitionDetail.Select().ToList<DataRow>().ForEach(r => r["PK"] = -1);//Nếu là copy thì Update PK thành 1
            }
            gcPurchaseRequisition.DataSource = dtPurchaseRequisitionDetail;
            gvPurchaseRequisition.BestFitColumns();
        }

        public void LoadDataGridviewAttachment()
        {
            DataTable dtPurchaseRequisitionDetail = _inf003.sp_PurchaseRequisitionAttachment_Select(_pk);
            if (_otp == "Copy")
            {
                dtPurchaseRequisitionDetail.Select().ToList<DataRow>().ForEach(r => r["PK"] = -1);//Nếu là copy thì Update PK thành 1
            }
            gcAttachment.DataSource = dtPurchaseRequisitionDetail;
            gvAttachment.BestFitColumns();
        }

        #endregion LoadDataGridview
        #endregion

        #region GenerateEvent
        public void GenerateEvent()
        {
            GenerateEventSearchlookupEdit();

            ButtonClick();
        }


        #region Generate Event Search lookup Edit

        public void GenerateEventSearchlookupEdit()
        {
            slueBuyer.EditValueChanged += slue_EditValueChanged;
            slueRequester.EditValueChanged += slue_EditValueChanged;
            slueSupplier.EditValueChanged += SlueSetTooltip_EditValueChanged;
            sluePaymentMethod.EditValueChanged += SlueSetTooltip_EditValueChanged;

            slueBuyer.Popup += slue_Popup;
            sluePaymentMethod.Popup += slue_Popup;
            slueSupplier.Popup += slue_Popup;
            slueRequester.Popup += slue_Popup;
        }

        private void SlueSetTooltip_EditValueChanged(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            var drv = slue.Properties.GetRowByKeyValue(slue.EditValue) as DataRowView;
            if (drv != null)
            {
                string a = ProcessGeneral.GetSafeString(drv.Row["Name"]);
                slue.ToolTip = a;
            }
        }

        private void slue_Popup(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            GridView a = slue.Properties.View;
            a.BestFitColumns();
        }

        private void slue_EditValueChanged(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            SetDescriptionText(slue);
        }

        public void SetDescriptionText(SearchLookUpEdit slue)
        {
            DataTable dtsource = slue.Properties.DataSource as DataTable;
            TextEdit desc = this.Controls.Find("txt" + slue.Name.Substring(4, slue.Name.Length - 4) + "Desc", true).FirstOrDefault() as TextEdit;
            if (dtsource == null)
            {
                desc.EditValue = "";
                return;
            }
            var drv = slue.Properties.GetRowByKeyValue(slue.EditValue) as DataRowView;
            if (drv != null)
            {
                string a = ProcessGeneral.GetSafeString(drv.Row["Name"]);

                desc.EditValue = a;
            }
            else
            {
                desc.EditValue = "";
            }
        }

        #endregion Generate Event Search lookup Edit


        #endregion GenerateEvent

        #region Handle Event
        #region Save

        public void Save()
        {
            #region Check input
            if (!CheckInput())
                return;
            #endregion

            #region Check if approved
            if (_needReApprove)
            {
                DataTable dt = _inf_approval.sp_ApprovalHistory_GetUserApproved(_functionInApprovalPK, _pk);

                if (dt.Rows.Count > 0)
                {
                    DialogResult dlResult = XtraMessageBox.Show("Phiếu này đã được duyệt. Nếu tiếp tục lưu sẽ phải duyệt lại từ đầu? ", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dlResult == DialogResult.Yes)
                    {
                        string _userName = ProcessGeneral.GetSafeString(txtCreatedBy.EditValue);
                        if (_userName == DeclareSystem.SysUserName)
                        {
                            dt = _inf_approval.sp_ApprovalHistory_Delete(_functionInApprovalPK, _pk);
                            XtraMessageBox.Show("Các phê duyệt trước đó đã bị xóa. Vui lòng xin phê duyệt lại.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _status = 0;
                            _needReApprove = false;
                            _conf.DisplayConfirm();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            #endregion

            #region Get Info

            _cls001.PK = _pk;
            _cls001.ProjectCode = "";
            _cls001.Requester = ProcessGeneral.GetSafeString(slueRequester.EditValue);
            _cls001.Buyer = ProcessGeneral.GetSafeString(slueBuyer.EditValue);
            _cls001.RequestNumber = ProcessGeneral.GetSafeString(txtRequestNumber.EditValue);
            _cls001.ReceiptNumber = ProcessGeneral.GetSafeString(txtReceiptNumber.EditValue);
            _cls001.PONumber = ProcessGeneral.GetSafeString(txtPoNumber.EditValue);
            _cls001.Supplier = ProcessGeneral.GetSafeInt(slueSupplier.EditValue); ;
            _cls001.PaymentMethod = ProcessGeneral.GetSafeInt(sluePaymentMethod.EditValue);
            _cls001.DayOfDebt = ProcessGeneral.GetSafeInt(spinDayOfDebt.EditValue);
            _cls001.Created_By = DeclareSystem.SysUserName;
            _cls001.Created_Date = DateTime.Now;
            _cls001.Updated_By = DeclareSystem.SysUserName;
            _cls001.Updated_Date = DateTime.Now;
            _cls001.Note = ProcessGeneral.GetSafeString(txtNote.EditValue);

            #endregion Get Info

            #region Save and show message

            //Update data
            DataTable dtSaveResult = _inf001.sp_PurchaseRequisition_Update(_cls001);
            //Get result info
            string msg = ProcessGeneral.GetSafeString(dtSaveResult.Rows[0]["ErrMsg"]);
            //Set new ID
            if (_cls001.PK == -1)
            {
                //Update new ID
                _cls001.PK = ProcessGeneral.GetSafeInt(dtSaveResult.Rows[0]["IDENTITY"]);
                _pk = _cls001.PK;
            }
            //Save Datail
            SaveDetail();

            //Save Attachment
            SaveAttachment();

            XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DisplayDataForEditing();

            #endregion Save and show message
        }

        public void SaveDetail()
        {
            DataTable dt = gcPurchaseRequisition.DataSource as DataTable;
            if (dt == null) return;
            dt.AcceptChanges();

            dt.Select().ToList<DataRow>().ForEach(r => r["PurchaseRequisitionPK"] = _pk);//Update FK thành PK của Product Code
            DataTable qdt = new DataTable();
            qdt.Columns.Add("PK", typeof(Int64));
            qdt.Columns.Add("PurchaseRequisitionPK", typeof(Int64));
            qdt.Columns.Add("StockPK", typeof(Int64));
            qdt.Columns.Add("Quantity", typeof(Int32));
            qdt.Columns.Add("Price", typeof(Int64));
            qdt.Columns.Add("Tax", typeof(Int32));
            qdt.Columns.Add("RequestedDate", typeof(DateTime));
            qdt.Columns.Add("ReceiveRequestDate", typeof(DateTime));
            qdt.Columns.Add("RequestDeliveryDate", typeof(DateTime));
            qdt.Columns.Add("DeliveryDate", typeof(DateTime));
            qdt.Columns.Add("DeliveryStatus", typeof(Int32));
            qdt.Columns.Add("Note", typeof(string));
            qdt.Columns.Add("OrdinalNumber", typeof(Int32));
            qdt.Columns.Add("MaterialRequirementPK", typeof(Int64));
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drAdd = qdt.NewRow();
                drAdd["PK"] = ProcessGeneral.GetSafeInt64(dr["PK"]);
                drAdd["PurchaseRequisitionPK"] = ProcessGeneral.GetSafeInt64(dr["PurchaseRequisitionPK"]);
                drAdd["StockPK"] = ProcessGeneral.GetSafeInt64(dr["StockPK"]);
                drAdd["Quantity"] = ProcessGeneral.GetSafeInt(dr["Quantity"]);
                drAdd["Price"] = ProcessGeneral.GetSafeInt64(dr["Price"]);
                drAdd["Tax"] = ProcessGeneral.GetSafeInt(dr["Tax"]);
                drAdd["RequestedDate"] = ProcessGeneral.GetSafeDatetimeDBNull(dr["RequestedDate"]);
                drAdd["ReceiveRequestDate"] = ProcessGeneral.GetSafeDatetimeDBNull(dr["ReceiveRequestDate"]);
                drAdd["RequestDeliveryDate"] = ProcessGeneral.GetSafeDatetimeDBNull(dr["RequestDeliveryDate"]);
                drAdd["DeliveryDate"] = ProcessGeneral.GetSafeDatetimeDBNull(dr["DeliveryDate"]);
                drAdd["DeliveryStatus"] = ProcessGeneral.GetSafeInt(dr["DeliveryStatus"]);
                drAdd["Note"] = ProcessGeneral.GetSafeString(dr["Note"]);
                drAdd["OrdinalNumber"] = ProcessGeneral.GetSafeInt(dr["OrdinalNumber"]);
                drAdd["MaterialRequirementPK"] = ProcessGeneral.GetSafeInt64(dr["MaterialRequirementPK"]);
                qdt.Rows.Add(drAdd);
            }
            _inf002.sp_PurchaseRequisitionDetail_Update(_pk, qdt);
        }

        public void SaveAttachment()
        {
            DataTable dt = gcAttachment.DataSource as DataTable;
            if (dt == null) return;
            dt.AcceptChanges();

            //Xóa những file trên server mà người dùng đã xóa trên lưới
            if (_pk != -1)
            {
                DeleteFile();
            }
            //Upload những file mới thêm vào lưới lên server
            UploadFile();

            //Lưu vào database
            dt.Select().ToList<DataRow>().ForEach(r => r["PurchaseRequisitionPK"] = _pk);//Update FK thành PK của Product Code
            DataTable qdt = new DataTable();
            qdt.Columns.Add("PK", typeof(Int64));
            qdt.Columns.Add("PurchaseRequisitionPK", typeof(Int64));
            qdt.Columns.Add("AttachName", typeof(String));
            qdt.Columns.Add("CreatedBy", typeof(String));
            qdt.Columns.Add("CreatedDate", typeof(DateTime));
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drAdd = qdt.NewRow();
                drAdd["PK"] = ProcessGeneral.GetSafeInt64(dr["PK"]);
                drAdd["PurchaseRequisitionPK"] = ProcessGeneral.GetSafeInt64(dr["PurchaseRequisitionPK"]);
                drAdd["AttachName"] = ProcessGeneral.GetSafeString(dr["AttachName"]);
                drAdd["CreatedBy"] = ProcessGeneral.GetSafeString(dr["CreatedBy"]);
                drAdd["CreatedDate"] = ProcessGeneral.GetSafeDatetimeDBNull(dr["CreatedDate"]);
                qdt.Rows.Add(drAdd);
            }
            _inf003.sp_PurchaseRequisitionAttachment_Update(_pk, qdt);
        }

        public void DeleteFile()
        {
            // Đường dẫn đến thư mục chứa các file
            string _path = folderPath + "\\" + _pk;
            if (!Directory.Exists(_path))
            {
                return;
            }
            // Kiểm tra từng file trong thư mục
            string[] files = Directory.GetFiles(_path);
            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                if (!IsFileInDataTable(fileName, dtAttachment))
                {
                    // Xóa file nếu tên file không có trong DataTable
                    File.Delete(filePath);
                }
            }
        }

        public void UploadFile()
        {
            string _path = folderPath + "\\" + _pk;
            // Duyệt qua từng dòng trong DataTable để kiểm tra và upload file
            foreach (DataRow dr in dtAttachment.Rows)
            {
                Int64 pk = ProcessGeneral.GetSafeInt64(dr["PK"]);
                if (!Directory.Exists(_path))
                {
                    try
                    {
                        // Tạo thư mục nếu chưa tồn tại
                        Directory.CreateDirectory(_path);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi tạo thư mục: " + ex.Message);
                    }
                }
                // Kiểm tra pk = -1 thì mới upload file
                if (pk == -1)
                {
                    string fileName = dr["AttachName"].ToString();
                    string filePath = folderPath + "\\" + _pk;
                    byte[] filevalue = (byte[])dr["BinaryValue"];
                    SaveFileToFolder(filevalue, filePath, fileName);
                }
            }
        }

        public static void SaveFileToFolder(byte[] fileData, string folderPath, string fileName)
        {
            string filePath = Path.Combine(folderPath, fileName);

            // Ghi dữ liệu từ mảng byte vào file
            File.WriteAllBytes(filePath, fileData);
        }

        // Kiểm tra xem tên file có trong dtAttachment hay không
        public static bool IsFileInDataTable(string fileName, DataTable dataTable)
        {
            return dataTable.Rows.Cast<DataRow>().Any(row => row["AttachName"].ToString() == fileName);
        }
        #region Check input
        public bool CheckInput()
        {
            Boolean success = true;
            var list = new List<Tuple<Control, string, int>>();
            list.Add(new Tuple<Control, string, int>(slueRequester, "Người đề nghị", 0));
            list.Add(new Tuple<Control, string, int>(slueBuyer, "Người mua hàng", 0));
            list.Add(new Tuple<Control, string, int>(slueSupplier, "Nhà cung cấp", 0));
            list.Add(new Tuple<Control, string, int>(sluePaymentMethod, "Hình thức thanh toán", 0));


            foreach (Tuple<Control, string, int> item in list)
            {
                string controlType = item.Item1.GetType().ToString();
                bool isnull = true;
                if (controlType == "DevExpress.XtraEditors.SearchLookUpEdit")
                {
                    SearchLookUpEdit a = item.Item1 as SearchLookUpEdit;
                    isnull = String.IsNullOrEmpty(ProcessGeneral.GetSafeString(a.EditValue));
                }
                else
                {
                    TextEdit a = item.Item1 as TextEdit;
                    isnull = String.IsNullOrEmpty(ProcessGeneral.GetSafeString(a.EditValue));
                }

                if (isnull)
                {
                    item.Item1.Focus();
                    success = false;
                    XtraMessageBox.Show(item.Item2 + " không được để trống!", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
            return success;
        }
        #endregion
        #endregion Save

        #region Generate Data Item
        public void Generate()
        {
            DataTable dt = new DataTable();
            DataTable dtSource = gcPurchaseRequisition.DataSource as DataTable;
            using (var f2 = new Frm001PurchaseRequisitionSelectItem(dtSource))
            {
                f2.Text = @"Chọn vật tư";
                f2.OnGetValue += (s, f) =>
                {
                    dt = f.Value as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        dtSource.ImportRow(row);
                    }
                    gcPurchaseRequisition.DataSource = dtSource;
                };
                f2.ShowDialog();
            }
        }
        #endregion

        #region SendEmail

        public void SendMailRequest(string EmailTo)
        {
            string title = "Yêu cầu phê duyệt Phiếu Đề nghị mua hàng số  " + _pk;
            string body = "Vui lòng kiểm tra để phê duyệt hoặc từ chối Phiếu Đề nghị mua hàng số  " + _pk + " tạo bởi " + txtCreatedBy.EditValue.ToString() + ". Xem chi tiết tại phần mềm VTStek, mục Đề nghị Mua hàng.";
            //string body = "Vui lòng kiểm tra để phê duyệt hoặc từ chối Phiếu Đề nghị mua hàng số  " + _pk + " tạo bởi " + txtCreatedBy.EditValue.ToString() + ". Xem chi tiết tại https://antaris.github.io/RazorEngine/ReferenceResolver.html";
            Email.Send(EmailTo, title, body);
        }

        public void SendMailNotice(string EmailTo, int status)
        {
            string actionName = status == 1 ? "được duyệt" : "bị từ chối";
            string title = "Thông báo tình trạng Phiếu Đề nghị mua hàng số " + _pk + " đã " + actionName + " bởi " + DeclareSystem.SysUserName;

            Email.Send(EmailTo, title, title);
        }

        #endregion SendEmail

        #region Print

        public void Print()
        {
            DataTable dt = _inf002.sp_PurchaseRequisitionDetail_Select(_pk);
            DataTable dtHeader = _inf001.sp_PurchaseRequisition_Select(_pk);
            DataTable dtSignature = _inf_approval.sp_ApprovalHistory_SelectUserSignature(_functionInApprovalPK, _pk);
            ReportPrintTool printTool = new ReportPrintTool(new Rpt001PurchaseRequisition(dt, dtHeader, dtSignature));
            printTool.ShowPreview();
            // Xuất báo cáo
            printTool.ShowPreview();
        }

        #endregion Print

        #region ClearForm

        public void ClearForm()
        {
        }

        public void SetDefaultInfo()
        {
        }

        #endregion ClearForm

        #region "KeyDown"

        private void F4_Keydown(DataTable tb, string nameOfForm, string nameOfText, string[] arrHideColumn)
        {
            #region ""

            if (tb.Rows.Count <= 0) return;
            var lG = new List<GridViewTransferDataColumnInit>();
            int i = 0;
            foreach (DataColumn col in tb.Columns)
            {
                string colName = col.ColumnName;
                GridViewTransferDataColumnInit item = new GridViewTransferDataColumnInit
                {
                    Caption = colName,
                    FieldName = colName,
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = FixedStyle.None,
                    //VisibleIndex = i,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 20,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                };
                if (arrHideColumn.Contains(col.ColumnName))
                {
                    item.VisibleIndex = -1;
                }
                else
                {
                    item.VisibleIndex = i;
                }
                lG.Add(item);
                i++;
            }
            var f = new FrmTransferData
            {
                DtSource = tb,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = nameOfForm.ToUpper(),
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true,
            };
            if (nameOfText == "abc")
            {
                f.IsMultiSelected = true;
            }

            #endregion ""

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                F4_Values(nameOfText, lDr);
            };
            f.ShowDialog();
        }

        private void F4_Values(string textboxName, List<DataRow> lDr)
        {
            if (textboxName == "GcDeliveryMethod_KeyDown")
            {
                int StockPK = ProcessGeneral.GetSafeInt(lDr[0]["PK"].ToString());
                string StockCode = ProcessGeneral.GetSafeString(lDr[0]["Code"].ToString());
                string StockName = ProcessGeneral.GetSafeString(lDr[0]["Name"].ToString());
                string UnitName = ProcessGeneral.GetSafeString(lDr[0]["UnitName"].ToString());
                int Unit = ProcessGeneral.GetSafeInt(lDr[0]["Unit"].ToString());
                gvPurchaseRequisition.SetRowCellValue(gvPurchaseRequisition.FocusedRowHandle, "StockPK", StockPK);
                gvPurchaseRequisition.SetRowCellValue(gvPurchaseRequisition.FocusedRowHandle, "StockCode", StockCode);
                gvPurchaseRequisition.SetRowCellValue(gvPurchaseRequisition.FocusedRowHandle, "StockName", StockName);
                gvPurchaseRequisition.SetRowCellValue(gvPurchaseRequisition.FocusedRowHandle, "UnitName", UnitName);
                gvPurchaseRequisition.SetRowCellValue(gvPurchaseRequisition.FocusedRowHandle, "Unit", Unit);
                gvPurchaseRequisition.FocusedColumn = gvPurchaseRequisition.Columns["Quantity"];
                gvPurchaseRequisition.ClearSelection();
                gvPurchaseRequisition.SelectCell(gvPurchaseRequisition.FocusedRowHandle, gvPurchaseRequisition.FocusedColumn);
                gvPurchaseRequisition.Focus();
            }
            else if (textboxName == "GcDeliveryStatus_KeyDown")
            {
                int DeliveryPK = ProcessGeneral.GetSafeInt(lDr[0]["PK"].ToString());
                string DeliveryName = ProcessGeneral.GetSafeString(lDr[0]["Name"].ToString());
                gvPurchaseRequisition.SetRowCellValue(gvPurchaseRequisition.FocusedRowHandle, "DeliveryStatus", DeliveryPK);
                gvPurchaseRequisition.SetRowCellValue(gvPurchaseRequisition.FocusedRowHandle, "DeliveryStatusName", DeliveryName);
            }
            else if (textboxName == "GcDeliveryMethod_KeyDown")
            {
            }
            gvPurchaseRequisition.BestFitColumns();
        }

        #endregion "KeyDown"
        #endregion Handle Event











    }
}