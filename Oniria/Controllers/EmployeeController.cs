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
    [Authorize(Roles = $"{nameof(ActorsRoles.DOCTOR)},{nameof(ActorsRoles.ASSISTANT)}")]
    public class EmployeeController : BaseController
    {
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
    }
}

