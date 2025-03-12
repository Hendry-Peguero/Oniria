namespace Oniria.Core.Application.Interfaces.Repositories.Maintenance
{
    public interface GetByIdAsync<Entity, TypeId>
    {
        abstract Task<Entity?> GetByIdAsync(TypeId id);
    }
}
