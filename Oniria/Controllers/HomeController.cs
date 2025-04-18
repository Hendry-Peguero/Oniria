using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Helpers;

namespace Oniria.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> HomeRedirection()
        {
            return Redirections.GetHomeByUserRole((await UserContext.GetLoggedUser())?.Roles?.FirstOrDefault());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
