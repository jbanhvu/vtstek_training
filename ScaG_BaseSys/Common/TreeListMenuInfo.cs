using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraTreeList.Columns;

namespace CNY_BaseSys.Common
{
    public class TreeListMenuInfo
    {
        public FixedStyle Style;
        public TreeListColumn Column;
        public bool Checked;

        public TreeListMenuInfo(TreeListColumn column, FixedStyle style, bool check)
        {
            this.Column = column;
            this.Style = style;
            this.Checked = check;
        }
    }
}
