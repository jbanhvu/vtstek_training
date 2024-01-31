using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_Buyer.Info
{
    public class Inf_PaymentRequest
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_PaymentRequest_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return _ac.TblReadDataSP("sp_PaymentRequest_Select", arrPara);
        }
        public DataTable sp_Leave_Update(Int64 PK, Int64 StaffPK, DateTime FromDate, DateTime ToDate, string Note)
        {
            var arrPara = new SqlParameter[5];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@StaffPK", SqlDbType.BigInt) { Value = StaffPK };
            arrPara[2] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            arrPara[3] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            arrPara[4] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = Note };
            return _ac.TblReadDataSP("sp_Leave_Update", arrPara);
        }
        #region  Excute
        public DataTable Excute(string SQL)
        {
            return _ac.TblReadDataSQL(SQL, null);
        }
        #endregion
    }
}
