using System.Text;

namespace MinimalAPIRoutingRequest
{
    public sealed class Example
    {
        public static string SomeMethodWithoutParameter() => "This is coming a method";
        public static string SomeMethodWithoutParameterNew() => "This is coming a method new";
        public static string SomeMethodWithParameter(string p)
        {
            var str = new StringBuilder("This is coming from").Append((char)32).Append(p);
            return str.ToString();
        }
    }
}
