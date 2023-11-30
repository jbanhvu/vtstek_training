using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_BaseSys.Common
{
    public class Cls001Constant
    {
        public const int StatusCancel = 0;
        public const int StatusNormal = 1;
        public const int StatusConfirm = 2;
        public const int StatusRelease = 3;
        public const int StatusApprove = 4;
        public const int StatusReject = 5;

        public const int CnyStatusCancel = 0;
        public const int CnyStatusInput = 1;
        public const int CnyStatusSubmit = 2;
        public const int CnyStatusConfirm = 3;
        public const int CnyStatusApprove = 4;
        public const int CnyStatusReject = 5;

        public const string StateAdd = "New";
        public const string StateEdit = "Edit";

        public const string StrSysCodeNo = "N";
        public const string StrSysCodeId = "I";

        public const string StrSrcPriceType = "MSC";
        public const string StrSrcColorRange = "MCR";
        public const string StrSrcMinType = "MMC";
        public const string StrSrcPriceChageReason = "MPR";
        public const string StrSrcPackingCode = "MPC";

        public const string StrSrcOowlCode = "SWL";
        public const string StrSrcServiceCode = "SSD";
        public const string StrSrcVatCode = "SVC";
        public const string StrSrcStatusCode = "SF";
        public const string StrSrcAppSheType = "SS";
    }
}
