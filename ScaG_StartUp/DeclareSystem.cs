using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace CNY_StartUp
{
    public static class DeclareSystem
    {
        public const string RunExeMainName = "CNY_Main.exe";
        public static bool IsCancel = false;
        public static string SysVersionReportServer = "";
        public static string SysVersionServer = "";
        public static string SysVersionReport = "";
        public static string SysVersion = "";
        public static string SysPathDllServer = "";
        public static string SysPathReportServer = "";
        /// <summary>
        ///     Path Folder Update In Server
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysPathUpdate = "";
        /// <summary>
        ///     Path File Version Server For Update Program
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public static string SysPathFileVersionServer = "";
        public static string SysConnectionString = "";


        public static void LaunchExe(string exePath = "")
        {
            try
            {
                if (!File.Exists(exePath)) return;
                var tShow = new Thread(() =>
                                   {
                                       var startInfo = new ProcessStartInfo() { FileName = exePath };
                                       Process.Start(startInfo);
                                   });
                tShow.SetApartmentState(ApartmentState.STA);
                tShow.Start();
                tShow.Join();
                tShow.Abort();
               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(string.Format("Error : {0}", ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LaunchExeWithOutThread(string exePath = "")
        {
            try
            {
                var startInfo = new ProcessStartInfo() { FileName = exePath };
                Process.Start(startInfo);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(string.Format("Error : {0}", ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool GetConnectionSQL(string strConnect)
        {
            bool result = true;
            int index = 0;
            string strCnn = "";
            index = strConnect.IndexOf("Connect Timeout", StringComparison.Ordinal);
            if (index < 0)
            {
                index = strConnect.IndexOf("Connection Timeout", StringComparison.Ordinal);
            }
            strCnn = index < 0 ? strConnect : String.Format("{0}Connection Timeout=15", strConnect.Substring(0, index));
            var cnn = new SqlConnection(strCnn);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }


            }
            catch 
            {
                result = false;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }

        public static DataTable TblReadDataSQL(string strConnect,string strSql, SqlParameter[] arrParameter)
        {
            var cnn = new SqlConnection(strConnect); 
            var cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSql, Connection = cnn, CommandTimeout = 0 };
            var da =new SqlDataAdapter();
            try
            {
                
                cnn.Open();
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                da.SelectCommand = cmd;
                var tbl = new DataTable();
                da.Fill(tbl);
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
            return null;
        }
    }
}
