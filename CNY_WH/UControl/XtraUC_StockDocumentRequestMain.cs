using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Info;
using CNY_WH.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using BorderSide = DevExpress.XtraPrinting.BorderSide;

namespace CNY_WH
{
    public partial class XtraUC_StockDocumentRequestMain : XtraUserControl
    {


        #region "Property"
        readonly Inf_StockDocumentRequest _inf = new Inf_StockDocumentRequest();
        WaitDialogForm _dlg;
        private string _strFilter = "";
        public string StrFilter
        {
            get
            {
                return _strFilter;
            }
            set
            {
                _strFilter = value;
            }
        }
        DataTable dt;
        public TextEdit TxtSearchp
        {
            get
            {
                return this.txtSearch;
            }

        }

        public GridView gvMainP
        {
            get
            {
                return this.gvMain;
            }
        }

        private readonly string [] _arrPrType;
        #endregion

        #region "Contructor"
        public XtraUC_StockDocumentRequestMain(params string[] arrPrType)
        {
            InitializeComponent();
            this._arrPrType = arrPrType;
            //txtSearch.Properties.Mask.MaskType = MaskType.RegEx;
            //txtSearch.Properties.Mask.EditMask = @"\d+";
            this.Load += XtraUCMain_Load;

            txtSearch.KeyDown += txtSearch_KeyDown;
            txtSearch.MouseMove += txtSearch_MouseMove;

            //btnSearch.Click += btnSearch_Click;
            //btnExportExcel.Click += btnExportExcel_Click;
            ProcessGeneral.SetTooltipControl(txtSearch, "Search By SDR No.", "Quick Search", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);
            GridViewCustomInit();
            dt = GetData();

            // ddbStatus.Visible = true;

            txtSearch.ButtonClick += TxtSearch_ButtonClick;

            btnSendEmail.Click += BtnSendEmail_Click;
            btnSendEmail.Visible = false;
        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
           // string strmailFrom = "vuongnq@tranducfurnishings.com";
            var frmSend = new FrmSendMailMultiAttach();
             // frmSend.MailServer = "mail.tranducfurnishings.com";
            //  frmSend.DomainName = "scavibh.local";
            frmSend.UserName = DeclareSystem.SysUserName;
            // frmSend.Port = DeclareSystem.SysMailPort;
            //frmSend.EmailLogin = strmailFrom;
            //frmSend.MailFrom = strmailFrom;
            //frmSend.PassLogin = "Vuong123456";
            frmSend.MailTo = "minhthuan.nguyen@scavi.com.vn,nguyenminhthuanit@gmail.com,QuocNam.Tran@scavi.com.vn";
            frmSend.Subject = "Test";
            frmSend.Content = "Test";
            frmSend.UseContentHTML = true;
            frmSend.EnableTextEditMailServer = false;
            frmSend.EnableTextEditDomainName = false;
            frmSend.EnableTextEditPort = false;
            frmSend.EnableTextEditUserName = false;
            //    frmSend.dtAttacch = ProcessGeneral.GetTableAttachFile("D:\\test1.txt", "D:\\test2.txt");
            frmSend.sendMailarg += (s1, e1) => { };
            frmSend.ShowDialog();
        }













        #endregion

        #region "methold"

        /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
        private void GridViewCustomInit()
        {


            // gcMain.ToolTipController = toolTipController1  ;



            gvMain.OptionsCustomization.AllowColumnResizing = true;
            gvMain.OptionsCustomization.AllowGroup = true;
            gvMain.OptionsCustomization.AllowColumnMoving = true;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = true;
            gvMain.OptionsCustomization.AllowFilter = true;







            gcMain.UseEmbeddedNavigator = true;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;

            //     gvMain.OptionsHint.ShowCellHints = true;
            gvMain.OptionsView.BestFitMaxRowCount = 1000;
            gvMain.OptionsView.ShowGroupPanel = true;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = true;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Auto;
            gvMain.OptionsView.ColumnAutoWidth = false;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.OptionsSelection.MultiSelect = false;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;

            gvMain.OptionsView.ShowFooter = false;

            gvMain.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMain)
            {
                AllowGroupBy = true,
                IsPerFormEvent = true,
            };

