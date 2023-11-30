using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys.Class;
using CNY_BaseSys.Info;
using DevExpress.Spreadsheet.Formulas;
using DevExpress.Spreadsheet.Functions;

namespace CNY_BaseSys.Common
{
  

    internal class Ctrl_MRPProgress
    {
        private readonly Inf_Progress _inf = new Inf_Progress();
        private Int64 _cny00019Pk = 0;
        private Int64 _cny00012Pk = 0;
        private Int64 _prHeaderPk = 0;
        public Int64 Cny00019Pk
        {
            get { return this._cny00019Pk; }
        }
        public Int64 Cny00012Pk
        {
            get { return this._cny00012Pk; }
        }
        public Int64 PrHeaderPk
        {
            get { return this._prHeaderPk; }
        }
        public Dictionary<Int64, PrNsoInfo> DicSoLine
        {
            get { return this._dicSoLine; }
        }
        private Dictionary<Int64, PrNsoInfo> _dicSoLine = new Dictionary<Int64, PrNsoInfo>(); // Key PKCHild


        private readonly FormulaEngine _engine;
        private readonly DataTable _dtShortCut;

        private Dictionary<Int64, MrpRoundedInfo> _dicDecimalRound = new Dictionary<Int64, MrpRoundedInfo>();
        public Dictionary<Int64, MrpRoundedInfo> DicDecimalRound
        {
            get { return this._dicDecimalRound; }
        }

        private Dictionary<string, decimal> _dicReleasePO = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> DicReleasePO
        {
            get { return this._dicReleasePO; }
        }



        private Dictionary<string, List<BoMInputAttInfo>> _dicAttValue = new Dictionary<string, List<BoMInputAttInfo>>(); // Key PKCHild
        public Dictionary<string, List<BoMInputAttInfo>> DicAttValue
        {
            get { return this._dicAttValue; }
        }
        private List<SoBoMDemandNormalInfo> _lDemand = new List<SoBoMDemandNormalInfo>();
        public List<SoBoMDemandNormalInfo> LDemand
        {
            get { return this._lDemand; }
        }

        private Dictionary<string, AttCrossTemp1Info> _dicAtt1 = new Dictionary<string, AttCrossTemp1Info>();



        public Ctrl_MRPProgress(Int64 cny00019Pk, Int64 cny00012Pk, Int64 prHeaderPk, FormulaEngine engine, DataTable dtShortCut)
        {
            this._engine = engine;
            this._dtShortCut = dtShortCut;
            this._cny00019Pk = cny00019Pk;
            this._cny00012Pk = cny00012Pk;
            this._prHeaderPk = prHeaderPk;
         
        }

        private decimal GetPoReleaseQty(Int64 cny00055pk,Int64 cny00020PK)
        {
            if (cny00055pk <= 0) return 0;
            decimal poReleaseQty = 0;
            string key = string.Format("{0}-{1}", cny00055pk, cny00020PK);
            if (!_dicReleasePO.TryGetValue(key, out poReleaseQty)) return 0;
            return poReleaseQty;
        }
       
