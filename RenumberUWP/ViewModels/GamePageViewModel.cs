using Renumber.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenumberUWP
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        private Game _game;

        public event PropertyChangedEventHandler PropertyChanged;

        public string YourNumbers { get; set; }

        public GamePageViewModel()
        {
        }

        public void SetGame(Game game)
        {
            _game = game;
            _game.onGuess += Guess_Handler;
        }

        private void Guess_Handler (Game game, EventArgs args)
        {

            YourNumbers = string.Empty;
            for(int i=0;i<_game.PlayerNumbers.Count; i++)
            {
                if (i != 0)
                    YourNumbers += " ";

                YourNumbers += _game.PlayerNumbers[i];

                if (i != _game.PlayerNumbers.Count - 1)
                    YourNumbers += ",";
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("YourNumbers"));
            }
        }
    }
}
