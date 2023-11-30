using System;

namespace CNY_BaseSys.Interfaces
{
    public interface IAbortable
    {
        event EventHandler Aborted;

        void Abort(); 
    }
}