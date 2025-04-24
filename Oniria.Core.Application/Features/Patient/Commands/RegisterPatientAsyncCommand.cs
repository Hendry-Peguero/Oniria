using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DreamToken.Command;
using Oniria.Core.Application.Features.DreamToken.Commands;
using Oniria.Core.Application.Features.MembershipAcquisition.Commands;
using Oniria.Core.Application.Features.User.Commands;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.DreamToken.Request;
using Oniria.Core.Dtos.MembershipAcquisition.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Core.Application.Features.Patient.Commands
{
    public class RegisterPatientAsyncCommand : IRequest<OperationResult>
    {
        public RegisterPatientRequest Request { get; set; }
    }

    public class RegisterPatientAsyncCommandHandler : IRequestHandler<RegisterPatientAsyncCommand, OperationResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RegisterPatientAsyncCommandHandler(
            IMediator mediator,
            IMapper mapper
        )
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult> Handle(RegisterPatientAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var request = command.Request;

            var userResult = await mediator.Send(new CreateUserAsyncCommand { Request = mapper.Map<CreateUserRequest>(request) });

            if (!userResult.IsSuccess)
            {
                result.AddError(userResult);
                return result;
            }

            var assignRoleResult = await mediator.Send(
                new AssignUserRoleAsyncCommand { UserId = userResult.Data!.Id, Role = ActorsRoles.PATIENT }
            );

            if (!assignRoleResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(assignRoleResult);
                return result;
            }

            var patientRequest = mapper.Map<CreatePatientRequest>(request);
            patientRequest.UserId = userResult.Data!.Id;
            var patientResult = await mediator.Send(new CreatePatientAsyncCommand { Request = patientRequest });

            if (!patientResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(patientResult);
                return result;
            }

            var membershipAcquisitionRequest = new CreateMembershipAcquisitionRequest
            {
                PatientId = patientResult.Data!.Id,
                MembershipId = request.MembershipId,
                AcquisitionDate = DateTime.UtcNow
            };
            var membershipAcquisitionResult = await mediator.Send(new CreateMembershipAcquisitionAsyncCommand { Request = membershipAcquisitionRequest });

            if (!membershipAcquisitionResult.IsSuccess)
            {
                await DeletePatient(patientResult.Data!, userResult.Data!, result);
                result.AddError(membershipAcquisitionResult);
                return result;
            }

            var dreamTokenResult = await mediator.Send(
                new CreateDreamTokenAsyncCommand
                {
                    Request = new CreateDreamTokenRequest
                    {
                        PatientId = patientResult.Data!.Id,
                        Amount = CalculatorHelper.GetAmountTokenByMembership(request.MembershipId),
                        RefreshDate = DateTime.UtcNow
                    }
                }
            );

            if (!dreamTokenResult.IsSuccess)
            {
                await DeleteMembershipAcquisition(
                    membershipAcquisitionResult.Data!,
                    patientResult.Data!,
                    userResult.Data!,
                    result
                );
                result.AddError(dreamTokenResult);
            }

            var sendEmail = await mediator.Send(new SendUserConfirmationEmailAsyncCommand { Email = request.Email });

            if (!sendEmail.IsSuccess)
            {
                await DeleteDreamToken(
                    dreamTokenResult.Data!,
                    membershipAcquisitionResult.Data!,
                    patientResult.Data!,
                    userResult.Data!,
                    result
                );
                result.AddError(sendEmail);
            }

            return result;
        }


        // RollBacks

        private async Task DeleteUser(
            UserResponse userResponse,
            OperationResult mainResult
        )
        {
            var userDeleteResult = await mediator.Send(new DeleteUserByIdAsyncCommand { UserId = userResponse.Id });
            if (!userDeleteResult.IsSuccess)
            {
                mainResult.AddError(userDeleteResult);
            }
        }

        private async Task DeletePatient(
            PatientEntity patientResponse,
            UserResponse userResponse,
            OperationResult mainResult
        )
        {
            var patientDeleteResult = await mediator.Send(new DeletePatientByIdAsyncCommand { Id = patientResponse.Id });
            if (!patientDeleteResult.IsSuccess)
            {
                mainResult.AddError(patientDeleteResult);
            }
            await DeleteUser(userResponse, mainResult);
        }

        private async Task DeleteMembershipAcquisition(
            MembershipAcquisitionEntity membershipAcquisitionResponse,
            PatientEntity patientResponse,
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
            await DeletePatient(patientResponse, userResponse, mainResult);
        }

        private async Task DeleteDreamToken(
            DreamTokenEntity dreamTokenResponse,
            MembershipAcquisitionEntity membershipAcquisitionResponse,
            PatientEntity patientResponse,
            UserResponse userResponse,
            OperationResult mainResult
        )
        {
            var dreamTokenDeleteResult = await mediator.Send(
                new DeleteDreamTokenByIdAsyncCommand { Id = dreamTokenResponse.Id }
            );
            if (!dreamTokenDeleteResult.IsSuccess)
            {
                mainResult.AddError(dreamTokenDeleteResult);
            }
            await DeleteMembershipAcquisition(membershipAcquisitionResponse, patientResponse, userResponse, mainResult);
        }
    }
}

