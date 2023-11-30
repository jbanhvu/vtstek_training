using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_Buyer.Class
{
    class Cls_007MaterialReceiptDetail
    {
        public Int64 PK { get; set; }
        public Int64 MaterialReceiptPK { get; set; }
        public Int64 StockPK { get; set; }
        public Int32 Quantity { get; set; }
        public String Note { get; set; }

    }
}
