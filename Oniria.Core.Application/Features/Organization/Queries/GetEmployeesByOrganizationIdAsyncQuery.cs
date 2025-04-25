using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.Organization.Queries
{
    public class GetEmployeesByOrganizationIdAsyncQuery : IRequest<OperationResult<List<EmployeeEntity>>>
    {
        public string OrganizationId { get; set; }
    }

    public class GetEmployeesByOrganizationIdAsyncQueryHandler : IRequestHandler<GetEmployeesByOrganizationIdAsyncQuery, OperationResult<List<EmployeeEntity>>>
    {
        private readonly IMediator mediator;

        public GetEmployeesByOrganizationIdAsyncQueryHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<OperationResult<List<EmployeeEntity>>> Handle(GetEmployeesByOrganizationIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<EmployeeEntity>>.Create();
            var employeesResult = await mediator.Send(new GetAllEmployeeAsyncQuery());

            if (!employeesResult.IsSuccess)
            {
                result.AddError(employeesResult);
                return result;
            }

            result.Data = employeesResult.Data!.Where(e => e.OrganizationId == request.OrganizationId).ToList();

            return result;
        }
    }
}
