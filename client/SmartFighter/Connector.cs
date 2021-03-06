using EasyHook;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Automation;


namespace SmartFighter {
    public delegate void SFVFocusChangedHandler(bool hasFocus);

    class Connector {
        private string channelName = null;
        private int sfvProcessId;
        public SmartFighterServer server;

        public event SFVFocusChangedHandler SFVFocusChangedEvent;

        public Connector(GameState game) {
            server = new SmartFighterServer(game);
            RemoteHooking.IpcCreateServer<SmartFighterServer>(ref channelName, WellKnownObjectMode.Singleton, server);
        }

        public void run(object sender, DoWorkEventArgs args) {
            BackgroundWorker worker = sender as BackgroundWorker;
            args.Result = false;
            sfvProcessId = getSFVProcessId();
            if (sfvProcessId == 0) {
                return;
            }
            injectHookLibrary(sfvProcessId, "Hooks.dll");
            Automation.AddAutomationFocusChangedEventHandler(onFocusChanged);
            while (!worker.CancellationPending) {
                Thread.Sleep(500);
            }
            args.Result = true;
        }

        private int getSFVProcessId() {
            Logger.Instance.log("Searching for SFV processes...");
            Process[] processes = Process.GetProcessesByName("StreetFighterV").Where(x => x.MainWindowHandle.ToInt64() > 0).ToArray();
            if (processes.Length == 0) {
                Logger.Instance.log("No SFV process found.");
                return 0;
            }
            int processId = processes[0].Id;
            Logger.Instance.log("Process found: {0}", processId);
            return processId;
        }

        private void injectHookLibrary(int processId, string library) {
            Logger.Instance.log("Injecting library...");            
            RemoteHooking.Inject(processId, InjectionOptions.DoNotRequireStrongName, library, library, channelName);
            Logger.Instance.log("Injection finished.");
        }

        private void onFocusChanged(object src, AutomationFocusChangedEventArgs args) {
             AutomationElement element = src as AutomationElement;
             if (element != null) {
                int processId = element.Current.ProcessId;
                SFVFocusChangedEvent(processId == sfvProcessId);
             }
        }
    }
}
