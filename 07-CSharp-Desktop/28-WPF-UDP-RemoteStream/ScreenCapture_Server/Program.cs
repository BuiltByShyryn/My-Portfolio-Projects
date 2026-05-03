using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ScreenCapture_Server
{
    class Program
    {
        static void Main()
        {
            UdpClient udpServer = new UdpClient(12345);
            Console.WriteLine(" Full-Frame Server Started");

            while (true)
            {
                IPEndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] req = udpServer.Receive(ref clientEP);
                if (Encoding.UTF8.GetString(req) != "GET_SHOT") continue;

                
                Rectangle bounds = Screen.PrimaryScreen.Bounds;
                using (Bitmap bmp = new Bitmap(bounds.Width, bounds.Height))
                using (Graphics g = Graphics.FromImage(bmp))
                using (MemoryStream ms = new MemoryStream())
                {
                    g.CopyFromScreen(0, 0, 0, 0, bounds.Size);

                   
                    var encoder = GetEncoder(ImageFormat.Jpeg);
                    var parms = new EncoderParameters(1);
                    
                    parms.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);
                    bmp.Save(ms, encoder, parms);

                    byte[] data = ms.ToArray();
                    int chunkSize = 60000; 

                 
                    for (int i = 0; i < data.Length; i += chunkSize)
                    {
                        int size = Math.Min(chunkSize, data.Length - i);
                        byte[] chunk = new byte[size];
                        Array.Copy(data, i, chunk, 0, size);
                        udpServer.Send(chunk, chunk.Length, clientEP);
                    }

                   
                    byte[] endSignal = Encoding.UTF8.GetBytes("FRAME_DONE");
                    udpServer.Send(endSignal, endSignal.Length, clientEP);

                    Console.WriteLine($"Sent Full Frame: {data.Length} bytes");
                }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            foreach (var codec in ImageCodecInfo.GetImageEncoders())
                if (codec.FormatID == format.Guid) return codec;
            return null;
        }
    }
}