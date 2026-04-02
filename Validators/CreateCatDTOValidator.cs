using FluentValidation;
using Cat_API_Project.DTO;

namespace Cat_API_Project.Validators
{
    public class CreateCatDTOValidator : AbstractValidator<CreateCatDTO>
    {
        public CreateCatDTOValidator() 
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
                .MaximumLength(20).WithMessage("Name can not exceed 20 characters");

            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long")
                .MaximumLength(20).WithMessage("Description can not exceed 50 characters");

            RuleFor(b => b.BreedId)
                .NotEmpty().WithMessage("Breed id is required")
                .GreaterThan(0).WithMessage("Breed id can not be less than 1");
        }
        
    }
}
