using FluentValidation;
using Cat_API_Project.DTO;

namespace Cat_API_Project.Validators
{
    public class UpdateCatDTOValidator : AbstractValidator<UpdateCatDTO>
    {
        public UpdateCatDTOValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must be at least two characters long")
                .MaximumLength(30).WithMessage("Name can not exceed 30 characters");

            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long")
                .MaximumLength(30).WithMessage("Description can not exceed 30 characters");

            RuleFor(b => b.BreedId)
                .NotEmpty().WithMessage("Breed id is required")
                .GreaterThan(0).WithMessage("Breed id must be greater than 0");
        }
    }
}
