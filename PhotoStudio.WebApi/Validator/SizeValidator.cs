using FluentValidation;
using PhotoStudio.Domain.Entities;
using PhotoStudio.ServicesDTO;

namespace PhotoStudio.WebApi.Validator
{
    public class SizeValidator : AbstractValidator<SizeDTO>
    {
        public SizeValidator()
        {
            RuleFor(s => s.Size).NotNull().NotEmpty().WithMessage("The size field is required")
           .MaximumLength(100).WithMessage("The size name field has a maximun lenght of 100 characteres");

        }
    }
}
