using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Membership
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApplicationContext context;

        public MembershipRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<MembershipEntity>> GetAllAsync()
        {
            return await context.Set<MembershipEntity>().ToListAsync();
        }

        public async Task<MembershipEntity?> GetByIdAsync(string id)
        {
            return await context.Set<MembershipEntity>().FindAsync(id);
        }
    }
}
