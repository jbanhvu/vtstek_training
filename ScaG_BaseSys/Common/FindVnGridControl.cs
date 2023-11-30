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
    
    public class FindVnGridControl : GridControl
    {
        protected override BaseView CreateDefaultView()
        {
            return CreateView("FindVnGridView");
            
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new FindVnGridViewInfoRegistrator());
        }
    }
}
