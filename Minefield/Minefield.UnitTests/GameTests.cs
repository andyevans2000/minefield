using Minefield.Core;
using Minefield.Core.Contracts;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Minefield.UnitTests
{
    public class GameTests
    {
        private readonly Mock<IBoardCreator> _mockBoardCreator = new Mock<IBoardCreator>();
        private readonly Square _startingSquare;
        private readonly List<Square> _sqaures;
        private readonly Board _board;
        private const int NumLives = 2;

        public GameTests()
        {
            _startingSquare = new Square(1, 2, false);
            _startingSquare.SetAsStartingSquare();
            _sqaures = new List<Square>
            {
                new Square(1, 1, true),
                _startingSquare,
                new Square(1, 3, false),
                new Square(2, 1, true),
                new Square(2, 2, false),
                new Square(2, 3, true),
                new Square(3, 1, true),
                new Square(3, 2, false),
                new Square(3, 3, true),
            };
            _board = new Board(_sqaures, 3);
        }

        [Fact]
        public void NewGameHasNotStartedStatus() => Assert.Equal(GameStatus.NotStarted, new Game(_mockBoardCreator.Object, new Configuration()).GameStatus);

        [Fact]
        public void StartedGameHasInPlayStatus() => Assert.Equal(GameStatus.InPlay, SetupBasicBoardAndStartGame().GameStatus);

        [Fact]
        public void StartedGameHasCorrectStartingSquare()
        {
            var game = SetupBasicBoardAndStartGame();

            Assert.Equal(_startingSquare.Row,game.CurrentSquare.Row);
            Assert.Equal(_startingSquare.Column, game.CurrentSquare.Column);
        }

        [Fact]
        public void MovingToASquareIncrementsMovesCorrectly()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Left);
            Assert.Equal(1, game.Moves);
        }

        [Fact]
        public void AtStartOfGameMovesAreZero() => Assert.Equal(0, SetupBasicBoardAndStartGame().Moves);

        [Fact]
        public void AtStartOfGameLivesAreCorrect() => Assert.Equal(NumLives, SetupBasicBoardAndStartGame().Lives);

        [Fact]
        public void MoveToUnminedSqaureLivesAreCorrect()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Up);
            Assert.Equal(NumLives, game.Lives);
        }

        [Fact]
        public void MoveToMinedSquareLivesAreCorrect()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Left); //left of start is mined
            Assert.Equal(NumLives-1, game.Lives);
        }

        [Fact]
        public void MoveToUnminedSquareGivesOkResult() => Assert.Equal(MoveResult.Ok, SetupBasicBoardAndStartGame().Move(MoveDirection.Up));

        [Fact]
        public void MoveToMinedSquareGivesHitMineResult() => Assert.Equal(MoveResult.HitMine, SetupBasicBoardAndStartGame().Move(MoveDirection.Left));

        [Fact]
        public void ImpossibleMoveGivesInvalidMoveResult() => Assert.Equal(MoveResult.InvalidMove, SetupBasicBoardAndStartGame().Move(MoveDirection.Down));

        [Fact]
        public void MoveToMinedSquareandLoseAllLivesGivesLoseResult()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Left); 
            var result = game.Move(MoveDirection.Up);
            Assert.Equal(MoveResult.Lose, result);
        }

        [Fact]
        public void TryingToMoveWhenGameIsOverThrowsGameOverException()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Left);
            game.Move(MoveDirection.Up);
            Assert.Throws<GameOverException>(() => game.Move(MoveDirection.Up));
        }

        [Fact]
        public void MoveToEndWithLivesLeftGivesWinResult()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Up); 
            var result = game.Move(MoveDirection.Up);
            Assert.Equal(MoveResult.Win, result);
        }

        [Fact]
        public void WinGameHasWonStatus()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Up); 
            game.Move(MoveDirection.Up);
            Assert.Equal(GameStatus.Won, game.GameStatus);
        }

        [Fact]
        public void LoseGameHasLoseStatus()
        {
            var game = SetupBasicBoardAndStartGame();
            game.Move(MoveDirection.Left);
            game.Move(MoveDirection.Up);
            Assert.Equal(GameStatus.Lost, game.GameStatus);
        }

        private Game SetupBasicBoardAndStartGame()
        {
            _mockBoardCreator.Setup(m => m.CreateBoard(10)).Returns(_board);
            var game = new Game(_mockBoardCreator.Object, new Configuration(numLives: NumLives));
            game.Start();

            return game;
        }
    }
}
