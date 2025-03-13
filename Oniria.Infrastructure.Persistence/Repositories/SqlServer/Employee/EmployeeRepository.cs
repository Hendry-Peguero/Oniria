using DreamHouse.Infrastructure.Persistence.Contexts;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
