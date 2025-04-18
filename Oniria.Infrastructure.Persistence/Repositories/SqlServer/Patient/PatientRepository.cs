using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;

namespace Oniria.Infrastructure.Persistence.Repositories.SqlServer.Patient
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DbSetWrapper<PatientEntity> wrapper;

        public PatientRepository(DbSetWrapper<PatientEntity> wrapper)
        {
            this.wrapper = wrapper;
        }

        public async Task CreateAsync(PatientEntity entity)
        {
            await wrapper.context.Set<PatientEntity>().AddAsync(entity);
            await wrapper.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PatientEntity entity)
        {
            await wrapper.context.SaveChangesAsync();
        }

        public async Task<List<PatientEntity>> GetAllAsync()
        {
            return await wrapper.Query().ToListAsync();
        }

        public async Task<PatientEntity?> GetByIdAsync(string id)
        {
            return await wrapper.Query().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task DeleteAsync(PatientEntity entity)
        {
            wrapper.context.Set<PatientEntity>().Remove(entity);
            await wrapper.context.SaveChangesAsync();
        }
    }
}