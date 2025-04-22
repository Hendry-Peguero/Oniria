using FluentValidation;

namespace Oniria.ViewModels.Auth
{
    public class RestorePasswordViewModel
    {
        public string Email { get; set; }
    }

    public class RestorePasswordViewModelValidator : AbstractValidator<RestorePasswordViewModel>
    {
        public RestorePasswordViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Set a valid format for mail");
        }
    }
}
