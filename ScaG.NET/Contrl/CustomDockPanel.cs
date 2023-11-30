using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;
using System.Drawing;

namespace CNY_Main.Contrl
{
    public class CustomDockPanel : DockPanel
    {
        public void SetWidthInternal(int width)
        {
            this.DockLayout.OriginalSize = new Size(width, this.DockLayout.OriginalSize.Height);
        }
    }
}
