using EasyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace SmartFighter.Hooks {
    public struct InjectedHook {
        public long address;
        public LocalHook hook;
    }

    public class Injector {
        private long searchStartAddress;
        private long searchEndAddress;
        private object callback;
        private Dictionary<string, InjectedHook> registry = new Dictionary<string, InjectedHook>();

        public Injector(Process process, object obj) {
            searchStartAddress = process.MainModule.BaseAddress.ToInt64();
            searchEndAddress = searchStartAddress + process.MainModule.ModuleMemorySize;
            callback = obj;
        }

        public long searchHook(byte[] searchedBytes) {
            return Memory.scanBytes(searchedBytes, searchStartAddress, searchEndAddress);
        }

        public LocalHook createHook(long address, Delegate hook, HookAddress hookAddress) {
            LocalHook localHook = LocalHook.Create(new IntPtr(address), hook, callback);
            localHook.ThreadACL.SetExclusiveACL(new int[1]);
            hookAddress.address = address;
            return localHook;
        }

        public bool registerHook(string name, Delegate hook, byte[] searchedBytes, HookAddress hookAddress) {
            InjectedHook injected;
            injected.address = searchHook(searchedBytes);
            if (injected.address == 0) {
                Communication.Interface.writeLog("Hook '{0}' not found.", name);
                return false;
            }
            Communication.Interface.writeLog("Hook '{0}' found at {1}", name, injected.address);
            injected.hook = createHook(injected.address, hook, hookAddress);
            registry[name] = injected;
            return true;
        }

        public InjectedHook getHook(string name) {
            return registry[name];
        }
    }
}
