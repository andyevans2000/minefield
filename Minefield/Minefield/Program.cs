using Microsoft.Extensions.DependencyInjection;
using Minefield.Core;
using Minefield.Core.Contracts;
using System;

namespace Minefield
{
    /// <summary>
    /// console app to play a simple game of minefield
    /// </summary>
    class Program
    {
        private static ServiceProvider _serviceProvider;

        /// <summary>
        /// TODO - add args to change board size, difficulty etc
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //we are using DI - so register our dependencies
            RegisterServices();

            var scope = _serviceProvider.CreateScope();
            //get the game object from the DI container
            var game = scope.ServiceProvider.GetRequiredService<IGame>();
            
            game.Start();
            Console.WriteLine($"You are about to start a minefield game on a board {game.Board.Size} with {game.Lives} lives");
            Console.WriteLine($"Starting position on board is {game.CurrentSquare}. You can move Up (U), Down(D), Left (L) and Right (R)");
            //loop until the game is over
            while (game.GameStatus == GameStatus.InPlay)
            {
                HandlePlay(game);
            }
            //emsure the services ae disposed off correctly
            DisposeServices();
        }

        private static void HandlePlay(IGame game)
        {
            //onvert text input to a move direction
            var moveDirection = GetMoveDirection(Console.ReadLine());
            if (moveDirection == null)
            {
                Console.WriteLine("Input must be U, D, L or R");
                return;
            }
            //make the move in the game
            var result = game.Move(moveDirection.Value);

            //convert the result of the move back to screen for the user
            switch (result)
            {
                case MoveResult.HitMine:
                    Console.WriteLine($"Oops you have hit a mine!");
                    break;
                case MoveResult.Lose:
                    Console.WriteLine($"You have hit a mine and the game is over, you have lost");
                    break;
                case MoveResult.Ok:
                    Console.WriteLine("Good move, you avoided a mine");
                    break;
                case MoveResult.WinHitMine:
                    Console.WriteLine("You have hit a mine and the game is over, but you have won");
                    break;
                case MoveResult.Win:
                    Console.WriteLine("You have won");
                    break;
                default:
                    Console.WriteLine("Invalid move");
                    break;
            }
            if (game.GameStatus == GameStatus.InPlay)
                Console.WriteLine($"Current square is {game.CurrentSquare} - you have made {game.Moves} moves and have {game.Lives} lives left. Move again!");
            else
                Console.WriteLine($"Game over!");
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddMinefieldGame();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
                return;
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        private static MoveDirection? GetMoveDirection(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            
            if (input.Equals("u", StringComparison.OrdinalIgnoreCase))
                return MoveDirection.Up;
            if (input.Equals("d", StringComparison.OrdinalIgnoreCase))
                return MoveDirection.Down;
            if (input.Equals("l", StringComparison.OrdinalIgnoreCase))
                return MoveDirection.Left;
            if (input.Equals("r", StringComparison.OrdinalIgnoreCase))
                return MoveDirection.Right;
            return null;
        }
    }
}
