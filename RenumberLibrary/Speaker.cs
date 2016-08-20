using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;


namespace RenumberLogic
{
    public class Speaker
    {
        private SpeechSynthesizer _synth;
        public Speaker()
        {
            _synth = new SpeechSynthesizer();
            _synth.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string text)
        {
            _synth.Speak(text);
        }
    }
}
