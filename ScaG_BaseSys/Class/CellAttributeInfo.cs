using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.UControl;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition;
using FixedStyle = DevExpress.XtraGrid.Columns.FixedStyle;

namespace CNY_BaseSys.Class
{
    public class AttStandardMRPTestInfo
    {

        public Int64 ID { get; set; }
        public Int64 AttibutePK { get; set; }
        public string AttibuteCode { get; set; }
        public string AttibuteName { get; set; }
        public string AttibuteValueFull { get; set; }
        public Int64 PK { get; set; }
        public Int64 PKRowHeader { get; set; }
        public string RowState { get; set; }
        public string AttibuteValueTemp { get; set; }
        public string AttibuteUnit { get; set; }
        public bool IsNumber { get; set; }
        public string DataAttType { get; set; }

        public string AttributeLinkPK { get; set; }
        public int SortIndex { get; set; }
        public bool IsNew { get; set; }
        public Int64 CNY00001PK { get; set; }
        public string UnitTemp { get; set; }
        public int IsRMStandardization { get; set; }
        public Int64 TDG00001PK { get; set; }
        public string UCUnitCode { get; set; }
        public string PurchaseUnit_010 { get; set; }


    }
    public class CNY00055ACrossTemp1Info
    {
        public Int64 CNY00055APK { get; set; }
        public decimal PRQuantity { get; set; }
        public decimal POQuantity { get; set; }
        public bool IsReleasedPO { get; set; }
        public int Status { get; set; }
        public int StatusOld { get; set; }
        public int Version { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string SubmittedBy { get; set; }
        public string SubmittedDate { get; set; }
        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedDate { get; set; }
        public int POStatus { get; set; }
    }
    public class AttCrossTemp1Info
    {
        public List<BoMInputAttInfo> LAttInfo { get; set; }
        public string StringAttValue { get; set; }
    }
    public class AssCompMRPUpdInfo
    {
        public Int64 CNY00016PK { get; set; }
        public Int64 CNY00016PKParent { get; set; }
        public string AssemblyComponent { get; set; }
    }
    public class MrpRoundedInfo
    {
        private int _decimalRound = 0;
        public int DecimalRound
        {
            get
            {
                return _decimalRound;
            }
            set
            {
                _decimalRound = value;
            }
        }

        private int _roundRule = 0;
        public int RoundRule
        {
            get
            {
                return _roundRule;
            }
            set
            {
                _roundRule = value;
            }
        }

        private int _roundType = 0;
        public int RoundType
        {
            get
            {
                return _roundType;
            }
            set
            {
                _roundType = value;
            }
        }

        public MrpRoundedInfo()
        {

        }
        public MrpRoundedInfo(int decimalRound, int roundRule, int roundType)
        {
            _decimalRound = decimalRound;
            _roundRule = roundRule;
            _roundType = roundType;
        }

    }

