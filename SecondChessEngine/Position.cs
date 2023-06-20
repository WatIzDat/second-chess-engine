using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public struct Position
    {
        public Position()
        {
        }

        // White piece bitboards
        public ulong WhitePawns { get; set; } = 0;

        public ulong WhiteKnights { get; set; } = 0;

        public ulong WhiteBishops { get; set; } = 0;

        public ulong WhiteRooks { get; set; } = 0;

        public ulong WhiteQueens { get; set; } = 0;

        public ulong WhiteKing { get; set; } = 0;

        // Black piece bitboards
        public ulong BlackPawns { get; set; } = 0;

        public ulong BlackKnights { get; set; } = 0;

        public ulong BlackBishops { get; set; } = 0;

        public ulong BlackRooks { get; set; } = 0;

        public ulong BlackQueens { get; set; } = 0;

        public ulong BlackKing { get; set; } = 0;

        // Occupancy bitboards
        public ulong WhiteOccupancies { get; set; } = 0;

        public ulong BlackOccupancies { get; set; } = 0;

        public ulong AllOccupancies { get; set; } = 0;

        // Side to move
        public bool WhiteToMove { get; set; } = true;

        // Castling rights
        public bool WhiteKingsideCastlingRights { get; set; } = false;

        public bool WhiteQueensideCastlingRights { get; set; } = false;

        public bool BlackKingsideCastlingRights { get; set; } = false;

        public bool BlackQueensideCastlingRights { get; set; } = false;

        // En passant target squares
        public ulong WhiteEnPassantTargetSquare { get; set; } = 0;

        public ulong BlackEnPassantTargetSquare { get; set; } = 0;

        // Halfmove clock

        public int HalfmoveClock { get; set; } = 0;

        // Usually unique hash key
        public ulong HashKey { get; set; } = 0;
    }
}
