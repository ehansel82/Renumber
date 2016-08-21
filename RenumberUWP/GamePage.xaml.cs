using Renumber.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RenumberUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : SpeakingPage
    {
        private Game _game;
        public GamePage()
        {
            this.InitializeComponent();
            _game = App.Current.Resources["Game"] as Game;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await base.Speak("OK, let's play.", media);
            await base.Speak("Remember these numbers.", media);

            foreach(var number in _game.Numbers)
            {
                await base.Speak(number.ToString(), media);
            }
            
        }
    }
}
