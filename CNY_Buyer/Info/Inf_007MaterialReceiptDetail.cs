using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Buyer.Info
{
    class Inf_007MaterialReceiptDetail
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_MaterialReceiptDetail_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@MaterialReceiptPK", SqlDbType.NVarChar) { Value = PK };
            return _ac.TblReadDataSP("sp_MaterialReceiptDetail_Select", arrPara);
        }        
        public DataTable sp_MaterialReceiptDetail_SelectByMaterialRequirementPK(Int64 MaterialRequirementPK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@MaterialRequirementPK", SqlDbType.NVarChar) { Value = MaterialRequirementPK };
            return _ac.TblReadDataSP("sp_MaterialReceiptDetail_SelectByMaterialRequirementPK", arrPara);
        }
        public DataTable sp_MaterialReceiptDetail_Update(Int64 MaterialReceiptPK, DataTable dt)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@MaterialReceiptPK", SqlDbType.Int) { Value = MaterialReceiptPK };
            arrPara[1] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_MaterialReceiptDetail_Update", arrPara);
        }
    }
}
