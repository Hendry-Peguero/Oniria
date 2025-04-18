using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.EmotionalStates
{
    public class EmotionalStatesRepository : IEmotionalStatesRepository
    {
        private readonly DbSetWrapper<EmotionalStatesEntity> wrapper;

        public EmotionalStatesRepository(DbSetWrapper<EmotionalStatesEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<EmotionalStatesEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<EmotionalStatesEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
