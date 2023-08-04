using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using CNY_WH.Common;
using CNY_WH.Info;
using CNY_WH.Class;
using CNY_BaseSys.Common;
using CNY_BaseSys;
using System.Drawing.Drawing2D;
using System.Globalization;
using CNY_BaseSys.Class;
using CNY_BaseSys.UControl;
using CNY_BaseSys.WForm;
using CNY_WH.WForm;
using DevExpress.DataProcessing;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using GridSumCol = DevExpress.Data.SummaryItemType;

namespace CNY_WH.UControl
{
    public partial class UctrlWHStockVoucherAe : UserControl
    {
        #region Declaration

        public TextEdit TxtId => this.txtId;
        public SearchLookUpEdit SlueType => this.sleType;
        public SearchLookUpEdit SlueSource => this.sleSource;
        public SearchLookUpEdit SlueDestination => this.sleDestination;
        public SearchLookUpEdit SlueFrwh => this.sleFrwh;
        public SearchLookUpEdit SlueTowh => this.sleTowh;
        
        public List<Object> LstCondIdSelected = new List<Object>();

        public GridView GvStkVae => this.gvuStkVae;
        public GridControl GcStkVae => this.gcuStkVae;
        public CheckEdit ChkInput => this.ChkInputRecPo;

        private readonly InfoWhMf _infMae;
        private InfoWh00001 _infStk001;
        private readonly InfoWh00002 _infWh002;
        private Dictionary<string, string> _dunit;
        private Dictionary<string, double> _dBatchQty;
        private Dictionary<string, double> _dSelBatQty;
        private Dictionary<Int64, double> _dLineQty;
        private RepositoryItemTextEdit _riTed;

        private long _lPkey;
        private string _sState;
        private string _sType;
        private bool _bChkInput = false;
        private bool _bRec = false;
        private string _sTransRit = "";

        private SystemGetCodeRule _sSysCodeNo;
        private SystemGetCodeRule _sSysCodeId;
        private SystemGetCodeRule _sSysBatchid;
        private SystemGetCodeRule _sSysAsnNo;

        private DataTable dtDeleted;
        private DataTable _dtRate;
        private DataTable _dtLocation;

        #endregion

        #region Constant

        private const string TableCny100 = "CNY00100";
        private const string TableCny104 = "CNY00104";
        private const string TableCny105A = "CNY00105A";

        private const string StkRecType = "1";
        private const string StkIssType = "2";
        private const string StkTraType = "3";
        private const string StkFgBalanceType = "T";

        private const string SCompCode = "CompCode";
        private const string SBatchComp = "CompCode";

        private const string CsStatus = "Status(F4)";
        private const string CsSel = "Selected";
        private const string CsReqNo = "Document No";
        private const string CsItemCode = "Item Code";
        private const string CsItemName = "Item Name";
        private const string CsItemColor = "Color";
        private const string CsItemSize = "Size";

        private const string CsReqUnit = "Unit";
        private const string CsQty = "Quantity";
        private const string CsPri = "Price";
        private const string CsCurr = "Currency";
        private const string CsAmt = "Amount";
        private const string CsOrderQty = "Order Quantity";
        private const string CsFactor = "Factor";
        private const string CsStkQty = "Stock Qty";
        private const string CsBalance = "Balance";
        private const string CsBatchNo = "BatchNo(F4)";
        private const string CsStkuQty = "STKUQTY";
        private const string CsStkUnit = "Stock Unit";
        private const string CsFrwah = "From WH";
        private const string CsTowah = "To WH";
        private const string CsRem = "Remark";
        private const string CsDocRef = "Doc_Reference";
        private const string CsAsnNo = "ASN No";
        private const string CsCartonNo = "Carton No";
        private const string CsLocation = "Location";
        private const string CsGw = "GW";
        private const string CsConfirmedDate = "Confirmed Date";
        private const string CsConfirmedBy = "Confirmed By";
        private const string CsTransDate = "Trans.Date";
        private const string CsValidBatch = "ValidBatch";

        private const string Sh1ColCnymf004Pkf = "CNYMF004PKF";
        private const string Sh2ColCnymf004Pkt = "CNYMF004PKT";
        private const string Sh3ColTdg00001Pk = "TDG00001PK";
        private const string Sh4ColCny00102Pk = "CNY00102PK";
        private const string Sh5ColCny00101Pk = "CNY00101PK";
        private const string Sh6ColCny00050Pk = "CNY00050PK";
        private const string Sh7ColCny00105Pk = "CNY00105PK";
        private const string Sh8ColCny00105APk = "CNY00105APK";
        private const string CsPk = "PK";

        private const int _withPopUp = 520;
        private const int _heightPopUp = 240;

        #endregion

        #region Init Form

        public UctrlWHStockVoucherAe(string state, string sType, long lPk)
        {
            _infMae = new InfoWhMf();
            _infStk001 = new InfoWh00001();
            _infWh002 = new InfoWh00002();
            _dunit = new Dictionary<string, string>();
            _dBatchQty = new Dictionary<string, double>();
            _dSelBatQty = new Dictionary<string, double>();
            _dLineQty = new Dictionary<long, double>();
            _riTed = new RepositoryItemTextEdit();
            dtDeleted = new DataTable();
            
            _sState = state;
            _bChkInput = _sState == Cls001Constant.StateAdd;

            _sType = sType;
            _lPkey = lPk;

            if (_sType == ClsWhConstant.ConStkRmRecRe ||
                _sType == ClsWhConstant.ConStkRmRecPo ||
                _sType == ClsWhConstant.ConStkFgRecRe ||
                _sType == ClsWhConstant.ConStkFgRecSo)
            {
                _sTransRit = ClsWhConstant.StrStkRec;
                _bRec = true;
            }
            else if (_sType == ClsWhConstant.ConStkRmIssRe ||
                     _sType == ClsWhConstant.ConStkRmIssMo ||
                     _sType == ClsWhConstant.ConStkFgIssRe ||
                     _sType == ClsWhConstant.ConStkFgIssSo)
            {
                _sTransRit = ClsWhConstant.StrStkIss;
                _bRec = false;
            }
            else
            {
                _sTransRit = ClsWhConstant.StrStkTransfer;
                _bRec = false;
            }

            long rno = Cls001MasterFiles.GetKeyCodeRule(TableCny104, Cls001Constant.StrSysCodeNo);
            _sSysCodeNo = new SystemGetCodeRule(rno, TableCny104);
            long rid = Cls001MasterFiles.GetKeyCodeRule(TableCny104, Cls001Constant.StrSysCodeId);
            _sSysCodeId = new SystemGetCodeRule(rid, TableCny104);
            long bid = Cls001MasterFiles.GetKeyCodeRule(TableCny100, Cls001Constant.StrSysCodeId);
            _sSysBatchid = new SystemGetCodeRule(bid, TableCny100);
            long lasn = Cls001MasterFiles.GetKeyCodeRule(TableCny105A, Cls001Constant.StrSysCodeNo);
            _sSysAsnNo = new SystemGetCodeRule(lasn, TableCny105A);

            InitializeComponent();
            Cls001GenFunctions.InitGridSelectCell(gcuStkVae, gvuStkVae, true, false);
            Cls001GenFunctions.ShowGridViewIndicator(gvuStkVae);
            Cls001GenFunctions.DrawRectangleSelectiononGridView(gcuStkVae, gvuStkVae);
            gvuStkVae.OptionsBehavior.FocusLeaveOnTab = true;

            LoadSearchLookupInfo();
            _dtRate = _infMae.GetRateFile();
            _dtLocation = _infStk001.LoadLocationInfo();

            this.Load += Uctrl001StockVoucherAe_Load;
            gcuStkVae.ProcessGridKey += GcuStkVae_ProcessGridKey;
            gvuStkVae.ShowingEditor += GvuStkVae_ShowingEditor;
            gvuStkVae.RowCellStyle += GvuStkVae_RowCellStyle;
            gvuStkVae.ValidatingEditor += GvuStkVae_ValidatingEditor;
            gvuStkVae.CellValueChanged += GvuStkVae_CellValueChanged;
            gvuStkVae.CellValueChanging += GvuStkVae_CellValueChanging;
            gvuStkVae.FocusedColumnChanged += GvuStkVae_FocusedColumnChanged;
            sleType.EditValueChanged += SleType_EditValueChanged;
            sleTowh.EditValueChanged += SleTowh_EditValueChanged;
            
            ChkInputRecPo.Visible = _sType == ClsWhConstant.ConStkRmRecRe || 
                                    _sType == ClsWhConstant.ConStkFgRecRe ||
                                    _sType == ClsWhConstant.ConStkRmIssRe ||
                                    _sType == ClsWhConstant.ConStkFgIssRe;
            ChkInputRecPo.CheckedChanged += ChkInputRecPo_CheckedChanged;
            ChkScanPo.CheckStateChanged += ChkScanPo_CheckStateChanged;
        }
        
        private void LoadSearchLookupInfo()
        {
            DataTable dtType =
                _infMae.LoadF4List110("Ltrim(Rtrim(CNY001)) + '-' + Ltrim(Rtrim(CNY002)) Description, PK ",
                    "CNYMF026", $"PARENTPK<>0 AND CNY004=1 AND RIGHT(RTRIM(CNY001),2)='01'");
            ProcessGeneral.LoadSearchLookup(sleType, dtType, "Description", "PK", BestFitMode.BestFit);

            DataTable dtwh =
                _infMae.LoadF4List110("Ltrim(Rtrim(CNY001)) + '-' + Ltrim(Rtrim(CNY002)) Description, PK ", "CNYMF004",
                    "1=1");
            ProcessGeneral.LoadSearchLookup(sleFrwh, dtwh, "Description", "PK", BestFitMode.BestFit);
            ProcessGeneral.LoadSearchLookup(sleTowh, dtwh, "Description", "PK", BestFitMode.BestFit);

            DataTable dtUnit = _infMae.LoadF4List110("CNY002, CNY001", "CNY00005", "1=1");
            for (int i = 0; i < dtUnit.Rows.Count; i++)
            {
                _dunit.Add(dtUnit.Rows[i]["CNY002"].ToString().Trim(), dtUnit.Rows[i]["CNY001"].ToString().Trim());
            }

            DataTable dtSupp = Cls001MasterFiles.GetSupplier();
            DataTable dtCust = Cls001MasterFiles.GetCustomer();
            DataTable dtDelivery = _infMae.LoadF4List110("CompanyCode, CNY001 CompanyName, PK", " CNYMF102 ", "1=1");
            
            switch (_sType)
            {
                case ClsWhConstant.ConStkFgIssSo:
                case ClsWhConstant.ConStkFgIssRe:
                    ProcessGeneral.LoadSearchLookup(sleSource, dtSupp, "Supplier Name", "PK", BestFitMode.BestFit);
                    ProcessGeneral.LoadSearchLookup(sleDestination, dtDelivery, "CompanyName", "CompanyCode", BestFitMode.BestFit);
                    break;

                case ClsWhConstant.ConStkRmRecPo:
                    ProcessGeneral.LoadSearchLookup(sleSource, dtSupp, "Supplier Name", "PK", BestFitMode.BestFit);
                    ProcessGeneral.LoadSearchLookup(sleDestination, dtDelivery, "CompanyName", "PK", BestFitMode.BestFit);

                    #region List POs

                    long lSource = ProcessGeneral.GetSafeInt64(SlueSource.EditValue);
                    DataTable dtPo = _infWh002.ListPoinAllocation_00120S(lSource, "", "", 0);
                    LoadxGridViewlist(xgvCondNo, dtPo, "PO No");
                    
                    #endregion
                    break;

                case ClsWhConstant.ConStkRmIssMo:
                    //MO
                    break;
                case ClsWhConstant.ConStkFgRecSo:
                case ClsWhConstant.ConStkFgRecRe:
                    ProcessGeneral.LoadSearchLookup(sleSource, dtCust, "Customer Name", "PK", BestFitMode.BestFit);
                    ProcessGeneral.LoadSearchLookup(sleDestination, dtSupp, "Supplier Name", "PK", BestFitMode.BestFit);
                    break;
            }
        }

