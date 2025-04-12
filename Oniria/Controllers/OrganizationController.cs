using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;

namespace Oniria.Controllers
{
    public class OrganizationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
