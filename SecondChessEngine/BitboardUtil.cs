using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SecondChessEngine
{
    public static class BitboardUtil
    {
        public static string[] IndexToCoordinate { get; set; } = new string[64]
        {
            "a1", "b1", "c1", "d1", "e1", "f1", "g1", "h1",
            "a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2",
            "a3", "b3", "c3", "d3", "e3", "f3", "g3", "h3",
            "a4", "b4", "c4", "d4", "e4", "f4", "g4", "h4",
            "a5", "b5", "c5", "d5", "e5", "f5", "g5", "h5",
            "a6", "b6", "c6", "d6", "e6", "f6", "g6", "h6",
            "a7", "b7", "c7", "d7", "e7", "f7", "g7", "h7",
            "a8", "b8", "c8", "d8", "e8", "f8", "g8", "h8"
        };

        public static ulong SetBit(this ulong bitboard, int location)
        {
            bitboard |= 1UL << location;

            return bitboard;
        }

        public static byte GetBit(this ulong bitboard, int location)
        {
            return (byte)((bitboard >> location) & 1U);
        }

        public static ulong ClearBit(this ulong bitboard, int location)
        {
            bitboard &= ~(1UL << location);

            return bitboard;
        }

        public static ulong IsolateLS1B(this ulong bitboard)
        {
            return bitboard & (0 - bitboard);
        }

        public static int GetLS1BIndex(ulong ls1b)
        {
            return BitOperations.PopCount(ls1b - 1);
        }
    }
}
