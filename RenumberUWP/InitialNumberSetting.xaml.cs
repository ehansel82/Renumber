﻿using Renumber.Logic;
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
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RenumberUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitialNumberSetting : Page
    {
        private TaskCompletionSource<bool> _tcs;

        public InitialNumberSetting()
        {
            this.InitializeComponent();
        }

        public async Task Speak(string text)
        {
            _tcs = new TaskCompletionSource<bool>();
            using (var synth = new SpeechSynthesizer())
            {
                var stream = await synth.SynthesizeTextToStreamAsync(text);
                media.MediaEnded += (o, e) => { _tcs.TrySetResult(true); };
                media.SetSource(stream, stream.ContentType);
                media.Play();
            }
            await _tcs.Task;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock.Text = string.Empty;
            var speaker = new SpeechManager();
            await Speak(textHowManyNumbers.Text);

            while (true)
            {
                var setting = await speaker.ListenForNumberSetting();
                if (!string.IsNullOrWhiteSpace(setting))
                {
                    textBlock.Text = setting;
                    break;
                }
            }
            App.Current.Resources["Game"] = new Game(Convert.ToInt32(textBlock.Text));
            await Task.Delay(2000);
            this.Frame.Navigate(typeof(GamePage));
        }
    }
}
