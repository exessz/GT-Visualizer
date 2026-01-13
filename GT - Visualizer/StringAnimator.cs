using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media;

namespace GT___Visualizer
{
    internal class StringAnimator
    {
        Line[] strings = new Line[6];  // Lines representing the strings
        double[] currentAmplitudes = new double[6];  // Vibration strength for each string
        double[] times = new double[6]; // Individual time counters for independent oscillations
        int amplitude = 20;  // Max amplitude when a string is "plucked"
        double currentAmplitude = 0; // Starting amplitude
        int startY = 100; // Base vertical offset
        double[] baseY = new double[6]; // Base Y position for each string


        public void Initialize(Canvas canvas, int stringCount)
        {
            int startX = 50;
            int endX = 650;
            int spacing = 50;

            // Create strings and add them to the canvas
            for (int i = 0; i < stringCount; i++)
            {
                Line horizontalLine = new Line();
                baseY[i] = startY + i * spacing;
                horizontalLine.X1 = startX;
                horizontalLine.Y1 = startY + i * spacing;
                horizontalLine.X2 = endX;
                horizontalLine.Y2 = startY + i * spacing;

                horizontalLine.Stroke = Brushes.Red;
                horizontalLine.StrokeThickness = 2;

                strings[i] = horizontalLine;
                currentAmplitudes[i] = currentAmplitude;

                canvas.Children.Add(horizontalLine);

            }
        }

        public void Update()
        {
            for (int i = 0; i < 6; i++)
            {
                times[i] += 0.1;

                currentAmplitudes[i] *= 0.98;
                double offset = Math.Sin( times[i]) * currentAmplitudes[i];
                strings[i].Y1 = baseY[i] + offset;
                strings[i].Y2 = baseY[i] + offset;
            }
        }

        public void Activate(float amplitude)
        {
            for (int i = 0; i < currentAmplitudes.Length; i++)
            {
                currentAmplitudes[i] = amplitude * 50;
            }
        }

        public void ActivateAll()
        {
            for (int i = 0; i < currentAmplitudes.Length; i++)
            {
                currentAmplitudes[i] = amplitude;
            }
        }

        public void ActivateString(int stringIndex)
        {
            currentAmplitudes[stringIndex] = amplitude;
        }
    }
}
