using System;
using System.Collections.Generic;
using System.Text;

namespace Minefield.Core
{
    /// <summary>
    /// Represents the mifield playing board.
    /// The board is analagous to a chess board being square with a number of squares.
    /// The board can be any size from 2x2 to 100x100.
    /// </summary>
    public class Board
    {
        public List<Square> Squares { get; private set; }

        public const int MaxSize = 100;
        public const int MinSize = 2;
   
        /// <summary>
        /// create a new playing board
        /// </summary>
        /// <param name="size">The size of the board side. The default is 10 (for a 10x10 chessboard size)</param>
        public Board(List<Square> squares, int size)
        {
            Size = size;
            Squares = squares;
        }

        /// <summary>
        /// The size of the board side. The default is 10 (for a 10x10 board)
        /// </summary>
        public int Size { get; private set; }
    }
}
