
namespace ChessBoard
{
    abstract class Pieces
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantyMovement { get; protected set; }

        public Board Board { get; protected set; }

        public Pieces(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            QuantyMovement = 0;
        }

        public void IncrementMoveCount() {
            QuantyMovement++;
        }
        public void DecrementMoveCount()
        {
            QuantyMovement--;
        }
        public  bool HasPossibleMoves() {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < Board.Rows; i++) {
                for (int j = 0; j < Board.Columns; j++) {
                    if (mat[i, j] == true) {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CanMoveFor(Position pos) { 
            return PossibleMoves()[pos.Row, pos.Column];
        }
        public abstract bool[,] PossibleMoves(); 
        
    }

   
}
