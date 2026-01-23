using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnnotations_FluentValidation.Models.DataAnnotations.Entities
{
    public class Product
    {
        [Length(2, 30, ErrorMessage = "{0} sahəsi {1} ilə {2} simvol arasında olmalıdır")]
        public string Name { get; set; }

        [Length(2, 255, ErrorMessage = "{0} sahəsi {1} ilə {2} simvol arasında olmalıdır")]
        public string Description { get; set; }

        [Range(1, 1000, MinimumIsExclusive = true, MaximumIsExclusive = false, ErrorMessage = "{0} sahəsi {1} ilə {2} aralığında olmalıdır")]
        public decimal Price { get; set; }

        [AllowedValues("S", "M", "L", "XL", "XXL", ErrorMessage = "{0} sahəsi üçün standart ölçülərdən başqa ölçülər verilə bilməz")]
        public string Size { get; set; }

        [DeniedValues("Electronics", "Computers", ErrorMessage = "{0} sahəsi Electronics, Computers dəyərlərini qəbul etmir")]
        public string Category { get; set; }

        [Base64String]
        public string Image { get; set; }
    }
}
