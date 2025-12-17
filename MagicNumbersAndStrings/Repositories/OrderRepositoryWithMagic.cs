using MagicNumbersAndStrings.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumbersAndStrings.Repositories
{
    internal class OrderRepositoryWithMagic : IOrderRepository
    {
        public string ProccessOrder(Order ord)
        {
            if (ord.Amount > 1000)
                ord.Discount = ord.Amount * 0.1m;//0.1m- Magic numbers

            ord.StatusTxt = "Processing";//"Processing"- Magic strings

            return ord.StatusTxt;
        }
    }
}
