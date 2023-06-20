using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class Search
    {
        public static Position position;
        //private static int max = Int32.MinValue;

        public static int[,] MVV_LVA { get; set; } = new int[12, 12]
        {
            {
                105,
                205,
                305,
                405,
                505,
                605,
                105,
                205,
                305,
                405,
                505,
                605
            },
            {
                104,
                204,
                304,
                404,
                504,
                604,
                104,
                204,
                304,
                404,
                504,
                604
            },
            {
                103,
                203,
                303,
                403,
                503,
                603,
                103,
                203,
                303,
                403,
                503,
                603
            },
            {
                102,
                202,
                302,
                402,
                502,
                602,
                102,
                202,
                302,
                402,
                502,
                602
            },
            {
                101,
                201,
                301,
                401,
                501,
                601,
                101,
                201,
                301,
                401,
                501,
                601
            },
            {
                100,
                200,
                300,
                400,
                500,
                600,
                100,
                200,
                300,
                400,
                500,
                600
            },
            {
                105,
                205,
                305,
                405,
                505,
                605,
                105,
                205,
                305,
                405,
                505,
                605
            },
            {
                104,
                204,
                304,
                404,
                504,
                604,
                104,
                204,
                304,
                404,
                504,
                604
            },
            {
                103,
                203,
                303,
                403,
                503,
                603,
                103,
                203,
                303,
                403,
                503,
                603
            },
            {
                102,
                202,
                302,
                402,
                502,
                602,
                102,
                202,
                302,
                402,
                502,
                602
            },
            {
                101,
                201,
                301,
                401,
                501,
                601,
                101,
                201,
                301,
                401,
                501,
                601
            },
            {
                100,
                200,
                300,
                400,
                500,
                600,
                100,
                200,
                300,
                400,
                500,
                600
            },
        };

        private static int Ply { get; set; }
        private static ushort[,] KillerMoves { get; set; } = new ushort[2, 64];

        //private static int[,,] HistoryMoves { get; set; } = new int[2, 64, 64];
        public static int RepetitionIndex { get; set; }
        public static ulong[] RepetitionTable = new ulong[1000];
        public static int[] PVLength { get; set; } = new int[64];
        public static ushort[,] PVTable { get; set; } = new ushort[64, 64];

        private static int FullDepthMoves { get; } = 4;
        private static int ReductionLimit { get; } = 3;

        private static bool IsRepetition()
        {
            for (int i = 0; i < RepetitionIndex; i++)
            {
                if (RepetitionTable[i] == position.HashKey)
                {
                    return true;
                }
            }

            return false;
        }

        public static int Negamax(int alpha, int beta, int depth)
        {
            bool futilityPrune = false;

            int score = int.MinValue;

            PVLength[Ply] = Ply;

            if (Ply > 0 && IsRepetition() || position.HalfmoveClock >= 100)
            {
                //Console.WriteLine("Draw!");
                return 0;
            }

            bool isPVNode = beta - alpha > 1;

            if (!isPVNode)
            {
                int? ttVal = TranspositionTable.ProbeHash(position, alpha, beta, depth);

                if (ttVal.HasValue)
                {
                    //Console.WriteLine(ttVal.Value);
                    return ttVal.Value;
                }
            }

            //bool foundPV = false;

            if (depth == 0) return Quiescence(alpha, beta);

            int hashFlag = TranspositionTable.Alpha;
            ushort bestMove = 0;

            bool inCheck = Move.IsSquareAttacked(position, position.WhiteToMove ? BitboardUtil.GetLS1BIndex(position.WhiteKing.IsolateLS1B()) : BitboardUtil.GetLS1BIndex(position.BlackKing.IsolateLS1B()), !position.WhiteToMove);
            //Console.WriteLine(inCheck);

            if (depth >= 3 && !inCheck && Ply > 0)
            {
                Position copiedPosition = position;

                Ply++;

                RepetitionIndex++;
                RepetitionTable[RepetitionIndex] = position.HashKey;

                if (position.WhiteEnPassantTargetSquare != 0)
                {
                    int square = BitboardUtil.GetLS1BIndex(position.WhiteEnPassantTargetSquare.IsolateLS1B());

                    position.HashKey ^= Zobrist.EnPassantFiles[square % 8];
                }
                else if (position.BlackEnPassantTargetSquare != 0)
                {
                    int square = BitboardUtil.GetLS1BIndex(position.BlackEnPassantTargetSquare.IsolateLS1B());

                    position.HashKey ^= Zobrist.EnPassantFiles[square % 8];
                }

                position.WhiteEnPassantTargetSquare = 0;
                position.BlackEnPassantTargetSquare = 0;

                position.WhiteToMove = position.WhiteToMove != true;

                position.HashKey ^= Zobrist.Side;

                score = -Negamax(-beta, -beta + 1, depth - 1 - 2);

                Ply--;

                RepetitionIndex--;

                position = copiedPosition;

                if (score >= beta)
                {
                    return beta;
                }
            }

            // razoring
            int eval = Evaluation.Evaluate(position);

            if (!isPVNode && !inCheck && depth <= 3)
            {
                //int threshold = alpha - 300 - (depth - 1) * 60;

                //if (eval < threshold)
                //{
                //    score = Quiescence(alpha, beta);
                //    if (score < threshold) return alpha;
                //}

                score = eval + 125;

                int newScore;

                if (score < beta)
                {
                    if (depth == 1)
                    {
                        newScore = Quiescence(alpha, beta);

                        return (newScore > score) ? newScore : score;
                    }

                    score += 175;

                    if (score < beta && depth <= 2)
                    {
                        newScore = Quiescence(alpha, beta);

                        if (newScore < beta)
                        {
                            return (newScore > score) ? newScore : score;
                        }
                    }
                }
            }

            int[] futilityMargin = new int[4] { 0, 200, 300, 500 };

            if (depth <= 3 
                && !isPVNode 
                && !inCheck 
                && Math.Abs(alpha) < 9000 
                && eval + futilityMargin[depth] <= alpha)
            {
                futilityPrune = true;
            }

            //int max = int.MinValue;
            int legalMoves = 0;
            MoveList moveList = new();

            Move.GenerateLegalMoves(position, ref moveList);

            SortMoves(ref moveList);

            int movesSearched = 0;

            for (int i = 0; i < moveList.Count; i++)
            {
                Position copiedPosition = position;

                Ply++;

                RepetitionIndex++;
                RepetitionTable[RepetitionIndex] = position.HashKey;

                if (!Move.MakeMove(ref position, moveList.Moves[i]))
                {
                    Ply--;

                    RepetitionIndex--;

                    continue;
                }

                int[] moveInfo = Move.ParseMove(moveList.Moves[i]);

                if (futilityPrune && legalMoves > 0)
                {
                    //int[] moveInfo = Move.ParseMove(moveList.Moves[i]);

                    if (moveInfo[3] == 0 && moveInfo[2] == 0)
                    {
                        if (!Move.IsSquareAttacked(position, position.WhiteToMove ? BitboardUtil.GetLS1BIndex(position.WhiteKing.IsolateLS1B()) : BitboardUtil.GetLS1BIndex(position.BlackKing.IsolateLS1B()), !position.WhiteToMove))
                        {
                            Ply--;

                            RepetitionIndex--;

                            position = copiedPosition;

                            continue;
                        }
                    }
                }

                legalMoves++;

                if (movesSearched == 0)
                {
                    score = -Negamax(-beta, -alpha, depth - 1);
                }
                // Late move reduction
                else
                {
                    //int[] moveInfo = Move.ParseMove(moveList.Moves[i]);

                    if (moveInfo[2] == 0
                        && moveInfo[3] == 0
                        && movesSearched >= FullDepthMoves
                        && depth >= ReductionLimit
                        && !inCheck
                       )
                    {
                        //score = -Negamax(-alpha - 1, -alpha, depth - 2);

                        //if (!isPVNode)
                        //{
                        //    score = -Negamax(-alpha - 1, -alpha, depth - (int)(Math.Sqrt(depth - 1) + Math.Sqrt(movesSearched - 1)) - 1);
                        //}
                        //else
                        //{
                        //    score = -Negamax(-alpha - 1, -alpha, depth - (int)((Math.Sqrt(depth - 1) + Math.Sqrt(movesSearched - 1)) * (2/3)) - 1);
                        //}

                        //if (!isPVNode)
                        //{
                        //    score = -Negamax(-alpha - 1, -alpha, (int)(Math.Sqrt(depth - 1) + Math.Sqrt(movesSearched - 1)));
                        //}
                        //else
                        //{
                        //    score = -Negamax(-alpha - 1, -alpha, (int)((Math.Sqrt(depth - 1) + Math.Sqrt(movesSearched - 1)) * (2/3)));
                        //}

                        if (movesSearched <= 6)
                        {
                            score = -Negamax(-alpha - 1, -alpha, depth - 2);
                        }
                        else
                        {
                            score = -Negamax(-alpha - 1, -alpha, depth / 2);
                        }
                    }
                    else score = alpha + 1;

                    // PVS
                    if (score > alpha)
                    {
                        score = -Negamax(-alpha - 1, -alpha, depth - 1);

                        if ((score > alpha) && (score < beta))
                        {
                            score = -Negamax(-beta, -alpha, depth - 1);
                        }
                    }
                }

                //if (foundPV)
                //{
                //    score = -Negamax(-alpha - 1, -alpha, depth - 1);

                //    if ((score > alpha) && (score < beta))
                //    {
                //        score = -Negamax(-beta, -alpha, depth - 1);
                //    }
                //}
                //else
                //{
                //    score = -Negamax(-beta, -alpha, depth - 1);
                //}

                Ply--;

                RepetitionIndex--;

                position = copiedPosition;

                movesSearched++;

                if (score >= beta)
                {
                    

                    TranspositionTable.RecordHash(position, depth, beta, TranspositionTable.Beta, moveList.Moves[i]);

                    if (Move.ParseMove(moveList.Moves[i])[3] == 0)
                    {
                        KillerMoves[1, Ply] = KillerMoves[0, Ply];
                        KillerMoves[0, Ply] = moveList.Moves[i];

                        //int[] moveInfo = Move.ParseMove(moveList.Moves[i]);

                        //if (moveInfo[3] == 0)
                        //{
                        //    int fromSquare = moveInfo[0];
                        //    int toSquare = moveInfo[1];

                        //    HistoryMoves[position.WhiteToMove ? 1 : 0, fromSquare, toSquare] += depth * depth;
                        //}
                    }

                    PVTable[Ply, Ply] = moveList.Moves[i];

                    for (int nextPly = Ply + 1; nextPly < PVLength[Ply + 1]; nextPly++)
                    {
                        PVTable[Ply, nextPly] = PVTable[Ply + 1, nextPly];
                    }

                    PVLength[Ply] = PVLength[Ply + 1];

                    return beta;
                }

                if (score > alpha)
                {
                    hashFlag = TranspositionTable.Exact;
                    bestMove = moveList.Moves[i];

                    alpha = score;

                    //foundPV = true;

                    PVTable[Ply, Ply] = moveList.Moves[i];

                    for (int nextPly = Ply + 1; nextPly < PVLength[Ply + 1]; nextPly++)
                    {
                        PVTable[Ply, nextPly] = PVTable[Ply + 1, nextPly];
                    }

                    PVLength[Ply] = PVLength[Ply + 1];
                }

                //if (score > max)
                //{
                //    max = score;
                //}
            }

            if (legalMoves == 0)
            {
                //Console.WriteLine("Test");
                //return inCheck ? int.MaxValue * (position.WhiteToMove ? -1 : 1) : 0;

                if (inCheck)
                {
                    alpha = -40000 + Ply;
                }
                else return 0;
            }

            TranspositionTable.RecordHash(position, depth, alpha, hashFlag, bestMove);
            //return max;
            return alpha;
        }

        private static int Quiescence(int alpha, int beta)
        {
            int standPat = Evaluation.Evaluate(position);

            if (standPat >= beta)
            {
                return beta;
            }

            if (alpha < standPat)
            {
                alpha = standPat;
            }

            MoveList moveList = new();

            Move.GenerateLegalCaptures(position, ref moveList);

            SortMoves(ref moveList);

            for (int i = 0; i < moveList.Count; i++)
            {
                //int promotion = Move.ParseMove(moveList.Moves[i])[2];

                //int bigDelta = 1000;
                //if (promotion > 0) bigDelta += 800;

                //if (standPat < alpha - bigDelta)
                //{
                //    return alpha;
                //}

                Position copiedPosition = position;

                Ply++;

                if (!Move.MakeMove(ref position, moveList.Moves[i]))
                {
                    Ply--;

                    continue;
                }

                int score = -Quiescence(-beta, -alpha);

                Ply--;

                position = copiedPosition;

                if (score >= beta)
                {
                    return beta;
                }

                if (score > alpha)
                {
                    alpha = score;
                }
            }

            return alpha;
        }

        private static int ScoreMove(ushort move)
        {
            int[] moveInfo = Move.ParseMove(move);

            int fromSquare = moveInfo[0];
            int toSquare = moveInfo[1];
            int capture = moveInfo[3];

            if (capture == 1)
            {
                int piece = 0;
                int targetPiece = 0;

                if (position.WhiteToMove == true)
                {
                    if (position.WhitePawns.GetBit(fromSquare) == 1)
                    {
                        piece = 0;
                    }
                    else if (position.WhiteKnights.GetBit(fromSquare) == 1)
                    {
                        piece = 1;
                    }
                    else if (position.WhiteBishops.GetBit(fromSquare) == 1)
                    {
                        piece = 2;
                    }
                    else if (position.WhiteRooks.GetBit(fromSquare) == 1)
                    {
                        piece = 3;
                    }
                    else if (position.WhiteQueens.GetBit(fromSquare) == 1)
                    {
                        piece = 4;
                    }
                    else if (position.WhiteKing.GetBit(fromSquare) == 1)
                    {
                        piece = 5;
                    }

                    if (position.BlackPawns.GetBit(toSquare) == 1)
                    {
                        targetPiece = 6;
                    }
                    else if (position.BlackKnights.GetBit(toSquare) == 1)
                    {
                        targetPiece = 7;
                    }
                    else if (position.BlackBishops.GetBit(toSquare) == 1)
                    {
                        targetPiece = 8;
                    }
                    else if (position.BlackRooks.GetBit(toSquare) == 1)
                    {
                        targetPiece = 9;
                    }
                    else if (position.BlackQueens.GetBit(toSquare) == 1)
                    {
                        targetPiece = 10;
                    }
                }
                else
                {
                    if (position.BlackPawns.GetBit(fromSquare) == 1)
                    {
                        piece = 6;
                    }
                    else if (position.BlackKnights.GetBit(fromSquare) == 1)
                    {
                        piece = 7;
                    }
                    else if (position.BlackBishops.GetBit(fromSquare) == 1)
                    {
                        piece = 8;
                    }
                    else if (position.BlackRooks.GetBit(fromSquare) == 1)
                    {
                        piece = 9;
                    }
                    else if (position.BlackQueens.GetBit(fromSquare) == 1)
                    {
                        piece = 10;
                    }
                    else if (position.BlackKing.GetBit(fromSquare) == 1)
                    {
                        piece = 11;
                    }

                    if (position.WhitePawns.GetBit(toSquare) == 1)
                    {
                        targetPiece = 0;
                    }
                    else if (position.WhiteKnights.GetBit(toSquare) == 1)
                    {
                        targetPiece = 1;
                    }
                    else if (position.WhiteBishops.GetBit(toSquare) == 1)
                    {
                        targetPiece = 2;
                    }
                    else if (position.WhiteRooks.GetBit(toSquare) == 1)
                    {
                        targetPiece = 3;
                    }
                    else if (position.WhiteQueens.GetBit(toSquare) == 1)
                    {
                        targetPiece = 4;
                    }
                }

                return MVV_LVA[piece, targetPiece] + 10000;
            }
            else
            {
                if (KillerMoves[0, Ply] == move)
                {
                    //Console.WriteLine("Killer move 1");
                    return 9000;
                }
                else if (KillerMoves[1, Ply] == move)
                {
                    //Console.WriteLine("Killer move 2");
                    return 8000;
                }
                //else
                //{
                //    return HistoryMoves[position.WhiteToMove ? 1 : 0, fromSquare, toSquare];
                //}
            }

            return 0;
        }

        private static void SortMoves(ref MoveList moves)
        {
            int[] moveScores = new int[moves.Count];

            for (int i = 0; i < moves.Count; i++)
            {
                //Console.WriteLine(TranspositionTable.Elements[position.HashKey % TranspositionTable.Size].Move);
                if (TranspositionTable.Elements[position.HashKey % TranspositionTable.Size].Move == moves.Moves[i])
                {
                    //Console.WriteLine("test");
                    moveScores[i] = 30000;
                }
                else
                {
                    moveScores[i] = ScoreMove(moves.Moves[i]);
                }
            }

            for (int current = 0; current < moves.Count; current++)
            {
                for (int next = current + 1; next < moves.Count; next++)
                {
                    if (moveScores[current] < moveScores[next])
                    {
                        (moveScores[next], moveScores[current]) = (moveScores[current], moveScores[next]);
                        (moves.Moves[next], moves.Moves[current]) = (moves.Moves[current], moves.Moves[next]);
                    }
                }
            }
        }

        public static void StartSearch(Position position, int depth)
        {
            int score = 0;

            for (int i = 0; i < KillerMoves.GetLength(0); i++)
            {
                for (int j = 0; j < KillerMoves.GetLength(1); j++)
                {
                    KillerMoves[i, j] = 0;
                }
            }

            for (int i = 0; i < PVTable.GetLength(0); i++)
            {
                for (int j = 0; j < PVTable.GetLength(1); j++)
                {
                    PVTable[i, j] = 0;
                }
            }

            for (int i = 0; i < PVLength.Length; i++)
            {
                PVLength[i] = 0;
            }

            Search.position = position;

            Stopwatch stopwatch = new();
            stopwatch.Start();

            for (int i = 1; i <= depth; i++)
            {
                score = Negamax(-int.MaxValue, int.MaxValue, i);

                Console.Write("PV: ");

                for (int j = 0; j < PVLength[0]; j++)
                {
                    int[] pvMoveInfo = Move.ParseMove(PVTable[0, j]);
                    
                    Console.Write($"{BitboardUtil.IndexToCoordinate[pvMoveInfo[0]]}{BitboardUtil.IndexToCoordinate[pvMoveInfo[1]]} ");
                }

                Console.WriteLine();
            }

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;

            Console.WriteLine($"Time Taken: {time.Hours}:{time.Minutes}:{time.Seconds}.{time.Milliseconds}");

            int[] bestMoveInfo = Move.ParseMove(PVTable[0, 0]);

            Console.WriteLine($"bestmove {BitboardUtil.IndexToCoordinate[bestMoveInfo[0]]}{BitboardUtil.IndexToCoordinate[bestMoveInfo[1]]}");
            Console.WriteLine($"Evaluation: {score}");
        }

        //public static string RunSearch(Position position, int depth)
        //{
        //    Ply = 0;

        //    Stopwatch stopwatch = new();
        //    stopwatch.Start();

        //    ushort bestMove = 0;

        //    Search.position = position;

        //    //int max = int.MinValue;
        //    int alpha = int.MinValue;
        //    int beta = int.MaxValue;

        //    MoveList moveList = new();

        //    Move.GenerateLegalMoves(Search.position, ref moveList);

        //    SortMoves(ref moveList);

        //    for (int i = 0; i < moveList.Count; i++)
        //    {
        //        Position copiedPosition = Search.position;

        //        if (!Move.MakeMove(ref Search.position, moveList.Moves[i]))
        //        {
        //            continue;
        //        }

        //        int score = -Negamax(-int.MaxValue, int.MaxValue, depth - 1);

        //        Search.position = copiedPosition;

        //        if (score >= beta)
        //        {
        //            PVTable[Ply, Ply] = moveList.Moves[i];

        //            for (int nextPly = Ply + 1; nextPly < PVLength[Ply + 1]; nextPly++)
        //            {
        //                PVTable[Ply, nextPly] = PVTable[Ply + 1, nextPly];
        //            }

        //            PVLength[Ply] = PVLength[Ply + 1];

        //            Console.WriteLine(beta);

        //            for (int j = 0; j < PVLength[0]; j++)
        //            {
        //                int[] pvMoveInfo = Move.ParseMove(PVTable[0, j]);

        //                Console.WriteLine($"PV: {BitboardUtil.IndexToCoordinate[pvMoveInfo[0]]}{BitboardUtil.IndexToCoordinate[pvMoveInfo[1]]} ");
        //            }

        //            int[] move = Move.ParseMove(moveList.Moves[i]);

        //            //return moveList.Moves[i];
        //            return $"{BitboardUtil.IndexToCoordinate[move[0]]}{BitboardUtil.IndexToCoordinate[move[1]]}";
        //        }

        //        if (score > alpha)
        //        {
        //            alpha = score;

        //            PVTable[Ply, Ply] = moveList.Moves[i];

        //            for (int nextPly = Ply + 1; nextPly < PVLength[Ply + 1]; nextPly++)
        //            {
        //                PVTable[Ply, nextPly] = PVTable[Ply + 1, nextPly];
        //            }

        //            PVLength[Ply] = PVLength[Ply + 1];

        //            bestMove = moveList.Moves[i];
        //        }

        //        //if (score > max)
        //        //{
        //        //    max = score;

        //        //    //Console.WriteLine(max);
        //        //    //return moveList.Moves[i];
        //        //    bestMove = moveList.Moves[i];
        //        //}
        //    }

        //    //return 0;
        //    //Console.WriteLine(max);

        //    stopwatch.Stop();
        //    TimeSpan timeSpan = stopwatch.Elapsed;

        //    Console.WriteLine($"Time: {timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}.{timeSpan.Milliseconds}");

        //    Console.WriteLine(alpha);

        //    for (int i = 0; i < PVLength[0]; i++)
        //    {
        //        int[] pvMoveInfo = Move.ParseMove(PVTable[0, i]);

        //        Console.WriteLine($"PV: {BitboardUtil.IndexToCoordinate[pvMoveInfo[0]]}{BitboardUtil.IndexToCoordinate[pvMoveInfo[1]]} ");
        //    }

        //    int[] moveInfo = Move.ParseMove(bestMove);

        //    //return bestMove;
        //    return $"{BitboardUtil.IndexToCoordinate[moveInfo[0]]}{BitboardUtil.IndexToCoordinate[moveInfo[1]]}";
        //}
    }
}
