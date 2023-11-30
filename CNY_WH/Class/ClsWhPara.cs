using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CNY_WH.Class
{
    public class ClsWhPara
    {
        public string SqlWhere { get; set; }
        public int TextIndex { get; set; }
        public string STextField { get; set; }

        public delegate void StkVouReqAeWiz(object sender, StkVouReqAeArgs e);
        public class StkVouReqAeArgs : EventArgs
        {
            public DataTable DtStkVouReqWiz { get; set; }
            public bool AddNew { get; set; }
        }

        public delegate void StkVouAeWiz(object sender, StkVouAeArgs e);
        public class StkVouAeArgs : EventArgs
        {
            public DataTable DtStkVouWizHea { get; set; }
            public DataTable DtStkVouWizDet { get; set; }
            public bool PerLine { get; set; }
        }

        public delegate void FireEventTransferData(object sender, TransferDataEventArgs e);
        public class TransferDataEventArgs : EventArgs
        {
            public string ID { get; set; }
            public DataTable DtSel { get; set; }
        }

        public delegate void F001StkVouAeWiz(object sender, F001StkVouAeArgs e);
        public class F001StkVouAeArgs : EventArgs
        {
            public DataTable DtF001StkVouWiz { get; set; }
            public bool bF001AddNew { get; set; }
        }
    }
}
