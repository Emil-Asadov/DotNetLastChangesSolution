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
            Console.WriteLine($"Result:{(char)13}{(char)10}{res}");

            Console.WriteLine();
            Console.WriteLine();

            IOrderRepository clsMagic = new OrderRepositoryWithMagic();
            res = clsMagic.ProccessOrder(new Order
            {
                Amount = 1500
            });
            Console.WriteLine($"Result(Magic):{(char)13}{(char)10}{res}");

            Console.ReadKey();
        }
    }
}
