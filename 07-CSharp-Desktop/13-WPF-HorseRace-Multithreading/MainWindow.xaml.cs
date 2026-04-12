using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HorseRace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<ProgressBar> horses;
        Random rnd = new Random();
        object lockObj = new object();
        bool raceFinished = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartRace(object sender, RoutedEventArgs e)
        {
            horses = new List<ProgressBar> { horse1, horse2, horse3, horse4, horse5 };

            foreach (var horse in horses)
            {
                Thread t = new Thread(() => RunHorse(horse));
                t.Start();
            }
        }


        private void RunHorse(ProgressBar horse)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            while (!raceFinished)
            {
                int step = rnd.Next(1, 5);

                Dispatcher.Invoke(() =>
                {
                    if (horse.Value < 100)
                        horse.Value += step;
                });

                bool finished = false;

                Dispatcher.Invoke(() =>
                {
                    if (horse.Value >= 100)
                        finished = true;
                });

                if (finished)
                {
                    lock (lockObj)
                    {
                        if (!raceFinished)
                        {
                            raceFinished = true;

                            Dispatcher.Invoke(() =>
                            {
                                MessageBox.Show("Winner!");
                            });
                        }
                    }
                }

                Thread.Sleep(100);
            }
        }
    }
}
