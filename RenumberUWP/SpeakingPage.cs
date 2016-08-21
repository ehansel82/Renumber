using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RenumberUWP
{
    public class SpeakingPage : Page
    {
        protected TaskCompletionSource<bool> _tcs;

        protected async Task Speak(string text, MediaElement media)
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

        protected void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            _tcs.TrySetResult(true);
        }
    }
}
