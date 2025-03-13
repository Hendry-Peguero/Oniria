using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Application.Interfaces.Repositories
{
    public interface IDreamAnalysisRepository:
    GetAllAsync<DreamAnalysisEntity>,
    GetByIdAsync<DreamAnalysisEntity, string>,
    CreateAsync<DreamAnalysisEntity>
    {

    
    }
}