        public DataSet GetData()
        {
            _dicAtt1.Clear();
            _dicReleasePO.Clear();
            _lDemand.Clear();
            _dicAttValue.Clear();
            DataSet dsReturn = new DataSet("DATAMRP");
            DataTable dtErrCode = new DataTable();
            dtErrCode.Columns.Add("ErrCode", typeof(int));
            string sql = " SELECT a.PK AS CNY00001PK,MAX(ISNULL(b.CNY002, 2)) AS DecimalRound,MIN(ISNULL(b.CNY003,0)) AS RoundRule,MIN(ISNULL(b.CNY004,0)) AS RoundType FROM dbo.CNY00001 a LEFT JOIN dbo.CNY00001A b ON a.PK = b.CNY00001PK AND b.CNY001 = 'PR' ";
            sql += " WHERE a.CNY001 <> '' AND a.CNY004 = 0 GROUP BY a.PK ";
            DataTable dtDecimalRound = _inf.TblExcuteSqlText(sql);
            _dicDecimalRound = dtDecimalRound.AsEnumerable().ToDictionary(p => p.Field<Int64>("CNY00001PK"),
                p => new MrpRoundedInfo(p.Field<int>("DecimalRound"), p.Field<int>("RoundRule"), p.Field<int>("RoundType")));
         
            DataSet dsLoad = _inf.GetDetailDataWhenLoadNew(_cny00019Pk, _cny00012Pk, _prHeaderPk);
            DataTable dtPr = dsLoad.Tables[0];
            DataTable dtBoMFinishing = dsLoad.Tables[1];
            DataTable dtBomNormal = dsLoad.Tables[2];
            DataTable dtAreaPaint = dsLoad.Tables[3];
            DataTable dtSoTemp = dsLoad.Tables[4];

            _dicReleasePO = dsLoad.Tables[5].AsEnumerable().ToDictionary(p => string.Format("{0}-{1}", p.Field<Int64>("CNY00055PK"), p.Field<Int64>("CNY00020PK")), p => p.Field<decimal>("POQuantity"));





            dsLoad.RemoveAllDataTableOnDataSet();
            _dicSoLine = dtSoTemp.AsEnumerable().Select(p => new
            {
                CNY00020PK = p.Field<Int64>("CNY00020PK"),
                BOMID = p.Field<Int64>("BOMID"),
                FactorProduct = p.Field<int>("FactorProduct"),
                OrderQuantity = p.Field<int>("OrderQuantity"),
                ProductCode = p.Field<string>("ProductCode"),
                Reference = p.Field<string>("Reference"),
                SortOrderNode = p.Field<int>("SortOrderNode"),
                TDG00001PK = p.Field<Int64>("TDG00001PK"),
                ProDimension = p.Field<string>("ProDimension"),
                RootSOQty = p.Field<int>("RootSOQty"),
                ProductName = p.Field<string>("ProductName"),

            }).Distinct().OrderBy(p => p.SortOrderNode).ToDictionary(p => p.CNY00020PK, p => new PrNsoInfo
            {
                BOMID = p.BOMID,
                CNY00020PK = p.CNY00020PK,
                FactorProduct = p.FactorProduct,
                OrderQuantity = p.OrderQuantity,
                ProductCode = p.ProductCode,
                Reference = p.Reference,
                SortOrderNode = p.SortOrderNode,
                TDG00001PK = p.TDG00001PK,
                ProDimension = p.ProDimension,
                RootSOQty = p.RootSOQty,
                ProductName = p.ProductName,

            });
            if (DicSoLine.Count <= 0)
            {
                dtErrCode.Rows.Add(0);
                dsReturn.Tables.Add(dtErrCode);
                return dsReturn;
            }
            dtErrCode.Rows.Add(1);

 
            var qPrNormal = dtPr.AsEnumerable().Where(p => !p.Field<bool>("IsFinishing")).Select(p => new
            {
                TDG00001PK = p.Field<Int64>("TDG00001PK"),
                CNY00002PK = p.Field<Int64>("CNY00002PK"),
                UC = p.Field<decimal>("UC"),
                UCUnitCode = p.Field<string>("UCUnitCode"),
                Waste = p.Field<decimal>("Waste"),
                PercentUsing = p.Field<decimal>("PercentUsing"),
                RateGram = p.Field<decimal>("RateGram"),
                CNY00020PK = p.Field<Int64>("CNY00020PK"),
                CNY00016PK = p.Field<Int64>("CNY00016PK"),
                OrderQuantity = p.Field<int>("OrderQuantity"),
                CNY00050PK = p.Field<Int64>("CNY00050PK"),
                QualityCode = p.Field<string>("QualityCode"),
                TDG00004PK = p.Field<Int64>("TDG00004PK"),
                CNY00050PKFi = p.Field<Int64>("CNY00050PKFi"),
                AreaPaint = p.Field<decimal>("AreaPaint"),
                FactorProduct = p.Field<int>("FactorProduct"),
                CancelLine = p.Field<bool>("CancelLine"),
                CancelFactor = p.Field<int>("CancelFactor"),
                IsFinishing = p.Field<bool>("IsFinishing"),
                PRQty = p.Field<decimal>("PRQty"),
                PRQtyS = p.Field<decimal>("PRQtyS"),
                CNY00056PK = p.Field<Int64>("CNY00056PK"),
                CNY00055PK = p.Field<Int64>("CNY00055PK"),
                StockQty = p.Field<decimal>("StockQty"),
                PackingFactor = p.Field<decimal>("PackingFactor"),
                OldDemandS = p.Field<decimal>("OldDemandS"),
                OldDemandD = p.Field<decimal>("OldDemandD"),
                CNY00004PK = p.Field<Int64>("CNY00004PK"),
                Status = p.Field<int>("Status"),
                Version = p.Field<int>("Version"),
                CreatedDate = p.Field<string>("CreatedDate"),
                CreatedBy = p.Field<string>("CreatedBy"),
                UpdatedDate = p.Field<string>("UpdatedDate"),
                UpdatedBy = p.Field<string>("UpdatedBy"),
                SubmittedBy = p.Field<string>("SubmittedBy"),
                SubmittedDate = p.Field<string>("SubmittedDate"),
                ConfirmedBy = p.Field<string>("ConfirmedBy"),
                ConfirmedDate = p.Field<string>("ConfirmedDate"),
                ApprovedBy = p.Field<string>("ApprovedBy"),
                ApprovedDate = p.Field<string>("ApprovedDate"),
                RejectedBy = p.Field<string>("RejectedBy"),
                RejectedDate = p.Field<string>("RejectedDate"),
                IsGrouping = p.Field<bool>("IsGrouping"),
                IsFormula = p.Field<bool>("IsFormula"),
                IsPacking = p.Field<bool>("IsPacking"),
                IsUnAllocate = p.Field<bool>("IsUnAllocate"),
                UnAllocateQty = p.Field<decimal>("UnAllocateQty"),
                ColorReference = p.Field<string>("ColorReference"),
                Supplier = p.Field<string>("Supplier"),
                UCUnit = p.Field<string>("UCUnit"),
                SupplierRef = p.Field<string>("SupplierRef"),
                CNY00001PK = p.Field<Int64>("CNY00001PK"),
                RMCode_001 = p.Field<string>("RMCode_001"),
                RMDescription_002 = p.Field<string>("RMDescription_002"),
                MainMaterialGroup = p.Field<string>("MainMaterialGroup"),
                BookingQuantity = p.Field<decimal>("BookingQuantity"),

                RoundedInfo = _dicDecimalRound[p.Field<Int64>("CNY00001PK")],
                FormulaString = p.Field<string>("FormulaString"),
                PurchaseUnit_010 = p.Field<string>("PurchaseUnit_010"),
                IsRMStandardization = p.Field<int>("IsRMStandardization"),
                IsPRissuedTypechecking = p.Field<bool>("IsPRissuedTypechecking"),
                LeadTime = p.Field<int>("LeadTime"),

            }).ToList();
            DateTime dServer = ProcessGeneral.GetServerDate();
            string defaultCurrentDate = dServer.ToString(ConstSystem.SysDateFormat);
            var qBoMRm = dtBomNormal.AsEnumerable().Where(p => p.Field<string>("ItemType") == "R").Select(p => new
            {
                CNY00020PK = p.Field<Int64>("CNY00020PK"),
                CNY00016PK = p.Field<Int64>("CNY00016PK"),
                MainMaterialGroup = p.Field<string>("MainMaterialGroup"),
                TDG00001PK = p.Field<Int64>("TDG00001PK"),
                RMCode_001 = p.Field<string>("RMCode_001"),
                RMDescription_002 = p.Field<string>("RMDescription_002"),
                ColorReference = p.Field<string>("ColorReference"),
                CNY00002PK = p.Field<Int64>("CNY00002PK"),
                Supplier = p.Field<string>("Supplier"),
                UC = p.Field<decimal>("UC"),
                UCUnitCode = p.Field<string>("UCUnitCode"),
                UCUnit = p.Field<string>("UCUnit"),
                Waste = p.Field<decimal>("Waste"),
                PercentUsing = p.Field<decimal>("PercentUsing"),
                OrderQuantity = p.Field<int>("OrderQuantity"),
                CNY00050PK = p.Field<Int64>("CNY00050PK"),
                Position = p.Field<string>("Position"),
                Factor = p.Field<decimal>("Factor"),
                SupplierRef = p.Field<string>("SupplierRef"),
                TDG00004PK = p.Field<Int64>("TDG00004PK"),
                RoundedInfo = _dicDecimalRound[p.Field<Int64>("CNY00001PK")],
                FactorProduct = p.Field<int>("FactorProduct"),
                CancelLine = p.Field<bool>("CancelLine"),
                CancelFactor = p.Field<int>("CancelFactor"),
                CNY00001PK = p.Field<Int64>("CNY00001PK"),
                PurchaseUnit_010 = p.Field<string>("PurchaseUnit_010"),
                IsRMStandardization = p.Field<int>("IsRMStandardization"),
                FormulaString = p.Field<string>("FormulaString"),
                IsPRissuedTypechecking = p.Field<bool>("IsPRissuedTypechecking"),
                BOMStatus = p.Field<int>("BOMStatus"),
                PurchaseType = p.Field<int>("PurchaseType"),
                Reference = p.Field<string>("Reference"),
                ProDimension = p.Field<string>("ProDimension"),
                RootSOQty = p.Field<int>("RootSOQty"),
                CNY00016PKParent = p.Field<Int64>("CNY00016PKParent"),
                TDG00001PKSO = p.Field<Int64>("TDG00001PKSO"),
                ProductCode = p.Field<string>("ProductCode"),
                ProductName = p.Field<string>("ProductName"),
                LeadTime = p.Field<int>("LeadTime"),
                CNY00016PKREP = p.Field<Int64>("CNY00016PKREP"),
                IsDelete = p.Field<bool>("IsDelete"),
            }).ToList();
            Int64 rowIndex = 0;



            List<SoBoMDemandNormalInfo> lDemandNormal = qBoMRm.GroupJoin(qPrNormal,
                  p => new
                  {
                      p.CNY00016PK,
                      p.CNY00020PK
                  },
                  t => new
                  {
                      t.CNY00016PK,
                      t.CNY00020PK
                  }, (p, j) => new { p, j })
                  .SelectMany(@t1 => @t1.j.DefaultIfEmpty(), (@t1, x) => new { @t1, x }) //  .Where(@t1 => @t1.x == null)
                  .Where(@t2 => @t2.x != null || (
                       //Math.Round(@t2.@t1.p.CancelFactor * @t2.@t1.p.OrderQuantity * @t2.@t1.p.UC * @t2.@t1.p.FactorProduct * (1 + @t2.@t1.p.Waste / 100) * @t2.@t1.p.PercentUsing / 100, @t2.@t1.p.DecimalRound) > 0 &&
                       (@t2.@t1.p.BOMStatus == 2 || @t2.@t1.p.BOMStatus == 3 || @t2.@t1.p.BOMStatus == 4)))
                  .Select(@t2 => new SoBoMDemandNormalInfo
                  {
                      PKRow = ++rowIndex,
                      MainMaterialGroup = @t2.x == null ? @t2.@t1.p.MainMaterialGroup : @t2.x.MainMaterialGroup,
                      TDG00001PK = @t2.x == null ? @t2.@t1.p.TDG00001PK : @t2.x.TDG00001PK,
                      RMCode_001 = @t2.x == null ? @t2.@t1.p.RMCode_001 : @t2.x.RMCode_001,
                      RMDescription_002 = @t2.x == null ? @t2.@t1.p.RMDescription_002 : @t2.x.RMDescription_002,
                      ColorReference = @t2.@t1.p.ColorReference,
                      CNY00002PK = @t2.@t1.p.CNY00002PK,
                      Supplier = @t2.@t1.p.Supplier,
                      UC = @t2.@t1.p.UC,
                      UCUnitCode = @t2.@t1.p.UCUnitCode,
                      UCUnit = @t2.@t1.p.UCUnit,
                      Waste = @t2.@t1.p.Waste,
                      PercentUsing = @t2.@t1.p.PercentUsing,
                      RateGram = 0,
                      CNY00020PK = @t2.@t1.p.CNY00020PK,
                      CNY00016PK = @t2.@t1.p.CNY00016PK,
                      OrderQuantity = @t2.@t1.p.OrderQuantity,
                      CNY00050PK = @t2.@t1.p.CNY00050PK,
                      Position = @t2.@t1.p.Position,
                      Factor = @t2.@t1.p.Factor,
                      QualityCode = "",
                      SupplierRef = @t2.@t1.p.SupplierRef,
                      TDG00004PK = @t2.@t1.p.TDG00004PK,
                      CNY00050PKFi = 0,
                      AreaPaint = 0,
                      DecimalRound = @t2.x == null ? @t2.@t1.p.RoundedInfo.DecimalRound : @t2.x.RoundedInfo.DecimalRound,
                      RoundRule = @t2.x == null ? @t2.@t1.p.RoundedInfo.RoundRule : @t2.x.RoundedInfo.RoundRule,
                      RoundType = @t2.x == null ? @t2.@t1.p.RoundedInfo.RoundType : @t2.x.RoundedInfo.RoundType,
                      FactorProduct = @t2.@t1.p.FactorProduct,
                      CancelLine = @t2.@t1.p.CancelLine,
                      CancelFactor = @t2.@t1.p.CancelFactor,
                      IsFinishing = false,
                      TotalDemandQty = MrpRounded(
                          (@t2.x == null ? 1 : @t2.x.PackingFactor) *
                                                                   MrpRounded(@t2.@t1.p.CancelFactor * @t2.@t1.p.OrderQuantity * @t2.@t1.p.UC * @t2.@t1.p.FactorProduct * (1 + @t2.@t1.p.Waste / 100) * @t2.@t1.p.PercentUsing / 100,
                                                                       (@t2.x == null ? @t2.@t1.p.RoundedInfo : @t2.x.RoundedInfo), true),
                          (@t2.x == null ? @t2.@t1.p.RoundedInfo : @t2.x.RoundedInfo), (@t2.x != null && @t2.x.PackingFactor != 1)),
                      IsReleasePR = @t2.x != null,
                      ColorReference_PR = @t2.x == null ? @t2.@t1.p.ColorReference : @t2.x.ColorReference,
                      CNY00002PK_PR = @t2.x == null ? @t2.@t1.p.CNY00002PK : @t2.x.CNY00002PK,
                      Supplier_PR = @t2.x == null ? @t2.@t1.p.Supplier : @t2.x.Supplier,
                      UC_PR = @t2.x == null ? @t2.@t1.p.UC : @t2.x.UC,
                      UCUnitCode_PR = @t2.x == null ? @t2.@t1.p.UCUnitCode : @t2.x.UCUnitCode,
                      UCUnit_PR = @t2.x == null ? @t2.@t1.p.UCUnit : @t2.x.UCUnit,
                      Waste_PR = @t2.x == null ? @t2.@t1.p.Waste : @t2.x.Waste,
                      PercentUsing_PR = @t2.x == null ? @t2.@t1.p.PercentUsing : @t2.x.PercentUsing,
                      RateGram_PR = 0,
                      OrderQuantity_PR = @t2.x == null ? @t2.@t1.p.OrderQuantity : @t2.x.OrderQuantity,
                      CNY00050PK_PR = @t2.x == null ? @t2.@t1.p.CNY00050PK : @t2.x.CNY00050PK,
                      SupplierRef_PR = @t2.x == null ? @t2.@t1.p.SupplierRef : @t2.x.SupplierRef,
                      TDG00004PK_PR = @t2.x == null ? @t2.@t1.p.TDG00004PK : @t2.x.TDG00004PK,
                      AreaPaint_PR = 0,
                      FactorProduct_PR = @t2.x == null ? @t2.@t1.p.FactorProduct : @t2.x.FactorProduct,
                      CancelLine_PR = @t2.x == null ? @t2.@t1.p.CancelLine : @t2.x.CancelLine,
                      CancelFactor_PR = @t2.x == null ? @t2.@t1.p.CancelFactor : @t2.x.CancelFactor,
                      IsFinishing_PR = false,
                      PRQty_PR = @t2.x == null ? MrpRounded(@t2.@t1.p.CancelFactor * @t2.@t1.p.OrderQuantity * @t2.@t1.p.UC * @t2.@t1.p.FactorProduct * (1 + @t2.@t1.p.Waste / 100) * @t2.@t1.p.PercentUsing / 100,
                          @t2.@t1.p.RoundedInfo, true) : @t2.x.PRQty,
                      CNY00056PK_PR = @t2.x == null ? 0 : @t2.x.CNY00056PK,
                      CNY00055PK_PR = @t2.x == null ? 0 : @t2.x.CNY00055PK,
                      PRQtyS_PR = @t2.x == null ? 0 : @t2.x.PRQtyS,
                      StockQty_PR = @t2.x == null ? 0 : @t2.x.StockQty,
                      AttributeLinkPK = @t2.x == null ? @t2.@t1.p.CNY00016PK.ToString().Trim() + "$$BM" : @t2.x.CNY00055PK.ToString().Trim() + "$$PR",
                      CNY00001PK = @t2.x == null ? @t2.@t1.p.CNY00001PK : @t2.x.CNY00001PK,
                      PackingFactor = @t2.x == null ? 1 : @t2.x.PackingFactor,
                      PurchaseUnit_010 = @t2.x == null ? @t2.@t1.p.PurchaseUnit_010 : @t2.x.PurchaseUnit_010,
                      OldDemandS = @t2.x == null ? 0 : @t2.x.OldDemandS,
                      OldDemandD = @t2.x == null ? 0 : @t2.x.OldDemandD,
                      IsRMStandardization = @t2.x == null ? @t2.@t1.p.IsRMStandardization : @t2.x.IsRMStandardization,
                      FormulaString = @t2.x == null ? @t2.@t1.p.FormulaString : @t2.x.FormulaString,
                      CNY00004PK = @t2.x == null ? 0 : @t2.x.CNY00004PK,
                      IsPRissuedTypechecking = @t2.x == null ? @t2.@t1.p.IsPRissuedTypechecking : @t2.x.IsPRissuedTypechecking,
                      IsRightRM = true,
                      BOMStatus = @t2.@t1.p.BOMStatus,
                      PurchaseType = @t2.@t1.p.PurchaseType,
                      Status = @t2.x == null ? 1 : @t2.x.Status,
                      Version = @t2.x == null ? 0 : @t2.x.Version,
                      CreatedDate = @t2.x == null ? defaultCurrentDate : @t2.x.CreatedDate,
                      CreatedBy = @t2.x == null ? DeclareSystem.SysUserName.ToUpper() : @t2.x.CreatedBy,
                      UpdatedDate = @t2.x == null ? defaultCurrentDate : @t2.x.UpdatedDate,
                      UpdatedBy = @t2.x == null ? DeclareSystem.SysUserName.ToUpper() : @t2.x.UpdatedBy,
                      SubmittedBy = @t2.x == null ? "N/A" : @t2.x.SubmittedBy,
                      SubmittedDate = @t2.x == null ? "01/01/1900" : @t2.x.SubmittedDate,
                      ConfirmedBy = @t2.x == null ? "N/A" : @t2.x.ConfirmedBy,
                      ConfirmedDate = @t2.x == null ? "01/01/1900" : @t2.x.ConfirmedDate,
                      ApprovedBy = @t2.x == null ? "N/A" : @t2.x.ApprovedBy,
                      ApprovedDate = @t2.x == null ? "01/01/1900" : @t2.x.ApprovedDate,
                      RejectedBy = @t2.x == null ? "N/A" : @t2.x.RejectedBy,
                      RejectedDate = @t2.x == null ? "01/01/1900" : @t2.x.RejectedDate,
                      Reference = @t2.@t1.p.Reference,
                      ProDimension = @t2.@t1.p.ProDimension,
                      RootSOQty = @t2.@t1.p.RootSOQty,
                      CNY00016PKParent = @t2.@t1.p.CNY00016PKParent,
                      TDG00001PKSO = @t2.@t1.p.TDG00001PKSO,
                      ProductCode = @t2.@t1.p.ProductCode,
                      ProductName = @t2.@t1.p.ProductName,
                      CNYMF016PK = 0,
                      IsGrouping = @t2.x == null ? false : @t2.x.IsGrouping,
                      IsFormula = @t2.x == null ? false : @t2.x.IsFormula,
                      IsPacking = @t2.x == null ? false : @t2.x.IsPacking,
                      IsUnAllocate = @t2.x == null ? false : @t2.x.IsUnAllocate,
                      UnAllocateQty = @t2.x == null ? 0 : @t2.x.UnAllocateQty,
                      LeadTime = @t2.x == null ? @t2.@t1.p.LeadTime : @t2.x.LeadTime,
                      IsDelete = @t2.@t1.p.IsDelete,
                      PK55SAVE = @t2.x == null ? 0 : @t2.x.CNY00055PK,
                      AssemblyComponent = "",
                      BookingQuantity = @t2.x == null ? 0 : @t2.x.BookingQuantity,
                      Color = "",
                      Route = "",
                      ChangeDemand = 0,
                      TDG00001PKBOM = @t2.@t1.p.TDG00001PK,
                      RMDescription_002BOM = @t2.@t1.p.RMDescription_002,
                      DIFFTDG00001PK = @t2.x == null || @t2.@t1.p.TDG00001PK == @t2.x.TDG00001PK ? 0 : 1,
                      CNY00016PKREP = @t2.@t1.p.CNY00016PKREP,
                  }).Where(p=>p.PurchaseType != 1).ToList();

      
            
        

            var qPrFinishing = dtPr.AsEnumerable().Where(p => p.Field<bool>("IsFinishing")).Select(p => new
            {
                TDG00001PK = p.Field<Int64>("TDG00001PK"),
                ColorReference = p.Field<string>("ColorReference"),
                CNY00002PK = p.Field<Int64>("CNY00002PK"),
                Supplier = p.Field<string>("Supplier"),
                UC = p.Field<decimal>("UC"),
                UCUnitCode = p.Field<string>("UCUnitCode"),
                UCUnit = p.Field<string>("UCUnit"),
                Waste = p.Field<decimal>("Waste"),
                PercentUsing = p.Field<decimal>("PercentUsing"),
                RateGram = p.Field<decimal>("RateGram"),
                CNY00020PK = p.Field<Int64>("CNY00020PK"),
                CNY00016PK = p.Field<Int64>("CNY00016PK"),
                OrderQuantity = p.Field<int>("OrderQuantity"),
                CNY00050PK = p.Field<Int64>("CNY00050PK"),
                QualityCode = p.Field<string>("QualityCode"),
                SupplierRef = p.Field<string>("SupplierRef"),
                TDG00004PK = p.Field<Int64>("TDG00004PK"),
                CNY00050PKFi = p.Field<Int64>("CNY00050PKFi"),
                AreaPaint = p.Field<decimal>("AreaPaint"),
                FactorProduct = p.Field<int>("FactorProduct"),
                CancelLine = p.Field<bool>("CancelLine"),
                CancelFactor = p.Field<int>("CancelFactor"),
                IsFinishing = p.Field<bool>("IsFinishing"),
                PRQty = p.Field<decimal>("PRQty"),
                CNY00056PK = p.Field<Int64>("CNY00056PK"),
                CNY00055PK = p.Field<Int64>("CNY00055PK"),
                PRQtyS = p.Field<decimal>("PRQtyS"),
                StockQty = p.Field<decimal>("StockQty"),
                PackingFactor = p.Field<decimal>("PackingFactor"),
                OldDemandS = p.Field<decimal>("OldDemandS"),
                OldDemandD = p.Field<decimal>("OldDemandD"),
                CNY00004PK = p.Field<Int64>("CNY00004PK"),
                Status = p.Field<int>("Status"),
                Version = p.Field<int>("Version"),
                CreatedDate = p.Field<string>("CreatedDate"),
                CreatedBy = p.Field<string>("CreatedBy"),
                UpdatedDate = p.Field<string>("UpdatedDate"),
                UpdatedBy = p.Field<string>("UpdatedBy"),
                SubmittedBy = p.Field<string>("SubmittedBy"),
                SubmittedDate = p.Field<string>("SubmittedDate"),
                ConfirmedBy = p.Field<string>("ConfirmedBy"),
                ConfirmedDate = p.Field<string>("ConfirmedDate"),
                ApprovedBy = p.Field<string>("ApprovedBy"),
                ApprovedDate = p.Field<string>("ApprovedDate"),
                RejectedBy = p.Field<string>("RejectedBy"),
                RejectedDate = p.Field<string>("RejectedDate"),
                IsGrouping = p.Field<bool>("IsGrouping"),
                IsFormula = p.Field<bool>("IsFormula"),
                IsPacking = p.Field<bool>("IsPacking"),
                IsUnAllocate = p.Field<bool>("IsUnAllocate"),
                UnAllocateQty = p.Field<decimal>("UnAllocateQty"),
                CNY00001PK = p.Field<Int64>("CNY00001PK"),
                RMCode_001 = p.Field<string>("RMCode_001"),
                RMDescription_002 = p.Field<string>("RMDescription_002"),
                MainMaterialGroup = p.Field<string>("MainMaterialGroup"),
                BookingQuantity = p.Field<decimal>("BookingQuantity"),
                RoundedInfo = _dicDecimalRound[p.Field<Int64>("CNY00001PK")],
                FormulaString = p.Field<string>("FormulaString"),
                PurchaseUnit_010 = p.Field<string>("PurchaseUnit_010"),
                IsRMStandardization = p.Field<int>("IsRMStandardization"),
                IsPRissuedTypechecking = p.Field<bool>("IsPRissuedTypechecking"),
                LeadTime = p.Field<int>("LeadTime"),
            }).ToList();

            var qFinishingTempBoM = dtBoMFinishing.AsEnumerable()
                .Where(p => p.Field<string>("TableCode") == "TDG00001").Select(p => new
                {

                    TDG00001PK = p.Field<Int64>("TDG00001PK"),
                    CNY00002PK = p.Field<Int64>("CNY00002PK"),
                    CNY001 = p.Field<decimal>("CNY001"),
                    UCUnitCode = p.Field<string>("UCUnitCode"),
                    RateGram = p.Field<decimal>("RateGram"),
                    CNY00050PK = p.Field<Int64>("CNY00050PK"),
                    CNY00012PK = p.Field<Int64>("CNY00012PK"),
                    QualityCode = p.Field<string>("QualityCode"),
                    TDG00004PK = p.Field<Int64>("TDG00004PK"),
                    CNY00050PKFi = p.Field<Int64>("CNY00050PKFi"),
                    CNY002 = p.Field<decimal>("CNY002"),
                    PK = p.Field<Int64>("PK"),
                    CancelLine = p.Field<bool>("CancelLine"),
                    Factor = p.Field<decimal>("Factor"),
                    StatusFi = p.Field<int>("StatusFi"),
                    PurchaseType = p.Field<int>("PurchaseType"),
                    ParentPK = p.Field<Int64>("ParentPK"),
                    CNYMF016PK = p.Field<Int64>("CNYMF016PK"),
                    IsGrouping = false,
                    IsFormula = false,
                    IsDelete = false,
                    MainMaterialGroup = p.Field<string>("MainMaterialGroup"),
                    RMCode_001 = p.Field<string>("RMCode_001"),
                    RMDescription_002 = p.Field<string>("RMDescription_002"),
                    ColorReference = p.Field<string>("ColorReference"),
                    Supplier = p.Field<string>("Supplier"),
                    UCUnitDesc = p.Field<string>("UCUnit"),
                    SupplierRef = p.Field<string>("SupplierRef"),
                    CNY00001PK = p.Field<Int64>("CNY00001PK"),
                    PurchaseUnit_010 = p.Field<string>("PurchaseUnit_010"),
                    IsRMStandardization = p.Field<int>("IsRMStandardization"),
                    FormulaString = p.Field<string>("FormulaString"),
                    IsPRissuedTypechecking = p.Field<bool>("IsPRissuedTypechecking"),
                    LeadTime = p.Field<int>("LeadTime"),
                    Color = p.Field<string>("Color"),
                    Route = p.Field<string>("Route"),
                    CNY00016PKREP = p.Field<Int64>("CNY00016PKREP"),
                }).ToList();
            var qFinishingTempError = qPrFinishing.Join(qFinishingTempBoM, p => new
            {
                CNY00016PK = p.CNY00016PK
            }, t => new
            {
                CNY00016PK = t.PK
            }, (p, t) => new
            {
                p,
                t
            }).Where(s => s.p.QualityCode != s.t.QualityCode
                          || s.p.CNY00050PKFi != s.t.CNY00050PKFi).Select(s => new
                          {
                              s.t.TDG00001PK,
                              s.t.CNY00002PK,
                              CNY001 = s.p.UC,
                              s.t.UCUnitCode,
                              s.p.RateGram,
                              s.t.CNY00050PK,
                              s.t.CNY00012PK,
                              s.p.QualityCode,
                              s.t.TDG00004PK,
                              s.p.CNY00050PKFi,
                              CNY002 = s.p.Waste,
                              s.t.PK,
                              s.p.CancelLine,
                              s.t.Factor,
                              s.t.StatusFi,
                              s.t.PurchaseType,
                              s.t.ParentPK,
                              s.t.CNYMF016PK,
                              IsGrouping = false,
                              IsFormula = false,
                              IsDelete = true,
                              s.t.MainMaterialGroup,
                              s.t.RMCode_001,
                              s.t.RMDescription_002,
                              s.t.ColorReference,
                              s.t.Supplier,
                              s.t.UCUnitDesc,
                              s.t.SupplierRef,
                              s.t.CNY00001PK,
                              s.t.PurchaseUnit_010,
                              s.t.IsRMStandardization,
                              s.t.FormulaString,
                              s.t.IsPRissuedTypechecking,
                              s.t.LeadTime,
                              s.t.Color,
                              s.t.Route,
                              s.t.CNY00016PKREP
                          }).Distinct().ToList();
            var qFinishingTemp = qFinishingTempBoM.Concat(qFinishingTempError).ToList();
            var qFinishingRm = _dicSoLine.Join(qFinishingTemp, p => p.Value.BOMID, t => t.CNY00012PK, (p, t) => new
            {
                SOINFOT = p.Value,
                PRINFOT = t
            }).GroupJoin(dtAreaPaint.AsEnumerable(),
                    p => new
                    {
                        CNY00020PK = p.SOINFOT.CNY00020PK,
                        QualityCode = p.PRINFOT.QualityCode,
                        CNY00050PKFi = p.PRINFOT.CNY00050PKFi,
                    },
                    t => new
                    {
                        CNY00020PK = t.Field<Int64>("CNY00020PK"),
                        QualityCode = t.Field<string>("QualityCode"),
                        CNY00050PKFi = t.Field<Int64>("FnishingColorPK")
                    }, (p, j) => new { p, j })
                .SelectMany(@t1 => @t1.j.DefaultIfEmpty(),
                    (@t1, x) => new { @t1, x }) //  .Where(@t1 => @t1.x == null)
                .Select(@t2 => new
                {
                    MainMaterialGroup = @t2.@t1.p.PRINFOT.MainMaterialGroup,
                    TDG00001PK = @t2.@t1.p.PRINFOT.TDG00001PK,
                    RMCode_001 = @t2.@t1.p.PRINFOT.RMCode_001,
                    RMDescription_002 = @t2.@t1.p.PRINFOT.RMDescription_002,
                    ColorReference = @t2.@t1.p.PRINFOT.ColorReference,
                    CNY00002PK = @t2.@t1.p.PRINFOT.CNY00002PK,
                    Supplier = @t2.@t1.p.PRINFOT.Supplier,
                    UC = @t2.@t1.p.PRINFOT.CNY001,
                    UCUnitCode = @t2.@t1.p.PRINFOT.UCUnitCode,
                    UCUnit = @t2.@t1.p.PRINFOT.UCUnitDesc,
                    Waste = @t2.@t1.p.PRINFOT.CNY002,
                    PercentUsing = (decimal)100,
                    RateGram = @t2.@t1.p.PRINFOT.RateGram,
                    CNY00020PK = @t2.@t1.p.SOINFOT.CNY00020PK,
                    CNY00016PK = @t2.@t1.p.PRINFOT.PK,
                    OrderQuantity = @t2.@t1.p.SOINFOT.OrderQuantity,
                    CNY00050PK = @t2.@t1.p.PRINFOT.CNY00050PK,
                    Position = "",
                    Factor = @t2.@t1.p.PRINFOT.Factor,
                    QualityCode = @t2.@t1.p.PRINFOT.QualityCode,
                    SupplierRef = @t2.@t1.p.PRINFOT.SupplierRef,
                    TDG00004PK = @t2.@t1.p.PRINFOT.TDG00004PK,
                    CNY00050PKFi = @t2.@t1.p.PRINFOT.CNY00050PKFi,
                    AreaPaint = @t2.x == null ? 0 : @t2.x.Field<decimal>("AreaPaint"),
                    RoundedInfo = _dicDecimalRound[@t2.@t1.p.PRINFOT.CNY00001PK],
                    FactorProduct = @t2.@t1.p.SOINFOT.FactorProduct,
                    CancelLine = @t2.@t1.p.PRINFOT.CancelLine,
                    CancelFactor = @t2.@t1.p.PRINFOT.CancelLine ? 0 : 1,
                    IsFinishing = true,
                    CNY00001PK = @t2.@t1.p.PRINFOT.CNY00001PK,
                    PurchaseUnit_010 = @t2.@t1.p.PRINFOT.PurchaseUnit_010,
                    IsRMStandardization = @t2.@t1.p.PRINFOT.IsRMStandardization,
                    FormulaString = @t2.@t1.p.PRINFOT.FormulaString,
                    IsPRissuedTypechecking = @t2.@t1.p.PRINFOT.IsPRissuedTypechecking,
                    //IsRightRM = GetRightRm(dicRightRm, @t2.@t1.p.PRINFOT.CNY00001PK),
                    BOMStatus = @t2.@t1.p.PRINFOT.StatusFi,
                    PurchaseType = @t2.@t1.p.PRINFOT.PurchaseType,
                    Reference = @t2.@t1.p.SOINFOT.Reference,
                    ProDimension = @t2.@t1.p.SOINFOT.ProDimension,
                    RootSOQty = @t2.@t1.p.SOINFOT.RootSOQty,
                    CNY00016PKParent = @t2.@t1.p.PRINFOT.ParentPK,
                    TDG00001PKSO = @t2.@t1.p.SOINFOT.TDG00001PK,
                    ProductCode = @t2.@t1.p.SOINFOT.ProductCode,
                    ProductName = @t2.@t1.p.SOINFOT.ProductName,
                    CNYMF016PK = @t2.@t1.p.PRINFOT.CNYMF016PK,
                    LeadTime = @t2.@t1.p.PRINFOT.LeadTime,
                    IsDelete = @t2.@t1.p.PRINFOT.IsDelete,
                    Color = @t2.@t1.p.PRINFOT.Color,
                    Route = @t2.@t1.p.PRINFOT.Route,
                    CNY00016PKREP = @t2.@t1.p.PRINFOT.CNY00016PKREP,
                }).ToList();
            List<SoBoMDemandNormalInfo> lDemandFinishing = qFinishingRm.GroupJoin(qPrFinishing,
                    p => new
                    {
                        p.CNY00016PK,
                        p.CNY00020PK,
                        p.QualityCode,
                        p.CNY00050PKFi
                    },
                    t => new
                    {
                        t.CNY00016PK,
                        t.CNY00020PK,
                        t.QualityCode,
                        t.CNY00050PKFi
                    }, (p, j) => new { p, j })
                .SelectMany(@t1 => @t1.j.DefaultIfEmpty(),
                    (@t1, x) => new { @t1, x }) //  .Where(@t1 => @t1.x == null)
                .Where(@t2 => @t2.x != null || (
                    MrpRounded(
                        @t2.@t1.p.CancelFactor * @t2.@t1.p.UC * (1 + @t2.@t1.p.Waste / 100) *
                        @t2.@t1.p.AreaPaint * @t2.@t1.p.FactorProduct * @t2.@t1.p.OrderQuantity *
                        @t2.@t1.p.RateGram / 100, @t2.@t1.p.RoundedInfo, true) > 0 &&
                    (@t2.@t1.p.BOMStatus == 2 || @t2.@t1.p.BOMStatus == 3 ||
                     @t2.@t1.p.BOMStatus == 4)))
                .Select(@t2 => new SoBoMDemandNormalInfo
                {
                    PKRow = ++rowIndex,
                    MainMaterialGroup = @t2.x == null ? @t2.@t1.p.MainMaterialGroup : @t2.x.MainMaterialGroup,
                    TDG00001PK = @t2.x == null ? @t2.@t1.p.TDG00001PK : @t2.x.TDG00001PK,
                    RMCode_001 = @t2.x == null ? @t2.@t1.p.RMCode_001 : @t2.x.RMCode_001,
                    RMDescription_002 = @t2.x == null ? @t2.@t1.p.RMDescription_002 : @t2.x.RMDescription_002,
                    ColorReference = @t2.@t1.p.ColorReference,
                    CNY00002PK = @t2.@t1.p.CNY00002PK,
                    Supplier = @t2.@t1.p.Supplier,
                    UC = @t2.@t1.p.UC,
                    UCUnitCode = @t2.@t1.p.UCUnitCode,
                    UCUnit = @t2.@t1.p.UCUnit,
                    Waste = @t2.@t1.p.Waste,
                    PercentUsing = @t2.@t1.p.PercentUsing,
                    RateGram = @t2.@t1.p.RateGram,
                    CNY00020PK = @t2.@t1.p.CNY00020PK,
                    CNY00016PK = @t2.@t1.p.CNY00016PK,
                    OrderQuantity = @t2.@t1.p.OrderQuantity,
                    CNY00050PK = @t2.@t1.p.CNY00050PK,
                    Position = @t2.@t1.p.Position,
                    Factor = @t2.@t1.p.Factor,
                    QualityCode = @t2.@t1.p.QualityCode,
                    SupplierRef = @t2.@t1.p.SupplierRef,
                    TDG00004PK = @t2.@t1.p.TDG00004PK,
                    CNY00050PKFi = @t2.@t1.p.CNY00050PKFi,
                    AreaPaint = @t2.@t1.p.AreaPaint,
                    DecimalRound = @t2.x == null ? @t2.@t1.p.RoundedInfo.DecimalRound : @t2.x.RoundedInfo.DecimalRound,
                    RoundRule = @t2.x == null ? @t2.@t1.p.RoundedInfo.RoundRule : @t2.x.RoundedInfo.RoundRule,
                    RoundType = @t2.x == null ? @t2.@t1.p.RoundedInfo.RoundType : @t2.x.RoundedInfo.RoundType,
                    FactorProduct = @t2.@t1.p.FactorProduct,
                    CancelLine = @t2.@t1.p.CancelLine,
                    CancelFactor = @t2.@t1.p.CancelFactor,
                    IsFinishing = true,
                    TotalDemandQty =
                        MrpRounded((@t2.x == null ? 1 : @t2.x.PackingFactor) *
                                                    MrpRounded(
                            @t2.@t1.p.CancelFactor * @t2.@t1.p.UC * (1 + @t2.@t1.p.Waste / 100) *
                            @t2.@t1.p.AreaPaint * @t2.@t1.p.FactorProduct * @t2.@t1.p.OrderQuantity *
                            @t2.@t1.p.RateGram / 100,
                            (@t2.x == null ? @t2.@t1.p.RoundedInfo : @t2.x.RoundedInfo), true),
                            (@t2.x == null ? @t2.@t1.p.RoundedInfo : @t2.x.RoundedInfo), (@t2.x != null && @t2.x.PackingFactor != 1)),
                    IsReleasePR = @t2.x != null,
                    ColorReference_PR = @t2.x == null ? @t2.@t1.p.ColorReference : @t2.x.ColorReference,
                    CNY00002PK_PR = @t2.x == null ? @t2.@t1.p.CNY00002PK : @t2.x.CNY00002PK,
                    Supplier_PR = @t2.x == null ? @t2.@t1.p.Supplier : @t2.x.Supplier,
                    UC_PR = @t2.x == null ? @t2.@t1.p.UC : @t2.x.UC,
                    UCUnitCode_PR = @t2.x == null ? @t2.@t1.p.UCUnitCode : @t2.x.UCUnitCode,
                    UCUnit_PR = @t2.x == null ? @t2.@t1.p.UCUnit : @t2.x.UCUnit,
                    Waste_PR = @t2.x == null ? @t2.@t1.p.Waste : @t2.x.Waste,
                    PercentUsing_PR = @t2.x == null ? @t2.@t1.p.PercentUsing : @t2.x.PercentUsing,
                    RateGram_PR = @t2.x == null ? @t2.@t1.p.RateGram : @t2.x.RateGram,
                    OrderQuantity_PR = @t2.x == null ? @t2.@t1.p.OrderQuantity : @t2.x.OrderQuantity,
                    CNY00050PK_PR = @t2.x == null ? @t2.@t1.p.CNY00050PK : @t2.x.CNY00050PK,
                    SupplierRef_PR = @t2.x == null ? @t2.@t1.p.SupplierRef : @t2.x.SupplierRef,
                    TDG00004PK_PR = @t2.x == null ? @t2.@t1.p.TDG00004PK : @t2.x.TDG00004PK,
                    AreaPaint_PR = @t2.x == null ? @t2.@t1.p.AreaPaint : @t2.x.AreaPaint,
                    FactorProduct_PR = @t2.x == null ? @t2.@t1.p.FactorProduct : @t2.x.FactorProduct,
                    CancelLine_PR = @t2.x == null ? @t2.@t1.p.CancelLine : @t2.x.CancelLine,
                    CancelFactor_PR = @t2.x == null ? @t2.@t1.p.CancelFactor : @t2.x.CancelFactor,
                    IsFinishing_PR = true,
                    PRQty_PR = @t2.x == null
                        ? MrpRounded(
                            @t2.@t1.p.CancelFactor * @t2.@t1.p.UC * (1 + @t2.@t1.p.Waste / 100) *
                            @t2.@t1.p.AreaPaint * @t2.@t1.p.FactorProduct * @t2.@t1.p.OrderQuantity *
                            @t2.@t1.p.RateGram / 100, @t2.@t1.p.RoundedInfo, true)
                        : @t2.x.PRQty,
                    CNY00056PK_PR = @t2.x == null ? 0 : @t2.x.CNY00056PK,
                    CNY00055PK_PR = @t2.x == null ? 0 : @t2.x.CNY00055PK,
                    PRQtyS_PR = @t2.x == null ? 0 : @t2.x.PRQtyS,
                    StockQty_PR = @t2.x == null ? 0 : @t2.x.StockQty,
                    AttributeLinkPK = @t2.x == null
                        ? @t2.@t1.p.CNY00016PK.ToString().Trim() + "$$BM"
                        : @t2.x.CNY00055PK.ToString().Trim() + "$$PR",
                    CNY00001PK = @t2.x == null ? @t2.@t1.p.CNY00001PK : @t2.x.CNY00001PK,
                    PackingFactor = @t2.x == null ? 1 : @t2.x.PackingFactor,
                    PurchaseUnit_010 = @t2.x == null ? @t2.@t1.p.PurchaseUnit_010 : @t2.x.PurchaseUnit_010,
                    OldDemandS = @t2.x == null ? 0 : @t2.x.OldDemandS,
                    OldDemandD = @t2.x == null ? 0 : @t2.x.OldDemandD,
                    IsRMStandardization = @t2.x == null ? @t2.@t1.p.IsRMStandardization : @t2.x.IsRMStandardization,
                    FormulaString = @t2.x == null ? @t2.@t1.p.FormulaString : @t2.x.FormulaString,
                    CNY00004PK = @t2.x == null ? 0 : @t2.x.CNY00004PK,
                    IsPRissuedTypechecking = @t2.x == null ? @t2.@t1.p.IsPRissuedTypechecking : @t2.x.IsPRissuedTypechecking,
                    IsRightRM = true,
                    BOMStatus = @t2.@t1.p.BOMStatus,
                    PurchaseType = @t2.@t1.p.PurchaseType,
                    Status = @t2.x == null ? 1 : @t2.x.Status,
                    Version = @t2.x == null ? 0 : @t2.x.Version,
                    CreatedDate = @t2.x == null ? defaultCurrentDate : @t2.x.CreatedDate,
                    CreatedBy = @t2.x == null ? DeclareSystem.SysUserName.ToUpper() : @t2.x.CreatedBy,
                    UpdatedDate = @t2.x == null ? defaultCurrentDate : @t2.x.UpdatedDate,
                    UpdatedBy = @t2.x == null ? DeclareSystem.SysUserName.ToUpper() : @t2.x.UpdatedBy,
                    SubmittedBy = @t2.x == null ? "N/A" : @t2.x.SubmittedBy,
                    SubmittedDate = @t2.x == null ? "01/01/1900" : @t2.x.SubmittedDate,
                    ConfirmedBy = @t2.x == null ? "N/A" : @t2.x.ConfirmedBy,
                    ConfirmedDate = @t2.x == null ? "01/01/1900" : @t2.x.ConfirmedDate,
                    ApprovedBy = @t2.x == null ? "N/A" : @t2.x.ApprovedBy,
                    ApprovedDate = @t2.x == null ? "01/01/1900" : @t2.x.ApprovedDate,
                    RejectedBy = @t2.x == null ? "N/A" : @t2.x.RejectedBy,
                    RejectedDate = @t2.x == null ? "01/01/1900" : @t2.x.RejectedDate,
                    Reference = @t2.@t1.p.Reference,
                    ProDimension = @t2.@t1.p.ProDimension,
                    RootSOQty = @t2.@t1.p.RootSOQty,
                    CNY00016PKParent = @t2.@t1.p.CNY00016PKParent,
                    TDG00001PKSO = @t2.@t1.p.TDG00001PKSO,
                    ProductCode = @t2.@t1.p.ProductCode,
                    ProductName = @t2.@t1.p.ProductName,
                    CNYMF016PK = @t2.@t1.p.CNYMF016PK,
                    IsGrouping = @t2.x == null ? false : @t2.x.IsGrouping,
                    IsFormula = @t2.x == null ? false : @t2.x.IsFormula,
                    IsPacking = @t2.x == null ? false : @t2.x.IsPacking,
                    IsUnAllocate = @t2.x == null ? false : @t2.x.IsUnAllocate,
                    UnAllocateQty = @t2.x == null ? 0 : @t2.x.UnAllocateQty,
                    LeadTime = @t2.x == null ? @t2.@t1.p.LeadTime : @t2.x.LeadTime,
                    IsDelete = @t2.@t1.p.IsDelete,
                    PK55SAVE = @t2.x == null ? 0 : @t2.x.CNY00055PK,
                    AssemblyComponent = "",
                    BookingQuantity = @t2.x == null ? 0 : @t2.x.BookingQuantity,
                    Color = @t2.@t1.p.Color,
                    Route = @t2.@t1.p.Route,
                    ChangeDemand = 0,
                    TDG00001PKBOM = @t2.@t1.p.TDG00001PK,
                    RMDescription_002BOM = @t2.@t1.p.RMDescription_002,
                    DIFFTDG00001PK = @t2.x == null || @t2.@t1.p.TDG00001PK == @t2.x.TDG00001PK ? 0 : 1,
                    CNY00016PKREP = @t2.@t1.p.CNY00016PKREP,
                }) //.Where(t => t.IsReleasePR || (t.TotalDemandQty > 0 && (t.BOMStatus == 2 || t.BOMStatus == 3 || t.BOMStatus == 4)))
                .Where(p => p.PurchaseType != 1).ToList();

            List<Int64> lCNy00016pk = lDemandNormal.Select(p => p.CNY00016PK).Distinct().ToList();
            sql = " SELECT a.CNY00008PK as AttibutePK,b.CNY001 AS AttibuteCode,b.CNY002 AS AttibuteName,CASE WHEN ISNULL(a.AttributeValue, '') = '' THEN '' ELSE ((a.AttributeValue) + (CASE  WHEN ISNULL(d.CNY002, '') = '' THEN '' ELSE '' + d.CNY002 + '' END)) END AS AttibuteValueFull, ";
            sql += "  a.PK,a.CNY00016PK,'Unchange' AS RowState,ISNULL(a.AttributeValue, '') AS AttibuteValueTemp, ";
            sql += " CASE WHEN ISNULL(d.CNY001, '') = '' THEN '' ELSE d.CNY001 + '-' + d.CNY002 END AS AttibuteUnit, CAST((CASE WHEN b.CNY003 = 'number' THEN 1 ELSE 0 END) AS bit) AS IsNumber,b.CNY003 AS DataAttType,b.CNY007 AS AttIndex,";
            sql += "isnull(a.AttributeValue,'') as AttributeValue,a.AttributeUnit";
            sql += " FROM  dbo.CNY00017 a INNER JOIN dbo.CNY00008 b ON a.CNY00008PK = b.PK LEFT JOIN dbo.CNY00005 d ON a.AttributeUnit = d.CNY001";
            if (lCNy00016pk.Count > 0)
            {
                sql += " where a.CNY00016PK IN(" + string.Join(",", lCNy00016pk) + ")";
            }
            else
            {
                sql += " where 1=0 ";
            }
            DataTable dtAtt17 = _inf.TblExcuteSqlText(sql);
            DataTable dt55Para = new DataTable();
            dt55Para.Columns.Add("CNY00055PK", typeof(Int64));
            dt55Para.Columns.Add("TDG00001PK", typeof(Int64));
            dt55Para.Columns.Add("CNY00001PK", typeof(Int64));
            dt55Para.Columns.Add("IsRMStandardization", typeof(int));
            var q55Att1 = lDemandNormal.Where(p => p.CNY00055PK_PR > 0).Select(p => new
            {
                CNY00055PK = p.CNY00055PK_PR,
                TDG00001PK = p.TDG00001PK,
                CNY00001PK = p.CNY00001PK,
                IsRMStandardization = p.IsRMStandardization
            }).Distinct().ToList();
            foreach (var item55Att1 in q55Att1)
            {
                dt55Para.Rows.Add(item55Att1.CNY00055PK, item55Att1.TDG00001PK, item55Att1.CNY00001PK,
                    item55Att1.IsRMStandardization);
            }
            dt55Para.AcceptChanges();
            DataTable dtAtt55Temp = _inf.GetAttByPrLine(dt55Para);
            Int64 idAtt = 0;
            if (dtAtt55Temp.Rows.Count > 0)
            {
                idAtt = dtAtt55Temp.AsEnumerable().Max(p => p.Field<Int64>("ID"));
            }
            var lCNY00016PkAtt = lDemandNormal.Where(p => !p.IsReleasePR).Select(p => new
            {
                p.CNY00016PK,
                p.AttributeLinkPK,
                p.CNY00001PK,
                p.IsRMStandardization,
                p.TDG00001PK,
                p.IsReleasePR,
                p.UCUnitCode,
                p.PurchaseUnit_010
            }).Distinct().ToList();
            List<AttStandardMRPTestInfo> qAttTemp = lCNY00016PkAtt.Join(dtAtt17.AsEnumerable(), p => new
            {
                p.CNY00016PK

            }, t => new
            {
                CNY00016PK = t.Field<Int64>("CNY00016PK")
            }, (p, t) => new AttStandardMRPTestInfo
            {

                AttibutePK = t.Field<Int64>("AttibutePK"),
                AttibuteCode = t.Field<string>("AttibuteCode"),
                AttibuteName = t.Field<string>("AttibuteName"),
                AttibuteValueFull = t.Field<string>("AttibuteValueFull"),
                PK = 0,
                PKRowHeader = p.CNY00016PK,
                RowState = DataStatus.Insert.ToString(),
                AttibuteValueTemp = t.Field<string>("AttributeValue"),
                AttibuteUnit = t.Field<string>("AttibuteUnit"),
                IsNumber = t.Field<bool>("IsNumber"),
                DataAttType = t.Field<string>("DataAttType"),
                AttributeLinkPK = p.AttributeLinkPK,
                SortIndex = t.Field<int>("AttIndex"),
                IsNew = true,
                CNY00001PK = p.CNY00001PK,
                UnitTemp = t.Field<string>("AttributeUnit"),
                IsRMStandardization = p.IsRMStandardization,
                TDG00001PK = p.TDG00001PK,
                PurchaseUnit_010 = p.PurchaseUnit_010,
                UCUnitCode = p.UCUnitCode,
                ID = ++idAtt
            }).ToList();
            if (qAttTemp.Count > 0)
            {
                var qCNy00001PkNew = lCNY00016PkAtt.Where(t => t.IsRMStandardization == 2).Select(t => t.CNY00001PK)
                    .Distinct().ToList();
                if (qCNy00001PkNew.Count > 0)
                {
                    sql = " SELECT (CASE WHEN ISNULL(b.CNY002, '') = '' THEN '' ELSE  ((b.CNY002) + (CASE WHEN ISNULL(c.CNY002, '') = '' THEN '' ELSE '' + c.CNY002 + '' END ))END ) AS AttibuteValueFull,";
                    sql += " ISNULL(b.CNY002, '') AS AttibuteValueTemp,(CASE WHEN ISNULL(c.CNY001, '') = '' THEN '' ELSE c.CNY001 + '-' + c.CNY002 END) AS AttibuteUnit,";
                    sql += " b.CNY003 AS UnitTemp,b.CNY00001PK,b.CNY00008PK as AttibutePK,b.CNY001 as GroupType";
                    sql += " FROM dbo.CNY00001D b LEFT JOIN dbo.CNY00005 c  ON b.CNY003 = c.CNY001 WHERE b.CNYSYS01Code = 'PR' AND b.CNY005 = 1 and b.CNY00001PK in (" + string.Join(",", qCNy00001PkNew) + ")";
                    DataTable dtStandardAtt = _inf.TblExcuteSqlText(sql);
                    if (dtStandardAtt.Rows.Count > 0)
                    {
                        var qAttUpd = qAttTemp.Join(dtStandardAtt.AsEnumerable(), p => new
                        {
                            CNY00001PK = p.CNY00001PK,
                            AttibutePK = p.AttibutePK

                        }, t => new
                        {
                            CNY00001PK = t.Field<Int64>("CNY00001PK"),
                            AttibutePK = t.Field<Int64>("AttibutePK")
                        }, (p, t) => new
                        {
                            ItemList = p,
                            ItemRow = t
                        }).ToList();
                        if (qAttUpd.Count > 0)
                        {
                            foreach (var itemAtt in qAttUpd)
                            {
                                DataRow standRow = itemAtt.ItemRow;
                                int groupType = ProcessGeneral.GetSafeInt(standRow["GroupType"]);
                                if (groupType != 1 && groupType != 2) continue;
                                AttStandardMRPTestInfo standItem = itemAtt.ItemList;
                                if (groupType == 1)
                                {
                                    standItem.AttibuteValueFull =
                                        ProcessGeneral.GetSafeString(standRow["AttibuteValueFull"]);
                                    standItem.AttibuteValueTemp =
                                        ProcessGeneral.GetSafeString(standRow["AttibuteValueTemp"]);
                                    standItem.AttibuteUnit = ProcessGeneral.GetSafeString(standRow["AttibuteUnit"]);
                                    standItem.UnitTemp = ProcessGeneral.GetSafeString(standRow["UnitTemp"]);
                                }
                                else
                                {
                                    qAttTemp.Remove(standItem);
                                }

                            }
                        }
                        List<Int64> dicGroupStandardTemp = dtStandardAtt.AsEnumerable()
                            .Select(p => p.Field<Int64>("CNY00001PK")).Distinct().ToList();
                        var qStandard1 = qAttTemp.Where(p => dicGroupStandardTemp.Contains(p.CNY00001PK)).Select(
                            p =>
                                new
                                {
                                    p.AttibutePK,
                                    p.AttibuteValueTemp,
                                    p.AttributeLinkPK,
                                    p.CNY00001PK,
                                    p.UnitTemp,
                                    p.UCUnitCode,
                                    p.PurchaseUnit_010
                                }).ToList();
                        if (qStandard1.Count > 0)
                        {
                            Dictionary<string, int> dicAttCount = qStandard1.GroupBy(p => p.AttributeLinkPK).Select(
                                s =>
                                    new
                                    {
                                        AttributeLinkPK = s.Key,
                                        CountAtt = s.Count()
                                    }).ToDictionary(p => p.AttributeLinkPK, p => p.CountAtt);
                            DataTable dtAttGrp = new DataTable();
                            dtAttGrp.Columns.Add("AttibutePK", typeof(Int64));
                            dtAttGrp.Columns.Add("AttibuteValueTemp", typeof(string));
                            dtAttGrp.Columns.Add("AttributeLinkPK", typeof(string));
                            dtAttGrp.Columns.Add("CNY00001PK", typeof(Int64));
                            dtAttGrp.Columns.Add("UnitTemp", typeof(string));
                            dtAttGrp.Columns.Add("CountAtt", typeof(int));
                            dtAttGrp.Columns.Add("UCUnitCode", typeof(string));
                            dtAttGrp.Columns.Add("PurchaseUnit_010", typeof(string));
                            foreach (var item1 in qStandard1)
                            {
                                DataRow dr = dtAttGrp.NewRow();
                                dr["AttibutePK"] = item1.AttibutePK;
                                dr["AttibuteValueTemp"] = item1.AttibuteValueTemp;
                                dr["AttributeLinkPK"] = item1.AttributeLinkPK;
                                dr["CNY00001PK"] = item1.CNY00001PK;
                                dr["UnitTemp"] = item1.UnitTemp;
                                dr["CountAtt"] = dicAttCount[item1.AttributeLinkPK];
                                dr["UCUnitCode"] = item1.UCUnitCode;
                                dr["PurchaseUnit_010"] = item1.PurchaseUnit_010;
                                dtAttGrp.Rows.Add(dr);
                            }
                            DataTable dtPacking = _inf.GetPackingFactorBeforLoad(dtAttGrp);
                            if (dtPacking.Rows.Count > 0)
                            {
                                var qUpdPacking = lDemandNormal.Join(dtPacking.AsEnumerable(), p => new
                                {
                                    p.AttributeLinkPK
                                }, t => new
                                {
                                    AttributeLinkPK = t.Field<string>("AttributeLinkPK")
                                }, (p, t) => new
                                {
                                    ItemList = p,
                                    ItemRow = t
                                }).ToList();
                                foreach (var itemPacking in qUpdPacking)
                                {
                                    SoBoMDemandNormalInfo itemNormal = itemPacking.ItemList;
                                    DataRow dr = itemPacking.ItemRow;
                                    decimal packingFactor = ProcessGeneral.GetSafeDecimal(dr["PackingFactor"]);
                                    itemNormal.PackingFactor = packingFactor;
                                    itemNormal.IsPacking = true;
                                    if (packingFactor != 1)
                                    {
                                        itemNormal.TotalDemandQty = MrpRounded(itemNormal.TotalDemandQty * packingFactor, itemNormal.DecimalRound, itemNormal.RoundRule, itemNormal.RoundType, true);
                                        itemNormal.PRQty_PR = MrpRounded(itemNormal.PRQty_PR * packingFactor, itemNormal.DecimalRound, itemNormal.RoundRule, itemNormal.RoundType, true);
                                    }
                                    itemNormal.UCUnitCode = ProcessGeneral.GetSafeString(dr["PurchaseUnit_010"]);
                                    itemNormal.UCUnit = ProcessGeneral.GetSafeString(dr["PurchaseUnit_010Desc"]);
                                }
                            }
                        }
                    }
                }
                var qAttMethold2 = qAttTemp.Where(p => p.IsRMStandardization == 1).ToList();
                if (qAttMethold2.Count > 0)
                {
                    DataTable dtAttM2 = _inf.GetAttGroupMethold2(qAttMethold2.Select(p => new
                    {
                        PK = p.TDG00001PK
                    }).Distinct().CopyToDataTableNew());
                    var qDelMethold2 = qAttMethold2.Where(p =>
                        !dtAttM2.AsEnumerable().Any(t =>
                            p.TDG00001PK == t.Field<Int64>("TDG00001PK") &&
                            p.AttibutePK == t.Field<Int64>("AttibutePK"))).ToList();
                    foreach (AttStandardMRPTestInfo itemDel in qDelMethold2)
                    {
                        qAttTemp.Remove(itemDel);
                    }
                    var qUpdMethold2 = qAttMethold2.Join(dtAttM2.AsEnumerable(), p => new
                    {
                        p.TDG00001PK,
                        p.AttibutePK
                    }, t => new
                    {
                        TDG00001PK = t.Field<Int64>("TDG00001PK"),
                        AttibutePK = t.Field<Int64>("AttibutePK")
                    }, (p, t) => new
                    {
                        ListItem = p,
                        RowItem = t
                    }).ToList();
                    foreach (var itemM2 in qUpdMethold2)
                    {
                        AttStandardMRPTestInfo attUpd = itemM2.ListItem;
                        DataRow dr = itemM2.RowItem;
                        attUpd.AttibuteValueFull = ProcessGeneral.GetSafeString(dr["AttibuteValueFull"]);
                        attUpd.AttibuteValueTemp = ProcessGeneral.GetSafeString(dr["AttibuteValueTemp"]);
                        attUpd.AttibuteUnit = ProcessGeneral.GetSafeString(dr["AttibuteUnit"]);
                        attUpd.UnitTemp = ProcessGeneral.GetSafeString(dr["UnitTemp"]);
                    }
                }
                foreach (var itemTemp in qAttTemp)
                {
                    DataRow drTemp = dtAtt55Temp.NewRow();
                    drTemp["AttibutePK"] = itemTemp.AttibutePK;
                    drTemp["AttibuteCode"] = itemTemp.AttibuteCode;
                    drTemp["AttibuteName"] = itemTemp.AttibuteName;
                    drTemp["AttibuteValueFull"] = itemTemp.AttibuteValueFull;
                    drTemp["PK"] = itemTemp.PK;
                    drTemp["PKRowHeader"] = itemTemp.PKRowHeader;
                    drTemp["RowState"] = itemTemp.RowState;
                    drTemp["AttibuteValueTemp"] = itemTemp.AttibuteValueTemp;
                    drTemp["AttibuteUnit"] = itemTemp.AttibuteUnit;
                    drTemp["IsNumber"] = itemTemp.IsNumber;
                    drTemp["DataAttType"] = itemTemp.DataAttType;
                    drTemp["AttributeLinkPK"] = itemTemp.AttributeLinkPK;
                    drTemp["SortIndex"] = itemTemp.SortIndex;
                    drTemp["IsNew"] = itemTemp.IsNew;
                    drTemp["CNY00001PK"] = itemTemp.CNY00001PK;
                    drTemp["UnitTemp"] = itemTemp.UnitTemp;
                    drTemp["IsRMStandardization"] = itemTemp.IsRMStandardization;
                    drTemp["TDG00001PK"] = itemTemp.TDG00001PK;
                    dtAtt55Temp.Rows.Add(drTemp);
                }
                dtAtt55Temp.AcceptChanges();
            }
            _dicAtt1 = dtAtt55Temp.AsEnumerable().GroupBy(p => new
            {
                AttributeLinkPK = p.Field<string>("AttributeLinkPK")
            }).Select(s => new
            {
                s.Key.AttributeLinkPK,
                LAttInfo = s.Select(t => new BoMInputAttInfo
                {
                    AttibutePK = t.Field<Int64>("AttibutePK"),
                    AttibuteName = t.Field<string>("AttibuteName"),
                    AttibuteValueFull = t.Field<string>("AttibuteValueFull"),
                    AttibuteValueTemp = t.Field<string>("AttibuteValueTemp"),
                    IsNumber = t.Field<bool>("IsNumber"),
                }).OrderBy(p => p.AttibutePK).ToList(),
                StringAttValue = string.Join("^^^^^",
                    s.Where(t => !string.IsNullOrEmpty(t.Field<string>("AttibuteValueFull")))
                        .Select(t => string.Format("{0}%%%%%{1}", t.Field<Int64>("AttibutePK"),
                            t.Field<string>("AttibuteValueFull"))).OrderBy(q => q))
            }).ToDictionary(p => p.AttributeLinkPK, p => new AttCrossTemp1Info
            {
                LAttInfo = p.LAttInfo,
                StringAttValue = p.StringAttValue
            });

            CalDemandRmRow(ref lDemandNormal, _dicAtt1, false);
            CalDemandRmRow(ref lDemandFinishing, _dicAtt1, true);


            _lDemand = lDemandNormal.Concat(lDemandFinishing).ToList();




            _dicAttValue = dtAtt55Temp.AsEnumerable().GroupBy(p => p.Field<string>("AttributeLinkPK")).Select(s => new
            {
                AttributeLinkPK = s.Key,
                AttData = s.Select(t => new BoMInputAttInfo
                {
                    AttibuteCode = t.Field<string>("AttibuteCode"),
                    AttibutePK = t.Field<Int64>("AttibutePK"),
                    AttibuteName = t.Field<string>("AttibuteName"),
                    AttibuteValueFull = t.Field<string>("AttibuteValueFull"),
                    PK = t.Field<Int64>("PK"),
                    RowState = ProcessGeneral.GetDataStatus(t.Field<string>("RowState")),
                    AttibuteValueTemp = t.Field<string>("AttibuteValueTemp"),
                    AttibuteUnit = t.Field<string>("AttibuteUnit"),
                    IsNumber = t.Field<bool>("IsNumber"),
                    ColumnIndex = t.Field<int>("SortIndex"),
                }).ToList()
            }).ToDictionary(p => p.AttributeLinkPK, p => p.AttData);




            dtSoTemp.Columns.Remove("ProductCode");
            dtSoTemp.Columns.Remove("Reference");
            dtSoTemp.Columns.Remove("OrderQuantity");
            dtSoTemp.Columns.Remove("TDG00001PK");
            dtSoTemp.Columns.Remove("BOMID");
            dtSoTemp.Columns.Remove("BOMIDN");
            dtSoTemp.Columns.Remove("FactorProduct");
            dtSoTemp.Columns.Remove("ProDimension");
            dtSoTemp.Columns.Remove("RootSOQty");
            dtSoTemp.Columns.Remove("ProductName");
            dtSoTemp.Columns.Remove("SortOrderNode");
            dtSoTemp.AcceptChanges();
            DataTable dtSoFinal = dtSoTemp.AsEnumerable().CopyToDataTable();
            dsReturn.Tables.Add(dtErrCode);
            dsReturn.Tables.Add(dtSoFinal);
            return dsReturn;
        }


