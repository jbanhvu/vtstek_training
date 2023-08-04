using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;

namespace CNY_Report.Class
{
    public class SOFinishingBoMInfo
    {
        public DataTable SourceTable { get; set; }
        public string StrData { get; set; }
    }

    public static class SoContant
    {
        public const int FormatOrderQtyDecimal = 0;
        public const int FormatSalePriceDecimal = 4;
    }
}