            gvMain.OptionsPrint.AutoWidth = false;
            gvMain.OptionsPrint.ShowPrintExportProgress = true;
            gvMain.OptionsPrint.AllowMultilineHeaders = true;
            gvMain.OptionsPrint.ExpandAllDetails = true;
            gvMain.OptionsPrint.ExpandAllGroups = true;
            gvMain.OptionsPrint.PrintDetails = true;
            gvMain.OptionsPrint.PrintFooter = true;
            gvMain.OptionsPrint.PrintGroupFooter = true;
            gvMain.OptionsPrint.PrintHeader = true;
            gvMain.OptionsPrint.PrintHorzLines = true;
            gvMain.OptionsPrint.PrintVertLines = true;
            gvMain.OptionsPrint.SplitCellPreviewAcrossPages = true;
            gvMain.OptionsPrint.SplitDataCellAcrossPages = true;
            gvMain.OptionsPrint.UsePrintStyles = false;
            gvMain.OptionsPrint.AllowCancelPrintExport = true;
            gvMain.OptionsPrint.AutoResetPrintDocument = true;


            
            //GridColumn[] arrGridCol = new GridColumn[28];

            //#region "Init Column"
            //arrGridCol[0] = new GridColumn
            //{
            //    Caption = @"PR Type",
            //    FieldName = "PRType",
            //    Name = "PRType",
            //    Visible = true,
            //    VisibleIndex = 0,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[1] = new GridColumn
            //{
                
            //    Caption = @"PR No.",
            //    FieldName = "PRNo",
            //    Name = "PRNo",
            //    Visible = true,
            //    VisibleIndex = 1,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true,},
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //  SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            //};
            //arrGridCol[2] = new GridColumn
            //{
            //    Caption = @"Version",
            //    FieldName = "Version",
            //    Name = "Version",
            //    Visible = true,
            //    VisibleIndex = 2,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};



            //arrGridCol[3] = new GridColumn
            //{
            //    Caption = @"Project No.",
            //    FieldName = "ProjectNo",
            //    Name = "ProjectNo",
            //    Visible = true,
            //    VisibleIndex = 3,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = visibleSo, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[4] = new GridColumn
            //{
            //    Caption = @"Project Name",
            //    FieldName = "ProjectName",
            //    Name = "ProjectName",
            //    Visible = true,
            //    VisibleIndex = 4,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = visibleSo, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[5] = new GridColumn
            //{
            //    Caption = @"Production Order",
            //    FieldName = "ProductionOrder",
            //    Name = "ProductionOrder",
            //    Visible = true,
            //    VisibleIndex = 5,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = visibleSo, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[6] = new GridColumn
            //{
            //    Caption = @"Cust. Order No.",
            //    FieldName = "CustomerOrderNo",
            //    Name = "CustomerOrderNo",
            //    Visible = true,
            //    VisibleIndex = 6,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = visibleSo, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

           
            //arrGridCol[7] = new GridColumn
            //{
            //    Caption = @"Customer",
            //    FieldName = "SearchName",
            //    Name = "SearchName",
            //    Visible = true,
            //    VisibleIndex = 7,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = visibleSo, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[8] = new GridColumn
            //{
            //    Caption = @"Customer",
            //    FieldName = "Customer",
            //    Name = "Customer",
            //    Visible = true,
            //    VisibleIndex = 8,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[9] = new GridColumn
            //{
            //    Caption = @"ETD",
            //    FieldName = "ETD",
            //    Name = "ETD",
            //    Visible = true,
            //    VisibleIndex = 9,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[10] = new GridColumn
            //{
            //    Caption = @"ETA",
            //    FieldName = "ETA",
            //    Name = "ETA",
            //    Visible = true,
            //    VisibleIndex = 10,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[11] = new GridColumn
            //{
            //    Caption = @"Status",
            //    FieldName = "Status",
            //    Name = "Status",
            //    Visible = true,
            //    VisibleIndex = 11,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[12] = new GridColumn
            //{
            //    Caption = @"Sender",
            //    FieldName = "Sender",
            //    Name = "Sender",
            //    Visible = true,
            //    VisibleIndex = 12,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[13] = new GridColumn
            //{
            //    Caption = @"Recipient",
            //    FieldName = "Recipient",
            //    Name = "Recipient",
            //    Visible = true,
            //    VisibleIndex = 13,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};


