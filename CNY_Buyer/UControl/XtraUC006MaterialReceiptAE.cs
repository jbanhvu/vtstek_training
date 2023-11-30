using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_Buyer.Class;
using CNY_Buyer.Common;
using CNY_Buyer.Info;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using CNY_BaseSys.Info;
using CNY_BaseSys.Class;

namespace CNY_Buyer.UControl
{
    public partial class XtraUC006MaterialReceiptAE : UserControl
    {
        #region Properties
        Inf_001PurchaseRequisition _inf001 = new Inf_001PurchaseRequisition();
        Inf_004MaterialRequirement _inf004 = new Inf_004MaterialRequirement();
        Inf_005MaterialRequirementDetail _inf005 = new Inf_005MaterialRequirementDetail();
        Inf_006MaterialReceipt _inf006 = new Inf_006MaterialReceipt();
        Inf_007MaterialReceiptDetail _inf007 = new Inf_007MaterialReceiptDetail();
        Cls_006MaterialReceipt _cls006 = new Cls_006MaterialReceipt();
        Inf_ApprovalHistory _inf_approval = new Inf_ApprovalHistory();
        List<Tuple<Control, int>> _list = new List<Tuple<Control, int>>();
        Int64 _pk;
        Int32 _status = 0;
        bool _isRejected;
        string _otp;
        string _receiver, _provider;
        readonly Int32 _functionInApprovalPK = 3;
        readonly String _menuCode = "MN00382";
        OpenFileDialog op = new OpenFileDialog();
        private bool _performEditValueChangeEvent = false;
        List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControlApprove = new List<Tuple<LabelControl, SimpleButton, SimpleButton>>();
        private WaitDialogForm dlg;
        PermissionFormInfo qPer;
        DataTable dtApproveHistory;
        #endregion

        #region Contractor
        public XtraUC006MaterialReceiptAE(Int64 pk, string otp)
        {
            InitializeComponent();
            _pk = pk;
            _otp = otp;
            qPer = ProcessGeneral.GetPermissionByFormCode("Frm006MaterialReceipt");

            LoadListControApprove();
            InitColumGridview();
            Declare_GridView(gcMaterialRequirement, gvMaterialRequirement);
            LockColumnGridview();
            OpenColumnGridviewByPermission();
            DeclareGridViewEvent();
            GenerateEventSearchlookupEdit();
            GenerateEventButton();
            //Load slue
            LoadDataToSlue();


            ButtonClick();
            //Load Data control
            DisplayConfirmQuotationAdding();
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
            else
            {
                DisplayConfirm();
            }



        }
        #endregion

        #region Declare GridView

        private void Declare_GridView(GridControl gcMain, GridView gvMain)
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
            gvMain.RowCountChanged += gvDeliveryList_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvDeliveryList_CustomDrawRowIndicator;
            gvMain.LeftCoordChanged += gvDeliveryList_LeftCoordChanged;
            gvMain.MouseMove += gvDeliveryList_MouseMove;
            gvMain.TopRowChanged += gvDeliveryList_TopRowChanged;
            gvMain.FocusedColumnChanged += gvDeliveryList_FocusedColumnChanged;
            gvMain.FocusedRowChanged += gvDeliveryList_FocusedRowChanged;
            gcMain.Paint += gcDeliveryList_Paint;
            gcMain.ForceInitialize();
            #endregion
        }
        private void DeclareGridViewEvent()
        {
            gvMaterialRequirement.KeyDown += gvDeliveryList_KeyDown;
            gvMaterialRequirement.RowCellStyle += gvDeliveryList_RowCellStyle;
            gvMaterialRequirement.ShowingEditor += GvMain_ShowingEditor;
            gvMaterialRequirement.CellValueChanged += GvMaterialRequirement_CellValueChanged;
            gvMaterialRequirement.CustomDrawFooterCell += GvMaterialRequirement_CustomDrawFooterCell;
        }

