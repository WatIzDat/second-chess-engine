using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class Random
    {
        private static uint State { get; set; } = 1804289383;

        // Get pseudo-random number with XORshift algorithm
        public static uint GetRandomUInt()
        {
            uint number = State;

            number ^= number << 13;
            number ^= number >> 17;
            number ^= number << 5;

            State = number;

            return number;
        }

        public static ulong GetRandomULong()
        {
            ulong number1 = (ulong)GetRandomUInt() & 0xFFFF;
            ulong number2 = (ulong)GetRandomUInt() & 0xFFFF;
            ulong number3 = (ulong)GetRandomUInt() & 0xFFFF;
            ulong number4 = (ulong)GetRandomUInt() & 0xFFFF;

            return number1 | (number2 << 16) | (number3 << 32) | (number4 << 48);
        }

        public static ulong GetULongFewBits()
        {
            return GetRandomULong() & GetRandomULong() & GetRandomULong();
        }
    }
}
