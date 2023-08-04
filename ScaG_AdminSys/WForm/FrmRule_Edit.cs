using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_AdminSys.Info;
using CNY_AdminSys.Properties;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using GridSumCol = DevExpress.Data.SummaryItemType;

namespace CNY_AdminSys.WForm
{
    public partial class FrmRule_Edit : FrmBase
    {
        #region "Property"


        readonly Inf_Rule _inf = new Inf_Rule();
        WaitDialogForm _dlg;

        private readonly string _ruleCode;
        private readonly string[] _arrFieldAllowDelete = { "AdvanceFunction", "SpecialFunction" };
        private readonly Form _fMain;
        private bool _isEditCellEvent = true;
   
        #endregion

        #region "Contructor"
        public FrmRule_Edit(string ruleCode, string ruleName, Form parentForm)
        {
            InitializeComponent();

            this._fMain = parentForm;
            _ruleCode = ruleCode;

            this.Text = string.Format("Building Treeview Menu ({0}-{1})", _ruleCode, ruleName);
            this.Load += Form_Load;
            this.FormClosed += Form_Closed;
            this.FormClosing += Form_Closing;
   
         
     

            InitTreeList(tlMain);
            InitTreeListMenu(tlMenu);
       
            chkOverride.Checked = true;


           

            DragDropEvents[] arrDragDropEvents = { dragDropEvents1, dragDropEvents2};





            foreach (DragDropEvents dragDropEvents in arrDragDropEvents)
            {

                dragDropEvents.DragDrop += Behavior_DragDrop;
                dragDropEvents.DragOver += Behavior_DragOver;


            }


        }



        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        private bool _checkKeyDown;
        #endregion








        #region "Form Event"
        private void Form_Load(object sender, EventArgs e)
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
     
            AllowCancel = false;




            AllowFind = false;
            AllowPrint = false;
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = true;
       
            AllowGenerate = false;
            AllowCombine = false;
            AllowCheck = false;
            AllowCopyObject = true;
            AllowSave = true;
            AllowRefresh = true;
            AllowClose = true;

            EnableSave = true;
            EnableRefresh = true;
            EnableClose = true;
            // Check Role
            EnableCopyObject = true;
            EnableRangSize = true;
            SetCaptionRangSize = "Duplicate";

            _dlg = new WaitDialogForm("");

            LoadDataTreeViewMenu(tlMenu,_inf.RuleEdit_LoadMenu(_ruleCode));
            LoadDataTreeView(tlMain, _inf.RuleEdit_Load(_ruleCode));
            _dlg.Close();
            _fMain.Enabled = false;

        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
           SaveData(true);
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {

           
            _fMain.Enabled = true;
            _fMain.Activate();
        }
        #endregion

      

        #region "override button menubar click"

        protected override void PerformCopy()
        {
            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.RuleEdit_LoadPermissionGroupCopy(_ruleCode);
            _dlg.Close();

            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string strPermissionGroup = "";
            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "PermisionGroupCode",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Name",
                    FieldName = "PermisionGroupName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Description",
                    FieldName = "PermisionGroupDescription",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
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
                    SummayType = GridSumCol.None,
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
                Text = @"Rule Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                strPermissionGroup = ProcessGeneral.GetSafeString(lDr[0]["PermisionGroupCode"]);
            };
            f.ShowDialog();


