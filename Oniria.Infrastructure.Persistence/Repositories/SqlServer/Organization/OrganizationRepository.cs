using Oniria.Infrastructure.Persistence.Contexts;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;
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

        public Task<OrganizationEntity> CreateAsync(OrganizationEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(OrganizationEntity id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrganizationEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationEntity?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationEntity> UpdateAsync(OrganizationEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
