using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class UCI
    {
        public static ushort ParseMove(Position position, string moveString)
        {
            MoveList moveList = new();

            Move.GenerateLegalMoves(position, ref moveList);

            for (int i = 0; i < moveList.Count; i++)
            {
                int[] moveInfo = Move.ParseMove(moveList.Moves[i]);

                string currentMoveString = BitboardUtil.IndexToCoordinate[moveInfo[0]] + BitboardUtil.IndexToCoordinate[moveInfo[1]];

                if (moveInfo[2] == 1)
                {
                    if (moveInfo[4] == 0 && moveInfo[5] == 0)
                    {
                        currentMoveString += "n";
                    }
                    else if (moveInfo[4] == 0 && moveInfo[5] == 1)
                    {
                        currentMoveString += "b";
                    }
                    else if (moveInfo[4] == 1 && moveInfo[5] == 0)
                    {
                        currentMoveString += "r";
                    }
                    else if (moveInfo[4] == 1 && moveInfo[5] == 1)
                    {
                        currentMoveString += "q";
                    }
                }

                if (moveString.Equals(currentMoveString))
                {
                    return moveList.Moves[i];
                }
            }

            return 0;
        }

        public static Position ParsePosition(string command)
        {
            Position position = new();

            command = command.Remove(0, 9);

            int indexOfMoves = command.IndexOf(" moves");

            if (command[..8].Equals("startpos"))
            {
                //Console.WriteLine("Loaded start pos!");
                position = FEN.LoadPositionFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            }
            else if (command[..3].Equals("fen"))
            {
                //FEN.LoadPositionFromFEN(subCommands[2]);
                
                string fen = command[4..];

                if (indexOfMoves >= 0)
                {
                    fen = command[4..indexOfMoves];
                }

                //Console.WriteLine("Loaded fen!");

                position = FEN.LoadPositionFromFEN(fen);
            }
            else
            {
                position = FEN.LoadPositionFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            }

            if (command.Contains(" moves "))
            {
                string[] moveStrings = command[(indexOfMoves + 7)..].Split(" ");

                for (int i = 0; i < moveStrings.Length; i++)
                {
                    //Console.WriteLine(moveStrings[i]);

                    ushort move = ParseMove(position, moveStrings[i]);

                    if (move == 0)
                    {
                        break;
                    }

                    Search.RepetitionIndex++;
                    Search.RepetitionTable[Search.RepetitionIndex] = position.HashKey;

                    Move.MakeMove(ref position, move);
                }
            }

            return position;
        }

        public static void ParseGo(Position position, string command)
        {
            int depth = -1;

            command = command[3..];

            if (command[..5].Equals("depth"))
            {
                depth = int.Parse(command[6..]);
            }
            else
            {
                depth = 10;
            }

            Search.StartSearch(position, depth);
        }

        public static void UCILoop()
        {
            Console.WriteLine("id name BadChessEngine");
            Console.WriteLine("id author Stupid Idiot");
            Console.WriteLine("uciok");

            Position position = new();

            while (true)
            {
                string input = Console.ReadLine() ?? "";

                if (input.Equals(""))
                {
                    continue;
                }

                if (input[..2].Equals("go"))
                {
                    ParseGo(position, input);
                }
                else if (input[..3].Equals("uci") && input.Length == 3)
                {
                    Console.WriteLine("id name BadChessEngine");
                    Console.WriteLine("id author Stupid Idiot");
                    Console.WriteLine("uciok");
                }
                else if (input[..4].Equals("quit"))
                {
                    break;
                }
                else if (input[..7].Equals("isready"))
                {
                    Console.WriteLine("readyok");
                }
                else if (input[..8].Equals("position"))
                {
                    position = ParsePosition(input);
                }
                else if (input[..10].Equals("ucinewgame"))
                {
                    position = ParsePosition("position startpos");
                }
            }
        }
    }
}
