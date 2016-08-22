using Renumber.Logic;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RenumberUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitialNumberSetting : SpeakingPage
    {
        public InitialNumberSetting()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock.Text = string.Empty;
            var speechManager = App.Current.Resources["SpeechManager"] as SpeechManager;
            await base.Speak(textHowManyNumbers.Text, media);

            while (true)
            {
                var setting = await speechManager.ListenForNumber();
                if (!string.IsNullOrWhiteSpace(setting))
                {
                    textBlock.Text = setting;
                    break;
                }
            }
            var game = App.Current.Resources["Game"] as Game;
            game.NumberCount = Convert.ToInt32(textBlock.Text);
            await Task.Delay(1500);
            base.Frame.Navigate(typeof(GamePage));
        }
    }
}