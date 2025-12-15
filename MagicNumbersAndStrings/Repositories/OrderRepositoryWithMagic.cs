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
        const decimal orderValueThreshold = 1000;
        const decimal orderDiscount = 0.1m;
        public string ProccessOrder(Order ord)
        {
            if (ord.Amount > 1000)
                ord.Discount = ord.Amount * 0.1m;//0.1m- Magic numbers

            ord.OrderStatus = Order.Status.Processing;//"Processing"- Magic strings
            var res = $"Enum(string): {ord.OrderStatus}{(char)13}{(char)10}Enum display name: {GetEnumDisplayName(ord.OrderStatus)}";

            return res;
        }

        public string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<DisplayAttribute>();

            return attr?.Name ?? value.ToString();
        }
    }
}
