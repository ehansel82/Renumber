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
            textListenerState.Text = string.Empty;
            textNumbers.Text = string.Empty;
            var speechManager = App.Current.Resources["SpeechManager"] as SpeechManager;
            speechManager.ListenerStateChanged += SpeechManager_ListenerStateChanged;
            await base.Speak("OK, let's play.", media);
            await base.Speak("Remember these numbers.", media);

            foreach(var number in _game.Numbers)
            {
                await base.Speak(number.ToString(), media);
            }

            await base.Speak("Tell me my numbers.", media);
            
            while(_game.Status == GameStatus.IN_PROGRESS)
            {
                while (true)
                {
                    var answer = await speechManager.ListenForNumber();
                    if (!string.IsNullOrWhiteSpace(answer))
                    {
                        if (string.IsNullOrWhiteSpace(textNumbers.Text))
                            textNumbers.Text += answer;
                        else
                            textNumbers.Text += $", {answer}";
                        
                        _game.Guess(Convert.ToInt32(answer));
                        if(_game.Status == GameStatus.WON)
                        {
                            await base.Speak("YOU WON THE GAME! CONGRATULATIONS!", media);
                            break;
                        }
                        else if (_game.Status == GameStatus.LOST){
                            await base.Speak("That was wrong. You lost the game. Goodbye.", media);
                            break;
                        }
                    }
                }
            } 
        }
        private async void SpeechManager_ListenerStateChanged(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognizerStateChangedEventArgs args)
        {
            await textListenerState.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var speechManager = App.Current.Resources["SpeechManager"] as SpeechManager;
                textListenerState.Text = speechManager.ListenerState.ToString();
            });
        }
    }
}
