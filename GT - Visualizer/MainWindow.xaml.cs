using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NAudio.Wave;

namespace GT___Visualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // GT Visualizer: Guitar string (and more) visualizer using WPF
        // Each string vibrates when a number key (1-6) is pressed or all at once with Space


        private AudioCapture audioCapture;
        private StringAnimator stringAnimator;
        private FrequencyAnalyzer frequencyAnalyzer;



        public MainWindow()
        {

            //for (int i = 0; i < WaveInEvent.DeviceCount; i++)    // get device number
            //{
            //    var caps = WaveInEvent.GetCapabilities(i);
            //    MessageBox.Show($"Device {i}: {caps.ProductName}");
            //}

            InitializeComponent();
            stringAnimator = new StringAnimator();
            stringAnimator.Initialize(DrawingCanvas, 6);
            frequencyAnalyzer = new FrequencyAnalyzer();


            // Handle key presses
            this.KeyDown += MainWindow_KeyDown;

            // Timer updates string vibrations
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();

            audioCapture = new AudioCapture();
            audioCapture.OnAmplitudeDetected += HandleAmplitude;
            audioCapture.OnStringDetected += HandleStringDetection;
            audioCapture.Start(0);
        }

        private void OpenTuner (object sender, RoutedEventArgs e)
        {
            TunerWindow tuner = new TunerWindow(audioCapture);
            tuner.ShowDialog();
        }

        private void HandleStringDetection(int stringIndex)
        {
            stringAnimator.ActivateString(stringIndex);
        }

        // Timer tick: update string positions based on their individual amplitudes and oscillation time
        private void Timer_Tick(object sender, EventArgs e)
        {
            stringAnimator.Update();
        }

        // Key press: set amplitude for a string or all strings (placeholder, to be replaced by NAudio)
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                stringAnimator.ActivateAll();
            }
            else if (e.Key == Key.D1) stringAnimator.ActivateString(0);
            else if (e.Key == Key.D2) stringAnimator.ActivateString(1);
            else if (e.Key == Key.D3) stringAnimator.ActivateString(2);
            else if (e.Key == Key.D4) stringAnimator.ActivateString(3);
            else if (e.Key == Key.D5) stringAnimator.ActivateString(4);
            else if (e.Key == Key.D6) stringAnimator.ActivateString(5);
        }

        private void HandleAmplitude(float amplitude)
        {
            stringAnimator.Activate(amplitude);
        }
    }
}