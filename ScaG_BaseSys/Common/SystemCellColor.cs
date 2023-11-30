using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public static class SystemCellColor
    {
        public static Color BackColorShowEditor { get { return Color.White; } }
        public static Color BackColorReadonly { get { return Color.AntiqueWhite; } }

        public static Color BackColor2Readonly { get { return Color.Azure; } }

        public static Color BackColor2ShowEditor { get { return Color.White; } }



        public static Color BackColorError { get { return Color.Red; } }
        public static Color BackColorSelectedRow { get { return Color.FromArgb(169, 249, 108); } }
        public static Color BackColorSelectedRowDetail { get { return Color.FromArgb(198, 182, 244); } }
        public static Color BackColorDefaultRowDetail { get { return Color.FromArgb(235, 255, 218); } }
        public static Color BackColorMasterRowExpanded { get { return Color.FromArgb(243, 205, 254); } }
        public static Color BackColorMasterRowCollapsed { get { return Color.FromArgb(252, 249, 182); } }
        public static Color BackColorMasterRowEmptyDetail { get { return Color.FromArgb(190, 221, 250); } }


        public static Color BackColorCellFocused { get { return Color.Aquamarine; } }
        public static Color BackColorCellSelected { get { return Color.LightBlue; } }


    }
}
