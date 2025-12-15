using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumbersAndStrings.Entities
{
    public class Order
    {
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string Status { get; set; }
    }
}
