using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class Perft
    {
        private static Position position;
        private static Stack<Position> stack = new Stack<Position>();
        private static ulong nodes;

        public static void Driver(int depth)
        {
            //ulong nodes = 0UL;

            if (depth == 0)
            {
                nodes++;
                return;
            }

            MoveList moveList = new();
            //int i;

            Move.GenerateLegalMoves(position, ref moveList);

            //Console.WriteLine($"Count: {moveList.Count}");

            for (int i = 0; i < moveList.Count; i++)
            {
                //Console.WriteLine($"Move: {moveList.Moves[i]}");

                //CopiedPositions.Positions.Push(position);
                Position copiedPosition = position;

                if (!Move.MakeMove(ref position, moveList.Moves[i]))
                {
                    continue;
                }

                Driver(depth - 1);

                //position = CopiedPositions.Positions.Pop();
                position = copiedPosition;
            }
        }

        public static void Run(Position position, int depth)
        {
            Perft.position = position;

            MoveList moveList = new();
            nodes = 0UL;
            //int i;

            Move.GenerateLegalMoves(Perft.position, ref moveList);

            //Console.WriteLine($"Count: {moveList.Count}");

            for (int i = 0; i < moveList.Count; i++)
            {
                //Console.WriteLine($"Move: {moveList.Moves[i]}");

                //CopiedPositions.Positions.Push(Perft.position);
                Position copiedPosition = Perft.position;

                if (!Move.MakeMove(ref Perft.position, moveList.Moves[i]))
                {
                    continue;
                }

                ulong cummulativeNodes = nodes;

                Driver(depth - 1);

                ulong oldNodes = nodes - cummulativeNodes;

                //Perft.position = CopiedPositions.Positions.Pop();
                Perft.position = copiedPosition;

                int[] moveInfo = Move.ParseMove(moveList.Moves[i]);

                Console.WriteLine($"Move: {BitboardUtil.IndexToCoordinate[moveInfo[0]]} {BitboardUtil.IndexToCoordinate[moveInfo[1]]} Nodes: {oldNodes}");
            }

            Console.WriteLine($"Nodes: {nodes}");
        }
    }
}
