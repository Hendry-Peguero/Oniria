using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.Organization.Request;

namespace Oniria.Core.Application.Features.Organization.Commands
{
    public class UpdateOrganizationAsyncCommand : IRequest<OperationResult<OrganizationEntity>>
    {
        public UpdateOrganizationRequest Request { get; set; }
    }

    public class UpdateOrganizationAsyncCommandHandler : IRequestHandler<UpdateOrganizationAsyncCommand, OperationResult<OrganizationEntity>>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateOrganizationAsyncCommandHandler(
            IOrganizationRepository organizationRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            this.organizationRepository = organizationRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<OrganizationEntity>> Handle(UpdateOrganizationAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<OrganizationEntity>.Create();
            var request = command.Request;
            var organizationToUpdate = await organizationRepository.GetByIdAsync(request.Id);

            if (organizationToUpdate == null)
            {
                result.AddError("The organization to update was not found");
                return result;
            }

            var employeeResult = await mediator.Send(new GetEmployeeByIdAsyncQuery { Id = request.EmployeeOwnerId });

            if (!employeeResult.IsSuccess)
            {
                result.AddError(employeeResult);
                return result;
            }

            organizationToUpdate = mapper.Map<OrganizationEntity>(request);

            try
            {
                await organizationRepository.UpdateAsync(organizationToUpdate);
            }
            catch (Exception ex)
            {
                result.AddError("Organization could not be updated");
                return result;
            }

            result.Data = organizationToUpdate;

            return result;
        }
    }
}
