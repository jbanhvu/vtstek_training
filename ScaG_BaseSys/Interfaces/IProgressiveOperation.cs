using System;

namespace CNY_BaseSys.Interfaces
{
    public interface IProgressiveOperation
    {
        event EventHandler OperationStart;
        event EventHandler OperationProgress;
        event EventHandler OperationEnd;

        string MainTitle { get; set; }

        string SubTitle { get; set; }

        int CurrentProgress { get; }
        int TotalSteps { get; }
        int CurrentStep { get; }

        void Start(); 
    }
}