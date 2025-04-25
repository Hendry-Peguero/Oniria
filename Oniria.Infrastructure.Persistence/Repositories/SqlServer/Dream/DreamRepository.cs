using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories
{
    public class DreamRepository : IDreamRepository
    {
        private readonly DbSetWrapper<DreamEntity> wrapper;

        public DreamRepository(DbSetWrapper<DreamEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<DreamEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<DreamEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task CreateAsync(DreamEntity dream)
        {
            await wrapper.context.Set<DreamEntity>().AddAsync(dream);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DreamEntity entity)
        {
            wrapper.context.Set<DreamEntity>().Remove(entity);
            await wrapper.context.SaveChangesAsync();
        }
    }
}
