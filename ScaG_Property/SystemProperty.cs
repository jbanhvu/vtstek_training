using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CNY_Property
{
    public static class SystemProperty
    {
        public static string SysWorkingServer = "";
        public static DataTable DtPrDepartmentEmail;
        public static Dictionary<string,DataTable> DicPrUserEmail;


        public static DataTable DtUserListWhenLogin;
        public static DataTable DtBoMAssignMain;
        public static string PathFtpBackupDatabaseTemp = "";
        public static string FtpUser = "";
        public static string FtpPass = "";



        public static DataTable SysTablePermissionMainForm ;
        public static string SysTempTestConnectString = "";


        public static string SysProductName = "";
        public static string SysMachineName = "";
        public static string SysDefaultGateway = "";
        public static string SysUserLoginWindows = "";
        public static bool SysIsDomainUser = true;
  

      
        public static string SysSkinName = "";



        /// <summary>
        ///     Set Item Per Page On GridView
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static int SysItemsPaging = 10;

        /// <summary>
        ///     Port SendMail
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static int SysMailPort = 25;
        public static string BackupDatabasePathFtp = "";
        public static string BackupDatabasePath = "";
        public static bool UseFTPServer = false;

        /// <summary>
        ///     mail server
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysMailServer = "";

        public static string SysMailUserName = "";

        public static string SysMailUserPass = "";

        /// <summary>
        ///     Pass login email 
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysDefaultSendMailPass = "";
        /// <summary>
        ///     Path Config Single Instance Exe When Run
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysPathConfigExe = "";


        /// <summary>
        ///     Version Program Read File CNY.ini
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysVersion = "";

     
 
        /// <summary>
        ///     Primary key Table ListUser
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static Int64 SysUserId = 0;
        /// <summary>
        ///     Username When Login System
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysUserName = "";
        public static string SysFullName = "";
        public static string SysEmail = "";
        public static string SysPassword = "";
        public static string SysChangePassDate = "";
        public static int SysFinanceYear = 0;
        public static string SysPositionsCode = "";
        public static string SysPositionsName = "";
        public static string SysDepartmentCode = "";
        public static string SysDepartmentName = "";
        public static string SysCompanyCode = "";
        public static string SysOsName = "";
        public static string SysConnectionString = "";
        public static SqlConnection SysConnect = new SqlConnection();
        /// <summary>
        ///     Save Username When Login System In Textbox Username frmLogin
        /// </summary>
        /// <remarks>
        ///      </remarks>
        public static string SysAccount = "";
        /// <summary>
        ///     Computer Name
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysHostName = "";
        /// <summary>
        ///     User Login In Sql Server
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysLoggedInUser = "";
        /// <summary>
        ///     Server Name Sql
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysServerSql = "";
        public static string SysDomainName = "";
        public static string SysUserInDomain = "";
        public static string SysPasswordOfUserInDomain = "";
        /// <summary>
        ///     Computer Name + Domain Name
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysFullDomainName = "";
        public static string SysIpAddress = "";
        public static Int64 SysIdAuthorization = 0;
        public static Int64 SysUserInGroupId = 0;
        public static string SysUserGroupCode = "";
        public static string SysPermissionGroupCode = "";
        public static DataTable SysDtCbUserGroup;
        public static DataTable SysDtCbPermission;
        public static string SysDataBaseName = "";
    }
}
