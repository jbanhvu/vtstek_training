using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using CNY_Main.Class;
using CNY_Main.Properties;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Property;
using CNY_WH.Common;

namespace CNY_Main
{
    //public class ContainStringFinal
    //{
    //    public  Int32 ComponentCount { get; set; }
    //    public string [] StrinItem { get; set; }
    //}



    static class Program
    {
       




        //1=1 1=1 1= 1= 1=1=1=
   
        public static Form FrmShow;
        public static bool CheckSingleInstance=true;
    /// <summary> The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            DevExpress.ExpressApp.FrameworkSettings.DefaultSettingsCompatibilityMode = DevExpress.ExpressApp.FrameworkSettingsCompatibilityMode.v20_1;
            //string d= convertToUnSign3("");
            //string f = d;


            //string a =
            //    "wPsktTzzer3oy4g40HlrTgvrb0Q90Iz6/DTUvZKj6NuMtcqFFm8AjUfsM8cBHSD6Pb7l2FQeLH4skUkLxugWMkjIH2kA73YiospEuIWv9UqMUeQiO2KNC0EXNuYIiziyXPHxFwio+E1YcZ7bHgwD+jXDoyz2an5P/lrhfOjkVoKw3iCEJ3JP6ll3BqFbbaT40t1ahwAnxCBb20IzdelHyQ==";
            //string b= EnDeCrypt.Decrypt(a, true);
            //string c = b;
            //Application.Run(new Form1());


            /*

            string str = "01,02,03,04,05,06";
            var q1 = str.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            int count = q1.Length;
            int loop = count - 2;
            List< ContainStringFinal > l = new List<ContainStringFinal>();
            foreach (string s in q1)
            {
                l.Add(new ContainStringFinal
                {
                    ComponentCount = 1,
                    StrinItem = new[] { s }
                });
            }
            if (loop > 0)
            {
                string lastItem = q1.Last();
                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = i+1; j < count; j++)
                    {
                        l.Add(new ContainStringFinal
                        {
                            ComponentCount = 2,
                            StrinItem = new[] {q1[i],q1[j]}
                        });
                    }
                }

                for (int k = 1; k < loop; k++)
                {
                    int componentCountPrivious = k + 1;
                    List<ContainStringFinal> lTemp = l.Where(p => p.ComponentCount == componentCountPrivious).ToList();
                    int componentCountCurrent = componentCountPrivious + 1;
                    foreach (var item in lTemp)
                    {
                        string[] arrItem = item.StrinItem;

                        string lastItemLoop = arrItem.Last();
                        if(lastItem == lastItemLoop) continue;


                        Int32 countFind = 0;
                        bool isFind = false;
                        for (int t = componentCountPrivious-1; t < count; t++)
                        {

                            string strAdd = q1[t];
                            if (strAdd == lastItemLoop)
                            {
                                countFind = 1;
                                isFind = true;
                            }

                            if (isFind && countFind>1)
                            {
                                string[] arrItemAdd = new string[componentCountCurrent];

                                for (int m = 0; m < arrItem.Length; m++)
                                {
                                    arrItemAdd[m] = arrItem[m];
                                }
                                arrItemAdd[componentCountCurrent - 1] = strAdd;

                                l.Add(new ContainStringFinal
                                {
                                    ComponentCount = componentCountCurrent,
                                    StrinItem = arrItemAdd
                                });
                            }
                            countFind++;


                        }


                    }

                }

            }



            l.Add(new ContainStringFinal
            {
                ComponentCount = count,
                StrinItem = new[] { string.Join(",",q1) }
            });

            string strFinal = "";
            foreach (var item in l)
            {
                string strItem = string.Join(",", item.StrinItem);
                strFinal = strFinal + strItem + " \n";



            }
            MessageBox.Show(strFinal);

    */
            //01
            //    02
            //    03
            //    04
            //    01,02
            //    01,03
            //    01,04
            //    02,03
            //    02,04
            //    03,04
            //    01,02,03,
            //01,02,04,
            //01,03,04
            //02,03,04
            //    01,02,03,04


