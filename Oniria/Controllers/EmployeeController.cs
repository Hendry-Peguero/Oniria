using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Employee.Commands;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Dtos.Employee.Request;
using System.Threading.Tasks;

namespace Oniria.Controllers
{
    public class EmployeeController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(CreateEmployeeRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await Mediator.Send(new CreateEmployeeAsyncCommand { Request = request });

            if (!result.IsSuccess)
            {
                foreach (var message in result.Messages)
                    ModelState.AddModelError(string.Empty, message);

                return View(request);
            }

            return RedirectToAction("Index", "Employee");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var result = await Mediator.Send(new GetEmployeeByIdAsyncQuery { Id = id });

            if (!result.IsSuccess || result.Data == null)
                return NotFound();

            var employee = result.Data;

            var model = new UpdateEmployeeRequest
            {
                Id = employee.Id,
                Dni = employee.Dni,
                Name = employee.Name,
                LastName = employee.LastName,
                BornDate = employee.BornDate,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                UserId = employee.UserId,
                OrganizationId = employee.OrganizationId,
                Status = employee.Status
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(string id, UpdateEmployeeRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(request);

            var result = await Mediator.Send(new UpdateEmployeeAsyncCommand { Request = request });

            if (!result.IsSuccess)
            {
                foreach (var message in result.Messages)
                    ModelState.AddModelError(string.Empty, message);

                return View(request);
            }

            return RedirectToAction("Index", "Employee");
        }
    }
}
