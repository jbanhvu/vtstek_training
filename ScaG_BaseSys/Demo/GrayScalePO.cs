using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CNY_BaseSys.Bases;

namespace CNY_BaseSys.Demo
{
    class GrayScalePO : BaseProgressiveOperation
    {
        Bitmap _bmp;

        public GrayScalePO(Bitmap bmp)
        {
            _bmp = bmp;
            _totalSteps = bmp.Width * bmp.Height;

            MainTitle = "Performing gray scale transformation";
            SubTitle = "Please wait...";
        }

        public override void Start()
        {
            _currentStep = 0;
            OnOperationStart(EventArgs.Empty);

            for (int x = 0; x < _bmp.Width; x++)
            {
                for (int y = 0; y < _bmp.Height; y++)
                {
                    Color c = _bmp.GetPixel(x, y);
                    int m = (c.R + c.G + c.B) / 3;
                    Color g = Color.FromArgb(m, m, m);

                    _bmp.SetPixel(x, y, g);

                    _currentStep++;
                    OnOperationProgress(EventArgs.Empty);
                }
            }

            OnOperationEnd(EventArgs.Empty);
        }
    }
}
