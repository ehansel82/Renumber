using System;
using System.Collections.Generic;

namespace Renumber.Logic
{
    public enum GameStatus
    {
        INITIAL, IN_PROGRESS, WON, LOST
    }

    public class Game
    {
        private int _currentNumber;
        private List<int> _playerNumbers;
        private Random _random;

        public List<int> Numbers { get; set; }

        public Game(int numberCount)
        {
            this.Status = GameStatus.INITIAL;
            _currentNumber = 0;
            _random = new Random();
            _playerNumbers = new List<int>();
            _initializeNumbers(numberCount);
        }

        public GameStatus Status { get; set; }
        public GameStatus Guess(int number)
        {
            Status = GameStatus.IN_PROGRESS;
            _playerNumbers.Add(number);
            if (number != Numbers[_currentNumber])
            {
                Status = GameStatus.LOST;
            }
            else if (_playerNumbers.Count == Numbers.Count)
            {
                Status = GameStatus.WON;
            }
            else
            {
                _currentNumber += 1;
            }
            return Status;
        }

        private void _initializeNumbers(int numberCount)
        {
            Numbers = new List<int>();
            for (int i = 0; i < numberCount; i++)
            {
                Numbers.Add(_random.Next(99));
            }
        }
    }
}