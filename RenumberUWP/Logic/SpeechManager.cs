using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace Renumber.Logic
{
    public class SpeechManager
    {

        private SpeechRecognizer _speechRecognizer;
        private List<string> _constraintsList;

        public SpeechManager()
        {
            _constraintsList = new List<string>();
            for (int i = 0; i < 99; i++)
            {
                _constraintsList.Add((i + 1).ToString());
            }
            _speechRecognizer = new SpeechRecognizer();

            _speechRecognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(5.0);
            _speechRecognizer.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(4.0);
            _speechRecognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(.5);
        }

        public async Task Initialize()
        {
            var constraints = new SpeechRecognitionListConstraint(_constraintsList);
            _speechRecognizer.Constraints.Clear();
            _speechRecognizer.Constraints.Add(constraints);
            await _speechRecognizer.CompileConstraintsAsync();
        }

        public SpeechRecognizerState ListenerState
        {
            get
            {
                return _speechRecognizer.State;
            }
        }

        public event TypedEventHandler<SpeechRecognizer, SpeechRecognizerStateChangedEventArgs> ListenerStateChanged
        {
            add { _speechRecognizer.StateChanged += value; }
            remove { _speechRecognizer.StateChanged -= value; }
        }
           
        public async Task<string> ListenForNumber()
        {
            var speechRecognitionResult = await _speechRecognizer.RecognizeAsync();
            return speechRecognitionResult.Text;
        }
    }
}