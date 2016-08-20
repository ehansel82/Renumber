using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.SpeechSynthesis;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Renumber.Logic
{
    public class Speaker
    {
        MediaElement _mediaElement;

        private SpeechSynthesizer _synth;
        public Speaker()
        {
            _mediaElement = new MediaElement();
            _synth = new SpeechSynthesizer();
        }

        public async Task Speak(string text)
        {
            var stream = await _synth.SynthesizeTextToStreamAsync(text);
            _mediaElement.SetSource(stream, stream.ContentType);
            _mediaElement.Play();
        }
    }
}