        private void GvAttachment_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void GvAttachment_DoubleClick(object sender, EventArgs e)
        {
            GridView gv = sender as GridView;
            string Path = "\\\\172.16.0.235\\cny\\Upload File\\PR\\" + _pk + "\\" + ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, "AttachName"));
            var fNew = new FrmHelpFinal { Text = "PR" };
            try
            {
                fNew.LoadDocument(Path);
                fNew.Show();
            }
            catch
            {

            }
        }

        private void GvMaterialRequirement_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }
        #endregion

        #region Gridview event


        private void InsertGridDeliveryTo()
        {
            gvMaterialRequirement.AddNewRow();
            DataRow dr = gvMaterialRequirement.GetDataRow(gvMaterialRequirement.FocusedRowHandle);
            dr["PK"] = -1;
            dr["MaterialRequirementPK"] = -1;
            dr["RequestedDate"] = DateTime.Today;
            dr["QuantityOfRequset"] = 1;

            gvMaterialRequirement.UpdateCurrentRow();
            gvMaterialRequirement.FocusedColumn = gvMaterialRequirement.Columns["StockCode"];
            gvMaterialRequirement.FocusedRowHandle = gvMaterialRequirement.RowCount - 1;
            gvMaterialRequirement.ClearSelection();
            gvMaterialRequirement.SelectCell(gvMaterialRequirement.RowCount - 1, gvMaterialRequirement.Columns["StockCode"]);
            gvMaterialRequirement.Focus();
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
                                F4_Keydown(_inf001.Excute("SELECT PK,Name,Code FROM dbo.Stock"),
                                    "List of Stock", "GcDeliveryMethod_KeyDown", new[] { "PK" });
                                break;
                            case "DeliveryStatusName":
                                F4_Keydown(_inf001.Excute("SELECT PK,Name,Code FROM Common_Masterdata WHERE Module='MaterialRequirementDetailStatus'"),
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
        private void GvMaterialRequirement_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //if (!_performEditValueChangeEvent) return;
            var gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            Int32 rH = e.RowHandle;
            if (fieldName == "Price" || fieldName == "Tax" || fieldName == "Quantity")
            {
                Int64 Price = ProcessGeneral.GetSafeInt64(gvMaterialRequirement.GetRowCellValue(rH, "Price"));
                Int64 Tax = ProcessGeneral.GetSafeInt64(gvMaterialRequirement.GetRowCellValue(rH, "Tax"));
                Int64 Quantity = ProcessGeneral.GetSafeInt64(gvMaterialRequirement.GetRowCellValue(rH, "Quantity"));
                Decimal Amount = (Price * Tax * Quantity / 100) + Price * Quantity;
                gvMaterialRequirement.SetRowCellValue(rH, "Amount", Amount);
            }

        }

        public void ButtonClick()
        {
            btnInsertMatarial.Click += BtnInsertMainMaterial_Click;
            btnDeleteRow.Click += BtnDeleteMainMaterial_Click;
        }


        private void BtnDeleteMainMaterial_Click(object sender, EventArgs e)
        {
            int rH = gvMaterialRequirement.FocusedRowHandle;
            if (rH + 1 == gvMaterialRequirement.RowCount)//Kiểm tra row hiện tại row cuối không
            {
                //Kiểm tra phía trên còn row nào không
                if (rH > 0)
                {
                    gvMaterialRequirement.FocusedRowHandle = rH - 1;
                }
            }
            else //nếu ko thì thì foucs nhảy xuống dưới 1 row
            {
                gvMaterialRequirement.FocusedRowHandle = rH;
            }
            gvMaterialRequirement.DeleteRow(rH);
            gvMaterialRequirement.UpdateCurrentRow();
            gvMaterialRequirement.Focus();
        }

        private void BtnInsertMainMaterial_Click(object sender, EventArgs e)
        {
            InsertGridDeliveryTo();
        }
        #endregion

        #region Generate Event Search lookup Edit
        public void GenerateEventSearchlookupEdit()
        {
            slueProvider.EditValueChanged += slue_EditValueChanged;
            slueReceiver.EditValueChanged += slue_EditValueChanged;

            slueProvider.Popup += slue_Popup;
            slueReceiver.Popup += slue_Popup;
        }

        private void slue_Popup(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            GridView a = slue.Properties.View;
            a.BestFitColumns();
        }

        void slue_EditValueChanged(object sender, EventArgs e)
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
        public void SetReciverAndProvider()
        {
            _receiver = ProcessGeneral.GetSafeString(slueReceiver.EditValue);
            _provider = ProcessGeneral.GetSafeString(slueProvider.EditValue);
        }
        #endregion

        #region Load Data To Search lookup Edit
        public void LoadDataToSlue()
        {
            //slueReceiver
            slueReceiver.Properties.DataSource = _inf001.Excute("SELECT UserName Code, FullName [Name],b.DepartmentName FROM dbo.ListUser a INNER JOIN dbo.ListDepartment b ON a.DepartmentCode=b.DepartmentCode WHERE a.UserName NOT IN ('admin') AND a.IsActive =1 ORDER BY a.UserName ");
            slueReceiver.Properties.DisplayMember = "Code";
            slueReceiver.Properties.ValueMember = "Code";
            slueReceiver.Properties.NullText = null;

            //slueProvider
            slueProvider.Properties.DataSource = _inf001.Excute("SELECT UserName Code, FullName [Name],b.DepartmentName FROM dbo.ListUser a INNER JOIN dbo.ListDepartment b ON a.DepartmentCode=b.DepartmentCode WHERE a.UserName NOT IN ('admin') AND a.IsActive =1 AND a.DepartmentCode='WHO' ORDER BY a.UserName ");
            slueProvider.Properties.DisplayMember = "Code";
            slueProvider.Properties.ValueMember = "Code";
            slueProvider.Properties.NullText = null;

        }
        #endregion

        #region Display Data For Editing
        public void DisplayDataForEditing()
        {
            DataTable dtHeader = _inf006.sp_MaterialReceipt_Select(_pk);
            if (dtHeader.Rows.Count == 0) return;
            _pk = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["PK"]);
            txtPK.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PK"]);
            txtMaterialRequirementPK.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["MaterialRequirementPK"]);
            slueProvider.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Provider"]);
            slueReceiver.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Receiver"]);
            txtNote.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Note"]);
            txtCreatedBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreatedBy"]);
            deCreatedDate.EditValue = ProcessGeneral.GetSafeDatetimeOjectNull(dtHeader.Rows[0]["CreatedDate"]);
            txtUpdatedBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["UpdatedBy"]);
            deUpdatedDate.EditValue = ProcessGeneral.GetSafeDatetimeOjectNull(dtHeader.Rows[0]["UpdatedDate"]);
            slueStatus.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Status"]);
            dtApproveHistory = _inf_approval.sp_ApprovalHistory_Select(_functionInApprovalPK, _pk);
            LoadDataGridviewMaterialRequirement();
            if (_otp == "Edit")
            {
                LockControlWhenApproved();
                OpenColumnGridviewByApproved();
            }

        }
        void LockControlWhenApproved()
        {
            //Lock Header
            if (dtApproveHistory.Rows.Count > 0)
            {
                txtCreatedBy.Properties.ReadOnly = true;
                deCreatedDate.Properties.ReadOnly = true;
                txtUpdatedBy.Properties.ReadOnly = true;
                deUpdatedDate.Properties.ReadOnly = true;
                btnInsertMatarial.Enabled = false;
                btnDeleteRow.Enabled = false;
            }

        }
        void LockColumnGridview()
        {
            foreach (GridColumn column in gvMaterialRequirement.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
            }
        }
        void OpenColumnGridviewByPermission()
        {
            if (qPer.StrSpecialFunction.Contains("WM"))
            {
                gvMaterialRequirement.Columns["QuantityInStock"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
            }


            if (qPer.StrSpecialFunction.Contains("TS"))
            {
                gvMaterialRequirement.Columns["OrdinalNumber"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["QuantityOfRequset"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["RequestedDate"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["StockPK"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["ReceiveRequestDate"].OptionsColumn.AllowEdit = true;
            }

        }
        void OpenColumnGridviewByApproved()
        {
            int maxLevelApproved = 0;
            if (dtApproveHistory.Rows.Count > 0)
            {
                maxLevelApproved = dtApproveHistory.AsEnumerable().Max(row => row.Field<int>("Level"));
            }

            //Nếu không có quyền Kho hoặc không phải trạng thái bằng 2 thì khóa 2 ô Số lượng trong kho và Ghi chú lại
            if (!qPer.StrSpecialFunction.Contains("WM") || maxLevelApproved != 2)
            {
                gvMaterialRequirement.Columns["QuantityInStock"].OptionsColumn.AllowEdit = false;
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = false;
            }

        }
        #endregion

        #region Display Data For Adding
        public void DisplayDataForAdding()
        {
            txtCreatedBy.Text = DeclareSystem.SysUserName;
            deCreatedDate.EditValue = DateTime.Today;
            LoadDataGridviewMaterialRequirement();

        }

        #endregion

        #region Save
        public void Save()
        {
            #region Check if approved
            DataTable dt = _inf_approval.sp_ApprovalHistory_GetUserApproved(_functionInApprovalPK, _pk);

            if (dt.Rows.Count > 0)
            {
                XtraMessageBox.Show("Không thể lưu phiếu đã được duyệt!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion

            #region Get Info
            _cls006.PK = _pk;
            _cls006.MaterialRequirementPK = ProcessGeneral.GetSafeInt(txtMaterialRequirementPK.EditValue);
            _cls006.Provider = ProcessGeneral.GetSafeString(slueProvider.EditValue);
            _cls006.Receiver = ProcessGeneral.GetSafeString(slueReceiver.EditValue);
            _cls006.ReceivedDate = ProcessGeneral.GetSafeDatetimeDBNull(deReceivedDate.EditValue);
            _cls006.CreatedBy = DeclareSystem.SysUserName;
            _cls006.CreatedDate = DateTime.Now;
            _cls006.UpdatedBy = DeclareSystem.SysUserName;
            _cls006.UpdatedDate = DateTime.Now;
            _cls006.Status = ProcessGeneral.GetSafeInt(slueStatus.EditValue);


            #endregion

            #region Save and show message
            //Update data
            DataTable dtSaveResult = _inf006.sp_MaterialReceipt_Update(_cls006);
            //Get result info
            string msg = ProcessGeneral.GetSafeString(dtSaveResult.Rows[0]["ErrMsg"]);
            //Set new ID
            if (_cls006.PK == -1)
            {
                //Update new ID
                _cls006.PK = ProcessGeneral.GetSafeInt(dtSaveResult.Rows[0]["IDENTITY"]);
                _pk = _cls006.PK;
            }
            //Save Datail
            SaveDetail();

            XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


            DisplayDataForEditing();
            #endregion
        }
        public void SaveDetail()
        {
            DataTable dt = gcMaterialRequirement.DataSource as DataTable;
            if (dt == null) return;
            dt.AcceptChanges();

            dt.Select().ToList<DataRow>().ForEach(r => r["MaterialReceiptPK"] = _pk);//Update FK thành PK của Product Code
            DataTable qdt = new DataTable();
            qdt.Columns.Add("PK", typeof(Int64));
            qdt.Columns.Add("MaterialReceiptPK", typeof(Int64));
            qdt.Columns.Add("StockPK", typeof(Int32));
            qdt.Columns.Add("Quantity", typeof(String));
            qdt.Columns.Add("Note", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drAdd = qdt.NewRow();
                drAdd["PK"] = ProcessGeneral.GetSafeInt64(dr["PK"]);
                drAdd["MaterialReceiptPK"] = ProcessGeneral.GetSafeInt64(dr["MaterialReceiptPK"]);
                drAdd["StockPK"] = ProcessGeneral.GetSafeInt64(dr["StockPK"]);
                drAdd["Quantity"] = ProcessGeneral.GetSafeInt(dr["QuantityOfRequset"]);
                drAdd["Note"] = ProcessGeneral.GetSafeString(dr["Note"]);
                qdt.Rows.Add(drAdd);
            }
            _inf005.sp_MaterialRequirementDetail_Update(_pk, qdt);
        }

        #endregion

        #region LoadDataGridview
        public void LoadDataGridviewMaterialRequirement()
        {
            DataTable dtMaterialRequirementDetail = _inf005.sp_MaterialRequirementDetail_Select(_pk);
            if (_otp == "Copy")
            {
                dtMaterialRequirementDetail.Select().ToList<DataRow>().ForEach(r => r["PK"] = -1);//Nếu là copy thì Update PK thành 1
            }
            gcMaterialRequirement.DataSource = dtMaterialRequirementDetail;
            gvMaterialRequirement.BestFitColumns();
        }

        #endregion

        #region ClearForm
        public void ClearForm()
        {
        }
        public void SetDefaultInfo()
        {
        }
        #endregion

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
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "StockPK", StockPK);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "StockCode", StockCode);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "StockName", StockName);
                gvMaterialRequirement.FocusedColumn = gvMaterialRequirement.Columns["QuantityOfRequset"];
                gvMaterialRequirement.ClearSelection();
                gvMaterialRequirement.SelectCell(gvMaterialRequirement.FocusedRowHandle, gvMaterialRequirement.FocusedColumn);
                gvMaterialRequirement.Focus();

            }
            else if (textboxName == "GcDeliveryStatus_KeyDown")
            {
                int DeliveryPK = ProcessGeneral.GetSafeInt(lDr[0]["PK"].ToString());
                string DeliveryName = ProcessGeneral.GetSafeString(lDr[0]["Name"].ToString());
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "DeliveryStatus", DeliveryPK);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "DeliveryStatusName", DeliveryName);
            }
            else if (textboxName == "MaterialRequirement")
            {
                int MaterialRequirementPK = ProcessGeneral.GetSafeInt(lDr[0]["PK"].ToString());
                DataTable dt = _inf007.sp_MaterialReceiptDetail_SelectByMaterialRequirementPK(MaterialRequirementPK);
                gcMaterialRequirement.DataSource = dt;
            }
            gvMaterialRequirement.BestFitColumns();
        }

        #endregion "KeyDown"

        #region InitColumGridview
        private void InitColumGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "MaterialReceiptPK", "MaterialReceiptPK", -1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "StockPK", "StockPK", -1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Mã vật tư", "StockCode", 1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Tên vật tư", "StockName", 2);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Số lượng", "Quantity", 2);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Đơn vị", "Unit", 3);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Ghi chú", "Note", 11);

            gvMaterialRequirement.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvMaterialRequirement.Columns["Quantity"].DisplayFormat.FormatString = "N0";

            InitFooterCell(gvMaterialRequirement, "Quantity");
        }

        void InitFooterCell(GridView gvMain, string ColumnName)
        {
            GridColumn column = gvMain.Columns[ColumnName];
            column.SummaryItem.SummaryType = SummaryItemType.Sum;
            column.SummaryItem.DisplayFormat = "{0:0,0}";
        }
        #endregion

        #region CheckRight
        private void DisplayConfirmQuotationAdding()
        {
            btnCreaterConfirm.Visible = false;
            btnReciverConfirm.Visible = false;
            btnReciverReject.Visible = false;
            btnWMApprove.Visible = false;
            btnWMReject.Visible = false;

            lblReciverConfirm.Visible = false;
            lblCreaterConfirm.Visible = false;
            lblWMConfirm.Visible = false;
        }
        private void DisplayConfirm()
        {
            CheckStatus();
            CheckRight();
        }
        void CheckRight()
        {
            if (_receiver!=DeclareSystem.SysUserName)
            {
                btnCreaterConfirm.Visible = false;
            }

            if (_provider != DeclareSystem.SysUserName)
            {
                btnReciverConfirm.Visible = false;
                btnReciverReject.Visible = false;
            }

            if (!qPer.StrSpecialFunction.Contains("WM"))
            {
                btnWMApprove.Visible = false;
                btnWMReject.Visible = false;
            }
        }
        void CheckStatus()
        {
            DataTable dtApproveHistory = _inf_approval.sp_ApprovalHistory_Select(_functionInApprovalPK, _pk);
            CheckApproveDetail(dtApproveHistory, 1);
            CheckApproveDetail(dtApproveHistory, 2);
            CheckApproveDetail(dtApproveHistory, 3);
            CheckApproveDetail(dtApproveHistory, 4);
            CheckApproveDetail(dtApproveHistory, 5);
            CheckApproveDetail(dtApproveHistory, 6);
            CheckApproveDetail(dtApproveHistory, 7);
            CheckApproveDetail(dtApproveHistory, 8);
            DisplayControlConfirm(_listControlApprove, 3, _status, _isRejected);
        }
        void CheckApproveDetail(DataTable dtApproveHistory, Int32 Level)
        {

            // Kiểm tra xem đã có những user nào approve bằng cách giá trị có tồn tại trong bảng lịch sử phê duyệt hay không bằng LINQ
            bool valueExists = dtApproveHistory.AsEnumerable().Any(row => row.Field<int>("Level") == Level);
            if (valueExists)
            {
                DataRow rowWithCondition = dtApproveHistory.AsEnumerable().FirstOrDefault(row => row.Field<int>("Level") == Level);
                _status = Level;
                DateTime ApprovedDate = ProcessGeneral.GetSafeDatetimeNullable(rowWithCondition["ApprovedDate"]);
                string ApprovedUse = ProcessGeneral.GetSafeString(rowWithCondition["UserName"]);
                int ActionID = ProcessGeneral.GetSafeInt(rowWithCondition["Status"]);
                GetTextForLabelConfirm(_listControlApprove, Level, ActionID, ApprovedUse, ApprovedDate);
            }

        }
        private void GetTextForLabelConfirm(List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControl, int Status, int ActionID, string userName, DateTime dateTime)
        {
            string Action = ActionID == 1 ? "Confirmed" : "Rejected";
            if (ActionID == 0)
            {
                _isRejected = true;
                _listControl[Status - 1].Item1.Appearance.ForeColor = Color.Red;
                _listControl[Status - 1].Item1.Appearance.Font = new System.Drawing.Font(_listControl[Status - 1].Item1.Appearance.Font, System.Drawing.FontStyle.Bold);
            }
            _listControl[Status - 1].Item1.Text = Action + " by " + userName + "\n at " + dateTime.ToString("dd/MM/yyyy hh:mm:ss");

        }
        #region DisplayControlConfirm
        private void DisplayControlConfirm(List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControl, int numberOfConfirm, int Status, bool Approved = false)
        {
            for (int controlIndex = 0; controlIndex < numberOfConfirm; controlIndex++)
            {
                if (Status > controlIndex)//Nếu trạng thái lớn hơn vị trí của control thì Label hiển thị thông báo hiện lên
                {
                    _listControl[controlIndex].Item1.Visible = true;
                }

                else
                {
                    _listControl[controlIndex].Item1.Visible = false;
                }

                if (Status == controlIndex && !_isRejected)//Nếu trạng thái bằng vị trí của control và không bị reject thì Button hiển thị nút xác nhận lên
                {
                    _listControl[controlIndex].Item2.Visible = true;
                    _listControl[controlIndex].Item3.Visible = true;
                }
                else
                {
                    _listControl[controlIndex].Item2.Visible = false;
                    _listControl[controlIndex].Item3.Visible = false;
                }
            }
        }
        #endregion
        #endregion

        #region LoadListControApprove
        private void LoadListControApprove()
        {
            _listControlApprove = new List<Tuple<LabelControl, SimpleButton, SimpleButton>>
            {
                Tuple.Create(lblCreaterConfirm,btnCreaterConfirm,btnCreaterConfirm),
                Tuple.Create(lblReciverConfirm,btnReciverConfirm,btnReciverReject),
                Tuple.Create(lblWMConfirm,btnWMApprove,btnWMReject),
            };
        }
        #endregion

        #region Generate Event Button Click
        private void GenerateEventButton()
        {
            btnCreaterConfirm.Click += BtnTSConfirm_Click;
            btnReciverConfirm.Click += BtnTMApprove_Click;
            btnReciverReject.Click += BtnTMReject_Click;
            btnWMApprove.Click += BtnWMApprove_Click;
            btnWMReject.Click += BtnWMReject_Click;

        }
        private void BtnWMReject_Click(object sender, EventArgs e)
        {
            UpdateStatus(0, 3, "");
        }

        private void BtnWMApprove_Click(object sender, EventArgs e)
        {
            UpdateStatus(1, 3, "");
        }

        private void BtnTMReject_Click(object sender, EventArgs e)
        {
            UpdateStatus(0, 2, "");
        }

        private void BtnTMApprove_Click(object sender, EventArgs e)
        {
            UpdateStatus(1, 2, "");
        }

        private void BtnTSConfirm_Click(object sender, EventArgs e)
        {
            UpdateStatus(1, 1, "");
        }
        private void UpdateStatus(Int32 Status, Int32 _currentLevel, string Note)
        {
            Cls_ApprovalHistory cls = new Cls_ApprovalHistory();
            cls.FunctionInApprovalPK = _functionInApprovalPK;
            cls.Level = _currentLevel;
            cls.ItemPKInFunction = _pk;
            cls.UserName = DeclareSystem.SysUserName;
            cls.Status = Status;
            cls.ApprovedDate = DateTime.Now;
            cls.Note = Note;
            DataTable dt = _inf_approval.sp_ApprovalHistory_Update(cls);
            if (dt.Rows.Count > 0)
            {
                string msg = ProcessGeneral.GetSafeString(dt.Rows[0]["ErrMsg"]);
                XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayConfirm();
            }
            //Gửi mail 
            if (Status == 1)// Gửi mail yêu cầu duyệt ước tiếp theo cho cấp cao hơn
            {
                //Lấy email cấp cao hơn
                dt = _inf_approval.sp_LevelFunctionApproval_GetUserApproval(_functionInApprovalPK, _menuCode, _currentLevel);
                foreach (DataRow dr in dt.Rows)
                {
                    SendMailRequest(ProcessGeneral.GetSafeString(dr["Email"]));
                }
            }
            // Gửi mail thống báo tình trạng cho các cấp thấp hơn
            //Lấy email cấp thấp hơn

            dt = _inf_approval.sp_ApprovalHistory_GetUserApproved(_functionInApprovalPK, _pk);
            foreach (DataRow dr in dt.Rows)
            {
                SendMailNotice(ProcessGeneral.GetSafeString(dr["Email"]), Status);
            }
        }
        #endregion

        #region SendEmail
        public void SendMailRequest(string EmailTo)
        {
            string title = "Yêu cầu phê duyệt Yêu cầu vật tư số  " + _pk;
            string body = "Vui lòng kiểm tra để phê duyệt hoặc từ chối Phiếu Yêu cầu vật tư số  " + _pk + " tạo bởi " + txtCreatedBy.EditValue.ToString() + ". Xem chi tiết tại phần mềm VTStek, mục Yêu cầu vật tư.";
            //Email.Send(EmailTo, title, body);
        }
        public void SendMailNotice(string EmailTo, int status)
        {
            string actionName = status == 1 ? "được duyệt" : "bị từ chối";
            string title = "Thông báo tình trạng Phiếu Đề nghị mua hàng số " + _pk + " đã " + actionName + " bởi " + DeclareSystem.SysUserName;

            //Email.Send(EmailTo, title, title);
        }
        #endregion

        #region Print
        public void Print()
        {

            //DataTable dt = _inf005.sp_MaterialRequirementDetail_Select(_pk);
            //ReportPrintTool printTool = new ReportPrintTool(new Rpt006MaterialReceipt(dt));
            //printTool.ShowPreview();
            //// Xuất báo cáo
            //printTool.ShowPreview();
        }
        #endregion
        #region Print
        public void Generate()
        {
            DataTable dt = _inf004.sp_MaterialRequirement_Select(-1);
            F4_Keydown(dt,"Danh sách yêu cầu vật tư", "MaterialRequirement", new[] { "PK" });
        }
        #endregion
    }
}