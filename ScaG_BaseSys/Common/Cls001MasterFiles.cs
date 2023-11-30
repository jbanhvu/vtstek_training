using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_BaseSys.Common
{
    public class Cls001MasterFiles
    {
        public static DataTable ExcecuteSql(string sString)
        {
            var accData = new AccessData(DeclareSystem.SysConnectionString);
            return accData.TblReadDataSQL(sString, null);
        }

        public static bool BlExcecuteSql(string sString)
        {
            var accData = new AccessData(DeclareSystem.SysConnectionString);
            return accData.BolExcuteSQL(sString, null);
        }
        public static bool IsRecordExist(string sText)
        {
            var accData = new AccessData(DeclareSystem.SysConnectionString);
            DataTable dt = accData.TblReadDataSQL(sText, null);
            return (dt != null && dt.Rows.Count > 0);
        }

        public static DateTime GetServerDate()
        {
            var accData = new AccessData(DeclareSystem.SysConnectionString);
            return ProcessGeneral.GetSafeDatetimeNullable(accData.TblReadDataSQL("select GETDATE() as CurrentDate", null).Rows[0]["CurrentDate"]);
        }

        public static string GetDescription(string sText, string sFi)
        {
            AccessData AccData = new AccessData(DeclareSystem.SysConnectionString);
            DataTable dt = AccData.TblReadDataSQL(sText, null);
            return (dt != null && dt.Rows.Count > 0) ? ProcessGeneral.GetSafeString(dt.Rows[0][sFi]) : "N/A";
        }
        public static long GetKeyCodeRule(string sTable)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sText = $"SELECT PKHeader FROM dbo.DefinitionCodeHeader WHERE TableCode='{sTable}'";
            DataTable dt = acData.TblReadDataSQL(sText, null);
            return (dt != null && dt.Rows.Count > 0) ? ProcessGeneral.GetSafeInt64(dt.Rows[0]["PKHeader"]) : 0;
        }
        public static long GetKeyCodeRule(string sTable, string sItem)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sText = $"SELECT PKHeader FROM dbo.DefinitionCodeHeader WHERE TableCode='{sTable}' and ItemCode='{sItem}'";
            DataTable dt = acData.TblReadDataSQL(sText, null);
            return (dt != null && dt.Rows.Count > 0) ? ProcessGeneral.GetSafeInt64(dt.Rows[0]["PKHeader"]) : 0;
        }
        public static string GetFormName(string sForm)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sFame = string.Format("Select Ltrim(Rtrim(FormName)) As sFi From ListMenu " +
                                         "Where ProjectCode <> '' And Upper(Ltrim(Rtrim(FormCode))) = '{0}'", sForm.ToUpper());
            var dt = acData.TblReadDataSQL(sFame, null);
            return (dt != null && dt.Rows.Count > 0) ? ProcessGeneral.GetSafeString(dt.Rows[0]["sFi"]) : "-";
        }
        
        public static string GetFormNameFromMenuCode(string sMenuCode)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sFame = "Select Ltrim(Rtrim(FormName)) As sFi From ListMenu " +
                           $"Where ProjectCode <> '' And Upper(Ltrim(Rtrim(MenuCode))) = '{sMenuCode.ToUpper()}'";
            var dt = acData.TblReadDataSQL(sFame, null);
            return (dt != null && dt.Rows.Count > 0) ? ProcessGeneral.GetSafeString(dt.Rows[0]["sFi"]) : "-";
        }

        public static DataTable GetCurrency()
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sCurr = "SELECT CNY001 [Code], CNY002 [Description 1], CNY003 [Description 2], PK FROM CNY00006";
            return acData.TblReadDataSQL(sCurr, null);
        }

        public static DataTable GetSupplier()
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sSupp = "SELECT CNY001 [Supplier Code], LTRIM(RTRIM(CNY002)) [Supplier Name], LTRIM(RTRIM(CNY003)) [Short Name], CNY010[Reference], PK FROM CNY00002 ";
            return acData.TblReadDataSQL(sSupp, null);
        }

        /// <summary>
        /// Description: Customer Code, Customer Name, Reference, PK
        /// </summary>
        public static DataTable GetCustomer()
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sSupp = "SELECT CNY001[Customer Code], LTRIM(RTRIM(CNY002))[Customer Name], CNY009 [Reference], PK FROM CNY00011 ";
            return acData.TblReadDataSQL(sSupp, null);
        }

        /// <summary>
        /// Description: Customer, Name, Code
        /// </summary>
        public static DataTable GetCustomerFile(string sCustomer)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sText = $"SELECT CNY003 [Customer], CNY002 [Name], CNY001 [Code] FROM CNY00011 WHERE CNY003 LIKE '%{sCustomer}%'";
            return acData.TblReadDataSQL(sText, null);
        }
        public static DataTable GetMsGroup()
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sSupp = "SELECT CNY001 [Code], CNY002 [Description 1], CNY003 [Description 2], PK FROM CNYMF002 ";
            return acData.TblReadDataSQL(sSupp, null);
        }

        public static DataTable GetSeason()
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sSeason = "SELECT CNY001 [Season Code], CNY002 [Description], PK FROM CNYMF031 WHERE CNY004=1";
            return acData.TblReadDataSQL(sSeason, null);
        }

        public static DataTable PressF4ForMFs(string sFi, string sTa, string sCri)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@Fields", SqlDbType.NVarChar) { Value = sFi };
            arrpara[1] = new SqlParameter("@Tables", SqlDbType.NVarChar) { Value = sTa };
            arrpara[2] = new SqlParameter("@Criteria", SqlDbType.NVarChar) { Value = sCri };
            return acData.TblReadDataSP("usp_CNY_0110", arrpara);
        }

        public static DataTable GetSupplierPaymentTerm00104(long lSupplierPk)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@SupplierPk", SqlDbType.BigInt) { Value = lSupplierPk };
            return acData.TblReadDataSP("usp_CNYG_00104", arrpara);
        }

        public static DataTable GetSupplierDeliveryMethod00105(long lSupplierPk)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@SupplierPk", SqlDbType.BigInt) { Value = lSupplierPk };
            return acData.TblReadDataSP("usp_CNYG_00105", arrpara);
        }

        public static DataTable GetSupplierDeliveryTerm00106(long lSupplierPk)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@SupplierPk", SqlDbType.BigInt) { Value = lSupplierPk };
            return acData.TblReadDataSP("usp_CNYG_00106", arrpara);
        }

        public static DataTable GetItemAttributes00108(string sItemCode)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@ItemCode", SqlDbType.NVarChar) { Value = sItemCode };
            return acData.TblReadDataSP("usp_CNYG_00108", arrpara);
        }

        public static DataTable GetAttributeValuesByKey00162(Int64 iCny08Pk)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = iCny08Pk };
            return acData.TblReadDataSP("usp_CNY_00162", arrpara);
        }

        public static DataTable GetUnit00109()
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            return acData.TblReadDataSP("usp_CNYG_00109", null);
        }

        public static DataTable GetMfAasy(string sCode)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sMf = $"SELECT AASY002 [Description], AASYID PK FROM AASY0000 WHERE AASYPK='{sCode}'";
            return acData.TblReadDataSQL(sMf, null);
        }

        public static DataTable GetStatusMf15(string sCode)
        {
            AccessData acData = new AccessData(DeclareSystem.SysConnectionString);
            string sMf = $"SELECT CNY002 [Description], CNY001 [Code], PK FROM CNYMF015 WHERE CNY004='{sCode}'";
            return acData.TblReadDataSQL(sMf, null);
        }
    }
}
