using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Buyer.Info
{
    class Inf_002PurchaseRequisitionDetail
    {

        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_PurchaseRequisitionDetail_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PurchaseRequisitionPK", SqlDbType.NVarChar) { Value = PK };
            return _ac.TblReadDataSP("sp_PurchaseRequisitionDetail_Select", arrPara);
        }
        public DataTable sp_PurchaseRequisitionDetail_Update(Int64 PurchaseRequisitionPK, DataTable dt)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@PurchaseRequisitionPK", SqlDbType.Int) { Value = PurchaseRequisitionPK };
            arrPara[1] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PurchaseRequisitionDetail_Update", arrPara);
        }
        public DataTable sp_PurchaseRequisitionDetail_SelectReport()
        {
            var arrPara = new SqlParameter[0];
            return _ac.TblReadDataSP("sp_PurchaseRequisitionDetail_SelectReport", arrPara);
        }

    }
}
