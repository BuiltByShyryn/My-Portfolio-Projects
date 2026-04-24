using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StarHunter_Exam
{
    public partial class GameWindow : Window
    {
        private Ellipse player; 
        private double playerX = 350;
        private double playerY = 250;

        private List<Ellipse> carrots = new List<Ellipse>();
        private int score = 0;
        private int botScore = 0;
        private readonly object scoreLock = new object(); 
        private Random rnd = new Random();

        private List<Ellipse> cows = new List<Ellipse>();

        
        private Semaphore semaphore = new Semaphore(2, 2);

        private HashSet<Key> pressedKeys = new HashSet<Key>();

        private bool isGameOver = false;

        public GameWindow()
        {
            InitializeComponent();
            CreatePlayer();
            SpawnCarrots();

            this.KeyDown += (s, e) => pressedKeys.Add(e.Key);
            this.KeyUp += (s, e) => pressedKeys.Remove(e.Key);

            CompositionTarget.Rendering += GameLoop;

            for (int i = 0; i < 3; i++)
            {
                Thread cowThread = new Thread(CowRoutine);
                cowThread.IsBackground = true;
                cowThread.Start();
            }
        }

        private void CreatePlayer()
        {
            playerX = 380;
            playerY = 280;

            player = new Ellipse
            {
                Width = 35,
                Height = 35,
                Fill = Brushes.DeepSkyBlue,
                Stroke = Brushes.AliceBlue,
                StrokeThickness = 3
            };

            System.Windows.Media.Effects.DropShadowEffect glow = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Colors.DodgerBlue,
                BlurRadius = 20,
                ShadowDepth = 0
            };
            player.Effect = glow;

            GameCanvas.Children.Add(player);

            Canvas.SetLeft(player, playerX);
            Canvas.SetTop(player, playerY);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (isGameOver) return;

            double speed = 0.5;

            if (pressedKeys.Contains(Key.W)) playerY -= speed;
            if (pressedKeys.Contains(Key.S)) playerY += speed;
            if (pressedKeys.Contains(Key.A)) playerX -= speed;
            if (pressedKeys.Contains(Key.D)) playerX += speed;

            if (GameCanvas.ActualWidth > 0 && GameCanvas.ActualHeight > 0)
            {
                if (playerY < 0) playerY = 0;
                if (playerY > GameCanvas.ActualHeight - 40) playerY = GameCanvas.ActualHeight - 40;
                if (playerX < 0) playerX = 0;
                if (playerX > GameCanvas.ActualWidth - 40) playerX = GameCanvas.ActualWidth - 40;
            }

            bool isNearCow = false;

            foreach (var cow in cows)
            {
                double cX = Canvas.GetLeft(cow);
                double cY = Canvas.GetTop(cow);

                if (Math.Abs(playerX - cX) < 50 && Math.Abs(playerY - cY) < 50)
                {
                    isNearCow = true;
                }

                if (Math.Abs(playerX - cX) < 30 && Math.Abs(playerY - cY) < 30)
                {
                    playerX = 380;
                    playerY = 280;

                    if (score > 0) score--;
                    lblPlayerScore.Text = $"🐰 Bunny: {score}";

                    player.Fill = Brushes.Red;
                }
            }

            if (isNearCow)
            {
                player.Fill = Brushes.Red;
                player.Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.OrangeRed,
                    BlurRadius = 25,
                    ShadowDepth = 0
                };
                lblOuch.Visibility = Visibility.Visible;
            }
            else
            {
                player.Fill = Brushes.DeepSkyBlue;
                player.Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.DodgerBlue,
                    BlurRadius = 20,
                    ShadowDepth = 0
                };
                lblOuch.Visibility = Visibility.Collapsed;
            }

            UpdatePlayerPosition();
            CheckCollision();
            CheckForGameOver();
        }

        private void SpawnCarrots()
        {
            for (int i = 0; i < 50; i++)
            {
                Ellipse carrot = new Ellipse
                {
                    Width = 15,
                    Height = 15,
                    Fill = Brushes.Gold,
                    Stroke = Brushes.Orange,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(carrot, rnd.Next(20, 800 - 40));
                Canvas.SetTop(carrot, rnd.Next(20, 450 - 100));

                GameCanvas.Children.Add(carrot);
                carrots.Add(carrot);
            }
        }

        private void UpdatePlayerPosition()
        {
            Canvas.SetLeft(player, playerX);
            Canvas.SetTop(player, playerY);
        }

        private void CheckCollision()
        {
            List<Ellipse> eatenCarrots = new List<Ellipse>();

            foreach (var carrot in carrots)
            {
                double cX = Canvas.GetLeft(carrot);
                double cY = Canvas.GetTop(carrot);

                if (Math.Abs(playerX - cX) < 30 && Math.Abs(playerY - cY) < 30)
                {
                    eatenCarrots.Add(carrot);
                }
            }

            lock (scoreLock)
            {
                foreach (var carrot in eatenCarrots)
                {
                    if (carrots.Contains(carrot))
                    {
                        GameCanvas.Children.Remove(carrot);
                        carrots.Remove(carrot);
                        score++;

                        lblPlayerScore.Text = $"🐰 Bunny: {score}";
                        progGame.Value = score + botScore;
                    }
                }
            }

            if (carrots.Count == 0)
            {
                CheckForGameOver();
            }
        }

        private void CowRoutine()
        {
            Ellipse cow = null;
            double cowSpeed = rnd.NextDouble() + 0.5;
            int myTargetIndex = 0;

            Dispatcher.Invoke(() =>
            {
                cow = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.MediumPurple,
                    Stroke = Brushes.Plum,
                    StrokeThickness = 2
                };

                System.Windows.Media.Effects.DropShadowEffect rivalGlow = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.Purple,
                    BlurRadius = 10,
                    ShadowDepth = 0
                };
                cow.Effect = rivalGlow;

                GameCanvas.Children.Add(cow);
                cows.Add(cow);

                Canvas.SetLeft(cow, rnd.Next(0, 700));
                Canvas.SetTop(cow, rnd.Next(0, 500));
            });

            while (!isGameOver)
            {
                semaphore.WaitOne();

                Dispatcher.Invoke(() =>
                {
                    lock (scoreLock)
                    {
                        if (carrots.Count == 0 || isGameOver) return;

                        myTargetIndex = cows.IndexOf(cow) % carrots.Count;
                        var targetCarrot = carrots[myTargetIndex];

                        double cowX = Canvas.GetLeft(cow);
                        double cowY = Canvas.GetTop(cow);
                        double targetX = Canvas.GetLeft(targetCarrot);
                        double targetY = Canvas.GetTop(targetCarrot);

                        if (cowX < targetX) cowX += cowSpeed; else cowX -= cowSpeed;
                        if (cowY < targetY) cowY += cowSpeed; else cowY -= cowSpeed;

                        if (cowX < 0) cowX = 0;
                        if (cowY < 0) cowY = 0;
                        if (cowX > GameCanvas.ActualWidth - 25) cowX = GameCanvas.ActualWidth - 25;
                        if (cowY > GameCanvas.ActualHeight - 25) cowY = GameCanvas.ActualHeight - 25;

                        Canvas.SetLeft(cow, cowX);
                        Canvas.SetTop(cow, cowY);

                        if (Math.Abs(cowX - targetX) < 15 && Math.Abs(cowY - targetY) < 15)
                        {
                            GameCanvas.Children.Remove(targetCarrot);
                            carrots.Remove(targetCarrot);
                            botScore++;
                            lblBotScore.Text = $"🐮 Cows: {botScore}";
                            progGame.Value = score + botScore;
                            CheckForGameOver();
                        }
                    }
                });

                semaphore.Release();
                Thread.Sleep(10);
            }
        }

        private void CheckForGameOver()
        {
            if (carrots.Count == 0 && isGameOver == false)
            {
                isGameOver = true;

                string message = (score > botScore)
                    ? "✨ BLUE BUNNY VICTORIOUS! ✨"
                    : "💜 Purple Carrot Cows stole the harvest...";

                MessageBox.Show(message + $"\n\nFinal Stash:\nBlue Bunny: {score}\nPurple Cows: {botScore}");

                MainWindow mainMenu = new MainWindow();
                mainMenu.Show();

                this.Close();
            }
        }
    }
}
