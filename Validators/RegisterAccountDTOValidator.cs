using Cat_API_Project.DTO;
using FluentValidation;

namespace Cat_API_Project.Validators
{
    public class RegisterAccountDTOValidator : AbstractValidator<RegisterAccountDTO>
    {
        public RegisterAccountDTOValidator()
        {
            RuleFor(f => f.FirstName)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character."); //all characters that are not letters or numbers

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Date of birth must be in the past.");
        }
    }
}
