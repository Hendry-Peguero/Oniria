using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.User.Commands;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Employee.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Core.Application.Features.Employee.Commands
{
    public class CreateEmployeeByOrganizationAsyncCommand : IRequest<OperationResult>
    {
        public CreateEmployeeByOrganizationRequest Request { get; set; }
    }

    public class CreateEmployeeByOrganizationAsyncCommandHandler : IRequestHandler<CreateEmployeeByOrganizationAsyncCommand, OperationResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateEmployeeByOrganizationAsyncCommandHandler(
            IMediator mediator,
            IMapper mapper
        )
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult> Handle(CreateEmployeeByOrganizationAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var request = command.Request;

            // Crear usuario
            var userResult = await mediator.Send(new CreateUserAsyncCommand
            {
                Request = mapper.Map<CreateUserRequest>(request)
            });

            if (!userResult.IsSuccess)
            {
                result.AddError(userResult);
                return result;
            }

            // Asignar rol de ASSISTANT
            var assignRoleResult = await mediator.Send(new AssignUserRoleAsyncCommand
            {
                UserId = userResult.Data!.Id,
                Role = ActorsRoles.ASSISTANT
            });

            if (!assignRoleResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(assignRoleResult);
                return result;
            }

            // Crear empleado
            var employeeRequest = mapper.Map<CreateEmployeeRequest>(request);
            employeeRequest.UserId = userResult.Data!.Id;
            employeeRequest.OrganizationId = request.OrganizationId;

            var employeeResult = await mediator.Send(new CreateEmployeeAsyncCommand
            {
                Request = employeeRequest
            });

            if (!employeeResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(employeeResult);
                return result;
            }

            // Enviar correo de confirmación
            var sendEmail = await mediator.Send(new SendUserConfirmationEmailAsyncCommand
            {
                Email = request.Email
            });

            if (!sendEmail.IsSuccess)
            {
                // Si falla el email no borramos, solo reportamos el error
                result.AddError(sendEmail);
            }

            return result;
        }

        private async Task DeleteUser(UserResponse userResponse, OperationResult result)
        {
            var deleteUserResult = await mediator.Send(new DeleteUserByIdAsyncCommand { UserId = userResponse.Id });
            if (!deleteUserResult.IsSuccess)
            {
                result.AddError(deleteUserResult);
            }
        }
    }
}
