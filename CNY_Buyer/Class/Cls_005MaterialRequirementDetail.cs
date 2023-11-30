using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_Buyer.Class
{
    class Cls_005MaterialRequirementDetail
    {
        public Int64 PK { get; set; }
        public Int64 MaterialRequirementPK { get; set; }
        public Int32 OrdinalNumber { get; set; }
        public String Component { get; set; }
        public Int64 StockPK { get; set; }
        public Int32 QuantityOfRequset { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime ReceiveRequestDate { get; set; }
        public Int32 QuantityInStock { get; set; }
        public String Note { get; set; }

    }
}
