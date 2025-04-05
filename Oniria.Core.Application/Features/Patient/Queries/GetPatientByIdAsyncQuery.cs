using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Patient.Queries
{
    public class GetPatientByIdAsyncQuery : IRequest<OperationResult<PatientEntity>>
    {
        public string Id { get; set; }
    }

    public class GetPatientByIdAsyncQueryHandler : IRequestHandler<GetPatientByIdAsyncQuery, OperationResult<PatientEntity>>
    {
        private readonly IPatientRepository patientRepository;

        public GetPatientByIdAsyncQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<OperationResult<PatientEntity>> Handle(GetPatientByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<PatientEntity>.Create();
            result.Data = await patientRepository.GetByIdAsync(request.Id);
            return result;
        }
    }
}
