using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.DreamAnalysis
{
    class DreamAnalysisRepository : IDreamAnalysisRepository
    {
        public Task<DreamAnalysisEntity> CreateAsync(DreamAnalysisEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<DreamAnalysisEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DreamAnalysisEntity?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
