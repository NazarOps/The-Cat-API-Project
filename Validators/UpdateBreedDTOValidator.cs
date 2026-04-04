using Cat_API_Project.DTO;
using FluentValidation;

namespace Cat_API_Project.Validators
{
    public class UpdateBreedDTOValidator : AbstractValidator<UpdateBreedDTO>
    {
        public UpdateBreedDTOValidator()
        {
            RuleFor(x => x.BreedName)
                .NotEmpty().WithMessage("Breed name is required")
                .MinimumLength(2).WithMessage("Breed name must be at least 2 characters long")
                .MinimumLength(50).WithMessage("Breed name can not exceed 50 characters");

            RuleFor(x => x.Origin)
                .NotEmpty().WithMessage("Origin is required")
                .MinimumLength(2).WithMessage("Country of origin must be at least 2 characters long")
                .MinimumLength(50).WithMessage("Country of origin can not exceed 50 characters");

            RuleFor(x => x.LifeSpan)
                .NotEmpty().WithMessage("LifeSpan is required")
                .MaximumLength(50).WithMessage("LifeSpan can not exceed 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(5).WithMessage("Descriptionmust be at least 5 characters long")
                .MinimumLength(200).WithMessage("Description can not exceed 50 characters");
        }
    }
}
