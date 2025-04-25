using FluentValidation;
using Oniria.Core.Domain.Enums;

namespace Oniria.ViewModels.Organization
{
    public class OrganizationProfileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeOwnerId { get; set; }
        public StatusEntity Status { get; set; }
    }

    public class OrganizationProfileViewModelValidator : AbstractValidator<OrganizationProfileViewModel>
    {
        public OrganizationProfileViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

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
