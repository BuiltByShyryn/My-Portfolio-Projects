using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace InterlockedSample
{
    internal class Program
    {

        static int Summa = 0;
        static Semaphore work_sem = null;
        static Stopwatch work_time = null;
        static long TotalTime = 0;

        static void Main(string[] args)
        {

            int thread_count = 10;
            int count = 10000;

            
            ThreadPool.SetMinThreads(thread_count, thread_count);
            work_sem = new Semaphore(thread_count, thread_count);

            for (int i = 0; i < thread_count; i++)
            {
                work_sem.WaitOne();
                ThreadPool.QueueUserWorkItem(ThreadProc, count);
            }
            //Thread.Sleep(2000);

            for (int i = 0; i<thread_count; i++)
            {
                work_sem.WaitOne();
            }
            Console.WriteLine($"Summa: {Summa}");
            Console.ReadLine();
        }

        static void ThreadProc(object param)
        {

            int count = (int)param;
            int id = Thread.CurrentThread.ManagedThreadId;
            
            for (int i = 0; i < count; i++)
            {
                Summa++;
            }
            Console.WriteLine($"id:{id} started to work");
            
            Stopwatch work_time = new Stopwatch();
            work_time.Reset();
            

            for (int i = 0;i < count; i++)
            {
                Interlocked.Add(ref Summa, 1);
            }
            work_time.Stop();
            Interlocked.Add(ref TotalTime, work_time.ElapsedTicks);
            Console.WriteLine($"The time:{TotalTime /10.0} microsec");
            work_sem.Release();
            
        }
    }
}