            if (string.IsNullOrEmpty(strPermissionGroup)) return;
            _dlg = new WaitDialogForm();
            bool isSuccess = _inf.RuleEdit_CopyPermission(_ruleCode, strPermissionGroup);
            _dlg.Close();
            if (isSuccess)
            {
                _dlg = new WaitDialogForm("");

                LoadDataTreeViewMenu(tlMenu, _inf.RuleEdit_LoadMenu(_ruleCode));
                LoadDataTreeView(tlMain, _inf.RuleEdit_Load(_ruleCode));
                _dlg.Close();

                XtraMessageBox.Show(string.Format("Copy Successfull From Permission Group Code {0} To Permission Group Code {1}..!",
                        strPermissionGroup, _ruleCode), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            else
            {
                XtraMessageBox.Show(string.Format("Copy UnSuccessfull From Permission Group Code {0} To Permission Group Code {1}..!",
                        strPermissionGroup, _ruleCode),
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            
        }

        protected override void PerformRangeSize()
        {

            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.RuleEdit_LoadPermissionGroupByGC(_ruleCode);
            _dlg.Close();

            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string strPermissionGroup = "";
            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "PermisionGroupCode",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Name",
                    FieldName = "PermisionGroupName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Description",
                    FieldName = "PermisionGroupDescription",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
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
                    SummayType = GridSumCol.None,
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
                Text = @"Rule Listing",
                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                strPermissionGroup = ProcessGeneral.GetStringPkDataTransferForm(lDr, "PermisionGroupCode", ",", true);
            };
            f.ShowDialog();


            if (string.IsNullOrEmpty(strPermissionGroup)) return;
            string[] strPgCopy = strPermissionGroup.Split(',');
            string strGroupSuccess = "";
            string strGroupError = "";
            _dlg = new WaitDialogForm();
            foreach (string s in strPgCopy)
            {
                if (string.IsNullOrEmpty(s)) continue;
                if (_inf.RuleEdit_DuplicatePermission(_ruleCode, s) == 1)
                {
                    strGroupSuccess = string.Format("{0}{1},", strGroupSuccess, s);
                }
                else
                {
                    strGroupError = string.Format("{0}{1},", strGroupError, s);
                }
            }
            _dlg.Close();
            if (!string.IsNullOrEmpty(strGroupSuccess))
            {
                strGroupSuccess = strGroupSuccess.Substring(0, strGroupSuccess.Length - 1);
                XtraMessageBox.Show(string.Format("Duplicate Successfull From Permission Group Code {0} To Permission Group Code {1} .!!!",
                        _ruleCode, strGroupSuccess),
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            if (!string.IsNullOrEmpty(strGroupError))
            {
                strGroupError = strGroupError.Substring(0, strGroupError.Length - 1);
                XtraMessageBox.Show(string.Format("Duplicate UnSuccessfull From Permission Group Code {0} To Permission Group Code {1}..! \n Because Group Code {1} was authorized",
                        _ruleCode, strGroupError),
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
        }
        
      

        protected override void PerformSave()
        {
           
          SaveData(false);


        }
        /// <summary>
        /// Perform when click Refresh button
        /// </summary>
        protected override void PerformRefresh()
        {
            _dlg = new WaitDialogForm("");

            LoadDataTreeViewMenu(tlMenu, _inf.RuleEdit_LoadMenu(_ruleCode));
            LoadDataTreeView(tlMain, _inf.RuleEdit_Load(_ruleCode));
            _dlg.Close();
         
        }



        #endregion


      

    


     
     




       

        #region "Proccess Treeview"




        private void LoadDataTreeView(TreeList tl, DataTable dt)
        {

            bool isShowFind = false;
            string findPanelText = "";
            if (tl.FindPanelVisible)
            {
                isShowFind = true;
                findPanelText = tl.FindFilterText;
                tl.HideFindPanel();
            }


         


            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";


            tl.BeginUpdate();
            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "RowState", "MenuCode", "IDCHECK", "SortOrderNode"
            );
         

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SortOrderNode"], "Sort Order", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormName"], "Menu Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormCode"], "Form Code", false, HorzAlignment.Near, TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProjectCode"], "Project Code", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleInsert"], "Insert", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleUpdate"], "Update", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleDelete"], "Delete", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleView"], "View", false, HorzAlignment.Center,TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["CheckAdvanceFunction"], "Not Check AF", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["AdvanceFunction"], "Advance Function (F4)", false, HorzAlignment.Near,TreeFixedStyle.None, "");
            tl.Columns["AdvanceFunction"].ImageIndex = 0;
            tl.Columns["AdvanceFunction"].ImageAlignment = StringAlignment.Near;



            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SpecialFunction"], "Special Function (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["SpecialFunction"].ImageIndex = 0;
            tl.Columns["SpecialFunction"].ImageAlignment = StringAlignment.Near;
            tl.ExpandAll();

            tl.BestFitColumns();
            tl.Columns["FormName"].Width += 30;

            tl.ForceInitialize();

            tl.BeginSort();
            tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();


            if (isShowFind)
            {
                tl.ShowFindPanelTreeList(findPanelText);
            }
        }



        private DataTable TableTreeviewTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("MenuCode", typeof(string));
            dt.Columns.Add("FormName", typeof(string));
            dt.Columns.Add("FormCode", typeof(string));
            dt.Columns.Add("ProjectCode", typeof(string));
            dt.Columns.Add("RoleInsert", typeof(bool));
            dt.Columns.Add("RoleUpdate", typeof(bool));
            dt.Columns.Add("RoleDelete", typeof(bool));
            dt.Columns.Add("RoleView", typeof(bool));
            dt.Columns.Add("CheckAdvanceFunction", typeof(bool));
            dt.Columns.Add("AdvanceFunction", typeof(string));
            dt.Columns.Add("SpecialFunction", typeof(string));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("SortOrderNode", typeof(Int32));
            dt.Columns.Add("ChildPK", typeof(string));
            dt.Columns.Add("ParentPK", typeof(string));
            dt.Columns.Add("IDCHECK", typeof(Int64));
            return dt;
        }

        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.folder_yellow_Close_icon);
            imgLt.Images.Add(Resources.folder_yellow_open_icon);
            imgLt.Images.Add(Resources.Document_txt_icon);
            return imgLt;
        }

      
        private void InitTreeList(TreeList treeList)
        {


            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = false;

            treeList.OptionsBehavior.Editable = true;
            treeList.OptionsView.ShowColumns = true;
            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;
            treeList.OptionsView.AutoWidth = false;
            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;
            treeList.StateImageList = GetImageListDisplayTreeView();
            treeList.OptionsBehavior.AutoChangeParent = false;
            treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            treeList.OptionsBehavior.AutoNodeHeight = true;

            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.OptionsView.ShowIndentAsRowStyle = true;
         



            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
             new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,
                

            };


