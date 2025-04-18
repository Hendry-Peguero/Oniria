using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipAcquisition
{
    public class MembershipAcquisitionRepository : IMembershipAcquisitionRepository
    {
        private readonly DbSetWrapper<MembershipAcquisitionEntity> wrapper;

        public MembershipAcquisitionRepository(DbSetWrapper<MembershipAcquisitionEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<MembershipAcquisitionEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<MembershipAcquisitionEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(ma => ma.Id == id);
        }

        public async Task CreateAsync(MembershipAcquisitionEntity entity)
        {
            await wrapper.context.Set<MembershipAcquisitionEntity>().AddAsync(entity);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MembershipAcquisitionEntity entity)
        {
            await wrapper.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MembershipAcquisitionEntity entity)
        {
            wrapper.context.Set<MembershipAcquisitionEntity>().Remove(entity);
            await wrapper.context.SaveChangesAsync();
        }
    }
}
