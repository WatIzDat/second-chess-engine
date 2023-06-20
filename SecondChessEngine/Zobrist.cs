using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class Zobrist
    {
        // White piece hash keys
        public static ulong[] WhitePawns { get; set; } = new ulong[64];
        public static ulong[] WhiteKnights { get; set; } = new ulong[64];
        public static ulong[] WhiteBishops { get; set; } = new ulong[64];
        public static ulong[] WhiteRooks { get; set; } = new ulong[64];
        public static ulong[] WhiteQueens { get; set; } = new ulong[64];
        public static ulong[] WhiteKings { get; set; } = new ulong[64];

        // Black piece hash keys
        public static ulong[] BlackPawns { get; set; } = new ulong[64];
        public static ulong[] BlackKnights { get; set; } = new ulong[64];
        public static ulong[] BlackBishops { get; set; } = new ulong[64];
        public static ulong[] BlackRooks { get; set; } = new ulong[64];
        public static ulong[] BlackQueens { get; set; } = new ulong[64];
        public static ulong[] BlackKings { get; set; } = new ulong[64];

        // Side hash key
        public static ulong Side { get; set; }

        // Castling rights hash keys
        public static ulong WhiteKingsideCastlingRights { get; set; }
        public static ulong WhiteQueensideCastlingRights { get; set; }
        public static ulong BlackKingsideCastlingRights { get; set; }
        public static ulong BlackQueensideCastlingRights { get; set; }

        // En passant file hash keys
        public static ulong[] EnPassantFiles { get; set; } = new ulong[8];

        public static void InitRandomKeys()
        {
            // Init piece keys
            for (int i = 0; i < 64; i++)
            {
                WhitePawns[i] = Random.GetRandomULong();
                WhiteKnights[i] = Random.GetRandomULong();
                WhiteBishops[i] = Random.GetRandomULong();
                WhiteRooks[i] = Random.GetRandomULong();
                WhiteQueens[i] = Random.GetRandomULong();
                WhiteKings[i] = Random.GetRandomULong();

                BlackPawns[i] = Random.GetRandomULong();
                BlackKnights[i] = Random.GetRandomULong();
                BlackBishops[i] = Random.GetRandomULong();
                BlackRooks[i] = Random.GetRandomULong();
                BlackQueens[i] = Random.GetRandomULong();
                BlackKings[i] = Random.GetRandomULong();
            }

            // Init en passant files keys
            for (int i = 0; i < 8; i++)
            {
                EnPassantFiles[i] = Random.GetRandomULong();
            }

            // Init castling rights keys
            WhiteKingsideCastlingRights = Random.GetRandomULong();
            WhiteQueensideCastlingRights = Random.GetRandomULong();
            BlackKingsideCastlingRights = Random.GetRandomULong();
            BlackQueensideCastlingRights = Random.GetRandomULong();

            // Init side key
            Side = Random.GetRandomULong();
        }

        public static ulong GenerateHashKey(Position position)
        {
            ulong key = 0UL;

            HashPiece(WhitePawns, position.WhitePawns, ref key);
            HashPiece(WhiteKnights, position.WhiteKnights, ref key);
            HashPiece(WhiteBishops, position.WhiteBishops, ref key);
            HashPiece(WhiteRooks, position.WhiteRooks, ref key);
            HashPiece(WhiteQueens, position.WhiteQueens, ref key);
            HashPiece(WhiteKings, position.WhiteKing, ref key);

            HashPiece(BlackPawns, position.BlackPawns, ref key);
            HashPiece(BlackKnights, position.BlackKnights, ref key);
            HashPiece(BlackBishops, position.BlackBishops, ref key);
            HashPiece(BlackRooks, position.BlackRooks, ref key);
            HashPiece(BlackQueens, position.BlackQueens, ref key);
            HashPiece(BlackKings, position.BlackKing, ref key);

            if (position.WhiteEnPassantTargetSquare != 0 || position.BlackEnPassantTargetSquare != 0)
            {
                int square = position.WhiteEnPassantTargetSquare != 0 ? BitboardUtil.GetLS1BIndex(position.WhiteEnPassantTargetSquare.IsolateLS1B()) : BitboardUtil.GetLS1BIndex(position.BlackEnPassantTargetSquare.IsolateLS1B());

                key ^= EnPassantFiles[square % 8];

                //if (square == 16 || square == 40)
                //{
                //    key ^= EnPassantFiles[0];
                //}
                //else if (square == 17 || square == 41)
                //{
                //    key ^= EnPassantFiles[1];
                //}
                //else if (square == 18 || square == 42)
                //{
                //    key ^= EnPassantFiles[2];
                //}
                //else if (square == 19 || square == 43)
                //{
                //    key ^= EnPassantFiles[3];
                //}
                //else if (square == 20 || square == 44)
                //{
                //    key ^= EnPassantFiles[4];
                //}
                //else if (square == 21 || square == 45)
                //{
                //    key ^= EnPassantFiles[5];
                //}
                //else if (square == 22 || square == 46)
                //{
                //    key ^= EnPassantFiles[6];
                //}
                //else if (square == 23 || square == 47)
                //{
                //    key ^= EnPassantFiles[7];
                //}
            }

            if (position.WhiteKingsideCastlingRights == true)
            {
                key ^= WhiteKingsideCastlingRights;
            }

            if (position.WhiteQueensideCastlingRights == true)
            {
                key ^= WhiteQueensideCastlingRights;
            }

            if (position.BlackKingsideCastlingRights == true)
            {
                key ^= BlackKingsideCastlingRights;
            }

            if (position.BlackQueensideCastlingRights == true)
            {
                key ^= BlackQueensideCastlingRights;
            }

            if (!position.WhiteToMove) key ^= Side;

            return key;
        }

        private static void HashPiece(ulong[] hashKeys, ulong bitboard, ref ulong currentKey)
        {
            int popCount = BitOperations.PopCount(bitboard);

            for (int i = 0; i < popCount; i++)
            {
                int square = BitboardUtil.GetLS1BIndex(bitboard.IsolateLS1B());
                
                currentKey ^= hashKeys[square];

                bitboard = bitboard.ClearBit(square);
            }
        }
    }
}
