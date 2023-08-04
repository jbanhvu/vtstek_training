using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Data;
using System.Data.SqlClient;


namespace CNY_Report.Info
{
    class Inf_002BoMReport
    {
        private readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataSet LoadData(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@BOMHeaderPK", SqlDbType.BigInt) { Value = pk };
            return _ac.DtsReadDataSP("sp_BOM_PrintFinal", arrPara);
        }
    }
}
