using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Buyer.Info
{
    class Inf_006MaterialReceipt
    {

        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable sp_MaterialReceipt_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = PK };
            return _ac.TblReadDataSP("sp_MaterialReceipt_Select", arrPara);
        }
        public DataTable sp_MaterialReceipt_Update(Cls_006MaterialReceipt cls)
        {
            var arrPara = new SqlParameter[11];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = cls.PK };
            arrPara[1] = new SqlParameter("@MaterialRequirementPK", SqlDbType.BigInt) { Value = cls.MaterialRequirementPK };
            arrPara[2] = new SqlParameter("@Receiver", SqlDbType.NVarChar) { Value = cls.Receiver };
            arrPara[3] = new SqlParameter("@Provider", SqlDbType.NVarChar) { Value = cls.Provider };
            arrPara[4] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar) { Value = cls.CreatedBy };
            arrPara[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime) { Value = cls.CreatedDate };
            arrPara[6] = new SqlParameter("@UpdatedBy", SqlDbType.NVarChar) { Value = cls.UpdatedBy };
            arrPara[7] = new SqlParameter("@UpdatedDate", SqlDbType.DateTime) { Value = cls.UpdatedDate };
            arrPara[8] = new SqlParameter("@ReceivedDate", SqlDbType.DateTime) { Value = cls.ReceivedDate };
            arrPara[9] = new SqlParameter("@Status", SqlDbType.Int) { Value = cls.Status };
            arrPara[10] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = cls.Note };

            return _ac.TblReadDataSP("sp_MaterialReceipt_Update", arrPara);
        }
    }
}
