using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GT___Visualizer
{
    /// <summary>
    /// Interaction logic for TunerWindow.xaml
    /// </summary>
    public partial class TunerWindow : Window
    {
        private AudioCapture audioCapture;
        public TunerWindow(AudioCapture capture)
        {
            InitializeComponent();
            audioCapture = capture;
            audioCapture.OnFrequencyDetected += UpdateTuner;

            this.Closing += (s, e) => audioCapture.OnFrequencyDetected -= UpdateTuner;
        }

        private void UpdateTuner(float frequency)
        {
            Dispatcher.Invoke(() =>
        {
                var (noteName, noteFreq) = GetClosestNote(frequency);
                var cents = CalculateCents(frequency, noteFreq);

                NoteText.Text = noteName;
                FrequencyText.Text = $"{frequency:F1} Hz";
                CentsText.Text = $"{cents:F0} cents";
            });
        }

        private double CalculateCents(float detectedFrequency, float targetFrequency)
        {
            return 1200 * (Math.Log(detectedFrequency / targetFrequency) / Math.Log(2));
        }

        private (string noteName, float targetFrequency) GetClosestNote(float frequency)
        {
            string[] noteNames = { "E", "B", "G", "D", "A", "E" };
            float[] noteFreqs = { 329.63f, 246.94f, 196.00f, 146.83f, 110.00f, 82.41f };

            int closestNote = -1;
            float minDifference = float.MaxValue;

            for (int i = 0; i < noteFreqs.Length; i++)
            {
                float difference = Math.Abs(frequency - noteFreqs[i]);

                if (difference < minDifference)
                {
                    closestNote = i;
                    minDifference = difference;
                }
            }
            if (minDifference < 50)
            {
                return (noteNames[closestNote], noteFreqs[closestNote]);
            }
            else return ("--", 0f);
        }
    }
}
