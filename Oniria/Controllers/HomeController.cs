using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Employee.Commands;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Application.Features.Organization.Commands;
using Oniria.Core.Application.Features.Patient.Commands;
using Oniria.Core.Application.Features.User.Queries;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Employee.Request;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.Helpers;
using Oniria.Infrastructure.Identity.Features.User.Commands;
using Oniria.Infrastructure.Identity.Features.User.Queries;
using Oniria.Infrastructure.Shared.Features.DeepSeek.Commands;

namespace Oniria.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMapper mapper;

        public HomeController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<IActionResult> HomeRedirection()
        {
            return Redirections.GetHomeByUserRole(
                (await Mediator.Send(new GetUserSessionAsyncQuery())).Data?.Roles?.FirstOrDefault()
            );
        }


        public async Task<IActionResult> Index()
        {
            var resultGenres = await Mediator.Send(new GetAllGenderAsyncQuery());
            var resultUsers = await Mediator.Send(new GetAllUsersAsyncQuery());
            var resultUser = await Mediator.Send(new GetUserByIdAsyncQuery() { UserId = "38fcc90e-9e2e-489d-87bb-8018178af366" });

            ViewBag.Genres = resultGenres.Data;
            ViewBag.Users = resultUsers.Data;
            ViewBag.User = resultUser.Data;

            return View();
        }

        public async Task<IActionResult> GetUserSession()
        {
            return Json(await Mediator.Send(new GetUserSessionAsyncQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var loginResult = await Mediator.Send(new LoginUserAsyncCommand
            {
                Request = new AuthenticationRequest
                {
                    Identifier = "moises",
                    Password = "123Pa$word!"
                }
            });

            if (!loginResult.IsSuccess)
            {
                return Json(loginResult);
            }

            var sessionResult = await Mediator.Send(new SetUserSessionAsyncCommand
            {
                User = loginResult.Data!
            });

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser()
        {
            var createResult = await Mediator.Send(new CreateUserAsyncCommand
            {
                Request = new CreateUserRequest
                {
                    Email = "moisesgironarias@gmail.com",
                    UserName = "moises",
                    Password = "123Pa$word!"
                }
            });

            if (!createResult.IsSuccess) return Json(createResult);

            var sendEmailResult = await Mediator.Send(new SendUserConfirmationEmailAsyncCommand
            {
                Email = createResult.Data!.Email
            });

            if (!sendEmailResult.IsSuccess) return Json(sendEmailResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser()
        {
            var editResult = await Mediator.Send(new UpdateUserAsyncCommand
            {
                Request = new UpdateUserRequest
                {
                    Id = "1229c1d0-9ae5-43a7-8f69-aebf09b557e8",
                    Email = "moisesgironarias@gmail.com",
                    UserName = "moises",
                    Password = "123Pa$word!",
                    Status = StatusEntity.ACTIVE
                }
            });

            if (!editResult.IsSuccess) return Json(editResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> RestorePassword()
        {
            var restoreResult = await Mediator.Send(new SendUserPasswordResetEmailAsyncCommand
            {
                Email = "moisesgironarias@gmail.com"
            });

            if (!restoreResult.IsSuccess) return Json(restoreResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeDream()
        {
            var analysisResult = await Mediator.Send(new AnalyzeDreamByPromptAsyncCommand
            {
                DreamPrompt = "Estoy en un edificio viejo con luces parpadeantes. Camino por un pasillo largo buscando una puerta específica, pero cada vez que abro una, solo encuentro más pasillos, todos iguales. Mi celular no tiene señal y cada reloj que veo marca una hora diferente. Empiezo a correr, pero mis pasos se vuelven lentos, como si algo invisible me estuviera arrastrando hacia atrás. Siento que me estoy quedando sin aire, pero no puedo detenerme. Al fondo del pasillo, hay una sombra que me observa inmóvil. No me ataca, pero su presencia me llena de angustia."
            });

            if (!analysisResult.IsSuccess) return Json(analysisResult);

            return Json(new { ok = true, result = analysisResult.Data });
        }





















        [HttpPost]
        public async Task<IActionResult> CreatePatient()
        {
            var createResult = await Mediator.Send(new CreatePatientAsyncCommand
            {
                Request = new CreatePatientRequest
                {
                    Name = "pepe",
                    LastName = "pepe",
                    BornDate = DateTime.Now,
                    PhoneNumber = "+1 000-000-0000",
                    GenderId = "69946032-ecec-43b3-99ef-b3eb93e89fe4",
                    Address = "calle#3",
                    UserId = "38fcc90e-9e2e-489d-87bb-8018178af366",
                    OrganizationId = "#######"
                }
            });

            if (!createResult.IsSuccess) return Json(createResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatient()
        {
            var updateResult = await Mediator.Send(new UpdatePatientAsyncCommand
            {
                Request = new UpdatePatientRequest
                {
                    Id = "",
                    Name = "pepe",
                    LastName = "pepe",
                    BornDate = DateTime.Now,
                    PhoneNumber = "+1 000-000-0000",
                    GenderId = "69946032-ecec-43b3-99ef-b3eb93e89fe4",
                    Address = "calle#3",
                    UserId = "38fcc90e-9e2e-489d-87bb-8018178af366",
                    OrganizationId = "#######",
                    Status = StatusEntity.INACTIVE
                }
            });

            if (!updateResult.IsSuccess) return Json(updateResult);

            return Json(new { ok = true });
        }









        [HttpPost]
        public async Task<IActionResult> CreateOrganization()
        {
            var createResult = await Mediator.Send(new CreateOrganizationAsyncCommand
            {
                Request = new CreateOrganizationRequest
                {
                    Name = "PEPES COMPANY",
                    Address = "calle#4pepe",
                    PhoneNumber = "+1 000-000-0000",
                    EmployeeOwnerld = "##########"
                }
            });

            if (!createResult.IsSuccess) return Json(createResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrganization()
        {
            var updateResult = await Mediator.Send(new UpdateOrganizationAsyncCommand
            {
                Request = new UpdateOrganizationRequest
                {
                    Id = "",
                    Name = "PEPES COMPANY++",
                    Address = "calle#4pepe++",
                    PhoneNumber = "+1 000-000-0000++",
                    EmployeeOwnerld = "##########",
                    Status = StatusEntity.INACTIVE
                }
            });

            if (!updateResult.IsSuccess) return Json(updateResult);

            return Json(new { ok = true });
        }








        [HttpPost]
        public async Task<IActionResult> CreateEmployee()
        {
            var createResult = await Mediator.Send(new CreateEmployeeAsyncCommand
            {
                Request = new CreateEmployeeRequest
                {
                    Dni = "000000000000",
                    Name = "pepito",
                    LastName = "grande",
                    BornDate = DateTime.Now,
                    PhoneNumber = "+1 000-000-0000",
                    Address = "calle#4pepe",
                    UserId = "38fcc90e-9e2e-489d-87bb-8018178af366"
                }
            });

            if (!createResult.IsSuccess) return Json(createResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee()
        {
            var updateResult = await Mediator.Send(new UpdateEmployeeAsyncCommand
            {
                Request = new UpdateEmployeeRequest
                {
                    Id = "",
                    Dni = "000000000000",
                    Name = "pepito",
                    LastName = "grande",
                    BornDate = DateTime.Now,
                    PhoneNumber = "+1 000-000-0000",
                    Address = "calle#4pepe",
                    UserId = "38fcc90e-9e2e-489d-87bb-8018178af366",
                    Status = StatusEntity.INACTIVE
                }
            });

            if (!updateResult.IsSuccess) return Json(updateResult);

            return Json(new { ok = true });
        }
    }
}
