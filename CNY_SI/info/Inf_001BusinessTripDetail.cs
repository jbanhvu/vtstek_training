using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_SI.Class;
using CNY_BaseSys;

namespace CNY_SI.Info
{
    internal class Inf_001BusinessTripDetail
    {

        private readonly AccessData acess = new AccessData(DeclareSystem.SysConnectionString);
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
    }
}
