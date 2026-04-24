using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
    internal class DiscountContext
    {
        private IDiscountStrategy strategy;

        public DiscountContext()
        {
            strategy = new NoDiscount(); 
        }

        public double Calculate(double price)
        {
            return strategy.ApplyDiscount(price);
        }

        public void ChangeStrategy(IDiscountStrategy strategy)
        {
            this.strategy = strategy;
        }
    }
}
