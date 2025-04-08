using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipAcquisition
{
    public class MembershipAcquisitionRepositoy : IMembershipAcquisitionRepositoy
    {
        private readonly ApplicationContext context;

        public MembershipAcquisitionRepositoy(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<MembershipAcquisitionEntity>> GetAllAsync()
        {
            return await context.Set<MembershipAcquisitionEntity>().ToListAsync();
        }

        public async Task<MembershipAcquisitionEntity?> GetByIdAsync(string id)
        {
            return await context.Set<MembershipAcquisitionEntity>().FindAsync(id);
        }

        public async Task<MembershipAcquisitionEntity> CreateAsync(MembershipAcquisitionEntity entity)
        {
            await context.Set<MembershipAcquisitionEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
