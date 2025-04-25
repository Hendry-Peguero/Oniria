using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Organization.Commands;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Extensions;
using Oniria.Helpers;
using Oniria.ViewModels.Organization;

namespace Oniria.Controllers
{
    [Authorize(Roles = nameof(ActorsRoles.DOCTOR))]
    public class OrganizationController : BaseController
    {
        public async Task<IActionResult> EmployeeRecords()
        {
            var organizationResult = await UserContext.GetEmployeeOrganizationInfo();
            var model = new List<EmployeeEntity>();

            if (organizationResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining organizational information");
                return View(model);
            }

            var resultEmployees = await Mediator.Send(new GetEmployeesByOrganizationIdAsyncQuery { OrganizationId = organizationResult.Id });

            if (!resultEmployees.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage("Employees could not be obtained");
                return View(model);
            }

            model = resultEmployees.Data!;

            return View(model);
        }

        public async Task<IActionResult> PatientRecords()
        {
            var organizationResult = await UserContext.GetEmployeeOrganizationInfo();
            var model = new List<PatientEntity>();

            if (organizationResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining organizational information");
                return View(model);
            }

            var resultPatients = await Mediator.Send(new GetAllPatientAsyncQuery());

            if (!resultPatients.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage("Employees could not be obtained");
                return View(model);
            }

            model = resultPatients.Data!.Where(p => p.OrganizationId == organizationResult.Id).ToList();

            return View(model);
        }


        public async Task<IActionResult> Profile()
        {
            var organizationResult = await UserContext.GetEmployeeOrganizationInfo();

            if (organizationResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining organizational information");
                return Redirections.HomeRedirection;
            }

            return View(Mapper.Map<OrganizationProfileViewModel>(organizationResult));
        }

        [HttpPost]
        public async Task<IActionResult> Profile(OrganizationProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var updateResult = await Mediator.Send(new UpdateOrganizationAsyncCommand { Request = Mapper.Map<UpdateOrganizationRequest>(model) });

            if (!updateResult.IsSuccess)
            {
                ModelState.AddGeneralError(updateResult);
            }
            else
            {
                ToastNotification.AddSuccessToastMessage("Successfully updated the organization's information.");
            }

            return Redirections.OrganizationProfile;
        }
    }
}
