using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class QuoteClient
{
    static void Main()
    {
        try
        {
          
            TcpClient client = new TcpClient("127.0.0.1", 12345);
            NetworkStream stream = client.GetStream();
            Console.WriteLine("Connected to Quote Server!");


           
            string auth = "Shyryn:12345";
            byte[] data = Encoding.UTF8.GetBytes(auth);
            stream.Write(data, 0, data.Length);

           
            byte[] authBuffer = new byte[1024];
            stream.Read(authBuffer, 0, authBuffer.Length);
            Console.WriteLine("Auth successful!");


            for (int i = 0; i < 5; i++)
            {
                
                byte[] request = Encoding.UTF8.GetBytes("GET_QUOTE");
                stream.Write(request, 0, request.Length);

            
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string quote = Encoding.UTF8.GetString(buffer, 0, bytes);

                Console.WriteLine($"\nQuote #{i + 1}: {quote}");
                Console.WriteLine("Press Enter for next quote");
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("Limit reached! Press any key to exit.");
        Console.ReadKey();
    }
}