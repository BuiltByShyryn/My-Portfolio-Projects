using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
    internal class BigPurchaseDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double price)
        {
            if (price > 10000)
                return price * 0.9; 
            return price;
        }
    }
}
