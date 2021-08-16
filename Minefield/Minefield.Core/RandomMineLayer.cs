using Minefield.Core.Contracts;
using System;

namespace Minefield.Core
{
    /// <summary>
    /// An implementation that lays lines at random, based on difficulty
    /// </summary>
    public class RandomMineLayer : IMineLayer
    {
        private readonly Random _random = new Random();
        private readonly int _mineChance;

        public bool TryToLayMine() => _random.Next(0, 100) < _mineChance;

        public RandomMineLayer(Configuration configuration)
        {
            if (configuration.Difficulty == Difficulty.Easy)
                _mineChance = 10;
            if (configuration.Difficulty == Difficulty.Medium)
                _mineChance = 30;
            if (configuration.Difficulty == Difficulty.Hard)
                _mineChance = 50; //50% chance of the square being a mine
        }
    }
}
