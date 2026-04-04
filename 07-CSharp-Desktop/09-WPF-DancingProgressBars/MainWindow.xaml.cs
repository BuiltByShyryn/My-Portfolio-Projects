using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace DancingProgressBars
{
    public partial class MainWindow : Window
    {
        private Random _rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            BarsContainer.Children.Clear(); // Clean the old bars
            int count = int.Parse(txtBarCount.Text);

            for (int i = 0; i < count; i++)
            {
                // 1. Create a new Bar
                ProgressBar pb = new ProgressBar { Height = 20, Margin = new Thickness(0, 5, 0, 5), Maximum = 100 };

                // 2. Give it a random color (The "Dancing" part!)
                pb.Foreground = new SolidColorBrush(Color.FromRgb((byte)_rnd.Next(256), (byte)_rnd.Next(256), (byte)_rnd.Next(256)));

                BarsContainer.Children.Add(pb);

                // 3. Start a new thread for EACH bar
                Thread t = new Thread(() => UpdateBar(pb));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void UpdateBar(ProgressBar pb)
        {
            int speed = _rnd.Next(20, 100); // Random "dance" speed

            for (int i = 0; i <= 100; i++)
            {
                // IMPORTANT: Only the Main UI Thread can touch the Progress Bar!
                // We use Dispatcher.Invoke to "send a message" to the UI.
                Dispatcher.Invoke(() => {
                    pb.Value = i;
                });

                Thread.Sleep(speed);
            }
        }
    }
}