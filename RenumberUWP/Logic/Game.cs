using System;
using System.Collections.Generic;

namespace Renumber.Logic
{
    public class Game
    {
        private List<int> _numbers;
        private Random _random;

        public Game(int numberCount)
        {
            _initializeNumbers(numberCount);
        }

        private void _initializeNumbers(int numberCount)
        {
            _numbers = new List<int>();
            for (int i = 0; i < numberCount; i++)
            {
                _numbers.Add(_random.Next(99));
            }
        }
    }
}