            //arrGridCol[14] = new GridColumn
            //{
            //    Caption = @"Ref Docs.",
            //    FieldName = "ReferenceDocs",
            //    Name = "ReferenceDocs",
            //    Visible = true,
            //    VisibleIndex = 14,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[15] = new GridColumn
            //{
            //    Caption = @"Note",
            //    FieldName = "Note",
            //    Name = "Note",
            //    Visible = true,
            //    VisibleIndex = 15,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};




          
          
         
           
            //arrGridCol[16] = new GridColumn
            //{
            //    Caption = @"Created Date",
            //    FieldName = "CreatedDate",
            //    Name = "CreatedDate",
            //    Visible = true,
            //    VisibleIndex = 16,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[17] = new GridColumn
            //{
            //    Caption = @"Created By",
            //    FieldName = "CreatedBy",
            //    Name = "CreatedBy",
            //    Visible = true,
            //    VisibleIndex = 17,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[18] = new GridColumn
            //{
            //    Caption = @"Adjusted Date",
            //    FieldName = "AdjustedDate",
            //    Name = "AdjustedDate",
            //    Visible = true,
            //    VisibleIndex = 18,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[19] = new GridColumn
            //{
            //    Caption = @"Adjusted By",
            //    FieldName = "AdjustedBy",
            //    Name = "AdjustedBy",
            //    Visible = true,
            //    VisibleIndex = 19,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[20] = new GridColumn
            //{
            //    Caption = @"Released Date",
            //    FieldName = "ReleasedDate",
            //    Name = "ReleasedDate",
            //    Visible = true,
            //    VisibleIndex = 20,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[21] = new GridColumn
            //{
            //    Caption = @"Released By",
            //    FieldName = "ReleasedBy",
            //    Name = "ReleasedBy",
            //    Visible = true,
            //    VisibleIndex = 21,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};



            //arrGridCol[22] = new GridColumn
            //{
            //    Caption = @"Confirmed Date",
            //    FieldName = "ConfirmedDate",
            //    Name = "ConfirmedDate",
            //    Visible = true,
            //    VisibleIndex = 22,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[23] = new GridColumn
            //{
            //    Caption = @"Confirmed By",
            //    FieldName = "ConfirmeBy",
            //    Name = "ConfirmeBy",
            //    Visible = true,
            //    VisibleIndex = 23,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};


            //arrGridCol[24] = new GridColumn
            //{
            //    Caption = @"Approved Date",
            //    FieldName = "ApprovedDate",
            //    Name = "ApprovedDate",
            //    Visible = true,
            //    VisibleIndex = 24,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};
            //arrGridCol[25] = new GridColumn
            //{
            //    Caption = @"Approved By",
            //    FieldName = "ApprovedBy",
            //    Name = "ApprovedBy",
            //    Visible = true,
            //    VisibleIndex = 25,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //arrGridCol[26] = new GridColumn
            //{
            //    Caption = @"PK",
            //    FieldName = "PK",
            //    Name = "PK",
            //    Visible = true,
            //    VisibleIndex = 26,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};





            //arrGridCol[27] = new GridColumn
            //{
            //    Caption = @"CNY00019PK",
            //    FieldName = "CNY00019PK",
            //    Name = "CNY00019PK",
            //    Visible = true,
            //    VisibleIndex = 27,
            //    Fixed = FixedStyle.None,
            //    OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
            //    OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
            //    //  ColumnEdit = repositoryText,
            //    //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
            //    AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
            //    //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            //};

            //#endregion

            //gvMain.Columns.AddRange(arrGridCol);
            //  
               
            //ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "PK", "CNY00019PK", "Customer");
            //bool visibleSo = !_arrPrType.Any(p => p == ConstSystem.DefaultPrSampleType || p == ConstSystem.DefaultPrSampleTypeF);

            //if (!visibleSo)
            //{
            //    ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "ProjectNo", "ProductionOrder", "ProjectName", "CustomerOrderNo", "SearchName");
            //}
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;

            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.CustomColumnDisplayText += GvMain_CustomColumnDisplayText;
            gvMain.EndGrouping += GvMain_EndGrouping;
            gcMain.ForceInitialize();



        }

