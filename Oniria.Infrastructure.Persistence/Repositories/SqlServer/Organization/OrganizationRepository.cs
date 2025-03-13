using DreamHouse.Infrastructure.Persistence.Contexts;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization
{
    class OrganizationRepository : IOrganizationRepository 
    {
        private readonly ApplicationContext context;

        public OrganizationRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Task<GenderEntity> CreateAsync(GenderEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<GenderEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GenderEntity?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenderEntity> UpdateAsync(GenderEntity entity)
        {
            throw new NotImplementedException();
        }

        Task DeleteAsync<GenderEntity>.UpdateAsync(GenderEntity id)
        {
            return UpdateAsync(id);
        }
    }
}
