using FluentValidation;
using Cat_API_Project.DTO;


namespace Cat_API_Project.Validators
{
    public class CreateBreedFactDTOValidator : AbstractValidator<CreateBreedFactDTO>
    {
        public CreateBreedFactDTOValidator()
        {
            RuleFor(f => f.Fact)
                .NotEmpty().WithMessage("A fact about the breed is required")
                .MinimumLength(10).WithMessage("Fact must be at least 10 characters long")
                .MaximumLength(300).WithMessage("Fact can not exceed 300 characters");

            RuleFor(t => t.Title)
                .MaximumLength(20).WithMessage("Title can not exceed 20 characters");

            RuleFor(i => i.BreedId)
                .GreaterThan(0).WithMessage("BreedId must be greater than 0");
        }
    }
}
