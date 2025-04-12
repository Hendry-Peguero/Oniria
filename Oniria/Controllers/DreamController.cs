using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Dream.Commands;
using Oniria.Core.Dtos.Dream.Request;
using System.Threading.Tasks;

namespace Oniria.Controllers
{
    public class DreamController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }

        
        public async Task<IActionResult> Create(CreateDreamRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await Mediator.Send(new CreateDreamAsyncCommand { Request = request });

            if (!result.IsSuccess)
            {
                foreach (var message in result.Messages)
                    ModelState.AddModelError(string.Empty, message);

                return View(request);
            }

            return RedirectToAction("Index", "Dream");
        }
    }
}
