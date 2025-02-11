using System;
using System.Collections.Generic;
using ChessBoard;

namespace Chess.ChessPieces
{
    internal class Knight : Pieces
    {

        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "H";

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

            pos.SetValues(Position.Row - 1, Position.Column - 2);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
        }
    }
}
