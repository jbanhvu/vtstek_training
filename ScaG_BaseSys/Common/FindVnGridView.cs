using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys.Common
{
   
   

  



    public class FindVnGridView : GridView
    {
        public FindVnGridView()
            : this(null)
        {
        }

        public FindVnGridView(GridControl grid)
            : base(grid)
        {
            // put your initialization code here
        }

        protected override string ViewName
        {
            get
            {
                return "FindVnGridView";
            }
        }

        protected override FindControl CreateFindPanel(object findPanelProperties)
        {
            FindControl findPanel = base.CreateFindPanel(findPanelProperties);
            OnFindPanelCreated(findPanel);
            return findPanel;
        }

        public event EventHandler<FindPanelCreatedEventArgs> FindPanelCreated;

        private void OnFindPanelCreated(FindControl findPanel)
        {
            if (FindPanelCreated != null)
                FindPanelCreated(this, new FindPanelCreatedEventArgs(findPanel));
        }
        public void FocusFindEdit()
        {
            if (FindPanel != null)
            {
                FindPanel.Focus();
                FindPanel.FocusFindEdit();
            }
        }
    }

    public class FindPanelCreatedEventArgs : EventArgs
    {
        private FindControl _FindControl;

        public FindControl FindControl
        {
            get
            {
                return _FindControl;
            }
            set
            {
                _FindControl = value;
            }
        }

        public FindPanelCreatedEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the FindPanelCreatedEventArgs class.
        /// </summary>
        /// <param name="findControl"></param>
        public FindPanelCreatedEventArgs(FindControl findControl)
        {
            _FindControl = findControl;
        }
    }


}