    public class SoBoMDemandNormalInfo
    {
        public Int64 PKRow { get; set; }
        public string MainMaterialGroup { get; set; }
        public Int64 TDG00001PK { get; set; }
        public string RMCode_001 { get; set; }
        public string RMDescription_002 { get; set; }
        public string ColorReference { get; set; }
        public Int64 CNY00002PK { get; set; }
        public string Supplier { get; set; }
        public decimal UC { get; set; }
        public string UCUnitCode { get; set; }
        public string UCUnit { get; set; }
        public decimal Waste { get; set; }
        public decimal PercentUsing { get; set; }
        public decimal RateGram { get; set; }
        public Int64 CNY00020PK { get; set; }
        public Int64 CNY00016PK { get; set; }
        public int OrderQuantity { get; set; }
        public Int64 CNY00050PK { get; set; }
        public string Position { get; set; }
        public decimal Factor { get; set; }
        public string QualityCode { get; set; }
        public string SupplierRef { get; set; }
        public Int64 TDG00004PK { get; set; }
        public Int64 CNY00050PKFi { get; set; }
        public decimal AreaPaint { get; set; }
        public int DecimalRound { get; set; }
        public int FactorProduct { get; set; }
        public bool CancelLine { get; set; }
        public int CancelFactor { get; set; }
        public bool IsFinishing { get; set; }
        public decimal TotalDemandQty { get; set; }
        public bool IsReleasePR { get; set; }
        public string ColorReference_PR { get; set; }
        public Int64 CNY00002PK_PR { get; set; }
        public string Supplier_PR { get; set; }
        public decimal UC_PR { get; set; }
        public string UCUnitCode_PR { get; set; }
        public string UCUnit_PR { get; set; }
        public decimal Waste_PR { get; set; }
        public decimal PercentUsing_PR { get; set; }
        public decimal RateGram_PR { get; set; }
        public int OrderQuantity_PR { get; set; }
        public Int64 CNY00050PK_PR { get; set; }
        public string SupplierRef_PR { get; set; }
        public Int64 TDG00004PK_PR { get; set; }
        public decimal AreaPaint_PR { get; set; }
        public int FactorProduct_PR { get; set; }
        public bool CancelLine_PR { get; set; }
        public int CancelFactor_PR { get; set; }
        public bool IsFinishing_PR { get; set; }
        public decimal PRQty_PR { get; set; }
        public Int64 CNY00056PK_PR { get; set; }
        public Int64 CNY00055PK_PR { get; set; }
        public decimal PRQtyS_PR { get; set; }
        public decimal StockQty_PR { get; set; }
        public string AttributeLinkPK { get; set; }
        public Int64 CNY00001PK { get; set; }
        public decimal PackingFactor { get; set; }
        public string PurchaseUnit_010 { get; set; }
        public decimal OldDemandS { get; set; }
        public decimal OldDemandD { get; set; }
        public int IsRMStandardization { get; set; }
        public string FormulaString { get; set; }
        public Int64 CNY00004PK { get; set; }
        public bool IsPRissuedTypechecking { get; set; }
        public bool IsRightRM { get; set; }
        public int BOMStatus { get; set; }
        public int PurchaseType { get; set; }
        public int Status { get; set; }
        public int Version { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string SubmittedBy { get; set; }
        public string SubmittedDate { get; set; }
        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedDate { get; set; }
        public string Reference { get; set; }
        public string ProDimension { get; set; }
        public int RootSOQty { get; set; }
        public Int64 CNY00016PKParent { get; set; }
        public Int64 TDG00001PKSO { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Int64 CNYMF016PK { get; set; }
        public bool IsGrouping { get; set; }
        public bool IsFormula { get; set; }
        public bool IsPacking { get; set; }
        public bool IsUnAllocate { get; set; }
        public decimal UnAllocateQty { get; set; }
        public int LeadTime { get; set; }

        public Int64 PK55SAVE { get; set; }
        public bool IsDelete { get; set; }
        public string AssemblyComponent { get; set; }

        public decimal BookingQuantity { get; set; }
        public string Color { get; set; }
        public string Route { get; set; }


        public int ChangeDemand { get; set; }

        public Int64 TDG00001PKBOM { get; set; }
        public int DIFFTDG00001PK { get; set; }
        public string RMDescription_002BOM { get; set; }
        public Int64 CNY00016PKREP { get; set; }
        public int RoundRule { get; set; }
        public int RoundType { get; set; }
    }
    public class TreeListPasteDataSOBreakDown
    {
        public Int32 GridRow { get; set; }
        public DataRow Row { get; set; }
    }
    public class SoInfoBomCrossItem
    {
        public int BOMNo { get; set; }
        public string BOMStatus { get; set; }

        public int BoMVersion { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int BOMStatusInt { get; set; }


    }
    public class PrAllocateF4Info
    {

        public Int64 CNY00020PK { get; set; }
        public int PlanQty { get; set; }
        public decimal PRQuantity { get; set; }

        public bool IsDecimal { get; set; }

    }
    public class PrNsoInfo
    {


        public string ProductCode { get; set; }
        public string Reference { get; set; }
        public int OrderQuantity { get; set; }

        public int InputQuantity { get; set; }
        public Int64 TDG00001PK { get; set; }
        public Int64 CNY00020PK { get; set; }
        public int FactorProduct { get; set; }
        public int SortOrderNode { get; set; }
        public Int64 BOMID { get; set; }

        public string ProDimension { get; set; }
        public int RootSOQty { get; set; }
        public string ProductName { get; set; }
    }
    public static class SmtpCommands
    {
        public const string Auth = ("AUTH ");

        public const string AuthLogin = "LOGIN";

        public const string Data = ("DATA");
        public const string Date = ("DATE: ");

        public const string EHello = ("EHLO ");
        public const string From = ("FROM: ");
        public const string To = ("TO: ");

        public const string ReplyTo = ("REPLY-TO: ");
        public const string Subject = ("SUBJECT: ");

        public const string Recipient = ("RCPT TO:");


        public const string Mail = ("MAIL FROM:");
        public const string Utf8 = ("SMTPUTF8");
        public const string Quit = ("QUIT");



        public const string StartTls = ("STARTTLS");


    }
    public struct Indexes
    {
        public int Start;
        public int End;
    }


    public class MenuInfo
    {
        public DevExpress.XtraGrid.Columns.FixedStyle Style;
        public GridColumn Column;
        public bool Checked;

        public MenuInfo(GridColumn column, DevExpress.XtraGrid.Columns.FixedStyle style, bool check)
        {
            this.Column = column;
            this.Style = style;
            this.Checked = check;
        }
    }
    public class UserSystemEmailInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string FristName { get; set; }
    }
    public class CalAreaAttInfo
    {
        public Int64 CNY00008PKCal { get; set; }
        public decimal ValueCal { get; set; }
    }
    public class ControlCheckInfo
    {
        public Control CtrEdit { get; set; }
        public object OldValue { get; set; }


    }
    public class TileImageItemInfo
    {
        public Image BackImage { get; set; }
        public string Text3 { get; set; }

        public Int32 IncreasedFontCaption { get; set; }


        public string CaptionText { get; set; }

        public Int32 IncreasedFontDescription { get; set; }

        public Image HeaderImage { get; set; }


        public Color ColorDesc { get; set; }

    }
    public class TreePopupFiItemInfo
    {
        public string Caption { get; set; }
        public string FieldName { get; set; }
        public HorzAlignment HorzAlign { get; set; }
        public DevExpress.XtraTreeList.Columns.FixedStyle FixStyle { get; set; }
        public FormatType FormatField { get; set; }
        public string FormatString { get; set; }






    }
    public class TreeViewTransferDataColumnInit
    {
        public string Caption { get; set; }
        public string FieldName { get; set; }
        public HorzAlignment HorzAlign { get; set; }
        public DevExpress.XtraTreeList.Columns.FixedStyle FixStyle { get; set; }
        public FormatType FormatField { get; set; }
        public string FormatString { get; set; }

     


   
      
    }
    public class ColorDemandPrInfoRpt
    {
        public Color ColorSet { get; set; }
        public string DescriptionSet { get; set; }
    }
    public class BOMCHECKPIVOTINFO
    {
        public string MainMaterialGroupCode { get; set; }
        public string FinishPR { get; set; }
        public string FinishPO { get; set; }



    }
    public class FinishingRptFinalInfo
    {
        public Int64 CNY00050PK { get; set; }
        public string FinishingColor { get; set; }


        public List<FinishingRptCheckInfo> GroupLV2 { get; set; }

    }

    public class FinishingRptCheckInfo
    {
        public string QualityCode { get; set; }
        public double AreaPaint { get; set; }




    }
    public class BOMCALPIVOTINFO
    {

        public Int64 CNY00020PK { get; set; }
        public double PlanDemand { get; set; }
        public string PlanUnit { get; set; }
        public double SODemand { get; set; }
        public string SOUnit { get; set; }



    }
    public class SOBOMPRPOPIVOTFINAL
    {


        public string RMCode_001 { get; set; }
        public string RMDescription_002 { get; set; }
        public string Dimenson { get; set; }
        public List<SOBOMPRPOPIVOTINFO> GroupIndexAggreate { get; set; }
    }

    public class SOBOMPRPOPIVOTINFO
    {

        public Int64 CNY00020PK { get; set; }
        public double BoMDemand { get; set; }
        public string BOMUnit { get; set; }
        public double PRQty { get; set; }
        public string PRUnit { get; set; }
        public double POQty { get; set; }
        public string POUnit { get; set; }



    }

    public class SOPRPOINFO
    {
        public Int64 CNY00020PK { get; set; }
        public string RMCode_001 { get; set; }
        public string RMDescription_002 { get; set; }
        public string Dimenson { get; set; }
        public decimal CalDemand { get; set; }
        public string CalUnit { get; set; }

        public decimal BoMDemand { get; set; }
        public string BOMUnit { get; set; }
        public decimal PRQty { get; set; }
        public string PRUnit { get; set; }
        public decimal POQty { get; set; }
        public string POUnit { get; set; }




    }

    public class SOBOMIDNOINFO
    {
        public string StrBOMID { get; set; }
        public string StrBoMNo { get; set; }
    }
    public class DragDropUseInfo
    {
        public bool AllowDragDrop { get; set; }
        public bool IsMove { get; set; }
      
    }
    public class CalDimensionInfoFinal
    {
        public Int32 Index { get; set; }
        public Int64 Tag { get; set; }
        public string FieldName { get; set; }
        public string AttName { get; set; }

        public Type ColumnType { get; set; }
        
    }
    public class DeletedInfoField
    {
        public string FieldName { get; set; }
        public Int32 VisibleIndex { get; set; }
        public Int32 SortOrder { get; set; }
        public bool IsAttribute { get; set; }
        public TreeListColumn TCol { get; set; }
        public Int64 Tag { get; set; }
    }
    public class DeletedInfoNode
    {
        public TreeListNode Node { get; set; }
        public Int64 ChildPK { get; set; }
        public string ItemType { get; set; }
        public string RowState { get; set; }
        public bool AllowUpdate { get; set; }
        public Int32 Index { get; set; }
    }

    public class DeletedInfoFiNode
    {
        public TreeListNode Node { get; set; }
        public Int64 ChildPK { get; set; }
        public string ItemType { get; set; }
        public string RowState { get; set; }
        public string AllowUpdate { get; set; }
        public Int32 Index { get; set; }

        public bool AttributeFixed { get; set; }
    }
    public class DeletedInfoFiNodeH
    {
        public TreeListNode Node { get; set; }
        public Int64 ChildPK { get; set; }
        public string ItemType { get; set; }
        public string RowState { get; set; }
        public Int32 Index { get; set; }

        public bool AttributeFixed { get; set; }
    }
    public class ControlDInfoRow
    {
        public Int32 RowHandle { get; set; }
        public Int64 ChildPK { get; set; }
        public string RowState { get; set; }
        public bool AllowUpdate { get; set; }
     
    }
    public class ControlDInfoNode
    {
        public TreeListNode Node { get; set; }
        public Int64 ChildPK { get; set; }
        public string ItemType { get; set; }
        public string RowState { get; set; }
        public bool AllowUpdate { get; set; }
        public Int32 Index { get; set; }
    }



    public class ControlDInfoFiNode
    {
        public TreeListNode Node { get; set; }
        public Int64 ChildPK { get; set; }
        public string ItemType { get; set; }
        public string RowState { get; set; }
        public string AllowUpdate { get; set; }
        public Int32 Index { get; set; }
        public bool AttributeFixed { get; set; }
    }

    public class ControlDInfoFieldGrid
    {
        public string FieldName { get; set; }
        public Int32 VisibleIndex { get; set; }
        public Int32 SortOrder { get; set; }
        public bool IsAttribute { get; set; }
        public GridColumn TCol { get; set; }
        public Int64 Tag { get; set; }
        public string FieldNameFrist { get; set; }
    }
    public class ControlDInfoFieldTree
    {
        public string FieldName { get; set; }
        public Int32 VisibleIndex { get; set; }
        public Int32 SortOrder { get; set; }
        public bool IsAttribute { get; set; }
        public TreeListColumn TCol { get; set; }
        public Int64 Tag { get; set; }
    }
    public class SearchLookUpEditInfoGeneral
    {
        public SearchLookUpEdit SearchEditControl { get; set; }
        public string ControlName { get; set; }
        public string TooltipError { get; set; }
        public TextEdit ControlDescription { get; set; }

    }


    public class BoMCompactDelAttInfo
    {
        public Int64 CNY00008PK { get; set; }
        public string FieldName { get; set; }
        public Int32 VisibleIndex { get; set; }


    }

    public class AttributeSaveFInfo
    {
        public Int64 PK { get; set; }
        public Int64 CNY00008PK_AttributePK { get; set; }
        public Int64 DetailPK { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeUnit { get; set; }
        public DataStatus RowState { get; set; }

    }
    public class DataRowIndexSort
    {
        public DataRow RowData { get; set; }
        public Int64 RowIndex { get; set; }
    }
    public class CellAttributeInfo
    {
        public ColumnAttributeInfo ColInfo { get; set; }
        public RowAttributeInfo RowInfo { get; set; }
       // public Int64 CellValueCny00010Pk { get; set; }
        public String CellDisplayText { get; set; }

        public DataStatus CellStatus { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsCombineName { get; set; }

        public string AttibuteValueTemp { get; set; }

        public string AttibuteUnit { get; set; }

        public bool IsNumber { get; set; }
    }

    public class AttributeSwap
    {
        public Int32 VisibleIndexSwap { get; set; }
        public Int32 VisibleIndexSet { get; set; }
    }
    public class BoMPaintNodeInfo
    {
        public Int64 CNYMF016PK { get; set; }
        public string StepWork { get; set; }
        public Int64 PKCode { get; set; }
        public string RMCode_001 { get; set; }
        public string RMDescription_002 { get; set; }
        public string Unit { get; set; }
        public string RMGroup_066 { get; set; }
        public string QualityCode { get; set; }
        public decimal RateGram { get; set; }
        public decimal RatePercent { get; set; }
        public decimal UC { get; set; }

        public decimal Tolerance { get; set; }
        public string Note { get; set; }
        public string RowState { get; set; }
        public Int32 ItemLevel { get; set; }
        public Int32 SortOrderNode { get; set; }
        public Int64 ChildPK { get; set; }
        public Int64 ParentPK { get; set; }



        public string Supplier { get; set; }
        public Int64 SupplierPK { get; set; }
        public string SupplierRef { get; set; }
        public Int64 TDG00004PK { get; set; }
        public bool AllowUpdate { get; set; }

        public string TableCode { get; set; }

        public string PurchaseType { get; set; }
    }
    

    public class BomFinishingChildSaveInfo
    {



        public Int64 PK { get; set; }
        public Int64 CNY00052PK { get; set; }
        public Int64 TDG00001PK { get; set; }

        public Int64 CNY00002PK { get; set; }
        public string CNY001_UpdateBy { get; set; }

        public DateTime CNY002_UpdateDate { get; set; }

        public string CNY003_Unit { get; set; }
        public decimal CNY004_RateGram { get; set; }


        public decimal CNY005_RatePercent { get; set; }
        public decimal CNY006_UC { get; set; }

        public Int32 SortOrderNode { get; set; }
        public string CNY007_Note { get; set; }
        public string QualityCode { get; set; }

        public Int64 TDG00004PK { get; set; }
        public Int64 BoMFiColorPK { get; set; }
        public Int64 CNY00050PKFi { get; set; }

        public Int64 CNYMF016PKFi { get; set; }

        public Int32 PurchaseType { get; set; }

        public decimal Tolerance { get; set; }
    }


    public class BomFinishingParentSaveInfo
    {
        public Int64 PK { get; set; }
        public Int64 CNY00051PK { get; set; }
        public Int64 CNYMF016PK { get; set; }

        public decimal CNY001_RateGram { get; set; }

        public decimal CNY002_RatePercent { get; set; }

        public string CNY003_Note { get; set; }
        public Int32 SortOrderNode { get; set; }
        public Int64 BoMFiColorPK { get; set; }
        public Int64 CNY00050PKFi { get; set; }

        
    }

    public class VisibleColBomInfo
    {
        private string _fieldName = "";
        public string FieldName { get { return this._fieldName; } set { this._fieldName = value; } }

        private bool _visible = false;
        public bool Visible { get { return this._visible; } set { this._visible = value; } }

        private bool _showInCustomizationForm = false;
        public bool ShowInCustomizationForm { get { return this._showInCustomizationForm; } set { this._showInCustomizationForm = value; } }
        public VisibleColBomInfo()
        {

        }

        public VisibleColBomInfo(string fieldName, bool visible, bool showInCustomizationForm=false)
        {
            _fieldName = fieldName;
            _visible = visible;
            _showInCustomizationForm = showInCustomizationForm;

        }

    }


    public class SOParaBoMFinishingInfo
    {
        private Int64 _cny00019Pk = 0;
        public Int64 CNY00019PK { get { return this._cny00019Pk; } set { this._cny00019Pk = value; } }

        private string _OrderNo = "";
        public string OrderNo { get { return this._OrderNo; } set { this._OrderNo = value; } }


        public SOParaBoMFinishingInfo()
        {

        }

        public SOParaBoMFinishingInfo(Int64 cny00019Pk, string orderNo)
        {
            _cny00019Pk = cny00019Pk;
            _OrderNo = orderNo;

        }






    }
    public class AttributeHeaderInfo
    {
        public string AttibuteName { get; set; }
        public DataAttType DataType { get; set; }
        public Int32 ColumnIndex { get; set; }

        public string AttibuteCode { get; set; }


    }
    public class BoMCompactAttInfo
    {
        public Int64 AttibutePK { get; set; }
        public string AttibuteName { get; set; }
        public DataAttType DataType { get; set; }
        public Int32 ColumnIndex { get; set; }

    }

    public class BoMInputAttInfoCompact
    {
      
        public string AttibuteName { get; set; }
        public string AttibuteValue { get; set; }

    }


    public class BoMInputAttPInfo
    {
        public Int64 AttibutePK { get; set; }
        public string AttibuteName { get; set; }
        public string AttibuteValue { get; set; }

        public string AttibuteValuePrint { get; set; }

        

    }


    public class BoMInputAttInfo
    {
        public Int64 AttibutePK { get; set; }
        public string AttibuteCode { get; set; }
        public string AttibuteName { get; set; }
        public string AttibuteValueFull { get; set; }

        public string AttibuteValueTemp { get; set; }

        public string AttibuteUnit { get; set; }

        public bool IsNumber { get; set; }

        //   public Int64 CNY00010PK { get; set; }
        public Int32 ColumnIndex { get; set; }



        public Int64 PK { get; set; }
        public DataStatus RowState { get; set; }//Unchange, Insert -- Update

        public bool IsPacking { get; set; }
        public bool CombineName { get; set; }

        //public bool AttributeFixed { get; set; }


        //public bool NoLoadBOM { get; set; }

    }

    public class AttItemFixInfo
    {
        public bool AttributeFixed { get; set; }


        public bool NoLoadBOM { get; set; }
    }
    public class FinishingColorCopyInfo
    {
        public Int64 CNY00050PKDes { get; set; }


        public Int64 CNY00050PKSou { get; set; }
    }

    public class DataColumnLayoutInfo
    {
        private string _fieldName = "";
        public string FieldName { get { return this._fieldName; } set { this._fieldName = value; } }

        private bool _visible = false;
        public bool Visible { get { return this._visible; } set { this._visible = value; } }

        private int _visibleIndexLoad = -1;
        public int VisibleIndexLoad { get { return this._visibleIndexLoad; } set { this._visibleIndexLoad = value; } }


        private int _visibleIndexCurrent = -1;
        public int VisibleIndexCurrent { get { return this._visibleIndexCurrent; } set { this._visibleIndexCurrent = value; } }


        private int _width = 0;
        public int Width { get { return this._width; } set { this._width = value; } }

        public DataColumnLayoutInfo()
        {

        }

        public DataColumnLayoutInfo(string fieldName, bool visible, int visibleIndexLoad, int visibleIndexCurrent,int width)
        {
            _fieldName = fieldName;
            _visible = visible;
            _visibleIndexLoad = visibleIndexLoad;
            _visibleIndexCurrent = visibleIndexCurrent;
            _width = width;

        }

    }

}
