using CNY_BaseSys;
using CNY_BaseSys.Common;
using System.Data;
using System.Data.SqlClient;
using System;

namespace CNY_Report.Info
{
    class Inf_005PurchaseRequisition
    {
        private readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataSet LoadData(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = PK };
            return _ac.DtsReadDataSP("sp_PR_PrintFinal", arrPara);
        }
    }
}
