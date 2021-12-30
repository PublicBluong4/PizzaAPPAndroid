using System;
using System.Collections.Generic;
using System.Text;

namespace Bluong4_Project2V4.Calc
{
    public static class CalcPrice
    {
        public static decimal totalBeforTax(decimal price, int amount)
        {
            return price * (decimal)amount;
        }
        public static decimal totalAfterTax(decimal price, int amount, decimal tax)
        {
            return price * (decimal)amount * tax;
        }
    }
}
