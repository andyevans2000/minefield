namespace Minefield.Core.Contracts
{
    public interface IBoardCreator
    {
        /// <summary>
        /// Create a new game board
        /// </summary>
        /// <param name="size">size of the board</param>
        /// <returns></returns>
        Board CreateBoard(int size);
    }
}
