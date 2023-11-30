using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_Buyer.Class
{
    class Cls_004MaterialRequirement
    {
        public Int64 PK { get; set; }
        public String ProjectCode { get; set; }
        public String ProjectName { get; set; }
        public String Requester { get; set; }
        public Int32 CriticalLevel { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int32 Status { get; set; }
        public Int32 RequestTimes { get; set; }
        public Int32 RequestType { get; set; }


    }
}