        public DataTable CalDataDetailByGroup(string mainGroupCode, List<Int64> lCNY00020PK)
        {
            var qDemand1 = _lDemand.Where(p => lCNY00020PK.Contains(p.CNY00020PK) && p.MainMaterialGroup.StartsWith(mainGroupCode)).Select(p => new
            {
                p.MainMaterialGroup,
                p.RMCode_001,
                p.RMDescription_002,
                StringAttValue = GetAttValueString(p.IsFinishing, p.AttributeLinkPK, _dicAtt1),
                ColorReference = p.IsReleasePR ? p.ColorReference_PR : p.ColorReference,
                Supplier = p.IsReleasePR ? p.Supplier_PR : p.Supplier,
                SupplierRef = p.IsReleasePR ? p.SupplierRef_PR : p.SupplierRef,
                UCUnit = p.IsReleasePR ? p.UCUnit_PR : p.UCUnit,
                CNY00055PK = p.IsReleasePR ? p.CNY00055PK_PR : 0,
                p.TotalDemandQty,
                p.PRQty_PR,
                p.CNY00020PK,
                p.DecimalRound,
                p.AttributeLinkPK
            }).GroupBy(p => new
            {
                p.MainMaterialGroup,
                p.RMCode_001,
                p.RMDescription_002,
                p.StringAttValue,
                p.ColorReference,
                p.Supplier,
                p.SupplierRef,
                p.UCUnit,
                p.CNY00020PK,
                p.DecimalRound,
                p.CNY00055PK
            }).Select(s => new
            {
                s.Key.MainMaterialGroup,
                s.Key.RMCode_001,
                s.Key.RMDescription_002,
                s.Key.StringAttValue,
                s.Key.ColorReference,
                s.Key.Supplier,
                s.Key.SupplierRef,
                s.Key.UCUnit,
                s.Key.CNY00020PK,
                s.Key.DecimalRound,
                s.Key.CNY00055PK,
                TotalDemandQty = s.Sum(t => t.TotalDemandQty),
                PRQty_PR = s.Sum(t => t.PRQty_PR),
                AttributeLinkPK = s.Min(t => t.AttributeLinkPK),
                POQty = GetPoReleaseQty(s.Key.CNY00055PK, s.Key.CNY00020PK)
            }).ToList();


            

            var qDemand2 = qDemand1.GroupBy(p => new
            {
                p.MainMaterialGroup,
                p.RMCode_001,
                p.RMDescription_002,
                p.StringAttValue,
                p.ColorReference,
                p.Supplier,
                p.SupplierRef,
                p.UCUnit,
                p.DecimalRound,
                p.CNY00020PK,
            }).Select(s => new
            {
                s.Key.MainMaterialGroup,
                s.Key.RMCode_001,
                s.Key.RMDescription_002,
                s.Key.StringAttValue,
                s.Key.ColorReference,
                s.Key.Supplier,
                s.Key.SupplierRef,
                s.Key.UCUnit,
                s.Key.DecimalRound,
                AttributeLinkPK = s.Min(t => t.AttributeLinkPK),
                s.Key.CNY00020PK,
                TotalDemandQty = s.Sum(t => t.TotalDemandQty),
                PRQty_PR = s.Sum(t => t.PRQty_PR),
                POQty = s.Sum(t => t.POQty),
            }).ToList();


            var qFinal = qDemand2.GroupBy(p => new
            {
                p.MainMaterialGroup,
                p.RMCode_001,
                p.RMDescription_002,
                p.StringAttValue,
                p.ColorReference,
                p.Supplier,
                p.SupplierRef,
                p.UCUnit,
                p.DecimalRound,
            }).Select(s => new
            {
                s.Key.MainMaterialGroup,
                s.Key.RMCode_001,
                s.Key.RMDescription_002,
                s.Key.StringAttValue,
                s.Key.ColorReference,
                s.Key.Supplier,
                s.Key.SupplierRef,
                s.Key.UCUnit,
                s.Key.DecimalRound,
                AttributeLinkPK = s.Min(t => t.AttributeLinkPK),
                TotalDemandQty = s.Sum(t => t.TotalDemandQty),
                PRQty_PR = s.Sum(t => t.PRQty_PR),
                POQty = s.Sum(t => t.POQty),
            }).OrderBy(p=>p.RMCode_001).ThenBy(p=>p.StringAttValue).ThenBy(p=>p.ColorReference).ToList();
            List<string> lAttLinkPk = qFinal.Select(p => p.AttributeLinkPK).Distinct().ToList();
            Dictionary<string, List<BoMInputAttInfo>> dicAttValue = _dicAttValue.Where(p => lAttLinkPk.Contains(p.Key)).ToDictionary(p => p.Key, p => p.Value);
           

            var qAtt = dicAttValue.SelectMany(p=>p.Value,(m,p)=>new
            {
                AttibutePK = p.AttibutePK,
                AttibuteCode = p.AttibuteCode,
                AttibuteName = p.AttibuteName,
                SortIndex = p.ColumnIndex,
                IsNumber = p.IsNumber
            }).Distinct().OrderBy(p => p.SortIndex).ToList();


            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("MainMaterialGroup", typeof(string));
            dtFinal.Columns.Add("RMCode_001", typeof(string));
            dtFinal.Columns.Add("RMDescription_002", typeof(string));
            foreach (var itemAtt in qAtt)
            {
                dtFinal.Columns.Add(string.Format("{0}-{1}", itemAtt.AttibutePK, itemAtt.AttibuteName),
                    typeof(string));
            }
            dtFinal.Columns.Add("Supplier", typeof(string));
            dtFinal.Columns.Add("SupplierRef", typeof(string));
            dtFinal.Columns.Add("ColorReference", typeof(string));
            dtFinal.Columns.Add("UCUnit", typeof(string));
           
            dtFinal.Columns.Add("TotalDemandQty", typeof(decimal));
            dtFinal.Columns.Add("PRQty_PR", typeof(decimal));
            dtFinal.Columns.Add("POQty", typeof(decimal));
            dtFinal.Columns.Add("DecimalRound", typeof(int));
         

            foreach (var item in qFinal)
            {
                DataRow dr = dtFinal.NewRow();
                dr["MainMaterialGroup"] = item.MainMaterialGroup;
                dr["RMCode_001"] = item.RMCode_001;
                dr["RMDescription_002"] = item.RMDescription_002;
                foreach (var itemAtt in qAtt)
                {
                    dr[string.Format("{0}-{1}", itemAtt.AttibutePK, itemAtt.AttibuteName)] = "";
                }

               
                string attributeLinkPK = item.AttributeLinkPK;



                if (!string.IsNullOrEmpty(attributeLinkPK))
                {
                    List<BoMInputAttInfo> lAttInfo;
                    if (dicAttValue.TryGetValue(attributeLinkPK, out lAttInfo) && lAttInfo.Count > 0)
                    {
                        foreach (BoMInputAttInfo info in lAttInfo)
                        {

                            dr[string.Format("{0}-{1}", info.AttibutePK, info.AttibuteName)] = info.AttibuteValueFull;
                        }
                    }
                }
                  
               
                dr["DecimalRound"] = item.DecimalRound;
                dr["Supplier"] = item.Supplier;
                dr["SupplierRef"] = item.SupplierRef;
                dr["ColorReference"] = item.ColorReference;
                dr["UCUnit"] = item.UCUnit;
                dr["TotalDemandQty"] = item.TotalDemandQty;
                dr["PRQty_PR"] = item.PRQty_PR;
                dr["POQty"] = item.POQty;
                
              
                dtFinal.Rows.Add(dr);
            
            }

            return dtFinal;
        }

