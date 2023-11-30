using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using CNY_BaseSys.Common;
using DevExpress.XtraGrid.Columns;

namespace CNY_BaseSys.WForm
{
    public partial class FrmListEmailCommon : DevExpress.XtraEditors.XtraForm
    {
       #region "Property"

        public event GetListEmailHandler getlistEmail = null;
        DataTable dt;
        private bool isMultiSelected = false;
        RepositoryItemCheckEdit chkedit = new RepositoryItemCheckEdit();
        private string strEmail = "";
        private string strFullName = "";
        #endregion

        #region "contructor"
        public FrmListEmailCommon(DataTable dtpara, bool isMultiSelectedPara)
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            isMultiSelected = isMultiSelectedPara;
            GridViewCustomInit();
            dt = dtpara;
            this.Load += FrmListEmailCommon_Load;
        }

        #endregion
       
        #region "Melthod"

          /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
         private void GridViewCustomInit()
        {
            
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            

        //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.OptionsCustomization.AllowQuickHideColumns = true;
            gridView1.OptionsCustomization.AllowSort = true;
            gridView1.OptionsCustomization.AllowFilter = true;
            gridView1.HorzScrollVisibility = ScrollVisibility.Auto;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsCustomization.AllowColumnResizing = true;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = true;
            gridView1.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gridView1.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gridView1.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsView.AllowCellMerge = false;
            // gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gridView1.OptionsNavigation.AutoFocusNewRow = true;
            gridView1.OptionsNavigation.UseTabKey = true;

            gridView1.FocusRectStyle = DrawFocusRectStyle.CellFocus;

            gridView1.OptionsSelection.MultiSelect = isMultiSelected;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = true;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsView.EnableAppearanceOddRow = true;

            gridView1.OptionsView.ShowFooter = false;
          
           
            //   gridView1.RowHeight = 25;

            gridView1.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gridView1.OptionsFind.AlwaysVisible = false;
            gridView1.OptionsFind.ShowCloseButton = true;
            gridView1.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gridView1)
            {
                IsPerFormEvent = true,
            };
            var gridColumnS = new GridColumn();
            gridColumnS.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnS.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.AppearanceCell.Options.UseTextOptions = true;
            gridColumnS.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumnS.Caption = @"_";
            gridColumnS.FieldName = "Selected";
            gridColumnS.Name = "Selected";
            gridColumnS.Visible = true;
            gridColumnS.VisibleIndex = 0;
            gridColumnS.Width = 20;
            gridColumnS.ColumnEdit = chkedit;
            gridColumnS.OptionsColumn.AllowSort = DefaultBoolean.False;
            gridColumnS.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            gridView1.Columns.Add(gridColumnS);


            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.Caption = @"Username";
            gridColumn3.FieldName = "UserName";
            gridColumn3.Name = "UserName";       
            gridColumn3.VisibleIndex =1;
            gridColumn3.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            gridView1.Columns.Add(gridColumn3);



            var gridColumn2 = new GridColumn();
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.Caption = @"Full Name";
            gridColumn2.FieldName = "FullName";
            gridColumn2.Name = "FullName";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            gridColumn2.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            gridView1.Columns.Add(gridColumn2);


            var gridColumn0 = new GridColumn();
            gridColumn0.AppearanceCell.Options.UseTextOptions = true;
            gridColumn0.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn0.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.Caption = @"Email";
            gridColumn0.FieldName = "Email";
            gridColumn0.Name = "Email";
            gridColumn0.Visible = true;
            gridColumn0.VisibleIndex = 3;
            gridColumn0.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            gridView1.Columns.Add(gridColumn0);

            var gridColumn1 = new GridColumn();
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.Caption = @"Positions";
            gridColumn1.FieldName = "PositionsName";
            gridColumn1.Name = "PositionsName";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 4;
            gridColumn1.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            gridView1.Columns.Add(gridColumn1);

            GridColumn gridColumn5 = new GridColumn();
            gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            gridColumn5.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn5.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn5.Caption = @"Department";
            gridColumn5.FieldName = "DepartmentName";
            gridColumn5.Name = "DepartmentName";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 5;
            gridColumn5.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            gridView1.Columns.Add(gridColumn5);

            gridControl1.ProcessGridKey += gridControl1_ProcessGridKey;
            gridView1.DoubleClick += gridView1_DoubleClick;
            gridView1.RowCountChanged += gridView1_RowCountChanged;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += gridView1_ShowingEditor;
            gridView1.Click += gridView1_Click;
            gridView1.CustomDrawColumnHeader += gridView1_CustomDrawColumnHeader;
            gridView1.RowStyle += gridView1_RowStyle;
            gridView1.CustomDrawCell += gridView1_CustomDrawCell;
            ProcessGeneral.HideVisibleColumnsGridView(gridView1, false, "PositionsName", "DepartmentName");
            gridControl1.ForceInitialize();

