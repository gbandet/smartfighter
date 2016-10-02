using System;
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
            for (long address = startAddress; address < endAddress - length; address++) {
                if (Memory.readByte(address) == search[0]) {
                    byte[] segment = Memory.readBytes(address, length);
                    if (matchCode(search, segment)) {
                        return address;
                    }
                }
            }
            return 0;
        }

        // Match bytes arrays with some heuristic rules to ignore static adresses
        private static bool matchCode(byte[] first, byte[] second) {
            int length = first.Length;
            if (second.Length != length) {
                return false;
            }
            for (int index = 0; index < length; ++index) {
                if (first[index] != second[index]) {
                    if (index > 0 && first[index -1] == 0xE8) {
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