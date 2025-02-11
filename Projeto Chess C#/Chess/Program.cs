using System;
using ChessBoard;
using ChessPieces;



namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessGame Game = new ChessGame();
                while (!Game.EndGame)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintChessGame(Game);

                        Console.Write("Origem: ");
                        Position origen = Screen.ParseChessPosition().ToPosition();
                        Game.ValidateOriginPosition(origen);

                        bool[,] possiblePosition = Game.BoardGame.Piece(origen).PossibleMoves();
                        Console.Clear();
                        Screen.ShowTable(Game.BoardGame, possiblePosition);

                        Console.Write("Destino: ");
                        Position destination = Screen.ParseChessPosition().ToPosition();
                        Game.ValidateDestinationPosition(origen, destination);

                        Game.ExecuteTurn(origen, destination);

                    }
                    catch (BoardException e) {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.Write("Press Enter to continue.");
                        Console.ReadLine();
                    }
                }

                Console.ReadLine();
                Console.Clear();
                Screen.PrintChessGame(Game);
            }
            catch (BoardException e) { 
                Console.WriteLine(e.Message);
            }
        }
    }
}