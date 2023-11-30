using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.Info;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.SqlServer.Server;

namespace CNY_BaseSys.WForm
{
    public partial class FrmAttributeGenerate : DevExpress.XtraEditors.XtraForm
    {
        #region "Property & Field"
        private readonly Inf_General _inf = new Inf_General();
        private readonly DataTable _dtItemSelected;
        private readonly string _rmCode;
        private readonly Int64 _pkCode = 0;
        private WaitDialogForm _dlg = null;

        GridHitInfo downHitInfo = null;
        public event GetCodeGenerateHandler OnGenerateData = null;
        private DataTable dtTempS;

        private bool _deleteAttNotNumber = false;
        public bool DeleteAttNotNumber
        {
            get { return this._deleteAttNotNumber; }
            set { this._deleteAttNotNumber = value; }
        }
        #endregion

        #region "Contructor"

        public FrmAttributeGenerate(DataTable dtItemSelected, string rmCode, Int64 pkCode)
        {
            InitializeComponent();
            dtTempS = Ctrl_TableGeneral.TableAttributeTempGenerate();
            this._dtItemSelected = dtItemSelected;


            this._rmCode = rmCode;
            this._pkCode = pkCode;
            GridViewRmSelectedCustomInit(gridControlLoadValue, gvLoadValue);
            GridViewRmSelectedCustomInit(gridControlSelectValue, gvSelectValue);



            this.MinimizeBox = false;
            this.Load += FrmGenerate_Load;

            btnCancel.Click += btnCancel_Click;
            btnNextFinish.Click += btnNextFinish_Click;

            btnRemoveAll.Click += btnRemoveAll_Click;
            btnRemoveOneRow.Click += btnRemoveOneRow_Click;
            btnSelectAll.Click += btnSelectAll_Click;
            btnSelectOneRow.Click += btnSelectOneRow_Click;



        }


        #endregion

        #region "Form Event"

        private void FrmGenerate_Load(object sender, EventArgs e)
        {
            _dlg = new WaitDialogForm();

            DataTable dt;
            if (_pkCode > 0)
            {
                dt = _inf.BOM_LoadGridAttributeGenerate(_pkCode, _dtItemSelected);
            }
            else if (string.IsNullOrEmpty(_rmCode))
            {
                dt = _inf.RMCode_LoadGridAttributeGenerate(_dtItemSelected);
            }
            else
            {
                dt = _inf.RMCode_LoadGridAttributeGenerate(_rmCode, _dtItemSelected);
            }
            dtTempS = dt.Clone();

            if (_deleteAttNotNumber)
            {
                var q1 = dt.AsEnumerable().Where(p => p.Field<string>("DataAttType") != "number").ToList();
                if (q1.Any())
                {
                    foreach (DataRow  drDel in q1)
                    {
                        dt.Rows.Remove(drDel);
                    }
                    dt.AcceptChanges();
                }
            }

            gridControlLoadValue.DataSource = dt;
            gridControlSelectValue.DataSource = dtTempS;
            BestFitColumnsGridViewRmSelected();



            _dlg.Close();



        }
        #endregion

        #region "Button Click Event"



