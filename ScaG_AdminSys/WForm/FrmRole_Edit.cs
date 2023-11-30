using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CNY_AdminSys.Info;
using CNY_AdminSys.Properties;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using GridSumCol = DevExpress.Data.SummaryItemType;

namespace CNY_AdminSys.WForm
{
    public partial class FrmRole_Edit : FrmBase
    {
        #region "Property"
        private readonly string[] _arrFieldAllowDelete = { "AdvanceFunction", "SpecialFunction" };
        private readonly Inf_Role _inf = new Inf_Role();
        private bool _checkKeyDown;
        private bool _disableEventWhenLoad = false;
        private WaitDialogForm _dlg;
        private string _roleCode;
        private string _ruleCode;
        private readonly Form _fMain;
        private Int64 _authorizationOnUserGroupId = 0;
        private DataTable _dtUser;
        private bool _isEditCellEvent = true;
        private bool _isPerformEdit = false;
        private Dictionary<Int64, DataTable> _dicPer = new Dictionary<Int64, DataTable>();
        #endregion

        #region "Contructor"
        public FrmRole_Edit(string roleCode, string roleName, string ruleCode, string ruleName, Int64 authorizationOnUserGroupId,DataTable dtUser, Form parentForm)
        {
            InitializeComponent();
            this._dtUser = dtUser;
            this._authorizationOnUserGroupId = authorizationOnUserGroupId;
            this._fMain = parentForm;
            this._roleCode = roleCode;
            this._ruleCode = ruleCode;
            this.Text = string.Format("Update Permission On User ({0}-{1})/({2}-{3})", roleCode, roleName, ruleCode, ruleName);

            GridViewUserCustomInit(gcUserInRole,gvUserInRole);
            
            InitTreeView(tlMain);

            this.Load += Form_Load;
            this.FormClosed += Form_Closed;
            btnView.Click += BtnView_Click;
         
        }

       
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion
        #region "Form Event"
        private void Form_Load(object sender, EventArgs e)
        {
            AllowAdd = false;
         
            AllowDelete = false;

            




            AllowFind = false;
            AllowPrint = false;
            AllowRevision = false;
            AllowBreakDown = false;
            AllowRangeSize = false;

            AllowGenerate = false;
            AllowCombine = false;
            AllowCheck = false;
           
            
           
            AllowClose = true;
            EnableClose = true;


            SetCaptionCopyObject = "Duplicate";
            UseButtonWhenLoad();

           
      
            _dlg = new WaitDialogForm("");
            _disableEventWhenLoad = true;
            LoadDataGridViewUser();




            DisplatDetailFocusedRowChanged(gvUserInRole);
            _disableEventWhenLoad = false;
            _dlg.Close();
            _fMain.Enabled = false;
        }
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {


            _fMain.Enabled = true;
            _fMain.Activate();
        }

        private void UseButtonWhenLoad()
        {
            AllowEdit = true;
            EnableEdit = true;

            AllowCancel = false;
            EnableCancel = false;


            AllowSave = false;
            EnableSave = false;


            AllowRefresh = true;
            EnableRefresh = true;

            AllowCopyObject = true;
            EnableCopyObject = true;
        }


        private void UseButtonWhenEdit()
        {
            AllowEdit = false;
            EnableEdit = false;

            AllowCancel = true;
            EnableCancel = true;


            AllowSave = true;
            EnableSave = true;


            AllowRefresh = false;
            EnableRefresh = false;

            AllowCopyObject = false;
            EnableCopyObject = false;
        }

        #endregion

        #region "Proccess Treeview"

