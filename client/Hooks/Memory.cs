using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SmartFighter.Hooks
{
    class Memory
    {
        public static byte readByte(long address)
        {
            return readBytes(address, 1)[0];
        }

        public static byte[] readBytes(long address, int length)
        {
            byte[] bytes = new byte[length];
            Marshal.Copy(new IntPtr(address), bytes, 0, length);
            return bytes;
        }


        public static long scanBytes(byte[] search, long startAddress, long endAddress) {
            int length = search.Length;
            var similars = new List<long>();
            for (long address = startAddress; address < endAddress - length; address++) {
                if (Memory.readByte(address) == search[0]) {
                    byte[] segment = Memory.readBytes(address, length);
                    if (matchCode(search, segment)) {
                        return address;
                    } else if (matchCode(search, segment, false)) {
                        similars.Add(address);
                    }
                }
            }
            if (similars.Count == 1) {
                Communication.Interface.writeLog("Address only heuristically found.");
                return similars[0];
            } else if (similars.Count > 1) {
                Communication.Interface.writeLog("Too many similar segments found.");
            }
            return 0;
        }

        // Match bytes arrays optionally with some heuristic rules to ignore static adresses
        private static bool matchCode(byte[] first, byte[] second, bool exact = true) {
            int length = first.Length;
            if (second.Length != length) {
                return false;
            }
            for (int index = 0; index < length; ++index) {
                if (first[index] != second[index]) {
                    if (!exact && index > 0 && first[index -1] == 0xE8) {
                        // call instruction: ignore 4 bytes
                        index += 3;
                    } else {
                        return false;
                    }
                }
            }
            return true;
        }

        public static int readInt(long address) {
            return BitConverter.ToInt32(Memory.readBytes(address, 4), 0);
        }

        public static uint readUInt(long address) {
            return BitConverter.ToUInt32(Memory.readBytes(address, 4), 0);
        }
    }
}