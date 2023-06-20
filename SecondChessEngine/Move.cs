using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SecondChessEngine
{
    public static class Move
    {
        // Precomputed move sets for pieces on any given square
        public static ulong[] KnightAttacks { get; } = new ulong[64]
        {
            132096UL,
329728UL,
659712UL,
1319424UL,
2638848UL,
5277696UL,
10489856UL,
4202496UL,
33816580UL,
84410376UL,
168886289UL,
337772578UL,
675545156UL,
1351090312UL,
2685403152UL,
1075839008UL,
8657044482UL,
21609056261UL,
43234889994UL,
86469779988UL,
172939559976UL,
345879119952UL,
687463207072UL,
275414786112UL,
2216203387392UL,
5531918402816UL,
11068131838464UL,
22136263676928UL,
44272527353856UL,
88545054707712UL,
175990581010432UL,
70506185244672UL,
567348067172352UL,
1416171111120896UL,
2833441750646784UL,
5666883501293568UL,
11333767002587136UL,
22667534005174272UL,
45053588738670592UL,
18049583422636032UL,
145241105196122112UL,
362539804446949376UL,
725361088165576704UL,
1450722176331153408UL,
2901444352662306816UL,
5802888705324613632UL,
11533718717099671552UL,
4620693356194824192UL,
288234782788157440UL,
576469569871282176UL,
1224997833292120064UL,
2449995666584240128UL,
4899991333168480256UL,
9799982666336960512UL,
1152939783987658752UL,
2305878468463689728UL,
1128098930098176UL,
2257297371824128UL,
4796069720358912UL,
9592139440717824UL,
19184278881435648UL,
38368557762871296UL,
4679521487814656UL,
9077567998918656UL
        };

        public static ulong[] WhitePawnAttacks { get; } = new ulong[64]
        {
            512UL,
            1280UL,
            2560UL,
            5120UL,
            10240UL,
            20480UL,
            40960UL,
            16384UL,
            131072UL,
            327680UL,
            655360UL,
            1310720UL,
            2621440UL,
            5242880UL,
            10485760UL,
            4194304UL,
            33554432UL,
            83886080UL,
            167772160UL,
            335544320UL,
            671088640UL,
            1342177280UL,
            2684354560UL,
            1073741824UL,
            8589934592UL,
            21474836480UL,
            42949672960UL,
            85899345920UL,
            171798691840UL,
            343597383680UL,
            687194767360UL,
            274877906944UL,
            2199023255552UL,
            5497558138880UL,
            10995116277760UL,
            21990232555520UL,
            43980465111040UL,
            87960930222080UL,
            175921860444160UL,
            70368744177664UL,
            562949953421312UL,
            1407374883553280UL,
            2814749767106560UL,
            5629499534213120UL,
            11258999068426240UL,
            22517998136852480UL,
            45035996273704960UL,
            18014398509481984UL,
            144115188075855872UL,
            360287970189639680UL,
            720575940379279360UL,
            1441151880758558720UL,
            2882303761517117440UL,
            5764607523034234880UL,
            11529215046068469760UL,
            4611686018427387904UL,
            0UL,
            0UL,
            0UL,
            0UL,
            0UL,
            0UL,
            0UL,
            0UL
        };

        public static ulong[] BlackPawnAttacks { get; } = new ulong[64]
        {
            0UL,
            0UL,
            0UL,
            0UL,
            0UL,
            0UL,
            0UL,
            0UL,
            2UL,
            5UL,
            10UL,
            20UL,
            40UL,
            80UL,
            160UL,
            64UL,
            512UL,
            1280UL,
            2560UL,
            5120UL,
            10240UL,
            20480UL,
            40960UL,
            16384UL,
            131072UL,
            327680UL,
            655360UL,
            1310720UL,
            2621440UL,
            5242880UL,
            10485760UL,
            4194304UL,
            33554432UL,
            83886080UL,
            167772160UL,
            335544320UL,
            671088640UL,
            1342177280UL,
            2684354560UL,
            1073741824UL,
            8589934592UL,
            21474836480UL,
            42949672960UL,
            85899345920UL,
            171798691840UL,
            343597383680UL,
            687194767360UL,
            274877906944UL,
            2199023255552UL,
            5497558138880UL,
            10995116277760UL,
            21990232555520UL,
            43980465111040UL,
            87960930222080UL,
            175921860444160UL,
            70368744177664UL,
            562949953421312UL,
            1407374883553280UL,
            2814749767106560UL,
            5629499534213120UL,
            11258999068426240UL,
            22517998136852480UL,
            45035996273704960UL,
            18014398509481984UL
        };

        public static ulong[] KingAttacks { get; } = new ulong[64]
        {
            770UL,
            1797UL,
            3594UL,
            7188UL,
            14376UL,
            28752UL,
            57504UL,
            49216UL,
            197123UL,
            460039UL,
            920078UL,
            1840156UL,
            3680312UL,
            7360624UL,
            14721248UL,
            12599488UL,
            50463488UL,
            117769984UL,
            235539968UL,
            471079936UL,
            942159872UL,
            1884319744UL,
            3768639488UL,
            3225468928UL,
            12918652928UL,
            30149115904UL,
            60298231808UL,
            120596463616UL,
            241192927232UL,
            482385854464UL,
            964771708928UL,
            825720045568UL,
            3307175149568UL,
            7718173671424UL,
            15436347342848UL,
            30872694685696UL,
            61745389371392UL,
            123490778742784UL,
            246981557485568UL,
            211384331665408UL,
            846636838289408UL,
            1975852459884544UL,
            3951704919769088UL,
            7903409839538176UL,
            15806819679076352UL,
            31613639358152704UL,
            63227278716305408UL,
            54114388906344448UL,
            216739030602088448UL,
            505818229730443264UL,
            1011636459460886528UL,
            2023272918921773056UL,
            4046545837843546112UL,
            8093091675687092224UL,
            16186183351374184448UL,
            13853283560024178688UL,
            144959613005987840UL,
            362258295026614272UL,
            724516590053228544UL,
            1449033180106457088UL,
            2898066360212914176UL,
            5796132720425828352UL,
            11592265440851656704UL,
            4665729213955833856UL
        };

        // Magic numbers for sliding pieces
        public static ulong[] BishopMagics { get; } = new ulong[64]
        {
            0x2020448008100UL,
            0x1820843102002050UL,
            0x8908108112005000UL,
            0x4042088010220UL,
            0x4124504000060141UL,
            0x2001042240440002UL,
            0x104008884100082UL,
            0x61208020210D0UL,
            0x1015060A1092212UL,
            0x10B4080228004900UL,
            0x8200040822004025UL,
            0x2022082044800UL,
            0xA002411140224800UL,
            0x120084802500004UL,
            0x412804606104280UL,
            0xCAB0088088080250UL,
            0x480081010C202UL,
            0x108803602244400UL,
            0x20884A1003820010UL,
            0x44044824001061UL,
            0x2200400A000A0UL,
            0x6000248020802UL,
            0x181020200900400UL,
            0x8802208200842404UL,
            0x120226064040400UL,
            0x4021004284808UL,
            0x4001404094050200UL,
            0x480A040008010820UL,
            0x2194082044002002UL,
            0x2008A20001004200UL,
            0x40908041041004UL,
            0x881002200540404UL,
            0x4001082002082101UL,
            0x8110408880880UL,
            0x8000404040080200UL,
            0x200020082180080UL,
            0x1184440400114100UL,
            0xC220008020110412UL,
            0x4088084040090100UL,
            0x8822104100121080UL,
            0x100111884008200AUL,
            0x2844040288820200UL,
            0x90901088003010UL,
            0x1000A218000400UL,
            0x1102010420204UL,
            0x8414A3483000200UL,
            0x6410849901420400UL,
            0x201080200901040UL,
            0x204880808050002UL,
            0x1001008201210000UL,
            0x16A6300A890040AUL,
            0x8049000441108600UL,
            0x2212002060410044UL,
            0x100086308020020UL,
            0x484241408020421UL,
            0x105084028429C085UL,
            0x4282480801080CUL,
            0x81C098488088240UL,
            0x1400000090480820UL,
            0x4444000030208810UL,
            0x1020142010820200UL,
            0x2234802004018200UL,
            0xC2040450820A00UL,
            0x2101021090020UL
        };
        public static ulong[] RookMagics { get; } = new ulong[64]
        {
            0x8A80104000800020UL,
0x140002000100040UL,
0x2801880A0017001UL,
0x100081001000420UL,
0x200020010080420UL,
0x3001C0002010008UL,
0x8480008002000100UL,
0x2080088004402900UL,
0x800098204000UL,
0x2024401000200040UL,
0x100802000801000UL,
0x120800800801000UL,
0x208808088000400UL,
0x2802200800400UL,
0x2200800100020080UL,
0x801000060821100UL,
0x80044006422000UL,
0x100808020004000UL,
0x12108A0010204200UL,
0x140848010000802UL,
0x481828014002800UL,
0x8094004002004100UL,
0x4010040010010802UL,
0x20008806104UL,
0x100400080208000UL,
0x2040002120081000UL,
0x21200680100081UL,
0x20100080080080UL,
0x2000A00200410UL,
0x20080800400UL,
0x80088400100102UL,
0x80004600042881UL,
0x4040008040800020UL,
0x440003000200801UL,
0x4200011004500UL,
0x188020010100100UL,
0x14800401802800UL,
0x2080040080800200UL,
0x124080204001001UL,
0x200046502000484UL,
0x480400080088020UL,
0x1000422010034000UL,
0x30200100110040UL,
0x100021010009UL,
0x2002080100110004UL,
0x202008004008002UL,
0x20020004010100UL,
0x2048440040820001UL,
0x101002200408200UL,
0x40802000401080UL,
0x4008142004410100UL,
0x2060820C0120200UL,
0x1001004080100UL,
0x20C020080040080UL,
0x2935610830022400UL,
0x44440041009200UL,
0x280001040802101UL,
0x2100190040002085UL,
0x80C0084100102001UL,
0x4024081001000421UL,
0x20030A0244872UL,
0x12001008414402UL,
0x2006104900A0804UL,
0x1004081002402UL
        };

        public static ulong[] BishopMasks { get; set; } = new ulong[64];
        public static ulong[] RookMasks { get; set; } = new ulong[64];

        public static ulong[,] BishopAttacks { get; set; } = new ulong[64, 512];
        public static ulong[,] RookAttacks { get; set; } = new ulong[64, 4096];

        // Occupancy bit counts
        public static int[] BishopOccupancyBitCounts { get; } = new int[64]
        {
            6, 5, 5, 5, 5, 5, 5, 6,
            5, 5, 5, 5, 5, 5, 5, 5,
            5, 5, 7, 7, 7, 7, 5, 5,
            5, 5, 7, 9, 9, 7, 5, 5,
            5, 5, 7, 9, 9, 7, 5, 5,
            5, 5, 7, 7, 7, 7, 5, 5,
            5, 5, 5, 5, 5, 5, 5, 5,
            6, 5, 5, 5, 5, 5, 5, 6
        };

        public static int[] RookOccupancyBitCounts { get; } = new int[64]
        {
            12, 11, 11, 11, 11, 11, 11, 12, 
            11, 10, 10, 10, 10, 10, 10, 11, 
            11, 10, 10, 10, 10, 10, 10, 11, 
            11, 10, 10, 10, 10, 10, 10, 11, 
            11, 10, 10, 10, 10, 10, 10, 11, 
            11, 10, 10, 10, 10, 10, 10, 11, 
            11, 10, 10, 10, 10, 10, 10, 11, 
            12, 11, 11, 11, 11, 11, 11, 12
        };

        // File constants
        public static ulong NotAFile { get; } = 18374403900871474942UL;
        public static ulong NotABFile { get; } = 18229723555195321596UL;
        public static ulong NotHFile { get; } = 9187201950435737471UL;
        public static ulong NotGHFile { get; } = 4557430888798830399UL;

        // https://www.chessprogramming.org/Encoding_Moves for how the flags work
        public static ushort EncodeMove(int from, int to, int promotion, int capture, int special1, int special2)
        {
            ushort move = 0;

            move = (ushort)(from | to << 6 | promotion << 12 | capture << 13 | special1 << 14 | special2 << 15);

            return move;
        }

        public static int[] ParseMove(ushort move)
        {
            int[] moveInfo = new int[6];

            moveInfo[0] = move & 63; // 010111 & 111111 = 010111
            moveInfo[1] = (move & 4032) >> 6; // 110110000000 & 111111000000 = 110110000000, 110110000000 >> 6 = 000000110110
            moveInfo[2] = (move & 4096) >> 12;
            moveInfo[3] = (move & 8192) >> 13;
            moveInfo[4] = (move & 16384) >> 14;
            moveInfo[5] = (move & 32768) >> 15;

            return moveInfo;
        }

        private static void AddMove(ref MoveList moves, ushort move)
        {
            moves.Moves[moves.Count] = move;

            moves.Count++;
        }

        public static void AddKnightMoves(Position position, ref MoveList moves, ulong knightBitboard, bool isWhite, bool capturesOnly)
        {
            int popCount = BitOperations.PopCount(knightBitboard);

            for (int i = 0; i < popCount; i++)
            {
                int from = BitboardUtil.GetLS1BIndex(knightBitboard.IsolateLS1B());
                ulong targets = KnightAttacks[from] & (isWhite ? ~position.WhiteOccupancies : ~position.BlackOccupancies);

                int targetPopCount = BitOperations.PopCount(targets);

                //Console.WriteLine($"From square: {from}");
                //Console.WriteLine($"Target squares: {targets}");

                for (int j = 0; j < targetPopCount; j++)
                {
                    int to = BitboardUtil.GetLS1BIndex(targets.IsolateLS1B());

                    //Console.WriteLine($"To square: {to}");

                    // Capture
                    if (BitboardUtil.GetBit(isWhite ? position.BlackOccupancies : position.WhiteOccupancies, to) == 1)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 0));
                    }
                    // Quiet move
                    else if (!capturesOnly)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 0, 0, 0));
                    }

                    targets = targets.ClearBit(to);
                }

                knightBitboard = knightBitboard.ClearBit(from);
            }
        }

        public static void AddPawnMoves(Position position, ref MoveList moves, ulong pawnBitboard, bool isWhite, bool capturesOnly)
        {
            int popCount = BitOperations.PopCount(pawnBitboard);
            ulong enPassantSquare = isWhite ? position.BlackEnPassantTargetSquare : position.WhiteEnPassantTargetSquare;

            for (int i = 0; i < popCount; i++)
            {
                int from = BitboardUtil.GetLS1BIndex(pawnBitboard.IsolateLS1B());
                int to = isWhite ? from + 8 : from - 8;

                if (position.AllOccupancies.GetBit(to) == 0)
                {
                    //Console.WriteLine($"From: {from}");
                    //Console.WriteLine($"To: {to}");

                    if (!capturesOnly)
                    {
                        if (isWhite)
                        {
                            // White promotions
                            if (from >= 48 && from <= 55)
                            {
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 0, 0)); // Knight promotion
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 0, 1)); // Bishop promotion
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 1, 0)); // Rook promotion
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 1, 1)); // Queen promotion
                            }
                            // Quiet pawn moves
                            else
                            {
                                // Single pawn move
                                AddMove(ref moves, EncodeMove(from, to, 0, 0, 0, 0));

                                // Double pawn move
                                if ((from >= 8 && from <= 15) && position.AllOccupancies.GetBit(to + 8) == 0)
                                {
                                    AddMove(ref moves, EncodeMove(from, to + 8, 0, 0, 0, 1));
                                }
                            }
                        }
                        else
                        {
                            // Black promotions
                            if (from >= 8 && from <= 15)
                            {
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 0, 0)); // Knight promotion
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 0, 1)); // Bishop promotion
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 1, 0)); // Rook promotion
                                AddMove(ref moves, EncodeMove(from, to, 1, 0, 1, 1)); // Queen promotion
                            }
                            // Quiet pawn moves
                            else
                            {
                                // Single pawn move
                                AddMove(ref moves, EncodeMove(from, to, 0, 0, 0, 0));

                                // Double pawn move
                                if ((from >= 48 && from <= 55) && position.AllOccupancies.GetBit(to - 8) == 0)
                                {
                                    AddMove(ref moves, EncodeMove(from, to - 8, 0, 0, 0, 1));
                                }
                            }
                        }
                    }
                }

                ulong targets = (isWhite ? WhitePawnAttacks[from] : BlackPawnAttacks[from]) & (isWhite ? position.BlackOccupancies : position.WhiteOccupancies);
                //Console.WriteLine($"Targets: {targets}");

                int targetPopCount = BitOperations.PopCount(targets);

                for (int j = 0; j < targetPopCount; j++)
                {
                    to = BitboardUtil.GetLS1BIndex(targets.IsolateLS1B());

                    if (isWhite)
                    {
                        // White promotion captures
                        if (from >= 48 && from <= 55)
                        {
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 0, 0)); // Knight promotion
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 0, 1)); // Bishop promotion
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 1, 0)); // Rook promotion
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 1, 1)); // Queen promotion
                        }
                        // Captures
                        else
                        {
                            // Single pawn move
                            AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 0));
                        }
                    }
                    else
                    {
                        // Black promotion captures
                        if (from >= 8 && from <= 15)
                        {
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 0, 0)); // Knight promotion
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 0, 1)); // Bishop promotion
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 1, 0)); // Rook promotion
                            AddMove(ref moves, EncodeMove(from, to, 1, 1, 1, 1)); // Queen promotion
                        }
                        // Captures
                        else
                        {
                            // Single pawn move
                            AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 0));
                        }
                    }

                    targets = targets.ClearBit(to);
                }

                if (enPassantSquare != 0)
                {
                    ulong epTargets = (isWhite ? WhitePawnAttacks[from] : BlackPawnAttacks[from]) & enPassantSquare;

                    //Console.WriteLine($"En passant target: {epTargets}");

                    if (epTargets != 0)
                    {
                        to = BitboardUtil.GetLS1BIndex(epTargets.IsolateLS1B());
                        AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 1));
                    }
                }

                pawnBitboard = pawnBitboard.ClearBit(from);
            }
        }

        public static void AddKingMoves(Position position, ref MoveList moves, ulong kingBitboard, bool isWhite, bool capturesOnly)
        {
            int from = BitboardUtil.GetLS1BIndex(kingBitboard.IsolateLS1B());
            ulong targets = KingAttacks[from] & (isWhite ? ~position.WhiteOccupancies : ~position.BlackOccupancies);

            int targetPopCount = BitOperations.PopCount(targets);

            //Console.WriteLine($"From square: {from}");
            //Console.WriteLine($"Target squares: {targets}");

            for (int j = 0; j < targetPopCount; j++)
            {
                int to = BitboardUtil.GetLS1BIndex(targets.IsolateLS1B());

                //Console.WriteLine($"To square: {to}");

                // Capture
                if (BitboardUtil.GetBit(isWhite ? position.BlackOccupancies : position.WhiteOccupancies, to) == 1)
                {
                    AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 0));
                }
                // Quiet move
                else if (!capturesOnly)
                {
                    AddMove(ref moves, EncodeMove(from, to, 0, 0, 0, 0));
                }

                targets = targets.ClearBit(to);
            }

            kingBitboard = kingBitboard.ClearBit(from);

            if (!capturesOnly)
            {
                // Add white castling moves
                if (isWhite)
                {
                    // White has kingside castling rights (king and king's rook haven't moved yet)
                    if (position.WhiteKingsideCastlingRights == true)
                    {
                        // No pieces are on f1 or g1
                        if ((position.AllOccupancies.GetBit(5) == 0) && (position.AllOccupancies.GetBit(6) == 0))
                        {
                            // Make sure e1 (king's location) and f1 are not under attack by black
                            if (!IsSquareAttacked(position, 4, isWhite: false) && !IsSquareAttacked(position, 5, isWhite: false))
                            {
                                //Console.WriteLine("White castling kingside is pseudo-legal");
                                // Add kingside castling move
                                AddMove(ref moves, EncodeMove(4, 6, 0, 0, 1, 0));
                            }
                        }
                    }

                    // White has queenside castling rights (king and queen's rook haven't moved yet)
                    if (position.WhiteQueensideCastlingRights == true)
                    {
                        // No pieces are on b1, c1, or d1
                        if ((position.AllOccupancies.GetBit(1) == 0)
                            && (position.AllOccupancies.GetBit(2) == 0)
                            && (position.AllOccupancies.GetBit(3) == 0))
                        {
                            // Make sure e1 (king's location) and c1 are not under attack by black
                            if (!IsSquareAttacked(position, 4, isWhite: false) && !IsSquareAttacked(position, 3, isWhite: false))
                            {
                                //Console.WriteLine("White castling queenside is pseudo-legal");
                                // Add queenside castling move
                                AddMove(ref moves, EncodeMove(4, 2, 0, 0, 1, 1));
                            }
                        }
                    }
                }
                // Add black castling moves
                else
                {
                    // Black has kingside castling rights (king and king's rook haven't moved yet)
                    if (position.BlackKingsideCastlingRights == true)
                    {
                        // No pieces are on f8 or g8
                        if ((position.AllOccupancies.GetBit(61) == 0) && (position.AllOccupancies.GetBit(62) == 0))
                        {
                            // Make sure e8 (king's location) and f8 are not under attack by white
                            if (!IsSquareAttacked(position, 60, isWhite: true) && !IsSquareAttacked(position, 61, isWhite: true))
                            {
                                //Console.WriteLine("Black castling kingside is pseudo-legal");
                                // Add kingside castling move
                                AddMove(ref moves, EncodeMove(60, 62, 0, 0, 1, 0));
                            }
                        }
                    }

                    // Black has queenside castling rights (king and queen's rook haven't moved yet)
                    if (position.BlackQueensideCastlingRights == true)
                    {
                        // No pieces are on b8, c8, or d8
                        if ((position.AllOccupancies.GetBit(57) == 0)
                            && (position.AllOccupancies.GetBit(58) == 0)
                            && (position.AllOccupancies.GetBit(59) == 0))
                        {
                            // Make sure e8 (king's location) and c8 are not under attack by black
                            if (!IsSquareAttacked(position, 60, isWhite: true) && !IsSquareAttacked(position, 59, isWhite: true))
                            {
                                //Console.WriteLine("Black castling queenside is pseudo-legal");
                                // Add queenside castling move
                                AddMove(ref moves, EncodeMove(60, 58, 0, 0, 1, 1));
                            }
                        }
                    }
                }
            }
        }

        public static void AddBishopMoves(Position position, ref MoveList moves, ulong bishopBitboard, bool isWhite, bool capturesOnly)
        {
            int popCount = BitOperations.PopCount(bishopBitboard);

            for (int i = 0; i < popCount; i++)
            {
                int from = BitboardUtil.GetLS1BIndex(bishopBitboard.IsolateLS1B());
                ulong targets = GetBishopAttacks(from, position.AllOccupancies) & (isWhite ? ~position.WhiteOccupancies : ~position.BlackOccupancies);

                int targetPopCount = BitOperations.PopCount(targets);

                //Console.WriteLine($"From square: {from}");
                //Console.WriteLine($"Target squares: {targets}");

                for (int j = 0; j < targetPopCount; j++)
                {
                    int to = BitboardUtil.GetLS1BIndex(targets.IsolateLS1B());

                    //Console.WriteLine($"To square: {to}");

                    // Capture
                    if (BitboardUtil.GetBit(isWhite ? position.BlackOccupancies : position.WhiteOccupancies, to) == 1)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 0));
                    }
                    // Quiet move
                    else if (!capturesOnly)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 0, 0, 0));
                    }

                    targets = targets.ClearBit(to);
                }

                bishopBitboard = bishopBitboard.ClearBit(from);
            }
        }

        public static void AddRookMoves(Position position, ref MoveList moves, ulong rookBitboard, bool isWhite, bool capturesOnly)
        {
            int popCount = BitOperations.PopCount(rookBitboard);

            for (int i = 0; i < popCount; i++)
            {
                int from = BitboardUtil.GetLS1BIndex(rookBitboard.IsolateLS1B());
                ulong targets = GetRookAttacks(from, position.AllOccupancies) & (isWhite ? ~position.WhiteOccupancies : ~position.BlackOccupancies);

                int targetPopCount = BitOperations.PopCount(targets);

                //Console.WriteLine($"From square: {from}");
                //Console.WriteLine($"Target squares: {targets}");

                for (int j = 0; j < targetPopCount; j++)
                {
                    int to = BitboardUtil.GetLS1BIndex(targets.IsolateLS1B());

                    //Console.WriteLine($"To square: {to}");

                    // Capture
                    if (BitboardUtil.GetBit(isWhite ? position.BlackOccupancies : position.WhiteOccupancies, to) == 1)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 0));
                    }
                    // Quiet move
                    else if (!capturesOnly)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 0, 0, 0));
                    }

                    targets = targets.ClearBit(to);
                }

                rookBitboard = rookBitboard.ClearBit(from);
            }
        }

        public static void AddQueenMoves(Position position, ref MoveList moves, ulong queenBitboard, bool isWhite, bool capturesOnly)
        {
            int popCount = BitOperations.PopCount(queenBitboard);

            for (int i = 0; i < popCount; i++)
            {
                int from = BitboardUtil.GetLS1BIndex(queenBitboard.IsolateLS1B());
                ulong targets = GetQueenAttacks(from, position.AllOccupancies) & (isWhite ? ~position.WhiteOccupancies : ~position.BlackOccupancies);

                int targetPopCount = BitOperations.PopCount(targets);

                //Console.WriteLine($"From square: {from}");
                //Console.WriteLine($"Target squares: {targets}");

                for (int j = 0; j < targetPopCount; j++)
                {
                    int to = BitboardUtil.GetLS1BIndex(targets.IsolateLS1B());

                    //Console.WriteLine($"To square: {to}");

                    // Capture
                    if (BitboardUtil.GetBit(isWhite ? position.BlackOccupancies : position.WhiteOccupancies, to) == 1)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 1, 0, 0));
                    }
                    // Quiet move
                    else if (!capturesOnly)
                    {
                        AddMove(ref moves, EncodeMove(from, to, 0, 0, 0, 0));
                    }

                    targets = targets.ClearBit(to);
                }

                queenBitboard = queenBitboard.ClearBit(from);
            }
        }

        public static bool IsSquareAttacked(Position position, int square, bool isWhite)
        {
            if (isWhite && ((BlackPawnAttacks[square] & position.WhitePawns) > 0)) return true;
            if (!isWhite && ((WhitePawnAttacks[square] & position.BlackPawns) > 0)) return true;
            if ((KnightAttacks[square] & (isWhite ? position.WhiteKnights : position.BlackKnights)) > 0) return true;
            if ((GetBishopAttacks(square, position.AllOccupancies)
                & (isWhite ? position.WhiteBishops : position.BlackBishops)) > 0) return true;
            if ((GetRookAttacks(square, position.AllOccupancies)
                & (isWhite ? position.WhiteRooks : position.BlackRooks)) > 0) return true;
            if ((GetQueenAttacks(square, position.AllOccupancies)
                & (isWhite ? position.WhiteQueens : position.BlackQueens)) > 0) return true;
            if ((KingAttacks[square] & (isWhite ? position.WhiteKing : position.BlackKing)) > 0) return true;

            return false;
        }

        public static void GenerateLegalMoves(Position position, ref MoveList moveList)
        {
            if (position.WhiteToMove == true)
            {
                AddPawnMoves(position, ref moveList, position.WhitePawns, true, false);
                AddKnightMoves(position, ref moveList, position.WhiteKnights, true, false);
                AddBishopMoves(position, ref moveList, position.WhiteBishops, true, false);
                AddRookMoves(position, ref moveList, position.WhiteRooks, true, false);
                AddQueenMoves(position, ref moveList, position.WhiteQueens, true, false);
                AddKingMoves(position, ref moveList, position.WhiteKing, true, false);
            }
            else
            {
                AddPawnMoves(position, ref moveList, position.BlackPawns, false, false);
                AddKnightMoves(position, ref moveList, position.BlackKnights, false, false);
                AddBishopMoves(position, ref moveList, position.BlackBishops, false, false);
                AddRookMoves(position, ref moveList, position.BlackRooks, false, false);
                AddQueenMoves(position, ref moveList, position.BlackQueens, false, false);
                AddKingMoves(position, ref moveList, position.BlackKing, false, false);
            }
        }

        public static void GenerateLegalCaptures(Position position, ref MoveList moveList)
        {
            if (position.WhiteToMove == true)
            {
                AddPawnMoves(position, ref moveList, position.WhitePawns, true, true);
                AddKnightMoves(position, ref moveList, position.WhiteKnights, true, true);
                AddBishopMoves(position, ref moveList, position.WhiteBishops, true, true);
                AddRookMoves(position, ref moveList, position.WhiteRooks, true, true);
                AddQueenMoves(position, ref moveList, position.WhiteQueens, true, true);
                AddKingMoves(position, ref moveList, position.WhiteKing, true, true);
            }
            else
            {
                AddPawnMoves(position, ref moveList, position.BlackPawns, false, true);
                AddKnightMoves(position, ref moveList, position.BlackKnights, false, true);
                AddBishopMoves(position, ref moveList, position.BlackBishops, false, true);
                AddRookMoves(position, ref moveList, position.BlackRooks, false, true);
                AddQueenMoves(position, ref moveList, position.BlackQueens, false, true);
                AddKingMoves(position, ref moveList, position.BlackKing, false, true);
            }
        }

        public static bool MakeMove(ref Position position, ushort move)
        {
            // Push current board position into copied positions stack
            //CopiedPositions.Positions.Push(position);
            Position copiedPosition = position;

            // Parse move
            int[] moveInfo = ParseMove(move);

            int fromSquare = moveInfo[0];
            int toSquare = moveInfo[1];
            int promotion = moveInfo[2];
            int capture = moveInfo[3];
            int special1 = moveInfo[4];
            int special2 = moveInfo[5];

            //Console.WriteLine($"From square: {fromSquare}");
            //Console.WriteLine($"To square: {toSquare}");

            if (position.WhiteKingsideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.WhiteKingsideCastlingRights;
            }
            if (position.WhiteQueensideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.WhiteQueensideCastlingRights;
            }
            if (position.BlackKingsideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.BlackKingsideCastlingRights;
            }
            if (position.BlackQueensideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.BlackQueensideCastlingRights;
            }

            // Increment halfmove clock
            position.HalfmoveClock++;

            if (position.WhiteToMove == true)
            {
                // Move white piece
                if (position.WhitePawns.GetBit(fromSquare) == 1)
                {
                    position.WhitePawns = position.WhitePawns.ClearBit(fromSquare);
                    position.WhitePawns = position.WhitePawns.SetBit(toSquare);

                    position.HashKey ^= Zobrist.WhitePawns[fromSquare];
                    position.HashKey ^= Zobrist.WhitePawns[toSquare];

                    // A pawn moved, so reset halfmove clock
                    position.HalfmoveClock = 0;
                }
                else if (position.WhiteKnights.GetBit(fromSquare) == 1)
                {
                    position.WhiteKnights = position.WhiteKnights.ClearBit(fromSquare);
                    position.WhiteKnights = position.WhiteKnights.SetBit(toSquare);

                    position.HashKey ^= Zobrist.WhiteKnights[fromSquare];
                    position.HashKey ^= Zobrist.WhiteKnights[toSquare];
                }
                else if (position.WhiteBishops.GetBit(fromSquare) == 1)
                {
                    position.WhiteBishops = position.WhiteBishops.ClearBit(fromSquare);
                    position.WhiteBishops = position.WhiteBishops.SetBit(toSquare);

                    position.HashKey ^= Zobrist.WhiteBishops[fromSquare];
                    position.HashKey ^= Zobrist.WhiteBishops[toSquare];
                }
                else if (position.WhiteRooks.GetBit(fromSquare) == 1)
                {
                    position.WhiteRooks = position.WhiteRooks.ClearBit(fromSquare);
                    position.WhiteRooks = position.WhiteRooks.SetBit(toSquare);

                    position.HashKey ^= Zobrist.WhiteRooks[fromSquare];
                    position.HashKey ^= Zobrist.WhiteRooks[toSquare];

                    // Remove appropriate castling rights if rook has moved from its original square
                    if (fromSquare == 7 && position.WhiteKingsideCastlingRights == true)
                    {
                        position.WhiteKingsideCastlingRights = false;
                    }

                    if (fromSquare == 0 && position.WhiteQueensideCastlingRights == true)
                    {
                        position.WhiteQueensideCastlingRights = false;
                    }
                }
                else if (position.WhiteQueens.GetBit(fromSquare) == 1)
                {
                    position.WhiteQueens = position.WhiteQueens.ClearBit(fromSquare);
                    position.WhiteQueens = position.WhiteQueens.SetBit(toSquare);

                    position.HashKey ^= Zobrist.WhiteQueens[fromSquare];
                    position.HashKey ^= Zobrist.WhiteQueens[toSquare];
                }
                else if (position.WhiteKing.GetBit(fromSquare) == 1)
                {
                    position.WhiteKing = position.WhiteKing.ClearBit(fromSquare);
                    position.WhiteKing = position.WhiteKing.SetBit(toSquare);

                    position.HashKey ^= Zobrist.WhiteKings[fromSquare];
                    position.HashKey ^= Zobrist.WhiteKings[toSquare];

                    // White king moved, so remove all of white's castling rights
                    position.WhiteKingsideCastlingRights = false;
                    position.WhiteQueensideCastlingRights = false;
                }

                // Update white occupancies
                position.WhiteOccupancies = position.WhiteOccupancies.ClearBit(fromSquare);
                position.WhiteOccupancies = position.WhiteOccupancies.SetBit(toSquare);
            }
            else
            {
                // Move black piece
                if (position.BlackPawns.GetBit(fromSquare) == 1)
                {
                    position.BlackPawns = position.BlackPawns.ClearBit(fromSquare);
                    position.BlackPawns = position.BlackPawns.SetBit(toSquare);

                    position.HashKey ^= Zobrist.BlackPawns[fromSquare];
                    position.HashKey ^= Zobrist.BlackPawns[toSquare];

                    // A pawn moved, so reset halfmove clock
                    position.HalfmoveClock = 0;
                }
                else if (position.BlackKnights.GetBit(fromSquare) == 1)
                {
                    position.BlackKnights = position.BlackKnights.ClearBit(fromSquare);
                    position.BlackKnights = position.BlackKnights.SetBit(toSquare);

                    position.HashKey ^= Zobrist.BlackKnights[fromSquare];
                    position.HashKey ^= Zobrist.BlackKnights[toSquare];
                }
                else if (position.BlackBishops.GetBit(fromSquare) == 1)
                {
                    position.BlackBishops = position.BlackBishops.ClearBit(fromSquare);
                    position.BlackBishops = position.BlackBishops.SetBit(toSquare);

                    position.HashKey ^= Zobrist.BlackBishops[fromSquare];
                    position.HashKey ^= Zobrist.BlackBishops[toSquare];
                }
                else if (position.BlackRooks.GetBit(fromSquare) == 1)
                {
                    position.BlackRooks = position.BlackRooks.ClearBit(fromSquare);
                    position.BlackRooks = position.BlackRooks.SetBit(toSquare);

                    position.HashKey ^= Zobrist.BlackRooks[fromSquare];
                    position.HashKey ^= Zobrist.BlackRooks[toSquare];

                    // Remove appropriate castling rights if rook has moved from its original square
                    if (fromSquare == 63 && position.BlackKingsideCastlingRights == true)
                    {
                        position.BlackKingsideCastlingRights = false;
                    }

                    if (fromSquare == 56 && position.BlackQueensideCastlingRights == true)
                    {
                        position.BlackQueensideCastlingRights = false;
                    }
                }
                else if (position.BlackQueens.GetBit(fromSquare) == 1)
                {
                    position.BlackQueens = position.BlackQueens.ClearBit(fromSquare);
                    position.BlackQueens = position.BlackQueens.SetBit(toSquare);

                    position.HashKey ^= Zobrist.BlackQueens[fromSquare];
                    position.HashKey ^= Zobrist.BlackQueens[toSquare];
                }
                else if (position.BlackKing.GetBit(fromSquare) == 1)
                {
                    position.BlackKing = position.BlackKing.ClearBit(fromSquare);
                    position.BlackKing = position.BlackKing.SetBit(toSquare);

                    position.HashKey ^= Zobrist.BlackKings[fromSquare];
                    position.HashKey ^= Zobrist.BlackKings[toSquare];

                    // Black king moved, so remove all of black's castling rights
                    position.BlackKingsideCastlingRights = false;
                    position.BlackQueensideCastlingRights = false;
                }

                // Update black occupancies
                position.BlackOccupancies = position.BlackOccupancies.ClearBit(fromSquare);
                position.BlackOccupancies = position.BlackOccupancies.SetBit(toSquare);
            }

            // Handle captures
            if (capture == 1)
            {
                // A piece was captured, so reset halfmove clock
                position.HalfmoveClock = 0;

                // Check every black bitboard to see which piece was captured
                if (position.WhiteToMove == true)
                {
                    if (position.BlackPawns.GetBit(toSquare) == 1)
                    {
                        position.BlackPawns = position.BlackPawns.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.BlackPawns[toSquare];
                    }
                    else if (position.BlackKnights.GetBit(toSquare) == 1)
                    {
                        position.BlackKnights = position.BlackKnights.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.BlackKnights[toSquare];
                    }
                    else if (position.BlackBishops.GetBit(toSquare) == 1)
                    {
                        position.BlackBishops = position.BlackBishops.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.BlackBishops[toSquare];
                    }
                    else if (position.BlackRooks.GetBit(toSquare) == 1)
                    {
                        position.BlackRooks = position.BlackRooks.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.BlackRooks[toSquare];

                        // If a rook on its original square has been captured, remove appropriate castling right
                        if (toSquare == 63 && position.BlackKingsideCastlingRights == true)
                        {
                            position.BlackKingsideCastlingRights = false;
                        }

                        if (toSquare == 56 && position.BlackQueensideCastlingRights == true)
                        {
                            position.BlackQueensideCastlingRights = false;
                        }
                    }
                    else if (position.BlackQueens.GetBit(toSquare) == 1)
                    {
                        position.BlackQueens = position.BlackQueens.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.BlackQueens[toSquare];
                    }
                    //else if (position.BlackKing.GetBit(toSquare) == 1)
                    //{
                    //    position.BlackKing = position.BlackKing.ClearBit(toSquare);
                    //}

                    // Update black occupancies
                    position.BlackOccupancies = position.BlackOccupancies.ClearBit(toSquare);
                }
                // Check every white bitboard to see which piece was captured
                else
                {
                    if (position.WhitePawns.GetBit(toSquare) == 1)
                    {
                        position.WhitePawns = position.WhitePawns.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.WhitePawns[toSquare];
                    }
                    else if (position.WhiteKnights.GetBit(toSquare) == 1)
                    {
                        position.WhiteKnights = position.WhiteKnights.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.WhiteKnights[toSquare];
                    }
                    else if (position.WhiteBishops.GetBit(toSquare) == 1)
                    {
                        position.WhiteBishops = position.WhiteBishops.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.WhiteBishops[toSquare];
                    }
                    else if (position.WhiteRooks.GetBit(toSquare) == 1)
                    {
                        position.WhiteRooks = position.WhiteRooks.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.WhiteRooks[toSquare];

                        // If a rook on its original square has been captured, remove appropriate castling right
                        if (toSquare == 7 && position.WhiteKingsideCastlingRights == true)
                        {
                            position.WhiteKingsideCastlingRights = false;
                        }

                        if (toSquare == 0 && position.WhiteQueensideCastlingRights == true)
                        {
                            position.WhiteQueensideCastlingRights = false;
                        }
                    }
                    else if (position.WhiteQueens.GetBit(toSquare) == 1)
                    {
                        position.WhiteQueens = position.WhiteQueens.ClearBit(toSquare);

                        position.HashKey ^= Zobrist.WhiteQueens[toSquare];
                    }

                    // Update white occupancies
                    position.WhiteOccupancies = position.WhiteOccupancies.ClearBit(toSquare);
                }
            }

            // Handle pawn promotions
            if (promotion == 1)
            {
                if (position.WhiteToMove == true)
                {
                    // Remove pawn from position
                    position.WhitePawns = position.WhitePawns.ClearBit(toSquare);
                    position.HashKey ^= Zobrist.WhitePawns[toSquare];

                    // Add promoted piece based on special1 and special2 flags
                    if (special1 == 0 && special2 == 0)
                    {
                        position.WhiteKnights = position.WhiteKnights.SetBit(toSquare);
                        position.HashKey ^= Zobrist.WhiteKnights[toSquare];
                    }
                    else if (special1 == 0 && special2 == 1)
                    {
                        position.WhiteBishops = position.WhiteBishops.SetBit(toSquare);
                        position.HashKey ^= Zobrist.WhiteBishops[toSquare];
                    }
                    else if (special1 == 1 && special2 == 0)
                    {
                        position.WhiteRooks = position.WhiteRooks.SetBit(toSquare);
                        position.HashKey ^= Zobrist.WhiteRooks[toSquare];
                    }
                    else if (special1 == 1 && special2 == 1)
                    {
                        position.WhiteQueens = position.WhiteQueens.SetBit(toSquare);
                        position.HashKey ^= Zobrist.WhiteQueens[toSquare];
                    }
                }
                else
                {
                    // Remove pawn from position
                    position.BlackPawns = position.BlackPawns.ClearBit(toSquare);
                    position.HashKey ^= Zobrist.BlackPawns[toSquare];

                    // Add promoted piece based on special1 and special2 flags
                    if (special1 == 0 && special2 == 0)
                    {
                        position.BlackKnights = position.BlackKnights.SetBit(toSquare);
                        position.HashKey ^= Zobrist.BlackKnights[toSquare];
                    }
                    else if (special1 == 0 && special2 == 1)
                    {
                        position.BlackBishops = position.BlackBishops.SetBit(toSquare);
                        position.HashKey ^= Zobrist.BlackBishops[toSquare];
                    }
                    else if (special1 == 1 && special2 == 0)
                    {
                        position.BlackRooks = position.BlackRooks.SetBit(toSquare);
                        position.HashKey ^= Zobrist.BlackRooks[toSquare];
                    }
                    else if (special1 == 1 && special2 == 1)
                    {
                        position.BlackQueens = position.BlackQueens.SetBit(toSquare);
                        position.HashKey ^= Zobrist.BlackQueens[toSquare];
                    }
                }
            }

            // Handle en passant captures
            if (capture == 1 && special2 == 1 && special1 == 0 && promotion == 0)
            {
                if (position.WhiteToMove == true)
                {
                    // Remove captured pawn
                    position.BlackPawns = position.BlackPawns.ClearBit(toSquare - 8);
                    position.HashKey ^= Zobrist.BlackPawns[toSquare - 8];

                    // Update black occupancies
                    position.BlackOccupancies = position.BlackOccupancies.ClearBit(toSquare - 8);
                }
                else
                {
                    // Remove captured pawn
                    position.WhitePawns = position.WhitePawns.ClearBit(toSquare + 8);
                    position.HashKey ^= Zobrist.WhitePawns[toSquare + 8];

                    // Update white occupancies
                    position.WhiteOccupancies = position.WhiteOccupancies.ClearBit(toSquare + 8);
                }
            }

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

            // Reset all en passant target squares
            position.WhiteEnPassantTargetSquare = 0;
            position.BlackEnPassantTargetSquare = 0;

            // Handle double pawn pushes
            if (special2 == 1 && promotion == 0 && capture == 0 && special1 == 0)
            {
                if (position.WhiteToMove == true)
                {
                    // Set white en passant target square
                    position.WhiteEnPassantTargetSquare = position.WhiteEnPassantTargetSquare.SetBit(toSquare - 8);

                    position.HashKey ^= Zobrist.EnPassantFiles[(toSquare - 8) % 8];
                }
                else
                {
                    // Set black en passant target square
                    position.BlackEnPassantTargetSquare = position.BlackEnPassantTargetSquare.SetBit(toSquare + 8);

                    position.HashKey ^= Zobrist.EnPassantFiles[(toSquare + 8) % 8];
                }
            }

            // Handle queenside castling
            if (special1 == 1 && special2 == 1 && promotion == 0 && capture == 0)
            {
                if (position.WhiteToMove == true)
                {
                    // Move A1 rook
                    position.WhiteRooks = position.WhiteRooks.ClearBit(0);
                    position.WhiteRooks = position.WhiteRooks.SetBit(3);

                    position.HashKey ^= Zobrist.WhiteRooks[0];
                    position.HashKey ^= Zobrist.WhiteRooks[3];

                    // Update white occupancies
                    position.WhiteOccupancies = position.WhiteOccupancies.ClearBit(0);
                    position.WhiteOccupancies = position.WhiteOccupancies.SetBit(3);
                }
                else
                {
                    // Move A8 rook
                    position.BlackRooks = position.BlackRooks.ClearBit(56);
                    position.BlackRooks = position.BlackRooks.SetBit(59);

                    position.HashKey ^= Zobrist.BlackRooks[56];
                    position.HashKey ^= Zobrist.BlackRooks[59];

                    // Update black occupancies
                    position.BlackOccupancies = position.BlackOccupancies.ClearBit(56);
                    position.BlackOccupancies = position.BlackOccupancies.SetBit(59);
                }
            }

            // Handle kingside castling
            if (special1 == 1 && special2 == 0 && promotion == 0 && capture == 0)
            {
                if (position.WhiteToMove == true)
                {
                    // Move H1 rook
                    position.WhiteRooks = position.WhiteRooks.ClearBit(7);
                    position.WhiteRooks = position.WhiteRooks.SetBit(5);

                    position.HashKey ^= Zobrist.WhiteRooks[7];
                    position.HashKey ^= Zobrist.WhiteRooks[5];

                    // Update white occupancies
                    position.WhiteOccupancies = position.WhiteOccupancies.ClearBit(7);
                    position.WhiteOccupancies = position.WhiteOccupancies.SetBit(5);
                }
                else
                {
                    // Move H8 rook
                    position.BlackRooks = position.BlackRooks.ClearBit(63);
                    position.BlackRooks = position.BlackRooks.SetBit(61);

                    position.HashKey ^= Zobrist.BlackRooks[63];
                    position.HashKey ^= Zobrist.BlackRooks[61];

                    // Update black occupancies
                    position.BlackOccupancies = position.BlackOccupancies.ClearBit(63);
                    position.BlackOccupancies = position.BlackOccupancies.SetBit(61);
                }
            }

            if (position.WhiteKingsideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.WhiteKingsideCastlingRights;
            }
            if (position.WhiteQueensideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.WhiteQueensideCastlingRights;
            }
            if (position.BlackKingsideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.BlackKingsideCastlingRights;
            }
            if (position.BlackQueensideCastlingRights == true)
            {
                position.HashKey ^= Zobrist.BlackQueensideCastlingRights;
            }

            position.AllOccupancies = position.WhiteOccupancies;
            position.AllOccupancies += position.BlackOccupancies;

            //if (IsSquareAttacked(position, position.WhiteToMove ? BitboardUtil.GetLS1BIndex(position.WhiteKing.IsolateLS1B()) : BitboardUtil.GetLS1BIndex(position.BlackKing.IsolateLS1B()), !position.WhiteToMove))
            //{
            //    position = copiedPosition;
            //    //position = CopiedPositions.Positions.Pop();

            //    // Return illegal move
            //    return false;
            //}
            //else
            //{
            //    //CopiedPositions.Positions.Pop();

            //    // Return legal move
            //    return true;
            //}

            // Change side to move
            //position.WhiteToMove = !position.WhiteToMove;
            position.WhiteToMove = position.WhiteToMove != true;
            position.HashKey ^= Zobrist.Side;

            //Console.WriteLine($"Incrementally updated hash: {position.HashKey}");

            ulong hashFromScratch = Zobrist.GenerateHashKey(position);
            //Console.WriteLine($"Hash from scratch:          {hashFromScratch}");

            if (position.HashKey != hashFromScratch)
            {
                Console.WriteLine("Incrementally updated hash key and hash from scratch don't match");
                Console.ReadLine();
            }

            //return true;

            //Console.WriteLine(position.WhiteToMove);

            if (IsSquareAttacked(position, position.WhiteToMove ? BitboardUtil.GetLS1BIndex(position.BlackKing.IsolateLS1B()) : BitboardUtil.GetLS1BIndex(position.WhiteKing.IsolateLS1B()), position.WhiteToMove))
            {
                position = copiedPosition;
                //position = CopiedPositions.Positions.Pop();

                // Return illegal move
                return false;
            }
            else
            {
                //CopiedPositions.Positions.Pop();

                // Return legal move
                return true;
            }
        }

        public static void ComputeKnightMoves()
        {
            for (int i = 0; i < 64; i++)
            {
                ulong bitboard = 0;
                ulong newBitboard = 0;

                bitboard = bitboard.SetBit(i);
                newBitboard =  (bitboard << 17) & NotAFile ;
                newBitboard += (bitboard << 10) & NotABFile;
                newBitboard += (bitboard >>  6) & NotABFile;
                newBitboard += (bitboard >> 15) & NotAFile ;
                newBitboard += (bitboard << 15) & NotHFile ;
                newBitboard += (bitboard <<  6) & NotGHFile;
                newBitboard += (bitboard >> 10) & NotGHFile;
                newBitboard += (bitboard >> 17) & NotHFile ;

                Console.WriteLine($"{newBitboard}UL,");
            }
        }

        private static void ComputePawnMoves()
        {
            for (int i = 0; i < 64; i++)
            {
                ulong bitboard = 0;
                ulong newBitboard = 0;

                bitboard = bitboard.SetBit(i);
                newBitboard += (bitboard << 7) & NotHFile;
                newBitboard += (bitboard << 9) & NotAFile;

                Console.WriteLine($"{newBitboard}UL,");
            }

            Console.WriteLine();

            for (int i = 0; i < 64; i++)
            {
                ulong bitboard = 0;
                ulong newBitboard = 0;

                bitboard = bitboard.SetBit(i);
                newBitboard += (bitboard >> 7) & NotAFile;
                newBitboard += (bitboard >> 9) & NotHFile;

                Console.WriteLine($"{newBitboard}UL,");
            }
        }

        private static void ComputeKingMoves()
        {
            for (int i = 0; i < 64; i++)
            {
                ulong bitboard = 0;
                ulong newBitboard = 0;

                bitboard = bitboard.SetBit(i);
                newBitboard += bitboard >> 8;
                newBitboard += bitboard << 8;
                newBitboard += (bitboard >> 9) & NotHFile;
                newBitboard += (bitboard << 9) & NotAFile;
                newBitboard += (bitboard >> 7) & NotAFile;
                newBitboard += (bitboard << 7) & NotHFile;
                newBitboard += (bitboard >> 1) & NotHFile;
                newBitboard += (bitboard << 1) & NotAFile;

                Console.WriteLine($"{newBitboard}UL,");
            }
        }

        public static ulong MaskBishopOccupancyBits(int square)
        {
            ulong attacks = 0;

            int rank;
            int file;

            int targetRank = square / 8;
            int targetFile = square % 8;

            for (rank = targetRank + 1, file = targetFile + 1; rank <= 6 && file <= 6; rank++, file++)
            {
                attacks = attacks.SetBit(file + (rank * 8));
            }

            for (rank = targetRank - 1, file = targetFile + 1; rank >= 1 && file <= 6; rank--, file++)
            {
                attacks = attacks.SetBit(file + (rank * 8));
            }

            for (rank = targetRank + 1, file = targetFile - 1; rank <= 6 && file >= 1; rank++, file--)
            {
                attacks = attacks.SetBit(file + (rank * 8));
            }

            for (rank = targetRank - 1, file = targetFile - 1; rank >= 1 && file >= 1; rank--, file--)
            {
                attacks = attacks.SetBit(file + (rank * 8));
            }

            return attacks;
        }

        public static ulong ComputeBishopAttacks(int square, ulong occupancy)
        {
            ulong attacks = 0;

            int rank;
            int file;

            int targetRank = square / 8;
            int targetFile = square % 8;

            for (rank = targetRank + 1, file = targetFile + 1; rank <= 7 && file <= 7; rank++, file++)
            {
                attacks = attacks.SetBit(file + (rank * 8));

                if (occupancy.GetBit(file + (rank * 8)) == 1)
                {
                    break;
                }
            }

            for (rank = targetRank - 1, file = targetFile + 1; rank >= 0 && file <= 7; rank--, file++)
            {
                attacks = attacks.SetBit(file + (rank * 8));

                if (occupancy.GetBit(file + (rank * 8)) == 1)
                {
                    break;
                }
            }

            for (rank = targetRank + 1, file = targetFile - 1; rank <= 7 && file >= 0; rank++, file--)
            {
                attacks = attacks.SetBit(file + (rank * 8));

                if (occupancy.GetBit(file + (rank * 8)) == 1)
                {
                    break;
                }
            }

            for (rank = targetRank - 1, file = targetFile - 1; rank >= 0 && file >= 0; rank--, file--)
            {
                attacks = attacks.SetBit(file + (rank * 8));

                if (occupancy.GetBit(file + (rank * 8)) == 1)
                {
                    break;
                }
            }

            return attacks;
        }

        public static ulong MaskRookOccupancyBits(int square)
        {
            ulong attacks = 0;

            int rank;
            int file;

            int targetRank = square / 8;
            int targetFile = square % 8;

            for (file = targetFile + 1; file <= 6; file++)
            {
                attacks = attacks.SetBit(file + (targetRank * 8));
            }

            for (file = targetFile - 1; file >= 1; file--)
            {
                attacks = attacks.SetBit(file + (targetRank * 8));
            }

            for (rank = targetRank + 1; rank <= 6; rank++)
            {
                attacks = attacks.SetBit(targetFile + (rank * 8));
            }

            for (rank = targetRank - 1; rank >= 1; rank--)
            {
                attacks = attacks.SetBit(targetFile + (rank * 8));
            }

            return attacks;
        }

        public static ulong ComputeRookAttacks(int square, ulong occupancy)
        {
            ulong attacks = 0;

            int rank;
            int file;

            int targetRank = square / 8;
            int targetFile = square % 8;

            for (file = targetFile + 1; file <= 7; file++)
            {
                attacks = attacks.SetBit(file + targetRank * 8);

                if (occupancy.GetBit(file + targetRank * 8) == 1)
                {
                    break;
                }
            }

            for (file = targetFile - 1; file >= 0; file--)
            {
                attacks = attacks.SetBit(file + targetRank * 8);

                if (occupancy.GetBit(file + targetRank * 8) == 1)
                {
                    break;
                }
            }

            for (rank = targetRank + 1; rank <= 7; rank++)
            {
                attacks = attacks.SetBit(targetFile + rank * 8);

                if (occupancy.GetBit(targetFile + rank * 8) == 1)
                {
                    break;
                }
            }

            for (rank = targetRank - 1; rank >= 0; rank--)
            {
                attacks = attacks.SetBit(targetFile + rank * 8);

                if (occupancy.GetBit(targetFile + rank * 8) == 1)
                {
                    break;
                }
            }

            return attacks;
        }

        public static ulong SetOccupancy(int index, int popCount, ulong attackMask)
        {
            ulong occupancy = 0UL;

            for (int i = 0; i < popCount; i++)
            {
                int square = BitboardUtil.GetLS1BIndex(attackMask.IsolateLS1B());

                attackMask = attackMask.ClearBit(square);

                if ((index & (1 << i)) > 0)
                {
                    occupancy |= (1UL << square);
                }
            }

            return occupancy;
        }

        public static void InitSliderAttackTables(bool isBishop)
        {
            for (int i = 0; i < 64; i++)
            {
                BishopMasks[i] = MaskBishopOccupancyBits(i);
                RookMasks[i] = MaskRookOccupancyBits(i);

                ulong attackMask = isBishop ? BishopMasks[i] : RookMasks[i];

                int popCount = BitOperations.PopCount(attackMask);

                int numOccupancies = 1 << popCount;

                for (int j = 0; j < numOccupancies; j++)
                {
                    if (isBishop)
                    {
                        ulong occupancy = SetOccupancy(j, popCount, attackMask);

                        int magicIndex = (int)((occupancy * BishopMagics[i]) >> (64 - BishopOccupancyBitCounts[i]));

                        BishopAttacks[i, magicIndex] = ComputeBishopAttacks(i, occupancy);
                    }
                    else
                    {
                        ulong occupancy = SetOccupancy(j, popCount, attackMask);

                        int magicIndex = (int)((occupancy * RookMagics[i]) >> (64 - RookOccupancyBitCounts[i]));

                        RookAttacks[i, magicIndex] = ComputeRookAttacks(i, occupancy);
                    }
                }
            }
        }

        public static ulong GetBishopAttacks(int square, ulong occupancy)
        {
            occupancy &= BishopMasks[square];
            occupancy *= BishopMagics[square];
            occupancy >>= 64 - BishopOccupancyBitCounts[square];

            return BishopAttacks[square, occupancy];
        }

        public static ulong GetRookAttacks(int square, ulong occupancy)
        {
            occupancy &= RookMasks[square];
            occupancy *= RookMagics[square];
            occupancy >>= 64 - RookOccupancyBitCounts[square];

            return RookAttacks[square, occupancy];
        }

        public static ulong GetQueenAttacks(int square, ulong occupancy)
        {
            ulong queenAttacks = 0UL;

            ulong bishopOccupancy = occupancy;
            ulong rookOccupancy = occupancy;

            bishopOccupancy &= BishopMasks[square];
            bishopOccupancy *= BishopMagics[square];
            bishopOccupancy >>= 64 - BishopOccupancyBitCounts[square];

            queenAttacks = BishopAttacks[square, bishopOccupancy];

            rookOccupancy &= RookMasks[square];
            rookOccupancy *= RookMagics[square];
            rookOccupancy >>= 64 - RookOccupancyBitCounts[square];

            queenAttacks |= RookAttacks[square, rookOccupancy];

            return queenAttacks;
        }

        public static ulong FindMagicNumber(int square, int relevantBits, bool isBishop)
        {
            ulong[] occupancies = new ulong[4096];
            ulong[] attacks = new ulong[4096];
            ulong[] usedAttacks = new ulong[4096];

            ulong attackMask = isBishop ? MaskBishopOccupancyBits(square) : MaskRookOccupancyBits(square);

            int numOccupancies = 1 << relevantBits;

            for (int i = 0; i < numOccupancies; i++)
            {
                occupancies[i] = SetOccupancy(i, relevantBits, attackMask);

                attacks[i] = isBishop ? ComputeBishopAttacks(square, occupancies[i]) : ComputeRookAttacks(square, occupancies[i]);
            }

            for (int i = 0; i < 100000000; i++)
            {
                ulong magicNumber = Random.GetULongFewBits();

                if (BitOperations.PopCount((attackMask * magicNumber) & 0xFF00000000000000) < 6) continue;

                for (int j = 0; j < 4096; j++)
                {
                    usedAttacks[j] = 0UL;
                }

                int k;
                bool fail;

                for (k = 0, fail = false; !fail && k < numOccupancies; k++)
                {
                    int magicIndex = (int)((occupancies[k] * magicNumber) >> (64 - relevantBits));

                    if (usedAttacks[magicIndex] == 0UL)
                    {
                        usedAttacks[magicIndex] = attacks[k];
                    }
                    else if (usedAttacks[magicIndex] != attacks[k])
                    {
                        fail = true;
                    }
                }

                if (!fail)
                {
                    return magicNumber;
                }
            }

            Console.WriteLine("Magic number failed");
            return 0UL;
        }

        public static void ComputeMagicNumbers()
        {
            Console.WriteLine("Rooks:");

            for (int i = 0; i < 64; i++)
            {
                Console.WriteLine($"0x{FindMagicNumber(i, RookOccupancyBitCounts[i], false):X}UL,");
            }

            Console.WriteLine();

            Console.WriteLine("Bishops:");

            for (int i = 0; i < 64; i++)
            {
                Console.WriteLine($"0x{FindMagicNumber(i, BishopOccupancyBitCounts[i], true):X}UL,");
            }
        }
    }
}
