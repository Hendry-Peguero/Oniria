using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Employee.Commands;
using Oniria.Core.Application.Features.MembershipAcquisition.Commands;
using Oniria.Core.Application.Features.User.Commands;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Employee.Request;
using Oniria.Core.Dtos.MembershipAcquisition.Request;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Core.Application.Features.Organization.Commands
{
    public class RegisterOrganizationAsyncCommand : IRequest<OperationResult>
    {
        public RegisterOrganizationRequest Request { get; set; }
    }

    public class RegisterOrganizationAsyncCommandHandler : IRequestHandler<RegisterOrganizationAsyncCommand, OperationResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RegisterOrganizationAsyncCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult> Handle(RegisterOrganizationAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var request = command.Request;

            var userResult = await mediator.Send(new CreateUserAsyncCommand { Request = mapper.Map<CreateUserRequest>(request) });

            if (!userResult.IsSuccess)
            {
                result.AddError(userResult);
                return result;
            }

            var assignRoleResult = await mediator.Send(new AssignUserRoleAsyncCommand
            {
                UserId = userResult.Data!.Id,
                Role = ActorsRoles.DOCTOR
            });

            if (!assignRoleResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(assignRoleResult);
                return result;
            }

            var employeeRequest = mapper.Map<CreateEmployeeRequest>(request);
            employeeRequest.UserId = userResult.Data!.Id;
            var employeeResult = await mediator.Send(new CreateEmployeeAsyncCommand { Request = employeeRequest });

            if (!employeeResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(employeeResult);
                return result;
            }

            var organizationRequest = mapper.Map<CreateOrganizationRequest>(request);
            organizationRequest.EmployeeOwnerId = employeeResult.Data!.Id;
            var organizationResult = await mediator.Send(new CreateOrganizationAsyncCommand { Request = organizationRequest });

            if (!organizationResult.IsSuccess)
            {
                await DeleteEmployee(employeeResult.Data!, userResult.Data!, result);
                result.AddError(organizationResult);
                return result;
            }

            var updateEmployeeRequest = mapper.Map<UpdateEmployeeRequest>(employeeResult.Data!);
            updateEmployeeRequest.OrganizationId = organizationResult.Data!.Id;
            var updateEmployee = await mediator.Send(
                new UpdateEmployeeAsyncCommand { Request = mapper.Map<UpdateEmployeeRequest>(employeeResult.Data!) }
            );

            if (!updateEmployee.IsSuccess)
            {
                await DeleteOrganization(organizationResult.Data!, employeeResult.Data!, userResult.Data!, result);
                result.AddError(updateEmployee);
                return result;
            }

            var acquisitionRequest = new CreateMembershipAcquisitionRequest
            {
                OrganizationId = organizationResult.Data!.Id,
                MembershipId = request.MembershipId,
                AcquisitionDate = DateTime.UtcNow
            };
            var acquisitionResult = await mediator.Send(new CreateMembershipAcquisitionAsyncCommand { Request = acquisitionRequest });

            if (!acquisitionResult.IsSuccess)
            {
                await DeleteOrganization(organizationResult.Data!, employeeResult.Data!, userResult.Data!, result);
                result.AddError(acquisitionResult);
                return result;
            }

            var sendEmail = await mediator.Send(new SendUserConfirmationEmailAsyncCommand { Email = request.Email });

            if (!sendEmail.IsSuccess)
            {
                await DeleteMembershipAcquisition(
                    acquisitionResult.Data!,
                    organizationResult.Data!,
                    employeeResult.Data!,
                    userResult.Data!,
                    result
                );
                result.AddError(sendEmail);
            }

            return result;
        }

        // Rollbacks

        private async Task DeleteUser(UserResponse user, OperationResult result)
        {
            var deleteResult = await mediator.Send(new DeleteUserByIdAsyncCommand { UserId = user.Id });
            if (!deleteResult.IsSuccess) result.AddError(deleteResult);
        }

        private async Task DeleteEmployee(EmployeeEntity employee, UserResponse user, OperationResult result)
        {
            var deleteResult = await mediator.Send(new DeleteEmployeeByIdAsyncCommand { Id = employee.Id });
            if (!deleteResult.IsSuccess) result.AddError(deleteResult);
            await DeleteUser(user, result);
        }

        private async Task DeleteOrganization(OrganizationEntity org, EmployeeEntity employee, UserResponse user, OperationResult result)
        {
            var deleteResult = await mediator.Send(new DeleteOrganizationByIdAsyncCommand { Id = org.Id });
            if (!deleteResult.IsSuccess) result.AddError(deleteResult);
            await DeleteEmployee(employee, user, result);
        }

        private async Task DeleteMembershipAcquisition(
            MembershipAcquisitionEntity membershipAcquisitionResponse,
            OrganizationEntity organizationResponse,
            EmployeeEntity employeeResponse,
            UserResponse userResponse,
            OperationResult mainResult
        )
        {
            var membershipAcquisitionDeleteResult = await mediator.Send(
                new DeleteMembershipAcquisitionByIdAsyncCommand { Id = membershipAcquisitionResponse.Id }
            );
            if (!membershipAcquisitionDeleteResult.IsSuccess)
            {
                mainResult.AddError(membershipAcquisitionDeleteResult);
            }
            await DeleteOrganization(organizationResponse, employeeResponse, userResponse, mainResult);
        }
    }
}
