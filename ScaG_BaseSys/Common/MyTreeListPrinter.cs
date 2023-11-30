using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.NativeBricks;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Printing;
using DevExpress.XtraTreeList.ViewInfo;

namespace CNY_BaseSys.Common
{
    public class MyTreeListOperationPrintEachNode : TreeListOperationPrintEachNode
    {
        private readonly Color _borderColor;

        public MyTreeListOperationPrintEachNode(TreeList treeList, TreeListPrinter printer, TreeListViewInfo viewInfo,
            Boolean printTree, Boolean printImages, Boolean printCheckBoxes, Color borderColor) : base(treeList,
            printer, viewInfo, printTree, printImages, printCheckBoxes)
        {
            this._borderColor = borderColor;
        }


        protected override ITextBrick CreateFooterBrick(TreeListColumn col, Rectangle rect)
        {
            ITextBrick brick = base.CreateFooterBrick(col, rect);
            brick.BorderColor = _borderColor;
            return brick;
        }

        protected override IBrick CreateCellBrick(CellInfo cell, TreeListNode node)
        {
            IVisualBrick brick = (IVisualBrick)base.CreateCellBrick(cell, node);
            brick.BorderColor = _borderColor;
            return brick;
        

        ////  IVisualBrick brick = (IVisualBrick)base.CreateCellBrick(cell, node);
        //bool isRichText = cell.Column.ColumnEdit is RepositoryItemMemoEdit;



        //    if (!isRichText)
        //    {
               

        //    if (cell.Column.VisibleIndex == 0)
        //    {
        //        if (ProcessGeneral.GetSafeString(node.GetValue(cell.Column)) == "Veneer cạnh tạp 23mm")
        //        {
        //            int height = cell.Bounds.Height;
        //            int width = cell.Bounds.Width;

        //        }
        //    }

        //    if (cell.Bounds.Height > 18)
        //    {

        //    }
        //    XETextBrick brickM = (XETextBrick) base.CreateCellBrick(cell, node);
        //    brickM.BorderColor = _borderColor;
          
        //    return brickM;

        //    // IRichTextBrick
        //    //Veneer cạnh tạp 23mm




        }
    }

    public class MyTreeListPrinter : TreeListPrinter
    {
        private readonly Color _borderColor;
        public MyTreeListPrinter(TreeList treeList, Color borderColor)
            : base(treeList)
        {
            this._borderColor = borderColor;
        }


        protected override TreeListOperationPrintEachNode CreatePrintEachNodeOperation()
        {
            return new MyTreeListOperationPrintEachNode(TreeList, this, TreeList.ViewInfo,
                TreeList.OptionsPrint.PrintTree, TreeList.OptionsPrint.PrintImages,
                TreeList.OptionsPrint.PrintCheckBoxes, _borderColor);
        }

        protected override ITextBrick CreateBandBrick(TreeListBand band, Rectangle rect)
        {
           // return base.CreateBandBrick(band, rect);

            ITextBrick brick = base.CreateBandBrick(band, rect);
            brick.BorderColor = _borderColor;
            return brick;
        }
    }

    public class TreeListBorderColorPrint : TreeList
    {
        private Color _borderColor = Color.Black;

        public Color BorderColor
        {
            get { return this._borderColor; }
            set { this._borderColor = value; }
        }
        public TreeListBorderColorPrint()
        {
          
        }
        public TreeListBorderColorPrint(Object ignore) : base(ignore)
        {

        }
        protected override TreeListPrinter CreatePrinter()
        {
            return new MyTreeListPrinter(this, _borderColor);
        }
    }

 


}
