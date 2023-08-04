using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.WForm;
using CNY_BaseSys.Common;
using CNY_WH.Info;
using CNY_WH.Common;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using CNY_BaseSys;
using CNY_WH.Class;
using CNY_WH.UControl;
using CNY_WH.Properties;
using CNY_WH.Report;
using DevExpress.Utils;

namespace CNY_WH.WForm
{
    public partial class FrmWHStockVoucher : FrmBase
    {
        #region Declaration

        private UctrlWHStockVoucher _uctrlSvm;
        private UctrlWHStockVoucherAe _uctrlSvae;
        private readonly InfoWh00001 _inf001Sv = new InfoWh00001();
        private DropDownButton _ddbExportFiles;

        GridView _gvStockV;
        GridView _gvStockVae;
        GridControl _gcstockVae;
        private CheckEdit _ChkInput;

        private string _sType;
        private DataTable _dtDetails;
        private InfoWhMf _infWmf;
        
        private bool _pGenAddNew;
        public static bool BIns;
        public static bool BUpd;
        public static bool BDel;
        public static bool BVie;
        public static DataTable DtAdvanceFunction;

        #endregion

        #region Constant

        private const string CsVoucherNo = "Voucher No";
        private const string CsOfficial = "Official";
        private const string CsStatus = "Status(F4)";
        private const string CsVersion = "Version";
        private const string CsReqNo = "Document No";
        private const string CsItemCode = "Item Code";
        private const string CsItemName = "Item Name";
        private const string CsItemColor = "Color";

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
        private const string CsDocTyp = "Document Type";

        private const string CsAsnNo = "ASN No";
        private const string CsCartonNo = "Carton No";
        private const string CsLocation = "Location";
        private const string CsGw = "GW";
        private const string CsConfirmedDate = "Confirmed Date";
        private const string CsConfirmedBy = "Confirmed By";
        private const string CsTransDate = "Trans.Date";

        private const string CsH1Col = "CNYMF004PKF";
        private const string CsH2Col = "CNYMF004PKT";
        private const string CsH3Col = "TDG00001PK";
        private const string CsH4Col = "CNY00102PK";
        private const string CsH5Col = "CNY00101PK";
        private const string CsH6Col = "CNY00050PK";
        private const string CsH7Col = "CNY00105PK";
        private const string CsH8Col = "CNY00105APK";
        private const string CsPk = "PK";

        private const int CiFmain = 0;
        private const int CiFae = 1;

        #endregion

        #region Scala Menu

        private const string ScaStockRecFile = "Import Stock Received File.";
        private const string ScaGoodReceiptNote = "Import Delivery Note(GRN)";
        private const string ScaStockIssFile = "Import Stock Issued File.";

        private const string ScaSo01File = "Import SO Layout 01:MO->SO with Batch Info.";
        private const string ScaSo06File = "Import SO Layout 06:MO->SO without Batch Info.";
        private const string ScaSo09File = "Import SO Layout 09:Finish Good";

        #endregion

        #region Init Form

        public FrmWHStockVoucher()
        {
            _infWmf= new InfoWhMf();
            InitializeComponent();

            _dtDetails = new DataTable();
            this.Load += Frm001StockVoucher_Load;
        }

        private void Frm001StockVoucher_Load(object sender, EventArgs e)
        {
            BIns = PerIns;
            BUpd = PerUpd;
            BDel = PerDel;
            BVie = PerViw;
            DtAdvanceFunction = DtPerFunction;
            HideMenuBonForm(CiFmain, 0);

            string sfrName = Cls001MasterFiles.GetFormNameFromMenuCode(this.MenuCode);
            this.Text = sfrName;

            _sType = _infWmf.GetStockDocType(this.MenuCode);
            _uctrlSvm = new UctrlWHStockVoucher(_sType)
            {
                Dock = DockStyle.Fill,
                Name = "UctrlSvm"
            };

            PanConStockVoucher.Controls.Add(_uctrlSvm);
            _gvStockV = _uctrlSvm.GvMainP;
            _gvStockV.DoubleClick += _gvStockV_DoubleClick;
            _gvStockV.KeyDown += _gvStockV_KeyDown;
            _ddbExportFiles = _uctrlSvm.DdbImportiScala;
            _ddbExportFiles.ArrowButtonClick += _ddbExportFiles_ArrowButtonClick;
            _ddbExportFiles.Click += _ddbExportFiles_Click;
        }
        
