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
     class Inf_Seal
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_Seal_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return _ac.TblReadDataSP("sp_Seal_Select", arrPara);
        }
        public DataTable sp_Seal_Update(Int64 PK, DateTime SealDate, Int64 StaffPK ,string Content ,string CreatedBy, DateTime CreatedDate, string UpdatedBy, DateTime UpdatedDate)
        {
            var arrPara = new SqlParameter[8];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@Content", SqlDbType.NVarChar) { Value = Content };
            arrPara[2] = new SqlParameter("@StaffPK", SqlDbType.BigInt) { Value = StaffPK };
            arrPara[3] = new SqlParameter("@SealDate", SqlDbType.DateTime) { Value = SealDate };
            arrPara[4] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar) { Value = CreatedBy };
            arrPara[5] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar) { Value = CreatedDate };
            arrPara[6] = new SqlParameter("@UpdatedBy", SqlDbType.NVarChar) { Value = UpdatedBy };
            arrPara[7] = new SqlParameter("@UpdatedDate", SqlDbType.NVarChar) { Value = UpdatedDate };
            return _ac.TblReadDataSP("sp_Seal_Update", arrPara);
        }
    }
}
