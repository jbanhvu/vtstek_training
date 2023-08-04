using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Class;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_WH.UControl
{
    public partial class XtraUC002ReceiptNoteAE : UserControl
    {
        #region Properties
        Inf_002ReceiptNote _inf002 = new Inf_002ReceiptNote();
        Cls_002ReceiptNote _cls = new Cls_002ReceiptNote();
        Inf_003ReceiptNoteDetail _inf003 = new Inf_003ReceiptNoteDetail();
        List<Tuple<Control, int>> _list = new List<Tuple<Control, int>>();
        Int64 _pk;
        private SystemGetCodeRule _sysCode;
        private int PK_CNY00011B = -1;
        OpenFileDialog op = new OpenFileDialog();
        #endregion

        #region Contractor
        public XtraUC002ReceiptNoteAE(int pk)
        {
            InitializeComponent();
            _pk = pk;

            Declare_GridView(gcDetail, gvDetail);

            DeclareGridViewEvent();


            GenerateEventSearchlookupEdit();

            //Load slue
            LoadDataToSlue();

            //Keydown
            Keydown();

            //Init gridview
            //InitGridview();


            ButtonClick();
            //Load Data control

            if (_pk == -1)
            {
                DisplayDataForAdding();

            }
            else
            {
                DisplayDataForEditing();
            }

        }


        #region " Griview "
        private void LoadGridDetail()
        {
            DataTable tba = _inf002.sp_ReceiptNote_Select(_pk);
            gcDetail.DataSource = tba;

            gvDetail.Columns["CNY00011FPK"].Visible = false;
            gvDetail.Columns["CNY00011PK"].Visible = false;
            gvDetail.Columns["CNY00007PK"].Visible = false;

            gvDetail.BestFitColumns();
        }

        private void DeclareGridViewEvent()
        {

            gvDetail.LeftCoordChanged += gvDeliveryList_LeftCoordChanged;
            gvDetail.MouseMove += gvDeliveryList_MouseMove;
            gvDetail.TopRowChanged += gvDeliveryList_TopRowChanged;
            gvDetail.FocusedColumnChanged += gvDeliveryList_FocusedColumnChanged;
            gvDetail.FocusedRowChanged += gvDeliveryList_FocusedRowChanged;
            gcDetail.Paint += gcDeliveryList_Paint;
        }


        private void InsertGridDetail()
        {
            gvDetail.AddNewRow();
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            dr["CNY00011FPK"] = -1; dr["CNY00011PK"] = _pk;
            gvDetail.UpdateCurrentRow();

        }

        #endregion

        #endregion

        

        #region Keydown
        public void Keydown()
        {
            #region Tab 1
            txtAddress.KeyDown += txt_KeyDown;
            #endregion

            #region Tab 2

            #endregion
        }
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            //Find control
            Control ct = sender as Control;

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab) || (e.KeyCode == Keys.Down))
            {
                FocusNextControl(ct);
                e.Handled = true;
            }
            else if ((e.KeyCode == Keys.Escape) || (e.KeyCode == Keys.Up))
            {
                FocusPreviousControl(ct);
            }
        }

        #region FocusNextControl
        void FocusNextControl(Control ct)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Item1 == ct)
                {
                    try
                    {
                        if (_list[i + 1].Item1.Enabled)
                        {
                            _list[i + 1].Item1.Focus();
                        }
                        else
                        {
                            FocusNextControl(_list[i + 1].Item1);
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    break;
                }
            }
        }
        void FocusPreviousControl(Control ct)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Item1 == ct)
                {
                    try
                    {
                        if (_list[i - 1].Item1.Enabled)
                        {
                            _list[i - 1].Item1.Focus();
                        }
                        else
                        {
                            FocusPreviousControl(_list[i - 1].Item1);
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    break;
                }
            }
        }
        #endregion
        #endregion

        #region "khai bao Gridview"

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
            #endregion
        }

        #endregion

        #region Gridview event



        private void gcDeliveryList_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangleSelection.PaintGridViewSelectionRect((GridControl)sender, e);
        }

        private void gvDeliveryList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_TopRowChanged(object sender, EventArgs e)
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_MouseMove(object sender, MouseEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_LeftCoordChanged(object sender, EventArgs e)
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDeliveryList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "DeliveryTerm":
                case "DeliveryMethod":
                case "PaymentTerm":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
                default:
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                    break;
            }
        }

        private void gvDeliveryList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

        private void gvDeliveryList_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
        }



        public void ButtonClick()
        {
            btnInsertDetail.Click += BtnInsertDetail_Click;
            btnDeleteDetail.Click += BtnDeleteDetail_Click; 
        }

        private void BtnInsertDetail_Click(object sender, EventArgs e)
        {
            InsertGridDetail();
        }

        private void BtnDeleteDetail_Click(object sender, EventArgs e)
        {
            gvDetail.DeleteRow(gvDetail.FocusedRowHandle);
        }

        #endregion

        #region Generate Event Search lookup Edit
        public void GenerateEventSearchlookupEdit()
        {
            slueSupplier.EditValueChanged += slue_EditValueChanged;
            slueStatus.EditValueChanged += slue_EditValueChanged;
            slueSupplier.Popup += slue_Popup;
            slueStatus.Popup += slue_Popup;
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
        #endregion

        #region Load Data To Search lookup Edit
        public void LoadDataToSlue()
        {
            //Category
            slueSupplier.Properties.DataSource = _inf002.Excute("SELECT RTRIM(CNY001) [Code],RTRIM(CNY002) [Name],CNY003 [Note] FROM dbo.CNYMF001");
            slueSupplier.Properties.DisplayMember = "Code";
            slueSupplier.Properties.ValueMember = "Code";
            slueSupplier.Properties.NullText = null;

            //Sale man
            slueStatus.Properties.DataSource = _inf002.Excute("SELECT RTRIM(CNY001) [Code],RTRIM(CNY002) [Name],CNY003 [Note] FROM dbo.CNYMF002");
            slueStatus.Properties.DisplayMember = "Code";
            slueStatus.Properties.ValueMember = "Code";
            slueStatus.Properties.NullText = null;

        }
        #endregion

        #region Display Data For Editing
        public void DisplayDataForEditing()
        {
            DataTable dtHeader = _inf002.Excute("SELECT * FROM dbo.CNY00011 WHERE PK=" + _pk);
            if (dtHeader.Rows.Count == 0) return;
            _pk = ProcessGeneral.GetSafeInt(dtHeader.Rows[0]["PK"]);
            txtAddress.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CNY007"]);
        }
        #endregion
 
        #region Display Data For Adding
        public void DisplayDataForAdding()
        {

            deCreatedDate.EditValue = DateTime.Today;

        }
        #endregion

        #region Save
        public void Save()
        {
            #region Check input
            //if (!CheckInput())
            //    return;
            #endregion

            #region Get Info
            _cls.PK = _pk;
            #endregion

            #region Save and show message
            //Update data
            DataTable dtSaveResult = _inf002.sp_ReceiptNote_Update(_cls);
            //Get result info
            string msg = ProcessGeneral.GetSafeString(dtSaveResult.Rows[0]["ErrMsg"]);
            //Set new ID
            if (_cls.PK == -1)
            {
                //Update new ID
                _cls.PK = ProcessGeneral.GetSafeInt(dtSaveResult.Rows[0]["IDENTITY"]);
                _pk = _cls.PK;
                _sysCode.SaveCodeData();
            }


            XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DisplayDataForEditing();
            #endregion
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

        #region Check input
   
        #endregion
    }
}
