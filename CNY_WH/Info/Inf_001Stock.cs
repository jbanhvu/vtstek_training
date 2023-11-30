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
    class Inf_001Stock
    {
        readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);

        #region  LoadData
        public DataTable sp_Stock_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };

            return ac.TblReadDataSP("sp_Stock_Select", arrPara);
        }

        #endregion

        #region  Save
        public DataTable sp_Stock_Update(Cls_001Stock cls)
        {
            var arrPara = new SqlParameter[16];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = cls.PK };
            arrPara[1] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = cls.Code };
            arrPara[2] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = cls.Name };
            arrPara[3] = new SqlParameter("@StockType", SqlDbType.Int) { Value = cls.StockType };
            arrPara[4] = new SqlParameter("@StockGroup", SqlDbType.Int) { Value = cls.StockGroup };
            arrPara[5] = new SqlParameter("@StockScope", SqlDbType.Int) { Value = cls.StockScope };
            arrPara[6] = new SqlParameter("@Unit", SqlDbType.Int) { Value = cls.Unit };
            arrPara[7] = new SqlParameter("@MinStock", SqlDbType.Int) { Value = cls.MinStock };
            arrPara[8] = new SqlParameter("@MaxStock", SqlDbType.Int) { Value = cls.MaxStock };
            arrPara[9] = new SqlParameter("@Created_By", SqlDbType.NVarChar) { Value = cls.Created_By };
            arrPara[10] = new SqlParameter("@Created_Date", SqlDbType.DateTime) { Value = cls.Created_Date };
            arrPara[11] = new SqlParameter("@Updated_By", SqlDbType.NVarChar) { Value = cls.Updated_By };
            arrPara[12] = new SqlParameter("@Updated_Date", SqlDbType.DateTime) { Value = cls.Updated_Date };
            arrPara[13] = new SqlParameter("@Origin", SqlDbType.Int) { Value = cls.Origin };
            arrPara[14] = new SqlParameter("@Certificate", SqlDbType.NVarChar) { Value = cls.Certificate };
            arrPara[15] = new SqlParameter("@Manufacturer", SqlDbType.Int) { Value = cls.Manufacturer };

            return ac.TblReadDataSP("sp_Stock_Update", arrPara);
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
