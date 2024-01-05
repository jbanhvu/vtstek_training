using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CNY_Buyer.Class
{
    class Cls_001PurchaseRequisition
    {
        //TotalItem  TotalQuantity TotalAmount  Status StatusCode  PurchasingStatus PurchasingStatusName

        public Int64 PK { get; set; }
        public String ProjectCode { get; set; }
        public String Requester { get; set; }
        public String Buyer { get; set; }
        public String RequestNumber { get; set; }
        public String ReceiptNumber { get; set; }
        public String PONumber { get; set; }
        public Int64 Supplier { get; set; }
        public Int32 PaymentMethod { get; set; }
        public Int32 DayOfDebt { get; set; }
        public String Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public String Updated_By { get; set; }
        public DateTime Updated_Date { get; set; }
        public String Note { get; set; }

    }
}
