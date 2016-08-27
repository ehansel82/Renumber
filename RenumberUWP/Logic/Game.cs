using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Renumber.Logic
{
    public enum GameStatus
    {
        IN_PROGRESS, WON, LOST
    }

    public class Game
    {
        private int _currentNumber;
        private List<int> _playerNumbers;
        private Random _random;
        private int _numberCount;

        public List<int> Numbers { get; set; }
        public bool EasyNumbers { get; set; }

        public delegate void GuessHandler(Game game, EventArgs args);

        public event GuessHandler onGuess;

        public List<int> PlayerNumbers
        {
            get
            {
                return _playerNumbers;
            }
        }

        public int NumberCount
        {
            get
            {
                return _numberCount;
            }
            set
            {
                _numberCount = value;
                _initializeNumbers(_numberCount);
            }
        }

        public Game(bool easyNumbers)
        {
            this.Status = GameStatus.IN_PROGRESS;
            _currentNumber = 0;
            _random = new Random();
            _playerNumbers = new List<int>();
            EasyNumbers = easyNumbers;
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
            if (onGuess != null)
                onGuess(this, EventArgs.Empty);

            return Status;
        }

        private void _initializeNumbers(int numberCount)
        {
            Numbers = new List<int>();
            for (int i = 0; i < numberCount; i++)
            {
                Numbers.Add(_random.Next(1,EasyNumbers ? 9 : 99));
            }
        }
    }
}