using Minefield.Core.Contracts;
using System.Linq;

namespace Minefield.Core
{
    public class Game : IGame
    {   
        private readonly IBoardCreator _boardCreator;
        private readonly Configuration _config;
        public const int MaxLives = 99;
        public const int MinLives = 1;

        public Game(IBoardCreator boardCreator, Configuration config)
        {
            _boardCreator = boardCreator;
            _config = config;
            Lives = config.NumLives;
            GameStatus = GameStatus.NotStarted;
        }

        public void Start()
        {
            Board = _boardCreator.CreateBoard(_config.BoardSize);
            CurrentSquare = Board.Squares.First(s => s.IsStartingSquare);
            GameStatus = GameStatus.InPlay;
        }
        /// <summary>
        /// The playing board
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// The current number of lives in the game, when the lives reaches zero the game ends
        /// </summary>
        public int Lives { get; private set; }

        /// <summary>
        /// the number of moves in the game, this starts at 0
        /// </summary>
        public int Moves { get; private set; }

        /// <summary>
        /// Status of the game
        /// </summary>
        public GameStatus GameStatus { get; private set; }

        /// <summary>
        /// Represents the current square in the game
        /// </summary>
        public Square CurrentSquare { get; private set; }

        /// <summary>
        /// Move to a new sqaure
        /// </summary>
        /// <param name="moveDirection">Up, down, left or right</param>
        /// <returns>The result of the move, could have been ok, hit a mine, lost the game or won the game</returns>
        public MoveResult Move(MoveDirection moveDirection)
        {
            //is the game already over
            if (GameStatus != GameStatus.InPlay)
                throw new GameOverException("Cannot move because the game is over!");

            var nextSquare = GetNextSquare(moveDirection);
            if (nextSquare == null) //is this a valid move - e.g. can't move left if you are at the left edge of the board already
                return MoveResult.InvalidMove;
            var okMove = nextSquare.MoveHere();
            Moves++;
            CurrentSquare = nextSquare;
            if (okMove) //the move was to a square with no mine
            {   
                if (CurrentSquare.Row == Board.Size) //the user has reached the other side so must have won
                {
                    GameStatus = GameStatus.Won;
                    return MoveResult.Win;
                }
                return MoveResult.Ok;
            }
            else //the move was to a sqaure with a mine
            {
                Lives--;
                if (Lives == 0) //there are now no lives so the game is lost
                {
                    GameStatus = GameStatus.Lost;
                    return MoveResult.Lose;
                }
                else if (CurrentSquare.Row == Board.Size) //the user has hit a mine on the last row so has still won as they have lives left
                {
                    GameStatus = GameStatus.Won;
                    return MoveResult.WinHitMine;
                }
                return MoveResult.HitMine;
            }
        }

        /// <summary>
        /// get the next sqaure that the user is trying to move to based on the position of the current square
        /// the next sqaure has to be next to the current sqaure or if the next sqaure does not exist the move must be invalid
        /// </summary>
        /// <param name="moveDirection"></param>
        /// <returns></returns>
        private Square GetNextSquare(MoveDirection moveDirection)
        {
            //up increments the row by 1
            if (moveDirection == MoveDirection.Up)
                return Board.Squares.FirstOrDefault(s => s.Column == CurrentSquare.Column && s.Row == CurrentSquare.Row + 1);
            // up decrements the row by 1
            if (moveDirection == MoveDirection.Down)
                return Board.Squares.FirstOrDefault(s => s.Column == CurrentSquare.Column && s.Row == CurrentSquare.Row - 1);
            //up increments the column by 1
            if (moveDirection == MoveDirection.Right)
                return Board.Squares.FirstOrDefault(s => s.Column == CurrentSquare.Column + 1 && s.Row == CurrentSquare.Row);
            // left decrements the column by 1
            return Board.Squares.FirstOrDefault(s => s.Column == CurrentSquare.Column - 1 && s.Row == CurrentSquare.Row);
        }
    }
}
