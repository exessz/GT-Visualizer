using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;


namespace GT___Visualizer
{
    internal class FrequencyAnalyzer
    {
        public float GetDominantFrequency(float[] samples, int sampleRate)
        {
            Complex[] complexSamples = new Complex[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                complexSamples[i] = new Complex(samples[i], 0);
            }
            Fourier.Forward(complexSamples);

            int maxIndex = 22;
            double maxMagnitude = 0;

            for (int i = 22; i < 400; i++)
            {
                double magnitude = Complex.Abs(complexSamples[i]);
                double WMagnitude = magnitude * (1.0 / Math.Pow(i / 22.0,0.7));

                if (WMagnitude > maxMagnitude)
                {
                    maxIndex = i;
                    maxMagnitude = WMagnitude;
                }
            }


            float frequency = maxIndex * sampleRate / (float)samples.Length;
            return frequency;
        }
    }
}