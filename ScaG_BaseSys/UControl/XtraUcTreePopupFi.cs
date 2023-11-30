using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_BaseSys.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;

namespace CNY_BaseSys.UControl
{
    public partial class XtraUcTreePopupFi : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property & Field"

        private bool _checkKeyDown;


        public DataTable DtSource
        {
            get { return (DataTable)tlMain.DataSource; }
        }
        public PopupContainerEdit PpEdit
        {
            get { return ppEdit; }
        }

        public string EditText
        {
            get { return ProcessGeneral.GetSafeString(ppEdit.EditValue); }
        }


        private string _keyField = "ChildPK";
        public string KeyField
        {
            get { return this._keyField; }
        }



        private string _parentField = "ParentPK";
        public string ParentField
        {
            get { return this._parentField; }
        }




        private string _displayField = "";
        public string DisplayField
        {
            get
            { return this._displayField; }
            set
            {
                this._displayField = value;
            }
        }

        private string _findPanelText = "";

        private int _expandTree = 0;


        private List<object> _lValue = new List<object>();
        public List<object> LValue
        {
            get
            { return this._lValue; }
            set
            {
                this._lValue = value;
            }
        }


        public Int32 PopupWidth
        {
            get
            { return this.popupCCTree.Width; }
            set
            {
                this.popupCCTree.Width = value;
            }
        }
        public Int32 PopupHeight
        {
            get
            { return this.popupCCTree.Height; }
            set
            {
                this.popupCCTree.Height = value;
            }
        }


        public bool ShowCheckBoxes
        {
            get { return tlMain.OptionsView.ShowCheckBoxes; }
            set
            {
                tlMain.OptionsView.ShowCheckBoxes = value;

            }
        }


        public bool ShowCheckAllButton
        {
            get { return btnCheckAll.Visible; }
            set
            {
                btnCheckAll.Visible = value;

            }
        }

        public bool ShowFinishedButton
        {
            get { return btnNextFinish.Visible; }
            set
            {
                btnNextFinish.Visible = value;

            }
        }
        public bool ShowGroupHeader
        {
            get { return groupControl3.ShowCaption; }
            set
            {
                groupControl3.ShowCaption = value;

            }
        }


        public bool MultiSelect
        {
            get { return tlMain.OptionsSelection.MultiSelect; }
            set { tlMain.OptionsSelection.MultiSelect = value; }
        }

        private bool _expandTreePopup = false;
        public bool ExpandTreePopup
        {
            get { return _expandTreePopup; }
            set
            {
                _expandTreePopup = value;

            }
        }

        public PopUpFiTreeList TlMain
        {
            get { return tlMain; }
        }


        public event OnGetDataTreeHandler OnGetData = null;

        #endregion
        #region "Contructor"

        public XtraUcTreePopupFi()
        {
            InitializeComponent();

            InitTreeList(tlMain);




            btnNextFinish.Click += btnNextFinish_Click;
            btnCheckAll.Click += BtnCheckAll_Click;

            ppEdit.KeyPress += PpEdit_KeyPress;

            ppEdit.QueryPopUp += PpEdit_QueryPopUp;
            ppEdit.CloseUp += PpEdit_CloseUp;
            ppEdit.Popup += PpEdit_Popup;
            ppEdit.KeyDown += PpEdit_KeyDown;
            ppEdit.EditValueChanged += PpEdit_EditValueChanged;

        }












        #endregion

        #region "Process Popup Control"

        private bool CompareDataKey(object value1, object value2)
        {
            bool rs = Object.Equals(value1, value2);
            return rs;
        }

        public void SetEditValue(List<object> lValue)
        {
            this._lValue.Clear();
            DataTable dtS = (DataTable)tlMain.DataSource;
            if (dtS == null)
            {
                ppEdit.EditValue = "";
                return;
            }

            string displayField = GetDisplayField(dtS);
            var q1 = dtS.AsEnumerable().Where(p => lValue.Any(t => CompareDataKey(t, p[_keyField]))).Select(p => new
            {
                Value = p[_keyField],
                Text = ProcessGeneral.GetSafeString(p[displayField])
            }).ToList();

            foreach (var item in q1)
            {
                this._lValue.Add(item.Value);
            }


            string text = string.Join(", ", q1.Select(p => p.Text).Distinct().ToArray()).Trim();
            ppEdit.EditValue = text;


        }

