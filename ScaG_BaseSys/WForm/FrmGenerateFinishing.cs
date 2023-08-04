using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using CNY_BaseSys.Common;
using System.Drawing.Drawing2D;
using System.IO;
using CNY_BaseSys.Info;
using CNY_BaseSys.Properties;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using GridFixedStyle = DevExpress.XtraGrid.Columns.FixedStyle;


using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition;
using DrawFocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle;
using FocusedColumnChangedEventArgs = DevExpress.XtraTreeList.FocusedColumnChangedEventArgs;
using ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility;
using ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
namespace CNY_BaseSys.WForm
{
    public partial class FrmGenerateFinishing : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
        public event OnGetBomFinishingFinalHandler OnGetBomInput = null;
        readonly Inf_Finishing _inf = new Inf_Finishing();
        private Int64 _cny00012PkCopy = 0;
        private Int64 _cny00050PkCopy = 0;
        private const int TotalStep = 3;
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        Dictionary<Int64, string> _dicTableFinishing = new Dictionary<Int64, string>(); //PKCHild_PKCode
        private DataTable _dtColorBoM = new DataTable();
        private readonly Int64 _cny00019Pk = 0;
        private Int64 _cny00051Pk = 0;
        private readonly RepositoryItemSpinEdit _repositorySpinN6;
        private bool _checkKeyDown;
        private readonly RepositoryItemTextEdit _repositoryTextUpper;
        private readonly RepositoryItemSpinEdit _repositorySpinN2;
        private Int64 _cny00050Pk = 0;
        #endregion

        #region "Contructor"
        public FrmGenerateFinishing(Int64 cny00019Pk, Int64 cny00051Pk, Int64 cny00050Pk)
        {
            InitializeComponent();


            this._cny00019Pk = cny00019Pk;

            this._cny00051Pk = cny00051Pk;
            this._cny00050Pk = cny00050Pk;





            _repositorySpinN2 = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = 0,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
                EditMask = "N2"
            };

            _repositorySpinN2.Buttons.Clear();


            _repositoryTextUpper = new RepositoryItemTextEdit
            {
                AutoHeight = false,
                CharacterCasing = CharacterCasing.Upper,
            };



