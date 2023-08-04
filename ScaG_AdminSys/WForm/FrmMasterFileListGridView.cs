using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CNY_AdminSys.Class;
using CNY_AdminSys.Common;
using CNY_AdminSys.Info;
using CNY_AdminSys.Properties;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using GridSumCol = DevExpress.Data.SummaryItemType;

namespace CNY_AdminSys.WForm
{
    public partial class FrmMasterFileListGridView : FrmBase
    {
        #region "Property"
        private string _tileFrom = "";
        private readonly Inf_MasterFileList _inf = new Inf_MasterFileList();
        private bool _CheckKeyDownGv;
        private WaitDialogForm _dlg;
        private bool _PerformEditValueChangedEvent = true;
        private PermissionFormInfo _perInfo = new PermissionFormInfo();
        private readonly RepositoryItemTextEdit _repositoryItemTextCode;
        private readonly RepositoryItemTextEdit _repositoryItemTextMaxLength100;
        private readonly RepositoryItemTextEdit _repositoryItemTextMaxLength200;
        private RepositoryItemSearchLookUpEdit _gridSLkModule;
        private readonly RepositoryItemPictureEdit _repositoryPicEditIcon;
        private List<Int64> _lDel = new List<Int64>();
        private DXMenuItem[] _menuMasterFileList = null;
        #endregion

        #region "Constructor"
        public FrmMasterFileListGridView(string tileFrom, string tileText, PermissionFormInfo perInfo)
        {
            InitializeComponent();
            _perInfo = perInfo;
            _tileFrom = tileFrom;
            this.Text = tileText;

            #region "Repository"
            _repositoryPicEditIcon = new RepositoryItemPictureEdit()
            {
                AutoHeight = false,
                SizeMode = PictureSizeMode.Squeeze,
            };

            _repositoryItemTextCode = new RepositoryItemTextEdit()
            {
                CharacterCasing = CharacterCasing.Upper,
                AutoHeight = false,
                MaxLength = 20
            };
            _repositoryItemTextCode.ContextImageOptions.Image = new Bitmap(Resources.chartsshowlegend_32x32, new Size(17, 17));

            _repositoryItemTextMaxLength100 = new RepositoryItemTextEdit()
            {
                AutoHeight = false,
                MaxLength = 100
            };

            _repositoryItemTextMaxLength200 = new RepositoryItemTextEdit()
            {
                AutoHeight = false,
                MaxLength = 200
            };
            #endregion

            InitGridView();
            gvMain.Click += GvMain_Click;
            btnAddRow.Click += BtnAddRow_Click;
            btnDeleteRow.Click += BtnDeleteRow_Click;
            InitMenuItem();
            this.FormClosing += FrmMasterFileListGridView_FormClosing;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadButtonWhenLoad();
            LoadDataGridView();
        }

        private void LoadButtonWhenLoad()
        {
            AllowEdit = false;
            AllowAdd = false;
            AllowDelete = false;
            AllowBreakDown = false;
            AllowCheck = false;
            AllowCopyObject = false;
            AllowGenerate = false;
            AllowCancel = false;
            AllowRevision = false;
            AllowFind = false;
            AllowPrint = false;
            AllowRangeSize = false;
            AllowCombine = false;

            AllowSave = true;
            AllowRefresh = true;
            AllowClose = true;

            EnableSave = _perInfo.PerUpd || _perInfo.PerIns || _perInfo.PerDel;
            EnableRefresh = true;
            EnableClose = true;
        }
        #endregion

