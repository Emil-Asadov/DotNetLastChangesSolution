using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumbersAndStrings.Entities
{
    public class Order
    {
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string? StatusTxt { get; set; }
        public enum Status
        {
            [Display(Name = "Pending Order")]
            Pending = 1,
            [Display(Name = "In Process")]
            Processing = 2,
            [Display(Name = "Completed")]
            Completed = 3,
            [Display(Name = "Canceled")]
            Cancelled = 4
        }
        public Status OrderStatus { get; set; }
    }
}
