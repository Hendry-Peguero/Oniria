using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipCategory
{
    public class MembershipCategoryRepository : IMembershipCategoryRepository
    {
        private readonly DbSetWrapper<MembershipCategoryEntity> wrapper;

        public MembershipCategoryRepository(DbSetWrapper<MembershipCategoryEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<MembershipCategoryEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<MembershipCategoryEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(mc => mc.Id == id);
        }
    }
}
