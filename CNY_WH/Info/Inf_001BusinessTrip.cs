using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_WH.Info
{
    internal class Inf_001BusinessTrip
    {
        private readonly AccessData acess = new AccessData(DeclareSystem.SysConnectionString);
        #region LoadData
        public DataTable sp_BusinessTrip_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return acess.TblReadDataSP("sp_BusinessTrip_Select", arrPara);
        }
        #endregion
        public DataTable sp_BusinessTripDetail_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@BusinessTripPK", SqlDbType.BigInt) { Value = PK };
            return acess.TblReadDataSP("sp_BusinessTripDetail_Select", arrPara);
        }
        public DataTable sp_BusinessTripDetail_SelectReport(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@BusinessTripPK", SqlDbType.BigInt) { Value = PK };
            return acess.TblReadDataSP("sp_BusinessTripDetail_SelectReport", arrPara);
        }
        public DataTable sp_BusinessTripDetail_Update(Int64 PK, DataTable dt)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@BusinessTripPK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return acess.TblReadDataSP("sp_BusinessTripDetail_Update", arrPara);
        }
        public DataTable sp_ApprovalHistory_SelectUserSignature(Int32 FunctionInApprovalPK, Int64 ItemPKInFunction)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };
            arrPara[1] = new SqlParameter("@ItemPKInFunction", SqlDbType.BigInt) { Value = ItemPKInFunction };

            return acess.TblReadDataSP("sp_ApprovalHistory_SelectUserSignature", arrPara);
        }
        #region  Excute
        public DataTable Excute(string SQL)
        {
            return acess.TblReadDataSQL(SQL, null);
        }
        #endregion Excute
    }
}
