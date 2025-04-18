using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbSetWrapper<EmployeeEntity> wrapper;

        public EmployeeRepository(DbSetWrapper<EmployeeEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task CreateAsync(EmployeeEntity entity)
        {
            await wrapper.context.Set<EmployeeEntity>().AddAsync(entity);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeEntity entity)
        {
            await wrapper.context.SaveChangesAsync();
        }

        public async Task<List<EmployeeEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<EmployeeEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task DeleteAsync(EmployeeEntity entity)
        {
            wrapper.context.Set<EmployeeEntity>().Remove(entity);
            await wrapper.context.SaveChangesAsync();
        }
    }
}
