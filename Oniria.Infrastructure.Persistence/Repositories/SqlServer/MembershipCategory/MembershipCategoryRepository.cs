using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipCategory
{
    public class MembershipCategoryRepository : IMembershipCategoryRepository
    {
        private readonly ApplicationContext context;

        public MembershipCategoryRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<MembershipCategoryEntity>> GetAllAsync()
        {
            return await context.Set<MembershipCategoryEntity>().ToListAsync();
        }

        public async Task<MembershipCategoryEntity?> GetByIdAsync(string id)
        {
            return await context.Set<MembershipCategoryEntity>().FindAsync(id);
        }
    }
}
