namespace Minefield.Core.Contracts
{
    public interface IGame
    {
        /// <summary>
        /// The current sqaure in the game
        /// </summary>
        Square CurrentSquare { get; }

        /// <summary>
        /// the status of the game
        /// </summary>
        GameStatus GameStatus { get; }
        /// <summary>
        /// how many lives are left
        /// </summary>
        int Lives { get; }

        /// <summary>
        /// how many moves have been made
        /// </summary>
        int Moves { get; }
        /// <summary>
        /// The playing board
        /// </summary>
        Board Board { get; }

        /// <summary>
        /// Move to a new swaure
        /// </summary>
        /// <param name="moveDirection">The direction to move</param>
        /// <returns>The result of the move</returns>
        MoveResult Move(MoveDirection moveDirection);
        
        /// <summary>
        /// Start the game
        /// </summary>
        void Start();
    }
}