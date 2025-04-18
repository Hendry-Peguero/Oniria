using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Membership
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly DbSetWrapper<MembershipEntity> wrapper;

        public MembershipRepository(DbSetWrapper<MembershipEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<MembershipEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<MembershipEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
