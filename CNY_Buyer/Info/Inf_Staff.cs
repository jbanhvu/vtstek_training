using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;
using DevExpress.CodeParser;

namespace CNY_Buyer.Info
{
     class Inf_Staff
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable sp_Staff_Select(Int64 PK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            return _ac.TblReadDataSP("sp_Staff_Select", arrPara);
        }
        public DataTable sp_Staff_Update(Int64 PK, DateTime DOB, string FullName, string Code, int EncrolNumber, 
                                            DateTime HireDate, int Sex, DateTime IdCard_CreatedDate, string IdCard_CreatedWhere,
                                            string IdCard_Number, string departmentCode, string positionCode, string educationLevel,
                                            string phoneNumber, string email, string seniority, string address)
        {
            var arrPara = new SqlParameter[17];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = PK };
            arrPara[1] = new SqlParameter("@DOB", SqlDbType.DateTime) { Value = DOB };
            arrPara[2] = new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = FullName };
            arrPara[3] = new SqlParameter("@Code", SqlDbType.NVarChar, 20) { Value = Code };
            arrPara[4] = new SqlParameter("@EncrolNumber", SqlDbType.Int) { Value = EncrolNumber };
            arrPara[5] = new SqlParameter("@HireDate", SqlDbType.DateTime) {Value = HireDate };
            arrPara[6] = new SqlParameter("@Sex", SqlDbType.Int) { Value = Sex};
            arrPara[7] = new SqlParameter("@IdentityCard_CreatedDate", SqlDbType.DateTime) { Value = IdCard_CreatedDate };
            arrPara[8] = new SqlParameter("@IdentityCard_CreatedWhere", SqlDbType.NVarChar) { Value = IdCard_CreatedWhere };
            arrPara[9] = new SqlParameter("@IdentityCard_Number", SqlDbType.NVarChar) { Value = IdCard_Number };
            arrPara[10] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = departmentCode };
            arrPara[11] = new SqlParameter("@PositionCode", SqlDbType.NVarChar) { Value = positionCode };
            arrPara[12] = new SqlParameter("@EducationLevel", SqlDbType.NVarChar) { Value = educationLevel };
            arrPara[13] = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value =phoneNumber };
            arrPara[14] = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email };
            arrPara[15] = new SqlParameter("@Seniority", SqlDbType.NVarChar) { Value = seniority };
            arrPara[16] = new SqlParameter("@Address", SqlDbType.NVarChar) { Value= address };

            return _ac.TblReadDataSP("sp_Staff_Update", arrPara);
        }
        #region  Excute
        public DataTable Excute(string SQL)
        {
            return _ac.TblReadDataSQL(SQL, null);
        }
        #endregion
    }
}
