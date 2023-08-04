using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys.WForm
{
    public partial class FrmStatusListGeneral : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"

        public event GetListStatusHandler getlistStatus = null;
        DataTable dt;
        #endregion

        #region "contructor"
        public FrmStatusListGeneral(DataTable dtpara)
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            GridViewCustomInit();
            dt = dtpara;
        }
        #endregion


        #region "Melthod"



        /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
        private void GridViewCustomInit()
        {

            gridControl1.UseEmbeddedNavigator = true;

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
            // gridView1.
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
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = true;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsView.EnableAppearanceOddRow = true;

            gridView1.OptionsView.ShowFooter = false;


            //   gridView1.RowHeight = 25;

            gridView1.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gridView1.OptionsFind.AlwaysVisible = false;
            gridView1.OptionsFind.ShowCloseButton = true;
            gridView1.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gridView1)
            {
                IsPerFormEvent = true,
            };

            GridColumn gridColumn1 = new GridColumn();
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn1.Caption = @"Status Code";
            gridColumn1.FieldName = "CodePer";
            gridColumn1.Name = "CodePer";
            gridColumn1.OptionsColumn.ReadOnly = true;
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = 80;
            gridView1.Columns.Add(gridColumn1);


            GridColumn gridColumn2 = new GridColumn();
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn2.Caption = @"Status Description";
            gridColumn2.FieldName = "DescPer";
            gridColumn2.Name = "DescPer";
            gridColumn2.OptionsColumn.ReadOnly = true;
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.Width = 115;
            gridView1.Columns.Add(gridColumn2);


            gridControl1.ForceInitialize();





        }
        /// <summary>
        ///     Create Event Tranfer Data To XtraUC032OutWorkAE
        /// </summary>
        private void CreateEventgetListStatus()
        {
            if (getlistStatus != null)
            {
                getlistStatus(this, new GetListStatusEventArgs
                {
                    StatusCode = ProcessGeneral.GetSafeString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns["CodePer"])),
                    StatusDescription = ProcessGeneral.GetSafeString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns["DescPer"])),

                });
            }
            this.Close();
        }
        #endregion

        #region "Control Event "
        private void FrmStatusListGeneral_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gridView1.IsDataRow(gridView1.FocusedRowHandle))
                {
                    CreateEventgetListStatus();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.IsDataRow(gridView1.FocusedRowHandle))
            {
                CreateEventgetListStatus();
            }
        }
        #endregion


    }
}
