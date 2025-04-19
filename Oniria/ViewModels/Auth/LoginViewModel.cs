using FluentValidation;

namespace Oniria.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Identifier)
                .NotEmpty()
                .WithMessage("Identifier is required");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
