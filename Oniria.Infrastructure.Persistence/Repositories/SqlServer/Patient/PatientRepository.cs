using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Patient
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationContext context;

        public  PatientRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<PatientEntity> CreateAsync(PatientEntity entity)
        {
            context.Set<PatientEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(PatientEntity id)
        {
            context.Set<PatientEntity>().Add(id);
            await context.SaveChangesAsync();
        }

        public async Task<List<PatientEntity>> GetAllAsync()
        {
            return await context.Set<PatientEntity>().ToListAsync();
        }

        public async Task<PatientEntity?> GetByIdAsync(string id)
        {
            return await context.Set<PatientEntity>().FindAsync(id);
        }

        public async Task<PatientEntity> UpdateAsync(PatientEntity entity)
        {
            context.Set<PatientEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}