using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fen Reader");

            string initialBoard = "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R";

            Board b;
            //b = new Board(FenReader.GetDefaultPiecePlacement());
            b = new Board(FenReader.ReadPiecePlacement(initialBoard));
            b.Print();
        }
    }

    static class FenReader
    {
        static string validPieces = "PNBRQKpnbrqk";

        public static char[,] ReadPiecePlacement(string fenPiecePlacement)
        {
            char[,] squares = new char[8, 8];
            
            // split by '/' and loop through all 8
            string[] ranks = fenPiecePlacement.Split('/');

            if (ranks.Length != 8)
            {
                throw new ArgumentException(string.Format("Fen Piece Placement is invalid. Incorrect number of ranks. Expected 8, got {0}", ranks.Length));
            }

            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    squares[i, j] = '-';

            for (int row=0; row<8; ++row)
            {
                string rank = ranks[row];

                for (int i = 0, col = 0; i < rank.Length; ++i, ++col)
                {
                    char piece = rank[i];
                    if (validPieces.Contains(piece))
                    {
                        // if its a valid piece then set it on the board
                        squares[col, row] = piece;
                    }
                    else if (char.IsDigit(piece))
                    {
                        // if its a number set the empty spaces
                        int numSpaces = int.Parse(piece.ToString());
                        col += numSpaces - 1;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Fen Piece Placement is invalid. Row contains invalid piece. Got this: {0}", rank));
                    }
                }
            }

            return squares;
        }

        public static char[,] GetDefaultPiecePlacement()
        {
            char[,] squares = new char[8, 8];
            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    squares[i, j] = '-';

            // Pawns
            for (int i = 0; i < 8; ++i)
            {
                squares[i, 6] = 'P';
                squares[i, 1] = 'p';
            }

            //White
            squares[0, 7] = 'R';
            squares[1, 7] = 'N';
            squares[2, 7] = 'B';
            squares[3, 7] = 'Q';
            squares[4, 7] = 'K';
            squares[5, 7] = 'B';
            squares[6, 7] = 'N';
            squares[7, 7] = 'R';

            ////Black
            squares[0, 0] = 'r';
            squares[1, 0] = 'n';
            squares[2, 0] = 'b';
            squares[3, 0] = 'q';
            squares[4, 0] = 'k';
            squares[5, 0] = 'b';
            squares[6, 0] = 'n';
            squares[7, 0] = 'r';

            return squares;
        }
    }

    class Board
    {
        char[,] squares = new char[8, 8];

        public Board(char[,] initialPos)
        {
            this.squares = initialPos;
        }

        public void Print()
        {
            Console.Write('\n');
            for (int i = 0; i <8; ++i)
            //for (int i = 7; i >= 0; --i)
            { 
                for (int j=0; j<8;++j)
                    Console.Write(squares[j,i]);
                Console.Write('\n');
            }
            Console.Write('\n');
        }
    }
}
