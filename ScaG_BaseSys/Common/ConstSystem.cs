using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public static class ConstSystem
    {
        public const string SysNameBoMMainLayout = "CNY_BOM-XtraUCBOMMain-gcMain.xml";
        public const string SysNameBoMMainFiLayout = "CNY_BOM-XtraUCBOMMainFi-gcMain.xml";
        public const bool BoMAllowSaveDiffUserCreated = false;
        public const bool PrAllowSaveDiffUserCreated = false;
        public const bool MoAllowSaveDiffUserCreated = false;
        public const string DefaultPrNormalType = "PR01";
        public const string DefaultPrDiffNormalType = "PR04";
        public const string DefaultPrFinishingType = "PR10";
        public const string DefaultPrSupplementType = "PR02";
        public const string DefaultPrSupplementTypeF = "PR20";
        public const string DefaultPrSampleType = "PR03";
        public const string DefaultPrSampleTypeF = "PR30";
        public const string DefaultPrDiffFinishingType = "PR40";
        public const int FormatDefaultDecimal = 10;
        public const int FormatCodePackingFactorDecimal = 10;
        public const int FormatUcDecimal = 5;
        public const int FormatFactorDecimal = 4;
        public const int FormatOrderQtyDecimal = 0;
        public const int FormatWeightDecimal = 2;
        public const int FormatWeightDecimalCheckSave = 10;
        public const int FormatFinishingValueDecimal = 3;
        public const int FormatRateGramValueDecimal = 2;
        public const int FormatVolumnDecimal = 2;

        public const int FormatPrQtyDecimal = 2;
        public const int FormatPoQtyDecimal = 2;
        public const int FormatStockQtyDecimal = 10;
        public const int FormatPackingFactorDecimal = 10;

        public const string BoMDefaultUnitDesc = "mm";
        public const string BoMDefaultUnit = "09";
        public const string RmCodeDefaultUnit = "09";
        public static bool OutLookOnline
        {
            get
            {
                return false;
            }
        }
        public const int SoTypeImportNomral =1;
        public static DataTable TblFactoryExport
        {
            get
            {
                
                var dt = new DataTable();
                dt.Columns.Add("FactoryCode", typeof(string));
                dt.Columns.Add("WorkingServer", typeof(string));
                dt.Rows.Add("SCAVIBH", "BIEN HOA");
                dt.Rows.Add("SCAVILD", "BAO LOC");
                dt.Rows.Add("SCAVILD1", "BAO LOC");
                dt.Rows.Add("SCAVILD2", "BAO LOC");
                dt.Rows.Add("SCAVILD3", "BAO LOC");
                dt.Rows.Add("SCAVIHUE", "HUE");
                dt.Rows.Add("SCAVILAOS", "LAOS");
                dt.Rows.Add("SCAVIDN", "DA NANG");
                return dt;
            }
        }
        public static DataTable TblWorkingServer
        {
            get
            {
                var dt = new DataTable();
                dt.Columns.Add("WorkingServer", typeof(string));
                dt.Rows.Add("BIEN HOA");
                dt.Rows.Add("BAO LOC");
                dt.Rows.Add("HUE");
                dt.Rows.Add("LAOS");
                dt.Rows.Add("DA NANG");
                return dt;
            }
        }
        public const double PaintBoMAreaConst = 1000000;
        public const String RootTableName = "RootTable";
        public const String DefaultCompRedInvoice = "01";

        public const String SysDateFormat = @"dd\/MM\/yyyy";

        public const String SysDateConvert = "dd/MM/yyyy";

        public const String SysMonthYearFormat = @"MM\/yyyy";

        public const String SysMonthYearConvert = "MM/yyyy";

        public const String SysYearMonthFormat = @"yyyy\/MM";

        public const String SysYearMonthConvert = "yyyy/MM";


        public const String SysDateHourMinuteFormat = @"dd\/MM\/yyyy hh:mm";

        public const String SysDateHourMinuteConvert = "dd/MM/yyyy hh:mm";


        public const Int32 SysBomManufacturing = 3;
        public const Int32 SysBomPurchasingVersion = 2;

        public const Int32 SysBomDevelopmentVersion = 0;


        public const Int32 SumRootNode = 100000;
        public const Int32 SumChildNode = 200000;
        private static readonly List<String> LCust = new List<string>
        {
            "BANLE", "MAITRANG", "CAOHUE", "THUYTIEN1", "PHAMTHIHA", "PHAMTHITHO", "DUCVUONG", "CENTRAL",
            "QUYNHNHU","SHOWROOM1","DONGPHONG","THUYLINH","HOAANHTHAO","VENUSCHARM","PHUONGTHA1","THITHY","XUANNHI",
            "MINHPHUONG","VANANH","PHUONGTHU","SCAVILD","BICHTHANH"
        };
        public static bool SaleOrderCheckPri(DateTime dCreateDate, string cust, string orderType)
        {
            if (orderType == "P")
                return false;
            if (DeclareSystem.SysServerSql.ToUpper().Trim() != "SCAVI00004")
                return false;
            bool checkCust = LCust.Any(p => p.ToUpper().Trim() == cust.ToUpper().Trim());
            if(checkCust)
                return false;
            bool computerHue = DeclareSystem.SysMachineName.ToUpper().Trim().Contains("SCAVIHUE");
            if (computerHue)
                return false;
            DateTime dCheck = new DateTime(2016,6,1);
            bool vCheck = ProcessGeneral.CompareDate(dCheck, dCreateDate);
            if (vCheck)
                return true;
            return false;


            // set {}
        }
    
        // public const Double DefaultFactor = 480;



       

        public const Int32 SysInputTypeAllowInput = 0;
        public const Int32 SysInputTypeSelectedSource = 1;
        public const Int32 SysInputTypeAutoIncrease = 2;
        public const Int32 SysInputTypeDateTime = 3;
        public const Int32 SysInputTypeGetField = 4;
    }
}
