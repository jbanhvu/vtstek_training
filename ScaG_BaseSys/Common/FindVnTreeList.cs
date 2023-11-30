using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Utils;
using DevExpress.XtraTreeList;

namespace CNY_BaseSys.Common
{

    public delegate void EndFilteringEventHandler(object sender, EventArgs e);
    public delegate void BeginFilteringEventHandler(object sender, EventArgs e);
    public delegate void FindPanelVisibleChangedEventHandler(object sender, EventArgs e);

    public class FindVnTreeList : TreeList
    {

        public event FindPanelVisibleChangedEventHandler OnFindPanelVisibleChanged = null;
        public event EndFilteringEventHandler EndFiltering;
        public event BeginFilteringEventHandler BeginFiltering;
        public FindVnTreeList()
        {
            //  FindPanel.FindButton.Click += FindButton_Click;


        }

        protected override IFindPanel CreateFindPanelCore()
        {
            IFindPanel info = base.CreateFindPanelCore();
            OnFindPanelVisibleChanged?.Invoke(this, EventArgs.Empty);
            return info;
        }






        public override void FilterNodes()
        {
            this.RaiseBeginFilteringEvent();
            base.FilterNodes();
            this.RaiseEndFilteringEvent();
        }
        protected virtual void RaiseBeginFilteringEvent()
        {
            BeginFiltering?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void RaiseEndFilteringEvent()
        {
            EndFiltering?.Invoke(this, EventArgs.Empty);
        }
    }










}