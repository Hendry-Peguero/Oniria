using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oniria.Attributes;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Application.Features.Membership.Queries;
using Oniria.Core.Application.Features.Organization.Commands;
using Oniria.Core.Application.Features.Patient.Commands;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.Extensions;
using Oniria.Helpers;
using Oniria.Infrastructure.Identity.Features.User.Commands;
using Oniria.ViewModels.Auth;

namespace Oniria.Controllers
{
    public class AuthController : BaseController
    {
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public IActionResult Login() => View();


        [HttpPost]
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var loginResult = await Mediator.Send(new LoginUserAsyncCommand { Request = Mapper.Map<AuthenticationRequest>(model) });

            if (!loginResult.IsSuccess)
            {
                ModelState.AddGeneralError(loginResult);
                return View(model);
            }

            return Redirections.HomeRedirection;
        }


        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public IActionResult RegisterType() => View();


        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> RegisterPatient()
        {
            var model = new RegisterPatientViewModel()
            {
                Genders = await GetGenderAsSelectListAsync(),
                Memberships = await GetPatientMembershipsAsListAsync()
            };
            return View(model);
        }


        [HttpPost]
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> RegisterPatient(RegisterPatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genders = await GetGenderAsSelectListAsync();
                model.Memberships = await GetPatientMembershipsAsListAsync();
                return View(model);
            }

            var registerResult = await Mediator.Send(new RegisterPatientAsyncCommand() { Request = Mapper.Map<RegisterPatientRequest>(model) });

            if (!registerResult.IsSuccess)
            {
                model.Genders = await GetGenderAsSelectListAsync();
                model.Memberships = await GetPatientMembershipsAsListAsync();
                ModelState.AddGeneralError(registerResult);
                return View(model);
            }

            ToastNotification.AddSuccessToastMessage("Your patient account was successfully created");

            return Redirections.Login;
        }


        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> RegisterOrganization()
        {
            var model = new RegisterOrganizationViewModel()
            {
                Memberships = await GetOrganizationMembershipsAsListAsync()
            };
            return View(model);
        }


        [HttpPost]
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> RegisterOrganization(RegisterOrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Memberships = await GetOrganizationMembershipsAsListAsync();
                return View(model);
            }

            var registerResult = await Mediator.Send(new RegisterOrganizationAsyncCommand() { Request = Mapper.Map<RegisterOrganizationRequest>(model) });

            if (!registerResult.IsSuccess)
            {
                model.Memberships = await GetOrganizationMembershipsAsListAsync();
                ModelState.AddGeneralError(registerResult);
                return View(model);
            }

            ToastNotification.AddSuccessToastMessage("Your Organization account was successfully created");

            return Redirections.Login;
        }


        [GoHome(GoHomeWhen.USER_OUT_SESSION)]
        public new async Task<IActionResult> SignOut()
        {
            var singOutResult = await Mediator.Send(new SignOutUserAsyncCommand());

            if (!singOutResult.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage(singOutResult.LastMessage());
                return Redirections.HomeRedirection;
            }

            var removeSession = await Mediator.Send(new RemoveUserSessionAsyncCommand());

            if (!removeSession.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage(removeSession.LastMessage());
            }

            return Redirections.HomeRedirection;
        }


        public async Task<IActionResult> ConfirmUserEmail(string userId, string token)
        {
            var confirmResult = await Mediator.Send(new ConfirmUserEmailAsyncCommand
            {
                UserId = userId,
                Token = token
            });

            if (!confirmResult.IsSuccess) return Redirections.Unauthorized;

            return View("Confirmations/_EmailConfirmation");
        }


        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public IActionResult RestorePassword() => View();


        [HttpPost]
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> RestorePassword(RestorePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var restoreResult = await Mediator.Send(new SendUserPasswordResetEmailAsyncCommand { Email = model.Email });

            if (!restoreResult.IsSuccess)
            {
                ModelState.AddGeneralError(restoreResult);
                return View(model);
            }

            ToastNotification.AddSuccessToastMessage("Your password has been successfully reset, please check your email");

            return Redirections.Login;
        }


        public async Task<IActionResult> ConfirmUserPasswordRestore(string userId, string token)
        {
            var confirmResult = await Mediator.Send(new ConfirmRestoreUserPasswordAsyncCommand
            {
                UserId = userId,
                Token = token
            });

            if (!confirmResult.IsSuccess) return Redirections.Unauthorized;

            return View("Confirmations/_ResetPasswordConfirmation", confirmResult.Data!);
        }


        private async Task<SelectList> GetGenderAsSelectListAsync()
        {
            var data = (await Mediator.Send(new GetAllGenderAsyncQuery())).Data;
            return new SelectList(data, "Id", "Description");
        }

        private async Task<List<MembershipEntity>> GetMembershipsAsync()
        {
            return (await Mediator.Send(
                new GetAllMembershipAsyncQuery(),
                "MembershipCategory",
                "BenefitRelations",
                "BenefitRelations.MembershipBenefit"
            )).Data!;
        }

        private async Task<List<MembershipEntity>> GetPatientMembershipsAsListAsync()
        {
            return 
                (await GetMembershipsAsync())
                .Where(m => m.MembershipCategory.Description == MembershipCategoryTypes.Patient.ToString())
                .ToList();
        }

        private async Task<List<MembershipEntity>> GetOrganizationMembershipsAsListAsync()
        {
            return
                (await GetMembershipsAsync())
                .Where(m => m.MembershipCategory.Description == MembershipCategoryTypes.Organization.ToString())
                .ToList();
        }
    }
}
