using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Common;
using CNY_WH.Info;
using CNY_WH.Report;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;

namespace CNY_WH.WForm
{
    public partial class FrmWHStockCard : FrmBase
    {
        #region Declaration

        private readonly InfoWhMf _infMae;
        private readonly InfoWhReports _infRep;
        private readonly string _sItemCode;
        public string CsFormActiveName;

        private const string ConColor = "Color";
        private const string ConBalQty = "BalanceQty";
      
        private const string ConTransType = "Transact Type";
        private const string ConDateTrans = "Transact Date";
        private const string ConDateIss = "Date Issued";
        
        private const string ConDocNo = "Document No.";
        private const string ConWh = "WareHouse";
        private const string ConQtyIn = "QtyIn";
        private const string ConQtyOut = "QtyOut";
        private const string ConDescription = "Description";

        private double _dInQty = 0;
        private double _dOutQty = 0;
        private double _dQty = 0;
        private double _dOb = 0;
        private double _dBal = 0;
        private DataTable _dtOb;

        #endregion

        #region Init Form

        public FrmWHStockCard(string sItemCode)
        {
            InitializeComponent();
            _infMae = new InfoWhMf();
            _infRep= new InfoWhReports();
            _dtOb = new DataTable();
            _sItemCode = sItemCode;

            Cls001GenFunctions.InitGridSelectCell(gcBatch, gvBatch, false);
            Cls001GenFunctions.InitGridSelectCell(gcTrans, gvTrans, false);
            gvBatch.OptionsView.ShowFooter = true;
            gvTrans.OptionsView.ShowFooter = true;

            this.Load += FrmStockCard_Load;
            this.FormClosed += FrmWHStockCard_FormClosed;
            txtWH.Properties.CharacterCasing = CharacterCasing.Upper;
            txtMatCode.Properties.CharacterCasing = CharacterCasing.Upper;

            gvBatch.CustomDrawFooter += GvBatch_CustomDrawFooter;
            gvBatch.CustomDrawFooterCell += GvBatch_CustomDrawFooterCell;

            gvTrans.CustomSummaryCalculate += GvTrans_CustomSummaryCalculate;
            gvTrans.CustomDrawFooter += GvTrans_CustomDrawFooter;
            gvTrans.CustomDrawFooterCell += GvTrans_CustomDrawFooterCell;
        }
        
        private void FrmStockCard_Load(object sender, EventArgs e)
        {
            #region ctrl_Text

            List<TextEdit> lstText = panelControl1.FindAllChildrenByType<TextEdit>().ToList();
            foreach (var eText in lstText)
            {
                eText.KeyDown += EText_KeyDown;
                eText.EditValueChanged += EText_EditValueChanged;
                eText.Validating += EText_Validating;
            }

            #endregion

            HideMenuB();
            txtMatCode.EditValue = _sItemCode;
            LoadGridInfo();
        }

        private void HideMenuB()
        {
            AllowGenerate = true;
            AllowRefresh = true;
            AllowPrint = true;
            AllowClose = true;

            EnableGenerate = true;
            EnableRefresh = true;
            EnablePrint = true;
            EnableClose = true;

            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowFind = false;
            AllowSave = false;
            AllowCancel = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = false;
            AllowRevision = false;
            AllowCollapse = false;
            AllowExpand = false;

        }

        private void FrmWHStockCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessGeneral.EnableFormWhenEndEdit(CsFormActiveName);
        }

        #endregion

        #region TextKey

