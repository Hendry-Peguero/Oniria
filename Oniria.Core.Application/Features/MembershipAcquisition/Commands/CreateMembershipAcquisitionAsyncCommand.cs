using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Application.Features.Patient.Queries;
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

            if (request.PatientId != null && request.OrganizationId != null)
            {
                result.AddError("You cannot assign a single membership to a patient and an organization");
                return result;
            }

            if (request.PatientId != null)
            {
                var patientResult = await mediator.Send(new GetPatientByIdAsyncQuery { Id = request.PatientId });

                if (!patientResult.IsSuccess)
                {
                    result.AddError(patientResult);
                    return result;
                }
            }

            if (request.OrganizationId != null)
            {
                var organizationResult = await mediator.Send(new GetOrganizationByIdAsyncQuery { Id = request.OrganizationId });

                if (!organizationResult.IsSuccess)
                {
                    result.AddError(organizationResult);
                    return result;
                }
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