            //    01,02//03,
            //     01,02//04
            //    01,03//04

            //    02,03//04
            // 


            //var q2 =string.Join(",", q1.SelectMany(p => q1, (p, t) => new { p, t }).Where(@t1 => @t1.p.Index < @t1.t.Index)
            //           .Select(@t1 => string.Format("'{0},{1}'", @t1.p.Value, @t1.t.Value))) + "," + string.Join(",", q1.Select(p => string.Format("'{0}'", p.Value)));

            //01,02,01,03,01,03,02,03,02,04,03,04




            //MathFormula mF = new MathFormula("1+(2+3)*2^2");
            //double value = mF.Value;

            //   MathFormula mFp = new MathFormula("1+(A+B)2");
            //mFp.AddParameter("A", 2);
            //mFp.AddParameter("B", 3);
            //double valuep = mFp.Value;


          


            SystemProperty.SysSkinName = GetSkinName();
            GetInfoPathConfigWorkingServer();
            bool showFirstInstance = SingleInstance.Start();
            if (CheckSingleInstance && GetConfigSingleInstance() && !showFirstInstance)
            {
                SingleInstance.ShowFirstInstance();
                return;
            }

            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SkinName = SystemProperty.SysSkinName;
            Skin skin = GridSkins.GetSkin(UserLookAndFeel.Default);
            if (skin != null)
            {
                skin.Properties[GridSkins.OptShowTreeLine] = true;
            }
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();


            Application.EnableVisualStyles();
            SystemProperty.SysOsName = GetOSFriendlyName();
        
            try
            {
                Application.Run(FrmShow ?? new FrmStartup());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //  XtraMessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (CheckSingleInstance && GetConfigSingleInstance())
            {
                SingleInstance.Stop();
            }


            //SystemGetCodeRule abc = new SystemGetCodeRule(19, "CNY00011");
            //abc.CreateCodeString();
            //string aa = abc.CodeRuleData.StrCode;
            //DataTable dta = abc.CodeRuleData.DtCode;
        }


        public static string GetSkinName()
        {
            string skinName = Settings.Default.SkinName;
            if (string.IsNullOrEmpty(skinName))
            {
                skinName = "DevExpress Style";
            }
            return skinName;
        }
       

        public static bool GetConfigSingleInstance()
        {
            if (!File.Exists(SystemProperty.SysPathConfigExe)) return false;
            try
            {
                XElement xelement = XElement.Load(SystemProperty.SysPathConfigExe);
                var query = (xelement.Descendants("Machine")
                    .Where(p => p.Element("Computer").Value.ToUpper().Trim() == Environment.MachineName.ToUpper().Trim())
                    .Select(p => new
                    {
                        Computer = p.Element("Computer").Value.ToUpper().Trim(),
                        SingleInstance = p.Element("SingleInstance").Value,
                    })).Take(1);
                bool result = true;
                foreach (var item in query)
                {
                    result = Convert.ToBoolean(item.SingleInstance);
                    break;
                }
                return result;
            }
            catch 
            {
                return true;
            }
        }
        /// <summary>
        ///     Get Os Name
        /// </summary>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetOSFriendlyName()
        {
            string result = string.Empty;
            var searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (var o in searcher.Get())
            {
                var os = (ManagementObject) o;
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }
        /// <summary>
        ///     Get Current Process Running Program
        /// </summary> <returns>
        ///     A System.Diagnostics.Process value...
        /// </returns>
        public static Process GetProgramProcess()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(Assembly.GetExecutingAssembly().GetName().Name);
                if (processes.Length > 0)
                {
                    return processes[0];
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
   
        /// <summary>
        ///     Get Current Program Usage Ram
        /// </summary>
        /// <param name="p" type="System.Diagnostics.Process">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static long GetRAMCounter(Process p)
        {
            try
            {
                //20221121 Vũ khóa lại để ko cần lấy thông số RAM
                //if (p != null)
                //{
                //    var ramCounter = new PerformanceCounter
                //                         {
                //                             CategoryName = "Process",
                //                             CounterName = "Working Set - Private",
                //                             InstanceName = p.ProcessName
                //                         };
                //    return Convert.ToInt64(ramCounter.NextValue());
                //}
                return 26214400;
            }
            catch
            {
                return 26214400;
            }
        }

        public static void GetInfoPathConfigWorkingServer()
        {

            //    SystemProperty.SysPathConfigExe = ConfigurationManager.AppSettings["ConfigExe"];
           // string b = EnDeCrypt.Encrypt("PRODUCTION", true);
            using (var iniS = new IniFile())
            {
                iniS.Load(Application.StartupPath + @"\Extension\CNY.ini");
                SystemProperty.SysWorkingServer = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "WorkingServer"), true);
            }

            using (var iniV = new IniFile())
            {
                iniV.Load(Application.StartupPath + @"\Extension\Version.ini");
                SystemProperty.SysVersion = iniV.GetKeyValue("version", "VersionCNY");
                SystemProperty.SysPathConfigExe = iniV.GetKeyValue("version", "PathConfigExe");
            }
          



        }


