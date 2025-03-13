using Oniria.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.EmotionalStates
{
    public class EmotionalStatesRepository : IEmotionalStatesRepository
    {
        private readonly ApplicationContext context;
        public EmotionalStatesRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<List<EmotionalStatesEntity>> GetAllAsync()
        {
            return await context.Set<EmotionalStatesEntity>().ToListAsync();
        }

        public async Task<EmotionalStatesEntity?> GetByIdAsync(string id)
        {
            return await context.Set<EmotionalStatesEntity>().FindAsync(id);
        }
    }
}
