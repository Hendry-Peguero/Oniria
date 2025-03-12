using DreamHouse.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Gender
{
    public class GenderRepository : IGenderRepository
    {
        private readonly ApplicationContext context;

        public GenderRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<GenderEntity>> GetAllAsync()
        {
            return await context.Set<GenderEntity>().ToListAsync();
        }

        public async Task<GenderEntity?> GetByIdAsync(string id)
        {
            return await context.Set<GenderEntity>().FindAsync(id);
        }
    }
}