        /// <summary>
        ///     Get All Values On Ini File
        /// </summary>
        public static void GetInfoApplication()
        {
            using (var iniS = new IniFile())
            {
                iniS.Load(Application.StartupPath + @"\Extension\CNY.ini");
                TempParameter.TempConnectStringProduction = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "ConnectionString"), true);
                TempParameter.TempConnectStringTest = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "ConnectionStringTest"), true);
                TempParameter.TempConnectStringTrial = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "ConnectionStringTrial"), true);


                //    SystemProperty.SysWorkingServer = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "WorkingServer"), true);

                //string a = @"C:\inetpub\vhosts\trangia.com\httpdocs\CNY_SOLUTION_CONFIG\BACKUPDBTEMP";
                //string b = EnDeCrypt.Encrypt(a, true);


                GetConnectStringServerMain();


                SystemProperty.SysConnect = new SqlConnection(SystemProperty.SysConnectionString);
                SystemProperty.SysUserInDomain = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "UID"), true);
                SystemProperty.SysPasswordOfUserInDomain = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "PUID"), true);
               

                SystemProperty.SysDefaultSendMailPass = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "DefaultSendMailPass"), true);
                SystemProperty.SysMailServer = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "MailServer"), true);
                SystemProperty.SysMailPort = ProcessGeneral.GetSafeInt(iniS.GetKeyValue("system", "MailPort"));


                SystemProperty.BackupDatabasePath = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "BackupDatabasePath"), true);
                SystemProperty.SysMailUserName = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "UIM"), true);
                SystemProperty.SysMailUserPass = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "PUIM"), true);
                SystemProperty.UseFTPServer = ProcessGeneral.GetSafeString(iniS.GetKeyValue("system", "UseFTPServer")) == "1";
                SystemProperty.BackupDatabasePathFtp = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "BackupDatabasePathFtp"), true);

                SystemProperty.FtpPass = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "FtpPass"), true);
                SystemProperty.FtpUser = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "FtpUser"), true);
                SystemProperty.PathFtpBackupDatabaseTemp = EnDeCrypt.Decrypt(iniS.GetKeyValue("system", "PathFtpBackupDatabaseTemp"), true);



            }


         
         

        }

        private static void GetTempConnectString()
        {
            int index = 0;
            string strCnn = "";
            index = SystemProperty.SysConnectionString.IndexOf("Connect Timeout", StringComparison.Ordinal);
            if (index < 0)
            {
                index = SystemProperty.SysConnectionString.IndexOf("Connection Timeout", StringComparison.Ordinal);
            }
            strCnn = index < 0 ? SystemProperty.SysConnectionString : String.Format("{0}Connection Timeout=15", SystemProperty.SysConnectionString.Substring(0, index));
            SystemProperty.SysTempTestConnectString = strCnn;
            
        }

        public static void GetConnectStringServerMain()
        {
            SystemProperty.SysConnectionString = TempParameter.TempConnectStringProduction;
            switch (SystemProperty.SysWorkingServer.ToUpper().Trim())
            {
                case "PRODUCTION":
                    SystemProperty.SysConnectionString = TempParameter.TempConnectStringProduction;
                    break;
                case "TRIAL":
                    SystemProperty.SysConnectionString = TempParameter.TempConnectStringTrial;
                    break;
                case "TEST":
                    SystemProperty.SysConnectionString = TempParameter.TempConnectStringTest;
                    break;
            }


            const string strInit = "Initial Catalog";
            int index1 = SystemProperty.SysConnectionString.IndexOf(strInit, StringComparison.Ordinal);
            string ind1 = SystemProperty.SysConnectionString.Substring(index1 + strInit.Length, SystemProperty.SysConnectionString.Length - index1 - strInit.Length).Trim();
            int index2 = ind1.IndexOf(";", StringComparison.Ordinal);
            string ind2 = ind1.Substring(0, index2);
            SystemProperty.SysDataBaseName = ind2.Replace("=", "").Trim();

            GetTempConnectString();


        }

       
        /// <summary>
        ///     Check Open Connection To SQL When Load Program
        /// </summary>
        public static bool CheckOpenConnectionSQL()
        {
         
            bool result = true;





            var cnn = new SqlConnection(SystemProperty.SysTempTestConnectString);
            try
            {
            
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }


            }
            catch 
            {
                XtraMessageBox.Show("Error Connection, Try Again...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                result = false;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                cnn.Dispose();
            }
            return result;
        }
        /// <summary>
        ///     Open new Form Close Current Form
        /// </summary>
        /// <param name="fShow" type="System.Windows.Forms.Form">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="chkSingleInstance" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void OpenFormByThread(Form fShow, bool chkSingleInstance)
        {
            var tShow = new Thread(() =>
                                       {
                                           FrmShow = fShow;
                                           CheckSingleInstance = chkSingleInstance;
                                           Main();
                                       });
            
            tShow.SetApartmentState(ApartmentState.STA);
            tShow.Start();
            tShow.Join();
            tShow.Abort();
          
        }


        public static void OpenMainFormByThread()
        {
            var tShow = new Thread(OpenMainForm);
            tShow.SetApartmentState(ApartmentState.STA);
            tShow.Start();
            tShow.Join();
            tShow.Abort();

        }

        private static void OpenMainForm()
        {
            SystemProperty.SysSkinName = GetSkinName();
            GetInfoPathConfigWorkingServer();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SkinName = SystemProperty.SysSkinName;
            Skin skin = GridSkins.GetSkin(UserLookAndFeel.Default);
            if (skin != null)
            {
                skin.Properties[GridSkins.OptShowTreeLine] = true;
            }
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            Application.EnableVisualStyles();
            SystemProperty.SysOsName = GetOSFriendlyName();
            try
            {
                Application.Run(new FrmMain
                {
                    TopMost = true
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //  XtraMessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        /// <summary>
        ///     The Time used to got Program
        /// </summary>
        /// <param name="secondsInput" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetUsageTimeProgram(int secondsInput)
        {
            int hours = 0, minutes = 0;
            string finalHours = "", finalMinutes = "", finalSeconds = "";
            hours = (secondsInput % 3600);finalHours = Convert.ToString(secondsInput / 3600);
            if (finalHours.Trim().Length == 1)
            {
                finalHours = String.Format("0{0}", finalHours);
            }
            minutes = (hours % 60);
            finalMinutes = Convert.ToString(hours / 60);
            if (finalMinutes.Trim().Length == 1)
            {
                finalMinutes = String.Format("0{0}", finalMinutes);
            }
            finalSeconds = Convert.ToString(minutes / 1);
            if (finalSeconds.Trim().Length == 1)
            {
                finalSeconds = String.Format("0{0}", finalSeconds);
            }
            return string.Format("{0}:{1}:{2}", finalHours, finalMinutes, finalSeconds);
        }
    }
}
