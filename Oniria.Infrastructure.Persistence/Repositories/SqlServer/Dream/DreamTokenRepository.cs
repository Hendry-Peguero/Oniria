using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Dream
{
    public class DreamTokenRepository : IDreamTokenRepository
    {
        private readonly DbSetWrapper<DreamTokenEntity> wrapper;

        public DreamTokenRepository(DbSetWrapper<DreamTokenEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task CreateAsync(DreamTokenEntity entity)
        {
            await wrapper.context.Set<DreamTokenEntity>().AddAsync(entity);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task<List<DreamTokenEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<DreamTokenEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(DreamTokenEntity entity)
        {
            await wrapper.context.SaveChangesAsync();
        }
    }
}
