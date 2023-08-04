using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CNY_Main
{
    public static class SingleInstance
    {
        public static readonly int WM_SHOWFIRSTINSTANCE =
            WinApi.RegisterWindowMessage("WM_SHOWFIRSTINSTANCE|{0}", ProgramInfo.AssemblyGuid);

        private static Mutex mutex;
        const string MutexName = "Local\\{4db335a5-e1e9-4453-b000-66713ae619f2_19901111_19901006_CNY_Main}";
        public static bool Start()
        {
            bool onlyInstance = false;
          //  string mutexName = String.Format("Local\\{0}", ProgramInfo.AssemblyGuid);

            // if you want your app to be limited to a single instance
            // across ALL SESSIONS (multiple users & terminal services), then use the following line instead:
            // string mutexName = String.Format("Global\\{0}", ProgramInfo.AssemblyGuid);

            mutex = new Mutex(true, MutexName, out onlyInstance);
            return onlyInstance;
        }

        public static void ShowFirstInstance()
        {
            WinApi.PostMessage(
                (IntPtr)WinApi.HWND_BROADCAST,
                WM_SHOWFIRSTINSTANCE,
                IntPtr.Zero,
                IntPtr.Zero);
        }

        public static void Stop()
        {
            mutex.ReleaseMutex();
        }
    }
}
