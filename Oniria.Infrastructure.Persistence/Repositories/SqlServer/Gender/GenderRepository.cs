using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Gender
{
    public class GenderRepository : IGenderRepository
    {
        private readonly DbSetWrapper<GenderEntity> wrapper;

        public GenderRepository(DbSetWrapper<GenderEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task<List<GenderEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<GenderEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
