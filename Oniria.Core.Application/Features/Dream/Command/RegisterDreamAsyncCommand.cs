using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DeepSeek.Commands;
using Oniria.Core.Application.Features.Dream.Commands;
using Oniria.Core.Application.Features.DreamAnalysis.Commands;
using Oniria.Core.Application.Features.DreamToken.Command;
using Oniria.Core.Application.Features.DreamToken.Queries;
using Oniria.Core.Application.Features.EmotionalStates.Queries;
using Oniria.Core.Application.Features.MembershipAcquisition.Queries;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Constants;
using Oniria.Core.Dtos.Dream.Request;
using Oniria.Core.Dtos.Dream.Response;
using Oniria.Core.Dtos.DreamAnalsys.Request;
using Oniria.Core.Dtos.DreamToken.Request;

namespace Oniria.Core.Application.Features.Dream.Command
{
    public class RegisterDreamAsyncCommand : IRequest<OperationResult<RegisterDreamResponse>>
    {
        public RegisterDreamRequest Request { get; set; }
    }

    public class RegisterDreamAsyncCommandHandler : IRequestHandler<RegisterDreamAsyncCommand, OperationResult<RegisterDreamResponse>>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RegisterDreamAsyncCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<RegisterDreamResponse>> Handle(RegisterDreamAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<RegisterDreamResponse>.Create();
            var request = command.Request;

            var membershipAcquisition = (await mediator.Send(new GetAllMembershipAcquisitionAsyncQuery()))
                .Data!
                .FirstOrDefault(dt => dt.PatientId == request.PatientId);

            if (membershipAcquisition is null)
            {
                result.AddError("There is no membership aquisition with this patient");
                return result;
            }

            var dreamToken = (await mediator.Send(new GetAllDreamTokensAsyncQuery()))
                .Data!
                .FirstOrDefault(dt => dt.PatientId == request.PatientId);

            if (dreamToken is null)
            {
                result.AddError("There is no dream token with this patient");
                return result;
            }

            if (dreamToken.Amount is not null)
            {
                if (dreamToken.Amount > 0)
                {
                    dreamToken.Amount -= 1;
                }
                else
                {
                    if (dreamToken.RefreshDate <= DateTime.UtcNow)
                    {
                        dreamToken.Amount = CalculatorHelper.GetAmountTokenByMembership(membershipAcquisition.MembershipId);
                        dreamToken.RefreshDate = DateTime.UtcNow.AddDays(1);
                    }
                    else
                    {
                        result.AddError("The user has no more tokens to continue using.");
                        return result;
                    }
                }
            }

            var analysisResult = await mediator.Send(new AnalyzeDreamByPromptAsyncCommand
            {
                DreamPrompt = request.Prompt
            });

            if (!analysisResult.IsSuccess)
            {
                result.AddError(analysisResult);
                return result;
            }

            var emotionalState = await mediator.Send(new GetEmotionalStateByDescriptionAsyncQuery { Description = analysisResult.Data!.EmotionalState });

            if (!emotionalState.IsSuccess)
            {
                result.AddError(emotionalState);
                return result;
            }

            var createDreamResult = await mediator.Send(new CreateDreamAsyncCommand
            {
                Request = new CreateDreamRequest
                {
                    PatientId = request.PatientId,
                    Prompt = request.Prompt,
                    Title = analysisResult.Data!.DreamTitle
                }
            });

            if (!createDreamResult.IsSuccess)
            {
                result.AddError(createDreamResult);
                return result;
            }

            var createAnalysisResult = await mediator.Send(new CreateDreamAnalysisAsyncCommand
            {
                Request = new CreateDreamAnalysisRequest
                {
                    Title = analysisResult.Data!.AnalysisTitle,
                    DreamId = createDreamResult.Data!.Id,
                    EmotionalStateId = emotionalState.Data!.Id,
                    Recommendation = analysisResult.Data!.Recommendation,
                    PatternBehaviour = analysisResult.Data!.PatternBehaviour
                }
            });

            if (!createAnalysisResult.IsSuccess)
            {
                await DeleteDream(createDreamResult.Data.Id, result);
                result.AddError(createAnalysisResult);
                return result;
            }

            var updateTokenResult = await mediator.Send(new UpdateDreamTokenAsyncCommand
            {
                Request = mapper.Map<UpdateDreamTokenRequest>(dreamToken)
            });

            if (!updateTokenResult.IsSuccess)
            {
                await DeleteDreamAnalysis(createAnalysisResult.Data!.Id, createDreamResult.Data!.Id, result);
                result.AddError(updateTokenResult);
                return result;
            }

            result.Data = new RegisterDreamResponse
            {
                DreamAnalysisId = createAnalysisResult.Data!.Id
            };

            return result;
        }

        // Métodos de rollback

        private async Task DeleteDream(string dreamId, OperationResult<RegisterDreamResponse> mainResult)
        {
            var deleteResult = await mediator.Send(new DeleteDreamByIdAsyncCommand { Id = dreamId });
            if (!deleteResult.IsSuccess)
            {
                mainResult.AddError(deleteResult);
            }
        }

        private async Task DeleteDreamAnalysis(string analysisId, string dreamId, OperationResult<RegisterDreamResponse> mainResult)
        {
            var deleteAnalysisResult = await mediator.Send(new DeleteDreamAnalysisByIdAsyncCommand { Id = analysisId });
            if (!deleteAnalysisResult.IsSuccess)
            {
                mainResult.AddError(deleteAnalysisResult);
            }

            await DeleteDream(dreamId, mainResult);
        }
    }
}
