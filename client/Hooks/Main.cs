using EasyHook;
using SmartFighter.Common;
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;


namespace SmartFighter.Hooks {
    public class Main : IEntryPoint {
        public Main(RemoteHooking.IContext inContext, string inChannelName) {
            try {
                Communication.Interface = RemoteHooking.IpcConnectClient<SmartFighterInterface>(inChannelName);
                Communication.Interface.writeLog("Dll injected.");
            } catch (Exception ex) {
                Logger.log(ex.ToString());
            }
        }

        public void Run(RemoteHooking.IContext inContext, string inChannelName) {
            try {
                Communication.Interface.writeLog("Injected code is starting...");
                Injector injector = new Injector(Process.GetCurrentProcess(), this);

                Communication.Interface.writeLog("Setting up hooks.");
                foreach (KeyValuePair<string, HookEntry> pair in HookDB.hooks) {
                    string name = pair.Key;
                    HookEntry entry = pair.Value;
                    injector.registerHook(name, entry.hook, entry.searchBytes, entry.hookAddress);
                }
                Communication.Interface.writeLog("Hook done");

                while (Communication.Interface.Active) {
                    Thread.Sleep(1000);
                }
            } catch (Exception ex) {
                Logger.log(ex.ToString());
            }
        }
    }
}
