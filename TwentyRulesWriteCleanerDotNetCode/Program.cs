namespace TwentyRulesWriteCleanerDotNetCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var inp = "ORD-12345";
            var id = inp.Substring(4);
            var parsed = int.Parse(id);

            ReadOnlySpan<char> input = inp;
            parsed = int.Parse(input[4..]);
        }
    }
}