            //gridControl1.UseEmbeddedNavigator = true;

          
            //   gridView1.RowHeight = 25;

         
          
        }
        /// <summary>
        ///     Create Event Tranfer Data To XtraUC032OutWorkAE
        /// </summary>
        private void CreateEventgetListEmail()
        {
            if (getlistEmail != null)
            {
                txtFocus.SelectNextControl(ActiveControl, true, true, true, true);
                GetEmailFullNameSelected();
                getlistEmail(this, new GetListEmailEventArgs
                {
                    
                    ListEmail = strEmail,   
                    ListFullName=strFullName,
                });
            }
            this.Close();
        }

        /// <summary>
        ///     Build String From Rows Selected In GridView
        /// </summary>
        /// <returns>
        ///     A string value...
        /// </returns>
        private void GetEmailFullNameSelected()
        {
            string strMail = "";
            string strName = "";
            var query = from p in ((DataTable)gridControl1.DataSource).AsEnumerable()
                        where p.Field<bool>("Selected")              
                        select new
                        {
                            EmailAddr = p["Email"].ToString(),
                            FullName = p["FullName"].ToString(),
                        };
            foreach (var item in query)
            {
                if (item.EmailAddr.Trim() != string.Empty)
                {
                    strMail += string.Format("{0}, ", item.EmailAddr.Trim());
                    
                }
                if (item.FullName.Trim() != string.Empty)
                {
                    strName += string.Format("{0}, ", item.FullName.Trim());
                }
            }
            if (strMail.Trim() != string.Empty)
            {
                strMail = strMail.Trim().Substring(0, strMail.Trim().Length - 1);
            }
            if (strName.Trim() != string.Empty)
            {
                strName = strName.Trim().Substring(0, strName.Trim().Length - 1);
            }
            strEmail = strMail;
            strFullName = strName;
        }
        #endregion


        #region "control event"
        private void FrmListEmailCommon_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
            gridView1.Columns["Selected"].Width = 20;

            gridView1.FocusedColumn = gridView1.VisibleColumns[1];
            gridView1.FocusedRowHandle = GridControl.AutoFilterRowHandle;
            gridView1.ShowEditor();

           
        }

        private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
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
  
        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CreateEventgetListEmail();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }


            if (e.KeyCode == Keys.Space)
            {
                switch (gridView1.FocusedColumn.FieldName)
                {
                    case "Selected":
                         bool selected = ProcessGeneral.GetSafeBool(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Selected"));
                         gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Selected", !selected);
                        break;
                        
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName != "Selected")
            {
                CreateEventgetListEmail();
            }
       
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            e.Cancel = gv.FocusedColumn.FieldName != "Selected";
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var gvC = sender as GridView;
            if (Convert.ToBoolean(gvC.GetRowCellValue(e.RowHandle, gvC.Columns["Selected"])) || gvC.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gvC.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }
            else
            {

                e.Appearance.BackColor = Color.FromArgb(235, 255, 218);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            Point clickPoint = gridControl1.PointToClient(Control.MousePosition);
            GridHitInfo hitInfo = gv.CalcHitInfo(clickPoint);
            if (hitInfo.InColumn && hitInfo.Column.FieldName == "Selected")
            {
                if (CheckboxHeaderCell.GetCheckedCount(gv) == gv.DataRowCount)
                    CheckboxHeaderCell.UnChekAll(gv);
                else
                    CheckboxHeaderCell.CheckAll(gv);
            }
            if (hitInfo.InRowCell)
            {
                if (hitInfo.RowHandle >= 0 || hitInfo.Column != null)
                {
                    if (hitInfo.Column.FieldName == "Selected")
                    {
                        if (Convert.ToBoolean(gv.GetRowCellValue(hitInfo.RowHandle, hitInfo.Column)))
                        {
                            gv.SetRowCellValue(hitInfo.RowHandle, hitInfo.Column, false);
                        }
                        else
                        {
                            gv.SetRowCellValue(hitInfo.RowHandle, hitInfo.Column, true);
                        }
                        gv.RefreshData();
                    }
                }

            }
        }




        private void gridView1_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.Column == gv.Columns["Selected"])
            {
                e.Info.InnerElements.Clear();
                e.Info.Appearance.ForeColor = Color.Blue;
                e.Painter.DrawObject(e.Info);
                CheckboxHeaderCell.DrawCheckBox(chkedit, e.Graphics, e.Bounds, CheckboxHeaderCell.GetCheckedCount(gv) == gv.DataRowCount);
                e.Handled = true;
            }
        }



        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }

        #endregion
    }
}