using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_SI.Class
{
    class Cls_001BusinessTrip
    {
        public Int64 PK { get; set; }
        public string RequestUser { get; set; }
        public string Content { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
        public string Conclusion { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set;}
        public string Note { get; set; }

    }
}
