using Cat_API_Project.DTO.Account.Login;
using FluentValidation;

namespace Cat_API_Project.Validators
{
    public class LoginAccountDTOValidator : AbstractValidator<LoginAccountDTO>
    {
        public LoginAccountDTOValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(p => p.Password)
                .NotEmpty();
        }
    }
}
