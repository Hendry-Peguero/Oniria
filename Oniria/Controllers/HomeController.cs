using Microsoft.AspNetCore.Mvc;

namespace Oniria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