        public bool CalDataDisplayBySoSelected(Dictionary<Int64, Tuple<string, int, int>> lCNY00020PK, out DataTable dtSumary,out DataTable dtFinal,out Dictionary<string, string> qLoop)
        {
            var qDemand1 = _lDemand.Where(p=> lCNY00020PK.ContainsKey(p.CNY00020PK)).Select(p => new
            {
                p.MainMaterialGroup,
                p.RMCode_001,
                p.RMDescription_002,
                StringAttValue = GetAttValueString(p.IsFinishing, p.AttributeLinkPK, _dicAtt1),
                ColorReference = p.IsReleasePR ? p.ColorReference_PR : p.ColorReference,
                Supplier = p.IsReleasePR ? p.Supplier_PR : p.Supplier,
                SupplierRef = p.IsReleasePR ? p.SupplierRef_PR : p.SupplierRef,
                UCUnit = p.IsReleasePR ? p.UCUnit_PR : p.UCUnit,
                CNY00055PK = p.IsReleasePR ? p.CNY00055PK_PR : 0,
                p.TotalDemandQty,
                p.PRQty_PR,
                p.CNY00020PK,
                p.DecimalRound,
                p.CNY00001PK,
                p.AttributeLinkPK
            }).GroupBy(p => new
            {
                p.MainMaterialGroup,
                p.RMCode_001,
                p.RMDescription_002,
                p.StringAttValue,
                p.ColorReference,
                p.Supplier,
                p.SupplierRef,
                p.UCUnit,
                p.CNY00020PK,
                p.DecimalRound,
                p.CNY00001PK,
                p.CNY00055PK
            }).Select(s => new
            {
                s.Key.MainMaterialGroup,
                s.Key.RMCode_001,
                s.Key.RMDescription_002,
                s.Key.StringAttValue,
                s.Key.ColorReference,
                s.Key.Supplier,
                s.Key.SupplierRef,
                s.Key.UCUnit,
                s.Key.CNY00020PK,
                s.Key.DecimalRound,
                s.Key.CNY00001PK,
                s.Key.CNY00055PK,
                TotalDemandQty = s.Sum(t => t.TotalDemandQty),
                PRQty_PR = s.Sum(t => t.PRQty_PR),
                AttributeLinkPK = s.Min(t => t.AttributeLinkPK),
                POQty = GetPoReleaseQty(s.Key.CNY00055PK, s.Key.CNY00020PK)
            }).ToList();


            if (qDemand1.Count <= 0)
            {
                dtSumary = null;
                dtFinal = null;
                qLoop = new Dictionary<string, string>();
                return false;
            }

            var qDemand2 = qDemand1.GroupBy(p => new
            {
                p.MainMaterialGroup,
                p.RMCode_001,
                p.RMDescription_002,
                p.StringAttValue,
                p.ColorReference,
                p.Supplier,
                p.SupplierRef,
                p.UCUnit,
                p.DecimalRound,
                p.CNY00001PK,
               // p.AttributeLinkPK,
                p.CNY00020PK,
            }).Select(s => new
            {
                s.Key.MainMaterialGroup,
                s.Key.RMCode_001,
                s.Key.RMDescription_002,
                s.Key.StringAttValue,
                s.Key.ColorReference,
                s.Key.Supplier,
                s.Key.SupplierRef,
                s.Key.UCUnit,
                s.Key.DecimalRound,
                s.Key.CNY00001PK,
                AttributeLinkPK = s.Min(t => t.AttributeLinkPK),
                s.Key.CNY00020PK,
                TotalDemandQty = s.Sum(t=>t.TotalDemandQty),
                PRQty_PR = s.Sum(t => t.PRQty_PR),
                POQty = s.Sum(t => t.POQty),
            }).ToList();

            var qFinal = qDemand2.Select(s => new
            {
                s.MainMaterialGroup,
                s.CNY00020PK,
                DiffDemand = s.TotalDemandQty - s.PRQty_PR == 0 ? 0 : 1,
                DiffPO = s.POQty - s.PRQty_PR == 0 ? 0 : 1,
            }).GroupBy(p => new
            {
                p.MainMaterialGroup,
                p.CNY00020PK,
            }).Select(s => new
            {
                s.Key.MainMaterialGroup,
                s.Key.CNY00020PK,
                DiffDemand = s.Max(t => t.DiffDemand),
                DiffPO = s.Max(t => t.DiffPO)
            }).ToList();
          


            var qGroup = qFinal.GroupBy(p => p.CNY00020PK).Select(s => new
            {
                CNY00020PK = s.Key,
                SOData = lCNY00020PK[s.Key],
                GroupData = s.Select(t => new
                {
                    MainMaterialGroupCode = ProcessGeneral.GetCodeInString(t.MainMaterialGroup, "-"),
                    FinishPR = t.DiffDemand == 1 ? "YELLOW" : "GREEN",
                    FinishPO = t.DiffPO == 1 ? "YELLOW" : "GREEN",
                })
            }).OrderBy(p=>p.SOData.Item2).ToList();
            List<string> lMainGroup = qFinal.Select(p => p.MainMaterialGroup).Distinct().Select(p => "'" + ProcessGeneral.GetCodeInString(p, "-") + "'").ToList();
            string strMainGroup = string.Join(",", lMainGroup);
            string sql = string.Format("SELECT Code as MainMaterialGroupCode,Description1 AS MainMaterialGroupDesc FROM dbo.GlobalCategoryGroup WHERE Code IN ({0}) ORDER BY SortOrderNode", strMainGroup);
            DataTable dtMainGroup = _inf.TblExcuteSqlText(sql);
            qLoop = dtMainGroup.AsEnumerable().ToDictionary(t => t.Field<string>("MainMaterialGroupCode"), t => t.Field<string>("MainMaterialGroupDesc"));
          

            dtFinal = new DataTable();
            dtFinal.Columns.Add("CNY00020PK", typeof(Int64));
            dtFinal.Columns.Add("Reference", typeof(string));
            dtFinal.Columns.Add("SOQty", typeof(int));

            dtSumary = new DataTable();
            dtSumary.Columns.Add("ColorType", typeof(string));
            dtSumary.Columns.Add("ColorDesc", typeof(string));
            dtSumary.Rows.Add("GREEN", "Required Qty = Purchased Qty");
            dtSumary.Rows.Add("YELLOW", "Required Qty <> Purchased Qty");





            foreach (var itemMain in qLoop)
            {

                dtFinal.Columns.Add(string.Format("FinishPR_{0}", itemMain.Key), typeof(string));
                dtFinal.Columns.Add(string.Format("FinishPO_{0}", itemMain.Key), typeof(string));
                dtSumary.Columns.Add(string.Format("FinishPR_{0}", itemMain.Key), typeof(string));
                dtSumary.Columns.Add(string.Format("FinishPO_{0}", itemMain.Key), typeof(string));
            }

            for (int i = 0; i <= 1; i++)
            {
                DataRow drS = dtSumary.Rows[i];
                foreach (var itemMain in qLoop)
                {
                    drS[string.Format("FinishPR_{0}", itemMain.Key)] = "";
                    drS[string.Format("FinishPO_{0}", itemMain.Key)] = "";
                }
            }
            dtSumary.AcceptChanges();

            Dictionary<string, int> dicRowFooter = new Dictionary<string, int> { { "GREEN", 0 }, { "YELLOW", 1 } };


            foreach (var item in qGroup)
            {
                DataRow drFinal = dtFinal.NewRow();
                drFinal["CNY00020PK"] = item.CNY00020PK;
                drFinal["Reference"] = item.SOData.Item1;
                drFinal["SOQty"] = item.SOData.Item3;

                foreach (var itemMain in qLoop)
                {
                    drFinal[string.Format("FinishPR_{0}", itemMain.Key)] = "";
                    drFinal[string.Format("FinishPO_{0}", itemMain.Key)] = "";
                }



                var lInfo = item.GroupData;

                foreach (var info in lInfo)
                {
                    string prField = string.Format("FinishPR_{0}", info.MainMaterialGroupCode);
                    string poField = string.Format("FinishPO_{0}", info.MainMaterialGroupCode);
                    drFinal[prField] = info.FinishPR;
                    drFinal[poField] = info.FinishPO;

                    if (!string.IsNullOrEmpty(info.FinishPR))
                    {
                        DataRow rowPrIndPr = dtSumary.Rows[dicRowFooter[info.FinishPR]];
                        string valueRowPr = ProcessGeneral.GetSafeString(rowPrIndPr[prField]);
                        if (string.IsNullOrEmpty(valueRowPr))
                        {
                            rowPrIndPr[prField] = info.FinishPR;
                        }
                    }



                    if (!string.IsNullOrEmpty(info.FinishPO))
                    {
                        DataRow rowPrIndPo = dtSumary.Rows[dicRowFooter[info.FinishPO]];
                        string valueRowPo = ProcessGeneral.GetSafeString(rowPrIndPo[poField]);
                        if (string.IsNullOrEmpty(valueRowPo))
                        {
                            rowPrIndPo[poField] = info.FinishPO;
                        }
                    }

                }





                dtFinal.Rows.Add(drFinal);
            };
            dtSumary.AcceptChanges();

            int rcSumary = dtSumary.Rows.Count;
            for (int j = rcSumary - 1; j >= 0; j--)
            {
                DataRow drSu = dtSumary.Rows[j];
                bool isDelSu = true;
                foreach (var itemMain in qLoop)
                {
                    if (ProcessGeneral.GetSafeString(drSu[string.Format("FinishPR_{0}", itemMain.Key)]) != "" || ProcessGeneral.GetSafeString(drSu[string.Format("FinishPO_{0}", itemMain.Key)]) != "")
                    {
                        isDelSu = false;
                        break;
                    }
                }

                if (isDelSu)
                {
                    dtSumary.Rows.RemoveAt(j);
                }
            }
            dtSumary.AcceptChanges();
            return true;
        }
        private void CalDemandRmRow(ref List<SoBoMDemandNormalInfo> lDemand, Dictionary<string, AttCrossTemp1Info> dicAtt1, bool isFinishing)
        {



            List<SoBoMDemandNormalInfo> lDel = new List<SoBoMDemandNormalInfo>();
            foreach (SoBoMDemandNormalInfo item in lDemand)
            {

                if (!item.IsReleasePR && item.CancelLine)
                {
                    lDel.Add(item);
                    continue;
                }
                if (item.CNY00055PK_PR <= 0 && item.IsDelete)
                {
                    lDel.Add(item);
                    continue;
                }

                if (item.FormulaString == "" || !item.IsFormula || !item.IsReleasePR)
                    continue;
                string formulaString = item.FormulaString;
                string attPkLink = item.AttributeLinkPK;
                List<CalAreaAttInfo> lInfo = new List<CalAreaAttInfo>();
                // Dictionary<Int64, SoInfoBomCrossValue> dicSoDemand = new Dictionary<Int64, SoInfoBomCrossValue>();
                if (!isFinishing)
                {
                    AttCrossTemp1Info attCross = new AttCrossTemp1Info();

                    if (dicAtt1.TryGetValueN(attPkLink, out attCross))
                    {
                        List<BoMInputAttInfo> lTest = attCross.LAttInfo;
                        lInfo = lTest.Where(t => t.AttibutePK > 0).Select(t => new
                        {
                            CNY00008PKCal = t.AttibutePK,
                            ValueCal = ProcessGeneral.GetSafeDecimal(t.AttibuteValueTemp)

                        }).GroupBy(t => t.CNY00008PKCal).Select(q => new CalAreaAttInfo
                        {
                            CNY00008PKCal = q.Key,
                            ValueCal = q.Max(s => s.ValueCal),

                        }).ToList();

                    }

                }
                Dictionary<String, decimal> dicFieldValue = new Dictionary<string, decimal>
                {
                    {"Factor", item.UC},
                    {"PlanQuantity", item.OrderQuantity},
                    {"Tolerance", item.Waste},
                    {"PercentUsing", item.PercentUsing},
                    {"FactorProduct", item.FactorProduct}
                };
                decimal demandSoNew = item.CancelFactor * CalFormulaString(_engine, _dtShortCut, formulaString, item.TotalDemandQty, lInfo, item.DecimalRound, dicFieldValue);
                item.TotalDemandQty = demandSoNew;

            }

            foreach (SoBoMDemandNormalInfo itemDel in lDel)
            {
                lDemand.Remove(itemDel);
            }


        }
       
