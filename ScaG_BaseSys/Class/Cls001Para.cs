using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNY_BaseSys.Class
{
    public class Cls001Para
    {
        public delegate void FireEventTransferData(object sender, TransferDataEventArgs e);
        public class TransferDataEventArgs : EventArgs
        {
            public string ID { get; set; }
            public DataTable DtSel { get; set; }
        }
    }
}