        private void btnNextFinish_Click(object sender, EventArgs e)
        {
            var btn = sender as SimpleButton;
            if (btn == null) return;
            txtFocus.SelectNextControl(ActiveControl, true, true, true, true);

            _dlg = new WaitDialogForm();
            if (OnGenerateData != null)
            {

                DataTable dtSelected = gridControlSelectValue.DataSource as DataTable;
                dtSelected.AcceptChanges();

                OnGenerateData(this, new GetCodeGenerateHEventArgs
                {
                    DtReturn = dtSelected,
                });

            }
            _dlg.Close();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        #endregion

        #region "Process Selected Value"

        #region "Init gridview"


        /// <summary>
        ///     Customize Design GridView
        /// </summary>
        /// <param name="gridControl" type="DevExpress.XtraGrid.GridControl">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="gridView" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void GridViewRmSelectedCustomInit(GridControl gridControl, GridView gridView)
        {
            gridControl.AllowDrop = true;
            gridControl.UseEmbeddedNavigator = true;

            gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridView.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            //   gvRM.OptionsBehavior.AutoPopulateColumns = false;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gridView.OptionsCustomization.AllowColumnMoving = false;
            gridView.OptionsCustomization.AllowQuickHideColumns = true;
            gridView.OptionsCustomization.AllowSort = true;
            gridView.OptionsCustomization.AllowFilter = true;
            gridView.HorzScrollVisibility = ScrollVisibility.Auto;
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsCustomization.AllowColumnResizing = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ShowIndicator = true;
            gridView.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gridView.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gridView.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gridView.OptionsView.ShowAutoFilterRow = false;
            gridView.OptionsView.AllowCellMerge = false;
            // gvRM.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gridView.OptionsNavigation.AutoFocusNewRow = true;
            gridView.OptionsNavigation.UseTabKey = true;

            gridView.FocusRectStyle = DrawFocusRectStyle.CellFocus;

            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView.OptionsView.EnableAppearanceEvenRow = false;
            gridView.OptionsView.EnableAppearanceOddRow = false;

            gridView.OptionsView.ShowFooter = true;


            //   gvRM.RowHeight = 25;

            gridView.OptionsFind.AllowFindPanel = true;
            //gvRM.OptionsFind.AlwaysVisible = true;//==>false==>gvRM.OptionsFind.ShowCloseButton = true;
            gridView.OptionsFind.AlwaysVisible = false;
            gridView.OptionsFind.ShowCloseButton = true;
            gridView.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gridView)
            {
                IsPerFormEvent = true,
            };

            GridColumn[] arrGridCol = new GridColumn[6];

            #region "Init Column"

            arrGridCol[0] = new GridColumn
            {
                Caption = @"AttibutePK",
                FieldName = "AttibutePK",
                Name = "AttibutePK",
                Visible = true,
                VisibleIndex = 0,
                Fixed = FixedStyle.Left,
                //  ColumnEdit = _chkedit,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };


            arrGridCol[1] = new GridColumn
            {
                Caption = @"Attribute Code",
                FieldName = "AttibuteCode",
                Name = "AttibuteCode",
                Visible = true,
                VisibleIndex = 1,
                Fixed = FixedStyle.Left,
                //  ColumnEdit = _chkedit,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };

            arrGridCol[2] = new GridColumn
            {
                Caption = @"Attribute Name",
                FieldName = "AttibuteName",
                Name = "AttibuteName",
                Visible = true,
                VisibleIndex = 2,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };

            arrGridCol[3] = new GridColumn
            {
                Caption = @"Attribute Value",
                FieldName = "AttibuteValue",
                Name = "AttibuteValue",
                Visible = true,
                VisibleIndex = 3,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"{0:N0} (item)", },
            };


            arrGridCol[4] = new GridColumn
            {
                Caption = @"PK",
                FieldName = "PK",
                Name = "PK",
                Visible = true,
                VisibleIndex = 4,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                // SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"{0:N0} (item)", },
            };

           

            arrGridCol[5] = new GridColumn
            {
                Caption = @"DataAttType",
                FieldName = "DataAttType",
                Name = "DataAttType",
                Visible = true,
                VisibleIndex = 5,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                // SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"{0:N0} (item)", },
            };


            #endregion

            gridView.Columns.AddRange(arrGridCol);




            gridControl.DragOver += gridControl_DragOver;
            gridControl.DragDrop += gridControl_DragDrop;
            gridView.MouseMove += gridView_MouseMove;
            gridView.MouseDown += gridView_MouseDown;
            gridView.KeyDown += gridView_KeyDown;
            gridView.DoubleClick += gridView_DoubleClick;
            gridView.CustomDrawFooter += gridView_CustomDrawFooter;
            gridView.CustomDrawFooterCell += gridView_CustomDrawFooterCell;
            gridView.RowCountChanged += gridView_RowCountChanged;
            gridView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            gridView.RowStyle += gridView_RowStyle;
            gridView.CustomDrawCell += gridView_CustomDrawCell;
            gridControl.DataSource = dtTempS;

            ProcessGeneral.HideVisibleColumnsGridView(gridView, false, "AttibutePK", "PK", "AttibuteValue", "DataAttType");
            gridControl.ForceInitialize();

        }






        #endregion

        #region "drag and drop"

        private DragData GetDragData(GridView view)
        {

            if (view == null || view.SelectedRowsCount == 0) return null;
            DragData result = new DragData
            {
                gcDrag = view.GridControl,
                gvDrag = view,
                sourceData = ((DataTable)view.GridControl.DataSource).Clone()
            };

            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                result.sourceData.ImportRow(view.GetDataRow(view.GetSelectedRows()[i]));
            }
            return result;
        }

        #endregion

        #region "gridview event"

