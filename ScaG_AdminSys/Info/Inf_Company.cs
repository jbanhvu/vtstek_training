using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;
using CNY_BaseSys.Common;

namespace CNY_AdminSys.Info
{
    public class Inf_Company
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable ListCompany_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_Company_Load", null);
        }

        public DataTable ListCurrency_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_Admin_LoadCurrency", null);
        }

        public int ListCompany_Insert(string compCode, string compName, string address, string phoneNumber, string fax, string personRepresent, string position, string bankName,
            string bankAddress, string bankAccount, string swiftCode, string taxRegNo, string baseCur, string domCur, string email)
        {
            var arrpara = new SqlParameter[15];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = compCode };
            arrpara[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar) { Value = compName };
            arrpara[2] = new SqlParameter("@Adrress", SqlDbType.NVarChar) { Value = address };
            arrpara[3] = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = phoneNumber };
            arrpara[4] = new SqlParameter("@Fax", SqlDbType.NVarChar) { Value = fax };
            arrpara[5] = new SqlParameter("@PersonRepresent", SqlDbType.NVarChar) { Value = personRepresent };
            arrpara[6] = new SqlParameter("@Positions", SqlDbType.NVarChar) { Value = position };
            arrpara[7] = new SqlParameter("@CNY001_BankName", SqlDbType.NVarChar) { Value = bankName };
            arrpara[8] = new SqlParameter("@CNY002_BankAddress", SqlDbType.NVarChar) { Value = bankAddress };
            arrpara[9] = new SqlParameter("@CNY003_BankAccount", SqlDbType.NVarChar) { Value = bankAccount };
            arrpara[10] = new SqlParameter("@CNY004_SwiftCode", SqlDbType.NVarChar) { Value = swiftCode };
            arrpara[11] = new SqlParameter("@CNY005_TaxRegNo", SqlDbType.NVarChar) { Value = taxRegNo };
            arrpara[12] = new SqlParameter("@CNY006_BaseCurrency", SqlDbType.NVarChar) { Value = baseCur };
            arrpara[13] = new SqlParameter("@CNY007_DomCurrency", SqlDbType.NVarChar) { Value = domCur };
            arrpara[14] = new SqlParameter("@CNY008_Email", SqlDbType.NVarChar) { Value = email };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Admin_ListCompany_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }


        public int ListCompany_Update(string compCode, string compName, string address, string phoneNumber, string fax, string personRepresent, string position, string bankName,
            string bankAddress, string bankAccount, string swiftCode, string taxRegNo, string baseCur, string domCur, string email)
        {
            var arrpara = new SqlParameter[15];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = compCode };
            arrpara[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar) { Value = compName };
            arrpara[2] = new SqlParameter("@Adrress", SqlDbType.NVarChar) { Value = address };
            arrpara[3] = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = phoneNumber };
            arrpara[4] = new SqlParameter("@Fax", SqlDbType.NVarChar) { Value = fax };
            arrpara[5] = new SqlParameter("@PersonRepresent", SqlDbType.NVarChar) { Value = personRepresent };
            arrpara[6] = new SqlParameter("@Positions", SqlDbType.NVarChar) { Value = position };
            arrpara[7] = new SqlParameter("@CNY001_BankName", SqlDbType.NVarChar) { Value = bankName };
            arrpara[8] = new SqlParameter("@CNY002_BankAddress", SqlDbType.NVarChar) { Value = bankAddress };
            arrpara[9] = new SqlParameter("@CNY003_BankAccount", SqlDbType.NVarChar) { Value = bankAccount };
            arrpara[10] = new SqlParameter("@CNY004_SwiftCode", SqlDbType.NVarChar) { Value = swiftCode };
            arrpara[11] = new SqlParameter("@CNY005_TaxRegNo", SqlDbType.NVarChar) { Value = taxRegNo };
            arrpara[12] = new SqlParameter("@CNY006_BaseCurrency", SqlDbType.NVarChar) { Value = baseCur };
            arrpara[13] = new SqlParameter("@CNY007_DomCurrency", SqlDbType.NVarChar) { Value = domCur };
            arrpara[14] = new SqlParameter("@CNY008_Email", SqlDbType.NVarChar) { Value = email };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Admin_ListCompany_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListCompany_Delete(string companyCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = companyCode };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Admin_ListCompany_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
    }
}
