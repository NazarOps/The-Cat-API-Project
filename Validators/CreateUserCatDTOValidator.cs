using Cat_API_Project.DTO;
using FluentValidation;

namespace Cat_API_Project.Validators
{
    public class CreateUserCatDTOValidator : AbstractValidator<CreateUserCatDTO>
    {
        public CreateUserCatDTOValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(d => d.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(b => b.BreedId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(i => i.ImageUrl)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));
        }
    }
}
