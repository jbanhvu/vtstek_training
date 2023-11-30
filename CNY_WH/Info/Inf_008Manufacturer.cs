using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CNY_BaseSys.Common;
using CNY_BaseSys;
using System.Data.SqlClient;

namespace CNY_WH.Info
{
    internal class Inf_008Manufacturer
    {
        readonly AccessData acessData = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_Manufacturer_select()
        {
            var arrPara = new SqlParameter[0];
            return acessData.TblReadDataSP("sp_Manufacturer_select", arrPara);
        }
        public DataTable sp_Manufacturer_Update(int PK, string Name, string Code)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = PK };
            arrPara[1] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = Name};
            arrPara[2] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = Code};
            return acessData.TblReadDataSP("sp_Manufacturer_Update", arrPara);
        }

        public DataTable sp_Manufacturer_Insert( string Name, string Code)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = Name };
            arrPara[1] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = Code };
            return acessData.TblReadDataSP("sp_Manufacturer_Insert", arrPara);
        }
    }
}
