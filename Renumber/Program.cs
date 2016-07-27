using System;
using System.Collections.Generic;
using System.Speech.Synthesis;

namespace Renumber
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            var random = new Random();
            var numbers = new List<int>();

            synth.Speak("Hi. How many numbers do you want to remember?");
            var numbersToRemember = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numbersToRemember; i++)
            {
                numbers.Add(random.Next(99));
            }

            synth.Speak("Remember these numbers.");
            synth.Rate = 2;
            foreach (var number in numbers)
            {
                synth.Speak(number.ToString());
            }
            for (int i = 0; i < numbersToRemember; i++)
            {
                var entry = Console.ReadLine();
                if (entry != numbers[i].ToString())
                {
                    synth.Speak("WRONG! Please play again.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            synth.Speak("YOU WIN! YAY!");
            Console.ReadLine();
        }
    }
}