using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CNY_BaseSys.Bases;

namespace CNY_BaseSys.Demo
{
    class NegativePO : BaseProgressiveOperation
    {
        Bitmap _bmp;

        public NegativePO(Bitmap bmp)
        {
            _bmp = bmp;
            _totalSteps = bmp.Width * bmp.Height;

            MainTitle = "Performing negative transformation";
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
                    Color n = Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
                    _bmp.SetPixel(x, y, n);

                    _currentStep++;
                    OnOperationProgress(EventArgs.Empty);
                }
            }

            OnOperationEnd(EventArgs.Empty);
        }
    }
}
