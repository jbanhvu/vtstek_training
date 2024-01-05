using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;

namespace CNY_Buyer.Info
{
    class Inf_TimeKeeper
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_TimeKeeper_Select_1(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return _ac.TblReadDataSP("sp_TimeKeeper_Select_1", arrPara);
        }
        public DataTable sp_TimeKeeper_Update(Int64 PK, Int64 EncrollNumber, DateTime Record, DateTime InsertDateTime)
        {
            var arrPara = new SqlParameter[4];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@EncrollNumber", SqlDbType.BigInt) { Value = EncrollNumber};
            arrPara[2] = new SqlParameter("@Record", SqlDbType.DateTime) { Value = Record };
            arrPara[3] = new SqlParameter("@InsertDateTime", SqlDbType.DateTime) { Value = InsertDateTime };
            return _ac.TblReadDataSP("sp_TimeKeeper_Update", arrPara);
        }
    }
}
