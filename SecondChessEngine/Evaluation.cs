using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class Evaluation
    {
        public static int PawnWeight { get; } = 100;
        public static int KnightWeight { get; } = 350;
        public static int BishopWeight { get; } = 350;
        public static int RookWeight { get; } = 525;
        public static int QueenWeight { get; } = 1000;
        private static int BishopPairBonus { get; } = 50;
        private static int KnightPairPenalty { get; } = 8;
        private static int RookPairPenalty { get; } = 16;
        private static int RookOpenFileBonus { get; } = 10;
        private static int RookHalfOpenFileBonus { get; } = 5;

        public static int[] MGPawnTable { get; set; } = new int[64]
        {
             0,   0,   0,   0,   0,   0,  0,   0,
            98, 134,  61,  95,  68, 126, 34, -11,
            -6,   7,  26,  31,  65,  56, 25, -20,
            -14,  13,   6,  21,  23,  12, 17, -23,
            -27,  -2,  -5,  12,  17,   6, 10, -25,
            -26,  -4,  -4, -10,   3,   3, 33, -12,
            -35,  -1, -20, -23, -15,  24, 38, -22,
            0,   0,   0,   0,   0,   0,  0,   0,
        };
        public static int[] MGKnightTable { get; set; } = new int[64]
        {
            -167, -89, -34, -49,  61, -97, -15, -107,
            -73, -41,  72,  36,  23,  62,   7,  -17,
            -47,  60,  37,  65,  84, 129,  73,   44,
            -9,  17,  19,  53,  37,  69,  18,   22,
            -13,   4,  16,  13,  28,  19,  21,   -8,
            -23,  -9,  12,  10,  19,  17,  25,  -16,
            -29, -53, -12,  -3,  -1,  18, -14,  -19,
            -105, -21, -58, -33, -17, -28, -19,  -23,
        };
        public static int[] MGBishopTable { get; set; } = new int[64]
        {
            -29,   4, -82, -37, -25, -42,   7,  -8,
            -26,  16, -18, -13,  30,  59,  18, -47,
            -16,  37,  43,  40,  35,  50,  37,  -2,
            -4,   5,  19,  50,  37,  37,   7,  -2,
            -6,  13,  13,  26,  34,  12,  10,   4,
            0,  15,  15,  15,  14,  27,  18,  10,
            4,  15,  16,   0,   7,  21,  33,   1,
            -33,  -3, -14, -21, -13, -12, -39, -21,
        };
        public static int[] MGRookTable { get; set; } = new int[64]
        {
             32,  42,  32,  51, 63,  9,  31,  43,
            27,  32,  58,  62, 80, 67,  26,  44,
            -5,  19,  26,  36, 17, 45,  61,  16,
            -24, -11,   7,  26, 24, 35,  -8, -20,
            -36, -26, -12,  -1,  9, -7,   6, -23,
            -45, -25, -16, -17,  3,  0,  -5, -33,
            -44, -16, -20,  -9, -1, 11,  -6, -71,
            -19, -13,   1,  17, 16,  7, -37, -26,
        };
        public static int[] MGQueenTable { get; set; } = new int[64]
        {
             -28,   0,  29,  12,  59,  44,  43,  45,
            -24, -39,  -5,   1, -16,  57,  28,  54,
            -13, -17,   7,   8,  29,  56,  47,  57,
            -27, -27, -16, -16,  -1,  17,  -2,   1,
            -9, -26,  -9, -10,  -2,  -4,   3,  -3,
            -14,   2, -11,  -2,  -5,   2,  14,   5,
            -35,  -8,  11,   2,   8,  15,  -3,   1,
            -1, -18,  -9,  10, -15, -25, -31, -50,
        };
        public static int[] MGKingTable { get; set; } = new int[64]
        {
           -65,  23,  16, -15, -56, -34,   2,  13,
            29,  -1, -20,  -7,  -8,  -4, -38, -29,
            -9,  24,   2, -16, -20,   6,  22, -22,
            -17, -20, -12, -27, -30, -25, -14, -36,
            -49,  -1, -27, -39, -46, -44, -33, -51,
            -14, -14, -22, -46, -44, -30, -15, -27,
            1,   7,  -8, -64, -43, -16,   9,   8,
            -15,  36,  12, -54,   8, -28,  24,  14,
        };

        public static int[] EGPawnTable { get; set; } = new int[64]
        {
             0,   0,   0,   0,   0,   0,   0,   0,
            178, 173, 158, 134, 147, 132, 165, 187,
            94, 100,  85,  67,  56,  53,  82,  84,
            32,  24,  13,   5,  -2,   4,  17,  17,
            13,   9,  -3,  -7,  -7,  -8,   3,  -1,
            4,   7,  -6,   1,   0,  -5,  -1,  -8,
            13,   8,   8,  10,  13,   0,   2,  -7,
            0,   0,   0,   0,   0,   0,   0,   0,
        };
        public static int[] EGKnightTable { get; set; } = new int[64]
        {
            -58, -38, -13, -28, -31, -27, -63, -99,
            -25,  -8, -25,  -2,  -9, -25, -24, -52,
            -24, -20,  10,   9,  -1,  -9, -19, -41,
            -17,   3,  22,  22,  22,  11,   8, -18,
            -18,  -6,  16,  25,  16,  17,   4, -18,
            -23,  -3,  -1,  15,  10,  -3, -20, -22,
            -42, -20, -10,  -5,  -2, -20, -23, -44,
            -29, -51, -23, -15, -22, -18, -50, -64,
        };
        public static int[] EGBishopTable { get; set; } = new int[64]
        {
            -14, -21, -11,  -8, -7,  -9, -17, -24,
            -8,  -4,   7, -12, -3, -13,  -4, -14,
            2,  -8,   0,  -1, -2,   6,   0,   4,
            -3,   9,  12,   9, 14,  10,   3,   2,
            -6,   3,  13,  19,  7,  10,  -3,  -9,
            -12,  -3,   8,  10, 13,   3,  -7, -15,
            -14, -18,  -7,  -1,  4,  -9, -15, -27,
            -23,  -9, -23,  -5, -9, -16,  -5, -17,
        };
        public static int[] EGRookTable { get; set; } = new int[64]
        {
             13, 10, 18, 15, 12,  12,   8,   5,
            11, 13, 13, 11, -3,   3,   8,   3,
            7,  7,  7,  5,  4,  -3,  -5,  -3,
            4,  3, 13,  1,  2,   1,  -1,   2,
            3,  5,  8,  4, -5,  -6,  -8, -11,
            -4,  0, -5, -1, -7, -12,  -8, -16,
            -6, -6,  0,  2, -9,  -9, -11,  -3,
            -9,  2,  3, -1, -5, -13,   4, -20,
        };
        public static int[] EGQueenTable { get; set; } = new int[64]
        {
            -9,  22,  22,  27,  27,  19,  10,  20,
            -17,  20,  32,  41,  58,  25,  30,   0,
            -20,   6,   9,  49,  47,  35,  19,   9,
            3,  22,  24,  45,  57,  40,  57,  36,
            -18,  28,  19,  47,  31,  34,  39,  23,
            -16, -27,  15,   6,   9,  17,  10,   5,
            -22, -23, -30, -16, -16, -23, -36, -32,
            -33, -28, -22, -43,  -5, -32, -20, -41,
        };
        public static int[] EGKingTable { get; set; } = new int[64]
        {
           -74, -35, -18, -18, -11,  15,   4, -17,
            -12,  17,  14,  17,  17,  38,  23,  11,
            10,  17,  23,  15,  20,  45,  44,  13,
            -8,  22,  24,  27,  26,  33,  26,   3,
            -18,  -4,  21,  24,  27,  23,   9, -11,
            -19,  -3,  11,  21,  23,  16,   7,  -9,
            -27, -11,   4,  13,  14,   4,  -5, -17,
            -53, -34, -21, -11, -28, -14, -24, -43
        };

        public static int WPawnCount { get; set; }
        public static int WKnightCount { get; set; }
        public static int WBishopCount { get; set; }
        public static int WRookCount { get; set; }
        public static int WQueenCount { get; set; }

        public static int BPawnCount { get; set; }
        public static int BKnightCount { get; set; }
        public static int BBishopCount { get; set; }
        public static int BRookCount { get; set; }
        public static int BQueenCount { get; set; }

        private static int PawnPhase { get; } = 0;
        private static int KnightPhase { get; } = 1;
        private static int BishopPhase { get; } = 1;
        private static int RookPhase { get; } = 2;
        private static int QueenPhase { get; } = 4;

        private static int[] KnightAdjust { get; set; } = new int[9]
        {
             -20, -16, -12, -8, -4,  0,  4,  8, 12
        };
        public static int[] RookAdjust { get; set; } = new int[9]
        {
              15,  12,   9,  6,  3,  0, -3, -6, -9
        };

        private static ulong[] Files { get; } = new ulong[8]
        {
            72340172838076673UL,
            144680345676153346UL,
            289360691352306692UL,
            578721382704613384UL,
            1157442765409226768UL,
            2314885530818453536UL,
            4629771061636907072UL,
            9259542123273814144UL
        };

        private static ulong[] FileMasks { get; set; } = new ulong[64];

        private static ulong[] RankMasks { get; set; } = new ulong[64];

        private static ulong[] IsolatedMasks { get; set; } = new ulong[64];

        private static ulong[] WhitePassedMasks { get; set; } = new ulong[64];

        private static ulong[] BlackPassedMasks { get; set; } = new ulong[64];

        private static int[] GetRank { get; } = new int[64]
        {
            7, 7, 7, 7, 7, 7, 7, 7,
            6, 6, 6, 6, 6, 6, 6, 6,
            5, 5, 5, 5, 5, 5, 5, 5,
            4, 4, 4, 4, 4, 4, 4, 4,
            3, 3, 3, 3, 3, 3, 3, 3,
            2, 2, 2, 2, 2, 2, 2, 2,
            1, 1, 1, 1, 1, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 0, 0
        };

        private static int[] GetFile { get; } = new int[64]
        {
            0, 1, 2, 3, 4, 5, 6, 7,
            0, 1, 2, 3, 4, 5, 6, 7,
            0, 1, 2, 3, 4, 5, 6, 7,
            0, 1, 2, 3, 4, 5, 6, 7,
            0, 1, 2, 3, 4, 5, 6, 7,
            0, 1, 2, 3, 4, 5, 6, 7,
            0, 1, 2, 3, 4, 5, 6, 7,
            0, 1, 2, 3, 4, 5, 6, 7
        };

        private static int MGDoubledPawnPenalty { get; } = 3;
        private static int EGDoubledPawnPenalty { get; } = 10;

        private static int IsolatedPawnPenalty { get; } = 10;

        private static int[] PassedPawnBonus { get; } = new int[8] { 0, 10, 30, 50, 75, 100, 150, 200 };


        private static ulong SetFileRankMasks(int fileNumber, int rankNumber)
        {
            ulong mask = 0UL;

            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    int square = file + rank * 8;

                    if (fileNumber != -1)
                    {
                        if (file == fileNumber)
                        {
                            mask |= BitboardUtil.SetBit(mask, square);
                        }
                    }
                    else if (rankNumber != -1)
                    {
                        if (rank == rankNumber)
                        {
                            mask |= BitboardUtil.SetBit(mask, square);
                        }
                    }
                }
            }

            return mask;
        }

        public static void InitEvaluationMasks()
        {
            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    int square = file + rank * 8;

                    FileMasks[square] |= SetFileRankMasks(file, -1);
                    //Console.WriteLine(FileMasks[square]);
                    RankMasks[square] |= SetFileRankMasks(-1, rank);
                    //Console.WriteLine(RankMasks[square]);
                    IsolatedMasks[square] |= SetFileRankMasks(file - 1, -1);
                    IsolatedMasks[square] |= SetFileRankMasks(file + 1, -1);
                    //Console.WriteLine(IsolatedMasks[square]);
                    WhitePassedMasks[square] |= SetFileRankMasks(file - 1, -1);
                    WhitePassedMasks[square] |= SetFileRankMasks(file, -1);
                    WhitePassedMasks[square] |= SetFileRankMasks(file + 1, -1);

                    for (int i = 0; i < (rank + 1); i++)
                    {
                        WhitePassedMasks[square] &= ~RankMasks[i * 8 + file];
                    }
                }
            }

            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    int square = file + rank * 8;

                    BlackPassedMasks[square] |= SetFileRankMasks(file - 1, -1);
                    BlackPassedMasks[square] |= SetFileRankMasks(file, -1);
                    BlackPassedMasks[square] |= SetFileRankMasks(file + 1, -1);

                    for (int i = 7; i > (rank - 1); i--)
                    {
                        //Console.WriteLine(i * 8 + file);
                        BlackPassedMasks[square] &= ~RankMasks[i * 8 + file];
                        
                        //BlackPassedMasks[square].ClearBit(i * 8 + file);
                    }
                }
            }
        }

        public static int Evaluate(Position position)
        {
            int score = 0;
            int sideMultiplier = position.WhiteToMove ? 1 : -1;

            int whiteMaterial = 0;
            int blackMaterial = 0;

            WPawnCount = BitOperations.PopCount(position.WhitePawns);
            WKnightCount = BitOperations.PopCount(position.WhiteKnights);
            WBishopCount = BitOperations.PopCount(position.WhiteBishops);
            WRookCount = BitOperations.PopCount(position.WhiteRooks);
            WQueenCount = BitOperations.PopCount(position.WhiteQueens);

            BPawnCount = BitOperations.PopCount(position.BlackPawns);
            BKnightCount = BitOperations.PopCount(position.BlackKnights);
            BBishopCount = BitOperations.PopCount(position.BlackBishops);
            BRookCount = BitOperations.PopCount(position.BlackRooks);
            BQueenCount = BitOperations.PopCount(position.BlackQueens);

            whiteMaterial += WPawnCount * PawnWeight;
            whiteMaterial += WKnightCount * KnightWeight;
            whiteMaterial += WBishopCount * BishopWeight;
            whiteMaterial += WRookCount * RookWeight;
            whiteMaterial += WQueenCount * QueenWeight;

            blackMaterial += BPawnCount * PawnWeight;
            blackMaterial += BKnightCount * KnightWeight;
            blackMaterial += BBishopCount * BishopWeight;
            blackMaterial += BRookCount * RookWeight;
            blackMaterial += BQueenCount * QueenWeight;

            score = whiteMaterial - blackMaterial;

            int lowMaterialEval = EvaluateLowMaterial(score, whiteMaterial, blackMaterial);

            //if (lowMaterialEval == 0)
            //{
            //    return lowMaterialEval;
            //}
            if (lowMaterialEval != score)
            {
                score = lowMaterialEval;
            }

            //if (IsMaterialDraw(score, whiteMaterial, blackMaterial))
            //{
            //    return 0;
            //}

            if (WBishopCount > 1) score += BishopPairBonus;
            if (BBishopCount > 1) score -= BishopPairBonus;

            if (WKnightCount > 1) score -= KnightPairPenalty;
            if (BKnightCount > 1) score += KnightPairPenalty;

            if (WRookCount > 1) score -= RookPairPenalty;
            if (BRookCount > 1) score += RookPairPenalty;

            score += KnightAdjust[WPawnCount] * WKnightCount;
            score -= KnightAdjust[BPawnCount] * BKnightCount;

            score += RookAdjust[WPawnCount] * WRookCount;
            score -= RookAdjust[BPawnCount] * BRookCount;

            score += EvalRooks(position);

            //int numOfPawnsInFile = BitOperations.PopCount(position.WhitePawns & FileMasks[BitboardUtil.GetLS1BIndex(position.WhitePawns.IsolateLS1B())]);

            //if (numOfPawnsInFile > 1)
            //{
            //    Console.WriteLine(numOfPawnsInFile);
            //}

            //Console.WriteLine(BitOperations.PopCount(position.WhitePawns & (position.WhitePawns << 8)));

            //ulong pawnBitboard = position.WhitePawns;

            //for (int i = 0; i < WPawnCount; i++)
            //{
            //    int square = BitboardUtil.GetLS1BIndex(pawnBitboard.IsolateLS1B());
            //    int numOfPawnsInFile = BitOperations.PopCount(position.WhitePawns & FileMasks[square]);

            //    if (numOfPawnsInFile > 1)
            //    {
            //        score -= numOfPawnsInFile * MGDoubledPawnPenalty;
            //    }

            //    pawnBitboard = pawnBitboard.ClearBit(square);
            //}

            //Console.WriteLine(position.WhitePawns & FileMasks[BitboardUtil.GetLS1BIndex(position.WhitePawns.IsolateLS1B())]);

            //int middleGameScore = MiddleGameEval(position);
            //int endGameScore = EndGameEval(position);
            int middleGameScore = 0;
            int endGameScore = 0;

            Parallel.Invoke(() =>
            {
                middleGameScore = MiddleGameEval(position);
            }, () =>
            {
                endGameScore = EndGameEval(position);
            });

            int phase = ComputePhase();

            //score += middleGameScore;
            score += ((middleGameScore * (256 - phase)) + (endGameScore * phase)) / 256;

            score *= sideMultiplier;

            return score;
        }

        private static int EvaluateLowMaterial(int score, int whiteMaterial, int blackMaterial)
        {
            int stronger;
            int weaker;

            if (score > 0)
            {
                stronger = whiteMaterial;
                weaker = blackMaterial;
            }
            else
            {
                weaker = whiteMaterial;
                stronger = blackMaterial;
            }

            if ((stronger == whiteMaterial ? WPawnCount : BPawnCount) == 0)
            {
                //if (WQueenCount == 0 && BQueenCount == 0)
                //{
                //    if (WRookCount == 0 && BRookCount == 0)
                //    {
                //        if (WBishopCount == 0 && BBishopCount == 0)
                //        {
                //            if (WKnightCount == 0 && BKnightCount == 0)
                //            {
                //                return true;
                //            }
                //        }
                //    }
                //}

                if (stronger <= KnightWeight)
                {
                    return 0;
                }

                if (WPawnCount == 0 && BPawnCount == 0)
                {
                    if (WQueenCount == 0 && BQueenCount == 0)
                    {
                        if (WRookCount == 0 && BRookCount == 0)
                        {
                            if (WBishopCount == 0 && BBishopCount == 0)
                            {
                                if ((stronger == whiteMaterial ? WKnightCount : BKnightCount) == 2)
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                }

                if (stronger == RookWeight && weaker == KnightWeight)
                {
                    return score / 2;
                }

                if (stronger == RookWeight + KnightWeight && weaker == RookWeight)
                {
                    return score / 2;
                }

                //if ((stronger == whiteMaterial ? WKnightCount : BKnightCount) == 2 && weaker == 0)
                //{
                //    return 0;
                //}

                //if ((stronger - weaker) < 400)
                //{
                //    return score / 16;
                //}
            }

            return score;
        }

        public static int EvalRooks(Position position)
        {
            int score = 0;

            ulong bitboard;
            int square;

            bitboard = position.WhiteRooks;

            for (int i = 0; i < WRookCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                if ((Files[square % 8] & (position.WhitePawns | position.BlackPawns)) == 0)
                {
                    score += RookOpenFileBonus;
                }
                else if ((Files[square % 8] & position.WhitePawns) == 0)
                {
                    score += RookHalfOpenFileBonus;
                }

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackRooks;

            for (int i = 0; i < BRookCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                if ((Files[square % 8] & (position.WhitePawns | position.BlackPawns)) == 0)
                {
                    score -= RookOpenFileBonus;
                }
                else if ((Files[square % 8] & position.BlackPawns) == 0)
                {
                    score -= RookHalfOpenFileBonus;
                }

                bitboard = bitboard.ClearBit(square);
            }

            return score;
        }

        private static int ComputePhase()
        {
            int totalPhase = (PawnPhase * 16) + (KnightPhase * 4) + (BishopPhase * 4) + (RookPhase * 4) + (QueenPhase * 2);

            int phase = totalPhase;

            phase -= WPawnCount   * PawnPhase;
            phase -= WKnightCount * KnightPhase;
            phase -= WBishopCount * BishopPhase;
            phase -= WRookCount   * RookPhase;
            phase -= WQueenCount  * QueenPhase;

            phase -= BPawnCount   * PawnPhase;
            phase -= BKnightCount * KnightPhase;
            phase -= BBishopCount * BishopPhase;
            phase -= BRookCount   * RookPhase;
            phase -= BQueenCount  * QueenPhase;

            phase = (phase * 256 + (totalPhase / 2)) / totalPhase;

            return phase;
        }

        private static int GetTropism(int square1, int square2)
        {
            //Console.WriteLine($"{BitboardUtil.IndexToCoordinate[square1]}: {7 - (Math.Abs(GetRank[square1] - GetRank[square2]) + Math.Abs(GetFile[square1] - GetFile[square2]))}");
            return 7 - (Math.Abs(GetRank[square1] - GetRank[square2]) + Math.Abs(GetFile[square1] - GetFile[square2]));
        }

        private static int EndGameEval(Position position)
        {
            int score = 0;

            ulong bitboard;
            int square;

            int blackKingLocation = BitboardUtil.GetLS1BIndex(position.BlackKing.IsolateLS1B());
            int whiteKingLocation = BitboardUtil.GetLS1BIndex(position.WhiteKing.IsolateLS1B());

            bitboard = position.WhitePawns;

            for (int i = 0; i < WPawnCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += EGPawnTable[square ^ 56];

                int numOfPawnsInFile = BitOperations.PopCount(position.WhitePawns & FileMasks[square]);

                if (numOfPawnsInFile > 1)
                {
                    score -= numOfPawnsInFile * EGDoubledPawnPenalty;
                    //Console.WriteLine("Doubled Pawn!");
                }

                if ((position.WhitePawns & IsolatedMasks[square]) == 0)
                {
                    score -= IsolatedPawnPenalty;
                }

                if ((WhitePassedMasks[square] & position.BlackPawns) == 0)
                {
                    score += PassedPawnBonus[GetRank[square]];
                    //Console.WriteLine("Passed pawn!");
                }

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteKnights;

            for (int i = 0; i < WKnightCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += EGKnightTable[square ^ 56];

                //Console.WriteLine(BitOperations.PopCount(Move.KnightAttacks[square] & ~position.WhiteOccupancies));
                score += (BitOperations.PopCount(Move.KnightAttacks[square] & ~position.WhiteOccupancies) - 4) * 4;

                score += 3 * GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteBishops;

            for (int i = 0; i < WBishopCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += EGBishopTable[square ^ 56];

                //Console.WriteLine(BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)));
                score += (BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 6) * 5;

                score += GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteRooks;

            for (int i = 0; i < WRookCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += EGRookTable[square ^ 56];

                score += (BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 7) * 2;

                score += GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteQueens;

            for (int i = 0; i < WQueenCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += EGQueenTable[square ^ 56];

                score += ((BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 14) * 2) + ((BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 14) * 2);

                score += 4 * GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackPawns;

            for (int i = 0; i < BPawnCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= EGPawnTable[square];

                int numOfPawnsInFile = BitOperations.PopCount(position.BlackPawns & FileMasks[square]);

                if (numOfPawnsInFile > 1)
                {
                    score += numOfPawnsInFile * EGDoubledPawnPenalty;
                    //Console.WriteLine("Doubled Pawn!");
                }

                if ((position.BlackPawns & IsolatedMasks[square]) == 0)
                {
                    score += IsolatedPawnPenalty;
                }

                if ((BlackPassedMasks[square] & position.WhitePawns) == 0)
                {
                    score += PassedPawnBonus[GetRank[square]];
                }

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackKnights;

            for (int i = 0; i < BKnightCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= EGKnightTable[square];

                score -= (BitOperations.PopCount(Move.KnightAttacks[square] & ~position.BlackOccupancies) - 4) * 4;

                score -= 3 * GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackBishops;

            for (int i = 0; i < BBishopCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= EGBishopTable[square];

                score -= (BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 6) * 5;

                score -= GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackRooks;

            for (int i = 0; i < BRookCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= EGRookTable[square];

                score -= (BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 7) * 4;

                score -= GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackQueens;

            for (int i = 0; i < BQueenCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= EGQueenTable[square];

                score -= ((BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 14) * 2) + ((BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 14) * 2);

                score -= 4 * GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            return score;
        }

        private static int MiddleGameEval(Position position)
        {
            int score = 0;

            ulong bitboard;
            int square;

            int blackKingLocation = BitboardUtil.GetLS1BIndex(position.BlackKing.IsolateLS1B());
            int whiteKingLocation = BitboardUtil.GetLS1BIndex(position.WhiteKing.IsolateLS1B());

            bitboard = position.WhitePawns;

            for (int i = 0; i < WPawnCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += MGPawnTable[square ^ 56];

                int numOfPawnsInFile = BitOperations.PopCount(position.WhitePawns & FileMasks[square]);

                if (numOfPawnsInFile > 1)
                {
                    score -= numOfPawnsInFile * MGDoubledPawnPenalty;
                }

                if ((position.WhitePawns & IsolatedMasks[square]) == 0)
                {
                    score -= IsolatedPawnPenalty;
                }

                if ((WhitePassedMasks[square] & position.BlackPawns) == 0)
                {
                    score += PassedPawnBonus[GetRank[square]];
                }

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteKnights;

            for (int i = 0; i < WKnightCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += MGKnightTable[square ^ 56];

                //Console.WriteLine(BitOperations.PopCount(Move.KnightAttacks[square] & ~position.WhiteOccupancies));
                score += (BitOperations.PopCount(Move.KnightAttacks[square] & ~position.WhiteOccupancies) - 4) * 4;

                //Console.WriteLine(BitboardUtil.GetLS1BIndex(position.BlackKing.IsolateLS1B()));
                score += 3 * GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteBishops;

            for (int i = 0; i < WBishopCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += MGBishopTable[square ^ 56];

                //Console.WriteLine(BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)));
                score += (BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 6) * 5;

                score += 2 * GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteRooks;

            for (int i = 0; i < WRookCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += MGRookTable[square ^ 56];

                score += (BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 7) * 2;

                score += 2 * GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.WhiteQueens;

            for (int i = 0; i < WQueenCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score += MGQueenTable[square ^ 56];

                score += ((BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 14) * 1) + ((BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 14) * 1);

                score += 2 * GetTropism(square, blackKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackPawns;

            for (int i = 0; i < BPawnCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= MGPawnTable[square];

                int numOfPawnsInFile = BitOperations.PopCount(position.BlackPawns & FileMasks[square]);

                if (numOfPawnsInFile > 1)
                {
                    score += numOfPawnsInFile * MGDoubledPawnPenalty;
                }

                if ((position.BlackPawns & IsolatedMasks[square]) == 0)
                {
                    score += IsolatedPawnPenalty;
                }

                if ((BlackPassedMasks[square] & position.WhitePawns) == 0)
                {
                    score += PassedPawnBonus[GetRank[square]];
                }

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackKnights;

            for (int i = 0; i < BKnightCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= MGKnightTable[square];

                score -= (BitOperations.PopCount(Move.KnightAttacks[square] & ~position.BlackOccupancies) - 4) * 4;

                score -= 3 * GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackBishops;

            for (int i = 0; i < BBishopCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= MGBishopTable[square];

                score -= (BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 6) * 5;

                score -= 2 * GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackRooks;

            for (int i = 0; i < BRookCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= MGRookTable[square];

                score -= (BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 7) * 2;

                score -= 2 * GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            bitboard = position.BlackQueens;

            for (int i = 0; i < BQueenCount; i++)
            {
                square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());

                score -= MGQueenTable[square];

                score -= ((BitOperations.PopCount(Move.GetBishopAttacks(square, position.AllOccupancies)) - 14) * 1) + ((BitOperations.PopCount(Move.GetRookAttacks(square, position.AllOccupancies)) - 14) * 1);

                score -= 2 * GetTropism(square, whiteKingLocation);

                bitboard = bitboard.ClearBit(square);
            }

            return score;
        }
    }
}
