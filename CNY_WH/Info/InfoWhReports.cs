using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CNY_BaseSys;
using CNY_BaseSys.Common;

namespace CNY_WH.Info
{
    class InfoWhReports
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable LoadRepStockReceive164(long lPk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            return _ac.TblReadDataSP("usp_CNY_00164", arrpara);
        }

        public DataSet LoadStockCardInfo159(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            return _ac.DtsReadDataSP("usp_CNY_00159", arrpara);
        }

        public DataTable LoadStockCardOb134(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            return _ac.TblReadDataSP("usp_CNY_0134", arrpara);
        }

        public DataTable LoadRepStockReceive135(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            // delete
            //var arrpara = new SqlParameter[4];
            //arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            //arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            //arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            //arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            //return _ac.TblReadDataSP("usp_CNY_0135", arrpara);
            return null;
        }

        public DataTable GetDataforRepStockCard160B(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            return _ac.TblReadDataSP("usp_CNY_00160B", arrpara);
        }

        public DataTable LoadRepStockCard160(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            return _ac.TblReadDataSP("usp_CNY_00160", arrpara);
        }

        #region Stock Card Apparel

        public DataSet LoadStockCardInfo_App_159(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            return _ac.DtsReadDataSP("usp_CNY_Apparel_00159", arrpara);
        }

        public DataTable LoadRepStockCard_App_160(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            return _ac.TblReadDataSP("usp_CNY_Apparel_00160", arrpara);
        }

        public DataTable LoadStockItemsBalance_App_00001(string sCond)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Cond", SqlDbType.NVarChar) { Value = sCond };

            return _ac.TblReadDataSP("usp_CNY_Apparel_00001", arrpara);
        }

        public DataTable RptInventoryByBatch_App_159I(string sCode, string sWh, DateTime dFr, DateTime dTo)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[2] = new SqlParameter("@Fr", SqlDbType.DateTime) { Value = dFr };
            arrpara[3] = new SqlParameter("@To", SqlDbType.DateTime) { Value = dTo };

            return _ac.TblReadDataSP("usp_CNY_Apparel_00159I", arrpara);
        }


        #endregion
    }
}
