namespace Minefield.Core
{
    /// <summary>
    /// Represents a square on the board, a square has coordinates (row, column) and might have a mine
    /// </summary>
    public class Square
    {
        public Square(int row, int column, bool containsMine)
        {
            Row = row;
            Column = column;
            ContainsMine = containsMine;
        }

        /// <summary>
        /// Does the sqauare have a mine
        /// </summary>
        public bool ContainsMine { get; private set; }

        /// <summary>
        /// has the squar already been revealed - you can move back to a square
        /// </summary>
        public bool IsRevealed { get; private set; }

        /// <summary>
        /// is this sqaur the starting sqaure - assumes 1 starting sqaure per game
        /// </summary>
        public bool IsStartingSquare { get; private set; }

        /// <summary>
        /// what row is the square in (rows starts with 1)
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// what column is the square in (cols starts with 1)
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Returns true if a move to this square is ok (already revealed or there is no mine)
        /// Returns false if there is a mine
        /// </summary>
        /// <returns></returns>
        public bool MoveHere()
        {
            if (IsRevealed) //if the sqaure has already been revealed the mine, if any, has been set off so the move is fine 
                return true;
            IsRevealed = true;
            return !ContainsMine;
        }

        /// <summary>
        /// set this square as the starting sqaure for the game - starting sqaures dont have mines
        /// </summary>
        public void SetAsStartingSquare()
        {
            IsStartingSquare = true;
            ContainsMine = false;
        }

        public override string ToString() => $"Square (row {Row} - col {Column})";
    }

    
}
