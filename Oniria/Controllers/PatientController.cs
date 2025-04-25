using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Dream.Command;
using Oniria.Core.Application.Features.Dream.Queries;
using Oniria.Core.Application.Features.DreamAnalysis.Queries;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Application.Features.Organization.Commands;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Application.Features.Patient.Commands;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Dream.Request;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Extensions;
using Oniria.Helpers;
using Oniria.ViewModels.Organization;
using Oniria.ViewModels.Patient;

namespace Oniria.Controllers
{
    public class PatientController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = $"{nameof(ActorsRoles.DOCTOR)},{nameof(ActorsRoles.ASSISTANT)}")]
        public async Task<IActionResult> CreatePatientByOrganization()
        {
            var model = new CreatePatientByOrganizationViewModel
            {
                Genders = await GetGenderAsSelectListAsync()
            };
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = $"{nameof(ActorsRoles.DOCTOR)},{nameof(ActorsRoles.ASSISTANT)}")]
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


        [Authorize(Roles = nameof(ActorsRoles.PATIENT))]
        public IActionResult RegisterDream() => View(new RegisterDreamViewModel());


        [HttpPost]
        [Authorize(Roles = nameof(ActorsRoles.PATIENT))]
        public async Task<IActionResult> RegisterDream(RegisterDreamViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var patientResult = await UserContext.GetUserPatientInfo();

            if (patientResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining patient information");
                return Redirections.HomeRedirection;
            }

            var dreamRequest = Mapper.Map<RegisterDreamRequest>(model);
            dreamRequest.PatientId = patientResult.Id;
            var registerResult = await Mediator.Send(new RegisterDreamAsyncCommand { Request = dreamRequest });

            if (!registerResult.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage(registerResult.LastMessage());
                return View(model);
            }

            ToastNotification.AddSuccessToastMessage("Dream successfully registered");

            return RedirectToAction("Detail", "DreamAnalysis", new { id = registerResult.Data!.DreamAnalysisId });

        }


        [Authorize(Roles = nameof(ActorsRoles.PATIENT))]
        public async Task<IActionResult> Profile()
        {
            var patientResult = await UserContext.GetUserPatientInfo();

            if (patientResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining patient information");
                return Redirections.HomeRedirection;
            }

            var model = Mapper.Map<PatientProfileViewModel>(patientResult);
            model.Genders = await GetGenderAsSelectListAsync();

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = nameof(ActorsRoles.PATIENT))]
        public async Task<IActionResult> Profile(PatientProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var updateResult = await Mediator.Send(new UpdatePatientAsyncCommand { Request = Mapper.Map<UpdatePatientRequest>(model) });

            if (!updateResult.IsSuccess)
            {
                ModelState.AddGeneralError(updateResult);
            }
            else
            {
                ToastNotification.AddSuccessToastMessage("Successfully updated the patient's information.");
            }

            return Redirections.PatientProfile;
        }


        [Authorize(Roles = nameof(ActorsRoles.PATIENT))]
        public async Task<IActionResult> DreamRecords()
        {
            var patientResult = await UserContext.GetUserPatientInfo();
            var model = new List<DreamEntity>();

            if (patientResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining patient information");
                return View(model);
            }

            model = await GetDreamsAsListAsync(patientResult.Id);

            return View(model);
        }


        [Authorize(Roles = nameof(ActorsRoles.PATIENT))]
        public async Task<IActionResult> DreamAnalisysRecords()
        {
            var patientResult = await UserContext.GetUserPatientInfo();
            var model = new List<DreamAnalysisEntity>();

            if (patientResult is null)
            {
                ToastNotification.AddErrorToastMessage("A problem occurred obtaining patient information");
                return View(model);
            }

            model = await GetDreamsAnalysisAsListAsync(patientResult.Id);

            return View(model);
        }


        private async Task<SelectList> GetGenderAsSelectListAsync()
        {
            var data = (await Mediator.Send(new GetAllGenderAsyncQuery())).Data;
            return new SelectList(data, "Id", "Description");
        }

        private async Task<List<DreamEntity>> GetDreamsAsListAsync(string patientId)
        {
            return (await Mediator.Send(new GetAllDreamAsyncQuery())).Data!
                .Where(d => d.PatientId == patientId)
                .ToList();
        }

        private async Task<List<DreamAnalysisEntity>> GetDreamsAnalysisAsListAsync(string patientId)
        {
            return (await Mediator.Send(new GetAllDreamAnalysisAsyncQuery(), a => a.Dream)).Data!
                .Where(d => d.Dream.PatientId == patientId)
                .ToList();
        }
    }
}
