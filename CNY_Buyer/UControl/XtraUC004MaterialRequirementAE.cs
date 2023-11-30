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
using DevExpress.XtraCharts.Native;

namespace CNY_Buyer.UControl
{
    public partial class XtraUC004MaterialRequirementAE : UserControl
    {
        #region Properties
        Inf_004MaterialRequirement _inf004 = new Inf_004MaterialRequirement();
        Cls_004MaterialRequirement _cls004 = new Cls_004MaterialRequirement();
        Inf_005MaterialRequirementDetail _inf005 = new Inf_005MaterialRequirementDetail();
        Inf_ApprovalHistory _inf_approval = new Inf_ApprovalHistory();
        List<Tuple<Control, int>> _list = new List<Tuple<Control, int>>();
        Int64 _pk;
        Int32 _status = 0;
        int _signPerRow = 6;
        bool _isRejected;
        string _otp;
        Int32 _functionInApprovalPK = 2;
        readonly String _menuCode = "MN00381";
        OpenFileDialog op = new OpenFileDialog();
        private bool _performEditValueChangeEvent = false;
        List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControlApprove = new List<Tuple<LabelControl, SimpleButton, SimpleButton>>();
        private WaitDialogForm dlg;
        PermissionFormInfo qPer;
        DataTable dtApproveHistory;
        bool _isChangeQuantity = false;
        int numberOfPerson;
        Confirmation _conf;

        #endregion

        #region Contractor
        public XtraUC004MaterialRequirementAE(Int64 pk, string otp)
        {
            InitializeComponent();
            _pk = pk;
            _otp = otp;
            qPer = ProcessGeneral.GetPermissionByFormCode("Frm004MaterialRequirement");
            Format();
            GenerateEvent();
            LoadData();
        }
        #endregion

