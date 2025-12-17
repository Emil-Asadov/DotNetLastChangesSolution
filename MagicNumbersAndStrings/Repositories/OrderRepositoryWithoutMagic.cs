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
    public class OrderRepositoryWithoutMagic : IOrderRepository
    {
        const decimal orderValueThreshold = 1000;
        const decimal orderDiscount = 0.1m;
        public string ProccessOrder(Order ord)
        {
            if (ord.Amount > orderValueThreshold)
                ord.Discount = ord.Amount * orderDiscount;

            ord.OrderStatus = Order.Status.Processing;
            var res = $"Enum(string): {ord.OrderStatus}{(char)13}{(char)10}Enum display name: {GetEnumDisplayName(ord.OrderStatus)}";

            return res;
        }

        public string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DisplayAttribute>();

            return attr?.Name ?? value.ToString();
        }
    }
}
