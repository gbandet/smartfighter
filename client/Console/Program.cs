using System;
using System.Diagnostics;
using System.Linq;
using EasyHook;
using System.Runtime.Remoting;
using System.Threading;


namespace SmartFighter {
    class Program {
        static String channelName = null;

        static void Main(string[] args) {
            int processId = getSFVProcessId();
            if (processId == 0) {
                return;
            }
            injectHooksLibrary(processId, "Hooks.dll");
            while (true) {
                Thread.Sleep(10000);
            }
        }

        private static int getSFVProcessId() {
            Console.WriteLine("Searching for SFV processes...");
            Process[] processes = Process.GetProcessesByName("StreetFighterV").Where(x => x.MainWindowHandle.ToInt64() > 0).ToArray();

            if (processes.Length == 0) {
                Console.WriteLine("No SFV process found.");
                return 0;
            }
            int processId = processes[0].Id;
            Console.WriteLine("Process found: {0}", processId);
            return processId;
        }

        private static void injectHooksLibrary(int processId, string library) {
            Console.WriteLine("Injecting library...");
            RemoteHooking.IpcCreateServer<SmartFighterServer>(ref channelName, WellKnownObjectMode.Singleton);
            RemoteHooking.Inject(processId, InjectionOptions.DoNotRequireStrongName, library, library, channelName);
            Console.WriteLine("Injection finished.");
        }
    }
}