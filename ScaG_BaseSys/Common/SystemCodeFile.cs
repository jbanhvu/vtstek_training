using System;

namespace CNY_BaseSys.Common
{
    public static class SystemCodeFile
    {
        
        //Sub-Contractor
        public const String PL_SUB_CONTRACTOR = "SUFG";
        public const String PL_SUB_INNER = "IN";
        //supplier Listing
        //public const String  PL_SUPPLIER_CODE = "'R','S','IN'"
        //public const String  PL_SUPPLIER = "'RF','RD','MI'"
        public const String PL_SUPPLIER =
            "'RF','RD','MI','OWLD','OWLF','WFS2','WFS8','WFSP','WDS2','WDS8','WDSP','SURM','SUFG'";

        public const Int32 PL_PAYMENT_TERM = 0;
        public const Int32 PL_DELIVERY_TERM = 1;
        public const Int32 PL_DELIVERY_METHOD = 2;
        //public const String  PL_SUPPLIER_FOR = "RF"
        //public const String  PL_SUPPLIER_DOM = "RD"
        //Code File
        public const String CODE_FILE_COLOR = "YA";
        public const String CODE_FILE_CUSTOMER = "YB";
        public const String CODE_FILE_COMPOSITION = "YE";
        public const String CODE_FILE_MATERIAL_SIZE = "YD";
        public const String CODE_FILE_MATERIAL_COLOR = "YY";  // 'wrong code file
        public const String CODE_FILE_MATERIAL_WIDTH = "YF";
        public const String CODE_FILE_MATERIAL_WEIGHT = "YG";
        public const String CODE_FILE_SEASON = "IU";
        public const String CODE_FILE_STORY = "IQ";
        public const String CODE_FILE_CASH_OFFICE = "AX";
        public const String CODE_FILE_COUNTY_CODE = "BM";
        public const String CODE_FILE_PRODUCT_LINE = "G";// 'Select from GL03 Where GL03001='G'
        public const String CODE_FILE_PRODUCT_GROUP = "IB";
        public const String CODE_FILE_PRODUCT_GROUP_EXT = "IK";
        public const String CODE_FILE_STOCK_ITEM_STATUS_CODE = "IC";
        public const String CODE_FILE_STOCK_ITEM_QUERY_QUEUE = "IA";
        public const String CODE_FILE_STOCK_ITEM_PRODUCT_CATEGORY = "II";
        public const String CODE_FILE_USER_DEFINE_10 = "IZ";//'Basic/Fashion
        public const String CODE_FILE_SUPPLIER_CATEGORY = "CB";

        public const String CODE_FILE_COST_TRANSPORT = "CT";
        public const String CODE_FILE_COST_LOGISTICS = "CL";
        public const String CODE_FILE_COST_OTHERS = "CO";
        public const String CODE_FILE_DEL_TERM_DEFINATION = "DD";
        public const String CODE_FILE_KIND_GARMENT = "GK";
        public const String CODE_FILE_LOGIS_FEE = "LF";
        public const String CODE_FILE_SERVICES = "SV";     //      ' Services
        public const String CODE_FILE_COMMISSION = "PC";  //        ' Percent Commission

        //'Stock Item
        public const String STOCK_ITEM_PRODUCT = "F";
        //Stock Document
        public const String STOCK_DOC_TYPE = "SD";
        public const String STOCK_RECEIVED = "01";
        public const String STOCK_ISSUED = "02";
        public const String STOCK_TRANSFERED = "03";
        //Accouting Report
        public const String REPORT_ACC_TYPE = "RA";
        public const String REPORT_ACC_AP = "RAP";
        public const String REPORT_ACC_AR = "RAR";
        public const String REPORT_ACC_ASSET = "RAS";
        public const String REPORT_ACC_EXP = "RAEX";
        public const String REPORT_ACC_GEN = "RAG";

        //Logistic Report
        public const String REPORT_LOGISTIC = "RLR";
        //'Warehouse reports
        public const String REPORT_WH_TYPE = "RW";
        //'MS Report
        public const String REPORT_MS_TYPE = "RS";
        //'MPS Report
        public const String REPORT_MPS_TYPE = "RMPS";
        //Sourcing File-General Condition
        public const String SOURCING_GENERAL_CONDITION = "SG";
        //'Production Report
        public const String REPORT_PRODUCTION = "RPR";

        public const String REPORT_OP = "ROP";

        //PO
        public const String REPORT_PO_TYPE = "RPO";
        public const String CODE_FILE_PO_SERVICE = "OC";
        public const String PRI_TYPE_NORMAL = "RT01";
        public const String CODE_FILE_RATE_LISTING = "RL";     //   ' Rate declared by accounting dept. USD
        public const String CODE_FILE_RATE_INVOICE = "RV";      //  ' Rate declared by accounting dept. VND

        public const String REPORT_SAMPLES = "LR";


        //P/R Sample
        public const String PRType = "00,01";
        public const String TypeSample = "02";
        //MO
        public const String MO_STA_NORMAL = "T01";
        public const String MO_STA_REVOKE = "T02";
        public const String MO_STA_SUPPLEMENT = "T03";
        public const String MO_STA_PRE_PROD = "T04";
        public const String MO_STA_SAMPLES = "T05";
        public const String MO_STA_SERVICES = "T06";
        public const String MO_STA_SUBORDER = "T07";
        public const String MO_STA_OTHER_BRD = "T08";

        //----------------------------------------------------------------------
        public const String STATUS_TYPE = "STA";
        public const String PRODUCTION_REASON_CODE = "PRC";
        public const String PRODUCTION_PERIOD_TIME = "PPT";

        public const String CODE_FILE_USER_DEFINE_9 = "IY";//'Seasons
        public const String CODE_FILE_USER_DEFINE_5 = "IU";//'Seasons
        public const String CODE_FILE_USER_DEFINE_1 = "IQ";// 'story



        public const Int32 GRP_TECKNICAL = 10;
        public const Int32 GRP_UC = 11;
        public const Int32 GRP_PACKAGING = 12;
        public const Int32 GRP_CONTROL = 13;

        public const String FG_REQ_RETAIL = "F01";
        public const String FG_REQ_OTHERS = "F02";
        public const String CCP_REF = "CCP01";
        public const String CCP_PRI = "CCP02";
        //-------Sourcing File------------------
        public const String SUPPLIER_STATUS = "STASP";
        public const String SUPPLIER_STATUS_LEVEL = "LEVSP";
        //Accounting
        //Dimemsion
        public const String CODE_FILE_DM_COST_CENTERS = "B";
        public const String CODE_FILE_DM_EXPENSIVE_TYPE = "C";
        public const String CODE_FILE_DM_REFERENCES = "D";
        public const String CODE_FILE_DM_DEBTOR_CREDITOR = "E";
        public const String CODE_FILE_DM_EMPLOYEE = "F";
        public const String CODE_FILE_DM_PRODUCT_LINE = "G";
        public const String CODE_FILE_DM_SALES_ORDER = "H";




    }
}
