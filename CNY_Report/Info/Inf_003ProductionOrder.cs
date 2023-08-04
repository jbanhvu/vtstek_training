using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Report.Info
{
    class Inf_003ProductionOrder
    {
        private readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable LoadSub1(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = pk };
            return _ac.TblReadDataSP("usp_ProductionReport_Sub1", arrPara);
        }
        public DataTable LoadSub2(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = pk };
            return _ac.TblReadDataSP("usp_ProductionReport_Sub2", arrPara);
        }
        public DataTable LoadHeader(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = pk };
            return _ac.TblReadDataSP("usp_ProductionReport_Header", arrPara);
        }
    }
}
