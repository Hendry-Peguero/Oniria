using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Application.Features.User.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.Employee.Request;

namespace Oniria.Core.Application.Features.Employee.Commands
{
    public class CreateEmployeeAsyncCommand : IRequest<OperationResult<EmployeeEntity>>
    {
        public CreateEmployeeRequest Request { get; set; }
    }

    public class CreateEmployeeAsyncCommandHandler : IRequestHandler<CreateEmployeeAsyncCommand, OperationResult<EmployeeEntity>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateEmployeeAsyncCommandHandler(
            IEmployeeRepository employeeRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            this.employeeRepository = employeeRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<EmployeeEntity>> Handle(CreateEmployeeAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<EmployeeEntity>.Create();
            var request = command.Request;

            // Only if the organization has value
            if (request.OrganizationId != null)
            {
                var organizationResult = await mediator.Send(new GetOrganizationByIdAsyncQuery { Id = request.OrganizationId });

                if (!organizationResult.IsSuccess)
                {
                    result.AddError(organizationResult);
                    return result;
                }
            }

            var userResult = await mediator.Send(new GetUserByIdAsyncQuery { UserId = request.UserId });

            if (!userResult.IsSuccess)
            {
                result.AddError(userResult);
                return result;
            }

            var employeeToCreate = mapper.Map<EmployeeEntity>(request);

            try
            {
                await employeeRepository.CreateAsync(employeeToCreate);
            }
            catch (Exception ex)
            {
                result.AddError("Employee could not be created");
                return result;
            }

            result.Data = employeeToCreate;

            return result;
        }
    }
}
