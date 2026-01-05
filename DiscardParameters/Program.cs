using DiscardParameters.MethodsModel;

namespace DiscardParameters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cls = new Methods();
            var res = cls.DiscardValue(125);
            Console.WriteLine(res);

            (_, string lastNameRes) = cls.DiscardTupleMembers();
            Console.WriteLine(lastNameRes);

            res = cls.DiscardOutParameter("126d");
            Console.WriteLine(res);

            _ = cls.DiscardReturnValue("125");

            Console.ReadKey();
        }
    }
}
