using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Employee.Queries
{
    public class GetAllEmployeeAsyncQuery : IRequest<OperationResult<List<EmployeeEntity>>> { }

    public class GetAllEmployeeAsyncQueryHandler : IRequestHandler<GetAllEmployeeAsyncQuery, OperationResult<List<EmployeeEntity>>>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetAllEmployeeAsyncQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<OperationResult<List<EmployeeEntity>>> Handle(GetAllEmployeeAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<EmployeeEntity>>.Create();

            result.Data = await employeeRepository.GetAllAsync();

            return result;
        }
    }
}
