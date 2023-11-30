using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.Class
{

    public class ColumnAttributeInfo
    {
        public Int32 ColVisibleIndex { get; set; }
        public Int32 ColNativeIndex { get; set; }
        public string FieldName { get; set; }
        public Int64 AttributePk { get; set; }
        public string Caption { get; set; }
        public DataAttType DataType { get; set; }

    }
}
