using CNY_BaseSys;
using CNY_BaseSys.Common;
//using DevExpress.DataAccess.Native.Data;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_WH.Info
{
    internal class Inf_010BusinessTripDetail
    {
        readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_BusinessTripDetail_Select()
        {
            var arrPara = new SqlParameter[0];
            return ac.TblReadDataSP("sp_BusinessTripDetail_Select", arrPara);
        }
        public DataTable sp_BusinessTripDetail_Update(int PK, string Name, string Note)
        {
            var arrPara = new SqlParameter[3];
            arrPara[0] = new SqlParameter("@Pk", SqlDbType.Int) { Value = PK };
            arrPara[1] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = Name };
            arrPara[2] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = Note };
            return ac.TblReadDataSP("sp_BusinessTripDetail_Update", arrPara);
        }
        public DataTable sp_BusinessTripDetail_Insert(string Name, string Note)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = Name };
            arrPara[1] = new SqlParameter("@Note", SqlDbType.NVarChar) { Value = Note };
            return ac.TblReadDataSP("sp_BusinessTripDetail_Insert ", arrPara);

        }
    }
}
