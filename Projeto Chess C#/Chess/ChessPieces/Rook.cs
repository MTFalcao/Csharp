using System;
using ChessBoard;

namespace ChessPieces
{
    internal class Rook : Pieces
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "T";

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

            // North
            pos.SetValues(Position.Row -1 , Position.Column);
            while (Board.IsValidPosition(pos) && CanMove(pos)) { 
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row--;
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
