using System.Drawing;

namespace CNY_BaseSys.Common
{
    public static class GridCellColor
    {
        public static Color BackColorShowEditor {get { return Color.BlanchedAlmond; } }
        public static Color BackColor2ShowEditor { get { return Color.Azure; } }
        public static Color BackColorError { get { return Color.Red; } }
        public static Color BackColorSelectedRow { get { return Color.FromArgb(169, 249, 108); } }
        public static Color BackColorSelectedRowDetail { get { return Color.FromArgb(198, 182, 244); } }
        public static Color BackColorDefaultRowDetail { get { return Color.FromArgb(235, 255, 218); } }
        public static Color BackColorMasterRowExpanded { get { return Color.FromArgb(243, 205, 254); } }
        public static Color BackColorMasterRowCollapsed { get { return Color.FromArgb(252, 249, 182); } }
        public static Color BackColorMasterRowEmptyDetail { get { return Color.FromArgb(190, 221, 250); } }
        public static Color BackColorReadonly { get { return Color.Gainsboro; } }
   
    }
}
