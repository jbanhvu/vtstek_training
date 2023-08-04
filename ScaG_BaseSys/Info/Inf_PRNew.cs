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
    public class Inf_PRNew
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataSet GetDetailDataWhenLoad(Int64 cny00019Pk, Int64 cny00012Pk, Int64 cny00054Pk)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            arrpara[1] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[2] = new SqlParameter("@CNY00054PK", SqlDbType.BigInt) { Value = cny00054Pk };
            return _ac.DtsReadDataSP("sp_PR_LoadListRMBOMSOALL", arrpara);
        }
        public string GetStatusDescription(int statuscode)
        {
            string sql = "";
            sql += String.Format(" SELECT LTRIM(RTRIM(CNY002)) as StatusDescription FROM [dbo].[CNYMF015] WHERE [CNY004]='PR' AND [CNY001] = {0} ", statuscode);
            DataTable dt = _ac.TblReadDataSQL(sql, null);
            return ProcessGeneral.GetSafeString(dt.Rows[0]["StatusDescription"]);
        }
        public DataTable GetSoHeaderInfo(Int64 cny00019Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            return _ac.TblReadDataSP("sp_PRNEW_LoadSOHeaderInfo", arrpara);
        }
        public DataTable GetPrHeaderInfo(Int64 cny00019Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            return _ac.TblReadDataSP("sp_PRNEW_LoadPRHeaderInfo", arrpara);
        }
        public Int64 GetPkHeaderCode(string tableCode, string itemCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@TableCode", SqlDbType.NVarChar) { Value = tableCode };
            arrpara[1] = new SqlParameter("@ItemCode", SqlDbType.NVarChar) { Value = itemCode };
            DataTable dt = _ac.TblReadDataSP("usp_PR_GetDefineCodeHeader", arrpara);
            return ProcessGeneral.GetSafeInt64(dt.Rows[0]["PKHeader"]);
        }
    }
}