        private void EText_Validating(object sender, CancelEventArgs e)
        {
            //var cTxtName = sender as TextEdit;
            //if (cTxtName == null) return;
            //if (string.IsNullOrEmpty(cTxtName.EditValue.ToString().Trim())) return;

            //switch (cTxtName.Name)
            //{
            //    #region Gen

            //    case "txtTransFrom":
            //        e.Cancel = !ClsWhGenFunctions.IsDate(txtTransFrom.EditValue.ToString());
            //        break;

            //    case "txtTransTo":
            //        e.Cancel = !ClsWhGenFunctions.IsDate(txtTransTo.EditValue.ToString());
            //        break;

            //        #endregion
            //}
        }
        private void EText_EditValueChanged(object sender, EventArgs e)
        {
            var cTxtName = sender as TextEdit;
            if (string.IsNullOrEmpty(cTxtName.EditValue.ToString().Trim())) return;

            try
            {
                switch (cTxtName.Name)
                {
                    case "txtTransFrom":
                        if (!ClsWhGenFunctions.IsDate(txtTransFrom.EditValue.ToString())) return;
                        txtTransFrom.EditValue = ClsWhGenFunctions.GetSafeDatetimeDmy(txtTransFrom.EditValue);
                        break;

                    case "txtTransTo":
                        if (!ClsWhGenFunctions.IsDate(txtTransTo.EditValue.ToString())) return;
                        txtTransTo.EditValue = ClsWhGenFunctions.GetSafeDatetimeDmy(txtTransTo.EditValue);
                        break;

                    case "txtMatCode":
                        string sCode = ProcessGeneral.GetSafeString(txtMatCode.EditValue).ToUpper();
                        DataTable dtMat = _infMae.LoadItemInfo132(sCode);
                        if (dtMat == null || dtMat.Rows.Count <= 0) return;
                        txtMatDesc.EditValue = ProcessGeneral.GetSafeString(dtMat.Rows[0]["Item Name"]);
                        txtUnit.EditValue = ProcessGeneral.GetSafeString(dtMat.Rows[0]["Item Unit Name"]);
                        break;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Invalid Data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        case "txtMatCode":
                            txtWH.Focus();
                            break;
                        case "txtWH":
                            txtTransFrom.Focus();
                            break;
                        case "txtTransFrom":
                            txtTransTo.Focus();
                            break;
                        case "txtTransTo":
                        case "txtOBQty":
                        case "txtUnit":
                            txtMatCode.Focus();
                            break;
                    }

                    #endregion
                    break;

                case Keys.Up:
                case Keys.Escape:
                    #region Escape

                    switch (cTxtName.Name)
                    {
                        case "txtMatCode":
                            txtTransTo.Focus();
                            break;
                        case "txtWH":
                            txtMatCode.Focus();
                            break;
                        case "txtTransFrom":
                            txtWH.Focus();
                            break;
                        case "txtTransTo":
                        case "txtOBQty":
                        case "txtUnit":
                            txtTransFrom.Focus();
                            break;
                    }

                    #endregion
                    break;

                case Keys.F4:
                    #region F4

                    switch (cTxtName.Name)
                    {
                        case "txtWH":
                            #region Fr Wh

                            using (var frmSel = new FrmF4Grid()
                            {
                                Text = @"WareHouse Listing",
                                Caption = "WareHouse Listing",
                                DataSource = _infMae.LoadF4List110("CNY001, CNY002", "CNYMF004", $"CNY001 LIKE '%{sTxtVa}%'"),
                                StrColE = "Code,Description",
                                iDL = 2
                            })
                            {
                                frmSel.FireEventData += (s1, e1) =>
                                {
                                    string[] sWh = e1.ID.Split(new char[] { '|' });
                                    txtWH.EditValue = sWh[0].ToUpper().Trim();
                                    txtWHDesc.EditValue = sWh[1].Trim();
                                };
                                frmSel.ShowDialog();
                            }

                            #endregion
                            break;
                    }

                    #endregion
                    break;
            }
        }

        #endregion

        #region Funs

