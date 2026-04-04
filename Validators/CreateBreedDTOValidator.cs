using Cat_API_Project.DTO;
using FluentValidation;

namespace Cat_API_Project.Validators
{
    public class CreateBreedDTOValidator : AbstractValidator<CreateBreedDTO>
    {
        public CreateBreedDTOValidator()
        {
            RuleFor(x => x.BreedName)
                .NotEmpty().WithMessage("Breed name is required")
                .MinimumLength(2).WithMessage("Breed name must be at least 8 characters long")
                .MaximumLength(100).WithMessage("Breed name can not exceed 25 characters");

            RuleFor(x => x.Origin)
                .NotEmpty().WithMessage("Country of origin is required")
                .MinimumLength(2).WithMessage("Origin must be at least 2 characters long")
                .MaximumLength(100).WithMessage("Origin can not exceed 100 characters");

            RuleFor(x => x.LifeSpan)
                .NotEmpty().WithMessage("LifeSpan is required")
                .MinimumLength(50).WithMessage("LifeSpan can not exceed 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(5).WithMessage("Description must be at least 5 characters long")
                .MaximumLength(200).WithMessage("Description can not exceed 200 characters");

        }
    }
}
