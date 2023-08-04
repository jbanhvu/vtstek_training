using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CNY_BaseSys;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;

namespace CNY_WH.Info
{
    public class Ctrl_MROSave
    {
        public bool Result { get; set; }
        public Int64 PrHeaderPk { get; set; }

        public string Message { get; set; }



        public bool IsInsert { get; set; }
    }
    public class Inf_MaterialReleaseOrder
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public Int64 PK { get; set; }
        public Int64 MROType { get; set; }
        public string MRONo { get; set; }
        public string Description { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public Int64 StatusPK { get; set; }
        public Int32 Version { get; set; }
        public Int32 StatusCode { get; set; }
        public Int32 CheckReject { get; set; }

        public string RejectDescription { get; set; }
        public DataTable LoadPackingWhenView(Int64 pkCode , out string bomUnit, out string purchaseUnit)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TDG00001PK", SqlDbType.BigInt) { Value = pkCode };
            DataSet ds = _ac.DtsReadDataSP("sp_PR_LoadPackingWView", arrpara);

            DataTable dtUnit = ds.Tables[1];
            if (dtUnit.Rows.Count > 0)
            {
                bomUnit = ProcessGeneral.GetSafeString(dtUnit.Rows[0]["BoMUnit"]);
                purchaseUnit = ProcessGeneral.GetSafeString(dtUnit.Rows[0]["PurchaseUnit"]);
            }
            else
            {
                bomUnit = "";
                purchaseUnit = "";
            }

            return ds.Tables[0];
        }



        public DataTable LoadListPackingFinal(Int64 pkCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TDG00001PK", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("sp_PR_LoadAttPackingFinal", arrpara);
        }

