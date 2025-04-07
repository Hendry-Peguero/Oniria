using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext context;

        public EmployeeRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeEntity> CreateAsync(EmployeeEntity entity)
        {
            await context.Set<EmployeeEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<EmployeeEntity> UpdateAsync(EmployeeEntity entity)
        {
            var entityToModify = await context.Set<EmployeeEntity>().FindAsync(entity.Id);
            if (entityToModify != null)
            {
                context.Entry(entityToModify).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<List<EmployeeEntity>> GetAllAsync()
        {
            return await context.Set<EmployeeEntity>().ToListAsync();
        }

        public async Task<EmployeeEntity?> GetByIdAsync(string id)
        {
            return await context.Set<EmployeeEntity>().FindAsync(id);
        }

        public async Task DeleteAsync(EmployeeEntity entity)
        {
            context.Set<EmployeeEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
