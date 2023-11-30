using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;
using CNY_BaseSys.Common;

namespace CNY_WH.Info
{
    class InfoWh00002
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        #region PO Allocation

        public DataTable LoadPoHeaderInfo_00125A(long lPoid)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Poid", SqlDbType.BigInt) { Value = lPoid };
            return _ac.TblReadDataSP("usp_CNY_00125A", arrpara);
        }
        
        public DataTable LoadPoAllocationDetails_00122A(long lKey)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lKey };
            return _ac.TblReadDataSP("usp_CNY_00122A", arrpara);
        }

        public DataTable LoadPoServicesDetails_M00122S(long lKey)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lKey };
            return _ac.TblReadDataSP("usp_CNY_00122S", arrpara);
        }

        public DataTable LoadSoinfoinStkAll_00161(long lPk, string sPrno)
        {
            var arrpara= new SqlParameter[2];

            arrpara[0]=new SqlParameter("@Pk", SqlDbType.BigInt) {Value = lPk};
            arrpara[1] = new SqlParameter("@Prno", SqlDbType.NVarChar) {Value = sPrno};

            return _ac.TblReadDataSP("usp_CNY_00161", arrpara);
        }

        public DataTable LoadStkRecinStkAll_00161W(long lPk)
        {
            var arrpara = new SqlParameter[1];

            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            
            return _ac.TblReadDataSP("usp_CNY_00161W", arrpara);
        }

        public DataTable ListPoinAllocation_00120A(string sPo)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pono", SqlDbType.NVarChar) { Value = sPo };
            return _ac.TblReadDataSP("usp_CNY_00120A", arrpara);
        }

        public DataTable ListPoinAllocation_00120S(long lSuppCust, string sPo, string sPoid, int iPono)
        {
            var arrpara = new SqlParameter[4];

            arrpara[0] = new SqlParameter("@Supk", SqlDbType.BigInt) { Value = lSuppCust };
            arrpara[1] = new SqlParameter("@Pono", SqlDbType.NVarChar) { Value = sPo };
            arrpara[2] = new SqlParameter("@Poid", SqlDbType.NVarChar) { Value = sPoid };
            arrpara[3] = new SqlParameter("@Bpono", SqlDbType.Int) {Value = iPono};

            return _ac.TblReadDataSP("usp_CNY_00120S", arrpara);
        }

        public DataTable ListSo_App_00120C(long lSuppCust, string sSo, string sSoid, int iSono)
        {
            var arrpara = new SqlParameter[4];

            arrpara[0] = new SqlParameter("@Custpk", SqlDbType.BigInt) { Value = lSuppCust };
            arrpara[1] = new SqlParameter("@Sono", SqlDbType.NVarChar) { Value = sSo };
            arrpara[2] = new SqlParameter("@Soid", SqlDbType.NVarChar) { Value = sSoid };
            arrpara[3] = new SqlParameter("@BSono", SqlDbType.Int) { Value = iSono };

            return _ac.TblReadDataSP("usp_CNY_Apparel_00120C", arrpara);
        }

        #endregion

    }
}
