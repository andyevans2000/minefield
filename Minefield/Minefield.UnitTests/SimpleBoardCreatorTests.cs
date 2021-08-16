using Minefield.Core;
using Minefield.Core.Contracts;
using Moq;
using System.Linq;
using Xunit;

namespace Minefield.UnitTests
{
    public class SimpleBoardCreatorTests
    {
        private readonly Mock<IMineLayer> _mockMineLayer = new Mock<IMineLayer>();

        [Fact]
        public void CreatorCreatesBoardWithCorrectNumberSquares()
        {
            var boardSize = 2;
            var board = SetupBoard(false, 2);
            Assert.Equal(boardSize * boardSize, board.Squares.Count);
        }

        [Fact]
        public void CreatorCreatesBoardWithSquaresWithMines()
        {
            var boardSize = 2;
            var board = SetupBoard(true, boardSize);
            Assert.Equal((boardSize * boardSize)-1, board.Squares.Count(s=>s.ContainsMine)); //take away 1 for starting sqaure which never has a mine
        }

        [Fact]
        public void CreatorCreatesBoardWithSquaresWithoutMines()
        {
            var board = SetupBoard(false, 2);
            Assert.Equal(0, board.Squares.Count(s => s.ContainsMine)); 
        }

        private Board SetupBoard(bool squaresHaveMines, int boardSize)
        {
            _mockMineLayer.Setup(m => m.TryToLayMine()).Returns(squaresHaveMines);
            var boardCreator = new SimpleBoardCreator(_mockMineLayer.Object);
            return boardCreator.CreateBoard(boardSize);
        }
    }
}
