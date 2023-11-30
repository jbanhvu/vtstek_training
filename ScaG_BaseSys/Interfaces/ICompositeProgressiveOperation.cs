using System;

namespace CNY_BaseSys.Interfaces
{
    public interface ICompositeProgressiveOperation :
      IProgressiveOperation
    {
        IProgressiveOperation CurrentOperation { get; }

        event EventHandler NewOperation;
    }
}