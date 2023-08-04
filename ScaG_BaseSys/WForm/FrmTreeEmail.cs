using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_BaseSys.Properties;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using SummaryItemType = DevExpress.Data.SummaryItemType;

namespace CNY_BaseSys.WForm
{
    public partial class FrmTreeEmail : DevExpress.XtraEditors.XtraForm
    {
        #region "Properties"
        private readonly DataTable _dtDepartment;
        public event TransferDataOnGridViewHandler OnTransferData = null;
        private readonly string[] _arrMailTo;
        #endregion


        #region "Contructor"



        public FrmTreeEmail(DataTable dtDepartment, string[] arrMailTo)
        {
            InitializeComponent();
            _arrMailTo = arrMailTo;
            _dtDepartment = dtDepartment;

            InitTreeList(tlMain);

            this.Load += FrmTreeEmail_Load;
            chkAll.CheckedChanged += ChkAll_CheckedChanged;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            btnCancel.Click += BtnCancel_Click;
            btnNextFinish.Click += BtnNextFinish_Click;
            btnCheckAll.Click += BtnCheckAll_Click;



        }

        private void CheckAllNodeTree(bool status)
        {
          

        
            foreach (TreeListNode node in tlMain.Nodes)
            {
                node.Checked = status;
                foreach (TreeListNode childNode in node.Nodes)
                {
                    childNode.Checked = status;
                }


            }
        }

        private void SetInfoCheckButton(bool enable, Image image, string text)
        {
            btnCheckAll.Image = image;
            btnCheckAll.ToolTip = text;
            btnCheckAll.Enabled = enable;
        }
        private void BtnCheckAll_Click(object sender, EventArgs e)
        {
            if (tlMain.AllNodesCount <= 0) return;
            if (btnCheckAll.ToolTip == @"Check All")
            {
                //foreach (TreeListNode node in tlMain.Nodes)
                //{
                //    node.Checked = true;
                //}
                CheckAllNodeTree(true);
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
            }
            else
            {
                //foreach (TreeListNode node in tlMain.Nodes)
                //{
                //    node.Checked = false;
                //}
                CheckAllNodeTree(false);
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }


        }


        private void BtnNextFinish_Click(object sender, EventArgs e)
        {
            if (OnTransferData != null)
            {
                Dictionary<string, UserSystemEmailInfo> dicEmail = new Dictionary<string, UserSystemEmailInfo>();


                List<TreeListNode> lNode = tlMain.GetAllCheckedNodes().Where(p=>!p.HasChildren).ToList();
                foreach (TreeListNode node in lNode)
                {
                    TreeListNode parentNode = node.ParentNode;
                    
                    string name = ProcessGeneral.GetSafeString(node.GetValue("Name"));
                    string code = ProcessGeneral.GetSafeString(node.GetValue("Code"));
                    string department = ProcessGeneral.GetSafeString(parentNode.GetValue("Name"));
                    string position = ProcessGeneral.GetSafeString(node.GetValue("Position"));
                    string email = ProcessGeneral.GetSafeString(node.GetValue("Email"));
                    int ind = name.LastIndexOf(" ", StringComparison.Ordinal);
                    string fristName = "";
                    if (ind >= 0)
                    {
                        fristName = name.Substring(ind + 1, name.Length - ind - 1);
                    }


                    UserSystemEmailInfo info = new UserSystemEmailInfo
                    {
                        Name = name,
                        Code = code,
                        Department = department,
                        Position = position,
                        FristName = fristName

                    };
                    dicEmail.Add(email,info);

                }



                // List<DataRow> returnRowsSelected = lR.Select(i => gvMain.GetDataRow(i)).ToList();
                OnTransferData(this, new TransferDataOnGridViewEventArgs
                {
                    DicEmail = dicEmail,
                });
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e){
            LoadDataTreeView(tlMain);
        }

        private void FrmTreeEmail_Load(object sender, EventArgs e)
        {
            Rectangle screen = Screen.FromControl(this).WorkingArea;
            this.Height = screen.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Location.X, 0);
            LoadDataTreeView(tlMain);
            
        }
        #endregion



       



        #region "Proccess Treeview"




        private void LoadDataTreeView(TreeList tl)
        {
            DataTable dtWhere = chkAll.Checked ? ProcessGeneral.GetDefaultTableStringPk() : _dtDepartment;
            DataTable dt = ProcessGeneral.GetListEmailByDepartmentTree(DeclareSystem.SysConnectionString, dtWhere,  _arrMailTo);
         
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
         

          

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Code"], "Code", false, HorzAlignment.Near, FixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Name"], "Name", false, HorzAlignment.Near, FixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Email"], "Email", false, HorzAlignment.Near, FixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Position"], "Position", false, HorzAlignment.Near, FixedStyle.None, "");
            



         

            tl.ExpandAll();
            tl.BestFitColumns();













         

            tl.ForceInitialize();

            //tl.BeginSort();
            //tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            //tl.EndSort();

           



            tl.EndUpdate();


            if (!chkAll.Checked && _dtDepartment.Rows.Count > 0)
            {
                foreach (TreeListNode node in tl.Nodes)
                {
                    node.Checked = true;
                    foreach (TreeListNode childNode in node.Nodes)
                    {
                        childNode.Checked = true;
                    }
                }
            }


            if (isShowFind)
            {
                tl.ShowFindPanelTreeList(findPanelText);
            }

            tl.Tag = dt.Rows.Count;
            SetAfterCheckNode(tl);


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


            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = true;
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



            treeList.OptionsView.AllowHtmlDrawHeaders = true;


            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;

            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;



            treeList.OptionsCustomization.AllowColumnResizing = true;

            treeList.OptionsCustomization.AllowQuickHideColumns = true;
            treeList.OptionsCustomization.AllowSort = true;
            treeList.OptionsCustomization.AllowFilter = true;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = true;

            treeList.OptionsCustomization.AllowColumnMoving = false;
            //treeList.OptionsMenu.EnableColumnMenu = true;



            treeList.OptionsBehavior.AllowExpandOnDblClick = true;

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,
                AllowColumnsChooser = true,

            };
         


            treeList.OptionsBehavior.ShowEditorOnMouseUp = false;


            treeList.GetStateImage += TreeList_GetStateImage;
            treeList.ShowingEditor += TreeList_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;





            treeList.NodeCellStyle += TreeList_NodeCellStyle;

            treeList.AfterCheckNode += TreeList_AfterCheckNode;






        }


        private void TreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            TreeList tl = (TreeList)sender;
            if (tl == null) return;
            SetAfterCheckNode(tl);


        }

        private void SetAfterCheckNode(TreeList tl)
        {
            int count = ProcessGeneral.GetSafeInt(tl.Tag);
            List<TreeListNode> lCheckNode = tl.GetAllCheckedNodes().ToList();
            if (lCheckNode.Count >= count)
            {
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");

            }
            else
            {
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }
        }






















        private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            //return;


            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;
            bool select = e.Node.Checked;
            LinearGradientBrush backBrush;
            if (select)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
            }
            else
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
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
            e.Cancel = true;


        }



      


        private void TreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {

           
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            if (!col.Visible) return;
            string fieldName = col.FieldName;


            if (node.HasChildren)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkGreen;
            }

            bool selected = node.Checked;


            if (node.HasChildren)
            {
                if (selected)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
            }
            else
            {
                if (fieldName == "Email")
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.Cornsilk;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {
                    if (selected)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
            }

        

        }

        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = node.HasChildren ? 0 : 1;
           
        }
      




        #endregion
    }
}