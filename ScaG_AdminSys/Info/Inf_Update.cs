using System.Data;
using CNY_BaseSys.Common;
using System.Data.SqlClient;
using CNY_BaseSys;

namespace CNY_AdminSys.Info
{
    public class Inf_Update
    {
        private readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
        public bool Machine_UpdateDomain(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TypeUpdate", SqlDbType.Structured) { Value = dt };
            return ac.BolExcuteSP("usp_Update_UpdateMachineDomain", arrpara);
        }
        public bool Machine_Update(string strDel, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@StringDel", SqlDbType.NVarChar) { Value = strDel };
            arrpara[1] = new SqlParameter("@TypeUpdate", SqlDbType.Structured) { Value = dt };
            return ac.BolExcuteSP("usp_Update_UpdateMachine", arrpara);
        }
        public DataTable Machine_Load()
        {
            return ac.TblReadDataSP("usp_Update_LoadMachine", null);
        }
        public bool Report_Update(string strDel, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@StringDel", SqlDbType.NVarChar) { Value = strDel };
            arrpara[1] = new SqlParameter("@TypeUpdate", SqlDbType.Structured) { Value = dt };
            return ac.BolExcuteSP("usp_Update_UpdateReport", arrpara);
        }
        public DataTable Report_Load()
        {
            return ac.TblReadDataSP("usp_Update_LoadReport", null);
        }
        public bool Component_Update(string strDel,DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@StringDel", SqlDbType.NVarChar) { Value = strDel };
            arrpara[1] = new SqlParameter("@TypeUpdate", SqlDbType.Structured) { Value = dt };
            return ac.BolExcuteSP("usp_Update_UpdateComponent", arrpara);
        }
        public DataTable Component_Load()
        {
            return ac.TblReadDataSP("usp_Update_LoadComponent", null);
        }
    }
}
