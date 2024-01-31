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
     class Inf_LaborContract
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_LaborContract_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return _ac.TblReadDataSP("sp_LaborContract_Select", arrPara);
        }
        public DataTable sp_LaborContract_Update(Int64 PK, Int64 StaffPK, DateTime SignDate, int Duration, int LaborType,
            DateTime ExpiredDate, DateTime CreatedDate, string CreatedUser, DateTime UpdatedDate, string UpdatedUser)
        {
            var arrPara = new SqlParameter[10];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@StaffPK", SqlDbType.BigInt) { Value = StaffPK };
            arrPara[2] = new SqlParameter("@SignDate", SqlDbType.DateTime) { Value = SignDate };
            arrPara[3] = new SqlParameter("@Duration", SqlDbType.Int) { Value = Duration };
            arrPara[4] = new SqlParameter("@LaborType", SqlDbType.Int) { Value= LaborType };
            arrPara[5] = new SqlParameter("@ExpiredDate", SqlDbType.DateTime) { Value=ExpiredDate };
            arrPara[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime) { Value=CreatedDate };
            arrPara[7] = new SqlParameter("@CreatedUser", SqlDbType.NVarChar) { Value = CreatedUser };
            arrPara[8] = new SqlParameter("@UpdatedDate", SqlDbType.DateTime) { Value = UpdatedDate};
            arrPara[9] = new SqlParameter("@UpdatedUser", SqlDbType.NVarChar) { Value = UpdatedUser };
            return _ac.TblReadDataSP("sp_LaborContract_Update", arrPara);
        }
        #region  Excute
        public DataTable Excute(string SQL)
        {
            return _ac.TblReadDataSQL(SQL, null);
        }
        #endregion
    }
}
