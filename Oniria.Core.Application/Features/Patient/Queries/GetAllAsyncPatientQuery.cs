using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
namespace Oniria.Core.Application.Features.Patient.Queries
{
    public class GetAllAsyncPatientQuery : IRequest<OperationResult<List<PatientEntity>>> { }
    public class GetAllAsyncPatientHandler : IRequestHandler<GetAllAsyncPatientQuery, OperationResult<List<PatientEntity>>>
    {
        private readonly IPatientRepository patientRepository;
        public GetAllAsyncPatientHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public async Task<OperationResult<List<PatientEntity>>> Handle(GetAllAsyncPatientQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<PatientEntity>>();
            result.Data = await patientRepository.GetAllAsync();
            return result;
        }
    }
}
