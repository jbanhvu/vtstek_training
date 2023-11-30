using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_WH.Class
{
    class Cls_001Stock
    {
        public Int32 PK { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }
        public Int32 StockType { get; set; }
        public Int32 StockGroup { get; set; }
        public Int32 StockScope { get; set; }
        public Int32 Unit { get; set; }
        public Int32 MinStock { get; set; }
        public Int32 MaxStock { get; set; }
        public String Created_By { get; set; }
        public Object Created_Date { get; set; }
        public String Updated_By { get; set; }
        public Object Updated_Date { get; set; }
        public Int32 Origin { get; set; }
        public String Certificate { get; set; }
        public Int32 Manufacturer { get; set; }

    }
}
