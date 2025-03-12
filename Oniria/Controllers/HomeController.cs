using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;

namespace Oniria.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
