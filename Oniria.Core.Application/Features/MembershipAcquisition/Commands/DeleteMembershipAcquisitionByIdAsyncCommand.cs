using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.MembershipAcquisition.Queries;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipAcquisition.Commands
{
    public class DeleteMembershipAcquisitionByIdAsyncCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
    }

    public class DeleteMembershipAcquisitionByIdAsyncCommandHandler : IRequestHandler<DeleteMembershipAcquisitionByIdAsyncCommand, OperationResult>
    {
        private readonly IMembershipAcquisitionRepository membershipAcquisitionRepository;
        private readonly IMediator mediator;

        public DeleteMembershipAcquisitionByIdAsyncCommandHandler(
            IMembershipAcquisitionRepository membershipAcquisitionRepository,
            IMediator mediator
        )
        {
            this.membershipAcquisitionRepository = membershipAcquisitionRepository;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(DeleteMembershipAcquisitionByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            var membershipAcquisitionResult = await mediator.Send(new GetMembershipAcquisitionByIdAsyncQuery { Id = command.Id });

            if (!membershipAcquisitionResult.IsSuccess)
            {
                result.AddError(membershipAcquisitionResult);
                return result;
            }

            try
            {
                await membershipAcquisitionRepository.DeleteAsync(membershipAcquisitionResult.Data!);
            }
            catch (Exception ex)
            {
                result.AddError("Membership Acquisition could not be removed");
            }

            return result;
        }
    }
}