        private void LoadDataTreeView(TreeList tl, DataTable dt)
        {




            if (tl.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    tl.Columns.Add(new TreeListColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }


                ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "RowState", "RoleInsertCheck", "RoleUpdateCheck", "SortOrderNode"
                     , "RoleDeleteCheck", "RoleViewCheck", "AdvanceFunctionCheck", "SpecialFunctionCheck", "CheckAdvanceFunctionCheck", "ID"
                 );


                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SortOrderNode"], "Sort Order", false, HorzAlignment.Near, TreeFixedStyle.None, "");
                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormName"], "Menu Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");
                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormCode"], "Form Code", false, HorzAlignment.Near, TreeFixedStyle.None, "");


                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProjectCode"], "Project Code", false, HorzAlignment.Near, TreeFixedStyle.None, "");
                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleInsert"], "Insert", false, HorzAlignment.Center, TreeFixedStyle.None, "");
                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleUpdate"], "Update", false, HorzAlignment.Center, TreeFixedStyle.None, "");
                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleDelete"], "Delete", false, HorzAlignment.Center, TreeFixedStyle.None, "");
                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RoleView"], "View", false, HorzAlignment.Center, TreeFixedStyle.None, "");
                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["CheckAdvanceFunction"], "Not Check AF", false, HorzAlignment.Center, TreeFixedStyle.None, "");

                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["AdvanceFunction"], "Advance Function (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
                tl.Columns["AdvanceFunction"].ImageIndex = 0;
                tl.Columns["AdvanceFunction"].ImageAlignment = StringAlignment.Near;



                ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SpecialFunction"], "Special Function (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
                tl.Columns["SpecialFunction"].ImageIndex = 0;
                tl.Columns["SpecialFunction"].ImageAlignment = StringAlignment.Near;
            }



            bool isShowFind = false;
            string findPanelText = "";
            if (tl.FindPanelVisible)
            {
                isShowFind = true;
                findPanelText = tl.FindFilterText;
                tl.HideFindPanel();
            }





            //tl.Columns.Clear();
            //tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";


            tl.BeginUpdate();
         
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

        private void DisplatDetailFocusedRowChanged(GridView gv)
        {
            int rH = gv.FocusedRowHandle;

            if (gv.RowCount == 0 || !gv.IsDataRow(rH))
            {
                LoadDataTreeView(tlMain, TableTreeviewTemplate());
                return;
            }
            if (_authorizationOnUserGroupId <= 0)
            {
                LoadDataTreeView(tlMain, TableTreeviewTemplate());
                return;
            }
            Int64 userInGroupId = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(rH, "UserInGroupID"));
            if (userInGroupId <= 0)
            {
                LoadDataTreeView(tlMain, TableTreeviewTemplate());
                return;
            }

            DataTable dtS;
            if (!_dicPer.TryGetValue(userInGroupId, out dtS))
            {
                dtS = TableTreeviewTemplate();
            }

            LoadDataTreeView(tlMain, dtS);
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            int rH = gvUserInRole.FocusedRowHandle;

            if (gvUserInRole.RowCount == 0 || !gvUserInRole.IsDataRow(rH)) goto error;
            if (_authorizationOnUserGroupId <= 0) goto error;

            Int64 userInGroupId = ProcessGeneral.GetSafeInt64(gvUserInRole.GetRowCellValue(rH, "UserInGroupID"));
            if (userInGroupId <= 0) goto error;
           
            _dlg = new WaitDialogForm();
            DataTable dtS = _inf.LoadTreeViewMainMenu(_authorizationOnUserGroupId, userInGroupId);
            _dlg.Close();
            if(dtS.Rows.Count<=0) goto error;
            string userName = ProcessGeneral.GetSafeString(gvUserInRole.GetRowCellValue(rH, "UserName"));
            var f = new FrmTreelistPermission(userName, dtS);
            f.ShowDialog();
            return;
            error:
            XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }



