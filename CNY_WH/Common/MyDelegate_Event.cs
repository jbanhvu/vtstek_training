using CNY_BaseSys.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CNY_WH.Common
{
    public delegate void SDRWizardHandle(object sender, SDRWizardEventArgs e);
    public class SDRWizardEventArgs : EventArgs
    {
        public DataTable DtGenerate { get; set; }
       
        public bool IsEvent { get; set; }
    }

    public delegate void TransferDataOnGridViewItemHandler(object sender, TransferDataOnGridViewEventArgs e);
    public class TransferDataOnGridViewEventArgs : EventArgs
    {
        public List<DataRow> ReturnRowsSelected { get; set; }
    }

}
