using SecondChessEngine;
using System.Numerics;

//Position startingPosition = FEN.LoadPositionFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

//Console.WriteLine($"Black pawns: {startingPosition.BlackPawns}");
//Console.WriteLine($"Black knights: {startingPosition.BlackKnights}");
//Console.WriteLine($"Black bishops: {startingPosition.BlackBishops}");
//Console.WriteLine($"Black rooks: {startingPosition.BlackRooks}");
//Console.WriteLine($"Black queens: {startingPosition.BlackQueens}");
//Console.WriteLine($"Black king: {startingPosition.BlackKing}");

//Console.WriteLine();

//Console.WriteLine($"White pawns: {startingPosition.WhitePawns}");
//Console.WriteLine($"White knights: {startingPosition.WhiteKnights}");
//Console.WriteLine($"White bishops: {startingPosition.WhiteBishops}");
//Console.WriteLine($"White rooks: {startingPosition.WhiteRooks}");
//Console.WriteLine($"White queens: {startingPosition.WhiteQueens}");
//Console.WriteLine($"White king: {startingPosition.WhiteKing}");

//Console.WriteLine();

//Console.WriteLine($"White to move: {startingPosition.WhiteToMove}");

//Console.WriteLine();

//Console.WriteLine($"White kingside castling rights: {startingPosition.WhiteKingsideCastlingRights}");
//Console.WriteLine($"White queenside castling rights: {startingPosition.WhiteQueensideCastlingRights}");
//Console.WriteLine($"Black kingside castling rights: {startingPosition.BlackKingsideCastlingRights}");
//Console.WriteLine($"Black queenside castling rights: {startingPosition.BlackQueensideCastlingRights}");

//Console.WriteLine();

//Console.WriteLine($"White en passant target square: {startingPosition.WhiteEnPassantTargetSquare}");
//Console.WriteLine($"Black en passant target square: {startingPosition.BlackEnPassantTargetSquare}");

//Console.WriteLine();

//Console.WriteLine($"Halfmove clock: {startingPosition.HalfmoveClock}");

//Console.WriteLine();

//Console.WriteLine($"White occupancies: {startingPosition.WhiteOccupancies}");
//Console.WriteLine($"Black occupancies: {startingPosition.BlackOccupancies}");
//Console.WriteLine($"All occupancies: {startingPosition.AllOccupancies}");

//Console.WriteLine();

//ulong pawnPushTest = 0;
//pawnPushTest = pawnPushTest.SetBit(32);

//Console.WriteLine($"Pawn push test: {pawnPushTest} {pawnPushTest << 8}");

//Console.WriteLine();

//Console.WriteLine($"Move: {Move.EncodeMove(23, 54, 0, 1, 1, 1)}");

//foreach (int moveInfo in Move.ParseMove(Move.EncodeMove(23, 54, 0, 1, 1, 1)))
//{
//    Console.WriteLine(moveInfo);
//}

//Console.WriteLine();

//Console.WriteLine((13 >> 0) & 1U);

//Console.WriteLine();

//Position testPosition = FEN.LoadPositionFromFEN("8/4nkp1/5P2/8/2bP4/2KN4/8/8 w - - 0 1");
//MoveList moveList = new();
////Move.AddKnightMoves(testPosition, ref moveList, testPosition.WhiteKnights, isWhite: true);
////Move.AddKnightMoves(testPosition, ref moveList, testPosition.BlackKnights, isWhite: false);

//Console.WriteLine();

////Move.AddPawnMoves(testPosition, ref moveList, testPosition.WhitePawns, isWhite: true);
////Move.AddPawnMoves(testPosition, ref moveList, testPosition.BlackPawns, isWhite: false);

//Move.AddKingMoves(testPosition, ref moveList, testPosition.WhiteKing, isWhite: true);
//Move.AddKingMoves(testPosition, ref moveList, testPosition.BlackKing, isWhite: false);

//Console.WriteLine();

//for (int i = 0; i < moveList.Count; i++)
//{
//    Console.WriteLine($"Move: {moveList.Moves[i]}");
//}

//Console.WriteLine();

//Console.WriteLine(Move.KnightAttacks[27]);

//Console.WriteLine();

//Console.WriteLine(Move.MaskBishopOccupancyBits(27));
//Console.WriteLine(Move.MaskRookOccupancyBits(27));

