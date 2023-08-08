using CNY_BaseSys;
using CNY_BaseSys.Common;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;

namespace CNY_WH.Info
{
    internal class Inf_009Survey
    {
        readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_Survey_Select(int PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return ac.TblReadDataSP("sp_Survey_Select", arrPara);
        }
        public DataTable sp_Survey_Update(int PK, int Project_PK, string Content, DateTime Start_At, DateTime End_At, string Conclusion, string Note, string Created_By, DateTime Created_date, string UpdateBy, DateTime UpdateDate)
        {
            var arrPara = new SqlParameter[11];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = PK };
            arrPara[1] = new SqlParameter("@Project_Pk", SqlDbType.Int) { Value= Project_PK };
            arrPara[2] = new SqlParameter("@Content",SqlDbType.NText) { Value = Content };
            arrPara[3] = new SqlParameter("@Start_At", SqlDbType.DateTime) { Value = Start_At };
            arrPara[4] = new SqlParameter("@End_At",SqlDbType.DateTime) { Value = End_At };
            arrPara[5] = new SqlParameter("@Conclusion", SqlDbType.NText) { Value = Conclusion };
            arrPara[6] = new SqlParameter("@Note", SqlDbType.NText) { Value= Note };
            arrPara[7] = new SqlParameter("@Created_By",SqlDbType.NVarChar) { Value = Created_By };
            arrPara[8] = new SqlParameter("@Created_Date", SqlDbType.DateTime) { Value= Created_date };
            arrPara[9] = new SqlParameter("@Updated_By", SqlDbType.NVarChar) { Value=UpdateBy };
            arrPara[10] = new SqlParameter("@Updated_Date", SqlDbType.DateTime) { Value =  UpdateDate };
            return ac.TblReadDataSP("sp_Survey_Update", arrPara);
        }
        public DataTable sp_Survey_InSert(int Project_PK, string Content, DateTime Start_At, DateTime End_At, string Status, string cost, string Conclusion, string Created_By, DateTime Created_date, string UpdateBy, DateTime UpdateDate, string Note)
        {
            var arrPara = new SqlParameter[12];
            
            arrPara[0] = new SqlParameter("@Project_Pk", SqlDbType.Int) { Value = Project_PK };
            arrPara[1] = new SqlParameter("@Content", SqlDbType.NText) { Value = Content };
            arrPara[2] = new SqlParameter("@Start_At", SqlDbType.DateTime) { Value = Start_At };
            arrPara[3] = new SqlParameter("@End_At", SqlDbType.DateTime) { Value = End_At };
            arrPara[4] = new SqlParameter("@Conclusion", SqlDbType.NText) { Value = Conclusion };
            arrPara[5] = new SqlParameter("@Note", SqlDbType.NText) { Value = Note };
            arrPara[6] = new SqlParameter("@Created_By", SqlDbType.NVarChar) { Value = Created_By };
            arrPara[7] = new SqlParameter("@Created_Date", SqlDbType.DateTime) { Value = Created_date };
            arrPara[8] = new SqlParameter("@Updated_By", SqlDbType.NVarChar) { Value = UpdateBy };
            arrPara[9] = new SqlParameter("@Updated_Date", SqlDbType.DateTime) { Value = UpdateDate };
            arrPara[10] = new SqlParameter("@Cost",SqlDbType.Decimal) { Value = cost};
            arrPara[11] = new SqlParameter("@Status",SqlDbType.NVarChar) { Value = Status };
            return ac.TblReadDataSP("sp_Survey_InSert", arrPara);
        }
        public DataTable Excute(string SQL)
        {
            return ac.TblReadDataSQL(SQL, null);
        }
    }
}
