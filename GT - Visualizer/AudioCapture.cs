using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT___Visualizer
{
    internal class AudioCapture
    {
        public event Action<float> OnAmplitudeDetected;
        public event Action<int> OnStringDetected;

        private WaveInEvent waveIn;

        private FrequencyAnalyzer frequencyAnalyzer = new FrequencyAnalyzer();

        public event Action<short[]> OnSamplesAvailable;


        public void Start(int deviceNumber)
        {
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = 2; // device number (focusrite scarlett in my situation), replace with your own
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.StartRecording();
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            float maxAmplitude = 0;
            int sampleIndex = 0;
            float[] fftBuffer = new float[16384];

            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                short sample = BitConverter.ToInt16(e.Buffer, i);
                float amplitude = sample / 32768f;
                if (sampleIndex < fftBuffer.Length)
                {
                    fftBuffer[sampleIndex] = amplitude;
                    sampleIndex++;
                }
                maxAmplitude = Math.Max(maxAmplitude, Math.Abs(amplitude));
            }

            float frequency = frequencyAnalyzer.GetDominantFrequency(fftBuffer, 44100);

            int stringIndex = GetStringIndex(frequency);

            if (maxAmplitude > 0.1f)
            {
                //OnAmplitudeDetected?.Invoke(maxAmplitude);

                if (stringIndex >=  0)
                {
                    OnStringDetected?.Invoke(stringIndex);
                }
            }
        }

        private int GetStringIndex(float frequency)
        {
            float[] stringFrequencies = new float[]  // regular guitar string tuning
            {
                329.63f,  // String 0 (E)
                246.94f,  // String 1 (B)
                196.00f,  // String 2 (G)
                146.83f,  // String 3 (D)
                110.00f,  // String 4 (A)
                82.41f    // String 5 (E)
            };

            int closestString = -1;
            float minDifference = float.MaxValue;

            for (int i = 0; i < stringFrequencies.Length; i++)
            {
                float difference = Math.Abs(frequency - stringFrequencies[i]);

                if (difference < minDifference)
                {
                    closestString = i;
                    minDifference = difference;
                }
            }
            if (minDifference < 10)
            {
                return closestString;
            }
            else return -1;
        }

    }
}
