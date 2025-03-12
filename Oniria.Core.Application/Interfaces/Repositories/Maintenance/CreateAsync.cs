namespace Oniria.Core.Application.Interfaces.Repositories.Maintenance
{
    public interface CreateAsync<Entity>
    {
        abstract Task<Entity> CreateAsync(Entity entity);
    }
}