        #region Format
        public void Format()
        {
            FormatHeader();
            FormatGridview(gcMaterialRequirement, gvMaterialRequirement);
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

        #region FormatHeader
        public void FormatHeader()
        {
            if (_otp == "Add" || _otp == "Copy")
            {

            }
            else
            {
                LockHeader();
            }
        }
        void LockHeader()
        {

        }

        #endregion

        #region FormatGridview

        private void FormatGridview(GridControl gcMain, GridView gvMain)
        {
            FormatGridView.InitAE(gcMain, gvMain);
            InitColumGridview();
            LockColumnGridview(gvMain);
            GenerateEventGridview();
            ButtonOnGridViewClick();
        }

        #region InitColumGridview
        private void InitColumGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "PK", "PK", -1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "UnitPK", "UnitPK", -1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "MaterialRequirementPK", "MaterialRequirementPK", -1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "StockPK", "StockPK", -1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "STT", "OrdinalNumber", 1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Tên hạng mục", "ProjectItem", 1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Tên cụm", "Component", 1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Mã vật tư (F4)", "StockCode", 1);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Tên vật tư (F4)", "StockName", 2);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Đơn vị tính (F4)", "UnitName", 2);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Số lượng yêu cầu", "QuantityOfRequset", 2);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Số lượng trong kho", "QuantityInStock", 2);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Chứng chỉ", "Certificate", 5);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Xuất xứ", "Origin", 5);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Hãng sản xuất", "Manufacturer", 5);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Ngày yêu cầu", "RequestedDate", 5);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Ngày yêu cầu nhận VT", "ReceiveRequestDate", 7);
            FormatGridView.CreateColumnOnGridview(gvMaterialRequirement, "Ghi chú", "Note", 11);



            gvMaterialRequirement.Columns["QuantityOfRequset"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvMaterialRequirement.Columns["QuantityOfRequset"].DisplayFormat.FormatString = "N0";
            gvMaterialRequirement.Columns["QuantityInStock"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvMaterialRequirement.Columns["QuantityInStock"].DisplayFormat.FormatString = "N0";
            gvMaterialRequirement.Columns["RequestedDate"].DisplayFormat.FormatType = FormatType.DateTime;
            gvMaterialRequirement.Columns["RequestedDate"].DisplayFormat.FormatString = "dd-MM-yyyy";
            gvMaterialRequirement.Columns["ReceiveRequestDate"].DisplayFormat.FormatType = FormatType.DateTime;
            gvMaterialRequirement.Columns["ReceiveRequestDate"].DisplayFormat.FormatString = "dd-MM-yyyy";


            InitFooterCell(gvMaterialRequirement, "QuantityOfRequset");
            InitFooterCell(gvMaterialRequirement, "QuantityInStock");
        }

        void InitFooterCell(GridView gvMain, string ColumnName)
        {
            GridColumn column = gvMain.Columns[ColumnName];
            column.SummaryItem.SummaryType = SummaryItemType.Sum;
            column.SummaryItem.DisplayFormat = "{0:0,0}";
        }
        #endregion

        #region Gridview Format Event 

        void LockColumnGridview(GridView gvMain)
        {
            foreach (GridColumn column in gvMain.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
            }
        }
        public void GenerateEventGridview()
        {
            gvMaterialRequirement.RowCellStyle += gvMaterialRequirement_RowCellStyle;
            gvMaterialRequirement.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;

            gvMaterialRequirement.KeyDown += gvDeliveryList_KeyDown;
            gvMaterialRequirement.RowCellStyle += gvDeliveryList_RowCellStyle;
            gvMaterialRequirement.ShowingEditor += GvMain_ShowingEditor;
            gvMaterialRequirement.CellValueChanged += GvMaterialRequirement_CellValueChanged;
            gvMaterialRequirement.CustomDrawFooterCell += GvMaterialRequirement_CustomDrawFooterCell;
        }
        private void gvMaterialRequirement_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;

            if (gv.IsRowSelected(e.RowHandle))
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
        private void GvMaterialRequirement_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
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
        #endregion

        #region Gridview Handle Event


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
                        break;
                        InsertGridDeliveryTo();
                    }
                case Keys.F4:
                    {
                        switch (gv.FocusedColumn.FieldName)
                        {
                            case "StockCode":
                            case "StockName":
                                F4_Keydown(_inf004.Excute("SELECT a.PK,a.Name,a.Code,b.PK UnitPK, b.NAME UnitName FROM dbo.Stock a INNER JOIN dbo.StockUnit b ON a.Unit =b.PK"),
                                    "List of Stock", "GcDeliveryMethod_KeyDown", new[] { "PK" });
                                break;
                            case "UnitName":
                                F4_Keydown(_inf004.Excute("SELECT PK, [Name] FROM dbo.StockUnit"),
                                    "List of Unit", "GcDeliveryStatus_KeyDown", new[] { "PK" });
                                break;
                        }
                        break;
                    }
                case Keys.F6:
                    {
                        switch (gv.FocusedColumn.FieldName)
                        {
                            case "StockName":
                            case "StockCode":
                                using (var f2 = new FrmInput())
                                {
                                    string _StockName = "";
                                    f2.Text = @"Nhập tên vật tư";
                                    f2.Size = new Size(340, f2.Height);
                                    f2.CommandPromt = "Nhập tên vật tư :";
                                    f2.SetWidthLable = 100;
                                    f2.MaskEdit = FunctionFormatModule.StrFormatFactorDecimal(true, false);
                                    f2.UseSystemPasswordChar = false;
                                    f2.CharacterCase = CharacterCasing.Normal;
                                    f2.OnGetValue += (s, f) =>
                                    {
                                        _StockName = ProcessGeneral.GetSafeString(f.Value);
                                        gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "StockName", _StockName);
                                    };

                                    f2.ShowDialog();
                                }
                                break;
                        }
                        break;
                    }
            }
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
        private void GvMaterialRequirement_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //if (!_performEditValueChangeEvent) return;
            var gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            
            Int32 rH = e.RowHandle;
            if (fieldName == "UnitName" || fieldName == "RequestedDate" || fieldName == "QuantityOfRequset" || fieldName == "ReceiveRequestDate"|| fieldName == "StockName" || fieldName == "Component" || fieldName == "ProjectItem" )
            {
                _isChangeQuantity = true;
            }
            else if (fieldName == "Note")
            {
            }

        }

        public void ButtonOnGridViewClick()
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
        #endregion

        #endregion

        #region LoadData
        public void LoadData()
        {
            LoadDataToSlue();

            
            

            //_conf.DisplayConfirmQuotationAdding();
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
        }
        #region Load Data To Search lookup Edit
        public void LoadDataToSlue()
        {
            //slueCriticalLevel
            DataTable dtCriticalLevel = _inf004.Excute("SELECT PK,Name FROM dbo.Common_Masterdata WHERE Module='CriticalLevel'");
            ProcessGeneral.LoadSearchLookup(slueCriticalLevel, dtCriticalLevel, "Name", "PK", DevExpress.XtraEditors.Controls.BestFitMode.BestFit, "PK");

            //slueRequester
            DataTable dtRequester = _inf004.Excute("SELECT UserName Code, FullName [Name],b.DepartmentName FROM dbo.ListUser a INNER JOIN dbo.ListDepartment b ON a.DepartmentCode=b.DepartmentCode WHERE a.UserName NOT IN ('admin') AND a.IsActive =1 ORDER BY a.UserName ");
            ProcessGeneral.LoadSearchLookup(slueRequester, dtRequester, "Name", "Code", DevExpress.XtraEditors.Controls.BestFitMode.BestFit);

            //slueProjectCode
            slueProjectCode.Properties.DataSource = _inf004.Excute("SELECT Project_Code Code, Project_Name Name FROM dbo.Project ");
            slueProjectCode.Properties.DisplayMember = "Code";
            slueProjectCode.Properties.ValueMember = "Code";
            slueProjectCode.Properties.NullText = null;

            //slueStatus
            slueStatus.Properties.DataSource = _inf004.Excute("SELECT Code, Name FROM dbo.Common_Masterdata WHERE Module = 'ApproveStatus'");
            slueStatus.Properties.DisplayMember = "Name";
            slueStatus.Properties.ValueMember = "Code";
            slueStatus.Properties.NullText = null;

            //slueRequestType
            DataTable dtType = _inf004.Excute("SELECT PK,Note as Name FROM dbo.FunctionInApproval WHERE Formname='MaterialRequirement';");
            string[] paraHideColumnslueRequestType = { "PK",  };
            ProcessGeneral.LoadSearchLookup(slueRequestType, dtType, "Name", "PK", DevExpress.XtraEditors.Controls.BestFitMode.BestFit, paraHideColumnslueRequestType);
        }
        #endregion

        #region Display Data For Editing
        public void DisplayDataForEditing()
        {
            DataTable dtHeader = _inf004.sp_MaterialRequirement_Select(_pk);
            if (dtHeader.Rows.Count == 0) return;
            _pk = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["PK"]);
            slueProjectCode.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ProjectCode"]);
            txtProjectCodeDesc.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["ProjectName"]);
            slueRequester.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Requester"]);
            txtRequestTimes.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["RequestTimes"]);
            txtPK.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PK"]);
            txtCreatedBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreatedBy"]);
            deCreatedDate.EditValue = ProcessGeneral.GetSafeDatetimeOjectNull(dtHeader.Rows[0]["CreatedDate"]);
            txtUpdatedBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["UpdatedBy"]);
            deUpdatedDate.EditValue = ProcessGeneral.GetSafeDatetimeOjectNull(dtHeader.Rows[0]["UpdatedDate"]);
            slueCriticalLevel.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CriticalLevel"]);
            slueStatus.EditValue = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["Status"]);
            slueRequestType.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["RequestType"]);
            dtApproveHistory = _inf_approval.sp_ApprovalHistory_Select(_functionInApprovalPK, _pk);
            LoadDataGridviewMaterialRequirement();
            if (_otp == "Edit")
            {
                LockControlWhenApproved();
                OpenColumnGridviewByApproved();
            }
            if ( _pk > 0)
            {
                _functionInApprovalPK = ProcessGeneral.GetSafeInt(slueRequestType.EditValue);
                _conf = new Confirmation(_functionInApprovalPK, _pk, _signPerRow, groupControl3, qPer, _menuCode);
                _conf.RemoveControlConfirm();
                _conf.GenerateControlConfirm();
                _conf.DisplayConfirm();
            }
        }
        void LockControlWhenApproved()
        {
            //Lock Header
            if (dtApproveHistory.Rows.Count > 0)
            {
                slueProjectCode.Properties.ReadOnly = true;
                txtProjectCodeDesc.Properties.ReadOnly = true;
                slueRequester.Properties.ReadOnly = true;
                txtCreatedBy.Properties.ReadOnly = true;
                deCreatedDate.Properties.ReadOnly = true;
                txtUpdatedBy.Properties.ReadOnly = true;
                deUpdatedDate.Properties.ReadOnly = true;
                slueCriticalLevel.Properties.ReadOnly = true;
                slueRequestType.Properties.ReadOnly = true;
                btnInsertMatarial.Enabled = false;
                btnDeleteRow.Enabled = false;
            }

        }

        void OpenColumnGridviewByPermission()
        {
            string _userName = ProcessGeneral.GetSafeString(txtCreatedBy.EditValue);
            if (qPer.StrSpecialFunction.Contains("TS") && _userName == DeclareSystem.SysUserName)
            {
                gvMaterialRequirement.Columns["QuantityOfRequset"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["RequestedDate"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["StockPK"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["ReceiveRequestDate"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["ProjectItem"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["Component"].OptionsColumn.AllowEdit = true;

            }
            if (qPer.StrSpecialFunction.Contains("TM"))
            {
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
            }
            if (qPer.StrSpecialFunction.Contains("WM"))
            {
                gvMaterialRequirement.Columns["QuantityInStock"].OptionsColumn.AllowEdit = true;
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
            }
            if (qPer.StrSpecialFunction.Contains("TD"))
            {
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
            }
            if (qPer.StrSpecialFunction.Contains("PS"))
            {
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
            }
            if (qPer.StrSpecialFunction.Contains("PM"))
            {
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
            }
            if (qPer.StrSpecialFunction.Contains("AS"))
            {
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
            }
            if (qPer.StrSpecialFunction.Contains("CEO"))
            {
                gvMaterialRequirement.Columns["Note"].OptionsColumn.AllowEdit = true;
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
            }

        }
        #endregion

        #region Display Data For Adding
        public void DisplayDataForAdding()
        {
            txtCreatedBy.Text = DeclareSystem.SysUserName;
            deCreatedDate.EditValue = DateTime.Today;
            if (slueCriticalLevel.Properties.DataSource != null)
            {
                DataTable dtSource = slueCriticalLevel.Properties.DataSource as DataTable;
                slueCriticalLevel.EditValue = dtSource.Rows[0]["PK"];
            }
            LoadDataGridviewMaterialRequirement();

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

        #region LoadListControApprove
       
        #endregion
        #endregion

        #region GenerateEvent
        public void GenerateEvent()
        {
            GenerateEventSearchlookupEdit();

        }

        #region Generate Event Search lookup Edit
        public void GenerateEventSearchlookupEdit()
        {
            slueProjectCode.EditValueChanged += slue_EditValueChanged;
            slueRequestType.EditValueChanged += slueUpdatNeedSave_EditValueChanged;
            slueProjectCode.EditValueChanged += slueUpdatNeedSave_EditValueChanged;
            slueRequester.EditValueChanged += slueUpdatNeedSave_EditValueChanged;
            slueCriticalLevel.Popup += slue_Popup;
            slueRequester.Popup += slue_Popup;
            slueProjectCode.Popup += slue_Popup;
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
        void slueUpdatNeedSave_EditValueChanged(object sender, EventArgs e)
        {
            if (_conf == null)
                return;
            _conf.needSave = true;
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
        #endregion

        #region Generate Event Button Click

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
                _conf.DisplayConfirm();
            }
            //Cập nhật trạng thái
            int _status = 2;
            if(Status==0)
            {
                _status = -1;
            }
            else
            {
                if(_currentLevel==  numberOfPerson)
                    _status = 1;
            }
            dt = _inf004.sp_MaterialRequirement_UpdateStatus(_pk, _status);

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
            DisplayDataForEditing();
        }
        #endregion

        #endregion

        #region Handle Event


        #region Save
        public void Save()
        {
            #region Check if approved
            if (_isChangeQuantity)
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
                            dt = _inf004.sp_MaterialRequirement_UpdateStatus(_pk, _status);
                            _isChangeQuantity = false;
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

            SaveHeader();
            SaveDetail();

            XtraMessageBox.Show("Lưu phiếu thành công", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _conf.needSave = false;
            HideSignature();
        }
        public void SaveHeader()
        {
            #region Get Info
            _cls004.PK = _pk;
            _cls004.ProjectCode = ProcessGeneral.GetSafeString(slueProjectCode.EditValue);
            _cls004.ProjectName = ProcessGeneral.GetSafeString(txtProjectCodeDesc.EditValue);
            _cls004.Requester = ProcessGeneral.GetSafeString(slueRequester.EditValue);
            _cls004.CriticalLevel = ProcessGeneral.GetSafeInt(slueCriticalLevel.EditValue);
            _cls004.RequestType = ProcessGeneral.GetSafeInt(slueRequestType.EditValue);
            _cls004.CreatedBy = DeclareSystem.SysUserName;
            _cls004.CreatedDate = DateTime.Now;
            _cls004.UpdatedBy = DeclareSystem.SysUserName;
            _cls004.UpdatedDate = DateTime.Now;
            _cls004.Status = _status;

            #endregion

            #region Save 
            //Update data
            DataTable dtSaveResult = _inf004.sp_MaterialRequirement_Update(_cls004);
            //Get result info
            string msg = ProcessGeneral.GetSafeString(dtSaveResult.Rows[0]["ErrMsg"]);
            //Set new ID
            if (_cls004.PK == -1)
            {
                //Update new ID
                _cls004.PK = ProcessGeneral.GetSafeInt(dtSaveResult.Rows[0]["IDENTITY"]);
                _pk = _cls004.PK;
            }
            _otp = "Edit";
            #endregion
        }
        public void SaveDetail()
        {
            DataTable dt = gcMaterialRequirement.DataSource as DataTable;
            if (dt == null) return;
            dt.AcceptChanges();

            dt.Select().ToList<DataRow>().ForEach(r => r["MaterialRequirementPK"] = _pk);//Update FK thành PK của Product Code
            DataTable qdt = new DataTable();
            qdt.Columns.Add("PK", typeof(Int64));
            qdt.Columns.Add("MaterialRequirementPK", typeof(Int64));
            qdt.Columns.Add("OrdinalNumber", typeof(Int32));
            qdt.Columns.Add("Component", typeof(String));
            qdt.Columns.Add("StockPK", typeof(Int64));
            qdt.Columns.Add("QuantityOfRequset", typeof(Int32));
            qdt.Columns.Add("RequestedDate", typeof(DateTime));
            qdt.Columns.Add("ReceiveRequestDate", typeof(DateTime));
            qdt.Columns.Add("QuantityInStock", typeof(Int32));
            qdt.Columns.Add("Note", typeof(string));
            qdt.Columns.Add("Unit", typeof(Int32));
            qdt.Columns.Add("ProjectItem", typeof(string));
            qdt.Columns.Add("NameWithoutCode", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drAdd = qdt.NewRow();
                drAdd["PK"] = ProcessGeneral.GetSafeInt64(dr["PK"]);
                drAdd["MaterialRequirementPK"] = ProcessGeneral.GetSafeInt64(dr["MaterialRequirementPK"]);
                drAdd["OrdinalNumber"] = ProcessGeneral.GetSafeInt(dr["OrdinalNumber"]);
                drAdd["Component"] = ProcessGeneral.GetSafeString(dr["Component"]);
                drAdd["StockPK"] = ProcessGeneral.GetSafeInt64(dr["StockPK"]);
                drAdd["QuantityOfRequset"] = ProcessGeneral.GetSafeInt(dr["QuantityOfRequset"]);
                drAdd["RequestedDate"] = ProcessGeneral.GetSafeDatetimeDBNull(dr["RequestedDate"]);
                drAdd["ReceiveRequestDate"] = ProcessGeneral.GetSafeDatetimeDBNull(dr["ReceiveRequestDate"]);
                drAdd["QuantityInStock"] = ProcessGeneral.GetSafeInt(dr["QuantityInStock"]);
                drAdd["Note"] = ProcessGeneral.GetSafeString(dr["Note"]);
                drAdd["Unit"] = ProcessGeneral.GetSafeInt(dr["UnitPK"]);
                drAdd["ProjectItem"] = ProcessGeneral.GetSafeString(dr["ProjectItem"]);
                drAdd["NameWithoutCode"] = GetNameWithoutCode(ProcessGeneral.GetSafeInt64(dr["StockPK"]), ProcessGeneral.GetSafeString(dr["StockName"]));
                qdt.Rows.Add(drAdd);
            }
            _inf005.sp_MaterialRequirementDetail_Update(_pk, qdt);
        }
        public string GetNameWithoutCode(Int64 StockPK, string StockName)
        {
            if (StockPK == 0)
                return StockName;
            return "";

        }
        #endregion

        #region KeyDown

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
                int UnitPK = ProcessGeneral.GetSafeInt(lDr[0]["UnitPK"].ToString());
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "StockPK", StockPK);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "StockCode", StockCode);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "StockName", StockName);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "UnitName", UnitName);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "UnitPK", UnitPK);
                gvMaterialRequirement.FocusedColumn = gvMaterialRequirement.Columns["QuantityOfRequset"];
                gvMaterialRequirement.ClearSelection();
                gvMaterialRequirement.SelectCell(gvMaterialRequirement.FocusedRowHandle, gvMaterialRequirement.FocusedColumn);
                gvMaterialRequirement.Focus();

            }
            else if (textboxName == "GcDeliveryStatus_KeyDown")
            {
                int UnitPK = ProcessGeneral.GetSafeInt(lDr[0]["PK"].ToString());
                string UnitName = ProcessGeneral.GetSafeString(lDr[0]["Name"].ToString());
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "UnitPK", UnitPK);
                gvMaterialRequirement.SetRowCellValue(gvMaterialRequirement.FocusedRowHandle, "UnitName", UnitName);

            }
            gvMaterialRequirement.BestFitColumns();
        }

        #endregion "KeyDown"

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
            //ReportPrintTool printTool = new ReportPrintTool(new Rpt004MaterialRequirement(dt));
            //printTool.ShowPreview();
            //// Xuất báo cáo
            //printTool.ShowPreview();
        }
        #endregion

        #endregion
    }
}