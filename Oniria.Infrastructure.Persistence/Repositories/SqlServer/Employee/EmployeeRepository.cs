using DreamHouse.Infrastructure.Persistence.Contexts;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization
{
    class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext context;

        public EmployeeRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Task<EmployeeEntity> CreateAsync(EmployeeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(EmployeeEntity id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEntity?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEntity> UpdateAsync(EmployeeEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
