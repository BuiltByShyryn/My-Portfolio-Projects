using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length == 3)
            {
                string firstNum = args[0];  
                string secondNum = args[1]; 
                string operation = args[2]; 

               
                double n1 = double.Parse(firstNum);
                double n2 = double.Parse(secondNum);

                
                if (operation == "+")
                {
                    Console.WriteLine($"{n1} + {n2} = {n1 + n2}");
                }
            }
            Console.ReadLine(); 

        }
    }
}
