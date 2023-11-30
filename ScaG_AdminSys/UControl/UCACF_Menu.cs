using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using CNY_AdminSys.Class;
using CNY_AdminSys.Info;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
namespace CNY_AdminSys.UControl
{
    public partial class UCACF_Menu : XtraUserControl
    {
        #region "Property"
        readonly Inf_Menu _inf = new Inf_Menu();
        Cls_AdminCodeFile _cls = new Cls_AdminCodeFile();
        private bool _disableEventWhenLoad = false;
        WaitDialogForm _dlg;
        private string _strPath = "";
        #endregion

        #region "Contructor"
        public UCACF_Menu()
        {
            InitializeComponent();

            pictureBoxIcon.DoubleClick += pictureBoxIcon_DoubleClick;
            bteGuideDocument.Properties.ReadOnly = true;
            bteProcessDocument.Properties.ReadOnly = true;


            bteGuideDocument.KeyDown += bteGuideDocument_KeyDown;
            bteProcessDocument.KeyDown += bteProcessDocument_KeyDown;

            bteGuideDocument.ButtonClick += bteGuideDocument_ButtonClick;
            bteProcessDocument.ButtonClick += bteProcessDocument_ButtonClick;


            GridViewCustomInit(gcMainAE, gvMainAE);
            ProcessGeneral.LoadSearchLookup(searchModule, _inf.Menu_LoadCBModule(), "Code", "Code");
            ProcessGeneral.LoadSearchLookup(searchShowType, _inf.Menu_LoadCBShowType(), "Code", "Code");

            this.Load += Control_Load;
            searchModule.EditValueChanged += searchModule_EditValueChanged;
            searchShowType.EditValueChanged += searchShowType_EditValueChanged;

            txtShowType.Enabled = false;
            txtModule.Enabled = false;

        }





        #endregion

        #region "Form Event"
        private void Control_Load(object sender, EventArgs e)
        {
            UseControlInput("Load");
            _disableEventWhenLoad = true;
            LoadDataInGridView(gcMainAE, gvMainAE);

            DisplatDetailFocusedRowChanged(gvMainAE);
            _disableEventWhenLoad = false;

        }

        #endregion

        #region "Combobox,TextBox Event"
   

        private void searchModule_EditValueChanged(object sender, EventArgs e)
        {
            txtModule.EditValue = ProcessGeneral.GetSafeString(searchModule.EditValue) != "" ? 
                ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(searchModule)["Description"]) : string.Empty;
        }

