using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Employee.Commands;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Employee.Request;
using Oniria.Extensions;
using Oniria.Helpers;
using Oniria.ViewModels.Employee;

namespace Oniria.Controllers
{
    public class EmployeeController : BaseController
    {
        [Authorize(Roles = $"{nameof(ActorsRoles.DOCTOR)},{nameof(ActorsRoles.ASSISTANT)}")]
        public async Task<IActionResult> Profile()
        {
            var employeeResult = await UserContext.GetUserEmployeeInfo();

            if (employeeResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining employee information");
                return Redirections.HomeRedirection;
            }

            return View(Mapper.Map<EmployeeProfileViewModel>(employeeResult));
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(ActorsRoles.DOCTOR)},{nameof(ActorsRoles.ASSISTANT)}")]
        public async Task<IActionResult> Profile(EmployeeProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var updateResult = await Mediator.Send(new UpdateEmployeeAsyncCommand { Request = Mapper.Map<UpdateEmployeeRequest>(model) });

            if (!updateResult.IsSuccess)
            {
                ModelState.AddGeneralError(updateResult);
            }
            else
            {
                ToastNotification.AddSuccessToastMessage("Successfully updated the employee's information");
            }

            return Redirections.EmployeeProfile;
        }

        [HttpGet]
        [Authorize(Roles = nameof(ActorsRoles.DOCTOR))]
        public IActionResult CreateEmployeeByOrganization() => View(new CreateEmployeeByOrganizationViewModel());

        [HttpPost]
        [Authorize(Roles = nameof(ActorsRoles.DOCTOR))]
        public async Task<IActionResult> CreateEmployeeByOrganization(CreateEmployeeByOrganizationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Obtener organización
            var organization = await UserContext.GetEmployeeOrganizationInfo();

            if (organization is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining important information, contact the administrator");
                return View(model);
            }

            // Mapear el ViewModel a Request y asignar organización
            var request = Mapper.Map<CreateEmployeeByOrganizationRequest>(model);
            request.OrganizationId = organization.Id;

            // Ejecutar el comando principal
            var result = await Mediator.Send(new CreateEmployeeByOrganizationAsyncCommand
            {
                Request = request
            });

            if (!result.IsSuccess)
            {
                ModelState.AddGeneralError(result);
                return View(model);
            }

            ToastNotification.AddSuccessToastMessage("The employee was created");

            return Redirections.HomeRedirection;
        }
    }
}