        public DataSet PrintPrData(Int64 pkHeader)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = pkHeader };
            return _ac.DtsReadDataSP("sp_PR_PrintFinalF1", arrpara);
        }

        public List<string> GetSortOrderAttribute(Dictionary<Int64, AttributeHeaderInfo> dicAttribute)
        {

            List<string> l = new List<string>();
            if (dicAttribute.Count <= 0) return l;
            var q1 = dicAttribute.Select(p => new
            {
                AttibutePK = p.Key,
                p.Value.AttibuteName
            }).CopyToDataTableNew();


            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = q1 };
            DataTable dt = _ac.TblReadDataSP("sp_WH_MRO_LoadListAttSort", arrpara);


            foreach (DataRow dr in dt.Rows)
            {
                l.Add(ProcessGeneral.GetSafeString(dr["AttibuteName"]));
            }

            return l;
        }

        public DataTable LoadListColorRef(Int64 pkCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TDG00001PK", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("sp_BOM_LoadColorRef", arrpara);
        }
        public DataTable LoadListAlterItemByCode(Int64 pkCode,string type)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@TDG00001PK", SqlDbType.BigInt) { Value = pkCode };
            arrpara[1] = new SqlParameter("@MROType", SqlDbType.NVarChar) { Value = type };
            return _ac.TblReadDataSP("sp_BOM_LoadListAlterItemPr", arrpara);
        }
        public DataTable LoadListPurchaserByCode()
        {
            return _ac.TblReadDataSP("sp_BOM_LoadListPuchaser", null);
        }


        public Dictionary<Int64, bool> LoadListAllowChangeWasteItemCode(DataTable dtPara)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtPara };
            DataTable dt= _ac.TblReadDataSP("sp_PR_GetAllowChangeWasteItemCode", arrpara);
            Dictionary<Int64, bool> dic = dt.AsEnumerable().Select(p=>new
            {
                TDG00001PK =  p.Field<Int64>("TDG00001PK"),
                AllowChangeWaste = p.Field<bool>("AllowChangeWaste"),
            }).ToDictionary(p=>p.TDG00001PK,t=>t.AllowChangeWaste);
            return dic;
        }

        public DataTable LoadListPurchaserByItemCode(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_GetPuchaserByItemCode", arrpara);
        }
        public DataTable LoadListAssCompByItemCode(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_UpdateAssComp", arrpara);
        }



        public DataTable GetFactorAttConvert(Int64 pkCode, DataTable dt, string bomUnit)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@PkProCode", SqlDbType.BigInt) { Value = pkCode };
            arrpara[1] = new SqlParameter("@CountAtt", SqlDbType.Int) { Value = dt.Rows.Count };
            arrpara[2] = new SqlParameter("@BoMUnit", SqlDbType.NVarChar) { Value = bomUnit };
            arrpara[3] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_LoadListFactorInfo", arrpara);
          
        }
        public DataTable GetListSupplierRef(Int64 pkCode, Int64 cny00020Pk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            arrpara[1] = new SqlParameter("@CNY00002PK", SqlDbType.BigInt) { Value = cny00020Pk };
            return _ac.TblReadDataSP("sp_BOM_LoadListSupplierByPkCodePkSuppF2", arrpara);

        }


        public string GetSupplierRefByKey(Int64 pkCode, Int64 cny00020Pk, string supRef, out Int64 tdg00004Pk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            arrpara[1] = new SqlParameter("@CNY00002PK", SqlDbType.BigInt) { Value = cny00020Pk };
            DataTable dt = _ac.TblReadDataSP("sp_BOM_LoadListSupplierByPkCodePkSuppF1", arrpara);
            int rC = dt.Rows.Count;
            tdg00004Pk = 0;
            if (rC <= 0) return "";
            if (rC == 1)
            {
                tdg00004Pk = ProcessGeneral.GetSafeInt64(dt.Rows[0]["TDG00004PK"]);
                return ProcessGeneral.GetSafeString(dt.Rows[0]["SupplierRef"]);
            }
            var q1 = dt.AsEnumerable().Where(p => p.Field<string>("SupplierRef").ToUpper().Trim() == supRef.ToUpper()).Select(p => p.Field<Int64>("TDG00004PK")).ToList();

            if (q1.Any())
            {
                tdg00004Pk = q1.First();
                return supRef;
            }

            return "";
        }
        public DataTable LoadListSupplierByCode(Int64 pkCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("sp_BOM_LoadListSupplierByPkCode", arrpara);
        }
        public DataTable LoadListStock()
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            return _ac.TblReadDataSP("sp_WH_StockList", arrpara);
        }
        public DataTable LoadListSupplierByCodeNew(Int64 pkCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("sp_WH_MRO_LoadListSupplier", arrpara);
        }
        public int DeleteAllData(Int64 headerPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PKHeader", SqlDbType.BigInt) { Value = headerPk };
            arrpara[1] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            return ProcessGeneral.GetSafeInt(_ac.TblReadDataSP("sp_WH_MRO_DeleteAll", arrpara).Rows[0]["ErrCode"]);
        }
        public DataSet DisplayPrDetailSOEdit(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.DtsReadDataSP("sp_PR_DisplayDetailWhenEditSOInfo", arrpara);
        }

        public List<BoMInputAttInfo> LoadListPackingInfoByCode(Int64 pkCode, Dictionary<Int64, AttributeHeaderInfo> dicAttributeRoot, out bool visiblePacking)
        {
            
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TDG00001PK", SqlDbType.BigInt) { Value = pkCode };
            DataSet ds = _ac.DtsReadDataSP("sp_PR_LoadPackingInfo", arrpara);

            DataTable dt = ds.Tables[0];
            List<BoMInputAttInfo> lReturn = dt.AsEnumerable().Where(p=> dicAttributeRoot.Any(t=>t.Key == p.Field<Int64>("AttibutePK"))).Select(f => new BoMInputAttInfo
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
            }).ToList();
            visiblePacking = ds.Tables[1].Rows.Count > 0;


            return lReturn;
        }

        public DataSet DisplayDetailNWhenEdit(Int64 prHeaderPk, string functionCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = functionCode };
            arrpara[1] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = prHeaderPk };
            return _ac.DtsReadDataSP("sp_WH_MRO_DisplayDetailWhenEdit", arrpara);
        }

        public DataSet MRO_Print(Int64 HeaderPk,string functionCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = functionCode };
            arrpara[1] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = HeaderPk };
            return _ac.DtsReadDataSP("sp_WH_MRO_Print", arrpara);
        }
        public DataTable DisplayHeaderWhenEdit(Int64 prHeaderPk, string functionCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = functionCode };
            arrpara[1] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = prHeaderPk };
            return _ac.TblReadDataSP("sp_WH_MRO_LoadHeaderInfoByID", arrpara);
        }
        public bool InsertCNY00056(DataTable dt, int version, string prType)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            arrpara[3] = new SqlParameter("@PRType", SqlDbType.NVarChar) { Value = prType };
            return _ac.BolExcuteSP("sp_PR_InsertCNY00056N", arrpara);
        }
        public bool UpdateCNY00056(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_PR_UpdateCNY00056", arrpara);
        }


        public bool InsertCNY00055(DataTable dt, int version, string prType)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            arrpara[3] = new SqlParameter("@PRType", SqlDbType.NVarChar) { Value = prType };
            return _ac.BolExcuteSP("sp_PR_InsertCNY00055N", arrpara);
        }
        public bool InsertUpdateDetail(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_WH_MRO_InsertUpdateDetail", arrpara);
        }

        public bool InsertDetailAttribute(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_WH_MRO_InsertDetailAttribute", arrpara);
        }
        public bool UpdateDetailAttribute(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_WH_MRO_UpdateDetailAttribute", arrpara);
        }

        public bool InsUpdDelSOHeader(Int64 pkHeader, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = pkHeader };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.BolExcuteSP("usp_PR_PRSO_InsUpdDel", arrpara);
        }

        public bool InsUpdDelColAtt(Int64 pkHeader, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNY00101PK", SqlDbType.BigInt) { Value = pkHeader };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.BolExcuteSP("sp_WH_MROCOL_InsUpdDel", arrpara);
        }

        public Int64 InsertHeader(string functionCode)
        {
            var arrpara = new SqlParameter[10];

            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[1] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = functionCode };
            arrpara[2] = new SqlParameter("@MROType", SqlDbType.BigInt) { Value = MROType };
            arrpara[3] = new SqlParameter("@MRONo", SqlDbType.NVarChar) { Value = MRONo };
            arrpara[4] = new SqlParameter("@Description", SqlDbType.NVarChar) { Value = Description };
            arrpara[5] = new SqlParameter("@Sender", SqlDbType.NVarChar) { Value = Sender };
            arrpara[6] = new SqlParameter("@Recipient", SqlDbType.NVarChar) { Value = Recipient };
            arrpara[7] = new SqlParameter("@StatusPK", SqlDbType.BigInt) { Value = StatusPK };
            arrpara[8] = new SqlParameter("@Version", SqlDbType.Int) { Value = Version };
            arrpara[9] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };

            DataTable dt = _ac.TblReadDataSP("sp_WH_MRO_InsertHeader", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["PKHeader"]);
            return 0;
        }


        public bool UpdateHeader(string functionCode)
        {
            var arrpara = new SqlParameter[13];
            arrpara[0] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = functionCode };
            arrpara[1] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrpara[2] = new SqlParameter("@MROType", SqlDbType.BigInt) { Value = MROType };
            arrpara[3] = new SqlParameter("@MRONo", SqlDbType.NVarChar) { Value = MRONo };
            arrpara[4] = new SqlParameter("@Description", SqlDbType.NVarChar) { Value = Description };
            arrpara[5] = new SqlParameter("@Sender", SqlDbType.NVarChar) { Value = Sender };
            arrpara[6] = new SqlParameter("@Recipient", SqlDbType.NVarChar) { Value = Recipient };
            arrpara[7] = new SqlParameter("@StatusPK", SqlDbType.BigInt) { Value = StatusPK };
            arrpara[8] = new SqlParameter("@StatusCode", SqlDbType.Int) { Value = StatusCode };
            arrpara[9] = new SqlParameter("@Version", SqlDbType.Int) { Value = Version };
            arrpara[10] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };
            arrpara[11] = new SqlParameter("@CheckReject", SqlDbType.Int) { Value = CheckReject };
            arrpara[12] = new SqlParameter("@RejectDescription", SqlDbType.NVarChar) { Value = RejectDescription };
            return _ac.BolExcuteSP("sp_WH_MRO_UpdateHeader", arrpara);

        }



        public DataTable MRO_LoadAttribute_OnNewItem(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_WH_MRO_LoadAttribute_OnNewItem", arrpara);
        }



        public DataTable LoadListUser()
        {
            return _ac.TblReadDataSP("sp_WH_MRO_LoadListUser", null);
        }
        public DataTable LoadListUnit()
        {
            return _ac.TblReadDataSP("sp_BOM_LoadListUnit", null);
        }

        public string GetStatusDescription(int statuscode)
        {
            string sql = "";
            sql += String.Format(" SELECT LTRIM(RTRIM(CNY002)) as StatusDescription FROM [dbo].[CNYMF015] WHERE [CNY004]='PR' AND [CNY001] = {0} ", statuscode);
            DataTable dt = _ac.TblReadDataSQL(sql, null);
            return ProcessGeneral.GetSafeString(dt.Rows[0]["StatusDescription"]);
        }
        public DataTable LoadListProductionOrderW(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@typeCust", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("usp_PR_LoadListProductionW", null);
        }
        public DataTable LoadListCustomerW()
        {
            return _ac.TblReadDataSP("usp_PR_LoadListCustomerW", null);
        }
        public DataTable LoadListCustomer()
        {
            return _ac.TblReadDataSP("usp_PR_LoadListCustomer", null);
        }
        public DataSet GetTableRejectOnStatus(string FunctionCode, Int64 HeaderPK,Int64 StatusPK)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = FunctionCode };
            arrpara[1] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = HeaderPK };
            arrpara[2] = new SqlParameter("@StatusPK", SqlDbType.BigInt) { Value = StatusPK };
            return _ac.DtsReadDataSP("sp_WH_MRO_RejectOnStatus", arrpara);


        }
        public DataTable LoadDataGridViewMain(string sqlWhere , string[] arrPrType)
        {
            
            string where = string.Join(",", arrPrType.Select(p => string.Format("'{0}'", p)).ToArray()).Trim();
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[1] = new SqlParameter("@UserCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            arrpara[2] = new SqlParameter("@Conds", SqlDbType.NVarChar) { Value = sqlWhere };
            arrpara[3] = new SqlParameter("@MROType", SqlDbType.NVarChar) { Value = where };
            return _ac.TblReadDataSP("sp_WH_MRO_LoadGridMain", arrpara);
        }
        
        public DataTable GetTableStatus(string FunctionCode, DataTable dtAdvanceFunc)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = FunctionCode };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtAdvanceFunc };
            return _ac.TblReadDataSP("sp_WH_MRO_SearchStatus", arrpara);
            
        }
        public DataTable LoadMaterialReleaseOrderType()
        {
            return _ac.TblReadDataSP("sp_WH_MRO_Type", null);
        }

        public DataTable MRO_LoadNewItem(string MROType, DataTable dtTDG00001PK, string Fillter)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[1] = new SqlParameter("@MROType", SqlDbType.NVarChar) { Value = MROType };
            arrpara[2] = new SqlParameter("@Conds", SqlDbType.NVarChar) { Value = Fillter };
            arrpara[3] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtTDG00001PK };
            

            return _ac.TblReadDataSP("sp_WH_MRO_LoadNewItem", arrpara);
        }
        public DataTable LoadGridSelectRmNormalWhenGenerateS(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_LoadListRMBOMFINAL_Supplement", arrpara);
        }

        public DataTable LoadGridSelectRmNormalWhenGenerateN(DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_LoadListRMBOMNFINAL", arrpara);
        }


        public DataTable LoadGridDataFinishingAreaFi(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_Fi_GetDataAreaPaintF1", arrpara);
        }


        public DataSet LoadGridSelectRmNormalWhenGenerateCustomer(DataTable dt, Int32 round)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@round", SqlDbType.Int) { Value = round };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.DtsReadDataSP("sp_PR_LoadListRMBOMFINAL_Customer", arrpara);
        }

        public DataTable CheckPrBeforSave(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("usp_PR_CheckPRBeforSave", arrpara);
        }

        public bool FeedBackPr(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.BolExcuteSP("sp_PR_FeedBackBOMByBoMPk", arrpara);
        }
        public DataTable CheckPrBeforSaveN(DataTable dt,Int64 cny00054pk)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = cny00054pk };
            arrpara[2] = new SqlParameter("@typeTest", SqlDbType.Structured) { Value = dt };

            return _ac.TblReadDataSP("usp_PR_CheckPRBeforSaveN1", arrpara);
        }

        public DataTable LoadGridSelectCategoryWhenGenerate(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_LoadListGlobalGroup", arrpara);
        }


        public DataTable LoadTablePrGenerateFinalCustomer(string tableName, string fieldAttSelGrp)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName };
            arrpara[1] = new SqlParameter("@FliedSelGrpAtt", SqlDbType.NVarChar) { Value = fieldAttSelGrp };
            return _ac.TblReadDataSP("sp_PR_GroupByRowGenerateFinal_Customer", arrpara);
        }
        public DataTable LoadTablePrGenerateFinal(string tableName , string fieldAttSelGrp, string fieldAttWhere)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName };
            arrpara[1] = new SqlParameter("@FliedSelGrpAtt", SqlDbType.NVarChar) { Value = fieldAttSelGrp };
            arrpara[2] = new SqlParameter("@FliedWhereAtt", SqlDbType.NVarChar) { Value = fieldAttWhere };
            return _ac.TblReadDataSP("sp_PR_GroupByRowGenerateFinal", arrpara);
        }
        public DataTable LoadAttributeWhenGenerate(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_LoadListAttributeGenerate", arrpara);
        }
        public bool CreateTableSqlServer(DataTable dt, string tableName)
        {
            
            return _ac.CreateTableSqlFromDataTable(dt, tableName);
        }
        public bool CopyDataFromDataTableToSqlTable(DataTable dt, string tableName)
        {

            return _ac.CopyDataFromDataTableToSqlTable(dt, tableName);
        }

        public bool DropSqlTable(string tableName)
        {
            string sql = string.Format("drop table {0}", tableName);
            return _ac.BolExcuteSQL(sql, null);
        }
        public bool BolExcuteSqlText(string sql)
        {
            return _ac.BolExcuteSQL(sql, null);
        }

      
        public Int64 GetPkHeaderCode(string tableCode, string itemCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@TableCode", SqlDbType.NVarChar) { Value = tableCode };
            arrpara[1] = new SqlParameter("@ItemCode", SqlDbType.NVarChar) { Value = itemCode };
            DataTable dt= _ac.TblReadDataSP("usp_PR_GetDefineCodeHeader", arrpara);
            return ProcessGeneral.GetSafeInt64(dt.Rows[0]["PKHeader"]);
        }
    }
}
