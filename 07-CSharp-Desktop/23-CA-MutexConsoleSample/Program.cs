using System;
using System.Threading;

namespace MutexSample
{
    internal class Program
    {
        static Mutex mutex = new Mutex();

        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(4, 4);
            ThreadPool.SetMaxThreads(20, 20);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadProc);
            }

            Console.ReadLine();
        }

        static void ThreadProc(object param)
        {
            mutex.WaitOne();
            try
            {
                Console.Write("Hello ");
                Console.Write($"{Thread.CurrentThread.ManagedThreadId}: ");
                Thread.Sleep(50);
                Console.Write("My name is ");
                Thread.Sleep(50);
                Console.Write("Someone ");
                Thread.Sleep(50);
                Console.WriteLine("How are you?");
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
