using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationContext context;

        public OrganizationRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<OrganizationEntity> CreateAsync(OrganizationEntity entity)
        {
            await context.Set<OrganizationEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<OrganizationEntity> UpdateAsync(OrganizationEntity entity)
        {
            OrganizationEntity? entityToModify = await context.Set<OrganizationEntity>().FindAsync(entity.Id);
            if (entityToModify != null)
            {
                context.Entry(entityToModify).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<List<OrganizationEntity>> GetAllAsync()
        {
            return await context.Set<OrganizationEntity>().ToListAsync();
        }

        public async Task<OrganizationEntity?> GetByIdAsync(string id)
        {
            return await context.Set<OrganizationEntity>().FindAsync(id);
        }

        public async Task DeleteAsync(OrganizationEntity entity)
        {
            context.Set<OrganizationEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
