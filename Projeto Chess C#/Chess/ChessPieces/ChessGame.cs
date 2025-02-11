using System;
using ChessBoard;
using ChessPieces;
using System.Collections.Generic;
using Chess.ChessPieces;


namespace ChessPieces
{
    class ChessGame
    {
        public Board BoardGame { get; private set; }
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool EndGame { get; private set; }
        private HashSet<Pieces> Pieces;
        private HashSet<Pieces> Captured;
        public bool IsGameInCheck { get; private set; }
        public Pieces VulnerableToEnPassant { get; private set; }

        public ChessGame()
        {
            BoardGame = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            EndGame = false;
            IsGameInCheck = false;
            VulnerableToEnPassant = null;
            Captured = new HashSet<Pieces>();
            Pieces = new HashSet<Pieces>();
            SetPieces();

        }
        public void SetNewPiece(char column, int row, Pieces pieces)
        {
            BoardGame.SetPieces(pieces, new PositionBoardChess(column, row).ToPosition());
            Pieces.Add(pieces);
        }

        public Pieces MakeMove(Position origen, Position destination)
        {  //Executa o movimento da peça
            Pieces p = BoardGame.RemovePieces(origen);
            p.IncrementMoveCount();
            Pieces capturedPiece = BoardGame.RemovePieces(destination);
            BoardGame.SetPieces(p, destination);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            //SpeciaPlay Small Castling
            if (p is King && destination.Column == origen.Column + 2)
            {
                Position OrigenRook = new Position(origen.Row, origen.Column + 3);
                Position DestinationRook = new Position(origen.Row, origen.Column + 1);
                Pieces R = BoardGame.RemovePieces(OrigenRook);
                R.IncrementMoveCount();
                BoardGame.SetPieces(R, DestinationRook);
            }
            //SpeciaPlay Big Castling
            if (p is King && destination.Column == origen.Column - 2)
            {
                Position OrigenRook = new Position(origen.Row, origen.Column - 4);
                Position DestinationRook = new Position(origen.Row, origen.Column - 1);
                Pieces R = BoardGame.RemovePieces(OrigenRook);
                R.IncrementMoveCount();
                BoardGame.SetPieces(R, DestinationRook);
            }

            //SpecialPlay en passant

            if (p is Pawn) {
                if (origen.Column != destination.Column && capturedPiece == null) {
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(destination.Row + 1, destination.Column);
                    }
                    else { 
                        posP = new Position(destination.Row -1, destination.Column);
                    }
                    capturedPiece = BoardGame.RemovePieces(posP);
                    Captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }
        public void UndoMove(Position origen, Position destination, Pieces capturedPiece)
        {
            Pieces p = BoardGame.RemovePieces(destination);
            p.DecrementMoveCount();
            if (capturedPiece != null)
            {
                BoardGame.SetPieces(capturedPiece, destination);
                Captured.Remove(capturedPiece);
            }
            BoardGame.SetPieces(p, origen);

            //SpeciaPlay Small Castling
            if (p is King && destination.Column == origen.Column + 2)
            {
                Position OrigenRook = new Position(origen.Row, origen.Column + 3);
                Position DestinationRook = new Position(origen.Row, destination.Column + 1);
                Pieces R = BoardGame.RemovePieces(DestinationRook);
                R.DecrementMoveCount();
                BoardGame.SetPieces(R, OrigenRook);
            }
            //SpeciaPlay Big Castling
            if (p is King && destination.Column == origen.Column + 2)
            {
                Position OrigenRook = new Position(origen.Row, origen.Column - 4);
                Position DestinationRook = new Position(origen.Row, destination.Column - 1);
                Pieces R = BoardGame.RemovePieces(DestinationRook);
                R.DecrementMoveCount();
                BoardGame.SetPieces(R, OrigenRook);
            }

            //SpecialPlay en Passant
            if (p is Pawn) {
                if (origen.Column != destination.Column && capturedPiece == VulnerableToEnPassant)
                {
                    Pieces pawn = BoardGame.RemovePieces(destination);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destination.Column);
                    }
                    else { 
                        posP = new Position( 4, destination.Column);
                    }
                    BoardGame.SetPieces(pawn, posP);
                }

            }
        }

