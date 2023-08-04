using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CNY_BaseSys;
using CNY_WH.Class;
using CNY_BaseSys.Common;

namespace CNY_WH.Info
{
    class Inf_002ReceiptNote
    {
        readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
        #region  LoadData
        public DataTable sp_ReceiptNote_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };

            return ac.TblReadDataSP("sp_ReceiptNote_Select", arrPara);
        }

        #endregion

        #region  Save
        public DataTable sp_ReceiptNote_Update(Cls_002ReceiptNote cls)
        {
            var arrpara = new SqlParameter[17];
            arrpara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = cls.PK };
            arrpara[1] = new SqlParameter("@ReceiptDate", SqlDbType.DateTime) { Value = cls.ReceiptDate };
            arrpara[2] = new SqlParameter("@Supplier", SqlDbType.NVarChar) { Value = cls.Supplier };
            arrpara[3] = new SqlParameter("@Address", SqlDbType.NVarChar) { Value = cls.Address };
            arrpara[4] = new SqlParameter("@Deliver", SqlDbType.NVarChar) { Value = cls.Deliver };
            arrpara[5] = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = cls.PhoneNumber };
            arrpara[6] = new SqlParameter("@PONumer", SqlDbType.NVarChar) { Value = cls.PONumer };
            arrpara[7] = new SqlParameter("@PODate", SqlDbType.DateTime) { Value = cls.PODate };
            arrpara[8] = new SqlParameter("@NoPO", SqlDbType.Bit) { Value = cls.NoPO };
            arrpara[9] = new SqlParameter("@Status", SqlDbType.Int) { Value = cls.Status };
            arrpara[10] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = cls.Note };
            arrpara[11] = new SqlParameter("@ApprovedUser", SqlDbType.NVarChar) { Value = cls.ApprovedUser };
            arrpara[12] = new SqlParameter("@ApproveDate", SqlDbType.DateTime) { Value = cls.ApproveDate };
            arrpara[13] = new SqlParameter("@Created_By", SqlDbType.NVarChar) { Value = cls.Created_By };
            arrpara[14] = new SqlParameter("@Created_Date", SqlDbType.DateTime) { Value = cls.Created_Date };
            arrpara[15] = new SqlParameter("@Updated_By", SqlDbType.NVarChar) { Value = cls.Updated_By };
            arrpara[16] = new SqlParameter("@Updated_Date", SqlDbType.DateTime) { Value = cls.Updated_Date };


            return ac.TblReadDataSP("sp_ReceiptNote_Update", arrpara);
        }

        #endregion

        #region  Excute
        public DataTable Excute(string SQL)
        {
            return ac.TblReadDataSQL(SQL, null);
        }
        #endregion
    }
}
