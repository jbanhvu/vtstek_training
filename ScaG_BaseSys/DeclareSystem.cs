using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CNY_BaseSys.Common;
using CNY_Property;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys
{
    public static class DeclareSystem
    {
        public static string SysWorkingServer { get { return SystemProperty.SysWorkingServer; } }
        public static WorkingAt SysWorkingAt
        {
            get
            {
                WorkingAt w = WorkingAt.PRODUCTION;
                switch (SysWorkingServer.ToUpper().Trim())
                {
                    case "PRODUCTION":
                        w = WorkingAt.PRODUCTION;
                        break;
                    case "TEST":
                        w = WorkingAt.TEST;
                        break;
                }
                return w;
            }
        }

        public static DataTable DtPrDepartmentEmail { get { return SystemProperty.DtPrDepartmentEmail; } }
        public static Dictionary<string, DataTable> DicPrUserEmail { get { return SystemProperty.DicPrUserEmail; } }
        public static DataTable DtUserListWhenLogin { get { return SystemProperty.DtUserListWhenLogin; } }

        public static GridControl GridControlBomAssignInfo { get; set; }
        public static GridView GridViewBomAssignInfo { get; set; }

        public static DataTable DtBoMAssignMain { get; set; }
        public static DateTime DateGetBoMAssignMain { get; set; }
        public static string PathFtpBackupDatabaseTemp { get { return SystemProperty.PathFtpBackupDatabaseTemp; } }
        public static string FtpUser { get { return SystemProperty.FtpUser; } }
        public static string FtpPass { get { return SystemProperty.FtpPass; } }

        public static string BackupDatabasePathFtp { get { return SystemProperty.BackupDatabasePathFtp; } }
        public static bool UseFTPServer { get { return SystemProperty.UseFTPServer; } }

        public static string BackupDatabasePath { get { return SystemProperty.BackupDatabasePath; } }
        public static DataTable SysTablePermissionMainForm { get { return SystemProperty.SysTablePermissionMainForm; } }

        public static string SysTempTestConnectString { get { return SystemProperty.SysTempTestConnectString; } }



        public static string SysProductName { get { return SystemProperty.SysProductName; } }
        public static string SysMachineName { get { return SystemProperty.SysMachineName; } }
        public static string SysDefaultGateway { get { return SystemProperty.SysDefaultGateway; } }
        public static string SysUserLoginWindows { get { return SystemProperty.SysUserLoginWindows; } }
        public static bool SysIsDomainUser { get { return SystemProperty.SysIsDomainUser; } }

   
        public static string SysSkinName { get { return SystemProperty.SysSkinName; } }


        /// <summary>
        ///     Set Item Per Page On GridView
        /// </summary>
        /// <value>
        ///     <para>
        ///         
        ///     </para>
        /// </value>
        /// <remarks>
        ///     
        /// </remarks>
        public static int SysItemsPaging { get { return SystemProperty.SysItemsPaging; } }
 
     
        /// <summary>
        ///     Port SendMail
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static int SysMailPort { get { return SystemProperty.SysMailPort; } }
        /// <summary>
        ///     mail server
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysMailServer { get { return SystemProperty.SysMailServer; } }


        public static string SysMailUserName { get { return SystemProperty.SysMailUserName; } }
        public static string SysMailUserPass { get { return SystemProperty.SysMailUserPass; } }

        /// <summary>
        ///     Pass login email 
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysDefaultSendMailPass { get { return SystemProperty.SysDefaultSendMailPass; } }
        /// <summary>
        ///     Path Config Single Instance Exe When Run
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysPathConfigExe { get { return SystemProperty.SysPathConfigExe; } }


        /// <summary>
        ///     Version Program Read File CNY.ini
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysVersion { get { return SystemProperty.SysVersion; } }

  
     
        /// <summary>
        ///     Primary key Table ListUser
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static Int64 SysUserId { get { return SystemProperty.SysUserId; } }
        /// <summary>
        ///     Username When Login System
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysUserName { get { return SystemProperty.SysUserName; } }

        public static string SysFullName { get { return SystemProperty.SysFullName; } }

        public static string SysEmail { get { return SystemProperty.SysEmail; } }

        public static string SysPassword { get { return SystemProperty.SysPassword; } }

        public static string SysChangePassDate { get { return SystemProperty.SysChangePassDate; } }

        public static int SysFinanceYear { get { return SystemProperty.SysFinanceYear; } }

        public static string SysPositionsCode { get { return SystemProperty.SysPositionsCode; } }

        public static string SysPositionsName { get { return SystemProperty.SysPositionsName; } }

        public static string SysDepartmentCode { get { return SystemProperty.SysDepartmentCode; } }

        public static string SysDepartmentName { get { return SystemProperty.SysDepartmentName; } }

        public static string SysCompanyCode { get { return SystemProperty.SysCompanyCode; } }

        public static string SysOsName { get { return SystemProperty.SysOsName; } }

        public static string SysConnectionString { get { return SystemProperty.SysConnectionString; } }

        public static SqlConnection SysConnect { get { return SystemProperty.SysConnect; } }
        /// <summary>
        ///     Save Username When Login System In Textbox Username frmLogin
        /// </summary>
        /// <remarks>
        ///      </remarks>
        public static string SysAccount { get { return SystemProperty.SysAccount; } }
        /// <summary>
        ///     Computer Name
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysHostName { get { return SystemProperty.SysHostName; } }
        /// <summary>
        ///     User Login In Sql Server
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysLoggedInUser { get { return SystemProperty.SysLoggedInUser; } }


        /// <summary>
        ///     Server Name Sql
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysServerSql { get { return SystemProperty.SysServerSql; } }

        public static string SysDomainName { get { return SystemProperty.SysDomainName; } }

        public static string SysUserInDomain { get { return SystemProperty.SysUserInDomain; } }

        public static string SysPasswordOfUserInDomain { get { return SystemProperty.SysPasswordOfUserInDomain; } }
        /// <summary>
        ///     Computer Name + Domain Name
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysFullDomainName { get { return SystemProperty.SysFullDomainName; } }

        public static string SysIpAddress { get { return SystemProperty.SysIpAddress; } }

        public static Int64 SysIdAuthorization { get { return SystemProperty.SysIdAuthorization; } }

        public static Int64 SysUserInGroupId { get { return SystemProperty.SysUserInGroupId; } }

        public static string SysUserGroupCode { get { return SystemProperty.SysUserGroupCode; } }

        public static string SysPermissionGroupCode { get { return SystemProperty.SysPermissionGroupCode; } }

        public static DataTable SysDtCbUserGroup { get { return SystemProperty.SysDtCbUserGroup; } }

        public static DataTable SysDtCbPermission { get { return SystemProperty.SysDtCbPermission; } }

        /// <summary>
        ///     Main DataBase When Login System
        /// </summary>
        /// <value>
        ///     <para>
        ///         
        ///     </para>
        /// </value>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysDataBaseName { get { return SystemProperty.SysDataBaseName; } }

   

        
    }
}