            //treeList.AllowDrop = false;
            //treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Multiple;

            //treeList.OptionsDragAndDrop.AcceptOuterNodes = false;
          
            //treeList.OptionsDragAndDrop.CanCloneNodesOnDrop = false;



            treeList.GetStateImage += TreeList_GetStateImage;
            treeList.ShowingEditor += TreeList_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;





            treeList.NodeCellStyle += TreeList_NodeCellStyle;
  

            treeList.KeyDown += TreeList_KeyDown;
            treeList.EditorKeyDown += TreeList_EditorKeyDown;
          
            treeList.CellValueChanged += TreeList_CellValueChanged;







            //tlMain.BeforeDropNode += TreeList_BeforeDropNode;
            //tlMain.BeforeDragNode += TreeList_BeforeDragNode;
            //tlMain.DragOver += TreeList_DragOver;
            //tlMain.DragDrop += TreeList_DragDrop;
            //tlMain.DragLeave += TreeList_DragLeave;
            //tlMain.GiveFeedback += TreeList_GiveFeedback;
           

            LoadDataTreeView(treeList, TableTreeviewTemplate());




        }

      


        private void TreeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (!_isEditCellEvent) return;
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            if (e.Column == null) return;
            string fieldName = e.Column.FieldName;

            CalEditValueChangedEvent(node);




            switch (fieldName)
            {
                case "RoleInsert":
                case "RoleUpdate":
                case "RoleDelete":
                    {

                        if (ProcessGeneral.GetSafeBool(e.Value))
                        {
                            _isEditCellEvent = false;
                            node.SetValue("RoleView", true);
                            _isEditCellEvent = true;

                        }
            


                    }
                    break;
            }

        }





       
        



        #region "Process Key Down"

        private void TreeList_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDown)
            {
                TreeList_KeyDown(sender, e);
            }
            _checkKeyDown = false;
        }

        private void TreeList_KeyDown(object sender, KeyEventArgs e)
        {

            TreeList tl = (TreeList) sender;
            if (tl == null) return;
            _checkKeyDown = true;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;

            string fieldName = col.FieldName;


            #region "Process F8 Key"

            if (e.KeyCode == Keys.F8)
            {
                List<TreeListNode> lNodeDel = tl.GetSelectedCells().Select(p => p.Node).Distinct().ToList();
                if (lNodeDel.Count <= 0) return;
                RemoveTreeListNode(lNodeDel);
                return;
            }





            #endregion

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







            #region "Process Delete key"



            if (e.KeyCode == Keys.Delete)
            {
                TextEdit tE = tl.ActiveEditor as TextEdit;
                if (tE != null) return;
                ProcessDeleteKey(tl);
                return;
            }

            #endregion

         


            #region "F4 Key"

            if (e.KeyCode == Keys.F4)
            {
                switch (fieldName)
                {
                    case "AdvanceFunction": //set value
                        ShowListAdvanceFunctionF4OnTree(node, fieldName);
                        break;
                    case "SpecialFunction": //set value
                        ShowListSpecialFunctionF4OnTree(node, fieldName);
                        break;
                }
                return;

            }
            #endregion

          

        }




        #region "F4 Key"
        private void ShowListAdvanceFunctionF4OnTree(TreeListNode node, string fieldName)
        {


            DataTable dtSource = _inf.RuleEdit_LoadAdvanceFunction(ProcessGeneral.GetSafeString(node.GetValue("MenuCode")));

            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "Code",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 30,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
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
                    IncreaseWdith = 30,
                    SummayType = GridSumCol.None,
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
                Size = new Size(400, 400),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Advacne Function Listing",
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
                node.SetValue(fieldName, ProcessGeneral.GetStringPkDataTransferForm(lDr, "Code", ",", true));
                CalEditValueChangedEvent(node);
              






            };
            f.ShowDialog();
        }


        private void ShowListSpecialFunctionF4OnTree(TreeListNode node, string fieldName)
        {


            DataTable dtSource = _inf.RuleEdit_LoadSpecialFunction(ProcessGeneral.GetSafeString(node.GetValue("FormCode")));

            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "Code",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 30,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
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
                    IncreaseWdith = 30,
                    SummayType = GridSumCol.None,
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
                Size = new Size(400, 400),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Special Function Listing",
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
                node.SetValue(fieldName, ProcessGeneral.GetStringPkDataTransferForm(lDr, "Code", ",", true));
                CalEditValueChangedEvent(node);
            





            };
            f.ShowDialog();
        }
        #endregion






        #region "Delete Key"

        private void ProcessDeleteKey(TreeList tl)
        {
            int topIndex = tl.TopVisibleNodeIndex;
            var q1 = tl.GetSelectedCells().ToList();

        
            //ProcessGeneral.GetDescriptionInString()

     


            var qField = q1.Select(p => p.Column.FieldName).Distinct().Where(p => _arrFieldAllowDelete.Any(t => t == p)).ToList();


            //SortOrder = p.Column.FieldName.IndexOf("-", StringComparison.Ordinal) > 0 ? 1 : 0,
            List<TreeListNode> tlCell = q1.Select(p => p.Node).Distinct().ToList();



            if (tlCell.Count <= 0 || qField.Count <= 0) return;

          


            tl.LockReloadNodes();




            foreach (TreeListNode nodeUpd in tlCell)
            {
                foreach (string field in qField)
                {
                    nodeUpd.SetValue(field,"");
                }
                CalEditValueChangedEvent(nodeUpd);
            }






            tl.UnlockReloadNodes();


            tl.BeginUpdate();
         
            tl.TopVisibleNodeIndex = topIndex;
            tl.EndUpdate();



        }



        #endregion

        private void CalEditValueChangedEvent(TreeListNode node)
        {
            string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState"));
            if (rowState == DataStatus.Unchange.ToString())
            {
                node.SetValue("RowState", DataStatus.Update.ToString());
            }
        }

       
  


    
        private Int32 GetMaxSortOrderValueOnNode(TreeList tl,TreeListNode node)
        {
            var q1 = node == null ? tl.Nodes : node.Nodes;
            if (q1.Count <= 0) return 1;
            return q1.Select(p => ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode"))).Max() + 1;
        }
      
        private TreeListNode AddNewNodeMain( TreeListNode parentNode, TreeListNode dataNode, Int32 sortIndex)
        {
            string parentPk = "";
            if (parentNode != null)
            {
                parentPk = ProcessGeneral.GetSafeString(parentNode.GetValue("ChildPK"));
            }

            string rowState = ProcessGeneral.GetSafeString(dataNode.GetValue("RowState"));
            if (string.IsNullOrEmpty(rowState))
            {
                rowState = DataStatus.Insert.ToString();
            }
            else if(rowState == DataStatus.Unchange.ToString())
            {
                rowState = DataStatus.Update.ToString();
            }
            TreeListNode node=tlMain.AppendNode(new []
            {
                dataNode.GetValue("MenuCode"),
                dataNode.GetValue("FormName"),
                dataNode.GetValue("FormCode"),
                dataNode.GetValue("ProjectCode"),
                false,
                false,
                false,
                false,
                false,
                "",
                "",
                rowState,
                sortIndex,
                dataNode.GetValue("MenuCode"),
                parentPk,
                dataNode.GetValue("IDCHECK"),

            },parentNode);


            return node;
        }









        #endregion








        private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
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

        //  private int updateRmSourceNo = 1;
        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = (TreeList) sender;
            if (tl == null) return;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "MenuCode":
                case "FormName":
                case "FormCode":
                case "ProjectCode":
                case "AdvanceFunction":
                case "SpecialFunction":
                    e.Cancel = true;
                    break;
                case "RoleView":
                    e.Cancel = ProcessGeneral.GetSafeBool(node.GetValue("RoleInsert")) || ProcessGeneral.GetSafeBool(node.GetValue("RoleUpdate")) || ProcessGeneral.GetSafeBool(node.GetValue("RoleDelete"));
                    break;
                case "RoleInsert":
                case "RoleUpdate":
                case "RoleDelete":
                case "CheckAdvanceFunction":
                    e.Cancel = false;
                    break;
                default:
                    e.Cancel = true;
                    break;

            }




        }
        private void TreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
      
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            if (node.HasChildren)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkGreen;
            }

       
            switch (fieldName)
            {
                case "RoleInsert":
                case "RoleUpdate":
                case "RoleDelete":
                case "RoleView":
                case "CheckAdvanceFunction":
               
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "AdvanceFunction":
                case "SpecialFunction":
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
            }

        



        }

        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {

            if (e.Node.HasChildren)//|| e.Node.ParentNode == null
            {
                e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
            }
            else
            {
                e.NodeImageIndex = 2;
            }
        }







        #endregion













  




        #region "Process TreeList Event Interaction With Databalse"



        private void SaveData(bool isClosedForm)
        {
            if (!chkOverride.Focused)
            {
                chkOverride.SelectNextControl(ActiveControl, true, true, true, true);
                chkOverride.Focus();
            }

            List<TreeListNode> lNode = tlMain.GetAllNodeTreeList();
            DataTable dtIns = TableTreeviewUpdateDBTemplate();
            DataTable dtUpd = TableTreeviewUpdateDBTemplate();
            DataTable dtDel = TablePKTemplate();
            var qDel = tlMenu.Nodes.Select(p => ProcessGeneral.GetSafeInt64(p.GetValue("IDCHECK"))).Where(p => p > 0).Distinct().ToList();
            foreach (Int64 pkDel in qDel)
            {
                dtDel.Rows.Add(pkDel);
            }

            foreach (TreeListNode node in lNode)
            {
                DataStatus rowState = ProcessGeneral.GetDataStatus(ProcessGeneral.GetSafeString(node.GetValue("RowState")));
                if(rowState == DataStatus.Unchange) continue;
                Int64 pkChild = ProcessGeneral.GetSafeInt64(node.GetValue("IDCHECK"));
                string menuCode = ProcessGeneral.GetSafeString(node.GetValue("MenuCode"));
                bool roleInsert = ProcessGeneral.GetSafeBool(node.GetValue("RoleInsert"));
                bool roleUpdate = ProcessGeneral.GetSafeBool(node.GetValue("RoleUpdate"));
                bool roleDelete = ProcessGeneral.GetSafeBool(node.GetValue("RoleDelete"));
                bool roleView = ProcessGeneral.GetSafeBool(node.GetValue("RoleView"));
                string advanceFunction = ProcessGeneral.GetSafeString(node.GetValue("AdvanceFunction"));
                string menuParent = ProcessGeneral.GetSafeString(node.GetValue("ParentPK"));
                Int32 sortOrder = ProcessGeneral.GetSafeInt(node.GetValue("SortOrderNode"));
                string specialFunction = ProcessGeneral.GetSafeString(node.GetValue("SpecialFunction"));
                bool checkAdvanceFunction = ProcessGeneral.GetSafeBool(node.GetValue("CheckAdvanceFunction"));

                if (rowState == DataStatus.Insert)
                {
                    dtIns.Rows.Add(pkChild,_ruleCode, menuCode, roleInsert, roleUpdate, roleDelete, roleView, advanceFunction, menuParent, sortOrder, specialFunction, checkAdvanceFunction);
                }
                else
                {
                    dtUpd.Rows.Add(pkChild, _ruleCode, menuCode, roleInsert, roleUpdate, roleDelete, roleView, advanceFunction, menuParent, sortOrder, specialFunction, checkAdvanceFunction);
                }


            }

            if (dtIns.Rows.Count <= 0 && dtUpd.Rows.Count <= 0 && dtDel.Rows.Count <= 0)
            {
                if (!isClosedForm)
                {
                    XtraMessageBox.Show("Create TreeView Permission Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }


            if(isClosedForm)
            {
                DialogResult dlResult = XtraMessageBox.Show("Do you want to perform function saving data befor closing form???", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlResult == DialogResult.No) return;
            }
            _dlg = new WaitDialogForm("");
            bool isSucess = _inf.RuleEdit_UpdateDB(dtIns, dtUpd, dtDel, chkOverride.Checked);
            _dlg.Close();
            if (isSucess)
            {
                if (!isClosedForm)
                {
                  
                    _dlg = new WaitDialogForm("");

                    LoadDataTreeViewMenu(tlMenu, _inf.RuleEdit_LoadMenu(_ruleCode));
                    LoadDataTreeView(tlMain, _inf.RuleEdit_Load(_ruleCode));
                    _dlg.Close();
                    XtraMessageBox.Show("Create TreeView Permission Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

             
            }
          
        }


        private DataTable TableTreeviewUpdateDBTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int64));
            dt.Columns.Add("PermisionGroupCode", typeof(string));
            dt.Columns.Add("MenuCode", typeof(string));
            dt.Columns.Add("RoleInsert", typeof(bool));
            dt.Columns.Add("RoleUpdate", typeof(bool));
            dt.Columns.Add("RoleDelete", typeof(bool));
            dt.Columns.Add("RoleView", typeof(bool));
            dt.Columns.Add("AdvanceFunction", typeof(string));
            dt.Columns.Add("MenuParent", typeof(string));
            dt.Columns.Add("SortOrder", typeof(int));
            dt.Columns.Add("SpecialFunction", typeof(string));
            dt.Columns.Add("CheckAdvanceFunction", typeof(bool));
            return dt;
        }
        private DataTable TablePKTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("PK", typeof(Int64));
            return dt;
        }
 
        #endregion


        #region "Proccess Gridview"


        private DataTable TableTemplateMenu()
        {
            var dt = new DataTable();
            dt.Columns.Add("MenuCode", typeof(string));
            dt.Columns.Add("FormName", typeof(string));
            dt.Columns.Add("FormCode", typeof(string));
            dt.Columns.Add("ProjectCode", typeof(string));
            
            dt.Columns.Add("ChildPK", typeof(string));
            dt.Columns.Add("ParentPK", typeof(string));
            dt.Columns.Add("IDCHECK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("SortOrderNode", typeof(Int32));
            
            return dt;
        }



        private void LoadDataTreeViewMenu(TreeList tl, DataTable dt)
        {

        
            
            
        
            bool isShowFind = false;
            string findPanelText = "";
            if (tl.FindPanelVisible)
            {
                isShowFind = true;
                findPanelText = tl.FindFilterText;
                tl.HideFindPanel();
            }





            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";


            tl.BeginUpdate();
            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "MenuCode", "IDCHECK", "SortOrderNode", "RowState");



            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormName"], "Menu Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormCode"], "Form Code", false, HorzAlignment.Near, TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProjectCode"], "Project Code", false, HorzAlignment.Near, TreeFixedStyle.None, "");
       

            tl.BestFitColumns();
            tl.Columns["FormName"].Width += 30;

            tl.ForceInitialize();

            tl.BeginSort();
            tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();


            tl.EndUpdate();


            if (isShowFind)
            {
                tl.ShowFindPanelTreeList(findPanelText);
            }
        }


        private TreeListNode AddNewNodeMenu(TreeListNode dataNode, Int32 sortIndex)
        {
            TreeListNode node = tlMenu.AppendNode(new[]
            {
                dataNode.GetValue("MenuCode"),
                dataNode.GetValue("FormName"),
                dataNode.GetValue("FormCode"),
                dataNode.GetValue("ProjectCode"),
                dataNode.GetValue("MenuCode"),
                "",
                dataNode.GetValue("IDCHECK"),
                dataNode.GetValue("RowState"),
                sortIndex,


            }, null);


            return node;
        }



        private void InitTreeListMenu(TreeList treeList)
        {

            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = false;

            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsView.ShowColumns = true;
            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;
            treeList.OptionsView.AutoWidth = false;
            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;
            treeList.OptionsBehavior.AutoChangeParent = false;
            treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            treeList.OptionsBehavior.AutoNodeHeight = true;

            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;
            treeList.StateImageList = GetImageListDisplayTreeView();
            treeList.OptionsView.ShowIndentAsRowStyle = true;




            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,
                ShowAutoFilerMenu = true,


            };


            //treeList.AllowDrop = false;
            //treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Multiple;

            //treeList.OptionsDragAndDrop.AcceptOuterNodes = false;

            //treeList.OptionsDragAndDrop.CanCloneNodesOnDrop = false;


            treeList.OptionsView.ShowHierarchyIndentationLines = DefaultBoolean.False;



            treeList.CustomDrawNodeIndicator += TreeListMenu_CustomDrawNodeIndicator;

            treeList.GetStateImage += TreeListMenu_GetStateImage;











        }

       
       







        private void TreeListMenu_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;

            LinearGradientBrush backBrush;

            bool selected = tl.Selection.Contains(e.Node);
            Int64 idCheck = ProcessGeneral.GetSafeInt64(e.Node.GetValue("IDCHECK"));
            if (idCheck > 0)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Red, Color.Azure, 90);
            }
            else if (selected)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }



            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            if (selected)
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




        private void TreeListMenu_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {

            e.NodeImageIndex = 2;
        }







        #endregion



  


        #region "Drag Drop Final"
        private void RemoveTreeListNode(List<TreeListNode> lNodeSelect)
        {



            List<TreeListNode> lParentNode = ProcessGeneral.GetParentNodesFormCollectionNodes(lNodeSelect);
            if (lParentNode.Count <= 0) return;

            

            tlMain.BeginUpdate();
            tlMain.LockReloadNodes();


            tlMenu.BeginUpdate();
            tlMenu.LockReloadNodes();

            int sortIndex = GetMaxSortOrderValueOnNode(tlMenu,null);
          

            foreach (TreeListNode node in lParentNode)
            {
                AddNewNodeMenu(node, sortIndex);
                sortIndex++;
                  
               
                var lChildNode = node.GetAllChildNode();
                foreach (TreeListNode cNode in lChildNode)
                {

                    AddNewNodeMenu(cNode, sortIndex);
                    sortIndex++;
                }

              
          
                tlMain.DeleteNode(node);

            }

            tlMenu.UnlockReloadNodes();
            tlMenu.EndUpdate();

            tlMain.UnlockReloadNodes();
            tlMain.EndUpdate();


        



        }
        private Stream ReadImageFile(string path)
        {
            return File.OpenRead(path);
        }
        private Cursor SetDragCursor(DragDropActions e)
        {
            if (e == DragDropActions.Move)
                return new Cursor(ReadImageFile(Application.StartupPath + @"\move.ico"));
            if (e == DragDropActions.Copy)
                return new Cursor(ReadImageFile(Application.StartupPath + @"\copy.ico"));
            if (e == DragDropActions.None)
                return Cursors.No;
            return Cursors.Default;
        }
        private void Behavior_DragOver(object sender, DragOverEventArgs e)
        {
        
            
            e.Default();
            Cursor current = Cursors.No;
            DragDropActions action = DragDropActions.None;
            if (e.InsertType == InsertType.None) goto finshed;
            TreeList tlTarget = (TreeList)e.Target;
            if (tlTarget.Name == "tlMenu" && e.InsertType == InsertType.AsChild) goto finshed;

            if (e.Source != e.Target)
            {
                if (tlTarget.Name == "tlMenu")
                {
                    action = DragDropActions.Move;
                    current = new Cursor(ReadImageFile(Application.StartupPath + @"\delete.ico"));
                    goto finshed;
                }
                action = DragDropActions.Move;
                current = SetDragCursor(DragDropActions.Move);
                goto finshed;
            }

            bool isFilterNode;
            TreeListNode nodeTarget = GetDestNode(tlTarget, e.Location, out isFilterNode);
            if (!isFilterNode && nodeTarget != null)
            {
                List<TreeListNode> lNode = (List<TreeListNode>)e.Data;
                bool contain = lNode.Any(p => p == nodeTarget);
                if (!contain)
                {
                    action = DragDropActions.Move;
                    current = SetDragCursor(DragDropActions.Move);
                }
            }

            finshed:

            e.Action = action;
            Cursor.Current = current;
        }

        private void Behavior_DragDrop(object sender, DragDropEventArgs e)
        {
            TreeListNode destNode = null;
            TreeList tlTarget = (TreeList)e.Target;
            e.Handled = true;
            if (e.Action == DragDropActions.None || e.InsertType == InsertType.None)
            {
                goto finish;
            }


            TreeList tlSource = (TreeList)e.Source;


            bool isFilterNode = false;
            if (tlTarget.VisibleNodesCount > 0)
            {
                destNode = GetDestNode(tlTarget, e.Location, out isFilterNode);
            }
            if (isFilterNode) goto finish;
            List<TreeListNode> lNode = ProcessGeneral.GetParentNodesFormCollectionNodes((List<TreeListNode>)e.Data);

            if (e.Source == e.Target)
            {

                if (destNode == null) goto finish;
                if (e.InsertType == InsertType.AsChild)
                {



                    tlTarget.BeginUpdate();
                    tlTarget.LockReloadNodes();
                    int maxSortOrderE = GetMaxSortOrderValueOnNode(tlTarget, destNode);
                    foreach (TreeListNode nodeE in lNode)
                    {

                        nodeE.SetValue("SortOrderNode", maxSortOrderE);
                        CalEditValueChangedEvent(nodeE);

                        tlTarget.MoveNode(nodeE, destNode, true);


                        //if (nodeE.HasChildren && !nodeE.Expanded)
                        //{
                        //    nodeE.ExpandAll();
                        //}

                        maxSortOrderE++;

                    }
                    destNode.Expanded = true;
                    tlTarget.UnlockReloadNodes();
                    tlTarget.EndUpdate();

                    //     
                    goto finish;
                }

                tlTarget.BeginUpdate();
                tlTarget.LockReloadNodes();
                TreeListNode parentNode = destNode.ParentNode;
                List<TreeListNode> lChild = parentNode != null ? parentNode.Nodes.ToList() : tlTarget.Nodes.ToList();
                lChild = lChild.Where(p => lNode.All(t => t != p)).ToList();


                int loop = 0;
                TreeListNode nodeFind = null;
                for (int i = 0; i < lChild.Count; i++)
                {
                    TreeListNode nodeLoop = lChild[i];
                    if (nodeLoop == destNode)
                    {
                        nodeFind = nodeLoop;
                        loop = i;
                        break;
                    }
                }

                if (nodeFind == null)
                {
                    tlTarget.UnlockReloadNodes();
                    tlTarget.EndUpdate();
                    goto finish;
                }

                int sortOrderAfter = ProcessGeneral.GetSafeInt(nodeFind.GetValue("SortOrderNode"));

                if (e.InsertType == InsertType.After)
                {
                    sortOrderAfter++;
                    loop++;
                }
                foreach (TreeListNode nodeA in lNode)
                {
                    nodeA.SetValue("SortOrderNode", sortOrderAfter);
                    if (tlTarget.Name == "tlMain")
                    {
                        CalEditValueChangedEvent(nodeA);
                        if (nodeA.ParentNode != parentNode)
                        {
                            tlTarget.MoveNode(nodeA, parentNode, true);

                        }
                    }
                    sortOrderAfter++;
                }

                for (int j = loop; j < lChild.Count; j++)
                {
                    TreeListNode nodeS = lChild[j];
                    nodeS.SetValue("SortOrderNode", sortOrderAfter);
                    CalEditValueChangedEvent(nodeS);
                    sortOrderAfter++;
                }
                tlTarget.UnlockReloadNodes();
                tlTarget.EndUpdate();

                goto finish;
            }



            if (e.InsertType == InsertType.AsChild && destNode != null)
            {



                tlTarget.BeginUpdate();
                tlTarget.LockReloadNodes();
                tlSource.BeginUpdate();
                tlSource.LockReloadNodes();
                int maxSortOrderEt = GetMaxSortOrderValueOnNode(tlTarget, destNode);
                foreach (TreeListNode nodeEt in lNode)
                {
                    AddNewNodeMain(destNode, nodeEt, maxSortOrderEt);
                    tlSource.DeleteNode(nodeEt);

                    maxSortOrderEt++;

                }
                destNode.Expanded = true;
                tlSource.UnlockReloadNodes();
                tlSource.EndUpdate();
                tlTarget.UnlockReloadNodes();
                tlTarget.EndUpdate();

                goto finish;
            }

            if (destNode == null)
            {
                tlTarget.BeginUpdate();
                tlTarget.LockReloadNodes();
                tlSource.BeginUpdate();
                tlSource.LockReloadNodes();
                int maxSortOrderEh = 1;
                foreach (TreeListNode nodeEh in lNode)
                {
                    if (tlTarget.Name == "tlMenu")
                    {

                        AddNewNodeMenu(nodeEh, maxSortOrderEh);
                        maxSortOrderEh++;
                        List<TreeListNode> lChildEh = nodeEh.GetAllChildNode().ToList();
                        foreach (TreeListNode nodeChildEh in lChildEh)
                        {
                            AddNewNodeMenu(nodeChildEh, maxSortOrderEh);
                            maxSortOrderEh++;
                        }

                    }
                    else
                    {
                        AddNewNodeMain(null, nodeEh, maxSortOrderEh);

                        maxSortOrderEh++;
                    }


                    tlSource.DeleteNode(nodeEh);


                }
                tlSource.UnlockReloadNodes();
                tlSource.EndUpdate();
                tlTarget.UnlockReloadNodes();
                tlTarget.EndUpdate();

                goto finish;
            }

            TreeListNode parentNodeF = destNode.ParentNode;
            List<TreeListNode> lChildF = parentNodeF != null ? parentNodeF.Nodes.ToList() : tlTarget.Nodes.ToList();

            int loopF = 0;
            TreeListNode nodeFindF = null;
            for (int i = 0; i < lChildF.Count; i++)
            {
                TreeListNode nodeLoopF = lChildF[i];
                if (nodeLoopF == destNode)
                {
                    nodeFindF = nodeLoopF;
                    loopF = i;
                    break;
                }
            }

            if (nodeFindF == null) goto finish;
            int sortOrderAfterF = ProcessGeneral.GetSafeInt(nodeFindF.GetValue("SortOrderNode"));

            if (e.InsertType == InsertType.After)
            {
                sortOrderAfterF++;
                loopF++;
            }

            tlTarget.BeginUpdate();
            tlTarget.LockReloadNodes();
            tlSource.BeginUpdate();
            tlSource.LockReloadNodes();








            foreach (TreeListNode nodeData in lNode)
            {



                if (tlTarget.Name == "tlMenu")
                {

                    AddNewNodeMenu(nodeData, sortOrderAfterF);
                    sortOrderAfterF++;
                    List<TreeListNode> lChildData = nodeData.GetAllChildNode().ToList();
                    foreach (TreeListNode nodeChildData in lChildData)
                    {
                        AddNewNodeMenu(nodeChildData, sortOrderAfterF);
                        sortOrderAfterF++;
                    }

                }
                else
                {
                    AddNewNodeMain(parentNodeF, nodeData, sortOrderAfterF);
                    sortOrderAfterF++;
                }



                tlSource.DeleteNode(nodeData);


            }

            for (int j = loopF; j < lChildF.Count; j++)
            {
                TreeListNode nodeSf = lChildF[j];
                nodeSf.SetValue("SortOrderNode", sortOrderAfterF);
                CalEditValueChangedEvent(nodeSf);
                sortOrderAfterF++;
            }




            tlSource.UnlockReloadNodes();
            tlSource.EndUpdate();
            tlTarget.UnlockReloadNodes();
            tlTarget.EndUpdate();

            finish:
            Cursor.Current = Cursors.Default;
        }

 
        TreeListNode GetDestNode(TreeList tl, Point hitPoint,out bool isFilterNode)
        {
            isFilterNode = false;
            Point pt = tl.PointToClient(hitPoint);
            TreeListHitInfo ht = tl.CalcHitInfo(pt);
            TreeListNode destNode = ht.Node;
            if (destNode is TreeListAutoFilterNode)
            {
                isFilterNode = true;
                return null;
            }
            return destNode;

        }



       




        #endregion

    
    }
}
