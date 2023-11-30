using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Buyer.Info
{
    class Inf_005MaterialRequirementDetail
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_MaterialRequirementDetail_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@MaterialRequirementPK", SqlDbType.NVarChar) { Value = PK };
            return _ac.TblReadDataSP("sp_MaterialRequirementDetail_Select", arrPara);
        }
        public DataTable sp_MaterialRequirementDetail_Update(Int64 MaterialRequirementPK, DataTable dt)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@MaterialRequirementPK", SqlDbType.Int) { Value = MaterialRequirementPK };
            arrPara[1] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_MaterialRequirementDetail_Update", arrPara);
        }
        public DataTable sp_MaterialRequirementDetail_SelectByListMaterialRequirementPK( DataTable dt)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_MaterialRequirementDetail_SelectByListMaterialRequirementPK", arrPara);
        }
    }
}
