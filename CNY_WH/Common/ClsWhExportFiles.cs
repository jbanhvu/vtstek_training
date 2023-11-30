using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevSpSheet = DevExpress.XtraSpreadsheet;
using MsExcel = Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;

namespace CNY_WH.Common
{
    public static class ClsWhExportFiles
    {
        public static void ExportDataToTextFile(DataTable dtExport, string sExportTableName)
        {
            try
            {
                AccessData AccData = new AccessData(DeclareSystem.SysConnectionString);
                #region Save File
                var f = new SaveFileDialog()
                {
                    Title = String.Format("Export Data To Text File."),
                    Filter = @"Text Files(.txt)|*.txt",
                    RestoreDirectory = true
                };
                #endregion

                if (f.ShowDialog() == DialogResult.OK)
                {
                    #region Check existed file
                    if (f.CheckFileExists)
                    {
                        if (
                            XtraMessageBox.Show("File name has already exists.Do you want to overwrite file?",
                                                "Question!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            File.Delete(f.FileName);
                        }
                        else
                        {
                            return;
                        }
                    }
                    #endregion

                    string strSql = string.Format(" Select * from SY41{0}00 Where SY41001='{1}' and SY41007 <> '0' ", DeclareSystem.SysCompanyCode, sExportTableName);
                    DataTable dtWidth = AccData.TblReadDataSQL(strSql, null);
                    var str = new StreamWriter(f.FileName, false, Encoding.Unicode);

                    #region formatGrid
                    foreach (DataRow datarow in dtExport.Rows)
                    {
                        string row = string.Empty;
                        for (int i = 0; i < dtExport.Columns.Count; i++)
                        {
                            string strResult = "";
                            if (dtExport.Columns[i].DataType == typeof(DateTime))
                            {
                                strResult = String.Format("{0:ddMMyy}", datarow[i]);
                            }
                            else
                            {
                                strResult = ProcessGeneral.GetSafeString(datarow[i]);
                            }
                            int width = ProcessGeneral.GetSafeInt(dtWidth.Rows[i]["SY41008"]);
                            if (strResult.Length < width)
                            {
                                strResult = strResult + ProcessGeneral.SetStringSpace(width - strResult.Length);
                            }
                            strResult = strResult.Substring(0, width);
                            row += strResult;
                        }
                        str.WriteLine(row);
                    }
                    #endregion

                    str.Flush();
                    str.Close();
                }
                XtraMessageBox.Show("Export data complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw new Exception("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        public static void ExportDataToExcelFile(DataTable dtExcel, object oEx)
        {
            try
            {
                #region Save file

                var f = new SaveFileDialog()
                {
                    Title = @"Save As...",
                    Filter = @"Excel Files (*.xls)|*.xls",
                    RestoreDirectory = true
                };

                #endregion

                if (f.ShowDialog() == DialogResult.OK)
                {
                    #region fn_Check exist

                    string pathExcel = f.FileName;
                    if (File.Exists(pathExcel))
                    {
                        if (
                            XtraMessageBox.Show("File name has already exists.Do you want to overwrite file?",
                                                "Question!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            File.Delete(pathExcel);
                        }
                        else
                        {
                            return;
                        }
                    }

                    #endregion

                    Application xlApp_Stk;
                    MsExcel.Worksheet xlSheet_Stk;
                    MsExcel.Workbook xlBook_Stk;

                    //doi tuong Trống để thêm vào xlApp_Stk
                    object missValue = Missing.Value;

                    //khoi tao doi tuong Com Excel moi
                    xlApp_Stk = new Application();
                    xlBook_Stk = xlApp_Stk.Workbooks.Add(missValue);

                    //su dung Sheet dau tien de thao tac
                    xlSheet_Stk = (MsExcel.Worksheet)xlBook_Stk.Worksheets.get_Item(1);

                    //không cho hiện ứng dụng Excel
                    xlApp_Stk.Visible = false;
                    int columnNumber = dtExcel.Columns.Count;
                    int rowNumber = dtExcel.Rows.Count;
                    int i, j;

                    try
                    {
                        for (i = 0; i < columnNumber; i++)
                        {
                            xlSheet_Stk.Cells[1, i + 1] = dtExcel.Columns[i].ColumnName;
                        }

                        for (i = 0; i < rowNumber; i++)
                        {
                            for (j = 0; j < columnNumber; j++)
                            {
                                xlSheet_Stk.Cells[i + 2, j + 1] = dtExcel.Rows[i][j];
                            }
                        }
                        xlApp_Stk.Columns.AutoFit();

                        //save file
                        xlBook_Stk.SaveAs(pathExcel, MsExcel.XlFileFormat.xlWorkbookNormal, missValue, missValue,
                                          missValue, missValue, MsExcel.XlSaveAsAccessMode.xlExclusive, missValue,
                                          missValue,
                                          missValue, missValue, missValue);
                        xlBook_Stk.Close(true, missValue, missValue);
                        xlApp_Stk.Quit();

                        ReleaseObject(xlSheet_Stk);
                        ReleaseObject(xlBook_Stk);
                        ReleaseObject(xlApp_Stk);
                    }
                    catch (Exception ex1)
                    {
                        throw new Exception(ex1.Message);
                    }
                    finally
                    {
                        foreach (Process pro in Process.GetProcessesByName("EXCEL"))
                        {
                            pro.Kill();
                        }
                    }

                    GC.SuppressFinalize(oEx);
                    XtraMessageBox.Show("Export data complete.", "Information", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void ExportGridViewtoExcel(GridView gView)
        {
            #region Export Files

            var f = new SaveFileDialog()
            {
                Title = @"Export Data",
                Filter = @"Excel 2007,2010,2013 Files (*.xlsx)|*.xlsx|Excel 2003 files (*.xls)|*.xls|Pdf files (*.pdf)|*.pdf",
                RestoreDirectory = true
            };

            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.CheckFileExists)
                {
                    File.Delete(f.FileName);
                }

                string pathExport = f.FileName;
                switch (Path.GetExtension(pathExport).ToLower().Trim())
                {
                    case ".pdf":
                        gView.ExportToPdf(pathExport);
                        break;
                    case ".xlsx":
                        gView.ExportToXlsx(pathExport);
                        break;
                    case ".xls":
                        gView.ExportToXls(pathExport);
                        break;
                }

                XtraMessageBox.Show("Export data complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2);
            }

            #endregion
        }
        public static void ExportDataToXmlFile(DataSet dsExport)
        {
            try
            {
                #region Save File

                var f = new SaveFileDialog()
                {
                    Title = String.Format("Export Data To Xml File."),
                    Filter = @"XML Files(.xml)|*.xml",
                    RestoreDirectory = true
                };

                #endregion

                if (f.ShowDialog() == DialogResult.OK)
                {
                    #region Check existed file

                    if (f.CheckFileExists)
                    {
                        if (
                            XtraMessageBox.Show("File name has already exists.Do you want to overwrite file?",
                                                "Question!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            File.Delete(f.FileName);
                        }
                        else
                        {
                            return;
                        }
                    }

                    #endregion

                    if (dsExport == null) return;
                    StreamWriter sWriXml = new StreamWriter(f.FileName, false);

                    dsExport.WriteXml(sWriXml);
                    sWriXml.Close();
                }
                XtraMessageBox.Show("Export data complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataSet ReadXmlFile()
        {
            try
            {
                DataSet dsRea = new DataSet();
                #region Open File

                var f = new OpenFileDialog()
                {
                    Title = String.Format("Select Xml File."),
                    Filter = @"XML Files(.xml)|*.xml",
                    RestoreDirectory = true
                };

                #endregion

                if (f.ShowDialog() == DialogResult.OK)
                {
                    dsRea.ReadXml(f.FileName, XmlReadMode.Auto);
                }
                XtraMessageBox.Show("Finished!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return dsRea;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
