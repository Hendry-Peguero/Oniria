using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipBenefit
{
    public class MembershipBenefitRepository : IMembershipBenefitRepository
    {
        private readonly ApplicationContext context;

        public MembershipBenefitRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<MembershipBenefitEntity>> GetAllAsync()
        {
            return await context.Set<MembershipBenefitEntity>().ToListAsync();
        }

        public async Task<MembershipBenefitEntity?> GetByIdAsync(string id)
        {
            return await context.Set<MembershipBenefitEntity>().FindAsync(id);
        }
    }
}
