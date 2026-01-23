using PropertyPattern.Entities;

namespace PropertyPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var clsTraditional = new User
            {
                Role = "Admin",
                Age = 25
            };

            #region Traditional Approach            
            if (clsTraditional is not null && clsTraditional.Role.Equals("Admin") && clsTraditional.Age > 21)
                Console.WriteLine("Grant admin access(Traditional Approach)");
            #endregion

            clsTraditional = null;

            #region Property Pattern
            if (clsTraditional is { Role: "Admin", Age: > 21 })
                Console.WriteLine("Grant admin access(Property Pattern)");
            #endregion
        }
    }
}
