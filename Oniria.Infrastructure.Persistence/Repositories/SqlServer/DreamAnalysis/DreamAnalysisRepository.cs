using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.DreamAnalysis
{
    public class DreamAnalysisRepository : IDreamAnalysisRepository
    {
        private readonly DbSetWrapper<DreamAnalysisEntity> wrapper;

        public DreamAnalysisRepository(DbSetWrapper<DreamAnalysisEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task CreateAsync(DreamAnalysisEntity entity)
        {
            await wrapper.context.Set<DreamAnalysisEntity>().AddAsync(entity);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task<List<DreamAnalysisEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<DreamAnalysisEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(DreamAnalysisEntity entity)
        {
            wrapper.context.Set<DreamAnalysisEntity>().Remove(entity);
            await wrapper.context.SaveChangesAsync();
        }
    }
}
