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
    public class UpdateEmployeeAsyncCommand : IRequest<OperationResult<EmployeeEntity>>
    {
        public UpdateEmployeeRequest Request { get; set; }
    }

    public class UpdateEmployeeAsyncCommandHandler : IRequestHandler<UpdateEmployeeAsyncCommand, OperationResult<EmployeeEntity>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateEmployeeAsyncCommandHandler(
            IEmployeeRepository employeeRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            this.employeeRepository = employeeRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<EmployeeEntity>> Handle(UpdateEmployeeAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<EmployeeEntity>.Create();
            var request = command.Request;
            var employeeToUpdate = await employeeRepository.GetByIdAsync(request.Id);

            if (employeeToUpdate == null)
            {
                result.AddError("The employee to update was not found");
                return result;
            }

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

            employeeToUpdate = mapper.Map<EmployeeEntity>(request);

            try
            {
                await employeeRepository.UpdateAsync(employeeToUpdate);
            }
            catch (Exception ex)
            {
                result.AddError("Employee could not be updated");
                return result;
            }

            result.Data = employeeToUpdate;

            return result;
        }
    }
}
