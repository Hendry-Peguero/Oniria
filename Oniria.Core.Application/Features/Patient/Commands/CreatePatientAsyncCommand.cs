using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Application.Features.User.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.Patient.Request;

namespace Oniria.Core.Application.Features.Patient.Commands
{
    public class CreatePatientAsyncCommand : IRequest<OperationResult<PatientEntity>>
    {
        public CreatePatientRequest Request { get; set; }
    }

    public class CreatePatientAsyncCommandHandler : IRequestHandler<CreatePatientAsyncCommand, OperationResult<PatientEntity>>
    {
        private readonly IPatientRepository patientRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreatePatientAsyncCommandHandler(
            IPatientRepository patientRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            this.patientRepository = patientRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<PatientEntity>> Handle(CreatePatientAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<PatientEntity>.Create();
            var request = command.Request;

            var genderResult = await mediator.Send(new GetGenderByIdAsyncQuery { Id = request.GenderId });

            if (!genderResult.IsSuccess)
            {
                result.AddError(genderResult);
                return result;
            }

            var organizationResult = await mediator.Send(new GetOrganizationByIdAsyncQuery { Id = request.OrganizationId });

            if (!organizationResult.IsSuccess)
            {
                result.AddError(organizationResult);
                return result;
            }

            var userResult = await mediator.Send(new GetUserByIdAsyncQuery { UserId = request.UserId });

            if (!userResult.IsSuccess)
            {
                result.AddError(userResult);
                return result;
            }

            var patientToCreate = mapper.Map<PatientEntity>(request);

            try
            {
                await patientRepository.CreateAsync(patientToCreate);
            }
            catch (Exception ex)
            {
                result.AddError("Patient could not be created");
                return result;
            }

            result.Data = patientToCreate;

            return result;
        }
    }
}
