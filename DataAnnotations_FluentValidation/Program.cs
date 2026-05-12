using DataAnnotations_FluentValidation.Models;
using DataAnnotations_FluentValidation.Models.DataAnnotations;
using DataAnnotations_FluentValidation.Models.DataAnnotations.Entities;
using DataAnnotations_FluentValidation.Models.FluentValidation;
using DataAnnotations_FluentValidation.Models.FluentValidation.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAnnotations_FluentValidation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;//Uncode-lerin normal gorunmesi ucun

            try
            {
                #region DataAnnotations
                Console.WriteLine("DataAnnotations:");
                var cls = new Product
                {
                    Name = "A",
                    Description = "D",
                    Price = 1001,
                    Size = "SSSS",
                    Category = "Electronics",
                    Image = "ssssssss"
                };

                var context = new ValidationContext(cls);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(cls, context, results, validateAllProperties: true);
                if (!isValid)
                {
                    foreach (var error in results)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                #endregion

                Console.WriteLine("****************************************************");

                #region FluentValidation
                Console.WriteLine("FluentValidation:");
                Customer cust = new()
                {
                    FirstName = "D",
                    LastName = "",
                    Email = "kk.ssgmail.com",
                    Address = "hhhhhh"

                };

                var validator = new CustomerValidator();
                var res = validator.Validate(cust);
                if (!res.IsValid)
                {
                    foreach (var error in res.Errors)
                    {
                        Console.WriteLine(new StringBuilder().Append(error.PropertyName).Append((char)58).Append((char)32).Append(error.ErrorMessage));
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
