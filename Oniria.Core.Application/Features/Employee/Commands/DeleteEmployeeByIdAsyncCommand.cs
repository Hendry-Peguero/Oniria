using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Employee.Commands
{
    public class DeleteEmployeeByIdAsyncCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
    }

    public class DeleteEmployeeByIdAsyncCommandHandler : IRequestHandler<DeleteEmployeeByIdAsyncCommand, OperationResult>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMediator mediator;

        public DeleteEmployeeByIdAsyncCommandHandler(
            IEmployeeRepository employeeRepository,
            IMediator mediator
        )
        {
            this.employeeRepository = employeeRepository;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(DeleteEmployeeByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var employeeResult = await mediator.Send(new GetEmployeeByIdAsyncQuery { Id = command.Id });

            if (!employeeResult.IsSuccess)
            {
                result.AddError(employeeResult);
                return result;
            }

            try
            {
                await employeeRepository.DeleteAsync(employeeResult.Data!);
            }
            catch (Exception)
            {
                result.AddError("Employee could not be removed");
            }

            return result;
        }
    }
}
