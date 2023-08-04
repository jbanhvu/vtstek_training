using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.Info
{
    public class Inf_Finishing
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable BoM_LoadListBomFinishingGenerateInfo(Int64 cny00012Pk, Int64 cny00050Pk, Int64 cny00051Pk)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[1] = new SqlParameter("@CNY00050PK", SqlDbType.BigInt) { Value = cny00050Pk };
            arrpara[2] = new SqlParameter("@CNY00051PK", SqlDbType.BigInt) { Value = cny00051Pk };
            return _ac.TblReadDataSP("sp_BOM_DisplayDetailWhenGenerate_FinishingFinalF1", arrpara);
        }
        public DataTable BoM_LoadListBomCopyWizard(Int64 cny00019Pk, string proOrderNo, string projectNo, string projectName, Int32 bomNo, bool isLoad)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            arrpara[1] = new SqlParameter("@ProOrderNo", SqlDbType.NVarChar) { Value = proOrderNo };
            arrpara[2] = new SqlParameter("@ProjectNo", SqlDbType.NVarChar) { Value = projectNo };
            arrpara[3] = new SqlParameter("@ProjectName", SqlDbType.NVarChar) { Value = projectName };
            arrpara[4] = new SqlParameter("@BoMNo", SqlDbType.Int) { Value = bomNo };
            arrpara[5] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = isLoad ? DeclareSystem.SysUserName.Trim() : "" };
            return _ac.TblReadDataSP("sp_CNY_BOM_LoadListBOMCopyFinishingFinalF2", arrpara);
        }
        public DataTable BoMMapsSODataCopy(Int64 cNy00019Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cNy00019Pk };
            return _ac.TblReadDataSP("sp_BoM_LoadSoMappingCopyF2Final", arrpara);
        }
        public DataTable LoadListFinishingColorByBomPkCopy(Int64 bomHeaderPk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@BOMHEADERPK", SqlDbType.BigInt) { Value = bomHeaderPk };
            return _ac.TblReadDataSP("sp_BoM_LoadListFinshingColorByBoMIDF2Copy", arrpara);
        }
        public DataTable LoadListPurchaseType()
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Code", typeof(int));
            //dt.Columns.Add("Description", typeof(string));
            //dt.Rows.Add(0, "Supplier");
            //dt.Rows.Add(1, "Customer");
            //return dt;

            return _ac.TblReadDataSP("sp_BoM_LoadListVendorMode", null);
        }
        public DataSet PrintBoMData(Int64 pkHeader,Int64 cny00019pk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@BOMHeaderPK", SqlDbType.BigInt) { Value = pkHeader };
            arrpara[1] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019pk };
            return _ac.DtsReadDataSP("sp_BOM_PrintFinal_Fi", arrpara);
        }
        public Int64 InsertUpdateBomHeader(Int64 cny00019pk,  string remark)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019pk };
            arrpara[1] = new SqlParameter("@CNY005_Remark", SqlDbType.NVarChar) { Value = remark };
            arrpara[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };
            DataTable dt = _ac.TblReadDataSP("sp_BOM_InsertUpdateHeaderFi", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["PKBOMHeader"]);
            return 0;
        }


        public bool BoM_DeleteColorFinishing(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.BolExcuteSP("sp_BoM_DelteFinishingColorHeader", arrpara);
        }
        public DataTable LoadListItemCodeWithSupplier()
        {
            return _ac.TblReadDataSP("sp_BOM_LoadListItemCodeFullFinal_R_F2", null);
        }

        public DataTable LoadListSupplierByCode(Int64 pkCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("sp_BOM_LoadListSupplierByPkCode", arrpara);
        }
        public DataTable GetListSupplierRef(Int64 pkCode, Int64 cny00020Pk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            arrpara[1] = new SqlParameter("@CNY00002PK", SqlDbType.BigInt) { Value = cny00020Pk };
            return _ac.TblReadDataSP("sp_BOM_LoadListSupplierByPkCodePkSuppF2", arrpara);

        }

        public DataTable LoadListSupplierByCodeNew(Int64 pkCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("sp_BOM_LoadListSupplierBOMF1", arrpara);
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
        public DataTable GetColorDisplayByCodeFinishing(Int64 cny00019Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            return _ac.TblReadDataSP("usp_BOM_LoadColorByCode_Finishing", arrpara);
        }
        public bool BoM_InsertCNY051(Int64 cny051Pk, Int64 cny012Pk, Int64 cny050Pk, int index, int version)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@CNY00051PK", SqlDbType.BigInt) { Value = cny051Pk };
            arrpara[1] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny012Pk };
            arrpara[2] = new SqlParameter("@CNY00050PK", SqlDbType.BigInt) { Value = cny050Pk };

            arrpara[3] = new SqlParameter("@Index", SqlDbType.Int) { Value = index };
            arrpara[4] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };
            arrpara[5] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            return _ac.BolExcuteSP("sp_BOM_InsertCNY051", arrpara);
        }
       

        public DataTable LoadListItemCode()
        {
            return _ac.TblReadDataSP("sp_BOM_LoadListItemCodeFullFinal_R_F1", null);
        }
        public bool BolExcuteSql(string sql)
        {
            return _ac.BolExcuteSQL(sql, null);
        }
        public bool BoM_InsertCNY052(DataTable dt, Int32 version, Int64 cny00012Pk, bool approveFinishing)
        {
            var arrpara = new SqlParameter[5];
            arrpara[0] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[1] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };
            arrpara[2] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[3] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[4] = new SqlParameter("@ApproveFinishing", SqlDbType.Bit) { Value = approveFinishing };
            return _ac.BolExcuteSP("sp_BOM_InsertCNY052", arrpara);
        }

        public bool BoM_UpdateCNY052(DataTable dt, Int32  version, bool approveFinishing)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[1] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };
            arrpara[2] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[3] = new SqlParameter("@ApproveFinishing", SqlDbType.Bit) { Value = approveFinishing };
            return _ac.BolExcuteSP("sp_BOM_UpdateCNY052", arrpara);
        }
        public bool BoM_UpdateCNY053(DataTable dt, Int32 version, bool approveFinishing)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[2] = new SqlParameter("@ApproveFinishing", SqlDbType.Bit) { Value = approveFinishing };
            return _ac.BolExcuteSP("sp_BOM_UpdateCNY053", arrpara);
        }
        public bool BoM_InsertCNY053(DataTable dt, Int32 version, Int64 cny00012Pk, bool approveFinishing)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[1] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[2] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[3] = new SqlParameter("@ApproveFinishing", SqlDbType.Bit) { Value = approveFinishing };
            return _ac.BolExcuteSP("sp_BOM_InsertCNY053", arrpara);
        }

        public bool BoM_FeedBackPurchaseStatus(DataTable dt, Int32 version, bool approveFinishing)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Version", SqlDbType.Int) { Value = version };
            arrpara[1] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName.ToUpper() };
            arrpara[2] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            arrpara[3] = new SqlParameter("@ApproveFinishing", SqlDbType.Bit) { Value = approveFinishing };
            return _ac.BolExcuteSP("sp_BOM_FeedBackPurchaseStatus", arrpara);
        }


        public DataTable BOM_LoadQualityCodeGenerate()
        {
            return _ac.TblReadDataSP("usp_BoM_LoadColorQualityCodeGenerate", null);
        }

      
        public DataTable LoadListStepWork()
        {
            return _ac.TblReadDataSP("sp_BOM_LoadListStepWork", null);
        }
        public DataTable LoadListUnit()
        {
            return _ac.TblReadDataSP("sp_BOM_LoadListUnit", null);
        }
    }
}
