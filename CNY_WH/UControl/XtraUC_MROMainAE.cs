using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Class;
using CNY_WH.Common;
using CNY_WH.Info;
using CNY_WH.WForm;
using CNY_WH.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using FocusedColumnChangedEventArgs = DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs;
using CellValueChangedEventArgs= DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using GridSumCol = DevExpress.Data.SummaryItemType;
using CNY_BaseSys.Class;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DataRow = System.Data.DataRow;

namespace CNY_WH
{
    public partial class XtraUC_MROMainAE : DevExpress.XtraEditors.XtraUserControl
    {

        #region "Property"
        private bool _changeEffectDrop = false;
        public object StatusMain
        {
            get { return this.txtStatus.EditValue; }
            set { this.txtStatus.EditValue = value; }
        }
        public TextEdit txtStatusP
        {
            get { return this.txtStatus; }
        }

        public object StatusDescriptionMain
        {
            get { return this.txtStatus.EditValue; }
            set { this.txtStatus.EditValue = value; }
        }

        //  private TreeListShowPopupType _popUpType = TreeListShowPopupType.EmptyCell;

        private string _option = "";

        public string _RejectDescription = "";

        private Int64 _prHeaderPk = 0;
        

        public Int64 PrHeaderPk
        {
            get { return this._prHeaderPk; }
            set { this._prHeaderPk = value; }
        }

        private Int64 _StatusPK = 0;
        public Int64 StatusPK
        {
            get { return this._StatusPK; }
            set { this._StatusPK = value; }
        }

        private Int64 _StatusMaxPK = 0;
        public Int64 StatusMaxPK
        {
            get { return this._StatusMaxPK; }
            set { this._StatusMaxPK = value; }
        }

        private string _UserNameStatusMax = "";
        public string UserNameStatusMax
        {
            get { return this._UserNameStatusMax; }
            set { this._UserNameStatusMax = value; }
        }
        //     private TreeListMultiCellSelector _tlCellSelected;
        private readonly string[] _arrTreeColumn;


        private readonly Inf_MaterialReleaseOrder _inf = new Inf_MaterialReleaseOrder();
       
        private bool _checkKeyDown;

        private Dictionary<Int64, AttributeHeaderInfo> _dicAttribute = new Dictionary<Int64, AttributeHeaderInfo>();//PKAtt + Name= Code + Desc
        private Dictionary<Int64, List<BoMInputAttInfo>> _dicAttValue = new Dictionary<Int64, List<BoMInputAttInfo>>(); // Key PKCHild

        private Dictionary<Int64, AttributeHeaderInfo> _dicAttributeRM = new Dictionary<Int64, AttributeHeaderInfo>();//PKAtt + Name= Code + Desc
        private Dictionary<Int64, List<BoMInputAttInfo>> _dicAttValueRM = new Dictionary<Int64, List<BoMInputAttInfo>>(); // Key PKCHild  

     


        private WaitDialogForm _dlg;






        private DataTable _dtUnit;


        //private DataTable _dtType;
        //public object ApproveUser
        //{
        //    get
        //    {
        //        return this.txtApprovedBy.EditValue;
        //    }
        //    set
        //    {
        //        this.txtApprovedBy.EditValue = value;
        //    }
        //}

        //public object ApproveDate
        //{
        //    get
        //    {
        //        return this.txtApprovedDate.EditValue;
        //    }
        //    set
        //    {
        //        this.txtApprovedDate.EditValue = value;
        //    }
        //}


        //public object ReleaseUser
        //{
        //    get
        //    {
        //        return this.txtReleasedBy.EditValue;
        //    }
        //    set
        //    {
        //        this.txtReleasedBy.EditValue = value;
        //    }
        //}

        //public object ReleaseDate
        //{
        //    get
        //    {

        //        return this.txtReleasedDate.EditValue;
        //    }
        //    set
        //    {
        //        this.txtReleasedDate.EditValue = value;
        //    }
        //}


        private readonly RepositoryItemSpinEdit _repositorySpinWaste;
        private readonly RepositoryItemSpinEdit _repositorySpinPoQty;
        private readonly RepositoryItemTextEdit _repositoryTextUpper;
        private readonly RepositoryItemTextEdit _repositoryTextNormal;
        private readonly RepositoryItemTextEdit _repositoryTextDateTime;

        private bool _performEditValueChangeEvent = true;
     
        private List<Int64> _lDelAttributePk = new List<Int64>();
        

        private readonly List<Int64> _lDelCNY00055PK = new List<Int64>();
        private readonly List<Int64> _lDelCNY00056PK = new List<Int64>();

        private const string _FunctionCode = "SR";
        private const string ErrorPrType = "MRO Type is not blank";
        private const string TableCodePR = "CNY00101";
        private const string TableItemCode = "R";
        private SystemGetCodeRule _sysCode;
        private DataTable _dtAdvanceFunc;


        Dictionary<Int64, DataTable> _dicTableColor = new Dictionary<Int64, DataTable>(); //PKCHild

        private string[] _arrFieldEditCtrlD = { "SourceName", "DestinationName", "PartnerName", "ActualQty", "Remark", "DocumentRef" };
        private string[] _arrFieldAllowDelete = { "Supplier", "Purchaser", "ETD", "ETA", "Note", "Color", "SupplierRef" };


        private List<Int64> _lCNY00016PK = new List<Int64>();

        #endregion


        #region "Contructor"

        public XtraUC_MROMainAE(string paraoption,DataTable dtAdvanceFunc, Int64 HeaderPk)
        {
            InitializeComponent();
            
            Int64 pkHeader = _inf.GetPkHeaderCode(TableCodePR, TableItemCode);
            _sysCode = new SystemGetCodeRule(pkHeader, TableCodePR);

            _dtUnit = _inf.LoadListUnit();

            _prHeaderPk = HeaderPk;
            _dtAdvanceFunc = dtAdvanceFunc;

            ProcessGeneral.LoadSearchLookup(searchMROType, _inf.LoadMaterialReleaseOrderType(), "Code", "CNYMF026PK", BestFitMode.BestFitResizePopup, "CNYMF026PK", "MROType");
            searchMROType.EditValueChanged += searchMROType_EditValueChanged;

            _repositorySpinPoQty = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = 0,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
                EditMask = FunctionFormatModule.StrFormatPoQtyDecimal(true, false)
            };

            _repositorySpinPoQty.Buttons.Clear();



            _repositorySpinWaste = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = 0,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
                EditMask = "N5"
            };
            _repositorySpinWaste.Buttons.Clear();



            _repositoryTextNormal = new RepositoryItemTextEdit
            {
                AutoHeight = false,
                CharacterCasing = CharacterCasing.Normal,
            };

            _repositoryTextUpper = new RepositoryItemTextEdit
            {
                AutoHeight = false,
                CharacterCasing = CharacterCasing.Upper,
            };

            _repositoryTextDateTime = new RepositoryItemTextEdit
            {
                AutoHeight = false,
                AllowNullInput = DefaultBoolean.True,
                AllowMouseWheel = false
            };
            _repositoryTextDateTime.Mask.MaskType = MaskType.DateTime;
            _repositoryTextDateTime.Mask.EditMask = ConstSystem.SysDateFormat;

            DataTable dtTempSource = Com_MaterialReleaseOrder.TableTreeviewTemplate();
            int colCountTemp = dtTempSource.Columns.Count;
            _arrTreeColumn = new string[colCountTemp];
            for (int i = 0; i < colCountTemp; i++)
            {
                _arrTreeColumn[i] = dtTempSource.Columns[i].ColumnName;
            }

            this._option = paraoption;

            txtStatus.Properties.ReadOnly = true;
            txtStatusDescription.Enabled = false;
            txtStatus.KeyDown += TxtStatus_KeyDown;


            btnImageRm.Click += btnImageRm_Click;

        
            txtVersion.Properties.ReadOnly = true;
        
            txtCreatedBy.Enabled = false;
            txtCreatedDate.Enabled = false;
            //txtApprovedBy.Enabled = false;
            //txtApprovedDate.Enabled = false;
            txtAdjustedDate.Enabled = false;
            txtAdjustedBy.Enabled = false;
            //txtReleasedBy.Enabled = false;
            //txtReleasedDate.Enabled = false;
            //txtConfirmBy.Enabled = false;
            //txtConfirmDate.Enabled = false;
            GridViewColorCustomInit();
            GridViewDetailCustomInit();
            InitTreeList(tlMain);

            toolTipControllerMain.GetActiveObjectInfo += toolTipControllerMain_GetActiveObjectInfo;



            //ProcessGeneral.SetDateEditFormat(dETD, ConstSystem.SysDateFormat, false, true, DefaultBoolean.Default);
            //ProcessGeneral.SetDateEditFormat(dETA, ConstSystem.SysDateFormat, false, true, DefaultBoolean.Default);

            txtMRONo.Properties.ReadOnly = true;

            txtSender.Properties.ReadOnly = true;
            txtRecipient.Properties.ReadOnly = true;

            txtSender.KeyDown += TxtSender_KeyDown;
            txtRecipient.KeyDown += TxtRecipient_KeyDown;

            txtType.Enabled = false;

           

            btnAddAttribute.Click += BtnAddAttribute_Click;
            btnRemoveAttribute.Click += BtnRemoveAttribute_Click;


            btnConvert.Click += BtnConvert_Click;
            //txtConfirmBy.Enabled = false;
            //txtConfirmDate.Enabled = false;

            btnExportExcel.Click += BtnExportExcel_Click;
            splitCCB.SplitterPosition = 0; // không hiện split bên phải
            chkDragDrop.CheckedChanged += ChkDragDrop_CheckedChanged;

            TabItems.KeyDown += TabItems_KeyDown;

