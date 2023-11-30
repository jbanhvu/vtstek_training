using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.Configuration;
using DevExpress.XtraEditors;
using System.Windows.Forms;


namespace CNY_BaseSys.Common
{
    public class AccessData
    {
        //GetDetailCompanyB sql text with parameter.
        #region "Property"
        private SqlConnection cnn;
        private SqlCommand cmd;
        private DataSet dts;
        private DataTable tbl;
        private SqlDataReader dr;
        private SqlDataAdapter da;
        private SqlTransaction tran;
        private SqlCommandBuilder cb;
        private readonly string connectionString = "";
        #endregion

        #region "contructor"
        public AccessData(string connectionStringPara)
        {
     
            this.connectionString = connectionStringPara;
        }
        #endregion

        #region "Process SQL server database"

        /// <summary>
        ///     Get Connection String SQL server Database
        /// </summary>
        /// <returns>
        ///     A System.Data.SqlClient.SqlConnection value...
        /// </returns>
        public SqlConnection BolConnect()
        {
            return new SqlConnection(connectionString);
            
        }

        public bool SaveTableIntoDatabase(string tblName, DataTable dt)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                string strSql = string.Format("SELECT * FROM {0}", tblName);
                da = new SqlDataAdapter(strSql, cnn);
                cb = new SqlCommandBuilder(da);
                da.Update(dt);
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                da.Dispose();
            }
            return false;
        }

        /// <summary>
        ///      Excute SQL text SQLserver Datatbase 
        /// </summary>
        /// <param name="strSql" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrParameter" type="System.Data.SqlClient.SqlParameter[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public Boolean BolExcuteSQL(string strSql, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                tran = cnn.BeginTransaction();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSql, Connection = cnn, Transaction = tran, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }

                cmd.ExecuteNonQuery();
                tran.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                tran.Rollback();
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                tran.Dispose();
            }
            return false;
        }


        /// <summary>
        ///     Excute Store Proceduce SQLserver Datatbase return Boolean 
        /// </summary>
        /// <param name="proceduceName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrParameter" type="System.Data.SqlClient.SqlParameter[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public Boolean BolExcuteSP(string proceduceName, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                tran = cnn.BeginTransaction();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = proceduceName, Connection = cnn, Transaction = tran, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }

                cmd.ExecuteNonQuery();
                tran.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception ex )
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                tran.Rollback();
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                tran.Dispose();
            }
            return false;
        }


        /// <summary>
        ///     Read Data Into Dataset by Store Proceduce
        /// </summary>
        /// <param name="proceduceName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrParameter" type="System.Data.SqlClient.SqlParameter[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataSet value...
        /// </returns>
        public DataSet DtsReadDataSP(string proceduceName, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = proceduceName, Connection = cnn, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                da = new SqlDataAdapter(cmd);
                dts = new DataSet();
                da.Fill(dts);
                cnn.Close();
                return dts;
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

        /// <summary>
        ///     Read Data Into Dataset by SQL text
        /// </summary>
        /// <param name="strSQL" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrParameter" type="System.Data.SqlClient.SqlParameter[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataSet value...
        /// </returns>
        public DataSet DtsReadDataSQL(string strSQL, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                da = new SqlDataAdapter(cmd);
                dts = new DataSet();
                da.Fill(dts);
                cnn.Close();
                return dts;
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

        /// <summary>
        ///     Read Data Into Datatable by Store Proceduce 
        /// </summary>
        /// <param name="proceduceName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrParameter" type="System.Data.SqlClient.SqlParameter[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public DataTable TblReadDataSP(string proceduceName, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = proceduceName, Connection = cnn, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                da = new SqlDataAdapter(cmd);
                tbl = new DataTable();
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

        /// <summary>
        ///     Read Data Into Datatable by SQL text
        /// </summary>
        /// <param name="strSQL" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public DataTable TblReadDataSQL(string strSQL, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                da = new SqlDataAdapter(cmd);
                tbl = new DataTable();
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

        public DataTable TblReadDataSQL(string strSQL, SqlParameter[] arrParameter, DataTable defaulTable)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                da = new SqlDataAdapter(cmd);
                tbl = new DataTable();
                da.Fill(tbl);
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
            return defaulTable;
        }



        /// <summary>
        ///      Read Data Into DataReader by Store Proceduce 
        /// </summary>
        /// <param name="proceduceName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrParameter" type="System.Data.SqlClient.SqlParameter[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.SqlClient.SqlDataReader value...
        /// </returns>
        public SqlDataReader DreadReadDataSP(string proceduceName, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = proceduceName, Connection = cnn, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                dr = cmd.ExecuteReader();
                cnn.Close();
                return dr;
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
                dr.Dispose();
            }
            return null;
        }

        /// <summary>
        ///    Read Data Into DataReader by SQL Text 
        /// </summary>
        /// <param name="strSQL" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrParameter" type="System.Data.SqlClient.SqlParameter[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.SqlClient.SqlDataReader value...
        /// </returns>
        public SqlDataReader DreadReadDataSQL(string strSQL, SqlParameter[] arrParameter)
        {
            try
            {
                cnn = BolConnect();
                cnn.Open();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn, CommandTimeout = 0 };
                if (arrParameter != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter t in arrParameter)
                        cmd.Parameters.Add(t);
                }
                dr = cmd.ExecuteReader();
                cnn.Close();
                return dr;
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
                dr.Dispose();
            }
            return null;
        }

        

        #endregion

        #region "Process Excel File"

        /// <summary>
        ///     Get Excel OLEDB connection string
        /// </summary>
        /// <param name="extension" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strPath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public string Getconnectexcel(string extension, string strPath)
        {
            string connString = "";
            connString = string.Format(extension.ToLower().Trim() == ".xls" ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;" :
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", strPath);
            return connString;
        }

        /// <summary>
        ///     Read Data from Excel File Into OleDBDataReader
        /// </summary>
        /// <param name="extension" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strPath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="sheetname" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.OleDb.OleDbDataReader value...
        /// </returns>
        public OleDbDataReader OleDbReadDataExcel(string extension, string strPath, string sheetname)
        {
            //string connectionstring = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", strPath);
            var connect = new OleDbConnection() { ConnectionString = Getconnectexcel(extension, strPath) };
            var command = new OleDbCommand(String.Format("select * from [{0}]", sheetname), connect);
            try
            {
                connect.Open();
                OleDbDataReader oldbDr = command.ExecuteReader();
                connect.Close();
                return oldbDr;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                command.Dispose();
            }
            return null;
        }

        /// <summary>
        ///     Read Data from Excel File Into DataTable
        /// </summary>
        /// <param name="extension" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strPath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="sheetname" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public DataTable TblReadDataExcel(string extension, string strPath, string sheetname)
        {
            var dtexcel = new DataTable();
            //   string connectionstring = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", strPath);
            var connect = new OleDbConnection() { ConnectionString = Getconnectexcel(extension, strPath) };
            var daexcel = new OleDbDataAdapter(String.Format("select * from [{0}]", sheetname), connect);
            try
            {

                connect.Open();
                daexcel.Fill(dtexcel);
                connect.Close();
                return dtexcel;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                daexcel.Dispose();
            }
            return null;

        }

        /// <summary>
        ///     Get Sheets Names On Excel File
        /// </summary>
        /// <param name="extension" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strPath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string[] value...
        /// </returns>
        public string[] GetExcelSheetNames(string extension, string strPath)
        {
            OleDbConnection objConn = null;
            DataTable dt = null;
            //  string sheetnames = "";
            try
            {
                objConn = new OleDbConnection(Getconnectexcel(extension, strPath));
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                var excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                objConn.Close();
                return excelSheets;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close(); 
                    objConn.Dispose(); 
                    dt.Dispose();
                }
               
            }
            return null;
        }

        #endregion



        #region "Create Datatable To Sql Table"

        public bool CopyDataFromDataTableToSqlTable(DataTable table, string tableName)
        {

            try
            {
           
                cnn = BolConnect();
                cnn.Open();
                tran = cnn.BeginTransaction();

                using (SqlBulkCopy sbCopy = new SqlBulkCopy(cnn, SqlBulkCopyOptions.TableLock, tran))
                {
                    sbCopy.BulkCopyTimeout = 0;
                    sbCopy.DestinationTableName = tableName;
                    foreach (DataColumn col in table.Columns)
                    {
                        string fieldName = col.ColumnName;
                        sbCopy.ColumnMappings.Add(fieldName, fieldName);
                    }
                    sbCopy.WriteToServer(table);
                }






              
                tran.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                tran.Rollback();
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                tran.Dispose();
            }
            return false;
        }


        public bool CreateTableSqlFromDataTable(DataTable table, string tableName)
        {
          
            try
            {
                string strSql = GetCreateFromDataTableSql(tableName, table);
                cnn = BolConnect();
                cnn.Open();
                tran = cnn.BeginTransaction();
                cmd = new SqlCommand { CommandType = CommandType.Text, CommandText = strSql, Connection = cnn, Transaction = tran, CommandTimeout = 0 };
                cmd.ExecuteNonQuery();
                tran.Commit();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                tran.Rollback();
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                tran.Dispose();
            }
            return false;
        }
        #endregion

        #region Static Methods


        private string GetCreateFromDataTableSql(string tableName, DataTable table)
        {
            string sql = "CREATE TABLE [" + tableName + "] (\n";
            // columns
            foreach (DataColumn column in table.Columns)
            {
                sql += "[" + column.ColumnName + "] " + SqlGetType(column) + ",\n";
            }
            sql = sql.TrimEnd(',', '\n') + "\n";
            // primary keys
            if (table.PrimaryKey.Length > 0)
            {
                sql += "CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED (";
                foreach (DataColumn column in table.PrimaryKey)
                {
                    sql += "[" + column.ColumnName + "],";
                }
                sql = sql.TrimEnd(',') + "))\n";
            }
            else
            {
                sql = sql + ")\n";
            }
            return sql;
        }



        // Return T-SQL data type definition, based on schema definition for a column
        private string SqlGetType(object type, int columnSize, int numericPrecision, int numericScale)
        {
    
            switch (type.ToString())
            {
                case "System.String":
                case "System.Char":
                    return "NVARCHAR(" + (columnSize == -1 ? 255 : columnSize) + ")";
                case "System.Decimal":
                case "System.Double":
                case "System.Single":
                    return "FLOAT";
                case "System.Int64":
                case "System.UInt64":
                    return "BIGINT";
                case "System.Int16":
                case "System.Int32":
                case "System.UInt16":
                case "System.UInt32":
                    return "INT";
                case "System.DateTime":
                    return "DATETIME";
                case "System.Boolean":
                    return "BIT";
                default:
                    return "VARBINARY(MAX)";
            }
        }

      
        // Overload based on DataColumn from DataTable type
        private string SqlGetType(DataColumn column)
        {
            return SqlGetType(column.DataType, column.MaxLength, 10, 2);
        }



       
        #endregion

    }
}
