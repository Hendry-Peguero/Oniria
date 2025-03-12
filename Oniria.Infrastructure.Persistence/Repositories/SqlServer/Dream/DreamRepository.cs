using DreamHouse.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oniria.Infrastructure.Persistence.Repositories
{

    public class DreamRepository : IDreamRepository
    {
        private readonly ApplicationContext context;

        public DreamRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DreamEntity>> GetAllAsync()
        {
            return await this.context.Set<DreamEntity>().ToListAsync();
        }

        public async Task<DreamEntity> GetByIdAsync(string id)
        {
            return await this.context.Set<DreamEntity>().FindAsync(id);
        }

        public async Task<DreamEntity> CreateAsync(DreamEntity dream)
        {
            this.context.Set<DreamEntity>().Add(dream);
            await this.context.SaveChangesAsync();
            return dream;
        }

        Task<List<DreamEntity>> GetAllAsync<DreamEntity>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<DreamEntity?> GetByIdAsync<DreamEntity, string>.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<DreamEntity> CreateAsync<DreamEntity>.CreateAsync(DreamEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