        private string GetDisplayField(DataTable dtS)
        {
            string displayField = _displayField;
            if (string.IsNullOrEmpty(displayField))
            {
                if (tlMain.VisibleColumns.Count > 0)
                {
                    displayField = tlMain.VisibleColumns[0].FieldName;
                }
                else
                {
                    displayField = dtS.Columns[0].ColumnName;
                }
            }

            return displayField;
        }

        private TreeListNode SetCheckOnTree()
        {
            tlMain.BeginUpdate();

            List<TreeListNode> lNode = tlMain.GetAllNodeTreeList();





            var q2 = lNode.GroupJoin(_lValue, p => p.GetValue(_keyField), t => t, (p, j) => new { p, j })
                .SelectMany(@t1 => @t1.j.DefaultIfEmpty(), (@t1, x) => new { @t1, x })
                // .Where(@t1 => @t1.x == null)
                .Select(@t1 => new
                {
                    Node = @t1.@t1.p,
                    Checked = @t1.x != null,
                }).ToList();

            bool isUncheck = false;
            TreeListNode nodeFocusNew = null;
            foreach (var item in q2)
            {
                bool isCheck = item.Checked;
                TreeListNode node = item.Node;
                if (isCheck)
                {
                    if (nodeFocusNew == null)
                    {
                        nodeFocusNew = node;
                    }

                    node.CheckState = CheckState.Checked;

                }
                else
                {
                    node.CheckState = CheckState.Unchecked;
                    isUncheck = true;
                }
            }



            if (isUncheck)
            {
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");




            }
            else
            {


                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");


            }



            tlMain.EndUpdate();

            return nodeFocusNew;
        }

        private TreeListNode SetSelectOnTree()
        {
            tlMain.BeginUpdate();

            List<TreeListNode> lNode = tlMain.GetAllNodeTreeList();





            var q2 = lNode.GroupJoin(_lValue, p => p.GetValue(_keyField), t => t, (p, j) => new { p, j })
                .SelectMany(@t1 => @t1.j.DefaultIfEmpty(), (@t1, x) => new { @t1, x })
                // .Where(@t1 => @t1.x == null)
                .Select(@t1 => new
                {
                    Node = @t1.@t1.p,
                    Checked = @t1.x != null,
                }).ToList();


            TreeListNode nodeFocusNew = null;
            foreach (var item in q2)
            {
                bool isCheck = item.Checked;
                TreeListNode node = item.Node;
                if (isCheck)
                {
                    if (nodeFocusNew == null)
                    {
                        nodeFocusNew = node;
                    }

                    node.Selected = true;

                }
                else
                {
                    node.Selected = false;

                }
            }




            tlMain.EndUpdate();

            return nodeFocusNew;
        }

        private void PpEdit_EditValueChanged(object sender, EventArgs e)
        {
            ppEdit.ToolTip = ProcessGeneral.GetSafeString(ppEdit.EditValue);

        }

        private void PpEdit_Popup(object sender, EventArgs e)
        {

            _expandTree++;
            bool isExpanded = false;
            if (_expandTree <= 1)
            {
                tlMain.BeginUpdate();
                tlMain.ExpandAll();
                tlMain.ForceInitialize();
                tlMain.EndUpdate();
                isExpanded = true;
            }

            if (!isExpanded && _expandTreePopup)
            {
                tlMain.BeginUpdate();
                tlMain.ExpandAll();
                tlMain.ForceInitialize();
                tlMain.EndUpdate();
            }

            TreeListNode nodeFocusNew = tlMain.OptionsView.ShowCheckBoxes ? SetCheckOnTree() : SetSelectOnTree();
            if (nodeFocusNew != null)
            {
                ProcessGeneral.SetFocusedCellOnTree(tlMain, nodeFocusNew, tlMain.VisibleColumns[0].FieldName);
            }



            BeginInvoke(new MethodInvoker(tlMain.FocusFindEditor));
            if (!string.IsNullOrEmpty(this._findPanelText))
            {
                tlMain.ApplyFindFilter(this._findPanelText);
                BeginInvoke(new MethodInvoker(tlMain.SetFocusAfterFindText));
            }

            //if (tlMain.AllNodesCount <= 0)
            //{
            //    SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            //    return;
            //}



        }


