using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Application.Features.Patient.Commands;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Extensions;
using Oniria.Helpers;
using Oniria.ViewModels.Patient;

namespace Oniria.Controllers
{
    public class PatientController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> CreatePatientByOrganization()
        {
            var model = new CreatePatientByOrganizationViewModel 
            { 
                Genders = await GetGenderAsSelectListAsync()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePatientByOrganization(CreatePatientByOrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genders = await GetGenderAsSelectListAsync();
                return View(model);
            }

            var organization = await UserContext.GetEmployeeOrganizationInfo();
            
            if (organization is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining important information, contact the administrator");
                model.Genders = await GetGenderAsSelectListAsync();
                return View(model);
            }

            var request = Mapper.Map<CreatePatientByOrganizationRequest>(model);
            request.OrganizationId = organization.Id;
            var result = await Mediator.Send(new CreatePatientByOrganizationAsyncCommand { Request = request });

            if (!result.IsSuccess)
            {
                model.Genders = await GetGenderAsSelectListAsync();
                ModelState.AddGeneralError(result);
                return View(model);
            }

            ToastNotification.AddSuccessToastMessage("The patient was created");

            return Redirections.HomeRedirection;
        }


        private async Task<SelectList> GetGenderAsSelectListAsync()
        {
            var data = (await Mediator.Send(new GetAllGenderAsyncQuery())).Data;
            return new SelectList(data, "Id", "Description");
        }
    }
}
