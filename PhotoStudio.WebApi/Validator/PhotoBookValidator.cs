using FluentValidation;
using PhotoStudio.ServicesDTO;

namespace PhotoStudio.WebApi.Validator
{
    public class PhotoBookValidator : AbstractValidator<PhotoBookDTO>
    {
        public PhotoBookValidator()
        {

            RuleFor(pb => pb.MaterialId).GreaterThan(0).WithMessage("MaterialId is required");
            RuleFor(pb => pb.SizeId).Must(SizeValid).WithMessage("SizeId is required");
            RuleFor(pb => pb.PortadaPrice).NotNull().WithMessage("Portada price is required"); ;
            RuleFor(pb => pb.PriceByPage).NotNull().WithMessage("PriceByPage is required");
        }

        private bool SizeValid(int size)
        {
            return size > 0;
        }
    }
}
