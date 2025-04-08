using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Employee.Queries
{
    public class GetEmployeeByIdAsyncQuery : IRequest<OperationResult<EmployeeEntity>>
    {
        public string Id { get; set; }
    }

    public class GetEmployeeByIdAsyncQueryHandler : IRequestHandler<GetEmployeeByIdAsyncQuery, OperationResult<EmployeeEntity>>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetEmployeeByIdAsyncQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<OperationResult<EmployeeEntity>> Handle(GetEmployeeByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<EmployeeEntity>.Create();
            var employee = await employeeRepository.GetByIdAsync(request.Id);

            if (employee == null)
            {
                result.AddError("Could not obtain the employee by ID");
            }
            else
            {
                result.Data = employee;
            }

            return result;
        }
    }
}
