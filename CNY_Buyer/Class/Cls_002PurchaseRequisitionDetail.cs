using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_Buyer.Class
{
    class Cls_002PurchaseRequisitionDetail
    {
        public Int64 PK { get; set; }
        public Int64 PurchaseRequisitionPK { get; set; }
        public Int64 StockPK { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
        public Int32 Tax { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime ReceiveRequestDate { get; set; }
        public DateTime RequestDeliveryDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DeliveryStatus { get; set; }
        public String Note { get; set; }

    }
}
