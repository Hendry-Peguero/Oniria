using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Infrastructure.Identity.Features.User.Queries;

namespace Oniria.Controllers
{
    public class HomeController : BaseController
    {
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
    }
}
