using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace ServerWPF
{
    public partial class MainWindow : Window
    {
        private SimpleServer server;

        public MainWindow()
        {
            InitializeComponent();
           
            server = new SimpleServer(this);
        }

        private void StartServer_Click(object sender, RoutedEventArgs e)
        {
            Thread srvThread = new Thread(server.Start);
            srvThread.IsBackground = true;
            srvThread.Start();
            AddToLog("SYSTEM: Server thread started...");
            MessageBox.Show("Server is running!");
        }

        private void StopServer_Click(object sender, RoutedEventArgs e)
        {
            server.Stop();
            AddToLog("SYSTEM: Server stopped.");
            MessageBox.Show("Server stopped!");
        }

        
        public void AddToLog(string message)
        {
            Dispatcher.Invoke(() => {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
                txtLog.ScrollToEnd();
            });
        }
    }

    class SimpleServer
    {
        private TcpListener listener;
        private bool running;
        private MainWindow _parent; 
        int clientsCount = 0;
        int maxClients = 5;
        int maxRequests = 10;
        string myPass = "12345";

        public SimpleServer(MainWindow parent) { _parent = parent; }

        public void Start()
        {
            running = true;
            listener = new TcpListener(IPAddress.Any, 12345);
            listener.Start();

            while (running)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    if (clientsCount >= maxClients)
                    {
                        Send(client, "BUSY");
                        client.Close();
                        continue;
                    }

                    clientsCount++;
                    Thread t = new Thread(HandleOneClient);
                    t.IsBackground = true;
                    t.Start(client);
                }
                catch { break; }
            }
        }

        private void HandleOneClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            string clientIp = client.Client.RemoteEndPoint.ToString();
            _parent.AddToLog($"CONNECT: {clientIp}"); 

            NetworkStream stream = client.GetStream();
            byte[] b = new byte[100];
            int userRequests = 0;

            try
            {
                int count = stream.Read(b, 0, b.Length);
                string pass = Encoding.UTF8.GetString(b, 0, count);

                if (pass != myPass)
                {
                    _parent.AddToLog($"AUTH FAIL: {clientIp}");
                    Send(client, "FAIL");
                    return;
                }

                Send(client, "OK");
                _parent.AddToLog($"AUTH SUCCESS: {clientIp}");

                while (userRequests < maxRequests)
                {
                    count = stream.Read(b, 0, b.Length);
                    if (count == 0) break;
                    string req = Encoding.UTF8.GetString(b, 0, count).ToUpper();

                    _parent.AddToLog($"REQUEST from {clientIp}: {req}");

                    if (req == "USD EUR") Send(client, "0.92 EUR");
                    else if (req == "EUR USD") Send(client, "1.08 USD");
                    else Send(client, "Rate not found");

                    userRequests++;
                }

                if (userRequests >= maxRequests)
                {
                    _parent.AddToLog($"LIMIT REACHED: {clientIp}");
                    Send(client, "LIMIT");
                }
            }
            catch { }
            finally
            {
                clientsCount--;
                _parent.AddToLog($"DISCONNECT: {clientIp}");
                client.Close();
            }
        }

        private void Send(TcpClient c, string m)
        {
            byte[] d = Encoding.UTF8.GetBytes(m);
            c.GetStream().Write(d, 0, d.Length);
        }

        public void Stop() { running = false; listener?.Stop(); }
    }
}