using CNY_BaseSys;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using System;
using System.Data;
using System.Data.SqlClient;


namespace CNY_BaseSys.Info
{
    public class Inf_ApprovalHistory
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_ApprovalHistory_Update(Cls_ApprovalHistory cls)
        {
            var arrPara = new SqlParameter[7];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.BigInt) { Value = cls.FunctionInApprovalPK };
            arrPara[1] = new SqlParameter("@Level", SqlDbType.Int) { Value = cls.Level };
            arrPara[2] = new SqlParameter("@ItemPKInFunction", SqlDbType.BigInt) { Value = cls.ItemPKInFunction };
            arrPara[3] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = cls.UserName };
            arrPara[4] = new SqlParameter("@Status", SqlDbType.Int) { Value = cls.Status };
            arrPara[5] = new SqlParameter("@ApprovedDate", SqlDbType.DateTime) { Value = cls.ApprovedDate };
            arrPara[6] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = cls.Note };

            return _ac.TblReadDataSP("sp_ApprovalHistory_Update", arrPara);
        }
        public DataTable sp_ApprovalHistory_Select(Int32 FunctionInApprovalPK, Int64 ItemPKInFunction)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };
            arrPara[1] = new SqlParameter("@ItemPKInFunction", SqlDbType.BigInt) { Value = ItemPKInFunction };

            return _ac.TblReadDataSP("sp_ApprovalHistory_Select", arrPara);
        }
        public DataTable sp_ApprovalHistory_SelectUserSignature(Int32 FunctionInApprovalPK, Int64 ItemPKInFunction)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };
            arrPara[1] = new SqlParameter("@ItemPKInFunction", SqlDbType.BigInt) { Value = ItemPKInFunction };

            return _ac.TblReadDataSP("sp_ApprovalHistory_SelectUserSignature", arrPara);
        }
        public DataTable sp_LevelFunctionApproval_GetUserApproval(Int32 FunctionInApprovalPK, string MenuCode, Int32 CurrentLevel)
        {
            var arrPara = new SqlParameter[3];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };
            arrPara[1] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = MenuCode };
            arrPara[2] = new SqlParameter("@CurrentLevel", SqlDbType.Int) { Value = CurrentLevel };

            return _ac.TblReadDataSP("sp_LevelFunctionApproval_GetUserApproval", arrPara);
        }

        public DataTable sp_LevelFunctionApproval_SelectByFunctionInApprovalPK(Int32 FunctionInApprovalPK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };

            return _ac.TblReadDataSP("sp_LevelFunctionApproval_SelectByFunctionInApprovalPK", arrPara);
        }
        public DataTable sp_ApprovalHistory_GetUserApproved(Int32 FunctionInApprovalPK, Int64 ItemPKInFunction)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };
            arrPara[1] = new SqlParameter("@ItemPKInFunction", SqlDbType.BigInt) { Value = ItemPKInFunction };

            return _ac.TblReadDataSP("sp_ApprovalHistory_GetUserApproved", arrPara);
        }
        public DataTable sp_ApprovalHistory_Delete(Int32 FunctionInApprovalPK, Int64 ItemPKInFunction)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };
            arrPara[1] = new SqlParameter("@ItemPKInFunction", SqlDbType.BigInt) { Value = ItemPKInFunction };

            return _ac.TblReadDataSP("sp_ApprovalHistory_Delete", arrPara);
        }
    }
}
