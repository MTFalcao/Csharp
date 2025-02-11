
namespace ChessBoard
{
     class Board
    {

        public int Rows { get; set; }
        public int Columns { get; set; }
        private Pieces[,] Pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Pieces[Rows, Columns];
        }

        public Pieces Piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Pieces Piece(Position pos) { 

            return Pieces[pos.Row, pos.Column];
        }
        public void SetPieces(Pieces p, Position pos)
        {
            if (CheckPosition(pos))
            {
                throw new BoardException("Já Existe uma peça nessa posição");
            }

            Pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        public Pieces RemovePieces(Position pos)
        {
            if (Piece(pos) == null) {
                return null;
            }
            Pieces aux = Piece(pos);
            aux.Position = null;
            Pieces[pos.Row, pos.Column] = null; 
            return aux;

        }


        public bool CheckPosition(Position pos) {
            ValidatePosition(pos);

            return Piece(pos) != null;
        }
        public void ValidatePosition(Position pos) {
            if (!IsValidPosition(pos)) {
                throw new BoardException("Posição invalida");
            }
        }
        public bool IsValidPosition(Position pos) // verifica se a posição é valida
        {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

    }
}
