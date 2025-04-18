using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipBenefitRelation
{
    public class MembershipBenefitRelationRepository : IMembershipBenefitRelationRepository
    {
        private readonly DbSetWrapper<MembershipBenefitRelationEntity> wrapper;

        public MembershipBenefitRelationRepository(DbSetWrapper<MembershipBenefitRelationEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<MembershipBenefitRelationEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<MembershipBenefitRelationEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(MembershipBenefitRelationEntity entity)
        {
            await wrapper.context.Set<MembershipBenefitRelationEntity>().AddAsync(entity);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MembershipBenefitRelationEntity entity)
        {
            await wrapper.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MembershipBenefitRelationEntity entity)
        {
            wrapper.context.Set<MembershipBenefitRelationEntity>().Remove(entity);
            await wrapper.context.SaveChangesAsync();
        }
    }
}
