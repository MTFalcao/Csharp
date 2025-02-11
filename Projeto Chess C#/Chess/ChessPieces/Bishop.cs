using System;
using System.Collections.Generic;
using ChessBoard;

namespace ChessPieces
{
    internal class Bishop : Pieces
    {

        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B";

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
            pos.SetValues(Position.Row + 1, Position.Column+- 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column +1);
            }
            return mat;
        }
    }
}
