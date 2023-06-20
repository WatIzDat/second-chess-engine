using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class TranspositionTable
    {
        //public static int? LookupFailed { get; } = null;

        public static int Exact { get; } = 0;
        public static int Alpha { get; } = 1;
        public static int Beta { get; } = 2;

        public static Element[] Elements { get; set; } = new Element[64000];
        public static ulong Size { get; set; } = 64000;

        public static int? ProbeHash(Position position, int alpha, int beta, int depth)
        {
            Element element = Elements[position.HashKey % Size];
            //Console.WriteLine(element.Key);
            //Console.WriteLine(element.Depth);
            //Console.WriteLine(element.Flags);
            //Console.WriteLine(element.Value);
            //Console.WriteLine(element.Move);

            //Console.WriteLine($"Element key: {element.Key}");
            //Console.WriteLine($"Position key:{position.HashKey}");

            if (element.Key == position.HashKey)
            {
                if (element.Depth >= depth)
                {
                    if (element.Flags == Exact)
                    {
                        return element.Value;
                    }

                    if (element.Flags == Alpha && element.Value <= alpha)
                    {
                        return alpha;
                    }

                    if (element.Flags == Beta && element.Value >= beta)
                    {
                        return beta;
                    }
                }
            }

            return null;
        }

        public static void RecordHash(Position position, int depth, int value, int flags, ushort move)
        {
            //Element element = Elements[position.HashKey % Size];

            //Console.WriteLine($"Position key:{position.HashKey}");

            Elements[position.HashKey % Size].Key = position.HashKey;
            Elements[position.HashKey % Size].Depth = depth;
            Elements[position.HashKey % Size].Value = value;
            Elements[position.HashKey % Size].Flags = flags;
            Elements[position.HashKey % Size].Move = move;
        }

        public struct Element
        {
            //public ulong key;
            //public int depth;
            //public int flags;
            //public int value;
            //public ushort bestMove;
            public ulong Key { get; set; }
            public int Depth { get; set; }
            public int Flags { get; set; }
            public int Value { get; set; }
            public ushort Move { get; set; }
        }
    }
}
