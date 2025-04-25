using FluentValidation;
using Oniria.Core.Domain.Enums;

namespace Oniria.ViewModels.Employee
{
    public class EmployeeProfileViewModel
    {
        public string Id { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public string? OrganizationId { get; set; }
        public StatusEntity Status { get; set; }
    }

    public class EmployeeProfileViewModelValidator : AbstractValidator<EmployeeProfileViewModel>
    {
        public EmployeeProfileViewModelValidator()
        {
            RuleFor(x => x.Dni)
                .NotEmpty().WithMessage("DNI is required")
                .MaximumLength(11)
                .WithMessage("DNI must not exceed 11 characters")
                .MinimumLength(11)
                .WithMessage("Can not be less than 11 characters");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\((809|829|849)\)\s?\d{3}-\d{4}$")
                .WithMessage("Phone number must be like (849) 586-9875");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters");
        }
    }
}
