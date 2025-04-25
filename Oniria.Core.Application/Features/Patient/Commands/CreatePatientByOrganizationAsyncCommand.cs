using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DreamToken.Command;
using Oniria.Core.Application.Features.DreamToken.Commands;
using Oniria.Core.Application.Features.MembershipAcquisition.Commands;
using Oniria.Core.Application.Features.User.Commands;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Constants;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.DreamToken.Request;
using Oniria.Core.Dtos.MembershipAcquisition.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Core.Application.Features.Patient.Commands
{
    public class CreatePatientByOrganizationAsyncCommand : IRequest<OperationResult>
    {
        public CreatePatientByOrganizationRequest Request { get; set; }
    }

    public class CreatePatientByOrganizationAsyncCommandHandler : IRequestHandler<CreatePatientByOrganizationAsyncCommand, OperationResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreatePatientByOrganizationAsyncCommandHandler(
            IMediator mediator,
            IMapper mapper
        )
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult> Handle(CreatePatientByOrganizationAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var request = command.Request;

            var userResult = await mediator.Send(new CreateUserAsyncCommand
            {
                Request = mapper.Map<CreateUserRequest>(request)
            });

            if (!userResult.IsSuccess)
            {
                result.AddError(userResult);
                return result;
            }

            var assignRoleResult = await mediator.Send(new AssignUserRoleAsyncCommand
            {
                UserId = userResult.Data!.Id,
                Role = ActorsRoles.PATIENT
            });

            if (!assignRoleResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(assignRoleResult);
                return result;
            }

            var patientRequest = mapper.Map<CreatePatientRequest>(request);
            patientRequest.UserId = userResult.Data!.Id;
            patientRequest.OrganizationId = request.OrganizationId;

            var patientResult = await mediator.Send(new CreatePatientAsyncCommand
            {
                Request = patientRequest
            });

            if (!patientResult.IsSuccess)
            {
                await DeleteUser(userResult.Data!, result);
                result.AddError(patientResult);
                return result;
            }

            var membershipAcquisitionRequest = new CreateMembershipAcquisitionRequest
            {
                PatientId = patientResult.Data!.Id,
                MembershipId = MembershipIdsConstants.PatientBasic,
                AcquisitionDate = DateTime.UtcNow
            };

            var membershipAcquisitionResult = await mediator.Send(new CreateMembershipAcquisitionAsyncCommand
            {
                Request = membershipAcquisitionRequest
            });

            if (!membershipAcquisitionResult.IsSuccess)
            {
                await DeletePatient(patientResult.Data!, userResult.Data!, result);
                result.AddError(membershipAcquisitionResult);
                return result;
            }

            var dreamTokenResult = await mediator.Send(new CreateDreamTokenAsyncCommand
            {
                Request = new CreateDreamTokenRequest
                {
                    PatientId = patientResult.Data!.Id,
                    Amount = CalculatorHelper.GetAmountTokenByMembership(MembershipIdsConstants.PatientBasic),
                    RefreshDate = DateTime.UtcNow
                }
            });

            if (!dreamTokenResult.IsSuccess)
            {
                await DeleteMembershipAcquisition(membershipAcquisitionResult.Data!, patientResult.Data!, userResult.Data!, result);
                result.AddError(dreamTokenResult);
                return result;
            }

            var sendEmail = await mediator.Send(new SendUserConfirmationEmailAsyncCommand
            {
                Email = request.Email
            });

            if (!sendEmail.IsSuccess)
            {
                await DeleteDreamToken(dreamTokenResult.Data!, membershipAcquisitionResult.Data!, patientResult.Data!, userResult.Data!, result);
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

        private async Task DeletePatient(PatientEntity patient, UserResponse userResponse, OperationResult result)
        {
            var deletePatientResult = await mediator.Send(new DeletePatientByIdAsyncCommand { Id = patient.Id });
            if (!deletePatientResult.IsSuccess)
            {
                result.AddError(deletePatientResult);
            }
            await DeleteUser(userResponse, result);
        }

        private async Task DeleteMembershipAcquisition(MembershipAcquisitionEntity acquisition, PatientEntity patient, UserResponse userResponse, OperationResult result)
        {
            var deleteResult = await mediator.Send(new DeleteMembershipAcquisitionByIdAsyncCommand { Id = acquisition.Id });
            if (!deleteResult.IsSuccess)
            {
                result.AddError(deleteResult);
            }
            await DeletePatient(patient, userResponse, result);
        }

        private async Task DeleteDreamToken(DreamTokenEntity token, MembershipAcquisitionEntity acquisition, PatientEntity patient, UserResponse userResponse, OperationResult result)
        {
            var deleteResult = await mediator.Send(new DeleteDreamTokenByIdAsyncCommand { Id = token.Id });
            if (!deleteResult.IsSuccess)
            {
                result.AddError(deleteResult);
            }
            await DeleteMembershipAcquisition(acquisition, patient, userResponse, result);
        }
    }
}
