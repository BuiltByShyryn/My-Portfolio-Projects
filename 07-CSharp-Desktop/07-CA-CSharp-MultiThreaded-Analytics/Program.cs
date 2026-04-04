using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading; 

namespace Homework_Task2
{
    internal class Program
    {
        
        static List<int> numbers = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine(" Starting Task 2 ");

           
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                numbers.Add(rnd.Next(1, 100000));
            }
            Console.WriteLine("10,000 numbers generated.");

            
            Thread threadMax = new Thread(FindMax);
            Thread threadMin = new Thread(FindMin);
            Thread threadAvg = new Thread(FindAvg);

           
            threadMax.Start();
            threadMin.Start();
            threadAvg.Start();

            
            threadMax.Join();
            threadMin.Join();
            threadAvg.Join();

            Console.WriteLine("\n--- All work is done! ---");
            Console.ReadLine(); 
        }

        

        static void FindMax()
        {
            int max = numbers[0];
            foreach (int n in numbers)
            {
                if (n > max) max = n;
            }
            Console.WriteLine($"[Thread MAX] finished. Result: {max}");
        }

        static void FindMin()
        {
            int min = numbers[0];
            foreach (int n in numbers)
            {
                if (n < min) min = n;
            }
            Console.WriteLine($"[Thread MIN] finished. Result: {min}");
        }

        static void FindAvg()
        {
            double sum = 0;
            foreach (int n in numbers)
            {
                sum += n;
            }
            double avg = sum / numbers.Count;
            Console.WriteLine($"[Thread AVG] finished. Result: {avg:F2}");
        }
    }
}