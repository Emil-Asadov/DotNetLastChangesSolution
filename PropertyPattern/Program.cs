using PropertyPattern.Entities;

namespace PropertyPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var cls = new User
            {
                Role = "Admin",
                Age = 25
            };

            #region Traditional Approach            
            if (cls is not null && cls.Role.Equals("Admin") && cls.Age > 21)
                Console.WriteLine("Grant admin access(Traditional Approach)");
            #endregion

            cls = null;

            #region Property Pattern
            if (cls is { Role: "Admin", Age: > 21 })
                Console.WriteLine("Grant admin access(Property Pattern)");
            #endregion
        }
    }
}
