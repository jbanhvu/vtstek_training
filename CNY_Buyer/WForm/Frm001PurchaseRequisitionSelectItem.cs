using CNY_BaseSys.Common;
using CNY_Buyer.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_Buyer.WForm
{
    public partial class Frm001PurchaseRequisitionSelectItem : Form
    {
        Inf_004MaterialRequirement _inf004 = new Inf_004MaterialRequirement();
        Inf_005MaterialRequirementDetail _inf005 = new Inf_005MaterialRequirementDetail();
        int _step = 1;
        int TotalStep = 2;
        public event OnGetValueHandler OnGetValue = null;
        DataTable dtDelete;
        public Frm001PurchaseRequisitionSelectItem(DataTable _dtDelete)
        {
            InitializeComponent();
            btnNextFinish.Click += BtnNextFinish_Click;
            btnBack.Click += BtnBack_Click;
            chkCheckRequest.EditValueChanged += ChkCheckRequest_EditValueChanged;
            chkItem.EditValueChanged += ChkItem_EditValueChanged;
            dtDelete = _dtDelete;
            InitTreeList(tlSO);
            InitTreeList(tlItem);
            InitColumtlSO();
            InitColumtlItem();
            GetMR();
            
        }
        private void InitTreeList(TreeList treeList)
        {
            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsBehavior.EditorShowMode = TreeListEditorShowMode.MouseDown;
            treeList.OptionsBehavior.AllowExpandOnDblClick = false;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = true;

            treeList.OptionsBehavior.Editable = true;
            treeList.OptionsView.ShowColumns = true;
            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;
            treeList.OptionsView.AutoWidth = true;
            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;
            treeList.OptionsView.AllowHtmlDrawHeaders = true;
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
            treeList.OptionsBehavior.AllowRecursiveNodeChecking = false;
            treeList.OptionsView.ShowCheckBoxes = true;
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,
                ShowAutoFilerMenu = true,
            };
            treeList.OptionsMenu.EnableNodeMenu = false;
        }
        private void ChkItem_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit chk = sender as CheckEdit;
            CheckUncheckAllNodes(tlItem, chk.Checked);
        }

        private void ChkCheckRequest_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit chk = sender as CheckEdit;
            CheckUncheckAllNodes(tlSO, chk.Checked);
        }
        private void CheckUncheckAllNodes(TreeList tl, bool isChecked)
        {
            foreach (TreeListNode node in tl.Nodes)
            {
                node.Checked = isChecked;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            _step = _step - 1;
            VisibleTabPageByStep();
            SetupButtonNextFinished();
        }

        private void BtnNextFinish_Click(object sender, EventArgs e)
        {
            if(_step==1)
            {
                _step++;
                GetMRDetail();
                VisibleTabPageByStep();
                SetupButtonNextFinished();
                //LoadDataFinal();
            }
            else if (_step==2)
            {
                GenerateFinal();
            }
        }
        private void GenerateFinal()
        {
            if (OnGetValue != null)
            {
                OnGetValue(this, new OnGetValueEventArgs
                {
                    Value = GetAndConvertSelectedNodes(),
                    Text = "",
                });
            }
            this.Close();
        }
        private DataTable GetAndConvertSelectedNodes()
        {
            List<TreeListNode> selectedNodes = tlItem.GetAllCheckedNodes();

            DataTable selectedNodesDataTable = new DataTable();

            // Sử dụng các cột trong DataSource của TreeList để tạo cột trong DataTable
            foreach (TreeListColumn column in tlItem.Columns)
            {
                selectedNodesDataTable.Columns.Add(column.FieldName, column.ColumnType);
            }

            // Thêm dữ liệu từ các node đã chọn vào DataTable
            foreach (TreeListNode node in selectedNodes)
            {
                DataRow newRow = selectedNodesDataTable.NewRow();

                foreach (TreeListColumn column in tlItem.Columns)
                {
                    newRow[column.FieldName] = node[column];
                }

                selectedNodesDataTable.Rows.Add(newRow);
            }

            return selectedNodesDataTable;
        }
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
                btnNextFinish.Image = Properties.Resources.forward_24x24_W;
                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+N)";
            }
            else
            {
                btnNextFinish.Text = @"Finish";
                btnNextFinish.Image = Properties.Resources.apply_24x24_W;
                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+F)";
            }
        }
        public void GetMR()
        {
            tlSO.OptionsView.ShowCheckBoxes=true;
            DataTable dt = _inf004.sp_MaterialRequirement_SelectForPRGenerate();
            tlSO.DataSource = dt;
            tlSO.BestFitColumns();
        }
        public void GetMRDetail()
        {
            tlItem.OptionsView.ShowCheckBoxes = true;
            DataTable dtPK = new DataTable();
            dtPK.Columns.Add("PK", typeof(Int64));
            List<TreeListNode> nodes = tlSO.GetAllCheckedNodes();
            foreach (TreeListNode node in nodes)
            {
                dtPK.Rows.Add(node["PK"]);
            }
            DataTable dt = _inf005.sp_MaterialRequirementDetail_SelectByListMaterialRequirementPK(dtPK);
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                int materialRequirementPK = Convert.ToInt32(dt.Rows[i]["MaterialRequirementPK"]);
                int ordinalNumber = Convert.ToInt32(dt.Rows[i]["OrdinalNumber"]);

                DataRow[] matchingRows = dtDelete.Select($"MaterialRequirementPK = {materialRequirementPK} AND OrdinalNumber = {ordinalNumber}");

                if (matchingRows.Length > 0)
                {
                    dt.Rows.RemoveAt(i);
                }
            }
            tlItem.DataSource = dt;
            tlItem.BestFitColumns();
        }
        public void CreateColumnOnTreeList(TreeList tl, string Caption, string Name, int VisibleIndex)
        {
            var gridColumn10 = new TreeListColumn();
            gridColumn10.Caption = Caption;
            gridColumn10.FieldName = Name;
            gridColumn10.Name = Name;
            gridColumn10.VisibleIndex = VisibleIndex;
            tl.Columns.Add(gridColumn10);
        }
        #region InitColumGridview
        private void InitColumtlSO()
        {
            CreateColumnOnTreeList(tlSO, "PK", "PK", -1);
            CreateColumnOnTreeList(tlSO, "Số đề nghị", "PK", 1);
            CreateColumnOnTreeList(tlSO, "Mã dự án", "ProjectCode", 1);
            CreateColumnOnTreeList(tlSO, "Tên dự án", "ProjectName", 2);
            CreateColumnOnTreeList(tlSO, "Lần đề nghị", "RequestTimes", 2);
            CreateColumnOnTreeList(tlSO, "Người yêu cầu", "Requester", 2);
            CreateColumnOnTreeList(tlSO, "Chức vụ", "Position", 3);
            CreateColumnOnTreeList(tlSO, "Bộ phận", "Department", 4);
            CreateColumnOnTreeList(tlSO, "Mực độ yêu cầu", "CriticalLevel", 4);
            CreateColumnOnTreeList(tlSO, "Ngày đề nghị", "CreatedDate", 4);
            CreateColumnOnTreeList(tlSO, "Ghi chú", "Note", 8);
            CreateColumnOnTreeList(tlSO, "Created By", "CreatedBy", 8);
            CreateColumnOnTreeList(tlSO, "Created Date", "CreatedDate", 9);
            CreateColumnOnTreeList(tlSO, "Updated By", "UpdatedBy", 10);
            CreateColumnOnTreeList(tlSO, "Updated Date", "UpdatedDate", 10);
        }
        private void InitColumtlItem()
        {
            CreateColumnOnTreeList(tlItem, "PK", "PK", -1);
            CreateColumnOnTreeList(tlItem, "UnitPK", "UnitPK", -1);
            CreateColumnOnTreeList(tlItem, "StockPK", "StockPK", -1);
            CreateColumnOnTreeList(tlItem, "Số đề nghị", "MaterialRequirementPK", 1);
            CreateColumnOnTreeList(tlItem, "Mã dự án", "ProjectCode", 1);
            CreateColumnOnTreeList(tlItem, "STT", "OrdinalNumber", 1);
            CreateColumnOnTreeList(tlItem, "Tên hạng mục", "ProjectItem", -1);
            CreateColumnOnTreeList(tlItem, "Tên cụm", "Component", -1);
            CreateColumnOnTreeList(tlItem, "Mã vật tư (F4)", "StockCode", 1);
            CreateColumnOnTreeList(tlItem, "Tên vật tư (F4)", "StockName", 2);
            CreateColumnOnTreeList(tlItem, "Đơn vị tính (F4)", "UnitName", 2);
            CreateColumnOnTreeList(tlItem, "Số lượng ", "Quantity",2);
            CreateColumnOnTreeList(tlItem, "Số lượng trong kho", "QuantityInStock", -2);
            CreateColumnOnTreeList(tlItem, "Chứng chỉ", "Certificate", 5);
            CreateColumnOnTreeList(tlItem, "Xuất xứ", "Origin", 5);
            CreateColumnOnTreeList(tlItem, "Hãng sản xuất", "Manufacturer", 5);
            CreateColumnOnTreeList(tlItem, "Ngày yêu cầu", "RequestedDate", 5);
            CreateColumnOnTreeList(tlItem, "Ngày yêu cầu nhận VT", "ReceiveRequestDate", 7);
            CreateColumnOnTreeList(tlItem, "Ghi chú", "Note", 11);
        }
        #endregion
    }
}
