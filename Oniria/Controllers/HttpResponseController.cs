using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;

namespace Oniria.Controllers
{
    public class HttpResponseController : BaseController
    {
        public IActionResult UnauthorizedView() => View("HttpResponses/_401");
        public IActionResult ForbiddenView() => View("HttpResponses/_403");
    }
}
