using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Domain.Enums;
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
                    Identifier = "moises",
                    Password = "272EZcW.H5S38sh"
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
                DreamPrompt = "Anoche tuve un sueño raro, de esos que te dejan una sensación extraña al despertar. Estaba caminando por un bosque oscuro, pero no era un bosque común. Los árboles eran altos, y sus sombras parecían seguirme, como si estuvieran observándome con algo más que simple indiferencia. El aire estaba espeso, denso, y aunque no estaba lloviendo, sentía como si cada gota que caía me pesara más que la anterior.\r\n\r\nNo sabía a dónde iba, pero no podía parar de caminar. Cada vez que intentaba tomar un camino distinto, me encontraba de nuevo en el mismo lugar, como si estuviera atrapado en un ciclo sin fin. Mis pasos resonaban en el vacío, y en algún momento comencé a sentir que ya no tenía fuerzas. Todo parecía empeorar con cada paso que daba; las sombras de los árboles crecían y se alargaban, y sentía como si estuvieran tomando forma humana, como si alguien me estuviera siguiendo, sin decir nada, solo observando.\r\n\r\nDe repente, llegué a un puente antiguo, hecho de piedras mojadas. La niebla era tan densa que no podía ver más allá de unos pocos metros. Decidí cruzarlo, y cuando lo hice, sentí un vacío en el estómago, como si estuviera cayendo, pero no caía, solo flotaba. Y entonces escuché una voz, suave y melancólica, que me decía: \"¿No te has dado cuenta aún? Estás aquí, pero no eres tú. Estás perdido entre lo que eres y lo que sientes.\"\r\n\r\nIntenté responder, pero no pude. Me desperté con esa frase resonando en mi cabeza, y por un momento sentí que la sensación de estar atrapado no era solo parte del sueño, sino algo que llevaba dentro desde hacía tiempo."
            });

            if (!analysisResult.IsSuccess) return Json(analysisResult);

            return Json(new { ok = true, result = analysisResult.Data });
        }
    }
}