        #region "Process GridView"
        private void LoadDataGridView()
        {
            _lDel.Clear();
            DataTable dt = _inf.MasterFileList_Load();

            gvMain.BeginUpdate();
            gcMain.DataSource = null;
            gvMain.Columns.Clear();
            gcMain.DataSource = dt;

            Dictionary<string, bool> dicCol = new Dictionary<string, bool>
            {
                {"PK", false },
                {"Code", true},
                {"Name", true},
                {"Description", true},
                {"Icon", true},
                {"Module", true},
                {"ModuleGroup", false},
                {"RowState", false},
            };
            gvMain.VisibleAndSortGridColumn(dicCol);

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Code"], "Master File Code", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
            gvMain.Columns["Code"].AppearanceCell.Options.UseTextOptions = true;
            gvMain.Columns["Code"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["Code"].ColumnEdit = _repositoryItemTextCode;
            gvMain.Columns["Code"].SummaryItem.SummaryType = GridSumCol.Count;
            gvMain.Columns["Code"].SummaryItem.DisplayFormat = @"Total : ";

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Name"], "Name", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
            gvMain.Columns["Name"].AppearanceCell.Options.UseTextOptions = true;
            gvMain.Columns["Name"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["Name"].ColumnEdit = _repositoryItemTextMaxLength100;
            gvMain.Columns["Name"].SummaryItem.SummaryType = GridSumCol.Count;
            gvMain.Columns["Name"].SummaryItem.DisplayFormat = @"{0:N0} (item)";

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Description"], "Description", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
            gvMain.Columns["Description"].AppearanceCell.Options.UseTextOptions = true;
            gvMain.Columns["Description"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["Description"].ColumnEdit = _repositoryItemTextMaxLength200;

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Icon"], "Icon", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
            //gvMain.Columns["Icon"].ColumnEdit = _repositoryPicEditIcon;

            _gridSLkModule = ProcessGeneral.CreateRepositoryItemSearchLookUpEdit(_inf.LoadModuleOnGridView(),
                "Description", "ModuleCode", BestFitMode.BestFitResizePopup);
            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Module"], "Module", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
            gvMain.Columns["Module"].AppearanceCell.Options.UseTextOptions = true;
            gvMain.Columns["Module"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["Module"].ColumnEdit = _gridSLkModule;
            _gridSLkModule.EditValueChanged += _gridSLkModule_EditValueChanged;

            gvMain.Columns["ModuleGroup"].GroupIndex = 0;
            gvMain.CollapseAllGroups();
            //gvMain.ExpandAllGroups();

            gvMain.BestFitColumns();
            gvMain.Columns["Icon"].Width = 50;
            gvMain.Columns["Module"].Width += 22;
            gvMain.FocusedColumn = gvMain.VisibleColumns[0];
            gvMain.EndUpdate();
        }

        private void InitGridView()
        {
            gcMain.UseEmbeddedNavigator = true;
            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = true;
            gvMain.OptionsCustomization.AllowFilter = true;

            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsView.ShowFooter = true;
            gvMain.OptionsView.ColumnAutoWidth = false;
            //gvMain.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsPrint.AutoWidth = false;

            gvMain.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            gvMain.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
                IsDrawFilter = true,
                IsBestFitDoubleClick = true,
            };
            gvMain.OptionsMenu.EnableFooterMenu = false;

            gvMain.RowCellStyle += GvMain_RowCellStyle;
            gvMain.RowCountChanged += GvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += GvMain_CustomDrawRowIndicator;
            //gvMain.CustomDrawCell += GvMain_CustomDrawCell;

            gvMain.FocusedRowChanged += GvMain_FocusedRowChanged;
            gvMain.FocusedColumnChanged += GvMain_FocusedColumnChanged;
            //gvMain.ShowingEditor += GvMain_ShowingEditor; //wait for use

            gcMain.Paint += GcMain_Paint;
            gvMain.LeftCoordChanged += GvMain_LeftCoordChanged;
            gvMain.TopRowChanged += GvMain_TopRowChanged;
            gvMain.MouseMove += GvMain_MouseMove;

            gvMain.CellValueChanged += GvMain_CellValueChanged;
            gcMain.KeyDown += GcMain_KeyDown;
            gcMain.EditorKeyDown += GcMain_EditorKeyDown;
            gvMain.CustomDrawFooter += GvMain_CustomDrawFooter;
            gvMain.CustomDrawFooterCell += GvMain_CustomDrawFooterCell;
            gvMain.ValidatingEditor += GvMain_ValidatingEditor;
            gvMain.PopupMenuShowing += GvMain_PopupMenuShowing;

            gvMain.CustomDrawGroupRow += GvMain_CustomDrawGroupRow;

            gcMain.ForceInitialize();
        }

        private void InsertNewRow()
        {
            DataTable dt = gcMain.DataSource as DataTable;
            if (dt == null) return;
            if (gvMain.IsEditing) gvMain.CloseEditor();

            DataTable dtModule = _gridSLkModule.DataSource as DataTable;
            string moduleCode = ProcessGeneral.GetSafeString(ProcessGeneral.GetValueFirstRowByFieldName(dtModule, "ModuleCode"));

            int rH = gvMain.FocusedRowHandle;

            DataRow dr = dt.NewRow();
            dr["PK"] = -1;
            dr["Code"] = "";
            dr["Name"] = "";
            dr["Description"] = "";
            dr["Icon"] = new byte[] { };
            dr["Module"] = ""; //moduleCode
            dr["RowState"] = DataStatus.Insert.ToString();
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();

            gcMain.Focus();
            gvMain.Focus();
            ProcessGeneral.SetFocusedCellOnGrid(gvMain, 0, "Code");
            gvMain.BestFitColumns();
            gvMain.Columns["Icon"].Width = 50;
            gvMain.Columns["Module"].Width += 22;

            gvMain.CollapseAllGroups();
            gvMain.ExpandGroupRow(-1);
        }

        private void GetTableDeleteMultiRow()
        {
            try
            {
                int[] arrRow = gvMain.GetSelectedRows().OrderByDescending(p => p).ToArray();
                if (arrRow.Length <= 0)
                {
                    XtraMessageBox.Show("No rows selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dt = gcMain.DataSource as DataTable;
                if (dt == null) return;
                DialogResult dlRs = XtraMessageBox.Show("Do you want to delete the selected rows?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlRs != DialogResult.Yes) return;
                foreach (int row in arrRow)
                {
                    if (row < 0) continue;
                    string rowState = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(row, "RowState"));
                    if (rowState != DataStatus.Insert.ToString())
                    {
                        Int64 pk = ProcessGeneral.GetSafeInt64(gvMain.GetRowCellValue(row, "PK"));
                        _lDel.Add(pk);
                    }

                    DataRow drDel = gvMain.GetDataRow(row);
                    dt.Rows.Remove(drDel);
                }
                dt.AcceptChanges();
                gvMain.BestFitColumns();
                gvMain.Columns["Icon"].Width = 50;
                gvMain.Columns["Module"].Width += 22;
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.ToString());
            }
        }

        private void DeleteRowOnGridView()
        {
            var qDel = _lDel.Where(p => p > 0).Select(p => p.ToString()).Distinct().ToList();
            if (qDel.Any())
            {
                string sQ1 = string.Join(",", qDel);
                if (!string.IsNullOrEmpty(sQ1))
                {
                    string sql = string.Format("DELETE FROM [dbo].[CNYSYS14] WHERE [PK] IN ({0})", sQ1);
                    _inf.BolExcuteSql(sql);
                    _lDel.Clear();
                }
            }
        }

        private void ReLoadPositionGridView(string codeFindGridView, string fieldName, int topIndexGridView)
        {
            int rHfocused = gvMain.FindRowInGridByColumn(codeFindGridView, fieldName);
            if (rHfocused < 0) return;
            gvMain.BeginUpdate();
            gvMain.TopRowIndex = topIndexGridView;
            gvMain.BeginSelection();
            gvMain.SetFocusedRowOnGrid(rHfocused);
            gvMain.EndSelection();
            gvMain.EndUpdate();
        }

        private DataTable TableCheckDataBeforeSave()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Icon", typeof(byte[]));
            dt.Columns.Add("Module", typeof(string));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("RowHandle", typeof(Int32));
            return dt;
        }

        private bool CheckDataBeforeSave(DataTable dtCheck, bool isShowMess, out bool isChange)
        {
            isChange = false;
            foreach (DataRow dr in dtCheck.Rows)
            {
                if (!isChange && ProcessGeneral.GetSafeString(dr["RowState"]) != DataStatus.Unchange.ToString())
                {
                    isChange = true;
                }

                if (ProcessGeneral.GetSafeString(dr["Code"]) == "")
                {
                    if (isShowMess)
                    {
                        XtraMessageBox.Show("Master File Code is not empty", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        ProcessGeneral.SetFocusedCellOnGrid(gvMain, ProcessGeneral.GetSafeInt(dr["RowHandle"]), "Code");
                        gvMain.ShowEditor();
                    }
                    return false;
                }
                if (ProcessGeneral.GetSafeString(dr["Name"]) == "")
                {
                    if (isShowMess)
                    {
                        XtraMessageBox.Show("Name is not empty", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        ProcessGeneral.SetFocusedCellOnGrid(gvMain, ProcessGeneral.GetSafeInt(dr["RowHandle"]), "Name");
                        gvMain.ShowEditor();
                    }
                    return false;
                }
                //if (ProcessGeneral.GetSafeString(dr["Description"]) == "")
                //{
                //    if (isShowMess)
                //    {
                //        XtraMessageBox.Show("Description is not empty", "Warning", MessageBoxButtons.OK,
                //            MessageBoxIcon.Warning);
                //        ProcessGeneral.SetFocusedCellOnGrid(gvMain, ProcessGeneral.GetSafeInt(dr["RowHandle"]), "Description");
                //        gvMain.ShowEditor();
                //    }
                //    return false;
                //}

                byte[] icon = (byte[])dr["Icon"];
                if (icon.Length <= 0)
                {
                    if (isShowMess)
                    {
                        XtraMessageBox.Show("Icon is not empty", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        ProcessGeneral.SetFocusedCellOnGrid(gvMain, ProcessGeneral.GetSafeInt(dr["RowHandle"]), "Icon");
                        gvMain.ShowEditor();
                    }
                    return false;
                }

                if (ProcessGeneral.GetSafeString(dr["Module"]) == "")
                {
                    if (isShowMess)
                    {
                        XtraMessageBox.Show("Module is not empty", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        ProcessGeneral.SetFocusedCellOnGrid(gvMain, ProcessGeneral.GetSafeInt(dr["RowHandle"]), "Module");
                        gvMain.ShowEditor();
                    }
                    return false;
                }
            }
            return true;
        }

        private Ctrl_MFListSave SaveData(MFListSaveType saveType)
        {
            if (gvMain.ActiveEditor != null)
            {
                gvMain.CloseEditor();
            }
            int rH = gvMain.FocusedRowHandle;
            int topRowIndexGridView = gvMain.TopRowIndex;
            string codeFindGridView = gvMain.IsDataRow(rH)
                ? ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rH, "Code"))
                : "";
            Ctrl_MFListSave ctrl = new Ctrl_MFListSave();
            DataTable dtGridView = gcMain.DataSource as DataTable;
            if (dtGridView == null || dtGridView.Rows.Count <= 0)
            {
                ctrl.Result = false;
                return ctrl;
            }
            DataTable dtCheck = TableCheckDataBeforeSave();
            if (gvMain.RowCount > 0)
            {
                int[] qData = Enumerable.Range(0, dtGridView.Rows.Count).Where(p =>
                    ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(p, "RowState")) !=
                    DataStatus.Unchange.ToString()).ToArray();
                foreach (int rowH in qData)
                {
                    dtCheck.Rows.Add(ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rowH, "Code")),
                        ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rowH, "Name")),
                        ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rowH, "Description")),
                        (byte[])gvMain.GetRowCellValue(rowH, "Icon"),
                        ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rowH, "Module")),
                        ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rowH, "RowState")), rowH);
                }
            }
            bool checkDataGridView = CheckDataBeforeSave(dtCheck, saveType == MFListSaveType.SaveClick, out var isChangeGridView);
            if (!checkDataGridView)
            {
                ctrl.Result = false;
                return ctrl;
            }

            bool changedData = isChangeGridView || _lDel.Count > 0;

            if (!changedData)
            {
                ctrl.Result = false;
                return ctrl;
            }

            if (saveType == MFListSaveType.FormMainClose)
            {
                DialogResult dlRs =
                    XtraMessageBox.Show(
                        "Do you want to save changes before leave your job? ", "Question",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlRs != DialogResult.Yes)
                {
                    ctrl.Result = false;
                    return ctrl;
                }
            }
            _dlg = new WaitDialogForm();
            SaveDataGridView(dtGridView);

            _dlg.Close();
            ctrl.Result = true;
            ctrl.CodeFindGridView = codeFindGridView;
            ctrl.FieldName = "Code";
            ctrl.TopIndexGridView = topRowIndexGridView;
            return ctrl;
        }

        private DataTable TableSaveTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Icon", typeof(byte[]));
            dt.Columns.Add("Module", typeof(string));
            dt.Columns.Add("UserUpd", typeof(string));
            return dt;
        }

        private void SaveDataGridView(DataTable dtGridViewData)
        {
            DataTable dtInsert = TableSaveTemp();
            DataTable dtUpdate = TableSaveTemp();
            foreach (DataRow dr in dtGridViewData.Rows)
            {
                string rowState = ProcessGeneral.GetSafeString(dr["RowState"]);
                if (rowState == DataStatus.Unchange.ToString()) continue;

                MasterFileListInfo info = new MasterFileListInfo
                {
                    PK = ProcessGeneral.GetSafeInt64(dr["PK"]),
                    Code = ProcessGeneral.GetSafeString(dr["Code"]),
                    Name = ProcessGeneral.GetSafeString(dr["Name"]),
                    Description = ProcessGeneral.GetSafeString(dr["Description"]),
                    Icon = (byte[])dr["Icon"],
                    Module = ProcessGeneral.GetSafeString(dr["Module"]),
                    UserUpd = DeclareSystem.SysUserName.ToUpper(),
                };
                if (rowState == DataStatus.Insert.ToString())
                {
                    dtInsert.Rows.Add(info.PK, info.Code, info.Name, info.Description, info.Icon, info.Module, info.UserUpd);
                }
                if (rowState == DataStatus.Update.ToString())
                {
                    dtUpdate.Rows.Add(info.PK, info.Code, info.Name, info.Description, info.Icon, info.Module, info.UserUpd);
                }
            }

            DeleteRowOnGridView();

            if (dtInsert.Rows.Count > 0)
            {
                _inf.MasterFileList_Insert(dtInsert);
            }

            if (dtUpdate.Rows.Count > 0)
            {
                _inf.MasterFileList_Update(dtUpdate);
            }
        }
        #endregion

        #region "Override button menubar click"
        protected override void PerformSave()
        {
            Ctrl_MFListSave ctrlSave = SaveData(MFListSaveType.SaveClick);
            if (!ctrlSave.Result) return;
            _dlg = new WaitDialogForm();
            LoadDataGridView();
            ReLoadPositionGridView(ctrlSave.CodeFindGridView, ctrlSave.FieldName, ctrlSave.TopIndexGridView);
            _dlg.Close();
            XtraMessageBox.Show("Records is updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void PerformRefresh()
        {
            //DialogResult dlgRs = XtraMessageBox.Show("Do you want to refresh data form Master File List?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dlgRs != DialogResult.Yes) return;
            _dlg = new WaitDialogForm();
            LoadDataGridView();
            _dlg.Close();
        }

        private void FrmMasterFileListGridView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!EnableSave)
                return;
            SaveData(MFListSaveType.FormMainClose);
        }
        #endregion

        #region "Gridview Events"
        private void _gridSLkModule_EditValueChanged(object sender, EventArgs e)
        {
            var searchLk = sender as SearchLookUpEdit;
            if (searchLk == null) return;
            gvMain.CloseEditor();
        }

        private void GvMain_Click(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (gv.RowCount < 0) return;
            GridControl gc = gv.GridControl;
            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
            if (!hi.InRowCell) return;
            GridColumn gCol = hi.Column;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
            if (fieldName == "Module" || fieldName == "Icon")
            {
                if (gv.ActiveEditor == null)
                {
                    gv.ShowEditor();
                }
            }
        }

        private void GvMain_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.AutoFilter) return;
            if (e.HitInfo.InRow)
            {
                GridView gv = sender as GridView;
                if (gv == null) return;
                GridColumn gCol = gv.FocusedColumn;
                if (gCol == null) return;
                if (!gv.IsDataRow(e.HitInfo.RowHandle) || e.Menu == null || gCol.FieldName == "Icon") return;
                gv.FocusedRowHandle = e.HitInfo.RowHandle;
                foreach (DXMenuItem item in _menuMasterFileList)
                {
                    e.Menu.Items.Add(item);
                }
            }
        }

        private void GvMain_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            int rH = gv.FocusedRowHandle;
            if (!gv.IsDataRow(rH)) return;
            string fieldName = gCol.FieldName;
            DataView currentDataView = gv.DataSource as DataView;

            switch (fieldName)
            {
                case "Code":
                    string code = ProcessGeneral.GetSafeString(e.Value);
                    if (string.IsNullOrEmpty(code))
                    {
                        e.ErrorText = "Master File Code is not empty";
                        e.Valid = false;
                        return;
                    }

                    if (currentDataView != null)
                        for (int i = 0; i < currentDataView.Count; i++)
                        {
                            if (i != gv.GetDataSourceRowIndex(gv.FocusedRowHandle))
                            {
                                if (currentDataView[i]["Code"].ToString() == code)
                                {
                                    e.ErrorText = string.Format("Master File Code {0} is duplicated", code);
                                    e.Valid = false;
                                    return;
                                }
                            }
                        }

                    break;
                case "Name":
                    string name = ProcessGeneral.GetSafeString(e.Value);
                    if (string.IsNullOrEmpty(name))
                    {
                        e.ErrorText = "Name is not empty";
                        e.Valid = false;
                    }
                    break;
                case "Icon":
                    string icon = ProcessGeneral.GetSafeString(e.Value);
                    if (string.IsNullOrEmpty(icon))
                    {
                        e.ErrorText = "Icon is not empty";
                        e.Valid = false;
                    }
                    break;
                case "Module":
                    string module = ProcessGeneral.GetSafeString(e.Value);
                    if (string.IsNullOrEmpty(module))
                    {
                        e.ErrorText = "Module is not empty";
                        e.Valid = false;
                    }
                    break;
            }
        }

        private void GvMain_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            //string tag = ProcessGeneral.GetSafeString(e.Column.Tag).ToUpper();
            //if (e.Column.FieldName != "Status") return; //if (e.Column.FieldName != "Status" && tag != "SUM") return;
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "Code")
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Graphics.DrawString(@"  " + e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            }
            else
            {
                e.Appearance.ForeColor = Color.Chocolate;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            }
            e.Handled = true;
        }

        private void GvMain_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //if (!_isShowFooter) return;
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }

        private void GcMain_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_CheckKeyDownGv)
            {
                GcMain_KeyDown(sender, e);
            }
            _CheckKeyDownGv = false;
        }

        private void GcMain_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            if (gc == null) return;
            var gv = gc.FocusedView as GridView;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            int rH = gv.FocusedRowHandle;
            string fieldName = gCol.FieldName;
            _CheckKeyDownGv = true;

            #region "Insert Key"
            if (e.KeyData == Keys.Insert)
            {
                InsertNewRow();
                return;
            }
            #endregion

            #region "F8 Key
            if (e.KeyData == Keys.F8)
            {
                GetTableDeleteMultiRow();
                return;
            }
            #endregion

            #region "Process F9 Key"
            if (e.KeyCode == Keys.F9)
            {
                gv.ExpandGroupRow(rH);
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion

            #region "Process F10 Key"
            if (e.KeyCode == Keys.F10)
            {
                gv.CollapseGroupRow(rH);
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion

            #region "Process F11 Key"
            if (e.KeyCode == Keys.F11)
            {
                gv.ExpandAllGroups();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion

            #region "Process F12 Key"
            if (e.KeyCode == Keys.F12)
            {
                gv.CollapseAllGroups();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion
        }

        private void GvMain_MouseMove(object sender, MouseEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GvMain_TopRowChanged(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GvMain_LeftCoordChanged(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GcMain_Paint(object sender, PaintEventArgs e)
        {
            GridControl gv = (GridControl)sender;
            if (gv == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect((GridControl)sender, e);
        }

        private void GvMain_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GvMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

            LinearGradientBrush backBrush;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "RowState"));

            if (rowState == DataStatus.Insert.ToString())
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                if (rowState == DataStatus.Update.ToString())
                {
                    backBrush = new LinearGradientBrush(e.Bounds, Color.Aquamarine, Color.Azure, 90);
                }
                else
                {
                    backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
                }
            }

            e.Graphics.FillRectangle(backBrush, e.Bounds);

            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

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

        private void GvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void GvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
                case "Code":
                    {
                        e.Appearance.ForeColor = Color.Blue;
                    }
                    break;
            }


            switch (fieldName)
            {
                case "Code":
                    if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "RowState")) != DataStatus.Insert.ToString())
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                    }
                    break;
                case "Name":
                case "Description":
                case "Icon":
                case "Module":
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

        private void GvMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!_PerformEditValueChangedEvent) return;
            GridView gv = sender as GridView;
            if (gv == null) return;
            int rH = e.RowHandle;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "RowState")) == DataStatus.Unchange.ToString())
            {
                gv.SetRowCellValue(rH, "RowState", DataStatus.Update.ToString());
            }
            string fieldName = gCol.FieldName;

            #region Wait for use
            //switch (fieldName)
            //{
            //    case "Code":
            //        string currentCode = ProcessGeneral.GetSafeString(e.Value).ToUpper();
            //        if (!string.IsNullOrEmpty(currentCode))
            //        {
            //            DataRow dr = gv.GetDataRow(rH);
            //            bool isDuplicate = false;
            //            DataTable dtS = gcMain.DataSource as DataTable;
            //            var q1 = dtS.AsEnumerable().FirstOrDefault(p =>
            //                p.Field<string>("Code").Equals(currentCode, StringComparison.OrdinalIgnoreCase) && p != dr);
            //            if (q1 != null)
            //            {
            //                isDuplicate = true;
            //            }

            //            if (isDuplicate)
            //            {
            //                XtraMessageBox.Show(string.Format("Master File Code {0} is duplicated", currentCode),
            //                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                _PerformEditValueChangedEvent = false;
            //                ProcessGeneral.SetFocusedCellOnGrid(gv, e.RowHandle, "Code");
            //                gv.SetRowCellValue(rH, "Code", "");
            //                _PerformEditValueChangedEvent = true;
            //            }
            //        }
            //        gv.Columns["Code"].BestFit();
            //        break;
            //}


            #endregion

            switch (fieldName)
            {
                case "Code":
                case "Name":
                case "Description":
                    gv.Columns[fieldName].BestFit();
                    break;
                case "Module":
                    gv.BestFitColumns();
                    gvMain.Columns["Icon"].Width = 50;
                    gvMain.Columns["Module"].Width += 22;
                    break;
            }
        }

        private void GvMain_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
            GridGroupRowInfo groupRowInfo = e.Info as GridGroupRowInfo;
            if (groupRowInfo != null)
            {
                Rectangle groupRowBounds = groupRowInfo.DataBounds;
                Rectangle expandButtonBounds = groupRowInfo.ButtonBounds;
                Rectangle textBounds = e.Bounds;
                textBounds.X = expandButtonBounds.Right + 4;

                Brush brush = e.Cache.GetGradientBrush(groupRowBounds, Color.LemonChiffon, Color.Tan, LinearGradientMode.Horizontal);

                Brush brushText = Brushes.Black, brushTextShadow = Brushes.White;
                if (e.RowHandle == gv.FocusedRowHandle)
                {
                    brush = brushTextShadow = Brushes.DarkBlue;
                    brushText = Brushes.White;
                }

                e.Graphics.FillRectangle(brush, groupRowBounds);

                Image img = gv.GetRowExpanded(e.RowHandle)
                    ? Resources._1450289438_bullet_toggle_minus
                    : Resources._1450289456_bullet_toggle_plus;
                e.Graphics.DrawImageUnscaled(img,
                    expandButtonBounds);
                //string s = string.Format("{0}", groupRowInfo.GroupText);
                string s = groupRowInfo.GroupValueText == ""
                    ? string.Format("Adding - {0} (Master File)", gv.GetChildRowCount(e.RowHandle))
                    : string.Format("{0} - {1} (Master File)", groupRowInfo.GroupValueText,
                        gv.GetChildRowCount(e.RowHandle));
                e.Appearance.DrawString(e.Cache, s, new Rectangle(textBounds.X + 1, textBounds.Y + 1,
                    textBounds.Width, textBounds.Height), brushTextShadow);
                e.Appearance.DrawString(e.Cache, s, textBounds, brushText);
            }

            e.Handled = true;
        }

        #endregion

        #region "Process Menu Item, Button events"
        private void InitMenuItem()
        {
            DXMenuItem itemMFListInsert = new DXMenuItem("Add New Row (Insert Key)", itemMFListInsert_Click, Resources.insertrows_16);
            DXMenuItem itemMFListDelete = new DXMenuItem("Delete Selected Rows (F8 Key)", itemMFListDelete_Click, Resources.deletesheetrows_16);
            _menuMasterFileList = new DXMenuItem[] { itemMFListInsert, itemMFListDelete };
        }

        private void itemMFListDelete_Click(object sender, EventArgs e)
        {
            GetTableDeleteMultiRow();
        }

        private void itemMFListInsert_Click(object sender, EventArgs e)
        {
            InsertNewRow();
        }

        private void BtnDeleteRow_Click(object sender, EventArgs e)
        {
            GetTableDeleteMultiRow();
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            InsertNewRow();
        }
        #endregion
    }
}