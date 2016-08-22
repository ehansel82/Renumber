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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RenumberUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InitialNumberSetting));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var speechManager = new SpeechManager();
            await speechManager.Initialize();
            App.Current.Resources["SpeechManager"] = speechManager;
        }

        private void gameDifficulty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gameDifficulty.SelectedIndex == 0)
                App.Current.Resources["Game"] = new Game(true);
            else
                App.Current.Resources["Game"] = new Game(false);
        }
    }
}
