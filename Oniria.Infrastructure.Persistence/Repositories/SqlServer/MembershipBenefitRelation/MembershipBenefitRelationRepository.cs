using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipBenefitRelation
{
    public class MembershipBenefitRelationRepository : IMembershipBenefitRelationRepository
    {
        private readonly ApplicationContext context;

        public MembershipBenefitRelationRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<MembershipBenefitRelationEntity>> GetAllAsync()
        {
            return await context.Set<MembershipBenefitRelationEntity>().ToListAsync();
        }

        public async Task<MembershipBenefitRelationEntity?> GetByIdAsync(string id)
        {
            return await context.Set<MembershipBenefitRelationEntity>().FindAsync(id);
        }
    }
}
