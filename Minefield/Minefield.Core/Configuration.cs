using System;

namespace Minefield.Core
{
    /// <summary>
    /// represents some configuration for the game - atm we are using default values
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Number of lives
        /// </summary>
        public int NumLives { get; private set; }
        
        /// <summary>
        /// size of the side of the playing board
        /// </summary>
        public int BoardSize { get; private set; }

        /// <summary>
        /// difficulty of the gamne
        /// </summary>
        public Difficulty Difficulty { get; private set; }

        public Configuration(int numLives=5, int boardSize=10, Difficulty difficulty=Difficulty.Hard)
        {
            if (boardSize < Board.MinSize || boardSize > Board.MaxSize)
                throw new ArgumentException($"{nameof(boardSize)} must be between {Board.MinSize} and {Board.MaxSize}");
            if (numLives < Game.MinLives || numLives > Game.MaxLives)
                throw new ArgumentException($"{nameof(numLives)} must be between {Game.MinLives} and {Game.MaxLives}");

            NumLives = numLives;
            BoardSize = boardSize;
            Difficulty = difficulty;
        }
    }
}
