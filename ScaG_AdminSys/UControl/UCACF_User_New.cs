using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_AdminSys.Class;
using CNY_AdminSys.Info;
using CNY_AdminSys.Properties;
using CNY_AdminSys.WForm;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition;
using CellValueChangedEventArgs = DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs;
using DrawFocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle;
using FixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using FocusedColumnChangedEventArgs = DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs;
using PopupMenuShowingEventArgs = DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs;
using ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility;
using ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode;
using SummaryItemType = DevExpress.Data.SummaryItemType;

namespace CNY_AdminSys.UControl
{
    public partial class UCACF_User_New : XtraUserControl
    {
        #region "Property"
        readonly Inf_User _inf = new Inf_User();
        readonly Cls_AdminCodeFile _cls = new Cls_AdminCodeFile();
        private bool _disableEventWhenLoad = false;
        private WaitDialogForm _dlg;
        private string _strPath = "";
        private Int64 _userId = 0;
       // private Int64 _userIdGetNext = 0;
        PermissionFormInfo _perInfo;

        //================
        //private Cls_UserReceipient lUserReceipient;
        //Dictionary<Int64, Cls_UserReceipient> _dicReceipient = new Dictionary<Int64, Cls_UserReceipient>();
        private string _userName = "";
        private Int64 _CNYSYS11PK = 0;
        private Int64 _CNYSYS11APK = 0;
        private string _WorkFunCode = "";
        //private Int64 _CNYSYS11APK = 0;
        private DataTable _dtUser;
        private DataTable _dtRule;
        Dictionary<string, DataTable> _dicRule = new Dictionary<string, DataTable>();
        Dictionary<Int64, DataTable> _dicUser = new Dictionary<Int64, DataTable>();
        Dictionary<Int64, DataTable> _dicStatus = new Dictionary<Int64, DataTable>();

        readonly List<string> lMemberDel = new List<string>();
        readonly List<Int64> lPkFunctionDel = new List<long>();
        readonly List<Int64> lPkReceipientDel = new List<long>();
        readonly List<Int64> lPkStatusDel = new List<long>();

        //Adjust 12/03/2020
        private readonly RepositoryItemTextEdit _repositoryItemTextCode;
        private readonly RepositoryItemCheckEdit _repositoryItemCheckEdit;
        private string _option = "";
        private CheckDataChangeOnControl<Int64, Int64, Int64, Int64> _chkCtrl = new CheckDataChangeOnControl<Int64, Int64, Int64, Int64>();
        private Dictionary<string, DataTable> _dicTlDetail = new Dictionary<string, DataTable>();
        private bool _CheckKeyDownGv;
        private bool _checkKeyDownTl = false;
        private bool _performEditValueChangeEvent = true;
        private DXMenuItem[] _menuModule = null;
        private List<string> _lDelModule = new List<string>();
        #endregion

        #region "Contructor"
        public UCACF_User_New()
        {
            InitializeComponent();
            _perInfo = ProcessGeneral.GetPermissionByFormCode("UCACF_User_New");
            tgShowPass.Enabled = DeclareSystem.SysUserName.ToUpper().Trim() == "ADMIN";

            ProcessGeneral.SetDateEditFormat(dtBrithday, ConstSystem.SysDateFormat, false, true, DefaultBoolean.Default);
            GridViewCustomInit();
            ProcessGeneral.LoadSearchLookup(searchPositions, _inf.ListUser_CBPositions(), "PositionsCode", "PositionsCode");
            ProcessGeneral.LoadSearchLookup(searchDepartMent, _inf.ListUser_CBDepartment(), "DepartmentCode", "DepartmentCode");

            //this.Load += Control_Load;
            searchPositions.EditValueChanged += searchPositions_EditValueChanged;
            searchDepartMent.EditValueChanged += searchDepartMent_EditValueChanged;

            pictureBoxIcon.DoubleClick += pictureBoxIcon_DoubleClick;

            txtPassword.Enabled = false;
            txtPositions.Enabled = false;
            txtDepartMent.Enabled = false;

            txtCreateDate.Enabled = false;
            txtCreateBy.Enabled = false;
            txtUpdateBy.Enabled = false;
            txtUpdateDate.Enabled = false;
            tgShowPass.EditValueChanged += TgShowPass_EditValueChanged;

            //===============================
            _dtUser = new DataTable();
            _dtRule = new DataTable();
            GridViewTestCustomInit(gcFunction, gvFunction);
            GridViewTestCustomInit(gcReceipient, gvReceipient);
            GridViewTestCustomInit(gcStatus, gvStatus);
            bool isDrop = DeclareSystem.SysUserName.ToUpper() == "ADMIN" || _perInfo.PerIns || _perInfo.PerUpd || _perInfo.PerDel;

            gcMember.Enabled = isDrop;
            gcFunction.Enabled = isDrop;
            gcReceipient.Enabled = isDrop;
            gcStatus.Enabled = isDrop;
            btnAddMember.Enabled = isDrop;
            btnRemoveMember.Enabled = isDrop;
            btnAddFunction.Enabled = isDrop;
            btnRemoveFunction.Enabled = isDrop;
            btnAddReceipient.Enabled = isDrop;
            btnRemoveReceipient.Enabled = isDrop;
            btnAddStatus.Enabled = isDrop;
            btnRemoveStatus.Enabled = isDrop;

            btnAddMember.Click += btnAddMember_Click;
            btnRemoveMember.Click += btnRemoveMember_Click;
            btnAddFunction.Click += btnAddFunction_Click;
            btnRemoveFunction.Click += btnRemoveFunction_Click;
            btnAddReceipient.Click += btnAddReceipient_Click;
            btnRemoveReceipient.Click += btnRemoveReceipient_Click;
            btnAddStatus.Click += btnAddStatus_Click;
            btnRemoveStatus.Click += btnRemoveStatus_Click;

            #region "Adjust Tab Extension"
            txtUserName.Properties.CharacterCasing = CharacterCasing.Upper;
            txtUserName.Properties.MaxLength = 20;
            txtFullName.Properties.MaxLength = 50;
            txtEmail.Properties.MaxLength = 50;
            _repositoryItemTextCode = new RepositoryItemTextEdit
            {
                AutoHeight = false,
            };
            _repositoryItemTextCode.ContextImageOptions.Image = new Bitmap(Resources.chartsshowlegend_32x321, new Size(17, 17));
            _repositoryItemCheckEdit = new RepositoryItemCheckEdit()
            {
                ValueChecked = 1,
                ValueUnchecked = 0,
                ValueGrayed = 2
            };
            InitGridViewMain(gcMain, gvMain);
            InitTreelist();
            InitMenuItem();
            btnAddRowMain.Click += BtnAddRowMain_Click;
            btnDeleteRowMain.Click += BtnDeleteRowMain_Click;
            xtraTabUser.SelectedPageChanged += XtraTabUser_SelectedPageChanged;
            btnCheckAll.Click += btnCheckAll_Click;
            FocusNextControl();
            #endregion
        }

        #region "Adjust Tab Extension"
        private void FocusNextControl()
        {
            //List<Control> lControlTab0 = new List<Control>
            //{
            //    txtUserName, txtPassword, dtBrithday, rdSex, txtFullName, txtEmail, searchDepartMent, searchPositions, tgShowPass, chkActive,
            //    txtCreateBy, txtCreateDate, txtUpdateBy, txtUpdateDate, pictureBoxIcon, gcMember, btnAddMember, btnRemoveMember,
            //};
            //List<Control> lControlTab1 = new List<Control>
            //{
            //    gcFunction, btnAddFunction, btnRemoveFunction, gcStatus, btnAddStatus,
            //    btnRemoveStatus, gcReceipient, btnAddReceipient, btnRemoveReceipient,
            //};
            //List<Control> lControlTab2 = new List<Control>
            //{
            //    gcMain, btnAddRowMain, btnDeleteRowMain,
            //    btnCheckAll, tlDetail,
            //};
            //FocusNextControl focusNext = new FocusNextControl(xtraTabUser, lControlTab0, lControlTab1, lControlTab2);
            //focusNext.AddTabIndexControl();
        }

        private void LoadControl()
        {
            _chkCtrl = new CheckDataChangeOnControl<Int64, Int64, Int64, Int64>();
            _chkCtrl.LoadControl(txtUserName, txtPassword, dtBrithday, rdSex, txtFullName, txtEmail, searchDepartMent, searchPositions,
                chkActive, pictureBoxIcon, gcMember, gcFunction, gcStatus, gcReceipient, gcMain, tlDetail);
            //_chkCtrl.LoadListDel(_lDelMotionDetail, _lDelMotionGroup, _lDelDocument);
            //_chkCtrl.LoadListAtt(_dicAttValue);
            //_chkCtrl.LoadListTable(_dicTableProduction);
            //_chkCtrl.LoadListTable(_dicTableOP);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UseControlInput("Load");
            //_disableEventWhenLoad = true;
            //_disableEventWhenLoad = false;

            splitCCMain.SplitterPosition = 350; // độ rộng hiển thị Function
            splitCCMain.FixedPanel = SplitFixedPanel.Panel1; // cố định độ rộng hiển thị Function

            //Adjust 12/03/2020
            //_dlg = new WaitDialogForm();
            //LoadDataGvMain();
            //_dlg.Close();
        }
        #endregion
        #endregion

        #region "Tab Extension"
        #region "Process GridViewMain"
        private DataTable TableLoadDataGridViewModuleTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ModuleCode", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }

        private void LoadDataGvMain()
        {
            _dicTlDetail.Clear();
            _lDelModule.Clear();
            DataTable dtMain = new DataTable();
            if (_userId == 0) //ADD
            {
                dtMain = TableLoadDataGridViewModuleTemp();
            }
            if (_userId > 0) //EDIT
            {
                dtMain = _inf.LoadDataGridView_Module(_userId);
            }

            gvMain.BeginUpdate();
            gcMain.DataSource = null;
            gvMain.Columns.Clear();
            gcMain.DataSource = dtMain;

            Dictionary<string, bool> dicCol = new Dictionary<string, bool>
            {
                {"ModuleCode", true },
                {"Description", true},
                {"RowState", false},
            };
            gvMain.VisibleAndSortGridColumn(dicCol);

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["ModuleCode"], "Module Code", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
            gvMain.Columns["ModuleCode"].AppearanceCell.Options.UseTextOptions = true;
            gvMain.Columns["ModuleCode"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["ModuleCode"].ColumnEdit = _repositoryItemTextCode;
            gvMain.Columns["ModuleCode"].SummaryItem.SummaryType = SummaryItemType.Count;
            gvMain.Columns["ModuleCode"].SummaryItem.DisplayFormat = @"Total : ";

            ProcessGeneral.SetGridColumnHeader(gvMain.Columns["Description"], "Description", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
            gvMain.Columns["Description"].AppearanceCell.Options.UseTextOptions = true;
            gvMain.Columns["Description"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["Description"].SummaryItem.SummaryType = SummaryItemType.Count;
            gvMain.Columns["Description"].SummaryItem.DisplayFormat = @"{0:N0} (item)";

            gvMain.BestFitColumns();
            gcMain.ForceInitialize();
            gvMain.EndUpdate();

            gvMain.SetFocusedRowOnGrid(gvMain.FocusedRowHandle);
            int rH = gvMain.FocusedRowHandle;
            Int64 cny00004Pk = ProcessGeneral.GetSafeInt64(txtPK.EditValue);
            string moduleCode = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rH, "ModuleCode"));

            DataTable dtTlDetail = _inf.LoadDataTreeList_Responsibility(-1, "XX"); //purchaserPk, moduleCode
            if (dtTlDetail.Rows.Count > 0)
            {
                _dicTlDetail = dtTlDetail.AsEnumerable().GroupBy(p => p.Field<string>("CNYSYS01Code")).Select(
                    s => new
                    {
                        CNYSYS01Code = s.Key,
                        TbSource = s.Select(n => new
                        {
                            isCheck = n.Field<int>("isCheck"),
                            PK = n.Field<Int64>("PK"),
                            Code = n.Field<string>("Code"),
                            Name = n.Field<string>("Name"),
                            Description = n.Field<string>("Description"),
                            PARENTPK = n.Field<Int64>("PARENTPK"),
                            CNY00004PK = n.Field<Int64>("CNY00004PK"),
                            CNYSYS01Code = n.Field<string>("CNYSYS01Code"),
                            RowState = n.Field<string>("RowState"),
                        }).CopyToDataTableNew()
                    }).ToDictionary(s => s.CNYSYS01Code, s => s.TbSource);
            }

            LoadDataTreeList(rH, cny00004Pk);
        }

        private void InitGridViewMain(GridControl gc, GridView gv)
        {
            gc.UseEmbeddedNavigator = true;

            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Append.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gv.OptionsBehavior.Editable = false;

            gv.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gv.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gv.OptionsCustomization.AllowColumnMoving = false;
            gv.OptionsCustomization.AllowQuickHideColumns = true;

            gv.OptionsCustomization.AllowSort = false; //true

            gv.OptionsCustomization.AllowFilter = false; //true

            gv.OptionsView.AllowCellMerge = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowIndicator = true;
            gv.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gv.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gv.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gv.OptionsView.ShowAutoFilterRow = false; //true
            gv.HorzScrollVisibility = ScrollVisibility.Auto;
            gv.OptionsView.ColumnAutoWidth = false;

            gv.OptionsMenu.ShowAutoFilterRowItem = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gv.OptionsNavigation.AutoFocusNewRow = true;
            gv.OptionsNavigation.UseTabKey = true;

            gv.OptionsSelection.MultiSelect = false; //true
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gv.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gv.OptionsSelection.EnableAppearanceFocusedRow = false;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;

            gv.OptionsView.EnableAppearanceEvenRow = false;
            gv.OptionsView.EnableAppearanceOddRow = false;
            gv.OptionsView.ShowFooter = true;

            gv.OptionsHint.ShowFooterHints = false;
            gv.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gv.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gv.OptionsFind.AllowFindPanel = false; //true

            gv.OptionsFind.AlwaysVisible = false;

            gv.OptionsFind.ShowCloseButton = true;
            gv.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gv)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsDrawFilter = true,
            };

            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.CustomDrawFooter += GvMain_CustomDrawFooter;
            gvMain.CustomDrawFooterCell += GvMain_CustomDrawFooterCell;

            gvMain.FocusedRowChanged += GvMain_FocusedRowChanged;
            gvMain.FocusedColumnChanged += GvMain_FocusedColumnChanged;
            gcMain.Paint += GcMain_Paint;
            gvMain.LeftCoordChanged += GvMain_LeftCoordChanged;
            gvMain.TopRowChanged += GvMain_TopRowChanged;
            gvMain.MouseMove += GvMain_MouseMove;

            gvMain.PopupMenuShowing += GvMain_PopupMenuShowing;
            gcMain.KeyDown += GcMain_KeyDown;
            gcMain.EditorKeyDown += GcMain_EditorKeyDown;

