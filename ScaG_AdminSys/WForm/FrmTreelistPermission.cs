using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CNY_AdminSys.Properties;
using CNY_BaseSys.Common;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using FixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;

namespace CNY_AdminSys.WForm
{
    public partial class FrmTreelistPermission : DevExpress.XtraEditors.XtraForm
    {
     

        #region "Property And Field"
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        private readonly List<TreeListNode> _lNodeDelete = new List<TreeListNode>();
        #endregion

        #region "Contructor"

        public FrmTreelistPermission(string userName, DataTable dtS)
        {

            //          DataTable dtTree = _ctrl.LoadTreeViewMainMenu(SystemProperty.SysIdAuthorization, SystemProperty.SysUserInGroupId);
            InitializeComponent();



            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
        
            this.Text = string.Format("Tree Menu Viewer [User : {0}]",userName);




            InitTreeView(treeList1);
            LoadTreeViewMenuFinal(treeList1, dtS);
            MaximizeBox = false;
            MinimizeBox = false;



        }

     

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }



     
        #endregion

     
  
       

    


  
        #region "Init Treeview Template"






        /// <summary>
        ///     For Load icon Into Treeview
        /// </summary>
        /// <returns>
        ///     A System.Windows.Forms.ImageList value...
        /// </returns>
        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.folder_yellow_Close_icon);
            imgLt.Images.Add(Resources.folder_yellow_open_icon);
            imgLt.Images.Add(Resources.Document_txt_icon);
            imgLt.Images.Add(Resources.knowledgebasearticle_16x16);
            imgLt.Images.Add(Resources.sendbehindtext_16x16);
            imgLt.Images.Add(Resources.suggestion_16x16);
            return imgLt;
        }
        /// <summary>
        ///     Init Treeview template
        /// </summary>
        private void InitTreeView(TreeList treeList)
        {
            treeList.AllowDrop = false;




            treeList.OptionsDragAndDrop.CanCloneNodesOnDrop = true;

            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = false;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = false;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsFind.ShowFindButton = false;
            treeList.OptionsFind.ShowClearButton = false;
            treeList.OptionsView.ShowAutoFilterRow = false;

            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.ShowHorzLines = false;
            treeList.OptionsView.ShowVertLines = false;
            treeList.OptionsView.ShowIndicator = false;
            treeList.OptionsView.AutoWidth = true;
            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;
            treeList.StateImageList = GetImageListDisplayTreeView();
            treeList.OptionsBehavior.AutoChangeParent = false;
            treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            treeList.OptionsBehavior.AutoNodeHeight = true;

            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = false;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Always;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = false;


            treeList.OptionsBehavior.ExpandNodesOnFiltering = true;

            treeList.OptionsSelection.MultiSelect = false;
            treeList.OptionsSelection.MultiSelectMode = TreeListMultiSelectMode.RowSelect;
            treeList.OptionsSelection.SelectNodesOnRightClick = false;
            treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeList.OptionsSelection.EnableAppearanceFocusedRow = false;






            treeList.GetStateImage += TreeList_GetStateImage;
            treeList.ShowingEditor += TreeList_ShowingEditor;









            treeList.NodeCellStyle += TreeList_NodeCellStyle;






            treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;

        }

        private void TreeList_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.FieldName == "FormName")
            {
                e.RepositoryItem = _repositoryTextGrid;
            }
        }


        
     
        private void TreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;


            if (node.HasChildren)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                if (node.Expanded)
                {

                    e.Appearance.BackColor = Color.BlanchedAlmond;
                    e.Appearance.BackColor2 = Color.Azure;
                }
            }



            if (treeList1.FocusedNode == e.Node && treeList1.FocusedColumn == e.Column)
            {
                e.Appearance.BackColor = Color.BlanchedAlmond;
                e.Appearance.BackColor2 = Color.SkyBlue;
            }
        }

        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;



        }
    




        private void TreeList_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            int isGuide = ProcessGeneral.GetSafeInt(e.Node.GetValue("GuideDocument"));
            int isProcess = ProcessGeneral.GetSafeInt(e.Node.GetValue("ProcessDocument"));
            if (isGuide == 1 && isProcess == 1)
            {
                e.NodeImageIndex = 5;
                return;
            }
            if (isGuide == 1)
            {
                e.NodeImageIndex = 3;
                return;
            }
            if (isProcess == 1)
            {
                e.NodeImageIndex = 4;
                return;
            }
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





        #region "Login"




        private void LoadTreeViewMenuFinal(TreeList tl, DataTable dtTree)
        {
            tl.OptionsView.ShowColumns = true;
  



    


            tl.Columns.Clear();
            tl.DataSource = null;
            tl.DataSource = dtTree;
            tl.ParentFieldName = "MenuParent";
            tl.KeyFieldName = "MenuChild";



            tl.BeginUpdate();
            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "MenuCode", "FormCode", "ProjectCode", "FolderContainForm", "RoleInsert", "RoleUpdate", "RoleDelete",
                "RoleView", "AdvanceFunction", "Level", "SortOrder", "Visible"
                , "SpecialFunction", "CheckAdvanceFunction", "ProcessDocument", "GuideDocument", "ShowCode");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormName"], "Form Name", false, HorzAlignment.Near, FixedStyle.None, "");
            tl.ExpandAll();

            tl.ForceInitialize();
            tl.BeginSort();
            tl.Columns["SortOrder"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();
            tl.OptionsView.ShowColumns = false;
            GetNodeDelete(tl);


        }


        private void GetNodeDelete(TreeList tl)
        {

            tl.BeginUpdate();
            tl.LockReloadNodes();
            _lNodeDelete.Clear();
            foreach (TreeListNode node in tl.Nodes)
            {

                GetNodeGoDown(node);
            }

            if (_lNodeDelete.Count > 0)
            {
                List<TreeListNode> lNode = new List<TreeListNode>();
                foreach (TreeListNode nodeDel in _lNodeDelete)
                {
                    TreeListNode nodeAdd = GetNodeDelete(nodeDel);
                    if (nodeAdd == null) continue;
                    lNode.Add(nodeAdd);
                }
                _lNodeDelete.Clear();
                foreach (TreeListNode nodeF in lNode)
                {
                    tl.DeleteNode(nodeF);
                }
            }
            tl.UnlockReloadNodes();
            tl.EndUpdate();

        }


        private TreeListNode GetNodeDelete(TreeListNode nodeDel)
        {

            TreeListNode parentNode = nodeDel.ParentNode;
            if (parentNode == null) return nodeDel;
            string check = ProcessGeneral.GetSafeString(parentNode.Tag);
            if (check == "Check") return null;
            parentNode.Tag = "Check";
            bool beforValue1 = ProcessGeneral.GetSafeBool(parentNode.GetValue("Visible"));
            if (!beforValue1)
            {
                return GetNodeDelete(parentNode);
            }
            return nodeDel;




        }


        private void GetNodeGoDown(TreeListNode tlNode)
        {

            bool visibleL1 = ProcessGeneral.GetSafeBool(tlNode.GetValue("Visible"));

            if (tlNode.Nodes.Count > 0)
            {
                foreach (TreeListNode node in tlNode.Nodes)
                {
                    bool visibleL2 = ProcessGeneral.GetSafeBool(node.GetValue("Visible"));
                    if (visibleL2)
                        tlNode.SetValue("Visible", true);

                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeListNode childNode in node.Nodes)
                        {
                            bool visibleL3 = ProcessGeneral.GetSafeBool(childNode.GetValue("Visible"));
                            if (visibleL3)
                            {
                                node.SetValue("Visible", true);
                            }
                            if (childNode.Nodes.Count > 0)
                            {
                                GetNodeGoDown(childNode);
                            }
                            else
                            {


                                if (!visibleL3)
                                    _lNodeDelete.Add(childNode);
                                GetNodeGoUp(childNode.ParentNode, visibleL3);
                            }




                        }
                    }
                    else
                    {
                        if (!visibleL2)
                            _lNodeDelete.Add(node);
                        GetNodeGoUp(node.ParentNode, visibleL2);
                    }

                }
            }
            else
            {
                if (!visibleL1)
                    _lNodeDelete.Add(tlNode);
                GetNodeGoUp(tlNode.ParentNode, visibleL1);
            }


        }
        private void GetNodeGoUp(TreeListNode tlNode, bool beforValue)
        {
            if (tlNode == null) return;
            bool currentValue = ProcessGeneral.GetSafeBool(tlNode.GetValue("Visible"));
            string currentTag = ProcessGeneral.GetSafeString(tlNode.Tag);
            if (currentValue && currentTag == "Visible") return;
            TreeListNode parentNode = tlNode.ParentNode;
            if (parentNode == null) return;
            if (beforValue)
            {
                parentNode.SetValue("Visible", true);
                parentNode.Tag = "Visible";
            }
            bool beforValue1 = ProcessGeneral.GetSafeBool(parentNode.GetValue("Visible"));
            GetNodeGoUpDetail(parentNode.ParentNode, beforValue1);


        }

        private void GetNodeGoUpDetail(TreeListNode currentNode, bool beforValue)
        {
            if (currentNode == null) return;
            bool currentValue = ProcessGeneral.GetSafeBool(currentNode.GetValue("Visible"));
            string currentTag = ProcessGeneral.GetSafeString(currentNode.Tag);
            if (currentValue && currentTag == "Visible") return;



            if (beforValue)
            {
                currentNode.SetValue("Visible", true);
                currentNode.Tag = "Visible";
            }
            bool beforValue1 = ProcessGeneral.GetSafeBool(currentNode.GetValue("Visible"));
            GetNodeGoUpDetail(currentNode.ParentNode, beforValue1);


        }        /// <summary>
                 /// Sau khi đăng nhập, hiện menu, thay đổi nút đăng nhập -> đăng xuất
                 /// </summary>
    

        #endregion
        
        #region "hotkey"


        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
             
                case Keys.Escape:
                    {

                        this.Close();
                        return true;
                    }
              

            }
            return base.ProcessCmdKey(ref message, keys);



        }

  
        #endregion


       
    }
}