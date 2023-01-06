using FluentValidation;
using PhotoStudio.ServicesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Validator
{
    public class MaterialValidator : AbstractValidator<MaterialDTO>
    {
        public MaterialValidator()
        {
            RuleFor(m => m.MaterialName).NotNull().NotEmpty().WithMessage("The field material name is required");
            RuleFor(m => m.MaterialName).MaximumLength(200).WithMessage("The material name field has a maximun lenght of 200 characteres");
        }
    }
}
