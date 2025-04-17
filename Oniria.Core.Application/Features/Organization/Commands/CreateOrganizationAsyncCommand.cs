using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.Organization.Request;

namespace Oniria.Core.Application.Features.Organization.Commands
{
    public class CreateOrganizationAsyncCommand : IRequest<OperationResult<OrganizationEntity>>
    {
        public CreateOrganizationRequest Request { get; set; }
    }

    public class CreateOrganizationAsyncCommandHandler : IRequestHandler<CreateOrganizationAsyncCommand, OperationResult<OrganizationEntity>>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateOrganizationAsyncCommandHandler(
            IOrganizationRepository organizationRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            this.organizationRepository = organizationRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<OrganizationEntity>> Handle(CreateOrganizationAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<OrganizationEntity>.Create();
            var request = command.Request;

            var employeeResult = await mediator.Send(new GetEmployeeByIdAsyncQuery { Id = request.EmployeeOwnerId });

            if (!employeeResult.IsSuccess)
            {
                result.AddError(employeeResult);
                return result;
            }

            var organizationToCreate = mapper.Map<OrganizationEntity>(request);

            try
            {
                await organizationRepository.CreateAsync(organizationToCreate);
            }
            catch (Exception ex)
            {
                result.AddError("Organization could not be created");
                return result;
            }

            result.Data = organizationToCreate;

            return result;
        }
    }
}
