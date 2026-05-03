using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Blackjack_Client
{
    class Program
    {
        static void Main()
        {
            UdpClient client = new UdpClient();
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12346);

            Console.WriteLine("♣️ Welcome to UDP Blackjack! ♣");
            int totalScore = 0;

            while (true)
            {
                Console.WriteLine("\n[Enter] to Hit, 's' to Stay, or 'exit' to quit:");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit") break;

             
                if (input.ToLower() == "s")
                {
                    Console.WriteLine("Standing... Waiting for Dealer's hand...");
                    byte[] stayReq = Encoding.UTF8.GetBytes($"STAY|{totalScore}");
                    client.Send(stayReq, stayReq.Length, serverEP);

                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] finalRes = client.Receive(ref remoteEP);

                    Console.WriteLine("\n========================");
                    Console.WriteLine(Encoding.UTF8.GetString(finalRes));
                    Console.WriteLine("========================\n");
                    break; 
                }

              
                byte[] request = Encoding.UTF8.GetBytes("DRAW");
                client.Send(request, request.Length, serverEP);

                IPEndPoint dealerEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] response = client.Receive(ref dealerEP);
                string data = Encoding.UTF8.GetString(response);

                
                string[] parts = data.Split('|');
                string cardName = parts[0];
                int cardValue = int.Parse(parts[1]);

                totalScore += cardValue;
                Console.WriteLine($">> You pulled: {cardName}");
                Console.WriteLine($">> Your Total Score: {totalScore}");

                if (totalScore > 21)
                {
                    Console.WriteLine(" BUST! You went over 21. Dealer wins.");
                    break;
                }
            }
            Console.WriteLine("\n--- Game Over ---");
            Console.WriteLine("Press any key to close this window...");
            Console.ReadKey();
            Console.WriteLine("Thanks for playing! Press any key to exit.");
            Console.ReadKey();
        }
    }
}