        private DataTable TableTreeviewTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int64));
            dt.Columns.Add("FormName", typeof(string));
            dt.Columns.Add("FormCode", typeof(string));
            dt.Columns.Add("ProjectCode", typeof(string));
            dt.Columns.Add("RoleInsert", typeof(bool));
            dt.Columns.Add("RoleUpdate", typeof(bool));
            dt.Columns.Add("RoleDelete", typeof(bool));
            dt.Columns.Add("RoleView", typeof(bool));
            dt.Columns.Add("AdvanceFunction", typeof(string));
            dt.Columns.Add("SortOrderNode", typeof(int));
            dt.Columns.Add("RoleInsertCheck", typeof(bool));
            dt.Columns.Add("RoleUpdateCheck", typeof(bool));
            dt.Columns.Add("RoleDeleteCheck", typeof(bool));
            dt.Columns.Add("RoleViewCheck", typeof(bool));
            dt.Columns.Add("AdvanceFunctionCheck", typeof(string));
            dt.Columns.Add("ChildPK", typeof(string));
            dt.Columns.Add("ParentPK", typeof(string));
            dt.Columns.Add("SpecialFunction", typeof(string));
            dt.Columns.Add("SpecialFunctionCheck", typeof(string));
            dt.Columns.Add("CheckAdvanceFunction", typeof(bool));
            dt.Columns.Add("CheckAdvanceFunctionCheck", typeof(bool));
            dt.Columns.Add("RowState", typeof(string));
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

        private void InitTreeView(TreeList treeList)
        {
            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
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

            TreeList tl = (TreeList)sender;
            if (tl == null) return;
            _checkKeyDown = true;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;

            string fieldName = col.FieldName;


        

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
                if (!_isPerformEdit) return;
                TextEdit tE = tl.ActiveEditor as TextEdit;
                if (tE != null) return;
                ProcessDeleteKey(tl);
                return;
            }

            #endregion




            #region "F4 Key"

            if (e.KeyCode == Keys.F4)
            {
                if (!_isPerformEdit) return;
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
          

            DataTable dtSource = _inf.AuthorizationOnUser_LoadAdvanceFunctionByID(ProcessGeneral.GetSafeString(node.GetValue("AdvanceFunctionCheck")));

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
           

            DataTable dtSource = _inf.AuthorizationOnUser_LoadSpecialFunctionByID(ProcessGeneral.GetSafeString(node.GetValue("SpecialFunctionCheck")));

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
                    nodeUpd.SetValue(field, "");
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
            if (!_isPerformEdit)
            {
                e.Cancel = true;
                return;
            }
            var tl = (TreeList)sender;
            if (tl == null) return;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "FormName":
                case "FormCode":
                case "ProjectCode":
                case "AdvanceFunction":
                case "SpecialFunction":
                    e.Cancel = true;
                    break;
                case "RoleInsert":
                    e.Cancel = !ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("RoleInsertCheck"));
                    break;
                case "RoleUpdate":
                    e.Cancel = !ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("RoleUpdateCheck"));
                    break;
                case "RoleDelete":
                    e.Cancel = !ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("RoleDeleteCheck"));
                    break;
                case "CheckAdvanceFunction":
                    e.Cancel = !ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("CheckAdvanceFunctionCheck"));
                    break;
                case "RoleView":
                    if (ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("RoleInsert")) ||
                        ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("RoleUpdate")) ||
                        ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("RoleDelete")))
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = !ProcessGeneral.GetSafeBool(tl.FocusedNode.GetValue("RoleViewCheck"));

                    }
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
             
                case "AdvanceFunction":
                case "SpecialFunction":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightGoldenrodYellow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "RoleInsert":
                    if (ProcessGeneral.GetSafeBool(e.Node.GetValue("RoleInsertCheck")))
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "RoleUpdate":
                    if (ProcessGeneral.GetSafeBool(e.Node.GetValue("RoleUpdateCheck")))
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "RoleDelete":
                    if (ProcessGeneral.GetSafeBool(e.Node.GetValue("RoleDeleteCheck")))
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "RoleView":
                    if (ProcessGeneral.GetSafeBool(e.Node.GetValue("RoleViewCheck")))
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
            
                case "CheckAdvanceFunction":
                    if (ProcessGeneral.GetSafeBool(e.Node.GetValue("CheckAdvanceFunctionCheck")))
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                default:
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

        #region "Proccess Gridview User"
        private DataTable TableTemplateGvUser()
        {
            var dt = new DataTable();
            dt.Columns.Add("UserID", typeof(Int64));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("UserInGroupID", typeof(Int64));
            return dt;
        }

        private void CreateDicPermission()
        {
            _dicPer.Clear();
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("PK", typeof(Int64));
            foreach (DataRow drUser in _dtUser.Rows)
            {
                dtTemp.Rows.Add(ProcessGeneral.GetSafeInt64(drUser["UserInGroupID"]));
            }

            DataTable dtFinal = _inf.AuthorizationOnUser_LoadTreeListPermission(dtTemp, _authorizationOnUserGroupId);
            _dicPer = dtFinal.AsEnumerable().GroupBy(p => p.Field<Int64>("PKLINK")).Select(s => new
            {
                PKLINK = s.Key,
                ListField = s.Select(t => new
                {
                    ID = t.Field<Int64>("ID"),
                    FormName = t.Field<String>("FormName"),
                    FormCode = t.Field<String>("FormCode"),
                    ProjectCode = t.Field<String>("ProjectCode"),

                    RoleInsert = t.Field<bool>("RoleInsert"),
                    RoleUpdate = t.Field<bool>("RoleUpdate"),
                    RoleDelete = t.Field<bool>("RoleDelete"),
                    RoleView = t.Field<bool>("RoleView"),

                    AdvanceFunction = t.Field<String>("AdvanceFunction"),
                    SortOrderNode = t.Field<Int32?>("SortOrderNode"),

                    RoleInsertCheck = t.Field<bool>("RoleInsertCheck"),
                    RoleUpdateCheck = t.Field<bool>("RoleUpdateCheck"),
                    RoleDeleteCheck = t.Field<bool>("RoleDeleteCheck"),
                    RoleViewCheck = t.Field<bool>("RoleViewCheck"),

                    AdvanceFunctionCheck = t.Field<String>("AdvanceFunctionCheck"),
                    ChildPK = t.Field<String>("ChildPK"),
                    ParentPK = t.Field<String>("ParentPK"),

                    SpecialFunction = t.Field<String>("SpecialFunction"),
                    SpecialFunctionCheck = t.Field<String>("SpecialFunctionCheck"),

                    CheckAdvanceFunction = t.Field<bool>("CheckAdvanceFunction"),
                    CheckAdvanceFunctionCheck = t.Field<bool>("CheckAdvanceFunctionCheck"),

                    RowState = t.Field<String>("RowState"),
                }).ToList().CopyToDataTableNew(),
            }).ToDictionary(s => s.PKLINK, s => s.ListField);

           
         
  

        }

        private void LoadDataGridViewUser()
        {


            CreateDicPermission();




            if (gvUserInRole.VisibleColumns.Count <= 0)
            {
                int ind = 0;
                foreach (DataColumn col in _dtUser.Columns)
                {
                    gvUserInRole.Columns.Add(new GridColumn { FieldName = col.ColumnName, VisibleIndex = ind });
                    ind++;
                }


                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["UserID"], "UserID", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["UserName"], "User Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["FullName"], "Full Name", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvUserInRole.Columns["Email"], "Email", DefaultBoolean.True, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.HideVisibleColumnsGridView(gvUserInRole, false, "UserID", "UserInGroupID");



            }
            gvUserInRole.BeginUpdate();




            //gc.DataSource = null;
            //gv.Columns.Clear();

            gcUserInRole.DataSource = _dtUser;


            gvUserInRole.BestFitColumns();
            gvUserInRole.EndUpdate();









        }

        private void GridViewUserCustomInit(GridControl gc, GridView gv)
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




            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;











            //     gvMainAE.OptionsHint.ShowCellHints = true;



            //   gv.MouseMove += gv_MouseMove;

            gv.RowStyle += gvTest_RowStyle;
            gv.RowCountChanged += gvTest_RowCountChanged;
            gv.CustomDrawRowIndicator += gvTest_CustomDrawRowIndicator;
            gv.FocusedRowChanged += Gv_FocusedRowChanged;

            gc.ForceInitialize();
        }

        private void Gv_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            if (!_disableEventWhenLoad)
            {
                DisplatDetailFocusedRowChanged(gv);
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

        



     

        #region "override button menubar click"

        /// <summary>
        /// Perform when click Edit button
        /// </summary>
        protected override void PerformEdit()
        {
            _isPerformEdit = true;

            UseButtonWhenEdit();
            gcUserInRole.Enabled = false;
            
        }

        protected override void PerformCopy()
        {
        
     

            int rH = gvUserInRole.FocusedRowHandle;
            Int64 userInGroupIdRoot = ProcessGeneral.GetSafeInt64(gvUserInRole.GetRowCellValue(rH, "UserInGroupID"));
            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.AuthorizationOnUser_LoadGridUserCopy(_roleCode, userInGroupIdRoot);
            _dlg.Close();

            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            List<Int64> l = new List<Int64>();
            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
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
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"User Name",
                    FieldName = "UserName",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Full Name",
                    FieldName = "FullName",
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
                    Caption = @"Email",
                    FieldName = "Email",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 30,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"UserInGroupID",
                    FieldName = "UserInGroupID",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
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
                    l.Add(ProcessGeneral.GetSafeInt64(dr["UserInGroupID"]));
                }
            };
            f.ShowDialog();


            if (l.Count<=0) return;
            _dlg = new WaitDialogForm();
  
         
            foreach (Int64 userInGroupIdCopy in l)
            {
                if (userInGroupIdCopy <= 0) continue;
                _inf.AuthorizationOnUser_Copy(userInGroupIdRoot,
                    _authorizationOnUserGroupId,
                    userInGroupIdCopy);
            }

            CreateDicPermission();
            _disableEventWhenLoad = true;
            DisplatDetailFocusedRowChanged(gvUserInRole);
            _disableEventWhenLoad = false;

            _dlg.Close();
            XtraMessageBox.Show(string.Format("Copy Successfull Permission From User {0} To User Selected...!!!", 
                ProcessGeneral.GetSafeString(gvUserInRole.GetRowCellValue(rH, "UserName"))
            ), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
        }
        private DataTable TableTreeviewUpdateDBTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int64));
            dt.Columns.Add("RoleInsert", typeof(bool));
            dt.Columns.Add("RoleUpdate", typeof(bool));
            dt.Columns.Add("RoleDelete", typeof(bool));
            dt.Columns.Add("RoleView", typeof(bool));
            dt.Columns.Add("AdvanceFunction", typeof(string));
            dt.Columns.Add("SpecialFunction", typeof(string));
            dt.Columns.Add("CheckAdvanceFunction", typeof(bool));
            return dt;
        }
        protected override void PerformSave()
        {

            
         



            if (!txtFocus.Focused)
            {
                txtFocus.SelectNextControl(ActiveControl, true, true, true, true);
                txtFocus.Focus();
            }

            List<TreeListNode> lNode = tlMain.GetAllNodeTreeList();
            DataTable dtUpd = TableTreeviewUpdateDBTemplate();

            foreach (TreeListNode node in lNode)
            {
                DataStatus rowState = ProcessGeneral.GetDataStatus(ProcessGeneral.GetSafeString(node.GetValue("RowState")));
                if (rowState != DataStatus.Update) continue;
                Int64 pkChild = ProcessGeneral.GetSafeInt64(node.GetValue("ID"));
                bool roleInsert = ProcessGeneral.GetSafeBool(node.GetValue("RoleInsert"));
                bool roleUpdate = ProcessGeneral.GetSafeBool(node.GetValue("RoleUpdate"));
                bool roleDelete = ProcessGeneral.GetSafeBool(node.GetValue("RoleDelete"));
                bool roleView = ProcessGeneral.GetSafeBool(node.GetValue("RoleView"));
                string advanceFunction = ProcessGeneral.GetSafeString(node.GetValue("AdvanceFunction"));
                string specialFunction = ProcessGeneral.GetSafeString(node.GetValue("SpecialFunction"));
                bool checkAdvanceFunction = ProcessGeneral.GetSafeBool(node.GetValue("CheckAdvanceFunction"));

                dtUpd.Rows.Add(pkChild, roleInsert, roleUpdate, roleDelete, roleView, advanceFunction, specialFunction, checkAdvanceFunction);


            }

          
            _dlg = new WaitDialogForm("");
            if (dtUpd.Rows.Count > 0)
            {
                _inf.AuthorizationOnUser_Update(dtUpd);
                CreateDicPermission();
                _disableEventWhenLoad = true;
                DisplatDetailFocusedRowChanged(gvUserInRole);
                _disableEventWhenLoad = false;
            }
       

            _isPerformEdit = false;
            UseButtonWhenLoad();
            gcUserInRole.Enabled = true;

            _dlg.Close();
            XtraMessageBox.Show(string.Format("Update Successful Permission For User {0}", ProcessGeneral.GetSafeString(gvUserInRole.GetRowCellValue(gvUserInRole.FocusedRowHandle, "UserName"))),
                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        /// <summary> Perform when click Cancel button
        /// </summary>
        protected override void PerformCancel()
        {
            _isPerformEdit = false;
            UseButtonWhenLoad();
            gcUserInRole.Enabled = true;
        }
        /// <summary>
        /// Perform when click Refresh button
        /// </summary>
        protected override void PerformRefresh()
        {
            _disableEventWhenLoad = true;
            CreateDicPermission();
            DisplatDetailFocusedRowChanged(gvUserInRole);
            _disableEventWhenLoad = false;
        }


      
        #endregion
    }

}
