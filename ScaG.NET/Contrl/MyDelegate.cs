using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_Main.Contrl
{
    public delegate void GetNodeInfoHandler(object sender, GetNodeInfoEventArgs e);
    public class GetNodeInfoEventArgs : EventArgs
    {
        public string FormName { get; set; }
        public string FormCode { get; set; }
        public string ClassModule { get; set; }
    }
}
