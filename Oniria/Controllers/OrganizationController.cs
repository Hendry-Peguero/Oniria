using Microsoft.AspNetCore.Mvc;

namespace Oniria.Controllers
{
    public class OrganizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
