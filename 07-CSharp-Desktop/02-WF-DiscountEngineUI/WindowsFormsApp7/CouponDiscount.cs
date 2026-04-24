using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
    internal class CouponDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double price)
        {
            return price - 1000; 
        }
    }
}
