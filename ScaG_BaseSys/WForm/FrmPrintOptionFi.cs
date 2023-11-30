using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using GridSumCol = DevExpress.Data.SummaryItemType;
using DevExpress.XtraGrid.Views.Base;

namespace CNY_BaseSys.WForm
{
    public partial class FrmPrintOptionFi : DevExpress.XtraEditors.XtraForm
    {

        #region "Property"
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        private readonly RepositoryItemCheckEdit _repositoryCheck;
        private DataTable _dtS;
        public event PrintBoMOptionHandler OnTransferData = null;
        #endregion

        #region "Contructor"


        public FrmPrintOptionFi(DataTable dtS)
        {
            InitializeComponent();
            _repositoryCheck = new RepositoryItemCheckEdit { AutoHeight = false };
            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            this._dtS = dtS;
            SetUpMainGrid();
            this.Load += FrmPrintOptionFi_Load;
            btnCancel.Click += BtnCancel_Click;
            btnNextFinish.Click += BtnNextFinish_Click;
        }

        private void FrmPrintOptionFi_Load(object sender, EventArgs e)
        {
            LoadDataAgreeGridView(_dtS);
        }

        #endregion
        #region "Process Agree Grid Final"





        private void CreateBandedGridHeader(BandedGridView gv, DataTable dtS)
        {


            GridBand[] arrBand = new GridBand[dtS.Columns.Count];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {
                BandedGridColumn gCol = new BandedGridColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new GridBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;

                string colName = col.ColumnName;
                string displayText = "";
                HorzAlignment horz = HorzAlignment.Center;
                string tag = colName;
                if (colName == "CNY00050PKFi")
                {
                    displayText = "CNY00050PKFi";
                   


                }
                else if (colName == "ColorCheck")
                {
                    displayText = ".";

                 
                }
                else if (colName.StartsWith("RMDescription_002"))
                {
                    displayText = "Finishing Color";
                    horz = HorzAlignment.Near;
                }
                else if (colName.Contains("-"))
                {
                    string quality = ProcessGeneral.GetDescriptionInString(colName, "-");
                    displayText = quality;
                    tag = quality;
                }
                else
                {
                    displayText = colName;
                }



                
    
             



                gCol.AppearanceCell.TextOptions.HAlignment = horz;
                gCol.AppearanceHeader.TextOptions.HAlignment = horz;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = horz;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].Tag = tag;

                gCol.Tag = tag;
                gCol.Caption = displayText;
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;




                gv.Columns.Add(gCol);
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].VisibleIndex = 0;
                arrBand[i].Width = 100;
                i++;
            }


     


            gv.Bands.AddRange(new[] { arrBand[0], arrBand[1], arrBand[2] });

            GridBand gParent = new GridBand();
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            gParent.Caption = @"Quality";
            gParent.VisibleIndex = 3;
            gParent.Width = 100;


