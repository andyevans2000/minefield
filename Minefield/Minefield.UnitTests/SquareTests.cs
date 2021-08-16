using Minefield.Core;
using Xunit;

namespace Minefield.UnitTests
{
    public class SquareTests
    {
        [Fact]
        public void StartingSquaresCantContainAMine()
        {
            var sqaure = new Square(1, 1, true);
            sqaure.SetAsStartingSquare();
            Assert.False(sqaure.ContainsMine);
        }

        [Fact]
        public void StartingSquaresAreCorrectlySet()
        {
            var sqaure = new Square(1, 1, true);
            sqaure.SetAsStartingSquare();
            Assert.True(sqaure.IsStartingSquare);
        }

        [Fact]
        public void SquaresHaveAMineIfSet() => Assert.True(new Square(1, 1, true).ContainsMine);

        [Fact]
        public void MovingToASquareWithAMineIsNotOk() => Assert.False(new Square(1, 1, true).MoveHere());

        [Fact]
        public void MovingToASquareWithoutAMineIsOk() => Assert.True(new Square(1, 1, false).MoveHere());

        [Fact]
        public void MovingToASquareASecondTimeWithAMineIsOk() 
        {
            var sqaure = new Square(1, 1, true);
            sqaure.MoveHere();
            Assert.True(sqaure.MoveHere());
        }
    }
}
