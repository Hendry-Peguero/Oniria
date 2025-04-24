using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Patient.Commands
{
    public class DeletePatientByIdAsyncCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
    }

    public class DeletePatientByIdAsyncCommandHandler : IRequestHandler<DeletePatientByIdAsyncCommand, OperationResult>
    {
        private readonly IPatientRepository patientRepository;
        private readonly IMediator mediator;

        public DeletePatientByIdAsyncCommandHandler(
            IPatientRepository patientRepository,
            IMediator mediator
        )
        {
            this.patientRepository = patientRepository;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(DeletePatientByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            var patientResult = await mediator.Send(new GetPatientByIdAsyncQuery { Id = command .Id });

            if (!patientResult.IsSuccess)
            {
                result.AddError(patientResult);
                return result;
            }

            try
            {
                await patientRepository.DeleteAsync(patientResult.Data!);
            }
            catch(Exception ex)
            {
                result.AddError("Patient could not be removed");
            }

            return result;
        }
    }
}
