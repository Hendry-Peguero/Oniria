namespace Oniria.Core.Domain.Interfaces.Repositories.Maintenance
{
    public interface CreateAsync<Entity>
    {
        abstract Task<Entity> CreateAsync(Entity entity);
    }
}