        private void GvMain_EndGrouping(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            gvP.BestFitColumns();
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

        private void GvMain_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "ReleasedDate":
                case "ApprovedDate":
                case "ConfirmedDate":
                    {
                    if (e.DisplayText.EndsWith("1900"))
                    {
                        e.DisplayText = "";
                    }
                }
                    break;
            }
        }


        /// <summary>
        ///     Get Data By Expression and None Exprssion Filter Database
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable GetData()
        {
            return _inf.LoadDataGridViewMain(this._strFilter, _arrPrType);
        }

        /// <summary>
        ///     Updae GridView Pagging, Combobox DataSource
        /// </summary>
        public void UpdateDataForGridView(bool isShowDialog = true)
        {
            if (isShowDialog)
            {
                _dlg = new WaitDialogForm("");
            }
            dt = GetData();
            LoadDataGridView();
            if (isShowDialog)
            {
                _dlg.Close();
            }
        }

        private void LoadDataGridView()
        {

            gvMain.BeginUpdate();


            gcMain.DataSource = dt;
            gvMain.Columns["CreatedDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvMain.Columns["CreatedDate"].DisplayFormat.FormatString = "dd/MM/yyyy";
            gvMain.Columns["AdjustedDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvMain.Columns["AdjustedDate"].DisplayFormat.FormatString = "dd/MM/yyyy";
            ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "PK", "SDRTypeCode");
            //bool visibleSo = !_arrPrType.Any(p => p == ConstSystem.DefaultPrSampleType || p == ConstSystem.DefaultPrSampleTypeF);

            //if (!visibleSo)
            //{
            //    ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "ProjectNo", "ProductionOrder", "ProjectName", "CustomerOrderNo", "SearchName");
            //}

            gvMain.BestFitColumns();
            gvMain.EndUpdate();


        }



        /// <summary>
        ///     Display data on gridview when textbox enter key or search button click
        /// </summary>
        private void SearchResult()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                this._strFilter = string.Format(@" AND [a].[CNY001] LIKE '%{0}%' ", txtSearch.Text.Trim().ToUpper());//AA14025


            }
            else
            {
                this._strFilter = string.Empty;
                XtraMessageBox.Show("Textbox search is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                txtSearch.Select();
            }
            UpdateDataForGridView();
        }
        #endregion

        #region "gridview event"




       

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

      
        #endregion

        #region "Control Load Event"

        private void XtraUCMain_Load(object sender, EventArgs e)
        {
            LoadDataGridView();

        }
        #endregion


        #region "Button Click Event"
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchResult();
            }
        }

        private void txtSearch_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(txtSearch), ToolTipController.DefaultController.GetTitle(txtSearch), txtSearch.PointToScreen(e.Location));
        }
        private void TxtSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit be = (ButtonEdit)sender;
            if (be == null) return;
            EditorButton btn = e.Button;
            if (btn == null) return;
            string tag = ProcessGeneral.GetSafeString(btn.Tag).ToUpper();
            switch (tag)
            {
                case "SEARCH":
                {
                    SearchResult();
                    }
                    break;
                case "PRINT":
                {
                    if (gvMain.RowCount <= 0)
                    {
                        XtraMessageBox.Show("No data display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                        }
                    if (!gcMain.IsPrintingAvailable)
                    {
                        XtraMessageBox.Show("The 'Print Function' library is not found", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }


                  

                        // Open the Preview window.
                        //   gcMain.ShowPrintPreview();

                        printableComponentLink1.Landscape = true;
                    printableComponentLink1.Margins.Bottom = 2;
                    printableComponentLink1.Margins.Top = 2;
                    printableComponentLink1.Margins.Left = 0;
                    printableComponentLink1.Margins.Right = 0;
                 


                        this.printableComponentLink1.CreateReportHeaderArea += this.printableComponentLink1_CreateReportHeaderArea;
                    printableComponentLink1.CreateDocument();

                    printableComponentLink1.ShowPreviewDialog(this);
                        this.printableComponentLink1.CreateReportHeaderArea -= this.printableComponentLink1_CreateReportHeaderArea;

                      
                 
                    }
                    break;
            }

        }

        private void printableComponentLink1_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
           


            TextBrick brick = e.Graph.DrawString("PR LIST", Color.Navy, new RectangleF(0, 0, ProcessGeneral.GetWidthGridView(gvMain), 40), BorderSide.None);
            brick.Font = new Font("Tahoma", 20F, ((FontStyle)((FontStyle.Bold))), GraphicsUnit.Point, ((byte)(0)));
            brick.BackColor = Color.Transparent;
            brick.StringFormat = new BrickStringFormat(StringAlignment.Near);


        }

        #endregion

    }
}
