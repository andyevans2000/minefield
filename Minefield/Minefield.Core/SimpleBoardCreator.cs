using Minefield.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minefield.Core
{
    /// <summary>
    /// A simple implementation that sets up the playing sqaures, some with mines and sets the starting sqaure
    /// </summary>
    public class SimpleBoardCreator: IBoardCreator
    {
        private readonly IMineLayer _mineLayer;

        public SimpleBoardCreator(IMineLayer mineLayer) => _mineLayer = mineLayer;

        public Board CreateBoard(int size)
        {
            if (size < Board.MinSize || size > Board.MaxSize)
                throw new ArgumentException($"{nameof(size)} must be between {Board.MinSize} and {Board.MaxSize}");

            //The board will have a list of squares - each sqaure has a unique row and column combo, for a 10x10 board we will have 100 squares
            //Some sqaures will have a mine created by the mine layer
            var squares = new List<Square>(size * size);
            for (var row = 1; row <= size; row++)
            {
                for (var col = 1; col <= size; col++)
                {
                    squares.Add(new Square(row, col, _mineLayer.TryToLayMine()));
                }
            }
            var randomCol = new Random().Next(1, size);
            //set one square at random on the first (bottom) row as the starting square, this wont have a mine to be nice to the player
            squares.First(s => s.Row == 1 && s.Column == randomCol).SetAsStartingSquare();

            return new Board(squares, size);
        }
    }
}
