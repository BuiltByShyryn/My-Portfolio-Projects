using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Blackjack_Server
{
    class Program
    {
        static Random rng = new Random();

        public class Card
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        static void Main()
        {
            UdpClient server = new UdpClient(12346);
            Console.WriteLine(" Dealer is ready on port 12346. Waiting for a player...");

            while (true)
            {
                IPEndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = server.Receive(ref clientEP);
                string request = Encoding.UTF8.GetString(data);

            
                if (request == "DRAW")
                {
                    var card = GetRandomCard();
                    string responseString = $"{card.Name}|{card.Value}";
                    byte[] response = Encoding.UTF8.GetBytes(responseString);
                    server.Send(response, response.Length, clientEP);
                    Console.WriteLine($"Dealt: {card.Name}");
                }

                
                else if (request.StartsWith("STAY|"))
                {
                    int playerFinalScore = int.Parse(request.Split('|')[1]);
                    int dealerScore = 0;
                    string dealerCards = "";

                  
                    while (dealerScore < 17)
                    {
                        var c = GetRandomCard();
                        dealerScore += c.Value;
                        dealerCards += $"[{c.Name}] ";
                    }

                    string result;
                    if (dealerScore > 21)
                        result = $"Dealer BUSTS with {dealerScore}! YOU WIN! ";
                    else if (dealerScore > playerFinalScore)
                        result = $"Dealer has {dealerScore}. YOU LOSE! ";
                    else if (dealerScore < playerFinalScore)
                        result = $"Dealer has {dealerScore}. YOU WIN! ";
                    else
                        result = $"Both have {dealerScore}. IT'S A TIE! ";

                    Console.WriteLine($"Showdown: Player({playerFinalScore}) vs Dealer({dealerScore})");

                    string finalMessage = $"Dealer's Cards: {dealerCards}\n{result}";
                    byte[] response = Encoding.UTF8.GetBytes(finalMessage);
                    server.Send(response, response.Length, clientEP);
                }
            }
        }

        static Card GetRandomCard()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

            int index = rng.Next(ranks.Length);
            return new Card
            {
                Name = $"{ranks[index]} of {suits[rng.Next(suits.Length)]}",
                Value = values[index]
            };
        }
    }
}