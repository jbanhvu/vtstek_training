using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_SI.Class;
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_SI.Info
{
    internal class Inf_001BusinessTrip
    {
        private readonly AccessData acess = new AccessData(DeclareSystem.SysConnectionString);
        #region LoadData
        public DataTable sp_BusinessTrip_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return acess.TblReadDataSP("sp_BusinessTrip_Select", arrPara);
        }
        #endregion
        #region Save
        public DataTable sp_BusinessTrip_Update(Cls_001BusinessTrip cls)
        {
            var arrPara = new SqlParameter[13];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = cls.PK };
            arrPara[1] = new SqlParameter("RequestUser", SqlDbType.NVarChar) { Value = cls.RequestUser };
            arrPara[2] = new SqlParameter("@Content", SqlDbType.NText) { Value = cls.Content };
            arrPara[3] = new SqlParameter("@StartAt", SqlDbType.DateTime) { Value = cls.StartAt };
            arrPara[4] = new SqlParameter("@EndAt", SqlDbType.DateTime) { Value = cls.EndAt };
            arrPara[5] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = cls.Cost };
            arrPara[6] = new SqlParameter("@Status", SqlDbType.NVarChar) { Value = cls.Status };
            arrPara[7] = new SqlParameter("@Conclusion", SqlDbType.NText) { Value= cls.Conclusion };
            arrPara[8] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar) { Value= cls.CreatedBy };
            arrPara[9] = new SqlParameter("@CreatedDate", SqlDbType.DateTime) { Value= cls.CreatedDate };
            arrPara[10] = new SqlParameter("@UpdatedBy", SqlDbType.NVarChar) { Value=cls.UpdatedBy };
            arrPara[11] = new SqlParameter("@UpdatedDate", SqlDbType.DateTime) { Value = cls.UpdatedDate };
            arrPara[12] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = cls.Note };

            return acess.TblReadDataSP("sp_BusinessTrip_Update", arrPara);
        }
        #region  Excute
        public DataTable Excute(string SQL)
        {
            return acess.TblReadDataSQL(SQL, null);
        }
        #endregion
        #endregion
    }
}
