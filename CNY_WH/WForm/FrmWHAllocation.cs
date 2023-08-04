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
using CNY_WH.Properties;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace CNY_WH.WForm
{
    public partial class FrmWHAllocation : FrmBase
    {
        #region Paras

        public event EventHandler OnClose = null;
        public string FrmWhallname;

        private readonly InfoWh00002 _infPoApp;

        private const string ItemCode = "Item Code";
        private const string ItemName = "Item Name";

        private const string Qty = "Qty";
        private const string Price = "Price";
        private const string Surcharge = "Surcharge";
        private const string Amount = "Amount";
        private const string PrQty = "PR.Qty";
        private const string PurQty = "Pur.Qty";
        private const string StkQty = "Stock Received";
        private const string Balance = "Balance";
        private const string Pk = "PK";

        #endregion

        #region Init

        private DataTable DtWhAllTemp()
        {
            DataTable dtwhTemp = new DataTable();

            dtwhTemp.Columns.Add("Item Code", typeof(string));
            dtwhTemp.Columns.Add("Item Name", typeof(string));
            dtwhTemp.Columns.Add("Unit", typeof(string));

            return dtwhTemp;
        }

        public FrmWHAllocation()
        {
            _infPoApp = new InfoWh00002();
            InitializeComponent();

            var dtPo = DtWhAllTemp();
            Cls001GenFunctions.InitGridSelectCell(gcWhallo, gvWhallo, false);
            Cls001GenFunctions.InitGridSelectCell(gcServices, gvServices, false);

            Cls001GenFunctions.GridViewCustomCols(gvWhallo, dtPo);
            Cls001GenFunctions.DrawRectangleSelectiononGridView(gcWhallo, gvWhallo);

            this.Load += FrmWHAllocation_Load;
            this.FormClosed += FrmWHAllocation_FormClosed;

            txtPoid.KeyDown += TxtPoid_KeyDown;
            btnPono.KeyDown += BtnPono_KeyDown;
            btnPono.ButtonPressed += BtnPono_ButtonPressed;

            gcWhallo.ProcessGridKey += GcWhallo_ProcessGridKey;
            gvWhallo.DoubleClick += GvWhallo_DoubleClick;
        }

        private void FrmWHAllocation_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessGeneral.EnableFormWhenEndEdit(FrmWhallname);
            OnClose?.Invoke(sender, EventArgs.Empty);
        }

        private void FrmWHAllocation_Load(object sender, EventArgs e)
        {
            #region HideButton

            AllowAdd = false;
            AllowDelete = false;
            AllowBreakDown = false;
            AllowDelete = false;
            AllowCheck = false;
            AllowCombine = false;
            AllowCopyObject = false;
            AllowEdit = false;
            AllowFind = false;
            AllowRefresh = false;
            AllowRangeSize = false;
            AllowRevision = false;
            AllowPrint = false;
            AllowGenerate = false;
            AllowSave = false;
            AllowCancel = false;

            #endregion

            txtPoid.Focus();
        }

        #endregion

        #region Btn Pono

        private bool IsValidPono()
        {
            try
            {
                long lPoid = ProcessGeneral.GetSafeInt64(txtPoid.EditValue);
                if (lPoid==0 )
                {
                    string sPono = ProcessGeneral.GetSafeString(btnPono.EditValue);
                    if (string.IsNullOrEmpty(sPono))
                    {
                        XtraMessageBox.Show("Please Input PONo.!", "Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return false;
                    }

                    string sPoid = $"SELECT PK FROM CNY00060 WHERE CNY001='{sPono}'";
                    string sPoidv = ProcessGeneral.GetSafeString(Cls001MasterFiles.GetDescription(sPoid, "PK"));
                    if (sPoidv=="N/A")
                    {
                        XtraMessageBox.Show("Please Input POID.!", "Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return false;
                    }

                    lPoid = ProcessGeneral.GetSafeInt64(sPoidv);
                }

                DataTable dtChk = _infPoApp.LoadPoHeaderInfo_00125A(lPoid);
                if (dtChk == null || dtChk.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Invalid Po No.!", "Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                SetPoHeaderInfo(dtChk);
                return true;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error On CheckingPO");
                return false;
            }
        }

        private void SetPoHeaderInfo(DataTable dtPo)
        {
            if (dtPo == null || dtPo.Rows.Count == 0)
            {
                txtSupplier.EditValue = "";
                txtPurchaser.EditValue = "";
                txtCurrency.EditValue = "";
                txtInvoiceCode.EditValue = "";
                txtDeliveryCode.EditValue = "";
                txtPoid.EditValue = "";
                btnPono.EditValue = "";
                dDeliveryDate.EditValue = "";
                dPODate.EditValue = "";
            }
            else
            {
                txtSupplier.EditValue = ProcessGeneral.GetSafeString(dtPo.Rows[0]["Supplier"]);
                txtPurchaser.EditValue = ProcessGeneral.GetSafeString(dtPo.Rows[0]["Purchaser"]);
                txtCurrency.EditValue = ProcessGeneral.GetSafeString(dtPo.Rows[0]["PoCurr"]);
                txtInvoiceCode.EditValue = ProcessGeneral.GetSafeString(dtPo.Rows[0]["InvoiceTo"]);
                txtDeliveryCode.EditValue = ProcessGeneral.GetSafeString(dtPo.Rows[0]["DelTo"]);
                txtPoid.EditValue = ProcessGeneral.GetSafeString(dtPo.Rows[0]["PK"]);
                btnPono.EditValue = ProcessGeneral.GetSafeString(dtPo.Rows[0]["PoNo"]);
                dDeliveryDate.EditValue = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(dtPo.Rows[0]["Delivery Date"]);
                dPODate.EditValue = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(dtPo.Rows[0]["PO Date"]);
            }
        }

        private void TxtPoid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Cursor.Current = Cursors.WaitCursor;
                    if (IsValidPono())
                    {
                        LoadPoDetails();
                        LoadPoServices();
                    }
                    Cursor.Current = Cursors.Default;
                    break;
            }
        }

        private void BtnPono_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit bte = (ButtonEdit)sender;
            int btind = bte.Properties.Buttons.IndexOf(e.Button);

            switch (btind)
            {
                case 0:
                    #region List Pono

                    string sPono = ProcessGeneral.GetSafeString(btnPono.EditValue);
                    DataTable dtSrc = _infPoApp.ListPoinAllocation_00120A(sPono);
                    if (dtSrc == null || dtSrc.Rows.Count == 0) return;

                    string sFina = Cls001GenFunctions.GetColumnNameFrdt(dtSrc);
                    using (var frmSel = new FrmF4Grid()
                    {
                        Text = @"Type Listing",
                        Caption = "Type Listing",
                        DataSource = dtSrc,
                        StrColE = sFina,
                        iDL = 2
                    })
                    {
                        frmSel.FireEventData += (s1, e1) =>
                        {
                            String[] str = e1.ID.Split(new Char[] { '|' });
                            btnPono.EditValue = str[0].ToUpper().Trim();
                            txtPoid.EditValue = str[4];
                        };
                        frmSel.WindowState = FormWindowState.Maximized;
                        frmSel.ShowDialog();
                    }

                    #endregion
                    break;

                case 1:
                    if (IsValidPono())
                    {
                        LoadPoDetails();
                        LoadPoServices();
                    }
                    break;

                case 2:
                    btnPono.EditValue = "";
                    txtPoid.EditValue = "";
                                        
                    SetPoHeaderInfo(null);
                    var dtPo = DtWhAllTemp();
                    gcWhallo.DataSource = dtPo;
                    gcWhallo.ForceInitialize();

                    if (gvServices.RowCount > 0) gcServices.DataSource = null;
                    break;
            }
        }

        private void BtnPono_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (IsValidPono())
                    {
                        LoadPoDetails();
                        LoadPoServices();
                    }
                    break;
                case Keys.F4:
                    #region List Pono

                    string sPono = ProcessGeneral.GetSafeString(btnPono.EditValue);
                    DataTable dtSrc = _infPoApp.ListPoinAllocation_00120A(sPono);
                    if (dtSrc == null || dtSrc.Rows.Count == 0) return;

                    string sFina = Cls001GenFunctions.GetColumnNameFrdt(dtSrc);
                    using (var frmSel = new FrmF4Grid()
                    {
                        Text = @"Type Listing",
                        Caption = "Type Listing",
                        DataSource = dtSrc,
                        StrColE = sFina,
                        iDL = 2
                    })
                    {
                        frmSel.FireEventData += (s1, e1) =>
                        {
                            String[] str = e1.ID.Split(new Char[] { '|' });
                            btnPono.EditValue = str[0].ToUpper().Trim();
                            txtPoid.EditValue = str[4];
                        };
                        frmSel.WindowState = FormWindowState.Maximized;
                        frmSel.ShowDialog();
                    }

                    #endregion
                    break;
            }
        }

        #endregion

        #region Load Data & Format Grid

        private void LoadPoDetails()
        {
            long lPky = ProcessGeneral.GetSafeInt64(txtPoid.EditValue);
            DataTable dtMaster = _infPoApp.LoadPoAllocationDetails_00122A(lPky);
            Cls001GenFunctions.GridViewCustomCols(gvWhallo, dtMaster);

            gcWhallo.DataSource = dtMaster;
            Formatgrid();
            gvWhallo.BestFitColumns();
        }

        private void Formatgrid()
        {
            int fCol = gvWhallo.Columns[Pk].AbsoluteIndex;
            for (int i = fCol; i < gvWhallo.Columns.Count; i++)
            {
                gvWhallo.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                gvWhallo.Columns[i].DisplayFormat.FormatString = "N2";
            }

            gvWhallo.Columns[Price].DisplayFormat.FormatType = FormatType.Numeric;
            gvWhallo.Columns[Price].DisplayFormat.FormatString = "N2";
            gvWhallo.Columns[Surcharge].DisplayFormat.FormatType = FormatType.Numeric;
            gvWhallo.Columns[Surcharge].DisplayFormat.FormatString = "N2";
            gvWhallo.Columns[Amount].DisplayFormat.FormatType = FormatType.Numeric;
            gvWhallo.Columns[Amount].DisplayFormat.FormatString = "N2";

            gvWhallo.Columns[PrQty].DisplayFormat.FormatType = FormatType.Numeric;
            gvWhallo.Columns[PrQty].DisplayFormat.FormatString = "N2";
            gvWhallo.Columns[PurQty].DisplayFormat.FormatType = FormatType.Numeric;
            gvWhallo.Columns[PurQty].DisplayFormat.FormatString = "N2";
            gvWhallo.Columns[Balance].DisplayFormat.FormatType = FormatType.Numeric;
            gvWhallo.Columns[Balance].DisplayFormat.FormatString = "N2";
            gvWhallo.Columns[StkQty].DisplayFormat.FormatType = FormatType.Numeric;
            gvWhallo.Columns[StkQty].DisplayFormat.FormatString = "N2";
            gvWhallo.Columns[Pk].Visible = false;
        }

        private void LoadPoServices()
        {
            long lPky = ProcessGeneral.GetSafeInt64(txtPoid.EditValue);
            DataTable dtService = _infPoApp.LoadPoServicesDetails_M00122S(lPky);
            if (dtService == null || dtService.Rows.Count == 0)
            {
                xTabService.PageVisible = false;
            }
            else
            {
                xTabService.PageVisible = true;
                Cls001GenFunctions.GridViewCustomCols(gvServices, dtService);
                gcServices.DataSource = dtService;

                FormatServicegrid();
                gvServices.BestFitColumns();
            }
        }

        private void FormatServicegrid()
        {
            gvServices.Columns[Price].DisplayFormat.FormatType = FormatType.Numeric;
            gvServices.Columns[Price].DisplayFormat.FormatString = "N2";
            gvServices.Columns[Amount].DisplayFormat.FormatType = FormatType.Numeric;
            gvServices.Columns[Amount].DisplayFormat.FormatString = "N2";

            gvServices.Columns[Qty].DisplayFormat.FormatType = FormatType.Numeric;
            gvServices.Columns[Qty].DisplayFormat.FormatString = "N2";
        }
        
        private void GvWhallo_DoubleClick(object sender, EventArgs e)
        {
            int iRow = gvWhallo.FocusedRowHandle;
            long lpk = ProcessGeneral.GetSafeInt64(gvWhallo.GetRowCellValue(iRow, Pk));
            string sItemCode = ProcessGeneral.GetSafeString(gvWhallo.GetFocusedRowCellValue(ItemCode));
            string sItemName = ProcessGeneral.GetSafeString(gvWhallo.GetFocusedRowCellValue(ItemName));

            if (gvWhallo.FocusedColumn.FieldName == StkQty)
            {
                LoadStkInfo(lpk, sItemCode + '-' + sItemName);
                return;
            }

            int iCol = gvWhallo.Columns[Pk].AbsoluteIndex;
            if (gvWhallo.FocusedColumn.AbsoluteIndex<=iCol) return;

            double dQty = ProcessGeneral.GetSafeDouble(gvWhallo.GetFocusedRowCellValue(gvWhallo.FocusedColumn));
            if (dQty <=0) return;
            
            string sPrno = ProcessGeneral.GetSafeString(gvWhallo.FocusedColumn.FieldName);
            LoadSoInfo(lpk, sPrno, sItemCode + '-' + sItemName);
        }

        private void GcWhallo_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F4)
            {
                int iRow = gvWhallo.FocusedRowHandle;
                long lpk = ProcessGeneral.GetSafeInt64(gvWhallo.GetRowCellValue(iRow, "PK"));

                string sItemCode = ProcessGeneral.GetSafeString(gvWhallo.GetFocusedRowCellValue(ItemCode));
                string sItemName = ProcessGeneral.GetSafeString(gvWhallo.GetFocusedRowCellValue(ItemName));

                if (gvWhallo.FocusedColumn.FieldName == StkQty)
                {
                    LoadStkInfo(lpk, sItemCode + '-' + sItemName);
                    return;
                }

                int iCol = gvWhallo.Columns[Pk].AbsoluteIndex;
                if (gvWhallo.FocusedColumn.AbsoluteIndex <= iCol) return;

                double dQty = ProcessGeneral.GetSafeDouble(gvWhallo.GetFocusedRowCellValue(gvWhallo.FocusedColumn));
                if (dQty <= 0) return;
                
                string sPrno = ProcessGeneral.GetSafeString(gvWhallo.FocusedColumn.FieldName);
                LoadSoInfo(lpk, sPrno, sItemCode + '-' + sItemName);
            }
        }

        private void LoadSoInfo(long lPk, string sPrno, string sItem)
        {
            DataTable dtSrc = _infPoApp.LoadSoinfoinStkAll_00161(lPk, sPrno);
            if (dtSrc == null || dtSrc.Rows.Count==0) return;

            string sFi = string.Join(",", dtSrc.Columns.OfType<DataRow>().Select(r => r[0].ToString()));
            using (var frmSel = new FrmF4Grid()
            {
                Text = sItem,
                Caption = @"SO Info.",
                DataSource = dtSrc,
                StrColE = sFi,
                iDL = 2
            })
            {
                frmSel.FireEventData += (s1, e1) => { };
                frmSel.WindowState = FormWindowState.Maximized;
                frmSel.ShowDialog();
            }
        }

        private void LoadStkInfo(long lPk, string sItem)
        {
            DataTable dtSrc = _infPoApp.LoadStkRecinStkAll_00161W(lPk);
            if (dtSrc == null || dtSrc.Rows.Count == 0) return;

            string sFi = string.Join(",", dtSrc.Columns.OfType<DataRow>().Select(r => r[0].ToString()));
            using (var frmSel = new FrmF4Grid()
            {
                Text = sItem,
                Caption = @"Stock Info.",
                DataSource = dtSrc,
                StrColE = sFi,
                iDL = 2
            })
            {
                frmSel.FireEventData += (s1, e1) => { };
                frmSel.WindowState = FormWindowState.Maximized;
                frmSel.ShowDialog();
            }
        }

        #endregion
    }
}
