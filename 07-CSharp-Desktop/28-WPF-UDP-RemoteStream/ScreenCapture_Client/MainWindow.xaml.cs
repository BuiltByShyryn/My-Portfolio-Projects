using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ScreenCapture_Client
{
    public partial class MainWindow : Window
    {
        private Socket udpClient;
        private IPEndPoint serverEP;
        private bool isCapturing = false;

        public MainWindow()
        {
            InitializeComponent();

            udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

          
            udpClient.Bind(new IPEndPoint(IPAddress.Any, 0));

            
            udpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, 1024 * 1024 * 2);
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (isCapturing)
            {
                isCapturing = false;
                btnStart.Content = "Start Capture";
                return;
            }

            isCapturing = true;
            btnStart.Content = "Stop Capture";

            int interval = int.Parse(txtInterval.Text);
            serverEP = new IPEndPoint(IPAddress.Parse(txtIp.Text), 12345);

            await Task.Run(async () =>
            {
                while (isCapturing)
                {
                    try
                    {
                        
                        byte[] request = Encoding.UTF8.GetBytes("GET_SHOT");
                        udpClient.SendTo(request, serverEP);
                        Console.WriteLine("Sent request for Full Frame");

                        
                        using (MemoryStream fullImageStream = new MemoryStream())
                        {
                            int chunkCount = 0;
                            while (true)
                            {
                                byte[] buffer = new byte[65507];
                                EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                                int receivedSize = udpClient.ReceiveFrom(buffer, ref remoteEP);

                                string signal = Encoding.UTF8.GetString(buffer, 0, receivedSize);

                                if (signal == "FRAME_DONE")
                                {
                                    Console.WriteLine($" Frame Complete! Total chunks received: {chunkCount}");
                                    break;
                                }

                                
                                fullImageStream.Write(buffer, 0, receivedSize);
                                chunkCount++;
                            }

                            
                            byte[] finalData = fullImageStream.ToArray();

                            Console.WriteLine($" Showing Image. Total Size: {finalData.Length / 1024} KB");

                            Dispatcher.Invoke(() =>
                            {
                                ShowImage(finalData, finalData.Length);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error during capture: " + ex.Message);
                    }

                    await Task.Delay(interval);
                }
            });
        }

        private void ShowImage(byte[] data, int size)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(data, 0, size))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    imgDisplay.Source = image;
                }
            }
            catch { 
            //smthg 
            }
        }
    }
}