            //gvMain.RowCellStyle += GvDetail_RowCellStyle;

            gc.ForceInitialize();
        }
        #endregion

        #region "Process TreeListDetail"
        private DataTable TableTreeListDetailTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("isCheck", typeof(int));
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("PARENTPK", typeof(Int64));
            dt.Columns.Add("CNY00004PK", typeof(Int64));
            dt.Columns.Add("CNYSYS01Code", typeof(string));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }

        private void LoadDataTreeList(int rowHandle, Int64 cny00004Pk)
        {
            try
            {
                if (_option == "EDIT")
                {
                    if (cny00004Pk <= 0) return;
                }
                DataTable dtS;
                if (rowHandle < 0)
                {
                    dtS = TableTreeListDetailTemp();
                }
                else
                {
                    string moduleCode = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(rowHandle, "ModuleCode"));
                    DataTable dtTreeList = _inf.LoadDataTreeList_Responsibility(cny00004Pk, moduleCode);
                    if (!_dicTlDetail.ContainsKey(moduleCode))
                    {
                        DataTable dtTreeListNew = dtTreeList.Clone();
                        foreach (DataRow dr in dtTreeList.Rows)
                        {
                            dr["CNYSYS01Code"] = moduleCode;
                            dtTreeListNew.ImportRow(dr);
                        }
                        dtTreeListNew.AcceptChanges();
                        _dicTlDetail.Add(moduleCode, dtTreeListNew);
                    }
                    dtS = _dicTlDetail[moduleCode];
                    //dtS.AcceptChanges();
                }

                tlDetail.BeginUpdate();
                tlDetail.DataSource = null;
                tlDetail.Columns.Clear();
                tlDetail.DataSource = dtS;
                tlDetail.KeyFieldName = "PK";
                tlDetail.ParentFieldName = "PARENTPK";

                ProcessGeneral.HideVisibleColumnsTreeList(tlDetail, false, "PK", "PARENTPK", "CNY00004PK", "CNYSYS01Code", "RowState");

                ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["isCheck"], "Select", false, HorzAlignment.Center, FixedStyle.None, "");
                tlDetail.Columns["isCheck"].AppearanceCell.Options.UseTextOptions = true;
                tlDetail.Columns["isCheck"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["Code"], "Code", false, HorzAlignment.Center, FixedStyle.None, "");
                tlDetail.Columns["Code"].AppearanceCell.Options.UseTextOptions = true;
                tlDetail.Columns["Code"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["Name"], "Name", false, HorzAlignment.Center, FixedStyle.None, "");
                tlDetail.Columns["Name"].AppearanceCell.Options.UseTextOptions = true;
                tlDetail.Columns["Name"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["Description"], "Description", false, HorzAlignment.Center, FixedStyle.None, "");
                tlDetail.Columns["Description"].AppearanceCell.Options.UseTextOptions = true;
                tlDetail.Columns["Description"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                tlDetail.Columns["CNY00004PK"].OptionsColumn.ShowInCustomizationForm = false;
                tlDetail.Columns["CNYSYS01Code"].OptionsColumn.ShowInCustomizationForm = false;
                tlDetail.Columns["RowState"].OptionsColumn.ShowInCustomizationForm = false;

                tlDetail.ClearColumnsFilter();
                tlDetail.SetAutoFilterValue(tlDetail.Columns["isCheck"], 1, AutoFilterCondition.Equals);

                //CreateColumnTreelist(tlDetail, "isCheck", "Check", 1);
                //CreateColumnTreelist(tlDetail, "PK", "PK", -1);
                //CreateColumnTreelist(tlDetail, "Code", "Code", 2);
                //CreateColumnTreelist(tlDetail, "Name", "Name", 3);
                //CreateColumnTreelist(tlDetail, "Description", "Description", 4);
                //CreateColumnTreelist(tlDetail, "PARENTPK", "PARENTPK", -1);
                //CreateColumnTreelist(tlDetail, "CNY00004PK", "CNY00004PK", -1);
                //CreateColumnTreelist(tlDetail, "CNYSYS01Code", "CNYSYS01Code", -1);
                //CreateColumnTreelist(tlDetail, "RowState", "RowState", -1);

                //tlDetail.Columns["isCheck"].ColumnEdit = _repositoryItemCheckEdit;

                tlDetail.ExpandAll();
                tlDetail.BestFitColumns();
                tlDetail.ForceInitialize();
                tlDetail.EndUpdate();

                UpdateCheckAllNodeTreeList(tlDetail);
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCheckAllNodeTreeList(TreeList tl)
        {
            int indicatorWidth = tl.IndicatorWidth;
            int topIndex = 0;
            int checkCount = 0;
            tl.BeginUpdate();
            tl.LockReloadNodes();

            foreach (TreeListNode node in tl.Nodes)
            {
                if (ProcessGeneral.GetSafeInt(node.GetValue("isCheck")) == 1)
                {
                    node.Checked = true;
                    checkCount++;
                }

                if (node.Nodes.Count > 0)
                {
                    UpdateCheckAllChildNode(node, ref checkCount);
                }
            }
            tl.UnlockReloadNodes();
            int indicatorWidthNew = tl.IndicatorWidth;
            if (indicatorWidth < indicatorWidthNew)
            {
                indicatorWidth = indicatorWidthNew;
            }
            tl.IndicatorWidth = indicatorWidth;
            tl.EndUpdate();
            int nodeCount = tl.AllNodesCount;


            if (nodeCount == 0 || nodeCount != checkCount)
            {
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }
            else
            {
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
            }

            tl.Tag = checkCount;
            tl.TopVisibleNodeIndex = topIndex;
        }
        private void UpdateCheckAllChildNode(TreeListNode tlNode, ref int checkCount)
        {
            foreach (TreeListNode node in tlNode.Nodes)
            {
                if (ProcessGeneral.GetSafeInt(node.GetValue("isCheck")) == 1)
                {
                    node.Checked = true;
                    checkCount++;
                }
                if (node.Nodes.Count > 0)
                {
                    UpdateCheckAllChildNode(node, ref checkCount);
                }
            }
        }

        private void SetCheckAllNodeTreeList(TreeList tl, bool status)
        {
            int indicatorWidth = tl.IndicatorWidth;
            int topIndex = tl.TopVisibleNodeIndex;
            tl.BeginUpdate();
            tl.LockReloadNodes();
            _performEditValueChangeEvent = false;
            foreach (TreeListNode node in tl.Nodes)
            {
                if (node.Checked != status)
                {
                    SetCheckNode(node, status, false);
                }

                if (node.Nodes.Count > 0)
                {
                    SetCheckAllChildNode(node, status);
                }
            }
            _performEditValueChangeEvent = true;
            tl.UnlockReloadNodes();
            int indicatorWidthNew = tl.IndicatorWidth;
            if (indicatorWidth < indicatorWidthNew)
            {
                indicatorWidth = indicatorWidthNew;
            }
            tl.IndicatorWidth = indicatorWidth;
            tl.EndUpdate();

            tl.Tag = status ? tl.AllNodesCount : 0;
            tl.TopVisibleNodeIndex = topIndex;
        }
        private void SetCheckAllChildNode(TreeListNode tlNode, bool status)
        {
            foreach (TreeListNode node in tlNode.Nodes)
            {
                if (node.Checked != status)
                {
                    SetCheckNode(node, status, false);

                }
                if (node.Nodes.Count > 0)
                {
                    SetCheckAllChildNode(node, status);
                }
            }
        }

        private void SetCheckNode(TreeListNode node, bool status, bool isTree)
        {
            if (!isTree)
                node.Checked = status;
            node.SetValue("isCheck", status ? 1 : 0);
            string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState"));
            if (rowState == DataStatus.Unchange.ToString())
            {
                node.SetValue("RowState", DataStatus.Update.ToString());
            }
        }

        private void UpdateStatusChildNode(TreeList tl, TreeListNode node)
        {
            int currentCheckCount = ProcessGeneral.GetSafeInt(tl.Tag);
            int childNodeCount = 0;
            bool isCheck = node.Checked;
            int topIndex = tl.TopVisibleNodeIndex;
            tl.BeginUpdate();
            tl.LockReloadNodes();
            _performEditValueChangeEvent = false;
            UpdateStatusChildNode(node, isCheck, ref childNodeCount);
            childNodeCount++;
            SetCheckNode(node, isCheck, true);

            UpdateCheckParent(node, ref currentCheckCount);

            _performEditValueChangeEvent = true;

            tl.UnlockReloadNodes();
            tl.EndUpdate();

            int nodeCount = tl.AllNodesCount;


            if (isCheck)
            {
                currentCheckCount = currentCheckCount + childNodeCount;
            }
            else
            {
                currentCheckCount = currentCheckCount - childNodeCount;
            }


            tl.Tag = currentCheckCount;

            if (!isCheck || currentCheckCount != nodeCount)
            {
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");

            }
            else
            {
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
            }

            tl.TopVisibleNodeIndex = topIndex;
        }


        private void UpdateCheckParent(TreeListNode node, ref int currentCheckCount)
        {
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null) return;

            bool isCheck = parentNode.Checked;
            bool currentCheck = ProcessGeneral.GetSafeInt(parentNode.GetValue("isCheck")) == 1;
            if (currentCheck)
            {
                if (!isCheck)
                {
                    currentCheckCount = currentCheckCount - 1;
                }
            }
            else
            {
                if (isCheck)
                {
                    currentCheckCount = currentCheckCount + 1;
                }
            }


            parentNode.SetValue("isCheck", isCheck ? 1 : 0);
            string rowState = ProcessGeneral.GetSafeString(parentNode.GetValue("RowState"));
            if (rowState == DataStatus.Unchange.ToString())
            {
                parentNode.SetValue("RowState", DataStatus.Update.ToString());
            }

            UpdateCheckParent(parentNode, ref currentCheckCount);
        }

        private void UpdateStatusChildNode(TreeListNode tlNode, bool isCheck, ref int childNodeCount)
        {
            foreach (TreeListNode node in tlNode.Nodes)
            {
                bool currentCheck = ProcessGeneral.GetSafeInt(node.GetValue("isCheck")) == 1;
                if (currentCheck != isCheck)
                {
                    childNodeCount++;
                    SetCheckNode(node, isCheck, true);
                }

                if (node.Nodes.Count > 0)
                {
                    UpdateStatusChildNode(node, isCheck, ref childNodeCount);
                }
            }
        }

        private void SetInfoCheckButton(bool enable, Image image, string text)
        {
            btnCheckAll.Image = image;
            btnCheckAll.ToolTip = text;
            btnCheckAll.Enabled = enable;
        }

        private void InitTreelist()
        {
            tlDetail.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            tlDetail.OptionsBehavior.EnableFiltering = true;
            tlDetail.OptionsFilter.AllowFilterEditor = true;
            tlDetail.OptionsFilter.AllowMRUFilterList = true;
            tlDetail.OptionsFilter.AllowColumnMRUFilterList = true;
            tlDetail.OptionsFilter.FilterMode = FilterMode.Smart;
            tlDetail.OptionsFind.AllowFindPanel = false;
            tlDetail.OptionsFind.AlwaysVisible = false;
            tlDetail.OptionsFind.ShowCloseButton = true;
            tlDetail.OptionsFind.HighlightFindResults = true;
            tlDetail.OptionsView.ShowAutoFilterRow = true; //false

            tlDetail.OptionsBehavior.Editable = true;
            tlDetail.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            tlDetail.OptionsView.ShowColumns = true;
            tlDetail.OptionsView.ShowHorzLines = true;
            tlDetail.OptionsView.ShowVertLines = true;
            tlDetail.OptionsView.ShowIndicator = true;
            tlDetail.OptionsView.AutoWidth = false;
            tlDetail.OptionsView.EnableAppearanceEvenRow = false;
            tlDetail.OptionsView.EnableAppearanceOddRow = false;
            tlDetail.StateImageList = GetImageListDisplayTreeView();
            tlDetail.OptionsBehavior.AutoChangeParent = false;
            tlDetail.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            tlDetail.OptionsBehavior.AutoNodeHeight = true;

            tlDetail.OptionsView.ShowSummaryFooter = false;

            tlDetail.OptionsBehavior.CloseEditorOnLostFocus = true;
            tlDetail.OptionsBehavior.KeepSelectedOnClick = true;
            tlDetail.OptionsBehavior.ShowEditorOnMouseUp = true;
            tlDetail.OptionsBehavior.SmartMouseHover = false;
            tlDetail.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            tlDetail.OptionsBehavior.AllowRecursiveNodeChecking = true;
            tlDetail.OptionsView.ShowCheckBoxes = true;

            tlDetail.OptionsCustomization.AllowColumnResizing = true;
            tlDetail.OptionsCustomization.AllowQuickHideColumns = false;
            tlDetail.OptionsCustomization.AllowSort = false;
            tlDetail.OptionsCustomization.AllowFilter = true;
            tlDetail.OptionsCustomization.AllowColumnMoving = false;

            tlDetail.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(tlDetail, true)
            {
                AllowSort = false,
                FilterShowChild = true,
                AllowColumnsChooser = false,
                ShowAutoFilerMenu = true
            };

            tlDetail.CustomDrawNodeIndicator += TlDetail_CustomDrawNodeIndicator;
            tlDetail.ShowingEditor += TlDetail_ShowingEditor;
            tlDetail.NodeCellStyle += TlDetail_NodeCellStyle;
            tlDetail.GetStateImage += TlDetail_GetStateImage;

            tlDetail.CellValueChanged += TlDetail_CellValueChanged;
            tlDetail.AfterCheckNode += TlDetail_AfterCheckNode;
            tlDetail.GetNodeDisplayValue += TlDetail_GetNodeDisplayValue;

            tlDetail.KeyDown += TlDetail_KeyDown;
            tlDetail.EditorKeyDown += TlDetail_EditorKeyDown;
        }
        #endregion

        #region "GridViewMain Events"
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
                ShowListModule();
                return;
            }
            #endregion

            #region "F8 Key
            if (e.KeyData == Keys.F8)
            {
                GetTableDeleteMultiModule();
                return;
            }
            #endregion
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
                if (!gv.IsDataRow(e.HitInfo.RowHandle)) return;
                gv.FocusedRowHandle = e.HitInfo.RowHandle;
                foreach (DXMenuItem item in _menuModule)
                {
                    e.Menu.Items.Add(item);
                }
            }
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

        private void GvMain_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
            Int64 cny00004Pk = ProcessGeneral.GetSafeInt64(txtPK.EditValue);
            LoadDataTreeList(gv.FocusedRowHandle, cny00004Pk);
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
            e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }

        private void GvMain_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            //if (!_isShowFooter) return;
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
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
            if (e.Column.FieldName == "ModuleCode")
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

            LinearGradientBrush backBrush;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "RowState"));

            if (rowState == DataStatus.Insert.ToString())
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
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

        #endregion

        #region TreeList Events and Function TreeList
        private void TlDetail_AfterCheckNode(object sender, NodeEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            int indicatorWidth = tl.IndicatorWidth;
            UpdateStatusChildNode(tl, node);
            int indicatorWidthNew = tl.IndicatorWidth;
            if (indicatorWidth < indicatorWidthNew)
            {
                indicatorWidth = indicatorWidthNew;
            }
            tl.IndicatorWidth = indicatorWidth;
        }

        private void TlDetail_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDownTl)
            {
                TlDetail_KeyDown(sender, e);
            }
            _checkKeyDownTl = false;
        }

        private void TlDetail_KeyDown(object sender, KeyEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            _checkKeyDownTl = true;
            TreeListNode node = tl.FocusedNode;

            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            // string fieldName = col.FieldName;
            //  int visibleIndex = col.VisibleIndex;

            #region "Process F9 Key"
            if (e.KeyCode == Keys.F9)
            {
                tl.BeginUpdate();
                tl.ExpandCollapseNodeSelected(true);
                tl.EndUpdate();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion

            #region "Process F10 Key"
            if (e.KeyCode == Keys.F10)
            {
                tl.BeginUpdate();
                tl.ExpandCollapseNodeSelected(false);
                tl.EndUpdate();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion

            #region "Process F11 Key"
            if (e.KeyCode == Keys.F11)
            {
                tl.ExpandAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion

            #region "Process F12 Key"
            if (e.KeyCode == Keys.F12)
            {
                tl.CollapseAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            #endregion
        }

        private void TlDetail_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {
            if (e.Column.FieldName == "isCheck")
            {
                e.Value = "";
            }
        }

        private void TlDetail_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (!_performEditValueChangeEvent) return;
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;

            string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState"));
            if (rowState == DataStatus.Unchange.ToString())
            {
                node.SetValue("RowState", DataStatus.Update.ToString());
            }
        }

        private void TlDetail_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            e.NodeImageIndex = e.Node.Level < 7 ? e.Node.Level : 7;
        }

        private void TlDetail_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            if (!col.Visible) return;

            if (node == tl.Nodes.AutoFilterNode) return;

            e.Appearance.GradientMode = LinearGradientMode.Vertical;
            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;

            if (e.Node.HasChildren)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void TlDetail_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;

            LinearGradientBrush backBrush;
            string rowState = ProcessGeneral.GetSafeString(e.Node.GetValue("RowState"));


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
            if (tl.Selection.Contains(e.Node))
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = e.Node.HasChildren ? Color.DarkMagenta : Color.DarkOrchid;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            string value = (tl.GetVisibleIndexByNode(e.Node) + 1).ToString().Trim();
            e.Graphics.DrawString(value, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache),
                e.Bounds, e.Appearance.GetStringFormat());
            e.ImageIndex = -1;
            e.Handled = true;
        }

        private void TlDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            // if(tl.FocusedColumn.FieldName!= "isCheck")
            e.Cancel = true;
        }

        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.project_16x16);
            imgLt.Images.Add(Resources.properties_16x16);
            imgLt.Images.Add(Resources.download_16x16);
            imgLt.Images.Add(Resources.fill_16x16);
            imgLt.Images.Add(Resources.group_16x16);
            imgLt.Images.Add(Resources.add_16x16);
            imgLt.Images.Add(Resources.convert_16x16);
            imgLt.Images.Add(Resources.apply_16x16);
            return imgLt;
        }
        #endregion

        #region "Process MenuItem, Control Events"
        private void InitMenuItem()
        {
            DXMenuItem itemModuleInsert = new DXMenuItem("Add New Module (Insert Key)", BtnAddRowMain_Click, Resources.insertrows_16);
            DXMenuItem itemModuleDelete = new DXMenuItem("Delete Selected Module (F8 Key)", BtnDeleteRowMain_Click, Resources.deletesheetrows_16);
            _menuModule = new DXMenuItem[] { itemModuleInsert, itemModuleDelete };
        }

        private void BtnAddRowMain_Click(object sender, EventArgs e)
        {
            ShowListModule();
        }

        private void BtnDeleteRowMain_Click(object sender, EventArgs e)
        {
            GetTableDeleteMultiModule();
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            if (tlDetail.AllNodesCount <= 0) return;
            if (btnCheckAll.ToolTip == @"Check All")
            {
                SetCheckAllNodeTreeList(tlDetail, true);
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
            }
            else
            {
                SetCheckAllNodeTreeList(tlDetail, false);
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }
        }

        private void XtraTabUser_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabUser.SelectedTabPage == TabUser)
            {
                if (_option.ToUpper() == "ADD")
                {
                    txtUserName.Select();
                }
                else
                {
                    txtFullName.Select();
                }
            }
        }
        #endregion

        #region "Function GridViewMain"
        /// <summary>
        /// 1 is CheckAll, 0 is UnCheckAll
        /// </summary>
        /// <param name="type"></param>
        private void CheckType(int type)
        {
            Int64 cny00004Pk = ProcessGeneral.GetSafeInt64(txtPK.EditValue);
            DataTable dtSTreeList = new DataTable();
            DataTable dtS = gcMain.DataSource as DataTable;
            if (dtS == null) return;
            int[] arrRow = gvMain.GetSelectedRows().OrderByDescending(p => p).ToArray();
            foreach (int row in arrRow)
            {
                string moduleCodeCheck = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(row, "ModuleCode"));
                DataTable dtTreeListCheckAll = _inf.LoadDataTreeList_Responsibility(cny00004Pk, moduleCodeCheck);
                if (_dicTlDetail.ContainsKey(moduleCodeCheck))
                {
                    _dicTlDetail.Remove(moduleCodeCheck);
                }
                DataTable dtTreeListCheckAllNew = dtTreeListCheckAll.Clone();
                foreach (DataRow dr in dtTreeListCheckAll.Rows)
                {
                    dr["isCheck"] = type;
                    dr["CNYSYS01Code"] = moduleCodeCheck;
                    dr["RowState"] = DataStatus.Update.ToString();
                    dtTreeListCheckAllNew.ImportRow(dr);
                }
                dtTreeListCheckAllNew.AcceptChanges();
                _dicTlDetail.Add(moduleCodeCheck, dtTreeListCheckAllNew);
                dtSTreeList = _dicTlDetail[moduleCodeCheck];
            }

            tlDetail.BeginUpdate();
            tlDetail.DataSource = null;
            tlDetail.Columns.Clear();
            tlDetail.DataSource = dtSTreeList;
            tlDetail.KeyFieldName = "PK";
            tlDetail.ParentFieldName = "PARENTPK";

            ProcessGeneral.HideVisibleColumnsTreeList(tlDetail, false, "PK", "PARENTPK", "CNY00004PK", "CNYSYS01Code", "RowState");

            ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["isCheck"], "Check", false, HorzAlignment.Center, FixedStyle.None, "");
            tlDetail.Columns["isCheck"].AppearanceCell.Options.UseTextOptions = true;
            tlDetail.Columns["isCheck"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["Code"], "Code", false, HorzAlignment.Center, FixedStyle.None, "");
            tlDetail.Columns["Code"].AppearanceCell.Options.UseTextOptions = true;
            tlDetail.Columns["Code"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["Name"], "Name", false, HorzAlignment.Center, FixedStyle.None, "");
            tlDetail.Columns["Name"].AppearanceCell.Options.UseTextOptions = true;
            tlDetail.Columns["Name"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            ProcessGeneral.SetTreeListColumnHeader(tlDetail.Columns["Description"], "Description", false, HorzAlignment.Center, FixedStyle.None, "");
            tlDetail.Columns["Description"].AppearanceCell.Options.UseTextOptions = true;
            tlDetail.Columns["Description"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

            tlDetail.ExpandAll();
            tlDetail.BestFitColumns();
            tlDetail.ForceInitialize();
            tlDetail.EndUpdate();
        }

        private DataTable TableLoadModuleCodeTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ModuleCode", typeof(string));
            return dt;
        }

        private void ShowListModule()
        {
            if (!_perInfo.PerIns)
            {
                XtraMessageBox.Show("You are not authorized to perform the function add new data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtS = gcMain.DataSource as DataTable;
            if (dtS == null) return;
            var q = dtS.AsEnumerable().Select(p => new
            {
                ModuleCode = ProcessGeneral.GetSafeString(p["ModuleCode"])
            }).ToList();
            DataTable dtCondition = q.Any() ? q.CopyToDataTableNew() : TableLoadModuleCodeTemp();
            DataTable dtModuleF4 = _inf.LoadModuleF4(dtCondition);
            if (dtModuleF4.Rows.Count <= 0)
            {
                XtraMessageBox.Show("You have selected all the modules in the system", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dtSourceModule = gcMain.DataSource as DataTable;

            #region "Init Column"
            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Module Code",
                    FieldName = "ModuleCode",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 1,
                    SummayType = SummaryItemType.Count,
                    SummaryFormatString = "Total :",
                    SummaryHorzAlign = HorzAlignment.Center,
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Description",
                    FieldName = "Description",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 1,
                    SummayType = SummaryItemType.Count,
                    SummaryFormatString = "{0:N0} (item)",
                    SummaryHorzAlign = HorzAlignment.Center,
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Index",
                    FieldName = "Index",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 1,
                    SummayType = SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center,
                },
            };

            #endregion

            var f = new FrmTransferData
            {
                DtSource = dtModuleF4,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = true,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(300, 550),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Modules/Functions List",
                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = false,
                IsShowFooter = true,
                IsShowAutoFilterRow = true,
                MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect,
                IsAddColumnSelected = true,
            };
            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                foreach (DataRow dr in lDr)
                {
                    DataRow drMd = dtSourceModule.NewRow();
                    drMd["ModuleCode"] = dr["ModuleCode"];
                    drMd["Description"] = dr["Description"];
                    drMd["RowState"] = DataStatus.Insert.ToString();
                    dtSourceModule.Rows.Add(drMd);
                    dtSourceModule.AcceptChanges();
                }
            };
            f.ShowDialog();
            gvMain.BestFitColumns();
        }

        private void GetTableDeleteMultiModule()
        {
            if (!_perInfo.PerDel)
            {
                XtraMessageBox.Show("You are not authorized to perform the function delete data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataTable dtS = gcMain.DataSource as DataTable;
            if (dtS == null) return;
            int[] arrRow = gvMain.GetSelectedRows().OrderByDescending(p => p).ToArray();
            if (arrRow.Length <= 0)
            {
                XtraMessageBox.Show("No rows selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            DialogResult dlRs = XtraMessageBox.Show("Do you want to delete the selected modules?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlRs != DialogResult.Yes) return;

            foreach (int row in arrRow)
            {
                string rowState = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(row, "RowState"));
                if (rowState != DataStatus.Insert.ToString())
                {
                    string moduleCode = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(row, "ModuleCode"));
                    _lDelModule.Add(moduleCode);
                }
                DataRow drDel = gvMain.GetDataRow(row);
                dtS.Rows.Remove(drDel);
            }

            dtS.AcceptChanges();
            gvMain.BestFitColumns();
        }

        private void DeleteModule(Int64 cny00004Pk)
        {
            var qDel = _lDelModule.Where(p => !string.IsNullOrEmpty(p))
                .Select(p => string.Format("'{0}'", p.ToString())).Distinct().ToList();
            if (qDel.Any())
            {
                string strModule = string.Join(",", qDel);
                if (!string.IsNullOrEmpty(strModule))
                {
                    _inf.DeleteModule(cny00004Pk, strModule);
                    _lDelModule.Clear();
                }
            }
        }

        private DataTable Format_Table(DataTable dt)
        {
            var abc = dt.AsEnumerable().Where(x => ProcessGeneral.GetSafeInt64(x["PARENTPK"]) == 0).Select(x => x).CopyToDataTable();
            for (int i = 0; i < abc.Rows.Count; i++)
            {
                Int64 a = ProcessGeneral.GetSafeInt64(abc.Rows[i]["PK"]);

                Int64 c1 = dt.AsEnumerable().Count(x => ProcessGeneral.GetSafeInt64(x["PARENTPK"]) == a);
                Int64 c2 = dt.AsEnumerable().Count(x => ProcessGeneral.GetSafeInt64(x["PARENTPK"]) == a && ProcessGeneral.GetSafeInt64(x["CNY00004PK"]) > 0);
                if (c1 != c2 && c2 > 0)
                {
                    dt.AsEnumerable().Where
                    (
                        n => ProcessGeneral.GetSafeInt64(n["PK"]) == a
                    ).ToList().ForEach(r =>
                    {
                        r["isCheck"] = 2;
                    });
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        private DataTable TableTreeListDetailSaveTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00004PK", typeof(Int64));
            dt.Columns.Add("CNY00001PK", typeof(Int64));
            dt.Columns.Add("Module", typeof(string));
            dt.Columns.Add("UserID", typeof(Int64));
            dt.Columns.Add("UserUpd", typeof(string));
            return dt;
        }

        private bool CheckDataBeforeSaveGvDetail(List<DataRow> ldrTlDetail, out bool isChange)
        {
            isChange = false;
            foreach (DataRow dr in ldrTlDetail)
            {
                if (!isChange && ProcessGeneral.GetSafeString(dr["RowState"]) != DataStatus.Unchange.ToString())
                {
                    isChange = true;
                }
            }
            return true;
        }

        private void SaveDataResponsibility(Int64 cny00004Pk, Int64 userId)
        {
            if (tlDetail.ActiveEditor != null)
                tlDetail.CloseEditor();
            var ldrDetail = _dicTlDetail.SelectMany(p => p.Value.AsEnumerable(), (m, n) => n).ToList();
            bool checkDataGridViewDetail = CheckDataBeforeSaveGvDetail(ldrDetail, out var isChangeGridViewDetail);
            if (!checkDataGridViewDetail)
            {
                return;
            }
            bool changedData = isChangeGridViewDetail || _lDelModule.Count > 0;
            if (!changedData)
            {
                //XtraMessageBox.Show("No data changed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtInsert = TableTreeListDetailSaveTemp();
            DataTable dtDelete = TableTreeListDetailSaveTemp();

            foreach (DataRow dr in ldrDetail)
            {
                int isCheck = ProcessGeneral.GetSafeInt(dr["isCheck"]);
                string moduleCheck = ProcessGeneral.GetSafeString(dr["CNYSYS01Code"]);
                string rowState = ProcessGeneral.GetSafeString(dr["RowState"]);

                if (moduleCheck == "XX" || rowState == DataStatus.Unchange.ToString()) continue;

                Cls_AdminCodeFile info = new Cls_AdminCodeFile
                {
                    Cny00001Pk = ProcessGeneral.GetSafeInt64(dr["PK"]),
                    Module = ProcessGeneral.GetSafeString(dr["CNYSYS01Code"]),
                    UserUpd = DeclareSystem.SysUserName.ToUpper()
                };

                if ((isCheck == 1 || isCheck == 2) && rowState == DataStatus.Update.ToString())
                {
                    dtInsert.Rows.Add(cny00004Pk, info.Cny00001Pk, info.Module, userId, info.UserUpd);
                }

                if (isCheck == 0 && rowState == DataStatus.Update.ToString())
                {
                    dtDelete.Rows.Add(cny00004Pk, info.Cny00001Pk, info.Module, userId, info.UserUpd);
                }
            }

            var lModule = dtDelete.AsEnumerable().Select(p => string.Format("'{0}'", p.Field<string>("Module"))).Distinct().ToList();
            string strModule = string.Join(",", lModule);
            var lDel = dtDelete.AsEnumerable().Select(p => p.Field<Int64>("CNY00001PK")).Distinct().ToList();
            string strCny00001Pk = string.Join(",", lDel);

            DeleteModule(cny00004Pk);

            if (dtInsert.Rows.Count > 0)
            {
                _inf.Insert_Responsibility(dtInsert);
            }
            if (dtDelete.Rows.Count > 0)
            {
                var dtDeleteNew = dtDelete.AsEnumerable().GroupBy(p => p.Field<string>("Module")).Select(s => new
                {
                    Module = s.Key,
                    StrCny00001Pk = string.Join(",", s.Select(p => p.Field<Int64>("CNY00001PK")))
                }).CopyToDataTableNew();
                foreach (DataRow dr in dtDeleteNew.Rows)
                {
                    string moduleNew = string.Format("'{0}'", ProcessGeneral.GetSafeString(dr["Module"]));
                    string strCny00001PkNew = ProcessGeneral.GetSafeString(dr["StrCny00001Pk"]);
                    _inf.Delete_ResponsibilityNEW(cny00004Pk, strCny00001PkNew, moduleNew);
                }
            }
        }

        private DataTable TableCountModuleTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Module", typeof(string));
            return dt;
        }

        private void SaveResponsibility(Int64 cny00004Pk)
        {
            if (tlDetail.ActiveEditor != null)
                tlDetail.CloseEditor();

            var ldrDetail = _dicTlDetail.SelectMany(p => p.Value.AsEnumerable(), (m, n) => n).ToList();
            DataTable dtResponsibility = TableTreeListDetailSaveTemp();

            foreach (DataRow dr in ldrDetail)
            {
                int isCheck = ProcessGeneral.GetSafeInt(dr["isCheck"]);
                string moduleCheck = ProcessGeneral.GetSafeString(dr["CNYSYS01Code"]);
                string rowState = ProcessGeneral.GetSafeString(dr["RowState"]);

                if (moduleCheck == "XX" || isCheck == 0) continue; //|| (isCheck == 1 && rowState == DataStatus.Unchange.ToString())

                Cls_AdminCodeFile info = new Cls_AdminCodeFile
                {
                    Cny00001Pk = ProcessGeneral.GetSafeInt64(dr["PK"]),
                    Module = ProcessGeneral.GetSafeString(dr["CNYSYS01Code"]),
                };
                dtResponsibility.Rows.Add(cny00004Pk, info.Cny00001Pk, info.Module);
            }

            DataTable dtCountModule = TableCountModuleTemp();
            foreach (DataRow dr in ldrDetail)
            {
                int isCheck = ProcessGeneral.GetSafeInt(dr["isCheck"]);
                string moduleCheck = ProcessGeneral.GetSafeString(dr["CNYSYS01Code"]);
                string rowState = ProcessGeneral.GetSafeString(dr["RowState"]);

                if (moduleCheck == "XX" || rowState == DataStatus.Insert.ToString() ||
                   (isCheck == 1 && rowState == DataStatus.Unchange.ToString()) ||
                   (isCheck == 0 && rowState == DataStatus.Unchange.ToString())) continue;

                Cls_AdminCodeFile info2 = new Cls_AdminCodeFile
                {
                    Module = ProcessGeneral.GetSafeString(dr["CNYSYS01Code"]),
                };
                dtCountModule.Rows.Add(info2.Module);
            }

            var lModule = dtCountModule.AsEnumerable().Select(p => string.Format("'{0}'", p.Field<string>("Module"))).Distinct().ToList();
            //var lModule = dtResponsibility.AsEnumerable().Select(p => string.Format("'{0}'", p.Field<string>("Module"))).Distinct().ToList();
            string strModule = string.Join(",", lModule);
            if (dtResponsibility.Rows.Count >= 0)
            {
                _inf.Update_Responsibility(cny00004Pk, dtResponsibility, strModule);
            }
        }
        #endregion
        #endregion



        //Tab User, Tab Notification
        #region "Use Button Control Input Empty Control Input"
        private void ClearTextControl()
        {
            txtUserName.EditValue = "";
            dtBrithday.DateTime = DateTime.Now;
            txtPassword.EditValue = @"cny";
            rdSex.SelectedIndex = 0;
            txtFullName.EditValue = "";
            txtEmail.EditValue = "";
            _userId = 0;
            _strPath = "";
            searchDepartMent.EditValue = "";
            searchPositions.EditValue = "";
            chkActive.Checked = true;
            txtCreateBy.EditValue = DeclareSystem.SysUserName.ToUpper();
            txtCreateDate.EditValue = DateTime.Now.ToString(ConstSystem.SysDateFormat);
            txtUpdateBy.EditValue = DeclareSystem.SysUserName.ToUpper();
            txtUpdateDate.EditValue = DateTime.Now.ToString(ConstSystem.SysDateFormat);

            pictureBoxIcon.Image = null;
            //==============
            _userName = "";
            gcMember.DataSource = TableMemberTemp();
            LoadDataInGridViewFunction("");
            gcFunction.DataSource = TableFunctionTemp();
            LoadDataInGridViewUiR(0);
            lMemberDel.Clear();
            lPkFunctionDel.Clear();
            lPkReceipientDel.Clear();

            //Adjust 12/03/2020
            txtPK.EditValue = -1;
        }


        private void UseControlInput(string option)
        {
            if (option == "Load")
            {
                txtUserName.Enabled = false;
                dtBrithday.Enabled = false;
                rdSex.Enabled = false;
                txtFullName.Enabled = false;
                txtEmail.Enabled = false;

                searchDepartMent.Enabled = false;
                searchPositions.Enabled = false;
                chkActive.Enabled = false;
                pictureBoxIcon.Enabled = false;
            }
            else
            {

                txtUserName.Enabled = option == "Add";
                dtBrithday.Enabled = true;
                rdSex.Enabled = true;
                txtFullName.Enabled = true;
                txtEmail.Enabled = true;

                searchDepartMent.Enabled = true;
                searchPositions.Enabled = true;
                chkActive.Enabled = true;
                pictureBoxIcon.Enabled = true;
            }

        }

        #endregion

        #region "methold"

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
            gv.OptionsView.ShowAutoFilterRow = true;
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

            //   gv.MouseMove += gv_MouseMove;
            gv.FocusedRowChanged += gv_FocusedRowChanged;
            gv.RowStyle += gv_RowStyle;
            gv.RowCountChanged += gv_RowCountChanged;
            gv.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gv.FocusedColumnChanged += gv_FocusedColumnChanged;
            gv.KeyDown += Gv_KeyDown;
            gv.DoubleClick += Gv_DoubleClick;
            gc.ForceInitialize();
        }



        private void LoadDataInGridView(GridControl gc, GridView gv)
        {



            _dlg = new WaitDialogForm();

            DataTable dtS = _inf.GridViewData_Load();

            if (DeclareSystem.SysUserName.ToUpper() != "ADMIN")
            {
                var q1 = dtS.AsEnumerable().Where(p => p.Field<string>("UserName").ToUpper().Trim() == "ADMIN").ToList();
                if (q1.Any())
                {
                    foreach (DataRow drQ1 in q1)
                    {
                        dtS.Rows.Remove(drQ1);
                    }
                    dtS.AcceptChanges();
                }

            }

            if (gv.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in dtS.Columns)
                {
                    gv.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }
                ProcessGeneral.HideVisibleColumnsGridView(gv, false, "UserID", "Password", "Signature", "PositionsCode", "DepartmentCode", "Sex");



                ProcessGeneral.SetGridColumnHeader(gv.Columns["RoleGroup"], "Role", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["FullName"], "Full Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["UserName"], "User Name", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["Email"], "Email", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["DateOfBirth"], "Date Of Birth", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["PositionsName"], "Position", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["DepartmentName"], "Department", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["IsActive"], "Active", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["ChangePassDate"], "Change Pass Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);

                ProcessGeneral.SetGridColumnHeader(gv.Columns["CreateDate"], "Created Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["CreateBy"], "Created By", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["UpdateDate"], "Updated Date", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gv.Columns["UpdateBy"], "Updated By", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            }
            gv.BeginUpdate();




            //gc.DataSource = null;
            //gv.Columns.Clear();

            gc.DataSource = dtS;


            gv.BestFitColumns();
            gv.EndUpdate();

            ////========================
            ////GetDataMasterDetail();
            //_dtUser = _inf.GetListUser();
            //_dtRule = _inf.GetListFunction();

            //DataTable dtTemp = new DataTable();
            //dtTemp.Columns.Add("USERNAME", typeof(string));

            //foreach (DataRow drTemp in dtS.Rows)
            //{
            //    dtTemp.Rows.Add(ProcessGeneral.GetSafeString(drTemp["UserName"]));
            //}

            //_dicRule.Clear();
            //_dicUser.Clear();
            //_dicStatus.Clear();

            //DataTable dtRiR = _inf.LoadFunctionByUser(dtTemp);
            //_dicRule = dtRiR.AsEnumerable().GroupBy(p => p.Field<string>("UserName")).Select(s => new
            //{
            //    UserName = s.Key,
            //    ListField = s.Select(t => new
            //    {
            //        FunctionCode = t.Field<String>("FunctionCode"),
            //        FunctionName = t.Field<String>("FunctionName"),
            //        FunctionDescription = t.Field<String>("FunctionDescription"),
            //        IsActive = t.Field<bool>("IsActive"),
            //        UserName = t.Field<String>("UserName"),
            //        CNYSYS11PK = t.Field<Int64>("CNYSYS11PK"),
            //    }).OrderBy(t => t.FunctionCode).ToList().CopyToDataTableNew(),
            //}).ToDictionary(s => s.UserName, s => s.ListField);

            ////Dic Status
            //DataTable dtSiF = _inf.LoadSatusByFunction(dtTemp);
            //_dicStatus = dtSiF.AsEnumerable().GroupBy(p => p.Field<Int64>("CNYSYS11PK")).Select(s => new
            //{
            //    CNYSYS11PK = s.Key,
            //    ListField = s.Select(t => new
            //    {
            //        StatusCode = t.Field<String>("StatusCode"),
            //        StatusDescription = t.Field<int>("StatusDescription"),
            //        CNYSYS11PK = t.Field<Int64>("CNYSYS11PK"),
            //        CNYSYS11APK = t.Field<Int64>("CNYSYS11APK"),
            //        RowState = "Unchange",
            //    }).OrderBy(t => t.StatusCode).ToList().CopyToDataTableNew(),
            //}).ToDictionary(s => s.CNYSYS11PK, s => s.ListField);


            //DataTable dtUiR = _inf.LoadUserByFunction(dtTemp);
            //_dicUser = dtUiR.AsEnumerable().GroupBy(p => p.Field<Int64>("CNYSYS11PK")).Select(s => new
            //{
            //    CNYSYS11PK = s.Key,
            //    ListField = s.Select(t => new
            //    {
            //        DepartmentName = t.Field<String>("DepartmentName"),
            //        UserID = t.Field<Int64>("UserID"),
            //        UserName = t.Field<String>("UserName"),
            //        FullName = t.Field<String>("FullName"),
            //        Email = t.Field<String>("Email"),
            //        CNYSYS11PK = t.Field<Int64>("CNYSYS11PK"),
            //    }).OrderBy(t => t.UserName).ToList().CopyToDataTableNew(),
            //}).ToDictionary(s => s.CNYSYS11PK, s => s.ListField);
            ////cho du lieu ve dong dau tien luoi de load User Receipient  
            //SetFocusOnGridFristRow_Function(gvFunction);
            _dlg.Close();
        }
        private void DisplatDetailFocusedRowChanged(GridView gv)
        {
            //int rH = gv.FocusedRowHandle;
            //if (gv.RowCount == 0 || !gv.IsDataRow(rH))
            //{
            //    _userId = 0;
            //    txtUserName.EditValue = string.Empty;
            //    txtFullName.EditValue = string.Empty;
            //    txtPassword.EditValue = @"cny";
            //    txtEmail.EditValue = string.Empty;
            //    dtBrithday.DateTime = DateTime.Now;
            //    searchPositions.EditValue = string.Empty;
            //    searchDepartMent.EditValue = string.Empty;
            //    rdSex.SelectedIndex = 0;
            //    pictureBoxIcon.Image = null;
            //    chkActive.Checked = true;
            //    txtCreateBy.EditValue = "";
            //    txtCreateDate.EditValue = "";
            //    txtUpdateBy.EditValue = "";
            //    txtUpdateDate.EditValue = "";
            //    _strPath = "";

            //    //===============================
            //    //LoadDataGvMainReceipientTbl(TableTempReceipientLoad());
            //    _userName = "";
            //    _CNYSYS11PK = 0;
            //    LoadDataInGridViewUiR(0);

            //}
            //else
            //{
            //    _userId = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(rH, gv.Columns["UserID"]));
            //    txtUserName.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["UserName"]));
            //    txtFullName.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["FullName"]));
            //    txtPassword.EditValue = EnDeCrypt.Decrypt(ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["Password"])), true);
            //    searchPositions.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["PositionsCode"]));
            //    searchDepartMent.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["DepartmentCode"]));
            //    txtEmail.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["Email"]));
            //    string sDate = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH,"DateOfBirth"));
            //    dtBrithday.DateTime = ProcessGeneral.ConvertDateTimeWithFormat(sDate, ConstSystem.SysDateConvert);

            //    chkActive.Checked = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rH, gv.Columns["IsActive"]));
            //    rdSex.SelectedIndex = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rH, gv.Columns["Sex"])) ? 1 : 0;

            //    object photoValue = gv.GetRowCellValue(rH, gv.Columns["Signature"]);
            //    if (!Convert.IsDBNull(photoValue) && photoValue != null)
            //    {
            //        pictureBoxIcon.Image = ProcessGeneral.ConvertByteArrayToImage((byte[])photoValue);//set image property of the picture box by creating a image from stream 
            //        pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;//set size mode property of the picture box to stretch 
            //        pictureBoxIcon.Refresh();//refresh picture box
            //    }
            //    else
            //    {
            //        pictureBoxIcon.Image = null;
            //    }
            //    txtCreateBy.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["CreateBy"]));
            //    txtCreateDate.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["CreateDate"]));
            //    txtUpdateBy.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["UpdateBy"]));
            //    txtUpdateDate.EditValue = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["UpdateDate"]));
            //    _strPath = "";
            //    //===========

            //    //LoadDataGvMainReceipient(_userId);
            //    _userName = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, gv.Columns["UserName"]));
            //    LoadDataInGridViewRiR(_userName);
            //    //cho du lieu ve dong dau tien luoi de load User Receipient  
            //    SetFocusOnGridFristRow_Function(gvFunction);
            //}

        }
        #endregion

        #region "Combobox,TextBox Event"
        private void TgShowPass_EditValueChanged(object sender, EventArgs e)
        {
            if (tgShowPass.IsOn)
            {
                // txtPassword.Properties.UseSystemPasswordChar = false;
                txtPassword.Properties.PasswordChar = '\0';
                //FrmInput

            }
            else
            {
                //txtPassword.Properties.UseSystemPasswordChar = true;
                txtPassword.Properties.PasswordChar = '*';
            }
        }


        private void searchPositions_EditValueChanged(object sender, EventArgs e)
        {
            txtPositions.EditValue = ProcessGeneral.GetSafeString(searchPositions.EditValue) != "" ? ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(searchPositions)["PositionsName"]) : string.Empty;
        }

        private void searchDepartMent_EditValueChanged(object sender, EventArgs e)
        {
            txtDepartMent.EditValue = ProcessGeneral.GetSafeString(searchDepartMent.EditValue) != "" ? ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(searchDepartMent)["DepartmentName"]) : string.Empty;
        }

        #endregion

        #region "gridview event"



        private void Gv_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (e.KeyCode == Keys.Enter)
            {
                UpdateUserRole(gv, gv.FocusedRowHandle);
            }

        }

        private void Gv_DoubleClick(object sender, EventArgs e)
        {


            var gv = (GridView)sender;
            if (gv == null) return;
            GridControl gc = gv.GridControl;
            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
            if (!hi.InRowCell) return;
            UpdateUserRole(gv, hi.RowHandle);

        }

        private void UpdateUserRole(GridView gv, int rH)
        {

            if (gv.RowCount == 0) return;
            if (!gv.IsDataRow(rH)) return;
            if (!_perInfo.PerIns && !_perInfo.PerUpd && !_perInfo.PerDel) return;

            var f = new FrmUserMemberOf(_userId, ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "UserName")),
                ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "FullName")), ProcessGeneral.GetSafeInt(gv.GetRowCellValue(rH, "IsActive")));
            f.ShowDialog();
            _disableEventWhenLoad = true;
            LoadDataInGridView(gv.GridControl, gv);
            if (gv.RowCount >= rH + 1)
            {
                gv.SelectRow(rH);
                gv.FocusedRowHandle = rH;
                gv.FocusedColumn = gv.Columns["UserName"];
            }
            DisplatDetailFocusedRowChanged(gv);
            _disableEventWhenLoad = false;
        }

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

        public bool PerformAdd(string option) //Adjust 12/03/2020: thêm option
        {
            _dicTlDetail.Clear();
            _option = option;

            UseControlInput("Add");
            ClearTextControl();
            txtUserName.Focus();

            //Adjust Tab Extension
            _userId = 0;
            //_userIdGetNext = ProcessGeneral.GetNextId("ListUser");
            //txtUserID.EditValue = ProcessGeneral.GetSafeInt64(_userIdGetNext);
            txtUserID.EditValue = -1;
            LoadControl();

            _userName = "";
            //Load dữ liệu lên Tab Notication
            LoadDataInGridViewFunction(_userName);

            _dlg = new WaitDialogForm();
            LoadDataGvMain();
            _dlg.Close();

            return true;

        }


        /// <summary>
        /// Perform when click edit button
        /// </summary>
        public bool PerformEdit(long _UserID, string _UserName, string option) //Adjust 12/03/2020: thêm option
        {
            _dicTlDetail.Clear();
            _option = option;

            UseControlInput("Edit");
            //Load du lieu len tab User Info
            _userId = _UserID;
            _userName = _UserName;
            //Adjust Tab Extension
            LoadControl();

            DisplatDataForEdit();
            gcMember.DataSource = _inf.UserMemberLoadGrid(_userId); // load dữ liệu lên lưới Member Of
            BestFitColumnsWithImage_Member();
            txtFullName.Focus();
            //Load dữ liệu lên Tab Notication
            LoadDataInGridViewFunction(_userName);

            _dlg = new WaitDialogForm();
            LoadDataGvMain();
            _dlg.Close();
            return true;
        }


        private void DisplatDataForEdit()
        {
            DataTable dtHeader = _inf.TabUserInfo_Load(string.Format("where a.UserID={0}", _userId));
            txtUserID.EditValue = ProcessGeneral.GetSafeInt64(dtHeader.Rows[0]["UserID"]);
            txtUserName.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["UserName"]);
            txtFullName.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["FullName"]);
            txtPassword.EditValue = EnDeCrypt.Decrypt(ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Password"]), true);
            searchPositions.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["PositionsCode"]);
            searchDepartMent.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DepartmentCode"]);
            txtEmail.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["Email"]);
            string sDate = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["DateOfBirth"]);
            dtBrithday.DateTime = ProcessGeneral.ConvertDateTimeWithFormat(sDate, ConstSystem.SysDateConvert);

            chkActive.Checked = ProcessGeneral.GetSafeBool(dtHeader.Rows[0]["IsActive"]);
            rdSex.SelectedIndex = ProcessGeneral.GetSafeBool(dtHeader.Rows[0]["Sex"]) ? 1 : 0;

            object photoValue = dtHeader.Rows[0]["Signature"];
            if (!Convert.IsDBNull(photoValue) && photoValue != null)
            {
                pictureBoxIcon.Image = ProcessGeneral.ConvertByteArrayToImage((byte[])photoValue);//set image property of the picture box by creating a image from stream 
                pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;//set size mode property of the picture box to stretch 
                pictureBoxIcon.Refresh();//refresh picture box
            }
            else
            {
                pictureBoxIcon.Image = null;
            }
            txtCreateBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreateBy"]);
            txtCreateDate.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["CreateDate"]);
            txtUpdateBy.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["UpdateBy"]);
            txtUpdateDate.EditValue = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["UpdateDate"]);
            _strPath = "";
            //===========

            ////LoadDataGvMainReceipient(_userId);
            //_userName = ProcessGeneral.GetSafeString(dtHeader.Rows[0]["UserName"]);
            //LoadDataInGridViewRiR(_userName);
            ////cho du lieu ve dong dau tien luoi de load User Receipient  
            //SetFocusOnGridFristRow_Function(gvFunction);

            DataRow dr = null;
            DataTable dtCNY00004 = _inf.LoadCNY00004(_userId);
            if (dtCNY00004.Rows.Count <= 0)
            {
                string userName = ProcessGeneral.GetSafeString(txtUserName.EditValue);
                string fullName = ProcessGeneral.GetSafeString(txtFullName.EditValue);
                string useModule = "";
                DataTable dtInsert = _inf.InsertCNY00004(userName, fullName, useModule, _userId);
                dr = dtInsert.Rows[0];
            }
            else
            {
                dr = dtCNY00004.Rows[0];
            }
            txtPK.EditValue = ProcessGeneral.GetSafeInt64(dr["PK"]);
        }

        #region "Proccess Methold grid Member of"

        #region "GridView Event"

        private void gvMember_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (e.KeyCode == Keys.F8)
            {
                btnRemoveMember_Click(sender, e);
            }

        }
        private void gvMember_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }

        private void gvMember_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
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


        private void gvMember_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvMember_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }

        private void gvMember_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex == 0)
            {
                Image icon = CNY_AdminSys.Properties.Resources.user_group_icon_24;
                e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }
        }

        private void gvMember_RowStyle(object sender, RowStyleEventArgs e)
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

        private DataTable TableMemberTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GroupUserCode", typeof(string));
            dt.Columns.Add("GroupUserName", typeof(string));
            dt.Columns.Add("GroupUserDescription", typeof(string));
            dt.Columns.Add("Priority", typeof(string));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }
        private void GridViewCustomInit()
        {


            // gcMember.ToolTipController = toolTipController1  ;
            gcMember.UseEmbeddedNavigator = true;

            gcMember.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMember.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMember.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMember.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMember.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvMember.OptionsBehavior.Editable = false;
            gvMember.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMember.OptionsCustomization.AllowColumnMoving = false;
            gvMember.OptionsCustomization.AllowQuickHideColumns = true;
            gvMember.OptionsCustomization.AllowSort = true;
            gvMember.OptionsCustomization.AllowFilter = true;

            //     gvMember.OptionsHint.ShowCellHints = true;

            gvMember.OptionsView.ShowGroupPanel = false;
            gvMember.OptionsView.ShowIndicator = true;
            gvMember.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMember.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMember.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMember.OptionsView.ShowAutoFilterRow = false;
            gvMember.OptionsView.AllowCellMerge = false;
            gvMember.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMember.OptionsView.ColumnAutoWidth = false;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMember.OptionsNavigation.AutoFocusNewRow = true;
            gvMember.OptionsNavigation.UseTabKey = true;

            gvMember.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMember.OptionsSelection.MultiSelect = true;
            gvMember.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMember.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvMember.OptionsSelection.EnableAppearanceFocusedCell = true;

            gvMember.OptionsView.EnableAppearanceEvenRow = true;
            gvMember.OptionsView.EnableAppearanceOddRow = true;

            gvMember.OptionsView.ShowFooter = false;

            gvMember.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvMember.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMember.OptionsFind.AlwaysVisible = false;
            gvMember.OptionsFind.ShowCloseButton = true;
            gvMember.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMember)
            {
                IsPerFormEvent = true,
            };

            gvMember.OptionsPrint.AutoWidth = false;

            //var gridColumn0 = new GridColumn();
            //gridColumn0.AppearanceHeader.Options.UseTextOptions = true;
            //gridColumn0.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            //gridColumn0.AppearanceCell.Options.UseTextOptions = true;
            //gridColumn0.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            //gridColumn0.Caption = @"Role Code";
            //gridColumn0.FieldName = "GroupUserCode";
            //gridColumn0.Name = "GroupUserCode";
            //gridColumn0.SummaryItem.SummaryType = SummaryItemType.Count;
            //gridColumn0.SummaryItem.DisplayFormat = @"Count :";
            //gridColumn0.Visible = true;
            //gridColumn0.VisibleIndex = 0;
            //gvMember.Columns.Add(gridColumn0);

            //var gridColumn1 = new GridColumn();
            //gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            //gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            //gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            //gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            //gridColumn1.Caption = @"Role Name";
            //gridColumn1.FieldName = "GroupUserName";
            //gridColumn1.Name = "GroupUserName";
            //gridColumn1.SummaryItem.SummaryType = SummaryItemType.Count;
            //gridColumn1.SummaryItem.DisplayFormat = @"{0:N0} (role)";
            //gridColumn1.Visible = true;
            //gridColumn1.VisibleIndex = 1;
            //gvMember.Columns.Add(gridColumn1);

            //var gridColumn2 = new GridColumn();
            //gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            //gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            //gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            //gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            //gridColumn2.Caption = @"Description";
            //gridColumn2.FieldName = "GroupUserDescription";
            //gridColumn2.Name = "GroupUserDescription";
            //gridColumn2.Visible = true;
            //gridColumn2.VisibleIndex = 2;
            //gvMember.Columns.Add(gridColumn2);

            //var gridColumn3 = new GridColumn();
            //gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            //gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            //gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridColumn3.Caption = @"Priority";
            //gridColumn3.FieldName = "Priority";
            //gridColumn3.Name = "Priority";
            //gridColumn3.Visible = true;
            //gridColumn3.VisibleIndex = 3;
            //gvMember.Columns.Add(gridColumn3);

            //var gridColumn4 = new GridColumn();
            //gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            //gridColumn4.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            //gridColumn4.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //gridColumn4.Caption = @"RowState";
            //gridColumn4.FieldName = "RowState";
            //gridColumn4.Name = "RowState";
            //gridColumn4.Visible = true;
            //gridColumn4.VisibleIndex = 4;
            //gvMember.Columns.Add(gridColumn4);

            gcMember.DataSource = TableMemberTemp();
            ProcessGeneral.SetGridColumnHeader(gvMember.Columns["GroupUserCode"], "Role Code", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMember.Columns["GroupUserName"], "Role Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMember.Columns["GroupUserDescription"], "Description", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvMember.Columns["Priority"], "Priority", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.HideVisibleColumnsGridView(gvMember, false, "RowState");

            gvMember.CustomDrawCell += gvMember_CustomDrawCell;
            gvMember.RowStyle += gvMember_RowStyle;
            gvMember.RowCountChanged += gvMember_RowCountChanged;
            gvMember.CustomDrawRowIndicator += gvMember_CustomDrawRowIndicator;
            gvMember.CustomDrawFooter += gvMember_CustomDrawFooter;
            gvMember.CustomDrawFooterCell += gvMember_CustomDrawFooterCell;
            gvMember.KeyDown += gvMember_KeyDown;
            gcMember.ForceInitialize();



        }

        private void BestFitColumnsWithImage_Member()
        {
            gvMember.BestFitColumns();
            gvMember.Columns["GroupUserCode"].Width += 20;
            //gvMember.Columns["GroupUserName"].Width += 30;
        }

        private DataTable TableTemplateGroupRole()
        {
            var dt = new DataTable();
            dt.Columns.Add("GroupUserCode", typeof(string));
            return dt;
        }

        private DataTable TableTemplateCode()
        {
            var dt = new DataTable();
            dt.Columns.Add("Code", typeof(string));
            return dt;
        }

        private DataTable TableTemplatePK()
        {
            var dt = new DataTable();
            dt.Columns.Add("PK", typeof(long));
            return dt;
        }
        #endregion

        public bool PerformDelete()
        {
            if (_userId == 0)
            {
                XtraMessageBox.Show("No row is selected to perform deleting", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return false;
            }

            if (txtUserName.Text.Trim().ToUpper() == "ADMIN")
            {
                XtraMessageBox.Show("This User don't allow to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to delete user {0} ? (yes/No) \n Note:You could not restore this record!", txtUserName.Text.Trim()), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return false;
            if (_inf.Delete(_userId) == 1)
            {
                XtraMessageBox.Show(string.Format("User {0} has been deleted from the database", txtUserName.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                _disableEventWhenLoad = true;
                LoadDataInGridView(gcMember, gvMember);
                DisplatDetailFocusedRowChanged(gvMember);
                _disableEventWhenLoad = false;
                return true;
            }
            else
            {
                XtraMessageBox.Show(string.Format("Could not delete User {0} \n because of an error in the process of deleting data", txtUserName.Text.Trim()),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }



        }

        //Adjust Tab Extension
        private bool CheckControlIsChangedValue()
        {
            if (_chkCtrl.CheckControlIsChangedValue())
            {
                _dlg.Close();
                return true;
            }
            return false;
        }

        public bool PerformSave(string option)
        {
            xtraTabUser.SelectedTabPage = TabUser;
            //Adjust Tab Extension
            if (!CheckInputControlBeforeSave())
            {
                return false;
            }
            if (!CheckControlIsChangedValue())
            {
                return false;
            }
            bool isSuccess = false;
            //xtraTabUser.SelectedTabPage = TabUser;
            SetParameterWhenSaveData();
            Int64 cny00004Pk = ProcessGeneral.GetSafeInt64(txtPK.EditValue);
            string userName = ProcessGeneral.GetSafeString(txtUserName.EditValue);
            string fullName = ProcessGeneral.GetSafeString(txtFullName.EditValue);
            string useModule = "";
            Int64 userId = ProcessGeneral.GetSafeInt64(txtUserID.EditValue);

            Int64 cny00004PkSave = -1;
            Int64 userIdSave = -1;

            if (option.ToUpper() == "ADD")
            {
                _userId = _inf.InsertUser(_cls);

                DataTable dtInsert = _inf.InsertCNY00004(userName, fullName, useModule, _userId); //userId
                if (_userId > 0 && ProcessGeneral.GetSafeInt(dtInsert.Rows[0]["errCode"]) == 1)
                {
                    _dlg.Close();
                    cny00004PkSave = ProcessGeneral.GetSafeInt64(dtInsert.Rows[0]["PK"]);
                    userIdSave = ProcessGeneral.GetSafeInt64(dtInsert.Rows[0]["UserID"]);
                    _userName = ProcessGeneral.GetSafeString(txtUserName.EditValue);
                    XtraMessageBox.Show(string.Format("Insert Successful User : {0}", txtUserName.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;
                }
                else
                {
                    _dlg.Close();
                    XtraMessageBox.Show(string.Format("User {0} have been already exists in system", txtUserName.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
            }
            else // edit
            {

                int resultUpdate = string.IsNullOrEmpty(_strPath) ? _inf.ListUser_Update_NoneImage(_cls) : _inf.ListUser_Update_WithImage(_cls);
                DataTable dtUpdate = _inf.UpdateCNY00004(cny00004Pk, userId, userName, fullName, useModule);
                if (resultUpdate == 1 && ProcessGeneral.GetSafeInt(dtUpdate.Rows[0]["errCode"]) == 1)
                {
                    _dlg.Close();
                    cny00004PkSave = ProcessGeneral.GetSafeInt64(dtUpdate.Rows[0]["PK"]);
                    userIdSave = ProcessGeneral.GetSafeInt64(dtUpdate.Rows[0]["UserID"]);
                    XtraMessageBox.Show(string.Format("Update Successful User : {0}", txtUserName.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    isSuccess = true;

                }
                else
                {
                    _dlg.Close();
                    XtraMessageBox.Show(string.Format("User {0} not exists in system", txtUserName.Text.Trim()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                }

            }

            if (!isSuccess) return false;

            #region Xoá những dòng đã chọn

            DeleteOnSelect(_userId);
            #endregion


            #region Lưu Member

            SaveMember(_userId);
            #endregion

            #region Lưu Function

            SaveFunction(_userId, _userName);
            #endregion

            #region Lưu Status

            SaveStatus();
            #endregion

            #region Lưu User Requiment

            SaveReceipient();
            #endregion

            #region Lưu Responsibility
            SaveDataResponsibility(cny00004PkSave, userIdSave);
            string sql = string.Format("SELECT DISTINCT CNYSYS01Code AS Module FROM dbo.CNY004 WHERE UserID={0}", _userId);
            DataTable dtStrModule = _inf.TblExcuteSqlText(sql);
            var lStrModule = dtStrModule.AsEnumerable().Select(p => p.Field<string>("Module")).ToList();
            string strUseModule = "";
            if (lStrModule.Any())
            {
                strUseModule = string.Join(",", lStrModule);
            }
            _inf.UpdateCNY00004(cny00004Pk, userId, userName, fullName, strUseModule);
            #endregion

            _strPath = "";
            UseControlInput("Edit");


            _disableEventWhenLoad = true;

            //Load du lieu len tab User Info
            DisplatDataForEdit();
            gcMember.DataSource = _inf.UserMemberLoadGrid(_userId); // load dữ liệu lên lưới Member Of
            BestFitColumnsWithImage_Member();

            //Load dữ liệu lên Tab Notication
            LoadDataInGridViewFunction(_userName);

            //Adjust Tab Extension
            _dlg = new WaitDialogForm();
            LoadDataGvMain();
            _dlg.Close();

            txtFullName.Select();

            _disableEventWhenLoad = false;
            return true;
        }

        private bool DeleteOnSelect(Int64 _UserID)
        {
            try
            {
                // xoá Member
                var q1 = lMemberDel.Where(p => p != "").Select(p => p.ToString()).Distinct().ToList();
                if (q1.Any())
                {
                    string sQ1 = string.Join("','", q1);
                    if (!string.IsNullOrEmpty(sQ1))
                    {
                        string sqlQ1 =
                            string.Format("DELETE FROM dbo.UserInGroup WHERE UserID = {0} AND GroupUserCode IN ('{1}')",
                                _UserID, sQ1);
                        _inf.BolExcuteSqlText(sqlQ1);
                        lMemberDel.Clear();
                    }
                }

                // Xoá Function
                var q2 = lPkFunctionDel.Where(p => p > 0).Select(p => p.ToString()).Distinct().ToList();
                if (q2.Any())
                {
                    string sQ2 = string.Join(",", q2);
                    if (!string.IsNullOrEmpty(sQ2))
                    {
                        string sqlUiR = string.Format("DELETE FROM [dbo].[CNYSYS12] WHERE [CNYSYS11PK] IN ({0})", sQ2);
                        _inf.BolExcuteSqlText(sqlUiR);

                        string sqlSiF = string.Format("DELETE FROM [dbo].[CNYSYS11A] WHERE [CNYSYS11PK] IN ({0})", sQ2);
                        _inf.BolExcuteSqlText(sqlSiF);

                        string sqlQ2 = string.Format("DELETE FROM [dbo].[CNYSYS11] WHERE [PK] IN ({0})", sQ2);
                        _inf.BolExcuteSqlText(sqlQ2);
                        lPkFunctionDel.Clear();
                    }
                }

                // Xoá Status
                var qstatus = lPkStatusDel.Where(p => p > 0).Select(p => p.ToString()).Distinct().ToList();
                if (qstatus.Any())
                {
                    string sQ2 = string.Join(",", qstatus);
                    if (!string.IsNullOrEmpty(sQ2))
                    {
                        string sqlUiR = string.Format("DELETE FROM [dbo].[CNYSYS12] WHERE [CNYSYS11APK] IN ({0})", sQ2);
                        _inf.BolExcuteSqlText(sqlUiR);

                        string sqlQ2 = string.Format("DELETE FROM [dbo].[CNYSYS11A] WHERE [PK] IN ({0})", sQ2);
                        _inf.BolExcuteSqlText(sqlQ2);
                        lPkStatusDel.Clear();
                    }
                }

                // Xoá User Requiment
                var q3 = lPkReceipientDel.Where(p => p > 0).Select(p => p.ToString()).Distinct().ToList();
                if (q3.Any())
                {
                    string sQ3 = string.Join(",", q3);
                    if (!string.IsNullOrEmpty(sQ3))
                    {
                        string sqlQ3 = string.Format("DELETE FROM [dbo].[CNYSYS12] WHERE [PK] IN ({0})", sQ3);
                        _inf.BolExcuteSqlText(sqlQ3);
                        lPkReceipientDel.Clear();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        private bool SaveMember(Int64 _UserID)
        {

            try
            {
                List<string> l = new List<string>();
                var dtS = (DataTable)gcMember.DataSource;
                var query = dtS.AsEnumerable().Where(p => ProcessGeneral.GetSafeString(p["RowState"]) == DataStatus.Insert.ToString())
                    .Select(p => new
                    {
                        GroupUserCode = ProcessGeneral.GetSafeString(p["GroupUserCode"])

                    }).ToList();
                if (!query.Any()) return false;
                foreach (var dr in query)
                {
                    l.Add(dr.GroupUserCode);
                }

                if (l.Count <= 0) return false;
                foreach (string s in l)
                {
                    _inf.UserInGroup_Insert(s, _UserID);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }


        private bool SaveFunction(Int64 _UserID, string _UserName)
        {

            try
            {
                var dtS = (DataTable)gcFunction.DataSource;
                var query = dtS.AsEnumerable().Where(p => ProcessGeneral.GetSafeString(p["RowState"]) != DataStatus.Unchange.ToString())
                    .Select(p => new
                    {
                        CNYSYS11PK = ProcessGeneral.GetSafeInt64(p["CNYSYS11PK"]),
                        FunctionCode = ProcessGeneral.GetSafeString(p["FunctionCode"]),
                        IsActive = ProcessGeneral.GetSafeInt(p["IsActive"]),
                        RowState = ProcessGeneral.GetSafeString(p["RowState"])
                    }).ToList();
                if (!query.Any()) return false;
                foreach (var dr in query)
                {
                    _inf.User_InsertUpdateFunction(dr.CNYSYS11PK, _UserID, _UserName, dr.FunctionCode, dr.IsActive, dr.RowState);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        private bool SaveStatus()
        {

            try
            {
                var q1 = _dicStatus.SelectMany(t => t.Value.AsEnumerable().Where(p => p.Field<string>("RowState") != DataStatus.Unchange.ToString()), (m, n) => new
                {
                    CNYSYS11PK = m.Key,
                    StatusCode = ProcessGeneral.GetSafeInt(n["StatusCode"]),
                    TypeReceipient = ProcessGeneral.GetSafeInt(n["TypeReceipient"]),
                    CNYSYS11APK = ProcessGeneral.GetSafeInt64(n["CNYSYS11APK"]),
                    RowState = ProcessGeneral.GetSafeString(n["RowState"]),
                }).Distinct().ToList();

                if (!q1.Any()) return false;

                DataTable dtIU = q1.CopyToDataTableNew();

                _inf.User_SaveStatus(dtIU);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        private bool SaveReceipient()
        {

            try
            {
                var q1 = _dicUser.SelectMany(t => t.Value.AsEnumerable().Where(p => p.Field<string>("RowState") == DataStatus.Insert.ToString()), (m, n) => new
                {
                    CNYSYS11APK = m.Key,
                    CNYSYS11PK = ProcessGeneral.GetSafeInt64(n["CNYSYS11PK"]),
                    UserID = ProcessGeneral.GetSafeInt64(n["UserID"]),
                    CNYSYS12PK = ProcessGeneral.GetSafeInt64(n["CNYSYS12PK"]),
                    RowState = ProcessGeneral.GetSafeString(n["RowState"]),
                }).Distinct().ToList();

                //var q1 = _dicReceipient.SelectMany(t => t.Value.dt.AsEnumerable().Where(p => p.Field<string>("RowState") == DataStatus.Insert.ToString()), (m, n) => new
                //{
                //    CNYSYS11PK = m.Key,
                //    UserID = ProcessGeneral.GetSafeInt64(n["UserID"]),
                //    CNYSYS12PK = ProcessGeneral.GetSafeInt64(n["CNYSYS12PK"]),
                //    RowState = ProcessGeneral.GetSafeString(n["RowState"]),
                //}).Distinct().ToList();

                if (!q1.Any()) return false;

                DataTable dtIU = q1.CopyToDataTableNew();

                _inf.User_SaveReceipient(dtIU);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
        public bool PerformCancel()
        {


            UseControlInput("Load");
            gcMember.Enabled = true;

            _disableEventWhenLoad = true;
            DisplatDetailFocusedRowChanged(gvMember);
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

                        txtFullName.Focus();
                        DisplatDataForEdit();
                        gcMember.DataSource = _inf.UserMemberLoadGrid(_userId);
                        BestFitColumnsWithImage_Member();
                        LoadDataInGridViewFunction(_userName);
                        lMemberDel.Clear();
                        lPkFunctionDel.Clear();
                        lPkReceipientDel.Clear();
                        //Adjust Tab Extension
                        _dlg = new WaitDialogForm();
                        LoadDataGvMain();
                        _dlg.Close();

                        _disableEventWhenLoad = false;

                    }
                    break;
                case "ADD":
                    {

                        ClearTextControl();
                        //Adjust Tab Extension
                        _dlg = new WaitDialogForm();
                        LoadDataGvMain();
                        _dlg.Close();

                    }
                    break;
                case "EDIT":
                    {
                        _disableEventWhenLoad = true;

                        DisplatDataForEdit();
                        gcMember.DataSource = _inf.UserMemberLoadGrid(_userId);
                        BestFitColumnsWithImage_Member();
                        LoadDataInGridViewFunction(_userName);
                        lMemberDel.Clear();
                        lPkFunctionDel.Clear();
                        lPkReceipientDel.Clear();
                        //Adjust Tab Extension
                        _dlg = new WaitDialogForm();
                        LoadDataGvMain();
                        _dlg.Close();

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
            _cls.UserID = _userId;
            _cls.UserName = ProcessGeneral.GetSafeString(txtUserName.EditValue);
            _cls.FullName = ProcessGeneral.GetSafeString(txtFullName.EditValue);
            _cls.Password = EnDeCrypt.Encrypt(ProcessGeneral.GetSafeString(txtPassword.EditValue), true);
            _cls.DateOfBirth = dtBrithday.DateTime;
            _cls.Sex = rdSex.SelectedIndex == 1;
            _cls.Email = ProcessGeneral.GetSafeString(txtEmail.EditValue);
            _cls.StrPath = _strPath;
            _cls.PositionsCode = ProcessGeneral.GetSafeString(searchPositions.EditValue);
            _cls.DepartmentCode = ProcessGeneral.GetSafeString(searchDepartMent.EditValue);
            _cls.IsActive = chkActive.Checked;
        }
        private bool CheckInputControlBeforeSave()
        {
            _dlg = new WaitDialogForm();
            if (ProcessGeneral.GetSafeString(txtUserName.EditValue) == "")
            {
                _dlg.Close();
                XtraMessageBox.Show("Username Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtUserName.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(txtFullName.EditValue) == "")
            {
                _dlg.Close();
                XtraMessageBox.Show("Full Name could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtFullName.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(txtEmail.EditValue) == "")
            {
                _dlg.Close();
                XtraMessageBox.Show("Email could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtEmail.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(searchDepartMent.EditValue) == "")
            {
                _dlg.Close();
                XtraMessageBox.Show("Department Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                searchDepartMent.Focus();
                return false;
            }
            if (ProcessGeneral.GetSafeString(searchPositions.EditValue) == "")
            {
                _dlg.Close();
                XtraMessageBox.Show("Positions Code could not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                searchPositions.Focus();
                return false;
            }
            _dlg.Close();
            return true;
        }

        #endregion

        #region "CheckBox,PictureBox Event"

        private void pictureBoxIcon_DoubleClick(object sender, EventArgs e)
        {

            var opf = new OpenFileDialog()
            {
                Title = @"Open File Image",
                Filter = @"Image File (.JPG,.PNG)|*.jpg;*.png",
                RestoreDirectory = true,
            };
            if (opf.ShowDialog() != DialogResult.OK)
                return;
            _strPath = Path.GetFullPath(opf.FileName).Trim();
            if (_strPath.Trim() != "")
            {
                pictureBoxIcon.Image = Image.FromFile(_strPath.Trim());
                pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxIcon.Refresh();
            }
            else
            {
                pictureBoxIcon.Image = null;
            }
        }

        #endregion

        #region Step 2
        //=============================================
        private DataTable TableFunctionTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FunctionCode", typeof(string));
            dt.Columns.Add("FunctionName", typeof(string));
            dt.Columns.Add("FunctionDescription", typeof(string));
            dt.Columns.Add("IsActive", typeof(bool));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("CNYSYS11PK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }
        private DataTable TableReceipientTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DepartmentName", typeof(string));
            dt.Columns.Add("UserID", typeof(Int64));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("CNYSYS11PK", typeof(Int64));
            dt.Columns.Add("CNYSYS11APK", typeof(Int64));
            dt.Columns.Add("CNYSYS12PK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }

        private DataTable TableStatusTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StatusCode", typeof(int));
            dt.Columns.Add("StatusDescription", typeof(string));
            dt.Columns.Add("CNYSYS11PK", typeof(Int64));
            dt.Columns.Add("TypeReceipient", typeof(int));
            dt.Columns.Add("CNYSYS11APK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }
        private void LoadDataInGridViewFunction(string UserName)
        {
            //Function in User
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("USERNAME", typeof(string));
            dtTemp.Rows.Add(UserName);

            _dicUser.Clear();
            _dicStatus.Clear();

            //Dic Status
            DataTable dtSiF = _inf.LoadSatusByFunction(dtTemp);
            _dicStatus = dtSiF.AsEnumerable().GroupBy(p => p.Field<Int64>("CNYSYS11PK")).Select(s => new
            {
                CNYSYS11PK = s.Key,
                ListField = s.Select(t => new
                {
                    StatusCode = t.Field<int>("StatusCode"),
                    StatusDescription = t.Field<String>("StatusDescription"),
                    CNYSYS11PK = t.Field<Int64>("CNYSYS11PK"),
                    TypeReceipient = t.Field<int>("TypeReceipient"),
                    CNYSYS11APK = t.Field<Int64>("CNYSYS11APK"),
                    RowState = "Unchange",
                }).OrderBy(t => t.StatusCode).ToList().CopyToDataTableNew(),
            }).ToDictionary(s => s.CNYSYS11PK, s => s.ListField);

            //Dic User Receppient
            DataTable dtUiR = _inf.LoadReceipientByFunction(dtTemp);
            _dicUser = dtUiR.AsEnumerable().GroupBy(p => p.Field<Int64>("CNYSYS11APK")).Select(s => new
            {
                CNYSYS11PK = s.Key,
                ListField = s.Select(t => new
                {
                    DepartmentName = t.Field<String>("DepartmentName"),
                    UserID = t.Field<Int64>("UserID"),
                    UserName = t.Field<String>("UserName"),
                    FullName = t.Field<String>("FullName"),
                    Email = t.Field<String>("Email"),
                    CNYSYS11PK = t.Field<Int64>("CNYSYS11PK"),
                    CNYSYS11APK = t.Field<Int64>("CNYSYS11APK"),
                    CNYSYS12PK = t.Field<Int64>("CNYSYS12PK"),
                    RowState = "Unchange",
                }).OrderBy(t => t.UserName).ToList().CopyToDataTableNew(),
            }).ToDictionary(s => s.CNYSYS11PK, s => s.ListField);

            // Load Function
            DataTable dtFunction = _inf.LoadFunction(dtTemp);


            DataTable dtS;
            if (dtFunction.Rows.Count <= 0)
            {
                dtS = TableFunctionTemp();
            }
            else
            {
                dtS = dtFunction;
            }

            //if (gvFunction.VisibleColumns.Count <= 0)
            //{
            //    int ind = 0;
            //    foreach (DataColumn col in dtS.Columns)
            //    {
            //        gvFunction.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
            //        ind++;
            //    }


            //    ProcessGeneral.SetGridColumnHeader(gvFunction.Columns["FunctionCode"], "Code", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
            //    ProcessGeneral.SetGridColumnHeader(gvFunction.Columns["FunctionName"], "Name", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
            //    ProcessGeneral.SetGridColumnHeader(gvFunction.Columns["FunctionDescription"], "Description", DefaultBoolean.True, HorzAlignment.Near, FixedStyle.None);
            //    ProcessGeneral.HideVisibleColumnsGridView(gvFunction, false, "FunctionDescription", "UserName");
            //}
            gvFunction.BeginUpdate();

            //gc.DataSource = null;
            //gv.Columns.Clear();

            gcFunction.DataSource = dtS;
            ProcessGeneral.SetGridColumnHeader(gvFunction.Columns["FunctionCode"], "Code", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvFunction.Columns["FunctionName"], "Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvFunction.Columns["FunctionDescription"], "Description", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvFunction.Columns["IsActive"], "Active", DefaultBoolean.True, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.HideVisibleColumnsGridView(gvFunction, false, "FunctionDescription", "UserName", "CNYSYS11PK", "RowState");
            gvFunction.Columns["FunctionCode"].SortOrder = ColumnSortOrder.Ascending;
            gvFunction.BestFitColumns();
            gvFunction.EndUpdate();


            //// đưa dữ liệu vào Dictionary chính
            //_dicReceipient.Clear();
            //foreach (DataRow dr in dtS.Rows)
            //{
            //    Int64 CNYSYS11PK = ProcessGeneral.GetSafeInt64(dr["CNYSYS11PK"]);
            //    DataTable dtReceipientAdd;
            //    if (!_dicUser.TryGetValue(CNYSYS11PK, out dtReceipientAdd))
            //    {
            //        dtReceipientAdd = TableReceipientTemp();
            //    }

            //    _dicReceipient.Add(CNYSYS11PK, new Cls_UserReceipient
            //    {
            //        dt = dtReceipientAdd
            //    });

            //}

            //cho du lieu ve dong dau tien luoi de load User Receipient  
            SetFocusOnGridFristRow_Function(gvFunction);

        }

        private void LoadDataInGridViewStatus(Int64 CNYSYS11PK)
        {
            //User Recepient in Function
            DataTable dtS;
            if (!_dicStatus.TryGetValue(CNYSYS11PK, out dtS))
            {
                dtS = TableStatusTemp();
            }

            if (!_dicStatus.ContainsKey(_CNYSYS11PK))
            {
                DataTable dtTemp = TableStatusTemp();
                _dicStatus.Add(_CNYSYS11PK, dtTemp);
            }

            gvStatus.BeginUpdate();

            gcStatus.DataSource = dtS;
            ProcessGeneral.SetGridColumnHeader(gvStatus.Columns["StatusCode"], "Code", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvStatus.Columns["StatusDescription"], "Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvStatus.Columns["TypeReceipient"], "Receipient Type", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.HideVisibleColumnsGridView(gvStatus, false, "CNYSYS11PK", "CNYSYS11APK", "RowState");
            gvStatus.Columns["StatusCode"].SortOrder = ColumnSortOrder.Ascending;

            //Grid lookup edit
            RepositoryItemGridLookUpEdit _riGridLookUp;
            _riGridLookUp = new RepositoryItemGridLookUpEdit();
            _riGridLookUp.DataSource = _inf.TypeReceipient_Load();
            _riGridLookUp.DisplayMember = "Description";
            _riGridLookUp.ValueMember = "Code";
            _riGridLookUp.NullText = "";
            _riGridLookUp.PopulateViewColumns();
            _riGridLookUp.BestFitMode = BestFitMode.BestFitResizePopup; //tự động co giãn cột theo dữ liệu

            //_riGridLookUp.View.Columns["Description"].Caption = "Diễn giải";
            //_riGridLookUp.View.Columns["Code"].Visible = false;
            _riGridLookUp.View.OptionsView.ShowAutoFilterRow = true;
            _riGridLookUp.View.BestFitColumns();
            gvStatus.Columns["TypeReceipient"].ColumnEdit = _riGridLookUp;
            gvStatus.Columns["TypeReceipient"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;


            gvStatus.BestFitColumns();
            gvStatus.EndUpdate();


        }

        private void LoadDataInGridViewUiR(Int64 CNYSYS11APK)
        {
            //User Recepient in Function


            DataTable dtS;
            if (!_dicUser.TryGetValue(CNYSYS11APK, out dtS))
            {
                dtS = TableReceipientTemp();
            }

            if (!_dicUser.ContainsKey(_CNYSYS11APK))
            {
                DataTable dtTemp = TableReceipientTemp();
                _dicUser.Add(_CNYSYS11APK, dtTemp);
            }
            //if (!_dicReceipient.TryGetValue(CNYSYS11PK, out lUserReceipient))
            //{
            //    dtS = TableReceipientTemp();
            //}
            //else
            //{
            //    dtS = lUserReceipient.dt;
            //}

            gvReceipient.BeginUpdate();

            //gc.DataSource = null;
            //gv.Columns.Clear();

            gcReceipient.DataSource = dtS;
            ProcessGeneral.SetGridColumnHeader(gvReceipient.Columns["DepartmentName"], "Department", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvReceipient.Columns["UserID"], "UserID", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvReceipient.Columns["UserName"], "User Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvReceipient.Columns["FullName"], "Full Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvReceipient.Columns["Email"], "Email", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.HideVisibleColumnsGridView(gvReceipient, false, "UserID", "CNYSYS11PK", "CNYSYS11APK", "CNYSYS12PK", "RowState");
            gvReceipient.Columns["DepartmentName"].SortOrder = ColumnSortOrder.Ascending;
            gvReceipient.BestFitColumns();
            gvReceipient.EndUpdate();


        }

        private void SetFocusOnGridFristRow_Function(GridView gv)
        {
            int rHc = 0;

            if (gv.RowCount <= 0 || !gv.IsDataRow(rHc) || gv.IsGroupRow(rHc))
            {
                LoadDataInGridViewStatus(0);
                LoadDataInGridViewUiR(0);
                _CNYSYS11PK = 0;
                _CNYSYS11APK = 0;
                _WorkFunCode = "";
                return;
            }

            _CNYSYS11PK = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(rHc, "CNYSYS11PK"));
            _WorkFunCode = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rHc, "FunctionCode"));
            LoadDataInGridViewStatus(_CNYSYS11PK);
            //LoadDataInGridViewUiR(_CNYSYS11PK);
            SetFocusOnGridFristRow_Status(gvStatus);

        }

        private void SetFocusOnGridFristRow_Status(GridView gv)
        {
            int rHc = 0;

            if (gv.RowCount <= 0 || !gv.IsDataRow(rHc) || gv.IsGroupRow(rHc))
            {
                LoadDataInGridViewUiR(0);
                _CNYSYS11APK = 0;
                return;
            }

            _CNYSYS11APK = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(rHc, "CNYSYS11APK"));
            LoadDataInGridViewUiR(_CNYSYS11APK);

        }


        public void DisplatDetailFocusedRowChanged_Function()
        {

            int rH = gvFunction.FocusedRowHandle;
            if (gvFunction.RowCount == 0)
            {
                LoadDataInGridViewStatus(0);
                LoadDataInGridViewUiR(0);
                _CNYSYS11PK = 0;
                _WorkFunCode = "";
            }
            else
            {
                _CNYSYS11PK = ProcessGeneral.GetSafeInt64(gvFunction.GetRowCellValue(rH, "CNYSYS11PK"));
                _WorkFunCode = ProcessGeneral.GetSafeString(gvFunction.GetRowCellValue(rH, "FunctionCode"));
                LoadDataInGridViewStatus(_CNYSYS11PK);
                LoadDataInGridViewUiR(_CNYSYS11PK);
            }

        }

        public void DisplatDetailFocusedRowChanged_Status()
        {

            int rH = gvStatus.FocusedRowHandle;
            if (gvStatus.RowCount == 0)
            {
                LoadDataInGridViewUiR(0);
                _CNYSYS11APK = 0;
            }
            else
            {
                _CNYSYS11APK = ProcessGeneral.GetSafeInt64(gvStatus.GetRowCellValue(rH, "CNYSYS11APK"));
                LoadDataInGridViewUiR(_CNYSYS11APK);
            }

        }


        #endregion

        #region "Process Grid Function In Role"

        #region "methold"

        private void GridViewTestCustomInit(GridControl gc, GridView gv)
        {

            gc.UseEmbeddedNavigator = true;

            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Append.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            if (gv.Name == "gvFunction" || gv.Name == "gvStatus")
            {
                gv.OptionsBehavior.Editable = true;
            }
            else
            {
                gv.OptionsBehavior.Editable = false;
            }

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
            gv.OptionsView.ShowAutoFilterRow = true;
            gv.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gv.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gv.OptionsNavigation.AutoFocusNewRow = true;
            gv.OptionsNavigation.UseTabKey = true;

            gv.OptionsSelection.MultiSelect = true;
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

            gv.RowStyle += gvTest_RowStyle;
            gv.RowCountChanged += gvTest_RowCountChanged;
            gv.CustomDrawRowIndicator += gvTest_CustomDrawRowIndicator;
            if (gv.Name == "gvFunction")
            {
                gv.ShowingEditor += gvFunction_ShowingEditor;
                gv.KeyDown += gvFunction_KeyDown;
                gv.CellValueChanged += gvFunction_CellValueChanged;
                gv.FocusedRowChanged += gvFunction_FocusedRowChanged;
                gv.FocusedColumnChanged += gvFunction_FocusedColumnChanged;

            }
            else if (gv.Name == "gvReceipient")
            {
                gv.KeyDown += gvReceipient_KeyDown;
            }
            else if (gv.Name == "gvStatus")
            {

                gv.ShowingEditor += gvStatus_ShowingEditor;
                gv.KeyDown += gvStatus_KeyDown;
                gv.CellValueChanged += gvStatus_CellValueChanged;
                gv.FocusedRowChanged += gvStatus_FocusedRowChanged;
                gv.FocusedColumnChanged += gvStatus_FocusedColumnChanged;
                gv.ShownEditor += gvStatus_ShownEditor;
                gv.RowCellStyle += gvStatus_RowCellStyle;
            }
            gc.ForceInitialize();
        }




        #endregion

        #region "gridview event"
        private void gvStatus_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;
            if (e.RowHandle == GridControl.AutoFilterRowHandle) return;
            string fieldName = e.Column.FieldName;
            switch (fieldName) //Lay ra ten cot trong gridview
            {
                case "TypeReceipient":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                    //e.Appearance.ForeColor = Color.DeepPink;
                    break;
                default:
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
            }

        }
        private void gvStatus_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn col = gv.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;

            switch (fieldName)
            {
                case "TypeReceipient":
                    e.Cancel = false;
                    break;
                    ;
                default:
                    e.Cancel = true;
                    break;

            }


        }

        private void gvStatus_ShownEditor(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            int rh = gv.FocusedRowHandle;
            if (rh == GridControl.AutoFilterRowHandle) return;

            BaseEdit baseEdit = gv.ActiveEditor;
            if (baseEdit == null) return;
            GridLookUpEdit gridLookUpEditMainAE = baseEdit as GridLookUpEdit;
            if (gridLookUpEditMainAE != null)
            {
                gridLookUpEditMainAE.EditValueChanged += GridLookUpEditMainAE_EditValueChanged;
                return;
            }
        }

        private void GridLookUpEditMainAE_EditValueChanged(object sender, EventArgs e)
        {

            GridLookUpEdit gridLookUpEditMainAE = sender as GridLookUpEdit;
            if (gridLookUpEditMainAE == null) return;

            gvStatus.CloseEditor();
            gridLookUpEditMainAE.EditValueChanged -= GridLookUpEditMainAE_EditValueChanged;
            //gvStatus.UpdateCurrentRow();
        }
        private void gvStatus_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["RowState"]));
            int CurrentRow = gv.FocusedRowHandle;//Lay ra ten dong hien tai
            if (rowState == DataStatus.Unchange.ToString())
            {
                gv.SetRowCellValue(CurrentRow, "RowState", DataStatus.Update.ToString());
            }

        }
        private void gvStatus_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged_Status();
            }
        }

        private void gvStatus_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged_Status();
            }

        }

        private void gvFunction_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (e.KeyCode == Keys.F8)
            {
                btnRemoveFunction_Click(sender, e);
            }

        }

        private void gvStatus_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (e.KeyCode == Keys.F8)
            {
                btnRemoveStatus_Click(sender, e);
            }

        }
        private void gvReceipient_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (e.KeyCode == Keys.F8)
            {
                btnRemoveReceipient_Click(sender, e);
            }

        }
        private void gvFunction_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn col = gv.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;

            switch (fieldName)
            {
                case "IsActive":
                    e.Cancel = false;
                    break;
                default:
                    e.Cancel = true;
                    break;

            }


        }
        private void gvFunction_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["RowState"]));
            int CurrentRow = gv.FocusedRowHandle;//Lay ra ten dong hien tai
            if (rowState == DataStatus.Unchange.ToString())
            {
                gv.SetRowCellValue(CurrentRow, "RowState", DataStatus.Update.ToString());
            }

        }
        private void gvFunction_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged_Function();
                SetFocusOnGridFristRow_Status(gvStatus);
            }
        }

        private void gvFunction_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged_Function();
                SetFocusOnGridFristRow_Status(gvStatus);
            }

        }
        private void gvTest_RowCountChanged(object sender, EventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvTest_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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



        private void gvTest_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;


            }


        }



        #endregion

        #endregion

        #region "Button CLick Event"
        private void btnRemoveMember_Click(object sender, EventArgs e)
        {
            if (gvMember.SelectedRowsCount == 0) return;
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to remove user {0} leave group selected ? (yes/No) \n Note:You could not restore this record!", txtUserName.Text.Trim()),
                   "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return;
            for (int i = 0; i < gvMember.SelectedRowsCount; i++)
            {
                DataRow dr = gvMember.GetDataRow(gvMember.GetSelectedRows()[i]);
                lMemberDel.Add(ProcessGeneral.GetSafeString(dr["GroupUserCode"]));
                dr.Delete();
            }
            ((DataTable)gcMember.DataSource).AcceptChanges();
            BestFitColumnsWithImage_Member();

        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            var dtS = (DataTable)gcMember.DataSource;
            DataTable dtCondition;
            if (dtS == null) return;
            var query = dtS.AsEnumerable()
                .Select(p => new { GroupUserCode = ProcessGeneral.GetSafeString(p["GroupUserCode"]) }).ToList();
            dtCondition = query.Any() ? query.CopyToDataTableNew() : TableTemplateGroupRole();

            DataTable dtSource = _inf.UserMemberLoadRoleSelected(dtCondition);
            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> l = new List<string>();
            DataTable dtMember = gcMember.DataSource as DataTable;

            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "GroupUserCode",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
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
                    FixStyle = GridFixedCol.None,
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
                    FixStyle = GridFixedCol.None,
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
                    FixStyle = GridFixedCol.None,
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
                Size = new Size(500, 600),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Role Listing",
                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true,
                MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
            };

            f.OnTransferData += (s1, e1) =>
            {

                List<DataRow> lDr = e1.ReturnRowsSelected;
                foreach (DataRow dr in lDr)
                {
                    //l.Add(ProcessGeneral.GetSafeString(dr["GroupUserCode"]));
                    DataRow drMb = dtMember.NewRow();
                    drMb["GroupUserCode"] = dr["GroupUserCode"];
                    drMb["GroupUserName"] = dr["GroupUserName"];
                    drMb["GroupUserDescription"] = dr["GroupUserDescription"];
                    drMb["Priority"] = dr["Priority"];
                    drMb["RowState"] = DataStatus.Insert.ToString();

                    dtMember.Rows.Add(drMb);
                    dtMember.AcceptChanges();
                }

                BestFitColumnsWithImage_Member();

            };
            f.ShowDialog();

        }


        private void btnRemoveFunction_Click(object sender, EventArgs e)
        {
            if (gvFunction.SelectedRowsCount == 0) return;
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to remove user {0} leave Function selected ? (yes/No) \n Note:You could not restore this record!", txtUserName.Text.Trim()),
                   "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return;
            for (int i = 0; i < gvFunction.SelectedRowsCount; i++)
            {
                DataRow dr = gvFunction.GetDataRow(gvFunction.GetSelectedRows()[i]);
                lPkFunctionDel.Add(ProcessGeneral.GetSafeInt64(dr["CNYSYS11PK"]));

                dr.Delete();
            }
            ((DataTable)gcFunction.DataSource).AcceptChanges();
            gvFunction.BestFitColumns();
            //cho du lieu ve dong dau tien luoi de load User Receipient  
            SetFocusOnGridFristRow_Function(gvFunction);
        }


        private Int64 GetPkWhenInsert()
        {
            return ProcessGeneral.GetNextId("CNYSYS11");
        }
        private Int64 GetPk11AWhenInsert()
        {
            return ProcessGeneral.GetNextId("CNYSYS11A");
        }
        private void btnAddFunction_Click(object sender, EventArgs e)
        {
            var dtS = (DataTable)gcFunction.DataSource;
            DataTable dtCondition;
            if (dtS == null) return;
            var query = dtS.AsEnumerable()
                .Select(p => new { Code = ProcessGeneral.GetSafeString(p["FunctionCode"]) }).ToList();
            dtCondition = query.Any() ? query.CopyToDataTableNew() : TableTemplateCode();

            DataTable dtSource = _inf.FunctionLoadSelected(dtCondition);
            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> l = new List<string>();
            DataTable dtFunction = gcFunction.DataSource as DataTable;
            //Int64 pk;
            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "FunctionCode",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
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
                    FieldName = "FunctionName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
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
                    FieldName = "FunctionDescription",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
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
                Size = new Size(400, 500),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Function Listing",
                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true,
                MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
            };

            f.OnTransferData += (s1, e1) =>
            {

                List<DataRow> lDr = e1.ReturnRowsSelected;
                foreach (DataRow dr in lDr)
                {
                    _CNYSYS11PK = GetPkWhenInsert();
                    if (!_dicStatus.ContainsKey(_CNYSYS11PK))
                    {
                        DataTable dtTemp = TableStatusTemp();
                        _dicStatus.Add(_CNYSYS11PK, dtTemp);
                    }
                    //if (!_dicUser.ContainsKey(_CNYSYS11PK))
                    //{
                    //    DataTable dtTemp = TableReceipientTemp();
                    //    _dicUser.Add(_CNYSYS11PK, dtTemp);
                    //}

                    DataRow drMb = dtFunction.NewRow();
                    drMb["FunctionCode"] = dr["FunctionCode"];
                    drMb["FunctionName"] = dr["FunctionName"];
                    drMb["FunctionDescription"] = dr["FunctionDescription"];
                    drMb["IsActive"] = 1;
                    drMb["UserName"] = txtUserName.Text.Trim();
                    drMb["CNYSYS11PK"] = _CNYSYS11PK;
                    _WorkFunCode = ProcessGeneral.GetSafeString(dr["FunctionCode"]);
                    drMb["RowState"] = DataStatus.Insert.ToString();

                    dtFunction.Rows.Add(drMb);
                    dtFunction.AcceptChanges();
                }

                gvFunction.BestFitColumns();

            };
            f.ShowDialog();

        }

        private void btnRemoveStatus_Click(object sender, EventArgs e)
        {
            if (gvStatus.SelectedRowsCount == 0) return;
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to remove user {0} leave Status selected ? (yes/No) \n Note:You could not restore this record!", txtUserName.Text.Trim()),
                   "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return;
            Int64 CNYSYS11APK;
            for (int i = 0; i < gvStatus.SelectedRowsCount; i++)
            {
                DataRow dr = gvStatus.GetDataRow(gvStatus.GetSelectedRows()[i]);
                CNYSYS11APK = ProcessGeneral.GetSafeInt64(dr["CNYSYS11APK"]);
                lPkStatusDel.Add(CNYSYS11APK);

                dr.Delete();
            }
    ((DataTable)gcStatus.DataSource).AcceptChanges();
            gvStatus.BestFitColumns();

        }

        private void btnAddStatus_Click(object sender, EventArgs e)
        {
            if (_CNYSYS11PK <= 0)
            {
                XtraMessageBox.Show("Select the previous Function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dtS = (DataTable)gcStatus.DataSource;
            DataTable dtCondition;
            if (dtS == null) return;
            var query = dtS.AsEnumerable()
                .Select(p => new { PK = ProcessGeneral.GetSafeInt64(p["StatusCode"]) }).ToList();
            dtCondition = query.Any() ? query.CopyToDataTableNew() : TableTemplatePK();

            DataTable dtSource = _inf.StatusLoadSelected(dtCondition, _WorkFunCode);
            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            List<string> l = new List<string>();
            DataTable dtStatus = gcStatus.DataSource as DataTable;
            //Int64 pk;
            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "Code",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
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
                    FieldName = "Description",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
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
                Size = new Size(400, 600),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Status Listing",
                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true,
                MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
            };

            f.OnTransferData += (s1, e1) =>
            {

                List<DataRow> lDr = e1.ReturnRowsSelected;
                foreach (DataRow dr in lDr)
                {
                    _CNYSYS11APK = GetPk11AWhenInsert();

                    if (!_dicUser.ContainsKey(_CNYSYS11APK))
                    {
                        DataTable dtTemp = TableReceipientTemp();
                        _dicUser.Add(_CNYSYS11APK, dtTemp);
                    }


                    DataRow drMb = dtStatus.NewRow();
                    drMb["StatusCode"] = dr["Code"];
                    drMb["StatusDescription"] = dr["Description"];
                    drMb["CNYSYS11PK"] = _CNYSYS11PK;
                    drMb["TypeReceipient"] = 0;
                    drMb["CNYSYS11APK"] = _CNYSYS11APK;
                    drMb["RowState"] = DataStatus.Insert.ToString();

                    dtStatus.Rows.Add(drMb);
                    dtStatus.AcceptChanges();
                }

                gvStatus.BestFitColumns();

            };
            f.ShowDialog();

        }



        private void btnRemoveReceipient_Click(object sender, EventArgs e)
        {
            if (gvReceipient.SelectedRowsCount == 0) return;
            DialogResult dlResult = XtraMessageBox.Show(string.Format("Do you want to remove user {0} leave Receipient selected ? (yes/No) \n Note:You could not restore this record!", txtUserName.Text.Trim()),
                   "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult == DialogResult.No) return;
            Int64 CNYSYS12PK;
            for (int i = 0; i < gvReceipient.SelectedRowsCount; i++)
            {
                DataRow dr = gvReceipient.GetDataRow(gvReceipient.GetSelectedRows()[i]);
                CNYSYS12PK = ProcessGeneral.GetSafeInt64(dr["CNYSYS12PK"]);
                lPkReceipientDel.Add(CNYSYS12PK);

                dr.Delete();
            }
            ((DataTable)gcReceipient.DataSource).AcceptChanges();
            gvReceipient.BestFitColumns();

        }

        private void btnAddReceipient_Click(object sender, EventArgs e)
        {
            if (_CNYSYS11APK <= 0)
            {
                XtraMessageBox.Show("Select the previous Status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dtS = (DataTable)gcReceipient.DataSource;
            DataTable dtCondition;
            if (dtS == null) return;
            var query = dtS.AsEnumerable()
                .Select(p => new { PK = ProcessGeneral.GetSafeInt64(p["UserID"]) }).ToList();
            dtCondition = query.Any() ? query.CopyToDataTableNew() : TableTemplatePK();

            DataTable dtSource = _inf.ReceipientLoadSelected(dtCondition, _userId);
            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            List<string> l = new List<string>();
            DataTable dtReceipient = gcReceipient.DataSource as DataTable;
            //Int64 pk;
            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Department Name",
                    FieldName = "DepartmentName",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
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
                    Caption = @"UserID",
                    FieldName = "UserID",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"User Name",
                    FieldName = "UserName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
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
                    Caption = @"Full Name",
                    FieldName = "FullName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
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
                    Caption = @"Email",
                    FieldName = "Email",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 3,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
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
                Size = new Size(600, 600),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"User Listing",
                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true,
                MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
            };

            f.OnTransferData += (s1, e1) =>
            {

                List<DataRow> lDr = e1.ReturnRowsSelected;
                foreach (DataRow dr in lDr)
                {

                    DataRow drMb = dtReceipient.NewRow();
                    drMb["DepartmentName"] = dr["DepartmentName"];
                    drMb["UserID"] = dr["UserID"];
                    drMb["UserName"] = dr["UserName"];
                    drMb["FullName"] = dr["FullName"];
                    drMb["Email"] = dr["Email"];
                    drMb["CNYSYS11PK"] = _CNYSYS11PK;
                    drMb["CNYSYS11APK"] = _CNYSYS11APK;
                    drMb["CNYSYS12PK"] = 0;
                    drMb["RowState"] = DataStatus.Insert.ToString();

                    dtReceipient.Rows.Add(drMb);
                    dtReceipient.AcceptChanges();
                }

                gvReceipient.BestFitColumns();

            };
            f.ShowDialog();

        }
        #endregion

    }
}
