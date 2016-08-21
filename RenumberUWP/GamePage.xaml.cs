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
    public sealed partial class GamePage : Page
    {
        private TaskCompletionSource<bool> _tcs;
        private Game _game;
        public GamePage()
        {
            this.InitializeComponent();
            _game = App.Current.Resources["Game"] as Game;
        }

        public async Task Speak(string text)
        {
            _tcs = new TaskCompletionSource<bool>();
            using (var synth = new SpeechSynthesizer())
            {
                var stream = await synth.SynthesizeTextToStreamAsync(text);
                media.MediaEnded -= Media_MediaEnded;
                media.MediaEnded += Media_MediaEnded;
                media.SetSource(stream, stream.ContentType);
                media.Play();
            }
            await _tcs.Task;
        }

        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            _tcs.TrySetResult(true); 
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Speak("OK, let's play.");
            await Speak("Remember these numbers.");

            foreach(var number in _game.Numbers)
            {
                await Speak(number.ToString());
            }
            
        }
    }
}
