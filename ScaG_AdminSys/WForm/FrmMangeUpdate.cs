using System;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CNY_AdminSys.Contrl;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

namespace CNY_AdminSys.WForm
{
    public partial class FrmMangeUpdate : FrmBase
    {
        #region "Property"

        private WaitDialogForm _dlg;
        readonly Ctrl_Update _ctrl = new Ctrl_Update();
        private string _pathVersion = "";
        #endregion

        #region " Contructor"

        public FrmMangeUpdate()
        {
            InitializeComponent();
            this.Load += FrmMangeUpdate_Load;
            GridViewUpdCustomInit();
            GridViewComCustomInit();
            GridViewRepCustomInit();
            GridViewMacCustomInit();
        }

        #endregion

        #region "Form Event"

        private void FrmMangeUpdate_Load(object sender, EventArgs e)
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            AllowCancel = false;
            AllowPrint = false;
            AllowGenerate = false;
            AllowBreakDown = false;
            AllowRevision = false;
            AllowRangeSize = false;
            AllowCopyObject = false;
            AllowCheck = false;
            AllowCombine = false;
            AllowFind = false;
            AllowSave = true;
            EnableSave = PerIns || PerUpd || PerDel;


            var ini = new IniFile();
            ini.Load(Application.StartupPath + @"\Extension\Version.ini");

            string pathUpd = ini.GetKeyValue("version", "UpdatePath");
            _pathVersion = pathUpd + @"\Version.ini";

          


            LoadDataGridUpdate();
            LoadDataGridComponent();
            LoadDataGridReport();
            LoadDataGridMachine();

        }

        #endregion

        

        #region "Process Gridview Update"

