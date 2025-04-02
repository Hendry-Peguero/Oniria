namespace Oniria.Core.Domain.Interfaces.Repositories.Maintenance
{
    public interface UpdateAsync<Entity>
    {
        abstract Task<Entity> UpdateAsync(Entity entity);
    }
}
