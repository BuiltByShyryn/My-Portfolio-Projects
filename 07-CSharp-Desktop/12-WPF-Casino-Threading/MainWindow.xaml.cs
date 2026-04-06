using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;

namespace Homework_Casino_Semaphore
{
    
    public class PlayerInfo
    {
        public string Name { get; set; }
        public int StartBalance { get; set; }
        public int EndBalance { get; set; }
    }

    public partial class MainWindow : Window
    {
        
        Semaphore table = new Semaphore(5, 5);
        List<PlayerInfo> allPlayersReport = new List<PlayerInfo>();
        Random globalRnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false; 
            lblStatus.Text = "Casino is running...";

            
            Thread dayThread = new Thread(SimulateDay);
            dayThread.Start();
        }

        void SimulateDay()
        {
            
            int totalPlayersCount = globalRnd.Next(20, 101);
            List<Thread> threads = new List<Thread>();

            for (int i = 1; i <= totalPlayersCount; i++)
            {
                
                int money = globalRnd.Next(100, 1001);
                PlayerInfo p = new PlayerInfo { Name = $"Игрок{i}", StartBalance = money };

                
                Thread t = new Thread(() => PlayGame(p));
                threads.Add(t);
                t.Start();
            }

            
            foreach (var t in threads) t.Join();

            
            WriteFile();

            
            Dispatcher.Invoke(() => {
                lblStatus.Text = "Day finished! Report saved.";
                btnStart.IsEnabled = true;
                MessageBox.Show($"Done! Total players: {totalPlayersCount}");
            });
        }

        void PlayGame(PlayerInfo p)
        {
            table.WaitOne(); 

            int currentMoney = p.StartBalance;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int roundsPlayed = 0; 

           
            while (currentMoney > 0 && roundsPlayed < 50)
            {
                
                int maxPossibleBet = Math.Max(1, currentMoney / 2);
                int minPossibleBet = 1;

                int bet = rnd.Next(minPossibleBet, maxPossibleBet + 1); 
                int myNumber = rnd.Next(0, 37);
                int rouletteResult = rnd.Next(0, 37);

                if (myNumber == rouletteResult)
                    currentMoney += (bet * 35);  
                else
                    currentMoney -= bet;

                roundsPlayed++;
                Thread.Sleep(5); 
            }

            p.EndBalance = currentMoney;

            lock (allPlayersReport)
            {
                allPlayersReport.Add(p);
            }

            table.Release(); 
        }

        void WriteFile()
        {
            
            using (StreamWriter sw = new StreamWriter("casino_report.txt"))
            {
                foreach (var p in allPlayersReport)
                {
                    sw.WriteLine($"{p.Name} [{p.StartBalance}] [{p.EndBalance}]");
                }
            }
        }
    }
}