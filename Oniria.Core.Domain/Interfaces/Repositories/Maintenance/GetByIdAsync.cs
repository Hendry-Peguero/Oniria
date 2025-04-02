namespace Oniria.Core.Domain.Interfaces.Repositories.Maintenance
{
    public interface GetByIdAsync<Entity, TypeId>
    {
        abstract Task<Entity?> GetByIdAsync(TypeId id);
    }
}