        private void HideMenuBonForm(int iLevel, long lKey)
        {
            if (iLevel == 0)
            {
                AllowAdd = true;
                AllowEdit = true;
                AllowDelete = true;
                AllowRefresh = true;
                AllowFind = true;
                AllowPrint = true;
                AllowClose = true;

                EnableAdd = BIns;
                EnableEdit = BIns;
                EnableDelete = BDel;
                EnableRefresh = true;
                EnableFind = true;
                EnablePrint = true;
                EnableClose = true;

                AllowSave = false;
                AllowCancel = false;
                AllowBreakDown = false;
                AllowRangeSize = false;
                AllowGenerate = false;
                AllowCombine = false;
                AllowCheck = false;
                AllowCopyObject = false;
                AllowRevision = false;
                AllowCollapse = false;
                AllowExpand = false;
            }
            else
            {
                AllowSave = true;
                AllowGenerate = true;
                AllowRefresh = true;
                AllowPrint = true;
                AllowClose = true;
                
                int iOff = lKey > 0
                    ? ProcessGeneral.GetSafeInt(_gvStockV.GetRowCellValue(_gvStockV.FocusedRowHandle, CsOfficial))
                    : 0;

                EnableSave = BIns & iOff == 0;
                EnableCancel = BIns;
                EnableGenerate = true;
                //EnableDelete = BDel;
                EnableRefresh = true;
                EnablePrint = true;
                EnableClose = true;

                AllowCancel = false;
                AllowAdd = false;
                AllowEdit = false;
                AllowDelete = false;
                AllowFind = false;
                AllowBreakDown = false;
                AllowRangeSize = false;
                AllowCombine = false;
                AllowCheck = false;
                AllowCopyObject = false;
                AllowRevision = false;
                AllowCollapse = false;
                AllowExpand = false;
            }
        }

        #endregion

        #region Grid_Keydown

        private void _gvStockV_KeyDown(object sender, KeyEventArgs e)
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

        private void _gvStockV_DoubleClick(object sender, EventArgs e)
        {
            PerformEdit();
        }

        #endregion

        #region Fn_Add, Edit, Cancel & Close

        private void AddUserControlMainAe(string state, long lKey)
        {
            _uctrlSvae = new UctrlWHStockVoucherAe(state, _sType, lKey)
            {
                Dock = DockStyle.Fill,
                Name = "UctrlSvae",
            };
            PanConStockVoucher.Controls.Add(_uctrlSvae);
            _gvStockVae = _uctrlSvae.GvStkVae;
            _gcstockVae = _uctrlSvae.GcStkVae;
            _ChkInput = _uctrlSvae.ChkInput;
        }

        private void RemoveUserControlMainAE(Control root, string target)
        {
            if (ProcessGeneral.FindControl(root, target) is UctrlWHStockVoucherAe xtraUcMainAeRemove)
            {
                root.Controls.Remove(xtraUcMainAeRemove);
            }
        }

