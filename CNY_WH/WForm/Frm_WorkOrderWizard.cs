using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_WH.Common;
using CNY_WH.Info;
using CNY_WH.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_WH.WForm
{
    public partial class Frm_WorkOrderWizard : XtraForm
    {
        #region "Property & Field"
 
        private readonly Inf_WorkOrderWizard _inf = new Inf_WorkOrderWizard();

        private WaitDialogForm _dlg = null;

        private DataTable dtSelectedInTab1 = new DataTable();
        private List<TreeListNode> lNodeSelected = new List<TreeListNode>();

        public event SDRWizardHandle OnPrWizard = null;

        private const Int32 TotalStep = 3;
        private Int32 _step = 1;

        private DataTable _dtUserAssignProduct;
        private string _itemType;
        #endregion

        #region "Contructor"

        public Frm_WorkOrderWizard(DataTable dtUserAssignProduct,string ItemType)
        {
            InitializeComponent();

            _dtUserAssignProduct = dtUserAssignProduct;
            _itemType = ItemType;
            //khoi tao treelist tab 1
            InitTreeList(tlMain);
            //khoi tao treelist tab 2
            InitTreeListItemCode(tlItemCode);
            //khoi tao treelist tab 3
            InitTreeListFinal(tlFinal);

            this.MinimizeBox = false;

            this.Load += Form_Load;

            btnCancel.Click += btnCancel_Click;
            btnNextFinish.Click += btnNextFinish_Click;
            btnBack.Click += btnBack_Click;
         
        }



        #endregion

        #region "Form Event"


        private void Form_Load(object sender, EventArgs e)
        {

            _step = 1;
            VisibleTabPageByStep();
            SetupButtonNextFinished();
            //Load du lieu len tab 1
            LoadDataTreeViewTab1();
        }

        #endregion

        #region "Load Data TreeView Tab 1"

        private void LoadDataTreeViewTab1()
        {
            _dlg = new WaitDialogForm();
            _inf.ItemType = _itemType;
            _inf.Conds = "";
                DataTable dtFinal = _inf.LoadProject();

            LoadDataTreeViewTab1(tlMain, dtFinal);
            _dlg.Close();


            tlMain.SelectNextControl(ActiveControl, true, true, true, true);
            tlMain.Focus();
            if (tlMain.AllNodesCount > 0)
            {
                tlMain.SetFocusedCellTreeList(tlMain.Nodes[0], tlMain.VisibleColumns[0]);
            }
        }

        private void LoadDataTreeViewTab1(TreeList tl, DataTable dt)
        {

            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";


            tl.BeginUpdate();

            //ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "CompanyCode", "CompanyName");


            //SetTreeListColumnHeader(tl.Columns["DepartmentCode"], "Department Code", false, HorzAlignment.Near, FixedStyle.None, "");


            tl.ExpandAll();


            tl.BestFitColumns();

            tl.ForceInitialize();


            tl.EndUpdate();
        }

        #endregion


        #region "Methold"



        private void VisibleTabPageByStep()
        {
            foreach (XtraTabPage page in xtraTabMain.TabPages)
            {
                Int32 tag = ProcessGeneral.GetSafeInt(page.Tag);
                page.PageVisible = tag == _step;
            }
        }

        private void SetupButtonNextFinished()
        {
            btnBack.Enabled = _step != 1;
            if (_step < TotalStep)
            {
                btnNextFinish.Text = @"Next";
                btnNextFinish.Image = Resources.forward_24x24_W;
                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+N)";
            }
            else
            {
                btnNextFinish.Text = @"Finish";
                btnNextFinish.Image = Resources.apply_24x24_W;
                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+F)";
            }
        }



        private DataTable GetTableSelectedInTab1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProjectCode", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            var q1 = tlMain.GetAllCheckedNodes().Where(p => !p.HasChildren).Select(p => new
            {
                ProjectCode = ProcessGeneral.GetSafeString(p.GetValue("ChildPK")),
                ProjectName = ProcessGeneral.GetSafeString(p.GetValue("ProjectName")),
            }).Distinct().ToList();
            if (!q1.Any())
                return dt;
            return q1.CopyToDataTableNew();
        }


        
        private List<TreeListNode> GetTableItemSelected()
        {

            return tlItemCode.GetAllCheckedNodes().Where(p => !p.HasChildren).ToList();

        }


        #endregion

        #region "Button Click Event"

        private void btnBack_Click(object sender, EventArgs e)
        {
            _step = _step - 1;
            VisibleTabPageByStep();
            SetupButtonNextFinished();


        }

        private void btnNextFinish_Click(object sender, EventArgs e)
        {

            txtFocus.SelectNextControl(ActiveControl, true, true, true, true);

            _step = _step + 1;


            if (_step == 2)
            {
                //Lay du lieu vao table da check chon o tab 1 de chuan bi day qua tab thu 2
                dtSelectedInTab1 = GetTableSelectedInTab1();

                if (dtSelectedInTab1.Rows.Count <= 0)
                {
                    XtraMessageBox.Show("No Row Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _step = _step - 1;
                    return;
                }

            }
            else if (_step == 3)
            {
                //kiem tra o tab 2 de check chon Node nao chua
                lNodeSelected = GetTableItemSelected();
                if (tlItemCode.AllNodesCount <= 0 || lNodeSelected.Count <= 0)
                {
                    XtraMessageBox.Show("No Row Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _step = _step - 1;
                    return;
                }
            }
            


            switch (_step)
            {
                case 2:
                    {
                        VisibleTabPageByStep();
                        SetupButtonNextFinished();
                        //load du lieu tab 2 theo check chon ben tab 1
                        LoadDataTreeViewTab2();
                        tlItemCode.SelectNextControl(ActiveControl, true, true, true, true);
                        tlItemCode.Focus();

                        if (tlItemCode.AllNodesCount > 0)
                        {
                            tlItemCode.SetFocusedCellTreeList(tlItemCode.Nodes[0], tlItemCode.VisibleColumns[0]);
                        }
                    }
                    break;
                case 3:
                    {
                        VisibleTabPageByStep();
                        SetupButtonNextFinished();
                        // Load du lieu tab 3 theo check chon o tab 2
                        LoadDataTreeViewTab3(tlFinal);
                        tlFinal.SelectNextControl(ActiveControl, true, true, true, true);
                        tlFinal.Focus();
                        if (tlFinal.AllNodesCount > 0)
                        {
                            tlFinal.SetFocusedCellTreeList(tlFinal.Nodes[0], tlFinal.VisibleColumns[0]);
                        }
                    }
                    break;
                default:
                    {
                        GenerateFinal();

                    }
                    break;
            }
        }

        private void GenerateFinal()
        {
            _dlg = new WaitDialogForm();
            bool isEvent = false;
            DataTable dtTemp = new DataTable();

            if (tlFinal.AllNodesCount > 0)
            {
                isEvent = true;
                DataTable dt = new DataTable();
                dt.Columns.Add("ParentPK", typeof (Int64));
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("UserName", typeof (string));

                var q1 = tlFinal.GetAllNodeTreeList().Where(p => !p.HasChildren).Select(p => new
                {
                    ParentPK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00001PK")),
                    ItemName = ProcessGeneral.GetSafeString(p.GetValue("ItemName")),
                    UserName = ProcessGeneral.GetSafeString(p.GetValue("UserName")),


                }).Distinct().ToList();
                if (!q1.Any())
                {
                    dtTemp = dt;
                }
                else
                {
                    dtTemp = q1.CopyToDataTableNew();
                }
            }

            if (OnPrWizard != null)
            {

                OnPrWizard(this, new SDRWizardEventArgs
                {
                    DtGenerate = dtTemp,
                    IsEvent = isEvent
                });

            }
            _dlg.Close();
            this.Close();

        }

        private void LoadDataTreeViewTab3(TreeList tl)
        {
            _dlg = new WaitDialogForm();
            DataTable dtI = tlItemCode.DataSource as DataTable;
           
            DataTable dt = dtI.Clone();
            List<TreeListNode> lNodeParent = lNodeSelected.Select(p => p.ParentNode).Distinct().ToList();


            foreach (TreeListNode parentNode in lNodeParent)
            {
                DataRowView pData = (DataRowView)tlItemCode.GetDataRecordByNode(parentNode);
                DataRow drP = pData.Row;
                dt.ImportRow(drP);

            }

            foreach (TreeListNode childNode in lNodeSelected)
            {
                DataRowView cData = (DataRowView)tlItemCode.GetDataRecordByNode(childNode);
                DataRow drC = cData.Row;
                dt.ImportRow(drC);
            }
            
            dt.AcceptChanges();

            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            tl.BeginUpdate();
            tl.ExpandAll();

            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "Check", "ProjectCode", "TDG00001PK", "UserName");


            SetTreeListColumnHeader(tl.Columns["Reference"], "Cus Ref.", false, HorzAlignment.Near, FixedStyle.None, "");

            SetTreeListColumnHeader(tl.Columns["ItemName"], "Item Name", false, HorzAlignment.Near, FixedStyle.None, "");

            //SetTreeListColumnHeader(tl.Columns["PRQty"], "Demand Qty", false, HorzAlignment.Center, FixedStyle.None,"");
            //tl.Columns["PRQty"].Format.FormatType = FormatType.Numeric;
            //tl.Columns["PRQty"].Format.FormatString = "#,0.##########";

            tl.BestFitColumns();

            tl.ForceInitialize();


            tl.EndUpdate();

            _dlg.Close();



        }

        private void LoadDataTreeViewTab2(TreeList tl, DataTable dt)
        {

            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            tl.BeginUpdate();
            ProcessGeneral.HideVisibleColumnsTreeList(tl, false,"Check", "ProjectCode", "TDG00001PK", "UserName");


            SetTreeListColumnHeader(tl.Columns["Reference"], "Cus Ref.", false, HorzAlignment.Near, FixedStyle.None, "");

            SetTreeListColumnHeader(tl.Columns["ItemName"], "Item Name", false, HorzAlignment.Near, FixedStyle.None, "");

            //SetTreeListColumnHeader(tl.Columns["PRQty"], "Demand Qty", false, HorzAlignment.Center, FixedStyle.None,"");
            //tl.Columns["PRQty"].Format.FormatType = FormatType.Numeric;
            //tl.Columns["PRQty"].Format.FormatString = "#,0.##########";

            tl.ExpandAll();

            tl.BestFitColumns();
            //tl.Columns["Selected"].Width = +200;
            tl.ForceInitialize();


            tl.EndUpdate();

            tl.BeginUpdate();

            foreach (TreeListNode node in tl.Nodes)
            {
                TreeListNodes lNode = node.Nodes;
                var q1 = lNode.Where(p => ProcessGeneral.GetSafeBool(p.GetValue("Check"))).ToList();
                if (q1.Count == lNode.Count)
                {
                    node.CheckAll();
                }
                else
                {
                    foreach (TreeListNode nodeChild in q1)
                    {
                        nodeChild.CheckState = CheckState.Checked;
                    }
                }


            

            }



            tl.EndUpdate();



          
        }

   

        private void LoadDataTreeViewTab2()
        {


            _dlg = new WaitDialogForm();
            //dua du lieu vao table dua theo check chon o tab 1 va loai bo du lieu da ton tai
            //Chu y phai co cot Check kieu Bit (mac dinh =1)
            DataTable dtPRItem = _inf.LoadGridSelectProductWhenGenerate(dtSelectedInTab1, _dtUserAssignProduct);
            if (dtPRItem.Rows.Count <= 0)
            {
                _dlg.Close();
                return;
            }

            // gan check theo Group da chon o tab 1
            DataTable dtFinal = StandardTreeTableItemCode(dtPRItem, dtSelectedInTab1);

            //Load du lieu sau khi da gan dc ParentPK va da Group vao
            LoadDataTreeViewTab2(tlItemCode, dtFinal);

            _dlg.Close();
        }

        private DataTable StandardTreeTableItemCode(DataTable dtTreeTemp, DataTable dtGroup)
        {
            //so bat dau ParentPK 
            Int64 beginParentPk = dtTreeTemp.Rows.Count + 10;
            //gan PK cho tung group
            var qP0 = dtGroup.AsEnumerable().Select(p => p.Field<string>("ProjectCode")).Distinct().Select(
                (p, inx) => new
                {
                    Index = inx + beginParentPk,
                    Code = p.ToString()
                }).ToList();

            //dua vao dictionary join lai
            Dictionary<string, GroupInWizard> qP1 = qP0.Join(dtGroup.AsEnumerable(), p => p.Code, t => t.Field<string>("ProjectCode"), (p, t) => new
            {
                KeyDic = t.Field<string>("ProjectCode"),
                TempData = new GroupInWizard
                {
                    Code = p.Code,
                    Description = t.Field<string>("ProjectName"),
                    Index = p.Index
                },

            }).ToDictionary(item => item.KeyDic, item => item.TempData);

            // add them 3 cot de lam check group
            dtTreeTemp.Columns.Add("ChildPK", typeof(Int64));
            dtTreeTemp.Columns.Add("ParentPK", typeof(Int64));
            dtTreeTemp.Columns.Add("RowIndex", typeof(Int64));
            int iLoop = 1;
            // gan so vao ChildPk va RowIndex va dua vao ParentPK
            foreach (DataRow drSource in dtTreeTemp.Rows)
            {
                
                string DepartmentCode = ProcessGeneral.GetSafeString(drSource["ProjectCode"]);

                drSource["ChildPK"] = iLoop;
                drSource["RowIndex"] = iLoop;

                GroupInWizard tempInfo1;
                if (qP1.TryGetValue(DepartmentCode, out tempInfo1))
                {
                    drSource["ParentPK"] = tempInfo1.Index;
                    //globalGrTemp = String.Format("{0}-{1}", tempInfo1.Code, tempInfo1.Description);
                    //drSource["Selected"] = tempInfo1.Code;
                }
               
                iLoop++;



            }
     
                var qP2 = qP1.Select(p => new
            {
                Code = p.Value.Code,
                Description = p.Value.Description,
                Index = p.Value.Index,
            }).Distinct().ToList();
            //gan ten vao cot can hien group
            foreach (var item in qP2)
            {
                DataRow drAddTree = dtTreeTemp.NewRow();
                drAddTree["Selected"] = String.Format("{0}-{1}", item.Code, item.Description);
                drAddTree["ChildPK"] = item.Index;
                drAddTree["ParentPK"] = 0;

                dtTreeTemp.Rows.Add(drAddTree);
            }
            dtTreeTemp.AcceptChanges();

            return dtTreeTemp;

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region "Proccess Treeview"



        private void SetTreeListColumnHeader(TreeListColumn tlCol, string caption, bool allowSort, HorzAlignment align,
            DevExpress.XtraTreeList.Columns.FixedStyle fixedStyle, object tag, bool allowFilter = true)
        {

            tlCol.AppearanceCell.TextOptions.HAlignment = align;
            tlCol.AppearanceHeader.TextOptions.HAlignment = align;
            tlCol.Caption = caption;
            tlCol.Fixed = fixedStyle;
            tlCol.OptionsColumn.AllowMove = false;
            tlCol.OptionsColumn.AllowSort = allowSort; //
            tlCol.Tag = tag;
            if (allowFilter)
            {
                tlCol.OptionsFilter.AllowFilter = true;
                tlCol.OptionsFilter.AllowAutoFilter = true;
                tlCol.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;
            }
            else
            {
                tlCol.OptionsFilter.AllowFilter = false;
                tlCol.OptionsFilter.AllowAutoFilter = false;
                //    tlCol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Default;

            }


        }




        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.Product_BoM_16x16);
            imgLt.Images.Add(Resources.RawMaterial_BoM_16x16);
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
            treeList.OptionsFind.AllowFindPanel = true; // cho phép Ctrl+F để tìm hay ko
            treeList.OptionsFind.AlwaysVisible = true; // có mặc định show cái tìm kiếm lên hay ko
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = true;

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
            treeList.OptionsBehavior.KeepSelectedOnClick = false;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.AllowDrop = false;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = true;


            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };
            

            treeList.ShowingEditor += TreeList_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;






            treeList.NodeCellStyle += TreeList_NodeCellStyle;

            treeList.GetStateImage += TreeList_GetStateImage;

       



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
    
            bool isCheck = e.Node.CheckState == CheckState.Checked;
       

            switch (fieldName)
            {
                case "DepartmentCode":
                    {
                        e.Appearance.ForeColor = Color.DarkBlue;
                    }
                    break;
                case "DepartmentName":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;

            }

            switch (fieldName)
            {
                case "DepartmentCode":
                case "DepartmentName":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Lavender;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                default:
                    {

                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                    break;
        
                    //case "IsFactor":
                    //    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    //    e.Appearance.BackColor = Color.DarkRed;
                    //    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    //    break;


            }


          




        }

        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = node.ParentNode == null ? 0 : 1;
        }

        private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;


            bool isCheck = e.Node.CheckState == CheckState.Checked;

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

        //  private int updateRmSourceNo = 1;
        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            e.Cancel = true;


        }

        #endregion
        
        #region "Proccess Treeview ItemCode"

        private void InitTreeListItemCode(TreeList treeList)
        {

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true; // cho phép Ctrl+F để tìm hay ko
            treeList.OptionsFind.AlwaysVisible = true; // có mặc định show cái tìm kiếm lên hay ko
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = true;

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
            treeList.OptionsBehavior.KeepSelectedOnClick = false;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.AllowDrop = false;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = true;


            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };







            treeList.ShowingEditor += TreeListItemCode_ShowingEditor;
            treeList.CustomDrawNodeIndicator += TreeListItemCode_CustomDrawNodeIndicator;

            treeList.NodeCellStyle += TreeListItemCode_NodeCellStyle;

            treeList.GetStateImage += TreeListItemCode_GetStateImage;
            treeList.GetNodeDisplayValue += TreeListItemCode_GetNodeDisplayValue; // hien thi the dieu kien

            treeList.BeforeCheckNode += TreeListItemCode_BeforeCheckNode;
            treeList.AfterCheckNode += TreeList_AfterCheckNode;



        }

        private void TreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;

            TreeListNode parentNode = node.ParentNode == null ? node : node.ParentNode;
            if(node.HasChildren)
            {
                List<TreeListNode> q1 = node.Nodes.Where(p => !ProcessGeneral.GetSafeBool(p.GetValue("Check"))).ToList();
               foreach(TreeListNode tlNode in q1)
                {
                    tlNode.Checked = false;
                }
            }

        }

        private void TreeListItemCode_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            bool isCheck;
            if (node.HasChildren)
            {
             
                isCheck = node.Nodes.Any(p => ProcessGeneral.GetSafeBool(p.GetValue("Check")));
                e.CanCheck = isCheck;
            }
            else
            {
                isCheck = ProcessGeneral.GetSafeBool(node.GetValue("Check"));
                e.CanCheck = isCheck;
            }



        }

        private void TreeListItemCode_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "Selected":
                    {
                        // neu ko phai la node cha thi hien thi ''
                        if (node.ParentNode != null)
                        {
                            e.Value = "";
                        }
                    }
                    break;
            }
        }

        private void TreeListItemCode_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = e.Node.HasChildren ? 0 : 1;
        }


        private void TreeListItemCode_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            bool isParent = node.ParentNode == null;
            bool isCheck = e.Node.CheckState == CheckState.Checked;
           

            if (isParent)
            {
                e.Appearance.Font = new Font("Tahoma", 9F, (FontStyle.Bold), GraphicsUnit.Point, 0);
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
            }
            switch (fieldName)
            {
                case "Selected":
                    {


                        e.Appearance.ForeColor = Color.MediumVioletRed;
                    }
                    break;
                case "UserName":
                    {


                        e.Appearance.ForeColor = Color.DarkBlue;
                    }
                    break;
                case "FullName":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;

            }
            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {



                if (!isParent)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {

                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }

                }



            }
            else
            {
                if (fieldName == "MainMaterialGroup")
                {
                    if (isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LavenderBlush;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {

                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                }
                else if (fieldName == "POQty")
                {
                  
                    if (!isParent)
                    {
                        bool IsFactor = ProcessGeneral.GetSafeBool(e.Node.GetValue("IsFactor"));
                        if (!IsFactor)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.OrangeRed;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.GhostWhite;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                  
                    }

                }
                else if (fieldName == "PRQty")
                {

                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Lavender;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                }                
                else
                {
                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
            }





        }

        private void TreeListItemCode_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;


            bool isCheck = e.Node.CheckState == CheckState.Checked;

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

        //  private int updateRmSourceNo = 1;
        private void TreeListItemCode_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            //switch(fieldName)
            //{
            //    case "":

            //        break;

            //}
            e.Cancel = true;

        }


        #endregion

        #region "Proccess Treeview Final"

        private void InitTreeListFinal(TreeList treeList)
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
            treeList.OptionsBehavior.KeepSelectedOnClick = false;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.AllowDrop = false;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = false;


            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };







            treeList.ShowingEditor += TreeListFinal_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeListFinal_CustomDrawNodeIndicator;






            treeList.NodeCellStyle += TreeListFinal_NodeCellStyle;

            treeList.GetStateImage += TreeListFinal_GetStateImage;


            treeList.GetNodeDisplayValue += TreeListFinal_GetNodeDisplayValue;






        }
        private void TreeListFinal_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "Selected":
                    {
                        if (node.ParentNode != null)
                        {
                            e.Value = "";
                        }
                    }
                    break;
            }
        }

        private void TreeListFinal_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = e.Node.HasChildren ? 0 : 1;
        }

        private void TreeListFinal_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            bool isParent = node.ParentNode == null;
            bool isCheck = e.Node.CheckState == CheckState.Checked;

            if (isParent)
            {
                e.Appearance.Font = new Font("Tahoma", 9F, (FontStyle.Bold), GraphicsUnit.Point, 0);
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
            }
            switch (fieldName)
            {
                case "Selected":
                    {


                        e.Appearance.ForeColor = Color.MediumVioletRed;
                    }
                    break;
                case "UserName":
                    {


                        e.Appearance.ForeColor = Color.DarkBlue;
                    }
                    break;
                case "FullName":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;

            }
            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {



                if (!isParent)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {

                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }

                }



            }
            else
            {
                if (fieldName == "MainMaterialGroup")
                {
                    if (isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LavenderBlush;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {

                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                }
                else if (fieldName == "POQty")
                {
                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.GhostWhite;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }

                }
                else if (fieldName == "PRQty")
                {

                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Lavender;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                }
                else
                {
                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
            }

        }

        private void TreeListFinal_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;


            bool isCheck = e.Node.Selected;

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

        //  private int updateRmSourceNo = 1;
        private void TreeListFinal_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            //  string fieldName = col.FieldName;
            e.Cancel = true;

        }


        #endregion


        #region "hotkey"


        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {


                #region "Command"
                case Keys.Control | Keys.Shift | Keys.B:
                    {
                        if (btnBack.Enabled)
                        {
                            btnBack_Click(null, null);
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.C:
                    {
                        this.Close();
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.N:
                    {

                        if (btnNextFinish.Enabled && btnNextFinish.Text == @"Next")
                        {
                            btnNextFinish_Click(null, null);
                        }
                        return true;
                    }

                case Keys.Control | Keys.Shift | Keys.F:
                    {
                        if (btnNextFinish.Enabled && btnNextFinish.Text == @"Finish")
                        {
                            btnNextFinish_Click(null, null);
                        }
                        return true;
                    }

                    #endregion

            }
            return base.ProcessCmdKey(ref message, keys);



        }


        #endregion


    }
}

