using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets; // For the Client Socket
using System.Text;
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
using System.Threading;

namespace GameClient
{
    
    public partial class MainWindow : Window
    {

        TcpClient playerVoice;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
         
            try
            {
                
                playerVoice = new TcpClient();

                
                playerVoice.Connect(txtIP.Text, 12345);

                
                lblStatus.Text = "Status: Connected to Referee!";
                lblStatus.Foreground = System.Windows.Media.Brushes.Green;

              
                btnConnect.IsEnabled = false;

                Thread listenThread = new Thread(ListenForResults);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect! Is the Server running?");
            }
        }

        private void SendChoice(string choice)
        {
            try
            {
                if (playerVoice != null && playerVoice.Connected)
                {
                    
                    NetworkStream stream = playerVoice.GetStream();

                   
                    byte[] data = Encoding.UTF8.GetBytes(choice);

                   
                    stream.Write(data, 0, data.Length);

                    lblStatus.Text = $"You chose: {choice}. Waiting for opponent...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lost connection to server!");
            }
        }

        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            SendChoice("ROCK");
        }

        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            SendChoice("PAPER");
        }

        private void btnScissors_Click(object sender, RoutedEventArgs e)
        {
            SendChoice("SCISSORS");
        }

        private void ListenForResults()
        {
            NetworkStream stream = playerVoice.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                   
                    Dispatcher.Invoke(() => {
                        lblStatus.Text = "RESULT: " + result;
                        if (result == "WIN") lblStatus.Foreground = Brushes.Gold;
                        else if (result == "LOSE") lblStatus.Foreground = Brushes.Gray;
                        else lblStatus.Foreground = Brushes.Blue; 
                    });
                }
            }
            catch { /* Server closed */ }
        }

    }

    
}
