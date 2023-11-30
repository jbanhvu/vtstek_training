using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_AdminSys.Class
{
    public class MasterFileListInfo
    {
        public Int64 PK { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Icon { get; set; }
        public string StrPath { get; set; }
        public string Module { get; set; }
        public string UserUpd { get; set; }
    }

    public class Ctrl_MFListSave
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string CodeFindGridView { get; set; }
        public string FieldName { get; set; }
        public Int32 TopIndexGridView { get; set; }
    }
}