            GridViewStatusListCustomInit();
        }

        #region "Gridview Status List"

        /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
        private void GridViewStatusListCustomInit()
        {


            // gcStatusList.ToolTipController = toolTipController1  ;
            gcStatusList.UseEmbeddedNavigator = true;

            gcStatusList.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcStatusList.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcStatusList.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcStatusList.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcStatusList.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gvStatusList.OptionsBehavior.AutoPopulateColumns = false;
            gvStatusList.OptionsBehavior.Editable = false;//cho phép edit hay ko
            gvStatusList.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvStatusList.OptionsCustomization.AllowColumnMoving = false; // cho phép kéo chuột di chuyển cột lên làm group hay ko
            gvStatusList.OptionsCustomization.AllowQuickHideColumns = false;
            gvStatusList.OptionsCustomization.AllowSort = true;
            gvStatusList.OptionsCustomization.AllowFilter = false;

            //     gvStatusList.OptionsHint.ShowCellHints = true;

            gvStatusList.OptionsView.ShowGroupPanel = false; // show cho phép kéo cột để group
            gvStatusList.OptionsView.ShowIndicator = true; // show số thứ tự
            gvStatusList.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvStatusList.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvStatusList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvStatusList.OptionsView.ShowAutoFilterRow = false;// show lọc hay ko
            gvStatusList.OptionsView.AllowCellMerge = false;
            gvStatusList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvStatusList.OptionsView.ColumnAutoWidth = false;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvStatusList.OptionsNavigation.AutoFocusNewRow = true;
            gvStatusList.OptionsNavigation.UseTabKey = true;

            gvStatusList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvStatusList.OptionsSelection.MultiSelect = false;
            gvStatusList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvStatusList.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvStatusList.OptionsSelection.EnableAppearanceFocusedCell = true;

            gvStatusList.OptionsView.EnableAppearanceEvenRow = true;
            gvStatusList.OptionsView.EnableAppearanceOddRow = true;

            gvStatusList.OptionsView.ShowFooter = false;// không hiện footer

            gvStatusList.OptionsHint.ShowCellHints = false;

            //   gridView1.RowHeight = 25;

            gvStatusList.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvStatusList.OptionsFind.AlwaysVisible = false;
            gvStatusList.OptionsFind.ShowCloseButton = true;
            gvStatusList.OptionsFind.HighlightFindResults = true;
            // đổi màu header khi click chọn
            //new MyFindPanelFilterHelper(gvStatusList)
            //{
            //    IsPerFormEvent = true,

            //};

            gvStatusList.OptionsPrint.AutoWidth = false;



            gvStatusList.FocusedRowChanged += gvStatusList_FocusedRowChanged;
            gvStatusList.CustomDrawCell += gvStatusList_CustomDrawCell;
            gvStatusList.RowStyle += gvStatusList_RowStyle;
            gvStatusList.RowCellStyle += gvStatusList_RowCellStyle;
            gvStatusList.RowCountChanged += gvStatusList_RowCountChanged;
            gvStatusList.CustomDrawRowIndicator += gvStatusList_CustomDrawRowIndicator;
            gvStatusList.FocusedColumnChanged += gvStatusList_FocusedColumnChanged;
            gvStatusList.CustomDrawFooter += gvStatusList_CustomDrawFooter;
            gvStatusList.CustomDrawFooterCell += gvStatusList_CustomDrawFooterCell;
            gvStatusList.ShowingEditor += gvStatusList_ShowingEditor;
            gvStatusList.CellValueChanged += gvStatusList_CellValueChanged;
            gvStatusList.GroupLevelStyle += gvStatusList_GroupLevelStyle;
            gvStatusList.CustomRowCellEdit += gvStatusList_CustomRowCellEdit; // format định dạng cột khi nhập liệu

            gcStatusList.ForceInitialize();

        }

        private void BestFitColumnsStatusList()
        {
            if (gvStatusList.RowCount <= 0) return;
            gvStatusList.BestFitColumns();
            gvStatusList.Columns["Status"].Width += 20;
            gvStatusList.Columns["AltStatus"].Width += 20;
        }

        #region "gridview Status List event"
        private void gvStatusList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
            //tô màu theo trạng thái insert, update
            LinearGradientBrush backBrush;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "RowState"));

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

        private void gvStatusList_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            e.LevelAppearance.BackColor = Color.LemonChiffon;
        }

        private void gvStatusList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            //string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["RowState"]));
            //int CurrentRow = gv.FocusedRowHandle;//Lay ra ten dong hien tai
            //if (rowState == DataStatus.Unchange.ToString())
            //{
            //    gv.SetRowCellValue(CurrentRow, "RowState", DataStatus.Update.ToString());
            //}
            //string fieldName = e.Column.FieldName;
            //switch (fieldName)
            //{

            //}
        }

        private void gvStatusList_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            //switch (fieldName)
            //{
            //}
        }
        private void gvStatusList_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn col = gv.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;

            switch (fieldName)
            {
                default:
                    e.Cancel = true;
                    break;

            }


        }

        private void gvStatusList_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            //Prevent default painting
            e.Handled = true;
        }
        private void gvStatusList_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "Status") return;
            if (e.Bounds.Width > 0 && e.Bounds.Height > 0)
            {
                Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
                using (brush)
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
            e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (e.Column.FieldName == "Status")
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


        private void gvStatusList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = sender as GridView;
           
        }

        private void gvStatusList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = sender as GridView;

        }

        private void gvStatusList_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvStatusList_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {

            if (e.Column.VisibleIndex == 0)
            {
                //Image icon = CNY_AdminSys.Properties.Resources.Increase_icon;
                //e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }

        }

        private void gvStatusList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;
            if (e.RowHandle == GridControl.AutoFilterRowHandle) return;
            string fieldName = e.Column.FieldName;
            //switch (fieldName) //Lay ra ten cot trong gridview
            //{

            //}

        }

        private void gvStatusList_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (e.RowHandle == GridControl.AutoFilterRowHandle) return;
            if (gv.FocusedRowHandle == e.RowHandle)
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;

            }
            else if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;
            }



        }
        #endregion

        #endregion
        private void TabItems_KeyDown(object sender, KeyEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            _checkKeyDown = true;
            TreeListNode node = tl.FocusedNode;
            int rH = tl.GetNodeIndex(node);

            #region "Key Insert"


            if (e.KeyCode == Keys.Insert)
            {
                if (searchMROType.Text == "") // chưa chọn loại phiếu
                {
                    XtraMessageBox.Show("Please choose MRO Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    xtraCC.SelectedTabPage = TabGeneral;
                    searchMROType.Focus();
                }
                else if (searchMROType.Text == "2101") // lấy dữ liệu từ Work Order - không nhập tay
                {
                    XtraMessageBox.Show(string.Format("With MRO Type: '{0}' - Please choose 'Generate'", ProcessGeneral.GetSafeString(txtType.EditValue)), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AddNewItemCode(tl, node, false);
                }
                return;
            }

            #endregion
        }

        #region "Drag Drop Node"
        private void ChkDragDrop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDragDrop.Checked)
            {
                tlMain.AllowDrop = true;
                tlMain.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Multiple;

                tlMain.OptionsDragAndDrop.AcceptOuterNodes = false;

                tlMain.OptionsDragAndDrop.CanCloneNodesOnDrop = true;

                tlMain.BeforeDropNode += TreeList_BeforeDropNode;
                tlMain.BeforeDragNode += TreeList_BeforeDragNode;
                tlMain.DragOver += TreeList_DragOver;
                tlMain.DragDrop += TreeList_DragDrop;
                tlMain.DragLeave += TreeList_DragLeave;
                tlMain.GiveFeedback += TreeList_GiveFeedback;
                tlMain.CalcNodeDragImageIndex += TreeList_CalcNodeDragImageIndex;
            }
            else
            {
                tlMain.AllowDrop = false;
                tlMain.OptionsDragAndDrop.DragNodesMode = DragNodesMode.None;

                tlMain.OptionsDragAndDrop.AcceptOuterNodes = false;

                tlMain.OptionsDragAndDrop.CanCloneNodesOnDrop = true;

                tlMain.BeforeDropNode -= TreeList_BeforeDropNode; tlMain.BeforeDragNode -= TreeList_BeforeDragNode;
                tlMain.DragOver -= TreeList_DragOver;
                tlMain.DragDrop -= TreeList_DragDrop;
                tlMain.DragLeave -= TreeList_DragLeave;
                tlMain.GiveFeedback -= TreeList_GiveFeedback;
                tlMain.CalcNodeDragImageIndex -= TreeList_CalcNodeDragImageIndex;

            }
        }

        #region "Drag Drop Event"
        private void TreeList_CalcNodeDragImageIndex(object sender, CalcNodeDragImageIndexEventArgs e)
        {
            DXDragEventArgs args = e.DragArgs.GetDXDragEventArgs(sender as TreeList);
            IDragNodesProvider provider = args.Data.GetData(typeof(IDragNodesProvider)) as IDragNodesProvider;
            if (provider == null) return;
            if (!AllowDropNodes(args, provider))
                e.ImageIndex = -1;
        }
        private void TreeList_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {

            if (e.Nodes.Count <= 1) return;
            int i = 0, j = i + 1;
            List<TreeListNode> nonDragNodes = new List<TreeListNode>();
            while (i != e.Nodes.Count - 1)
            {
                TreeListNode nonDragNode = ProcessGeneral.HasAsParent(e.Nodes[i], e.Nodes[j]);
                if (nonDragNode != null)
                    nonDragNodes.Add(nonDragNode);
                if (j == e.Nodes.Count - 1)
                {
                    i++; j = i + 1;
                }
                else
                    j++;
            }
            if (nonDragNodes.Count == 0) return;
            foreach (TreeListNode node in nonDragNodes)
                e.Nodes.Remove(node);
        }
        private void TreeList_DragOver(object sender, DragEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            IDragNodesProvider provider = e.Data.GetData(typeof(IDragNodesProvider)) as IDragNodesProvider;
            if (provider == null)
            {
                _changeEffectDrop = false;
                e.Effect = DragDropEffects.None;


            }
            else
            {
                DXDragEventArgs args = e.GetDXDragEventArgs(tl);

                TreeListHitInfo hi = args.HitInfo;
                if (hi.HitInfoType == HitInfoType.Empty)
                {
                    TreeListHitTest hTest = hi.HitTest;
                    TreeListNode node = hTest.Node;
                    if (node == null)
                    {
                        _changeEffectDrop = true;
                        e.Effect = DragDropEffects.Move;


                    }
                    else
                    {

                        _changeEffectDrop = false;

                    }


                }
                else
                {
                    _changeEffectDrop = false;
                    bool allowDragDrop = AllowDropNodes(args, provider);
                    if (!allowDragDrop)
                    {
                        e.Effect = DragDropEffects.None;
                    }

                }
            }
            SetDragCursor(e.Effect);

        }
        private void TreeList_BeforeDropNode(object sender, BeforeDropNodeEventArgs e)
        {
            if (_changeEffectDrop)
            {
                e.Cancel = false;
                return;
            }
            e.Cancel = true;
        }

        //private string GetDescriptionLevel(Int32 level)
        //{
        //    var q1 = _dtLevelDepartment.AsEnumerable()
        //            .Where(p => p.Field<Int32>("Level") == level)
        //            .Select(p => p.Field<string>("Name").Trim())
        //            .ToList();
        //    if (q1.Any())
        //        return q1.First();
        //    return "";
        //}
        //private void UpdateLevelTreeListDragDrop(TreeList tl, List<TreeListNode> lNodeUpd)
        //{
        //    tl.BeginUpdate();
        //    tl.LockReloadNodes();

        //    foreach (TreeListNode node in lNodeUpd)
        //    {
        //        int pLevel = node.Level + 1;
        //        node.SetValue("Level", pLevel);
        //        node.SetValue("Category", GetDescriptionLevel(pLevel));

        //        CalEditValueChangedEvent(node);

        //        foreach (TreeListNode cNode in node.GetAllChildNode())
        //        {
        //            int cLevel = cNode.Level + 1;
        //            cNode.SetValue("Level", cLevel);
        //            cNode.SetValue("Category", GetDescriptionLevel(cLevel));
        //            CalEditValueChangedEvent(cNode);
        //        }
        //    }
        //    tl.UnlockReloadNodes();
        //    tl.EndUpdate();
        //}
        private Int32 GetMaxSortOrderValueOnNode(TreeListNode node)
        {
            var q1 = node == null ? tlMain.Nodes : node.Nodes;
            if (q1.Count <= 0) return 1;
            return q1.Select(p => ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode"))).Max();
        }

        private Int32 GetMaxSortOrderValueOnListNode(List<TreeListNode> lNode)
        {
            if (lNode.Count <= 0) return 1;
            return lNode.Select(p => ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode"))).Max();
        }
        private void TreeList_DragDrop(object sender, DragEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) goto finshed;

            IDragNodesProvider providerN = e.Data.GetData(typeof(IDragNodesProvider)) as IDragNodesProvider;//sai vi triif (providerN == null) goto finshed;
            if (_changeEffectDrop)
            {



                var qSameParent = providerN.DragNodes.Where(p => p.ParentNode == null).ToList();
                var qDiffParent = providerN.DragNodes.Where(p => p.ParentNode != null).ToList();
                int countDiff = qDiffParent.Count;
                int countSame = qSameParent.Count;
                if (qSameParent.Count > 0 && countDiff > 0) goto finshed;
                if (countDiff > 0)
                {
                    tl.BeginUpdate();
                    tl.LockReloadNodes();
                    //    tl.BeginUpdate();
                    int maxSortOrder = GetMaxSortOrderValueOnNode(null);
                    List<TreeListNode> lNodeUpd1 = new List<TreeListNode>();
                    foreach (TreeListNode node in providerN.DragNodes)
                    {
                        node.SetValue("SortOrderNode", maxSortOrder);

                        CalEditValueChangedEvent(node);

                        tl.MoveNode(node, null, true);



                        maxSortOrder++;
                        lNodeUpd1.Add(node);
                    }
                    tl.UnlockReloadNodes();
                    tl.EndUpdate();
                    //UpdateLevelTreeListDragDrop(tl, lNodeUpd1);
                    goto finshed;
                }
                if (countSame > 0) //k di chuyen
                {
                    //qSameParent

                    tl.BeginUpdate();
                    tl.LockReloadNodes();

                    List<TreeListSortIndexAfterDrop> qNodeDrop = ProcessGeneral.GetListTreeNodeSameLevelAfterFormNodeS(tl, qSameParent);
                    //   int visibleIndex = qNodeDrop.First().VisibleIndex;

                    int maxSortOrderC = GetMaxSortOrderValueOnListNode(qNodeDrop.Where(p => p.IsDropNode == 1).Select(p => p.Node).ToList());

                    foreach (var itemC in qNodeDrop.Where(p => p.IsDropNode == 0).OrderBy(p => p.RowIndex))
                    {
                        TreeListNode nodeC = itemC.Node;
                        nodeC.SetValue("SortOrderNode", maxSortOrderC);
                        CalEditValueChangedEvent(nodeC);
                        maxSortOrderC++;
                    }






                    // int i1 = 1;
                    //foreach (TreeListSortIndexAfterDrop itemC1 in qNodeDrop)
                    //{
                    //    TreeListNode nodeC1 = itemC1.Node;
                    //    tl.SetNodeIndex(nodeC1, itemC1.RowIndex);

                    //}
                    tl.UnlockReloadNodes();
                    tl.EndUpdate();



                    //   tl.BeginSort();
                    //   tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
                    //     tl.EndSort();





                    goto finshed;
                }

            }
            else
            {
                DXDragEventArgs args = e.GetDXDragEventArgs(tl);
                var rInfo = args.HitInfo.HitTest.RowInfo;
                if (rInfo == null) goto finshed;
                TreeListNode destNode = rInfo.Node;
                if (destNode == null) goto finshed;



                if (args.DragInsertPosition == DragInsertPosition.None) goto finshed;

                if (args.DragInsertPosition == DragInsertPosition.AsChild)
                {
                    tl.BeginUpdate();
                    tl.LockReloadNodes();
                    int maxSortOrderE = GetMaxSortOrderValueOnNode(destNode);
                    List<TreeListNode> lNodeUpd2 = new List<TreeListNode>();
                    foreach (TreeListNode nodeE in providerN.DragNodes)
                    {

                        nodeE.SetValue("SortOrderNode", maxSortOrderE);
                        CalEditValueChangedEvent(nodeE);

                        tl.MoveNode(nodeE, destNode, true);

                        maxSortOrderE++;
                        lNodeUpd2.Add(nodeE);
                    }
                    tl.UnlockReloadNodes();
                    tl.EndUpdate();
                    destNode.ExpandAll();

                    //UpdateLevelTreeListDragDrop(tl, lNodeUpd2);
                    goto finshed;
                }
                TreeListNode parentNode = destNode.ParentNode;



                if (args.DragInsertPosition == DragInsertPosition.After)
                {


                    var qSameParent = providerN.DragNodes.Where(p => p.ParentNode == parentNode).ToList();
                    var qDiffParent = providerN.DragNodes.Where(p => p.ParentNode != parentNode).ToList();
                    int countDiff = qDiffParent.Count;
                    int countSame = qSameParent.Count;
                    if (countSame > 0 && countDiff > 0) goto finshed;

                    if (countDiff > 0)
                    {
                        //List<TreeListNode> qNodeAfterDrop = new List<TreeListNode>();
                        //foreach (TreeListNode node in qDiffParent)
                        //{
                        //    qNodeAfterDrop.Add(node);
                        //    tl.MoveNode(node, parentNode, true);
                        //}

                        tl.BeginUpdate();
                        tl.LockReloadNodes();

                        List<TreeListSortIndexAfterDrop> qNodeDrop = ProcessGeneral.GetListTreeNodeSameLevelAfterFormNode(tl, parentNode, destNode, qDiffParent, "SortOrderNode", false);
                        List<TreeListNode> lNodeUpd3 = new List<TreeListNode>();
                        foreach (TreeListSortIndexAfterDrop itemF in qNodeDrop)
                        {


                            TreeListNode nodeF = itemF.Node;
                            nodeF.SetValue("SortOrderNode", itemF.RowIndex);
                            CalEditValueChangedEvent(nodeF);
                            if (itemF.IsDropNode == 1)
                            {
                                tl.MoveNode(nodeF, parentNode, true);
                            }
                            lNodeUpd3.Add(nodeF);

                        }
                        tl.UnlockReloadNodes();
                        tl.EndUpdate();
                        //UpdateLevelTreeListDragDrop(tl, lNodeUpd3);
                        goto finshed;
                    }

                    if (countSame > 0)//k di chuyen
                    {
                        //qSameParent


                        tl.BeginUpdate();
                        tl.LockReloadNodes();

                        List<TreeListSortIndexAfterDrop> qNodeDrop = ProcessGeneral.GetListTreeNodeSameLevelAfterFormNode(tl, parentNode, destNode, qSameParent, "SortOrderNode", true);

                        foreach (TreeListSortIndexAfterDrop itemF in qNodeDrop)
                        {


                            TreeListNode nodeF = itemF.Node;
                            nodeF.SetValue("SortOrderNode", itemF.RowIndex);
                            CalEditValueChangedEvent(nodeF);


                        }
                        tl.UnlockReloadNodes();
                        tl.EndUpdate();

                        goto finshed;
                    }

                }


                if (args.DragInsertPosition == DragInsertPosition.Before)
                {



                    var qSameParent = providerN.DragNodes.Where(p => p.ParentNode == parentNode).ToList();
                    var qDiffParent = providerN.DragNodes.Where(p => p.ParentNode != parentNode).ToList();
                    int countDiff = qDiffParent.Count;
                    int countSame = qSameParent.Count;
                    if (countSame > 0 && countDiff > 0) goto finshed;
                    //  int visibleIndexDest = tl.GetVisibleIndexByNode(destNode);
                    if (countDiff > 0)
                    {

                        tl.BeginUpdate();
                        tl.LockReloadNodes();

                        List<TreeListSortIndexAfterDrop> qNodeDrop = ProcessGeneral.GetListTreeNodeSameLevelBeforFormNode(tl, parentNode, destNode, qDiffParent, "SortOrderNode", false);
                        List<TreeListNode> lNodeUpd4 = new List<TreeListNode>();
                        foreach (TreeListSortIndexAfterDrop itemF in qNodeDrop)
                        {


                            TreeListNode nodeF = itemF.Node;
                            nodeF.SetValue("SortOrderNode", itemF.RowIndex);
                            CalEditValueChangedEvent(nodeF);
                            if (itemF.IsDropNode == 1)
                            {
                                tl.MoveNode(nodeF, parentNode, true);
                            }
                            lNodeUpd4.Add(nodeF);
                        }
                        tl.UnlockReloadNodes();
                        tl.EndUpdate();

                        //UpdateLevelTreeListDragDrop(tl, lNodeUpd4);






                        goto finshed;
                    }

                    if (countSame > 0)//k di chuyen
                    {
                        //qSameParent


                        tl.BeginUpdate();
                        tl.LockReloadNodes();

                        List<TreeListSortIndexAfterDrop> qNodeDrop = ProcessGeneral.GetListTreeNodeSameLevelBeforFormNode(tl, parentNode, destNode, qSameParent, "SortOrderNode", true);

                        foreach (TreeListSortIndexAfterDrop itemF in qNodeDrop)
                        {


                            TreeListNode nodeF = itemF.Node;
                            nodeF.SetValue("SortOrderNode", itemF.RowIndex);
                            CalEditValueChangedEvent(nodeF);
                            if (itemF.IsDropNode == 1)
                            {
                                tl.MoveNode(nodeF, parentNode, true);
                            }

                        }
                        tl.UnlockReloadNodes();
                        tl.EndUpdate();



                        goto finshed;
                    }
                }
            }



            finshed:
            SetDefaultCursor();
        }

        private void TreeList_DragLeave(object sender, EventArgs e)
        {
            SetDefaultCursor();
        }

        private void TreeList_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }

        #endregion

        #region "Drag Drop Methold"

        private void SetDefaultCursor()
        {
            Cursor = Cursors.Default;
        }
        private Stream ReadImageFile(string path)
        {
            return File.OpenRead(path);
        }

        private void SetDragCursor(DragDropEffects e)
        {
            if (e == DragDropEffects.Move)
                Cursor = new Cursor(ReadImageFile(Application.StartupPath + @"\move.ico"));
            if (e == DragDropEffects.Copy)
                Cursor = new Cursor(ReadImageFile(Application.StartupPath + @"\copy.ico"));
            if (e == DragDropEffects.None)
                Cursor = Cursors.No;
        }



        private int GetMaxLevelDragNode(IDragNodesProvider provider)
        {

            int maxLevel = 1;
            if (provider == null) return maxLevel;
            foreach (TreeListNode node in provider.DragNodes)
            {
                int templevel = GetMaxLevelNode(node);
                if (templevel > maxLevel)
                {
                    maxLevel = templevel;
                }
            }
            return maxLevel;
        }
        private int GetMaxLevelNode(TreeListNode node)
        {
            var q1 = node.GetAllChildNode().Select(p => p.Level).Distinct().ToList();
            int count = q1.Count;
            return count + 1;
        }

        private bool AllowDropNodes(DXDragEventArgs e, IDragNodesProvider provider)
        {

            //var q1 = _dtLevelDepartment.AsEnumerable().Select(p => p.Field<Int32>("Level")).ToList();
            //if (!q1.Any()) return false;
            TreeListHitInfo hi = e.HitInfo;
            TreeListHitTest ht = hi.HitTest;
            DevExpress.XtraTreeList.ViewInfo.RowInfo rowInfo = ht.RowInfo;

            //int maxLevelDragNode = GetMaxLevelDragNode(provider);
            //int maxLevelAllow = q1.Max();

           // int levelNodeDest = 0;
            if (rowInfo == null) goto finish;
            TreeListNode destNode = rowInfo.Node;
            if (destNode == null) goto finish;
            if (e.DragInsertPosition == DragInsertPosition.None) goto finish;

            if (e.DragInsertPosition == DragInsertPosition.AsChild)
            {
                if (ProcessGeneral.GetSafeInt64(destNode.GetValue("PKCode")) <= 0) return true;
                //if (itemType.ToUpper() == "R") return false;

                //int destLevelP = (int)destNode.GetValue("CodeLevel");
                //bool destSameChildP = (bool)destNode.GetValue("AllowChildSameLevel");
                //foreach (TreeListNode nodeP in provider.DragNodes)
                //{

                //    if (ProcessGeneral.GetSafeInt64(nodeP.GetValue("PKCode")) <= 0) continue;
                //    int sourceLevelP = ProcessGeneral.GetSafeInt(nodeP.GetValue("CodeLevel"));
                //    //  bool sourceSameChild = (bool)node.GetValue("AllowChildSameLevel");
                //    if (destLevelP > sourceLevelP) return false;
                //    if (destLevelP == sourceLevelP && !destSameChildP)
                //        return false;
                //}
                goto finish;
            }

            TreeListNode parentNode = destNode.ParentNode;
            if (parentNode != null)
            {
                destNode = parentNode;
            }
            finish:
            //int levelAllow = maxLevelAllow - levelNodeDest;
            //if (maxLevelDragNode > levelAllow) return false;
            return true;
        }
        #endregion
        #endregion
        private void searchMROType_EditValueChanged(object sender, EventArgs e)
        {
            if (ProcessGeneral.GetSafeString(searchMROType.EditValue) != "")
            {
                txtType.EditValue = ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(searchMROType)["Description"]);
                if (_option.ToUpper() == "ADD")
                {
                    RefreshDataWhenAddOrCopy(true);
                }
            }
            else
            {
                txtType.EditValue = string.Empty;
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {

            DataTable dtC = (DataTable) gcColor.DataSource;
            if (dtC == null || dtC.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No Data Display", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            var f1 = new SaveFileDialog()
            {
                Title = @"Export Data To Excel File",
                Filter = @"Excel 2007,2010,2013 Files (*.xlsx)|*.xlsx|Excel 2003 files (*.xls)|*.xls",
                RestoreDirectory = true
            };
            if (f1.ShowDialog() != DialogResult.OK) return;
            string pathExport = f1.FileName;
            if (f1.CheckFileExists)
            {
                File.Delete(pathExport);
            }
       

            //using (FrmExportPRDetail f = new FrmExportPRDetail())
            //{
            //    f.ExportExcel(dtC, _dicAttValueRM, _dicAttributeRM, pathExport);
            //  //  f.ShowDialog();
            //}
            
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);



        }

        private void EnableLookupType()
        {
            //if (_option == "EDIT")
            //{
            //    searchMROType.Enabled = false;
            //    return;
            //}

            searchMROType.Enabled = tlMain.AllNodesCount <= 0;
        }


        #endregion





        #region "TextEdit"
        private void TxtStatus_KeyDown(object sender, KeyEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            if (e.KeyCode == Keys.F4)
            {
                _dlg = new WaitDialogForm();
                DataTable dtSource = _inf.GetTableStatus(_FunctionCode, _dtAdvanceFunc);
                _dlg.Close();
                if (dtSource.Rows.Count <= 0)
                {
                    XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #region "Init Column"

                var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"CNYMF015PK",
                    FieldName = "CNYMF015PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 30,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
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
                    Text = @"Status Listing",
                    StrFilter = "",
                    IsMultiSelected = false,
                    IsShowFindPanel = false,
                    IsShowFooter = false,
                    IsShowAutoFilterRow = true
                };

                f.OnTransferData += (s1, e1) =>
                {
                    List<DataRow> lDr = e1.ReturnRowsSelected;
                    //_StatusPK = ProcessGeneral.GetSafeInt64(lDr[0]["CNYMF015PK"]);
                    //tE.EditValue = ProcessGeneral.GetSafeString(lDr[0]["Code"]);
                    //txtStatusDescription.EditValue = ProcessGeneral.GetSafeString(lDr[0]["Description"]);
                    // set Check Reject
                    SetCheckReject(ProcessGeneral.GetSafeInt64(lDr[0]["CNYMF015PK"]), ProcessGeneral.GetSafeString(lDr[0]["Code"]), ProcessGeneral.GetSafeString(lDr[0]["Description"]));
                };
                f.ShowDialog();
                return;
            }
            
        }

        private void SetCheckReject(Int64 statusPK, string statusCode, string statusName)
        {
            // set Check Reject
            DataSet ds = new DataSet();
            ds = _inf.GetTableRejectOnStatus(_FunctionCode, _prHeaderPk, statusPK);
            // đổi tên check Reject hay Removed
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkReject.Checked = ProcessGeneral.GetSafeBool(ds.Tables[0].Rows[0]["Rejected"]);
                chkReject.Text = ProcessGeneral.GetSafeString(ds.Tables[0].Rows[0]["UserName"]) == "" ? "Rejected" : "Removed"; //"Rejected"
                _RejectDescription = ProcessGeneral.GetSafeString(ds.Tables[0].Rows[0]["UserName"]) == "" ? "Rejected" : "Removed"; //"Rejected"
            }
            else
            {
                chkReject.Checked = false;
                chkReject.Text = "Rejected";
                _RejectDescription = "Rejected";
            }
            // Enable check Reject
            if (ds.Tables[1].Rows.Count > 0)
            {
                chkReject.Enabled = false;
            }
            else
            {
                chkReject.Enabled = true;
                if (statusPK == ProcessGeneral.GetSafeInt64(ds.Tables[2].Rows[0]["StatusMaxPK"]))
                    // kiểm tra khi chọn status lớn nhất có kiểm tra phân hệ trước đã ký ko
                {
                    // hiện tại chưa kiểm tra
                    _StatusPK = statusPK;
                    txtStatus.EditValue = statusCode;
                    txtStatusDescription.EditValue = statusName;
                }
                else
                {
                    _StatusPK = statusPK;
                    txtStatus.EditValue = statusCode;
                    txtStatusDescription.EditValue = statusName;
                }
                
            }
        }
        private void TxtRecipient_KeyDown(object sender, KeyEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            if (e.KeyCode == Keys.F4)
            {
                ShowListUserF4OnText(tE);
                return;
            }
            if (e.KeyCode == Keys.Delete)
            {
                tE.EditValue = "";
                return;
            }
        }

        private void TxtSender_KeyDown(object sender, KeyEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            if (e.KeyCode == Keys.F4)
            {
                ShowListUserF4OnText(tE);
                return;
            }
            if (e.KeyCode == Keys.Delete)
            {
                tE.EditValue = "";
                return;
            }
        }
        private void ShowListUserF4OnText(TextEdit tE)
        {

            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.LoadListUser();
            _dlg.Close();
            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Username",
                    FieldName = "UserName",
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
                    Caption = @"Full Name",
                    FieldName = "FullName",
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
                Text = @"User Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                tE.EditValue = ProcessGeneral.GetSafeString(lDr[0]["UserName"]);

            };
            f.ShowDialog();
        }
        #endregion



        #region "Generate Attribute"
        private void BtnRemoveAttribute_Click(object sender, EventArgs e)
        {
            RemoveAttribute(tlMain.FocusedNode);
        }


        private void RemoveAttribute(TreeListNode node)
        {
            
            var q1 = tlMain.GetSelectedCells().Select(p => new
            {
                CNY00008PK = ProcessGeneral.GetSafeInt64(p.Column.Tag),
                p.Column.FieldName,
                p.Column.VisibleIndex
            }).Where(p => p.CNY00008PK > 0).Distinct().ToList();




            if (!q1.Any()) return;

            var qDel = q1.Where(p => _dicAttribute.Any(t => t.Key == p.CNY00008PK)).Select(p => new BoMCompactDelAttInfo
            {
                CNY00008PK = p.CNY00008PK,
                FieldName = p.FieldName,
                VisibleIndex = p.VisibleIndex
            }).ToList();
            if (!qDel.Any()) return;
            DataTable dtTemp = tlMain.DataSource as DataTable;
            if (dtTemp == null) return;


            DialogResult dlResult = XtraMessageBox.Show("Do you want to delete this column attribute selected ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlResult != DialogResult.Yes) return;


            Int64 pkFind = 0;

            if (node != null)
            {
                pkFind = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));

            }


            DataTable dtFinal = dtTemp.Clone();

            foreach (DataRow drTemp in dtTemp.Rows)
            {


                dtFinal.ImportRow(drTemp);
            }
            dtFinal.AcceptChanges();
            foreach (var itemDel in qDel)
            {
                Int64 cny00008Pk = itemDel.CNY00008PK;

                _dicAttribute.Remove(cny00008Pk);

                dtFinal.Columns.Remove(itemDel.FieldName);
                

            }
            dtFinal.AcceptChanges();
            foreach (var item in _dicAttValue)
            {
                List<BoMInputAttInfo> lBomInfo = item.Value;
                var qDelAtt = lBomInfo.Where(p => qDel.Any(t => t.CNY00008PK == p.AttibutePK)).ToList();
                foreach (BoMInputAttInfo infoDel in qDelAtt)
                {
                    if (infoDel.RowState != DataStatus.Insert)
                    {
                        _lDelAttributePk.Add(infoDel.PK);
                    }
                    lBomInfo.Remove(infoDel);
                }
            }



       
          

          
           


            LoadDataTreeView(tlMain, dtFinal, false);


      
            if (pkFind > 0)
            {
                TreeListNode nodeFinal = tlMain.FindNodeByKeyID(pkFind);
                if (nodeFinal != null)
                {
                    string tFieldName = tlMain.VisibleColumns[qDel.Min(p => p.VisibleIndex) - 1].FieldName;
                    ProcessGeneral.SetFocusedCellOnTree(tlMain, nodeFinal, tFieldName);
                }

            }
            
        




        }
    



        private void BtnAddAttribute_Click(object sender, EventArgs e)
        {
            GenerateAttribute();
        }

        private void GenerateAttribute()
        {



            
            DataTable dtSelectItem = new DataTable();
            dtSelectItem.Columns.Add("CNY00008PK", typeof(Int64));
            foreach (var item1 in _dicAttribute)
            {
                dtSelectItem.Rows.Add(item1.Key);
            }






            var f1 = new FrmAttributeGenerate(dtSelectItem, "", 0);
            f1.OnGenerateData += (s, e) =>
            {



                DataTable dtR = e.DtReturn;
                if (dtR.Rows.Count > 0)
                {
                    var qAd = dtR.AsEnumerable().Select(p => new
                    {
                        AttibutePK = ProcessGeneral.GetSafeInt64(p["AttibutePK"]),
                        AttibuteName = string.Format("{0}-{1}", ProcessGeneral.GetSafeString(p["AttibuteCode"]), ProcessGeneral.GetSafeString(p["AttibuteName"])),
                        DataType = ProcessGeneral.GetDataAttType(ProcessGeneral.GetSafeString(p["DataAttType"]))
                    }).Distinct().Select(p => new BoMCompactAttInfo
                    {
                        AttibutePK = p.AttibutePK,
                        AttibuteName = p.AttibuteName,
                        DataType = p.DataType,
                    }).Where(p => _dicAttribute.All(t => t.Key != p.AttibutePK)).ToList();
                    InsertColAttributeEditValueChanged(qAd, tlMain.FocusedNode);

                }


            };
            f1.ShowDialog();
            
        }




        private void InsertColAttributeEditValueChanged(List<BoMCompactAttInfo> qAd, TreeListNode node)
        {
            
            if (!qAd.Any()) return;
            DataTable dtTemp = tlMain.DataSource as DataTable;
            if (dtTemp == null) return;





            foreach (var itemAd in qAd)
            {

                string attName = itemAd.AttibuteName;
                Int64 attPkT = itemAd.AttibutePK;
                _dicAttribute.Add(attPkT, new AttributeHeaderInfo
                {
                    AttibuteName = attName,
                    DataType = itemAd.DataType
                });

                dtTemp.Columns.Add(attName, typeof(string));



            }
            Int64 pkFind = 0;
            string tFieldName = "";
            if (node != null)
            {
                pkFind = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
                if (tlMain.FocusedColumn == null)
                {
                    tFieldName = tlMain.VisibleColumns[0].FieldName;
                }
                else
                {
                    tFieldName = tlMain.FocusedColumn.FieldName;
                }
               
            }
           



            dtTemp.AcceptChanges();

            DataTable dtFinal = dtTemp.Clone();

            foreach (DataRow drTemp in dtTemp.Rows)
            {


                dtFinal.ImportRow(drTemp);
            }
            LoadDataTreeView(tlMain, dtFinal, false);








            if (pkFind > 0)
            {
                TreeListNode nodeFinal = tlMain.FindNodeByKeyID(pkFind);
                if (nodeFinal != null)
                {
                    ProcessGeneral.SetFocusedCellOnTree(tlMain, nodeFinal, tFieldName);
                }

            }



            





        }
        #endregion


       



     

        #region "View Image"

        private void btnImageRm_Click(object sender, EventArgs e)
        {
            if (tlMain.AllNodesCount <= 0) return;
            TreeListNode node = tlMain.FocusedNode;
            if (node == null) return;
            Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            if (pkCode == 0) return;

            var f = new FrmViewImageProduct(pkCode);

            f.ShowDialog();
        }



        #endregion



        #region "Load Data"

        

        public void LoadDataWhenEdit(DataTable dtHeader, bool isBestFit)
        {
           
            xtraCC.SelectedTabPageIndex = 0;
            chkReject.Visible = true;

            _lDelAttributePk.Clear();
            _lDelCNY00055PK.Clear();
            _lDelCNY00056PK.Clear();
            _lCNY00016PK.Clear();
            DataRow drHeader = dtHeader.Rows[0];

            _prHeaderPk = ProcessGeneral.GetSafeInt64(drHeader["PK"]);
            _StatusPK = ProcessGeneral.GetSafeInt64(drHeader["StatusPK"]);
            txtStatus.EditValue = ProcessGeneral.GetSafeString(drHeader["StatusCode"]);
            txtStatusDescription.EditValue = ProcessGeneral.GetSafeString(drHeader["StatusDescription"]);

            searchMROType.EditValue = ProcessGeneral.GetSafeInt64(drHeader["MROType"]);
            txtVersion.EditValue = ProcessGeneral.GetSafeInt(drHeader["Version"]);
            txtDescription.EditValue = ProcessGeneral.GetSafeString(drHeader["Description"]);
            txtCreatedBy.EditValue = ProcessGeneral.GetSafeString(drHeader["CreatedBy"]);
            txtCreatedDate.EditValue = FormatDateMin(ProcessGeneral.GetSafeString(drHeader["CreatedDate"]));
            txtAdjustedBy.EditValue = ProcessGeneral.GetSafeString(drHeader["AdjustedBy"]);
            txtAdjustedDate.EditValue = FormatDateMin(ProcessGeneral.GetSafeString(drHeader["AdjustedDate"]));
            chkReject.Checked = ProcessGeneral.GetSafeBool(drHeader["Rejected"]);
            chkReject.Text = ProcessGeneral.GetSafeString(drHeader["UserName"]) == "" ? "Rejected" : "Removed"; //"Rejected"
            _RejectDescription = ProcessGeneral.GetSafeString(drHeader["UserName"]) == "" ? "Rejected" : "Removed"; //"Rejected"
            //txtReleasedBy.EditValue = ProcessGeneral.GetSafeString(drHeader["ReleasedBy"]);
            //txtReleasedDate.EditValue = FormatDateMin(ProcessGeneral.GetSafeString(drHeader["ReleasedDate"]));
            //txtApprovedBy.EditValue = ProcessGeneral.GetSafeString(drHeader["ApprovedBy"]);
            //txtApprovedDate.EditValue = FormatDateMin(ProcessGeneral.GetSafeString(drHeader["ApprovedDate"]));
            //txtConfirmBy.EditValue = ProcessGeneral.GetSafeString(drHeader["ConfirmedBy"]);
            //txtConfirmDate.EditValue = FormatDateMin(ProcessGeneral.GetSafeString(drHeader["ConfirmedDate"]));

            txtRecipient.EditValue = ProcessGeneral.GetSafeString(drHeader["ReciUser"]);
            txtSender.EditValue = ProcessGeneral.GetSafeString(drHeader["SenderUser"]);
            txtMRONo.EditValue = ProcessGeneral.GetSafeString(drHeader["MRONo"]);
            //dETD.EditValue = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(drHeader["ETD"]);
            //dETA.EditValue = ProcessGeneral.GetSafeDatetimeNullableReturnMinDate(drHeader["ETA"]);


            DataSet dsPr = _inf.DisplayDetailNWhenEdit(_prHeaderPk,_FunctionCode);
            DataTable dtParent = dsPr.Tables[0];
            //DataTable dtChild = dsPr.Tables[2];
            DataTable dtAtemp = dsPr.Tables[1];
            DataTable dtAttCol = dsPr.Tables[3];
            //DataTable dtAttRm = dsPr.Tables[4];

            // Load Status List
            gvStatusList.Columns.Clear();
            gcStatusList.DataSource = dsPr.Tables[4];
            ProcessGeneral.HideVisibleColumnsGridView(gvStatusList, false,"PK", "FunctionCode", "AltStatus", "CNYMF015PK");
            ProcessGeneral.SetGridColumnHeader(gvStatusList.Columns["UserName"], "User Name", DefaultBoolean.False, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvStatusList.Columns["DateExecutive"], "Date Executive", DefaultBoolean.False, HorzAlignment.Near, GridFixedCol.None);
            gvStatusList.Columns["DateExecutive"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvStatusList.Columns["DateExecutive"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";

            BestFitColumnsStatusList();
            //==============================

            //=== Status lớn nhất và người ký lớn nhất
            if (dsPr.Tables[5].Rows.Count > 0)
            {
                _StatusMaxPK = ProcessGeneral.GetSafeInt64(dsPr.Tables[5].Rows[0]["StatusMaxPK"]);
                _UserNameStatusMax = ProcessGeneral.GetSafeString(dsPr.Tables[5].Rows[0]["UserNameStatusMax"]);
                if (_StatusPK == _StatusMaxPK && _UserNameStatusMax != "") chkReject.Enabled=false;
            }

            //CreateDicChildRmAtt(dtAttRm);

            //CreateDictionaryColorWhenEditAndCopy(dtChild);

            DataTable dtPrFinal = StandardTreeTableWhenEditCopy(dtParent, dtAtemp, dtAttCol);






            LoadDataTreeView(tlMain, dtPrFinal, isBestFit);

            

            panelStatusList.Visible = true; // Hiển thị lưới Status List

            EnableLookupType();


        }



        private void CreateDicChildRmAtt(DataTable dtAttRm)
        {

            _dicAttributeRM.Clear();




            _dicAttValueRM.Clear();
            _dicAttValueRM = dtAttRm.AsEnumerable().GroupBy(f => new
            {
                CNY00016PK = f.Field<Int64>("CNY00016PK"),
            }).Where(myGroup => myGroup.Any())
                .Select(myGroup => new
                {
                    KeyDic = myGroup.Key.CNY00016PK,
                    TempData = myGroup.GroupBy(f => new
                    {
                        AttibuteCode = f.Field<String>("AttibuteCode"),
                        AttibuteName = f.Field<String>("AttibuteName"),
                        AttibuteValueFull = f.Field<String>("AttibuteValueFull"),
                        AttibutePK = f.Field<Int64>("AttibutePK"),
                        AttibuteValueTemp = f.Field<String>("AttibuteValueTemp"),
                        AttibuteUnit = f.Field<String>("AttibuteUnit"),
                        IsNumber = f.Field<Boolean>("IsNumber"),
                        PK = f.Field<Int64>("PK"),
                        RowState = ProcessGeneral.GetDataStatus(f.Field<String>("RowState"))
                    }).Select(m => new BoMInputAttInfo
                    {
                        AttibuteCode = m.Key.AttibuteCode,
                        AttibuteName = m.Key.AttibuteName,
                        AttibuteValueFull = m.Key.AttibuteValueFull,
                        AttibutePK = m.Key.AttibutePK,
                        AttibuteValueTemp = m.Key.AttibuteValueTemp,
                        AttibuteUnit = m.Key.AttibuteUnit,
                        IsNumber = m.Key.IsNumber,
                        PK = m.Key.PK,
                        RowState = m.Key.RowState,
                    }).Where(n => !string.IsNullOrEmpty(n.AttibuteCode)).ToList()
                }).ToDictionary(item => item.KeyDic, item => item.TempData);


            var qAd = dtAttRm.AsEnumerable().Select(p => new
            {
                AttibutePK = ProcessGeneral.GetSafeInt64(p["AttibutePK"]),
                AttibuteName = string.Format("{0}-{1}", ProcessGeneral.GetSafeString(p["AttibuteCode"]), ProcessGeneral.GetSafeString(p["AttibuteName"])),
                ColumnIndex = ProcessGeneral.GetSafeInt(p["AttIndex"]),
            }).Distinct()
            .Select(p => new BoMCompactAttInfo
            {
                AttibutePK = p.AttibutePK,
                AttibuteName = p.AttibuteName,
                ColumnIndex = p.ColumnIndex
            }).OrderBy(p=>p.ColumnIndex).ToList();


            //var qFinal = dtTreeTemp.AsEnumerable().Join(queryPivot1, p => p.Field<Int64>("ChildPK"), t => t.CNY00016PK, (p, t) => new {
            //    SourceRow = p,
            //    Data = t
            //}).ToList();




            foreach (var itemAd in qAd)
            {
                _dicAttributeRM.Add(itemAd.AttibutePK, new AttributeHeaderInfo
                {
                    AttibuteName = itemAd.AttibuteName,
                    ColumnIndex = itemAd.ColumnIndex
                });

     



            }
          



        }




        private void CreateDictionaryColorWhenEditAndCopy(DataTable dtColor, bool isClear = true)
        {
            if (isClear)
            {
            
                _dicTableColor.Clear();
            }

            _dicTableColor = dtColor.AsEnumerable().GroupBy(f => new
            {
                ParentPK = f.Field<Int64>("ParentPK"),
            }).Select(myGroup => new
            {
                myGroup.Key.ParentPK,
                GroupIndexAggreate = myGroup.Select(f => f).CopyToDataTable()
            }).ToDictionary(t => t.ParentPK, t => t.GroupIndexAggreate);

        }



        private DataTable StandardTreeTableWhenEditCopy(DataTable dtParent,  DataTable dtAtemp, DataTable dtAttCol)
        {

            _dicAttribute.Clear();




            _dicAttValue.Clear();
            _dicAttValue = dtAtemp.AsEnumerable().GroupBy(f => new
                {
                    CNY00055PK = f.Field<Int64>("CNY00102PK"),
                }).Where(myGroup => myGroup.Any())
                .Select(myGroup => new
                {
                    KeyDic = myGroup.Key.CNY00055PK,
                    TempData = myGroup.GroupBy(f => new
                    {
                        AttibuteCode = f.Field<String>("AttibuteCode"),
                        AttibuteName = f.Field<String>("AttibuteName"),
                        AttibuteValueFull = f.Field<String>("AttibuteValueFull"),
                        AttibutePK = f.Field<Int64>("AttibutePK"),
                        AttibuteValueTemp = f.Field<String>("AttibuteValueTemp"),
                        AttibuteUnit = f.Field<String>("AttibuteUnit"),
                        IsNumber = f.Field<Boolean>("IsNumber"),
                        PK = f.Field<Int64>("PK"),
                        RowState = ProcessGeneral.GetDataStatus(f.Field<String>("RowState"))
                    }).Select(m => new BoMInputAttInfo
                    {
                        AttibuteCode = m.Key.AttibuteCode,
                        AttibuteName = m.Key.AttibuteName,
                        AttibuteValueFull = m.Key.AttibuteValueFull,
                        AttibutePK = m.Key.AttibutePK,
                        AttibuteValueTemp = m.Key.AttibuteValueTemp,
                        AttibuteUnit = m.Key.AttibuteUnit,
                        IsNumber = m.Key.IsNumber,
                        PK = m.Key.PK,
                        RowState = m.Key.RowState,
                    }).Where(n => !string.IsNullOrEmpty(n.AttibuteCode)).ToList()
                }).ToDictionary(item => item.KeyDic, item => item.TempData);


            var qAd = dtAtemp.AsEnumerable().Select(p => new
            {
                AttibutePK = ProcessGeneral.GetSafeInt64(p["AttibutePK"]),
                AttibuteName = string.Format("{0}-{1}", ProcessGeneral.GetSafeString(p["AttibuteCode"]), ProcessGeneral.GetSafeString(p["AttibuteName"])),
                DataType = ProcessGeneral.GetDataAttType(ProcessGeneral.GetSafeString(p["DataAttType"]))
            }).Union(dtAttCol.AsEnumerable().Select(p => new
            {
                AttibutePK = ProcessGeneral.GetSafeInt64(p["AttibutePK"]),
                AttibuteName = ProcessGeneral.GetSafeString(p["AttibuteName"]),
                DataType = ProcessGeneral.GetDataAttType(ProcessGeneral.GetSafeString(p["DataAttType"]))
            }))
            .Select(p => new BoMCompactAttInfo
            {
                AttibutePK = p.AttibutePK,
                AttibuteName = p.AttibuteName,
                DataType = p.DataType
            }).ToList();


            //var qFinal = dtTreeTemp.AsEnumerable().Join(queryPivot1, p => p.Field<Int64>("ChildPK"), t => t.CNY00016PK, (p, t) => new {
            //    SourceRow = p,
            //    Data = t
            //}).ToList();




            foreach (var itemAd in qAd)
            {

                string attName = itemAd.AttibuteName;
                Int64 attPkT = itemAd.AttibutePK;
                _dicAttribute.Add(attPkT, new AttributeHeaderInfo
                {
                    AttibuteName = attName,
                    DataType = itemAd.DataType
                });

                dtParent.Columns.Add(attName, typeof(string));



            }
            foreach (DataRow drParent in dtParent.Rows)
            {
                Int64 childPkP = ProcessGeneral.GetSafeInt64(drParent["ChildPK"]);
               


                List<BoMInputAttInfo> lBomInfoLoopP;

                if (_dicAttValue.TryGetValue(childPkP, out lBomInfoLoopP))
                {
                    foreach (BoMInputAttInfo tP in lBomInfoLoopP)
                    {
                        drParent[string.Format("{0}-{1}", tP.AttibuteCode, tP.AttibuteName)] = tP.AttibuteValueFull;


                    }

                }

            }

            dtParent.AcceptChanges();
            return dtParent;




        }



        private string FormatDateMin(string date)
        {
            if (date == "01/01/1900")
                return "";
            return date;
        }
        public void LoadDataWhenAdd(bool isBestFit)
        {
          
            xtraCC.SelectedTabPageIndex = 0;
            chkReject.Visible = false;

            _lDelAttributePk.Clear();
            _lDelCNY00055PK.Clear();
            _lDelCNY00056PK.Clear();
            _lCNY00016PK.Clear();
            DefaultDataOnAdd();
            RefreshDataWhenAddOrCopy(isBestFit);

            txtDescription.Focus();
            

            //searchStatus.Enabled = false; //Không cho thay đổi Status
            panelStatusList.Visible = false; // Ẩn lưới Status List

        }

        private void DefaultDataOnAdd()
        {
            DateTime dServer = ProcessGeneral.GetServerDate();
           
            ClearForm();
            //Set Status
            DataTable dt=new DataTable();
            dt=_inf.GetTableStatus(_FunctionCode, _dtAdvanceFunc);
            _StatusPK = ProcessGeneral.GetSafeInt64(dt.Rows[0]["CNYMF015PK"]);
            txtStatus.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Code"]);
            txtStatusDescription.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Description"]);
            ////////////////////////
            DataTable dtType = _inf.LoadMaterialReleaseOrderType();
            searchMROType.EditValue = ProcessGeneral.GetSafeString(dtType.Rows[0]["CNYMF026PK"]);

            //-------------
            txtCreatedBy.EditValue = DeclareSystem.SysUserName.ToUpper();
            txtCreatedDate.EditValue = dServer.ToString(ConstSystem.SysDateFormat);
            txtAdjustedBy.EditValue = DeclareSystem.SysUserName.ToUpper();
            txtAdjustedDate.EditValue = dServer.ToString(ConstSystem.SysDateFormat);


            //txtReleasedBy.EditValue = @"N/A";
            //txtReleasedDate.EditValue = @"";
            //txtApprovedBy.EditValue = @"N/A";
            //txtApprovedDate.EditValue = @"";
            //txtConfirmBy.EditValue = @"N/A";
            //txtConfirmDate.EditValue = @"";
            _prHeaderPk = 0;

            txtVersion.EditValue = 0;
            txtSender.EditValue = DeclareSystem.SysUserName.ToUpper();

            //lookupType.EditValue = ConstSystem.DefaultPrNormalType;

            _sysCode.CreateCodeString();
            txtMRONo.EditValue = _sysCode.CodeRuleData.StrCode;

        }

        public void ClearForm()
        {
            var q1 = this.FindAllChildrenByType<Control>();
            foreach (Control itemQ1 in q1)
            {
                if (itemQ1 is TextEdit)
                {
                    TextEdit tE = itemQ1 as TextEdit;
                    //tE.EditValue = (tE.Name == "txtStatusTab5") ? "1" : "";
                    tE.EditValue = "";
                }
                if (itemQ1 is LookUpEdit)
                {
                    LookUpEdit lE = itemQ1 as LookUpEdit;
                    lE.EditValue = "";
                }
                if (itemQ1 is SearchLookUpEdit)
                {
                    SearchLookUpEdit slE = itemQ1 as SearchLookUpEdit;
                    slE.EditValue = "";
                }
                if (itemQ1 is ToggleSwitch)
                {
                    ToggleSwitch tS = itemQ1 as ToggleSwitch;
                    tS.IsOn = false;
                }

                if (itemQ1 is CheckEdit)
                {
                    CheckEdit cE = itemQ1 as CheckEdit;
                    cE.Checked = false;
                }

                if (itemQ1 is DateEdit)
                {
                    DateEdit dE = itemQ1 as DateEdit;
                    dE.DateTime = new DateTime(1900, 1, 1);
                }
            }

        }

        public int RefreshDataWhenEditAndSave(bool isBestFit)
        {


            DataTable dtHeader = _inf.DisplayHeaderWhenEdit(_prHeaderPk, _FunctionCode);
            if (dtHeader.Rows.Count <= 0) return 1;

         
            LoadDataWhenEdit(dtHeader, isBestFit);
            return 0;
        }

        public void RefreshDataWhenAddOrCopy(bool isBestFit)
        {

            _dicAttribute.Clear();
            _dicAttValue.Clear();
            _dicTableColor.Clear();
            ClearDataSourceColor();
            LoadDataTreeView(tlMain, Com_MaterialReleaseOrder.TableTreeviewTemplate(), isBestFit);

            EnableLookupType();

        }

        #endregion


        #region "Proccess Treeview"

        private void LoadDataTreeView(TreeList tl, DataTable dt, bool isBestFit)
        {
            string fixCol = tl.GetFirstFixColLeft();
            bool isShowFind = false;
            string findPanelText = "";
            if (tl.FindPanelVisible)
            {
                isShowFind = true;
                findPanelText = tl.FindFilterText;
                tl.HideFindPanel();
            }
            Dictionary<string, Int32> dicWidth = new Dictionary<string, int>();
            if (!isBestFit)
            {
                foreach (TreeListColumn colWidth in tl.VisibleColumns)
                {
                    dicWidth.Add(colWidth.FieldName, colWidth.Width);
                   
                
                }
            }

            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            List<string> arr = _inf.GetSortOrderAttribute(_dicAttribute);
            tl.BeginUpdate();
            //ẩn hiện cột và thể hiện vị trí cột Attribute
            if (searchMROType.Text == "2101") // lấy dữ liệu từ Work Order
            {
                Com_MaterialReleaseOrder.VisibleTreeColumnSort(tl, true, arr);
            }
            else
            {
                Com_MaterialReleaseOrder.VisibleTreeColumnSort(tl, false, arr);
            }
            //ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "TDG00001PK", "SourcePK", "DestinationPK", "ProductionPointPK", "UnitPK", "OriginalPK", "PartnerPK"
            //    , "DateUpate", "UserUpdate", "ComputerUpdate", "RowState", "AllowUpdate");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ItemType"], ".", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SortOrderNode"], "LineID", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ItemCode"], "Item Code (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["ItemCode"].ImageIndex = 0;
            tl.Columns["ItemCode"].ImageAlignment = StringAlignment.Near;
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ItemName"], "Item Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Reference"], "Reference", false, HorzAlignment.Near, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SourceName"], "Source (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["SourceName"].ImageIndex = 0;
            tl.Columns["SourceName"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["DestinationName"], "Destination (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["DestinationName"].ImageIndex = 0;
            tl.Columns["DestinationName"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductionPointName"], "Pro.Point", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["ProductionPointName"].ImageIndex = 0;
            tl.Columns["ProductionPointName"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["UnitName"], "Unit", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            //tl.Columns["UnitName"].ImageIndex = 0;
            //tl.Columns["UnitName"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["PartnerName"], "Partner (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["PartnerName"].ImageIndex = 0;
            tl.Columns["PartnerName"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Quantity"], "Quantity", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["Quantity"].Format.FormatType = FormatType.Numeric;
            tl.Columns["Quantity"].Format.FormatString = FunctionFormatModule.StrFormatStockQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Using"], "Using", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["Using"].Format.FormatType = FormatType.Numeric;
            tl.Columns["Using"].Format.FormatString = FunctionFormatModule.StrFormatStockQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Tolerance"], "Tolerance", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["Tolerance"].Format.FormatType = FormatType.Numeric;
            tl.Columns["Tolerance"].Format.FormatString = FunctionFormatModule.StrFormatStockQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["AmountUC"], "Amount-UC", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["AmountUC"].Format.FormatType = FormatType.Numeric;
            tl.Columns["AmountUC"].Format.FormatString = FunctionFormatModule.StrFormatStockQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["NeededQty"], "Needed Qty", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["NeededQty"].Format.FormatType = FormatType.Numeric;
            tl.Columns["NeededQty"].Format.FormatString = FunctionFormatModule.StrFormatStockQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ActualQty"], "Actual Qty", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["ActualQty"].Format.FormatType = FormatType.Numeric;
            tl.Columns["ActualQty"].Format.FormatString = FunctionFormatModule.StrFormatStockQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Remark"], "Remark", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["DocumentRef"], "Doc.Ref", false, HorzAlignment.Near, TreeFixedStyle.None, "");

            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ETD"], "ETD", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            //tl.Columns["ETD"].Format.FormatType = FormatType.DateTime;
            //tl.Columns["ETD"].Format.FormatString = ConstSystem.SysDateFormat;

            
            foreach (var item in _dicAttribute)
            {
                string s = item.Value.AttibuteName;

                TreeListColumn tColS = tl.Columns[s];
                if (tColS == null) continue;
                ProcessGeneral.SetTreeListColumnHeader(tColS, s, false, HorzAlignment.Near, TreeFixedStyle.None, item.Key);

                tColS.ImageIndex = 0;
                tColS.ImageAlignment = StringAlignment.Near;
            }


            tl.ExpandAll();


            if (isBestFit)
            {
                tl.BestFitColumns();
                //tl.Columns["ETD"].Width = 80;
                //tl.Columns["ETA"].Width = 80;
                //if (tl.Columns["Color"].Width > 200)
                //{
                //    tl.Columns["Color"].Width = 200;
                //}
                
            }
            else
            {
                foreach (var itemWidth in dicWidth)
                {
                    TreeListColumn colWidthSet = tl.Columns[itemWidth.Key];
                    if (colWidthSet == null) continue;
                    colWidthSet.Width = itemWidth.Value;
                }
            }
            if (!string.IsNullOrEmpty(fixCol) && tl.Columns[fixCol] != null && tl.Columns[fixCol].Visible)
            {
                tl.SetFirstFixColLeft(fixCol);
            }
            tl.ForceInitialize();
            tl.EndUpdate();

            if (isShowFind)
            {
                tl.ShowFindPanelTreeList(findPanelText);
            }

            //LoadDetailSourceNullNode();

         

        }

        private void LoadDetailSourceNullNode()
        {
            //if (tlMain.VisibleNodesCount > 0) return;
            //DataTable dtColor = Ctrl_PrGenneral.TableGridChildN1Template();
            //LoadDataGridViewColorFrist(dtColor);
            //LoadDataGridViewDetailFrist(dtColor);

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
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = false;

            treeList.OptionsBehavior.Editable = true; treeList.OptionsView.ShowColumns = true;
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


            treeList.KeyDown += TreeList_KeyDown;
            treeList.EditorKeyDown += TreeList_EditorKeyDown;
            treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;



            
            treeList.CellValueChanged += TreeList_CellValueChanged;

            treeList.FocusedNodeChanged += TreeList_FocusedNodeChanged;


            treeList.CustomColumnDisplayText += TreeList_CustomColumnDisplayText;

           
            LoadDataTreeView(treeList, Com_MaterialReleaseOrder.TableTreeviewTemplate(), true);




        }



        private void TreeList_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "ETD":
                case "ETA":
                {
                    if (e.DisplayText == "01/01/1900")
                    {
                        e.DisplayText = "";
                    }
                }
                    break;
                case "OriginalPK":
                case "OriginalLineID":
                case "Quantity":
                case "Using":
                case "Tolerance":
                case "AmountUC":
                case "NeededQty":
                case "ActualQty":
                    {
                        if (e.DisplayText == "0")
                        {
                            e.DisplayText = "";
                        }
                    }
                    break;
            }
        }



        private void TreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            //SetReloadDataSourceRm(e.OldNode, e.Node);


            if (tl.AllNodesCount <= 0) return;
            TreeListNode focusedNode = tl.FocusedNode;
            if (focusedNode == null) return;

           
            Int64 pkChild = ProcessGeneral.GetSafeInt64(focusedNode.GetValue("ChildPK"));
            //DataTable dtColor;
            //if (!_dicTableColor.TryGetValue(pkChild, out dtColor))
            //{
            //    //_dicTableColor.Add(pkChild, Ctrl_PrGenneral.TableGridChildN1Template());
            //    dtColor = _dicTableColor[pkChild];
            //}

            //if (dtColor.Rows.Count > 0)
            //{
            //    dtColor = StandardTableColorWhenLoad(dtColor);
            //    //LoadDataGridViewColorFrist(dtColor.AsEnumerable().OrderBy(p=>p.Field<string>("Reference")).ThenBy(p => p.Field<string>("RmDimension")).ThenBy(p => p.Field<string>("Position")).CopyToDataTable());
            //}
            //else
            //{
            //    LoadDataGridViewColorFrist(dtColor);
            //}
            //LoadDataGridViewDetailFrist(dtColor);


           


        }

        private DataTable StandardTableColorWhenLoad(DataTable dtColor)
        {

            List<string> qRemove = dtColor.Columns.Cast<DataColumn>().Where(p => p.ColumnName.IndexOf("-", StringComparison.Ordinal) > 0).Select(p => p.ColumnName).ToList();
            int colDelNo = qRemove.Count;
            if (colDelNo>0)
                return dtColor;



            //if (colDelNo > 0)
            //{
            //    foreach (string coldDel in qRemove)
            //    {
            //        dtColor.Columns.Remove(coldDel);
            //    }
            //    dtColor.AcceptChanges();
            //}

            

            Int64[] arrCny00016Pk = dtColor.AsEnumerable().Select(p => p.Field<Int64>("CNY00016PK")).Distinct().ToArray();

            Dictionary<Int64, List<BoMInputAttInfo>> dicAttValueRmTemp = _dicAttValueRM.Where(p => arrCny00016Pk.Any(t => t == p.Key)).ToDictionary(p => p.Key, p => p.Value);

            var qAdd = dicAttValueRmTemp.SelectMany(p => p.Value, (m, n) => new
            {
                AttibuteName = string.Format("{0}-{1}", n.AttibuteCode, n.AttibuteName),
                n.IsNumber
            }).Distinct().ToList();

            if (qAdd.Any())
            {
                foreach (var itemAd in qAdd)
                {                

                    string attName = itemAd.AttibuteName;
                    if (itemAd.IsNumber)
                    {
                        dtColor.Columns.Add(attName, typeof(double));
                    }
                    else
                    {
                        dtColor.Columns.Add(attName, typeof(string));
                    }
                }


              










                dtColor.AcceptChanges();
                foreach (DataRow drColor in dtColor.Rows)
                {
                    Int64 cny00016Pk = ProcessGeneral.GetSafeInt64(drColor["CNY00016PK"]);
                    List<BoMInputAttInfo> lBomInfoLoopP;
                    if (dicAttValueRmTemp.TryGetValue(cny00016Pk, out lBomInfoLoopP))
                    {
                        foreach (BoMInputAttInfo tP in lBomInfoLoopP)
                        {
                            drColor[string.Format("{0}-{1}", tP.AttibuteCode, tP.AttibuteName)] = ConvertStringToDoubleAtt(tP.AttibuteValueTemp,tP.IsNumber);


                        }

                    }

                }
                dtColor.AcceptChanges();

            }
          

         

        
            return dtColor;


            
        }
        private object ConvertStringToDoubleAtt(string value, bool isNumber)
        {
            if (!isNumber) return value;
            if (string.IsNullOrEmpty(value))
                return null;
            double v1 = 0;
            if (!double.TryParse(value, out v1)) return null;
            if (v1 <= 0) return null;
            return v1;
        }
        private bool _isFormatTree = true;
        private void TreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (!_isFormatTree) return;
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;


            try
            {
                string fieldName = col.FieldName;
                bool isParent = node.HasChildren;
                bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
                //bool isGrouping = ProcessGeneral.GetSafeBool(node.GetValue("IsGrouping"));
                //bool isPacking = ProcessGeneral.GetSafeBool(node.GetValue("IsPacking"));

                //if (isGrouping)
                //{
                //    if (isParent)
                //    {
                //        e.Appearance.Font = new Font("Tahoma", 9F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                //    }
                //    else
                //    {
                //        e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                //    }
                //}
                //else
                //{
                if (isParent)
                    {
                        e.Appearance.Font = new Font("Tahoma", 9F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                    }
                    else
                    {
                        e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                    }

                //}


                switch (fieldName)
                {
                    case "Quantity":
                    case "Using":
                    case "Tolerance":
                    case "AmountUC":
                        {

                            e.Appearance.ForeColor = Color.DarkOrange;
                        }
                        break;
                    case "NeededQty":
                        {

                            e.Appearance.ForeColor = Color.DarkRed;
                        }
                        break;
                    case "ActualQty":
                        {

                            e.Appearance.ForeColor = Color.Blue;
                        }
                        break;
                    default:

                        e.Appearance.ForeColor = Color.Black;
                        break;

                }

                if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
                {


                    Int64 childPk = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
                    List<BoMInputAttInfo> lBomInfo;
                    bool find = _dicAttValue.TryGetValue(childPk, out lBomInfo);
                    if (!find)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightGoldenrodYellow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        Int64 pkAttribute = ProcessGeneral.GetSafeInt64(col.Tag);
                        //   AttributeHeaderInfo headInfo = _dicAttribute[pkAttribute];
                        BoMInputAttInfo infoCheck = lBomInfo.FirstOrDefault(p => p.AttibutePK == pkAttribute);
                        if (infoCheck != null)
                        {
                            switch (infoCheck.RowState)
                            {
                                case DataStatus.Unchange:
                                    {
                                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                        e.Appearance.BackColor = Color.LightGoldenrodYellow;
                                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                                    }
                                    break;
                                case DataStatus.Insert:
                                    {
                                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                        e.Appearance.BackColor = Color.GreenYellow;
                                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;

                                    }
                                    break;
                                case DataStatus.Update:
                                    {
                                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                        e.Appearance.BackColor = Color.MediumAquamarine;
                                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;

                                    }
                                    break;
                            }





                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.LightGoldenrodYellow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }



                }
                else
                {
                    switch (fieldName)
                    {

                        case "ItemCode":
                        case "ItemName":
                        case "Reference":
                            {
                                if (ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate")))
                                {
                                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                    e.Appearance.BackColor = Color.Ivory;
                                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                                }
                                else
                                {
                                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                    e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                                }

                            }
                            break;
                        case "LineID":
                        case "ProductionPointName":
                        case "UnitName":
                        case "OriginalPK":
                        case "OriginalLineID":
                        case "NeededQty":
                        case "Quantity":
                        case "Using":
                        case "Tolerance":
                        case "AmountUC":
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }
                            break;
                        case "PartnerName":
                        case "SourceName":
                        case "DestinationName":
                        
                            {
                                if (allowUpdate)
                                {
                                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                    e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                                    e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                                }
                                else
                                {
                                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                    e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                                }

                            }
                            break;
                        case "ActualQty":
                        case "Remark":
                        case "DocumentRef":
                            {
                              
                                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                    e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                                    e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;

                            }
                            break;

                    }



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           



        }

        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;


            bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            switch (fieldName)
            {
                
                case "ProductionPointPK":
                case "UnitPK":
                case "PartnerPK":
                //case "Quantity":
                //case "Using":
                //case "Tolerance":
                //case "AmountUC":
                    e.Cancel = !allowUpdate;
                    break;
                case "SourcePK":
                case "DestinationPK":
                case "ActualQty":
                case "Remark":
                case "DocumentRef":
                    e.Cancel = false;
                    break;
                default:
                    e.Cancel = true;
                    break;
            }



        }

        private void TreeList_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            string fieldName = e.Column.FieldName;

            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {
                e.RepositoryItem = _repositoryTextNormal;
            }
            else
            {
                switch (fieldName)
                {
                    case "ETD":
                    case "ETA":
                        e.RepositoryItem = _repositoryTextDateTime;
                        break;
                    case "Remark":
                    case "DocumentRef":
                        e.RepositoryItem = _repositoryTextNormal;
                        break;
                    case "ItemCode":
                        e.RepositoryItem = _repositoryTextUpper;
                        break;

                }
            }

        }
    

        private void TreeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (!_performEditValueChangeEvent) return;
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            if (e.Column == null) return;
            CalEditValueChangedEvent(node);
            string fieldName = e.Column.FieldName;
            //TreeListNode parentNode = node.ParentNode;
            bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            switch (fieldName)
            {
                //case "Quantity":
                //case "Using":
                //case "Tolerance":
                //case "AmountUC":
                case "ActualQty":
                    {
                        if (allowUpdate)
                        {
                            double ActualQty = ProcessGeneral.GetSafeDouble(node.GetValue("ActualQty"));
                            //double Using = ProcessGeneral.GetSafeDouble(node.GetValue("Using"));
                            //double Tolerance = ProcessGeneral.GetSafeDouble(node.GetValue("Tolerance"));
                            //double AmountUC = ProcessGeneral.GetSafeDouble(node.GetValue("AmountUC"));
                            double Quantity = ActualQty;
                            double NeededQty = ActualQty;
                            node.SetValue("Quantity", Quantity);
                            node.SetValue("NeededQty", NeededQty);
                            CalEditValueChangedEvent(node);
                            //if (parentNode != null)
                            //{
                            //double qtySumPo = parentNode.GetAllChildNode().Sum(p => ProcessGeneral.GetSafeDouble(p.GetValue("POQty_CNY003")));
                            //    parentNode.SetValue("NeededQty", qtySumPo);
                            //    CalEditValueChangedEvent(parentNode);
                            //}
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
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            _checkKeyDown = true;
            TreeListNode node = tl.FocusedNode;
            int rH = tl.GetNodeIndex(node);

            #region "Key Insert"


            if (e.KeyCode == Keys.Insert)
            {
                //if (searchMROType.Text == "") // chưa chọn loại phiếu
                //{
                //    XtraMessageBox.Show("Please choose MRO Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    searchMROType.Focus();
                //    xtraCC.SelectedTabPage = TabGeneral;
                //}
                //else if (searchMROType.Text == "2101") // lấy dữ liệu từ Work Order - không nhập tay
                //{
                //    XtraMessageBox.Show(string.Format("With MRO Type: '{0}' - Please choose 'Generate'", ProcessGeneral.GetSafeString(txtType.EditValue)), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    AddNewItemCode(tl, node, false);
                //}
                //return;
            }

            #endregion

            if (node == null) return;
            //   TreeListNode parentNode = node.ParentNode; 
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;


            string fieldName = col.FieldName;

            int visibleIndex = col.VisibleIndex;

            #region "Process F6 Key"
            if (e.KeyCode == Keys.F6)
            {
                //FrmPackingInfo fInfo = new FrmPackingInfo(ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK")));
                //fInfo.ShowDialog();
                return;
            }


            #endregion

            #region "Process F8 Key"

            if (e.KeyCode == Keys.F8)
            {
                DeletePrLine(tl);
                EnableLookupType();
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

                //ProcessDeleteKey(tl);
                return;
            }

            #endregion

            #region "Process Ctrl+D key"

            if (e.KeyData == (Keys.Control | Keys.D))
            {
                ProcessControlDkey(tl);
                return;
            }

            #endregion

            #region "Key Ctrl+V"

            if (e.KeyData == (Keys.Control | Keys.V) && tl.AllNodesCount > 0)
            {
                //TextEdit tE = tl.ActiveEditor as TextEdit;
                //if (tE != null) return;
                //e.SuppressKeyPress = true;
                //e.Handled = true;
                //  PasteDataOnTreeList(tl);

                return;
            }

            #endregion


            #region "F4 Key"

            if (e.KeyCode == Keys.F4)
            {
                if (fieldName.IndexOf("-", StringComparison.Ordinal) <= 0)
                {
                    switch (fieldName)
                    {
                        case "ItemCode": //set value
                            ShowListRmCodeF4OnTree(node, fieldName);
                            break;
                        case "PartnerName":
                            ShowListSupplierF4OnTree(node, fieldName);

                            break;
                        case "SourceName":
                            ShowListSourceF4OnTree(node, fieldName);
                            break;
                        case "DestinationName":
                            ShowListDestinationF4OnTree(node, fieldName);

                            break;
                        case "SupplierRef": //set value
                            ShowListSupRef4OnTree(node, fieldName);
                            break;
                    }


                }
                else
                {

                    bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
                    if (allowUpdate)
                    {
                        ShowAttributeEditOnTree(e.KeyCode, tl, node, fieldName, visibleIndex);
                    }
                    // ShowListAttributeValueF4OnGridView(node, fieldName);
                }
                return;

            }

            #endregion

            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {
                bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
                if (allowUpdate)
                {
                    ShowAttributeEditOnTree(e.KeyCode, tl, node, fieldName, visibleIndex);
                }


                return;
            }

        }



        #region "Control + D Key"

        private Int32 GetSortOrderFieldControlD(bool isAttribute)
        {
            if (isAttribute) return 3;
            return 2;
        }


        private void ProcessControlDkey(TreeList tl)
        {
            int topIndex = tl.TopVisibleNodeIndex;
            var q1 = tl.GetSelectedCells().ToList();

       


            var qField = q1.Select(p => new
            {
                p.Column.FieldName,
                p.Column.VisibleIndex,
                IsAttribute = p.Column.FieldName.IndexOf("-", StringComparison.Ordinal) > 0,
                Tag = ProcessGeneral.GetSafeInt64(p.Column.Tag)

            }).Distinct().Where(p => _arrFieldEditCtrlD.Any(t => t == p.FieldName) || p.IsAttribute).Select(p => new ControlDInfoFieldTree
            {
                FieldName = p.FieldName,
                VisibleIndex = p.VisibleIndex,
                SortOrder = GetSortOrderFieldControlD(p.IsAttribute),
                IsAttribute = p.IsAttribute,
                TCol = tl.Columns[p.FieldName],
                Tag = p.Tag

            }).OrderByDescending(p => p.SortOrder).ThenBy(p => p.VisibleIndex).ToList();


            //SortOrder = p.Column.FieldName.IndexOf("-", StringComparison.Ordinal) > 0 ? 1 : 0,
            List<ControlDInfoNode> tlCell = q1.Select(p => new
            {
                Node = p.Node,
                ChildPK = ProcessGeneral.GetSafeInt64(p.Node.GetValue("ChildPK")),
              //  ItemType = ProcessGeneral.GetSafeString(p.Node.GetValue("ItemType")),
                RowState = ProcessGeneral.GetSafeString(p.Node.GetValue("RowState")),
                AllowUpdate = ProcessGeneral.GetSafeBool(p.Node.GetValue("AllowUpdate")),
            }).Distinct().Select((p, idx) => new ControlDInfoNode
            {
                Node = p.Node,
                ChildPK = p.ChildPK,
              //  ItemType = p.ItemType,
                RowState = p.RowState,
                Index = idx,
                AllowUpdate = p.AllowUpdate,
            }).ToList();
            tlCell = tlCell.Where(p => p.Index == 0 || p.AllowUpdate).ToList();


            if (tlCell.Count <= 1 || qField.Count <= 0) return;

            List<ControlDInfoFieldTree> qFieldAtt = qField.Where(p => p.IsAttribute).ToList();
            List<ControlDInfoFieldTree> qFieldNotAtt = qField.Where(p => !p.IsAttribute).ToList();


        
            bool isAttribute = qFieldAtt.Count > 0;


            bool isSourceName = false;
            bool isDestinationName = false;
            bool isSupplier = false;
            foreach (var item in qFieldNotAtt)
            {
                string fieldName = item.FieldName;
                if (fieldName == "SourceName")
                {
                    isSourceName = true;
                }
                else if (fieldName == "DestinationName")
                {
                    isDestinationName = true;
                }
                else if (fieldName == "PartnerName")
                {
                    isSupplier = true;
                }
            }




            tl.LockReloadNodes();







            ControlDpasteAtrribute(tlCell, qFieldAtt, qFieldNotAtt,  isAttribute, isSourceName, isDestinationName, isSupplier);



            tl.UnlockReloadNodes();


            tl.BeginUpdate();
          
            tl.TopVisibleNodeIndex = topIndex;
            tl.EndUpdate();



        }

        private Int64 GetParentPkOnTree(TreeListNode node, bool isAddChild)
        {
            if (node == null)
                return 0;
            TreeListNode parentNode = isAddChild ? node : node.ParentNode;
            if (parentNode == null)
                return 0;
            return ProcessGeneral.GetSafeInt64(parentNode.GetValue("ChildPK"));
        }

        private Int64 GetChildPkWhenInsert(int row = 0)
        {
            return ProcessGeneral.GetNextId("SDRTABLE", row);
        }

        private void AddNewItemCode(TreeList tl, TreeListNode focuseNode, bool isAddChild)
        {

            var dtTDG00001PK = tlMain.GetAllNodeTreeList()
                    .Select(p => new
                    {
                        TDG00001PK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00001PK")),
                    }).ToList().CopyToDataTableNew();

            //var dtTDG00001PK2 = tlMain.GetAllNodeTreeList()
            //    .Where(p => ProcessGeneral.GetSafeString(p.GetValue("RowState")) != DataStatus.Unchange.ToString())
            //        .Select(p => new
            //        {
            //            TDG00001PK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00001PK")),
            //            ItemCode = ProcessGeneral.GetSafeString(p.GetValue("ItemCode")),
            //            RowState = ProcessGeneral.GetSafeString(p.GetValue("RowState")),
            //        }).ToList().CopyToDataTableNew();

            _dlg = new WaitDialogForm();
            DataTable dtM = _inf.MRO_LoadNewItem(ProcessGeneral.GetSafeString(searchMROType.Text), dtTDG00001PK,"");
            _dlg.Close();

            if (dtM.Rows.Count <= 0)
            {
                XtraMessageBox.Show("Data is not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Frm_SDR_AddNewItem frm = new Frm_SDR_AddNewItem(dtM)
            {
                //ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = true,
                FormBorderStyle = FormBorderStyle.Sizable,
                Size = new Size(1024, 600),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Item Code Listing",
            };

            frm.OnWizardFinish += (s1, e1) =>
            {
                Int64 parentPk = GetParentPkOnTree(focuseNode, isAddChild);
                List<DataRow> lDr = e1.ReturnRowsSelected;
                DataTable dtAtemp = _inf.MRO_LoadAttribute_OnNewItem(lDr.AsEnumerable().Select(p => new
                {
                    TDG00001PK = p.Field<Int64>("PKCode")
                }).CopyToDataTableNew());

                var qAd = dtAtemp.AsEnumerable().Select(p => new
                {
                    AttibutePK = ProcessGeneral.GetSafeInt64(p["AttibutePK"]),
                    AttibuteName = string.Format("{0}-{1}", ProcessGeneral.GetSafeString(p["AttibuteCode"]), ProcessGeneral.GetSafeString(p["AttibuteName"])),
                    DataType = ProcessGeneral.GetDataAttType(ProcessGeneral.GetSafeString(p["DataAttType"]))
                }).Distinct().Select(p => new BoMCompactAttInfo
                {
                    AttibutePK = p.AttibutePK,
                    AttibuteName = p.AttibuteName,
                    DataType = p.DataType,
                }).Where(p => _dicAttribute.All(t => t.Key != p.AttibutePK)).ToList();



                DataTable dtTemp = tl.DataSource as DataTable;
                if (dtTemp == null) return;

                bool isRefersh = false;


                foreach (var itemAd in qAd)
                {
                    isRefersh = true;
                    string attName = itemAd.AttibuteName;
                    Int64 attPkT = itemAd.AttibutePK;
                    _dicAttribute.Add(attPkT, new AttributeHeaderInfo
                    {
                        AttibuteName = attName,
                        DataType = itemAd.DataType
                    });

                    dtTemp.Columns.Add(attName, typeof(string));



                }

                TreeListColumn focusCol = tl.FocusedColumn;
                if (focusCol == null)
                {
                    focusCol = tl.VisibleColumns[0];
                }
                string fieldName = focusCol.FieldName;
                Int64 pkFind = 0;

                DataTable dtS;


                if (isRefersh)
                {

                    dtTemp.AcceptChanges();
                    DataTable dtFinal = dtTemp.Clone();
                    foreach (DataRow drTemp in dtTemp.Rows)
                    {
                        dtFinal.ImportRow(drTemp);
                    }
                    LoadDataTreeView(tl, dtFinal, false);
                    dtS = tl.DataSource as DataTable;


                }
                else
                {
                    dtS = dtTemp;
                }



                var qCol = tl.Columns.Select(p => new
                {
                    Tag = ProcessGeneral.GetSafeInt64(p.Tag),
                    p.FieldName,
                }).Where(p => p.Tag > 0).Select((p, j) => new
                {
                    Index = j,
                    p.Tag,
                    p.FieldName,
                    AttName = GetAttName(p.FieldName),

                }).ToList().Where(p =>
                    _dicAttribute.Any(t => t.Key == p.Tag && t.Value.DataType == DataAttType.Number))
                    .OrderBy(p => p.Index).ToList();




                //SoComponentPivotInfo


                var queryPivot1 = dtAtemp.AsEnumerable().GroupBy(p => new
                {
                    PKCode = p.Field<Int64>("PKCode"),
                }).Where(myGroup => myGroup.Any()).Select(myGroup => new
                {
                    KeyDic = myGroup.Key.PKCode,
                    TempData = myGroup.GroupBy(t => new
                    {
                        AttibuteCode = t.Field<String>("AttibuteCode"),
                        AttibuteName = t.Field<String>("AttibuteName"),
                        AttibutePK = t.Field<Int64>("AttibutePK"),
                        AttibuteValueFull = t.Field<String>("AttibuteValueFull"),
                        AttibuteValueTemp = t.Field<String>("AttibuteValueTemp"),
                        AttibuteUnit = t.Field<String>("AttibuteUnit"),
                        IsNumber = t.Field<Boolean>("IsNumber"),

                    }).Select(m => new BoMInputAttInfo
                    {
                        AttibuteCode = m.Key.AttibuteCode,
                        AttibuteName = m.Key.AttibuteName,
                        AttibutePK = m.Key.AttibutePK,
                        AttibuteValueFull = m.Key.AttibuteValueFull,
                        AttibuteValueTemp = m.Key.AttibuteValueTemp,
                        AttibuteUnit = m.Key.AttibuteUnit,
                        IsNumber = m.Key.IsNumber,
                    }).Where(n => !string.IsNullOrEmpty(n.AttibuteCode)).ToList()
                }).ToDictionary(item => item.KeyDic, item => item.TempData);



                Int64 pk = GetChildPkWhenInsert(lDr.Count);
                Int32 sortOrder = 1;
                //Int32 soLine = 1;

                var q1 = tl.GetAllNodeTreeList();
                if (q1.Any())
                {
                    var q2 = q1.Select(p => new
                    {
                        GroupColumn = 1,
                        SortOrderNode = ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode"))
                    }).GroupBy(p => p.GroupColumn).Select(s => new
                    {
                        GroupColumn = s.Key,
                        SortOrderNode = s.Max(t => t.SortOrderNode) + 1,
                        //SOLine = s.Max(t => t.SOLine) + 1,
                    }).First();
                    sortOrder = q2.SortOrderNode;
                    //soLine = q2.SOLine;
                }


                foreach (DataRow drItem in lDr)
                {
                    //string soLine = CreateCodeRuleSoLine();

                    if (pkFind == 0)
                    {
                        pkFind = pk;
                    }


                    Int64 pkCode = ProcessGeneral.GetSafeInt64(drItem["PKCode"]);

                    DataRow drAdd = dtS.NewRow();

                    
                    drAdd["ProductionOrder"] = "";
                    drAdd["CusRef"] = "";
                    drAdd["ProductCode"] = "";
                    drAdd["ProductName"] = "";
                    drAdd["ItemType"] = ProcessGeneral.GetSafeString(drItem["ItemType"]);
                    drAdd["SortOrderNode"] = sortOrder;
                    drAdd["TDG00001PK"] = pkCode;
                    drAdd["ItemCode"] = ProcessGeneral.GetSafeString(drItem["ItemCode"]);
                    drAdd["ItemName"] = ProcessGeneral.GetSafeString(drItem["ItemName"]);
                    drAdd["Reference"] = ProcessGeneral.GetSafeString(drItem["Reference"]);
                    drAdd["MainMaterialGroup"] = "";
                    drAdd["SourcePK"] = 0;
                    drAdd["DestinationPK"] = 0;
                    drAdd["ProductionPointPK"] = 0;
                    drAdd["UnitPK"] = ProcessGeneral.GetSafeInt64(drItem["UnitPK"]);
                    drAdd["UnitName"] = ProcessGeneral.GetSafeString(drItem["Unit"]);
                    drAdd["CNY00020PK"] = 0;
                    drAdd["CNY00016PK"] = 0;
                    drAdd["PartnerPK"] = ProcessGeneral.GetSafeInt64(drItem["CustomerPK"]);
                    drAdd["PartnerName"] = ProcessGeneral.GetSafeString(drItem["CustomerName"]);
                    drAdd["Quantity"] = 0;
                    drAdd["Using"] = 1;
                    drAdd["Tolerance"] = 1;
                    drAdd["AmountUC"] = 1;
                    drAdd["NeededQty"] = 0;
                    drAdd["ActualQty"] = 0;
                    drAdd["Remark"] = "";
                    drAdd["DocumentRef"] = "";
                    drAdd["BoMNo"] = "";
                    drAdd["ChildPK"] = pk;
                    drAdd["ParentPK"] = parentPk;
                    drAdd["RowState"] = DataStatus.Insert.ToString();
                    drAdd["AllowUpdate"] = true;


                    List<BoMInputAttInfo> lBomInfo = new List<BoMInputAttInfo>();
                    List<BoMInputAttInfo> lInPut;
                    if (queryPivot1.TryGetValue(pkCode, out lInPut))
                    {
                        foreach (var t in lInPut)
                        {
                            String attibuteValueFull = t.AttibuteValueFull;
                            if (attibuteValueFull == "") continue;
                            String attibuteCode = t.AttibuteCode;
                            String attibuteName = t.AttibuteName;

                            Int64 attibutePk = t.AttibutePK;

                            String attibuteValueTemp = t.AttibuteValueTemp;
                            String attibuteUnit = t.AttibuteUnit;
                            bool isNumber = t.IsNumber;

                            drAdd[string.Format("{0}-{1}", attibuteCode, attibuteName)] = attibuteValueFull;


                            BoMInputAttInfo info = new BoMInputAttInfo
                            {
                                AttibuteCode = attibuteCode,
                                AttibuteName = attibuteName,
                                AttibuteValueFull = attibuteValueFull,
                                AttibutePK = attibutePk,

                                IsNumber = isNumber,
                                AttibuteValueTemp = attibuteValueTemp,
                                AttibuteUnit = attibuteUnit,
                                RowState = DataStatus.Insert,
                                PK = 0
                            };

                            lBomInfo.Add(info);

                        }
                    }

                    _dicAttValue.Add(pk, lBomInfo);



                    //if (qCol.Any())
                    //{
                    //    var qDimenson = qCol.Select(p => new
                    //    {
                    //        p.AttName,
                    //        AttFull = ProcessGeneral.GetSafeString(drAdd[p.FieldName]),
                    //    }).Where(p => !string.IsNullOrEmpty(p.AttFull))
                    //        .Select(p => string.Format("({0} - {1})", p.AttName, p.AttFull)).ToArray();
                    //    if (qDimenson.Any())
                    //    {
                    //        string dimension = string.Join(" - ", qDimenson).Trim();
                    //        drAdd["Dimension"] = dimension;
                    //    }
                    //}

                    dtS.Rows.Add(drAdd);
                    //soLine++;
                    sortOrder++;
                    pk++;
                    //CodeRuleReturnSave codeReturn = _sysCodeSoline.SaveCodeData();
                    //if (!codeReturn.IsSusccess)
                    //{
                    //    XtraMessageBox.Show("SO Line. Is Not Created. \n Please Recheck Data.!!!", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                }

                if (pkFind > 0)
                {
                    dtS.AcceptChanges();
                    //List<string> arr = _inf.GetSortOrderAttribute(_dicAttribute);
                    ////ẩn hiện cột và thể hiện vị trí cột Attribute
                    //if (searchMROType.Text == "2101") // lấy dữ liệu từ Work Order
                    //{
                    //    Com_MaterialReleaseOrder.VisibleTreeColumnSort(tl, true, arr);
                    //}
                    //else
                    //{
                    //    Com_MaterialReleaseOrder.VisibleTreeColumnSort(tl, false, arr);
                    //}
                    TreeListNode nodeFinal = tl.FindNodeByKeyID(pkFind);
                    if (nodeFinal != null)
                    {
                        ProcessGeneral.SetFocusedCellOnTree(tl, nodeFinal, fieldName);
                    }

                }
                
                tl.BestFitColumns();
                EnableLookupType();


            };
            frm.ShowDialog();
            //f.ShowDialog();








        }

        private BoMInputAttInfo GetAttValueByAttPk(List<BoMInputAttInfo> lBomInfo, Int64 pkAttribute)
        {
            return lBomInfo.FirstOrDefault(p => p.AttibutePK == pkAttribute);
        }

        private void ControlDpasteAtrribute(List<ControlDInfoNode> tlCell, List<ControlDInfoFieldTree> qFieldAtt, List<ControlDInfoFieldTree> qFieldNotAtt,
             bool isAttribute, bool isSourceName, bool isDestinationName, bool isSupplier)
        {
            _isFormatTree = false;
            ControlDInfoNode item0 = tlCell[0];
            TreeListNode node0 = item0.Node;
            Int64 childPk0 = item0.ChildPK;

            SuppRefSet isCalSupRef = isSupplier ? SuppRefSet.SubSet : SuppRefSet.NotSet;

            List<Int64> lCalEdit = new List<Int64>();
            if (isAttribute)
            {
                List<BoMInputAttInfo> lBomInfo0;
                if (!_dicAttValue.TryGetValue(childPk0, out lBomInfo0))
                {
                    lBomInfo0 = new List<BoMInputAttInfo>();
                    _dicAttValue.Add(childPk0, lBomInfo0);
                }

                for (int i = 1; i < tlCell.Count; i++)
                {
                    ControlDInfoNode itemL = tlCell[i];
                    TreeListNode nodeL = itemL.Node;
                    //DataStatus rowStateL = ProcessGeneral.GetDataStatus(itemL.RowState);
                    Int64 childPkl = itemL.ChildPK;
                    List<BoMInputAttInfo> lBomInfoL;
                    bool isFindAttA = true;

                    if (!_dicAttValue.TryGetValue(childPkl, out lBomInfoL))
                    {
                        isFindAttA = false;
                        lBomInfoL = new List<BoMInputAttInfo>();

                    }

                    foreach (ControlDInfoFieldTree itemField in qFieldAtt)
                    {
                        string fieldName = itemField.FieldName;
                        string[] arr = fieldName.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        string attibuteCode = arr[0].Trim();
                        string attibuteName = arr[1].Trim();
                        Int64 pkAttribute = itemField.Tag;
                        BoMInputAttInfo bomInfo0 = GetAttValueByAttPk(lBomInfo0, pkAttribute);
                        BoMInputAttInfo bomInfoL = GetAttValueByAttPk(lBomInfoL, pkAttribute);
                        if (bomInfo0 == null && bomInfoL == null) continue;
                        if (bomInfo0 != null && bomInfoL != null)
                        {
                            if (bomInfo0.AttibuteValueFull == bomInfoL.AttibuteValueFull) continue;
                        }
                        if (bomInfo0 != null)
                        {
                            if (bomInfoL == null)
                            {
                                lBomInfoL.Add(new BoMInputAttInfo
                                {
                                    AttibuteCode = attibuteCode,
                                    AttibuteName = attibuteName,
                                    AttibutePK = pkAttribute,
                                    AttibuteValueFull = bomInfo0.AttibuteValueFull,
                                    AttibuteUnit = bomInfo0.AttibuteUnit,
                                    IsNumber = bomInfo0.IsNumber,
                                    AttibuteValueTemp = bomInfo0.AttibuteValueTemp,
                                    RowState = DataStatus.Insert,
                                    PK = 0,


                                });
                            }
                            else
                            {
                                bomInfoL.AttibuteValueFull = bomInfo0.AttibuteValueFull;
                                bomInfoL.AttibuteUnit = bomInfo0.AttibuteUnit;
                                bomInfoL.IsNumber = bomInfo0.IsNumber;
                                bomInfoL.AttibuteValueTemp = bomInfo0.AttibuteValueTemp;
                                if (bomInfoL.RowState == DataStatus.Unchange)
                                {
                                    bomInfoL.RowState = DataStatus.Update;
                                }

                            }
                            nodeL.SetValue(fieldName, bomInfo0.AttibuteValueFull);

                        }
                        else
                        {
                            nodeL.SetValue(fieldName, "");
                            if (bomInfoL.RowState != DataStatus.Insert)
                            {
                                _lDelAttributePk.Add(bomInfoL.PK);
                            }

                            lBomInfoL.Remove(bomInfoL);
                        }
                    }


                    if (!isFindAttA)
                    {
                        _dicAttValue.Add(childPkl, lBomInfoL);
                    }
                    // _dicTableProduction.Add(itemF.CNY00016PK, itemF.GroupIndexAggreate);
                }



            }

            List<ControlDInfoFieldTree> qFieldNotAttFinal = qFieldNotAtt.Where(p => p.FieldName != "SupplierRef").ToList();

            if (qFieldNotAttFinal.Any() || isAttribute)
            {
                List<CalDimensionInfoFinal> qCol2 = new List<CalDimensionInfoFinal>();
                if (isAttribute)
                {
                    qCol2 = tlMain.VisibleColumns.Select(p => new
                    {
                        Tag = ProcessGeneral.GetSafeInt64(p.Tag),
                        p.FieldName,
                        Index = p.VisibleIndex
                    }).Where(p => p.Tag > 0).Where(p =>
                        _dicAttribute.Any(t => t.Key == p.Tag && t.Value.DataType == DataAttType.Number)).Select(p =>
                        new CalDimensionInfoFinal
                        {
                            Index = p.Index,
                            Tag = p.Tag,
                            FieldName = p.FieldName,
                            AttName = GetAttName(p.FieldName),

                        }).OrderBy(p => p.Index).ToList();
                }
            
                for (int u = 1; u < tlCell.Count; u++)
                {
                    ControlDInfoNode itemU = tlCell[u];
                    TreeListNode nodeU = itemU.Node;


                    bool isEdit = false;
                    foreach (ControlDInfoFieldTree fieldFinalInfo in qFieldNotAttFinal)
                    {
                        isEdit = true;
                        string fieldFinal = fieldFinalInfo.FieldName;
                        nodeU.SetValue(fieldFinal, node0.GetValue(fieldFinal));
                        switch (fieldFinal)
                        {
                            case "SourceName":
                                nodeU.SetValue("SourcePK", node0.GetValue("SourcePK"));
                                break;
                            case "DestinationName":
                                nodeU.SetValue("DestinationPK", node0.GetValue("DestinationPK"));
                                break;
                            case "PartnerName":
                                nodeU.SetValue("PartnerPK", node0.GetValue("PartnerPK"));
                                break;

                        }
                    }
                    //if (isAttribute)
                    //{
                    //    string dimension2 = CalDimension(nodeU, qCol2);
                    //    if (dimension2 != ProcessGeneral.GetSafeString(nodeU.GetValue("Dimension")))
                    //    {
                    //        nodeU.SetValue("Dimension", dimension2);
                    //        isEdit = true;
                    //    }
                    //}

                    if (isEdit)
                    {
                        lCalEdit.Add(itemU.ChildPK);

                    }
                }
            }



            //if (isSupRef)
            //{
            //    isCalSupRef = SuppRefSet.MainSet;
            //}


            //if (isCalSupRef != SuppRefSet.NotSet)
            //{
            //    Int64 pkCode0 = ProcessGeneral.GetSafeInt64(node0.GetValue("TDG00001PK"));
            //    Int64 supplierPk0 = ProcessGeneral.GetSafeInt64(node0.GetValue("CNY00002PK"));
            //    Int64 tdg00004pk = ProcessGeneral.GetSafeInt64(node0.GetValue("TDG00004PK"));
            //    string supplierRef0 = ProcessGeneral.GetSafeString(node0.GetValue("SupplierRef"));
            //    for (int u = 1; u < tlCell.Count; u++)
            //    {
            //        ControlDInfoNode itemU = tlCell[u];
            //        TreeListNode nodeU = itemU.Node;
            //        Int64 pkCodeLs = ProcessGeneral.GetSafeInt64(nodeU.GetValue("TDG00001PK"));
            //        Int64 supPkLs = ProcessGeneral.GetSafeInt64(nodeU.GetValue("CNY00002PK"));
            //        if (isCalSupRef == SuppRefSet.SubSet)
            //        {

            //            if (pkCodeLs == pkCode0 && supPkLs == supplierPk0)
            //            {
            //                nodeU.SetValue("SupplierRef", supplierRef0);
            //                nodeU.SetValue("TDG00004PK", tdg00004pk);
            //                lCalEdit.Add(itemU.ChildPK);
            //            }
            //            else
            //            {
            //                Int64 tdg00004pkNew;
            //                string supRefNew = _inf.GetSupplierRefByKey(pkCodeLs, supPkLs,
            //                    ProcessGeneral.GetSafeString(nodeU.GetValue("SupplierRef")), out tdg00004pkNew);
            //                nodeU.SetValue("SupplierRef", supRefNew);
            //                nodeU.SetValue("TDG00004PK", tdg00004pkNew);
            //                lCalEdit.Add(itemU.ChildPK);
            //            }
            //        }
            //        else
            //        {
            //            if (pkCodeLs == pkCode0 && supPkLs == supplierPk0)
            //            {
            //                nodeU.SetValue("SupplierRef", supplierRef0);
            //                nodeU.SetValue("TDG00004PK", tdg00004pk);
            //                lCalEdit.Add(itemU.ChildPK);
            //            }
            //        }


            //    }
            //}


            if (lCalEdit.Count > 0)
            {
                lCalEdit = lCalEdit.Select(p => p).Distinct().ToList();
                List<TreeListNode> lNodeCal = tlCell.Where(p => lCalEdit.Any(t => t == p.ChildPK)).Select(p => p.Node).ToList();
                foreach (TreeListNode nodeCal in lNodeCal)
                {
                    CalEditValueChangedEvent(nodeCal);
                }
            }




            _isFormatTree = true;



        }
        private string GetAttName(string fieldName)
        {
            string[] arr = fieldName.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 2)
                return arr[1].Trim();
            return fieldName;
        }
        private string CalDimension(TreeListNode node, List<CalDimensionInfoFinal> qCol)
        {

            if (!qCol.Any()) return "";
            var qDimenson = qCol.Select(p => new
                {
                    p.AttName,
                    AttFull = ProcessGeneral.GetSafeString(node.GetValue(p.FieldName)),
                }).Where(p => !string.IsNullOrEmpty(p.AttFull))
                .Select(p => string.Format("({0} - {1})", p.AttName, p.AttFull)).ToArray();
            if (qDimenson.Any())
                return string.Join(" - ", qDimenson).Trim();
            return "";
        }



        #endregion


        #region "Delete Key"

        private void ProcessDeleteKey(TreeList tl)
        {
            int topIndex = tl.TopVisibleNodeIndex;
            var q1 = tl.GetSelectedCells().ToList();




            var qField = q1.Select(p => new
            {
                p.Column.FieldName,
                p.Column.VisibleIndex,
                IsAttribute = p.Column.FieldName.IndexOf("-", StringComparison.Ordinal) > 0,
                Tag = ProcessGeneral.GetSafeInt64(p.Column.Tag)

            }).Distinct().Where(p => _arrFieldAllowDelete.Any(t => t == p.FieldName) || p.IsAttribute).Select(p => new DeletedInfoField
            {
                FieldName = p.FieldName,
                VisibleIndex = p.VisibleIndex,
                SortOrder = GetSortOrderFieldControlD(p.IsAttribute),
                IsAttribute = p.IsAttribute,
                TCol = tl.Columns[p.FieldName],
                Tag = p.Tag

            }).OrderByDescending(p => p.SortOrder).ThenBy(p => p.VisibleIndex).ToList();


            //SortOrder = p.Column.FieldName.IndexOf("-", StringComparison.Ordinal) > 0 ? 1 : 0,
            List<DeletedInfoNode> tlCell = q1.Select(p => new
            {
                Node = p.Node,
                ChildPK = ProcessGeneral.GetSafeInt64(p.Node.GetValue("ChildPK")),
                //  ItemType = ProcessGeneral.GetSafeString(p.Node.GetValue("ItemType")),
                RowState = ProcessGeneral.GetSafeString(p.Node.GetValue("RowState")),
                //AllowUpdate = ProcessGeneral.GetSafeBool(p.Node.GetValue("AllowUpdate")),
            }).Distinct().Select((p, idx) => new DeletedInfoNode
            {
                Node = p.Node,
                ChildPK = p.ChildPK,
                //   ItemType = p.ItemType,
                RowState = p.RowState,
                Index = idx,
                //AllowUpdate = p.AllowUpdate,
            }).ToList();
            tlCell = tlCell.Where(p => p.Index == 0 || p.AllowUpdate).ToList();



            if (tlCell.Count <= 0 || qField.Count <= 0) return;

            List<DeletedInfoField> qFieldAtt = qField.Where(p => p.IsAttribute).ToList();
            List<DeletedInfoField> qFieldNotAtt = qField.Where(p => !p.IsAttribute).ToList();



            bool isAttribute = qFieldAtt.Count > 0;


            tl.LockReloadNodes();







            DeletedValueAtrribute(tlCell, qFieldAtt, qFieldNotAtt, isAttribute);



            tl.UnlockReloadNodes();


            tl.BeginUpdate();

            tl.TopVisibleNodeIndex = topIndex;
            tl.EndUpdate();



        }


        private void DeletedValueAtrribute(List<DeletedInfoNode> tlCell, List<DeletedInfoField> qFieldAtt, List<DeletedInfoField> qFieldNotAtt, bool isAttribute)
        {
            _isFormatTree = false;
            if (isAttribute)
            {
                foreach (DeletedInfoNode itemL in tlCell)
                {
                    TreeListNode nodeL = itemL.Node;
                    //DataStatus rowStateL = ProcessGeneral.GetDataStatus(itemL.RowState);
                    Int64 childPkl = itemL.ChildPK;
                    List<BoMInputAttInfo> lBomInfoL;
                    if (!_dicAttValue.TryGetValue(childPkl, out lBomInfoL)) continue;

                    foreach (DeletedInfoField itemField in qFieldAtt)
                    {
                        string fieldName = itemField.FieldName;
                        // string[] arr = fieldName.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        //string attibuteCode = arr[0].Trim();
                        //string attibuteName = arr[1].Trim();
                        Int64 pkAttribute = itemField.Tag;
                        BoMInputAttInfo bomInfoL = GetAttValueByAttPk(lBomInfoL, pkAttribute);
                        if (bomInfoL == null) continue;
                        nodeL.SetValue(fieldName, "");
                        if (bomInfoL.RowState != DataStatus.Insert)
                        {
                            _lDelAttributePk.Add(bomInfoL.PK);
                        }

                        lBomInfoL.Remove(bomInfoL);
                    }
                }
            }

            DateTime minDate = new DateTime(1900, 1, 1);

            if (qFieldNotAtt.Any() || isAttribute)
            {
                List<CalDimensionInfoFinal> qCol2 = new List<CalDimensionInfoFinal>();
                if (isAttribute)
                {
                    qCol2 = tlMain.VisibleColumns.Select(p => new
                    {
                        Tag = ProcessGeneral.GetSafeInt64(p.Tag),
                        p.FieldName,
                        Index = p.VisibleIndex
                    }).Where(p => p.Tag > 0).Where(p =>
                        _dicAttribute.Any(t => t.Key == p.Tag && t.Value.DataType == DataAttType.Number)).Select(p =>
                        new CalDimensionInfoFinal
                        {
                            Index = p.Index,
                            Tag = p.Tag,
                            FieldName = p.FieldName,
                            AttName = GetAttName(p.FieldName),

                        }).OrderBy(p => p.Index).ToList();
                }




                //foreach (DeletedInfoNode itemU in tlCell)
                //{
                //    TreeListNode nodeU = itemU.Node;
                //    bool isEdit = false;
                //    bool isSupRef = false;
                //    foreach (DeletedInfoField fieldFinalInfo in qFieldNotAtt)
                //    {

                //        string fieldFinal = fieldFinalInfo.FieldName;
                //        switch (fieldFinal)
                //        {
                //            case "Supplier":
                //                isSupRef = true;
                //                nodeU.SetValue(fieldFinal, "");
                //                nodeU.SetValue("CNY00002PK", 0);
                //                isEdit = true;
                //                break;
                //            case "Purchaser":
                //                nodeU.SetValue(fieldFinal, "");
                //                nodeU.SetValue("CNY00004PK",0);
                //                isEdit = true;
                //                break;
                //            case "Color":
                //                nodeU.SetValue(fieldFinal, "");
                //                nodeU.SetValue("CNY00050PK", 0);
                //                isEdit = true;
                //                break;
                //            case "ETD":
                //            case "ETA":
                //                nodeU.SetValue(fieldFinal, minDate);
                //                isEdit = true;
                //                break;
                //            case "Note":
                //                nodeU.SetValue(fieldFinal, "");
                //                isEdit = true;
                //                break;
                //            case "SupplierRef":
                //            {
                //                isSupRef = true;

                //            }
                //                break;

                //        }
                //    }
                //    if (isAttribute)
                //    {
                //        string dimension2 = CalDimension(nodeU, qCol2);
                //        if (dimension2 != ProcessGeneral.GetSafeString(nodeU.GetValue("Dimension")))
                //        {
                //            nodeU.SetValue("Dimension", dimension2);
                //            isEdit = true;
                //        }
                //    }
                //    if (isSupRef)
                //    {
                //        nodeU.SetValue("SupplierRef", "");
                //        nodeU.SetValue("TDG00004PK", 0);
                //        isEdit = true;

                //    }
                //    if (isEdit)
                //    {
                //        CalEditValueChangedEvent(nodeU);

                //    }
                //}
            }












            _isFormatTree = true;





        }

        #endregion



        private void ShowAttributeEditOnTree(Keys keyCode, TreeList tl, TreeListNode node, string fieldName, int visibleIndex)
        {

            bool isAlphabet;
            string letter = keyCode.CheckIsAlphabet(out isAlphabet);
            if (string.IsNullOrEmpty(letter)) return;
            Int64 childPk = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));

            DataAttType tempType = DataAttType.String;
            Int64 pkAttribute = ProcessGeneral.GetSafeInt64(tl.Columns[fieldName].Tag);
            AttributeHeaderInfo headInfo = _dicAttribute[pkAttribute];
            tempType = headInfo.DataType;





            string strKey = "";
            string defaultUnit = "";

            List<BoMInputAttInfo> lBomInfo;
            BoMInputAttInfo infoCheck = null;
            if (_dicAttValue.TryGetValue(childPk, out lBomInfo))
            {
                infoCheck = lBomInfo.FirstOrDefault(p => p.AttibutePK == pkAttribute);
            }





            if (isAlphabet)
            {
                strKey = letter;
                if (infoCheck != null)
                {
                    defaultUnit = ProcessGeneral.GetCodeInString(infoCheck.AttibuteUnit);
                }
            }
            else
            {
                if (infoCheck != null)
                {
                    strKey = infoCheck.AttibuteValueTemp;
                    defaultUnit = ProcessGeneral.GetCodeInString(infoCheck.AttibuteUnit);
                }
            }

            if (string.IsNullOrEmpty(defaultUnit) && tempType == DataAttType.Number)
            {
                defaultUnit = ConstSystem.BoMDefaultUnit;
            }
            AttValueReturnInfo rt = ProcessGeneral.ShowFormInputAttributeOnTree(tl, node, visibleIndex, this.FindForm(), tempType, strKey, _dtUnit, defaultUnit, isAlphabet);
            if (!rt.IsSave) return;
            SetValueAttribueWhenInputOnTree(rt, childPk, tempType, pkAttribute, node, fieldName);

            TreeListColumn tCol = tl.VisibleColumns[visibleIndex + 1];
            if (tCol != null)
            {
                ProcessGeneral.SetFocusedCellOnTree(tl, node, tCol.FieldName);
            }
            else
            {

                TreeListNode nextNode = node.NextNode;
                if (nextNode == null) return;
                TreeListColumn nextColumn = tl.VisibleColumns[0];
                if (nextColumn == null) return;
                ProcessGeneral.SetFocusedCellOnTree(tl, nextNode, nextColumn.FieldName);
            }



        }



        private void SetValueAttribueWhenInputOnTree(AttValueReturnInfo rt, Int64 childPk, DataAttType dataType, Int64 pkAttribute, TreeListNode node, string fieldName)
        {




            string[] arr = fieldName.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            string attibuteCode = arr[0].Trim();
            string attibuteName = arr[1].Trim();




            bool isNumber = dataType == DataAttType.Number;


            string attibuteUnit = string.Format("{0}-{1}", rt.UnitCode, rt.UnitName).Trim();
            string valueTemp = dataType.GetAttributeStringValue(rt.Value);
            string valueFull = string.Format("{0}{1}", valueTemp, rt.UnitName).Trim();


            string valueFullCurrent = ProcessGeneral.GetSafeString(node.GetValue(fieldName));
            if (valueFull == valueFullCurrent) return;



            node.SetValue(fieldName, valueFull);
            List<BoMInputAttInfo> lBomInfo;

            if (!_dicAttValue.TryGetValue(childPk, out lBomInfo))
            {
                lBomInfo = new List<BoMInputAttInfo>
                {
                    new BoMInputAttInfo
                    {
                        AttibuteCode = attibuteCode,
                        AttibuteName = attibuteName,
                        AttibutePK = pkAttribute,
                        AttibuteValueFull = valueFull,
                        AttibuteUnit = attibuteUnit,
                        IsNumber = isNumber,
                        AttibuteValueTemp = valueTemp,
                        RowState = DataStatus.Insert,
                        PK = 0
                    }
                };

                _dicAttValue.Add(childPk, lBomInfo);
            }
            else
            {
                BoMInputAttInfo infoCheck = lBomInfo.FirstOrDefault(p => p.AttibutePK == pkAttribute);
                if (infoCheck != null)
                {

                    infoCheck.AttibuteValueFull = valueFull;
                    infoCheck.AttibuteUnit = attibuteUnit;
                    infoCheck.IsNumber = isNumber;
                    infoCheck.AttibuteValueTemp = valueTemp;
                    if (infoCheck.RowState == DataStatus.Unchange)
                    {
                        infoCheck.RowState = DataStatus.Update;
                    }




                }
                else
                {
                    lBomInfo.Add(new BoMInputAttInfo
                    {
                        AttibuteCode = attibuteCode,
                        AttibuteName = attibuteName,

                        AttibutePK = pkAttribute,
                        AttibuteValueFull = valueFull,
                        AttibuteUnit = attibuteUnit,
                        IsNumber = isNumber,
                        AttibuteValueTemp = valueTemp,
                        RowState = DataStatus.Insert,
                        PK = 0

                    });
                }

            }



            var qCol = tlMain.VisibleColumns.Select(p => new
            {
                Tag = ProcessGeneral.GetSafeInt64(p.Tag),
                p.FieldName,
                Index = p.VisibleIndex
            }).Where(p => p.Tag > 0).Where(p =>
                _dicAttribute.Any(t => t.Key == p.Tag && t.Value.DataType == DataAttType.Number)).Select(p => new CalDimensionInfoFinal
            {
                Index = p.Index,
                Tag = p.Tag,
                FieldName = p.FieldName,
                AttName = GetAttName(p.FieldName),

            }).OrderBy(p => p.Index).ToList();

            //string dimension = CalDimension(node, qCol);
            //if (dimension != ProcessGeneral.GetSafeString(node.GetValue("Dimension")))
            //{
            //    node.SetValue("Dimension", dimension);
            //    CalEditValueChangedEvent(node);
            //}





        }






        private void ShowListColorRefF4OnTree(TreeListNode node, string fieldName)
        {

            bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            if (!allowUpdate) return;
            Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            if (pkCode <= 0) return;
            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.LoadListColorRef(pkCode);
            _dlg.Close();
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
                new GridViewTransferDataColumnInit
                {
                    Caption = @"PK",
                    FieldName = "PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
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
                Text = @"Color Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                node.SetValue(fieldName, string.Format("{0}-{1}", ProcessGeneral.GetSafeString(lDr[0]["Code"]), ProcessGeneral.GetSafeString(lDr[0]["Description"])));
                node.SetValue("CNY00050PK", ProcessGeneral.GetSafeInt64(lDr[0]["PK"]));
                CalEditValueChangedEvent(node);

            };
            f.ShowDialog();
        }



        private void ShowListPurchaserF4OnTree(TreeListNode node, string fieldName)
        {

            bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            if (!allowUpdate) return;
            //Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("PKCode"));
            //if (pkCode <= 0) return;
            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.LoadListPurchaserByCode();
            _dlg.Close();
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
                new GridViewTransferDataColumnInit
                {
                    Caption = @"PK",
                    FieldName = "PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
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
                Text = @"Purchaser Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                node.SetValue(fieldName, string.Format("{0}-{1}", ProcessGeneral.GetSafeString(lDr[0]["Code"]), ProcessGeneral.GetSafeString(lDr[0]["Description"])));
                node.SetValue("CNY00004PK", ProcessGeneral.GetSafeInt64(lDr[0]["PK"]));
                CalEditValueChangedEvent(node);

            };
            f.ShowDialog();
        }


        private void ShowListSupRef4OnTree(TreeListNode node, string fieldName)
        {

            bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            if (!allowUpdate) return;

            Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            Int64 supplierPk = ProcessGeneral.GetSafeInt64(node.GetValue("CNY00002PK"));
            if (pkCode <= 0 || supplierPk <= 0) return;
            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.GetListSupplierRef(pkCode, supplierPk);
            _dlg.Close();

            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>
            {
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Supplier Ref",
                    FieldName = "SupplierRef",
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
                    Caption = @"TDG00004PK",
                    FieldName = "TDG00004PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
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
                Text = @"Supplier Ref Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                node.SetValue(fieldName, ProcessGeneral.GetSafeString(lDr[0]["SupplierRef"]));

                node.SetValue("TDG00004PK", ProcessGeneral.GetSafeInt64(lDr[0]["TDG00004PK"]));
                CalEditValueChangedEvent(node);

            };
            f.ShowDialog();
        }

        private void ShowListSourceF4OnTree(TreeListNode node, string fieldName)
        {
            //bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            //string itemType = ProcessGeneral.GetSafeString(node.GetValue("ItemType"));
            //if (!allowUpdate || itemType == "P") return;
            //Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            //if (pkCode <= 0) return;
            DataTable dtSource = _inf.LoadListStock();

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
                 new GridViewTransferDataColumnInit
                {
                    Caption = @"PK",
                    FieldName = "PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                }, new GridViewTransferDataColumnInit
                {
                    Caption = @"Alt Description",
                    FieldName = "AltDescription",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                }
            };

            #endregion

            var f = new FrmTransferData
            {
                DtSource = dtSource,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Source Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                DataRow dr = lDr[0];
                //node.SetValue(fieldName, string.Format("{0}-{1}", ProcessGeneral.GetSafeString(dr["Code"]), ProcessGeneral.GetSafeString(dr["Description"])));
                node.SetValue(fieldName, ProcessGeneral.GetSafeString(dr["Description"]));

                Int64 SourcePK = ProcessGeneral.GetSafeInt64(dr["PK"]);
                node.SetValue("SourcePK", SourcePK);
                CalEditValueChangedEvent(node);

            };
            f.ShowDialog();
        }
        private void ShowListDestinationF4OnTree(TreeListNode node, string fieldName)
        {
            //bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            //string itemType = ProcessGeneral.GetSafeString(node.GetValue("ItemType"));
            //if (!allowUpdate || itemType == "P") return;
            //Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            //if (pkCode <= 0) return;
            DataTable dtSource = _inf.LoadListStock();

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
                 new GridViewTransferDataColumnInit
                {
                    Caption = @"PK",
                    FieldName = "PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                }, new GridViewTransferDataColumnInit
                {
                    Caption = @"Alt Description",
                    FieldName = "AltDescription",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                }
            };

            #endregion

            var f = new FrmTransferData
            {
                DtSource = dtSource,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Destination Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                DataRow dr = lDr[0];
                //node.SetValue(fieldName, string.Format("{0}-{1}", ProcessGeneral.GetSafeString(dr["Code"]), ProcessGeneral.GetSafeString(dr["Description"])));
                node.SetValue(fieldName, ProcessGeneral.GetSafeString(dr["Description"]));

                Int64 DestinationPK = ProcessGeneral.GetSafeInt64(dr["PK"]);
                node.SetValue("DestinationPK", DestinationPK);
                CalEditValueChangedEvent(node);

            };
            f.ShowDialog();
        }

        private void ShowListSupplierF4OnTree(TreeListNode node, string fieldName)
        {
            bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            string itemType = ProcessGeneral.GetSafeString(node.GetValue("ItemType"));
            if (!allowUpdate || itemType=="P") return;
            Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            //if (pkCode <= 0) return;
            DataTable dtSource = _inf.LoadListSupplierByCodeNew(pkCode);

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
                 new GridViewTransferDataColumnInit
                {
                    Caption = @"PK",
                    FieldName = "PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                }, new GridViewTransferDataColumnInit
                {
                    Caption = @"Supplier Ref",
                    FieldName = "SupplierRef",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                }

            };

            #endregion

            var f = new FrmTransferData
            {
                DtSource = dtSource,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Supplier Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                DataRow dr = lDr[0];
                //node.SetValue(fieldName, string.Format("{0}-{1}", ProcessGeneral.GetSafeString(dr["Code"]), ProcessGeneral.GetSafeString(dr["Description"])));
                node.SetValue(fieldName, ProcessGeneral.GetSafeString(dr["Description"]));

                Int64 supplierPk = ProcessGeneral.GetSafeInt64(dr["PK"]);
                node.SetValue("PartnerPK", supplierPk);
                CalEditValueChangedEvent(node);

                /*
                    _dlg = new WaitDialogForm();
                    Int64 tdg00004pk = ProcessGeneral.GetSafeInt64(node.GetValue("PKCode"));
                    string supRef = _inf.GetSupplierRefByKey(pkCode, supplierPk, ProcessGeneral.GetSafeString(node.GetValue("SupplierRef")),out tdg00004pk);

                    node.SetValue("SupplierRef", supRef);

                    node.SetValue("TDG00004PK", tdg00004pk);


                    CalEditValueChangedEvent(node);
                    _dlg.Close();
                    */

            };
            f.ShowDialog();
        }

        private void ShowListRmCodeF4OnTree(TreeListNode node, string fieldName)
        {


            bool allowUpdate = ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"));
            if (!allowUpdate) return;

            //Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            //TreeListNode parentNode = node.ParentNode;
            //Int64 cny00016Pk = 0;
            //if (parentNode != null)
            //{
            //    Int64 parentChildPk = ProcessGeneral.GetSafeInt64(parentNode.GetValue("ChildPK"));
            //    DataTable dtColor;
            //    if (_dicTableColor.TryGetValue(parentChildPk, out dtColor))
            //    {
            //        if (dtColor.Rows.Count > 0)
            //        {
            //            cny00016Pk = ProcessGeneral.GetSafeInt64(dtColor.Rows[0]["CNY00016PK"]);
            //        }
            //    }
            //}
            //else if (gvColor.RowCount > 0)
            //{
            //    cny00016Pk = ProcessGeneral.GetSafeInt64(gvColor.GetRowCellValue(0, "CNY00016PK"));
            //}


            //if (pkCode <= 0) return;
            var dtTDG00001PK = tlMain.GetAllNodeTreeList()
                    .Select(p => new
                    {
                        TDG00001PK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00001PK")),
                    }).ToList().CopyToDataTableNew();

            _dlg = new WaitDialogForm();
            DataTable dtSource = _inf.MRO_LoadNewItem(ProcessGeneral.GetSafeString(searchMROType.Text), dtTDG00001PK, "");
            _dlg.Close();

            if (dtSource.Rows.Count <= 0)
            {
                XtraMessageBox.Show("Data is not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            #region "Init Column"
            var lG = new List<GridViewTransferDataColumnInit>
            {
             
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Code",
                    FieldName = "ItemCode",
                    HorzAlign = HorzAlignment.Near,
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
                    Caption = @"Description",
                    FieldName = "ItemName",
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
                    Caption = @"PK Code",
                    FieldName = "PKCode",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Customer",
                    FieldName = "CustomerName",
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
               
            };

            #endregion

            var f = new FrmTransferData
            {
                DtSource = dtSource,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = true,
                FormBorderStyle = FormBorderStyle.Sizable,
                Size = new Size(900, 600),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Alter Item Code Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = true,
                IsShowFooter = false,
                IsShowAutoFilterRow = false
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;


                DataRow dr = lDr[0];
                node.SetValue(fieldName, ProcessGeneral.GetSafeString(dr["ItemCode"]));
                node.SetValue("ItemName", ProcessGeneral.GetSafeString(dr["ItemName"]));
                if (ProcessGeneral.GetSafeInt64(dr["CustomerPK"])!=0)
                {
                    node.SetValue("PartnerPK", ProcessGeneral.GetSafeInt64(dr["CustomerPK"]));
                    node.SetValue("PartnerName", ProcessGeneral.GetSafeString(dr["CustomerName"]));
                }
                node.SetValue("UnitPK", ProcessGeneral.GetSafeInt64(dr["UnitPK"]));
                node.SetValue("UnitName", ProcessGeneral.GetSafeString(dr["Unit"]));
                Int64 pkCodeNew = ProcessGeneral.GetSafeInt64(dr["PKCode"]);
                node.SetValue("TDG00001PK", pkCodeNew);

                //_dlg = new WaitDialogForm();

                //DataTable dtSup = _inf.LoadListSupplierByCode(pkCodeNew);


                //if (dtSup.Rows.Count == 1)
                //{

                //    node.SetValue("Supplier", string.Format("{0}-{1}", dtSup.Rows[0]["Code"], dtSup.Rows[0]["Description"]));
                //    node.SetValue("CNY00002PK", ProcessGeneral.GetSafeInt64(dtSup.Rows[0]["PK"]));


                //}

                //Int64 supplierPk = ProcessGeneral.GetSafeInt64(node.GetValue("CNY00002PK"));
                //if (pkCodeNew <= 0 || supplierPk <= 0)
                //{
                //    node.SetValue("SupplierRef", "");

                //    node.SetValue("TDG00004PK", 0);
                //}
                //else
                //{
                //    Int64 tdg00004pk;
                //    string supRefNew = _inf.GetSupplierRefByKey(pkCodeNew, supplierPk, ProcessGeneral.GetSafeString(node.GetValue("SupplierRef")), out tdg00004pk);
                //    node.SetValue("SupplierRef", supRefNew);

                //    node.SetValue("TDG00004PK", tdg00004pk);

                //}

                //_dlg.Close();

                CalEditValueChangedEvent(node);




                


            };
            f.ShowDialog();
        }





        private void CalEditValueChangedEvent(TreeListNode node)
        {
            string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState"));
            if (rowState == DataStatus.Unchange.ToString())
            {
                node.SetValue("RowState", DataStatus.Update.ToString());
            }
        }






        private void RemoveSoLine(Int64 pkChild)
        {
            DataTable dtSoChild;
            if (_dicTableColor.TryGetValue(pkChild, out dtSoChild))
            {
                foreach (DataRow dr in dtSoChild.Rows)
                {
                    Int64 pkChildC = ProcessGeneral.GetSafeInt64(dr["ChildPK"]);
                    string rowStateC = ProcessGeneral.GetSafeString(dr["RowState"]);
                    if (rowStateC != DataStatus.Insert.ToString())
                    {
                        _lDelCNY00056PK.Add(pkChildC);
                        _lCNY00016PK.Add(ProcessGeneral.GetSafeInt64(dr["CNY00016PK"]));
                    }

                }
                _dicTableColor.Remove(pkChild);
            }
        }

        private void RemoveTreeListNode(List<TreeListNode> lNode)
        {
            foreach (TreeListNode node in lNode)
            {
                Int64 pkChild = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
                //RemoveSoLine(pkChild);
                string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState"));
                if (_dicAttValue.ContainsKey(pkChild))
                {
                    _dicAttValue.Remove(pkChild);
                }

                if (rowState != DataStatus.Insert.ToString())
                {
                    _lDelCNY00055PK.Add(pkChild);

                }

                tlMain.DeleteNode(node);

            }

        }

        private void DeletePrLine(TreeList tl)
        {
            List<TreeListNode> lNodeDel = tl.GetSelectedCells().Select(p => p.Node).Distinct().ToList();
            if (lNodeDel.Count <= 0) return;

            DialogResult dlResult = XtraMessageBox.Show("Do you want to delete this records selected ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlResult != DialogResult.Yes) return;


            List<TreeListNode> lParentDel = new List<TreeListNode>();
            List<TreeListNode> lChildDel = new List<TreeListNode>();

            foreach (TreeListNode nodeDel in lNodeDel)
            {
                //bool allowUpdate = ProcessGeneral.GetSafeBool(nodeDel.GetValue("AllowUpdate"));
                //if (!allowUpdate) continue;

                if (nodeDel.HasChildren)
                {
                    lParentDel.Add(nodeDel);
                }
                else
                {
                    lChildDel.Add(nodeDel);
                }
            }

            if (lParentDel.Count > 0)
            {
                foreach (TreeListNode nodeAdd in lParentDel)
                {
                    foreach (TreeListNode nodeAc in nodeAdd.Nodes)
                    {
                        lChildDel.Add(nodeAc);
                    }
                
                }
                lChildDel = lChildDel.Distinct().ToList();
            }
          


            tl.BeginUpdate();
            tl.LockReloadNodes();
            _isFormatTree = false;

            RemoveTreeListNode(lChildDel);
            RemoveTreeListNode(lParentDel);
           
            _isFormatTree = true;
            tl.UnlockReloadNodes();
            tl.EndUpdate();

            //LoadDetailSourceNullNode();







        }
        #endregion





    



        #region "Show Tooltip"

      

        private void toolTipControllerMain_GetActiveObjectInfo(object sender,
            ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gcColor) return;
            GridView gv = (GridView)gcColor.FocusedView;
            if (gv == null) return;
            GridHitInfo hi = gv.CalcHitInfo(e.ControlMousePosition);
            if (!hi.InRowCell) return;
            GridColumn gCol = hi.Column;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
           
          


            if (fieldName != "Tolerance" && fieldName != "UC") return;
            int rH = hi.RowHandle;
            if (!gv.IsDataRow(rH)) return;
            double v1 = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, fieldName));
            string fieldName2 = "";
            string text1 = "";
            if (fieldName == "Tolerance")
            {
                fieldName2 = "ToleranceBOM";
                text1 = "(%)Waste";
            }
            else
            {
                fieldName2 = "UC_BOM";
                text1 = "Amount";
            }
            double v2 = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, fieldName2));
            if (v1 == v2) return;

            string text = string.Format("<color=red>{0} (PR = {1:#,0.#####} <> BOM = {2:#,0.#####})</color>", text1, v1, v2);
          
            SuperToolTip sTooltip = new SuperToolTip();
            // Create an object to initialize the SuperToolTip.
            SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();
            args.Title.Text = @"<size=12> <color=blue><b>Info</b></color></size>";
            args.Contents.Text = text;
            sTooltip.Setup(args);
            // Enable HTML formatting.
            sTooltip.AllowHtmlText = DefaultBoolean.True;

            e.Info = new ToolTipControlInfo();
            //<bold>Updated by John (DevExpress Support):</bold>
            //e.Info.Object = hitInfo.Column;
            e.Info.Object = hi.HitTest.ToString() + hi.RowHandle.ToString(); //NewLine
            //<bold>End Update</bold>
            e.Info.ToolTipType = ToolTipType.SuperTip;
            e.Info.SuperTip = sTooltip;
        }

        #endregion

        private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;

            LinearGradientBrush backBrush;
            string rowState = ProcessGeneral.GetSafeString(e.Node.GetValue("RowState"));
            bool allowUpdate = ProcessGeneral.GetSafeBool(e.Node.GetValue("AllowUpdate"));

            if (!allowUpdate)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Yellow, Color.Azure, 90);
            }
            else
            {
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
        
        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;

            if (node.HasChildren)
            {
                e.NodeImageIndex = 0;
            }
            else
            {
                e.NodeImageIndex = 1;
            }
        }
   
       





        #endregion




     






        #region "Save data"

        

        private DataTable TableTempCny00055()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("CNY00054PK", typeof(Int64));
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("CNY0002PK", typeof(Int64));

         
            dt.Columns.Add("CNY002_PRQty", typeof(double));
            dt.Columns.Add("CNY003_POQty", typeof(double));
         
            dt.Columns.Add("CNY007_ETD", typeof(DateTime));
            dt.Columns.Add("CNY008_ETA", typeof(DateTime));
            dt.Columns.Add("CNY010_StockQuantity", typeof(double));

            dt.Columns.Add("CNY011_UCUnit", typeof(string));
            dt.Columns.Add("CNY012_Note", typeof(string));
            dt.Columns.Add("CNY00050PK", typeof(Int64));
            dt.Columns.Add("CNY00004PK", typeof(Int64));

            dt.Columns.Add("IsHasChild", typeof(bool));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("CNY00014PK", typeof(Int64));
            dt.Columns.Add("Dimension", typeof(string));
            dt.Columns.Add("CNY002_PRQtyB", typeof(double));
            dt.Columns.Add("CNY003_POQtyB", typeof(double));
            dt.Columns.Add("CNY011_UCUnitB", typeof(string));
            dt.Columns.Add("PackingFactor", typeof(double));
            dt.Columns.Add("TDG00004PK", typeof(Int64));


            dt.Columns.Add("IsGrouping", typeof(bool));
            dt.Columns.Add("IsPacking", typeof(bool));
            return dt;
        }
        private DataTable TableTempCny00056()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("CNY00055PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY001_PlanQty", typeof(int));
            dt.Columns.Add("CNY00054PK", typeof(Int64));
            dt.Columns.Add("CNY002_PRQty", typeof(double));
            dt.Columns.Add("CNY003_POQty", typeof(double));
            dt.Columns.Add("CNY004_UC", typeof(double));

            dt.Columns.Add("CNY005_Tolerance", typeof(double));
            dt.Columns.Add("CNY006_PercentUsing", typeof(double));
            dt.Columns.Add("CNY002_PRQtyB", typeof(double));
            dt.Columns.Add("CNY003_POQtyB", typeof(double));

            dt.Columns.Add("CNY00019PK", typeof(Int64));
            return dt;
        }


        private DataTable TableTempSaveAttribute()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("ForeignPK", typeof(Int64));
            dt.Columns.Add("AttributePK", typeof(Int64));
            dt.Columns.Add("AttributeValue", typeof(string));
            dt.Columns.Add("AttributeUnit", typeof(string));


            return dt;
        }

        private bool CheckDataBeforeSave()
        {
            if (searchMROType.Text == "") // chưa chọn loại phiếu
            {
                XtraMessageBox.Show("Please choose MRO Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                xtraCC.SelectedTabPage = TabGeneral;
                searchMROType.Focus();
                return false;
            }
            if (_option.ToUpper().Trim() == "ADD")
            {
                CodeRuleReturnSave codeReturn = _sysCode.SaveCodeData();
                if (!codeReturn.IsSusccess)
                {
                    XtraMessageBox.Show("MRO No. Is Not Created. \n Please Recheck Data.!!!", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                txtMRONo.EditValue = _sysCode.CodeRuleData.StrCode;
            }

            return true;
        }


        public Ctrl_MROSave SaveData()
        {
            if (!txtMRONo.Focused)
            {
                txtMRONo.SelectNextControl(ActiveControl, true, true, true, true);

            }
            Ctrl_MROSave ctrl = new Ctrl_MROSave();
            List<TreeListNode> lNode = tlMain.GetAllNodeTreeList();

            if (!CheckDataBeforeSave())
            {
                 ctrl.Result = false;
                 return ctrl;
            }


            _inf.MROType = ProcessGeneral.GetSafeInt64(searchMROType.EditValue);
            _inf.MRONo = ProcessGeneral.GetSafeString(txtMRONo.EditValue);
            //_inf.MRONo = String.Format("{0:yyyyMMdd}", dETD.DateTime);
            //_inf.MRONo = String.Format("{0:yyyyMMdd}", dETA.DateTime);
            _inf.Description = ProcessGeneral.GetSafeString(txtDescription.EditValue);
            _inf.Sender = ProcessGeneral.GetSafeString(txtSender.EditValue);
            _inf.Recipient = ProcessGeneral.GetSafeString(txtRecipient.EditValue);
            _inf.Version = ProcessGeneral.GetSafeInt(txtVersion.Text.Trim());
            _inf.StatusPK = _StatusPK;
            _inf.StatusCode = ProcessGeneral.GetSafeInt(txtStatus.EditValue);
            _inf.CheckReject = ProcessGeneral.GetSafeInt(chkReject.Checked);
            _inf.RejectDescription = _RejectDescription;
            //string releaseBy = txtReleasedBy.Text.Trim();
            //string confirmBy = txtConfirmBy.Text.Trim();
            //string approveBy = txtApprovedBy.Text.Trim();

            //DateTime releaseDate = ProcessGeneral.ConvertDateTimeWithFormat(txtReleasedDate.Text.Trim(), ConstSystem.SysDateConvert);
            //DateTime confirmDate = ProcessGeneral.ConvertDateTimeWithFormat(txtConfirmDate.Text.Trim(), ConstSystem.SysDateConvert);
            //DateTime approveDate = ProcessGeneral.ConvertDateTimeWithFormat(txtApprovedDate.Text.Trim(), ConstSystem.SysDateConvert);



            #region Save Header
            if (this._option.ToUpper() == "ADD")
            {

                Int64 pkTemp = _inf.InsertHeader(_FunctionCode);
                if (pkTemp == 0)
                {
                    XtraMessageBox.Show("System Error While Save Data", "Exclamation", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    ctrl.Result = false;
                    return ctrl;
                }
                _prHeaderPk = pkTemp;
            }
            else
            {
                _inf.PK = _prHeaderPk;
                if (!_inf.UpdateHeader(_FunctionCode))
                {
                    XtraMessageBox.Show("System Error While Save Data", "Exclamation", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    ctrl.Result = false;
                    return ctrl;
                }
                //int status= ProcessGeneral.GetSafeInt(searchStatus.Text.Trim());
                //if (DeclareSystem.SysUserName.ToUpper() != "ADMIN")
                //{
                //    if (status == ConstSystemStatus.STATUS_CONFIRM || status == ConstSystemStatus.STATUS_APPROVE ||
                //        status == ConstSystemStatus.STATUS_REJECT)
                //    {
                //        isSaveDetail = false;
                //    }
                //}
            }

#endregion

            _dlg = new WaitDialogForm();

            #region Save Atribute theo PK của Header
            SaveAttCol(_prHeaderPk);
            #endregion

            #region Save Deatail và Atribute trong Detail
            bool isSaveDetail = true;

            if (isSaveDetail)
            {
                int version= ProcessGeneral.GetSafeInt(txtVersion.Text.Trim());

                SaveDetail(lNode, _prHeaderPk, version);
            }

#endregion

            //var qFed = _lCNY00016PK.Where(p => p > 0).Select(p => new
            //{
            //    CNY00016PK = p
            //}).Distinct().ToList();

            //if (qFed.Count > 0)
            //{
            //    DataTable dtFed = qFed.CopyToDataTableNew();
            //    _inf.FeedBackPr(dtFed);

            //}

            _lCNY00016PK.Clear();


            _dlg.Close();
            ctrl.Result = true;
            ctrl.PrHeaderPk = _prHeaderPk;
            ctrl.IsInsert = this._option.ToUpper() == "ADD";
            _option = "EDIT";
            return ctrl;
        }


        private void SaveAttCol(Int64 prHeaderPk)
        {
            DataTable dtC = ProcessGeneral.GetAttColByAttValueDic(_dicAttValue, "CNY00008PK");
            _inf.InsUpdDelColAtt(prHeaderPk, dtC);
        }

        private bool SaveDetail(List<TreeListNode> lParent, Int64 pkPrHeader, Int32 version)
        {


            try
            {
                DeleteDetailOnTree();
                //CNY002_PRQtyB = Math.Round(ProcessGeneral.GetSafeDecimal(p.GetValue("PRQty_CNY002B")), ConstSystem.FormatPrQtyDecimal),
                DataTable dtCNY00102InsertUpdate = tlMain.GetAllNodeTreeList()
                .Where(p => ProcessGeneral.GetSafeString(p.GetValue("RowState")) != DataStatus.Unchange.ToString())
                    .Select(p => new
                    {
                        PK = ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK")),
                        CNY00101PK = pkPrHeader,
                        TDG00001PK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00001PK")),
                        SourcePK = ProcessGeneral.GetSafeInt64(p.GetValue("SourcePK")),
                        DestinationPK = ProcessGeneral.GetSafeInt64(p.GetValue("DestinationPK")),
                        ProductionPointPK = ProcessGeneral.GetSafeInt64(p.GetValue("ProductionPointPK")),
                        UnitPK = ProcessGeneral.GetSafeInt64(p.GetValue("UnitPK")),
                        OriginalPK = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00020PK")),
                        OriginalLineID = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00016PK")),
                        PartnerPK = ProcessGeneral.GetSafeInt64(p.GetValue("PartnerPK")),
                        SortOrderNode = ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode")),
                        Quantity = ProcessGeneral.GetSafeDecimal(p.GetValue("Quantity")),
                        Using = ProcessGeneral.GetSafeDecimal(p.GetValue("Using")),
                        Tolerance = ProcessGeneral.GetSafeDecimal(p.GetValue("Tolerance")),
                        AmountUC = ProcessGeneral.GetSafeDecimal(p.GetValue("AmountUC")),
                        NeededQty = ProcessGeneral.GetSafeDecimal(p.GetValue("NeededQty")),
                        ActualQty = ProcessGeneral.GetSafeDecimal(p.GetValue("ActualQty")),
                        Remark = ProcessGeneral.GetSafeString(p.GetValue("Remark")),
                        DocumentRef = ProcessGeneral.GetSafeString(p.GetValue("DocumentRef")),
                        RowState = ProcessGeneral.GetSafeString(p.GetValue("RowState")),
                    }).ToList().CopyToDataTableNew();

                if (dtCNY00102InsertUpdate.Rows.Count > 0)
                {
                    _inf.InsertUpdateDetail(dtCNY00102InsertUpdate, version);
                }

                DataTable dtAttributeInsert = TableTempSaveAttribute();
                DataTable dtAttributeUpdate = TableTempSaveAttribute();

                var qAttributeSave = _dicAttValue.SelectMany(t => t.Value.Where(p => p.RowState != DataStatus.Unchange), (m, n) => new AttributeSaveFInfo
                {
                    PK = n.PK,
                    CNY00008PK_AttributePK = n.AttibutePK,
                    DetailPK = m.Key,
                    AttributeValue = n.AttibuteValueTemp,
                    AttributeUnit = ProcessGeneral.GetCodeInString(n.AttibuteUnit),
                    RowState = n.RowState,
                }).Distinct().ToList();


                foreach (AttributeSaveFInfo infoAtt in qAttributeSave)
                {
                    if (infoAtt.RowState == DataStatus.Insert)
                    {
                        dtAttributeInsert.Rows.Add(infoAtt.PK, infoAtt.DetailPK, infoAtt.CNY00008PK_AttributePK, infoAtt.AttributeValue, infoAtt.AttributeUnit);

                    }
                    else
                    {
                        dtAttributeUpdate.Rows.Add(infoAtt.PK, infoAtt.DetailPK, infoAtt.CNY00008PK_AttributePK, infoAtt.AttributeValue, infoAtt.AttributeUnit);
                    }

                }
                if (dtAttributeUpdate.Rows.Count > 0)
                {
                    _inf.UpdateDetailAttribute(dtAttributeUpdate, version);
                }
                if (dtAttributeInsert.Rows.Count > 0)
                {
                    _inf.InsertDetailAttribute(dtAttributeInsert, version);
                }


                //if (dt19Pk.Rows.Count > 0)
                //{
                //    dt19Pk = dt19Pk.AsEnumerable().Select(p => new {
                //        PK = p.Field<Int64>("PK")
                //    }).Distinct().CopyToDataTableNew();

                //}
                //_inf.InsUpdDelSOHeader(_prHeaderPk, dt19Pk);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }











        }




        private void DeleteDetailOnTree()
        {



            var q3 = _lDelAttributePk.Where(p => p > 0).Select(p => p.ToString()).Distinct().ToList();
            if (q3.Any())
            {
                string sQ3 = string.Join(",", q3);
                if (!string.IsNullOrEmpty(sQ3))
                {
                    string sqlQ3 = string.Format("DELETE FROM [dbo].[CNY00103] WHERE [PK] IN ({0})", sQ3);
                    _inf.BolExcuteSqlText(sqlQ3);
                    _lDelAttributePk.Clear();
                }
            }

            
            //var q4 = _lDelCNY00056PK.Where(p => p > 0).Select(p => p.ToString()).Distinct().ToList();
            //if (q4.Any())
            //{
            //    string sQ4 = string.Join(",", q4);
            //    if (!string.IsNullOrEmpty(sQ4))
            //    {
            //        string sqlQ4 = string.Format("DELETE FROM [dbo].[CNY00056] WHERE [PK] IN ({0})", sQ4);
            //        _inf.BolExcuteSqlText(sqlQ4);
            //        _lDelCNY00056PK.Clear();
            //    }
            //}


            var q5 = _lDelCNY00055PK.Where(p => p > 0).Select(p => p.ToString()).Distinct().ToList();
            if (q5.Any())
            {
                string sQ5 = string.Join(",", q5);
                if (!string.IsNullOrEmpty(sQ5))
                {
                    string sqlQ5 = string.Format("DELETE FROM [dbo].[CNY00102] WHERE [PK] IN ({0})", sQ5);
                    _inf.BolExcuteSqlText(sqlQ5);
                    _lDelCNY00055PK.Clear();
                }
            }




        }
        #endregion


        #region "Reversion Status"
        public bool RevisionStatus()
        {
            DialogResult dlResult = XtraMessageBox.Show("Do you want to revise??", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult != DialogResult.Yes) return false;

            txtVersion.EditValue = ProcessGeneral.GetSafeInt(txtVersion.EditValue) + 1;
            txtStatus.EditValue = @"1";

            //txtApprovedDate.EditValue = "";
            //txtApprovedBy.EditValue = @"N/A";
            //txtReleasedDate.EditValue = "";
            //txtReleasedBy.EditValue = @"N/A";

            return true;


        }
        #endregion






        #region "Generate Data"


        //private Dictionary<Int64, AttributeHeaderInfo> _dicAttribute = new Dictionary<Int64, AttributeHeaderInfo>();//PKAtt + Name= Code + Desc
        //private Dictionary<Int64, List<BoMInputAttInfo>> _dicAttValue = new Dictionary<Int64, List<BoMInputAttInfo>>(); // Key PKCHild


        public bool GenerateData(string productionOrderNo)
        {
            if (searchMROType.Text == "") // chưa chọn loại phiếu
            {
                XtraMessageBox.Show("Please choose MRO Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                xtraCC.SelectedTabPage = TabGeneral;
                searchMROType.Focus();
                return false;
            }
            if (searchMROType.Text != "2101") // Không lấy dữ liệu từ Work Order - Chỉ nhập tay
            {
                XtraMessageBox.Show(string.Format("With MRO Type: '{0}' - Please choose key 'Insert'", ProcessGeneral.GetSafeString(txtType.EditValue)), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return GenerateDataN(productionOrderNo);


        }


        private bool GenerateDataN(string productionOrderNo)
        {
            if (xtraCC.SelectedTabPageIndex != 1)
            {
                xtraCC.SelectedTabPageIndex = 1;
            }

            //DataTable dtSe = Ctrl_PrGenneral.TableTempBomsoSelectedNpr();

            //foreach (var itemColor in _dicTableColor)
            //{
            //    DataTable dtColor = itemColor.Value;
            //    var q1 = dtColor.AsEnumerable().Where(p =>
            //                                                    ProcessGeneral.GetSafeString(p["RowState"]) ==
            //                                                    DataStatus.Insert.ToString()).Select(p => new
            //                                                    {
            //                                                        CNY00020PK = ProcessGeneral.GetSafeInt64(p["CNY00020PK"]),
            //                                                        CNY00016PK = ProcessGeneral.GetSafeInt64(p["CNY00016PK"]),
            //                                                        PRQty_CNY002B = ProcessGeneral.GetSafeDouble(p["PRQty_CNY002B"]),
            //                                                    }).Where(p => p.PRQty_CNY002B > 0).GroupBy(p => new
            //                                                    {
            //                                                        p.CNY00020PK,
            //                                                        p.CNY00016PK
            //                                                    }).Select(t => new
            //                                                    {
            //                                                        t.Key.CNY00020PK,
            //                                                        t.Key.CNY00016PK,
            //                                                        PRQty_CNY002B = t.Sum(s => s.PRQty_CNY002B)
            //                                                    }).ToList();
            //    foreach (var item in q1)
            //    {

            //        DataRow dr = dtSe.NewRow();
            //        dr["CNY00020PK"] = item.CNY00020PK;
            //        dr["CNY00016PK"] = item.CNY00016PK;
            //        dr["PRQty_CNY002B"] = item.PRQty_CNY002B;
            //        dtSe.Rows.Add(dr);
            //    }
            //}

            //if (_dicTableColor.Count > 1 && dtSe.Rows.Count > 1)
            //{
            //    dtSe = dtSe.AsEnumerable().GroupBy(p => new
            //    {
            //        CNY00020PK = p.Field<Int64>("CNY00020PK"),
            //        CNY00016PK = p.Field<Int64>("CNY00016PK"),
            //    }).Select(t => new
            //    {
            //        t.Key.CNY00020PK,
            //        t.Key.CNY00016PK,
            //        PRQty_CNY002B = t.Sum(s => s.Field<double>("PRQty_CNY002B"))
            //    }).CopyToDataTableNew();
            //}


            TreeListNode node = null;
            string fieldName = "";
            bool isBestFit = true;
            if (tlMain.AllNodesCount > 0)
            {
                isBestFit = false;
                node = tlMain.FocusedNode;
                if (tlMain.FocusedColumn != null)
                {
                    fieldName = tlMain.FocusedColumn.FieldName;
                }

            }


            DataTable dtSourceTree;
            DataTable dtS = (DataTable)tlMain.DataSource;
            if (dtS == null)
            {
                dtSourceTree = Com_MaterialReleaseOrder.TableTreeviewTemplate();
            }
            else
            {
                dtSourceTree = dtS.Clone();
                if (dtS.Rows.Count > 0)
                {
                    foreach (DataRow drS in dtS.Rows)
                    {
                        dtSourceTree.ImportRow(drS);
                    }
                    dtSourceTree.AcceptChanges();
                }

            }

            bool isEvent = false;
            DataTable dtGenerate = new DataTable();
            var f = new Frm_ProductionOrderWizard(productionOrderNo)
            {
                //DtSelectedPr = dtSe,
                DicAttributeRoot = _dicAttribute,
                DicAttValueRoot = _dicAttValue,
                DtSourceTree = dtSourceTree,
                //EtaDate = dETA.DateTime,
                //EtdDate = dETD.DateTime,
                DicTableColorRoot = _dicTableColor,
                DicAttValueRmRoot = _dicAttValueRM,
                DicAttributeRMRoot = _dicAttributeRM,
            };
            f.OnSDRWizard += (s, e) =>
            {
                isEvent = e.IsEvent;
                dtGenerate = e.DtGenerate;
            };
            f.ShowDialog();
            if (isEvent)
            {
                GeneratePrResultN(dtGenerate, node, fieldName, isBestFit);
                EnableLookupType();
                return true;

            }

            return false;
        }

        private void GeneratePrResultN(DataTable dtGenerate, TreeListNode node, string fieldName, bool isBestFit)
        {
            _dlg = new WaitDialogForm();
            Int64 pkFind = 0;
            if (node != null)
            {
                pkFind = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
            }
            LoadDataTreeView(tlMain, dtGenerate, isBestFit);

            if (pkFind > 0)
            {
                TreeListNode nodeFinal = tlMain.FindNodeByKeyID(pkFind);
                string tFieldName = fieldName;
                if (string.IsNullOrEmpty(tFieldName))
                {
                    tFieldName = "ItemCode";
                }
                ProcessGeneral.SetFocusedCellOnTree(tlMain, nodeFinal, tFieldName);
            }
            _dlg.Close();


        }



        private bool GenerateDataS(string productionOrderNo)
        {
            if (xtraCC.SelectedTabPageIndex != 1)
            {
                xtraCC.SelectedTabPageIndex = 1;
            }

            //DataTable dtSe = Ctrl_PrGenneral.TableTempBOMSOSelectedPR();

            //foreach (var itemColor in _dicTableColor)
            //{
            //    DataTable dtColor = itemColor.Value;
            //    var q1 = dtColor.AsEnumerable().Where(p =>
            //                                                    ProcessGeneral.GetSafeString(p["RowState"]) ==
            //                                                    DataStatus.Insert.ToString()).Select(p => new
            //                                                    {
            //                                                        CNY00020PK = ProcessGeneral.GetSafeInt64(p["CNY00020PK"]),
            //                                                        CNY00016PK = ProcessGeneral.GetSafeInt64(p["CNY00016PK"]),
            //                                                        PlanQuantity = ProcessGeneral.GetSafeInt(p["PlanQuantity"]),
            //                                                    }).Where(p => p.PlanQuantity > 0).GroupBy(p => new
            //                                                    {
            //                                                        p.CNY00020PK,
            //                                                        p.CNY00016PK
            //                                                    }).Select(t => new
            //                                                    {
            //                                                        t.Key.CNY00020PK,
            //                                                        t.Key.CNY00016PK,
            //                                                        PlanQuantity = t.Sum(s => s.PlanQuantity)
            //                                                    }).ToList();
            //    foreach (var item in q1)
            //    {

            //        DataRow dr = dtSe.NewRow();
            //        dr["CNY00020PK"] = item.CNY00020PK;
            //        dr["CNY00016PK"] = item.CNY00016PK;
            //        dr["PlanQty"] = item.PlanQuantity;
            //        dtSe.Rows.Add(dr);
            //    }
            //}

            //if (_dicTableColor.Count > 1 && dtSe.Rows.Count > 1)
            //{
            //    dtSe = dtSe.AsEnumerable().GroupBy(p => new
            //    {
            //        CNY00020PK = p.Field<Int64>("CNY00020PK"),
            //        CNY00016PK = p.Field<Int64>("CNY00016PK"),
            //    }).Select(t => new
            //    {
            //        t.Key.CNY00020PK,
            //        t.Key.CNY00016PK,
            //        PlanQty = t.Sum(s => s.Field<Int32>("PlanQty"))
            //    }).CopyToDataTableNew();
            //}


            //TreeListNode node = null;
            //string fieldName = "";
            //bool isBestFit = true;
            //if (tlMain.AllNodesCount > 0)
            //{
            //    isBestFit = false;
            //    node = tlMain.FocusedNode;
            //    if (tlMain.FocusedColumn != null)
            //    {
            //        fieldName = tlMain.FocusedColumn.FieldName;
            //    }

            //}

            //DataTable dtSourceTree;
            //DataTable dtS = tlMain.DataSource as DataTable;
            //if (dtS == null)
            //{
            //    dtSourceTree = Com_MaterialReleaseOrder.TableTreeviewTemplate();
            //}
            //else
            //{
            //    dtSourceTree = dtS.Clone();
            //    if (dtS.Rows.Count > 0)
            //    {
            //        foreach (DataRow drS in dtS.Rows)
            //        {
            //            dtSourceTree.ImportRow(drS);
            //        }
            //        dtSourceTree.AcceptChanges();
            //    }

            //}

            //bool isEvent = false;
            //DataTable dtGenerate = new DataTable();
            //var f = new Frm002PRNewWizardTL(productionOrderNo)
            //{
            //    DtSelectedPr = dtSe,
            //    DicAttributeRoot = _dicAttribute,
            //    DicAttValueRoot = _dicAttValue,
            //    DtSourceTree = dtSourceTree,
            //    EtaDate = dETA.DateTime,
            //    EtdDate = dETD.DateTime,
            //    DicTableColorRoot = _dicTableColor,
            //    DicAttValueRmRoot = _dicAttValueRM,
            //    DicAttributeRMRoot = _dicAttributeRM,
            //};
            //f.OnPrWizard += (s, e) =>
            //{
            //    isEvent = e.IsEvent;
            //    dtGenerate = e.DtGenerate;
            //};
            //f.ShowDialog();
            //if (isEvent)
            //{
            //    GeneratePrResultS(dtGenerate, node, fieldName, isBestFit);
            //    EnableLookupType();
            //    return true;

            //}

            return false;
        }


        private void GeneratePrResultS(DataTable dtGenerate, TreeListNode node, string fieldName, bool isBestFit)
        {
            _dlg = new WaitDialogForm();
            Int64 pkFind = 0;
            if (node != null)
            {
                pkFind = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
            }
            LoadDataTreeView(tlMain, dtGenerate, isBestFit);

            if (pkFind > 0)
            {
                TreeListNode nodeFinal = tlMain.FindNodeByKeyID(pkFind);
                string tFieldName = fieldName;
                if (string.IsNullOrEmpty(tFieldName))
                {
                    tFieldName = "RMCode_001";
                }
                ProcessGeneral.SetFocusedCellOnTree(tlMain, nodeFinal, tFieldName);
            }
            _dlg.Close();


        }



        #endregion


        #region "GroupBy Data"


        //private Dictionary<Int64, AttributeHeaderInfo> _dicAttribute = new Dictionary<Int64, AttributeHeaderInfo>();//PKAtt + Name= Code + Desc
        //private Dictionary<Int64, List<BoMInputAttInfo>> _dicAttValue = new Dictionary<Int64, List<BoMInputAttInfo>>(); // Key PKCHild
        public void GroupByItemCode()
        {
            //if (xtraCC.SelectedTabPageIndex != 1)
            //{
            //    xtraCC.SelectedTabPageIndex = 1;
            //}
            //var qCol = tlMain.VisibleColumns.Select(p => new
            //{
            //    Tag = ProcessGeneral.GetSafeInt64(p.Tag),
            //    p.FieldName,
            //    Index = p.VisibleIndex
            //}).Where(p => p.Tag > 0).Where(p =>
            //    _dicAttribute.Any(t => t.Key == p.Tag && t.Value.DataType == DataAttType.Number)).Select(p =>
            //    new CalDimensionInfoFinal
            //    {
            //        Index = p.Index,
            //        Tag = p.Tag,
            //        FieldName = p.FieldName,
            //        AttName = GetAttName(p.FieldName),

            //    }).OrderBy(p => p.Index).ToList();
            //int loop = 0;
            //while (true)
            //{

               
            //    DataTable dtS = tlMain.DataSource as DataTable;
            //    if (dtS == null) return;

            //    var q1 = dtS.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted && p.Field<Int64>("ParentPK") <= 0 && p.Field<bool>("AllowUpdate") && !p.Field<bool>("IsHasChild")).ToList();
            //    if (q1.Count <= 1) return;


               
            //    var q2 = q1.AsEnumerable().GroupBy(p => new
            //    {
               
            //        TDG00001PK = p.Field<Int64>("TDG00001PK"),
            //        CNY00050PK = p.Field<Int64>("CNY00050PK"),
            //        CNY00002PK = p.Field<Int64>("CNY00002PK"),
            //        CNY00004PK = p.Field<Int64>("CNY00004PK"),
            //        UnitCode_CNY011 = p.Field<String>("UnitCode_CNY011"),
            //        RMCode_001 = p.Field<string>("RMCode_001"),
            //        RMDescription_002 = p.Field<string>("RMDescription_002"),
            //        Color = p.Field<string>("Color"),
            //        Supplier = p.Field<string>("Supplier"),
            //        Unit = p.Field<string>("Unit"),
            //        Purchaser = p.Field<string>("Purchaser"),
            //        TDG00004PK = p.Field<Int64>("TDG00004PK"),
            //        SupplierRef = p.Field<String>("SupplierRef"),

            //    }).Where(s=>s.Count()>1).Select(s => new
            //    {
            //        s.Key.TDG00001PK,
            //        s.Key.RMCode_001,
            //        s.Key.RMDescription_002,
            //        s.Key.Color,
            //        s.Key.Supplier,
            //        s.Key.SupplierRef,
            //        s.Key.Unit,
            //        s.Key.Purchaser,
            //        s.Key.CNY00050PK,
            //        s.Key.CNY00002PK,
            //        s.Key.CNY00004PK,
            //        s.Key.UnitCode_CNY011,
            //        s.Key.TDG00004PK,
            //        ChildPK = string.Join(",", s.Select(t => t.Field<Int64>("ChildPK").ToString()).ToArray()),
            //        ParentPK = "",
            //    }).OrderBy(s => s.RMCode_001).ToList();
            //    if (!q2.Any()) return;
           



            //    if (loop > 0)
            //    {
            //        DialogResult dlResult = XtraMessageBox.Show("Do you want to perform function group by item code ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (dlResult != DialogResult.Yes) return;
            //    }
            //    DataTable dtStep1 = Ctrl_PrGenneral.TableStep1GroupTemplate();

            //    int w = 0;
            //    foreach (var itemQ2 in q2)
            //    {
            //        DataRow drStep1 = dtStep1.NewRow();
            //        drStep1["Selected"] = w == 0;
            //        drStep1["TDG00001PK"] = itemQ2.TDG00001PK;
            //        drStep1["RMCode_001"] = itemQ2.RMCode_001;
            //        drStep1["RMDescription_002"] = itemQ2.RMDescription_002;
            //        drStep1["Color"] = itemQ2.Color;
            //        drStep1["Supplier"] = itemQ2.Supplier;
            //        drStep1["Unit"] = itemQ2.Unit;
            //        drStep1["Purchaser"] = itemQ2.Purchaser;
            //        drStep1["CNY00050PK"] = itemQ2.CNY00050PK;
            //        drStep1["CNY00002PK"] = itemQ2.CNY00002PK;
            //        drStep1["CNY00004PK"] = itemQ2.CNY00004PK;
            //        drStep1["UnitCode_CNY011"] = itemQ2.UnitCode_CNY011;
            //        drStep1["ChildPK"] = itemQ2.ChildPK;
            //        drStep1["ParentPK"] = itemQ2.ParentPK;
            //        drStep1["TDG00004PK"] = itemQ2.TDG00004PK;
            //        drStep1["SupplierRef"] = itemQ2.SupplierRef;
            //        dtStep1.Rows.Add(drStep1);
            //        w++;
            //    }



              

             
            //    //
            //    bool isEvent = false;
            //    List<PrGroupByInfomation> lPrGroupBy = new List<PrGroupByInfomation>();
            //   // List<BoMInputAttInfo> lPacking = new List<BoMInputAttInfo>();
            //    var f = new Frm002PRNewGroupByItem(_dtUnit)
            //    {
            //        DicAttributeRoot = _dicAttribute,
            //        DicAttValueRoot = _dicAttValue,
            //        DtSourceTree = q1.CopyToDataTable(),
            //        DtStep1 = dtStep1,
            //        DicTableColorRoot =_dicTableColor,
            //        DicAttValueRmRoot = _dicAttValueRM,
            //        DicAttributeRMRoot = _dicAttributeRM
            //    };
            //    f.OnPrWizard += (s, e) =>
            //    {
            //        isEvent = e.IsEvent;
            //        lPrGroupBy = e.LGroupBy;
            //      //  lPacking = e.LPacking;
            //    };
            //    f.ShowDialog();
            //    if (!isEvent) return;

            //    GroupByPrResult(tlMain, lPrGroupBy, qCol);
            //    loop++;
            //}
        

        }
        private void BtnConvert_Click(object sender, EventArgs e)
        {
            TreeListNode node = tlMain.FocusedNode;
            if (node == null) return;
            if (!ProcessGeneral.GetSafeBool(node.GetValue("AllowUpdate"))) return;
            if (ProcessGeneral.GetSafeBool(node.GetValue("IsGrouping"))) return;

            DataTable dtColor = gcColor.DataSource as DataTable;
            if (dtColor == null) return;
  



            string  unitDescPur = ProcessGeneral.GetSafeString(node.GetValue("Unit"));
            string  unitDescBoM = ProcessGeneral.GetSafeString(node.GetValue("UnitB"));
            string unitCodePur = ProcessGeneral.GetSafeString(node.GetValue("UnitCode_CNY011"));
            string unitCodeBoM = ProcessGeneral.GetSafeString(node.GetValue("UnitCode_CNY011B"));
       
            DialogResult dlResult;
            bool isBoM;
            if (unitCodePur == unitCodeBoM)
            {
                dlResult = XtraMessageBox.Show("Do you want to convert BoM Unit to Purchase Unit", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                isBoM = false;
            }
            else
            {
                dlResult = XtraMessageBox.Show(string.Format("Do you want to convert Purchase Unit ({0}) to BoM Unit ({1})", unitDescPur, unitDescBoM), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                isBoM = true;
            }
            if (dlResult != DialogResult.Yes) return;
            Int64 childPk = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
            if (isBoM)
            {
                double factor = ProcessGeneral.GetSafeDouble(node.GetValue("UnitCode_CNY011B"));
                if (factor == 1) return;
            
                node.SetValue("Unit", unitDescBoM);
                node.SetValue("UnitCode_CNY011", unitCodeBoM);
                node.SetValue("PRQty_CNY002", node.GetValue("PRQty_CNY002B"));
                node.SetValue("POQty_CNY003", node.GetValue("POQty_CNY003B"));
                node.SetValue("PackingFactor", 1);
                CalEditValueChangedEvent(node);


                foreach (DataRow drColor in dtColor.Rows)
                {
                    drColor["PRQty_CNY002"] = drColor["PRQty_CNY002B"];
                    drColor["POQty_CNY003"] = drColor["POQty_CNY003B"];
                    string rowState = ProcessGeneral.GetSafeString(drColor["RowState"]);
                    if (rowState == DataStatus.Unchange.ToString())
                    {
                        drColor["RowState"] = DataStatus.Update.ToString();
                    }
                }
                   
                dtColor.AcceptChanges();
                _dicTableColor[childPk] = dtColor;
                return;
            }
            _dlg = new WaitDialogForm();
            DataTable dtGf = Com_MaterialReleaseOrder.TablePackingFactorAttTemplate();
            List<BoMInputAttInfo> lInfo;
            if (_dicAttValue.TryGetValue(childPk, out lInfo))
            {
                foreach (BoMInputAttInfo itemInfo in lInfo)
                {
                    dtGf.Rows.Add(itemInfo.AttibutePK, itemInfo.AttibuteValueTemp, ProcessGeneral.GetCodeInString(itemInfo.AttibuteUnit));
                }
            }

            Int64 pkCode = ProcessGeneral.GetSafeInt64(node.GetValue("TDG00001PK"));
            DataTable dtPac = _inf.GetFactorAttConvert(pkCode, dtGf, unitCodeBoM);

         

            if (dtPac.Rows.Count <= 0)
            {
                _dlg.Close();
                XtraMessageBox.Show("No data convert.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //DataRow drPac = Ctrl_PrGenneral.GetPackingInfoConvert(dtPac);
            //double packingFactor = ProcessGeneral.GetSafeDouble(drPac["Factor"]);
            //string packUnitCode = ProcessGeneral.GetSafeString(drPac["UnitCode"]);
            //string packingUnitDesc = ProcessGeneral.GetSafeString(drPac["UnitName"]);



           
            //if (packingFactor != 1)
            //{
            //    double prQty = 0;
            //    double poQty = 0;
            //    foreach (DataRow drColor in dtColor.Rows)
            //    {






            //        double prQtyBTemp = ProcessGeneral.GetSafeDouble(drColor["PRQty_CNY002B"]);
            //        double poQtyBTemp = ProcessGeneral.GetSafeDouble(drColor["POQty_CNY003B"]);

            //        double prQtyTemp = Math.Round(prQtyBTemp * packingFactor, ConstSystem.FormatPrQtyDecimal);
            //        double poQtyTemp = Math.Round(poQtyBTemp * packingFactor, ConstSystem.FormatPoQtyDecimal);

            //        prQty += prQtyTemp;
            //        poQty += poQtyTemp;




            //        drColor["PRQty_CNY002"] = prQtyTemp;
            //        drColor["POQty_CNY003"] = poQtyTemp;


            //        string rowStateTemp = ProcessGeneral.GetSafeString(drColor["RowState"]);
            //        if (rowStateTemp == DataStatus.Unchange.ToString())
            //        {
            //            drColor["RowState"] = DataStatus.Update.ToString();
            //        }



            //    }

            //    node.SetValue("PRQty_CNY002", prQty);
            //    node.SetValue("POQty_CNY003", poQty);
            //}
         

           

            



            //node.SetValue("Unit", packingUnitDesc);
            //node.SetValue("UnitCode_CNY011", packUnitCode);
        
            //node.SetValue("PackingFactor", packingFactor);


            //node.SetValue("IsPacking", true);
            CalEditValueChangedEvent(node);



            _dlg.Close();
        }

//        private void GroupByPrResult(TreeList tl, List<PrGroupByInfomation> lPrGroupBy,List<CalDimensionInfoFinal> qCol)
//        {   
//            _dlg = new WaitDialogForm();
//            tl.LockReloadNodes();
//            TreeListNode nodeFinal = null;

//            foreach (PrGroupByInfomation itemGb in lPrGroupBy)
//            {






//                Dictionary<Int32, Int64> lChildPk = itemGb.LChildPk;

//                List<PrTreeGroupingInfo> lNode = tl.Nodes.Join(lChildPk, p => new
//                {
//                    ChildPK = ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK"))
//                }, t => new {
//                    ChildPK = t.Value
//                }, (m, n) => new PrTreeGroupingInfo
//                {
//                    TNode = m,
//                    ChildPK = n.Value,
//                    RowState = ProcessGeneral.GetSafeString(m.GetValue("RowState")),
//                    SortIndex = n.Key,
//                    DifUC = ProcessGeneral.GetSafeBool(m.GetValue("DifUC")) ? 1 : 0
//                }).OrderByDescending(p=>p.DifUC).ThenBy(p => p.SortIndex).ToList();

//                //ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK")
              
            
//                List<BoMInputAttInfo> lBoMInput = itemGb.LBoMInput;
       
//                PrTreeGroupingInfo parentInfo = lNode[0];
//                TreeListNode parentNode = parentInfo.TNode;
//                Int64 pkFind = parentInfo.ChildPK;

//                nodeFinal = parentNode;

        

//                DataTable dtColRoot = itemGb.TableColor;
//                //string [] arrColDelColor = dtColRoot.Columns.Cast<DataColumn>().Where(p => p.ColumnName.IndexOf("-", StringComparison.Ordinal)>0).Select(p => p.ColumnName).ToArray();
//                //if (arrColDelColor.Length > 0)
//                //{
//                //    foreach (string colDel in arrColDelColor)
//                //    {
//                //        dtColRoot.Columns.Remove(colDel);
//                //    }
//                //    dtColRoot.AcceptChanges();
//                //}
////              


//                foreach (DataRow drCol in dtColRoot.Rows)
//                {
//                    Int64 parPk = ProcessGeneral.GetSafeInt64(drCol["ParentPK"]);
//                    if (pkFind != parPk)
//                    {
//                        drCol["ParentPK"] = pkFind;
//                    }
                
//                }


//                for (int i = 1; i < lNode.Count; i++)
//                {
//                    PrTreeGroupingInfo childInfo = lNode[i];
                  
//                    Int64 pkDelT = childInfo.ChildPK;
//                    if (_dicTableColor.ContainsKey(pkDelT))
//                    {
                        
//                        _dicTableColor.Remove(pkDelT);
//                    }
//                    TreeListNode nodeLoop = childInfo.TNode;

                
//                    if (_dicAttValue.ContainsKey(pkDelT))
//                    {
//                        _dicAttValue.Remove(pkDelT);
//                    }

//                    if (childInfo.RowState != DataStatus.Insert.ToString())
//                    {
//                        _lDelCNY00055PK.Add(pkDelT);

//                    }
//                    tl.DeleteNode(nodeLoop);


//                }

               
            

               
                
//                dtColRoot.AcceptChanges();
               
                




//                if (parentInfo.RowState == DataStatus.Unchange.ToString())
//                {
//                    parentNode.SetValue("RowState", DataStatus.Update.ToString());
                 
//                }
             
//                parentNode.SetValue("PRQty_CNY002", itemGb.PRQty_CNY002);
//                parentNode.SetValue("POQty_CNY003", itemGb.POQty_CNY003);

//                parentNode.SetValue("PRQty_CNY002B", itemGb.PRQty_CNY002B);
//                parentNode.SetValue("POQty_CNY003B", itemGb.POQty_CNY003B);

//                parentNode.SetValue("UnitCode_CNY011B", itemGb.UnitCode_CNY011B);
//                parentNode.SetValue("UnitB", itemGb.UnitB);
//                parentNode.SetValue("UnitCode_CNY011", itemGb.UnitCode_CNY011);
//                parentNode.SetValue("Unit", itemGb.Unit);
//                parentNode.SetValue("PackingFactor", itemGb.PackingFactor);
//                parentNode.SetValue("IsGrouping", true);
//                parentNode.SetValue("IsPacking", itemGb.IsPacking);


//                List<BoMInputAttInfo> lBomInfo;
//                if (_dicAttValue.TryGetValue(pkFind, out lBomInfo))
//                {
//                    foreach (BoMInputAttInfo infoCheck in lBomInfo)
//                    {
//                        parentNode.SetValue(string.Format("{0}-{1}", infoCheck.AttibuteCode, infoCheck.AttibuteName), "");
                 

//                        if (infoCheck.RowState != DataStatus.Insert)
//                        {
//                            _lDelAttributePk.Add(infoCheck.PK);
//                        }
//                    }
//                    _dicAttValue.Remove(pkFind);
//                }
//                _dicAttValue.Add(pkFind, lBoMInput);

//                foreach (BoMInputAttInfo infoSet in lBoMInput)
//                {
//                    parentNode.SetValue(string.Format("{0}-{1}", infoSet.AttibuteCode, infoSet.AttibuteName), infoSet.AttibuteValueFull);

//                }



//                string dimension = CalDimension(parentNode, qCol);
//                parentNode.SetValue("Dimension", dimension);


//                if (!_dicTableColor.ContainsKey(pkFind))
//                {
//                    _dicTableColor.Add(pkFind, dtColRoot);
//                }
//                else
//                {
//                    _dicTableColor[pkFind] = dtColRoot;
//                }
               

//            }
            
//            if (nodeFinal != null)
//            {
//                if (!tl.Focused)
//                {
//                    tl.Focus();
//                }
//                tl.Selection.Clear();

//                tl.SetFocusedCellTreeList(nodeFinal, tl.VisibleColumns[0]);
//            }
           
//            tl.UnlockReloadNodes();
     
//            _dlg.Close();


//            //if (lPacking.Count <= 0) return;
//            //DialogResult dlResult = XtraMessageBox.Show("Do you want to perform function allocating demand quantity for each method packaging of item?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//            //if (dlResult != DialogResult.Yes) return;
//            //AllocatingItemCode(lNodeDist);

//        }




        #endregion









        #region "Process Grid Color"

        private void ClearDataSourceColor()
        {
            gvColor.BeginUpdate();





            gcColor.DataSource = null;
            gvColor.Columns.Clear();
            gvColor.EndUpdate();
        }
        private void LoadDataGridViewColorFrist(DataTable dt)
        {

            //gvColor.BeginUpdate();




            
            //gcColor.DataSource = null;
            //gvColor.Columns.Clear();

            //gcColor.DataSource = dt;


        

         
          


            //Dictionary<string, bool> dicCol = new Dictionary<string, bool>
            //{
            //    {"Reference", true},
            //    {"PRQty_CNY002", true},
            //    {"POQty_CNY003", true},
            //    { "PRQty_CNY002B", false},
            //    {"POQty_CNY003B", false},
            //    {"PlanQuantity", true},
            //    {"UC", true},
            //    {"Factor", true},
            //    {"TotalQuantity", true},
            //    {"Position", true},
            //    { "AssemblyComponent", true},
            //    {"RmDimension", false},
            //};


            //var q1 = dt.Columns.Cast<DataColumn>().Where(p => p.ColumnName.IndexOf("-", StringComparison.Ordinal) > 0).Select(p => p.ColumnName).ToList();
            //var q2 = _dicAttributeRM.Select(p => new
            //{
            //    p.Value.AttibuteName,
            //    p.Value.ColumnIndex,
            //    p.Key,
            //    p.Value.DataType
            //}).Where(p => q1.Any(t => t == p.AttibuteName)).OrderBy(p => p.ColumnIndex).ToList();


            //foreach (var item2 in q2)
            //{

            //    dicCol.Add(item2.AttibuteName, true);
            //}



            //dicCol.Add("Tolerance", true);
            //dicCol.Add("PercentUsing", true);
            //dicCol.Add("RootSOQty", true);
            //dicCol.Add("RMDescription_002", true);
            //dicCol.Add("ProductionOrder", true);
            //dicCol.Add("ProjectName", true);
            //dicCol.Add("ProDimension", true);
            //dicCol.Add("CNY00020PK", false);
            //dicCol.Add("RMCode_001", false);
            //dicCol.Add("TDG00001PK", false);
            //dicCol.Add("CNY00016PK", false);
            //dicCol.Add("ChildPK", false);
            //dicCol.Add("ParentPK", false);
            //dicCol.Add("RowState", false);
            //dicCol.Add("AllowUpdate", false);
            //dicCol.Add("ToleranceBOM", false);
            //dicCol.Add("CNY00019PK", false);
            //dicCol.Add("UC_BOM", false);
            //gvColor.VisibleAndSortGridColumn(dicCol);


            //foreach (var item in q2)
            //{
            //    string s = item.AttibuteName;

            //    GridColumn tColS = gvColor.Columns[s];
            //    if (tColS == null) continue;


            //    string caption = ProcessGeneral.GetDescriptionInString(s, "-");


            //    if (item.DataType == DataAttType.Number)
            //    {
            //        string caption1 = string.Format("({0})", ConstSystem.BoMDefaultUnitDesc);
            //        if (caption1.Length > caption.Length)
            //        {
            //            caption = caption1;

            //        }
            //        ProcessGeneral.SetGridColumnHeader(tColS, caption, DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None, item.Key);
            //        tColS.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //        tColS.DisplayFormat.FormatType = FormatType.Numeric;
            //        tColS.DisplayFormat.FormatString = "#,0.##";
            //    }
            //    else
            //    {
            //        ProcessGeneral.SetGridColumnHeader(tColS, caption, DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None, item.Key);
                    
            //        tColS.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //    }
            //    tColS.ImageIndex = 0;
            //    tColS.ImageAlignment = StringAlignment.Near;
            //}


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PlanQuantity"], "Pro. Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["PlanQuantity"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            //gvColor.Columns["PlanQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["PlanQuantity"].DisplayFormat.FormatString = "N0";
            //gvColor.Columns["PlanQuantity"].ImageIndex = 0;
            //gvColor.Columns["PlanQuantity"].ImageAlignment = StringAlignment.Near;
            ////gvColor.Columns["PlanQuantity"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvColor.Columns["PlanQuantity"].SummaryItem.DisplayFormat = @"{0:N0}";
            


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["UC"], "Amount", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["UC"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["UC"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatUcDecimal(false, false);

            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Tolerance"], "Waste (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["Tolerance"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["Tolerance"].DisplayFormat.FormatString = "#,0.#####";

            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PercentUsing"], "Using (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["PercentUsing"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["PercentUsing"].DisplayFormat.FormatString = "#,0.#####";


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Factor"], "Quantity", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["Factor"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["Factor"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatFactorDecimal(false, false);


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["TotalQuantity"], "Total Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["TotalQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["TotalQuantity"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatFactorDecimal(false, false);


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PRQty_CNY002"], "Demand Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["PRQty_CNY002"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["PRQty_CNY002"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            ////gvColor.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvColor.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["POQty_CNY003"], "Purchase Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["POQty_CNY003"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["POQty_CNY003"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
            ////gvColor.Columns["POQty_CNY003"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvColor.Columns["POQty_CNY003"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PRQty_CNY002B"], "Demand Qty  (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["PRQty_CNY002B"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["PRQty_CNY002B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            ////gvColor.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvColor.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["POQty_CNY003B"], "Purchase Qty (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["POQty_CNY003B"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["POQty_CNY003B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
            ////gvColor.Columns["POQty_CNY003"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvColor.Columns["POQty_CNY003"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RmDimension"], "Dimension (RM)", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ProDimension"], "Dimension (PRO.)", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ProjectName"], "Project Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);


            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ProductionOrder"], "Production Order", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RootSOQty"], "Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvColor.Columns["RootSOQty"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvColor.Columns["RootSOQty"].DisplayFormat.FormatString = "N0";
            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Position"], "Position", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Reference"], "Reference", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            ////gvColor.Columns["Reference"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvColor.Columns["Reference"].SummaryItem.DisplayFormat = @"Total:";

            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["AssemblyComponent"], "Ass - Comp", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);

            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RMCode_001"], "Item Code", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
        
            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RMDescription_002"], "Item Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
          

        








            ////ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ColorCode"], "Color", DefaultBoolean.False, HorzAlignment.Near,
            ////    GridFixedCol.None);


            ////var qColAtt = gvColor.Columns.Where(p => p.FieldName.IndexOf("-", StringComparison.Ordinal) > 0).ToList();

            ////foreach (GridColumn gColD in qColAtt)
            ////{
               
            ////    ProcessGeneral.SetGridColumnHeader(gColD, gColD.Caption, DefaultBoolean.False, HorzAlignment.Near,
            ////        GridFixedCol.None);
            ////    gColD.ImageIndex = 0;
            ////    gColD.ImageAlignment = StringAlignment.Near;
            ////}
            
            //gvColor.BestFitColumns();



            //foreach (var item1 in q2)
            //{
            //    string s1 = item1.AttibuteName;
            //    GridColumn tColS1 = gvColor.Columns[s1];
            //    if (tColS1 == null) continue;
            //    string caption1 = ProcessGeneral.GetDescriptionInString(s1, "-");
            //    if (item1.DataType == DataAttType.Number)
            //    {
            //        caption1 = string.Format("{0}<br>({1})", caption1, ConstSystem.BoMDefaultUnitDesc);
            //    }
            //    tColS1.Caption = caption1;


            //}



            //gvColor.EndUpdate();


        }



        private void GridViewColorCustomInit()
        {



            gcColor.UseEmbeddedNavigator = true;

            gcColor.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvColor.OptionsBehavior.Editable = true;
            gvColor.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvColor.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvColor.OptionsCustomization.AllowColumnMoving = false;
            gvColor.OptionsCustomization.AllowQuickHideColumns = true;

            gvColor.OptionsCustomization.AllowSort = false;

            gvColor.OptionsCustomization.AllowFilter = false;
            gvColor.OptionsView.AllowHtmlDrawHeaders = true;

            gvColor.OptionsView.ShowGroupPanel = false;
            gvColor.OptionsView.ShowIndicator = true;
            gvColor.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvColor.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvColor.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvColor.OptionsView.ShowAutoFilterRow = false;
            gvColor.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvColor.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvColor.OptionsNavigation.AutoFocusNewRow = true;
            gvColor.OptionsNavigation.UseTabKey = true;

            gvColor.OptionsSelection.MultiSelect = true;
            gvColor.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvColor.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvColor.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvColor.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvColor.OptionsView.EnableAppearanceEvenRow = false;
            gvColor.OptionsView.EnableAppearanceOddRow = false;
            gvColor.OptionsView.ShowFooter = false;
            gvColor.OptionsView.RowAutoHeight = true;
            gvColor.OptionsHint.ShowFooterHints = false;
            gvColor.OptionsHint.ShowCellHints = true;
            //   gridView1.RowHeight = 25;

            gvColor.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvColor.OptionsFind.AllowFindPanel = false;

            gvColor.OptionsView.BestFitMaxRowCount = 10;
            gvColor.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
           
            new MyFindPanelFilterHelper(gvColor)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };




            gvColor.ShowingEditor += gvColor_ShowingEditor;
            gvColor.RowCountChanged += gvColor_RowCountChanged;
            gvColor.CustomDrawRowIndicator += gvColor_CustomDrawRowIndicator;

            gvColor.RowCellStyle += gvColor_RowCellStyle;

            gvColor.LeftCoordChanged += gvColor_LeftCoordChanged;
            gvColor.MouseMove += gvColor_MouseMove;
            gvColor.TopRowChanged += gvColor_TopRowChanged;
            gvColor.FocusedColumnChanged += gvColor_FocusedColumnChanged;
            gvColor.FocusedRowChanged += gvColor_FocusedRowChanged;
            gcColor.Paint += gcColor_Paint;
            gcColor.EditorKeyDown += gcColor_EditorKeyDown;
            gcColor.KeyDown += gcColor_KeyDown;
            gvColor.CellValueChanged += GvColor_CellValueChanged;

            gvColor.CustomRowCellEdit += GvColor_CustomRowCellEdit;

            gcColor.ForceInitialize();



        }





        #region "GridView Event"


        private void GvColor_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "POQty_CNY003":
                    e.RepositoryItem = _repositorySpinPoQty;
                    break;
                case "Tolerance":
                    e.RepositoryItem = _repositorySpinWaste;
                    break;
            }
        }

        private void gvColor_ShowingEditor(object sender, CancelEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;
            TreeListNode node = tlMain.FocusedNode;
            if (node == null)
            {
                e.Cancel = true;
                return;
            }


            int rH = gv.FocusedRowHandle;
            if (rH < 0) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;

            bool allowUpdate = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rH, "AllowUpdate"));
            string fieldName = gCol.FieldName;
            switch (fieldName)
            {
                case "POQty_CNY003":
                    e.Cancel = !allowUpdate;
                    break;
                case "Tolerance":
                    if (!allowUpdate)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        
                        e.Cancel = !ProcessGeneral.GetSafeBool(node.GetValue("AllowChangeWaste"));
                    }
                
                    break;
                default:
                    e.Cancel = true;
                    break;
            }

        }


        private void gvColor_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            int visibleIndex = gCol.VisibleIndex;
            if (visibleIndex < 0) return;
            string fieldName = gCol.FieldName;



            if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(rH, gCol))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }





            switch (fieldName)
            {
                case "PRQty_CNY002":
                case "PRQty_CNY002B":
                {


                    e.Appearance.ForeColor = Color.DarkOrange;
                }
                    break;
                case "POQty_CNY003":
                case "POQty_CNY003B":
                {

                    e.Appearance.ForeColor = Color.DarkRed;
                }
                    break;
                case "RootSOQty":
                {

                    e.Appearance.ForeColor = Color.DarkBlue;
                }
                    break;
                case "PlanQuantity":
                {

                    e.Appearance.ForeColor = Color.DarkGreen;
                }
                    break;
                case "Position":
                {
                    e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkGreen;
                }
                    break;

            }




            // bool allowUpdate = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rH,"AllowUpdate"));
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "RowState"));


            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {
                if (rowState == DataStatus.Insert.ToString() && ProcessGeneral.GetSafeString(e.CellValue) != "")
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.GreenYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
            }
            else
            {



               

                switch (fieldName)
                {
                    case "Tolerance":
                    {
                        if (Equals(e.CellValue, gv.GetRowCellValue(rH, "ToleranceBOM")))
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.WhiteSmoke;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.LightSalmon;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                        break;
                    case "UC":
                    {
                        if (Equals(e.CellValue, gv.GetRowCellValue(rH, "UC_BOM")))
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.WhiteSmoke;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.LightSalmon;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                        break;

                    case "TotalQuantity":
                    case "Factor":
                    
                    case "PercentUsing":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.WhiteSmoke;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                        break;
                    case "RMCode_001":
                    case "RMDescription_002":
                    case "Reference":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                        break;
                    case "ProjectName":
                    case "AssemblyComponent":
                    case "ProductionOrder":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                        break;
                    case "PRQty_CNY002":
                    case "PRQty_CNY002B":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Cornsilk;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    case "POQty_CNY003":
                    case "POQty_CNY003B":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightPink;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    case "PlanQuantity":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.PaleGoldenrod;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    case "RootSOQty":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Khaki;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                        break;
                    case "Position":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.PaleGoldenrod;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                        break;
                    case "RmDimension":
                    {
                        if (rowState == DataStatus.Insert.ToString() && ProcessGeneral.GetSafeString(e.CellValue) != "")
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.GreenYellow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.LightGoldenrodYellow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                        break;
                    case "ProDimension":
                    {
                        if (rowState == DataStatus.Insert.ToString() && ProcessGeneral.GetSafeString(e.CellValue) != "")
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.GreenYellow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.LightYellow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                        break;

                }

            }









        }

        private void gvColor_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvColor_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvColor_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvColor_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvColor_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcColor_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvColor_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvColor_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
            bool allowUpdate = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "AllowUpdate"));
        




            

            if (!allowUpdate)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Yellow, Color.Azure, 90);
            }
            else
            {
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


    


        private void gcColor_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDown)
            {
                gcColor_KeyDown(sender, e);
            }
            _checkKeyDown = false;
        }


        private void gcColor_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            if (gc == null) return;
            var gv = gc.FocusedView as GridView;
            if (gv == null) return;
           // GridColumn gColF = gv.FocusedColumn;
            //string fieldName = gColF.FieldName;
            //int visibleIndex = gColF.VisibleIndex;
            //int rH = gv.FocusedRowHandle;
            _checkKeyDown = true;







            #region "Process Ctrl+D key"

            if (e.KeyData == (Keys.Control | Keys.D))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                ProcessControlDkeyColor(gv);
                return;
            }

            #endregion



            #region "Process F8 Key"

            if (e.KeyCode == Keys.F8)
            {



                DeleteSoLine(gv);
                EnableLookupType();
                return;
            }





            #endregion





        }

        private void ProcessControlDkeyColor(GridView gv)
        {
            TreeListNode node = tlMain.FocusedNode;
            if (node == null) return;


            if (!ProcessGeneral.GetSafeBool(node.GetValue("AllowChangeWaste"))) return;

            int []arrRow = gv.GetSelectedRows();
            if (arrRow.Length <= 1) return;

            string[] arrField = { "Tolerance" }; 

            var q1 = gv.GetSelectedCells().Select(p=>p.Column.FieldName).Distinct().ToList();

           


            var qField = q1.Where(p => arrField.Any(t => t == p)).ToList();
            if (!qField.Any()) return;


            bool reCal = false;
            _isEditColor = false;

            int rH0 = arrRow[0];
            double waste = ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH0, "Tolerance"));
            double packingFactor = ProcessGeneral.GetSafeDouble(node.GetValue("PackingFactor"));
            for (int i = 1; i < arrRow.Length; i++)
            {
                int rHi = arrRow[i];
                if(!ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rHi, "AllowUpdate")))continue;
                gv.SetRowCellValue(rHi, "Tolerance", waste);


                double prQtyCny002B = Math.Round(ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rHi, "UC")) * ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rHi, "PlanQuantity")) * (1 + waste / 100) *
                                                 ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rHi, "PercentUsing")) / 100, ConstSystem.FormatPrQtyDecimal);


                double poQtyCny003B = Math.Round(ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rHi, "UC")) * ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rHi, "PlanQuantity")) * (1 + waste / 100) *
                                                 ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rHi, "PercentUsing")) / 100, ConstSystem.FormatPoQtyDecimal);
   

                gv.SetRowCellValue(rHi, "POQty_CNY003B", poQtyCny003B);
                gv.SetRowCellValue(rHi, "PRQty_CNY002B", prQtyCny002B);
                gv.SetRowCellValue(rHi, "POQty_CNY003", Math.Round(poQtyCny003B * packingFactor, ConstSystem.FormatPoQtyDecimal));
                gv.SetRowCellValue(rHi, "PRQty_CNY002", Math.Round(prQtyCny002B * packingFactor, ConstSystem.FormatPrQtyDecimal));

                string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rHi, "RowState"));
                if (rowState == DataStatus.Unchange.ToString())
                {
                    gv.SetRowCellValue(rHi, "RowState", DataStatus.Update.ToString());

                }
                reCal = true;
            }

            _isEditColor = true;





            if (reCal)
            {
                ReCalDataColorGrid((DataTable)gcColor.DataSource);
            }


            
          
        }

        private void DeleteSoLine(GridView gv)
        {
            List<DataRow> lRow = gv.GetSelectedRows().Where(p=>ProcessGeneral.GetSafeBool(gv.GetRowCellValue(p, "AllowUpdate"))).Select(gv.GetDataRow).ToList();
            if (lRow.Count <= 0) return;

            DialogResult dlResult = XtraMessageBox.Show("Do you want to delete this records selected ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlResult != DialogResult.Yes) return;

            DataTable dtS = gcColor.DataSource as DataTable;
            if (dtS == null) return;
            foreach (DataRow dr in lRow)
            {
                Int64 pkChildC = ProcessGeneral.GetSafeInt64(dr["ChildPK"]);
                string rowStateC = ProcessGeneral.GetSafeString(dr["RowState"]);
                if (rowStateC != DataStatus.Insert.ToString())
                {
                    _lDelCNY00056PK.Add(pkChildC);
                    _lCNY00016PK.Add(ProcessGeneral.GetSafeInt64(dr["CNY00016PK"]));

                }
                dtS.Rows.Remove(dr);
            }

            dtS.AcceptChanges();
            ReCalDataColorGrid(dtS);





        }


        private void ReCalDataColorGrid(DataTable dtColor)
        {


            if (dtColor == null)
            {
                dtColor = gcColor.DataSource as DataTable;
            }
           
            TreeListNode node = tlMain.FocusedNode;
            Int64 pkChild = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
            string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState"));
            if (dtColor == null || dtColor.Rows.Count <= 0)
            {

             
                if (_dicAttValue.ContainsKey(pkChild))
                {
                    _dicAttValue.Remove(pkChild);
                }

                if (rowState != DataStatus.Insert.ToString())
                {
                    _lDelCNY00055PK.Add(pkChild);

                }
 
                tlMain.DeleteNode(node);
                if (tlMain.AllNodesCount > 0)
                {
                    TreeListNode focNode = tlMain.FocusedNode;
                    if (focNode != null)
                    {
                        if (!tlMain.Focused)
                        {
                            tlMain.Focus();

                        }
                        TreeListColumn tCol = tlMain.FocusedColumn;
                        if (tCol == null)
                        {
                            tCol = tlMain.VisibleColumns[0];
                        }
                        tlMain.SelectCell(focNode, tCol);
                    }

          
                }
   
            
            }
            else
            {
                var qSum = dtColor.AsEnumerable().Select(p => new
                {
                    GroupColumn = 1,
                    PRQty_CNY002 = p.Field<double>("PRQty_CNY002"),
                    POQty_CNY003 = p.Field<double>("POQty_CNY003"),
                    PRQty_CNY002B = p.Field<double>("PRQty_CNY002B"),
                    POQty_CNY003B = p.Field<double>("POQty_CNY003B"),
                }).GroupBy(p => p.GroupColumn).Select(s => new
                {
                    GroupColumn = s.Key,
                    PRQty_CNY002 = s.Sum(t => t.PRQty_CNY002),
                    POQty_CNY003 = s.Sum(t => t.POQty_CNY003),
                    PRQty_CNY002B = s.Sum(t => t.PRQty_CNY002B),
                    POQty_CNY003B = s.Sum(t => t.POQty_CNY003B),
                }).First();
                node.SetValue("PRQty_CNY002",qSum.PRQty_CNY002);
                node.SetValue("POQty_CNY003", qSum.POQty_CNY003);
                node.SetValue("PRQty_CNY002B", qSum.PRQty_CNY002B);
                node.SetValue("POQty_CNY003B", qSum.POQty_CNY003B);
                if (rowState == DataStatus.Unchange.ToString())
                {
                    node.SetValue("RowState", DataStatus.Update.ToString());
                }
            }
            _dicTableColor[pkChild] = dtColor;
            LoadDataGridViewDetailFrist(dtColor);






        }

        public void PerformCheck()
        {


            if (tlMain.AllNodesCount <= 0) return;



            TreeListNode focusedNode = tlMain.FocusedNode ?? tlMain.Nodes[0];
            TreeListColumn focusedCol = tlMain.FocusedColumn ?? tlMain.VisibleColumns[0];

          



            List<TreeListNode> qNode = tlMain.GetAllNodeTreeList().Where(p => ProcessGeneral.GetSafeInt64(p.GetValue("ParentPK")) <= 0 &&
//                                                                              ProcessGeneral.GetSafeBool(p.GetValue("AllowUpdate")) && 
                                                                              !ProcessGeneral.GetSafeBool(p.GetValue("IsHasChild")) &&
                                                                              ProcessGeneral.GetSafeBool(p.GetValue("DifUC"))).ToList();

           
            if (qNode.Count <= 0) return;

            DialogResult dlResult = XtraMessageBox.Show("Do you want to update demand on this records changed Amount in  BOM?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlResult != DialogResult.Yes) return;






            _isEditColor = false;
            foreach (TreeListNode node in qNode)
            {
                //if (node != focusedNode) continue;
                tlMain.SetFocusedCellTreeList(node, focusedCol);
                Int64 pkChild = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
                DataTable dtColor;
                if (!_dicTableColor.TryGetValue(pkChild, out dtColor) || dtColor.Rows.Count<=0) continue;
               



                var q2 = dtColor.AsEnumerable().Where(p => p.RowState != DataRowState.Deleted &&
                                                           p.Field<double>("UC") != p.Field<double>("UC_BOM")).ToList();
                if (q2.Count <= 0) continue;



                foreach (DataRow drQ2 in q2)
                {

                    double ucBom = ProcessGeneral.GetSafeDouble(drQ2["UC_BOM"]);
                    int planQuantity = ProcessGeneral.GetSafeInt(drQ2["PlanQuantity"]);
                    double tolerance = ProcessGeneral.GetSafeDouble(drQ2["Tolerance"]);
                    double percentUsing = ProcessGeneral.GetSafeDouble(drQ2["PercentUsing"]);
                    double prQtyCny002B = Math.Round(ucBom * planQuantity * (1 + tolerance / 100) * percentUsing / 100, ConstSystem.FormatPrQtyDecimal);
                    double poQtyCny003B = Math.Round(ucBom * planQuantity * (1 + tolerance / 100) * percentUsing / 100, ConstSystem.FormatPoQtyDecimal);
                    double packingFactor = ProcessGeneral.GetSafeDouble(node.GetValue("PackingFactor"));

                    drQ2["UC"] = ucBom;
                    drQ2["POQty_CNY003B"] = poQtyCny003B;
                    drQ2["PRQty_CNY002B"] = prQtyCny002B;
                    drQ2["POQty_CNY003"] = Math.Round(poQtyCny003B * packingFactor, ConstSystem.FormatPoQtyDecimal);
                    drQ2["PRQty_CNY002"] = Math.Round(prQtyCny002B * packingFactor, ConstSystem.FormatPrQtyDecimal);

                  

                    string rowState = ProcessGeneral.GetSafeString(drQ2["RowState"]);
                    if (rowState == DataStatus.Unchange.ToString())
                    {
                        drQ2["RowState"] = DataStatus.Update.ToString();

                    }
                    
                }



                dtColor.AcceptChanges();
                ReCalDataColorGrid(dtColor, node);



            }


            _isEditColor = true;








            tlMain.FocusedNode = focusedNode;
            tlMain.FocusedColumn = focusedCol;

            tlMain.BeginSelection();
            tlMain.Selection.Clear();
            tlMain.SelectNode(focusedNode);
            tlMain.EndSelection();
        
        }


        private void ReCalDataColorGrid( DataTable dtColor, TreeListNode node)
        {


            node.SetValue("DifUC", false);





            var qSum = dtColor.AsEnumerable().Where(p=>p.RowState != DataRowState.Deleted).Select(p => new
            {
                GroupColumn = 1,
                PRQty_CNY002 = p.Field<double>("PRQty_CNY002"),
                POQty_CNY003 = p.Field<double>("POQty_CNY003"),
                PRQty_CNY002B = p.Field<double>("PRQty_CNY002B"),
                POQty_CNY003B = p.Field<double>("POQty_CNY003B"),
            }).GroupBy(p => p.GroupColumn).Select(s => new
            {
                GroupColumn = s.Key,
                PRQty_CNY002 = s.Sum(t => t.PRQty_CNY002),
                POQty_CNY003 = s.Sum(t => t.POQty_CNY003),
                PRQty_CNY002B = s.Sum(t => t.PRQty_CNY002B),
                POQty_CNY003B = s.Sum(t => t.POQty_CNY003B),
            }).First();
            node.SetValue("PRQty_CNY002", qSum.PRQty_CNY002);
            node.SetValue("POQty_CNY003", qSum.POQty_CNY003);
            node.SetValue("PRQty_CNY002B", qSum.PRQty_CNY002B);
            node.SetValue("POQty_CNY003B", qSum.POQty_CNY003B);
            string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState"));
            if (rowState == DataStatus.Unchange.ToString())
            {
                node.SetValue("RowState", DataStatus.Update.ToString());
            }





            LoadDataGridViewDetailFrist(dtColor);






        }


        private void CalWasteChanged(GridView gv , int rH , double waste)
        {
            _isEditColor = false;
            TreeListNode node = tlMain.FocusedNode;




            double prQtyCny002B = Math.Round(ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, "UC")) * ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, "PlanQuantity")) * (1 + waste / 100) *
                                              ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, "PercentUsing")) / 100, ConstSystem.FormatPrQtyDecimal);


            double poQtyCny003B = Math.Round(ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, "UC")) * ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, "PlanQuantity")) * (1 + waste / 100) *
                                              ProcessGeneral.GetSafeDouble(gv.GetRowCellValue(rH, "PercentUsing")) / 100, ConstSystem.FormatPoQtyDecimal);
            double packingFactor = ProcessGeneral.GetSafeDouble(node.GetValue("PackingFactor"));

            gv.SetRowCellValue(rH, "POQty_CNY003B", poQtyCny003B);
            gv.SetRowCellValue(rH, "PRQty_CNY002B", prQtyCny002B);



            gv.SetRowCellValue(rH, "POQty_CNY003", Math.Round(poQtyCny003B * packingFactor, ConstSystem.FormatPoQtyDecimal));
            gv.SetRowCellValue(rH, "PRQty_CNY002", Math.Round(prQtyCny002B * packingFactor, ConstSystem.FormatPrQtyDecimal));


           

      
            
            ReCalDataColorGrid((DataTable)gcColor.DataSource);
            _isEditColor = true;
        }

        private bool _isEditColor = true;

        private void GvColor_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!_isEditColor) return;
            GridView gv = (GridView) sender;
            if (gv == null) return;
            int rH = e.RowHandle;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "RowState"));
            if (rowState == DataStatus.Unchange.ToString())
            {
                gv.SetRowCellValue(rH, "RowState", DataStatus.Update.ToString());
               
            }


            string fieldName = e.Column.FieldName;
            switch (fieldName)
            {
                case "POQty_CNY003":
                    _isEditColor = false;
                    TreeListNode node = tlMain.FocusedNode;
                    double poQtyCny003B = Math.Round(ProcessGeneral.GetSafeDouble(e.Value) / ProcessGeneral.GetSafeDouble(node.GetValue("PackingFactor")), ConstSystem.FormatPoQtyDecimal);
                    gv.SetRowCellValue(rH, "POQty_CNY003B", poQtyCny003B);
                    ReCalDataColorGrid((DataTable)gcColor.DataSource);
                    _isEditColor = true;
                    break;
                case "Tolerance":
                    CalWasteChanged(gv, rH, ProcessGeneral.GetSafeDouble(e.Value));
                    break;
            }
        }















        #endregion

        #endregion




        #region "Process Grid Detail"


        private void LoadDataGridViewDetailFrist(DataTable dtColor)
        {
            //DataTable dt;
            //if (dtColor == null || dtColor.Rows.Count <= 0)
            //{
            //    dt = Ctrl_PrGenneral.TableGridMChildTemplate();


            //}
            //else
            //{
            //    var q1 = dtColor.AsEnumerable().GroupBy(p => new
            //    {
            //        Reference = p.Field<string>("Reference"),
            //        PlanQuantity = p.Field<Int32>("PlanQuantity"),
            //        RMCode_001 = p.Field<string>("RMCode_001"),
            //        RMDescription_002 = p.Field<string>("RMDescription_002"),
            //        ProDimension = p.Field<string>("ProDimension"),
            //      //  UC = p.Field<double>("UC"),
            //        Tolerance = p.Field<double>("Tolerance"),
            //        PercentUsing = p.Field<double>("PercentUsing"),
            //        ProjectName = p.Field<string>("ProjectName"),
            //        ProductionOrder = p.Field<string>("ProductionOrder"),
            //        RootSOQty = p.Field<Int32>("RootSOQty"),
            //        CNY00020PK = p.Field<Int64>("CNY00020PK"),

            //    }).Select(t => new
            //    {
            //        t.Key.Reference,
            //        PRQty_CNY002 = t.Sum(s => s.Field<double>("PRQty_CNY002")),
            //        POQty_CNY003 = t.Sum(s => s.Field<double>("POQty_CNY003")),
            //        PRQty_CNY002B = t.Sum(s => s.Field<double>("PRQty_CNY002B")),
            //        POQty_CNY003B = t.Sum(s => s.Field<double>("POQty_CNY003B")),
            //        t.Key.PlanQuantity,
            //       // t.Key.AssemblyComponent,
            //        t.Key.RMCode_001,
            //        t.Key.RMDescription_002,
            //        t.Key.ProDimension,
            //        //  Count = t.Count(),
            //        UC = t.Sum(s => s.Field<double>("UC")),
            //        t.Key.Tolerance,
            //        t.Key.PercentUsing,
            //        t.Key.ProjectName,
            //        t.Key.ProductionOrder,
            //        t.Key.RootSOQty,
            //        t.Key.CNY00020PK,
            //    }).ToList();

            //    dt = q1.CopyToDataTableNew();

            //}



            //gvDetail.BeginUpdate();





            //gcDetail.DataSource = null;
            //gvDetail.Columns.Clear();

            //gcDetail.DataSource = dt;

            //Dictionary<string, bool> dicCol = new Dictionary<string, bool>
            //{
            //    {"Reference", true},
            //    {"PRQty_CNY002", true},
            //    {"POQty_CNY003", true},
            //    { "PRQty_CNY002B", false},
            //    {"POQty_CNY003B", false},
            //    {"PlanQuantity", true},
            //    {"UC", true},
            //    {"RMCode_001", false},
            //    {"RMDescription_002", true},
            //    {"ProductionOrder", true},
            //    {"ProjectName", true},
            //    {"ProDimension", true},
            //    {"Tolerance", true},
            //    {"PercentUsing", true},
                
            //    {"RootSOQty", false},
            //    {"CNY00020PK", false},
            //};
            //gvDetail.VisibleAndSortGridColumn(dicCol);






            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PlanQuantity"], "Pro. Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["PlanQuantity"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            //gvDetail.Columns["PlanQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["PlanQuantity"].DisplayFormat.FormatString = "N0";
            //gvDetail.Columns["PlanQuantity"].ImageIndex = 0;
            //gvDetail.Columns["PlanQuantity"].ImageAlignment = StringAlignment.Near;
            ////gvMain.Columns["PlanQuantity"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvMain.Columns["PlanQuantity"].SummaryItem.DisplayFormat = @"{0:N0}";



            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["UC"], "Amount", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["UC"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["UC"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatUcDecimal(false, false);

            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["Tolerance"], "Waste (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["Tolerance"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["Tolerance"].DisplayFormat.FormatString = "#,0.#####";

            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PercentUsing"], "Using (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["PercentUsing"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["PercentUsing"].DisplayFormat.FormatString = "#,0.#####";


        




            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PRQty_CNY002"], "Demand Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["PRQty_CNY002"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["PRQty_CNY002"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            ////gvMain.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvMain.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["POQty_CNY003"], "Purchase Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["POQty_CNY003"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["POQty_CNY003"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
            ////gvMain.Columns["POQty_CNY003"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvMain.Columns["POQty_CNY003"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";


            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PRQty_CNY002B"], "Demand Qty (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["PRQty_CNY002B"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["PRQty_CNY002B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            ////gvMain.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvMain.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["POQty_CNY003B"], "Purchase Qty (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["POQty_CNY003B"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["POQty_CNY003B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);



            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["ProDimension"], "Dimension (PRO.)", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);


            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["ProjectName"], "Project Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);


            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["ProductionOrder"], "Production Order", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["RootSOQty"], "Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            //gvDetail.Columns["RootSOQty"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvDetail.Columns["RootSOQty"].DisplayFormat.FormatString = "N0";
   
            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["Reference"], "Reference", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            ////gvMain.Columns["Reference"].SummaryItem.SummaryType = GridSumCol.Sum;
            ////gvMain.Columns["Reference"].SummaryItem.DisplayFormat = @"Total:";

           
            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["RMCode_001"], "Item Code", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);

            //ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["RMDescription_002"], "Item Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);











            ////ProcessGeneral.SetGridColumnHeader(gvMain.Columns["ColorCode"], "Color", DefaultBoolean.False, HorzAlignment.Near,
            ////    GridFixedCol.None);


            ////var qColAtt = gvMain.Columns.Where(p => p.FieldName.IndexOf("-", StringComparison.Ordinal) > 0).ToList();

            ////foreach (GridColumn gColD in qColAtt)
            ////{

            ////    ProcessGeneral.SetGridColumnHeader(gColD, gColD.Caption, DefaultBoolean.False, HorzAlignment.Near,
            ////        GridFixedCol.None);
            ////    gColD.ImageIndex = 0;
            ////    gColD.ImageAlignment = StringAlignment.Near;
            ////}

            //gvDetail.BestFitColumns();

            //gvDetail.EndUpdate();


        }



        private void GridViewDetailCustomInit()
        {



            gcDetail.UseEmbeddedNavigator = true;

            gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvDetail.OptionsBehavior.Editable = true;
            gvDetail.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvDetail.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvDetail.OptionsCustomization.AllowColumnMoving = false;
            gvDetail.OptionsCustomization.AllowQuickHideColumns = true;

            gvDetail.OptionsCustomization.AllowSort = false;

            gvDetail.OptionsCustomization.AllowFilter = false;


            gvDetail.OptionsView.ShowGroupPanel = false;
            gvDetail.OptionsView.ShowIndicator = true;
            gvDetail.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvDetail.OptionsView.ShowAutoFilterRow = false;
            gvDetail.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvDetail.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvDetail.OptionsNavigation.AutoFocusNewRow = true;
            gvDetail.OptionsNavigation.UseTabKey = true;

            gvDetail.OptionsSelection.MultiSelect = true;
            gvDetail.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvDetail.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvDetail.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvDetail.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvDetail.OptionsView.EnableAppearanceEvenRow = false;
            gvDetail.OptionsView.EnableAppearanceOddRow = false;
            gvDetail.OptionsView.ShowFooter = false;
            gvDetail.OptionsView.RowAutoHeight = true;
            gvDetail.OptionsHint.ShowFooterHints = false;
            gvDetail.OptionsHint.ShowCellHints = true;
            //   gridView1.RowHeight = 25;

            gvDetail.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvDetail.OptionsFind.AllowFindPanel = false;

            gvDetail.OptionsView.BestFitMaxRowCount = 10;
            gvDetail.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvDetail)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };




            gvDetail.ShowingEditor += gvDetail_ShowingEditor;
            gvDetail.RowCountChanged += gvDetail_RowCountChanged;
            gvDetail.CustomDrawRowIndicator += gvDetail_CustomDrawRowIndicator;

            gvDetail.RowCellStyle += gvDetail_RowCellStyle;

            gvDetail.LeftCoordChanged += gvDetail_LeftCoordChanged;
            gvDetail.MouseMove += gvDetail_MouseMove;
            gvDetail.TopRowChanged += gvDetail_TopRowChanged;
            gvDetail.FocusedColumnChanged += gvDetail_FocusedColumnChanged;
            gvDetail.FocusedRowChanged += gvDetail_FocusedRowChanged;
            gcDetail.Paint += gcDetail_Paint;
          




            gcDetail.ForceInitialize();



        }

        #region "GridView Event"




        private void gvDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

        }


        private void gvDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
     
            string fieldName = gCol.FieldName;



            if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(rH, gCol))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }












            switch (fieldName)
            {
                case "PRQty_CNY002":
                case "PRQty_CNY002B":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "POQty_CNY003":
                case "POQty_CNY003B":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "RootSOQty":
                    {

                        e.Appearance.ForeColor = Color.DarkBlue;
                    }
                    break;
                case "PlanQuantity":
                    {

                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;

            }

            switch (fieldName)
            {

                case "Count":
                case "UC":
                case "Tolerance":
                case "PercentUsing":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.WhiteSmoke;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "RMCode_001":
                case "RMDescription_002":
                case "Reference":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "ProjectName":
                case "AssemblyComponent":
                case "ProductionOrder":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "PRQty_CNY002":
                case "PRQty_CNY002B":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.Cornsilk;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
                case "POQty_CNY003":
                case "POQty_CNY003B":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightPink;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
                case "PlanQuantity":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
                case "RootSOQty":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Khaki;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "ProDimension":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;

            }











        }

        private void gvDetail_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvDetail_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcDetail_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvDetail_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
            bool isSelected = gv.IsRowSelected(e.RowHandle);
            LinearGradientBrush backBrush;
            if (isSelected)
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

        #endregion

    }
}
