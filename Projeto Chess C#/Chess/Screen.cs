using System;
using System.Collections.Generic;
using ChessBoard;
using ChessPieces;

namespace Chess
{
    class Screen
    {
        public static void PrintChessGame(ChessGame game) {
            Screen.ShowTable(game.BoardGame);
            Console.WriteLine();
            PrintCapturedPieces(game);
            Console.WriteLine($"Turn: {game.Turn}");

            if (!game.EndGame)
            {
                Console.WriteLine($"Awaiting Player Action: {game.ActualPlayer}");
                if (game.IsGameInCheck)
                {
                    Console.WriteLine("CHECK!");
                }
                Console.WriteLine();
            }
            else {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine($"Winner: {game.ActualPlayer}");
            }
        }
        public static void PrintCapturedPieces(ChessGame game) { 
            Console.WriteLine("Captured Pieces");
            Console.Write("White: ");
            PrintCollection(game.CapturedPieces(Color.White));
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintCollection(game.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
           
        }

        public static void PrintCollection(HashSet<Pieces> collection) {
            Console.Write("[");
            foreach (Pieces p in collection)
            {
                Console.Write($"{p} ");
            }
            Console.Write("]");
            Console.WriteLine();
            
        }
        public static void ShowTable(Board board) // Print BoardGame
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {

                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            
            Console.Write("  a b c d e f g h");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ShowTable(Board board, bool[,] possiblePosition) // Print BoardGame with possible position
        {
            ConsoleColor OriginalBackGround = Console.BackgroundColor;
            ConsoleColor NewBackGround = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    // Verifica se a posição (i, j) está nas possíveis posições da peça
                    if (possiblePosition[i, j])
                    {
                        Console.BackgroundColor = NewBackGround;
                    }
                    else
                    {
                        Console.BackgroundColor = OriginalBackGround;
                    }

                    // Exibe a peça na posição (i, j)
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = OriginalBackGround;
                }
                Console.WriteLine();
            }

            // Exibe as colunas no final da tabela
            Console.Write("  A B C D E F G H");
            Console.BackgroundColor = OriginalBackGround;
            Console.WriteLine();
            Console.WriteLine();
        }


        public static PositionBoardChess ParseChessPosition()
        {
            string s = Console.ReadLine().ToLower();
            char columns = s[0];
            int rows = int.Parse($"{s[1]}");
            return new PositionBoardChess(columns, rows);
        }


        public static void PrintPiece(Pieces piece)
        {
            if (piece == null)
            {
                Console.Write("- ");

            }
            else
            {

                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}