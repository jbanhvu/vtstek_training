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
    class Inf_003ReceiptNoteDetail
    {
        readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);

        #region  LoadData
        public DataTable sp_ReceiptNoteDetail_Select(Int64 ReceiptNotePK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@ReceiptNotePK", SqlDbType.BigInt) { Value = ReceiptNotePK };

            return ac.TblReadDataSP("sp_ReceiptNoteDetail_Select", arrPara);
        }

        #endregion

        #region  Save
        public DataTable sp_ReceiptNoteDetail_Update(Int64 ReceiptNotePK, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@ReceiptNotePK", SqlDbType.Int) { Value = ReceiptNotePK };
            arrpara[1] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return ac.TblReadDataSP("sp_ReceiptNoteDetail_Update", arrpara);
        }

        #endregion

    }
}
