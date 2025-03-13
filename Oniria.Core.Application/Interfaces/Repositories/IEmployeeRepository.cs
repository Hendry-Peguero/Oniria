using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Interfaces.Repositories
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