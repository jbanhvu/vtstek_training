using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Class
{
    public class PasteDataFromFile
    {
        public Int32 RowIndex { get; set; }
        public List<ColInfoFromFile> LstColPaste { get; set; }
    }

    public class ColInfoFromFile
    {
        public Int32 ColIndex { get; set; }
        public String ColData { get; set; }
    }
}
