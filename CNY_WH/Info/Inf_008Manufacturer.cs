using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_WH.Info
{
    internal class Inf_008Manufacturer
    {
        private readonly AccessData acess = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_Manufacturer_select()
        {
            var arrPara = new SqlParameter[0];
            return acess.TblReadDataSP("sp_Manufacturer_select", arrPara);
        }
        public DataTable sp_Manufacturer_update(int PK, string Name, string Code)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = PK };
            arrPara[1] = new SqlParameter("@Name", SqlDbType.VarChar) { Value = Name };
            arrPara[2] = new SqlParameter("@Code", SqlDbType.VarChar) { Value = Code };

            return acess.TblReadDataSP("sp_Manufacturer_update", arrPara);
        }
        public DataTable sp_Manufacturer_insert(string Name, string Code)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@Name", SqlDbType.VarChar) { Value = Name };
            arrPara[1] = new SqlParameter("@Code", SqlDbType.VarChar) { Value = Code };

            return acess.TblReadDataSP("sp_Manufacturer_insert", arrPara);
        }
    }
}