        private void LoadDataGridUpdate()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            var iniS = new IniFile();
            iniS.Load(_pathVersion);
            foreach (IniFile.IniSection.IniKey k in iniS.GetSection("version").Keys)
            {
                dt.Rows.Add(k.Name.Trim(), k.Value.Trim());
            }
            gcUpd.DataSource = dt;
            BestFitColumnsGvUpd();
        }

        private void BestFitColumnsGvUpd()
        {
            gvUpd.BestFitColumns();
            gvUpd.Columns["Value"].Width += 200;
            gvUpd.Columns["Name"].Width += 50;
        }
        private void GridViewUpdCustomInit()
        {

             
             // gcUpd.ToolTipController = toolTipController1  ;
            gcUpd.UseEmbeddedNavigator = true;
         
            gcUpd.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcUpd.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcUpd.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcUpd.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcUpd.EmbeddedNavigator.Buttons.Remove.Visible = false;
            

        //   gvCom.OptionsBehavior.AutoPopulateColumns = false;
            gvUpd.OptionsBehavior.Editable = true;
            gvUpd.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvUpd.OptionsCustomization.AllowColumnMoving = false;
            gvUpd.OptionsCustomization.AllowQuickHideColumns = true;
            gvUpd.OptionsCustomization.AllowSort = true;
            gvUpd.OptionsCustomization.AllowFilter = true;

      //     gvUpd.OptionsHint.ShowCellHints = true;
           
            gvUpd.OptionsView.ShowGroupPanel = false;
            gvUpd.OptionsView.ShowIndicator = true;
            gvUpd.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvUpd.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvUpd.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvUpd.OptionsView.ShowAutoFilterRow = false;
            gvUpd.OptionsView.AllowCellMerge = false;
            gvUpd.HorzScrollVisibility = ScrollVisibility.Auto;
            gvUpd.OptionsView.ColumnAutoWidth = false;

            //gvCom.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
             
            gvUpd.OptionsNavigation.AutoFocusNewRow = true;
            gvUpd.OptionsNavigation.UseTabKey = true;

            gvUpd.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvUpd.OptionsSelection.MultiSelect = true;
            gvUpd.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvUpd.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvUpd.OptionsSelection.EnableAppearanceFocusedCell = true;

            gvUpd.OptionsView.EnableAppearanceEvenRow = true;
            gvUpd.OptionsView.EnableAppearanceOddRow = true;

            gvUpd.OptionsView.ShowFooter = true;

            gvUpd.OptionsHint.ShowCellHints = false;
         
            //   gvCom.RowHeight = 25;

            gvUpd.OptionsFind.AllowFindPanel = true;
            //gvCom.OptionsFind.AlwaysVisible = true;//==>false==>gvCom.OptionsFind.ShowCloseButton = true;
            gvUpd.OptionsFind.AlwaysVisible = false;
            gvUpd.OptionsFind.ShowCloseButton = true;
            gvUpd.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvUpd)
            {
                IsPerFormEvent = true,
            };
            gvUpd.OptionsPrint.AutoWidth = false;

            var gridColumn0 = new GridColumn();
            gridColumn0.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn0.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.AppearanceCell.Options.UseTextOptions = true;
            gridColumn0.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.Caption = @"Key";
            gridColumn0.FieldName = "Name";
            gridColumn0.Name = "Name";
            gridColumn0.Visible = true;
            gridColumn0.VisibleIndex = 0;
            gridColumn0.Fixed = FixedStyle.Left;
            gridColumn0.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn0.SummaryItem.DisplayFormat = @"Total :";
            gvUpd.Columns.Add(gridColumn0);

            var gridColumn1 = new GridColumn();
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.Caption = @"Value";
            gridColumn1.FieldName = "Value";
            gridColumn1.Name = "Value";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 1;
            gridColumn1.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn1.SummaryItem.DisplayFormat = @"{0:N0} (item)";
            gvUpd.Columns.Add(gridColumn1);

          
            gvUpd.CustomDrawCell += gvUpd_CustomDrawCell;
            gvUpd.RowStyle += gvUpd_RowStyle;
            gvUpd.RowCountChanged += gvUpd_RowCountChanged;
            gvUpd.CustomDrawRowIndicator += gvUpd_CustomDrawRowIndicator;
            gvUpd.CustomDrawFooter += gvUpd_CustomDrawFooter;
            gvUpd.CustomDrawFooterCell += gvUpd_CustomDrawFooterCell;
            gvUpd.ShowingEditor += gvUpd_ShowingEditor;
            gvUpd.RowCellStyle += gvUpd_RowCellStyle;
            gvUpd.CellValueChanged += gvUpd_CellValueChanged;
            gcUpd.ForceInitialize();
        
          
        }

        private void gvUpd_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            BestFitColumnsGvUpd();
        }

        private void gvUpd_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "Value":
                    e.Appearance.BackColor = Color.BlanchedAlmond;
                    e.Appearance.BackColor2 = Color.Azure;
                    break;

            }
        }

        private void gvUpd_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            e.Cancel = gv.FocusedColumn.FieldName == "Name";
        }


        private void gvUpd_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gvUpd_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "Name" && e.Column.FieldName != "Value") return;
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "Name")
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

        private void gvUpd_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvUpd_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }

        private void gvUpd_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = CNY_AdminSys.Properties.Resources.Mimetypes_ini_icon;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void gvUpd_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }

        }   


        #endregion

        #region "Process Gridview Choose Component"

        private void LoadDataGridComponent()
        {
            gcCom.DataSource = _ctrl.Component_Load();
            BestFitColumnsGvCom();
        }


        private void GridViewComCustomInit()
        {
            var chkeditCom = new RepositoryItemCheckEdit() { AutoHeight = true };
            gcCom.UseEmbeddedNavigator = true;

            gcCom.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcCom.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcCom.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcCom.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcCom.EmbeddedNavigator.Buttons.Remove.Visible = false;

            
            //   gvCom.OptionsBehavior.AutoPopulateColumns = false;
            gvCom.OptionsBehavior.Editable = true;
            gvCom.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvCom.OptionsCustomization.AllowColumnMoving = false;
            gvCom.OptionsCustomization.AllowQuickHideColumns = true;
            gvCom.OptionsCustomization.AllowSort = true;
            gvCom.OptionsCustomization.AllowFilter = true;

            //     gvUpd.OptionsHint.ShowCellHints = true;

            gvCom.OptionsView.ShowGroupPanel = false;
            gvCom.OptionsView.ShowIndicator = true;
            gvCom.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvCom.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvCom.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvCom.OptionsView.ShowAutoFilterRow = false;
            gvCom.OptionsView.AllowCellMerge = false;
            gvCom.HorzScrollVisibility = ScrollVisibility.Auto;
            gvCom.OptionsView.ColumnAutoWidth = false;

            //gvCom.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvCom.OptionsNavigation.AutoFocusNewRow = true;
            gvCom.OptionsNavigation.UseTabKey = true;

            gvCom.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvCom.OptionsSelection.MultiSelect = true;
            gvCom.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvCom.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvCom.OptionsSelection.EnableAppearanceFocusedCell = true;

            gvCom.OptionsView.EnableAppearanceEvenRow = true;
            gvCom.OptionsView.EnableAppearanceOddRow = true;

            gvCom.OptionsView.ShowFooter = true;

            gvCom.OptionsHint.ShowCellHints = false;

            //   gvCom.RowHeight = 25;

            gvCom.OptionsFind.AllowFindPanel = true;
            //gvCom.OptionsFind.AlwaysVisible = true;//==>false==>gvCom.OptionsFind.ShowCloseButton = true;
            gvCom.OptionsFind.AlwaysVisible = false;
            gvCom.OptionsFind.ShowCloseButton = true;
            gvCom.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvCom)
            {
                IsPerFormEvent = true,
            };

            gvCom.OptionsPrint.AutoWidth = false;


            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.Caption = @"ID";
            gridColumn3.FieldName = "ID";
            gridColumn3.Name = "ID";       
            gridColumn3.VisibleIndex =0;
            gvCom.Columns.Add(gridColumn3);



            var gridColumn2 = new GridColumn();
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.Caption = @"Component Name";
            gridColumn2.FieldName = "ComponentName";
            gridColumn2.Name = "ComponentName";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn2.SummaryItem.DisplayFormat = @"Total :";
            gvCom.Columns.Add(gridColumn2);


            var gridColumn0 = new GridColumn();
            gridColumn0.AppearanceCell.Options.UseTextOptions = true;
            gridColumn0.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn0.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.Caption = @"File Name";
            gridColumn0.FieldName = "FileName";
            gridColumn0.Name = "FileName";
            gridColumn0.Visible = true;
            gridColumn0.VisibleIndex = 2;
            gridColumn0.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn0.SummaryItem.DisplayFormat = @"{0:N0} (item)";
            gvCom.Columns.Add(gridColumn0);

          


            var gridColumnS = new GridColumn();
            gridColumnS.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnS.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.AppearanceCell.Options.UseTextOptions = true;
            gridColumnS.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.Caption = @"Is Update";
            gridColumnS.FieldName = "IsUpdate";
            gridColumnS.Name = "IsUpdate";
            gridColumnS.Visible = true;
            gridColumnS.VisibleIndex = 3;
            gridColumnS.Width = 20;
            gridColumnS.ColumnEdit = chkeditCom;
            gridColumnS.OptionsColumn.AllowSort = DefaultBoolean.False;
            gvCom.Columns.Add(gridColumnS);


            gcCom.ProcessGridKey += gcCom_ProcessGridKey;
            gvCom.RowCountChanged += gvCom_RowCountChanged;
            gvCom.CustomDrawRowIndicator += gvCom_CustomDrawRowIndicator;
            gvCom.RowStyle += gvCom_RowStyle;
            gvCom.CustomDrawCell += gvCom_CustomDrawCell;
            gvCom.RowCellStyle += gvCom_RowCellStyle;
            gvCom.CustomDrawFooter += gvCom_CustomDrawFooter;
            gvCom.CustomDrawFooterCell += gvCom_CustomDrawFooterCell;
            gvCom.CellValueChanged += gvCom_CellValueChanged;
            ProcessGeneral.HideVisibleColumnsGridView(gvCom, false, "ID");
            gcCom.ForceInitialize();
          
        }

       

        private void BestFitColumnsGvCom()
        {
            gvCom.BestFitColumns();
            gvCom.Columns["ComponentName"].Width += 50;
            gvCom.Columns["FileName"].Width += 150;
        }

        private void gvCom_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsUpdate") return;
            BestFitColumnsGvCom();
        }

        private void gvCom_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = CNY_AdminSys.Properties.Resources.file_extension_dll_icon;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void gvCom_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            switch (e.Column.FieldName)
            {
                case "IsUpdate":
                    if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "IsUpdate")))
                    {
                        e.Appearance.BackColor = Color.BlanchedAlmond;
                        e.Appearance.BackColor2 = Color.Azure;
                    }
                    break;

            }
       
        }

        private void gvCom_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gvCom_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "ComponentName" && e.Column.FieldName != "FileName") return;
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "ComponentName")
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



        private void gcCom_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            var gv = gc.FocusedView as GridView;
            if (e.KeyData == Keys.Insert)
            {
                ProcessGeneral.AddNewRowGV(gv, 0, "", "", false);
            }
            if (e.KeyData == Keys.Escape)
            {
                gv.DeleteSelectedRows();
            }
            if (e.KeyCode == Keys.F5)
            {
                ProcessGeneral.UnCheckSelectedInGridView(gv, "IsUpdate");
            }
        }


        private void gvCom_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }
        }

        private void gvCom_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvCom_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }


               


        #endregion

        #region "Process Gridview Choose Report"

        
        private void LoadDataGridReport()
        {
            gcRep.DataSource = _ctrl.Report_Load();
            BestFitColumnsGvRep();
        }


        private void GridViewRepCustomInit()
        {
            var chkeditCom = new RepositoryItemCheckEdit() { AutoHeight = true };
            gcRep.UseEmbeddedNavigator = true;

            gcRep.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcRep.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcRep.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcRep.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcRep.EmbeddedNavigator.Buttons.Remove.Visible = false;
            
            
            //   gvCom.OptionsBehavior.AutoPopulateColumns = false;
            gvRep.OptionsBehavior.Editable = true;
            gvRep.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvRep.OptionsCustomization.AllowColumnMoving = false;
            gvRep.OptionsCustomization.AllowQuickHideColumns = true;
            gvRep.OptionsCustomization.AllowSort = true;
            gvRep.OptionsCustomization.AllowFilter = true;

            //     gvUpd.OptionsHint.ShowCellHints = true;

            gvRep.OptionsView.ShowGroupPanel = false;
            gvRep.OptionsView.ShowIndicator = true;
            gvRep.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvRep.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvRep.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvRep.OptionsView.ShowAutoFilterRow = false;
            gvRep.OptionsView.AllowCellMerge = false;
            gvRep.HorzScrollVisibility = ScrollVisibility.Auto;
            gvRep.OptionsView.ColumnAutoWidth = false;

            //gvCom.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvRep.OptionsNavigation.AutoFocusNewRow = true;
            gvRep.OptionsNavigation.UseTabKey = true;

            gvRep.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvRep.OptionsSelection.MultiSelect = true;
            gvRep.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvRep.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvRep.OptionsSelection.EnableAppearanceFocusedCell = true;

            gvRep.OptionsView.EnableAppearanceEvenRow = true;
            gvRep.OptionsView.EnableAppearanceOddRow = true;

            gvRep.OptionsView.ShowFooter = true;

            gvRep.OptionsHint.ShowCellHints = false;

            //   gvCom.RowHeight = 25;

            gvRep.OptionsFind.AllowFindPanel = true;
            //gvCom.OptionsFind.AlwaysVisible = true;//==>false==>gvCom.OptionsFind.ShowCloseButton = true;
            gvRep.OptionsFind.AlwaysVisible = false;
            gvRep.OptionsFind.ShowCloseButton = true;
            gvRep.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvRep)
            {
                IsPerFormEvent = true,
            };

            gvRep.OptionsPrint.AutoWidth = false;


            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.Caption = @"ID";
            gridColumn3.FieldName = "ID";
            gridColumn3.Name = "ID";       
            gridColumn3.VisibleIndex =0;
            gvRep.Columns.Add(gridColumn3);



            var gridColumn2 = new GridColumn();
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.Caption = @"Report Name";
            gridColumn2.FieldName = "ReportName";
            gridColumn2.Name = "ReportName";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn2.SummaryItem.DisplayFormat = @"Total :";
            gvRep.Columns.Add(gridColumn2);


            var gridColumn0 = new GridColumn();
            gridColumn0.AppearanceCell.Options.UseTextOptions = true;
            gridColumn0.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn0.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.Caption = @"File Name";
            gridColumn0.FieldName = "FileName";
            gridColumn0.Name = "FileName";
            gridColumn0.Visible = true;
            gridColumn0.VisibleIndex = 2;
            gridColumn0.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn0.SummaryItem.DisplayFormat = @"{0:N0} (item)";
            gvRep.Columns.Add(gridColumn0);


            var gridColumn1 = new GridColumn();
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.Caption = @"Description";
            gridColumn1.FieldName = "Description";
            gridColumn1.Name = "Description";
            gridColumn1.VisibleIndex = 3;
            gvRep.Columns.Add(gridColumn1);


            var gridColumnS = new GridColumn();
            gridColumnS.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnS.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.AppearanceCell.Options.UseTextOptions = true;
            gridColumnS.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.Caption = @"Is Update";
            gridColumnS.FieldName = "IsUpdate";
            gridColumnS.Name = "IsUpdate";
            gridColumnS.Visible = true;
            gridColumnS.VisibleIndex = 4;
            gridColumnS.Width = 20;
            gridColumnS.ColumnEdit = chkeditCom;
            gridColumnS.OptionsColumn.AllowSort = DefaultBoolean.False;
            gvRep.Columns.Add(gridColumnS);


            gcRep.ProcessGridKey += gcRep_ProcessGridKey;
            gvRep.RowCountChanged += gvRep_RowCountChanged;
            gvRep.CustomDrawRowIndicator += gvRep_CustomDrawRowIndicator;
            gvRep.RowStyle += gvRep_RowStyle;
            gvRep.CustomDrawCell += gvRep_CustomDrawCell;
            gvRep.RowCellStyle += gvRep_RowCellStyle;
            gvRep.CustomDrawFooter += gvRep_CustomDrawFooter;
            gvRep.CustomDrawFooterCell += gvRep_CustomDrawFooterCell;
            gvRep.CellValueChanged += gvRep_CellValueChanged;
            ProcessGeneral.HideVisibleColumnsGridView(gvRep, false, "ID");
            gcRep.ForceInitialize();
          
        }



        private void BestFitColumnsGvRep()
        {
            gvRep.BestFitColumns();
            gvRep.Columns["ReportName"].Width += 50;
            gvRep.Columns["FileName"].Width += 100;
        }

        private void gvRep_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsUpdate") return;
            BestFitColumnsGvRep();
        }

        private void gvRep_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = CNY_AdminSys.Properties.Resources.custom_reports_icon;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void gvRep_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            switch (e.Column.FieldName)
            {
                case "IsUpdate":
                    if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "IsUpdate")))
                    {
                        e.Appearance.BackColor = Color.BlanchedAlmond;
                        e.Appearance.BackColor2 = Color.Azure;
                    }
                    break;

            }
       
        }

        private void gvRep_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gvRep_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "ReportName" && e.Column.FieldName != "FileName") return;
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "ReportName")
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



        private void gcRep_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            var gv = gc.FocusedView as GridView;
            if (e.KeyData == Keys.Insert)
            {
                ProcessGeneral.AddNewRowGV(gv, 0, "", "", "", false);
            }
            if (e.KeyData == Keys.Escape)
            {
                gv.DeleteSelectedRows();
            }
            if (e.KeyCode == Keys.F5)
            {
                ProcessGeneral.UnCheckSelectedInGridView(gv, "IsUpdate");
            }
        }


        private void gvRep_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }
        }

        private void gvRep_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvRep_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }

        #endregion
        
        #region "Process Gridview Choose Machine"

        private void LoadDataGridMachine()
        {
            gcMac.DataSource = _ctrl.Machine_Load();
            BestFitColumnsGvMac();
        }


        private void GridViewMacCustomInit()
        {
            var chkeditCom = new RepositoryItemCheckEdit() { AutoHeight = true };
            gcMac.UseEmbeddedNavigator = true;

            gcMac.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMac.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMac.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMac.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMac.EmbeddedNavigator.Buttons.Remove.Visible = false;

            
            //   gvCom.OptionsBehavior.AutoPopulateColumns = false;
            gvMac.OptionsBehavior.Editable = true;
            gvMac.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMac.OptionsCustomization.AllowColumnMoving = false;
            gvMac.OptionsCustomization.AllowQuickHideColumns = true;
            gvMac.OptionsCustomization.AllowSort = true;
            gvMac.OptionsCustomization.AllowFilter = true;

            //     gvUpd.OptionsHint.ShowCellHints = true;

            gvMac.OptionsView.ShowGroupPanel = false;
            gvMac.OptionsView.ShowIndicator = true;
            gvMac.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMac.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMac.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMac.OptionsView.ShowAutoFilterRow = false;
            gvMac.OptionsView.AllowCellMerge = false;
            gvMac.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMac.OptionsView.ColumnAutoWidth = false;

            //gvCom.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMac.OptionsNavigation.AutoFocusNewRow = true;
            gvMac.OptionsNavigation.UseTabKey = true;

            gvMac.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMac.OptionsSelection.MultiSelect = true;
            gvMac.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMac.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvMac.OptionsSelection.EnableAppearanceFocusedCell = true;

            gvMac.OptionsView.EnableAppearanceEvenRow = true;
            gvMac.OptionsView.EnableAppearanceOddRow = true;

            gvMac.OptionsView.ShowFooter = true;

            gvMac.OptionsHint.ShowCellHints = false;

            //   gvCom.RowHeight = 25;

            gvMac.OptionsFind.AllowFindPanel = true;
            //gvCom.OptionsFind.AlwaysVisible = true;//==>false==>gvCom.OptionsFind.ShowCloseButton = true;
            gvMac.OptionsFind.AlwaysVisible = false;
            gvMac.OptionsFind.ShowCloseButton = true;
            gvMac.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMac)
            {
                IsPerFormEvent = true,
            };

            gvMac.OptionsPrint.AutoWidth = false;


            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.Caption = @"ID";
            gridColumn3.FieldName = "ID";
            gridColumn3.Name = "ID";       
            gridColumn3.VisibleIndex =0;
            gvMac.Columns.Add(gridColumn3);



            var gridColumn2 = new GridColumn();
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.Caption = @"Machine Name";
            gridColumn2.FieldName = "MachineName";
            gridColumn2.Name = "MachineName";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn2.SummaryItem.DisplayFormat = @"Total : {0:N0} (item)";
            gvMac.Columns.Add(gridColumn2);



          


            var gridColumnS = new GridColumn();
            gridColumnS.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnS.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.AppearanceCell.Options.UseTextOptions = true;
            gridColumnS.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.Caption = @"Is Update";
            gridColumnS.FieldName = "IsUpdate";
            gridColumnS.Name = "IsUpdate";
            gridColumnS.Visible = true;
            gridColumnS.VisibleIndex = 3;
            gridColumnS.Width = 20;
            gridColumnS.ColumnEdit = chkeditCom;
            gridColumnS.OptionsColumn.AllowSort = DefaultBoolean.False;
            gvMac.Columns.Add(gridColumnS);


            gcMac.ProcessGridKey += gcMac_ProcessGridKey;
            gvMac.RowCountChanged += gvMac_RowCountChanged;
            gvMac.CustomDrawRowIndicator += gvMac_CustomDrawRowIndicator;
            gvMac.RowStyle += gvMac_RowStyle;
            gvMac.CustomDrawCell += gvMac_CustomDrawCell;
            gvMac.RowCellStyle += gvMac_RowCellStyle;
            gvMac.CustomDrawFooter += gvMac_CustomDrawFooter;
            gvMac.CustomDrawFooterCell += gvMac_CustomDrawFooterCell;
            gvMac.CellValueChanged += gvMac_CellValueChanged;
            ProcessGeneral.HideVisibleColumnsGridView(gvMac, false, "ID");
            gcMac.ForceInitialize();
          
        }

       

        private void BestFitColumnsGvMac()
        {
            gvMac.BestFitColumns();
            gvMac.Columns["MachineName"].Width += 200;
        }

        private void gvMac_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsUpdate")return;
            BestFitColumnsGvMac();
        }

        private void gvMac_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = CNY_AdminSys.Properties.Resources.Computer_icon;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void gvMac_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            switch (e.Column.FieldName)
            {
                case "IsUpdate":
                    if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "IsUpdate")))
                    {
                        e.Appearance.BackColor = Color.BlanchedAlmond;
                        e.Appearance.BackColor2 = Color.Azure;
                    }
                    break;

            }
       
        }

        private void gvMac_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gvMac_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "MachineName" ) return;
            if(e.Bounds.Width>0 && e.Bounds.Height>0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            e.Appearance.ForeColor = Color.Red;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            e.Graphics.DrawString(@"   " + e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            e.Handled = true;
        }



        private void gcMac_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            var gv = gc.FocusedView as GridView;
            if (e.KeyData == Keys.Insert)
            {
                ProcessGeneral.AddNewRowGV(gv, 0, "", false);
            }
            if (e.KeyData == Keys.Escape)
            {
                gv.DeleteSelectedRows();
            }
            if (e.KeyCode == Keys.F5)
            {
                ProcessGeneral.UnCheckSelectedInGridView(gv, "IsUpdate");
            }
        }


        private void gvMac_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
            e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }

        private void gvMac_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvMac_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }


               


        #endregion

        #region "Overide Button Event Click"
        /// <summary>
        /// Perform when click Add button
        /// </summary>
        protected override void PerformAdd()
        {
            DialogResult kq= XtraMessageBox.Show("Do you want to geting list machine form domain???", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.No) return;
            _dlg = new WaitDialogForm("");
            var dt =new  DataTable();
            dt.Columns.Add("MachineName", typeof(string));
            var dirEntry = new DirectoryEntry("LDAP://" + DeclareSystem.SysDomainName, DeclareSystem.SysUserInDomain, DeclareSystem.SysPasswordOfUserInDomain, AuthenticationTypes.Secure);
            var dirSearch = new DirectorySearcher(dirEntry) {Filter = "(objectClass=Computer)"};
            foreach (SearchResult sr in dirSearch.FindAll())
            {
                dt.Rows.Add(sr.GetDirectoryEntry().Name.Substring(3).Trim());
            }
            _ctrl.Machine_UpdateDomain(dt);
            LoadDataGridMachine();
            _dlg.Close();
            XtraMessageBox.Show("Insert Successful List Machine Form Domain into database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Perform when click Refresh button
        /// </summary>
        protected override void PerformRefresh()
        {
            _dlg = new WaitDialogForm("");
            LoadDataGridUpdate();
            LoadDataGridComponent();
            LoadDataGridReport();
            LoadDataGridMachine();
            _dlg.Close();

        }

        /// <summary>
        ///     Perform when click Save button
        /// </summary>
        protected override void PerformSave()
        {
            _dlg = new WaitDialogForm("");
            switch (xtraTabMain.SelectedTabPageIndex)
            {
                case 0:
                    txtFocusUpd.SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case 1:
                    txtFocusCom.SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case 2:
                    txtFocusRep.SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case 3:
                    txtFocusMac.SelectNextControl(ActiveControl, true, true, true, true);
                    break;

            }
            ((DataTable)gcUpd.DataSource).AcceptChanges();
            var dt = gcUpd.DataSource as DataTable;
            var ini = new IniFile();
            ini.Load(_pathVersion);
            foreach (DataRow dr in dt.Rows)
            {
                string sName = ProcessGeneral.GetSafeString(dr["Name"]);
                string sValue = ProcessGeneral.GetSafeString(dr["Value"]);
                if (!string.IsNullOrEmpty(sName) && !string.IsNullOrEmpty(sValue))
                {
                    ini.SetKeyValue("version", sName, sValue);
                }
        
            }
            ini.Save(_pathVersion);

            //save component

            var dtComSource = gcCom.DataSource as DataTable;
            string strDelCom = ProcessGeneral.StringCumulativeRowsDeletedInTable(dtComSource, "ID");
            DataTable dtInsUpdCom = dtComSource.GetChanges(DataRowState.Added | DataRowState.Modified);
            _ctrl.Component_Update(strDelCom, dtInsUpdCom);
      
            //save report

            var dtRepSource = gcRep.DataSource as DataTable;
            string strDelRep = ProcessGeneral.StringCumulativeRowsDeletedInTable(dtRepSource, "ID");
            DataTable dtInsUpdRep = dtRepSource.GetChanges(DataRowState.Added | DataRowState.Modified);
            _ctrl.Report_Update(strDelRep, dtInsUpdRep);

            //save machine

            var dtMacSource = gcMac.DataSource as DataTable;
            string strDelMac = ProcessGeneral.StringCumulativeRowsDeletedInTable(dtMacSource, "ID");
            DataTable dtInsUpdMac = dtMacSource.GetChanges(DataRowState.Added | DataRowState.Modified);
            _ctrl.Machine_Update(strDelMac, dtInsUpdMac);

            //reload data
            
            LoadDataGridUpdate();
            LoadDataGridComponent();
            LoadDataGridReport();
            LoadDataGridMachine();
            _dlg.Close();
            XtraMessageBox.Show("Create Successful Function Auto Update Program Scax", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region "Tab Event"
   
        private void xtraTabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page == xtraTabPageMac)
            {
                AllowAdd = true;
                EnableAdd = PerIns || PerUpd || PerDel;
            }
            else
            {
                AllowAdd = false;
            }
        }
        #endregion
    }
}
