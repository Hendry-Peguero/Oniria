using FluentValidation;

namespace Oniria.ViewModels.Patient
{
    public class RegisterDreamViewModel
    {
        public string Prompt { get; set; }
    }

    public class RegisterDreamViewModelValidator : AbstractValidator<RegisterDreamViewModel>
    {
        public RegisterDreamViewModelValidator()
        {
            RuleFor(x => x.Prompt)
                .NotEmpty().WithMessage("Prompt is required")
                .MaximumLength(350).WithMessage("Prompt must not exceed 350 characters");
        }
    } 
}
