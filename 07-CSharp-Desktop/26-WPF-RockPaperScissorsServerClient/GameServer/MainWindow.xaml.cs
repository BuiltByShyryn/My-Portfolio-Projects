using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace GameServer
{
    public partial class MainWindow : Window
    {
        TcpListener refereeEar;
        List<TcpClient> playerList = new List<TcpClient>();


        string player1Choice = "";
        string player2Choice = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                refereeEar = new TcpListener(IPAddress.Any, 12345);

                refereeEar.Start();

                txtLog.AppendText("SYSTEM: Server started! Waiting for players...\n");

                btnStart.IsEnabled = false;
                Thread waitThread = new Thread(AcceptPlayers);
                waitThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting server: " + ex.Message);
            }
        }

        private void AcceptPlayers()
        {
            try
            {
                while (playerList.Count < 2)
                {
                    TcpClient client = refereeEar.AcceptTcpClient();

                    playerList.Add(client);


                    int num = playerList.Count;
                    Thread t = new Thread(() => HandlePlayer(num));
                    t.Start();

                    string welcome = "Welcome to the game!";
                    byte[] data = Encoding.UTF8.GetBytes(welcome);

                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                    Dispatcher.Invoke(() =>
                    {
                        txtLog.AppendText($"PLAYER {playerList.Count} CONNECTED!\n");
                    });
                }

                Dispatcher.Invoke(() =>
                {
                    txtLog.AppendText("SYSTEM: Match is ready to begin! Both players are here.\n");
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during accepting player: " + ex.Message);
            }
        }
        private void HandlePlayer(int playerNumber)
        {
            TcpClient client = playerList[playerNumber - 1];
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string choice = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (playerNumber == 1) player1Choice = choice;
                    else player2Choice = choice;

                    Dispatcher.Invoke(() => txtLog.AppendText($"Player {playerNumber} chose: {choice}\n"));

                    CheckWinner();
                }
            }
            catch { /* Connection lost */ }
        }
        private void CheckWinner()
        {
            if (player1Choice == "" || player2Choice == "") return;

            string result1 = "";
            string result2 = "";

            if (player1Choice == player2Choice)
            {
                result1 = "DRAW";
                result2 = "DRAW";
            }
            else if ((player1Choice == "ROCK" && player2Choice == "SCISSORS") ||
                     (player1Choice == "PAPER" && player2Choice == "ROCK") ||
                     (player1Choice == "SCISSORS" && player2Choice == "PAPER"))
            {
                result1 = "WIN";
                result2 = "LOSE";
            }
            else
            {
                result1 = "LOSE";
                result2 = "WIN";
            }



            Dispatcher.Invoke(() => txtLog.AppendText($"RESULT: P1 {result1} - P2 {result2}\n"));


            byte[] data1 = Encoding.UTF8.GetBytes(result1);
            playerList[0].GetStream().Write(data1, 0, data1.Length);

            byte[] data2 = Encoding.UTF8.GetBytes(result2);
            playerList[1].GetStream().Write(data2, 0, data2.Length);

            Dispatcher.Invoke(() => txtLog.AppendText($"SENT: P1={result1}, P2={result2}\n"));

            player1Choice = "";


            player2Choice = "";


        }


    }
}