//Console.WriteLine();

//Console.WriteLine(Move.ComputeBishopAttacks(27, 316659349848576UL));
//Console.WriteLine(Move.ComputeRookAttacks(27, 8796395014144UL));

//Console.WriteLine();

//ulong attackMask = Move.MaskBishopOccupancyBits(27);

//for (int i = 0; i < 100; i++)
//{
//    Console.WriteLine(Move.SetOccupancy(i, BitOperations.PopCount(attackMask), attackMask));
//}

////Console.WriteLine(SecondChessEngine.Random.GetRandomUInt() & 0xFFFF);
////Console.WriteLine(SecondChessEngine.Random.GetRandomULong() & SecondChessEngine.Random.GetRandomULong() & SecondChessEngine.Random.GetRandomULong());
//Console.WriteLine(SecondChessEngine.Random.GetULongFewBits());

////Move.ComputePawnMoves();

//Move.ComputeMagicNumbers();

Move.InitSliderAttackTables(isBishop: true);
Move.InitSliderAttackTables(isBishop: false);
Zobrist.InitRandomKeys();

//ulong occupancy = 0UL.SetBit(34).SetBit(13).SetBit(54).SetBit(9).SetBit(38).SetBit(12).SetBit(52);

//Console.WriteLine(occupancy);

//Console.WriteLine();

//Console.WriteLine(Move.GetBishopAttacks(27, occupancy));
//Console.WriteLine(Move.GetRookAttacks(36, occupancy));
//Console.WriteLine(Move.GetQueenAttacks(27, occupancy));

//Position position = FEN.LoadPositionFromFEN("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
//MoveList moveList = new();

//Move.AddBishopMoves(position, ref moveList, position.WhiteBishops, isWhite: true);
//Move.AddBishopMoves(position, ref moveList, position.BlackBishops, isWhite: false);

//Console.WriteLine(position.AllOccupancies);

//Move.AddRookMoves(position, ref moveList, position.WhiteRooks, isWhite: true);
//Move.AddRookMoves(position, ref moveList, position.BlackRooks, isWhite: false);

//Move.ComputeMagicNumbers();

//Move.AddQueenMoves(position, ref moveList, position.WhiteQueens, isWhite: true);
//Move.AddQueenMoves(position, ref moveList, position.BlackQueens, isWhite: false);

//Console.WriteLine(Move.IsSquareAttacked(position, 5, true));

//Position position = FEN.LoadPositionFromFEN("rn1q1k1r/pb1P2p1/1pp2p1p/2b5/B4Nn1/2Q3P1/PPP4P/RNB1K2R w KQ - 3 17");
////MoveList moveList = new();

////Move.AddKingMoves(position, ref moveList, position.WhiteKing, true);
////Move.AddKingMoves(position, ref moveList, position.BlackKing, false);


//Console.WriteLine($"Is legal: {Move.MakeMove(ref position, 836)}");
//// 196
//// 8653

//Console.WriteLine();

//Console.WriteLine(position.WhiteKing);

//Console.WriteLine(position.HalfmoveClock);

// Maybe there is something wrong with checks?
// Maybe the program can't block checks?
// There may be something wrong with pins
//Position test = FEN.LoadPositionFromFEN("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1");
//r3k2r/p1ppqpb1/bn2Pnp1/4N3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 0 1

//Move.MakeMove(ref test, 837);
//Console.WriteLine(test.WhiteRooks);
//Move.ComputeKnightMoves();

//Perft.Run(test, 4);
//CopiedPositions.Positions.Push(test);

//Move.MakeMove(ref test, 3070);
//Console.WriteLine(test.BlackKnights);

//test = CopiedPositions.Positions.Pop();
//Console.WriteLine(test.BlackKnights);

//MoveList moveList = new();

//Move.GenerateLegalMoves(test, ref moveList);

//for (int i = 0; i < moveList.Count; i++)
//{
//    CopiedPositions.Positions.Push(test);

//    Move.MakeMove(ref test, moveList.Moves[i]);

