using Renumber.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechRecognition;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RenumberUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitialNumberSetting : Page
    {
        public InitialNumberSetting()
        {
            this.InitializeComponent();
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Resources["Game"] = new Game(Convert.ToInt32(textNumbers.Text));       
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var speaker = new Speaker();
            await speaker.Speak(textHowManyNumbers.Text);

            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

            var constraints = new SpeechRecognitionListConstraint(new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });

            speechRecognizer.Constraints.Add(constraints);

            // Compile the dictation grammar by default.
            await speechRecognizer.CompileConstraintsAsync();

            // Start recognition.
            while (true)
            {
                SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();
                if (!string.IsNullOrWhiteSpace(speechRecognitionResult.Text))
                {
                    textNumbers.Text = speechRecognitionResult.Text;
                    break;
                }
            }
        }
    }
}
