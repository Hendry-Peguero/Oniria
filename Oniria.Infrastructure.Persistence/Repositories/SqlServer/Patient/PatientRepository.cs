using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Patient
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationContext context;

        public PatientRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<PatientEntity> CreateAsync(PatientEntity entity)
        {
            await context.Set<PatientEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<PatientEntity> UpdateAsync(PatientEntity entity)
        {
            PatientEntity? entityToModify = await context.Set<PatientEntity>().FindAsync(entity.Id);
            if (entityToModify != null)
            {
                context.Entry(entityToModify).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<List<PatientEntity>> GetAllAsync()
        {
            return await context.Set<PatientEntity>().ToListAsync();
        }

        public async Task<PatientEntity?> GetByIdAsync(string id)
        {
            return await context.Set<PatientEntity>().FindAsync(id);
        }

        public async Task DeleteAsync(PatientEntity entity)
        {
            context.Set<PatientEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}