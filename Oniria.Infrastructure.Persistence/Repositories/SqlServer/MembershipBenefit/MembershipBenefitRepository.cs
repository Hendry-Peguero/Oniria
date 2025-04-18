using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipBenefit
{
    public class MembershipBenefitRepository : IMembershipBenefitRepository
    {
        private readonly DbSetWrapper<MembershipBenefitEntity> wrapper;

        public MembershipBenefitRepository(DbSetWrapper<MembershipBenefitEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<MembershipBenefitEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<MembershipBenefitEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
