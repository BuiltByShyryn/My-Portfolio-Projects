using System;
using System.Linq;
using System.Threading;

namespace MutexSample
{
    internal class Program
    {
        
        static Mutex mutex = new Mutex();

        
        static int[] data = new int[10];

        static void Main(string[] args)
        {
            Console.WriteLine(" Mutex Task 1 Start ");

          
            Thread thread1 = new Thread(ModifyArray);
            Thread thread2 = new Thread(FindMax);

            
            thread1.Start();
            thread2.Start();

           
            thread1.Join();
            thread2.Join();

            Console.WriteLine("\nAll threads finished. Press Enter to exit.");
            Console.ReadLine();
        }

        static void ModifyArray()
        {
            Console.WriteLine("[Thread 1] I want the key! Waiting...");

            mutex.WaitOne(); 

            Console.WriteLine("[Thread 1] I have the key! Modifying the array:");
            Random rnd = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = rnd.Next(1, 50); 
                Console.Write(data[i] + " ");
                Thread.Sleep(300); 
            }

            Console.WriteLine("\n[Thread 1] Done! Releasing the key.");
            mutex.ReleaseMutex(); 
        }

        static void FindMax()
        {
            Console.WriteLine("[Thread 2] I want the key too! Waiting...");

            mutex.WaitOne(); 

            Console.WriteLine("[Thread 2] Finally got the key! Finding the Max...");
            int max = data.Max();
            Console.WriteLine($"[Thread 2] The Maximum value found is: {max}");

            mutex.ReleaseMutex(); 
        }
    }
}