using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;
using Oniria.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.DreamAnalysis
{
    class DreamAnalysisRepository : IDreamAnalysisRepository
    {
        private readonly ApplicationContext context;

        public DreamAnalysisRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<DreamAnalysisEntity> CreateAsync(DreamAnalysisEntity entity)
        {
            await context.Set<DreamAnalysisEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<DreamAnalysisEntity>> GetAllAsync()
        {
            return await context.Set<DreamAnalysisEntity>().ToListAsync();
        }

        public async Task<DreamAnalysisEntity?> GetByIdAsync(string id)
        {
            return await context.Set<DreamAnalysisEntity>().FindAsync(id);
        }
    }
}