        private void PpEdit_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            _findPanelText = "";
            if (!string.IsNullOrEmpty(tlMain.FindFilterText.Trim()))
            {
                tlMain.ApplyFindFilter("");

            }


        }

        private void PpEdit_KeyPress(object sender, KeyPressEventArgs e)
        {

            bool isInputString;
            string letter = e.KeyChar.CheckIsKeyInputText(out isInputString);
            if (!isInputString)
            {
                _findPanelText = "";
                return;
            }


            _findPanelText = letter;
            ppEdit.ShowPopup();



        }

        private void PpEdit_KeyDown(object sender, KeyEventArgs e)
        {
            //#region "Delete Key"
            //if (e.KeyCode == Keys.Delete)
            //{
            //    this._lValue.Clear();
            //    ppEdit.EditValue = "";
            //    return;

            //}
            //#endregion
            #region "Process Ctrl+C key"

            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control && !string.IsNullOrEmpty(ppEdit.Text))
            {
                Clipboard.SetText(ppEdit.Text);
                return;
            }

            #endregion
        }

        private void PpEdit_QueryPopUp(object sender, CancelEventArgs e)
        {
            e.Cancel = DtSource == null || DtSource.Rows.Count <= 0;
        }




        #endregion








        #region "Button Click Event"









        private void btnNextFinish_Click(object sender, EventArgs e)
        {


            this._lValue.Clear();
            List<TreeListNode> lNode = new List<TreeListNode>();
            DataTable dtS = (DataTable)tlMain.DataSource;
            if (dtS == null)
            {
                ppEdit.EditValue = "";
                goto finsh;
            }

            string displayField = GetDisplayField(dtS);


            if (tlMain.OptionsView.ShowCheckBoxes)
            {
                lNode = tlMain.GetAllCheckedNodes().ToList();

            }
            else
            {
                if (tlMain.OptionsSelection.MultiSelect)
                {
                    lNode = tlMain.GetSelectedCells().Select(p => p.Node).Distinct().ToList();
                }
                else
                {
                    if (tlMain.FocusedNode != null)
                    {
                        lNode.Add(tlMain.FocusedNode);
                    }
                }

            }



            var q1 = lNode.Select(p => new
            {
                Value = p.GetValue(_keyField),
                Text = ProcessGeneral.GetSafeString(p.GetValue(displayField))
            }).ToList();

            foreach (var item in q1)
            {
                this._lValue.Add(item.Value);
            }


            string text = string.Join(", ", q1.Select(p => p.Text).Distinct().ToArray()).Trim();
            ppEdit.EditValue = text;


            finsh:

            ppEdit.ClosePopup();
            OnGetData?.Invoke(this, new OnGetDataTreeEventArgs
            {
                LNode = lNode
            });


        }

        private void BtnCheckAll_Click(object sender, EventArgs e)
        {
            if (tlMain.AllNodesCount <= 0) return;
            List<TreeListNode> lNode = tlMain.GetAllNodeTreeList();
            if (btnCheckAll.ToolTip == @"Check All")
            {
                var qNodeNotCheck = lNode.Where(p => p.CheckState != CheckState.Checked).ToList();
                foreach (TreeListNode node in qNodeNotCheck)
                {

                    node.CheckState = CheckState.Checked;
                }

                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
            }
            else
            {
                var qNodeNotUnCheck = lNode.Where(p => p.CheckState != CheckState.Unchecked).ToList();
                foreach (TreeListNode node in qNodeNotUnCheck)
                {
                    node.CheckState = CheckState.Unchecked;
                }

                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }


        }















        #endregion




        #region "Proccess Treeview"



        public void LoadDataTreeView(DataTable dtSource, string keyFieldName, string parentFieldName, params TreeViewTransferDataColumnInit[] arrColFormat)
        {


            _expandTree = 0;
            this._keyField = keyFieldName;
            this._parentField = parentFieldName;









            tlMain.Columns.Clear();
            tlMain.DataSource = null;
            if (dtSource == null || dtSource.Rows.Count <= 0) return;





            tlMain.DataSource = dtSource;
            tlMain.ParentFieldName = parentFieldName;
            tlMain.KeyFieldName = keyFieldName;

            //   string[] arr = DicAttributeRoot.Select(p => p.Value.AttibuteName).ToArray();
            tlMain.BeginUpdate();






            bool isFormat = (arrColFormat != null) && arrColFormat.Length > 0;




            if (isFormat)
            {
                foreach (var item in arrColFormat)
                {
                    string fieldName = item.FieldName.Trim();
                    if (fieldName == _keyField || fieldName == _parentField) continue;
                    TreeListColumn gCol = tlMain.Columns[fieldName];
                    if (gCol == null) continue;
                    gCol.AppearanceCell.Options.UseTextOptions = true;
                    gCol.AppearanceCell.TextOptions.HAlignment = item.HorzAlign;
                    gCol.AppearanceHeader.Options.UseTextOptions = true;
                    gCol.AppearanceHeader.TextOptions.HAlignment = item.HorzAlign;
                    gCol.Caption = item.Caption.Trim();
                    gCol.OptionsColumn.AllowMove = false;
                    gCol.OptionsColumn.AllowSort = false; //
                    gCol.Fixed = item.FixStyle;
                    gCol.Format.FormatType = item.FormatField;
                    if (!string.IsNullOrEmpty(item.FormatString))
                    {
                        gCol.Format.FormatString = item.FormatString;
                    }
                }
            }

            //   tlMain.ExpandAll();
            tlMain.ForceInitialize();
            tlMain.EndUpdate();
            tlMain.BestFitColumns();
            //foreach (TreeListNode node in tl.Nodes)
            //{
            //    node.Checked = true;
            //}
            //  SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
        }

        public void HideColumnsTree(params string[] arrCol)
        {
            foreach (string fieldName in arrCol)
            {
                TreeListColumn gCol = tlMain.Columns[fieldName];
                if (gCol == null) continue;
                gCol.Visible = false;


            }
        }

        private void SetInfoCheckButton(bool enable, Image image, string text)
        {
            btnCheckAll.Image = image;
            btnCheckAll.ToolTip = text;
            btnCheckAll.Enabled = enable;
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

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = true;
            treeList.OptionsFind.ShowCloseButton = false;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsFind.ShowFindButton = false;
            treeList.OptionsFind.ShowClearButton = false;
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

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = true;

            treeList.OptionsSelection.MultiSelect = true;
            treeList.OptionsSelection.MultiSelectMode = TreeListMultiSelectMode.CellSelect;
            treeList.OptionsBehavior.ExpandNodesOnFiltering = true;
            treeList.OptionsBehavior.AllowExpandOnDblClick = false;
            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };






            treeList.GetStateImage += TreeList_GetStateImage;
            treeList.ShowingEditor += TreeList_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;





            treeList.NodeCellStyle += TreeList_NodeCellStyle;




            treeList.AfterCheckNode += TreeList_AfterCheckNode;

            treeList.EditorKeyDown += TreeList_EditorKeyDown;
            treeList.KeyDown += TreeList_KeyDown;
            treeList.DoubleClick += TreeList_DoubleClick;
        }

        private void TreeList_DoubleClick(object sender, EventArgs e)
        {
            var tl = (TreeList)sender;
            if (tl == null) return;
            TreeListHitInfo hi = tl.CalcHitInfo(tl.PointToClient(Control.MousePosition));
            if (hi.InRow)
            {
                btnNextFinish_Click(null, null);
                return;
            }


        }

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

            TreeList tl = sender as TreeList;
            if (tl == null) return;
            _checkKeyDown = true;
            TreeListNode node = tl.FocusedNode;






            if (node == null) return;
            //   TreeListNode parentNode = node.ParentNode; 
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;


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



            #region "Process Enter Key"
            if (e.KeyCode == Keys.Enter)
            {
                btnNextFinish_Click(null, null);
            }

            #endregion








        }



        private void TreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            List<TreeListNode> lCheckNode = tl.GetAllCheckedNodes().ToList();
            if (lCheckNode.Count == tl.AllNodesCount)
            {
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");

            }
            else
            {
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
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
            //  string fieldName = col.FieldName;
            bool isCheck;

            if (tl.OptionsView.ShowCheckBoxes)
            {
                isCheck = e.Node.CheckState == CheckState.Checked;
            }
            else
            {
                if (tl.OptionsSelection.MultiSelect)
                {
                    isCheck = e.Node.Selected;
                }
                else
                {
                    isCheck = node == tl.FocusedNode;
                }

            }

            if (node.ParentNode == null && node.HasChildren)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
            }


            if (isCheck)
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
            }
            else
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
            }



        }

        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;



        }
        private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = (TreeList)sender;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;


            bool isCheck;

            if (tl.OptionsView.ShowCheckBoxes)
            {
                isCheck = e.Node.CheckState == CheckState.Checked;
            }
            else
            {
                isCheck = e.Node.Selected;
            }

            LinearGradientBrush backBrush;

            if (isCheck)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
            }


            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            if (isCheck)
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
    }
}
