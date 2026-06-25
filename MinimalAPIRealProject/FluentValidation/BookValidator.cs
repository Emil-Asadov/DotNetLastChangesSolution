using FluentValidation;
using MinimalAPIRealProject.Models.Entity;
using System.Globalization;

namespace MinimalAPIRealProject.FluentValidation
{
    public class BookValidator : AbstractValidator<BookRequest>
    {
        public BookValidator()
        {
            RuleFor(x => x.Isbn).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz").Length(6).WithMessage("{PropertyName} sahəsi {MaxLength} simvol olmalıdır");
            RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz").Length(1, 50).WithMessage("{PropertyName} sahəsi minimum {MinLength} maksimum {MaxLength} simvol arasında olmalıdır");
            RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz").Length(5, 100).WithMessage("{PropertyName} sahəsi minimum {MinLength} maksimum {MaxLength} simvol arasında olmalıdır");
            RuleFor(x => x.PageCount).GreaterThan(20).WithMessage("{PropertyName} minimum {ComparisonValue} çox olmalıdır");

            RuleFor(x => x.PublishDate).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz").Must((model, date, context) =>
            {
                //model- BookRequest
                //date- input value
                //context- ValidationContext<BookRequest>
                bool res = DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
                if (!res)
                {
                    context.MessageFormatter.AppendArgument("InputDate", date);
                    return false;
                }

                return true;
            }).WithMessage("{InputDate} düzgün tarix deyil. {PropertyName} sahəsi dd.MM.yyyy formatında olmalıdır");
        }
    }
}
