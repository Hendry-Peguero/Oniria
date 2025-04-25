using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.DreamAnalsys.Response;

namespace Oniria.Core.Application.Features.DeepSeek.Commands
{
    public class AnalyzeDreamByPromptAsyncCommand : IRequest<OperationResult<DreamAnalysisDeepSeekResponse>>
    {
        public string DreamPrompt { get; set; }
    }
}