        private string GetAttValueString(bool isFinishing, string attLinkPk, Dictionary<string, AttCrossTemp1Info> dicAtt1)
        {
            if (isFinishing) return "";
            AttCrossTemp1Info info;
            if (dicAtt1.TryGetValue(attLinkPk, out info))
                return info.StringAttValue;
            return "";
        }
        private decimal CalFormulaString(FormulaEngine engine, DataTable dtShortCut, string formularPara, decimal currentValue, List<CalAreaAttInfo> lInfo, int roundDecimal, Dictionary<String, decimal> dicFieldValue = null)
        {
            string formular = formularPara;
            if (string.IsNullOrEmpty(formular)) return currentValue;


            while (true)
            {
                string charA = "[";
                int indexA = formular.IndexOf(charA, StringComparison.Ordinal);
                if (indexA < 0) break;
                string charB = "]";
                int indexB = formular.IndexOf(charB, StringComparison.Ordinal);
                string c = formular.Substring(indexA + 1, indexB - indexA - 1);
                string replace = string.Format("{0}{1}{2}", charA, c, charB);
                decimal value = 0;

                var q2 = dtShortCut.AsEnumerable().Where(p => p.Field<String>("ShortCutText") == c).Select(p => p.Field<Int64>("CNY00008PK")).Where(p => p > 0).ToArray();
                if (q2.Any())
                {
                    Int64 cny00008Pk = q2[0];
                    var q3 = lInfo.Where(p => p.CNY00008PKCal == cny00008Pk).Select(p => p.ValueCal).Where(p => p > 0).ToArray();
                    if (q3.Any())
                    {
                        value = q3[0];
                    }
                }
                formular = formular.Replace(replace, value.ToString());

            }

            if (dicFieldValue != null)
            {
                while (true)
                {
                    string charA = "<";
                    int indexA = formular.IndexOf(charA, StringComparison.Ordinal);
                    if (indexA < 0) break;
                    string charB = ">";
                    int indexB = formular.IndexOf(charB, StringComparison.Ordinal);
                    string c = formular.Substring(indexA + 1, indexB - indexA - 1);
                    string replace = string.Format("{0}{1}{2}", charA, c, charB);
                    decimal value = 0;


                    dicFieldValue.TryGetValue(c, out value);



                    formular = formular.Replace(replace, value.ToString());

                }
            }




            try
            {
                //   MathFormula mF = new MathFormula(formular);//return mF.Value;

                //   FormulaEngine engine = spMain.Document.FormulaEngine;


                ParameterValue result = engine.Evaluate(formular);
                decimal res = (decimal)result.NumericValue;
                return Math.Round(res, roundDecimal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

            //  return r;
        }

        private decimal MrpRounded(decimal numInput, int numTang, int roundedRules, int roundedType, bool isPerform)
        {
            if (!isPerform)
                return numInput;
            return Math.Round(numInput, numTang);
            //if (roundedType == 0)
            //    return Math.Round(numInput, numTang);
            //if (roundedType != 1)
            //    return numInput;

            //decimal result = 0;
            //int lenNumTang = numTang.ToString().Length;
            //int divisor = ArrIntIncrease[lenNumTang];
            //int currentNo = Convert.ToInt32((int)numInput / divisor * divisor);
            //int remainder = Convert.ToInt32((int)numInput % divisor);
            //int iNumTang = numTang;
            //if (remainder > 0)
            //{
            //    if (roundedRules == 2) // giảm
            //        iNumTang = numTang - divisor;
            //}
            //if (roundedRules == 2) // giảm
            //{
            //    result = currentNo - iNumTang;
            //}
            //else
            //{
            //    result = currentNo + iNumTang;
            //}
            //return result;
        }

        private decimal MrpRounded(decimal numInput, MrpRoundedInfo info, bool isPerform)
        {


            if (!isPerform)
                return numInput;

            int numTang = info.DecimalRound;
            return Math.Round(numInput, numTang);
            //int roundedType = info.RoundType;
            //if (roundedType == 0)
            //    return Math.Round(numInput, numTang);
            //if (roundedType != 1)
            //    return numInput;

            //int roundedRules = info.RoundRule;
            //decimal result = 0;
            //int lenNumTang = numTang.ToString().Length;
            //int divisor = ArrIntIncrease[lenNumTang];
            //int currentNo = Convert.ToInt32((int)numInput / divisor * divisor);
            //int remainder = Convert.ToInt32((int)numInput % divisor);
            //int iNumTang = numTang;
            //if (remainder > 0)
            //{
            //    if (roundedRules == 2) // giảm
            //        iNumTang = numTang - divisor;
            //}
            //if (roundedRules == 2) // giảm
            //{
            //    result = currentNo - iNumTang;
            //}
            //else
            //{
            //    result = currentNo + iNumTang;
            //}
            //return result;
        }
        private DataTable TableAttributeTempLoad()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AttibutePK", typeof(Int64));
            dt.Columns.Add("AttibuteCode", typeof(string));
            dt.Columns.Add("AttibuteName", typeof(string));
            dt.Columns.Add("AttibuteValueFull", typeof(string));
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("AttibuteValueTemp", typeof(string));
            dt.Columns.Add("AttibuteUnit", typeof(string));
            dt.Columns.Add("IsNumber", typeof(bool));
            dt.Columns.Add("AttributeLinkPK", typeof(string));
            dt.Columns.Add("ColumnIndex", typeof(int));
            return dt;
        }
        private DataTable TableDetailPrBoMTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PKRow", typeof(Int64));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("CNY00050PKFi", typeof(Int64));
            dt.Columns.Add("QualityCode", typeof(string));
            dt.Columns.Add("Factor", typeof(decimal));
            dt.Columns.Add("IsFinishing", typeof(bool));
            dt.Columns.Add("DecimalRound", typeof(int));
            dt.Columns.Add("CNY00056PK_PR", typeof(Int64));
            dt.Columns.Add("CNY00055PK_PR", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("UC", typeof(decimal));
            dt.Columns.Add("UC_PR", typeof(decimal));
            dt.Columns.Add("Waste", typeof(decimal));
            dt.Columns.Add("Waste_PR", typeof(decimal));
            dt.Columns.Add("PercentUsing", typeof(decimal));
            dt.Columns.Add("PercentUsing_PR", typeof(decimal));
            dt.Columns.Add("RateGram", typeof(decimal));
            dt.Columns.Add("RateGram_PR", typeof(decimal));
            dt.Columns.Add("OrderQuantity", typeof(int));
            dt.Columns.Add("OrderQuantity_PR", typeof(int));
            dt.Columns.Add("AreaPaint", typeof(decimal));
            dt.Columns.Add("AreaPaint_PR", typeof(decimal));
            dt.Columns.Add("FactorProduct", typeof(int));
            dt.Columns.Add("FactorProduct_PR", typeof(int));
            dt.Columns.Add("CancelFactor", typeof(int));
            dt.Columns.Add("CancelFactor_PR", typeof(int));
            dt.Columns.Add("TotalDemandQty", typeof(decimal));
            dt.Columns.Add("PRQty_PR", typeof(decimal));
            dt.Columns.Add("OldDemand", typeof(decimal));
            dt.Columns.Add("PackingFactor", typeof(decimal));
            dt.Columns.Add("PK55SAVE", typeof(Int64));
            dt.Columns.Add("IsRMStandardization", typeof(int));
            dt.Columns.Add("FormulaString", typeof(string));
            dt.Columns.Add("IsPRissuedTypechecking", typeof(bool));
            dt.Columns.Add("IsRightRM", typeof(bool));
            dt.Columns.Add("PurchaseType", typeof(int));
            dt.Columns.Add("Status", typeof(int));
            dt.Columns.Add("Version", typeof(int));
            dt.Columns.Add("CreatedBy", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(string));
            dt.Columns.Add("UpdatedBy", typeof(string));
            dt.Columns.Add("UpdatedDate", typeof(string));
            dt.Columns.Add("SubmittedBy", typeof(string));
            dt.Columns.Add("SubmittedDate", typeof(string));
            dt.Columns.Add("ConfirmedBy", typeof(string));
            dt.Columns.Add("ConfirmedDate", typeof(string));
            dt.Columns.Add("ApprovedBy", typeof(string));
            dt.Columns.Add("ApprovedDate", typeof(string));
            dt.Columns.Add("RejectedBy", typeof(string));
            dt.Columns.Add("RejectedDate", typeof(string));
            dt.Columns.Add("IsGrouping", typeof(bool));
            dt.Columns.Add("IsFormula", typeof(bool));
            dt.Columns.Add("IsPacking", typeof(bool));
            dt.Columns.Add("LeadTime", typeof(int));
            dt.Columns.Add("BookingQuantity", typeof(decimal));
            dt.Columns.Add("IsDelete", typeof(bool));
            dt.Columns.Add("BOMStatus", typeof(int));
            dt.Columns.Add("TDG00001PKBOM", typeof(Int64));
            dt.Columns.Add("DIFFTDG00001PK", typeof(int));
            dt.Columns.Add("RMDescription_002BOM", typeof(string));
            dt.Columns.Add("Reference", typeof(string));
            dt.Columns.Add("Position", typeof(string));
            dt.Columns.Add("AssemblyComponent", typeof(string));
            dt.Columns.Add("TDG00001PKSO", typeof(Int64));
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("ProDimension", typeof(string));
            dt.Columns.Add("RootSOQty", typeof(int));
            dt.Columns.Add("CancelLine", typeof(bool));
            dt.Columns.Add("CancelLine_PR", typeof(bool));
            dt.Columns.Add("Route", typeof(string));
            dt.Columns.Add("Color", typeof(string));
            dt.Columns.Add("CNY00016PKREP", typeof(Int64));
            dt.Columns.Add("RoundRule", typeof(int));
            dt.Columns.Add("RoundType", typeof(int));
            return dt;
        }
    }

}
