using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_WH.Class
{
    class Cls_002ReceiptNote
    {
        public Int64 PK { get; set; }
        public DateTime ReceiptDate { get; set; }
        public String Supplier { get; set; }
        public String Address { get; set; }
        public String Deliver { get; set; }
        public String PhoneNumber { get; set; }
        public String PONumer { get; set; }
        public DateTime PODate { get; set; }
        public Boolean NoPO { get; set; }
        public Int32 Status { get; set; }
        public String Note { get; set; }
        public String ApprovedUser { get; set; }
        public DateTime ApproveDate { get; set; }
        public String Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public String Updated_By { get; set; }
        public DateTime Updated_Date { get; set; }

    }
}
