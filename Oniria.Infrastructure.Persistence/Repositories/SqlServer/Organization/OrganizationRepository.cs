using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DbSetWrapper<OrganizationEntity> wrapper;

        public OrganizationRepository(DbSetWrapper<OrganizationEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task CreateAsync(OrganizationEntity entity)
        {
            await wrapper.context.Set<OrganizationEntity>().AddAsync(entity);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrganizationEntity entity)
        {
            await wrapper.context.SaveChangesAsync();
        }

        public async Task<List<OrganizationEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<OrganizationEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(org => org.Id == id);
        }

        public async Task DeleteAsync(OrganizationEntity entity)
        {
            wrapper.context.Set<OrganizationEntity>().Remove(entity);
            await wrapper.context.SaveChangesAsync();
        }
    }
}