        public void ExecuteTurn(Position origen, Position destination)
        {  // Executa o turno, trocando a vez dos jogadores
            Pieces capturedPiece = MakeMove(origen, destination);
            if (isInCheck(ActualPlayer))
            {
                UndoMove(origen, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in check");
            }
            Pieces p = BoardGame.Piece(destination);
            //SpecialPlay Promotion

            if (p is Pawn) { 
                if ((p.Color == Color.White && destination.Row == 0) ||(p.Color  == Color.Black && destination.Row == 8)){
                    p = BoardGame.RemovePieces(destination);
                    Pieces.Remove(p);
                    Pieces queen = new Queen(BoardGame, p.Color);
                    BoardGame.SetPieces(queen, destination);
                    Pieces.Add(queen);
                    
                }
            }

            if (isInCheck(Opponent(ActualPlayer)))
            {
                IsGameInCheck = true;
            }
            else
            {
                IsGameInCheck = false;
            }
            if (isInCheckMate(Opponent(ActualPlayer)))
            {
                EndGame = true;
            }
            else
            {
                Turn++;
                SwitchPlayer();
            }

            

            //SpecialPlay en Passant

            if (p is Pawn && (destination.Row == origen.Row - 2 || destination.Row == origen.Row + 2))
            {
                VulnerableToEnPassant = p;
            }
            else {
                VulnerableToEnPassant = null;
            }
        }
        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;

            }
            else
            {
                return Color.White;
            }
        }

        private Pieces King(Color color)
        { //mostrar o Rei de uma cor
            foreach (Pieces p in PiecesInGame(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }
        public bool isInCheck(Color color)
        {
            Pieces K = King(color);
            foreach (Pieces p in PiecesInGame(Opponent(color)))
            {
                bool[,] mat = p.PossibleMoves();
                if (mat[K.Position.Row, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }
        public bool isInCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach (Pieces p in PiecesInGame(color))
            {
                bool[,] mat = p.PossibleMoves();
                for (int i = 0; i < BoardGame.Rows; i++)
                {
                    for (int j = 0; j < BoardGame.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origen = p.Position;
                            Position destination = new Position(i, j);
                            Pieces capturedPiece = MakeMove(origen, destination);
                            bool CheckTest = isInCheck(color);
                            UndoMove(origen, destination, capturedPiece);
                            if (!isInCheck(color))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        private void SwitchPlayer()
        {
            if (ActualPlayer == Color.White)
            {
                ActualPlayer = Color.Black;
            }
            else
            {
                ActualPlayer = Color.White;
            }
        }

        public HashSet<Pieces> CapturedPieces(Color color)
        {
            HashSet<Pieces> aux = new HashSet<Pieces>();
            foreach (Pieces p in Captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }

            }
            return aux;
        }
        public HashSet<Pieces> PiecesInGame(Color color)
        {
            HashSet<Pieces> aux = new HashSet<Pieces>();
            foreach (Pieces p in Pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }

            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }
        public void ValidateOriginPosition(Position pos)
        {
            if (BoardGame.Piece(pos) == null)
            {

                throw new BoardException("Não Existe peça na posição de origem escolhida!");
            }
            if (ActualPlayer != BoardGame.Piece(pos).Color)
            {
                throw new BoardException("Peça de origem escolhida não é sua!");
            }
            if (!BoardGame.Piece(pos).HasPossibleMoves())
            {
                throw new BoardException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }
        public void ValidateDestinationPosition(Position origen, Position destination)
        {
            if (!BoardGame.Piece(origen).CanMoveFor(destination))
            {

                throw new BoardException("Posição de destino invalida");
            }
        }
        private void SetPieces()
        {

            // White Pieces 
            SetNewPiece('a', 1, new Rook(BoardGame, Color.White));
            SetNewPiece('b', 1, new Knight(BoardGame, Color.White));
            SetNewPiece('c', 1, new Bishop(BoardGame, Color.White));
            SetNewPiece('d', 1, new Queen(BoardGame, Color.White));
            SetNewPiece('e', 1, new King(BoardGame, Color.White, this));
            SetNewPiece('f', 1, new Bishop(BoardGame, Color.White));
            SetNewPiece('g', 1, new Knight(BoardGame, Color.White));
            SetNewPiece('h', 1, new Rook(BoardGame, Color.White));

            // Whites Pawns
            SetNewPiece('a', 2, new Pawn(BoardGame, Color.White, this));
            SetNewPiece('b', 2, new Pawn(BoardGame, Color.White, this));
            SetNewPiece('c', 2, new Pawn(BoardGame, Color.White, this));    
            SetNewPiece('d', 2, new Pawn(BoardGame, Color.White, this));
            SetNewPiece('e', 2, new Pawn(BoardGame, Color.White, this));
            SetNewPiece('f', 2, new Pawn(BoardGame, Color.White, this)); 
            SetNewPiece('g', 2, new Pawn(BoardGame, Color.White, this));
            SetNewPiece('h', 2, new Pawn(BoardGame, Color.White, this));

            // Black Pieces
            SetNewPiece('a', 8, new Rook(BoardGame, Color.Black));
            SetNewPiece('b', 8, new Knight(BoardGame, Color.Black));
            SetNewPiece('c', 8, new Bishop(BoardGame, Color.Black));
            SetNewPiece('d', 8, new Queen(BoardGame, Color.Black));
            SetNewPiece('e', 8, new King(BoardGame, Color.Black, this));
            SetNewPiece('f', 8, new Bishop(BoardGame, Color.Black));
            SetNewPiece('g', 8, new Knight(BoardGame, Color.Black));
            SetNewPiece('h', 8, new Rook(BoardGame, Color.Black));

            // Black Pawns
            SetNewPiece('a', 7, new Pawn(BoardGame, Color.Black, this));
            SetNewPiece('b', 7, new Pawn(BoardGame, Color.Black, this));
            SetNewPiece('c', 7, new Pawn(BoardGame, Color.Black, this));
            SetNewPiece('d', 7, new Pawn(BoardGame, Color.Black, this));
            SetNewPiece('e', 7, new Pawn(BoardGame, Color.Black, this));
            SetNewPiece('f', 7, new Pawn(BoardGame, Color.Black, this));
            SetNewPiece('g', 7, new Pawn(BoardGame, Color.Black, this));
            SetNewPiece('h', 7, new Pawn(BoardGame, Color.Black, this));


        }
    }
}
