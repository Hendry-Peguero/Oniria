using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IPatientRepository :
        GetAllAsync<PatientEntity>,
        GetByIdAsync<PatientEntity, string>,
        CreateAsync<PatientEntity>,
        UpdateAsync<PatientEntity>,
        DeleteAsync<PatientEntity>
    {
    }
}
