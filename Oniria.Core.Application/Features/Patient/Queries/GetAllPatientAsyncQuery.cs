using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
namespace Oniria.Core.Application.Features.Patient.Queries
{
    public class GetAllPatientAsyncQuery : IRequest<OperationResult<List<PatientEntity>>> { }

    public class GetAllPatientAsyncQueryHandler : IRequestHandler<GetAllPatientAsyncQuery, OperationResult<List<PatientEntity>>>
    {
        private readonly IPatientRepository patientRepository;

        public GetAllPatientAsyncQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<OperationResult<List<PatientEntity>>> Handle(GetAllPatientAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<PatientEntity>>.Create();
            result.Data = await patientRepository.GetAllAsync();
            return result;
        }
    }
}
