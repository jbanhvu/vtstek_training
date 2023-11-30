using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_Buyer.Class
{
    class Cls_006MaterialReceipt
    {
        public Int64 PK { get; set; }
        public Int64 MaterialRequirementPK { get; set; }
        public String Receiver { get; set; }
        public String Provider { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Object ReceivedDate { get; set; }
        public Int32 Status { get; set; }
        public String Note { get; set; }

    }
}
