using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CNY_AdminSys.Info;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_AdminSys.WForm
{
    public partial class FrmUserMemberOf : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
        private readonly Inf_User _inf = new Inf_User();
        private readonly Int64 _userId = 0;
        #endregion

        #region "Contructor"

        public FrmUserMemberOf(Int64 userIdP,string username,string fullname,int active)
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this._userId = userIdP;
            txtUserName.EditValue = username;
            txtFullName.EditValue = fullname;
            if (active == 0)
            {
                chkBlock.Checked = true;
                chkActive.Checked = false;
            }
            else
            {
                chkBlock.Checked = false;
                chkActive.Checked = true;
            }
            GridViewCustomInit();
            txtUserName.Enabled = false;
            txtFullName.Enabled = false;
            chkBlock.Properties.ReadOnly = true;
            chkActive.Properties.ReadOnly = true;
            this.Load += FrmUserMemberOf_Load;
            btnAdd.Click += btnAdd_Click;
            btnCancel.Click += btnCancel_Click;
            btnRemove.Click += btnRemove_Click;
            btnSave.Click += btnSave_Click;
            
          
        }
        
        #endregion

        #region "FormEvent"

        private void FrmUserMemberOf_Load(object sender, EventArgs e)
        {
            gcMainAE.DataSource = _inf.UserMemberLoadGrid(_userId);
            BestFitColumnsWithImage();
        }


        #endregion

        #region "GridView Event"


        private void gvMainAE_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }

        private void gvMainAE_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "GroupUserCode" && e.Column.FieldName != "GroupUserName") return;
            Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
            using (brush)
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "GroupUserCode")
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
      

        private void gvMainAE_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvMainAE_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }

        private void gvMainAE_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex == 0)
            {
                Image icon = CNY_AdminSys.Properties.Resources.user_group_icon_24;
                e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }
        }

        private void gvMainAE_RowStyle(object sender, RowStyleEventArgs e)
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

        #region "Button CLick Event"

        

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (gvMainAE.SelectedRowsCount == 0) return;
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to remove user {0} leave group selected !!!?? (yes/No) \n Note:You could not restore this record!", txtUserName.Text.Trim()),
                   "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return;
            for (int i = 0; i < gvMainAE.SelectedRowsCount; i++)
            {
                DataRow dr = gvMainAE.GetDataRow(gvMainAE.GetSelectedRows()[i]);
                _inf.UserInGroup_Delete(ProcessGeneral.GetSafeString(dr["GroupUserCode"]), _userId);
                dr.Delete();
            }((DataTable)gcMainAE.DataSource).AcceptChanges(); 
            BestFitColumnsWithImage();
        
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dtS = (DataTable)gcMainAE.DataSource;
            if (dtS == null) return;
            var query = dtS.AsEnumerable()
                .Select(p => new { GroupUserCode = ProcessGeneral.GetSafeString(p["GroupUserCode"]) }).ToList();
            DataTable dtCondition = query.Any() ? query.CopyToDataTableNew() : TableTemplateGroupRole();

            DataTable dtSource = _inf.UserMemberLoadRoleSelected(dtCondition);
            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> l= new List<string>();

            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "GroupUserCode",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Name",
                    FieldName = "GroupUserName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = 1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Description",
                    FieldName = "GroupUserDescription",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Priority",
                    FieldName = "Priority",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = FixedStyle.None,
                    VisibleIndex = 3,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 30,
                    SummayType = SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
            };

            #endregion

            var f = new FrmTransferData
            {
                DtSource = dtSource,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(800, 600),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Role Listing",
                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true,
                MultiSelectMode = GridMultiSelectMode.RowSelect
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                foreach (var dr in lDr)
                {
                    l.Add(ProcessGeneral.GetSafeString(dr["GroupUserCode"]));
                }
            };
            f.ShowDialog();


            if (l.Count <= 0) return;
            foreach (string s in l)
            {
                _inf.UserInGroup_Insert(s, _userId);
            }
            gcMainAE.DataSource = _inf.UserMemberLoadGrid(_userId);
            BestFitColumnsWithImage();
        }
        


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPass.Text))
            {
                if (ProcessGeneral.GetSafeString(txtPassword.EditValue) != ProcessGeneral.GetSafeString(txtConfirmPass.EditValue))
                {
                    XtraMessageBox.Show("TextBox Confirm Password does not match with TextBox Input Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmPass.Select();
                    return;
                }
                _inf.UpdatePasswordUser(_userId, EnDeCrypt.Encrypt(ProcessGeneral.GetSafeString(txtConfirmPass.Text), true));
                XtraMessageBox.Show(string.Format("Update Pass For User {0} Successfull.",txtUserName.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Proccess Methold"

        private void GridViewCustomInit()
        {

             
             // gcMainAE.ToolTipController = toolTipController1  ;
            gcMainAE.UseEmbeddedNavigator = true;
         
            gcMainAE.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMainAE.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMainAE.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMainAE.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMainAE.EmbeddedNavigator.Buttons.Remove.Visible = false;
            
             
        //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMainAE.OptionsBehavior.Editable = false;
            gvMainAE.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMainAE.OptionsCustomization.AllowColumnMoving = false;
            gvMainAE.OptionsCustomization.AllowQuickHideColumns = true;
            gvMainAE.OptionsCustomization.AllowSort = true;
            gvMainAE.OptionsCustomization.AllowFilter = true;

      //     gvMainAE.OptionsHint.ShowCellHints = true;
           
            gvMainAE.OptionsView.ShowGroupPanel = false;
            gvMainAE.OptionsView.ShowIndicator = true;
            gvMainAE.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMainAE.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMainAE.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMainAE.OptionsView.ShowAutoFilterRow = false;
            gvMainAE.OptionsView.AllowCellMerge = false;
            gvMainAE.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMainAE.OptionsView.ColumnAutoWidth = false;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
             
            gvMainAE.OptionsNavigation.AutoFocusNewRow = true;
            gvMainAE.OptionsNavigation.UseTabKey = true;

            gvMainAE.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMainAE.OptionsSelection.MultiSelect = true;
            gvMainAE.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMainAE.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvMainAE.OptionsSelection.EnableAppearanceFocusedCell = true;

            gvMainAE.OptionsView.EnableAppearanceEvenRow = true;
            gvMainAE.OptionsView.EnableAppearanceOddRow = true;

            gvMainAE.OptionsView.ShowFooter = true;

            gvMainAE.OptionsHint.ShowCellHints = false;
         
            //   gridView1.RowHeight = 25;

            gvMainAE.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMainAE.OptionsFind.AlwaysVisible = false;
            gvMainAE.OptionsFind.ShowCloseButton = true;
            gvMainAE.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMainAE)
            {
                IsPerFormEvent = true,
            }; 

            gvMainAE.OptionsPrint.AutoWidth = false;

            var gridColumn0 = new GridColumn();
            gridColumn0.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn0.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.AppearanceCell.Options.UseTextOptions = true;
            gridColumn0.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn0.Caption = @"Role Code";
            gridColumn0.FieldName = "GroupUserCode";
            gridColumn0.Name = "GroupUserCode";
            gridColumn0.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn0.SummaryItem.DisplayFormat = @"Count :";
            gridColumn0.Visible = true;
            gridColumn0.VisibleIndex = 0;          
            gvMainAE.Columns.Add(gridColumn0);

            var gridColumn1 = new GridColumn();
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.Caption = @"Role Name";
            gridColumn1.FieldName = "GroupUserName";
            gridColumn1.Name = "GroupUserName";
            gridColumn1.SummaryItem.SummaryType = SummaryItemType.Count;
            gridColumn1.SummaryItem.DisplayFormat = @"{0:N0} (role)";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 1;          
            gvMainAE.Columns.Add(gridColumn1);

            var gridColumn2 = new GridColumn();
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.Caption = @"Description";
            gridColumn2.FieldName = "GroupUserDescription";
            gridColumn2.Name = "GroupUserDescription";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            gvMainAE.Columns.Add(gridColumn2);

            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn3.Caption = @"Priority";
            gridColumn3.FieldName = "Priority";
            gridColumn3.Name = "Priority";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 3;
            gvMainAE.Columns.Add(gridColumn3);
     
            gvMainAE.CustomDrawCell += gvMainAE_CustomDrawCell;
            gvMainAE.RowStyle += gvMainAE_RowStyle;
            gvMainAE.RowCountChanged += gvMainAE_RowCountChanged;
            gvMainAE.CustomDrawRowIndicator += gvMainAE_CustomDrawRowIndicator;
            gvMainAE.CustomDrawFooter += gvMainAE_CustomDrawFooter;
            gvMainAE.CustomDrawFooterCell += gvMainAE_CustomDrawFooterCell;
            gcMainAE.ForceInitialize();

        
          
        }

         private void BestFitColumnsWithImage()
         {
             gvMainAE.BestFitColumns();
             gvMainAE.Columns["GroupUserCode"].Width += 20;
             gvMainAE.Columns["GroupUserName"].Width += 30;
         }
        
        private DataTable TableTemplateGroupRole()
        {
            var dt =new DataTable();
            dt.Columns.Add("GroupUserCode", typeof(string));
            return dt;
        }
        #endregion

       
    
    }
}