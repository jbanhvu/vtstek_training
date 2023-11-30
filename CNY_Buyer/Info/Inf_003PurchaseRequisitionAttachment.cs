using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Buyer.Info
{
    class Inf_003PurchaseRequisitionAttachment
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_PurchaseRequisitionAttachment_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PurchaseRequisitionPK", SqlDbType.NVarChar) { Value = PK };
            return _ac.TblReadDataSP("sp_PurchaseRequisitionAttachment_Select", arrPara);
        }
        public DataTable sp_PurchaseRequisitionAttachment_Update(Int64 PurchaseRequisitionPK, DataTable dt)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@PurchaseRequisitionPK", SqlDbType.Int) { Value = PurchaseRequisitionPK };
            arrPara[1] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_PurchaseRequisitionAttachment_Update", arrPara);
        }
    }
}
