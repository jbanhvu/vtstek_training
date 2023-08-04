using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;

namespace CNY_WH.Info
{
    internal class Inf_003StockType
    {
        readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_StockType_Select()
        {
            var arrPara = new SqlParameter[0];
            return ac.TblReadDataSP("sp_StockType_Select", arrPara);
        }

        public DataTable sp_StockType_Update(int PK, string Name)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = PK };
            arrPara[1] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = Name };
            return ac.TblReadDataSP("sp_StockType_Update", arrPara);
        }

        public DataTable sp_StockType_Insert(string Name)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = Name };
            return ac.TblReadDataSP("sp_StockType_Insert", arrPara);
        }
    }
}
