using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.DreamAnalysis.Queries;
using Oniria.Core.Domain.Enums;
using Oniria.Helpers;

namespace Oniria.Controllers
{
    public class DreamAnalysisController : BaseController
    {
        [Authorize(Roles = nameof(ActorsRoles.PATIENT))]
        public async Task<IActionResult> Detail(string id)
        {
            var dreamAnalysisResult = await Mediator.Send(
                new GetDreamAnalysisByIdAsyncQuery { Id = id },
                da => da.Dream,
                da => da.EmotionalState
            );

            if (!dreamAnalysisResult.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage("An error occurred, it is not possible to view details of an analysis");
                return Redirections.HomeRedirection;
            }

            return View(dreamAnalysisResult.Data!);
        }
    }
}