//    Console.WriteLine(test.WhitePawns);
//    Console.WriteLine(test.WhiteKnights);
//    Console.WriteLine(test.WhiteBishops);
//    Console.WriteLine(test.WhiteRooks);
//    Console.WriteLine(test.WhiteQueens);
//    Console.WriteLine(test.WhiteKing);
//    Console.WriteLine(test.BlackPawns);
//    Console.WriteLine(test.BlackKnights);
//    Console.WriteLine(test.BlackBishops);
//    Console.WriteLine(test.BlackRooks);
//    Console.WriteLine(test.BlackQueens);
//    Console.WriteLine(test.BlackKing);
//    Console.WriteLine(test.WhiteToMove);

//    Console.WriteLine();

//    Console.ReadLine();

//    test = CopiedPositions.Positions.Pop();

//    Console.WriteLine(test.WhitePawns);
//    Console.WriteLine(test.WhiteKnights);
//    Console.WriteLine(test.WhiteBishops);
//    Console.WriteLine(test.WhiteRooks);
//    Console.WriteLine(test.WhiteQueens);
//    Console.WriteLine(test.WhiteKing);
//    Console.WriteLine(test.BlackPawns);
//    Console.WriteLine(test.BlackKnights);
//    Console.WriteLine(test.BlackBishops);
//    Console.WriteLine(test.BlackRooks);
//    Console.WriteLine(test.BlackQueens);
//    Console.WriteLine(test.BlackKing);
//    Console.WriteLine(test.WhiteToMove);

//    Console.WriteLine();
//}


//Console.WriteLine(Zobrist.GenerateHashKey(FEN.LoadPositionFromFEN("rnbqkbnr/pppppppp/8/8/8/4P3/PPPP1PPP/RNBQKBNR b KQkq - 0 1")));
//Position position = FEN.LoadPositionFromFEN("r1bqkbnr/ppp2ppp/2np4/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R w KQkq - 0 4");
//Move.MakeMove(ref position, 16772);

//Position position = FEN.LoadPositionFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

//Console.WriteLine(Search.RunSearch(position, 5));
//Search.position = position;
//Search.Negamax(-int.MaxValue, int.MaxValue, 6);

//int[] moveInfo = Move.ParseMove(Search.PVTable[0, 0]);

//Console.WriteLine($"{BitboardUtil.IndexToCoordinate[moveInfo[0]]}{BitboardUtil.IndexToCoordinate[moveInfo[1]]}");

//for (int i = 0; i < Search.PVLength[0]; i++)
//{
//    int[] pvMoveInfo = Move.ParseMove(Search.PVTable[0, i]);

//    Console.WriteLine($"PV: {BitboardUtil.IndexToCoordinate[pvMoveInfo[0]]}{BitboardUtil.IndexToCoordinate[pvMoveInfo[1]]} ");
//}
//Search.StartSearch(position, 6);
//Console.WriteLine(Evaluation.Evaluate(position));
//Console.WriteLine(Evaluation.EGRookTable[48 ^ 56]);
//MoveList moveList = new();
//Move.GenerateLegalCaptures(position, ref moveList);

//foreach (ushort move in moveList.Moves)
//{
//    Console.WriteLine(move);
//}

//Console.Write("Depth: ");
//int depth = int.Parse(Console.ReadLine() ?? "");

//Console.WriteLine("End?");

//while (Console.ReadLine() != "Yes")
//{
//    Search.StartSearch(FEN.LoadPositionFromFEN(Console.ReadLine() ?? ""), depth);

//    Console.WriteLine("End?");
//}

//Search.StartSearch(FEN.LoadPositionFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"), 8);
Evaluation.InitEvaluationMasks();
//Console.Write("Depth: ");
//int depth = int.Parse(Console.ReadLine() ?? "8");

//while (true)
//{
//    Search.StartSearch(FEN.LoadPositionFromFEN(Console.ReadLine() ?? ""), depth);
//}

//Console.WriteLine($"Score: {Evaluation.Evaluate(FEN.LoadPositionFromFEN("r1bqkb1r/pppp1ppp/2n5/4p3/2B1P1n1/5N2/PPPP1PPP/RNBQ1RK1 w kq - 6 5"))}");

//ushort move = UCI.ParseMove(position, "e2e4");

//if (move > 0)
//{
//    Move.MakeMove(ref position, move);
//    Console.WriteLine(position.WhitePawns);
//}

//UCI.ParseGo(UCI.ParsePosition("position startpos moves e2e4"), "go depth 11");
UCI.UCILoop();