        private void LoadFormSvAe(Int64 lKey)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                HideMenuBonForm(CiFae, lKey);
                AddUserControlMainAe(lKey > 0 ? Cls001Constant.StateEdit : Cls001Constant.StateAdd, lKey);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
                XtraMessageBox.Show("Error on Form_StockVoucher", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void PerformAdd()
        {
            _uctrlSvm.Visible = false;
            LoadFormSvAe(-1);
        }
        protected override void PerformEdit()
        {
            _uctrlSvm.Visible = false;
            long lKey = _gvStockV.FocusedRowHandle < 0
                ? -1
                : long.Parse(_gvStockV.GetRowCellValue(_gvStockV.FocusedRowHandle, CsPk).ToString());
            LoadFormSvAe(lKey);
        }
       
        protected override void PerformClose()
        {
            if (_uctrlSvm.Visible)
            {
                base.PerformClose();
            }
            else
            {
                PerformCancel();
            }
        }
        protected override void PerformCancel()
        {
            RemoveUserControlMainAE(PanConStockVoucher, "UctrlSvae");
            _uctrlSvm.Visible = true;
            HideMenuBonForm(CiFmain, 0);
        }

        #endregion

        #region Fn_Generate

        protected override void PerformGenerate()
        {
            #region Paras
            
            string[] sTypeDoc = ProcessGeneral.GetSafeString(_uctrlSvae.SlueType.Text).Split(new char[] {'-'});
            string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);
            long lfWh = ProcessGeneral.GetSafeInt64(_uctrlSvae.SlueFrwh.EditValue);
            long ltWh = ProcessGeneral.GetSafeInt64(_uctrlSvae.SlueTowh.EditValue);
            long lSuppCust = 0;
            long lSource = ProcessGeneral.GetSafeInt64(_uctrlSvae.SlueSource.EditValue);
            long lDesti = ProcessGeneral.GetSafeInt64(_uctrlSvae.SlueDestination.EditValue);
            string sCondid = "0";
            foreach (var item in _uctrlSvae.LstCondIdSelected)
            {
                sCondid += string.Format(",{0}", item);
            }

            if (sType == string.Empty)
            {
                XtraMessageBox.Show("Invalid Type Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                _uctrlSvae.SlueType.Focus();
                return;
            }

            //if (sType == ClsWhConstant.ConStkRmRecPo && lSuppCust == 0)
            //{
            //    XtraMessageBox.Show("Invalid Source Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            //    _uctrlSvae.SlueSource.Focus();
            //    return;
            //}

            if (lfWh == 0)
            {
                XtraMessageBox.Show("Invalid WareHouse Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                _uctrlSvae.SlueFrwh.Focus();
                return;
            }

            if (ltWh == 0)
            {
                XtraMessageBox.Show("Invalid WareHouse Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                _uctrlSvae.SlueTowh.Focus();
                return;
            }

            if (!AvailablePo(sCondid))
            {
                return;
            }

            switch (_sType)
            {

                case ClsWhConstant.ConStkFgIssSo:
                case ClsWhConstant.ConStkFgIssRe:
                case ClsWhConstant.ConStkRmIssMo:
                    lSuppCust = lDesti;
                    break;

                case ClsWhConstant.ConStkFgRecSo:
                case ClsWhConstant.ConStkFgRecRe:
                case ClsWhConstant.ConStkRmRecPo:
                    lSuppCust = lSource;
                    break;
            }

            #endregion

            // var frmStkVaeWiz = new FrmWHStockVoucherWizard(sType, lSuppCust, lfWh, ltWh);
            try
            {
                //frmStkVaeWiz.MdiParent = this.MdiParent;
                //frmStkVaeWiz.WindowState = FormWindowState.Normal;
                //frmStkVaeWiz.StartPosition = FormStartPosition.CenterScreen;
                //frmStkVaeWiz.SFormStkaeName = this.Name;
                //frmStkVaeWiz.F001GetWizItem += (sSender, eEvent) =>
                //{
                //    Cursor.Current = Cursors.WaitCursor;
                //    _dtDetails = eEvent.DtF001StkVouWiz;
                //    _pGenAddNew = eEvent.bF001AddNew;
                //    if (_dtDetails.Rows.Count > 0)
                //    {
                //        GenerateVoucherDetails(sType, _dtDetails, _pGenAddNew);
                //    }
                //    Cursor.Current = Cursors.Default;
                //};
                //frmStkVaeWiz.Show();
                //this.Enabled = false;

                Cursor.Current = Cursors.WaitCursor;
                _dtDetails = _inf001Sv.GenerateStkVoucherRecPo00102R(_sType, sCondid, "", lfWh, ltWh, 0);
                GenerateVoucherDetails(sType, _dtDetails, true);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                XtraMessageBox.Show($"Error: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AvailablePo(string sPono)
        {
            long lSource = ProcessGeneral.GetSafeInt64(_uctrlSvae.SlueSource.EditValue);
            string sText = $"SELECT PK, CNY00002PK, CNYMF102PK FROM CNY00060 WHERE PK in ({sPono})";
            DataTable dtS = Cls001MasterFiles.ExcecuteSql(sText);
            if (dtS == null || dtS.Rows.Count == 0)
            {
                XtraMessageBox.Show("Invalid PONo.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            long lSou = ProcessGeneral.GetSafeInt64(dtS.Rows[0][1]);
            long lDes = ProcessGeneral.GetSafeInt64(dtS.Rows[0][2]);

            if (lSource == 0)
            {
                _uctrlSvae.SlueSource.EditValue = lSou;
                _uctrlSvae.SlueDestination.EditValue = lDes;
            }
            else
            {
                if (lSou != lSource)
                {
                    XtraMessageBox.Show(
                        "The system does not allow to have more than one supplier per good receipt notes. Enter or scan another PO to continue.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void GenerateVoucherDetails(string sType, DataTable dtLine, bool pAddNew)
        {
            try
            {
                var dtSrc = (DataTable)_gcstockVae.DataSource;
                
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
                                    on new { A = ProcessGeneral.GetSafeInt64(t1[CsH4Col]) }
                                    equals new { A = ProcessGeneral.GetSafeInt64(t2[CsH4Col]) }
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
                _gcstockVae.DataSource = dtSrc;
                _gvStockVae.BestFitColumns();
                HideCols(sType);
                FormatGridCols();

                _ChkInput.CheckState = CheckState.Unchecked;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(@"Error on StockVoucher\Void_Generate " + ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideCols(string sType)
        {
            switch (sType)
            {
                case ClsWhConstant.ConStkRmRecPo:
                case ClsWhConstant.ConStkRmRecRe:
                    ProcessGeneral.HideVisibleColumnsGridView(_gvStockVae, false
                        , CsStatus, CsFrwah, CsTowah, CsFactor, CsStkuQty, CsStkUnit, CsDocRef, CsOrderQty, CsStkQty, CsBalance
                        , CsAsnNo, CsCartonNo, CsConfirmedBy, CsConfirmedDate, CsGw, CsTransDate
                        , CsH1Col, CsH2Col, CsH3Col, CsH4Col, CsH5Col, CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;

                case ClsWhConstant.ConStkFgRecRe:
                case ClsWhConstant.ConStkFgRecSo:
                    ProcessGeneral.HideVisibleColumnsGridView(_gvStockVae, false
                        , CsStatus, CsFrwah, CsTowah, CsFactor, CsStkuQty, CsStkUnit, CsDocRef
                        , CsCurr, CsAmt, CsOrderQty, CsStkQty, CsBalance
                        , CsH1Col, CsH2Col, CsH3Col, CsH4Col, CsH5Col, CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;

                case ClsWhConstant.ConStkRmIssMo:
                case ClsWhConstant.ConStkRmIssRe:
                case ClsWhConstant.ConStkFgIssRe:
                    ProcessGeneral.HideVisibleColumnsGridView(_gvStockVae, false
                        , CsStatus, CsFrwah, CsTowah, CsFactor, CsStkuQty, CsStkUnit, CsDocRef
                        , CsPri, CsCurr, CsAmt, CsOrderQty, CsStkQty, CsBalance
                        , CsGw, CsConfirmedBy, CsConfirmedDate, CsTransDate
                        , CsH1Col, CsH2Col, CsH3Col, CsH4Col, CsH5Col, CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;

                case ClsWhConstant.ConStkFgIssSo:
                    ProcessGeneral.HideVisibleColumnsGridView(_gvStockVae, false
                        , CsStatus, CsFrwah, CsTowah, CsDocRef, CsFactor, CsStkuQty, CsStkUnit
                        , CsAsnNo, CsCartonNo, CsLocation, CsGw, CsConfirmedBy, CsConfirmedDate, CsTransDate
                        , CsH1Col, CsH2Col, CsH3Col, CsH4Col, CsH5Col, CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;

                default:
                    ProcessGeneral.HideVisibleColumnsGridView(_gvStockVae, false
                        , CsStatus, CsPri, CsCurr, CsFactor, CsStkuQty, CsStkUnit, CsDocRef
                        , CsAsnNo, CsCartonNo, CsLocation, CsConfirmedBy, CsConfirmedDate, CsGw, CsTransDate
                        , CsH1Col, CsH2Col, CsH3Col, CsH4Col, CsH5Col, CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;
            }
        }

        private void FormatGridCols()
        {
            _gvStockVae.Columns[CsQty].DisplayFormat.FormatType = FormatType.Numeric;
            _gvStockVae.Columns[CsQty].DisplayFormat.FormatString = "N3";
            _gvStockVae.Columns[CsStkQty].DisplayFormat.FormatType = FormatType.Numeric;
            _gvStockVae.Columns[CsStkQty].DisplayFormat.FormatString = "N3";
            _gvStockVae.Columns[CsOrderQty].DisplayFormat.FormatType = FormatType.Numeric;
            _gvStockVae.Columns[CsOrderQty].DisplayFormat.FormatString = "N3";
            _gvStockVae.Columns[CsBalance].DisplayFormat.FormatType = FormatType.Numeric;
            _gvStockVae.Columns[CsBalance].DisplayFormat.FormatString = "N3";
            _gvStockVae.Columns[CsPri].DisplayFormat.FormatType = FormatType.Numeric;
            _gvStockVae.Columns[CsPri].DisplayFormat.FormatString = "N5";
            _gvStockVae.Columns[CsAmt].DisplayFormat.FormatType = FormatType.Numeric;
            _gvStockVae.Columns[CsAmt].DisplayFormat.FormatString = "N3";

            _gvStockVae.Columns[CsPri].Caption = @"Landed Cost";
            _gvStockVae.Columns[CsPri].Width = _gvStockVae.Columns[CsBatchNo].Width;
        }

        #endregion

        #region Fn_Save

        protected override void PerformSave()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!_uctrlSvae.SaveHeader()) return;
            //_uctrlSvae.SaveDetails();
            _uctrlSvae.SaveDetailswithLocation();
            //_uctrlSvae.ChangePoStatus();

            _uctrlSvae.LoadGridForAdj();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Fn_Refresh & Find

        protected override void PerformRefresh()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!_uctrlSvm.Visible)
            {
                _uctrlSvae.LoadGridForAdj();
            }
            else
            {
                _uctrlSvm.UpdateDataForGridView(DeclareSystem.SysUserName, true);
            }
            Cursor.Current = Cursors.Default;
        }

        private DataTable GetTableFieldSearch()
        {
            DataTable dtFiled = new DataTable();
            dtFiled.Columns.Add("FieldValue", typeof(string));
            dtFiled.Columns.Add("FieldDisplay", typeof(string));
            dtFiled.Columns.Add("FieldType", typeof(string));

            dtFiled.Rows.Add("C26.CNY001", "Document Type", "string");
            dtFiled.Rows.Add("C104.CNY001", "Voucher No.", "string");
            dtFiled.Rows.Add("C104.CNY003", "Description", "string");
            dtFiled.Rows.Add("C104.CNY002", "Date Issued", "datetime");
            dtFiled.Rows.Add("C104.CNY004", "Source", "string");
            dtFiled.Rows.Add("C104.CNY005", "Destination", "string");
            dtFiled.Rows.Add("C104.CNY006", "Invoices/Docs", "string");
            dtFiled.Rows.Add("C104.CNY011", "Printed", "string");
            dtFiled.Rows.Add("C104.CNY012", "Version", "int");
            dtFiled.Rows.Add("C104.CNY010", "Modified By", "string");
            dtFiled.Rows.Add("C104.CNY009", "Modified Date", "datetime");
            dtFiled.Rows.Add("C104.PK", "PK", "number");
            return dtFiled;
        }

        private void frm_searchEvent(object sender, SearchEventArgs e)
        {
            _uctrlSvm.StrFilter = e.filterexpression.Replace(" Where ", " ");
            _uctrlSvm.UpdateDataForGridView("");
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

        #region Delete & Print

        protected override void PerformDelete()
        {
            try
            {
                int iRow = _gvStockV.FocusedRowHandle;
                long lKey = ProcessGeneral.GetSafeInt64(_gvStockV.GetRowCellValue(iRow, CsPk));
                int iOfficial = ProcessGeneral.GetSafeInt(_gvStockV.GetRowCellValue(iRow, CsOfficial));
                int iVersion = ProcessGeneral.GetSafeInt(_gvStockV.GetRowCellValue(iRow, CsVersion));

                if (iVersion==0 && iOfficial == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete the Voucher ID " + lKey + " ?",
                            "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _inf001Sv.DeleteDetails_App_CNY1056_00109(lKey, 1);
                        XtraMessageBox.Show("Finished!");
                        _uctrlSvm.UpdateDataForGridView("");
                    }
                }
                else
                {
                    XtraMessageBox.Show("You Can Not Delete Official Document!");
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Delete Failed!" + e.Message);
            }

            base.PerformDelete();
        }

        protected override void PerformPrint()
        {
            int rH = _gvStockV.FocusedRowHandle;
            Int64 repPk = ProcessGeneral.GetSafeInt64(_uctrlSvm.Visible
                ? _gvStockV.GetRowCellValue(rH, CsPk)
                : _uctrlSvae.TxtId.EditValue);

            //string[] sTypeDoc = ProcessGeneral.GetSafeString(_gvStockV.GetRowCellValue(rH, CsDocTyp)).Split(new char[] { '-' });
            //string sType = ProcessGeneral.GetSafeString(sTypeDoc[0]);

            if (_gvStockV.RowCount <= 0)
            {
                XtraMessageBox.Show("Invalid data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (_sType)
            {
                case ClsWhConstant.ConStkRmRecPo:
                case ClsWhConstant.ConStkRmRecRe:
                    PrintStockVoucherRec(repPk, 0);
                    break;

                case ClsWhConstant.ConStkFgRecSo:
                case ClsWhConstant.ConStkFgRecRe:
                    PrintStockVoucherRec(repPk, 1);
                    break;

                case ClsWhConstant.ConStkRmIssMo:
                case ClsWhConstant.ConStkRmIssRe:
                case ClsWhConstant.ConStkFgIssSo:
                case ClsWhConstant.ConStkFgIssRe:
                    PrintStockVoucherIss(repPk, 0);
                    break;

                case ClsWhConstant.ConStkRmTransRe:
                case ClsWhConstant.ConStkRmTransNoRe:
                case ClsWhConstant.ConStkFgTransRe:
                case ClsWhConstant.ConStkFgTransNoRe:
                    PrintStockVoucherTrans(repPk, 0);
                    break;
            }
        }

        private void PrintStockVoucherRec(long lPk, int iFg)
        {
            FrmWHPrint fPrint = new FrmWHPrint();
            fPrint.PrintStockReceive(lPk, iFg);
        }

        private void PrintStockVoucherIss(long lPk, int iFg)
        {
            FrmWHPrint fPrint = new FrmWHPrint();
            fPrint.PrintStockVoucherIss(lPk, iFg);
        }

        private void PrintStockVoucherTrans(long lPk, int iFg)
        {
            FrmWHPrint fPrint = new FrmWHPrint();
            fPrint.PrintStockVoucherTrans(lPk, iFg);
        }

        #endregion

        #region Import iScala

        private string GetDocType()
        {
            var vTy = _gvStockV.GetSelectedRows()
                .Select(s => new { sType = ProcessGeneral.GetSafeString(_gvStockV.GetRowCellValue(s, CsDocTyp)) })
                .Distinct().ToList();

            int iCoty = 0;
            string sCope = "";
            foreach (var vRo in vTy)
            {
                sCope = vRo.sType;
                iCoty++;
            }

            if (iCoty > 1)
            {
                XtraMessageBox.Show("Invalid Document Type. Please Recheck.", "Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return "";
            }

            return sCope;
        }

        private void ShowFormExportiScala(string sMenu)
        {
            //var fi = this.FindForm() as FrmBase;
            //if (fi == null) return;

            //string sCope = GetDocType();
            //if (string.IsNullOrEmpty(sCope)) return;

            //string[] sType = sCope.Split(new char[] { '-' });
            //string sSelKey = ProcessGeneral.GetStringPKGridView(_gvStockV, CsPk);
            
            //var frmExp = new FrmWHStockVoucherExport(sType[0], sSelKey, sMenu);
            //try
            //{
            //    frmExp.MdiParent = this.MdiParent;
            //    frmExp.WindowState = FormWindowState.Normal;
            //    frmExp.StartPosition = FormStartPosition.CenterScreen;
            //    frmExp.Frm001StockVoucherName = this.Name;
            //    frmExp.SetDefaultCommandAndPermission(fi);
            //    frmExp.Show();
            //    this.Enabled = false;
            //}
            //catch (Exception)
            //{
            //    Cursor.Current = Cursors.Default;
            //    XtraMessageBox.Show($"Error on btnExportFiletoImportiScala_Click", @"Error", MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //}
        }
        
        private void OnItemConstant_Click(object sender, EventArgs e)
        {
            var item = sender as DXMenuItem;
            if (item == null) return;

            string sMenu = item.Caption.Trim();
            ShowFormExportiScala(sMenu);
        }

        private DXPopupMenu CreatePopupImportiScala(string sType)
        {
            DXPopupMenu menuiScala = new DXPopupMenu();

            switch (sType)
            {
                case ClsWhConstant.ConStkRmRecPo:
                    menuiScala.Items.Add(new DXMenuItem(ScaStockRecFile, OnItemConstant_Click, Resources.Number_1_icon));
                    menuiScala.Items.Add(new DXMenuItem(ScaGoodReceiptNote, OnItemConstant_Click, Resources.Number_2_icon));
                    break;
                    
                case ClsWhConstant.ConStkFgRecSo:
                case ClsWhConstant.ConStkRmRecRe:
                case ClsWhConstant.ConStkFgRecRe:
                    //menuiScala.Items.Add(new DXMenuItem(ScaStockRecFile, OnItemConstant_Click, Resources.Number_1_icon));
                    ShowFormExportiScala(sType);
                    break;

                case ClsWhConstant.ConStkRmIssMo:
                    menuiScala.Items.Add(new DXMenuItem(ScaSo01File, OnItemConstant_Click, Resources.Number_4_icon));
                    menuiScala.Items.Add(new DXMenuItem(ScaSo06File, OnItemConstant_Click, Resources.Number_5_icon));
                    break;

                case ClsWhConstant.ConStkFgIssSo:
                    menuiScala.Items.Add(new DXMenuItem(ScaSo01File, OnItemConstant_Click, Resources.Number_4_icon));                    
                    menuiScala.Items.Add(new DXMenuItem(ScaSo09File, OnItemConstant_Click, Resources.Number_6_icon));
                    break;

                case ClsWhConstant.ConStkRmIssRe:
                case ClsWhConstant.ConStkFgIssRe:
                    //menuiScala.Items.Add(new DXMenuItem(ScaStockIssFile, OnItemConstant_Click, Resources.Number_3_icon));
                    ShowFormExportiScala(sType);
                    break;

                default:
                    menuiScala.Items.Add(new DXMenuItem(ScaStockRecFile, OnItemConstant_Click, Resources.Number_1_icon));
                    menuiScala.Items.Add(new DXMenuItem(ScaGoodReceiptNote, OnItemConstant_Click, Resources.Number_2_icon));
                    menuiScala.Items.Add(new DXMenuItem(ScaStockIssFile, OnItemConstant_Click,Resources.Number_3_icon));
                    menuiScala.Items.Add(new DXMenuItem(ScaSo01File, OnItemConstant_Click, Resources.Number_4_icon));
                    menuiScala.Items.Add(new DXMenuItem(ScaSo06File, OnItemConstant_Click, Resources.Number_5_icon));
                    menuiScala.Items.Add(new DXMenuItem(ScaSo09File, OnItemConstant_Click, Resources.Number_6_icon));
                    break;
            }

            return menuiScala;
        }

        private void _ddbExportFiles_ArrowButtonClick(object sender, EventArgs e)
        {
            var btndd = sender as DropDownButton;
            if (btndd == null) return;
           
            string sCope = GetDocType();
            string[] sType = sCope.Split(new char[] { '-' });
            string sTypeCode = sType[0].Trim();
            if (string.IsNullOrEmpty(sTypeCode))
            {
                XtraMessageBox.Show("Please Select Stock Document No.", "Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            btndd.DropDownControl = CreatePopupImportiScala(sTypeCode);
        }

        private void _ddbExportFiles_Click(object sender, EventArgs e)
        {
            _ddbExportFiles_ArrowButtonClick(sender, e);
        }

        #endregion

    }
}
