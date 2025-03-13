using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Dream
{
    class DreamTokenRepository : IDreamTokenRepository
    {
        public Task<DreamTokenEntity> CreateAsync(DreamTokenEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<DreamTokenEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DreamTokenEntity?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DreamTokenEntity> UpdateAsync(DreamTokenEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
