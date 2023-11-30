using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Buyer.Info
{
    class Inf_004MaterialRequirement
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable sp_MaterialRequirement_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = PK };
            return _ac.TblReadDataSP("sp_MaterialRequirement_Select", arrPara);
        }
        public DataTable sp_MaterialRequirement_SelectForPRGenerate()
        {
            var arrPara = new SqlParameter[0];
            return _ac.TblReadDataSP("sp_MaterialRequirement_SelectForPRGenerate", arrPara);
        }
        public DataTable sp_MaterialRequirement_Update(Cls_004MaterialRequirement cls)
        {
            var arrPara = new SqlParameter[12];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = cls.PK };
            arrPara[1] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrPara[2] = new SqlParameter("@ProjectName", SqlDbType.NVarChar) { Value = cls.ProjectName };
            arrPara[3] = new SqlParameter("@Requester", SqlDbType.NVarChar) { Value = cls.Requester };
            arrPara[4] = new SqlParameter("@CriticalLevel", SqlDbType.Int) { Value = cls.CriticalLevel };
            arrPara[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar) { Value = cls.CreatedBy };
            arrPara[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime) { Value = cls.CreatedDate };
            arrPara[7] = new SqlParameter("@UpdatedBy", SqlDbType.NVarChar) { Value = cls.UpdatedBy };
            arrPara[8] = new SqlParameter("@UpdatedDate", SqlDbType.DateTime) { Value = cls.UpdatedDate };
            arrPara[9] = new SqlParameter("@Status", SqlDbType.Int) { Value = cls.Status };
            arrPara[10] = new SqlParameter("@RequestTimes", SqlDbType.Int) { Value = cls.RequestTimes };
            arrPara[11] = new SqlParameter("@RequestType", SqlDbType.Int) { Value = cls.RequestType };
            return _ac.TblReadDataSP("sp_MaterialRequirement_Update", arrPara);
        }
        public DataTable sp_MaterialRequirement_UpdateStatus(Int64 PK, Int32 Status)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@Status", SqlDbType.NVarChar) { Value = Status };
            return _ac.TblReadDataSP("sp_MaterialRequirement_UpdateStatus", arrPara);
        }
        #region  Excute
        public DataTable Excute(string SQL)
        {
            return _ac.TblReadDataSQL(SQL, null);
        }
        #endregion
    }
}
