using System;
using System.Collections.Generic;


namespace SmartFighter.Hooks {
    public struct HookEntry {
        public byte[] searchBytes;
        public Delegate hook;
        public HookAddress hookAddress;

        public HookEntry(Delegate hook_, HookAddress address, byte[] search) {
            hook = hook_;
            searchBytes = search;
            hookAddress = address;
        }
    }

    public static class HookDB {
        public static Dictionary<string, HookEntry> hooks = new Dictionary<string, HookEntry> {
            {"setStats", new HookEntry(
                new Hooks.DSetStats(Hooks.SetStatsHook),
                Hooks.SetStatsAddress,
                new byte[16] { 0xFF, 0x41, 0x08, 0x45, 0x33, 0xC0, 0x83, 0xFA, 0x01, 0x77, 0x19, 0x48, 0x63, 0xC2, 0xFF, 0x44 })
            },
            {"setRoundStats", new HookEntry(
                new Hooks.DSetRoundStats(Hooks.SetRoundStatsHook),
                Hooks.SetRoundStatsAddress,
                new byte[32] { 0x48, 0x89, 0x5C, 0x24, 0x18, 0x48, 0x89, 0x6C, 0x24, 0x20, 0x57, 0x41, 0x56, 0x41, 0x57, 0x48,
                               0x83, 0xEC, 0x40, 0x48, 0x8B, 0xE9, 0xC7, 0x81, 0x18, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })
            },
            {"getVersusMode", new HookEntry(
                new Hooks.DGetVersusMode(Hooks.GetVersusModeHook),
                Hooks.GetVersusModeAddress,
                new byte[7] { 0x8B, 0x81, 0x54, 0x04, 0x00, 0x00, 0xC3 })
            },
            { "getRounds", new HookEntry(
                new Hooks.DGetRounds(Hooks.GetRoundsHook),
                Hooks.GetRoundsAddress,
                new byte[8] { 0x8B, 0x81, 0x58, 0x04, 0x00, 0x00, 0xC3, 0xCC })
            },
            {"getTimer", new HookEntry(
                new Hooks.DGetTimer(Hooks.GetTimerHook),
                Hooks.GetTimerAddress,
                new byte[7] { 0x8B, 0x81, 0x5C, 0x04, 0x00, 0x00, 0xC3 })
            },
            { "selectMenu", new HookEntry(
                 new Hooks.DSelectMenu(Hooks.SelectMenuHook),
                 Hooks.SelectMenuAddress,
                 new byte[16] { 0x48, 0x89, 0x5C, 0x24, 0x08, 0x57, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xF9, 0xE8, 0xFE, 0x37 })
            },
            { "setupMatch", new HookEntry(
                 new Hooks.DSetupMatch(Hooks.SetupMatchHook),
                 Hooks.SetupMatchAddress,
                 new byte[24] { 0x48, 0x89, 0x5C, 0x24, 0x10, 0x48, 0x89, 0x74, 0x24, 0x18, 0x55, 0x57, 0x41, 0x56, 0x48, 0x8D,
                                0xAC, 0x24, 0x20, 0xFF, 0xFF, 0xFF, 0x48, 0x81 })
            },
            { "setupGameUI", new HookEntry(
                new Hooks.DSetupGameUI(Hooks.SetupGameUIHook),
                Hooks.SetupGameUIAddress,
                new byte[32] { 0x48, 0x89, 0x5C, 0x24, 0x10, 0x48, 0x89, 0x74, 0x24, 0x18, 0x48, 0x89, 0x7C, 0x24, 0x20, 0x55,
                               0x41, 0x54, 0x41, 0x55, 0x41, 0x56, 0x41, 0x57, 0x48, 0x8D, 0xAC, 0x24, 0x50, 0xEE, 0xFF, 0xFF })
            }
        };
    }
}
