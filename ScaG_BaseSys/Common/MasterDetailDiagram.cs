using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  System.Data;

namespace CNY_BaseSys.Common
{
    public class MasterDetailDiagram
    {
        public DataTable Dt { get; set; }
        public string TableName { get; set; }
        public Int32 Level { get; set; }
        public string RelationColName { get; set; }
        public  string ParentTable { get; set; }
        public Int32 Index { get; set; }
    }
}
