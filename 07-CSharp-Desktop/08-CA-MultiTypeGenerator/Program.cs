using System;
using System.Threading; 

namespace Homework_Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Starting Task 1: The Multi-Generator ");

            
            Thread workerInt = new Thread(GenIn);
            Thread workerDouble = new Thread(GenDoub);
            Thread workerChars = new Thread(GenSymb);

            
            workerInt.Start();
            workerDouble.Start();
            workerChars.Start();

            
            workerInt.Join();
            workerDouble.Join();
            workerChars.Join();

            Console.WriteLine("\n All Generations Finished! ");
            Console.ReadLine();
        }

        

        static void GenIn()
        {
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                int val = rnd.Next(1, 100);
                Console.WriteLine($"[INT Thread] generated: {val}");
                Thread.Sleep(500); 
            }
        }

        static void GenDoub()
        {
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                double val = rnd.NextDouble() * 100;
                Console.WriteLine($"[DOUBLE Thread] generated: {val:F2}");
                Thread.Sleep(500);
            }
        }

        static void GenSymb()
        {
            char[] symbols = { '!', '@', '#', '$', '%', '&', '*' };
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                char val = symbols[rnd.Next(symbols.Length)];
                Console.WriteLine($"[SYMBOL Thread] generated: {val}");
                Thread.Sleep(500);
            }
        }
    }
}