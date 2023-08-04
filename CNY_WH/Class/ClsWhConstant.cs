using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_WH.Class
{
    public class ClsWhConstant
    {
        public const string ConWhStatus = "STA";

        public const int StatusCancel = 0;
        public const int StatusNormal = 1;
        public const int StatusConfirm = 2;
        public const int StatusRelease = 3;
        public const int StatusApprove = 4;
        public const int StatusReject = 5;

        public const string StrStatusCancel = "CAN";
        public const string StrStatusComplete = "COM";
        public const string StrStatusInprocess = "INP";
        public const string StrStatusNotStart = "NOS";
        public const string StrStatusReject = "REJ";
        public const string StrStatusReplace = "REP";
        public const string StrStatusRevise = "REV";
        public const string StrStatusApprove = "APP";
        public const string StrStatusPending = "PEN";

        public const string ConWhStkVouReq = "SVR";

        public const string ConMoPrePro = "M01";
        public const string ConMoNormal = "M02";
        public const string ConMorevoke = "M03";
        public const string ConMoSupmen = "M04";

        public const string ConStkRmRecPo = "1101";
        public const string ConStkRmRecRe = "1201";
        public const string ConStkFgRecSo = "1301";
        public const string ConStkFgRecRe = "1401";

        public const string ConMenuCodeStkRmRecPo = "MN00308";
        public const string ConMenuCodeStkRmRecRe = "MN00309";
        public const string ConMenuCodeStkFgRecSo = "MN00253";
        public const string ConMenuCodeStkFgRecRe = "MN00252";

        public const string ConStkRmIssMo = "2101";
        public const string ConStkRmIssRe = "2201";
        public const string ConStkFgIssSo = "2301";
        public const string ConStkFgIssRe = "2401";

        public const string ConMenuCodeStkRmIssMo = "MN00312";
        public const string ConMenuCodeStkRmIssRe = "MN00313";
        public const string ConMenuCodeStkFgIssSo = "MN00314";
        public const string ConMenuCodeStkFgIssRe = "MN00315";

        public const string ConStkRmTransRe = "3101";
        public const string ConStkRmTransNoRe = "3201";
        public const string ConStkFgTransRe = "3301";
        public const string ConStkFgTransNoRe = "3401";

        public const string ConStkRmTakingRe = "4101";
        public const string ConStkRmTakingReNo = "4201";
        public const string ConStkFgTakingRe = "4301";
        public const string ConStkFgTakingReNo = "4401";

        public const string StkRec = "1000";
        public const string StkIss = "2000";
        public const string StkTransfer = "3000";

        public const string StrStkRec = "R";
        public const string StrStkIss = "I";
        public const string StrStkTransfer = "T";

        public const string ConBatchSys = "S";
        public const string ConBatchTran = "T";
    }
}
