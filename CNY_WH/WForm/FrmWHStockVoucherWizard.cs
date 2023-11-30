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
using CNY_WH.Common;
using CNY_WH.Info;
using CNY_WH.Class;

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_WH.WForm
{
    public partial class FrmWHStockVoucherWizard : System.Windows.Forms.Form
    {
        #region Declaration

        public string SFormStkaeName;
        private readonly string _sType;
        private readonly long _lSuppCustReQ;
        private readonly Int64 _lFwh;
        private readonly Int64 _lTwh;
        private DataTable _dtResult;
        private GridHitInfo _downHitInfo = null;
        private readonly InfoWh00001 _infWmawi;
        private readonly InfoWh00002 _infWh002;
       
        public event ClsWhPara.F001StkVouAeWiz F001GetWizItem = null;

        private const string CsStatus = "Status(F4)";
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

        private const string CsH1Col = "CNYMF004PKF";
        private const string CsH2Col = "CNYMF004PKT";
        private const string CsH3Col = "TDG00001PK";
        private const string CsH4Col = "CNY00102PK";
        private const string CsH5Col = "CNY00101PK";
        private const string CsH6Col = "CNY00050PK";
        private const string CsH7Col = "CNY00105PK";
        private const string CsH8Col = "CNY00105APK";
        private const string CsPk = "PK";

        #endregion

        #region Init Form

        public FrmWHStockVoucherWizard(string sType, long lSuppCustReQ, Int64 lFwh, Int64 lTwh)
        {
            InitializeComponent();
            _sType = sType;
            _lSuppCustReQ = lSuppCustReQ;
            _lFwh = lFwh;
            _lTwh = lTwh;
            _infWmawi = new InfoWh00001();
            _infWh002= new InfoWh00002();

            this.Load += Frm001StockVoucherWizard_Load;
            this.FormClosed += Frm001StockVoucherWizard_FormClosed;

            _dtResult = GetTableTemplategv();
            GridViewCustomInit(gridControlLoadValue, gvLoadValue, _dtResult, sType);
            GridViewCustomInit(gridControlSelectValue, gvSelectValue, _dtResult, sType);

            btnRemoveAll.Click += BtnRemoveAll_Click;
            btnSelectAll.Click += BtnSelectAll_Click;
            btnRemoveOneRow.Click += BtnRemoveOneRow_Click;
            btnSelectOneRow.Click += BtnSelectOneRow_Click;
            btnSearch.Click += BtnSearch_Click;
            btnFinish.Click += BtnFinish_Click;
            btnCancel.Click += BtnCancel_Click;

            chkAddNew.CheckedChanged += ChkAddNew_CheckedChanged;
            chkOverWrite.CheckedChanged += ChkOverWrite_CheckedChanged;

            beReqId.ButtonPressed += BeReqId_ButtonPressed;
            beReqId.KeyDown += BeReqId_KeyDown;
            beReqNo.ButtonPressed += BeReqNo_ButtonPressed;
            beReqNo.KeyDown += BeReqNo_KeyDown;
        }

        private void Frm001StockVoucherWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessGeneral.EnableFormWhenEndEdit(SFormStkaeName);
        }

        private void Frm001StockVoucherWizard_Load(object sender, EventArgs e)
        {
            chkAddNew.Checked = true;
            List<TextEdit> lTxt = groupControl1.FindAllChildrenByType<TextEdit>().ToList();
            foreach (var eText in lTxt)
            {
                eText.KeyDown += EText_KeyDown;
            }

            ChangeConditionsLabel();
        }

        private void ChangeConditionsLabel()
        {
            switch (_sType)
            {
                case ClsWhConstant.ConStkRmRecPo:
                    lblPoso.Text = @"PO ID.: ";
                    lblReqNo.Text = @"PO No.: ";
                    break;
                case ClsWhConstant.ConStkRmIssMo:
                    lblPoso.Text = @"MO ID.: ";
                    lblReqNo.Text = @"MO No.: ";
                    break;
                case ClsWhConstant.ConStkFgRecSo:
                case ClsWhConstant.ConStkFgIssSo:
                    lblPoso.Text = @"SO ID.: ";
                    lblReqNo.Text = @"SO No.: ";
                    break;
                default:
                    lblPoso.Text = @"Request ID.: ";
                    lblReqNo.Text = @"Request No.: ";
                    break;
            }
        }

        private void EText_KeyDown(object sender, KeyEventArgs e)
        {
            var cTxtName = sender as TextEdit;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                case Keys.Down:
                    if (cTxtName != null && string.IsNullOrEmpty(cTxtName.EditValue.ToString())) return;
                    ResultDataSearch();
                    break;

                case Keys.Up:
                case Keys.Escape:
                    break;

                case Keys.F4:
                    //string sId = ProcessGeneral.GetSafeString(txtReqid.EditValue);

                    //using (var frmSel = new FrmF4Grid()
                    //{
                    //    Text = @"Request Listing",
                    //    Caption = "Request Listing",
                    //    DataSource = _infWmawi.ListMos141(sCusOrd, sRef),
                    //    StrColE = "PK,Customer Code,Reference,Order No,MRO No,MO Qty,WH,Factory",
                    //    iDL = 1
                    //})
                    //{
                    //    frmSel.FireEventData += (s1, e1) =>
                    //    {
                    //        String sPk = e1.ID.Replace('|', ',');
                    //        txtReqid.EditValue = sPk;
                    //    };
                    //    frmSel.ShowDialog();
                    //}
                    break;
            }
        }

        #endregion

        #region Button Pressed & KeyDown
        
        private void BeReqId_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit bte = (ButtonEdit)sender;
            int btind = bte.Properties.Buttons.IndexOf(e.Button);

            switch (btind)
            {
                case 0:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            LoadListPo(false);
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            LoadListSo(false);
                            break;
                        default:
                            //Request ID
                            break;
                    }
                    break;

                case 1:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            if (IsValidPoid()) ResultDataSearch();
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            if (IsValidSoid()) ResultDataSearch();
                            break;
                        default:
                            //Request ID
                            break;
                    }
                    break;

                case 2:
                    beReqId.EditValue = "";
                    ClearGrid();
                    break;
            }
        }

        private void BeReqId_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            if (IsValidPoid()) ResultDataSearch();
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            if (IsValidSoid()) ResultDataSearch();
                            break;
                        default:
                            //
                            break;
                    }
                    break;
                case Keys.F4:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            LoadListPo(false);
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            LoadListSo(false);
                            break;
                        default:
                            //Request ID
                            break;
                    }
                    break;
            }
        }
        
        private void BeReqNo_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit bte = (ButtonEdit)sender;
            int btind = bte.Properties.Buttons.IndexOf(e.Button);

            switch (btind)
            {
                case 0:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            LoadListPo(true);
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            LoadListSo(true);
                            break;
                        default:
                            //Request ID
                            break;
                    }
                    break;

                case 1:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            if (IsValidPono()) ResultDataSearch();
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            if (IsValidSono()) ResultDataSearch();
                            break;
                        default:
                            //Request ID
                            break;
                    }
                    break;

                case 2:
                    beReqNo.EditValue = "";
                    ClearGrid();
                    break;
            }
        }

        private void BeReqNo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            if (IsValidPono()) ResultDataSearch();
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            if (IsValidSono()) ResultDataSearch();
                            break;
                        default:
                            //
                            break;
                    }
                    break;
                case Keys.F4:
                    switch (_sType)
                    {
                        case ClsWhConstant.ConStkRmRecPo:
                            LoadListPo(true);
                            break;
                        case ClsWhConstant.ConStkRmIssMo:
                            //
                            break;
                        case ClsWhConstant.ConStkFgRecSo:
                        case ClsWhConstant.ConStkFgIssSo:
                            LoadListSo(true);
                            break;
                        default:
                            //Request ID
                            break;
                    }
                    break;
            }
        }

        private bool IsValidPoid()
        {
            try
            {
                string sPoid = ProcessGeneral.GetSafeString(beReqId.EditValue);
                if (string.IsNullOrEmpty(sPoid))
                {
                    XtraMessageBox.Show("Please Input POID.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                string sPo = sPoid.Replace(",", "','");
                string sPochk = $"SELECT PK FROM CNY00060 WHERE (CNY00002PK='{_lSuppCustReQ}') AND (PK IN ('{sPo}'))";
                DataTable dtPo = Cls001MasterFiles.ExcecuteSql(sPochk);
                if (dtPo == null || dtPo.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Invalid POID.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
                
                string svapo = Cls001GenFunctions.GetStrColumninDataTable(dtPo, "PK");
                beReqId.EditValue = svapo;
                return true;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error On CheckingPO");
                return false;
            }
        }

        private bool IsValidPono()
        {
            try
            {
                string sPono = ProcessGeneral.GetSafeString(beReqNo.EditValue);
                if (string.IsNullOrEmpty(sPono))
                {
                    XtraMessageBox.Show("Please Input PONo.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                string sPo = sPono.Replace(",", "','");
                string sPoid = $"SELECT CNY001 FROM CNY00060 WHERE (CNY00002PK='{_lSuppCustReQ}') AND (CNY001 IN ('{sPo}'))";
                DataTable dtPo = Cls001MasterFiles.ExcecuteSql(sPoid);
                if (dtPo == null || dtPo.Rows.Count==0)
                {
                    XtraMessageBox.Show("Invalid PONo.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
                
                string svapo = Cls001GenFunctions.GetStrColumninDataTable(dtPo, "CNY001");
                beReqNo.EditValue = svapo;
                return true;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error On CheckingPONo");
                return false;
            }
        }

        private void LoadListPo(bool bPono)
        {
            string sPono = ProcessGeneral.GetSafeString(beReqNo.EditValue);
            string sPoid = ProcessGeneral.GetSafeString(beReqId.EditValue);
            int iPono = ProcessGeneral.GetSafeBoolInt(bPono);

            DataTable dtSrc = _infWh002.ListPoinAllocation_00120S(_lSuppCustReQ, sPono, sPoid, iPono);
            if (dtSrc == null || dtSrc.Rows.Count == 0) return;

            string sFina = Cls001GenFunctions.GetColumnNameFrdt(dtSrc);
            using (var frmSel = new FrmF4Grid()
            {
                Text = @"PO Listing",
                Caption = "PO Listing",
                DataSource = dtSrc,
                StrColE = sFina,
                iDL = 1
            })
            {
                frmSel.FireEventData += (s1, e1) =>
                {
                    if (bPono)
                    {
                        string sNo= ProcessGeneral.GetSafeString(e1.ID);
                        beReqNo.EditValue = Cls001GenFunctions.ReverseString(sNo);
                    }
                    else
                    {
                        string sId = Cls001GenFunctions.GetStrColumninDataTable(e1.DtSel, "POID");
                        beReqId.EditValue = sId;
                    }
                };
                frmSel.WindowState = FormWindowState.Maximized;
                frmSel.ShowDialog();
            }
        }

        private void ClearGrid()
        {
            gridControlLoadValue.DataSource = null;
            gridControlSelectValue.DataSource = null;

            //var dtClr = GetTableTemplategv();
            //gridControlLoadValue.DataSource = dtClr;
            //gridControlLoadValue.ForceInitialize();
            //gridControlSelectValue.DataSource = dtClr;
            //gridControlSelectValue.ForceInitialize();
        }

        private bool IsValidSoid()
        {
            try
            {
                string sSoid = ProcessGeneral.GetSafeString(beReqId.EditValue);
                if (string.IsNullOrEmpty(sSoid))
                {
                    XtraMessageBox.Show("Please Input SOID.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                string sSo = sSoid.Replace(",", "','");
                string sSochk = $"SELECT CNY019PK FROM CNY00019 WHERE (CNY00011PK_O='{_lSuppCustReQ}') AND (CNY019PK IN ('{sSo}'))";
                DataTable dtSo = Cls001MasterFiles.ExcecuteSql(sSochk);
                if (dtSo == null || dtSo.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Invalid SOID.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                string svaso = Cls001GenFunctions.GetStrColumninDataTable(dtSo, "CNY019PK");
                beReqId.EditValue = svaso;
                return true;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error On CheckingSO");
                return false;
            }
        }

        private bool IsValidSono()
        {
            try
            {
                string sSono = ProcessGeneral.GetSafeString(beReqNo.EditValue);
                if (string.IsNullOrEmpty(sSono))
                {
                    XtraMessageBox.Show("Please Input SONo.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                string sSo = sSono.Replace(",", "','");
                string sSoid = $"SELECT CNY001 FROM CNY00019 WHERE (CNY00011PK_O='{_lSuppCustReQ}') AND (CNY001 IN ('{sSo}'))";
                DataTable dtSo = Cls001MasterFiles.ExcecuteSql(sSoid);
                if (dtSo == null || dtSo.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Invalid SONo.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                string svaso = Cls001GenFunctions.GetStrColumninDataTable(dtSo, "CNY001");
                beReqNo.EditValue = svaso;
                return true;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error On CheckingSONo");
                return false;
            }
        }

        private void LoadListSo(bool bSono)
        {
            string sSono = ProcessGeneral.GetSafeString(beReqNo.EditValue);
            string sSoid = ProcessGeneral.GetSafeString(beReqId.EditValue);
            int iSono = ProcessGeneral.GetSafeBoolInt(bSono);

            DataTable dtSrc = _infWh002.ListSo_App_00120C(_lSuppCustReQ, sSono, sSoid, iSono);
            if (dtSrc == null || dtSrc.Rows.Count == 0) return;

            string sFina = Cls001GenFunctions.GetColumnNameFrdt(dtSrc);
            using (var frmSel = new FrmF4Grid()
            {
                Text = @"SO Listing",
                Caption = "SO Listing",
                DataSource = dtSrc,
                StrColE = sFina,
                iDL = 1
            })
            {
                frmSel.FireEventData += (s1, e1) =>
                {
                    if (bSono)
                    {
                        string sNo = ProcessGeneral.GetSafeString(e1.ID);
                        beReqNo.EditValue = Cls001GenFunctions.ReverseString(sNo);
                    }
                    else
                    {
                        string sId = Cls001GenFunctions.GetStrColumninDataTable(e1.DtSel, "SOID");
                        beReqId.EditValue = sId;
                    }
                };
                frmSel.WindowState = FormWindowState.Maximized;
                frmSel.ShowDialog();
            }
        }

        #endregion

        #region "Init GridView"

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

            dtTemp.Columns.Add(CsH8Col, typeof(long));
            dtTemp.Columns.Add(CsAsnNo, typeof(string));
            dtTemp.Columns.Add(CsCartonNo, typeof(string));
            dtTemp.Columns.Add(CsLocation, typeof(string));
            dtTemp.Columns.Add(CsGw, typeof(double));
            dtTemp.Columns.Add(CsConfirmedDate, typeof(DateTime));
            dtTemp.Columns.Add(CsConfirmedBy, typeof(string));
            dtTemp.Columns.Add(CsTransDate, typeof(DateTime));

            dtTemp.Columns.Add(CsH6Col, typeof(string));
            dtTemp.Columns.Add(CsH1Col, typeof(long));
            dtTemp.Columns.Add(CsH2Col, typeof(long));
            dtTemp.Columns.Add(CsH3Col, typeof(long));
            dtTemp.Columns.Add(CsH4Col, typeof(long));
            dtTemp.Columns.Add(CsH5Col, typeof(long));
            dtTemp.Columns.Add(CsH7Col, typeof(long));

            return dtTemp;
        }
        private void GridViewCustomInit(GridControl gridControl, GridView gridView, DataTable dtGr, string sType)
        {
            Cls001GenFunctions.InitGridSelectCell(gridControl, gridView, false);
            Cls001GenFunctions.GridViewCustomCols(gridView, dtGr);
            HideColumns(gridView);

            gridControl.DragOver += GridControl_DragOver;
            gridControl.DragDrop += GridControl_DragDrop;
            gridView.MouseMove += GridView_MouseMove;
            gridView.MouseDown += GridView_MouseDown;
            gridView.DoubleClick += GridView_DoubleClick;
            gridControl.DataSource = dtGr;
            gridView.BestFitColumns();
        }

        #endregion

        #region Select Row, Drag & Drop

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            if (view.Name == "gvLoadValue")
            {
                BtnSelectOneRow_Click(null, null);
            }
            if (view.Name == "gvSelectValue")
            {
                BtnRemoveOneRow_Click(null, null);
            }
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            _downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (hitInfo.InRowCell)
            {
                if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                    _downHitInfo = hitInfo;
            }
        }

        private DragData GetDragData(GridView view)
        {
            if (view == null || view.SelectedRowsCount == 0) return null;
            DragData result = new DragData();
            result.gcDrag = view.GridControl;
            result.gvDrag = view;
            result.sourceData = ((DataTable)view.GridControl.DataSource).Clone();
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                result.sourceData.ImportRow(view.GetDataRow(view.GetSelectedRows()[i]));
            }
            return result;
        }

        private void GridView_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && _downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(_downHitInfo.HitPoint.X - dragSize.Width / 2,
                 _downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(GetDragData(view), DragDropEffects.Move);
                    _downHitInfo = null;
                    DXMouseEventArgs ev = DXMouseEventArgs.GetMouseArgs(e);
                    ev.Handled = true;
                }
            }
        }

        private void GridControl_DragDrop(object sender, DragEventArgs e)
        {
            int dropTargetRowHandle = -1;
            GridControl grid = sender as GridControl;
            Point pt = new Point(e.X, e.Y);
            pt = grid.PointToClient(pt);
            GridView view = grid.GetViewAt(pt) as GridView;
            GridHitInfo hitInfoA = view.CalcHitInfo(pt);
            if (hitInfoA.HitTest == GridHitTest.EmptyRow)
                dropTargetRowHandle = view.DataRowCount;
            else
            {
                dropTargetRowHandle = hitInfoA.RowHandle < 0 ? 0 : hitInfoA.RowHandle + 1;
            }
            ((DataTable)grid.DataSource).AcceptChanges();
            DataTable dtDrop = grid.DataSource as DataTable;
            DragData data = e.Data.GetData(typeof(DragData)) as DragData;
            if (data != null)
            {
                if (data.sourceData.Rows.Count > 0)
                {
                    //perform insert row grid drop
                    int pos = dropTargetRowHandle;
                    for (int i = 0; i < data.sourceData.Rows.Count; i++)
                    {
                        if (dtDrop.Rows.Count > 0)
                        {
                            DataRow drDrop = dtDrop.NewRow();
                            int h = 0;
                            foreach (object items in data.sourceData.Rows[i].ItemArray)
                            {
                                drDrop[h] = items;
                                h++;
                            }
                            h = 0;
                            dtDrop.Rows.InsertAt(drDrop, pos);
                        }
                        else
                        {
                            dtDrop.ImportRow(data.sourceData.Rows[i]);
                        }
                        dtDrop.AcceptChanges();
                        pos++;

                    }

                    ProcessGeneral.DeleteSelectedRows(data.gvDrag);
                    ((DataTable)data.gcDrag.DataSource).AcceptChanges();
                }
            }
        }

        private void GridControl_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(DragData)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void SetSelectRowGridView(GridView gv, int gvNumberRowOld, int numberRowSelected, int maxRowSelected)
        {
            if (numberRowSelected > 0)
            {
                if (maxRowSelected == gvNumberRowOld - 1)
                {
                    gv.FocusedRowHandle = gv.RowCount - 1;
                    gv.SelectRow(gv.RowCount - 1);

                }
                else
                {
                    if (maxRowSelected == 0)
                    {
                        gv.FocusedRowHandle = 0;
                        gv.SelectRow(0);
                    }
                    else
                    {
                        if (numberRowSelected == 1)
                        {
                            gv.FocusedRowHandle = maxRowSelected - numberRowSelected + 1;
                            gv.SelectRow(maxRowSelected - numberRowSelected + 1);
                        }
                        else
                        {
                            gv.FocusedRowHandle = maxRowSelected - numberRowSelected;
                            gv.SelectRow(maxRowSelected - numberRowSelected);
                        }
                    }
                }
            }
        }

        #endregion

        #region "HotKey"

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.D1:
                    {
                        BtnRemoveAll_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D2:
                    {
                        BtnRemoveOneRow_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D3:
                    {
                        BtnSelectOneRow_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D4:
                    {
                        BtnSelectAll_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D5:
                    {
                        BtnSearch_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D6:
                    {
                        BtnFinish_Click(null, null);
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion

        #region Btn_Click

        private void ChkOverWrite_CheckedChanged(object sender, EventArgs e)
        {
            chkAddNew.Checked = !chkOverWrite.Checked;
        }

        private void ChkAddNew_CheckedChanged(object sender, EventArgs e)
        {
            chkOverWrite.Checked = !chkAddNew.Checked;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                _dtResult = (DataTable)gridControlSelectValue.DataSource;
                F001GetWizItem?.Invoke(this, new ClsWhPara.F001StkVouAeArgs
                {
                    DtF001StkVouWiz = _dtResult,
                    bF001AddNew=chkAddNew.Checked
                });

                ProcessGeneral.EnableFormWhenEndEdit(SFormStkaeName);
                this.Dispose();
                this.Close();
            }
            catch (Exception)
            {
                XtraMessageBox.Show(@"Error on StockVoucherRequest_Wizard\Void_Finish ", "Error!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ResultDataSearch();
        }

        private void BtnSelectOneRow_Click(object sender, EventArgs e)
        {
            int numberRowSelected = gvLoadValue.SelectedRowsCount;
            int gvNumberRowOld = gvLoadValue.RowCount;
            int maxRowSelected = 0;

            for (int i = 0; i < numberRowSelected; i++)
            {
                if (gvLoadValue.GetSelectedRows()[i] >= 0)
                {
                    ProcessGeneral.AddNewRowGV(gvSelectValue, gvLoadValue.GetDataRow(gvLoadValue.GetSelectedRows()[i]));
                    if (i == numberRowSelected - 1)
                    {
                        maxRowSelected = gvLoadValue.GetSelectedRows()[i];
                    }
                }
            }

            ProcessGeneral.DeleteSelectedRows(gvLoadValue);
            if (gvLoadValue.RowCount > 0)
            {
                SetSelectRowGridView(gvLoadValue, gvNumberRowOld, numberRowSelected, maxRowSelected);
            }
            gvLoadValue.BestFitColumns();
            gvSelectValue.BestFitColumns();
        }

        private void BtnRemoveOneRow_Click(object sender, EventArgs e)
        {
            int numberRowSelected = gvSelectValue.SelectedRowsCount;
            int gvNumberRowOld = gvSelectValue.RowCount;
            int maxRowSelected = 0;
            for (int i = 0; i < numberRowSelected; i++)
            {
                if (gvSelectValue.GetSelectedRows()[i] >= 0)
                {
                    ProcessGeneral.AddNewRowGV(gvLoadValue, gvSelectValue.GetDataRow(gvSelectValue.GetSelectedRows()[i]));
                    if (i == numberRowSelected - 1)
                    {
                        maxRowSelected = gvSelectValue.GetSelectedRows()[i];
                    }
                }

            }
            ProcessGeneral.DeleteSelectedRows(gvSelectValue);
            if (gvSelectValue.RowCount > 0)
            {
                SetSelectRowGridView(gvSelectValue, gvNumberRowOld, numberRowSelected, maxRowSelected);
            }
            gvLoadValue.BestFitColumns();
            gvSelectValue.BestFitColumns();
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            ProcessGeneral.InsertDataAllFromgvATogvB(gridControlLoadValue, gvLoadValue, gvSelectValue, _dtResult);
            gvLoadValue.BestFitColumns();
            gvSelectValue.BestFitColumns();
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            ProcessGeneral.InsertDataAllFromgvATogvB(gridControlSelectValue, gvSelectValue, gvLoadValue, _dtResult);
            gvLoadValue.BestFitColumns();
            gvSelectValue.BestFitColumns();
        }
       
        #endregion

        #region Fn_Search

        private void ResultDataSearch()
        {
            Cursor.Current = Cursors.WaitCursor;

            string sId = ProcessGeneral.GetSafeString(beReqId.EditValue);
            string sNo = ProcessGeneral.GetSafeString(beReqNo.EditValue);
            int iChk = ProcessGeneral.GetSafeBoolInt(ChkShowReceivedItems.CheckState);
            DataTable dt;
            
            switch(_sType)
            {
                case ClsWhConstant.ConStkRmRecPo: //Rec PO
                    dt = _infWmawi.GenerateStkVoucherRecPo00102R(_sType, sId, sNo, _lFwh, _lTwh, iChk);
                    break;

                //case ClsWhConstant.ConStkFgRecSo: // Rec SO
                //    dt =  _infWmawi.GenerateStkVoucherRecSo00102F(_sType, sId, sNo, _lFwh, _lTwh, iChk);
                //    break;

                case ClsWhConstant.ConStkRmIssMo: // Iss MO
                    dt = _infWmawi.GenerateStkVoucherIssMo00103R(_sType, sId, sNo, _lFwh, _lTwh, iChk);
                    break;

                case ClsWhConstant.ConStkFgIssSo: // Iss SO 11.Mar.20
                case ClsWhConstant.ConStkFgRecSo: // Rec SO DUNG TAM. XEM LAY TU PACKING LIST HAY SCAN NTN ROI CHINH LAI.
                    dt = _infWmawi.GenerateStkVoucherIssSo_App_00103F(_sType, sId, sNo, _lFwh, _lTwh, iChk);
                    break;
                    
                case ClsWhConstant.ConStkRmRecRe:   // Rec Rm Req
                case ClsWhConstant.ConStkFgRecRe:   // Rec Fg Req
                case ClsWhConstant.ConStkRmIssRe:   // Iss Rm Req
                case ClsWhConstant.ConStkFgIssRe:   // Iss Fg Req
                case ClsWhConstant.ConStkRmTransRe:
                case ClsWhConstant.ConStkRmTransNoRe:
                case ClsWhConstant.ConStkFgTransRe:
                case ClsWhConstant.ConStkFgTransNoRe:
                    dt = _infWmawi.GenerateStkVoucherfrReq00102(_sType, sId, sNo, _lFwh, _lTwh, iChk);
                    break;

                default:    // not yet finish. check later.
                    dt = _infWmawi.GenerateStkVoucherfrReqFg00104(_sType, sId);
                    break;
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("No Data Display GridView", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
                return;
            }

            _dtResult = dt;
            Cls001GenFunctions.GridViewCustomCols(gvLoadValue, _dtResult);
            gridControlLoadValue.DataSource = _dtResult;
            gvLoadValue.BestFitColumns();

            var dtSelt = dt.Clone();
            Cls001GenFunctions.GridViewCustomCols(gvSelectValue, dtSelt);
            gridControlSelectValue.DataSource = dtSelt;

            HideColumns(gvLoadValue);
            HideColumns(gvSelectValue);
            Cursor.Current = Cursors.Default;
        }
        private void HideColumns(GridView gv)
        {
            switch (_sType)
            {
                case ClsWhConstant.ConStkRmRecPo:
                case ClsWhConstant.ConStkRmRecRe:
                case ClsWhConstant.ConStkFgRecSo:
                case ClsWhConstant.ConStkFgRecRe:
                case ClsWhConstant.ConStkRmIssRe:
                case ClsWhConstant.ConStkFgIssSo:
                case ClsWhConstant.ConStkFgIssRe:
                    ProcessGeneral.HideVisibleColumnsGridView(gv, false, CsStatus, CsFrwah, CsTowah
                        , CsBatchNo, CsStkQty, CsStkUnit, CsAmt, CsBalance, CsQty, CsCurr, CsPri, CsRem
                        , CsDocRef, CsAsnNo, CsCartonNo, CsLocation, CsConfirmedDate, CsConfirmedBy, CsGw
                        , CsTransDate, CsValidBatch, CsFactor, CsStkuQty, CsH1Col, CsH2Col, CsH3Col, CsH4Col, CsH5Col
                        , CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;

                case ClsWhConstant.ConStkRmIssMo:
                    ProcessGeneral.HideVisibleColumnsGridView(gv, false, CsStatus, CsFrwah, CsTowah
                        , CsBatchNo, CsStkQty, CsStkUnit, CsAmt, CsBalance, CsCurr, CsPri, CsDocRef
                        , CsAsnNo, CsCartonNo, CsLocation, CsConfirmedDate, CsConfirmedBy, CsGw
                        , CsTransDate, CsValidBatch, CsFactor, CsStkuQty, CsH1Col, CsH2Col, CsH3Col, CsH4Col
                        , CsH5Col, CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;
                default:
                    ProcessGeneral.HideVisibleColumnsGridView(gv, false, CsStatus, CsAmt, CsCurr
                        , CsPri, CsFactor, CsStkuQty, CsAsnNo, CsCartonNo, CsLocation, CsConfirmedDate
                        , CsConfirmedBy, CsGw, CsTransDate, CsValidBatch, CsH1Col, CsH2Col, CsH3Col, CsH4Col
                        , CsH5Col, CsH6Col, CsH7Col, CsH8Col, CsPk);
                    break;
            }
            gv.Columns[CsOrderQty].DisplayFormat.FormatType = FormatType.Numeric;
            gv.Columns[CsOrderQty].DisplayFormat.FormatString = "N3";
        }

        #endregion
    }
}
