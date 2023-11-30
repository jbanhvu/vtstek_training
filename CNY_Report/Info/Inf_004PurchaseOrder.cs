using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Report.Info
{
    class Inf_004PurchaseOrder
    {
        private readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataSet LoadHeader(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = pk };
            return _ac.DtsReadDataSP("sp_GetPOReportHeader", arrPara);
        }
        public DataSet LoadDetail(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = pk };
            return _ac.DtsReadDataSP("sp_GetPOReportDetail", arrPara);
        }
    }
}
