using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.Info
{
    internal class Inf_Progress
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable TblExcuteSqlText(string sql)
        {
            return _ac.TblReadDataSQL(sql, null);

        }
        public DataTable BoM_LoadTableShortCut()
        {
            return _ac.TblReadDataSP("BoM_LoadTableShortCut", null);
        }
        public DataSet GetDetailDataWhenLoadNew(Int64 cny00019Pk, Int64 cny00012Pk, Int64 cny00054Pk)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            arrpara[1] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[2] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = cny00054Pk };
            return _ac.DtsReadDataSP("sp_MRP_RptProgress", arrpara);
        }
        public DataTable GetTable55BByPrHeader(Int64 cny00054pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = cny00054pk };
            return _ac.TblReadDataSP("usp_MRP_GetDetail55BInfoByHeaderPK_Rpt", arrpara);
        }
        public DataTable GetPackingFactorBeforLoad(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TBLGROUPATT", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_MRP_GetPackingFactorForAtt_Rpt", arrpara);
        }
        public DataTable GetAttByPrLine(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_MRP_GetAttByPRLine_Rpt", arrpara);
        }
        public DataTable GetAttGroupMethold2(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_MRP_GetAttGroupMethold2_Rpt", arrpara);
        }
        public DataTable GetSoHeaderInfo(Int64 cny00019Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            return _ac.TblReadDataSP("sp_MRP_LoadSOHeaderInfo_Rpt", arrpara);
        }
        public Int64 GetBomFinishingPk(Int64 cny00019Pk)
        {
            DataTable dt = _ac.TblReadDataSQL(string.Format("SELECT PK FROM dbo.CNY00012 WHERE CNY00019PK={0}", cny00019Pk), null);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["PK"]);
            return 0;
        }
        public Int64 GetPrHeaderPK(Int64 cny00019Pk)
        {
            DataTable dt = _ac.TblReadDataSQL(string.Format("SELECT CNY00054PK FROM dbo.PRSOMAPPING WHERE IsNew=1 AND CNY00019PK={0}", cny00019Pk), null);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["CNY00054PK"]);
            return 0;
        }


    }
}