        private void gridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.OptionsView.ShowAutoFilterRow || !gv.IsDataRow(e.RowHandle)) return;
            string filterCellText = gv.GetRowCellDisplayText(GridControl.AutoFilterRowHandle, e.Column);
            if (String.IsNullOrEmpty(filterCellText)) return;
            int filterTextIndex = e.DisplayText.IndexOf(filterCellText, StringComparison.CurrentCultureIgnoreCase);
            if (filterTextIndex == -1) return;
            XPaint.Graphics.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, filterCellText, e.Appearance, Color.Black, Color.Gold, false,
                filterTextIndex);
            e.Handled = true;
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = GridCellColor.BackColorSelectedRow;
            e.Appearance.BackColor2 = GridCellColor.BackColor2ShowEditor;
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }

        private void gridView_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gridView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "AttibuteCode" && e.Column.FieldName != "AttibuteValue") return;
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "AttibuteCode")
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                e.Graphics.DrawString(@"   " + e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            }
            else
            {
                e.Appearance.ForeColor = Color.Chocolate;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            }
            e.Handled = true;
        }

        private void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }

        private void gridView_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            // if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (ModifierKeys != Keys.None) return;
            if (hitInfo.InRowCell)
            {
                if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                    downHitInfo = hitInfo;
            }

        }

        private void gridView_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                //Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                //    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                 downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(GetDragData(view), DragDropEffects.Move);
                    downHitInfo = null;
                    DXMouseEventArgs ev = DXMouseEventArgs.GetMouseArgs(e);
                    ev.Handled = true;
                }
            }



        }


        private void gridControl_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(DragData)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void gridControl_DragDrop(object sender, DragEventArgs e)
        {
            int dropTargetRowHandle = -1;
            GridControl grid = sender as GridControl;
            if (grid == null) return;
            Point pt = new Point(e.X, e.Y);
            pt = grid.PointToClient(pt);
            GridView view = grid.GetViewAt(pt) as GridView;
            if (view == null) return;
            GridHitInfo hitInfoA = view.CalcHitInfo(pt);
            if (hitInfoA.HitTest == GridHitTest.EmptyRow)
                dropTargetRowHandle = view.DataRowCount;
            else
            {
                if (hitInfoA.RowHandle < 0)
                {
                    dropTargetRowHandle = 0;
                }
                else
                {
                    dropTargetRowHandle = hitInfoA.RowHandle + 1;
                }
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

                    //perform insert row grid drop

                    //perform delete row grid drag
                    ProcessGeneral.DeleteSelectedRows(data.gvDrag);
                    ((DataTable)data.gcDrag.DataSource).AcceptChanges();
                    //perform delete row grid drag
                }
            }

            BestFitColumnsGridViewRmSelected();
        }
        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.KeyCode == Keys.Enter)
            {
                if (gv.Name == "gvLoadValue")
                {
                    btnSelectOneRow_Click(null, null);
                }
                if (gv.Name == "gvSelectValue")
                {
                    btnRemoveOneRow_Click(null, null);
                }
            }

        }
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridControl gc = gv.GridControl;
            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(MousePosition));
            if (hi.InColumn && !hi.InRow)
            {
                BestFitColumnsGridViewRmSelected();
                ProcessGeneral.SetSelectedWhenGridViewDoubleClick(gv);
            }
            else
            {
                if (gv.Name == "gvLoadValue")
                {
                    btnSelectOneRow_Click(null, null);
                }
                if (gv.Name == "gvSelectValue")
                {
                    btnRemoveOneRow_Click(null, null);
                }
            }


        }

        #endregion

        #region "Methold Process"

        /// <summary>
        ///    Set Auto Wdith GridView
        /// </summary>
        private void BestFitColumnsGridViewRmSelected()
        {
            gvLoadValue.BestFitColumns();
            gvSelectValue.BestFitColumns();
            gvLoadValue.Columns["AttibuteCode"].Width += 10;
            gvLoadValue.Columns["AttibuteValue"].Width += 10;
            gvSelectValue.Columns["AttibuteCode"].Width += 10;
            gvSelectValue.Columns["AttibuteValue"].Width += 10;
        }




        /// <summary>
        ///     Set Row Selected When Insert,Delete Rows
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="gvNumberRowOld" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="numberRowSelected" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="maxRowSelected" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void SetSelectRowGridView(GridView gv, int gvNumberRowOld, int numberRowSelected, int maxRowSelected)
        {
            if (numberRowSelected <= 0) return;
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

        #endregion





        #region "button click"



        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            ProcessGeneral.InsertDataAllFromgvATogvB(gridControlSelectValue, gvSelectValue, gvLoadValue, dtTempS);
            BestFitColumnsGridViewRmSelected();
        }


        private void btnRemoveOneRow_Click(object sender, EventArgs e)
        {
            



            int numberRowSelected = gvSelectValue.SelectedRowsCount;
            int gvNumberRowOld = gvSelectValue.RowCount;
            int maxRowSelected = 0;
            for (int i = 0; i < numberRowSelected; i++)
            {
                int rHs = gvSelectValue.GetSelectedRows()[i];
                if (rHs >= 0)
                {
                    ProcessGeneral.AddNewRowGV(gvLoadValue, gvSelectValue.GetDataRow(rHs));
                    if (i == numberRowSelected - 1)
                    {
                        maxRowSelected = rHs;
                    }
                }

            }
            ProcessGeneral.DeleteSelectedRows(gvSelectValue);
            if (gvSelectValue.RowCount > 0)
            {
                SetSelectRowGridView(gvSelectValue, gvNumberRowOld, numberRowSelected, maxRowSelected);
            }
            BestFitColumnsGridViewRmSelected();
        }

        private void btnSelectOneRow_Click(object sender, EventArgs e)
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
            BestFitColumnsGridViewRmSelected();
            //SortRowInGridView(gvSelectValue);
        }



        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            ProcessGeneral.InsertDataAllFromgvATogvB(gridControlLoadValue, gvLoadValue, gvSelectValue, dtTempS);
            BestFitColumnsGridViewRmSelected();

        }






        #endregion


        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.D1:
                    {
                        btnRemoveAll_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D2:
                    {
                        btnRemoveOneRow_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D3:
                    {
                        btnSelectOneRow_Click(null, null);
                        return true;
                    }
                case Keys.Control | Keys.D4:
                    {
                        btnSelectAll_Click(null, null);
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion



        #endregion
    }
}
