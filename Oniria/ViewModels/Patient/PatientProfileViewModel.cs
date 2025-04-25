using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oniria.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Oniria.ViewModels.Patient
{
    public class PatientProfileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BornDate { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string GenderId { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
        public StatusEntity Status { get; set; }

        // Lists
        public SelectList? Genders { get; set; }
    }

    public class PatientProfileViewModelValidator : AbstractValidator<PatientProfileViewModel>
    {
        public PatientProfileViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^\((809|829|849)\)\s?\d{3}-\d{4}$")
                .WithMessage("Phone number must be like (849) 586-9875");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

            RuleFor(x => x.GenderId)
                .NotEmpty().WithMessage("Gender is required");
        }
    }
}
