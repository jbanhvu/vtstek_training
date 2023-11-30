using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_BaseSys.Class
{
    public class Cls_ApprovalHistory
    {
        public Int64 PK { get; set; }
        public Int32 FunctionInApprovalPK { get; set; }
        public Int32 Level { get; set; }
        public Int64 ItemPKInFunction { get; set; }
        public String UserName { get; set; }
        public Int32 Status { get; set; }
        public DateTime ApprovedDate { get; set; }
        public String Note { get; set; }

    }
}
