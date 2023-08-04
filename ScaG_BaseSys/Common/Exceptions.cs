using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    class OutOfSpaceExcecption : Exception
    {
        public OutOfSpaceExcecption()
            : base()
        {
        }

        public OutOfSpaceExcecption(string message)
            : base(message)
        {
        }

        public OutOfSpaceExcecption(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
