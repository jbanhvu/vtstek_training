using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;

namespace CNY_BaseSys.Common
{
    public class PopUpFiTreeList : TreeList
    {

        public void FocusFindEditor()
        {
            if (FindPanel != null)
            {
                FindPanel.FocusFindEditor();

            }
        }


        public void SetFocusAfterFindText()
        {
            SendKeys.Send("{RIGHT}");
        }
    }
}
