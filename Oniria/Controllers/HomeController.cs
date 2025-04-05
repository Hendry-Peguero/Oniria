using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Dtos.Email.Request;
using Oniria.Infrastructure.Identity.Features.User.Commands;
using Oniria.Infrastructure.Identity.Features.User.Queries;
using Oniria.Infrastructure.Shared.Features.Email.Commands;
using Oniria.Core.Domain.Enums;
using AutoMapper;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMapper mapper;

        public HomeController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var resultGenres = await Mediator.Send(new GetAllGenderAsyncQuery());
            var resultUsers = await Mediator.Send(new GetAllUsersAsyncQuery());
            var resultUser = await Mediator.Send(new GetUserByIdAsyncQuery() { UserId = "1ok2l-vxztp-yub64-qm7fr-1298z" });

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
                    Identifier = "admin@email.com",
                    Password = "123Pa$$Word!"
                }
            });

            if (!loginResult.IsSuccess) {
                return Json(loginResult);
            }

            var sessionResult = await Mediator.Send(new SetUserSessionAsyncCommand
            {
                User = loginResult.Data!
            });

            return Json(new { OK = true });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser()
        {
            var createResult = await Mediator.Send(new CreateUserAsyncCommand
            {
                Request = new CreateUserRequest
                {
                    Email = "moisesgironarias@gmail.com",
                    UserName = "xd",
                    Password = "123Pa$word!"
                }
            });

            if (!createResult.IsSuccess) return Json(createResult);

            var uriResult = await Mediator.Send(new CreateConfirmationEmailUrlAsyncCommand
            {
                UserId = createResult.Data!.Id
            });

            if (!uriResult.IsSuccess) return Json(uriResult);

            var sendEmailResult = await Mediator.Send(new SendEmailAsyncCommand
            {
                Request = new EmailRequest
                {
                    To = createResult.Data!.Email,
                    Body = $"USER: {createResult.Data.Id} // TOKEN: {uriResult.Data}",
                    Subject = "Solo probando"
                }
            });

            if (!sendEmailResult.IsSuccess) return Json(sendEmailResult);

            return Json(new { OK = true });
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

            return Json(new { OK = true });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail()
        {
            var confirmResult = await Mediator.Send(new ConfirmUserEmailAsyncCommand
            {
                UserId = "c04c082e-db68-4a78-94ef-a3e0100030eb",
                Token = "Q2ZESjhEbG5INDFKb0JaT29NdFNESzZCcnJXaDRrWlRvam9Sd3ZkejVtZjZUUHNvNUIzQUxIbnNpRUo1RlpTKzV5ZldRcFlzZGpqMFduL250dHVaWnhPaUc4T1JMelBzNmdWbjkyNUNVWUFLVXlWb3VnNmVGOGpkOWdFZEorNW9aUXdVVGRYSXhSVGNmdXJ4amZtbXdyeS9DRUR1TjY0WGpQUUVNMXFpelVMSlBVSlB6NGt6dEowRUFZUWJpY0QrdG5tMW9CQUY3QkNURzhmd2EzbDcyQkl5RW1hcktITDJJdDJBcm1Wa3RsaHJiQm1Nak1hSjh3aDFZaFBpeWs0TTkzbmgzUT09"
            });

            if (!confirmResult.IsSuccess) return Json(confirmResult);

            return Json(new { OK = true });
        }
    }
}