        protected override void PerformPrint()
        {
            string sCode = ProcessGeneral.GetSafeString(txtMatCode.EditValue);
            if (string.IsNullOrEmpty(sCode))
            {
                XtraMessageBox.Show("Invalid data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sWh = ProcessGeneral.GetSafeString(txtWH.EditValue);
            // Set Transaction Date
            if (string.IsNullOrEmpty(txtTransFrom.Text))
            {
                if (!string.IsNullOrEmpty(txtTransTo.Text))
                {
                    txtTransFrom.EditValue = txtTransTo.Text;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTransTo.Text))
                {
                    txtTransTo.EditValue = $@"{Cls001MasterFiles.GetServerDate():dd-MMM-yyyy}";
                }
            }
            DateTime dFr = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(txtTransFrom.EditValue);
            DateTime dTo = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(txtTransTo.EditValue);

            _infRep.GetDataforRepStockCard160B(sCode, sWh, dFr, dTo);
            PrintStockCard(sCode, sWh, dFr, dTo);
        }

        private void PrintStockCard(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            RptStockCard repStk = new RptStockCard(sCode, sWh, dFr, dTo);
            ReportPrintTool pt = new ReportPrintTool(repStk);
            Form form = pt.PreviewForm;

            form.MdiParent = this.ParentForm;
            pt.ShowPreview();
            form.Show();
        }

        protected override void PerformGenerate()
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadGridInfo();
            Cursor.Current = Cursors.Default;
        }
        private void LoadGridInfo()
        {
            string sCode = ProcessGeneral.GetSafeString(txtMatCode.EditValue);
            if (string.IsNullOrEmpty(sCode))
            {
                XtraMessageBox.Show("Invalid Code!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sWh = ProcessGeneral.GetSafeString(txtWH.EditValue);
            // Set Transaction Date
            if (string.IsNullOrEmpty(txtTransFrom.Text))
            {
                if (!string.IsNullOrEmpty(txtTransTo.Text))
                {
                    txtTransFrom.EditValue = txtTransTo.Text;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTransTo.Text))
                {
                    txtTransTo.EditValue = $@"{Cls001MasterFiles.GetServerDate():dd-MMM-yyyy}";
                }
            }

            DateTime dFr = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(txtTransFrom.EditValue);
            DateTime dTo = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(txtTransTo.EditValue);
            DataSet dsBatchInfo = _infRep.LoadStockCardInfo_App_159(sCode, sWh, dFr, dTo);

            DataTable dtBatch = dsBatchInfo.Tables[0];
            DataTable dtTrans = dsBatchInfo.Tables[1];
            _dtOb = dsBatchInfo.Tables[2];
            
            Cls001GenFunctions.GridViewCustomCols(gvBatch, dtBatch);
            gcBatch.DataSource = dtBatch;
            gvBatch.BestFitColumns();
            
            Cls001GenFunctions.GridViewCustomCols(gvTrans, dtTrans);
            gcTrans.DataSource = dtTrans;
            gvTrans.BestFitColumns();
            FormatGrid();
        }

        #endregion

        #region Format Grid

        private void FormatGrid()
        {
            try
            {
                // Format Batch Grid
                gvBatch.Columns[ConColor].SummaryItem.SummaryType = SummaryItemType.Custom;
                gvBatch.Columns[ConColor].SummaryItem.DisplayFormat = @"Total";
                gvBatch.Columns[ConBalQty].SummaryItem.SummaryType = SummaryItemType.Sum;
                gvBatch.Columns[ConBalQty].SummaryItem.DisplayFormat = @"{0:N5}";

                gvBatch.Columns[ConBalQty].DisplayFormat.FormatType = FormatType.Numeric;
                gvBatch.Columns[ConBalQty].DisplayFormat.FormatString = "N5";

                // Format Trans Grid
                gvTrans.Columns[ConTransType].SummaryItem.SummaryType = SummaryItemType.Custom;
                gvTrans.Columns[ConTransType].SummaryItem.DisplayFormat = @"Total";
                gvTrans.Columns[ConDateTrans].SummaryItem.SummaryType = SummaryItemType.Custom;
                gvTrans.Columns[ConDateTrans].SummaryItem.DisplayFormat = @"OB";
                gvTrans.Columns[ConDateIss].SummaryItem.SummaryType = SummaryItemType.Custom;
                gvTrans.Columns[ConDateIss].SummaryItem.DisplayFormat = @"{0:N5}";

                gvTrans.Columns[ConQtyIn].SummaryItem.SummaryType = SummaryItemType.Sum;
                gvTrans.Columns[ConQtyIn].SummaryItem.DisplayFormat = @"{0:N5}";
                gvTrans.Columns[ConQtyOut].SummaryItem.SummaryType = SummaryItemType.Sum;
                gvTrans.Columns[ConQtyOut].SummaryItem.DisplayFormat = @"{0:N5}";

                gvTrans.Columns[ConWh].SummaryItem.SummaryType = SummaryItemType.Custom;
                gvTrans.Columns[ConWh].SummaryItem.DisplayFormat = @"Balance";
                gvTrans.Columns[ConDocNo].SummaryItem.SummaryType = SummaryItemType.Custom;
                gvTrans.Columns[ConDocNo].SummaryItem.DisplayFormat = @"{0:N5}";

                gvTrans.Columns[ConQtyIn].DisplayFormat.FormatType = FormatType.Numeric;
                gvTrans.Columns[ConQtyIn].DisplayFormat.FormatString = "N5";
                gvTrans.Columns[ConQtyOut].DisplayFormat.FormatType = FormatType.Numeric;
                gvTrans.Columns[ConQtyOut].DisplayFormat.FormatString = "N5";
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error on Void_FormatGrid()", "Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void GvBatch_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != ConColor && e.Column.FieldName != ConBalQty) return;
            e.Appearance.ForeColor = e.Column.FieldName == ConColor ? Color.Chocolate : Color.Black;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
        }
        private void GvBatch_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            e.Handled = true;
        }

