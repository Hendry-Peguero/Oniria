using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{


    public interface IEmployeeRepository :
    GetAllAsync<EmployeeEntity>,
    GetByIdAsync<EmployeeEntity, string>,
    CreateAsync<EmployeeEntity>,
    UpdateAsync<EmployeeEntity>,
    DeleteAsync<EmployeeEntity>
    {

    }
}