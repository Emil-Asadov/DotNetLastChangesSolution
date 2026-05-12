using DataAnnotations_FluentValidation.Models.FluentValidation.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnnotations_FluentValidation.Models.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz").Length(2, 30).WithMessage("{PropertyName} sahəsi minimum {MinLength} maksimum {MaxLength} simvol arasında olmalıdır");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz");

            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz").EmailAddress().WithMessage("{PropertyName} sahəsindəki məlumat Email formatında olmalıdır");

            RuleFor(x => x.Address).NotEmpty().WithMessage("{PropertyName} daxil edilməlidir").MinimumLength(2).WithMessage("{PropertyName} minimum 2 simvol olmalıdır").MaximumLength(5).WithMessage("{PropertyName} maximum 5 simvol olmalıdır");
        }
    }
}
