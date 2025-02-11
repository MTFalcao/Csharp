using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;
using ChessPieces;

namespace Chess.ChessPieces
{
    internal class Pawn : Pieces
    {
        private ChessGame Game;
        public Pawn(Board board, Color color, ChessGame game) : base(board, color)
        {
            Game = game;
        }

        public override string ToString()
        {
            return "P";

        }

        private bool HasEnemy(Position pos)
        {
            Pieces p = Board.Piece(pos);
            return p != null && p.Color != Color;

        }

        private bool Free(Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Row - 1, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row - 2, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos) && QuantyMovement == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                //SpecialPlay en Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.IsValidPosition(left) && HasEnemy(left) && Board.Piece(left) == Game.VulnerableToEnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.IsValidPosition(right) && HasEnemy(right) && Board.Piece(right) == Game.VulnerableToEnPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(Position.Row + 1, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row + 2, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos) && QuantyMovement == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                //SpecialPlay en Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.IsValidPosition(left) && HasEnemy(left) && Board.Piece(left) == Game.VulnerableToEnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.IsValidPosition(right) && HasEnemy(right) && Board.Piece(right) == Game.VulnerableToEnPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }
            }
            return mat;
        }

    }
}

