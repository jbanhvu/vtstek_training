using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;

namespace CNY_BaseSys.Common
{
    public class FocusRowRectangleGridControl : GridControl
    {
        protected override BaseView CreateDefaultView()
        {
            return CreateView("FocusRowRectangleGridView");
        }
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new FocusRowRectangleGridViewInfoRegistrator());
        }
    }

    public class FocusRowRectangleGridView : GridView
    {
        public FocusRowRectangleGridView() : this(null) { }
        public FocusRowRectangleGridView(GridControl grid) : base(grid) { }
        protected override string ViewName { get { return "FocusRowRectangleGridView"; } }
    }

    public class FocusRowRectangleGridViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName { get { return "FocusRowRectangleGridView"; } }
        public override BaseView CreateView(GridControl grid) { return new FocusRowRectangleGridView(grid as GridControl); }
        public override BaseViewInfo CreateViewInfo(BaseView view) { return new FocusRowRectangleGridViewInfo(view as FocusRowRectangleGridView); }
        public override BaseViewHandler CreateHandler(BaseView view) { return new FocusRowRectangleGridHandler(view as FocusRowRectangleGridView); }
        public override BaseViewPainter CreatePainter(BaseView view) { return new FocusRowRectangleGridPainter(view as DevExpress.XtraGrid.Views.Grid.GridView); }
    }

    public class FocusRowRectangleGridViewInfo : GridViewInfo
    {
        public FocusRowRectangleGridViewInfo(GridView gridView) : base(gridView) { }
    }

    public class FocusRowRectangleGridHandler : DevExpress.XtraGrid.Views.Grid.Handler.GridHandler
    {
        public FocusRowRectangleGridHandler(GridView gridView) : base(gridView) { }
    }

    public class FocusRowRectangleGridPainter : GridPainter
    {
        public FocusRowRectangleGridPainter(GridView view) : base(view) { }

        protected override void DrawRowFocus(GridViewDrawArgs e, GridRowInfo ri)
        {
            base.DrawRowFocus(e, ri);
            if (CanDrawRowFocus(ri))
                e.Cache.DrawRectangle(e.Cache.GetPen(Color.Black, 3), new Rectangle(ri.DataBounds.X, ri.DataBounds.Y, ri.DataBounds.Width, ri.DataBounds.Height));
        }

        protected override bool CanDrawRowFocus(GridRowInfo ri)
        {
            //return base.CanDrawRowFocus(ri);
            return ri.RowHandle == View.FocusedRowHandle;
            //return ((ri.RowState & GridRowCellState.Focused) == GridRowCellState.Focused && (View.FocusRectStyle == DrawFocusRectStyle.RowFocus || (ri.IsGroupRow && View.FocusRectStyle != DrawFocusRectStyle.None)));
        }

    }
}