            for (int t = 3; t < arrBand.Length; t++)
            {
                arrBand[t].VisibleIndex = t;
                gParent.Children.Add(arrBand[t]);
            }
            gv.Bands.Add(gParent);

        }













        private void BestFitBandsGridAgree(BandedGridView view)
        {

            view.BeginUpdate();
            view.OptionsView.ShowColumnHeaders = true;


            foreach (BandedGridColumn col in view.Columns)
            {
                GridBand gBand = col.OwnerBand;
                string fieldName = col.FieldName;
                if (fieldName == "CNY00050PKFi")
                {
                    gBand.Visible = false;
                }
                else if(fieldName.StartsWith("Value-"))
                {
                    gBand.Visible = false;
                }
                else if (fieldName == "RMDescription_002")
                {
                    col.Caption = gBand.Caption;
                    int width = col.GetBestWidth();
                    col.Width = width > 230 ? 230 : width;
                }
                else
                {
                    col.Caption = gBand.Caption;
                    col.Width = col.GetBestWidth() + 20;

                }


            }

            // view.BestFitColumns();
            //   view.Columns["MenuCode"].Width += 20;
            view.OptionsView.ShowColumnHeaders = false;

            

            view.EndUpdate();



            //view.Bands[0].Visible = false;


        }


        private void LoadDataAgreeGridView(DataTable dtS)
        {



            gvMain.BeginUpdate();
            gvMain.Bands.Clear();
            gvMain.Columns.Clear();
            gcMain.DataSource = null;

            CreateBandedGridHeader(gvMain, dtS);
            gcMain.DataSource = dtS;
            BestFitBandsGridAgree(gvMain);

            gvMain.EndUpdate();




        }





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
            gvMain.OptionsView.RowAutoHeight = true;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;



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

            gvMain.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvMain.OptionsMenu.EnableFooterMenu = false;
            gvMain.OptionsMenu.EnableColumnMenu = false;



            gvMain.RowCellStyle += gvAgree_RowCellStyle;

            gvMain.LeftCoordChanged += gvAgree_LeftCoordChanged;
            gvMain.MouseMove += gvAgree_MouseMove;
            gvMain.TopRowChanged += gvAgree_TopRowChanged;
            gvMain.FocusedColumnChanged += gvAgree_FocusedColumnChanged;
            gvMain.FocusedRowChanged += gvAgree_FocusedRowChanged;
            gvMain.RowCountChanged += gvAgree_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvAgree_CustomDrawRowIndicator;

            gcMain.Paint += gcAgree_Paint;
            gvMain.CustomRowCellEdit += GvAgree_CustomRowCellEdit;
            gvMain.ShowingEditor += gvAgree_ShowingEditor;
        
            gvMain.CustomColumnDisplayText += GvAgree_CustomColumnDisplayText;
            gcMain.ForceInitialize();


        }

        private void GvAgree_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
            if (fieldName.Contains("-"))
            {
                e.DisplayText = "";
            }
        }





        #region "KeyDown"




    
 
        #endregion

        private void GvAgree_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            if (fieldName == "ColorCheck")
            {
                e.RepositoryItem = _repositoryCheck;
            }
            else if (fieldName == "RMDescription_002")
            {
                e.RepositoryItem = _repositoryTextGrid;
            }
            else if (fieldName.StartsWith("Check-"))
            {
              


                string quality = ProcessGeneral.GetDescriptionInString(fieldName, "-");
                string value = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, string.Format("Value-{0}", quality)));
                if (!string.IsNullOrEmpty(value))
                {
                    e.RepositoryItem = _repositoryCheck;

                }
                else
                {
                    e.RepositoryItem = _repositoryTextGrid;

                }
            }



            

        }

        private void gvAgree_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
            if (fieldName == "ColorCheck")
            {

                e.Cancel = false;
            }
            else if (fieldName.StartsWith("Check-"))
            {
                string quality = ProcessGeneral.GetDescriptionInString(fieldName, "-");
                string value = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, string.Format("Value-{0}", quality)));
                e.Cancel = string.IsNullOrEmpty(value);
            }
            else
            {
                e.Cancel = true;
            }

        }
        private void gvAgree_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
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
                case "RMDescription_002":
                    {
                        e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
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

            if (fieldName == "ColorCheck")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.LightGoldenrodYellow;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName.StartsWith("Check-"))
            {
                string quality = ProcessGeneral.GetDescriptionInString(fieldName, "-");
                string value = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, string.Format("Value-{0}", quality)));
              
                if (!string.IsNullOrEmpty(value))
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                }

            }
            else
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
        }

        private void gvAgree_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvAgree_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvAgree_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvAgree_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvAgree_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcAgree_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvAgree_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvAgree_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //GetStatusPriorityPaintOnRow(GridView gv, int rH)
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;

            LinearGradientBrush backBrush;
            bool isSelected = gv.IsRowSelected(e.RowHandle);
            if (isSelected)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

            if (isSelected)
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

        #endregion



        #region "Button Click Event"


        private void BtnNextFinish_Click(object sender, EventArgs e)
        {
            if (OnTransferData != null)
            {
                
                if (!chkFocus.Focused)
                {
                    chkFocus.SelectNextControl(ActiveControl, true, true, true, true);
                    chkFocus.Focus();
                }

                DataTable dtF = new DataTable();
                dtF.Columns.Add("CNY00050PKFi", typeof(Int64));
                dtF.Columns.Add("QualityCode", typeof(string));
                DataTable dtS = (DataTable) gcMain.DataSource;

                if (dtS != null)
                {
                    int colC = dtS.Columns.Count;
                    foreach (DataRow dr in dtS.Rows)
                    {
                        if (!ProcessGeneral.GetSafeBool(dr["ColorCheck"])) continue;
                        Int64 cny00050PkFi = ProcessGeneral.GetSafeInt64(dr["CNY00050PKFi"]);
                        for (int i = 3; i < colC; i++)
                        {
                            if (i % 2 == 0) continue;
                            string fieldName = dtS.Columns[i].ColumnName;
                            string quality = ProcessGeneral.GetDescriptionInString(fieldName, "-");
                            string value = ProcessGeneral.GetSafeString(dr[string.Format("Value-{0}", quality)]);
                            if(string.IsNullOrEmpty(value)) continue;
                            if(!ProcessGeneral.GetSafeBool(dr[fieldName])) continue;
                            dtF.Rows.Add(cny00050PkFi, quality);

                        }
                    }
                }

                OnTransferData(this, new PrintBoMOptionEventArgs
                {
                    DtFiQlt = dtF

                });
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}