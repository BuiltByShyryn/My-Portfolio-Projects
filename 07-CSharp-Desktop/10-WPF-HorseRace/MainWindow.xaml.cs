using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace HorseRaceSimulator
{
    public partial class MainWindow : Window
    {
        private Random _rnd = new Random();
        private int _currentRank = 1; // 1st, 2nd, 3rd...
        private object _finishLineLock = new object(); // The "Lock" object

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Reset everything for a new race
            lstResults.Items.Clear();
            _currentRank = 1;

            // Start 5 threads (one for each horse)
            new Thread(() => RunHorse(pbHorse1, "Horse #1")).Start();
            new Thread(() => RunHorse(pbHorse2, "Horse #2")).Start();
            new Thread(() => RunHorse(pbHorse3, "Horse #3")).Start();
            new Thread(() => RunHorse(pbHorse4, "Horse #4")).Start();
            new Thread(() => RunHorse(pbHorse5, "Horse #5")).Start();
        }

        private void RunHorse(ProgressBar horseBar, string name)
        {
            // Reset bar to 0
            Dispatcher.Invoke(() => horseBar.Value = 0);

            // The Racing Loop
            for (int i = 0; i <= 100; i++)
            {
                int speed = _rnd.Next(20, 150); // Random speed for each step
                Dispatcher.Invoke(() => horseBar.Value = i);
                Thread.Sleep(speed);
            }

            // 🏁 THE FINISH LINE 🏁
            lock (_finishLineLock) // Only one thread can enter this block at a time!
            {
                Dispatcher.Invoke(() => {
                    lstResults.Items.Add($"{_currentRank} Place: {name}");
                    _currentRank++;
                });
            }
        }
    }
}