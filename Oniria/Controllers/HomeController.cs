using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Gender.Queries;

namespace Oniria.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetAllGenderAsyncQuery());

            return View(result.Data);
        }
    }
}
