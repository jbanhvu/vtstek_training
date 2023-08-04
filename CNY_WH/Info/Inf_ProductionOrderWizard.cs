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
    public class PrGlobalCategoryInfo
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Int64 Index { get; set; }
        public string FormulaString { get; set; }
        public string FormulaStringDisplay { get; set; }
        public string PurchaseUnit { get; set; }
    }
    public class PurchaserGenerateInfo
    {
        public Int64 CNY00004PK { get; set; }
        public string Purchaser { get; set; }
    }
    public class PRBOMIDNOINFO
    {
        public string StrBOMID { get; set; }
        public string StrBoMNo { get; set; }
    }
    public class PrSOGenerateInfoStep1
    {

        public string Customer { get; set; }
        public string CustomerOrderNo { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string DeliveryDate { get; set; }
        public string ProductionOrder { get; set; }
        public string OrderNo { get; set; }
        public string OrderLine { get; set; }
        public string MainMaterialGroup { get; set; }
        public string ProductCode { get; set; }
        public string Reference { get; set; }
        public string ProductName { get; set; }
        public Int32 OrderQuantity { get; set; }
        public Int32 PlanQuantity { get; set; }
        public Int32 BalanceQuantity { get; set; }
        public string FinishingColor { get; set; }
        public Int64 TDG00001PK { get; set; }
        public Int64 CNY00020PK { get; set; }
        public Int64 CNY00019PK { get; set; }
        public Int64 ChildPK { get; set; }
        public Int64 ParentPK { get; set; }
        public string ProDimension { get; set; }

        public string BOMID { get; set; }
        public string BoMNo { get; set; }
    }
    public class Inf_ProductionOrderWizard
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable LoadReportUcChanged(string sqlWhere)
        {

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Conds", SqlDbType.NVarChar) { Value = sqlWhere };
            return _ac.TblReadDataSP("sp_PR_DisplayReportChangeUC", arrpara);
        }
        public DataTable BoM_LoadTableShortCut()
        {
            return _ac.TblReadDataSP("BoM_LoadTableShortCut", null);
        }



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
            DataTable dt = _ac.TblReadDataSP("sp_PR_LoadListAttSort", arrpara);


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
        public DataTable LoadListAlterItemByCode(Int64 pkCode,Int64 cny00016Pk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PkProCode", SqlDbType.BigInt) { Value = pkCode };
            arrpara[1] = new SqlParameter("@CNY00016PK", SqlDbType.BigInt) { Value = cny00016Pk };
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
        public DataTable LoadListSupplierByCodeNew(Int64 pkCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("sp_BOM_LoadListSupplierBOM", arrpara);
        }
        public int DeleteAllPrData(Int64 headerPk, out string mess)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PKHeader", SqlDbType.BigInt) { Value = headerPk };
            arrpara[1] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            DataTable dt = _ac.TblReadDataSP("sp_Pr_DeleteAll", arrpara);
            mess = ProcessGeneral.GetSafeString(dt.Rows[0]["ErrMess"]);
            return ProcessGeneral.GetSafeInt(dt.Rows[0]["ErrCode"]);
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

        public DataSet DisplayPrDetailNWhenEdit(Int64 prHeaderPk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = prHeaderPk };
            return _ac.DtsReadDataSP("sp_PR_DisplayDetailWhenEdit", arrpara);
        }
        public DataTable DisplayPrHeaderWhenEdit(Int64 prHeaderPk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = prHeaderPk };
            return _ac.TblReadDataSP("sp_PR_LoadHeaderInfoByID", arrpara);
        }
        public bool InsertCNY00056(DataTable dt, int version, string prType)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            arrpara[3] = new SqlParameter("@PRType", SqlDbType.NVarChar) { Value = prType };
            return _ac.BolExcuteSP("sp_PR_InsertCNY00056N1", arrpara);
        }
        public bool UpdateCNY00056(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_PR_UpdateCNY00056N1", arrpara);
        }


        public bool InsertCNY00055(DataTable dt, int version, string prType)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            arrpara[3] = new SqlParameter("@PRType", SqlDbType.NVarChar) { Value = prType };
            return _ac.BolExcuteSP("sp_PR_InsertCNY00055N1", arrpara);
        }
        public bool UpdateCNY00055(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_PR_UpdateCNY00055N1", arrpara);
        }

        public bool InsertPrDetailAttribute(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_Pr_InsertDetailAttribute", arrpara);
        }
        public bool UpdatePrDetailAttribute(DataTable dt, int version)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper().Trim() };
            return _ac.BolExcuteSP("sp_Pr_UpdateDetailAttribute", arrpara);
        }

        public bool InsUpdDelSOHeader(Int64 pkHeader, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = pkHeader };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.BolExcuteSP("usp_PR_PRSO_InsUpdDel", arrpara);
        }

        public bool ClearRowErrorWhenSav(Int64 pkHeader)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = pkHeader };
            return _ac.BolExcuteSP("usp_PR_ClearRowErrorWhenSave", arrpara);
        }
        public bool InsUpdDelColAtt(Int64 pkHeader, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = pkHeader };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.BolExcuteSP("usp_PR_PRCOL_InsUpdDel", arrpara);
        }

        public Int64 InsertPrHeader(string prNo, string prType, string etd, string eta, string refDoc, string senderUser, string reciUser, int version, int status, string note,
               string releaseBy, DateTime releaseDate, string confirmBy, DateTime confirmDate, string approveBy, DateTime approveDate)
        {
            var arrpara = new SqlParameter[17];

            arrpara[0] = new SqlParameter("@CNY001_PRNo", SqlDbType.NVarChar) { Value = prNo };
            arrpara[1] = new SqlParameter("@CNY002_PRType", SqlDbType.NVarChar) { Value = prType };
            arrpara[2] = new SqlParameter("@CNY003_ETD", SqlDbType.NVarChar) { Value = etd };
            arrpara[3] = new SqlParameter("@CNY004_ETA", SqlDbType.NVarChar) { Value = eta };
            arrpara[4] = new SqlParameter("@CNY005_RefDoc", SqlDbType.NVarChar) { Value = refDoc };
            arrpara[5] = new SqlParameter("@CNY006_SendUser", SqlDbType.NVarChar) { Value = senderUser };
            arrpara[6] = new SqlParameter("@CNY007_ReciUser", SqlDbType.NVarChar) { Value = reciUser };
            arrpara[7] = new SqlParameter("@CNY008_Version", SqlDbType.Int) { Value = version };
            arrpara[8] = new SqlParameter("@CNY009_Status", SqlDbType.Int) { Value = status };
            arrpara[9] = new SqlParameter("@CNY018_Note", SqlDbType.NVarChar) { Value = note };
            arrpara[10] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };


            arrpara[11] = new SqlParameter("@CNY014_ReleasedBy", SqlDbType.NVarChar) { Value = releaseBy };
            arrpara[12] = new SqlParameter("@CNY015_ReleasedDate", SqlDbType.DateTime) { Value = releaseDate };
            arrpara[13] = new SqlParameter("@CNY016_ApprovedBy", SqlDbType.NVarChar) { Value = approveBy };
            arrpara[14] = new SqlParameter("@CNY017_ApprovedDate", SqlDbType.DateTime) { Value = approveDate };
            arrpara[15] = new SqlParameter("@CNY019_ConfirmedBy", SqlDbType.NVarChar) { Value = confirmBy };
            arrpara[16] = new SqlParameter("@CNY020_ConfirmedDate", SqlDbType.DateTime) { Value = confirmDate };
            DataTable dt = _ac.TblReadDataSP("sp_PR_InsertHeader", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["PKHeader"]);
            return 0;
        }


        public bool UpdatePrHeader(Int64 prPk, string etd, string eta, string refDoc, string senderUser, string reciUser, int version, int status, string note,
            string releaseBy, DateTime releaseDate, string confirmBy, DateTime confirmDate, string approveBy, DateTime approveDate)
        {
            var arrpara = new SqlParameter[16];
            arrpara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = prPk };
            arrpara[1] = new SqlParameter("@CNY003_ETD", SqlDbType.NVarChar) { Value = etd };
            arrpara[2] = new SqlParameter("@CNY004_ETA", SqlDbType.NVarChar) { Value = eta };
            arrpara[3] = new SqlParameter("@CNY005_RefDoc", SqlDbType.NVarChar) { Value = refDoc };
            arrpara[4] = new SqlParameter("@CNY006_SendUser", SqlDbType.NVarChar) { Value = senderUser };
            arrpara[5] = new SqlParameter("@CNY007_ReciUser", SqlDbType.NVarChar) { Value = reciUser };
            arrpara[6] = new SqlParameter("@CNY008_Version", SqlDbType.Int) { Value = version };
            arrpara[7] = new SqlParameter("@CNY009_Status", SqlDbType.Int) { Value = status };
            arrpara[8] = new SqlParameter("@CNY018_Note", SqlDbType.NVarChar) { Value = note };
            arrpara[9] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };

            arrpara[10] = new SqlParameter("@CNY014_ReleasedBy", SqlDbType.NVarChar) { Value = releaseBy };
            arrpara[11] = new SqlParameter("@CNY015_ReleasedDate", SqlDbType.DateTime) { Value = releaseDate };
            arrpara[12] = new SqlParameter("@CNY016_ApprovedBy", SqlDbType.NVarChar) { Value = approveBy };
            arrpara[13] = new SqlParameter("@CNY017_ApprovedDate", SqlDbType.DateTime) { Value = approveDate };
            arrpara[14] = new SqlParameter("@CNY019_ConfirmedBy", SqlDbType.NVarChar) { Value = confirmBy };
            arrpara[15] = new SqlParameter("@CNY020_ConfirmedDate", SqlDbType.DateTime) { Value = confirmDate };
            return _ac.BolExcuteSP("sp_PR_UpdateHeader", arrpara);

        }







        public DataTable LoadListUser()
        {
            return _ac.TblReadDataSP("usp_PR_LoadListUser", null);
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
            return _ac.TblReadDataSP("usp_PR_LoadListProductionW", arrpara);
        }


        public DataTable LoadListProductionOrderW()
        {
            return _ac.TblReadDataSP("usp_PR_LoadListProductionW1", null);
        }
        public DataTable LoadListCustomerW()
        {
            return _ac.TblReadDataSP("usp_PR_LoadListCustomerW", null);
        }
        public DataTable LoadListCustomer()
        {
            return _ac.TblReadDataSP("usp_PR_LoadListCustomer", null);
        }
        public DataTable LoadDataGridViewPrNewMain_Use(string sqlWhere , string[] arrPrType)
        {
            
            string where = string.Join(" OR ", arrPrType.Select(p => string.Format("(a.CNY002 ='{0}')", p)).ToArray()).Trim();
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@UserCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            arrpara[1] = new SqlParameter("@Conds", SqlDbType.NVarChar) { Value = sqlWhere };
            arrpara[2] = new SqlParameter("@PRType", SqlDbType.NVarChar) { Value = where };
            return _ac.TblReadDataSP("usp_PR_LoadGridMain", arrpara);
        }
        public DataSet LoadDataGridViewSoWhenGenerateNormal(DataTable dtCust , DataTable dtPro)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@typeCust", SqlDbType.Structured) { Value = dtCust };
            arrpara[1] = new SqlParameter("@typeProduction", SqlDbType.Structured) { Value = dtPro };
            return _ac.DtsReadDataSP("sp_WH_SDR_LoadListWOGenerate", arrpara);
        }

        public DataTable LoadKhccnlvt(DataTable dtCust, DataTable dtPro,string projectName)
        {//
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@round", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@ProjectName", SqlDbType.NVarChar) { Value = projectName };
            arrpara[2] = new SqlParameter("@typeCust", SqlDbType.Structured) { Value = dtCust };
            arrpara[3] = new SqlParameter("@typeProduction", SqlDbType.Structured) { Value = dtPro };
            DataSet ds = _ac.DtsReadDataSP("sp_PR_LoadListSOGenerateFinalRptNew", arrpara);

            if (ds.Tables[0].Rows.Count <= 0 && ds.Tables[1].Rows.Count <= 0) return ds.Tables[0].Clone();

            var q1 = ds.Tables[0].AsEnumerable().Concat(ds.Tables[1].AsEnumerable())
                .OrderBy(p => p.Field<string>("SearchName"))
                .ThenBy(p => p.Field<string>("ProOrderNo")).ThenBy(p => p.Field<string>("Reference"))
                .ThenBy(p => p.Field<string>("MainMaterialGroup")).ThenBy(p => p.Field<string>("RMCode_001"))
                .ThenBy(p => p.Field<string>("ColorReference")).ThenBy(p => p.Field<string>("Supplier"))
                .ThenBy(p => p.Field<string>("SupplierRef")).CopyToDataTable();
            return q1;


        }


        public DataTable GetTableStatus(string moduleCode)
        {
            return _ac.TblReadDataSQL(string.Format("SELECT LTRIM(RTRIM([CNY001])) AS CodePer,LTRIM(RTRIM(CNY002)) as DescPer FROM [dbo].[CNYMF015] WHERE [CNY004]='{0}'", moduleCode), null);
        }

        public DataTable LoadGridSelectRmNormalWhenGenerateS(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PR_LoadListRMBOMFINAL_Supplement", arrpara);
        }

        public DataTable LoadGridSelectRmNormalWhenGenerateN(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
          //  arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_WH_SDR_LoadListItemGenerate", arrpara);
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
        public DataTable CheckPrBeforSaveN(DataTable dt, Int64 cny00054pk)
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
        public DataTable LoadTablePrGenerateFinal(string tableName, string fieldAttSelGrp, string fieldAttWhere)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName };
            arrpara[1] = new SqlParameter("@FliedSelGrpAtt", SqlDbType.NVarChar) { Value = fieldAttSelGrp };
            arrpara[2] = new SqlParameter("@FliedWhereAtt", SqlDbType.NVarChar) { Value = fieldAttWhere };
            return _ac.TblReadDataSP("sp_WH_SDR_GroupByRowItemGenerateFinal", arrpara);
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
