namespace Minefield.Core
{
    /// <summary>
    /// What move direction has the user chosen
    /// </summary>
    public enum MoveDirection
    {
        /// <summary>
        /// Move left
        /// </summary>
        Left,
        /// <summary>
        /// Move right
        /// </summary>
        Right,
        /// <summary>
        /// move up
        /// </summary>
        Up,
        /// <summary>
        /// move down
        /// </summary>
        Down
    }

    /// <summary>
    /// user could chose the difficulty level of the game
    /// </summary>
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    /// <summary>
    /// The result of a move
    /// </summary>
    public enum MoveResult
    {
        /// <summary>
        /// The game is won with no mine hit on the last move (the user reached the other side with at least 1 life left)
        /// </summary>
        Win,

        /// <summary>
        /// The game is won with a mine hit on the last move (the user reached the other side with at least 1 life left)
        /// </summary>
        WinHitMine,
        /// <summary>
        /// The move did not hit a mine and the game is onging
        /// </summary>
        Ok,
        /// <summary>
        /// The user hit a mine but the game is still in play as they have at least 1 life left
        /// </summary>
        HitMine,
        
        /// <summary>
        /// User tried to move to an invalid sqaure
        /// </summary>
        InvalidMove,
        /// <summary>
        /// The game is lost (no lives left)
        /// </summary>
        Lose
    }

    /// <summary>
    /// The current game status
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// game is not started yet
        /// </summary>
        NotStarted,
        /// <summary>
        /// the game is over and the user won
        /// </summary>
        Won,
        /// <summary>
        /// the game is over and the user lost
        /// </summary>
        Lost,
        /// <summary>
        /// the game is ongoing
        /// </summary>
        InPlay
    }
}
