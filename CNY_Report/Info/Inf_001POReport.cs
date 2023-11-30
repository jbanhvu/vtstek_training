using CNY_BaseSys;
using CNY_BaseSys.Common;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Report.Info
{
    class Inf_001POReport
    {
        private readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable LoadPODeatis(long POPK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = POPK };
            return _ac.TblReadDataSP("sp_PO_PrintDeatils", arrPara);
        }

        public DataTable LoadPOHeader(long POPK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@C60PK", SqlDbType.BigInt) { Value = POPK };
            return _ac.TblReadDataSP("sp_PO_PrintPOHeader", arrPara);
        }


        public DataTable LoadLSXNumber(long POPK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = POPK };
            return _ac.TblReadDataSP("sp_PO_LoadLSXNumber", arrPara);
        }
    }
}
