using System;

namespace Minefield.Core
{
    /// <summary>
    /// Thrown when a move is attempted on a game that is not in play
    /// </summary>
    public class GameOverException:Exception
    {
        public GameOverException()
        {
        }

        public GameOverException(string message)
            : base(message)
        {
        }

        public GameOverException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
