using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CNY_BaseSys.WForm
{
    public partial class FrmAllocateQty : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"



        public event OnGetValueHandler OnGetValue = null;
        private decimal _totalDemand = 0;
        private decimal _stockQty = 0;
        private DataTable _dtS;
        private readonly RepositoryItemSpinEdit _repositorySpinPoQty;
        #endregion


        #region "Contructor"


        public FrmAllocateQty(decimal totalDemand, decimal stockQty , DataTable dtS , int decimalRow)
        {

            InitializeComponent();

            _repositorySpinPoQty = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = 0,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
                EditMask = string.Format("N{0}", decimalRow)
            };

            _repositorySpinPoQty.Buttons.Clear();

            _totalDemand = totalDemand;
            _stockQty = stockQty;
            _dtS = dtS;
            txtTotalDemand.Text = string.Format(@"{0:#,0.##########}", _totalDemand);
            txtStockQty.Text = string.Format(@"{0:#,0.##########}", _stockQty);
            txtDemandQty.Text = string.Format(@"{0:#,0.##########}", _totalDemand - _stockQty);
            txtTotalDemand.Properties.ReadOnly = true;
            txtDemandQty.Properties.ReadOnly = true;
            txtStockQty.Properties.ReadOnly = true;
            SetUpMainGrid();



            this.Load += Frm_Load;

            this.FormClosed += FrmAllocateQty_FormClosed;
        }
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                #region "System"
               
                case Keys.Escape:
                    {

                        this.Close();
                        return true;
                    }
                #endregion

         

            }
            return base.ProcessCmdKey(ref message, keys);



        }
        private void FrmAllocateQty_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataTable dtS = (DataTable) gcMain.DataSource;
            var q1 = dtS.AsEnumerable().Where(p => p.Field<bool>("IsChanged")).ToList();

            DataTable dtTemp = q1.Count > 0 ? q1.CopyToDataTable() : dtS.Clone();


            OnGetValue?.Invoke(this, new OnGetValueEventArgs
            {
                DtTemp = dtTemp
            });
            this.Close();
        }

        private void LoadDataGridMain()
        {
            gvMain.BeginUpdate();
            gvMain.Columns.Clear();
            gcMain.DataSource = null;
            gcMain.DataSource = _dtS;
            ProcessGeneral.HideVisibleColumnsGridView(gvMain,false, "CNY00020PK", "RowState", "CNY00055BPK", "IsChanged", "IsTable");
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["ProductCode"], "Product Code", DefaultBoolean.Default, HorzAlignment.Near, FixedStyle.None);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Reference"], "Reference", DefaultBoolean.Default, HorzAlignment.Near, FixedStyle.None);



            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["TotalPRQuantity"], "Demand Qty", DefaultBoolean.Default, HorzAlignment.Center, FixedStyle.None);
            gvMain.Columns["TotalPRQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            gvMain.Columns["TotalPRQuantity"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatDefaultDecimal(false, false);
            gvMain.Columns["TotalPRQuantity"].SummaryItem.SummaryType = SummaryItemType.Sum;
            gvMain.Columns["TotalPRQuantity"].SummaryItem.DisplayFormat = FunctionFormatModule.StrFormatDefaultDecimal(false, true);


            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["ReleasePRQuantity"], "Released Qty", DefaultBoolean.Default, HorzAlignment.Center, FixedStyle.None);
            gvMain.Columns["ReleasePRQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            gvMain.Columns["ReleasePRQuantity"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatDefaultDecimal(false, false);
            gvMain.Columns["ReleasePRQuantity"].SummaryItem.SummaryType = SummaryItemType.Sum;
            gvMain.Columns["ReleasePRQuantity"].SummaryItem.DisplayFormat = FunctionFormatModule.StrFormatDefaultDecimal(false, true);

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["LeftPRQuantity"], "Left Qty", DefaultBoolean.Default, HorzAlignment.Center, FixedStyle.None);
            gvMain.Columns["LeftPRQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            gvMain.Columns["LeftPRQuantity"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatDefaultDecimal(false, false);
            gvMain.Columns["LeftPRQuantity"].SummaryItem.SummaryType = SummaryItemType.Sum;
            gvMain.Columns["LeftPRQuantity"].SummaryItem.DisplayFormat = FunctionFormatModule.StrFormatDefaultDecimal(false, true);


            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["PRQuantity"], "Allocate Qty", DefaultBoolean.Default, HorzAlignment.Center, FixedStyle.None);
            gvMain.Columns["PRQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            gvMain.Columns["PRQuantity"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatDefaultDecimal(false, false);
            gvMain.Columns["PRQuantity"].SummaryItem.SummaryType = SummaryItemType.Sum;
            gvMain.Columns["PRQuantity"].SummaryItem.DisplayFormat = FunctionFormatModule.StrFormatDefaultDecimal(false, true);

        
         

    
         

            gvMain.BestFitColumns();
            gvMain.EndUpdate();


        }

        private void Frm_Load(object sender, EventArgs e)
        {
            LoadDataGridMain();
        }


      


        #endregion

      


        #region "GridView Event && Init Grid"

        
  


     
       

    


        private void SetUpMainGrid()
        {


            gcMain.UseEmbeddedNavigator = true;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = false;
            gvMain.OptionsCustomization.AllowFilter = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
 
            gvMain.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

            gvMain.OptionsView.AllowHtmlDrawHeaders = true;
            gvMain.OptionsView.AllowHtmlDrawGroups = true;
            gvMain.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Wrap;
            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;

            gvMain.OptionsView.ShowFooter = true;


            //   gridView1.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;



          
            gvMain.OptionsPrint.AllowCancelPrintExport = true;
            gvMain.OptionsPrint.AllowMultilineHeaders = true;
            gvMain.OptionsPrint.AutoResetPrintDocument = true;
            gvMain.OptionsPrint.AutoWidth = false;
            gvMain.OptionsPrint.PrintHeader = false;
            gvMain.OptionsPrint.PrintPreview = true;
            gvMain.OptionsPrint.PrintSelectedRowsOnly = false;
            gvMain.OptionsPrint.UsePrintStyles = false;

            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvMain.OptionsMenu.EnableFooterMenu = false;
            gvMain.OptionsMenu.EnableColumnMenu = false;




            gvMain.RowCellStyle += gvMain_RowCellStyle;

            gvMain.LeftCoordChanged += gvMain_LeftCoordChanged;
            gvMain.MouseMove += gvMain_MouseMove;
            gvMain.TopRowChanged += gvMain_TopRowChanged;
            gvMain.FocusedColumnChanged += gvMain_FocusedColumnChanged;
            gvMain.FocusedRowChanged += gvMain_FocusedRowChanged;
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;

            gcMain.Paint += gcMain_Paint;
            gcMain.KeyDown += gcMain_KeyDown;
            gcMain.EditorKeyDown += gcMain_EditorKeyDown;
            gvMain.ShowingEditor += gvMain_ShowingEditor;

            gvMain.CustomDrawFooter += gvMain_CustomDrawFooter;
            gvMain.CustomDrawFooterCell += gvMain_CustomDrawFooterCell;
            gvMain.CustomColumnDisplayText += GvMain_CustomColumnDisplayText;
            gvMain.CustomRowCellEdit += gvMain_CustomRowCellEdit;
            gvMain.CellValueChanged += GvMain_CellValueChanged;
            gcMain.ForceInitialize();



        }
        private bool _isEdit = true;
        private void GvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (!_isEdit) return;
            GridView gv = (GridView)sender;
            if (gv == null) return;
            gv.UpdateCurrentRow();
            int rH = e.RowHandle;
            if (!ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rH, "IsChanged")))
            {
                gv.SetRowCellValue(rH, "IsChanged", true);
            }
            
        }

        private void gvMain_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "PRQuantity":
                    e.RepositoryItem = _repositorySpinPoQty;
                    break;
            }
        }

        private void gvMain_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gvMain_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "TotalPRQuantity" && e.Column.FieldName != "ReleasePRQuantity" && e.Column.FieldName != "LeftPRQuantity" && e.Column.FieldName != "PRQuantity") return;
            Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
            using (brush)
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            e.Appearance.ForeColor = Color.Chocolate;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            e.Handled = true;
        }
        private void GvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            if (!e.IsForGroupRow)
            {
                if (fieldName == "ReleasePRQuantity" || fieldName == "LeftPRQuantity" || fieldName == "PRQuantity")
                {
                    string text = e.DisplayText;
                    if (text == "0")
                    {
                        e.DisplayText = "";
                    }

                }
            }
        }

      

        private bool _checkKeyDown;
        private void gcMain_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDown)
            {
                gcMain_KeyDown(sender, e);
            }
            _checkKeyDown = false;
        }


        private void gcMain_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = (GridControl)sender;
            if (gc == null) return;
            var gv = (GridView)gc.FocusedView;
            if (gv == null) return;
            GridColumn gColF = gv.FocusedColumn;
            int visibleIndex = gColF.VisibleIndex;
           // string fieldName = gColF.FieldName;

            int rH = gv.FocusedRowHandle;
            _checkKeyDown = true;


            #region "Key Enter"

            if (e.KeyCode == Keys.Enter)
            {
                gv.SelectedNextCell(rH, visibleIndex);
                return;
            }
            #endregion


     



        }

    
        #region "Key Down"



        #endregion



        private void gvMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            e.Cancel = gCol.FieldName != "PRQuantity";
        }
        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            int visibleIndex = gCol.VisibleIndex;
            if (visibleIndex < 0) return;
            string fieldName = gCol.FieldName;

            switch (fieldName)
            {
                case "Reference":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkGreen;
                    break;
                case "LeftPRQuantity":
                case "ReleasePRQuantity":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkRed;
                    break;
                case "PRQuantity":
                case "TotalPRQuantity":
                    e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                    break;
            }

            if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(rH, gCol))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            switch (fieldName)
            {

                case "PRQuantity":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                    }
                    break;
                default:
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;

            }





        }

        private void gvMain_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcMain_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
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
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "RowState"));
            if (rowState == DataStatus.Insert.ToString())
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }
            else if (rowState == DataStatus.Update.ToString())
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.MediumAquamarine, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }


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


        #endregion
    }
}
