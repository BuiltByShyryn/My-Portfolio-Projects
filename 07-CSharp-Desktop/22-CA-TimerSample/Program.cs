using System;
using System.Threading;

namespace TimerSample
{
    internal class Program
    {
        static Timer timer1 = null;
        static AutoResetEvent evnStop = new AutoResetEvent(false);
        static int count = 0;

        static void Main(string[] args)
        {
            int thread_count = 10;
            ThreadPool.SetMinThreads(thread_count, thread_count);

            timer1 = new Timer(TimerProc, null, 500, 500);
            evnStop.WaitOne();
        }

        static void TimerProc(object state)
        {
            count++;
            Console.WriteLine($"Timer tick: {count}");

            if (count >= 10)
            {
                timer1.Dispose();
                evnStop.Set();
            }
        }
    }
}
