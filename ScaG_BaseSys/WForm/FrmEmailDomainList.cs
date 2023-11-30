using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using System.Drawing.Drawing2D;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.WForm
{
    public partial class FrmEmailDomainList : DevExpress.XtraEditors.XtraForm
    {
         #region "Property"

        public event GetListEmailDomainHandler getSelected = null;
        private bool isMultiSelected = false;
        private string _strFiler = "";
        #endregion

        #region "contructor"
        public FrmEmailDomainList(bool isMultiSelectedPara,string strFilter)
        {
            InitializeComponent();
            this.Load += Form_Load;
            MaximizeBox = false;
            MinimizeBox = false;
            isMultiSelected = isMultiSelectedPara;
            this._strFiler = strFilter;
            GridViewCustomInit();
        }
        #endregion

        #region "Melthod"
     


          /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
        private void GridViewCustomInit()
        {
            
        //    gridControl1.UseEmbeddedNavigator = true;

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
            gridView1.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            gridView1.OptionsView.ShowAutoFilterRow = false;
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

            gridView1.OptionsView.ShowFooter = true;
          
           
            //   gridView1.RowHeight = 25;

            gridView1.OptionsFind.AllowFindPanel = true;
            gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gridView1.OptionsFind.ShowCloseButton = true;
            gridView1.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gridView1)
            {
                IsPerFormEvent = true,
            };

            

            var gridColumn4 = new GridColumn();
            gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            gridColumn4.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn4.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn4.Caption = @"Username";
            gridColumn4.FieldName = "UserName";
            gridColumn4.Name = "UserName";
            gridColumn4.VisibleIndex = 0;
            gridColumn4.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn4.SummaryItem.DisplayFormat = @"Count :";//"Average = {0:n2}"
            gridColumn4.Fixed = FixedStyle.Left;
            gridView1.Columns.Add(gridColumn4);

            var gridColumn0 = new GridColumn();
            gridColumn0.AppearanceCell.Options.UseTextOptions = true;
            gridColumn0.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn0.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.Caption = @"Display Name";
            gridColumn0.FieldName = "DisplayName";
            gridColumn0.Name = "DisplayName";
            gridColumn0.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn0.SummaryItem.DisplayFormat = @"{0:n0} (user)";//"Average = {0:n2}"
            gridColumn0.Visible = true;
            gridColumn0.VisibleIndex = 1;
            gridView1.Columns.Add(gridColumn0);

            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn3.Caption = @"Email";
            gridColumn3.FieldName = "Email";
            gridColumn3.Name = "Email";       
            gridColumn3.VisibleIndex =2;
            gridView1.Columns.Add(gridColumn3);

            gridControl1.ProcessGridKey += gridControl1_ProcessGridKey;
            gridView1.DoubleClick += gridView1_DoubleClick;
            gridView1.RowCountChanged += gridView1_RowCountChanged;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            gridView1.CustomDrawCell += gridView1_CustomDrawCell;
            gridView1.RowStyle += gridView1_RowStyle;
            gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
            gridView1.CustomDrawFooter += gridView1_CustomDrawFooter;
            gridControl1.ForceInitialize();

        
          
        }

    
        /// <summary>
        ///     Create Event Tranfer Data To XtraUC032OutWorkAE
        /// </summary>
        private void CreateEventGetSelected()
        {
            if (getSelected != null)
            {
                string strUserName = "";
                string strDisplayName = "";
                string strEmail = "";
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    strUserName += ProcessGeneral.GetSafeString(gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "UserName"))+",";
                    strDisplayName += ProcessGeneral.GetSafeString(gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "DisplayName")) + ",";
                    strEmail += ProcessGeneral.GetSafeString(gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "Email")) + ",";
                }
                if (strUserName.Length > 0)
                {
                    strUserName = strUserName.Substring(0, strUserName.Length - 1).Trim();
                }
                if (strDisplayName.Length > 0)
                {
                    strDisplayName = strDisplayName.Substring(0, strDisplayName.Length - 1).Trim();
                }
                if (strEmail.Length > 0)
                {
                    strEmail = strEmail.Substring(0, strEmail.Length - 1).Trim();
                }
                getSelected(this, new GetListEmailDomainEventArgs
                {
                    StrUserName = strUserName,
                    StrDisplayName = strDisplayName,
                    StrEmail = strEmail,

                });
            }
            this.Close();
        }

     
        #endregion

        #region "Control Event "

        private void Form_Load(object sender, EventArgs e)
        {    
            gridControl1.DataSource = ProcessGeneral.GetAllInforUserInDomain();
            gridView1.BestFitColumns();
            gridView1.Columns["UserName"].Width += 30;
            gridView1.Columns["DisplayName"].Width += 20;
            if (!string.IsNullOrEmpty(this._strFiler))
            {
                gridView1.ApplyFindFilter(this._strFiler);
            }
         

        }


        private void gridView1_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }

        private void gridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "UserName" && e.Column.FieldName != "DisplayName") return;
            Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "UserName")
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


        private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = CNY_BaseSys.Properties.Resources.gnome_stock_person;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
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
        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gridView1_DoubleClick(sender, e);
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
          
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            CreateEventGetSelected();

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
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }

        #endregion
    }
}