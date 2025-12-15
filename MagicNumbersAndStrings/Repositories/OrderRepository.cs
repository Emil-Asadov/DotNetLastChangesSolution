using MagicNumbersAndStrings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumbersAndStrings.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        public string ProccessOrder(Order ord)
        {
            if (ord.Amount > 1000)
                ord.Discount = ord.Amount * 0.1m;//0.1m- Magic numbers

            return ord.Status = "Processing";//"Processing"- Magic strings
        }
    }
}
