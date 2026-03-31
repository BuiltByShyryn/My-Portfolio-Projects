using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Boss is starting the worker...");

            ProcessStartInfo startInfo = new ProcessStartInfo();


            
            string currentFolder = AppDomain.CurrentDomain.BaseDirectory;

           
            string childPath = Path.Combine(currentFolder, "ChildCalculator.exe");

          
            startInfo.FileName = childPath;
            startInfo.Arguments = "7 3 +";

            
            startInfo.UseShellExecute = true;

            try
            {
                Process.Start(startInfo);
                Console.WriteLine(" CHILD STARTED SUCCESSFULLY!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ERROR: " + ex.Message);
                Console.WriteLine("Make sure you 'Built' the Child project first!");
            }

            Console.WriteLine("Boss is done. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
