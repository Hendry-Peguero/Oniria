using DreamHouse.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Repositories
{

    public class DreamRepository : IDreamRepository
    {
        private readonly ApplicationContext context;

        public DreamRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<DreamEntity>> GetAllAsync()
        {
            return await this.context.Set<DreamEntity>().ToListAsync();
        }

        public async Task<DreamEntity?> GetByIdAsync(string id)
        {
            return await this.context.Set<DreamEntity>().FindAsync(id);
        }

        public async Task<DreamEntity> CreateAsync(DreamEntity dream)
        {
            this.context.Set<DreamEntity>().Add(dream);
            await this.context.SaveChangesAsync();
            return dream;
        }
    }
}
