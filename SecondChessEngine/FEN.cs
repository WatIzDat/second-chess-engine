using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class FEN
    {
        public static Position LoadPositionFromFEN(string fen)
        {
            Position position = new();

            string[] splitFen = fen.Split(' ');
            string pieces = splitFen[0];
            string sideToMove = splitFen[1];
            string castlingRights = splitFen[2];
            string epTargetSquare = splitFen[3];
            string halfmoveClock = splitFen[4];

            int file = 0;
            int rank = 7;

            foreach (char character in pieces)
            {
                if (character == '/')
                {
                    rank--;
                    file = 0;

                    continue;
                }

                int location = file + (rank * 8);

                switch (character)
                {
                    case 'p':
                        position.BlackPawns = position.BlackPawns.SetBit(location);
                        position.BlackOccupancies = position.BlackOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'n':
                        position.BlackKnights = position.BlackKnights.SetBit(location);
                        position.BlackOccupancies = position.BlackOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'b':
                        position.BlackBishops = position.BlackBishops.SetBit(location);
                        position.BlackOccupancies = position.BlackOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'r':
                        position.BlackRooks = position.BlackRooks.SetBit(location);
                        position.BlackOccupancies = position.BlackOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'q':
                        position.BlackQueens = position.BlackQueens.SetBit(location);
                        position.BlackOccupancies = position.BlackOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'k':
                        position.BlackKing = position.BlackKing.SetBit(location);
                        position.BlackOccupancies = position.BlackOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'P':
                        position.WhitePawns = position.WhitePawns.SetBit(location);
                        position.WhiteOccupancies = position.WhiteOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'N':
                        position.WhiteKnights = position.WhiteKnights.SetBit(location);
                        position.WhiteOccupancies = position.WhiteOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'B':
                        position.WhiteBishops = position.WhiteBishops.SetBit(location);
                        position.WhiteOccupancies = position.WhiteOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'R':
                        position.WhiteRooks = position.WhiteRooks.SetBit(location);
                        position.WhiteOccupancies = position.WhiteOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'Q':
                        position.WhiteQueens = position.WhiteQueens.SetBit(location);
                        position.WhiteOccupancies = position.WhiteOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case 'K':
                        position.WhiteKing = position.WhiteKing.SetBit(location);
                        position.WhiteOccupancies = position.WhiteOccupancies.SetBit(location);
                        position.AllOccupancies = position.AllOccupancies.SetBit(location);
                        break;
                    case '1':
                        break;
                    case '2':
                        file++;
                        break;
                    case '3':
                        file += 2;
                        break;
                    case '4':
                        file += 3;
                        break;
                    case '5':
                        file += 4;
                        break;
                    case '6':
                        file += 5;
                        break;
                    case '7':
                        file += 6;
                        break;
                    case '8':
                        file += 7;
                        break;
                    default:
                        break;
                }

                file++;
            }

            if (sideToMove.Equals("w"))
            {
                position.WhiteToMove = true;
            }
            else if (sideToMove.Equals("b"))
            {
                position.WhiteToMove = false;
            }

            if (!castlingRights.Equals("-"))
            {
                foreach (char character in castlingRights)
                {
                    switch (character)
                    {
                        case 'K':
                            position.WhiteKingsideCastlingRights = true;
                            break;
                        case 'Q':
                            position.WhiteQueensideCastlingRights = true;
                            break;
                        case 'k':
                            position.BlackKingsideCastlingRights = true;
                            break;
                        case 'q':
                            position.BlackQueensideCastlingRights = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!epTargetSquare.Equals("-"))
            {
                int epTargetFile = 0;
                int epTargetRank = 0;

                switch (epTargetSquare[0])
                {
                    case 'a':
                        epTargetFile = 0;
                        break;
                    case 'b':
                        epTargetFile = 1;
                        break;
                    case 'c':
                        epTargetFile = 2;
                        break;
                    case 'd':
                        epTargetFile = 3;
                        break;
                    case 'e':
                        epTargetFile = 4;
                        break;
                    case 'f':
                        epTargetFile = 5;
                        break;
                    case 'g':
                        epTargetFile = 6;
                        break;
                    case 'h':
                        epTargetFile = 7;
                        break;
                    default:
                        break;
                }

                switch (epTargetSquare[1])
                {
                    case '3':
                        epTargetRank = 2;

                        position.WhiteEnPassantTargetSquare = position.WhiteEnPassantTargetSquare.SetBit(epTargetFile + (epTargetRank * 8));
                        break;
                    case '6':
                        epTargetRank = 5;

                        position.BlackEnPassantTargetSquare = position.BlackEnPassantTargetSquare.SetBit(epTargetFile + (epTargetRank * 8));
                        break;
                    default:
                        break;
                }
            }

            if (!halfmoveClock.Equals("0"))
            {
                position.HalfmoveClock = int.Parse(halfmoveClock);
            }

            ulong hashKey = Zobrist.GenerateHashKey(position);
            position.HashKey = hashKey;

            return position;
        }
    }
}
