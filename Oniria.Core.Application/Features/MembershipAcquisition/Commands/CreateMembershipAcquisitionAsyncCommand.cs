using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.User.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.MembershipAcquisition.Request;

namespace Oniria.Core.Application.Features.MembershipAcquisition.Commands
{
    public class CreateMembershipAcquisitionAsyncCommand : IRequest<OperationResult<MembershipAcquisitionEntity>>
    {
        public CreateMembershipAcquisitionRequest Request { get; set; }
    }

    public class CreateMembershipAcquisitionAsyncCommandHandler : IRequestHandler<CreateMembershipAcquisitionAsyncCommand, OperationResult<MembershipAcquisitionEntity>>
    {
        private readonly IMembershipAcquisitionRepository membershipAcquisitionRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateMembershipAcquisitionAsyncCommandHandler(
            IMembershipAcquisitionRepository membershipAcquisitionRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            this.membershipAcquisitionRepository = membershipAcquisitionRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<MembershipAcquisitionEntity>> Handle(CreateMembershipAcquisitionAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<MembershipAcquisitionEntity>.Create();
            var request = command.Request;

            // Validación del usuario
            var userResult = await mediator.Send(new GetUserByIdAsyncQuery { UserId = request.OwnerId });

            if (!userResult.IsSuccess)
            {
                result.AddError(userResult);
                return result;
            }

            var acquisitionToCreate = mapper.Map<MembershipAcquisitionEntity>(request);

            try
            {
                await membershipAcquisitionRepository.CreateAsync(acquisitionToCreate);
            }
            catch (Exception)
            {
                result.AddError("Membership acquisition could not be created");
                return result;
            }

            result.Data = acquisitionToCreate;

            return result;
        }
    }
}
