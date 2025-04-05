using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Application.Features.Patient.Queries
{    
    public class GetByIdAsyncPatientQuery : IRequest<OperationResult<PatientEntity>>
    {
        public string Id { get; set; }
    }

    public class GetByIdAsyncPatientQueryHandler : IRequestHandler<GetByIdAsyncPatientQuery, OperationResult<PatientEntity>>
    {
        private readonly IPatientRepository patientRepository;
        public GetByIdAsyncPatientQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public async Task<OperationResult<PatientEntity>> Handle(GetByIdAsyncPatientQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PatientEntity>();
            result.Data = await patientRepository.GetByIdAsync(request.Id);
            return result;
        }
    }
}
