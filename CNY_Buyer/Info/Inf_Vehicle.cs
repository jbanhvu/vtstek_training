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
    class Inf_Vehicle
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_Vehicle_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return _ac.TblReadDataSP("sp_Vehicle_Select", arrPara);
        }
        public DataTable sp_Vehicle_Update(Int64 PK, string Name, string Registration, string Note, string CreatedBy, DateTime CreatedDate, string UpdatedBy ,DateTime UpdatedDate)
        {
            var arrPara = new SqlParameter[8];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = Name };
            arrPara[2] = new SqlParameter("@Registration", SqlDbType.NVarChar) { Value = Registration };
            arrPara[3] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = Note };
            arrPara[4] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar) { Value = CreatedBy };
            arrPara[5] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar) { Value = CreatedDate };
            arrPara[6] = new SqlParameter("@UpdatedBy", SqlDbType.NVarChar) { Value = UpdatedBy };
            arrPara[7] = new SqlParameter("@UpdatedDate", SqlDbType.NVarChar) { Value = UpdatedDate };
            return _ac.TblReadDataSP("sp_Vehicle_Update", arrPara);
        }
    }
}