        private void GvTrans_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != ConTransType && e.Column.FieldName != ConDateIss && e.Column.FieldName != ConDateTrans
                && e.Column.FieldName != ConWh && e.Column.FieldName != ConQtyIn && e.Column.FieldName != ConQtyOut
                && e.Column.FieldName != ConDocNo) return;

            e.Appearance.ForeColor = e.Column.FieldName == ConQtyIn || e.Column.FieldName == ConQtyOut ||
                                     e.Column.FieldName == ConDocNo
                ? Color.Black
                : Color.Chocolate;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
        }
        private void GvTrans_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            e.Handled = true;
        }
        private void GvTrans_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                _dInQty = 0;
                _dOutQty = 0;
                _dQty = 0;
            }
            #region Get Qty In-Out

            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch ((e.Item as GridSummaryItem).FieldName)
                {
                    case ConQtyIn:
                        if (e.FieldValue != DBNull.Value)
                        {
                            _dInQty += Convert.ToDouble(e.FieldValue);
                        }
                        break;
                    case ConQtyOut:
                        if (e.FieldValue != DBNull.Value)
                        {
                            _dOutQty += Convert.ToDouble(e.FieldValue);
                        }
                        break;
                    case ConDocNo:
                        _dQty += ProcessGeneral.GetSafeDouble(e.GetValue(ConQtyIn)) - ProcessGeneral.GetSafeDouble(e.GetValue(ConQtyOut));
                        break;
                }
            }

            #endregion

            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch ((e.Item as GridSummaryItem).FieldName)
                {
                    case ConQtyIn:
                        e.TotalValue = _dInQty;
                        break;
                    case ConQtyOut:
                        e.TotalValue = _dOutQty;
                        break;

                    case ConDateIss:
                        if (string.IsNullOrEmpty(txtTransFrom.Text))
                        {
                            _dOb = 0;
                        }
                        else
                        {
                            _dOb = _dtOb == null || _dtOb.Rows.Count == 0
                                ? 0
                                : ProcessGeneral.GetSafeDouble(_dtOb.Rows[0][1]);
                        }
                        e.TotalValue = _dOb;
                        break;

                    case ConDocNo:
                        _dBal = _dOb + _dQty;
                        e.TotalValue = _dBal;
                        txtOBQty.EditValue = _dBal;
                        break;
                }
            }
        }

        #endregion
    }
}
