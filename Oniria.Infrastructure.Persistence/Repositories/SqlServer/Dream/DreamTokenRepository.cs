using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;
using Oniria.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Dream
{
    class DreamTokenRepository : IDreamTokenRepository
    {
        private readonly ApplicationContext context;

        public DreamTokenRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<DreamTokenEntity> CreateAsync(DreamTokenEntity entity)
        {
            await context.Set<DreamTokenEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<DreamTokenEntity>> GetAllAsync()
        {
            return await context.Set<DreamTokenEntity>().ToListAsync();
        }

        public async Task<DreamTokenEntity?> GetByIdAsync(string id)
        {
            return await context.Set<DreamTokenEntity>().FindAsync(id);
        }

        public async Task<DreamTokenEntity> UpdateAsync(DreamTokenEntity entity)
        {
            DreamTokenEntity? entityToModify = await context.Set<DreamTokenEntity>().FindAsync(entity.Id);
            if (entityToModify != null)
            {
                context.Entry(entityToModify).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
            return entity;
        }
    }
}
