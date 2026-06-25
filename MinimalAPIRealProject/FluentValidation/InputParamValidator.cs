using FluentValidation;
using MinimalAPIRealProject.Models.Entity;

namespace MinimalAPIRealProject.FluentValidation
{
    public class InputParamValidator : AbstractValidator<InputParam>
    {
        public InputParamValidator()
        {
            RuleFor(x => x.Isbn).NotEmpty().WithMessage("{PropertyName} sahəsi boş ola bilməz").Length(6).WithMessage("{PropertyName} sahəsi {MaxLength} simvol olmalıdır").Matches(@"^[0-9-]+$").WithMessage("{PropertyName} yalnız rəqəmlər və '-' simvolundan ibarət ola bilər");
        }
    }
}
