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
                    Identifier = "admin",
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
                    Id = "1ok2l-vxztp-yub64-qm7fr-1298z",
                    Email = "admin@email.com",
                    UserName = "admin",
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
                    Name = "pepePatient",
                    LastName = "pepePatient",
                    BornDate = DateTime.Now,
                    PhoneNumber = "+1 000-000-0000",
                    GenderId = "70374aeb-5380-46dd-b3d5-88dc4c8056c9",
                    Address = "calle#3",
                    UserId = "a7fd3ce1-444c-4a15-ad4c-4116dd7a3c63",
                    OrganizationId = "523e3dcd-f17b-48b9-831e-63be9db1eced"
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
                    Id = "cd0fe123-b102-47b6-8772-4e9da9fcdedc",
                    Name = "pepe++-",
                    LastName = "pepe++-",
                    BornDate = DateTime.Now,
                    PhoneNumber = "+1 000-000-0000++-",
                    GenderId = "70374aeb-5380-46dd-b3d5-88dc4c8056c9",
                    Address = "calle#3++-",
                    UserId = "a7fd3ce1-444c-4a15-ad4c-4116dd7a3c63",
                    OrganizationId = "523e3dcd-f17b-48b9-831e-63be9db1eced",
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
                    EmployeeOwnerId = "8a6334fb-a7e2-48f7-92e7-3dd7a8fb025f"
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
                    Id = "523e3dcd-f17b-48b9-831e-63be9db1eced",
                    Name = "PEPES COMPANY++",
                    Address = "calle#4pepe++",
                    PhoneNumber = "+1 000-000-0000++",
                    EmployeeOwnerId = "8a6334fb-a7e2-48f7-92e7-3dd7a8fb025f",
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
                    UserId = "a7fd3ce1-444c-4a15-ad4c-4116dd7a3c63"
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
                    Id = "8a6334fb-a7e2-48f7-92e7-3dd7a8fb025f",
                    Dni = "000000000000+",
                    Name = "pepito+",
                    LastName = "grande+",
                    BornDate = DateTime.Now,
                    PhoneNumber = "+1 000-000-0000+",
                    Address = "calle#4pepe+",
                    UserId = "a7fd3ce1-444c-4a15-ad4c-4116dd7a3c63",
                    OrganizationId = "523e3dcd-f17b-48b9-831e-63be9db1eced",
                    Status = StatusEntity.INACTIVE
                }
            });

            if (!updateResult.IsSuccess) return Json(updateResult);

            return Json(new { ok = true });
        }
    }
}
