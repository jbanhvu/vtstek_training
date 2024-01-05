using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Buyer.Class;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Buyer.Info
{
    class Inf_001PurchaseRequisition
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable sp_PurchaseRequisition_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = PK };
            return _ac.TblReadDataSP("sp_PurchaseRequisition_Select", arrPara);
        }
        public DataTable sp_FunctionInApproval_Select(string option, string formName)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@Option", SqlDbType.NVarChar) { Value = option};
            arrPara[1] = new SqlParameter("@Formname", SqlDbType.NVarChar) { Value = formName };
            return _ac.TblReadDataSP("sp_FunctionInApproval_Select", arrPara);
        }


        public DataTable sp_PurchaseRequisition_UpdateRequestType(Int64 PK, Int64 FunctionInApprovalPK)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = PK };
            arrPara[1] = new SqlParameter("@FunctionInApprovalPK", SqlDbType.Int) { Value = FunctionInApprovalPK };
            return _ac.TblReadDataSP("sp_PurchaseRequisition_UpdateRequestType", arrPara);
        }
        public DataTable sp_PurchaseRequisition_Update(Cls_001PurchaseRequisition cls)
        {
            var arrPara = new SqlParameter[15];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = cls.PK };
            arrPara[1] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrPara[2] = new SqlParameter("@Requester", SqlDbType.NVarChar) { Value = cls.Requester };
            arrPara[3] = new SqlParameter("@Buyer", SqlDbType.NVarChar) { Value = cls.Buyer };
            arrPara[4] = new SqlParameter("@RequestNumber", SqlDbType.NVarChar) { Value = cls.RequestNumber };
            arrPara[5] = new SqlParameter("@ReceiptNumber", SqlDbType.NVarChar) { Value = cls.ReceiptNumber };
            arrPara[6] = new SqlParameter("@PONumber", SqlDbType.NVarChar) { Value = cls.PONumber };
            arrPara[7] = new SqlParameter("@Supplier", SqlDbType.BigInt) { Value = cls.Supplier };
            arrPara[8] = new SqlParameter("@PaymentMethod", SqlDbType.Int) { Value = cls.PaymentMethod };
            arrPara[9] = new SqlParameter("@DayOfDebt", SqlDbType.Int) { Value = cls.DayOfDebt };
            arrPara[10] = new SqlParameter("@Created_By", SqlDbType.NVarChar) { Value = cls.Created_By };
            arrPara[11] = new SqlParameter("@Created_Date", SqlDbType.DateTime) { Value = cls.Created_Date };
            arrPara[12] = new SqlParameter("@Updated_By", SqlDbType.NVarChar) { Value = cls.Updated_By };
            arrPara[13] = new SqlParameter("@Updated_Date", SqlDbType.DateTime) { Value = cls.Updated_Date };
            arrPara[14] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = cls.Note };
            return _ac.TblReadDataSP("sp_PurchaseRequisition_Update", arrPara);
        }
        #region  Excute
        public DataTable Excute(string SQL)
        {
            return _ac.TblReadDataSQL(SQL, null);
        }
        #endregion
    }
}