        private void LoadxGridViewlist(XtraUCGridViewPopup xgvCond, DataTable dtSource, string sDisplayField)
        {
            int index = -1;
            var lColumn = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = "POID",
                    FieldName = "POID",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = ++index,
                    FormatField = FormatType.None,
                    FormatString = string.Empty,
                    IncreaseWdith = 20,
                    SummayType = GridSumCol.Count,
                    SummaryFormatString = "  Total :",
                    SummaryHorzAlign = HorzAlignment.Near,
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = "PO No",
                    FieldName = "PO No",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = ++index,
                    FormatField = FormatType.None,
                    FormatString = string.Empty,
                    IncreaseWdith = 40,
                    SummayType = GridSumCol.Count,
                    SummaryFormatString = "{0:N0} (item)",
                    SummaryHorzAlign = HorzAlignment.Center,
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = "Supplier",
                    FieldName = "Supplier",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = ++index,
                    FormatField = FormatType.None,
                    FormatString = string.Empty,
                    IncreaseWdith = 40,
                    SummayType = GridSumCol.None
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = "PR No",
                    FieldName = "PR No",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = ++index,
                    FormatField = FormatType.None,
                    FormatString = string.Empty,
                    IncreaseWdith = 20,
                    SummayType = GridSumCol.None,
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = "LSX",
                    FieldName = "LSX",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = ++index,
                    FormatField = FormatType.None,
                    FormatString = string.Empty,
                    IncreaseWdith = 20,
                    SummayType = GridSumCol.None,
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = "PO Type",
                    FieldName = "PO Type",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = string.Empty,
                    IncreaseWdith = 20,
                    SummayType = GridSumCol.None,
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = "PR Type",
                    FieldName = "PR Type",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = string.Empty,
                    IncreaseWdith = 20,
                    SummayType = GridSumCol.None,
                },
            };
            xgvCond.KeyField = "POID";
            xgvCond.DisplayField = sDisplayField;
            xgvCond.PopupWidth = _withPopUp;
            xgvCond.PopupHeight = _heightPopUp;
            xgvCond.IsShowFindPanel = true;
            xgvCond.IsShowFooter = false;
            xgvCond.IsShowAutoFilterRow = false;
            xgvCond.Separator = ", ";
            xgvCond.IsMultiSelected = true;
            xgvCond.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            xgvCond.IsAddColumnSelected = true;
            xgvCond.ShowButton = false;
            xgvCond.ShowFinishButton = false;
            xgvCond.LoadDataGridView(dtSource, lColumn);
            xgvCond.OnTransferData += XgvCond_OnTransferData;
        }

        private void XgvCond_OnTransferData(object sender, CNY_BaseSys.Common.TransferDataOnGridViewEventArgs e)
        {
            LstCondIdSelected.Clear();

            string sCondid = "0";
            List<DataRow> lRow = e.ReturnRowsSelected;
            foreach (var row in lRow)
            {
                LstCondIdSelected.Add(ProcessGeneral.GetSafeInt64(row["POID"]));
                sCondid += $",{ProcessGeneral.GetSafeInt64(row["POID"])}";
            }

            ScanPo(sCondid);
        }

        private void GetStockVoucherId()
        {
            _sSysCodeId.ArrData = new List<CodeRuleFieldData>()
            {
                new CodeRuleFieldData
                {
                    TableFieldSql = CsPk,
                    StringValue = ProcessGeneral.GetSafeString(txtId.EditValue)
                },
            };
            _sSysCodeId.CreateCodeString();

            txtId.EditValue = _sSysCodeId.CodeRuleData.StrCode;
            _lPkey = ProcessGeneral.GetSafeInt64(txtId.EditValue);
        }

        private void Uctrl001StockVoucherAe_Load(object sender, EventArgs e)
        {
            #region ctrl_Text

            List<TextEdit> lstText = panelControl2.FindAllChildrenByType<TextEdit>().ToList();
            foreach (var eText in lstText)
            {
                eText.KeyDown += EText_KeyDown;
                eText.Leave += EText_Leave;
            }

            #endregion

            if (_sType == ClsWhConstant.ConStkRmRecPo)
            {
                if (ChkScanPo.CheckState == CheckState.Checked)
                {
                    lblReqNo.Text = @"POID";
                    txtCondId.Visible = true;
                    xgvCondNo.Visible = false;
                }
                else
                {
                    lblReqNo.Text = @"PO No";
                    txtCondId.Visible = false;
                    xgvCondNo.Visible = true;
                }
            }
            
            if (_sState == Cls001Constant.StateAdd)
            {
                string sType =
                    $" Select Ltrim(Rtrim(CNY001)) + '-' + Ltrim(Rtrim(CNY002)) Description, PK From CNYMF026 where CNY001='{_sType}'";
                DataTable dtType = Cls001MasterFiles.ExcecuteSql(sType);
                if (dtType == null || dtType.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Invalid Stock Type. Please Reselect in List.", "Warning!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    sleType.ReadOnly = false;
                }
                else
                {
                    sleType.Text = dtType.Rows[0][1].ToString().Trim();
                }

                GetStockVoucherId();
            }

            txtId.EditValue = _lPkey;
            txtDateIss.Focus();
            LoadStockVoucher();
        }

        private void LoadStockVoucher()
        {
            if (_sState == Cls001Constant.StateAdd)
            {
                #region AddNew

                txtDateIss.EditValue = string.Format("{0:dd-MMM-yyyy}", ProcessGeneral.GetServerDate());

                txtCrdate.EditValue = ProcessGeneral.GetServerDate();
                txtCrdate.Properties.DisplayFormat.FormatString = "dd-MMM-yyyy hh:mm:ss";
                txtCrdate.Properties.Mask.EditMask = @"dd-MMM-yyyy hh:mm:ss";
                txtCrby.EditValue = DeclareSystem.SysUserName;

                txtModiDate.EditValue = ProcessGeneral.GetServerDate();
                txtModiDate.Properties.DisplayFormat.FormatString = "dd-MMM-yyyy hh:mm:ss";
                txtModiDate.Properties.Mask.EditMask = @"dd-MMM-yyyy hh:mm:ss";
                txtModiby.EditValue = DeclareSystem.SysUserName;

                txtVer.EditValue = @"0";

                #endregion
            }
            else
            {
                #region Adjust

                string sHed =
                    $"Select C104.*, C26.CNY001 + '-' + C26.CNY002 DocType, C26.PK DocPk From CNY00104 C104 INNER JOIN CNYMF026 C26 ON C26.PK=C104.CNYMF026PK Where C104.PK='{_lPkey}'";
                var dtHeader = Cls001MasterFiles.ExcecuteSql(sHed);
                if (dtHeader == null || dtHeader.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Invalid Data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtId.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PK"]);
                txtVoucherNo.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY001"]);
                sleType.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DocPk"]);
                txtDateIss.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY002"]);
                sleSource.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY004"]);
                sleDestination.EditValue = $@"0{ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY005"])}";
                txtDescription.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY003"]);
                txtOrderNo.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY006"]);
                txtVer.EditValue = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["CNY012"]);
                txtCrby.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY008"]);
                txtCrdate.EditValue = string.Format("{0:dd-MMM-yyyy}", dtHeader.Rows[0]["CNY007"]);
                txtModiby.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY010"]);
                txtModiDate.EditValue = string.Format("{0:dd-MMM-yyyy}", dtHeader.Rows[0]["CNY009"]);
                ChkOfficialDoc.Checked = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["CNY011"]) == 1;
                sleFrwh.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNYMF004PKS"]);
                sleTowh.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNYMF004PKD"]);

                #endregion

                LoadGridForAdj();
            }
        }

        public void LoadGridForAdj()
        {
            try
            {
                DataTable dtDet = new DataTable();
                string[] sTypeDoc = sleType.Text.Trim().Split(new char[] {'-'});
                string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);

                switch (sType)
                {
                    case ClsWhConstant.ConStkRmRecPo:
                        // F14.08.20
                        dtDet = _infStk001.LoadGvStockDocDetails00110R(sType, _lPkey);
                        break;
                    case ClsWhConstant.ConStkFgRecSo:
                    case ClsWhConstant.ConStkFgIssSo:
                        //dtDet = _infStk001.LoadGvStockDocDetails00110F(sType, _lPkey);
                        dtDet = _infStk001.LoadGvStockDocDetails_App_00110F(sType, _lPkey);
                        break;

                    case ClsWhConstant.ConStkFgRecRe:
                    case ClsWhConstant.ConStkRmRecRe:
                        dtDet = _infStk001.LoadGvStockDocDetails_App_00110(sType, _lPkey);
                        break;

                    case ClsWhConstant.ConStkRmIssMo:
                    case ClsWhConstant.ConStkRmIssRe:
                    case ClsWhConstant.ConStkFgIssRe:
                        dtDet = _infStk001.LoadGvStockDocDetails00110(sType, _lPkey);
                        break;
                }

                gcuStkVae.DataSource = dtDet;
                gvuStkVae.BestFitColumns();

                LoadDictQty(_lPkey);
                HideCols(sType);
                FormatGridCols();

                switch (sType)
                {
                    case ClsWhConstant.ConStkRmRecRe:
                    case ClsWhConstant.ConStkRmIssRe:
                    case ClsWhConstant.ConStkFgRecRe:
                    case ClsWhConstant.ConStkFgIssRe:
                        var dchk = dtDet.AsEnumerable()
                            .Where(w => ProcessGeneral.GetSafeInt(w[Sh4ColCny00102Pk]) == 0)
                            .Select(s => s).Take(1);
                        ChkInputRecPo.Checked = dchk.Any();
                        _bChkInput = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(@"Error on StockVoucher\LoadGridForAdj " + ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadDictQty(long lPk)
        {
            if(_sState==Cls001Constant.StateAdd) return;

            //Note Line Qty
            _dLineQty.Clear();
            DataTable dtLine = (DataTable) gcuStkVae.DataSource;
            if (dtLine == null || dtLine.Rows.Count == 0) return;

            for (int i = 0; i < dtLine.Rows.Count; i++)
            {
                long lipk = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][CsPk]);
                double dliQty = ProcessGeneral.GetSafeDouble(dtLine.Rows[i][CsQty]);
                if (!_dLineQty.ContainsKey(lipk)) _dLineQty.Add(lipk, dliQty);
            }

            // Note Batch Qty
            _dBatchQty.Clear();
            DataSet dsDic = _infStk001.LoadBatchInfo_App_00105A(lPk);
            if (dsDic == null || dsDic.Tables.Count == 0) return;

            // Current
            DataTable dtDic = dsDic.Tables[0];
            //DataTable dtDic = _infStk001.LoadBatchInfo00105A(lPk, 0, 0, "");
            if (dtDic == null || dtDic.Rows.Count == 0) return;

            for (int i = 0; i < dtDic.Rows.Count; i++)
            {
                string sBatchno = ProcessGeneral.GetSafeString(dtDic.Rows[i][0]);
                double dQty = ProcessGeneral.GetSafeDouble(dtDic.Rows[i][1]);
                if (!_dBatchQty.ContainsKey(sBatchno)) _dBatchQty.Add(sBatchno, dQty);
            }

            // Selected on voucher
            _dSelBatQty.Clear();
            DataTable dtSeDic = dsDic.Tables[1];
            if (dtSeDic == null || dtSeDic.Rows.Count == 0) return;

            for (int i = 0; i < dtSeDic.Rows.Count; i++)
            {
                string sSeBatchno = ProcessGeneral.GetSafeString(dtSeDic.Rows[i][0]);
                double dSeQty = ProcessGeneral.GetSafeDouble(dtSeDic.Rows[i][1]);
                if (!_dSelBatQty.ContainsKey(sSeBatchno)) _dSelBatQty.Add(sSeBatchno, dSeQty);
            }
        }

        private void HideCols(string sType)
        {
            switch (sType)
            {
                case ClsWhConstant.ConStkRmRecPo:
                case ClsWhConstant.ConStkRmRecRe:
                    ProcessGeneral.HideVisibleColumnsGridView(gvuStkVae, false
                        , CsStatus, CsFrwah, CsTowah, CsFactor, CsStkuQty, CsStkUnit, CsDocRef, CsOrderQty, CsStkQty, CsBalance
                        , CsAsnNo, CsCartonNo, CsGw, CsConfirmedBy, CsConfirmedDate, CsTransDate, CsValidBatch
                        , Sh1ColCnymf004Pkf, Sh2ColCnymf004Pkt, Sh3ColTdg00001Pk, Sh4ColCny00102Pk
                        , Sh5ColCny00101Pk, Sh6ColCny00050Pk, Sh7ColCny00105Pk, Sh8ColCny00105APk, CsPk);
                    break;

                case ClsWhConstant.ConStkFgRecSo:
                case ClsWhConstant.ConStkFgRecRe:
                    ProcessGeneral.HideVisibleColumnsGridView(gvuStkVae, false
                        , CsStatus, CsFrwah, CsTowah, CsFactor, CsStkuQty, CsStkUnit, CsDocRef
                        , CsCurr, CsAmt, CsOrderQty, CsStkQty, CsBalance, CsValidBatch
                        , Sh1ColCnymf004Pkf, Sh2ColCnymf004Pkt, Sh3ColTdg00001Pk, Sh4ColCny00102Pk
                        , Sh5ColCny00101Pk, Sh6ColCny00050Pk, Sh7ColCny00105Pk, Sh8ColCny00105APk, CsPk);
                    break;

                case ClsWhConstant.ConStkRmIssMo:
                case ClsWhConstant.ConStkRmIssRe:
                case ClsWhConstant.ConStkFgIssRe:
                    ProcessGeneral.HideVisibleColumnsGridView(gvuStkVae, false
                        , CsStatus, CsFrwah, CsTowah, CsFactor, CsStkuQty, CsStkUnit, CsDocRef
                        , CsPri, CsCurr, CsAmt, CsOrderQty, CsStkQty, CsBalance
                        , CsGw, CsConfirmedBy, CsConfirmedDate, CsTransDate, CsValidBatch
                        , Sh1ColCnymf004Pkf, Sh2ColCnymf004Pkt, Sh3ColTdg00001Pk, Sh4ColCny00102Pk
                        , Sh5ColCny00101Pk, Sh6ColCny00050Pk, Sh7ColCny00105Pk, Sh8ColCny00105APk, CsPk);
                    break;

                case ClsWhConstant.ConStkFgIssSo:
                    ProcessGeneral.HideVisibleColumnsGridView(gvuStkVae, false
                        , CsStatus, CsFrwah, CsTowah, CsFactor, CsStkuQty, CsStkUnit, CsDocRef
                        , CsCurr, CsAmt, CsOrderQty, CsStkQty, CsBalance
                        , CsAsnNo, CsCartonNo, CsLocation, CsGw, CsConfirmedBy, CsConfirmedDate, CsTransDate, CsValidBatch
                        , Sh1ColCnymf004Pkf, Sh2ColCnymf004Pkt, Sh3ColTdg00001Pk
                        , Sh4ColCny00102Pk, Sh5ColCny00101Pk, Sh6ColCny00050Pk, Sh7ColCny00105Pk
                        , Sh8ColCny00105APk, CsPk);
                    break;

                default:
                    ProcessGeneral.HideVisibleColumnsGridView(gvuStkVae, false
                        , CsStatus, CsFactor, CsStkuQty, CsStkUnit, CsDocRef
                        , CsAsnNo, CsCartonNo, CsLocation, CsGw, CsConfirmedBy, CsConfirmedDate, CsTransDate, CsValidBatch
                        , Sh1ColCnymf004Pkf, Sh2ColCnymf004Pkt, Sh3ColTdg00001Pk, Sh4ColCny00102Pk
                        , Sh5ColCny00101Pk, Sh6ColCny00050Pk, Sh7ColCny00105Pk, Sh8ColCny00105APk, CsPk);
                    break;
            }
        }

        private void FormatGridCols()
        {
            gvuStkVae.Columns[CsQty].DisplayFormat.FormatType = FormatType.Numeric;
            gvuStkVae.Columns[CsQty].DisplayFormat.FormatString = "N3";
            gvuStkVae.Columns[CsStkQty].DisplayFormat.FormatType = FormatType.Numeric;
            gvuStkVae.Columns[CsStkQty].DisplayFormat.FormatString = "N3";
            gvuStkVae.Columns[CsOrderQty].DisplayFormat.FormatType = FormatType.Numeric;
            gvuStkVae.Columns[CsOrderQty].DisplayFormat.FormatString = "N3";
            gvuStkVae.Columns[CsBalance].DisplayFormat.FormatType = FormatType.Numeric;
            gvuStkVae.Columns[CsBalance].DisplayFormat.FormatString = "N3";
            gvuStkVae.Columns[CsPri].DisplayFormat.FormatType = FormatType.Numeric;
            gvuStkVae.Columns[CsPri].DisplayFormat.FormatString = "N5";
            gvuStkVae.Columns[CsAmt].DisplayFormat.FormatType = FormatType.Numeric;
            gvuStkVae.Columns[CsAmt].DisplayFormat.FormatString = "N3";

            gvuStkVae.Columns[CsGw].DisplayFormat.FormatType = FormatType.Numeric;
            gvuStkVae.Columns[CsGw].DisplayFormat.FormatString = "N3";

            gvuStkVae.Columns[CsPri].Caption = @"Landed Cost";
            gvuStkVae.Columns[CsPri].Width = gvuStkVae.Columns[CsBatchNo].Width;

            _riTed.CharacterCasing = CharacterCasing.Upper;
            gvuStkVae.Columns[CsLocation].ColumnEdit = _riTed;
        }

        #endregion

        #region Check Changed

        private DataTable GetTableTemplategv()
        {
            DataTable dtTemp = new DataTable();

            dtTemp.Columns.Add(CsStatus, typeof(string));
            dtTemp.Columns.Add(CsReqNo, typeof(string));
            dtTemp.Columns.Add(CsItemCode, typeof(string));
            dtTemp.Columns.Add(CsItemName, typeof(string));
            dtTemp.Columns.Add(CsItemColor, typeof(string));
            dtTemp.Columns.Add(CsItemSize, typeof(string));
            dtTemp.Columns.Add(CsReqUnit, typeof(string));
            dtTemp.Columns.Add(CsQty, typeof(double));
            dtTemp.Columns.Add(CsPri, typeof(double));
            dtTemp.Columns.Add(CsCurr, typeof(string));
            dtTemp.Columns.Add(CsAmt, typeof(double));
            dtTemp.Columns.Add(CsOrderQty, typeof(double));
            dtTemp.Columns.Add(CsFactor, typeof(string));
            dtTemp.Columns.Add(CsStkQty, typeof(double));
            dtTemp.Columns.Add(CsBalance, typeof(double));
            dtTemp.Columns.Add(CsBatchNo, typeof(string));
            dtTemp.Columns.Add(CsStkuQty, typeof(double));
            dtTemp.Columns.Add(CsStkUnit, typeof(string));
            dtTemp.Columns.Add(CsFrwah, typeof(string));
            dtTemp.Columns.Add(CsTowah, typeof(string));
            dtTemp.Columns.Add(CsRem, typeof(string));
            dtTemp.Columns.Add(CsDocRef, typeof(string));

            dtTemp.Columns.Add(Sh8ColCny00105APk, typeof(long));
            dtTemp.Columns.Add(CsAsnNo, typeof(string));
            dtTemp.Columns.Add(CsCartonNo, typeof(string));
            dtTemp.Columns.Add(CsLocation, typeof(string));
            dtTemp.Columns.Add(CsGw, typeof(double));
            dtTemp.Columns.Add(CsConfirmedDate, typeof(DateTime));
            dtTemp.Columns.Add(CsConfirmedBy, typeof(string));
            dtTemp.Columns.Add(CsTransDate, typeof(DateTime));
            dtTemp.Columns.Add(CsValidBatch, typeof(int));

            dtTemp.Columns.Add(Sh6ColCny00050Pk, typeof(string));
            dtTemp.Columns.Add(Sh1ColCnymf004Pkf, typeof(long));
            dtTemp.Columns.Add(Sh2ColCnymf004Pkt, typeof(long));
            dtTemp.Columns.Add(Sh3ColTdg00001Pk, typeof(long));
            dtTemp.Columns.Add(Sh4ColCny00102Pk, typeof(long));
            dtTemp.Columns.Add(Sh5ColCny00101Pk, typeof(long));
            dtTemp.Columns.Add(Sh7ColCny00105Pk, typeof(long));
            dtTemp.Columns.Add(CsPk, typeof(long));

            return dtTemp;
        }

        private void AddLineWhenCheckRecPoChanged()
        {
            try
            {
                if (!_bChkInput) return;
                long lfwh = ProcessGeneral.GetSafeInt64(sleFrwh.EditValue);
                long ltwh = ProcessGeneral.GetSafeInt64(sleTowh.EditValue);
                if (lfwh == 0 || ltwh == 0)
                {
                    XtraMessageBox.Show("Invalid WareHouse Code!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string[] sTypeDoc = sleType.Text.Trim().Split(new char[] {'-'});
                string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);

                DataTable dtGrid = (DataTable) gcuStkVae.DataSource;
                if (ChkInputRecPo.CheckState == CheckState.Checked)
                {
                    if (dtGrid == null) dtGrid = GetTableTemplategv();
                    DataRow drw = dtGrid.NewRow();
                    drw[CsStatus] = ClsWhConstant.StrStatusInprocess;
                    drw[CsPk] = "-1";
                    dtGrid.Rows.Add(drw);
                    gcuStkVae.DataSource = dtGrid;
                    HideCols(sType);

                    gvuStkVae.FocusedColumn = gvuStkVae.Columns[CsItemCode];
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error on Void_CheckRecPoChanged", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkInputRecPo_CheckedChanged(object sender, EventArgs e)
        {
            if (_sType == ClsWhConstant.ConStkRmRecPo) return;
            AddLineWhenCheckRecPoChanged();
        }

        private void SetCondFocus()
        {
            if (ChkScanPo.CheckState == CheckState.Checked)
            {
                lblReqNo.Text = @"POID";
                txtCondId.Visible = true;
                xgvCondNo.Visible = false;

                xtraTabControl1.SelectedTabPageIndex = 1;
                txtCondId.Focus();
            }
            else
            {
                lblReqNo.Text = @"PO No";
                txtCondId.Visible = false;
                xgvCondNo.Visible = true;

                xtraTabControl1.SelectedTabPageIndex = 1;
                xgvCondNo.Focus();
            }
        }
        private void ChkScanPo_CheckStateChanged(object sender, EventArgs e)
        {
            SetCondFocus();
        }

        #endregion

        #region TextKey

        private void GetStockVoucherNo()
        {
            try
            {
                _sSysCodeNo.ArrData = new List<CodeRuleFieldData>()
                {
                    new CodeRuleFieldData
                    {
                        TableFieldSql = SCompCode,
                        StringValue = DeclareSystem.SysCompanyCode
                    },
                };
                _sSysCodeNo.CreateCodeString();

                txtVoucherNo.EditValue = _sSysCodeNo.CodeRuleData.StrCode;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error occur at GetStockVoucherNo(). ", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        private void SleType_EditValueChanged(object sender, EventArgs e)
        {
            if (_sState == Cls001Constant.StateAdd)
            {
                if (GvStkVae == null || GvStkVae.RowCount == 0)
                {
                    GetStockVoucherNo();
                }
            }
        }

        private void SleTowh_EditValueChanged(object sender, EventArgs e)
        {
            if (_sState == Cls001Constant.StateAdd)
            {
                if (GvStkVae == null || GvStkVae.RowCount == 0)
                {
                    if (!ChkInputRecPo.Checked) ChkInputRecPo.CheckState = CheckState.Checked;
                }
            }
        }

        private void EText_Leave(object sender, EventArgs e)
        {
            var cTxtName = sender as TextEdit;
            if (cTxtName == null) return;

            switch (cTxtName.Name)
            {
                case "txtDateIss":
                    cTxtName.EditValue = string.Format("{0: dd-MMM-yyyy}", Convert.ToDateTime(cTxtName.EditValue));
                    break;
            }
        }

        private void EText_KeyDown(object sender, KeyEventArgs e)
        {
            var cTxtName = sender as TextEdit;
            string sTxtVa = ProcessGeneral.GetSafeString(cTxtName.EditValue);
            
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                case Keys.Down:

                    #region Enter

                    switch (cTxtName.Name)
                    {
                        case "sleType":
                            txtDateIss.Focus();
                            break;
                        case "txtDateIss":
                            sleFrwh.Focus();
                            break;
                        case "sleFrwh":
                            sleTowh.Focus();
                            break;
                        case "sleTowh":
                            txtOrderNo.Focus();
                            break;
                        case "txtOrderNo":
                            SetCondFocus();
                            break;
                        case "txtCondId":
                            ScanPo(sTxtVa);
                            break;
                    }

                    #endregion

                    break;

                case Keys.Up:
                case Keys.Escape:

                    #region Escape

                    switch (cTxtName.Name)
                    {
                        case "sleType":
                            txtOrderNo.Focus();
                            break;
                        case "txtDateIss":
                            sleType.Focus();
                            break;
                        case "sleFrwh":
                            txtDateIss.Focus();
                            break;
                        case "sleTowh":
                            sleFrwh.Focus();
                            break;
                        case "txtOrderNo":
                            sleTowh.Focus();
                            break;
                    }

                    #endregion

                    break;

                case Keys.F4:

                    #region F4

                    switch (cTxtName.Name)
                    {
                        case "txtType":

                            #region Type

                            //using (var frmSel = new FrmF4Grid()
                            //{
                            //    Text = @"Type Listing",
                            //    Caption = "Type Listing",
                            //    DataSource = _infMae.LoadF4List110("Ltrim(Rtrim(CNY001)), Ltrim(Rtrim(CNY002)), PK ", "CNYMF026", "1=1"),
                            //    StrColE = "Code,Description,PK",
                            //    iDL = 2
                            //})
                            //{
                            //    frmSel.FireEventData += (s1, e1) =>
                            //    {
                            //        String[] str = e1.ID.Split(new Char[] { '|' });
                            //        sleType.EditValue = str[0].ToUpper().Trim();
                            //        txtTypeDesc.EditValue = $"{str[2]}-{ str[1].Trim()}";
                            //    };
                            //    frmSel.ShowDialog();
                            //}

                            #endregion

                            break;
                    }

                    #endregion

                    break;
            }
        }

        #endregion

        #region Functions

        private string GetAsnNo()
        {
            int iComp = ProcessGeneral.GetSafeInt(DeclareSystem.SysCompanyCode);
            _sSysAsnNo.ArrData = new List<CodeRuleFieldData>()
            {
                new CodeRuleFieldData
                {
                    TableFieldSql = SBatchComp,
                    StringValue = iComp.ToString()
                },
            };
            _sSysAsnNo.CreateCodeString();
            CodeRuleReturnSave sRuAsnNo = _sSysAsnNo.SaveCodeData();
            return ProcessGeneral.GetSafeString(_sSysAsnNo.CodeRuleData.StrCode);
        }

        private void AssignAsnNo(DataTable dtSrc)
        {
            if (_sType == ClsWhConstant.ConStkRmRecRe ||
                _sType == ClsWhConstant.ConStkRmRecPo ||
                _sType == ClsWhConstant.ConStkFgRecRe ||
                _sType == ClsWhConstant.ConStkFgRecSo)
            {
                if (dtSrc == null || dtSrc.Rows.Count == 0) return;
                dtSrc.AsEnumerable()
                    .Where(w => ProcessGeneral.GetSafeString(w[CsAsnNo]) == "").ToList()
                    .ForEach(dr => dr[CsAsnNo] = GetAsnNo());
            }
        }

        private string GetStringBatchNo(long iPk102)
        {
            string sBatch = "";
            var dtDet = (DataTable) gcuStkVae.DataSource;

            if (dtDet != null && dtDet.Rows.Count > 0)
            {
                var vBat = dtDet.AsEnumerable()
                    .Where(w => ProcessGeneral.GetSafeInt64(w[Sh4ColCny00102Pk]) == iPk102
                                && ProcessGeneral.GetSafeString(w[CsBatchNo]) != "")
                    .Select(s => new {Batch = ProcessGeneral.GetSafeString(s[CsBatchNo])}).Distinct();
                if (vBat.Any())
                {
                    foreach (var item in vBat)
                    {
                        sBatch += string.Format("{0},", item.Batch);
                    }
                }
            }

            if (sBatch.Length > 1) sBatch = sBatch.Substring(0, sBatch.Length - 1);
            return sBatch;
        }

        private double GetSelQtyBy102Pk(long l102Pk, long lT01Pk, string sColName)
        {
            DataTable dtDet = (DataTable)gcuStkVae.DataSource;
            if (dtDet == null) return 0;
            double dSelQty = 0;

            if (sColName == CsQty)
            {
                var vQty = dtDet.AsEnumerable()
                    .Where(w => ProcessGeneral.GetSafeInt64(w[Sh4ColCny00102Pk]) == l102Pk
                                && ProcessGeneral.GetSafeInt64(w[Sh3ColTdg00001Pk]) == lT01Pk
                                && ProcessGeneral.GetSafeString(w[CsBatchNo]) != "")
                    .Select(s => new { Qty = ProcessGeneral.GetSafeDouble(s[CsQty]) })
                    .Sum(m => m.Qty);
                dSelQty = ProcessGeneral.GetSafeDouble(vQty);
            }

            if (sColName == CsOrderQty)
            {
                var vQty = dtDet.AsEnumerable()
                    .Where(w => ProcessGeneral.GetSafeInt64(w[Sh4ColCny00102Pk]) == l102Pk
                                && ProcessGeneral.GetSafeInt64(w[Sh3ColTdg00001Pk]) == lT01Pk)
                    .Select(s => new { Qty = ProcessGeneral.GetSafeDouble(s[CsOrderQty]) })
                    .Sum(m => m.Qty);
                dSelQty = ProcessGeneral.GetSafeDouble(vQty);
            }

            if (sColName == CsStkQty)
            {
                var vQty = dtDet.AsEnumerable()
                    .Where(w => ProcessGeneral.GetSafeInt64(w[Sh4ColCny00102Pk]) == l102Pk
                                && ProcessGeneral.GetSafeInt64(w[Sh3ColTdg00001Pk]) == lT01Pk)
                    .Select(s => new { Qty = ProcessGeneral.GetSafeDouble(s[CsStkQty]) })
                    .Sum(m => m.Qty);
                dSelQty = ProcessGeneral.GetSafeDouble(vQty);
            }

            return dSelQty;
        }

        private double GetSelQtyByBatchNo(string sBatNo)
        {
            DataTable dtDet = (DataTable)gcuStkVae.DataSource;
            if (dtDet == null) return 0;
            if (string.IsNullOrEmpty(sBatNo)) return 0;

            dtDet.AcceptChanges();
            var vQty = dtDet.AsEnumerable()
                .Where(w => ProcessGeneral.GetSafeString(w[CsBatchNo]) == sBatNo)
                .Select(s => new { Qty = ProcessGeneral.GetSafeDouble(s[CsQty]) }).Sum(m => m.Qty);
            double dSelQty = ProcessGeneral.GetSafeDouble(vQty);
            return dSelQty;
        }

        private void SetStockQtyonGrid(string sBatchno, double dBalance, double dPrice)
        {
            int iRow = gvuStkVae.FocusedRowHandle;
            long l102Pk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, Sh4ColCny00102Pk));
            long lT01Pk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, Sh3ColTdg00001Pk));
            double dOrdQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsOrderQty));
            double dStkQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsStkQty));
            double dSelQty = GetSelQtyBy102Pk(l102Pk, lT01Pk, CsQty);
            double dResQty = dOrdQty - dStkQty - dSelQty;
            double dnBal = dBalance <= dResQty ? dBalance : dResQty;

            gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, CsQty, dnBal);
            gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, CsPri, dPrice);
            gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, CsBatchNo, sBatchno);
            gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, CsBalance, dResQty - dnBal);
            if (dBalance < dResQty) AddNewLine();
        }

        private void SetDtStkQtyonGrid(DataTable dtSelect)
        {
            DataTable dtSel = dtSelect.Clone();
            for (int i = dtSelect.Rows.Count - 1; i >= 0; i--)
            {
                DataRow drSelect = dtSelect.Rows[i];
                dtSel.ImportRow(drSelect);
            }
            var dtS = gcuStkVae.DataSource as DataTable;
            dtS.AcceptChanges();

            int iRow = gvuStkVae.FocusedRowHandle;
            int iLrow = iRow;
            //long l102Pk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, Sh4ColCny00102Pk));
            //long lT01Pk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, Sh3ColTdg00001Pk));
            //long lCurrPk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, CsPk));

            //double dOrdQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsOrderQty));
            //double dStkQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsStkQty));
            //double dSelQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsQty)); //GetSelQtyBy102Pk(l102Pk, lT01Pk, CsQty);
            //double dResQty = dOrdQty - dStkQty; //dOrdQty - dStkQty - dSelQty

            //CoreV
            double dOrdQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsQty));
            double dResQty = dOrdQty;

            double dAvaQty = 0;
            double dnBal = 0;

            int iSelCol = dtSel.Columns[CsPri].Ordinal + 1;
            int ifGrCol = gvuStkVae.Columns[CsItemSize].AbsoluteIndex + 1;
            int itGrCol = gvuStkVae.Columns[CsReqUnit].AbsoluteIndex;

            for (int i = 0; i < dtSel.Rows.Count; i++)
            {
                DataRow dr = dtSel.Rows[i];
                dAvaQty = ProcessGeneral.GetSafeDouble(dr["Available Qty"]);
                if (dAvaQty <= dResQty)
                {
                    dnBal = dAvaQty;
                    dResQty = dResQty - dAvaQty;
                }
                else
                {
                    dnBal = dResQty;
                    dResQty = 0;
                }

                double dPrice = ProcessGeneral.GetSafeDouble(dr[CsPri]);
                string sBatchno = ProcessGeneral.GetSafeString(dr["BatchNo"]);
                string sAsnNo = ProcessGeneral.GetSafeString(dr["ASN_No"]);
                string sCartonNo = ProcessGeneral.GetSafeString(dr["Carton_No"]);
                string sLocation = ProcessGeneral.GetSafeString(dr["Location"]);
                string sConfBy = ProcessGeneral.GetSafeString(dr["Confirm_By"]);
                DateTime dConfDate = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(dr["Confirm_Date"]);
                decimal mGw = ProcessGeneral.GetSafeDecimal(dr["GW"]);

                gvuStkVae.SetRowCellValue(iRow + i, CsQty, dnBal);
                gvuStkVae.SetRowCellValue(iRow + i, CsPri, dPrice);
                gvuStkVae.SetRowCellValue(iRow + i, CsBatchNo, sBatchno);
                gvuStkVae.SetRowCellValue(iRow + i, CsBalance, dResQty);

                gvuStkVae.SetRowCellValue(iRow + i, CsAsnNo, sAsnNo);
                gvuStkVae.SetRowCellValue(iRow + i, CsCartonNo, sCartonNo);
                gvuStkVae.SetRowCellValue(iRow + i, CsLocation, sLocation);
                gvuStkVae.SetRowCellValue(iRow + i, CsConfirmedDate, dConfDate);
                gvuStkVae.SetRowCellValue(iRow + i, CsConfirmedBy, sConfBy);
                gvuStkVae.SetRowCellValue(iRow + i, CsGw, mGw);
                gvuStkVae.SetRowCellValue(iRow + i, CsValidBatch, 1);

                if (dResQty > 0)
                {
                    DataRow drs = gvuStkVae.GetDataRow(iRow);
                    DataRow drsn = dtS.NewRow();

                    for (int k = 0; k < gvuStkVae.Columns.Count; k++)
                    {
                        drsn[k] = drs[k];
                    }

                    drsn[CsAmt] = 0.000000;
                    drsn[CsPk] = -1;

                    dtS.Rows.InsertAt(drsn, iRow + i + 1);
                    iLrow = iRow + i + 1;

                    if (i == dtSel.Rows.Count - 1)
                    {
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsQty, dResQty);
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsPri, "0.000000");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsBatchNo, "");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsBalance, dResQty);

                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsAsnNo, "");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsCartonNo, "");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsLocation, "");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsConfirmedDate, "1900-01-01");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsConfirmedBy, "");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsGw, "0.000");
                        gvuStkVae.SetRowCellValue(iRow + i + 1, CsValidBatch, 1);
                    }

                    if (!_dBatchQty.ContainsKey(sBatchno)) _dBatchQty.Add(sBatchno, dAvaQty);

                    #region Fulfil Att Info.

                    for (int j = iSelCol; j < dtSel.Columns.Count; j++)
                    {
                        string sSelColName = ProcessGeneral.GetSafeString(dtSel.Columns[j].ColumnName);
                        string sSelValue = ProcessGeneral.GetSafeString(dtSel.Rows[0][j]);

                        for (int k = ifGrCol; k < itGrCol; k++)
                        {
                            string sGrColName = ProcessGeneral.GetSafeString(gvuStkVae.Columns[k].FieldName);
                            if (sSelColName == sGrColName)
                            {
                                gvuStkVae.SetRowCellValue(iRow + i, sSelColName, sSelValue);
                                break;
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    break;
                }
            }

            if (iLrow + 1 == dtS.Rows.Count - 1)
            {
                gvuStkVae.SelectRow(iLrow + 1);
                gvuStkVae.FocusedRowHandle = iLrow + 1;
                gvuStkVae.FocusedColumn = gvuStkVae.Columns[CsItemCode];
            }
            else
            {
                gvuStkVae.SelectRow(iLrow);
                gvuStkVae.FocusedRowHandle = iLrow;
            }
        }

        private void AddNewLine()
        {
            if (XtraMessageBox.Show("Do You Want to Add New Line?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int iRow = gvuStkVae.FocusedRowHandle;
                long l102Pk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, Sh4ColCny00102Pk));
                if (l102Pk == 0)
                {
                    string[] sColfii = { CsStatus, CsBatchNo, CsPk };
                    string[] sColvii = { ClsWhConstant.StrStatusInprocess, "", "-1" };

                    Cls001GenFunctions.SplitLine(gcuStkVae, true, sColfii, sColvii);
                    return;
                }

                double dQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsOrderQty));
                double dStkQty = ProcessGeneral.GetSafeDouble(gvuStkVae.GetRowCellValue(iRow, CsStkQty));
                long lT01Pk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, Sh3ColTdg00001Pk));

                double dSel = GetSelQtyBy102Pk(l102Pk, lT01Pk, CsQty);
                double nQty = dQty - dStkQty - dSel;
                string sQty = nQty.ToString(CultureInfo.InvariantCulture);

                if (nQty > 0)
                {
                    string[] sColfi = { CsBatchNo, CsBalance, CsQty, CsPri, CsPk };
                    string[] sColvi = { "", $"{sQty}", sQty, "0.000000", "-1" };
                    Cls001GenFunctions.SplitLine(gcuStkVae, true, sColfi, sColvi);
                }
                else
                {
                    XtraMessageBox.Show("Can not Insert New Line.\n(Issued Quantity is greater than Order Quantity.)",
                        "Error!"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddNewEmptyLine()
        {
            int iRow = gvuStkVae.FocusedRowHandle;
            long l102Pk = ProcessGeneral.GetSafeInt64(gvuStkVae.GetRowCellValue(iRow, Sh4ColCny00102Pk));
            if (l102Pk == 0)
            {
                string[] sColfii = { CsStatus, CsBatchNo, CsCurr, CsPk };
                string[] sColvii = { ClsWhConstant.StrStatusInprocess, "", "02-USD", "-1" };
                Cls001GenFunctions.SplitLine(gcuStkVae, false, sColfii, sColvii);

                gvuStkVae.FocusedRowHandle += 1;
                gvuStkVae.FocusedColumn = gvuStkVae.Columns[CsItemCode];
            }
        }

        private void LoadAsnInfo(string sAsnNo, int ifrow)
        {
            var dtDetails = (DataTable)gcuStkVae.DataSource;
            if (dtDetails == null || dtDetails.Rows.Count == 0) return;
            dtDetails.AcceptChanges();

            var vAsn = dtDetails.AsEnumerable()
                .Where(w => ProcessGeneral.GetSafeString(w[CsAsnNo]) == sAsnNo)
                .Select(s => new
                {
                    ASN_No = s[CsAsnNo],
                    Carton_No = s[CsCartonNo],
                    Location = s[CsLocation],
                    ConfDate = s[CsConfirmedDate],
                    ConfBy = s[CsConfirmedBy],
                    GW = s[CsGw]
                }).Take(1);

            foreach (var vr in vAsn)
            {
                gvuStkVae.SetRowCellValue(ifrow, CsAsnNo, vr.ASN_No);
                gvuStkVae.SetRowCellValue(ifrow, CsCartonNo, vr.Carton_No);
                gvuStkVae.SetRowCellValue(ifrow, CsLocation, vr.Location);
                gvuStkVae.SetRowCellValue(ifrow, CsConfirmedDate, vr.ConfDate);
                gvuStkVae.SetRowCellValue(ifrow, CsConfirmedBy, vr.ConfBy);
                gvuStkVae.SetRowCellValue(ifrow, CsGw, vr.GW);
            }
        }

        private void LoadGwInfo(decimal mGw, int ifrow)
        {
            var dtDetails = (DataTable)gcuStkVae.DataSource;
            if (dtDetails == null || dtDetails.Rows.Count == 0) return;
            dtDetails.AcceptChanges();

            string sAsnNo = ProcessGeneral.GetSafeString(gvuStkVae.GetRowCellValue(ifrow, CsAsnNo));
            dtDetails.AsEnumerable()
                .Where(w => ProcessGeneral.GetSafeString(w[CsAsnNo]) == sAsnNo).ToList()
                .ForEach(dr => dr[CsGw] = mGw);
        }

        #endregion

        #region GridKeys & Format
        
        private void GcuStkVae_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                GridControl grid = sender as GridControl;
                if (!(grid?.FocusedView is GridView gview)) return;

                if ((e.Modifiers == Keys.None && gview.FocusedColumn.VisibleIndex == -1))
                {
                    if (gview.IsEditing) gview.CloseEditor();
                    if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Right)
                    {
                        SelectNextControl(grid, e.Modifiers == Keys.None, false, true, true);
                    }

                    gview.FocusedRowHandle = gview.RowCount - 1;
                    gview.FocusedColumn = gview.Columns[CsItemCode];
                    e.Handled = true;
                }
                string sColFi = ProcessGeneral.GetSafeString(gview.FocusedColumn.FieldName);
                int iCurColind = ProcessGeneral.GetSafeInt(gview.FocusedColumn.AbsoluteIndex);

                string[] sTypeDoc = sleType.Text.Trim().Split(new char[] {'-'});
                string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);
                string sCode = ProcessGeneral.GetSafeString(gview.GetRowCellValue(gview.FocusedRowHandle, CsItemCode));

                switch (e.KeyCode)
                {
                    case Keys.F8:
                        #region F8

                        //Return Qty=0;
                        int iRow = gview.FocusedRowHandle;
                        string sLiStatus = ProcessGeneral.GetSafeString(gview.GetRowCellValue(iRow, CsStatus));
                        if (sLiStatus == ClsWhConstant.StrStatusComplete)
                        {
                            XtraMessageBox.Show("Invalid Status.", "Noted!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        long ltedepk = ProcessGeneral.GetSafeInt64(gview.GetRowCellValue(iRow, CsPk));
                        if (ltedepk == -1)
                        {
                            gvuStkVae.DeleteRow(iRow);
                        }

                        #endregion
                        break;

                    case Keys.F3:
                        if (sColFi == CsBatchNo)
                        {
                            #region Batch No With Att.Info

                            if (sType.Substring(0, 1) == StkRecType) return;
                            string swaCode = ProcessGeneral.GetSafeString(gvuStkVae.GetRowCellValue(gvuStkVae.FocusedRowHandle,CsItemCode));
                            string swaWh = ProcessGeneral.GetSafeString(gvuStkVae.GetRowCellValue(gvuStkVae.FocusedRowHandle,CsFrwah));
                            string[] swaWcod = swaWh.Split(new char[] {'-'});
                            long lwa102Pk = ProcessGeneral.GetSafeInt64(
                                gvuStkVae.GetRowCellValue(gvuStkVae.FocusedRowHandle, Sh4ColCny00102Pk));
                            string swaBatch = GetStringBatchNo(lwa102Pk);

                            #region Conds Att Info.
                            
                            int iwafGrCol = gvuStkVae.Columns[CsItemSize].AbsoluteIndex + 1;
                            int iwatGrCol = gvuStkVae.Columns[CsReqUnit].AbsoluteIndex;
                            string sCond = "";

                            for (int j = iwafGrCol; j < iwatGrCol; j++)
                            {
                                string swaGrColName = ProcessGeneral.GetSafeString(gvuStkVae.Columns[j].FieldName);
                                string swaGrColVal =
                                    ProcessGeneral.GetSafeString(gvuStkVae.GetRowCellValue(gvuStkVae.FocusedRowHandle,
                                        swaGrColName));
                                if (string.IsNullOrEmpty(swaGrColVal)) continue;
                                sCond += $" And [{swaGrColName}]='{swaGrColVal}' ";
                            }

                            #endregion

                            #region Datasource

                            DataTable dtwaSelSrc = (DataTable) gcuStkVae.DataSource;
                            var vSel = dtwaSelSrc.AsEnumerable()
                                .Where(w => ProcessGeneral.GetSafeString(w[CsItemCode]) == swaCode &&
                                            ProcessGeneral.GetSafeString(w[CsBatchNo]) != "")
                                .GroupBy(g => g[CsBatchNo])
                                .Select(s =>
                                    new
                                    {
                                        BAT = ProcessGeneral.GetSafeString(s.Key),
                                        QTY = s.Sum(m => ProcessGeneral.GetSafeDouble(m[CsQty]))
                                    });
                            DataTable dtwaSel = vSel.CopyToDataTableNew();
                            DataTable dtwasrc = _infStk001.LoadBatchInfo00105(swaCode, sType, swaWcod[0], swaBatch,
                                dtwaSel, sCond);
                            if (dtwasrc == null) return;

                            #endregion

                            #region Column Name

                            string sColName = Cls001GenFunctions.GetColumnNameFrdt(dtwasrc);

                            #endregion

                            using (var frmSel = new FrmF4Grid()
                            {
                                Text = @"Batch Listing",
                                Caption = @"Batch Listing",
                                DataSource = dtwasrc,
                                StrColE = sColName,
                                iDL = 2
                            })
                            {
                                frmSel.FireEventData += (s1, e1) =>
                                {
                                    string[] swaid = e1.ID.Split(new char[] {'|'});
                                    double dwaBal = ProcessGeneral.GetSafeDouble(swaid[3]);
                                    string swaBatchno = ProcessGeneral.GetSafeString(swaid[2].Trim());
                                    double dwaPri = ProcessGeneral.GetSafeDouble(swaid[4]);

                                    SetStockQtyonGrid(swaBatchno, dwaBal, dwaPri);
                                    if (!_dBatchQty.ContainsKey(swaBatchno)) _dBatchQty.Add(swaBatchno, dwaBal);
                                };
                                frmSel.ShowDialog();
                            }

                            #endregion
                        }
                        break;

                    case Keys.F4:
                        #region F4

                        switch (sColFi)
                        {
                            case CsStatus:
                                #region Status

                                //using (var frmSel = new FrmF4Grid()
                                //{
                                //    Text = @"Status Listing",
                                //    Caption = @"Status Listing",
                                //    DataSource = _infMae.LoadF4List110("AASY001, AASY002", "AASY0000",
                                //    $"AASYPK='MO' AND AASY003='STA' "),
                                //    StrColE = "Code,Description",
                                //    iDL = 2
                                //})
                                //{
                                //    frmSel.FireEventData += (s1, e1) =>
                                //    {
                                //        string[] sid = e1.ID.Split(new char[] { '|' });
                                //        gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, sStatus, sid[0].Trim());
                                //    };
                                //    frmSel.ShowDialog();
                                //}

                                gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, CsStatus,
                                    ClsWhConstant.StrStatusComplete);

                                #endregion
                                break;

                            case CsItemCode:
                                #region Item Code

                                Boolean bff = ((sType == ClsWhConstant.ConStkRmRecRe
                                                || sType == ClsWhConstant.ConStkRmIssRe
                                                || sType == ClsWhConstant.ConStkFgRecRe
                                                || sType == ClsWhConstant.ConStkFgIssRe)
                                               && ChkInputRecPo.CheckState == CheckState.Checked);
                                if (!bff) return;

                                DataTable dtItem = _infMae.LoadItemCodeInfo00166(sType, sCode);

                                if (dtItem == null) return;
                                string sColItemName = Cls001GenFunctions.GetColumnNameFrdt(dtItem);

                                using (var frmSel = new FrmF4Grid()
                                {
                                    Text = @"Item List",
                                    Caption = @"Item List",
                                    DataSource = dtItem,
                                    StrColE = sColItemName,
                                    iDL = 2
                                })
                                {
                                    frmSel.FireEventData += (s1, e1) =>
                                        {
                                            ReloadGridViewAfterSelectItems(gcuStkVae, gvuStkVae, e1.DtSel, sType);
                                        };

                                    frmSel.WindowState = FormWindowState.Maximized;
                                    frmSel.ShowDialog();
                                }

                                #endregion
                                break;

                            case CsCurr:
                                #region Currency

                                if (sType == ClsWhConstant.ConStkRmRecPo || sType == ClsWhConstant.ConStkFgRecSo)
                                    return;
                                using (var frmSel = new FrmF4Grid()
                                {
                                    Text = @"Currency Listing",
                                    Caption = @"Currency Listing",
                                    DataSource =
                                        _infMae.LoadF4List110("LTRIM(RTRIM(CNY001)) + '-' + LTRIM(RTRIM(CNY002))",
                                            "CNY00006", $"1=1 "),
                                    StrColE = "Description",
                                    iDL = 0
                                })
                                {
                                    frmSel.FireEventData += (s1, e1) =>
                                    {
                                        string[] sid = e1.ID.Split(new char[] {'|'});
                                        gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, CsCurr, sid[0].Trim());
                                    };
                                    frmSel.ShowDialog();
                                }

                                #endregion
                                break;

                            case CsBatchNo:
                                #region Batch No.

                                if (sType.Substring(0, 1) == StkRecType) return;
                                string sWh = ProcessGeneral.GetSafeString(SlueFrwh.Text);
                                string[] sWcod = sWh.Split(new char[] {'-'});
                                long l102Pk = ProcessGeneral.GetSafeInt64(
                                    gvuStkVae.GetRowCellValue(gvuStkVae.FocusedRowHandle, Sh4ColCny00102Pk));
                                string sBatch = GetStringBatchNo(l102Pk);

                                #region Datasource

                                DataTable dtSelSrc = (DataTable) gcuStkVae.DataSource;
                                var vSel = dtSelSrc.AsEnumerable()
                                    .Where(w => ProcessGeneral.GetSafeString(w[CsItemCode]) == sCode &&
                                                ProcessGeneral.GetSafeString(w[CsBatchNo]) != "")
                                    .GroupBy(g => g[CsBatchNo])
                                    .Select(s =>
                                        new
                                        {
                                            BATCHNO = ProcessGeneral.GetSafeString(s.Key),
                                            QTY = s.Sum(m => ProcessGeneral.GetSafeDouble(m[CsQty]))
                                        });
                                DataTable dtSel = vSel.CopyToDataTableNew();
                                DataTable dtsrc =
                                    _infStk001.LoadBatchInfo_App_00105(sCode, sType, sWcod[0], sBatch, dtSel, null);
                                if (dtsrc == null || dtsrc.Rows.Count == 0) return;

                                #endregion

                                #region Column Name

                                string sColName = Cls001GenFunctions.GetColumnNameFrdt(dtsrc);

                                #endregion

                                using (var frmSel = new FrmF4Grid()
                                {
                                    Text = @"Batch Listing",
                                    Caption = @"Batch Listing",
                                    DataSource = dtsrc,
                                    StrColE = sColName,
                                    iDL = 2
                                })
                                {
                                    frmSel.FireEventData += (s1, e1) => { SetDtStkQtyonGrid(e1.DtSel); };
                                    frmSel.ShowDialog();
                                    //frmSel.FireEventData += (s1, e1) =>
                                    //{
                                        //string[] sid = e1.ID.Split(new char[] {'|'});
                                        //double dBal = ProcessGeneral.GetSafeDouble(sid[3]);
                                        //long lBatchno = ProcessGeneral.GetSafeInt64(sid[2].Trim());
                                        //double dPri = ProcessGeneral.GetSafeDouble(sid[4]);

                                        //SetStockQtyonGrid(lBatchno, dBal, dPri);
                                        //if (!_dBatchQty.ContainsKey(lBatchno)) _dBatchQty.Add(lBatchno, dBal);

                                        //#region Fulfil Att Info.

                                        //DataTable dtSero = e1.DtSel;
                                        //int iSelCol = dtSero.Columns["Available Qty"].Ordinal + 1;
                                        //int ifGrCol = gvuStkVae.Columns[CsItemColor].AbsoluteIndex + 1;
                                        //int itGrCol = gvuStkVae.Columns[CsReqUnit].AbsoluteIndex;

                                        //for (int i = iSelCol; i < dtSero.Columns.Count; i++)
                                        //{
                                        //    string sSelColName =
                                        //        ProcessGeneral.GetSafeString(dtSero.Columns[i].ColumnName);
                                        //    string sSelValue = ProcessGeneral.GetSafeString(dtSero.Rows[0][i]);

                                        //    for (int j = ifGrCol; j < itGrCol; j++)
                                        //    {
                                        //        string sGrColName =
                                        //            ProcessGeneral.GetSafeString(gvuStkVae.Columns[j].FieldName);
                                        //        if (sSelColName == sGrColName)
                                        //        {
                                        //            gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, sSelColName,
                                        //                sSelValue);
                                        //            break;
                                        //        }
                                        //    }
                                        //}

                                        //#endregion
                                    //};
                                    //frmSel.ShowDialog();
                                }

                                #endregion
                                break;

                            case CsLocation:
                                #region Location

                                if (_sType == ClsWhConstant.ConStkFgIssRe || _sType == ClsWhConstant.ConStkFgIssSo) return;

                                if (_dtLocation == null || _dtLocation.Rows.Count == 0) return;
                                string sColLocName = Cls001GenFunctions.GetColumnNameFrdt(_dtLocation);

                                using (var frmSel = new FrmF4Grid()
                                {
                                    Text = @"Location Listing",
                                    Caption = @"Location Listing",
                                    DataSource = _dtLocation,
                                    StrColE = sColLocName,
                                    iDL = 0
                                })
                                {
                                    frmSel.FireEventData += (s1, e1) =>
                                    {
                                        string[] sid = e1.ID.Split(new char[] { '|' });
                                        gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, CsLocation, sid[0].Trim());
                                    };
                                    frmSel.ShowDialog();
                                }

                                #endregion
                                break;
                        }
                        
                        #region F4 Att

                        if (sType.Substring(0, 1) != StkRecType
                            || ChkInputRecPo.CheckState != CheckState.Checked) return;

                        int iFromCol = gvuStkVae.Columns[CsItemSize].AbsoluteIndex + 1;
                        int iToCol = gvuStkVae.Columns[CsReqUnit].AbsoluteIndex;
                        if (iCurColind < iFromCol || iCurColind > iToCol) return;

                        string[] sColVa = sColFi.Split(new char[] {'-'});
                        long i008Pk = ProcessGeneral.GetSafeInt64(sColVa[0]);

                        if (i008Pk > 0)
                        {
                            DataTable dtF4AttSrc = _infMae.LoadF4AttributeValues00162(i008Pk);
                            if (dtF4AttSrc == null) return;
                            string sColAttName = Cls001GenFunctions.GetColumnNameFrdt(dtF4AttSrc);

                            using (var frmSel = new FrmF4Grid()
                            {
                                Text = sColFi,
                                Caption = sColFi,
                                DataSource = dtF4AttSrc,
                                StrColE = sColAttName,
                                iDL = 0
                            })
                            {
                                frmSel.FireEventData += (s1, e1) =>
                                {
                                    gvuStkVae.SetRowCellValue(gvuStkVae.FocusedRowHandle, gvuStkVae.FocusedColumn,
                                        e1.ID.Trim());
                                };
                                frmSel.ShowDialog();
                            }
                        }

                        #endregion

                        #endregion
                        break;

                    case Keys.F6:
                        AddNewLine();
                        break;

                    case Keys.Insert:
                        AddNewEmptyLine();
                        break;

                    case Keys.Delete:
                        #region Delete Pri & Cur

                        if (sType == ClsWhConstant.ConStkRmRecPo
                            || sType == ClsWhConstant.ConStkFgRecSo
                            || ChkInputRecPo.CheckState != CheckState.Checked) return;

                        int idf = ProcessGeneral.GetMinRowSelectedOnGrid(gvuStkVae);
                        int idt = ProcessGeneral.GetMaxRowSelectedOnGrid(gvuStkVae);

                        switch (sColFi)
                        {
                            case CsPri:
                                for (int i = idf; i <= idt; i++)
                                {
                                    gvuStkVae.SetRowCellValue(i, CsPri, 0);
                                }

                                break;
                            case CsCurr:
                                for (int i = idf; i <= idt; i++)
                                {
                                    gvuStkVae.SetRowCellValue(i, CsCurr, "");
                                }

                                break;
                        }

                        #endregion
                        break;

                    case Keys.Enter:
                        #region Enter

                        switch (sColFi)
                        {
                            case CsQty:
                                gvuStkVae.FocusedColumn = _bRec ? gvuStkVae.Columns[CsPri] : gvuStkVae.Columns[CsBatchNo];
                                break;
                            case CsBatchNo:
                            case CsPri:
                                gvuStkVae.FocusedColumn = gvuStkVae.Columns[CsRem];
                                break;
                            case CsRem:
                                gvuStkVae.FocusedColumn = gvuStkVae.Columns[CsCartonNo];
                                break;
                            case CsCartonNo:
                                gvuStkVae.FocusedColumn = gvuStkVae.Columns[CsLocation];
                                break;
                            case CsLocation:
                                gvuStkVae.FocusedColumn = gvuStkVae.Columns[CsGw];
                                break;
                        }

                        #endregion
                        break;
                }

                #region Ctrl & D

                if (e.KeyData == (Keys.Control | Keys.D))
                {
                    switch (sColFi)
                    {
                        case CsPri:
                        case CsCurr:
                            ProcessGeneral.CopyValueCellOnColumn(gvuStkVae, gvuStkVae.FocusedColumn);
                            break;
                    }
                }

                #endregion
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error on GcuStkVae_ProcessGridKey");
            }
        }

        private void ReloadGridViewAfterSelectItems(GridControl gc, GridView gv
            , DataTable dtSelected, string sType)
        {
            try
            {
                if (dtSelected == null || dtSelected.Rows.Count == 0) return;

                DataTable dtsrc = (DataTable) gc.DataSource;
                if (dtsrc == null || dtsrc.Rows.Count == 0)
                {
                    dtsrc = dtSelected;
                    Cls001GenFunctions.GridViewCustomCols(gv, dtsrc);
                    AssignAsnNo(dtsrc);

                    gc.DataSource = dtsrc;
                    gv.BestFitColumns();

                    HideCols(sType);
                    FormatGridCols();
                    LoadDictQty(_lPkey);
                    
                    return;
                }

                #region Add New Columns

                DataTable dtns = dtsrc.Clone();
                var vsel = dtSelected.Columns.OfType<DataColumn>().Select(dc =>
                    new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));
                var vselcol = vsel.AsEnumerable()
                    .Where(w => dtns.Columns.Contains(w.ColumnName) == false)
                    .Select(s => s);
                dtns.Columns.AddRange(vselcol.ToArray());

                #endregion
                
                #region Sort datatable new source

                int inewind = 0;
                int iattind = dtns.Columns[CsReqUnit].Ordinal;
                int iPkind = dtns.Columns[CsPk].Ordinal;
                int icolcou = dtns.Columns.Count;

                //From Item Code To Old Attribute
                for (int i = 0; i < iattind; i++)
                {
                    dtns.Columns[i].SetOrdinal(inewind);
                    inewind++;
                }

                // From PK to New Attribute
                for (int i = iPkind + 1; i < icolcou; i++)
                {
                    dtns.Columns[i].SetOrdinal(inewind);
                    inewind++;
                }

                #endregion

                #region Import Data

                for (int i = 0; i < dtsrc.Rows.Count; i++)
                {
                    DataRow dr = dtsrc.Rows[i];
                    if (string.IsNullOrEmpty(dr[CsItemName].ToString())) continue;
                    dtns.ImportRow(dr);
                }

                for (int i = dtSelected.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtSelected.Rows[i];
                    if (string.IsNullOrEmpty(dr[CsItemCode].ToString())) continue;
                    dtns.ImportRow(dr);

                    int irow = dtns.Rows.Count;
                    DataRow dnr = dtns.Rows[irow - 1];
                    dnr[CsStatus] = ClsWhConstant.StrStatusInprocess;
                    dnr[CsPk] = "-1";
                    dnr[Sh1ColCnymf004Pkf] = ProcessGeneral.GetSafeInt64(sleFrwh.EditValue);
                    dnr[Sh2ColCnymf004Pkt] = ProcessGeneral.GetSafeInt64(sleTowh.EditValue);
                }

                #endregion

                Cls001GenFunctions.GridViewCustomCols(gv, dtsrc);
                AssignAsnNo(dtns);
                gc.DataSource = dtns;
                //gc.ForceInitialize();
                gv.BestFitColumns();

                HideCols(sType);
                FormatGridCols();
                //LoadDictQty(_lPkey);
            }
            catch (Exception )
            {
                XtraMessageBox.Show("Error on Reload_Grid", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadGridViewAfterScanItems(GridControl gc, GridView gv
          , DataTable dtSelected, string sType)
        {
            try
            {
                if (dtSelected == null || dtSelected.Rows.Count == 0) return;

                DataTable dtsrc = (DataTable)gc.DataSource;
                dtsrc.AcceptChanges();
                if (dtsrc == null || dtsrc.Rows.Count == 0)
                {
                    dtsrc = dtSelected;
                    Cls001GenFunctions.GridViewCustomCols(gv, dtsrc);
                    AssignAsnNo(dtsrc);

                    gc.DataSource = dtsrc;
                    gv.BestFitColumns();

                    HideCols(sType);
                    FormatGridCols();

                    return;
                }

                #region Add New Columns

                DataTable dtns = dtsrc.Clone();
                //var vsel = dtSelected.Columns.OfType<DataColumn>().Select(dc =>
                //    new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));
                //var vselcol = vsel.AsEnumerable()
                //    .Where(w => dtns.Columns.Contains(w.ColumnName) == false)
                //    .Select(s => s);
                //dtns.Columns.AddRange(vselcol.ToArray());

                #endregion

                #region Sort datatable new source

                //int inewind = 0;
                //int iattind = dtns.Columns[CsReqUnit].Ordinal;
                //int iPkind = dtns.Columns[CsPk].Ordinal;
                //int icolcou = dtns.Columns.Count;

                //From Item Code To Old Attribute
                //for (int i = 0; i < iattind; i++)
                //{
                //    dtns.Columns[i].SetOrdinal(inewind);
                //    inewind++;
                //}

                // From PK to New Attribute
                //for (int i = iPkind + 1; i < icolcou; i++)
                //{
                //    dtns.Columns[i].SetOrdinal(inewind);
                //    inewind++;
                //}

                #endregion

                #region Import Data

                for (int i = 0; i < dtsrc.Rows.Count; i++)
                {
                    DataRow dr = dtsrc.Rows[i];
                    if (string.IsNullOrEmpty(dr[CsItemName].ToString())) continue;
                    dtns.ImportRow(dr);
                }

                for (int i = dtSelected.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtSelected.Rows[i];
                    if (string.IsNullOrEmpty(dr[CsItemCode].ToString())) continue;
                    dtns.ImportRow(dr);

                    int irow = dtns.Rows.Count;
                    DataRow dnr = dtns.Rows[irow - 1];
                    dnr[CsStatus] = ClsWhConstant.StrStatusInprocess;
                    dnr[CsPk] = "-1";
                    dnr[CsCurr] = "02-USD";
                    dnr[Sh1ColCnymf004Pkf] = ProcessGeneral.GetSafeInt64(sleFrwh.EditValue);
                    dnr[Sh2ColCnymf004Pkt] = ProcessGeneral.GetSafeInt64(sleTowh.EditValue);
                }

                #endregion

                //Cls001GenFunctions.GridViewCustomCols(gv, dtsrc);

                AssignAsnNo(dtns);
                gc.DataSource = dtns;
                gc.ForceInitialize();
                gv.BestFitColumns();

                HideCols(sType);
                FormatGridCols();

                gv.FocusedRowHandle = dtns.Rows.Count - 1;
                gv.FocusedColumn = gv.Columns[CsQty];
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error on Reload_Grid", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GvuStkVae_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (!(sender is GridView gv) || gv.RowCount == 0) return;
            string sBatch = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, CsBatchNo));
            string sVb = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, CsValidBatch));

            if (ChkOfficialDoc.CheckState == CheckState.Checked)
            {
                e.Appearance.BackColor = SystemColors.ButtonFace;
            }
            else
            {
                switch (e.Column.FieldName)
                {
                    case CsBatchNo:
                        e.Appearance.BackColor = sVb == "1" ? Color.LightGoldenrodYellow : Color.OrangeRed;
                        break;

                    case CsPri:
                        e.Appearance.BackColor = _sTransRit == ClsWhConstant.StrStkRec && string.IsNullOrEmpty(sBatch)
                            ? SystemColors.Window
                            : SystemColors.ButtonFace;
                        break;

                    case CsItemCode:
                        break;

                    case CsCartonNo:
                    case CsLocation:
                    case CsGw:
                        e.Appearance.BackColor = string.IsNullOrEmpty(sBatch)
                            ? SystemColors.Window
                            : SystemColors.ButtonFace;
                        break;

                    case CsSel:
                    case CsQty:
                    case CsRem:
                    case CsDocRef:
                        e.Appearance.BackColor = SystemColors.Window;
                        break;
                }
            }

            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.Appearance.BackColor = Color.FromArgb(169, 249, 108);

            //int iRow = e.RowHandle;
            //int ie = e.Column.AbsoluteIndex;
            //string sLiStatus = ProcessGeneral.GetSafeString(gv.GetRowCellValue(iRow, CsStatus));

            //if (sLiStatus == ClsWhConstant.StrStatusComplete)
            //{
            //    e.Appearance.BackColor = SystemColors.ButtonFace;
            //}
            //else if (sLiStatus == ClsWhConstant.StrStatusInprocess)
            //{
            //    int iStatus = gvuStkVae.Columns[CsStatus].AbsoluteIndex;
            //    int iItemName = gvuStkVae.Columns[CsItemName].AbsoluteIndex;
            //    int iAmt = gvuStkVae.Columns[CsAmt].AbsoluteIndex;
            //    int iBatch = gvuStkVae.Columns[CsBatchNo].AbsoluteIndex;
            //    int iDocNo = gvuStkVae.Columns[CsReqNo].AbsoluteIndex;
            //    int iDocRef = gvuStkVae.Columns[CsDocRef].AbsoluteIndex;
            //    int iRem = gvuStkVae.Columns[CsRem].AbsoluteIndex;

            //    e.Appearance.BackColor = (ie == iStatus || ie == iItemName
            //                                            || (ie >= iAmt && ie <= iBatch)
            //                                            || ie == iDocNo || ie == iDocRef || ie == iRem)
            //        ? SystemColors.ButtonFace
            //        : SystemColors.Window;
            //}
            //else
            //{
            //    int iFromCol = gvuStkVae.Columns[CsItemColor].AbsoluteIndex + 1;
            //    int iToCol = gvuStkVae.Columns[CsReqUnit].AbsoluteIndex;
            //    int iQtyCol = gvuStkVae.Columns[CsQty].AbsoluteIndex;
            //    int iPriCol = gvuStkVae.Columns[CsPri].AbsoluteIndex;
            //    int iCurCol = gvuStkVae.Columns[CsCurr].AbsoluteIndex;

            //    #region Common

            //    if (ie == iQtyCol)
            //        e.Appearance.BackColor = SystemColors.Window;
            //    else if (ie == iPriCol)
            //        e.Appearance.BackColor = ProcessGeneral.GetSafeDecimal(e.CellValue) == 0
            //            ? SystemColors.Window
            //            : SystemColors.ButtonFace;
            //    else if (ie == iCurCol)
            //        e.Appearance.BackColor = string.IsNullOrEmpty(e.CellValue.ToString())
            //            ? Color.LightBlue
            //            : SystemColors.ButtonFace;
            //    else if (ie < iFromCol || ie > iToCol)
            //        e.Appearance.BackColor = SystemColors.ButtonFace;
            //    else
            //    {
            //        e.Appearance.BackColor = Color.LightBlue;
            //    }

            //    #endregion
            //}

            //if (!gv.IsRowSelected(e.RowHandle)) return;
            //e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            //e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
            //e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }

        private void GvuStkVae_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null || gv.RowCount == 0) return;
            int iRow = gv.FocusedRowHandle;
            string sFil = gv.FocusedColumn.FieldName;

            string sBatch = ProcessGeneral.GetSafeString(gv.GetRowCellValue(iRow, CsBatchNo));

            if (ChkOfficialDoc.CheckState == CheckState.Checked)
            {
                e.Cancel = true;
            }
            else
            {
                long lC102Pk = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(iRow, Sh4ColCny00102Pk));
                if (lC102Pk == 0)
                {
                    switch (sFil)
                    {
                        case CsQty:
                        case CsGw:
                            e.Cancel = ChkOfficialDoc.CheckState == CheckState.Checked;
                            break;

                        case CsPri:
                            e.Cancel = _sTransRit != ClsWhConstant.StrStkRec || !string.IsNullOrEmpty(sBatch);
                            break;

                        case CsRem:
                            e.Cancel = false;
                            break;

                        case CsItemCode:
                        case CsAsnNo:
                        case CsCartonNo:
                        case CsLocation:
                            e.Cancel = !string.IsNullOrEmpty(sBatch);
                            break;

                        default:
                            e.Cancel = true;
                            break;
                    }
                }
                else
                {
                    switch (sFil)
                    {
                        case CsBatchNo:
                            e.Cancel = _sTransRit == ClsWhConstant.StrStkRec || !string.IsNullOrEmpty(sBatch);
                            break;

                        case CsPri:
                            e.Cancel = !(_sTransRit == ClsWhConstant.StrStkRec && string.IsNullOrEmpty(sBatch));
                            break;

                        case CsAsnNo:
                        case CsCartonNo:
                        case CsLocation:
                        case CsGw:
                            e.Cancel = !string.IsNullOrEmpty(sBatch);
                            break;

                        case CsSel:
                        case CsQty:
                        case CsRem:
                        case CsDocRef:
                            e.Cancel = false;
                            break;

                        default:
                            e.Cancel = true;
                            break;
                    }
                }
            }

            //string sLiStatus = ProcessGeneral.GetSafeString(gv.GetRowCellValue(iRow, CsStatus));

            //if (sLiStatus == ClsWhConstant.StrStatusComplete)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    long lC102Pk = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(iRow, Sh4ColCny00102Pk));
            //    if (lC102Pk == 0)
            //    {
            //        switch (sFil)
            //        {
            //            case CsReqNo:
            //            case CsStatus:
            //            case CsItemName:
            //            case CsAmt:
            //            case CsOrderQty:
            //            case CsStkQty:
            //            case CsBalance:
            //            case CsBatchNo:
            //            case CsDocRef:
            //            case CsRem:
            //                e.Cancel = true;
            //                break;
            //            default:
            //                e.Cancel = false;
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        switch (sFil)
            //        {
            //            case CsQty:
            //                e.Cancel = false;
            //                break;

            //            case CsBatchNo:
            //                e.Cancel = _sType == ClsWhConstant.ConStkFgRecRe
            //                           || _sType == ClsWhConstant.ConStkFgRecSo
            //                           || _sType == ClsWhConstant.ConStkRmRecRe
            //                           || _sType == ClsWhConstant.ConStkRmRecPo;
            //                break;

            //            case CsPri:
            //                double dPrice = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(iRow, CsPri));
            //                e.Cancel = dPrice > 0;
            //                break;

            //            case CsCurr:
            //                string sCurr = ProcessGeneral.GetSafeString(gv.GetRowCellValue(iRow, CsCurr));
            //                e.Cancel = !string.IsNullOrEmpty(sCurr);
            //                break;

            //            default:
            //                e.Cancel = true;
            //                break;
            //        }
            //    }
            //}
        }

        private void GvuStkVae_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!(sender is GridView gv) || gv.RowCount == 0) return;
            DrawRectangleSelection.RePaintGridView(gv);
        }

        private void GvuStkVae_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null || gv.RowCount == 0) return;
            string fieldName = gv.FocusedColumn.FieldName;
            int ifrow = gv.FocusedRowHandle;

            try
            {
                string[] sTypeDoc = sleType.Text.Trim().Split(new char[] { '-' });
                string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);
                string sCode = ProcessGeneral.GetSafeString(e.Value);

                switch (fieldName)
                {
                    case CsItemCode:
                        if (string.IsNullOrEmpty(sCode)) return;
                        DataTable dtItem = _infMae.LoadItemCodeInfo00166(sType, sCode);
                        if (dtItem == null || dtItem.Rows.Count == 0)
                        {
                            XtraMessageBox.Show("Invalid Item Code");
                            return;
                        }
                        ReloadGridViewAfterScanItems(gcuStkVae, gvuStkVae, dtItem, sType);
                        break;

                    case CsQty:
                        #region Qty
                        
                        if (sType.Substring(0, 1) == StkTraType) return;
                        double dSQty = ProcessGeneral.GetSafeDouble(e.Value);
                        if (dSQty < 0)
                        {
                            e.Valid = false;
                            return;
                        }

                        long lPk = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(ifrow, CsPk));
                        string sBatch = ProcessGeneral.GetSafeString(gv.GetRowCellValue(ifrow, CsBatchNo));
                        double dQty = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(ifrow, CsBalance));
                        double dpri = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(ifrow, CsPri));

                        double dliQty = lPk == -1 ? dSQty : ProcessGeneral.GetSafeDouble(_dLineQty[lPk]);
                        double dBatQty = string.IsNullOrEmpty(sBatch) || !_dBatchQty.ContainsKey(sBatch)
                            ? 0
                            : ProcessGeneral.GetSafeDouble(_dBatchQty[sBatch]);

                        //double dliQty = 0;
                        //bool bOriQty = _dLineQty.TryGetValue(lPk, out dliQty);
                        //double dSQty = ProcessGeneral.GetSafeDouble(e.Value);
                        //double dBatQty;
                        //bool bBatQty = _dBatchQty.TryGetValue(sBatch, out dBatQty);

                        switch (sType.Substring(0, 1))
                        {
                            case StkRecType:

                                #region Adjust RecQty

                                double dOrderQty = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(ifrow, CsOrderQty));
                                if (_sState == Cls001Constant.StateAdd)
                                {
                                    if (dSQty > dOrderQty)
                                    {
                                        SplitPoline(dOrderQty, dSQty);
                                    }
                                }

                                if (dSQty > dliQty)
                                {
                                    e.Valid = true;
                                    gv.SetRowCellValue(ifrow, CsAmt, dSQty * dpri);
                                }
                                else
                                {
                                    Double dnewBal = dliQty - dSQty;
                                    if (dnewBal <= dBatQty)
                                    {
                                        e.Valid = true;
                                        gv.SetRowCellValue(ifrow, CsAmt, dSQty * dpri);
                                    }
                                    else
                                    {
                                        Double davQty = Math.Round(dliQty - dBatQty, 5);
                                        XtraMessageBox.Show($"Available Qty >= {davQty}");
                                        e.Valid = false;
                                    }
                                }
                                
                                #endregion

                                break;

                            case StkIssType:
                                //double doQty = _sState == Cls001Constant.StateAdd
                                //    ? ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(gv.FocusedRowHandle, CsQty))
                                //    : dliQty;
                                //double dSelBQty = GetSelQtyByBatchNo(sBatch);
                                //double dnBatQty = _sState == Cls001Constant.StateAdd
                                //    ? Math.Round(dBatQty + doQty - dSelBQty, 6)
                                //    : Math.Round(dBatQty + doQty, 6);

                                double dUsedBatQty = string.IsNullOrEmpty(sBatch) || !_dSelBatQty.ContainsKey(sBatch)
                                    ? 0
                                    : ProcessGeneral.GetSafeDouble(_dSelBatQty[sBatch]);
                                double dnBatQty = Math.Round(dBatQty + dUsedBatQty, 6);
                                double doldQty = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(ifrow, CsQty));
                                double dSelBQty = string.IsNullOrEmpty(sBatch)
                                    ? 0
                                    : GetSelQtyByBatchNo(sBatch) - (doldQty - dSQty);

                                if (lPk == -1 && ChkInputRecPo.CheckState == CheckState.Checked && string.IsNullOrEmpty(sBatch))
                                {
                                    dQty = dSQty;
                                    e.Valid = true;
                                    gv.SetRowCellValue(ifrow, CsOrderQty, dQty);
                                    gv.SetRowCellValue(ifrow, CsBalance, dQty);
                                }

                                #region Reduce IssQty

                                if (_sState == Cls001Constant.StateEdit)
                                {
                                    if (dliQty > dSQty)
                                    {
                                        e.Valid = true;
                                        gv.SetRowCellValue(ifrow, CsAmt, dSQty * dpri);
                                        break;
                                    }
                                }

                                #endregion

                                #region Adjust IssQty

                                if (dSelBQty <= dnBatQty)
                                {
                                    e.Valid = true;
                                    gv.SetRowCellValue(ifrow, CsAmt, dSQty * dpri);
                                }
                                else
                                {
                                    XtraMessageBox.Show($"Available Qty:{dnBatQty}");
                                    e.Value = dnBatQty;
                                    e.Valid = false;
                                }

                                //if (dQty < dSQty)
                                //{
                                //    if (dSQty <= dnBatQty)
                                //    {
                                //        if (dSQty - doQty > dQty)
                                //        {
                                //            if (XtraMessageBox.Show("Accept Over Quantity?", "Confirmation",
                                //                    MessageBoxButtons.YesNo,
                                //                    MessageBoxIcon.Question) == DialogResult.Yes)
                                //            {
                                //                e.Valid = true;
                                //                gv.SetRowCellValue(ifrow, CsAmt, dSQty * dpri);
                                //            }
                                //            else
                                //            {
                                //                e.Valid = false;
                                //            }
                                //        }
                                //        else
                                //        {
                                //            e.Valid = true;
                                //            gv.SetRowCellValue(ifrow, CsAmt, dSQty * dpri);
                                //        }
                                //    }
                                //    else
                                //    {
                                //        XtraMessageBox.Show($"Available Qty:{dnBatQty}");
                                //        e.Valid = false;
                                //    }
                                //}
                                //else
                                //{
                                //    if (dSQty <= dnBatQty)
                                //    {
                                //        e.Valid = true;
                                //        gv.SetRowCellValue(ifrow, CsAmt, dSQty * dpri);
                                //    }
                                //    else
                                //    {
                                //        XtraMessageBox.Show($"Available Qty:{dnBatQty}");
                                //        e.Valid = false;
                                //    }
                                //}

                                #endregion

                                break;
                        }
                        break;

                    #endregion

                    case CsPri:
                        double dQty1 = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(ifrow, CsQty));
                        double dpri1 = ProcessGeneral.GetSafeDouble(e.Value);
                        gv.SetRowCellValue(ifrow, CsAmt, dQty1 * dpri1);
                        break;

                    case CsAsnNo:
                        LoadAsnInfo(sCode, ifrow);
                        break;

                    case CsGw:
                        LoadGwInfo(ProcessGeneral.GetSafeDecimal(e.Value), ifrow);
                        break;

                    case CsLocation:
                        e.Valid = CheckLocation(e.Value.ToString().Trim());
                        break;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(@"Error on Grid\ValidatingEditor", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SplitPoline(double dOrderQty, double dRecQty)
        {
            var dtS = gcuStkVae.DataSource as DataTable;
            if (dtS == null || dtS.Rows.Count == 0) return;
            dtS.AcceptChanges();

            var gv = gcuStkVae.FocusedView as GridView;
            DataRow dr = gv.GetDataRow(gv.FocusedRowHandle);
            dr[CsQty] = dOrderQty;
            DataRow dr1 = dtS.NewRow();

            for (int i = 0; i < gv.Columns.Count; i++)
            {
                dr1[i] = dr[i];
            }

            dr1[CsPk] = -1;
            dr1[CsQty] = dRecQty - dOrderQty;
            dr1[CsPri] = 0;
            dr1[CsRem] = "FOC";

            dtS.Rows.InsertAt(dr1, gv.FocusedRowHandle + 1);
        }

        private bool CheckLocation(string sLoc)
        {
            var vlo = _dtLocation.AsEnumerable()
                .Where(w => ProcessGeneral.GetSafeString(w["Code"]) == sLoc)
                .Select(s => s)
                .ToList();

            return vlo.Any();
        }

        private void GvuStkVae_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null || gv.RowCount == 0) return;
            string fieldName = e.Column.FieldName;
            string sCode = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, CsItemCode));
            string sAsnCode = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, CsAsnNo));

            try
            {
                switch (fieldName)
                {
                    case CsGw:
                        if (ChkInputRecPo.CheckState == CheckState.Checked && gv.IsLastRow)
                        {
                            if (!string.IsNullOrEmpty(sCode)) AddNewEmptyLine();
                        }
                        break;

                    case CsRem:
                        if (ChkInputRecPo.CheckState == CheckState.Checked && gv.IsLastRow &&
                            _sType == ClsWhConstant.ConStkFgIssRe)
                        {
                            if (!string.IsNullOrEmpty(sCode) && !string.IsNullOrEmpty(sAsnCode)) AddNewEmptyLine();
                        }
                        break;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(@"Error on Grid\CellValueChanged", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GvuStkVae_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null || gv.RowCount == 0) return;
            string fieldName = e.Column.FieldName;
            
            try
            {
                switch (fieldName)
                {
                    case CsSel:
                        int iSel = ProcessGeneral.GetSafeBoolInt(e.Value);
                        if (iSel == 0)
                        {
                            gv.SetRowCellValue(e.RowHandle, CsQty, 0);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(@"Error on Grid\CellValueChanging", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Fn_Save

        private bool CheckIssuedQty()
        {
            var dtDetails = (DataTable)gcuStkVae.DataSource;
            if (dtDetails == null || dtDetails.Rows.Count == 0) return false;

            DataTable dtChk = _infStk001.DtCheckBalanceQtylessthanIssuedQty_App_00002(_lPkey);
            if (dtChk == null || dtChk.Rows.Count == 0) return true;

            dtDetails.AsEnumerable()
                .Where(w => ProcessGeneral.GetSafeInt(w[CsValidBatch]) == 0).ToList()
                .ForEach(dr => dr[CsValidBatch] = 1);

            dtDetails.AsEnumerable()
                .Join(dtChk.AsEnumerable(),
                    d => ProcessGeneral.GetSafeString(d[CsBatchNo]),
                    c => ProcessGeneral.GetSafeString(c["BatchNo"]),
                    (d, c) => d).ToList()
                .ForEach(dr =>
                {
                    dr[CsValidBatch] = 0;
                    dr[CsStkQty] = 0;
                });

            XtraMessageBox.Show("Invalid Balance Quantity!");
            return false;
        }

        private bool CheckBeforeSave()
        {
            long iCnyMf004Pks = ProcessGeneral.GetSafeInt64(sleFrwh.EditValue);
            if (iCnyMf004Pks == 0)
            {
                XtraMessageBox.Show("Invalid From WareHouse!");
                return false;
            }

            long iCnyMf004Pkd = ProcessGeneral.GetSafeInt64(sleTowh.EditValue);
            if (iCnyMf004Pkd == 0)
            {
                XtraMessageBox.Show("Invalid To WareHouse!");
                return false;
            }

            if (_sType == ClsWhConstant.ConStkRmRecRe ||
                _sType == ClsWhConstant.ConStkRmRecPo ||
                _sType == ClsWhConstant.ConStkFgRecRe ||
                _sType == ClsWhConstant.ConStkFgRecSo)
            {
                //ChkOfficialDoc.CheckState = CheckState.Checked;
            }

            if (_sType == ClsWhConstant.ConStkFgIssRe ||
                _sType == ClsWhConstant.ConStkFgIssSo) return CheckIssuedQty();

            return true;
        }

        private void GetDistinctOrderNo()
        {
            try
            {
                var dtLine = (DataTable) gcuStkVae.DataSource;
                if (dtLine == null || dtLine.Rows.Count == 0) return;
                dtLine.AcceptChanges();

                var vOrder = dtLine.AsEnumerable()
                    .Where(w => string.IsNullOrEmpty(ProcessGeneral.GetSafeString(w[CsReqNo])) == false
                                && ProcessGeneral.GetSafeDouble(w[CsQty]) > 0)
                    .Select(s => new {Order_No = ProcessGeneral.GetSafeString(s[CsReqNo])}).Distinct();

                string sOrderNo = vOrder.Aggregate("", (current, item) => current + $"{item.Order_No.Trim()}, ");

                if (sOrderNo.Trim().Length > 0)
                {
                    sOrderNo = sOrderNo.Substring(0, sOrderNo.Trim().Length - 1);
                    sOrderNo = sOrderNo.Replace("'", "");
                }

                txtDescription.EditValue = sOrderNo;
            }
            catch (Exception)
            {
                MessageBox.Show(@"Occur on Void Get_Distinct_OrderNo.!");
            }
        }

        public bool SaveHeader()
        {
            if (!CheckBeforeSave()) return false;
            #region Para

            if (_sState == Cls001Constant.StateAdd)
            {
                #region Check Voucher ID.

                _sSysCodeId.ArrData = new List<CodeRuleFieldData>()
                {
                    new CodeRuleFieldData
                    {
                        TableFieldSql = CsPk,
                        StringValue = txtId.Text.Trim()
                    },
                };
                CodeRuleReturnSave sCoidRe = _sSysCodeId.SaveCodeData();

                if (!sCoidRe.IsSusccess)
                {
                    XtraMessageBox.Show("New ID will be Generated! ", "Warning!"
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                txtId.EditValue = _sSysCodeId.CodeRuleData.StrCode;
                _lPkey = ProcessGeneral.GetSafeInt64(txtId.EditValue);

                #endregion

                #region Check Voucher No.

                _sSysCodeNo.ArrData = new List<CodeRuleFieldData>()
                {
                    new CodeRuleFieldData
                    {
                        TableFieldSql = SCompCode,
                        StringValue = DeclareSystem.SysCompanyCode
                    },
                };
                CodeRuleReturnSave sConore = _sSysCodeNo.SaveCodeData();
                if (!sConore.IsSusccess)
                {
                    XtraMessageBox.Show("New Vourcher No. will be Generated!", "Warning!"
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                txtVoucherNo.EditValue = _sSysCodeNo.CodeRuleData.StrCode;

                #endregion
            }
            else
            {
                dtDeleted = ((DataTable)gcuStkVae.DataSource).GetChanges(DataRowState.Deleted);
            }

            long iPk = _lPkey;
            txtId.EditValue = _lPkey;
            GetDistinctOrderNo();

            long iCnyMf026Pk = ProcessGeneral.GetSafeInt64(sleType.EditValue);
            string s001 = ProcessGeneral.GetSafeString(txtVoucherNo.EditValue);
            DateTime d002 = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(txtDateIss.EditValue);
            string s003 = ProcessGeneral.GetSafeString(txtDescription.EditValue);
            long i004 = ProcessGeneral.GetSafeInt64(sleSource.EditValue);
            long i005 = ProcessGeneral.GetSafeInt64(sleDestination.EditValue);
            string s006 = ProcessGeneral.GetSafeString(txtOrderNo.EditValue);
            DateTime d007 = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(txtCrdate.EditValue);
            string s008 = ProcessGeneral.GetSafeString(txtCrby.EditValue);
            DateTime d009 = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(ProcessGeneral.GetServerDate());
            string s010 = ProcessGeneral.GetSafeString(DeclareSystem.SysUserName);
            string s011 = ChkOfficialDoc.CheckState==CheckState.Checked ? "1" : "0";
            int i012 = ProcessGeneral.GetSafeInt(txtVer.EditValue);
            long iCnyMf004Pks = ProcessGeneral.GetSafeInt64(sleFrwh.EditValue);
            long iCnyMf004Pkd = ProcessGeneral.GetSafeInt64(sleTowh.EditValue);
            string sComp = ProcessGeneral.GetSafeString(DeclareSystem.SysCompanyCode);

            #endregion

            return _infStk001.InsertHeader_CNY00104_00106(iPk, iCnyMf026Pk, s001, d002, s003
                , i004, i005, s006, d007, s008, d009, s010, s011, i012, iCnyMf004Pks, iCnyMf004Pkd, sComp);
        }

        public void SaveDetailswithLocation()
        {
            try
            {
                var dtDetails = (DataTable)gcuStkVae.DataSource;
                if (dtDetails == null || dtDetails.Rows.Count == 0) return;
                if (_sState != Cls001Constant.StateAdd)
                {
                    //DeleteDetails();
                    _infStk001.DeleteDetails_App_CNY1056_00109(_lPkey, 0);
                }

                string[] sTypeDoc = sleType.Text.Trim().Split(new char[] { '-' });
                string sVoucherType = ProcessGeneral.GetSafeString(sTypeDoc[0]);

                #region Save Data

                dtDetails.AcceptChanges();

                var dtLocation = dtDetails.AsEnumerable()
                    .Where(w => (ProcessGeneral.GetSafeString(w[CsAsnNo]) != "" ||
                                 ProcessGeneral.GetSafeString(w[CsLocation]) != "") &&
                                ProcessGeneral.GetSafeDouble(w[CsQty]) > 0)
                    .Select(s => new
                    {
                        ASN_No = s[CsAsnNo],
                        Carton_No = s[CsCartonNo],
                        Location = s[CsLocation],
                        ConfDate = s[CsConfirmedDate],
                        ConfBy = s[CsConfirmedBy],
                        GW = s[CsGw],
                        CNY00105APK = -1 //s[Sh8ColCny00105APk]
                    }).Distinct().CopyToDataTableNew();

                if (dtLocation == null || dtLocation.Rows.Count == 0)
                {
                    SaveCny00105_106(dtDetails, sVoucherType);
                }
                else
                {
                    foreach (DataRow dr in dtLocation.Rows)
                    {
                        long lC105APk = ProcessGeneral.GetSafeInt64(dr[Sh8ColCny00105APk]);
                        long lCny00105APk = lC105APk > 0 ? lC105APk : ProcessGeneral.GetNextId("CNY00105A");

                        string sAsnno = ProcessGeneral.GetSafeString(dr["ASN_No"]);
                        string sCartonNo = ProcessGeneral.GetSafeString(dr["Carton_No"]);
                        string sLocation = ProcessGeneral.GetSafeString(dr[CsLocation]);
                        DateTime tConfDate = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(dr["ConfDate"]);
                        string sConfUser = ProcessGeneral.GetSafeString(dr["ConfBy"]);
                        string sConfBy = string.IsNullOrEmpty(sConfUser) ? DeclareSystem.SysUserName : sConfUser;
                        decimal mGw = ProcessGeneral.GetSafeDecimal(dr[CsGw]);

                        _infStk001.InsertCarton_CNY00105A_App_00107(lCny00105APk, _lPkey, sAsnno
                            , sCartonNo, sLocation, tConfDate, sConfBy, mGw);

                        IEnumerable<DataRow> drDet = dtDetails.AsEnumerable().Where(w =>
                                ProcessGeneral.GetSafeString(w[CsAsnNo]) == sAsnno &&
                                ProcessGeneral.GetSafeString(w[CsCartonNo]) == sCartonNo &&
                                ProcessGeneral.GetSafeString(w[CsLocation]) == sLocation)
                            .Select(s => s);
                        DataTable dta = drDet.CopyToDataTable<DataRow>();
                        dta.AsEnumerable().ToList().ForEach(rw =>
                        {
                            rw[Sh8ColCny00105APk] = lCny00105APk;
                        });

                        SaveCny00105_106(dta, sVoucherType);
                    }

                    #region Rows without Location

                    IEnumerable<DataRow> drDetwol = dtDetails.AsEnumerable().Where(w =>
                            string.IsNullOrEmpty(ProcessGeneral.GetSafeString(w[CsAsnNo])) &&
                            string.IsNullOrEmpty(ProcessGeneral.GetSafeString(w[CsCartonNo])) &&
                            string.IsNullOrEmpty(ProcessGeneral.GetSafeString(w[CsLocation])))
                        .Select(s => s);
                    DataTable dtawol = drDetwol.CopyToDataTable<DataRow>();
                    dtawol.AsEnumerable().ToList().ForEach(rw =>
                    {
                        rw[Sh8ColCny00105APk] = 0;
                    });

                    SaveCny00105_106(dtawol, sVoucherType);

                    #endregion
                }

                #endregion

                if (ChkOfficialDoc.CheckState == CheckState.Checked)
                {
                    _infStk001.UpdateBalanceStockQty_CNY00100BC_App_00157(_lPkey);
                }
                ChangePoStatus();

                _sState = Cls001Constant.StateEdit;
                XtraMessageBox.Show("Voucher Updated Successfully!");
            }
            catch (Exception)
            {
                XtraMessageBox.Show("UctrlStockVoucherAe-SaveDetails");
            }
        }
        
        private void SaveCny00105_106(DataTable dtLine, string sType)
        {
            for (int i = 0; i < dtLine.Rows.Count; i++)
            {
                double l003Qty = ProcessGeneral.GetSafeDouble(dtLine.Rows[i][CsQty]);
                if (l003Qty <= 0) continue;

                #region Paras Details

                long iItemPk = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh3ColTdg00001Pk]);
                //long iPk = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][CsPk]);
                //bool bAdj = iPk >= 0;
                //long i105Pk = bAdj ? iPk : ProcessGeneral.GetNextId("CNY00105");

                bool bAdj = false;
                long i105Pk = ProcessGeneral.GetNextId("CNY00105");
                long iCny00105Pk = bAdj ? ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh7ColCny00105Pk]) : i105Pk;
                long iCny00105APk = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh8ColCny00105APk]);
                long iReqLinid = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh4ColCny00102Pk]);
                long iWhfid = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh1ColCnymf004Pkf]);
                long iWhtid = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh2ColCnymf004Pkt]);
                DateTime d001TransDate = Cls001MasterFiles.GetServerDate();
                string s002BatchNo = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsBatchNo]);
                double l004Pri = ProcessGeneral.GetSafeDouble(dtLine.Rows[i][CsPri]);
                string[] s005Curr = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsCurr]).Split(new char[] { '-' });
                int i008Whiss = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsStatus]) ==
                                ClsWhConstant.StrStatusComplete ? 1 : 0;
                double l009Factor = ProcessGeneral.GetSafeDouble(dtLine.Rows[i][CsFactor]);
                double l010StkuQty = l009Factor * l003Qty;
                string s011DocNo = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsDocRef]);
                string s012Rem = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsRem]);
                string sCny00050Pk = ProcessGeneral.GetSafeString(dtLine.Rows[i][Sh6ColCny00050Pk]);
                bool bSaveBatch = false;

                long lWhd = 0;
                switch (sType)
                {
                    case ClsWhConstant.ConStkRmRecPo:
                    case ClsWhConstant.ConStkFgRecSo:
                    case ClsWhConstant.ConStkRmRecRe:
                    case ClsWhConstant.ConStkFgRecRe:
                        lWhd = iWhtid;
                        if (string.IsNullOrEmpty(s002BatchNo))
                        {
                            s002BatchNo = GetBatchNo();
                        }

                        bSaveBatch = !bAdj;
                        break;
                    default:
                        lWhd = iWhfid;
                        break;
                }

                #endregion

                if (string.IsNullOrEmpty(s002BatchNo)) continue;

                #region Rate & Base Currency

                string s006BaseCurr = "02";
                decimal d007Rate = 0;

                var vRate = _dtRate.AsEnumerable().Where(w =>
                        ProcessGeneral.GetSafeString(w["FrCur"]) == s005Curr[0].Trim() &&
                        ProcessGeneral.GetSafeString(w["ToCur"]) == s006BaseCurr)
                    .Select(s => new { mRate = ProcessGeneral.GetSafeDecimal(s["Rate"]) })
                    .ToList();

                foreach (var vva in vRate)
                {
                    d007Rate = ProcessGeneral.GetSafeDecimal(vva.mRate);
                }

                #endregion

                _infStk001.InsertDetails_CNY00105_00107(i105Pk, _lPkey, iItemPk, iReqLinid
                    , lWhd, d001TransDate, s002BatchNo, l003Qty, l004Pri, s005Curr[0]
                    , s006BaseCurr, d007Rate, i008Whiss, l009Factor, l010StkuQty, s011DocNo
                    , s012Rem, iCny00105Pk, iCny00105APk);

                SaveAttributes(dtLine, i, bAdj, i105Pk);

                #region Transfer

                if (sType == ClsWhConstant.ConStkRmTransRe
                    || sType == ClsWhConstant.ConStkRmTransNoRe
                    || sType == ClsWhConstant.ConStkFgTransRe
                    || sType == ClsWhConstant.ConStkFgTransNoRe)
                {
                    long it105Pk = bAdj
                        ? iCny00105Pk
                        : ProcessGeneral.GetNextId("CNY00105");

                    string st002BatchNo = bAdj
                        ? ProcessGeneral.GetSafeString(dtLine.Rows[i][CsBatchNo])
                        : GetBatchNo();
                    lWhd = iWhtid;

                    _infStk001.InsertDetails_CNY00105_00107(it105Pk, _lPkey, iItemPk, iReqLinid
                        , lWhd, d001TransDate, st002BatchNo, l003Qty, l004Pri, s005Curr[0], s006BaseCurr
                        , d007Rate, i008Whiss, l009Factor, l010StkuQty, s011DocNo, s012Rem, i105Pk, iCny00105APk);

                    SaveAttributes(dtLine, i, bAdj, it105Pk);
                }

                #endregion
            }
        }

        private string GetBatchNo()
        {
            int iComp = ProcessGeneral.GetSafeInt(DeclareSystem.SysCompanyCode);
            _sSysBatchid.ArrData = new List<CodeRuleFieldData>()
            {
                new CodeRuleFieldData
                {
                    TableFieldSql = SBatchComp,
                    StringValue = iComp.ToString()
                },
            };
            _sSysBatchid.CreateCodeString();
            CodeRuleReturnSave sBatchreid = _sSysBatchid.SaveCodeData();
            return ProcessGeneral.GetSafeString(_sSysBatchid.CodeRuleData.StrCode);
        }

        private void SaveAttributes(DataTable dtLine, int iRow, bool bAdj, long i105Pk)
        {
            try
            {
                int fCol = dtLine.Columns[CsItemSize].Ordinal + 1;
                int tCol = dtLine.Columns[CsReqUnit].Ordinal;

                for (int j = fCol; j < tCol; j++)
                {
                    string sColVas = ProcessGeneral.GetSafeString(dtLine.Rows[iRow][j]);
                    string[] sColVa = sColVas.Split(new char[] { '-' });

                    if (!string.IsNullOrEmpty(sColVa[0]))
                    {
                        string sColFis = ProcessGeneral.GetSafeString(dtLine.Columns[j].ColumnName);
                        string[] sColFi = sColFis.Split(new char[] { '-' });
                        long i008Pk = ProcessGeneral.GetSafeInt64(sColFi[0]);

                        long lAttPk = bAdj ? 0 : ProcessGeneral.GetNextId("CNY00106");
                        string sUnitCode;
                        bool sUnitStr = _dunit.TryGetValue(sColVa[1], out sUnitCode);

                        _infStk001.InsertAttributes_CNY00106_00108(lAttPk, i105Pk, i008Pk, sColVa[0], sUnitCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"SaveAttributes_Error: {ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ChangePoStatus()
        {
            string[] sTypeDoc = sleType.Text.Trim().Split(new char[] { '-' });
            string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);
            if (sType == ClsWhConstant.ConStkRmRecPo)
            {
                string sVouk = ProcessGeneral.GetSafeString(_lPkey);
                _infStk001.UpdateGrninPoStatus_CNY061_00165(sVouk);
            }
        }

        public void SaveDetails()
        {
            try
            {
                var dtLine = (DataTable) gcuStkVae.DataSource;
                if (dtLine == null || dtLine.Rows.Count == 0) return;
                if (_sState != Cls001Constant.StateAdd)
                {
                    //DeleteDetails();
                }

                string[] sTypeDoc = sleType.Text.Trim().Split(new char[] {'-'});
                string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);

                #region Save Data

                dtLine.AcceptChanges();
                for (int i = 0; i < dtLine.Rows.Count; i++)
                {
                    #region Paras Details

                    long iItemPk = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh3ColTdg00001Pk]);
                    long iPk = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][CsPk]);
                    bool bAdj = iPk >= 0;
                    long i105Pk = bAdj ? iPk : ProcessGeneral.GetNextId("CNY00105");
                    long iCny00105Pk = bAdj ? ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh7ColCny00105Pk]) : i105Pk;

                    long iCny00105APk = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh8ColCny00105APk]);
                    long iReqLinid = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh4ColCny00102Pk]);
                    long iWhfid = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh1ColCnymf004Pkf]);
                    long iWhtid = ProcessGeneral.GetSafeInt64(dtLine.Rows[i][Sh2ColCnymf004Pkt]);
                    DateTime d001TransDate = Cls001MasterFiles.GetServerDate();
                    string s002BatchNo = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsBatchNo]);
                    double l003Qty = ProcessGeneral.GetSafeDouble(dtLine.Rows[i][CsQty]);
                    double l004Pri = ProcessGeneral.GetSafeDouble(dtLine.Rows[i][CsPri]);
                    string[] s005Curr = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsCurr]).Split(new char[] {'-'});
                    int i008Whiss = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsStatus]) ==
                                    ClsWhConstant.StrStatusComplete ? 1 : 0;
                    double l009Factor = ProcessGeneral.GetSafeDouble(dtLine.Rows[i][CsFactor]);
                    double l010StkuQty = l009Factor * l003Qty;
                    string s011DocNo = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsDocRef]);
                    string s012Rem = ProcessGeneral.GetSafeString(dtLine.Rows[i][CsRem]);
                    string sCny00050Pk = ProcessGeneral.GetSafeString(dtLine.Rows[i][Sh6ColCny00050Pk]);
                    bool bSaveBatch = false;

                    long lWhd = 0;
                    switch (sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkRmRecRe:
                        case ClsWhConstant.ConStkFgRecRe:
                            lWhd = iWhtid;
                            if (string.IsNullOrEmpty(s002BatchNo))
                            {
                                s002BatchNo = GetBatchNo();
                            }

                            bSaveBatch = !bAdj;
                            break;
                        default:
                            lWhd = iWhfid;
                            break;
                    }

                    #endregion

                    if (string.IsNullOrEmpty(s002BatchNo)) continue;
                    #region Rate & Base Currency

                    string s006BaseCurr = "02";
                    decimal d007Rate = 0;

                    var vRate = _dtRate.AsEnumerable().Where(w =>
                            ProcessGeneral.GetSafeString(w["FrCur"]) == s005Curr[0].Trim() &&
                            ProcessGeneral.GetSafeString(w["ToCur"]) == s006BaseCurr)
                        .Select(s => new { mRate = ProcessGeneral.GetSafeDecimal(s["Rate"]) })
                        .ToList();

                    foreach (var vva in vRate)
                    {
                        d007Rate = ProcessGeneral.GetSafeDecimal(vva.mRate);
                    }

                    #endregion

                    //_infStk001.InsertDetails_CNY00105_00107(i105Pk, _lPkey, iItemPk, iReqLinid
                    //    , lWhd, d001TransDate, s002BatchNo, l003Qty, l004Pri, s005Curr[0]
                    //    , s006BaseCurr, d007Rate, i008Whiss, l009Factor, l010StkuQty, s011DocNo
                    //    , s012Rem, iCny00105Pk, iCny00105APk);

                    //SaveAttributes(dtLine, i, bAdj, i105Pk);

                    //long i100Pk = bAdj ? iPk : ProcessGeneral.GetNextId("CNY00100");
                    //string sComp = ProcessGeneral.GetSafeString(DeclareSystem.SysCompanyCode);
                    //_infStk001.InsertBatchDetails_CNY00100_00157(i100Pk, iItemPk, lWhd
                    //    , sCny00050Pk, l002BatchNo, 0, 0, l004Pri, sComp);
                    //if (bSaveBatch) SaveBatchAttributes(dtLine, i, i100Pk);

                    #region Transfer

                    if (sType == ClsWhConstant.ConStkRmTransRe || sType == ClsWhConstant.ConStkRmTransNoRe
                                                               || sType == ClsWhConstant.ConStkFgTransRe ||
                                                               sType == ClsWhConstant.ConStkFgTransNoRe)
                    {
                        long it105Pk = bAdj ? iCny00105Pk : ProcessGeneral.GetNextId("CNY00105");
                        string st002BatchNo = bAdj ? ProcessGeneral.GetSafeString(dtLine.Rows[i][CsBatchNo]) : GetBatchNo();
                        lWhd = iWhtid;

                        //_infStk001.InsertDetails_CNY00105_00107(it105Pk, _lPkey, iItemPk, iReqLinid
                        //    , lWhd, d001TransDate, st002BatchNo, l003Qty, l004Pri, s005Curr[0], s006BaseCurr
                        //    , d007Rate, i008Whiss, l009Factor, l010StkuQty, s011DocNo, s012Rem, i105Pk, iCny00105APk);

                        //SaveAttributes(dtLine, i, bAdj, it105Pk);

                        //long it100Pk = bAdj ? it105Pk : ProcessGeneral.GetNextId("CNY00100");
                        //_infStk001.InsertBatchDetails_CNY00100_00157(it100Pk, iItemPk, iWhtid
                        //    , sCny00050Pk, lt002BatchNo, 0, 0, l004Pri, sComp);
                        //if (!bAdj) SaveBatchAttributes(dtLine, i, it100Pk);
                    }

                    #endregion
                }

                #endregion

                _sState = Cls001Constant.StateEdit;
                XtraMessageBox.Show("Saving Finished!");
            }
            catch (Exception)
            {
                XtraMessageBox.Show("UctrlStockVoucherAe-SaveDetails");
            }
        }

        private void DeleteDetails()
        {
            try
            {
                string sDelPk = "0";
                if (dtDeleted != null && dtDeleted.Rows.Count > 0)
                {
                    sDelPk = dtDeleted.Rows.Cast<DataRow>().Aggregate(sDelPk, (current, drDel)
                        => current + string.Format(",{0}", ProcessGeneral.GetSafeInt64(drDel[CsPk, DataRowVersion.Original])));
                    _infStk001.DeleteDetails_CNY1056_00109(sDelPk);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Delete_Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveBatchAttributes(DataTable dtLine, int iRow, long i100Pk)
        {
            try
            {
                int fCol = dtLine.Columns[CsItemSize].Ordinal + 1;
                int tCol = dtLine.Columns[CsReqUnit].Ordinal;

                for (int j = fCol; j < tCol; j++)
                {
                    string sColVas = ProcessGeneral.GetSafeString(dtLine.Rows[iRow][j]);
                    string[] sColVa = sColVas.Split(new char[] {'-'});

                    if (!string.IsNullOrEmpty(sColVa[0]))
                    {
                        string sColFis = ProcessGeneral.GetSafeString(dtLine.Columns[j].ColumnName);
                        string[] sColFi = sColFis.Split(new char[] {'-'});
                        long i008Pk = ProcessGeneral.GetSafeInt64(sColFi[0]);

                        string sUnitCode;
                        bool sUnitStr = _dunit.TryGetValue(sColVa[1], out sUnitCode);
                        _infStk001.InsertBatchAttributes_CNY00100B_00158(i100Pk, i008Pk, sColVa[0], sUnitCode);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($@"SaveBatchAttributes_Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Scan PO

        private void ScanPo(string sCondid)
        {
            #region Paras
            
            long lfWh = ProcessGeneral.GetSafeInt64(sleFrwh.EditValue);
            long ltWh = ProcessGeneral.GetSafeInt64(sleTowh.EditValue);
            
            if (_sType == string.Empty)
            {
                XtraMessageBox.Show("Invalid Type Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
                sleType.Focus();
                return;
            }

            if (lfWh == 0)
            {
                XtraMessageBox.Show("Invalid WareHouse Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
                sleFrwh.Focus();
                return;
            }

            if (ltWh == 0)
            {
                XtraMessageBox.Show("Invalid WareHouse Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
                sleTowh.Focus();
                return;
            }

            if (!AvailablePo(sCondid))
            {
                txtCondId.EditValue = "";
                return;
            }

            #endregion

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable dtPo = _infStk001.GenerateStkVoucherRecPo00102R(_sType, sCondid, "", lfWh, ltWh, 0);
                GenerateVoucherDetails(_sType, dtPo, true);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
                XtraMessageBox.Show($"Error on Void_ScanPO", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AvailablePo(string sPono)
        {
            long lSource = ProcessGeneral.GetSafeInt64(SlueSource.EditValue);
            string sText = $"SELECT PK, CNY00002PK, CNYMF102PK FROM CNY00060 WHERE PK in ({sPono})";
            DataTable dtS = Cls001MasterFiles.ExcecuteSql(sText);
            if (dtS == null || dtS.Rows.Count == 0)
            {
                XtraMessageBox.Show("Invalid PONo.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int iSup = dtS.AsEnumerable().Select(s => ProcessGeneral.GetSafeInt64(s["CNY00002PK"])).Distinct().Count();
            long lSou = ProcessGeneral.GetSafeInt64(dtS.Rows[0][1]);
            long lDes = ProcessGeneral.GetSafeInt64(dtS.Rows[0][2]);

            if (iSup==1)
            {
                SlueSource.EditValue = lSou;
                SlueDestination.EditValue = lDes;
            }
            else
            {
                XtraMessageBox.Show(
                    "The system does not allow to have more than one supplier per good receipt notes. Enter or scan another PO to continue.",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void GenerateVoucherDetails(string sType, DataTable dtLine, bool pAddNew)
        {
            try
            {
                var dtSrc = (DataTable)gcuStkVae.DataSource;

                if (!pAddNew)
                {
                    dtSrc = dtLine;
                }
                else
                {
                    if (dtSrc == null || dtSrc.Rows.Count == 0)
                    {
                        dtSrc = dtLine;
                    }
                    else
                    {
                        #region Filter Data

                        dtSrc.AcceptChanges();
                        var dNew = (from t1 in dtLine.AsEnumerable()
                                    join t2 in dtSrc.AsEnumerable()
                                    on new { A = ProcessGeneral.GetSafeInt64(t1[Sh4ColCny00102Pk]) }
                                    equals new { A = ProcessGeneral.GetSafeInt64(t2[Sh4ColCny00102Pk]) }
                                    into t3
                                    from t4 in t3.DefaultIfEmpty()
                                    where t4 == null
                                    select t1).ToList();

                        if (dNew.Any())
                        {
                            dtLine = dNew.CopyToDataTable();
                        }
                        else
                        {
                            dtLine.Rows.Clear();
                        }

                        #endregion
                        foreach (DataRow dr in dtLine.Rows)
                        {
                            dtSrc.ImportRow(dr);
                        }
                    }
                }

                gcuStkVae.DataSource = dtSrc;
                gvuStkVae.BestFitColumns();
                HideCols(sType);
                FormatGridCols();

                txtCondId.EditValue = "";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(@"Error on Void_ScanPO " + ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
