namespace Oniria.Core.Domain.Interfaces.Repositories.Maintenance
{
    public interface CreateAsync<Entity>
    {
        abstract Task CreateAsync(Entity entity);
    }
}
