using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace SmartFighter.Hooks
{
    public class HookAddress {
        public long address;

        public HookAddress(long addr = 0) {
            address = addr;
        }
    }

    public static class Hooks
    {
        // SetStats Hook
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate void DSetStats(long val1, uint val2);
        public static DSetStats SetStatsDelegate;
        public static HookAddress SetStatsAddress = new HookAddress();

        public static void SetStatsHook(long address, uint result)
        {
            if (SetStatsDelegate == null) {
                SetStatsDelegate = Marshal.GetDelegateForFunctionPointer(new IntPtr(SetStatsAddress.address), typeof(DSetStats)) as DSetStats;
            }
            Communication.Interface.writeLog("Match result: {0}", result);
            Communication.Interface.setMatchResults((int)result);
            SetStatsDelegate(address, result);
        }

        // SetRoundStats Hook
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate int DSetRoundStats(long address);
        public static DSetRoundStats SetRoundStatsDelegate;
        public static HookAddress SetRoundStatsAddress = new HookAddress();

        public static int SetRoundStatsHook(long address) {
            if (SetRoundStatsDelegate == null) {
                SetRoundStatsDelegate = Marshal.GetDelegateForFunctionPointer(new IntPtr(SetRoundStatsAddress.address), typeof(DSetRoundStats)) as DSetRoundStats;
            }

            int ret = SetRoundStatsDelegate(address);
            List<int> player1 = new List<int>();
            List<int> player2 = new List<int>();
            for (int round = 0; round < 5; round++) {
                int result1 = Memory.readInt(address + 284 + 8 * round);
                int result2 = Memory.readInt(address + 288 + 8 * round);
                if (result1 < 4096 && result2 < 4096) {
                    player1.Add(result1);
                    player2.Add(result2);
                }
            }
            Communication.Interface.writeLog("Round result: {0} {1}", String.Join("/", player1.ToArray()), String.Join("/", player2.ToArray()));
            Communication.Interface.setRoundResults(player1.ToArray(), player2.ToArray());
            return ret;
        }

        // GetVersusMode Hook
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate long DGetVersusMode(long address);
        public static DGetVersusMode GetVersusModeDelegate;
        public static HookAddress GetVersusModeAddress = new HookAddress();

        public static long GetVersusModeHook(long address) {
            if (GetVersusModeDelegate == null) {
                GetVersusModeDelegate = Marshal.GetDelegateForFunctionPointer(new IntPtr(GetVersusModeAddress.address), typeof(DGetVersusMode)) as DGetVersusMode;
            }
            long ret = GetVersusModeDelegate(address);
            Communication.Interface.writeLog("Versus Mode: {0}", ret);
            Communication.Interface.setVersusMode((int)ret);
            return ret;
        }

        // GetRounds Hook
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate long DGetRounds(long address);
        public static DGetRounds GetRoundsDelegate;
        public static HookAddress GetRoundsAddress = new HookAddress();

        public static long GetRoundsHook(long address) {
            if (GetRoundsDelegate == null) {
                GetRoundsDelegate = Marshal.GetDelegateForFunctionPointer(new IntPtr(GetRoundsAddress.address), typeof(DGetRounds)) as DGetRounds;
            }
            long ret = GetRoundsDelegate(address);
            Communication.Interface.writeLog("Number of Rounds: {0}", ret );
            Communication.Interface.setRoundCount((int)ret);
            return ret;
        }

        // GetTimer Hook
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate long DGetTimer(long address);
        public static DGetTimer GetTimerDelegate;
        public static HookAddress GetTimerAddress = new HookAddress();

        public static long GetTimerHook(long address) {
            if (GetTimerDelegate == null) {
                GetTimerDelegate = Marshal.GetDelegateForFunctionPointer(new IntPtr(GetTimerAddress.address), typeof(DGetTimer)) as DGetTimer;
            }
            long ret = GetTimerDelegate(address);
            Communication.Interface.writeLog("Round timer: {0}", ret);
            Communication.Interface.setRoundTimer((int)ret);
            return ret;
        }

        // SelectMenu Hook
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate int DSelectMenu(long address);
        public static DSelectMenu SelectMenuDelegate;
        public static HookAddress SelectMenuAddress = new HookAddress();

        public static int SelectMenuHook(long address) {
            if (SelectMenuDelegate == null) {
                SelectMenuDelegate = Marshal.GetDelegateForFunctionPointer(new IntPtr(SelectMenuAddress.address), typeof(DSelectMenu)) as DSelectMenu;
            }
            int selected = Memory.readInt(address + 1084);
            Communication.Interface.writeLog("Menu selected: {0}", selected);
            Communication.Interface.setGameMode(selected);
            return SelectMenuDelegate(address);
        }

        // SetupMatch
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate int DSetupMatch(long address);
        public static DSetupMatch SetupMatchDelegate;
        public static HookAddress SetupMatchAddress = new HookAddress();

        public static int SetupMatchHook(long address) {
            if (SetupMatchDelegate == null) {
                SetupMatchDelegate = Marshal.GetDelegateForFunctionPointer(new IntPtr(SetupMatchAddress.address), typeof(DSetupMatch)) as DSetupMatch;
            }
            Communication.Interface.writeLog("Game starting...");
            Communication.Interface.setGameStart();
            return SetupMatchDelegate(address);
        }
    }
}
