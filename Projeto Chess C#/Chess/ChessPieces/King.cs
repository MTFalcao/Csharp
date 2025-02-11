using System;
using ChessBoard;
using ChessPieces;

namespace ChessPieces
{
    internal class King : Pieces
    {
        private ChessGame Game;
        public King(Board board, Color color, ChessGame game) : base(board, color)
        {
            Game = game;
        }

        public override string ToString()
        {
            return "K";

        }

        private bool CanMove(Position pos)
        {
            Pieces p = Board.Piece(pos);
            return p == null || p.Color != Color;

        }

        private bool TestRookForCastling(Position pos)
        {
            Pieces p = Board.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.QuantyMovement == 0;
        }
        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);


            // North
            pos.SetValues(Position.Row - 1, Position.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // Northeast 
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // East
            pos.SetValues(Position.Row, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // Southeast
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // South
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // Southwest
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // West
            pos.SetValues(Position.Row, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // Northwest 
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //#SpecialPlay Castling
            if (QuantyMovement == 0 && !Game.IsGameInCheck)
            {
                // PosR1 = posição do Rook / Small Castling
                Position posR1 = new Position(Position.Row, Position.Column + 3);
                if (TestRookForCastling(posR1))
                {

                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }
                // PosR2 = posição do Rook / Big Castling
                Position posR2 = new Position(Position.Row, Position.Column - 4);
                if (TestRookForCastling(posR2))
                {

                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }

            }
            return mat;
        }
    }
}
