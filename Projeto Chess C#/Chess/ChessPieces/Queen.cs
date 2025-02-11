using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;

namespace Chess.ChessPieces
{
    internal class Queen : Pieces
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "Q";

        }

        private bool CanMove(Position pos)
        {
            Pieces p = Board.Piece(pos);
            return p == null || p.Color != Color;

        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            //NW
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column - 1);
            }

            //NE
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column + 1);
            }

            //SW
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column - 1);
            }

            //SE
            pos.SetValues(Position.Row + 1, Position.Column + -1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column + 1);
            }

            // North
            pos.SetValues(Position.Row - 1, Position.Column);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row -1 ,pos.Column );
            }

            // South
            pos.SetValues(Position.Row + 1, Position.Column);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row++;
            }

            // East
            pos.SetValues(Position.Row, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column++;
            }
            // West
            pos.SetValues(Position.Row, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column--;
            }
            return mat;
        }
    }
}