        private void searchShowType_EditValueChanged(object sender, EventArgs e)
        {
            txtShowType.EditValue = ProcessGeneral.GetSafeString(searchShowType.EditValue) != "" ?
                ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(searchShowType)["Description"]) : string.Empty;
        }

        #endregion

        #region "Process Button Edit"


        private void bteProcessDocument_KeyDown(object sender, KeyEventArgs e)
        {
            ButtonEdit edit = (ButtonEdit) sender;
            if (edit == null) return;
            if (e.KeyCode == Keys.Delete)
            {
              
                _inf.ListMenu_Update_ProcessDocument(txtMenu_Code.Text.Trim(), null);
                FormatButtonEdit(edit, false);
                gvMainAE.SetRowCellValue(gvMainAE.FocusedRowHandle, "ProcessDocument", 0);

            }
            if (e.KeyCode == Keys.F12)
            {
                ProcessGeneral.OpenHelpForm(txtMenu_Code.Text.Trim(), false);
            }
        }

        private void bteGuideDocument_KeyDown(object sender, KeyEventArgs e)
        {

            ButtonEdit edit = (ButtonEdit) sender;
            if (edit == null) return;
            if (e.KeyCode == Keys.Delete)
            {

                _inf.ListMenu_Update_GuideDocument(txtMenu_Code.Text.Trim(), null);
                FormatButtonEdit(edit, false);
                gvMainAE.SetRowCellValue(gvMainAE.FocusedRowHandle, "GuideDocument", 0);
            }
            if (e.KeyCode == Keys.F12)
            {
             
                ProcessGeneral.OpenHelpForm(txtMenu_Code.Text.Trim(), true);
            }
        }

        private void bteProcessDocument_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
         
            ButtonEdit edit = (ButtonEdit) sender;
            if (edit == null) return;
            OpenFileDialog opF = new OpenFileDialog
            {
                RestoreDirectory = true,
                Multiselect = false,
                Filter = @"pdf Files|*.pdf",
                Title = @"Selected Process Document File Upload"
            };
            if (opF.ShowDialog() == DialogResult.OK)
            {

                byte[] contents = ProcessGeneral.ConvertFileToByteArray(opF.FileName);
                _inf.ListMenu_Update_ProcessDocument(txtMenu_Code.Text.Trim(), contents);
                FormatButtonEdit(edit, true);
                gvMainAE.SetRowCellValue(gvMainAE.FocusedRowHandle, "ProcessDocument", 1);

            }
        }

        private void bteGuideDocument_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
          
            ButtonEdit edit = (ButtonEdit) sender;
            if (edit == null) return;
            OpenFileDialog opF = new OpenFileDialog
            {
                RestoreDirectory = true,
                Multiselect = false,
                Filter = @"pdf Files|*.pdf",
                Title = @"Selected Guide Document File Upload"
            };
            if (opF.ShowDialog() == DialogResult.OK)
            {
                byte[] contents = ProcessGeneral.ConvertFileToByteArray(opF.FileName);
                _inf.ListMenu_Update_GuideDocument(txtMenu_Code.Text.Trim(), contents);
                FormatButtonEdit(edit, true);
                gvMainAE.SetRowCellValue(gvMainAE.FocusedRowHandle, "GuideDocument", 1);
            }
        }





        #endregion


        #region "Use Button Control Input Empty Control Input"
        private void ClearTextControl()
        {
            txtSystemName.EditValue = "";
            txtMenu_Code.EditValue = "";
            txtFolderContain.EditValue = "";
            txtFormCode.EditValue = "";
            txtFormName.EditValue = "";
            txtProjectCode.EditValue = "";
            searchModule.EditValue = "";
            searchShowType.EditValue = "";
            _strPath = "";
            chkActive.Checked = true;
            pictureBoxIcon.Image = null;
        }


        private void UseControlInput(string option)
        {
            if (option == "Load")
            {
                txtMenu_Code.Enabled = false;
                txtFolderContain.Enabled = false;
                txtFormCode.Enabled = false;
                txtFormName.Enabled = false;
                txtProjectCode.Enabled = false;
                searchModule.Enabled = false;
                searchShowType.Enabled = false;
                chkActive.Enabled = false;
                pictureBoxIcon.Enabled = false;
                bteGuideDocument.Enabled = false;
                bteProcessDocument.Enabled = false;
                txtSystemName.Enabled = false;
            }
            else
            {


                if (option == "Add")
                {
                    bteGuideDocument.Enabled = false;
                    bteProcessDocument.Enabled = false;
                }
                else
                {
                    bteGuideDocument.Enabled = true;
                    bteProcessDocument.Enabled = true;
                }
                txtMenu_Code.Enabled = false;
                txtFolderContain.Enabled = true;
                txtFormCode.Enabled = true;
                txtFormName.Enabled = true;
                txtProjectCode.Enabled = true;
                searchModule.Enabled = true;
                searchShowType.Enabled = true;
                chkActive.Enabled = true;
                pictureBoxIcon.Enabled = true;
                txtSystemName.Enabled = DeclareSystem.SysUserName.ToUpper() == "ADMIN";

            }

        }

        #endregion

        #region "methold"

        private void FormatButtonEdit(ButtonEdit bte, bool isContent)
        {
            if (isContent)
            {
                bte.BackColor = Color.Aquamarine;
                bte.EditValue = @"Press F12 Key View Document";
            }
            else
            {
                bte.BackColor = Color.White;
                bte.EditValue = null;
            }
        }

        private void GridViewCustomInit(GridControl gc, GridView gv)
        {







            gc.UseEmbeddedNavigator = true;

            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Append.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gv.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gv.OptionsCustomization.AllowColumnMoving = false;
            gv.OptionsCustomization.AllowQuickHideColumns = true;

            gv.OptionsCustomization.AllowSort = true;

            gv.OptionsCustomization.AllowFilter = true;

            gv.OptionsView.AllowCellMerge = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowIndicator = true;
            gv.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gv.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gv.OptionsView.ShowAutoFilterRow = false;
            gv.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gv.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gv.OptionsNavigation.AutoFocusNewRow = true;
            gv.OptionsNavigation.UseTabKey = true;

            gv.OptionsSelection.MultiSelect = false;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gv.OptionsSelection.EnableAppearanceFocusedRow = false;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;


            //gv.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            //gv.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;
            //SetResetSelectionClickOutsideCheckboxSelectorPropertyValue(gv, false);





            gv.OptionsView.EnableAppearanceEvenRow = false;
            gv.OptionsView.EnableAppearanceOddRow = false;
            gv.OptionsView.ShowFooter = false;

            gv.OptionsHint.ShowFooterHints = false;
            gv.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gv.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gv.OptionsFind.AllowFindPanel = true;

            gv.OptionsFind.AlwaysVisible = false;





            gv.OptionsFind.ShowCloseButton = true;
            gv.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gv)
            {
                IsPerFormEvent = true,
                IsDrawFilter = true,
            };




            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;











            //     gvMainAE.OptionsHint.ShowCellHints = true;



            //   gv.MouseMove += gv_MouseMove;
            gv.FocusedRowChanged += gv_FocusedRowChanged;
            gv.RowStyle += gv_RowStyle;
            gv.RowCountChanged += gv_RowCountChanged;
            gv.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gv.FocusedColumnChanged += gv_FocusedColumnChanged;
            gc.ForceInitialize();
        }

        private void LoadDataInGridView(GridControl gc, GridView gv)
        {



            _dlg = new WaitDialogForm();
            DataTable dtS = _inf.GridViewData_Load();

        
            if (gv.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in dtS.Columns)
                {
                    gv.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }
                ProcessGeneral.HideVisibleColumnsGridView(gv, false, "MenuImage", "ProcessDocument", "GuideDocument", "ShowCode", "WorkFunCode");



                ProcessGeneral.SetGridColumnHeader(gv.Columns["FormCode"], "Form Code", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["SystemName"], "System Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["FormName"], "Form Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["MenuCode"], "Code", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["ProjectCode"], "Project Code", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["FolderContainForm"], "Folder Contain Form", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["ShowDescription"], "Show Type", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["WorkFunName"], "Module", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["IsActive"], "Active", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            }
            gv.BeginUpdate();




            //gc.DataSource = null;
            //gv.Columns.Clear();

            gc.DataSource = dtS;


            gv.BestFitColumns();
            gv.EndUpdate();

            _dlg.Close();
        }

     
        private void DisplatDetailFocusedRowChanged(GridView gv)
        {
            if (gv.RowCount == 0)
            {
                txtMenu_Code.EditValue = string.Empty;
                txtFormCode.EditValue = string.Empty;
                txtFormName.EditValue = string.Empty;
                txtProjectCode.EditValue = string.Empty;
                txtFolderContain.EditValue = string.Empty;
                pictureBoxIcon.Image = null;
                _strPath = "";
                FormatButtonEdit(bteGuideDocument, false);
                FormatButtonEdit(bteProcessDocument, false);

                searchModule.EditValue = "";
                searchShowType.EditValue = "";
                chkActive.Checked = true;

                txtSystemName.EditValue = string.Empty;






            }
            else
            {
                txtSystemName.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["SystemName"]));
                txtMenu_Code.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["MenuCode"]));
                txtFormCode.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["FormCode"]));
                txtFormName.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["FormName"]));
                txtProjectCode.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["ProjectCode"]));
                txtFolderContain.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["FolderContainForm"]));
                object photoValue = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["MenuImage"]);
                if (!Convert.IsDBNull(photoValue) && photoValue != null)
                {
                    pictureBoxIcon.Image = ProcessGeneral.ConvertByteArrayToImage((byte[])photoValue);//set image property of the picture box by creating a image from stream 
                    pictureBoxIcon.SizeMode = PictureBoxSizeMode.CenterImage;//set size mode property of the picture box to stretch 
                    pictureBoxIcon.Refresh();//refresh picture box
                }
                else
                {
                    pictureBoxIcon.Image = null;
                }
                _strPath = "";
                bool eG = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["GuideDocument"])) == 1;
                bool eP = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["ProcessDocument"])) == 1;
                FormatButtonEdit(bteGuideDocument, eG);
                FormatButtonEdit(bteProcessDocument, eP);

                searchModule.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["WorkFunCode"]));
                searchShowType.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["ShowCode"]));
                chkActive.Checked = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["IsActive"]));
            }

        }
        #endregion

        #region "gridview event"



    

  

        private void gv_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged(gv);
            }
        }
        private void gv_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged(gv);
            }

        }

        private void gv_RowCountChanged(object sender, EventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
            bool selected = gv.IsRowSelected(e.RowHandle);
            if (selected)
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }

            if (selected)
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



        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (gv.FocusedRowHandle == e.RowHandle)
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;


            }


        }
        #endregion

        #region "override button menubar click"

        public bool PerformAdd()
        {
            UseControlInput("Add");
            ClearTextControl();
            gcMainAE.Enabled = false;
            txtFormCode.Focus();
            txtMenu_Code.EditValue = _inf.ListMenu_CreateMenuCode();
            return true;

        }


        /// <summary>
        /// Perform when click edit button
        /// </summary>
        public bool PerformEdit()
        {
            if (gvMainAE.RowCount == 0 || !gvMainAE.IsDataRow(gvMainAE.FocusedRowHandle))
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return false;
            }


            UseControlInput("Edit");
            gcMainAE.Enabled = false;

          
            if (DeclareSystem.SysUserName.ToUpper() == "ADMIN")
            {
                txtSystemName.Focus();
            }
            else
            {
                txtFormName.Focus();
            }
            
            return true;
        }
        public bool PerformDelete()
        {
            if (txtMenu_Code.Text.Trim() == "")
            {
                XtraMessageBox.Show("No row is selected to perform deleting", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return false;
            }

        
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to delete menu {0} !!!?? (yes/No) \n Note:You could not restore this record!", txtMenu_Code.Text.Trim()), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return false;
            if (_inf.Delete(txtMenu_Code.Text.Trim()) == 1)
            {
                XtraMessageBox.Show(string.Format("Menu {0} has been deleted from the database", txtMenu_Code.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                _disableEventWhenLoad = true;
                LoadDataInGridView(gcMainAE, gvMainAE);
                DisplatDetailFocusedRowChanged(gvMainAE);
                _disableEventWhenLoad = false;
                return true;
            }
            else
            {
                XtraMessageBox.Show(string.Format("Could not delete Menu {0} \n because of an error in the process of deleting data", txtMenu_Code.Text.Trim()),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }



        }


        public bool PerformSave(string option)
        {




            if (!CheckInputControlBeforSave())
                return false;
            bool isSuccess = false;
            SetParameterWhenSaveData();
            if (option.ToUpper() == "ADD")
            {
                if (_inf.Insert(_cls) == 1)
                {
                    XtraMessageBox.Show(string.Format("Insert Successful Menu : {0}", txtMenu_Code.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Menu {0} have been already exists in system", txtMenu_Code.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {


                int resultUpdate = string.IsNullOrEmpty(_strPath) ? _inf.ListMenu_Update_NoneImage(_cls) : _inf.ListMenu_Update_WithImage(_cls);
                if (resultUpdate == 1)
                {
                    XtraMessageBox.Show(string.Format("Update Successful Menu : {0}", txtMenu_Code.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    XtraMessageBox.Show(string.Format("Menu {0} not exists in system", txtMenu_Code.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                }

            }
            if (!isSuccess) return false;

            _strPath = "";
            UseControlInput("Load");
            gcMainAE.Enabled = true;

            _disableEventWhenLoad = true;
            LoadDataInGridView(gcMainAE, gvMainAE);
            DisplatDetailFocusedRowChanged(gvMainAE);
            _disableEventWhenLoad = false;
            return true;






        }

        public bool PerformCancel()
        {


            UseControlInput("Load");
            gcMainAE.Enabled = true;

            _disableEventWhenLoad = true;
            DisplatDetailFocusedRowChanged(gvMainAE);
            _disableEventWhenLoad = false;
            return true;


        }
        public bool PerformRefresh(string option)
        {
            switch (option.ToUpper())
            {
                case "":
                {

                    _disableEventWhenLoad = true;
                    LoadDataInGridView(gcMainAE, gvMainAE);
                    DisplatDetailFocusedRowChanged(gvMainAE);
                    _disableEventWhenLoad = false;

                }
                    break;
                case "ADD":
                {
                    ClearTextControl();
                }
                    break;
                case "EDIT":
                {
                    _disableEventWhenLoad = true;
                    DisplatDetailFocusedRowChanged(gvMainAE);
                    _disableEventWhenLoad = false;
                }
                    break;
                default: break;
            }

            return true;
        }


        #endregion

        #region "Update Data"

        private void SetParameterWhenSaveData()
        {
            _cls.MenuCode = ProcessGeneral.GetSafeString(txtMenu_Code.EditValue);
            _cls.FormName = ProcessGeneral.GetSafeString(txtFormName.EditValue);
            _cls.FormCode = ProcessGeneral.GetSafeString(txtFormCode.EditValue);
            _cls.ProjectCode = ProcessGeneral.GetSafeString(txtProjectCode.EditValue);
            _cls.FolderContainForm = ProcessGeneral.GetSafeString(txtFolderContain.EditValue);
            _cls.WorkFunCode = ProcessGeneral.GetSafeString(searchModule.EditValue);
            _cls.ShowCode = ProcessGeneral.GetSafeString(searchShowType.EditValue);
            _cls.MenuImagePath = _strPath;
            _cls.IsActiveMenu = chkActive.Checked;
            _cls.SystemName = ProcessGeneral.GetSafeString(txtSystemName.EditValue);

        }
        private bool CheckInputControlBeforSave()
        {
            if (ProcessGeneral.GetSafeString(txtMenu_Code.EditValue) == "")
            {
                XtraMessageBox.Show("Menu Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtMenu_Code.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(txtFormName.EditValue) == "")
            {
                XtraMessageBox.Show("Form Name could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtFormName.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(searchShowType.EditValue) == "")
            {
                XtraMessageBox.Show("Show Type could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                searchShowType.Focus();
                return false;
            }
            //if (ProcessGeneral.GetSafeString(txtFormCode.EditValue) == "")
            //{
            //    XtraMessageBox.Show("Form Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            //    txtFormCode.Focus();
            //    return false;
            //}
            //if (ProcessGeneral.GetSafeString(txtProjectCode.EditValue) == "")
            //{
            //    XtraMessageBox.Show("Project Code Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            //    txtProjectCode.Focus();
            //    return false;
            //}
            return true;
        }
       
        #endregion

       

        #region "PictureBox Event"

        private void pictureBoxIcon_DoubleClick(object sender, EventArgs e)
        {
         
            var opf = new OpenFileDialog()
            {
                Title = @"Open File Image",
                Filter = @"Image File (.JPG,.PNG)|*.jpg;*.png",
                RestoreDirectory = true,
            }; if (opf.ShowDialog() != DialogResult.OK)
                return;
            _strPath = Path.GetFullPath(opf.FileName).Trim();
            if (_strPath.Trim() != "")
            {
                pictureBoxIcon.Image = Image.FromFile(_strPath.Trim());
                pictureBoxIcon.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBoxIcon.Refresh();
            }
            else
            {
                pictureBoxIcon.Image = null;
            }
        }
        #endregion
    }
}
