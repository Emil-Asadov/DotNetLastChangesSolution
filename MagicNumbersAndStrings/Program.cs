using MagicNumbersAndStrings.Entities;
using MagicNumbersAndStrings.Repositories;

namespace MagicNumbersAndStrings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IOrderRepository cls = new OrderRepository();
            var res = cls.ProccessOrder(new Order
            {
                Amount = 1500
            });
            Console.WriteLine($"Result: {res}");
            Console.ReadKey();
        }
    }
}
