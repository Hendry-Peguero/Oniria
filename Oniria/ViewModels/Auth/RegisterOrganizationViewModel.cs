using FluentValidation;
using Oniria.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Oniria.ViewModels.Auth
{
    public class RegisterOrganizationViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string EmployeeDni { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeBornDate { get; set; } = DateTime.Now.AddYears(-25);

        public string EmployeePhoneNumber { get; set; }
        public string EmployeeAddress { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string OrganizationPhoneNumber { get; set; }

        public string MembershipId { get; set; }


        // Lists
        public List<MembershipEntity>? Memberships { get; set; }
    }

    public class RegisterOrganizationViewModelValidator : AbstractValidator<RegisterOrganizationViewModel>
    {
        public RegisterOrganizationViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(4).WithMessage("Username must be at least 4 characters long");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
                .WithMessage("Password must contain uppercase, lowercase and a number");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.Password).WithMessage("Passwords are not the same");

            RuleFor(x => x.EmployeeDni)
                .NotEmpty().WithMessage("DNI is required")
                .MaximumLength(11)
                .WithMessage("DNI must not exceed 11 characters")
                .MinimumLength(11)
                .WithMessage("Can not be less than 11 characters");

            RuleFor(x => x.EmployeeName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

            RuleFor(x => x.EmployeeLastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

            RuleFor(x => x.EmployeePhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\((809|829|849)\)\s?\d{3}-\d{4}$")
                .WithMessage("Phone number must be like (849) 586-9875");

            RuleFor(x => x.EmployeeAddress)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

            RuleFor(x => x.OrganizationName)
                .NotEmpty().WithMessage("Organization name is required")
                .MaximumLength(150).WithMessage("Organization name must not exceed 150 characters");

            RuleFor(x => x.OrganizationAddress)
                .NotEmpty().WithMessage("Organization address is required")
                .MaximumLength(250).WithMessage("Organization address must not exceed 250 characters");

            RuleFor(x => x.OrganizationPhoneNumber)
                .NotEmpty().WithMessage("Organization phone number is required")
                .Matches(@"^\((809|829|849)\)\s?\d{3}-\d{4}$")
                .WithMessage("Phone number must be like (849) 586-9875");

            RuleFor(x => x.MembershipId)
                .NotEmpty().WithMessage("Select a valid membership");
        }
    }
}
