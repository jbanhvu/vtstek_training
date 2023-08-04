using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;

namespace CNY_BaseSys.Common
{
  
    public class FindVnGridViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName
        {
            get { return "FindVnGridView"; }
        }

        public override BaseView CreateView(GridControl grid)
        {
            return new FindVnGridView(grid);
        }
    }
}
