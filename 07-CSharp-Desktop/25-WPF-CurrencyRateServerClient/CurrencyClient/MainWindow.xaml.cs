using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace ClientWPF
{
    public partial class MainWindow : Window
    {
        TcpClient client;
        NetworkStream stream;

        public MainWindow() { InitializeComponent(); }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (client == null || !client.Connected)
                {
                    client = new TcpClient("127.0.0.1", 12345);
                    stream = client.GetStream();

                   
                    byte[] auth = Encoding.UTF8.GetBytes("12345");
                    stream.Write(auth, 0, auth.Length);

                    byte[] response = new byte[100];
                    int bytes = stream.Read(response, 0, response.Length);
                    if (Encoding.UTF8.GetString(response, 0, bytes) != "OK")
                    {
                        txtResponse.Text = "Auth Failed!";
                        client.Close(); return;
                    }
                }

                
                byte[] data = Encoding.UTF8.GetBytes(txtRequest.Text);
                stream.Write(data, 0, data.Length);

                byte[] buf = new byte[100];
                int count = stream.Read(buf, 0, buf.Length);
                txtResponse.Text = Encoding.UTF8.GetString(buf, 0, count);

            }
            catch (Exception ex) { txtResponse.Text = "Error: " + ex.Message; }
        }
    }
}