            _repositorySpinN6 = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = 0,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
                EditMask = "N6"
            };
            _repositorySpinN6.Buttons.Clear();
            InitTreeList();


            txtSearchBOMNo.Properties.MaxLength = 10;
            txtSearchBOMNo.Properties.CharacterCasing = CharacterCasing.Upper;
            txtSearchBOMNo.Properties.Mask.MaskType = MaskType.RegEx;
            txtSearchBOMNo.Properties.Mask.EditMask = @"\d+";



            txtSearchProOrderNo.Properties.CharacterCasing = CharacterCasing.Upper;


            btnSearch.Click += btnSearch_Click;

            txtSearchBOMNo.KeyDown += txtSearchBOMNo_KeyDown;
            txtSearchBOMNo.MouseMove += txtSearchBOMNo_MouseMove;

            txtSearchProjectNo.KeyDown += txtSearchProjectNo_KeyDown;
            txtSearchProjectNo.MouseMove += txtSearchProjectNo_MouseMove;

            txtSearchProjectName.KeyDown += txtSearchProjectName_KeyDown;
            txtSearchProjectName.MouseMove += txtSearchProjectName_MouseMove;

            txtSearchProOrderNo.KeyDown += txtSearchProOrderNo_KeyDown;
            txtSearchProOrderNo.MouseMove += txtSearchProOrderNo_MouseMove;



            ProcessGeneral.SetTooltipControl(txtSearchBOMNo, "Search By BOM No.", "Quick Search", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);
            ProcessGeneral.SetTooltipControl(txtSearchProjectNo, "Search By Project No.", "Quick Search", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);
            ProcessGeneral.SetTooltipControl(txtSearchProjectName, "Search By Project Name", "Quick Search", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);
            ProcessGeneral.SetTooltipControl(txtSearchProOrderNo, "Search By Production Order No.", "Quick Search", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);

            GridViewMainCustomInit();


            //    DataTable dtSeaP, SearchSaleOrderMain filter, bool isSearchStr, bool emptyFilter

            btnCancel.Click += btnCancel_Click;
            btnNextFinish.Click += btnNextFinish_Click;
            btnBack.Click += btnBack_Click;



            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };






            GridViewFinishingCustomInit(gcFiSO, gvFiSO);




            txtOrderType__Des.Properties.ReadOnly = true;
            txtOrderNo.Properties.ReadOnly = true;

            txtCustomer_Name.Properties.ReadOnly = true;


            txtCurrency_Des.Properties.ReadOnly = true;

            txtProjectNo.Properties.ReadOnly = true;
            txtProjectName.Properties.ReadOnly = true;
            txtCustormerOrderNo.Properties.ReadOnly = true;
            dtpOrderDate.Properties.ReadOnly = true;
            dtpDeliveryDate.Properties.ReadOnly = true;

            txtSaleMan.Properties.ReadOnly = true;

            txtCustomerReference.Properties.ReadOnly = true;

            txtProOrderNo.Properties.ReadOnly = true;
            this.Load += Frm_Load;
            btnCheckAll.Click += BtnCheckAll_Click;

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

        private void CheckAllNodeTree(bool status)
        {
            var q1 = tlMain.GetAllNodeTreeList();
            foreach (TreeListNode node in q1)
            {
                node.Checked = status;
            }
        }
        private void Frm_Load(object sender, EventArgs e)
        {

            bool isHasData = LoadSaleOrderInfo();
            LoadDataGridMain(true);
            VisibleTabPageByStep(WizardButtonType.Load);
            SetupButtonNextFinished();
            btnCheckAll.Visible = false;
            if (!isHasData)
            {
                XtraMessageBox.Show("Error Data For Production Order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        private void SetInfoCheckButton(bool enable, Image image, string text)
        {
            btnCheckAll.Image = image;
            btnCheckAll.ToolTip = text;
            btnCheckAll.Enabled = enable;
        }
        private bool LoadSaleOrderInfo()
        {
            var dlg = new WaitDialogForm();
            DataTable dtHeader = _inf.BoMMapsSODataCopy(_cny00019Pk);
            dlg.Close();
            if (dtHeader.Rows.Count <= 0) return false;
            DataRow drHeader = dtHeader.Rows[0];




            txtOrderType__Des.EditValue = ProcessGeneral.GetSafeString(drHeader["OrderTypeDes"]);
            txtOrderNo.EditValue = ProcessGeneral.GetSafeString(drHeader["OrderNumber"]);

            txtCustomer_Name.EditValue = ProcessGeneral.GetSafeString(drHeader["CustDes"]);


            txtCurrency_Des.EditValue = ProcessGeneral.GetSafeString(drHeader["CurencyDes"]);

            txtProjectNo.EditValue = ProcessGeneral.GetSafeString(drHeader["ProjectNo"]);
            txtProjectName.EditValue = ProcessGeneral.GetSafeString(drHeader["ProjectName"]);
            txtCustormerOrderNo.EditValue = ProcessGeneral.GetSafeString(drHeader["CustOrderNumber"]);
            dtpOrderDate.EditValue = ProcessGeneral.GetSafeString(drHeader["OrderDate"]);
            dtpDeliveryDate.EditValue = ProcessGeneral.GetSafeString(drHeader["DeliveryDate"]);
            txtSaleMan.EditValue = ProcessGeneral.GetSafeString(drHeader["SaleMenDes"]);

            txtCustomerReference.EditValue = ProcessGeneral.GetSafeString(drHeader["CustRef"]);

            txtProOrderNo.EditValue = ProcessGeneral.GetSafeString(drHeader["ProOrderNo"]);


            return true;
        }


        #endregion

        #region "methold"


        private void GridViewMainCustomInit()
        {
            gcMain.UseEmbeddedNavigator = true;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            //   gvSO.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = true;
            gvMain.OptionsCustomization.AllowFilter = true;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsCustomization.AllowColumnResizing = true;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            // gvSO.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;

            gvMain.OptionsSelection.MultiSelect = false;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;

            gvMain.OptionsView.ShowFooter = false;


            //   gvSO.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = true;
            //gvSO.OptionsFind.AlwaysVisible = true;//==>false==>gvSO.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
            };



            GridColumn[] arrGridCol = new GridColumn[21];
            int ind = -1;

            #region "Init Column"


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Status",
                FieldName = "CNY014_Status",
                Name = "CNY014_Status",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Fin. Process No.",
                FieldName = "CNY015_BOMNo",
                Name = "CNY015_BOMNo",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Version",
                FieldName = "CNY004_Version",
                Name = "CNY004_Version",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Customer",
                FieldName = "Customer",
                Name = "Customer",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Prod. Order No.",
                FieldName = "ProOrderNo",
                Name = "ProOrderNo",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };






            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Project No.",
                FieldName = "ProjectNo",
                Name = "ProjectNo",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Project Name.",
                FieldName = "ProjectName",
                Name = "ProjectName",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };





            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Remark",
                FieldName = "CNY005_Remark",
                Name = "CNY005_Remark",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };









            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Created Date",
                FieldName = "CreatedDate",
                Name = "CreatedDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Created By",
                FieldName = "CreatedBy",
                Name = "CreatedBy",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Adjusted Date",
                FieldName = "AdjustedDate",
                Name = "AdjustedDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Adjusted By",
                FieldName = "AdjustedBy",
                Name = "AdjustedBy",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Released Date",
                FieldName = "ReleasedDate",
                Name = "ReleasedDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Released By",
                FieldName = "ReleasedBy",
                Name = "ReleasedBy",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };



            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Approved Date",
                FieldName = "ApprovedDate",
                Name = "ApprovedDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Approved By",
                FieldName = "ApprovedBy",
                Name = "ApprovedBy",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Order No.",
                FieldName = "OrderNumber",
                Name = "OrderNumber",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Cust. Order No.",
                FieldName = "CustOrderNumner",
                Name = "CustOrderNumner",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };



            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"BOM Code",
                FieldName = "CNY001_BOMCode",
                Name = "CNY001_BOMCode",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };




            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"CNY00019PK",
                FieldName = "CNY00019PK",
                Name = "CNY00019PK",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };





            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"BOMHeaderPK",
                FieldName = "BOMHeaderPK",
                Name = "BOMHeaderPK",
                Visible = true,
                VisibleIndex = ind,
                Fixed = GridFixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            #endregion

            gvMain.Columns.AddRange(arrGridCol);
            //     
            ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "CNY00019PK", "BOMHeaderPK", "OrderNumber", "CustOrderNumner", "CNY001_BOMCode");


            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;


            gvMain.RowStyle += gvMain_RowStyle;

            gcMain.ForceInitialize();

            //gcSO.UseEmbeddedNavigator = true;


            //   gvSO.RowHeight = 25;



        }



        public static DataTable TableTempProductionBoMCopy()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("CustomerOrderNo", typeof(string));
            dt.Columns.Add("ProjectNo", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("ProductionOrder", typeof(string));
            dt.Columns.Add("DeliveryDate", typeof(string));
            dt.Columns.Add("CNY00012PK", typeof(Int64));
            return dt;
        }
        private void LoadDataGridMain(bool isLoad)
        {
            var dlg = new WaitDialogForm();
            Int32 bomNo = 0;
            if (!string.IsNullOrEmpty(txtSearchBOMNo.Text.Trim()))
            {
                bomNo = ProcessGeneral.GetSafeInt(txtSearchBOMNo.Text.Trim());
            }


            DataTable dtS = _inf.BoM_LoadListBomCopyWizard(_cny00019Pk, txtSearchProOrderNo.Text.Trim(), txtSearchProjectNo.Text.Trim(),
                txtSearchProjectName.Text.Trim(), bomNo, isLoad);


            btnNextFinish.Enabled = dtS.Rows.Count > 0;


            gcMain.DataSource = dtS;
            gvMain.BestFitColumns();

            dlg.Close();
        }





        private void VisibleTabPageByStep(WizardButtonType btnType)
        {
            


            int step;
            bool status;
            if (btnType == WizardButtonType.Load)
            {
                step = 0;
                status = false;
            }
            else
            {
                int currentStep = ProcessGeneral.GetSafeInt(xtraTabMain.SelectedTabPage.Tag);
                if (btnType == WizardButtonType.Back)
                {
                    step = currentStep - 1;
                    status = false;
                }
                else if (btnType == WizardButtonType.Next)
                {
                    step = currentStep + 1;
                    status = true;
                }
                else
                {
                    step = currentStep;
                    status = true;
                }

            }

            btnCheckAll.Visible = status;
            foreach (XtraTabPage page in xtraTabMain.TabPages)
            {
                Int32 tag = ProcessGeneral.GetSafeInt(page.Tag);
                page.PageVisible = tag == step;
            }
        }

        private void SetupButtonNextFinished()
        {
            int step = ProcessGeneral.GetSafeInt(xtraTabMain.SelectedTabPage.Tag);
            btnBack.Enabled = step != 0;

            if (step == TotalStep - 1)
            {
                btnNextFinish.Text = @"Finish";
                btnNextFinish.Image = Resources.apply_24x24_W;
                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+F)";



            }
            else
            {
                btnNextFinish.Text = @"Next";
                btnNextFinish.Image = Resources.forward_24x24_W;
                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+N)";
            }
        }







        #endregion



        #region "textbox, combobox event"


        private void txtSearchProjectName_MouseMove(object sender, MouseEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(tE),
                ToolTipController.DefaultController.GetTitle(tE), tE.PointToScreen(e.Location));
        }

        private void txtSearchProjectName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataGridMain(false);
            }
        }

        private void txtSearchProjectNo_MouseMove(object sender, MouseEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(tE),
                ToolTipController.DefaultController.GetTitle(tE), tE.PointToScreen(e.Location));
        }

        private void txtSearchProjectNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataGridMain(false);
            }
        }

        private void txtSearchProOrderNo_MouseMove(object sender, MouseEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(tE),
                ToolTipController.DefaultController.GetTitle(tE), tE.PointToScreen(e.Location));
        }

        private void txtSearchProOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataGridMain(false);
            }
        }

        private void txtSearchBOMNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataGridMain(false);
            }
        }

        private void txtSearchBOMNo_MouseMove(object sender, MouseEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(tE),
                ToolTipController.DefaultController.GetTitle(tE), tE.PointToScreen(e.Location));
        }
        #endregion

        #region "gridview event"

        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
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
            bool selected = gv.IsRowSelected(e.RowHandle);
            if (selected)
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
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
        }



        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {

            var gvP = sender as GridView;
            if (gvP == null) return;
            if (gvP.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gvP.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = GridCellColor.BackColorSelectedRow;
                e.Appearance.BackColor2 = GridCellColor.BackColor2ShowEditor;
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;

            }

        }











        #endregion




        #region "button click event "




        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataGridMain(false);

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!btnBack.Enabled) return;
            VisibleTabPageByStep(WizardButtonType.Back);
            SetupButtonNextFinished();


        }
        private void btnNextFinish_Click(object sender, EventArgs e)
        {
            if (!btnNextFinish.Enabled) return;

            txtFocus.SelectNextControl(ActiveControl, true, true, true, true);

            string text = btnNextFinish.Text.Trim().ToUpper();

            WizardButtonType btnType = text == "NEXT" ? WizardButtonType.Next : WizardButtonType.Finish;


            VisibleTabPageByStep(btnType);
            SetupButtonNextFinished();


            int step = ProcessGeneral.GetSafeInt(xtraTabMain.SelectedTabPage.Tag);
            if (btnType == WizardButtonType.Next)
            {
                if (step == 1)
                {
                    WaitDialogForm dlg = new WaitDialogForm();
                    _cny00012PkCopy = ProcessGeneral.GetSafeInt64(gvMain.GetRowCellValue(gvMain.FocusedRowHandle, "BOMHeaderPK"));
                    _dtColorBoM = _inf.LoadListFinishingColorByBomPkCopy(_cny00012PkCopy);
                    SetupButtonNextFinished();




                    LoadDataGridViewFinishing(gcFiSO, gvFiSO, _dtColorBoM);
                    ProcessGeneral.SetFocusedCellOnGrid(gvFiSO, 0, 0);
                    dlg.Close();

                    if (_dtColorBoM.Rows.Count <= 0)
                    {
                        VisibleTabPageByStep(WizardButtonType.Back);
                        SetupButtonNextFinished();
                        XtraMessageBox.Show("No Data Display",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (step == 2)
                {
                    WaitDialogForm dlg = new WaitDialogForm();
                    _cny00050PkCopy = ProcessGeneral.GetSafeInt64(gvFiSO.GetRowCellValue(gvFiSO.FocusedRowHandle, "CNY00050PK"));
               
                    SetupButtonNextFinished();


                    DataTable dt5253 = _inf.BoM_LoadListBomFinishingGenerateInfo(_cny00012PkCopy, _cny00050PkCopy, _cny00051Pk);

                    DataTable dtTemp = Ctrl_FinishingGenneral.TableTreeviewFinishingTemplate();

                    var q1 = dt5253.AsEnumerable().Select(p => new
                    {
                        CNYMF016PK = p.Field<Int64>("CNYMF016PK"),
                        StepWork = p.Field<string>("StepWork"),
                        PKCode = p.Field<Int64>("PKCode"),
                        RMCode_001 = p.Field<string>("RMCode_001"),
                        RMDescription_002 = p.Field<string>("RMDescription_002"),
                        Unit = p.Field<string>("Unit"),
                        RMGroup_066 = p.Field<string>("RMGroup_066"),
                        QualityCode = p.Field<string>("QualityCode"),
                        RateGram = p.Field<decimal>("RateGram"),
                        RatePercent = p.Field<decimal>("RatePercent"),
                        UC = p.Field<decimal>("UC"),
                        Tolerance = p.Field<decimal>("Tolerance"),
                        Note = p.Field<string>("Note"),
                        RowState = p.Field<string>("RowState"),
                        ItemLevel = p.Field<int>("ItemLevel"),
                        SortOrderNode = p.Field<int>("SortOrderNode"),
                        ChildPK = p.Field<Int64>("ChildPK"),
                        ParentPK = p.Field<Int64>("ParentPK"),
                        Supplier = p.Field<string>("Supplier"),
                        SupplierPK = p.Field<Int64>("SupplierPK"),
                        SupplierRef = p.Field<string>("SupplierRef"),
                        TDG00004PK = p.Field<Int64>("TDG00004PK"),
                        AllowUpdate = p.Field<bool>("AllowUpdate"),
                        TableCode = p.Field<string>("TableCode"),
                        PurchaseType = p.Field<string>("PurchaseType"),


                    }).OrderBy(p => p.ItemLevel).ThenBy(p => p.SortOrderNode).ToList();
                    if (q1.Any())
                    {
                        dtTemp = q1.CopyToDataTableNew();
                    }

                    LoadDataTreeView(dtTemp);

                    dlg.Close();
                }

            }
            else
            {

                
                OnGetBomInput?.Invoke(this, new OnGetBomFinishingFinalEventArgs
                {
                    TreeReturn = tlMain
                });
                this.Close();

            }


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



        #region "Tree Map SO "












        #region "Load Data"










        private void CreateDictionaryFinishingWhenEditAndCopy(DataTable dtFinishing, bool isClear = true)
        {
            if (isClear)
            {
                _dicTableFinishing.Clear();
            }


            _dicTableFinishing = dtFinishing.AsEnumerable().GroupBy(f => new
            {
                CNY00020PK = f.Field<Int64>("CNY00020PK"),
            }).Where(myGroup => myGroup.Any()).Select(myGroup => new
            {
                KeyDic = myGroup.Key.CNY00020PK,
                TempData = string.Join("\r\n", myGroup.Select(f => GetStringFinishingInfo(f.Field<String>("PartNoDesc"), f.Field<String>("FinishingColor")).Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray())

            }).ToDictionary(item => item.KeyDic, item => item.TempData);

        }


        private string GetStringFinishingInfo(string partNo, string finishingColor)
        {

            if (!string.IsNullOrEmpty(partNo) && !string.IsNullOrEmpty(finishingColor))
            {
                return string.Format("{0} : {1}", partNo, finishingColor);
            }
            if (string.IsNullOrEmpty(finishingColor) && string.IsNullOrEmpty(partNo))
            {
                return "";
            }
            if (!string.IsNullOrEmpty(finishingColor))
                return finishingColor;
            return partNo;
        }


        private DataTable StandardTreeTableWhenEditCopy(DataTable dtTreeTemp)
        {


            foreach (DataRow drSource in dtTreeTemp.Rows)
            {
                Int64 childPk = ProcessGeneral.GetSafeInt64(drSource["ChildPK"]);



                string strFinishing;
                if (_dicTableFinishing.TryGetValue(childPk, out strFinishing))
                {
                    drSource["FinishingColor"] = strFinishing;
                }

                drSource["Selected"] = false;

            }
            dtTreeTemp.AcceptChanges();
            return dtTreeTemp;
        }


        #endregion










        #endregion


        #region "Process Grid Finishing"




        private void GridViewFinishingCustomInit(GridControl gcFi, GridView gvFi)
        {


            // gcFi
            //   gvFi

            gcFi.UseEmbeddedNavigator = true;

            gcFi.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcFi.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcFi.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcFi.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcFi.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvFi.OptionsBehavior.Editable = true;
            gvFi.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvFi.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvFi.OptionsCustomization.AllowColumnMoving = false;
            gvFi.OptionsCustomization.AllowQuickHideColumns = true;

            gvFi.OptionsCustomization.AllowSort = false;

            gvFi.OptionsCustomization.AllowFilter = false;


            gvFi.OptionsView.ShowGroupPanel = false;
            gvFi.OptionsView.ShowIndicator = true;
            gvFi.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvFi.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvFi.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvFi.OptionsView.ShowAutoFilterRow = false;
            gvFi.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvFi.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvFi.OptionsNavigation.AutoFocusNewRow = true;
            gvFi.OptionsNavigation.UseTabKey = true;

            gvFi.OptionsSelection.MultiSelect = false;
            gvFi.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvFi.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvFi.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvFi.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvFi.OptionsView.EnableAppearanceEvenRow = false;
            gvFi.OptionsView.EnableAppearanceOddRow = false;
            gvFi.OptionsView.ShowFooter = false;
            gvFi.OptionsView.RowAutoHeight = true;
            gvFi.OptionsHint.ShowFooterHints = false;
            gvFi.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gvFi.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvFi.OptionsFind.AllowFindPanel = false;


            gvFi.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvFi)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };






            gvFi.ShowingEditor += gvFi_ShowingEditor;

            gvFi.CustomRowCellEdit += gvFi_CustomRowCellEdit;
      
  
          

            gvFi.RowCountChanged += gvFi_RowCountChanged;
            gvFi.CustomDrawRowIndicator += gvFi_CustomDrawRowIndicator;

            
            gvFi.RowStyle += gvFi_RowStyle;

            gcFi.ForceInitialize();



        }





        #region "GridView Event"
        private void gvFi_RowStyle(object sender, RowStyleEventArgs e)
        {

            var gvP = sender as GridView;
            if (gvP == null) return;
            if (gvP.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gvP.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = GridCellColor.BackColorSelectedRow;
                e.Appearance.BackColor2 = GridCellColor.BackColor2ShowEditor;
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;

            }

        }

        private void gvFi_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            string fieldName = e.Column.FieldName;
            switch (fieldName)
            {
                case "FinishingColor":
                    e.RepositoryItem = _repositoryTextGrid;
                    break;

            }

        }


        private void gvFi_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }


  
     
       









        private void gvFi_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvFi_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

            bool selected = gv.IsRowSelected(e.RowHandle);



            if (selected)
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }
            else
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.Silver, Color.Azure, 90);
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





       
        #endregion



        private void LoadDataGridViewFinishing(GridControl gcFi, GridView gvFi, DataTable dt)
        {



            gvFi.BeginUpdate();



            gcFi.DataSource = null;
            gvFi.Columns.Clear();

            gcFi.DataSource = dt;
            ProcessGeneral.HideVisibleColumnsGridView(gvFi, false, "CNY00050PK");


            ProcessGeneral.SetGridColumnHeader(gvFi.Columns["ColorCode"], "Code", DefaultBoolean.False, HorzAlignment.Near,
                DevExpress.XtraGrid.Columns.FixedStyle.None);
            gvFi.Columns["ColorCode"].ImageIndex = 0;
            gvFi.Columns["ColorCode"].ImageAlignment = StringAlignment.Near;
            ProcessGeneral.SetGridColumnHeader(gvFi.Columns["FinishingColor"], "Description", DefaultBoolean.False, HorzAlignment.Near, DevExpress.XtraGrid.Columns.FixedStyle.None);


            BestFitGridFinishing(gvFi);


            gvFi.EndUpdate();
        }

        private void BestFitGridFinishing(GridView gvFi)
        {
            gvFi.BestFitColumns();

            gvFi.Columns["ColorCode"].Width += 10;
            if (gvFi.Columns["FinishingColor"].Width > 700)
            {
                gvFi.Columns["FinishingColor"].Width = 700;
            }
        }


        #endregion




        #region "Tree Final"

        #region "Property"

       

      



     


        #endregion


      




        #region "Proccess Treeview"

     

        private void LoadDataTreeView(DataTable dt)
        {

            tlMain.Columns.Clear();
            tlMain.DataSource = null;

            tlMain.DataSource = dt;
            tlMain.ParentFieldName = "ParentPK";
            tlMain.KeyFieldName = "ChildPK";

            tlMain.Tag = dt.Rows.Count;
            tlMain.BeginUpdate();


            Ctrl_FinishingGenneral.VisibleTreeColumnSortPaint(tlMain);


            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["QualityCode"], "Quality", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RowState"], "RowState", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["PKCode"], "PKCode", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["ItemLevel"], "ItemLevel", false, HorzAlignment.Center, TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["CNYMF016PK"], "CNYMF016PK", false,
                HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["SortOrderNode"], "SortOrderNode", true, HorzAlignment.Center,
                TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["StepWork"], "Route Group (F4)", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            tlMain.Columns["StepWork"].ImageIndex = 0;
            tlMain.Columns["StepWork"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RMCode_001"], "Item Code (F4)", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            tlMain.Columns["RMCode_001"].ImageIndex = 0;
            tlMain.Columns["RMCode_001"].ImageAlignment = StringAlignment.Near;




            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RMDescription_002"], "Item Name", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["Unit"], "Unit (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tlMain.Columns["Unit"].ImageIndex = 0;
            tlMain.Columns["Unit"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RMGroup_066"], "RM Group", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");





            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RateGram"], "Rate (Gr)", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tlMain.Columns["RateGram"].Format.FormatType = FormatType.Numeric;
            tlMain.Columns["RateGram"].Format.FormatString = "#,0.##";

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["RatePercent"], "Rate (%)", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tlMain.Columns["RatePercent"].Format.FormatType = FormatType.Numeric;
            tlMain.Columns["RatePercent"].Format.FormatString = "#,0.##";

            //  tl.Columns["RatePercent"].Format.FormatString = "C2";

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["UC"], "UC", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tlMain.Columns["UC"].Format.FormatType = FormatType.Numeric;
            tlMain.Columns["UC"].Format.FormatString = "#,0.######";


            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["Tolerance"], "Waste (%)", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            tlMain.Columns["Tolerance"].Format.FormatType = FormatType.Numeric;
            tlMain.Columns["Tolerance"].Format.FormatString = "#,0.###";

            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["Note"], "Note", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");



            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["Supplier"], "Supplier (F4)", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            tlMain.Columns["Supplier"].ImageIndex = 0;
            tlMain.Columns["Supplier"].ImageAlignment = StringAlignment.Near;




            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["SupplierRef"], "Supplier Ref (F4)", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            tlMain.Columns["SupplierRef"].ImageIndex = 0;
            tlMain.Columns["SupplierRef"].ImageAlignment = StringAlignment.Near;



            ProcessGeneral.SetTreeListColumnHeader(tlMain.Columns["PurchaseType"], "Vendor Mode (F4)", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            tlMain.Columns["PurchaseType"].ImageIndex = 0;
            tlMain.Columns["PurchaseType"].ImageAlignment = StringAlignment.Near;



            tlMain.ExpandAll();
            tlMain.BestFitColumns();
            tlMain.Columns["StepWork"].Width += 60;
            if (tlMain.Columns["UC"].Width < 70)
            {
                tlMain.Columns["UC"].Width = 70;
            }


            tlMain.ForceInitialize();

            tlMain.BeginSort();
            tlMain.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tlMain.EndSort();
            tlMain.EndUpdate();
        }




        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.Assembly_BoM_16x16);
            imgLt.Images.Add(Resources.RawMaterial_BoM_16x16);
            imgLt.Images.Add(Resources.NoItem_BoM_16x16);
            return imgLt;
        }


        private void InitTreeList()
        {
            //tlMain.AllowDrop = false;
            //tlMain.OptionsDragAndDrop.DragNodesMode = DragNodesMode.None;

            //tlMain.OptionsDragAndDrop.AcceptOuterNodes = false;

            //tlMain.OptionsDragAndDrop.CanCloneNodesOnDrop = true;

            tlMain.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            tlMain.OptionsBehavior.EnableFiltering = true;
            tlMain.OptionsFilter.AllowFilterEditor = true;
            tlMain.OptionsFilter.AllowMRUFilterList = true;
            tlMain.OptionsFilter.AllowColumnMRUFilterList = true;
            tlMain.OptionsFilter.FilterMode = FilterMode.Smart;
            tlMain.OptionsFind.AllowFindPanel = true;
            tlMain.OptionsFind.AlwaysVisible = false;
            tlMain.OptionsFind.ShowCloseButton = true;
            tlMain.OptionsFind.HighlightFindResults = true;
            tlMain.OptionsView.ShowAutoFilterRow = false;

            tlMain.OptionsBehavior.Editable = true;
            tlMain.OptionsView.ShowColumns = true;
            tlMain.OptionsView.ShowHorzLines = true;
            tlMain.OptionsView.ShowVertLines = true;
            tlMain.OptionsView.ShowIndicator = true;
            tlMain.OptionsView.AutoWidth = false;
            tlMain.OptionsView.EnableAppearanceEvenRow = false;
            tlMain.OptionsView.EnableAppearanceOddRow = false;
            tlMain.StateImageList = GetImageListDisplayTreeView();
            tlMain.OptionsBehavior.AutoChangeParent = false;
            tlMain.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            tlMain.OptionsBehavior.AutoNodeHeight = true;

            tlMain.OptionsView.ShowSummaryFooter = false;

            tlMain.OptionsBehavior.CloseEditorOnLostFocus = true;
            tlMain.OptionsBehavior.KeepSelectedOnClick = true;
            tlMain.OptionsBehavior.ShowEditorOnMouseUp = true;
            tlMain.OptionsBehavior.SmartMouseHover = false;
            tlMain.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;






            tlMain.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
             new TreeListMultiCellSelector(tlMain, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };


            tlMain.OptionsBehavior.AllowRecursiveNodeChecking = true;
            tlMain.OptionsView.ShowCheckBoxes = true;



            tlMain.GetStateImage += TreeList_GetStateImage;
            tlMain.ShowingEditor += TreeList_ShowingEditor;



            tlMain.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;





            tlMain.NodeCellStyle += TreeList_NodeCellStyle;
           

            tlMain.KeyDown += TreeList_KeyDown;
            tlMain.EditorKeyDown += TreeList_EditorKeyDown;
            tlMain.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;





            tlMain.GetNodeDisplayValue += TreeList_GetNodeDisplayValue;

            
            tlMain.AfterCheckNode += TreeList_AfterCheckNode;
            tlMain.BeforeCheckNode += TreeList_BeforeCheckNode;

            LoadDataTreeView(Ctrl_FinishingGenneral.TableTreeviewFinishingTemplate());




        }
        private void TreeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            TreeList tl = (TreeList)sender;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            e.CanCheck = node.ParentNode == null;
           




        }
        private void TreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            TreeList tl = (TreeList)sender;
            if (tl == null) return;
            SetCheckParentNode(e.Node);


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

        private void SetCheckParentNode(TreeListNode node)
        {
            if (node == null) return;
            TreeListNode parentNode = node.ParentNode;
            if (node.ParentNode == null) return;
            if (parentNode.Checked) return;
     

            if (parentNode.Nodes.Any(p => p.Checked))
            {
                parentNode.Checked = true;
            }


        
           

        }



        private void TreeList_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            if (e.Column == null) return;
            string fieldName = e.Column.FieldName;
            if (node.ParentNode == null)
            {
                switch (fieldName)
                {

                    case "RMCode_001":
                    case "RMDescription_002":
                    case "Unit":
                    case "RMGroup_066":
                    case "UC":
                    case "PurchaseType":
                        e.Value = "";
                        break;
                }
            }
            else
            {
                switch (fieldName)
                {




                    case "StepWork":
                        e.Value = "";
                        break;
                }
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
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            _checkKeyDown = true;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
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

            







        }






     
     




    
        #endregion






        


        private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = (TreeList)sender;
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
            e.Cancel = true;




        }


        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;

            e.NodeImageIndex = node.ParentNode == null ? 0 : 1;

            //if (e.Node.HasChildren)//|| e.Node.ParentNode == null
            //{
            //    e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
            //}
            //else
            //{
            //    e.NodeImageIndex = 2;
            //}
        }


        private void TreeList_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            string fieldName = e.Column.FieldName;

            switch (fieldName)
            {

                case "UC":
                    e.RepositoryItem = _repositorySpinN6;
                    break;
                case "RateGram":
                    e.RepositoryItem = _repositorySpinN2;
                    break;
                case "RMCode_001":
                    e.RepositoryItem = _repositoryTextUpper;
                    break;
                case "QualityCode":
                    {
                        e.RepositoryItem = _repositoryTextUpper;

                    }
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
            bool noParent = node.ParentNode == null;
            if (noParent)
            {
                e.Appearance.Font = new Font("Tahoma", 9F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkRed;
            }
            bool isCheck = node.CheckState == CheckState.Checked;



            if (noParent)
            {
                switch (fieldName)
                {


                    case "StepWork":
                    case "RatePercent":
                    case "RateGram":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
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
                            else
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                            }

                        }
                        break;

                }
            }
            else
            {
                switch (fieldName)
                {
                    case "RMCode_001":
                    case "RMDescription_002":
                    case "RatePercent":
                    case "RateGram":
                    case "UC":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
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
                            else
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                            }
                        }
                        break;

                }
            }



        }




        #endregion


        




        #endregion


    }
}